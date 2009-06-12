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

[GuidAttribute("0000010d-0000-0000-C000-000000000046")]
[InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
[ComImportAttribute()]
public interface IViewObject
{
    void Draw([MarshalAs(UnmanagedType.U4)] int dwDrawAspect, int lindex, IntPtr pvAspect, IntPtr ptd, IntPtr hdcTargetDev, IntPtr hdcDraw, IntPtr lprcBounds, IntPtr lprcWBounds, IntPtr pfnContinue, int dwContinue);
}

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
        public string proj_info = null;
    };

    public class GMapImageCache
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

            mre.Reset();
            theThread = new Thread(new ThreadStart(delegate() { ImageRenderer(url); }));
            theThread.SetApartmentState(ApartmentState.STA);
            theThread.IsBackground = true;
            theThread.Start();
            mre.WaitOne();
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

        public Bitmap GetMapImage(Rectangle pixRect, IMapImageReadyListener listener, int zoom, string url, out string proj_info)
        {
            Bitmap bmp = null;
            cache_entry ce = null;
            proj_info = null;
            rwl.AcquireReaderLock(1000);
            if (cache_map.ContainsKey(url))
            {
                ce = cache_map[url];
                if (ce.done)
                {
                    proj_info = ce.proj_info;
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
            return null;
        }

        public void PageLoaded(object o)
        {
            string s = o.ToString();
            LoadComplete(s);
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
                Uri url = new Uri(ce.url);
                string req1 = url.GetComponents(UriComponents.HttpRequestUrl, UriFormat.UriEscaped);
                string req2 = webBrowser.Url.GetComponents(UriComponents.HttpRequestUrl, UriFormat.UriEscaped);

                if (req1 != req2)
                {
                    webBrowser.Navigate(url);
                }
                else
                {
                    string hash = ce.url.Substring(ce.url.LastIndexOf("#") + 1);
                    webBrowser.Document.InvokeScript("doGetPage", new Object[] { (Object)hash });
                }

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
                    if (loaded)
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
                IHTMLElementRender render = elt.DomElement as IHTMLElementRender;
                bmp = new Bitmap(ce.pixRect.Width, ce.pixRect.Height);
                IViewObject viewobj = webBrowser.Document.DomDocument as IViewObject;
                if (viewobj != null)
                {
                    Graphics g = Graphics.FromImage(bmp);
                    IntPtr hdc = g.GetHdc();
                    viewobj.Draw(8, 1, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, hdc, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, 0);
                    g.ReleaseHdc(hdc);
                }
                else if (render != null)
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

        private void LoadComplete(string url)
        {
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
            elt = webBrowser.Document.GetElementById("proj_info");
            if (elt != null)
            {
                ce.proj_info = elt.InnerText;
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
            webBrowser.ScriptErrorsSuppressed = true;

            webBrowser.ObjectForScripting = new ObjectForScriptingClass(this); 

            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 200;
            timer.Tick += delegate(object sender, EventArgs e)
            {
                ProcessQueue();
            };

            mre.Set();
            timer.Start();
            Application.Run();
        }
    };

    class GMapProvider : IMapProvider
    {
        #region IMapProvider Members
        private static GMapImageCache imageCache = null;
        static readonly object padlock = new object();

        public GMapProvider(string u, string n, Guid id)
        {
            url = u;
            name = n;
            guid = id;
            lock (padlock)
            {
                if (imageCache == null)
                {
                    imageCache = new GMapImageCache(url);
                }
            }

            enabled = true;
        }

        public string Url
        {
            get { return url; }
            set
            {
                url = value;
                imageCache.Refresh();
            }
        }

        public string Title
        {
            get { return name; }
            set { name = value; }
        }

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
            int zm = (int)Math.Round(MaxZoomLevel - zoomLevel);
            if (zm < 0) zm = 0;
            int max = (proj_res != null) ? proj_res.Length - 1 : (int)(MaxZoomLevel - MinZoomLevel);
            if (zm > max) zm = max;
            return zm;
        }

        public int DrawMap(IMapImageReadyListener listener, Graphics graphics, Rectangle drawRectangle,
            Rectangle clipRectangle, IGPSLocation center, double zoomLevel)
        {
            int zoom = GMZoom(zoomLevel);
            int size = 512;
            int maxX, maxY;
            if (proj_bounds != null)
            {
                if (!useGDAL)
                {
                    string msg = "This map requires the GDAL plugin to display in the Routes Panel.";
                    float left = drawRectangle.Left * .8f + drawRectangle.Right * .2f;
                    float right = drawRectangle.Left * .2f + drawRectangle.Right * .8f;
                    float top = drawRectangle.Top * .8f + drawRectangle.Bottom * .2f;
                    float bottom = drawRectangle.Top * .2f + drawRectangle.Bottom * .8f;

                    Font myFont = new Font("Arial", 14);
                    graphics.DrawString(msg, myFont, Brushes.Black, new RectangleF(left, top, right - left, bottom - top));
                    return 0;
                }

                float res = proj_res[zoom];

                maxX = (int)Math.Ceiling((proj_bounds[2] - proj_bounds[0]) / res);
                maxY = (int)Math.Ceiling((proj_bounds[3] - proj_bounds[1]) / res);
            }
            else
            {
                maxX = maxY = 256 << zoom;
            }

            imageCache.NudgeQueue();

            Point offset = new Point(0, 0);
            Point tlP = MercatorGPSToPixel(center, zoomLevel);
            tlP.X -= drawRectangle.Width >> 1;
            tlP.Y -= drawRectangle.Height >> 1;
            if (tlP.X < 0)
            {
                offset.X = -tlP.X;
                tlP.X = 0;
            }

            if (tlP.Y < 0)
            {
                offset.Y = -tlP.Y;
                tlP.Y = 0;
            }

            Rectangle rawDraw = new Rectangle(tlP, drawRectangle.Size);
            if (rawDraw.Right > maxX)
            {
                rawDraw.Width = maxX - rawDraw.Left;
            }
            if (rawDraw.Bottom > maxY)
            {
                rawDraw.Height = maxY - rawDraw.Top;
            }

            Point aligned_tlP = new Point(tlP.X - tlP.X%size, tlP.Y - tlP.Y%size);
            int nq = 0;
            Size sz = new Size(size, size);
            for (Point tl1 = aligned_tlP; tl1.X < rawDraw.Right; tl1.X += size)
            {
                for (Point tl2 = tl1; tl2.Y < rawDraw.Bottom; tl2.Y += size)
                {
                    Point delta = new Point(0,0);
                    Point cpt = new Point(tl2.X+(size/2), tl2.Y+((size+GMapImageCache.logoSize)/2));
                    if (cpt.X >= maxX)
                    {
                        delta.X = cpt.X - maxX + 1;
                        cpt.X -= delta.X;
                    }
                    
                    if (cpt.Y >= maxY)
                    {
                        delta.Y = cpt.Y - maxY + 1;
                        cpt.Y -= delta.Y;
                    }

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
                    
                    Uri uri = new Uri(url+hash);
                    string theUrl = uri.GetComponents(UriComponents.HttpRequestUrl, UriFormat.UriEscaped);
                    if (theUrl.IndexOf("?") == -1) {
                        theUrl += "?";
                    } else if (theUrl[theUrl.Length-1] != '&') {
                        theUrl += "&";
                    }
                    theUrl += "strp" + uri.Fragment;

                    string pi;
                    Bitmap bmp = imageCache.GetMapImage(new Rectangle(tl2, sz), listener, zoom, theUrl, out pi);
                    if (bmp != null)
                    {
                        if (SetProjInfo(pi))
                        {
                            imageCache.Refresh();
                            return 1;
                        }

                        Rectangle s = new Rectangle(delta.X, delta.Y, size-delta.X, size-delta.Y);
                        Rectangle r = new Rectangle(tl2.X - rawDraw.X + drawRectangle.X + offset.X,
                            tl2.Y - rawDraw.Y + drawRectangle.Y + offset.Y, size-delta.X, size-delta.Y);

                        graphics.DrawImage(bmp, r, s, GraphicsUnit.Pixel);
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

        private Point GDALGPSToPixel(IGPSLocation where, double zoomLevel)
        {
            int zm = GMZoom(zoomLevel);
            double x, y;
            float res = proj_res[zm];
            double[] conv = new double[3];
            try
            {
                toMap.TransformPoint(conv, where.LongitudeDegrees, where.LatitudeDegrees, 0);
            }
            catch
            {
                conv[0] = where.LongitudeDegrees;
                conv[1] = where.LatitudeDegrees;
            }
            x = (conv[0] - proj_bounds[0]) / res;
            y = (proj_bounds[3] - conv[1]) / res;

            return new Point((int)Math.Round(x), (int)Math.Round(y));
        }

        private Point MercatorGPSToPixel(IGPSLocation where, double zoomLevel)
        {
            if (useGDAL)
            {
                return GDALGPSToPixel(where, zoomLevel);
            }

            int zm = GMZoom(zoomLevel);
            double x, y;
            double z1 = Math.Pow(2, zm) * 64;
            double z2 = z1 * 2;

            x = where.LongitudeDegrees * z2 / 180 + z2;
            double s = Math.Sin(where.LatitudeDegrees * Math.PI / 180);
            s = Math.Max(s, -.9999);
            s = Math.Min(s, 0.9999);
            y = z2 - z1 * Math.Log((1 + s) / (1 - s)) / Math.PI;
            return new Point((int)Math.Round(x), (int)Math.Round(y));
        }

        private IGPSLocation GDALPixelToGPS(Point where, double zoomLevel)
        {
            int zm = GMZoom(zoomLevel);
            double x, y;
            float res = proj_res[zm];
            double[] conv = new double[3];

            double xin = where.X * res + proj_bounds[0];
            double yin = proj_bounds[3] - where.Y * res;
            try
            {
                toDisplay.TransformPoint(conv, xin, yin, 0);
            }
            catch
            {
            }
            x = conv[0];
            y = conv[1];

            return new GPSLocation((float)y, (float)x);
        }

        private IGPSLocation MercatorPixelToGPS(Point where, double zoomLevel)
        {
            if (useGDAL)
            {
                return GDALPixelToGPS(where, zoomLevel);
            }

            int zm = GMZoom(zoomLevel);
            double x, y;
            double z = 128 * Math.Pow(2, zm);

            x = ((where.X) / z - 1) * 180;
            y = Math.Min(Math.Max(1 - (where.Y) / z, -1), 1) * Math.PI;

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

        private OSGeo.OSR.SpatialReference MakeSR(string proj)
        {
            OSGeo.OSR.SpatialReference sr = null;
            try
            {
                sr = new OSGeo.OSR.SpatialReference("");
                Regex r = new Regex(@"EPSG:(\d+)");
                Match m = r.Match(proj);
                if (m.Success)
                {
                    sr.ImportFromEPSG(Convert.ToInt32(m.Groups[1].Value));
                }
                else
                {
                    sr.ImportFromProj4(proj);
                }
                if (sr.IsProjected() != 0)
                {
                    sr.ExportToProj4(out proj);
                    proj = proj.Replace(" +wktext ", " ");
                    sr = new OSGeo.OSR.SpatialReference("");
//                    sr.SetProjCS("x");
  //                  sr.SetWellKnownGeogCS("WGS84");
                    sr.ImportFromProj4(proj);
                }
            }
            catch
            {
                sr = null;
            }

            return sr;
        }

        private OSGeo.OSR.CoordinateTransformation MakeCT(OSGeo.OSR.SpatialReference sr1, OSGeo.OSR.SpatialReference sr2)
        {
            OSGeo.OSR.CoordinateTransformation ct = null;
            try
            {
                ct = new OSGeo.OSR.CoordinateTransformation(sr1, sr2);
            }
            catch
            {
            }
            return ct;
        }

        private Regex pi_decode = new Regex(@"\{res:\[([-+0-9\.,e]+)\]\s*,\s*" +
                @"bounds:\[([-+0-9\.,e]+)\]\s*,\s*" +
                "projection:\"([^\"]*)\"\\s*,\\s*" +
                "displayProjection:\"([^\"]*)\"\\s*,\\s*" +
                @"tileSize:\[([-+0-9\.,e]+)\]\s*\}$");

        private float[] projArray(string a)
        {
            string[] A = a.Split(',');
            float[] r = new float[A.Length];
            for (int i = 0; i < A.Length; i++)
            {
                r[i] = (float)Convert.ToDouble(A[i], NumberFormatInfo.InvariantInfo);
            }
            return r;
        }

        private void SetGDALProjInfo(string displayProjection, string projection)
        {
            OSGeo.OSR.SpatialReference disp = MakeSR(displayProjection);
            OSGeo.OSR.SpatialReference map = MakeSR(projection);

            if (disp != null && map != null)
            {
                toMap = MakeCT(disp, map);
                toDisplay = MakeCT(map, disp);

                useGDAL = toMap != null && toDisplay != null;
            }
        }

        private bool SetProjInfo(string proj_info)
        {
            if (proj_info == proj_attrs) return false;

            proj_attrs = proj_info;
            proj_res = null;
            proj_bounds = null;
            proj_tile = null;

            bool ret = useGDAL;
            useGDAL = false;

            Match m = pi_decode.Match(proj_info == null ? "" : proj_info);
            if (!m.Success)
            {
                return ret;
            }

            proj_res = projArray(m.Groups[1].Value);
            proj_bounds = projArray(m.Groups[2].Value);
            string projection = m.Groups[3].Value;
            string displayProjection = m.Groups[4].Value;
            proj_tile = projArray(m.Groups[5].Value);

            try
            {
                SetGDALProjInfo(displayProjection, projection);
            }
            catch
            {
            }

            return true;
        }

        private string proj_attrs = null;
        private OSGeo.OSR.CoordinateTransformation toMap,toDisplay;
        private float[] proj_bounds;
        private float[] proj_res;
        private float[] proj_tile;
        private bool useGDAL = false;

        private bool enabled = true;

        private int minZoom = 1;
        private int maxZoom = 25;
        private string url;
        private string name;
        private Guid guid;
    }

    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public class ObjectForScriptingClass
    {
        public ObjectForScriptingClass(GMapImageCache o)
        {
            owner = o;
        }

        public void PageLoaded(object o)
        {
            owner.PageLoaded(o);
        }

        public bool wantsPageLoaded = true;
        public bool wantsMapLoaded = false;

        private GMapImageCache owner;
    }
}
