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
using System.Xml;
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
    using xmlnode = XmlElement;
    using System.IO;

    public partial class GMapRouteControl : UserControl
    {
        private XmlDocument root = null;

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
            mapTypePopup.Enabled = true;
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
            Uri uri = new Uri(newType.url + "gsmc&");
            string theUrl = uri.GetComponents(UriComponents.HttpRequestUrl, UriFormat.UriEscaped);
            if (theUrl.IndexOf("?") == -1)
            {
                theUrl += "?";
            }
            else if (theUrl[theUrl.Length - 1] != '&')
            {
                theUrl += "&";
            }
            theUrl += "arrp=1" + uri.Fragment;
            uri = new Uri(theUrl);
            
            selected_guid = guid = newType.guid;
            string req1 = uri.GetComponents(UriComponents.HttpRequestUrl, UriFormat.UriEscaped);
            string req2 = webBrowser.Url != null ? webBrowser.Url.GetComponents(UriComponents.HttpRequestUrl, UriFormat.UriEscaped) : null;

            if (req1 != req2)
            {
                ready = false;
                webBrowser.Navigate(uri);
            }
            else
            {
                string hash = uri.Fragment.Substring(1);
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

        private xmlnode CreateChild(XmlNode parent, string tag, IList<string> attributes)
        {
            xmlnode elt = null;
            if (tag == "")
            {
                parent.AppendChild(root.CreateTextNode(attributes[0]));
            }
            else
            {
                elt = root.CreateElement(tag);
                if (attributes != null)
                {
                    for (int i = 0; i < attributes.Count; i += 2)
                    {
                        elt.SetAttribute(attributes[i], attributes[i + 1]);
                    }
                }

                if (parent != null)
                {
                    parent.AppendChild(elt);
                }
            }

            return elt;
        }

        private String XmlDateString(DateTime date)
        {
            return string.Format(CultureInfo.InvariantCulture.DateTimeFormat, "{0:s}Z", date);
        }

        private void AddFloatTrack(SortedList<long, IList<string>> trackpts, bool smoothing,
            ITimeDataSeries<float> raw_data, ITimeDataSeries<float> smooth_data, string name)
        {
            ITimeDataSeries<float> data = smoothing && smooth_data != null ? smooth_data : raw_data;

            if (data != null)
            {
                foreach (ITimeValueEntry<float> f in data)
                {
                    long s = Plugin.GetElapsedSeconds(data, f);
                    IList<string> attrs;
                    if (trackpts.ContainsKey(s))
                    {
                        attrs = trackpts[s];
                    }
                    else
                    {
                        trackpts[s] = attrs = new List<string>();
                    }
                    float v = f.Value;
                    attrs.Add(name);
                    attrs.Add(v.ToString());
                }
            }
        }

        private void FitToPage()
        {
            if (ready && this.Visible && changed)
            {
                changed = false;

                IRouteSettings rs = Plugin.GetApplication().SystemPreferences.RouteSettings;
                string linecolor = String.Format("#{0:X2}{1:X2}{2:X2}",
                        rs.RouteColor.R, rs.RouteColor.G, rs.RouteColor.B);
                float weight = rs.RouteWidth;
                float opacity = (float)(rs.RouteColor.A / 255.0);
                float n = -200, s = 200, e = -200, w = 200;
                webBrowser.Document.InvokeScript("clear_markers");
                int nact = activities != null ? activities.Count : 0;
                int nrt = routes != null ? routes.Count : 0;
                int ntot = nact + nrt;
                HtmlElement elt = webBrowser.Document.GetElementById("xml_iframe");
                xmlnode athleteLog = null;
                bool smoothing = false;

                root = null;

                if (elt != null && activities != null)
                {
                    smoothing = elt.GetAttribute("smoothing") != "";
                    root = new XmlDocument();
                    xmlnode fitnessWorkbook = CreateChild(root, "FitnessWorkbook", null);
                    athleteLog = CreateChild(fitnessWorkbook, "AthleteLog", null);
                    ILogbook logbook = Plugin.GetApplication().Logbook;
                    CreateChild(athleteLog, "Athlete", new string[] {
                            "Id", logbook.Athlete.ReferenceId,
                            "Name", logbook.Athlete.Name
                        });
                }
                for (int i = 0; i < ntot; i++)
                {
                    IActivity activity = i < nact ? activities[i] : null;
                    IRoute route = i >= nact ? routes[i - nact] : null;
                    IGPSRoute groute = activity != null ? activity.GPSRoute : route.GPSRoute;

                    if (groute == null)
                    {
                        continue;
                    }

                    string refid = activity != null ? activity.ReferenceId : route.ReferenceId;

                    ActivityInfo ai = activity != null ? ActivityInfoCache.Instance.GetInfo(activity) : null;
                    if (root != null && activity != null)
                    {
                        xmlnode act = CreateChild(athleteLog, "Activity", new string[] {
                                "StartTime", XmlDateString(activity.StartTime),
                                "Id", activity.ReferenceId
                        });
                        CreateChild(act, "Metadata", new string[] {
                            "Source", activity.Metadata.Source,
                            "Modified", XmlDateString(activity.Metadata.Modified)
                        });
                        CreateChild(act, "Duration", new string[] {
                            "TotalSeconds", ((int)ai.Time.TotalSeconds).ToString() });
                        CreateChild(act, "Elevation", new string[] {
                            "AscendMeters", activity.TotalAscendMetersEntered.ToString(),
                            "DescendMeters", activity.TotalDescendMetersEntered.ToString()
                        });
                        CreateChild(act, "Calories", new string[] {
                                "TotalCal", activity.TotalCalories.ToString()
                        });

                        if (activity.Name != "")
                        {
                            xmlnode name = CreateChild(act, "Name", null);
                            CreateChild(name, "", new string[] { activity.Name });
                        }

                        if (activity.Notes != "")
                        {
                            xmlnode notes = CreateChild(act, "Notes", null);
                            CreateChild(notes, "", new string[] { activity.Notes });
                        }

                        if (activity.Laps != null && activity.Laps.Count > 0)
                        {
                            xmlnode laps = CreateChild(act, "Laps", null);
                            foreach (ILapInfo li in activity.Laps)
                            {
                                CreateChild(laps, "Lap", new string[] {
                                    "StartTime", XmlDateString(li.StartTime),
                                    "DurationSeconds", li.TotalTime.TotalSeconds.ToString() });
                            }
                        }
                        
                        CreateChild(act, "Weather", new string[] {
                            "Conditions", activity.Weather.Conditions.ToString(),
                            "Temp", activity.Weather.TemperatureCelsius.ToString()
                        });
                        CreateChild(act, "Category", new string[] {
                            "Id", activity.Category.ReferenceId,
                            "Name", activity.Category.Name
                        });
                        CreateChild(act, "Location", new string[] {
                            "Name", activity.Location
                        });
                        xmlnode equip = CreateChild(act, "EquipmentUsed", null);
                        foreach (IEquipmentItem eq in activity.EquipmentUsed)
                        {
                            CreateChild(equip, "EquipmentItem", new string[] {
                                "Id", eq.ReferenceId,
                                "Name", eq.Name
                            });
                        }
                        xmlnode track = CreateChild(act, "Track", new string[] {
                            "StartTime", XmlDateString(groute.StartTime)
                        });

                        SortedList<long, IList<string>> trackpts = new SortedList<long,IList<string>>();

                        foreach (ITimeValueEntry<IGPSPoint> pt in groute)
                        {
                            long secs = Plugin.GetElapsedSeconds(groute, pt);
                            IList<string> attrs;
                            if (trackpts.ContainsKey(secs))
                            {
                                attrs = trackpts[secs];
                            }
                            else
                            {
                                trackpts[secs] = attrs = new List<string>();
                            }
                            IGPSPoint p = pt.Value;
                            attrs.Add("lat");
                            attrs.Add(FloatToCoord(p.LatitudeDegrees));
                            attrs.Add("lon");
                            attrs.Add(FloatToCoord(p.LongitudeDegrees));
                            if (!smoothing)
                            {
                                attrs.Add("ele");
                                attrs.Add(FloatToCoord(p.ElevationMeters));
                            }
                        }
                        if (smoothing)
                        {
                            AddFloatTrack(trackpts, smoothing, activity.ElevationMetersTrack,
                                ai.SmoothedElevationTrack, "ele");
                        }
                        AddFloatTrack(trackpts, smoothing, activity.HeartRatePerMinuteTrack, ai.SmoothedHeartRateTrack, "hr");
                        AddFloatTrack(trackpts, smoothing, activity.CadencePerMinuteTrack, ai.SmoothedCadenceTrack, "cadence");
                        AddFloatTrack(trackpts, smoothing, activity.PowerWattsTrack, ai.SmoothedPowerTrack, "power");
                        AddFloatTrack(trackpts, smoothing, activity.DistanceMetersTrack, null, "dist");
                        if (smoothing)
                        {
                            AddFloatTrack(trackpts, smoothing, null, ai.SmoothedSpeedTrack, "speed");
                        }
                        foreach (long tm in trackpts.Keys)
                        {
                            IList<string> sl = trackpts[tm];
                            sl.Insert(0, tm.ToString());
                            sl.Insert(0, "tm");
                            CreateChild(track, "pt", sl);
                        }

                        if (activity.DistanceMarkersMeters != null && activity.DistanceMarkersMeters.Count > 0)
                        {
                            xmlnode dmm = CreateChild(act, "DistanceMarkers", null);
                            foreach (float m in activity.DistanceMarkersMeters)
                            {
                                CreateChild(dmm, "Marker", new string[] { "dist", m.ToString() });
                            }
                        }
                    }
                    else
                    {
                        string values = "";
                        foreach (ITimeValueEntry<IGPSPoint> pt in groute)
                        {
                            IGPSPoint p = pt.Value;
                            if (p.LatitudeDegrees < s) s = p.LatitudeDegrees;
                            if (p.LatitudeDegrees > n) n = p.LatitudeDegrees;
                            if (p.LongitudeDegrees < w) w = p.LongitudeDegrees;
                            if (p.LongitudeDegrees > e) e = p.LongitudeDegrees;

                            values += "," + FloatToCoord(p.LatitudeDegrees);
                            values += "," + FloatToCoord(p.LongitudeDegrees);
                        }
                        DateTime start = (activity != null ? activity.StartTime : groute.StartTime).ToLocalTime();
                        webBrowser.Document.InvokeScript("add_marker", new Object[] {
                                groute[0].Value.LatitudeDegrees,
                                groute[0].Value.LongitudeDegrees,
                                Properties.Resources.Route_StartTime_Text + start.ToShortDateString() + " " + start.ToShortTimeString(),
                                "start_" + refid
                            });
                        DateTime end = (ai != null ? ai.EndTime :
                                groute.EntryDateTime(groute[groute.Count - 1])).ToLocalTime();

                        webBrowser.Document.InvokeScript("add_marker", new Object[] {
                                groute[groute.Count-1].Value.LatitudeDegrees,
                                groute[groute.Count-1].Value.LongitudeDegrees,
                                Properties.Resources.Route_EndTime_Text + end.ToShortDateString() + " " + end.ToShortTimeString(),
                                "end_" + refid
                            });

                        webBrowser.Document.InvokeScript("add_polyline", new Object[] {
                                linecolor, values.Substring(1), weight, opacity, refid });
                    }
                }

                if (root != null)
                {
                    StringWriter sw = new StringWriter();
                    root.Save(sw);
                    root = null;
                    webBrowser.Document.InvokeScript("add_polyline_from_xml", new Object[] { sw.ToString() });
                }
                else if (n > s && e > w)
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
                SelectProvider(curMapType);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GMapRouteControl));
            resources.ApplyResources(this.fitToPageBtn, "fitToPageBtn");

            // dont apply resources, because mapTypeLbl is anchored to the right
            // but has its initial position set relative to the top left.
            // if you re-apply resources after the parent window has changed
            // size, it gets moved to the wrong location.
            this.mapTypeLbl.Text = resources.GetString("mapTypeLbl.Text");
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
                    if ((otime == null || otime.ToString() == "") &&
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
                            if (pt < 0)
                            {
                                pt = 0;
                            }
                            else if (pt >= act.GPSRoute.Count)
                            {
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

                        float dist = GetInterpolatedValue(ddt, when) - delta;
                        float elev = GetInterpolatedValue(ai.SmoothedElevationTrack, when);
                        float hrt = GetInterpolatedValue(ai.SmoothedHeartRateTrack, when);
                        float pace = GetInterpolatedValue(ai.SmoothedSpeedTrack, when);
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
