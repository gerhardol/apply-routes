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

    File: ApplyRoutes/Activities/GMapRouteControl.cs
***********************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Data.GPS;
using ZoneFiveSoftware.Common.Data;
using ApplyRoutesPlugin.MapProviders;
using System.Text.RegularExpressions;
using System.Security.Permissions;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using System.Globalization;
using ApplyRoutesPlugin.UI;

namespace ApplyRoutesPlugin.Activities
{
    public partial class GMapRouteControl : UserControl
    {
        public GMapRouteControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            webBrowser.ScriptErrorsSuppressed = true;

            fitToPageBtn.Click += delegate(object sender, EventArgs e)
            {
                changed = true;
                FitToPage();
            };

            selected_guid = guid;
            mapTypePopup.Enabled = false;
            mti = SetupMapTypes();

            mapTypePopup.ButtonClick += delegate(object sender, EventArgs e)
            {
                Plugin.OpenListPopup(Plugin.GetApplication().VisualTheme,
                                     mapTypes,
                                     mapTypePopup, "Title", mti,
                                     delegate(MapTypeInfo newType)
                                     {
                                         SelectProvider(newType);
                                     });
            };

            webBrowser.ObjectForScripting = new ObjectForScriptingClass(this);
            ThemeChanged(Plugin.GetApplication().VisualTheme);
            if (controls == null)
            {
                controls = new List<GMapRouteControl>();
            }
            controls.Add(this);
        }

        private void SelectProvider(MapTypeInfo newType)
        {
            mti = newType;
            mapTypePopup.Text = newType.title;
            Uri url = new Uri(newType.url + "gsmc&");
            selected_guid = guid = newType.guid;
            string req1 = url.GetComponents(UriComponents.HttpRequestUrl, UriFormat.UriEscaped);
            string req2 = webBrowser.Url.GetComponents(UriComponents.HttpRequestUrl, UriFormat.UriEscaped);

            if (req1 != req2)
            {
                ready = false;
                webBrowser.Navigate(url);
            }
            else
            {
                string hash = url.Fragment.Substring(1);
                webBrowser.Document.InvokeScript("doGetPage", new Object[] { (Object)hash });
            }
        }

        public void MapLoaded()
        {
            ready = true;
            mapTypePopup.Enabled = true;
            if (activities != null || routes != null)
            {
                changed = true;
                FitToPage();
            }
        }

        private void FitToPage()
        {
            if (ready && this.Visible && changed)
            {
                changed = false;

                float n = -200, s = 200, e = -200, w = 200;
                webBrowser.Document.InvokeScript("clear_markers");
                IRouteSettings rs = Plugin.GetApplication().SystemPreferences.RouteSettings;
                string linecolor = String.Format("#{0:X2}{1:X2}{2:X2}",
                        rs.RouteColor.R, rs.RouteColor.G, rs.RouteColor.B);
                float weight = rs.RouteWidth;
                float opacity = (float)(rs.RouteColor.A / 255.0);

                if (activities != null)
                {
                    foreach (IActivity a in activities)
                    {
                        if (a.GPSRoute != null) {
                            ActivityInfo ai = ActivityInfoCache.Instance.GetInfo(a);
                            string values = "";
                            foreach (ITimeValueEntry<IGPSPoint> pt in a.GPSRoute)
                            {
                                IGPSPoint p = pt.Value;
                                if (p.LatitudeDegrees < s) s = p.LatitudeDegrees;
                                if (p.LatitudeDegrees > n) n = p.LatitudeDegrees;
                                if (p.LongitudeDegrees < w) w = p.LongitudeDegrees;
                                if (p.LongitudeDegrees > e) e = p.LongitudeDegrees;

                                values += "," + FloatToCoord(p.LatitudeDegrees);
                                values += "," + FloatToCoord(p.LongitudeDegrees);
                            }
                            DateTime start = a.StartTime.ToLocalTime();
                            webBrowser.Document.InvokeScript("add_marker", new Object[] {
                                a.GPSRoute[0].Value.LatitudeDegrees,
                                a.GPSRoute[0].Value.LongitudeDegrees,
                                Properties.Resources.Route_StartTime_Text + start.ToShortDateString() + " " + start.ToShortTimeString(),
                                "start_" + a.ReferenceId
                            });
                            DateTime end = ai.EndTime.ToLocalTime();
                            webBrowser.Document.InvokeScript("add_marker", new Object[] {
                                a.GPSRoute[a.GPSRoute.Count-1].Value.LatitudeDegrees,
                                a.GPSRoute[a.GPSRoute.Count-1].Value.LongitudeDegrees,
                                Properties.Resources.Route_EndTime_Text + end.ToShortDateString() + " " + end.ToShortTimeString(),
                                "end_" + a.ReferenceId
                            });

                            webBrowser.Document.InvokeScript("add_polyline", new Object[] {
                                linecolor, values.Substring(1), weight, opacity, a.ReferenceId });
                        }
                    }
                }
                if (routes != null) {
                    foreach (IRoute r in routes)
                    {
                        if (r.GPSRoute != null)
                        {
                            foreach (ITimeValueEntry<IGPSPoint> pt in r.GPSRoute)
                            {
                                IGPSPoint p = pt.Value;
                                if (p.LatitudeDegrees < s) s = p.LatitudeDegrees;
                                if (p.LatitudeDegrees > n) n = p.LatitudeDegrees;
                                if (p.LongitudeDegrees < w) w = p.LongitudeDegrees;
                                if (p.LongitudeDegrees > e) e = p.LongitudeDegrees;
                            }
                        }
                    }
                }
                if (n > s && e > w)
                {
                    webBrowser.Document.InvokeScript("zoom_to_rect", new Object[] { n, e, s, w });
                }
            }
        }

        public static void ResetMapTypes()
        {
            if (controls != null)
            {
                foreach (GMapRouteControl gmrc in controls)
                {
                    gmrc.SetupMapTypes();
                }
            }
        }

        private MapTypeInfo SetupMapTypes()
        {
            mapTypes = new List<MapTypeInfo>();
            MapTypeInfo curMapType = null, firstMapType = null;
            foreach (GMapProvider mp in ExtendMapProviders.GetMapProviders())
            {
                Match m = Regex.Match(mp.Url, "[#&]t=([-a-zA-Z_0-9]+)&");
                bool isSat = m.Success && m.Groups[1].Value == "SATELLITE";
                if (mp.Enabled || isSat)
                {
                    MapTypeInfo mti = new MapTypeInfo();
                    mti.title = mp.Title;
                    mti.url = mp.Url;
                    mti.guid = mp.Id.ToString();
                    mapTypes.Add(mti);
                    if (firstMapType == null)
                    {
                        firstMapType = mti;
                    }
                    if (mti.guid == selected_guid ||
                        (curMapType == null && isSat))
                    {
                        curMapType = mti;
                    }
                }
            }

            if (curMapType == null)
            {
                curMapType = firstMapType;
            }

            if (curMapType != null)
            {
                guid = selected_guid = curMapType.guid;
                mapTypePopup.Text = curMapType.title;
                webBrowser.Navigate(curMapType.url + "gsmc&");
            }

            return curMapType;
        }

        public void ShowPage()
        {
            if (guid != "" && guid != selected_guid)
            {
                foreach (MapTypeInfo m in mapTypes)
                {
                    if (m.guid == guid)
                    {
                        selected_guid = guid;
                        SelectProvider(m);
                        break;
                    }
                }
            }

            this.Show();
            FitToPage();
        }

        public void ThemeChanged(ITheme visualTheme)
        {
            Plugin.ThemeChanged(fitToPageBtn, visualTheme);
            Plugin.ThemeChanged(mapTypeLbl, visualTheme);
            mapTypePopup.ThemeChanged(visualTheme);
        }

        public void UICultureChanged(System.Globalization.CultureInfo culture)
        {
            Refresh();
        }

        public void RefreshPage()
        {
        
        }

        public IList<IActivity> Activities
        {
            get { return activities; }
            set
            {
                activities = value;
                changed = true;
                FitToPage();
            }
        }

        public IList<IRoute> Routes
        {
            get { return routes; }
            set
            {
                routes = value;
                changed = true;
                FitToPage();
            }
        }

        public string Url
        {
            get { return webBrowser.Url.AbsolutePath; }
            set { webBrowser.Url = new Uri(value); }
        }

        private string FloatToCoord(float c)
        {
            string s = ((double)c).ToString("F7", NumberFormatInfo.InvariantInfo);
            s = s.Trim('0');
            if (s == ".")
            {
                s = "0";
            }
            return s;
        }

        public static string SelectedGuid
        {
            get { return guid; }
            set { guid = value; }
        }

        class MapTypeInfo
        {
            public string Title
            {
                get { return title; }
            }
            public string title;
            public string url;
            public string guid;
        };

        private MapTypeInfo mti = null;
        private static IList<GMapRouteControl> controls = null;
        private IList<MapTypeInfo> mapTypes = null;
        private IList<IActivity> activities = null;
        private IList<IRoute> routes = null;
        private bool ready = false;
        private bool changed = false;
        private string selected_guid = "";
        private static string guid = "";

        static float GetInterpolatedValue(INumericTimeDataSeries s, DateTime when)
        {
            if (s != null)
            {
                ITimeValueEntry<float> value = s.GetInterpolatedValue(when);
                if (value != null)
                {
                    return value.Value;
                }
            }
            return -1e30f;
        }

        internal string RouteInfo(object okey, object otime, object opt)
        {
            if (activities == null) return "";
            string key = okey.ToString();
            foreach (IActivity act in activities)
            {
                if (act.ReferenceId == key)
                {
                    ActivityInfo ai = ActivityInfoCache.Instance.GetInfo(act);
                    if ((otime == null || otime.ToString() == "")&& 
                        (opt == null || opt.ToString() == ""))
                    {
                        return ai.TimeNonPaused.TotalSeconds.ToString();
                    }
                    else
                    {
                        double t;
                        DateTime when;

                        IDistanceDataTrack ddt = act.DistanceMetersTrack;
                        if (ddt == null) ddt = act.GPSRoute.GetDistanceMetersTrack();
                        float delta = 0;

                        if (otime != null && otime.ToString() != "")
                        {
                            t = Convert.ToDouble(otime.ToString());
                            if (t < 0) t = 0;
                            if (t > ai.TimeNonPaused.TotalSeconds)
                            {
                                t = ai.TimeNonPaused.TotalSeconds;
                            }
                            when = ddt.StartTime.AddSeconds(t);
                            foreach (IValueRange<DateTime> nmt in ai.NonMovingTimes)
                            {
                                if (when < nmt.Lower)
                                {
                                    break;
                                }
                                delta += GetInterpolatedValue(ddt, nmt.Upper);
                                delta -= GetInterpolatedValue(ddt, nmt.Lower);
                                when = when.Add(nmt.Upper.Subtract(nmt.Lower));
                            }
                        }
                        else
                        {
                            int pt = Convert.ToInt32(opt.ToString());
                            if (pt < 0) {
                                pt = 0;
                            } else if (pt >= act.GPSRoute.Count) {
                                pt = act.GPSRoute.Count - 1;
                            }
                            when = act.GPSRoute.EntryDateTime(act.GPSRoute[pt]);
                            DateTime npt = when;
                            foreach (IValueRange<DateTime> nmt in ai.NonMovingTimes)
                            {
                                if (when <= nmt.Lower)
                                {
                                    break;
                                }
                                if (when >= nmt.Upper)
                                {
                                    delta += GetInterpolatedValue(ddt, nmt.Upper);
                                    delta -= GetInterpolatedValue(ddt, nmt.Lower);
                                    npt = npt.Subtract(nmt.Upper.Subtract(nmt.Lower));
                                }
                                else
                                {
                                    delta += GetInterpolatedValue(ddt, when);
                                    delta -= GetInterpolatedValue(ddt, nmt.Lower);
                                    npt = npt.Subtract(when.Subtract(nmt.Lower));
                                    break;
                                }
                            }
                            t = npt.Subtract(act.StartTime).TotalSeconds;
                        }
                        
                        float dist = GetInterpolatedValue(ddt,when) - delta;
                        float elev = GetInterpolatedValue(ai.SmoothedElevationTrack,when);
                        float hrt = GetInterpolatedValue(ai.SmoothedHeartRateTrack,when);
                        float pace = GetInterpolatedValue(ai.SmoothedSpeedTrack,when);
                        IGPSPoint where = act.GPSRoute.GetInterpolatedValue(when).Value;
                        if (elev == -1e30f)
                        {
                            elev = where.ElevationMeters;
                        }

                        return
                            String.Format(NumberFormatInfo.InvariantInfo,
                                "{0:F};{1:s};{2:F};{3:F};{4:F};{5:F};{6:F};{7:s};{8:F}",
                                new object[] {
                                    dist,
                                    TimeSpan.FromSeconds(t).ToString(),
                                    where.LatitudeDegrees, where.LongitudeDegrees, elev,
                                    hrt, pace,
                                    when.ToLocalTime().ToString("t"),
                                    t
                                });
                    }
                }
            }
            return "";
        }
    }
    
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public class ObjectForScriptingClass
    {
        public ObjectForScriptingClass(GMapRouteControl control)
        {
            ownerControl = control;
        }

        public void MapLoaded()
        {
            ownerControl.MapLoaded();
        }

        public string RouteInfo(object key, object time, object pt)
        {
            return ownerControl.RouteInfo(key, time, pt);
        }

        public bool wantsMapLoaded = true;
        public bool hasRouteInfo = true;

        private GMapRouteControl ownerControl;
    }
}
