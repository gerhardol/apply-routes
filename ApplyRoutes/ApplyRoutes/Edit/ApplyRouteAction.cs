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

    File: ApplyRoutes/Edit/ApplyRouteAction.cs
***********************************************************************/
using System;
using System.ComponentModel;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

using ZoneFiveSoftware.Common.Data;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Data.GPS;
using ZoneFiveSoftware.Common.Visuals;

using ApplyRoutesPlugin.UI;

namespace ApplyRoutesPlugin.Edit
{
    class ApplyRouteAction : IAction
    {
        public ApplyRouteAction(IList<IActivity> activities)
        {
            this.activities = activities;
        }

        #region IAction Members

        public bool Enabled
        {
            get { return activities != null; }
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

        public void UpdateEquipment(IList<IEquipmentItem> eList, bool add)
        {
            if (eList != null && eList.Count != 0)
            {
                foreach (IActivity activity in activities)
                {
                    foreach (IEquipmentItem eItem in eList)
                    {
                        if (add)
                        {
                            if (!activity.EquipmentUsed.Contains(eItem))
                            {
                                activity.EquipmentUsed.Add(eItem);
                            }
                        }
                        else
                        {
                            activity.EquipmentUsed.Remove(eItem);
                        }
                    }
                }
            }
        }

        private static long GetElapsedSeconds<T>(ITimeDataSeries<T> s, ITimeValueEntry<T> e)
        {
            TimeSpan span = s.EntryDateTime(e).Subtract(s.StartTime);
            return Convert.ToInt64(span.TotalSeconds);
        }

        private static long GetTotalElapsedSeconds<T>(ITimeDataSeries<T> s)
        {
            if (s.Count > 1)
            {
                return GetElapsedSeconds(s, s[s.Count - 1]);
            }
            else
            {
                return 0;
            }
        }

        public void Run(Rectangle rectButton)
        {
            ApplyRouteForm m = new ApplyRouteForm(activities);
            if (m.ShowDialog() == DialogResult.OK)
            {
                IList<IRoute> routes = m.SelectedRoutes;
                if (routes != null && routes.Count == 1)
                {
                    bool applyToAll = m.ApplyToAll;
                    foreach (IActivity activity in activities)
                    {
                        if (activity.GPSRoute == null || applyToAll)
                        {
                            ActivityInfo ai = ActivityInfoCache.Instance.GetInfo(activity);
                            IGPSRoute rt = routes[0].GPSRoute;
                            GPSRoute route = new GPSRoute();
                            double numer = ai.Time.TotalSeconds;
                            long totalElapsedSeconds = GetTotalElapsedSeconds(rt);
                            if (numer == 0.0)
                            {
                                numer = totalElapsedSeconds;
                            }
                            bool applyLinearly = m.ApplyLinearly || totalElapsedSeconds == 0;
                            IDistanceDataTrack dmt = rt.GetDistanceMetersTrack();
                            double timeScale = !applyLinearly ? 
                                numer / totalElapsedSeconds :
                                numer / rt.TotalDistanceMeters;

                            int i;
                            for (i = 0; i < rt.Count; i++)
                            {
                                ITimeValueEntry<IGPSPoint> tpt = rt[i];
                                double elapsed = (!applyLinearly ?
                                    GetElapsedSeconds(rt, tpt) :
                                    dmt[i].Value) * timeScale;
                                IGPSPoint point = tpt.Value;
                                DateTime t = activity.StartTime.AddSeconds(elapsed);
                                route.InsertAtPosition(i, t, point);
                            }

                            dmt = route.GetDistanceMetersTrack();
                            float dist = 0;
                            long dmtTotalElapsedSeconds = GetTotalElapsedSeconds(dmt);
                            for (i = 0; i < activity.Laps.Count; i++)
                            {
                                ILapInfo li = activity.Laps[i];
                                DateTime t = li.StartTime.AddSeconds(li.TotalTime.TotalSeconds);
                                float old_dist = dist;

                                dist = t.Subtract(dmt.StartTime).TotalSeconds >= dmtTotalElapsedSeconds ?
                                        route.TotalDistanceMeters :
                                        dmt.GetInterpolatedValue(t).Value;

                                li.TotalDistanceMeters = dist - old_dist;
                            }
                            activity.Name = routes[0].Name;
                            activity.Location = routes[0].Location;
                            activity.GPSRoute = route;
                            //activity.DistanceMetersTrack = route.GetDistanceMetersTrack();
                            activity.UseEnteredData = false;
                        }
                    }
                }
            }
        }

        public string Title
        {
            get { return Properties.Resources.Edit_ApplyRouteAction_Text; }
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
