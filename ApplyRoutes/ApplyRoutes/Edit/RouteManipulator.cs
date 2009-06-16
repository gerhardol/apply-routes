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

    File: ApplyRoutes/Edit/RouteManipulator.cs
***********************************************************************/
using System;
using System.ComponentModel;
using System.Drawing;
using System.Collections.Generic;

using ZoneFiveSoftware.Common.Data;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Data.GPS;
using ZoneFiveSoftware.Common.Visuals;

using ApplyRoutesPlugin.UI;

namespace ApplyRoutesPlugin.Edit
{
    class RouteManipulator : IAction
    {
        public RouteManipulator(IList<IActivity> activities)
        {
            this.activities = activities;
        }

        #region IAction Members

        public bool Enabled
        {
            get
            {
                return activities != null;
            }
        }

        public bool HasMenuArrow
        {
            get { return true; }
        }

        public Image Image
        {
            get { return CommonResources.Images.Edit16; }
        }

        public void Refresh()
        {
        }

        public void Run(Rectangle rectButton)
        {
            if (activities != null)
            {
                TreeListPopup treePop = new TreeListPopup();

                treePop.Tree.Columns.Add(new TreeList.Column("Title"));
                treePop.Tree.RowData = new IAction[] {
                        new ApplyRouteAction(activities),
                        new MakeRouteAction(activities)
                };

                treePop.ItemSelected += delegate(object sender, TreeListPopup.ItemSelectedEventArgs e)
                {
                    if (e.Item is IAction)
                    {
                        ((IAction)e.Item).Run(rectButton);
                    }
                };

                treePop.ThemeChanged(Plugin.GetApplication().VisualTheme);
                treePop.Popup(rectButton);
            }
        }

        public string Title
        {
            get { return Properties.Resources.Edit_RouteManipulation_Text; }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private void treePop_PopupClosed(object sender, EventArgs e)
        {
        }

        private void treePop_ItemSelected(object sender, TreeListPopup.ItemSelectedEventArgs e)
        {
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private IList<IActivity> activities = null;
    }
}
