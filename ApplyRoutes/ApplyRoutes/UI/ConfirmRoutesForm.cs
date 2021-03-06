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

    File: ApplyRoutes/UI/ConfirmRoutesForm.cs
***********************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ApplyRoutesPlugin.Edit;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Data.Fitness;

namespace ApplyRoutesPlugin.UI
{
    public partial class ConfirmRoutesForm : Form
    {
        public ConfirmRoutesForm(IList<ActivityRoutePair> list)
        {
            InitializeComponent();
            Plugin.registerDefaultBtns(this);
            arpList = list;

            int width = this.activityRouteTree.Width - this.activityRouteTree.VScrollBar.Width - 2;
            int w1 = width / 2;
            int w2 = width - w1;

            this.activityRouteTree.Columns.Add(new TreeList.Column("ActivityName", Properties.Resources.CreateRouteForm_Activity, w2, StringAlignment.Near));
            this.activityRouteTree.Columns.Add(new TreeList.Column("RouteName", Properties.Resources.CreateRouteForm_Route, w1, StringAlignment.Near));
            this.activityRouteTree.RowData = arpList;
#if ST_2_1
            this.activityRouteTree.SelectedChanged += delegate(object sender, EventArgs e)
#else
            this.activityRouteTree.SelectedItemsChanged += delegate(object sender, EventArgs e)
#endif            
            {
                SetupActivityRoutePop();
            };
            this.activityRouteTree.ColumnClicked += delegate(object sender, TreeList.ColumnEventArgs e)
            {
            };

            this.activityRoutePop.ButtonClick += delegate(object sender, EventArgs e)
            {
                if (curArp != null)
                {
                    Plugin.OpenListPopup(Plugin.GetApplication().VisualTheme,
                                         Plugin.GetApplication().Logbook.Routes,
                                         activityRoutePop, "Name", curArp.Route,
                                         delegate(IRoute route)
                                         {
                                             curArp.Route = route;
                                             activityRouteTree.Invalidate();
                                             SetupActivityRoutePop();
                                         });
                }
            };

            SetupActivityRoutePop();
            ThemeChanged(Plugin.GetApplication().VisualTheme);
        }

        public void ThemeChanged(ITheme visualTheme)
        {
            activityRouteTree.ThemeChanged(visualTheme);
            activityRoutePop.ThemeChanged(visualTheme);
            activityText.ThemeChanged(visualTheme);

            Plugin.ThemeChanged(this, visualTheme);
            Plugin.ThemeChanged(okBtn, visualTheme);
            Plugin.ThemeChanged(cancelBtn, visualTheme);
        }

        public void SetupActivityRoutePop()
        {
            if (activityRouteTree.Selected.Count == 1)
            {
                ActivityRoutePair arp = (ActivityRoutePair)activityRouteTree.Selected[0];
                activityText.Text = arp.ActivityName;
                curArp = arp;
                if (arp.Route != null)
                {
                    activityRoutePop.Text = arp.RouteName;
                }
                return;
            }
            curArp = null;
            activityRoutePop.Text = "";
        }

        private ActivityRoutePair curArp;
        private IList<ActivityRoutePair> arpList;
    }
}
