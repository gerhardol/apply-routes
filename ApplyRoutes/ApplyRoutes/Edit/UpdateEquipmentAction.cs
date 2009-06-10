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
        public UpdateEquipmentAction(IList<IActivity> activities)
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

        public void Run(Rectangle rectButton)
        {
            UpdateEquipmentForm m = new UpdateEquipmentForm(activities);
            if (m.ShowDialog() == DialogResult.OK)
            {
                UpdateEquipment(m.EquipmentToAdd, true);
                UpdateEquipment(m.EquipmentToRemove, false);
            }
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
    }
}
