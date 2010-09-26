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
#if !ST_2_1
using ZoneFiveSoftware.Common.Visuals.Util;
#endif
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Data;
using System.ComponentModel;
using ApplyRoutesPlugin.Activities;

namespace ApplyRoutesPlugin.Views
{
    class SendToView : IAction
    {
        #region IAction Members
#if ST_2_1
        public SendToView(IList<IActivity> a, IList<IRoute> r)
        {
            activities = a;
            routes = r;
        }
#else
        public SendToView(IDailyActivityView aview, IActivityReportsView rview)
        {
            this.dailyView = aview;
            this.reportView = rview;
        }
        public SendToView(IRouteView dummy, IRouteView view)
        {
            this.routeView = view;
        }
#endif

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
            get { return Properties.Resources.ApplyRoutes.ToBitmap(); }
        }

        public IList<string> MenuPath
        {
            get
            {
                return new List<string>();
            }
        }

        public void Refresh()
        {
        }

        public void Run(System.Drawing.Rectangle rectButton)
        {
            Plugin.GetApplication().ShowView(GUIDs.ApplyRoutesView, "");//TODO exception
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
            get
            {
                int num = (activities != null ? activities.Count : 0) +
                    (routes != null ? routes.Count : 0);

                return Plugin.NumberedActivityText(Properties.Resources.Edit_SendToRouteControl_Text, num);
            }
        }

        private bool firstRun = true;
        public bool Visible
        {
            get
            {
                //Analyze menu must be Visible at first call, otherwise it is hidden
                //Could be done with listeners too
                //TODO exception
                if (true == firstRun) { firstRun = false; return true; }
                if (activities.Count == 0) return false;
                return true;
            }
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

#if !ST_2_1
        private IDailyActivityView dailyView = null;
        private IActivityReportsView reportView = null;
        private IRouteView routeView = null;
#endif
        private IList<IActivity> _activities = null;
        private IList<IActivity> activities
        {
            get
            {
#if !ST_2_1
                //activities are set either directly or by selection,
                //not by more than one
                if (_activities == null)
                {
                    if (dailyView != null)
                    {
                        return CollectionUtils.GetAllContainedItemsOfType<IActivity>(dailyView.SelectionProvider.SelectedItems);
                    }
                    else if (reportView != null)
                    {
                        return CollectionUtils.GetAllContainedItemsOfType<IActivity>(reportView.SelectionProvider.SelectedItems);
                    }
                    else
                    {
                        return new List<IActivity>();
                    }
                }
#endif
                return _activities;
            }
            set
            {
                _activities = value;
            }
        }
        private IList<IRoute> _routes = null;
        private IList<IRoute> routes
        {
            get
            {
#if !ST_2_1
                //activities are set either directly or by selection,
                //not by more than one
                if (_routes == null)
                {
                    if (routeView != null)
                    {
                        return CollectionUtils.GetAllContainedItemsOfType<IRoute>(routeView.SelectionProvider.SelectedItems);
                    }
                    else
                    {
                        return new List<IRoute>();
                    }
                }
#endif
                return _routes;
            }
            set
            {
                _routes = value;
            }
        }
    }
}
