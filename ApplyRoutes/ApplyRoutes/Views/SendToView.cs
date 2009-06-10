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

    File: ApplyRoutes/Views/SendToView.cs
***********************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Data.Fitness;
using System.ComponentModel;
using ApplyRoutesPlugin.Activities;

namespace ApplyRoutesPlugin.Views
{
    class SendToView : IAction
    {
        #region IAction Members
        public SendToView(IList<IActivity> a, IList<IRoute> r)
        {
            activities = a;
            routes = r;
        }

        public bool Enabled
        {
            get { return true; }
        }

        public bool HasMenuArrow
        {
            get { return false; }
        }

        public System.Drawing.Image Image
        {
            get { return null; }
        }

        public void Refresh()
        {
        }

        public void Run(System.Drawing.Rectangle rectButton)
        {
            Plugin.GetApplication().ShowView(Plugin.thePlugin.Id, "");
            GMapActivityDetail view = Plugin.GetApplication().ActiveView as GMapActivityDetail;
            if (view != null)
            {
                GMapRouteControl control = view.CreatePageControl() as GMapRouteControl;
                if (control != null)
                {
                    if (activities != null)
                    {
                        control.Activities = activities;
                    }
                    if (routes != null)
                    {
                        control.Routes = routes;
                    }
                }
            }
        }

        public string Title
        {
            get { return Properties.Resources.Edit_SendToRouteControl_Text; }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        IList<IActivity> activities;
        IList<IRoute> routes;
    }
}
