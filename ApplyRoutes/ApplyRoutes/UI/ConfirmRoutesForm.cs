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

            this.activityRouteTree.Columns.Add(new TreeList.Column("ActivityName", "Activity", w2, StringAlignment.Near));
            this.activityRouteTree.Columns.Add(new TreeList.Column("RouteName", "Route", w1, StringAlignment.Near));
            this.activityRouteTree.RowData = arpList;
            this.activityRouteTree.SelectedChanged += delegate(object sender, EventArgs e)
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
                    TreeListPopup treePop = new TreeListPopup();

                    treePop.Tree.Columns.Add(new TreeList.Column("Name"));
                    treePop.Tree.RowData = Plugin.GetApplication().Logbook.Routes;

                    treePop.ItemSelected += delegate(object s, TreeListPopup.ItemSelectedEventArgs i)
                    {
                        if (i.Item is IRoute)
                        {
                            curArp.Route = (IRoute)i.Item;
                            activityRouteTree.Invalidate();
                            SetupActivityRoutePop();
                        }
                    };

                    treePop.ThemeChanged(Plugin.GetApplication().VisualTheme);
                    treePop.Popup(activityRoutePop.Parent.RectangleToScreen(activityRoutePop.Bounds));
                }
            };

            SetupActivityRoutePop();
            ThemeChanged(Plugin.GetApplication().VisualTheme);
        }

        static public void ThemeChanged(Control control, ITheme visualTheme)
        {
            control.ForeColor = visualTheme.ControlText;
            control.BackColor = visualTheme.Control;
        }

        public void ThemeChanged(ITheme visualTheme)
        {
            activityRouteTree.ThemeChanged(visualTheme);
            activityRoutePop.ThemeChanged(visualTheme);
            activityText.ThemeChanged(visualTheme);

            ThemeChanged(okBtn, visualTheme);
            ThemeChanged(cancelBtn, visualTheme);
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
