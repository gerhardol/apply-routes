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
#if !ST_2_1
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Visuals.Util;
#endif

using ApplyRoutesPlugin.UI;
using System.Reflection;

namespace ApplyRoutesPlugin.Edit
{
    class JoinActivitiesAction : IAction
    {
#if !ST_2_1
        public JoinActivitiesAction(IDailyActivityView aview, IActivityReportsView rview)
        {
            this.dailyView = aview;
            this.reportView = rview;
        }
#else
        public JoinActivitiesAction(IList<IActivity> activities, IList<IRoute> dummy)
        {
            this.activities = activities;
        }
#endif
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

#if ST_2_1
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
#endif
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

            Type lapType = null;
            PropertyInfo lapRest = null;
            PropertyInfo lapNotes = null;
            foreach (IActivity a in salist.Values)
            {
                if (a.Laps.Count > 0)
                {
                    lapType = a.Laps[0].GetType();
                    break;
                }
            }

            if (lapType != null)
            {
                lapRest = lapType.GetProperty("Rest");
                lapNotes = lapType.GetProperty("Notes");
                if (lapRest != null && !(lapRest.CanRead && lapRest.CanWrite))
                    lapRest = null;
                if (lapNotes != null && !(lapNotes.CanRead && lapNotes.CanWrite))
                    lapNotes = null;
            }

            IActivity result =
#if ST_2_1
                 salist.Values[0];
           result.QueueEvents = true;
            bool hasCopy = false;
                    Type type = result.GetType();
                    IActivity tmp = (IActivity)Activator.CreateInstance(type);
                    MethodInfo theMethod = type.GetMethod("CopyTo");
                    if (tmp != null && theMethod != null)
                    {
                        hasCopy = true;
                        theMethod.Invoke(null, new object [] {
                                result, tmp
                        });
                        result = tmp;
                        Plugin.GetApplication().Logbook.Activities.Add(result);
                    }
#else
              Plugin.GetApplication().Logbook.Activities.AddCopy(salist.Values[0]);
#endif
            String note = Plugin.NumberedActivityText(Properties.Resources.Edit_JoinedActivities_Text,
                    salist.Count);

            if (result.Notes != "")
            {
                note += "\r\n--\r\n";
            }

            result.Notes += note;

            GPSRoute route = new GPSRoute();
            DistanceDataTrack ddt = new DistanceDataTrack();
            NumericTimeDataSeries cpmt = new NumericTimeDataSeries();
            NumericTimeDataSeries emt = new NumericTimeDataSeries();
            NumericTimeDataSeries hrt = new NumericTimeDataSeries();
            NumericTimeDataSeries pwt = new NumericTimeDataSeries();
            DateTime endTime = result.StartTime;

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
                    result.TotalAscendMetersEntered += activity.TotalAscendMetersEntered;
                    result.TotalCalories += activity.TotalCalories;
                    result.TotalDescendMetersEntered += activity.TotalDescendMetersEntered;
                    result.TotalDistanceMetersEntered += activity.TotalDistanceMetersEntered;
                    result.TotalTimeEntered += activity.TotalTimeEntered;
                    if (result.Notes != "" && activity.Notes != "")
                    {
                        result.Notes += "\r\n--\r\n";
                    }
                    result.Notes += activity.Notes;
                    if (activity.MaximumCadencePerMinuteEntered > result.MaximumCadencePerMinuteEntered)
                        result.MaximumCadencePerMinuteEntered = activity.MaximumCadencePerMinuteEntered;
                    if (activity.MaximumHeartRatePerMinuteEntered > result.MaximumHeartRatePerMinuteEntered)
                        result.MaximumHeartRatePerMinuteEntered = activity.MaximumHeartRatePerMinuteEntered;
                    if (activity.MaximumPowerWattsEntered > result.MaximumPowerWattsEntered)
                        result.MaximumPowerWattsEntered = activity.MaximumPowerWattsEntered;

                    foreach (float d in activity.DistanceMarkersMeters)
                    {
                        result.DistanceMarkersMeters.Add((float)(dist + d));
                    }

                    if (activity.Laps.Count > 0)
                    {
                        foreach (ILapInfo lap in activity.Laps)
                        {
                            result.Laps.Add(lap.StartTime.Add(adjust), lap.TotalTime);
                            ILapInfo li = result.Laps[result.Laps.Count - 1];
                            li.AverageCadencePerMinute = lap.AverageCadencePerMinute;
                            li.AverageHeartRatePerMinute = lap.AverageHeartRatePerMinute;
                            li.AveragePowerWatts = lap.AveragePowerWatts;
                            li.ElevationChangeMeters = lap.ElevationChangeMeters;
                            li.TotalCalories = lap.TotalCalories;
                            li.TotalDistanceMeters = lap.TotalDistanceMeters;
                            if (lapRest != null)
                            {
                                lapRest.SetValue(li, lapRest.GetValue(lap, null), null);
                            }
                            if (lapNotes != null)
                            {
                                lapNotes.SetValue(li, lapNotes.GetValue(lap, null), null);
                            }
                        }
                    }
                    else
                    {
                        result.Laps.Add(activity.StartTime.Add(adjust), ai.EndTime.Subtract(activity.StartTime));
                        ILapInfo li = result.Laps[result.Laps.Count - 1];
                        li.AverageCadencePerMinute = ai.AverageCadence;
                        li.AverageHeartRatePerMinute = ai.AverageHeartRate;
                        li.AveragePowerWatts = ai.AveragePower;
                        li.TotalCalories = activity.TotalCalories;
                        li.TotalDistanceMeters = (float)ai.DistanceMeters;
                    }

                    if (activity.StartTime > prevEnd)
                    {
                        result.TimerPauses.Add(new ValueRange<DateTime>(prevEnd, activity.StartTime));
                    }

                    foreach (IValueRange<DateTime> dtr in activity.TimerPauses)
                    {
                        result.TimerPauses.Add(dtr);
                    }

#if ST_2_1
                    if (!hasCopy)
                    {
                        /*
                         * If we didnt manage to make a copy (for whatever reason - perhaps
                         * ST changed the CopyTo method) its better to delete the
                         * remaining laps
                         */
                        Plugin.GetApplication().Logbook.Activities.Remove(activity);
                    }
#endif
                }
                dist += ai.DistanceMeters;
            }

            if (route.Count == 0) route = null;
            if (ddt.Count == 0) ddt = null;
            if (cpmt.Count == 0) cpmt = null;
            if (emt.Count == 0) emt = null;
            if (hrt.Count == 0) hrt = null;
            if (pwt.Count == 0) pwt = null;

            result.GPSRoute = route;
            result.DistanceMetersTrack = ddt;
            result.CadencePerMinuteTrack = cpmt;
            result.ElevationMetersTrack = emt;
            result.HeartRatePerMinuteTrack = hrt;
            result.PowerWattsTrack = pwt;
#if ST_2_1
            result.QueueEvents = false;
#endif
        }

        public string Title
        {
            get {
                int num = Enabled ? activities.Count : 0;
                return Plugin.NumberedActivityText(Properties.Resources.Edit_JoinRoutesAction_Text, num);
            }
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
