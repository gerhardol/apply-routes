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

namespace ApplyRoutesPlugin.Activities
{
    public partial class GMapRouteControl : UserControl
    {
        public GMapRouteControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            fitToPageBtn.Click += delegate(object sender, EventArgs e)
            {
                FitToPage();
            };

            SortedList<string, string> mapTypes = new SortedList<string, string>();
            string curMapType = "AR - Earth";
            mapTypes[curMapType] = "SATELLITE_3D";
            foreach (GMapProvider mp in ExtendMapProviders.GetMapProviders())
            {
                Match m = Regex.Match(mp.Url, "#t=([a-zA-Z_0-9]+)&$");
                if (m.Success)
                {
                    string type = m.Groups[1].Value;
                    if (mp.Enabled || type == "SATELLITE")
                    {
                        mapTypes[mp.Title] = type;
                        if (type == "SATELLITE")
                        {
                            curMapType = mp.Title;
                        }
                    }
                }
            }

            mapTypePopup.Text = curMapType;
            mapTypePopup.ButtonClick += delegate(object sender, EventArgs e)
            {
                Plugin.OpenListPopup(Plugin.GetApplication().VisualTheme,
                                     mapTypes.Keys,
                                     mapTypePopup, null, curMapType,
                                     delegate(string newType)
                                     {
                                         webBrowser.Document.InvokeScript("doGetPage",
                                             new Object[] { "gsmc&t=" + mapTypes[newType] });
                                         mapTypePopup.Text = curMapType = newType;
                                     });
            };

            webBrowser.ObjectForScripting = new ObjectForScriptingClass(this);

            mapTypePopup.Enabled = false;
            webBrowser.Navigate("http://maps.myosotissp.com/gmroutectrl.html#gsmc&t=SATELLITE");
            ThemeChanged(Plugin.GetApplication().VisualTheme);
        }

        public void MapLoaded()
        {
            ready = true;
            mapTypePopup.Enabled = true;
            if (activities != null || routes != null)
            {
                FitToPage();
            }
        }

        private void FitToPage()
        {
            if (ready)
            {
                float n = -200, s = 200, e = -200, w = 200;
                webBrowser.Document.InvokeScript("clear_markers");
                IRouteSettings rs = Plugin.GetApplication().SystemPreferences.RouteSettings;
                string linecolor = String.Format("#{0:X2}{1:X2}{2:X2}",
                        rs.RouteColor.R, rs.RouteColor.G, rs.RouteColor.B);

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

                                values += "," + ((double)p.LatitudeDegrees).ToString("F7", NumberFormatInfo.InvariantInfo);
                                values += "," + ((double)p.LongitudeDegrees).ToString("F7", NumberFormatInfo.InvariantInfo);
                            }
                            DateTime start = a.StartTime.ToLocalTime();
                            webBrowser.Document.InvokeScript("add_marker", new Object[] {
                                a.GPSRoute[0].Value.LatitudeDegrees,
                                a.GPSRoute[0].Value.LongitudeDegrees,
                                "Start: " + start.ToShortDateString() + " " + start.ToShortTimeString()
                            });
                            DateTime end = ai.EndTime.ToLocalTime();
                            webBrowser.Document.InvokeScript("add_marker", new Object[] {
                                a.GPSRoute[a.GPSRoute.Count-1].Value.LatitudeDegrees,
                                a.GPSRoute[a.GPSRoute.Count-1].Value.LongitudeDegrees,
                                "End: " + end.ToShortDateString() + " " + end.ToShortTimeString()
                            });

                            webBrowser.Document.InvokeScript("add_polyline", new Object[] {
                                linecolor, values.Substring(1) });
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
                if (ready)
                {
                    FitToPage();
                }
            }
        }

        public IList<IRoute> Routes
        {
            get { return routes; }
            set
            {
                routes = value;
                if (ready)
                {
                    FitToPage();
                }
            }
        }

        public string Url
        {
            get { return webBrowser.Url.AbsolutePath; }
            set { webBrowser.Url = new Uri(value); }
        }

        private IList<IActivity> activities = null;
        private IList<IRoute> routes = null;
        private bool ready;
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

        private GMapRouteControl ownerControl;
    }
}
