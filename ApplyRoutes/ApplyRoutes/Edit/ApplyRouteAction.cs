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

        private void ApplyRoute(IActivity activity, IRoute iroute,
                bool forceApplyLinearly, IDistanceDataTrack aDist, double laps)
        {
            ActivityInfo ai = ActivityInfoCache.Instance.GetInfo(activity);
            IGPSRoute rt = iroute.GPSRoute;
            IDistanceDataTrack dmt = rt.GetDistanceMetersTrack();
            int i;

            if (laps != 1)
            {
                IGPSRoute nrt = new GPSRoute();
                TimeSpan offset = TimeSpan.FromSeconds(0);
                int last = (int)Math.Floor(laps);
                double rem = laps - last;
                for (i = 0; i <= last; i++)
                {
                    for (int j = 0; j < rt.Count; j++)
                    {
                        ITimeValueEntry<IGPSPoint> itv = rt[j];
                        if (i == last && dmt[j].Value / rt.TotalDistanceMeters >= rem)
                        {
                            break;
                        }
                        nrt.Add(rt.EntryDateTime(itv).Add(offset), itv.Value);
                    }
                    offset = offset.Add(TimeSpan.FromSeconds(rt.TotalElapsedSeconds));
                }
                rt = nrt;
                dmt = rt.GetDistanceMetersTrack();
            }
            
            GPSRoute route = new GPSRoute();
            double numer = ai.Time.TotalSeconds;
            long totalElapsedSeconds = Plugin.GetTotalElapsedSeconds(rt);
            if (numer == 0.0)
            {
                numer = totalElapsedSeconds;
            }
            bool applyLinearly = forceApplyLinearly || totalElapsedSeconds == 0;
            double timeScale = !applyLinearly ?
                numer / totalElapsedSeconds :
                numer / rt.TotalDistanceMeters;

            double distScale = aDist != null && dmt.Max > 0 ? aDist.Max / dmt.Max : 1;

            if (aDist != null)
            {
                int i1,i2;
                double rt1 = 0, rt2 = 0;
                long at1 = 0, at2 = 0;
                GPSPoint.ValueInterpolator interp = new GPSPoint.ValueInterpolator();

                double prev = -1;
                for (i = i1 = i2 = 0; i1 < rt.Count-1 || i2 < aDist.Count-1; i++)
                {
                    IGPSPoint point = rt[i1].Value;
                    double rdist = dmt[i1].Value * distScale;
                    double adist = aDist[i2].Value;
                    DateTime t;
                    if (rdist < adist && i1 < rt.Count - 1)
                    {
                        t = aDist.GetTimeAtDistanceMeters(rdist);
                        if (i2 > 0 && !applyLinearly)
                        {
                            long rtm = Plugin.GetElapsedSeconds(dmt, dmt[i1]);
                            if (rtm > rt2)
                            {
                                rt1 = dmt.GetTimeAtDistanceMeters(aDist[i2 - 1].Value / distScale).Subtract(dmt.StartTime).TotalSeconds; ;
                                rt2 = dmt.GetTimeAtDistanceMeters(adist / distScale).Subtract(dmt.StartTime).TotalSeconds;
                                at1 = Plugin.GetElapsedSeconds(aDist, aDist[i2 - 1]);
                                at2 = Plugin.GetElapsedSeconds(aDist, aDist[i2]);
                            }
                            if (rt2 > rt1)
                            {
                                double elapsed = at1 + (rtm - rt1) * (at2 - at1) / (rt2 - rt1);
                                t = aDist.StartTime.AddSeconds(elapsed);
                            }
                        }
                        i1++;
                    }
                    else if (i2 < aDist.Count - 1)
                    {
                        double elapsed = Plugin.GetElapsedSeconds(aDist, aDist[i2]);
                        t = aDist.StartTime.AddSeconds(elapsed);
                        if (i1 > 0)
                        {
                            double rd1 = dmt[i1 - 1].Value * distScale;
                            if (rd1 <= adist)
                            {
                                point = interp.Interpolate(rt[i1 - 1].Value, point, (adist - rd1) / (rdist - rd1));
                            }
                        }
                        i2++;
                    }
                    else
                    {
                        break;
                    }
                    double cur = t.Subtract(activity.StartTime).TotalSeconds;
                    if (cur <= prev)
                    {
                        continue;
                    }
                    else if (Math.Round(cur) == Math.Round(prev))
                    {
                        cur = Math.Ceiling(cur);
                        if (cur == Math.Round(prev))
                        {
                            continue;
                        }
                        t = aDist.StartTime.AddSeconds(cur);
                    }
                    prev = cur;
                    route.InsertAtPosition(i, t, point);
                }
            }
            else
            {
                for (i = 0; i < rt.Count; i++)
                {
                    ITimeValueEntry<IGPSPoint> tpt = rt[i];
                    DateTime t;
                    double elapsed = (!applyLinearly ?
                        Plugin.GetElapsedSeconds(rt, tpt) :
                        dmt[i].Value) * timeScale;
                    t = activity.StartTime.AddSeconds(elapsed);

                    IGPSPoint point = tpt.Value;
                    route.InsertAtPosition(i, t, point);
                }
            }

            dmt = route.GetDistanceMetersTrack();
            float dist = 0;
            long dmtTotalElapsedSeconds = Plugin.GetTotalElapsedSeconds(dmt);
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
            activity.Name = iroute.Name;
            activity.Location = iroute.Location;
            activity.GPSRoute = route;
            //activity.DistanceMetersTrack = route.GetDistanceMetersTrack();
            activity.UseEnteredData = false;
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
                            IDistanceDataTrack adist = null;
                            double laps = 1;
                            if (m.PreserveDistances != ApplyRouteForm.PreserveDistEnum.kDontPreserveDistances)
                            {
                                adist = activity.DistanceMetersTrack;
                                if ((adist == null || adist.Count == 0 || m.ApplyLinearly))
                                {
                                    adist = new DistanceDataTrack();
                                    adist.Add(activity.StartTime, 0);
                                    if (activity.Laps.Count > 0)
                                    {
                                        float dist = 0;
                                        for (int i = 0; i < activity.Laps.Count; i++)
                                        {
                                            ILapInfo li = activity.Laps[i];
                                            DateTime t = li.StartTime.AddSeconds(li.TotalTime.TotalSeconds);

                                            if (!float.IsNaN(li.TotalDistanceMeters))
                                            {
                                                dist += li.TotalDistanceMeters;
                                                adist.Add(t, dist);
                                            }
                                        }
                                    }
                                    ActivityInfo ai = ActivityInfoCache.Instance.GetInfo(activity);
                                    if (adist.Max < ai.DistanceMeters)
                                    {
                                        adist.Add(ai.EndTime, (float)ai.DistanceMeters);
                                    }
                                }
                                double elaps = adist.Max / routes[0].TotalDistanceMeters;
                                if (m.PreserveDistances == ApplyRouteForm.PreserveDistEnum.kPreserveDistExactly)
                                {
                                    laps = elaps;
                                }
                                else if (m.PreserveDistances == ApplyRouteForm.PreserveDistEnum.kPreserveDistRounded)
                                {
                                    laps = Math.Round(elaps);
                                    if (laps == 0) laps = 1;
                                }
                            }
                            ApplyRoute(activity, routes[0], m.ApplyLinearly, adist, laps);
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
