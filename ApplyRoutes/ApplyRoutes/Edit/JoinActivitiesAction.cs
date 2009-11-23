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

    File: ApplyRoutes/Edit/JoinActivitiesAction.cs
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
    class JoinActivitiesAction : IAction
    {
        public JoinActivitiesAction(IList<IActivity> activities)
        {
            this.activities = activities;
        }

        #region IAction Members

        public bool Enabled
        {
            get { return activities != null && activities.Count > 1 && checkSalistSpan(SAList());}
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

        private SortedList<DateTime, IActivity> SAList()
        {
            SortedList<DateTime, IActivity> salist = new SortedList<DateTime, IActivity>();
            foreach (IActivity activity in activities)
            {
                if (salist.ContainsKey(activity.StartTime))
                    return null;

                salist.Add(activity.StartTime, activity);
            }
            return salist;
        }

        private bool checkSalistSpan(SortedList<DateTime, IActivity> salist)
        {
            if (salist == null)
            {
                return false;
            }

            /*Determine whether ST can handle tracks longer than 65535 seconds */
            NumericTimeDataSeries ntd = new NumericTimeDataSeries();
            DateTime st = DateTime.FromBinary(0);
            ntd.Add(st, 0);
            DateTime et = st.AddDays(5);
            ntd.Add(et, 0);
            if (ntd.EntryDateTime(ntd[1]) != et)
            {
                /* It cant handle long tracks */
                DateTime start = salist.Keys[0];
                IActivity last = salist.Values[salist.Count - 1];
                ActivityInfo ai = ActivityInfoCache.Instance.GetInfo(last);

                TimeSpan span = TimeSpan.FromSeconds(0);
                DateTime endTime = start;

                /* Take account of overlaps between segments:
                 *  if a segment starts before the previous one
                 *  finished, we will have to adjust its start time,
                 *  which will make the finished track longer.
                 */
                for (int i = 0; i < salist.Count; i++)
                {
                    IActivity activity = salist.Values[i];
                    if (activity.StartTime < endTime)
                    {
                        span = span.Add(endTime.Subtract(activity.StartTime));
                    }
                    ai = ActivityInfoCache.Instance.GetInfo(activity);
                    endTime = ai.EndTime;
                }

                span = span.Add(endTime.Subtract(start));
                return span.TotalSeconds <= 65535;
            }
            else
            {
                return true;
            }
        }

        private void Append<T>(ITimeDataSeries<T> dst, ITimeDataSeries<T> src, TimeSpan adjust, ref DateTime end)
        {
            if (src != null)
            {
                for (int i = 0; i < src.Count; i++)
                {
                    ITimeValueEntry<T> tpt = src[i];
                    DateTime when = src.EntryDateTime(tpt).Add(adjust);
                    dst.Add(when, tpt.Value);
                    if (when > end)
                    {
                        end = when;
                    }
                }
            }
        }

        private void AppendOff(ITimeDataSeries<float> dst, ITimeDataSeries<float> src, TimeSpan adjust, ref DateTime end, float offset)
        {
            if (src != null)
            {
                for (int i = 0; i < src.Count; i++)
                {
                    ITimeValueEntry<float> tpt = src[i];
                    float value = tpt.Value + offset;
                    DateTime when = src.EntryDateTime(tpt).Add(adjust);
                    dst.Add(when, value);
                    if (when > end)
                    {
                        end = when;
                    }
                }
            }
        }

        public void Run(Rectangle rectButton)
        {
            if (activities == null || activities.Count <= 1)
            {
                return;
            }

            SortedList<DateTime, IActivity> salist = SAList();
            if (!checkSalistSpan(salist))
            {
                return;
            }

            IActivity first = salist.Values[0];
            first.QueueEvents = true;
            GPSRoute route = new GPSRoute();
            DistanceDataTrack ddt = new DistanceDataTrack();
            NumericTimeDataSeries cpmt = new NumericTimeDataSeries();
            NumericTimeDataSeries emt = new NumericTimeDataSeries();
            NumericTimeDataSeries hrt = new NumericTimeDataSeries();
            NumericTimeDataSeries pwt = new NumericTimeDataSeries();
            DateTime endTime = first.StartTime;

            double dist = 0;
            TimeSpan adjust = TimeSpan.FromSeconds(0);

            for (int i = 0; i < salist.Count; i++)
            {
                IActivity activity = salist.Values[i];
                ActivityInfo ai = ActivityInfoCache.Instance.GetInfo(activity);
                DateTime prevEnd = endTime;
                if (activity.StartTime < prevEnd)
                {
                    adjust = adjust.Add(prevEnd.Subtract(activity.StartTime));
                }

                Append(route, activity.GPSRoute, adjust, ref endTime);
                AppendOff(ddt, activity.DistanceMetersTrack, adjust, ref endTime, ddt.Count > 0 ? ddt.Max : 0);
                Append(cpmt, activity.CadencePerMinuteTrack, adjust, ref endTime);
                Append(emt, activity.ElevationMetersTrack, adjust, ref endTime);
                Append(hrt, activity.HeartRatePerMinuteTrack, adjust, ref endTime);
                Append(pwt, activity.PowerWattsTrack, adjust, ref endTime);

                if (i > 0)
                {
                    first.TotalAscendMetersEntered += activity.TotalAscendMetersEntered;
                    first.TotalCalories += activity.TotalCalories;
                    first.TotalDescendMetersEntered += activity.TotalDescendMetersEntered;
                    first.TotalDistanceMetersEntered += activity.TotalDistanceMetersEntered;
                    first.TotalTimeEntered += activity.TotalTimeEntered;
                    if (first.Notes != "" && activity.Notes != "")
                    {
                        //first.Notes += "\n--\n";
                    }
                    first.Notes += activity.Notes;
                    if (activity.MaximumCadencePerMinuteEntered > first.MaximumCadencePerMinuteEntered)
                        first.MaximumCadencePerMinuteEntered = activity.MaximumCadencePerMinuteEntered;
                    if (activity.MaximumHeartRatePerMinuteEntered > first.MaximumHeartRatePerMinuteEntered)
                        first.MaximumHeartRatePerMinuteEntered = activity.MaximumHeartRatePerMinuteEntered;
                    if (activity.MaximumPowerWattsEntered > first.MaximumPowerWattsEntered)
                        first.MaximumPowerWattsEntered = activity.MaximumPowerWattsEntered;

                    foreach (float d in activity.DistanceMarkersMeters)
                    {
                        first.DistanceMarkersMeters.Add((float)(dist + d));
                    }

                    foreach (ILapInfo lap in activity.Laps)
                    {
                        first.Laps.Add(lap.StartTime.Add(adjust), lap.TotalTime);
                        ILapInfo li = first.Laps[first.Laps.Count - 1];
                        li.AverageCadencePerMinute = lap.AverageCadencePerMinute;
                        li.AverageHeartRatePerMinute = lap.AverageHeartRatePerMinute;
                        li.AveragePowerWatts = lap.AveragePowerWatts;
                        li.ElevationChangeMeters = lap.ElevationChangeMeters;
                        li.TotalCalories = lap.TotalCalories;
                        li.TotalDistanceMeters = lap.TotalDistanceMeters;
                    }

                    if (activity.StartTime > prevEnd) {
                        first.TimerPauses.Add(new ValueRange<DateTime>(prevEnd, activity.StartTime));
                    }

                    foreach (IValueRange<DateTime> dtr in activity.TimerPauses)
                    {
                        first.TimerPauses.Add(dtr);
                    }

                    Plugin.GetApplication().Logbook.Activities.Remove(activity);
                }
                dist += ai.DistanceMeters;
            }

            if (route.Count == 0) route = null;
            if (ddt.Count == 0) ddt = null;
            if (cpmt.Count == 0) cpmt = null;
            if (emt.Count == 0) emt = null;
            if (hrt.Count == 0) hrt = null;
            if (pwt.Count == 0) pwt = null;

            first.GPSRoute = route;
            first.DistanceMetersTrack = ddt;
            first.CadencePerMinuteTrack = cpmt;
            first.ElevationMetersTrack = emt;
            first.HeartRatePerMinuteTrack = hrt;
            first.PowerWattsTrack = pwt;
            first.QueueEvents = false;
        }

        public string Title
        {
            get {
                int num = Enabled ? activities.Count : 0;
                return Plugin.NumberedActivityText(Properties.Resources.Edit_JoinRoutesAction_Text, num);
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

        private IList<IActivity> activities = null;
    }
}
