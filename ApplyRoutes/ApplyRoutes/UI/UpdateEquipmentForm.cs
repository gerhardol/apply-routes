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

    File: ApplyRoutes/UI/UpdateEquipmentForm.cs
***********************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Data.GPS;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Data.Measurement;

namespace ApplyRoutesPlugin.UI
{
    

    public partial class UpdateEquipmentForm : Form
    {
        public UpdateEquipmentForm(IList<IActivity> activities)
        {
            InitializeComponent();
            Plugin.registerDefaultBtns(this);

            this.activities = activities;
            int width = this.equipmentList.Width - this.equipmentList.VScrollBar.Width - 2;
            int w1 = width / 3;
            int w2 = width - 2 * w1;

            this.equipmentList.Columns.Add(new TreeList.Column("Brand", "Brand", w2, StringAlignment.Near));
            this.equipmentList.Columns.Add(new TreeList.Column("Model", "Model", w1, StringAlignment.Near));
            this.equipmentList.Columns.Add(new TreeList.Column("Type", "Type", w1, StringAlignment.Near));
            this.equipmentList.RowData = Plugin.GetApplication().Logbook.Equipment;

            commonItems = CommonEquipment();
            /*
            
                IList<IEquipmentItem> usedEquipment = UsedEquipment();
                foreach (Object obj in usedEquipment)
                {
                    this.equipmentList.SetCheckedState(obj, CheckState.Indeterminate);
                }
            */
            foreach (Object obj in commonItems)
            {
                this.equipmentList.SetCheckedState(obj, CheckState.Checked);
            }
            
            ThemeChanged(Plugin.GetApplication().VisualTheme);
        }

        public void ThemeChanged(ITheme visualTheme)
        {
            Plugin.ThemeChanged(equipmentLbl, visualTheme);
            equipmentList.ThemeChanged(visualTheme);

            Plugin.ThemeChanged(okBtn, visualTheme);
            Plugin.ThemeChanged(cancelBtn, visualTheme);
        }

        public IList<IEquipmentItem> DiffList(IList<IEquipmentItem> list, IList<IEquipmentItem> sublist, bool sub)
        {
            for (int i = list.Count; i-- != 0; )
            {
                if (sublist.Contains(list[i]) == sub)
                {
                    list.RemoveAt(i);
                }
            }
            return list;
        }

        public IList<IEquipmentItem> CommonEquipment()
        {
            IList<IEquipmentItem> list = new List<IEquipmentItem>(Plugin.GetApplication().Logbook.Equipment);
            foreach (IActivity activity in activities)
            {
                DiffList(list, activity.EquipmentUsed, false);
                if (list.Count == 0)
                {
                    break;
                }
            }
            return list;
        }

        public IList<IEquipmentItem> UsedEquipment()
        {
            IList<IEquipmentItem> list = new List<IEquipmentItem>();
            foreach (IActivity activity in activities)
            {
                foreach (IEquipmentItem item in activity.EquipmentUsed)
                {
                    if (!list.Contains(item))
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public IList<IEquipmentItem> EquipmentToAdd
        {
            get {
                return DiffList(ConvertToListOf<IEquipmentItem>(equipmentList.CheckedElements),
                                commonItems, true);
            }
        }

        public IList<IEquipmentItem> EquipmentToRemove
        {
            get
            {
                return DiffList(new List<IEquipmentItem>(commonItems),
                                ConvertToListOf<IEquipmentItem>(equipmentList.CheckedElements),
                                true);
            }
        }

        public static IList<T> ConvertToListOf<T>(IList iList)  
        {  
            IList<T> result = new List<T>();  
            foreach(T value in iList)
            {  
                result.Add(value);
            }

            return result;
        }

        public static IList ConvertFromListOf<T>(IList<T> iList)
        {
            ArrayList result = new ArrayList();
            foreach (T value in iList)
            {
                result.Add(value);
            }

            return result;
        }

        private IList<IActivity> activities = null;
        private IList<IEquipmentItem> commonItems = null;
    }
}
