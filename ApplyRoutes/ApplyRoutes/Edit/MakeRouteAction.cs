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

using ZoneFiveSoftware.Common.Data;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Data.GPS;
using ZoneFiveSoftware.Common.Visuals;

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
            get { return Route.Name; }
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
        public MakeRouteAction(IList<IActivity> activities)
        {
            this.activities = activities;
        }

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
            get { return null; }
        }

        public void Refresh()
        {
        }

        public void Run(Rectangle rectButton)
        {
            if (activities != null)
            {
                List<ActivityRoutePair> list = new List<ActivityRoutePair>();
                foreach (IActivity activity in activities)
                {
                    if (activity.GPSRoute != null && activity.Name != "")
                    {
                        IList<IRoute> routes = Plugin.GetApplication().Logbook.Routes;
                        IRoute theRoute = null;
                        foreach (IRoute route in routes)
                        {
                            if (route.Name == activity.Name)
                            {
                                theRoute = route;
                                break;
                            }
                            if ((route.Name == "" || route.Name == "New route") &&
                                route.GPSRoute == null)
                            {
                                theRoute = route;
                            }
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
                            theRoute.Name = activity.Name;
                            theRoute.Notes = activity.Notes;
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

        private IList<IActivity> activities = null;
    }
}
