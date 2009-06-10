/***********************************************************************
    Copyright 2008-2009 Mark Williams

    Licensed under the Apache License, Version 2.0 (the "License");
    you may not use this file except in compliance with the License.
    You may obtain a copy of the License at

        http://www.apache.org/licenses/LICENSE-2.0

    Unless required by applicable law or agreed to in writing, software
    distributed under the License is distributed on an "AS IS" BASIS,
    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
    See the License for the specific language governing permissions and
    limitations under the License. 

    File: ApplyRoutes/MapProviders/GMapProvider.cs
***********************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Visuals.Fitness.GPS;
using System.Drawing;
using ZoneFiveSoftware.Common.Data.GPS;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using mshtml;
using System.Text.RegularExpressions;
using System.Threading;
using System.ComponentModel;
using System.Security.Permissions;
using System.Globalization;

[Guid("3050f669-98b5-11cf-bb82-00aa00bdce0b"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    ComVisible(true),
    ComImport]
interface IHTMLElementRender
{
    void DrawToDC([In] IntPtr hDC);
    void SetDocumentPrinter([In, MarshalAs(UnmanagedType.BStr)] string bstrPrinterName, [In] IntPtr hDC);
};

namespace ApplyRoutesPlugin.MapProviders
{
    class cache_entry
    {
        public cache_entry(string u, IMapImageReadyListener l, Rectangle r, int z)
        {
            url = u;
            listener = l;
            pixRect = r;
            zoom = z;
        }

        public string url;
        public IMapImageReadyListener listener;
        public Rectangle pixRect;
        public int zoom;
        public Bitmap bmp = null;
        public bool done = false;
        public bool loaded = false;
        public int minZoom = -1;
        public int maxZoom = -1;
    };

    class GMapImageCache
    {
        private ReaderWriterLock rwl = new ReaderWriterLock();
        private ManualResetEvent mre = new ManualResetEvent(false);
        private bool aborted;
        public const int logoSize = 32;

        private SortedList<string, cache_entry> cache_map = new SortedList<string,cache_entry>();
        private List<cache_entry> to_do = new List<cache_entry>();

        private int timeout = 15;

        private Thread theThread = null;
        private AsyncOperation operOnMainThread = null;
        private WebBrowser webBrowser;

        public GMapImageCache(string url)
        {
            operOnMainThread = AsyncOperationManager.CreateOperation(null);

            theThread = new Thread(new ThreadStart(delegate() { ImageRenderer(url); }));
            theThread.SetApartmentState(ApartmentState.STA);
            theThread.IsBackground = true;
            theThread.Start();
        }

        private void CallImageReadyListener(object state)
        {
            cache_entry ce = state as cache_entry;
            if (ce != null)
            {
                ce.listener.NotifyMapImageReady(ce);
            }
        }

        private void NotifyMapImageReady(cache_entry ce)
        {
            operOnMainThread.Post(new SendOrPostCallback(CallImageReadyListener), ce);
        }

        public void ClearToDo()
        {
            rwl.AcquireWriterLock(1000);
            foreach (cache_entry ce in to_do)
            {
                cache_map.Remove(ce.url);
            }
            to_do.Clear();
            aborted = true;
            rwl.ReleaseWriterLock();
        }

        public int ToDoSize()
        {
            rwl.AcquireReaderLock(1000);
            int t = to_do.Count;
            rwl.ReleaseReaderLock();
            return t;
        }

        public void Refresh()
        {
            rwl.AcquireWriterLock(1000);
            cache_map.Clear();
            to_do.Clear();
            aborted = true;
            rwl.ReleaseWriterLock();
        }

        public void NudgeQueue()
        {
            if (ToDoSize() != 0)
            {
                Thread.Sleep(1);
            }
        }

        public Bitmap GetMapImage(Rectangle pixRect, IMapImageReadyListener listener, int zoom, string url)
        {
            Bitmap bmp = null;
            cache_entry ce = null;
            rwl.AcquireReaderLock(1000);
            if (cache_map.ContainsKey(url))
            {
                ce = cache_map[url];
                if (ce.done)
                {
                    bmp = ce.bmp;
                }
            }
            rwl.ReleaseReaderLock();
            if (bmp != null)
            {
                return bmp;
            }
            if (ce != null)
            {
                Thread.Sleep(5);
                return null;
            }
            ce = new cache_entry(url, listener, pixRect, zoom);
            rwl.AcquireWriterLock(1000);
            cache_map[url] = ce;
            to_do.Add(ce);
            rwl.ReleaseWriterLock();
            mre.Set();
            return null;
        }

        private bool CheckImgs()
        {
            foreach (HtmlElement elt in webBrowser.Document.Images)
            {
                if (elt.GetAttribute("hideWhileLoading") == "True")
                {
                    if (elt.GetAttribute("loaded") != "True")
                        return false;
                }
            }
            return true;
        }

        private bool inqueue = false;

        private void ProcessQueue()
        {
            if (inqueue)
            {
                return;
            }
            inqueue = true;
            rwl.AcquireReaderLock(1000);
            cache_entry ce = null;
            if (to_do.Count > 0)
            {
                ce = to_do[0];
            }
            rwl.ReleaseReaderLock();
            if (ce == null)
            {
                // mre.Reset();
                // mre.WaitOne();
            }
            else
            {
                DateTime time = DateTime.Now;
                if (webBrowser.Document != null)
                {
                    string hash = ce.url.Substring(ce.url.LastIndexOf("#") + 1);
                    webBrowser.Document.InvokeScript("doGetPage", new Object[] { (Object)hash });
                }
                webBrowser.Navigate(ce.url);

                while (true)
                {
                    Thread.Sleep(0);
                    TimeSpan elapsedTime = DateTime.Now - time;
                    rwl.AcquireReaderLock(1000);
                    bool loaded = ce.loaded;
                    bool a = aborted;
                    if (a)
                    {
                        LockCookie lc = rwl.UpgradeToWriterLock(1000);
                        aborted = false;
                        rwl.DowngradeFromWriterLock(ref lc);
                    }
                    rwl.ReleaseReaderLock();
                    if (a) break;
                    if (loaded && CheckImgs())
                    {
                        DoComplete(ce);
                        break;
                    }

                    if (elapsedTime.Seconds >= timeout)
                    {
                        NotifyMapImageReady(ce);
                        rwl.AcquireWriterLock(1000);
                        if (ce.bmp == null)
                        {
                            cache_map.Remove(ce.url);
                        }
                        to_do.Remove(ce);
                        rwl.ReleaseWriterLock();
                        break;
                    }
                    Application.DoEvents();
                }

            }
            inqueue = false;
        }

        private void DoComplete(cache_entry ce)
        {
            if (webBrowser.Document == null)
            {
                return;
            }
            HtmlElement elt = webBrowser.Document.GetElementById("map");
            if (elt == null)
            {
                return;
            }

            Bitmap bmp = null;
            if (elt.Children.Count == 0)
            {
                webBrowser.Document.InvokeScript("load");
            }
            else
            {
                IHTMLElementRender render = (IHTMLElementRender)elt.DomElement;
                if (render != null)
                {
                    bmp = new Bitmap(ce.pixRect.Width, ce.pixRect.Height);
                    if (true)
                    {
                        Graphics g = Graphics.FromImage(bmp);
                        IntPtr hdc = g.GetHdc();
                        render.DrawToDC(hdc);
                        g.ReleaseHdc(hdc);
                    }
                    else
                    {
                        ((Control)webBrowser).DrawToBitmap(bmp, new Rectangle(0, 0, ce.pixRect.Width, ce.pixRect.Height));
                    }
                }
            }

            rwl.AcquireWriterLock(1000);
            ce.bmp = bmp;
            ce.done = true;
            to_do.Remove(ce);
            if (bmp == null)
            {
                cache_map.Remove(ce.url);
            }
            rwl.ReleaseWriterLock();
            NotifyMapImageReady(ce);
            return;
        }

        private void LoadComplete(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            string url = e.Url.OriginalString;
            if (url.EndsWith("&"))
            {
                url = url.Substring(0, url.LastIndexOf("&"));
            }
            rwl.AcquireReaderLock(1000);
            cache_entry ce = null;
            if (cache_map.ContainsKey(url))
            {
                ce = cache_map[url];
            }
            rwl.ReleaseReaderLock();
            if (ce == null)
            {
                // this request must have been killed.
                return;
            }
            HtmlElement elt = webBrowser.Document.GetElementById("zoom_id");
            if (elt != null) {
                string [] res = elt.InnerText.Split(new char[] { ',' });
                ce.minZoom = Convert.ToInt32(res[0]);
                ce.maxZoom = Convert.ToInt32(res[1]);
            }
            rwl.AcquireWriterLock(1000);
            ce.loaded = true;
            rwl.ReleaseWriterLock();
        }

        private void ImageRenderer(string initUrl)
        {
            GMapBrowser browser = new GMapBrowser(initUrl);
            webBrowser = browser.Browser;
            webBrowser.ScrollBarsEnabled = false;
            webBrowser.BringToFront();
            webBrowser.Show();

            webBrowser.DocumentCompleted += LoadComplete;

            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 200;
            timer.Tick += delegate(object sender, EventArgs e)
            {
                ProcessQueue();
            };

            timer.Start();
            Application.Run();
        }
    }

    class GMapProvider : IMapProvider
    {
        #region IMapProvider Members

        public GMapProvider(string u, string n, Guid id)
        {
            url = u;
            name = n;
            guid = id;
            if (imageCache == null)
            {
                imageCache = new GMapImageCache(url);
            }
            
            enabled = true;
        }

        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        public string Title
        {
            get { return name; }
            set { name = value; }
        }

        private static GMapImageCache imageCache = null;

        public void ClearDownloadQueue()
        {
            imageCache.ClearToDo();
        }

        public int DownloadQueueSize
        {
            get { return imageCache.ToDoSize(); }
        }

        private int GMZoom(double zoomLevel)
        {
            return (int)Math.Round(MaxZoomLevel - zoomLevel);
        }

        public int DrawMap(IMapImageReadyListener listener, Graphics graphics, Rectangle drawRectangle,
            Rectangle clipRectangle, IGPSLocation center, double zoomLevel)
        {
            int zoom = GMZoom(zoomLevel);
            int size = 512;
            int max = 256 << zoom;

            imageCache.NudgeQueue();

            Point tlP = MercatorGPSToPixel(center, zoomLevel);
            tlP.X -= drawRectangle.Width >> 1;
            tlP.Y -= drawRectangle.Height >> 1;
            if (tlP.X < 0) tlP.X = 0;
            if (tlP.Y < 0) tlP.Y = 0;
            Rectangle rawDraw = new Rectangle(tlP, drawRectangle.Size);
            if (rawDraw.Right > max) rawDraw.Width = max - rawDraw.Left;
            if (rawDraw.Bottom > max) rawDraw.Height = max - rawDraw.Top;

            Point aligned_tlP = new Point(tlP.X - tlP.X%size, tlP.Y - tlP.Y%size);
            int nq = 0;
            Size sz = new Size(size, size);
            for (Point tl1 = aligned_tlP; tl1.X < rawDraw.Right; tl1.X += size)
            {
                for (Point tl2 = tl1; tl2.Y < rawDraw.Bottom; tl2.Y += size)
                {
                    Point cpt = new Point(tl2.X+(size/2), tl2.Y+((size+GMapImageCache.logoSize)/2));
                    IGPSLocation c = MercatorPixelToGPS(cpt, zoomLevel);
                    Point cpt2 = MercatorGPSToPixel(c, zoomLevel);
                    if (cpt2.X != cpt.X || cpt2.Y != cpt.Y)
                    {
                        cpt2.X = cpt.X;
                    }
                    string hash = String.Format(NumberFormatInfo.InvariantInfo, "s={0:D},{1:D}&ll={2:F7},{3:F7}&z={4:D}&q={5:D},{6:D}", new object[] {
                        size, size + GMapImageCache.logoSize,
                        (double)c.LatitudeDegrees, (double)c.LongitudeDegrees,
                        zoom,
                        cpt.X, cpt.Y
                        });
                    
                    string theUrl = url + hash;
                    Bitmap bmp = imageCache.GetMapImage(new Rectangle(tl2, sz), listener, zoom, theUrl);
                    if (bmp != null)
                    {
                        Rectangle r = new Rectangle(tl2.X - rawDraw.X + drawRectangle.X,
                            tl2.Y - rawDraw.Y + drawRectangle.Y, size, size);
                        graphics.DrawImage(bmp, r);
                    }
                    else
                    {
                        nq++;
                    }
                }
            }

            return nq;
        }

        public bool FractionalZoom
        {
            get { return false; }
        }

        public Guid Id
        {
            get { return guid; }
        }

        public Rectangle MapImagePixelRect(object mapImage, Rectangle drawRectangle, IGPSLocation center, double zoomLevel)
        {
            cache_entry ce = mapImage as cache_entry;
            if (ce != null && ce.zoom == GMZoom(zoomLevel) && ce.done)
            {
                if (ce.minZoom >= 0 && ce.maxZoom >= 0)
                {
                    minZoom = 25 - ce.maxZoom;
                    maxZoom = 25 - ce.minZoom;
                }
                Point tlP = MercatorGPSToPixel(center, zoomLevel);
                tlP.X -= drawRectangle.Width >> 1;
                tlP.Y -= drawRectangle.Height >> 1;
                if (tlP.X < 0) tlP.X = 0;
                if (tlP.Y < 0) tlP.Y = 0;
                Rectangle r = new Rectangle(ce.pixRect.X - tlP.X + drawRectangle.X,
                            ce.pixRect.Y - tlP.Y + drawRectangle.Y, ce.pixRect.Size.Width, ce.pixRect.Size.Height);
                r.Intersect(drawRectangle);
                return r;
            }
            return Rectangle.Empty;
        }

        public double MaxZoomLevel
        {
            get { return maxZoom; }
        }

        public double MinZoomLevel
        {
            get { return minZoom; }
        }

        public string Name
        {
            get { return name; }
        }

        public void Refresh(Rectangle drawRectangle, IGPSLocation center, double zoomLevel)
        {
            imageCache.Refresh();
        }

        #endregion

        #region IMapProjection Members

        private delegate void InvokeDelegate();
        //private bool untrue = false;

        private Point MercatorGPSToPixel(IGPSLocation where, double zoomLevel)
        {
            int zm = GMZoom(zoomLevel);
            int z1 = 64 << zm;
            int z2 = z1 << 1;

            double x = where.LongitudeDegrees * z2 / 180 + z2;
            double s = Math.Sin(where.LatitudeDegrees * Math.PI / 180);
            double z = 128 << GMZoom(zoomLevel);
            s = Math.Max(s, -.9999);
            s = Math.Min(s, 0.9999);
            double y = z2 - z1*Math.Log((1 + s)/(1 - s))/Math.PI;
            return new Point((int)Math.Round(x), (int)Math.Round(y));
        }

        private IGPSLocation MercatorPixelToGPS(Point where, double zoomLevel)
        {
            double z = 128 << GMZoom(zoomLevel);

            double x = ((where.X) / z - 1) * 180;
            double y = Math.Min(Math.Max(1 - (where.Y) / z, -1), 1) * Math.PI;

            y = (2 * Math.Atan(Math.Exp(y)) - Math.PI / 2) * 180 / Math.PI;

            return new GPSLocation((float)y, (float)x);
        }

        public Point GPSToPixel(IGPSLocation origin, double zoomLevel, IGPSLocation gps)
        {
            Point orgP = MercatorGPSToPixel(origin, zoomLevel);
            Point gpsP = MercatorGPSToPixel(gps, zoomLevel);
            return new Point(gpsP.X - orgP.X, gpsP.Y - orgP.Y);
        }

        public IGPSLocation PixelToGPS(IGPSLocation origin, double zoomLevel, Point pixel)
        {
            Point orgP = MercatorGPSToPixel(origin, zoomLevel);
            Point pos = new Point(orgP.X + pixel.X, orgP.Y + pixel.Y);
            IGPSLocation loc = MercatorPixelToGPS(pos, zoomLevel);
            Point p2 = GPSToPixel(origin, zoomLevel, loc);
            if (p2.X != pixel.X || p2.Y != pixel.Y)
            {
                p2.X = pixel.X;
            }
            return loc;
        }

        #endregion

        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        private bool enabled = true;

        private int minZoom = 1;
        private int maxZoom = 25;
        private string url;
        private string name;
        private Guid guid;
    }
}
