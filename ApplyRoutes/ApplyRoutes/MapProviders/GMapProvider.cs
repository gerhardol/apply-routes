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
#if ST_2_1
using ZoneFiveSoftware.Common.Visuals.Fitness.GPS;
#else
using ZoneFiveSoftware.Common.Visuals.Mapping;
#endif
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
        public cache_entry(string u, IMapImageReadyListener l, Rectangle r, int z, IGPSBounds bounds)
        {
            url = u;
            listener = l;
            pixRect = r;
            zoom = z;
            this.bounds = bounds;
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
        public IGPSBounds bounds = null;
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
#if ST_2_1
                ce.listener.NotifyMapImageReady(ce);
#else
                ce.listener.InvalidateRegion(ce.bounds);
#endif
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

        public Bitmap GetMapImage(Rectangle pixRect, IMapImageReadyListener listener, int zoom, string url, out string proj_info, IGPSBounds bounds)
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
            ce = new cache_entry(url, listener, pixRect, zoom, bounds);
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
                string req2 = webBrowser.Url != null ? webBrowser.Url.GetComponents(UriComponents.HttpRequestUrl, UriFormat.UriEscaped) : null;

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

    class GMapProvider :
#if ST_2_1
        IMapProvider
#else
        IMapTileProvider
#endif

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
            m_Proj = new GMapProjection(this);
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

        public int GMZoom(double zoomLevel)
        {
            int zm = (int)Math.Round(MaximumZoom - zoomLevel);
            if (zm < 0) zm = 0;
            int max = (proj_res != null) ? proj_res.Length - 1 : (int)(MaximumZoom - MinimumZoom);
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
                if (gdal_wrapper == null)
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
            Point tlP = m_Proj.MercatorGPSToPixel(center, zoomLevel);
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

                    IGPSLocation c = m_Proj.MercatorPixelToGPS(cpt, zoomLevel);
                    Point cpt2 = m_Proj.MercatorGPSToPixel(c, zoomLevel);
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

                    if (url.LastIndexOf('#') == -1)
                    {
                        url += "#&";
                    }
                    else if (url[url.Length - 1] != '&')
                    {
                        url += "&";
                    }

                    Uri uri = new Uri(url+hash);
                    string theUrl = uri.GetComponents(UriComponents.HttpRequestUrl, UriFormat.UriEscaped);
                    if (theUrl.IndexOf("?") == -1) {
                        theUrl += "?";
                    } else if (theUrl[theUrl.Length-1] != '&') {
                        theUrl += "&";
                    }
                    theUrl += "strp=1" + uri.Fragment;

                    string pi;
                    //Find outer bounds for tile in WGS84 coordinates
                    //used in ST3 when notifying ST to draw tiles
                    //(could be optimized for ST_2_1)
                    Point NWpt = new Point(tl2.X, tl2.Y);
                    IGPSLocation NWgps = m_Proj.MercatorPixelToGPS(NWpt, zoomLevel);
                    Point NEpt = new Point(tl2.X, tl2.Y + size + GMapImageCache.logoSize);
                    IGPSLocation NEgps = m_Proj.MercatorPixelToGPS(NEpt, zoomLevel);
                    Point SEpt = new Point(tl2.X + size, tl2.Y + size + GMapImageCache.logoSize);
                    IGPSLocation SEgps = m_Proj.MercatorPixelToGPS(SEpt, zoomLevel);
                    Point SWpt = new Point(tl2.X + size);
                    IGPSLocation SWgps = m_Proj.MercatorPixelToGPS(SWpt, zoomLevel);
                    IGPSBounds bounds = new GPSBounds(
                        new GPSLocation((float)Math.Max(NWgps.LatitudeDegrees,  NEgps.LatitudeDegrees),
                                        (float)Math.Min(NWgps.LongitudeDegrees, SWgps.LongitudeDegrees)),
                        new GPSLocation((float)Math.Min(SWgps.LatitudeDegrees,  SEgps.LatitudeDegrees),
                                        (float)Math.Max(NEgps.LongitudeDegrees, SEgps.LongitudeDegrees)));

                    Bitmap bmp = imageCache.GetMapImage(new Rectangle(tl2, sz), listener, zoom, theUrl, out pi, bounds);

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

        public Guid Id
        {
            get { return guid; }
        }

        public IMapProjection MapProjection
        {
            get { return m_Proj; }
        }

        public double MaximumZoom
        {
            get { return maxZoom; }
        }

        public double MinimumZoom
        {
            get { return minZoom; }
        }

        public bool SupportsFractionalZoom
        {
            get { return true; }
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

#if ST_2_1
        //A few methods differ ST2/ST3, the ST2 methods are separated
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
                Point tlP = m_Proj.MercatorGPSToPixel(center, zoomLevel);
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
            get { return MaximumZoom; }
        }

        public double MinZoomLevel
        {
            get { return MinimumZoom; }
        }
        public bool FractionalZoom
        {
            get { return SupportsFractionalZoom; }
        }

        #region IMapProjection Members
        public System.Drawing.Point GPSToPixel(ZoneFiveSoftware.Common.Data.GPS.IGPSLocation origin, double zoomLevel, ZoneFiveSoftware.Common.Data.GPS.IGPSLocation gps)
        {
            return m_Proj.GPSToPixel(origin, zoomLevel, gps);
        }
        public ZoneFiveSoftware.Common.Data.GPS.IGPSLocation PixelToGPS(ZoneFiveSoftware.Common.Data.GPS.IGPSLocation origin, double zoomLevel, System.Drawing.Point pixel)
        {
            return m_Proj.PixelToGPS(origin, zoomLevel, pixel);
        }
        #endregion
#endif

        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        private readonly GMapProjection m_Proj;
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
#if ST_2_1
            Object o = GDAL111Wrapper.wrapper.make_wrapper(displayProjection, projection);
            if (o == null)
            {
                o = GDAL11013Wrapper.wrapper.make_wrapper(displayProjection, projection);
            }
            gdal_wrapper = o;
#else
            gdal_wrapper = GDAL113Wrapper.wrapper.make_wrapper(displayProjection, projection);
#endif
        }

        private bool SetProjInfo(string proj_info)
        {
            if (proj_info == proj_attrs) return false;

            proj_attrs = proj_info;
            proj_res = null;
            proj_bounds = null;
            proj_tile = null;

            bool ret = gdal_wrapper != null;
            gdal_wrapper = null;

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
        public float[] proj_bounds;
        public float[] proj_res;
        private float[] proj_tile;
        public Object gdal_wrapper = null;

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
