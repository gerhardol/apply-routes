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

    File: ApplyRoutes/Edit/MakeRouteAction.cs
***********************************************************************/
using System;
using System.ComponentModel;
using System.Drawing;
using System.Collections.Generic;
using System.Collections.Specialized;

using ZoneFiveSoftware.Common.Data;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Data.GPS;
using ZoneFiveSoftware.Common.Visuals;
#if !ST_2_1
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Visuals.Util;
#endif

using ApplyRoutesPlugin.UI;
using System.Windows.Forms;

namespace ApplyRoutesPlugin.Edit
{
    public class ActivityRoutePair 
    {
        public ActivityRoutePair(IActivity a, IRoute r)
        {
            activity = a;
            route = r;
        }

        public IActivity Activity {
            get { return activity; }
            set { activity = value; }
        }

        public IRoute Route {
            get { return route; }
            set { route = value; }
        }

        public string RouteName
        {
            get { return Route != null ? Route.Name : ""; }
        }

        public string ActivityName
        {
            get { return Activity.StartTime.ToLocalTime().ToShortDateString() + " " + Activity.Name; }
        }

        private IActivity activity;
        private IRoute route;
    }

    class MakeRouteAction : IAction
    {
#if !ST_2_1
        public MakeRouteAction(IDailyActivityView aview, IActivityReportsView rview)
        {
            this.dailyView = aview;
            this.reportView = rview;
        }
#else
        public MakeRouteAction(IList<IActivity> activities, IList<IRoute> dummy)
        {
            this.activities = activities;
        }
#endif

        #region IAction Members

        public bool Enabled
        {
            get
            {
                if (activities != null)
                {
                    foreach (IActivity activity in activities)
                    {
                        if (activity.GPSRoute != null)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        public bool HasMenuArrow
        {
            get { return false; }
        }

        public Image Image
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

        public void Run(Rectangle rectButton)
        {
            if (activities != null)
            {
                List<ActivityRoutePair> list = new List<ActivityRoutePair>();
                StringCollection usedRoutes = new StringCollection();

                foreach (IActivity activity in activities)
                {
                    if (activity.GPSRoute != null)
                    {
                        IEnumerable<IRoute> routes = Plugin.GetApplication().Logbook.Routes;
                        IRoute theRoute = null;
                        foreach (IRoute route in routes)
                        {
                            if (!usedRoutes.Contains(route.ReferenceId))
                            {
                                if (route.Name == activity.Name)
                                {
                                    theRoute = route;
                                    break;
                                }
                                if (route.GPSRoute == null || route.GPSRoute.Count <= 1)
                                {
                                    theRoute = route;
                                }
                            }
                        }
                        if (theRoute != null)
                        {
                            usedRoutes.Add(theRoute.ReferenceId);
                        }
                        ActivityRoutePair arp = new ActivityRoutePair(activity, theRoute);
                        list.Add(arp);
                    }
                }

                ConfirmRoutesForm m = new ConfirmRoutesForm(list);
                if (m.ShowDialog() == DialogResult.OK)
                {
                    foreach (ActivityRoutePair arp in list)
                    {
                        IRoute theRoute = arp.Route;
                        IActivity activity = arp.Activity;
                        if (theRoute != null)
                        {
                            IActivityCategory cat = activity.Category;

                            theRoute.Category = cat.Name;
                            while ((cat = cat.Parent) != null)
                            {
                                theRoute.Category = cat.Name + ": " + theRoute.Category;
                            }
                            theRoute.GPSRoute = new GPSRoute(activity.GPSRoute);
                            theRoute.Location = activity.Location;
                            if (activity.Name != "")
                            {
                                theRoute.Name = activity.Name;
                            }
                            if (activity.Notes != "")
                            {
                                theRoute.Notes = activity.Notes;
                            }
                        }

                    }
                }
                m.Dispose();
            }
        }

        public string Title
        {
            get { return Properties.Resources.Edit_CreateRouteAction_Text; }
        }

        public bool Visible
        {
            get
            {
                if (activities.Count > 0) return true;
                return false;
            }
        }
        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

#if !ST_2_1
        private IDailyActivityView dailyView = null;
        private IActivityReportsView reportView = null;
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
    }
}
