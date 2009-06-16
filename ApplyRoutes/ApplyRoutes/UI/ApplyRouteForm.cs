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

    File: ApplyRoutes/UI/ApplyRouteForm.cs
***********************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Data.GPS;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Data.Measurement;

namespace ApplyRoutesPlugin.UI
{
    

    public partial class ApplyRouteForm : Form
    {
        public enum PreserveDistEnum {
            kDontPreserveDistances,
            kPreserveDistScaled,
            kPreserveDistRounded,
            kPreserveDistExactly
        };

        public ApplyRouteForm(IList<IActivity> activities)
        {
            InitializeComponent();
            Plugin.registerDefaultBtns(this);

            this.activities = activities;
            
            bool hasGPS = false, hasDist = false;
            foreach (IActivity activity in activities) {
                if (activity.GPSRoute != null && activity.GPSRoute.Count != 0) {
                    hasGPS = true;
                }
                if (activity.DistanceMetersTrack != null)
                {
                    hasDist = true;
                }
            }
            this.ignoreGPSActChk.Enabled = hasGPS;
            this.ignoreGPSActChk.CheckState = CheckState.Checked;
            this.preserveDistChk.CheckState = hasDist ? CheckState.Checked : CheckState.Unchecked;
            this.preserve_dist_scaled_rad.Checked = true;

            EventHandler refreshHandler = delegate(object sender, EventArgs e)
            {
                RefreshPage();
            };

            
            this.ignoreGPSActChk.CheckedChanged += refreshHandler;
            this.routeList.SelectedChanged += refreshHandler;
            this.preserve_dist_exactly_rad.CheckedChanged += refreshHandler;
            this.preserve_dist_rounded_rad.CheckedChanged += refreshHandler;
            this.preserve_dist_scaled_rad.CheckedChanged += refreshHandler;
            this.preserveDistChk.CheckedChanged += refreshHandler;

            this.routeList.Columns.Add(new TreeList.Column("Name", Properties.Resources.ApplyRouteForm_Name, 100, StringAlignment.Near));

            List<IRoute> routes = new List<IRoute>();
            foreach (IRoute rt in Plugin.GetApplication().Logbook.Routes)
            {
                if (rt.GPSRoute != null && rt.GPSRoute.Count != 0)
                {
                    routes.Add(rt);
                }
            }
            this.routeList.RowData = routes;

            RefreshPage();

            ThemeChanged(Plugin.GetApplication().VisualTheme);
        }

        public void RefreshPage()
        {
            double min = 1e6, max = -1e6, avg = 0;
            int nGps = 0;
            int nToUpdate = 0;
            int nWithDist = 0;
            foreach (IActivity activity in activities)
            {
                if (activity.GPSRoute != null && activity.GPSRoute.Count != 0)
                {
                    nGps++;
                    if (ignoreGPSActChk.Checked)
                    {
                        continue;
                    }
                }
                ActivityInfo ai = ActivityInfoCache.Instance.GetInfo(activity);
                double dist = ai.DistanceMeters;
                if (dist > max) max = dist;
                if (dist < min) min = dist;
                avg += dist;
                nToUpdate++;
                if (activity.DistanceMetersTrack != null || activity.Laps.Count > 0)
                {
                    nWithDist++;
                }
            }

            ignoreGPSActChk.Enabled = nGps > 0;
            preserveDistChk.Enabled = nWithDist > 0;
            numActTxt.Text = nToUpdate.ToString();
            minDistTxt.Text = min < 1e6 ? DistanceAsString(min) : "";
            maxDistTxt.Text = max > 0 ? DistanceAsString(max) : "";
            avgDistTxt.Text = nToUpdate > 0 ? DistanceAsString(avg / nToUpdate) : "";
            okBtn.Enabled = nToUpdate > 0 && routeList.Selected.Count > 0;
            preserve_dist_exactly_rad.Visible = preserveDistChk.Checked;
            preserve_dist_rounded_rad.Visible = preserveDistChk.Checked;
            preserve_dist_scaled_rad.Visible = preserveDistChk.Checked;
            Boolean laps_visible = preserveDistChk.Checked && nToUpdate == 1 && routeList.Selected.Count > 0 && avg > 0;
            Laps_lbl.Visible = laps_visible;
            laps_txt.Visible = laps_visible;
            if (laps_visible)
            {
                IRoute route = routeList.SelectedItems[0] as IRoute;

                double laps = 1;
                if (preserve_dist_exactly_rad.Checked)
                {
                    laps = Math.Round(avg / route.TotalDistanceMeters, 2);
                    if (laps == 0) laps = 1;
                }
                else if (preserve_dist_rounded_rad.Checked)
                {
                    laps = Math.Round(avg / route.TotalDistanceMeters);
                    if (laps < 1) laps = 1;
                }
                laps_txt.Text = laps.ToString();
            }
        }

        public static string DistanceAsString(double adist)
        {
            Length.Units displayUnits = Plugin.GetApplication().SystemPreferences.DistanceUnits;

            // Convert distances to the preferred length when displaying.
            string distanceFormatString = "N" + Length.DefaultDecimalPrecision(displayUnits) + "U";
            return Length.ToString(Length.Convert(adist, Length.Units.Meter, displayUnits),
                        displayUnits, distanceFormatString);
        }

        public void ThemeChanged(ITheme visualTheme)
        {
            Plugin.ThemeChanged(numActLbl, visualTheme);
            numActTxt.ThemeChanged(visualTheme);
            Plugin.ThemeChanged(minDistLbl, visualTheme);
            minDistTxt.ThemeChanged(visualTheme);
            Plugin.ThemeChanged(maxDistLbl, visualTheme);
            maxDistTxt.ThemeChanged(visualTheme);
            Plugin.ThemeChanged(avgDistLbl, visualTheme);
            avgDistTxt.ThemeChanged(visualTheme);

            Plugin.ThemeChanged(routeLbl, visualTheme);
            Plugin.ThemeChanged(ignoreGPSActChk, visualTheme);
            Plugin.ThemeChanged(preserveDistChk, visualTheme);
            Plugin.ThemeChanged(preserve_dist_exactly_rad, visualTheme);
            Plugin.ThemeChanged(preserve_dist_rounded_rad, visualTheme);
            Plugin.ThemeChanged(preserve_dist_scaled_rad, visualTheme);
            Plugin.ThemeChanged(Laps_lbl, visualTheme);
            laps_txt.ThemeChanged(visualTheme);

            routeList.ThemeChanged(visualTheme);

            Plugin.ThemeChanged(okBtn, visualTheme);
            Plugin.ThemeChanged(cancelBtn, visualTheme);
        }

        public IList<IRoute> SelectedRoutes
        {
            get { return ConvertToListOf<IRoute>(routeList.SelectedItems); }
        }

        public bool ApplyToAll
        {
            get { return ignoreGPSActChk.CheckState == CheckState.Unchecked; }
        }

        public bool ApplyLinearly
        {
            get { return applyTimesLinearlyChk.Checked; }
        }

        public PreserveDistEnum PreserveDistances
        {
            get
            {
                if (preserveDistChk.Checked)
                {
                    if (preserve_dist_rounded_rad.Checked)
                    {
                        return PreserveDistEnum.kPreserveDistRounded;
                    }
                    else if (preserve_dist_exactly_rad.Checked)
                    {
                        return PreserveDistEnum.kPreserveDistExactly;
                    }
                    else
                    {
                        return PreserveDistEnum.kPreserveDistScaled;
                    }
                }
                else
                {
                    return PreserveDistEnum.kDontPreserveDistances;
                }
            }
        }
                
        public static IList<T> ConvertToListOf<T>(IList iList)  
        {  
            IList<T> result = new List<T>();  
            foreach(T value in iList)
            {  
                result.Add(value);
            }

            return result;
        }

        public static IList ConvertFromListOf<T>(IList<T> iList)
        {
            ArrayList result = new ArrayList();
            foreach (T value in iList)
            {
                result.Add(value);
            }

            return result;
        }

        private IList<IActivity> activities = null;
    }
}
