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

    File: ApplyRoutes/Edit/UpdateEquipmentAction.cs
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
    class UpdateEquipmentAction : IAction
    {
        public UpdateEquipmentAction(IList<IActivity> activities, IList<IRoute> routes)
        {
            this.activities = activities;
            this.routes = routes;
        }

        #region IAction Members

        public bool Enabled
        {
            get { return true; }
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

        public void Run(Rectangle rectButton)
        {
            UpdateEquipmentForm m = new UpdateEquipmentForm(activities, null);
            if (m.ShowDialog() == DialogResult.OK)
            {
                if (activities != null)
                {
                    UpdateEquipment(m.EquipmentToAdd, true);
                    UpdateEquipment(m.EquipmentToRemove, false);
                }

                if (m.SelectedName != "")
                {
                    if (activities != null)
                    {
                        foreach (IActivity activity in activities)
                        {
                            activity.Name = m.SelectedName;
                        }
                    }
                    if (routes != null)
                    {
                        foreach (IRoute route in routes)
                        {
                            route.Name = m.SelectedName;
                        }
                    }
                }

                if (m.SelectedLocation != "")
                {
                    if (activities != null)
                    {
                        foreach (IActivity activity in activities)
                        {
                            activity.Location = m.SelectedLocation;
                        }
                    }
                    if (routes != null)
                    {
                        foreach (IRoute route in routes)
                        {
                            route.Location = m.SelectedLocation;
                        }
                    }
                }

                if (m.NewCategory != null)
                {
                    if (activities != null)
                    {
                        foreach (IActivity activity in activities)
                        {
                            activity.Category = m.NewCategory;
                        }
                    }
                }

                if (m.FromName != "" && m.ToName != "")
                {
                    foreach (IActivity activity in Plugin.GetApplication().Logbook.Activities)
                    {
                        if (UpdateEquipmentForm.CanonicalName(activity.Name) == m.FromName)
                        {
                            activity.Name = m.ToName;
                        }
                    }

                    foreach (IRoute route in Plugin.GetApplication().Logbook.Routes)
                    {
                        if (UpdateEquipmentForm.CanonicalName(route.Name) == m.FromName)
                        {
                            route.Name = m.ToName;
                        }
                    }
                }

                if (m.FromLocation != "" && m.ToLocation != "")
                {
                    foreach (IActivity activity in Plugin.GetApplication().Logbook.Activities)
                    {
                        if (UpdateEquipmentForm.CanonicalName(activity.Location) == m.FromLocation)
                        {
                            activity.Location = m.ToLocation;
                        }
                    }

                    foreach (IRoute route in Plugin.GetApplication().Logbook.Routes)
                    {
                        if (UpdateEquipmentForm.CanonicalName(route.Location) == m.FromLocation)
                        {
                            route.Location = m.ToLocation;
                        }
                    }
                }
            }
            m.Dispose();
        }

        public string Title
        {
            get { return Properties.Resources.Edit_UpdateEquipment_Text; }
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
        private IList<IRoute> routes = null;
    }
}
