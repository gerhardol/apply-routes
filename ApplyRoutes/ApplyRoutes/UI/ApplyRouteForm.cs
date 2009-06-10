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
        public ApplyRouteForm(IList<IActivity> activities)
        {
            InitializeComponent();
            Plugin.registerDefaultBtns(this);

            this.activities = activities;
            int width = this.equipmentList.Width - this.equipmentList.VScrollBar.Width - 2;
            int w1 = width / 3;
            int w2 = width - 2 * w1;

            this.equipmentList.Columns.Add(new TreeList.Column("Brand", "Brand", w2, StringAlignment.Near));
            this.equipmentList.Columns.Add(new TreeList.Column("Model", "Model", w1, StringAlignment.Near));
            this.equipmentList.Columns.Add(new TreeList.Column("Type", "Type", w1, StringAlignment.Near));
            this.equipmentList.RowData = Plugin.GetApplication().Logbook.Equipment;

            commonItems = CommonEquipment();
            /*
            
                IList<IEquipmentItem> usedEquipment = UsedEquipment();
                foreach (Object obj in usedEquipment)
                {
                    this.equipmentList.SetCheckedState(obj, CheckState.Indeterminate);
                }
            */
            foreach (Object obj in commonItems)
            {
                this.equipmentList.SetCheckedState(obj, CheckState.Checked);
            }
            //this.equipmentList.Selected = ConvertFromListOf<IEquipmentItem>(commonItems);
            bool hasGPS = false;
            foreach (IActivity activity in activities) {
                if (activity.GPSRoute != null && activity.GPSRoute.Count != 0) {
                    hasGPS = true;
                    break;
                }
            }
            this.ignoreGPSActChk.Enabled = hasGPS;
            this.ignoreGPSActChk.CheckState = CheckState.Checked;

            this.routeList.Columns.Add(new TreeList.Column("Name", "Name", 100, StringAlignment.Near));
            this.routeList.RowData = Plugin.GetApplication().Logbook.Routes;

            RefreshPage();

            ThemeChanged(Plugin.GetApplication().VisualTheme);
        }

        public void RefreshPage()
        {
            numActTxt.Text = activities.Count.ToString();
            double min = 1e6, max = -1e6, avg = 0;
            foreach (IActivity activity in activities) {
                ActivityInfo ai = ActivityInfoCache.Instance.GetInfo(activity);
                double dist = ai.DistanceMeters;
                if (dist > max) max = dist;
                if (dist < min) min = dist;
                avg += dist;
            }
            minDistTxt.Text = min < 1e6 ? DistanceAsString(min) : "";
            maxDistTxt.Text = max > 0 ? DistanceAsString(max) : "";
            avgDistTxt.Text = activities.Count > 0 ? DistanceAsString(avg / activities.Count) : "";
        }

        public string DistanceAsString(double adist)
        {
            Length.Units displayUnits = Plugin.GetApplication().SystemPreferences.DistanceUnits;

            // Convert distances to the preferred length when displaying.
            string distanceFormatString = "N" + Length.DefaultDecimalPrecision(displayUnits) + "U";
            return Length.ToString(Length.Convert(adist, Length.Units.Meter, displayUnits),
                        displayUnits, distanceFormatString);
        }

        static public void ThemeChanged(ZoneFiveSoftware.Common.Visuals.Button button, ITheme visualTheme)
        {
            button.ForeColor = visualTheme.ControlText;
            button.BackColor = visualTheme.Control;
        }

        static public void ThemeChanged(Control control, ITheme visualTheme)
        {
            control.ForeColor = visualTheme.ControlText;
        }

        public void ThemeChanged(ITheme visualTheme)
        {
            ThemeChanged(numActLbl, visualTheme);
            numActTxt.ThemeChanged(visualTheme);
            ThemeChanged(minDistLbl, visualTheme);
            minDistTxt.ThemeChanged(visualTheme);
            ThemeChanged(maxDistLbl, visualTheme);
            maxDistTxt.ThemeChanged(visualTheme);
            ThemeChanged(avgDistLbl, visualTheme);
            avgDistTxt.ThemeChanged(visualTheme);

            ThemeChanged(routeLbl, visualTheme);
            ThemeChanged(ignoreGPSActChk, visualTheme); 
            routeList.ThemeChanged(visualTheme);
            
            ThemeChanged(equipmentLbl, visualTheme);
            equipmentList.ThemeChanged(visualTheme);

            ThemeChanged(okBtn, visualTheme);
            ThemeChanged(cancelBtn, visualTheme);
        }

        public IList<IRoute> SelectedRoutes
        {
            get { return ConvertToListOf<IRoute>(routeList.SelectedItems); }
        }

        public bool ApplyToAll
        {
            get { return ignoreGPSActChk.CheckState == CheckState.Unchecked; }
        }

        public IList<IEquipmentItem> DiffList(IList<IEquipmentItem> list, IList<IEquipmentItem> sublist, bool sub)
        {
            for (int i = list.Count; i-- != 0; )
            {
                if (sublist.Contains(list[i]) == sub)
                {
                    list.RemoveAt(i);
                }
            }
            return list;
        }

        public IList<IEquipmentItem> CommonEquipment()
        {
            IList<IEquipmentItem> list = new List<IEquipmentItem>(Plugin.GetApplication().Logbook.Equipment);
            foreach (IActivity activity in activities)
            {
                DiffList(list, activity.EquipmentUsed, false);
                if (list.Count == 0)
                {
                    break;
                }
            }
            return list;
        }

        public IList<IEquipmentItem> UsedEquipment()
        {
            IList<IEquipmentItem> list = new List<IEquipmentItem>();
            foreach (IActivity activity in activities)
            {
                foreach (IEquipmentItem item in activity.EquipmentUsed)
                {
                    if (!list.Contains(item))
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public IList<IEquipmentItem> EquipmentToAdd
        {
            get {
                return DiffList(ConvertToListOf<IEquipmentItem>(equipmentList.CheckedElements),
                                commonItems, true);
            }
        }

        public IList<IEquipmentItem> EquipmentToRemove
        {
            get
            {
                return DiffList(new List<IEquipmentItem>(commonItems),
                                ConvertToListOf<IEquipmentItem>(equipmentList.CheckedElements),
                                true);
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
        private IList<IEquipmentItem> commonItems = null;
    }
}
