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
        public UpdateEquipmentForm(IList<IActivity> activities, IList<IRoute> routes)
        {
            InitializeComponent();
            Plugin.registerDefaultBtns(this);

            this.activities = activities;
            this.routes = routes;

            if (activities != null)
            {
                int width = this.equipmentList.Width - this.equipmentList.VScrollBar.Width - 2;
                int w1 = width / 3;
                int w2 = width - 2 * w1;

                this.equipmentList.Columns.Add(new TreeList.Column("Brand", "Brand", w2, StringAlignment.Near));
                this.equipmentList.Columns.Add(new TreeList.Column("Model", "Model", w1, StringAlignment.Near));
                this.equipmentList.Columns.Add(new TreeList.Column("Type", "Type", w1, StringAlignment.Near));
                this.equipmentList.RowData = Plugin.GetApplication().Logbook.Equipment;

                commonItems = CommonEquipment();

                foreach (Object obj in commonItems)
                {
                    this.equipmentList.SetCheckedState(obj, CheckState.Checked);
                }
            }

            ITheme theme = Plugin.GetApplication().VisualTheme;

            EventHandler fromPopupClick = delegate(object sender, EventArgs e)
            {
                string selected;
                SortedList<string, string> names = GetIActivityNameList(Plugin.GetApplication().Logbook.Activities,
                        null, out selected, sender == fromNameTxt || sender == toNameTxt);

                if (names.Count != 0)
                {
                    Plugin.OpenListPopup(Plugin.GetApplication().VisualTheme,
                                         names.Values,
                                         (Control)sender, null, selected,
                                         delegate(string name)
                                         {
                                             ((ZoneFiveSoftware.Common.Visuals.TextBox)sender).Text = name;
                                         });
                }
            };

            this.fromNameTxt.ButtonClick += fromPopupClick;
            this.fromLocationTxt.ButtonClick += fromPopupClick;
            this.toNameTxt.ButtonClick += fromPopupClick;
            this.toLocationTxt.ButtonClick += fromPopupClick;

            if (activities != null)
            {
                string selected;
                SortedList<string, string> selectedNames;

                selectedNames = GetIActivityNameList(
                        activities,
                        null, out selected, true);
                if (selectedNames.Count == 1)
                {
                    selectedNamesTxt.Text = selectedNames.Values[0];
                }
                selectedNames = GetIActivityNameList(
                        activities,
                        null, out selected, false);
                if (selectedNames.Count == 1)
                {
                    selectedLocationsTxt.Text = selectedNames.Values[0];
                }
            }

            EventHandler renamePopupClick = delegate(object sender, EventArgs e)
            {
                if (activities == null)
                {
                    return;
                }

                string selected;
                SortedList<string, string> selectedNames = GetIActivityNameList(
                        activities,
                        null, out selected, sender == selectedNamesTxt);

                SortedList<string, string> names = GetIActivityNameList(
                        Plugin.GetApplication().Logbook.Activities,
                        null, out selected, sender == selectedNamesTxt);

                if (names.Count != 0)
                {
                    if (selectedNames.Count == 1)
                    {
                        selected = names[selectedNames.Keys[0]];
                    }
                    Plugin.OpenListPopup(Plugin.GetApplication().VisualTheme,
                                         names.Values,
                                         (Control)sender, null, selected,
                                         delegate(string name)
                                         {
                                             ((ZoneFiveSoftware.Common.Visuals.TextBox)sender).Text = name;
                                         });
                }
            };

            selectedNamesTxt.ButtonClick += renamePopupClick;
            selectedLocationsTxt.ButtonClick += renamePopupClick;
            /*
            IActivityCategory common = GetCommonCategory();
            if (common != null)
            {
                categoryFromTxt.Text = GetCategoryName(common);
            }*/

            selectedCategoriesTxt.ButtonClick += delegate(object sender, EventArgs e)
            {
                SortedList<string, IActivityCategory> categories = GetCategoryList();
                Plugin.OpenListPopup(Plugin.GetApplication().VisualTheme,
                                     categories.Keys,
                                     (Control)sender, null, null,
                                     delegate(string name)
                                     {
                                         ((ZoneFiveSoftware.Common.Visuals.TextBox)sender).Text = name;
                                         newCategory = categories[name];
                                     });
            };

            //TabPage renameTab = updateTab.TabPages["renameTab"];
            //TabPage globalRenameTab = updateTab.TabPages["globalRenameTab"];
            //TabPage updateEquipmentTab = updateTab.TabPages["updateEquipmentTab"];
            
            if (activities == null)
            {
                updateTab.TabPages.RemoveByKey("updateEquipmentTab");
            }
            if (activities == null && routes == null)
            {
                updateTab.TabPages.RemoveByKey("renameTab");
            //    renameTab.Enabled = false;
                updateTab.SelectTab("globalRenameTab");
            }

            ThemeChanged(theme);
        }

        public void ThemeChanged(ITheme visualTheme)
        {
            equipmentList.ThemeChanged(visualTheme);
            selectedNamesTxt.ThemeChanged(visualTheme);
            selectedLocationsTxt.ThemeChanged(visualTheme);
            fromNameTxt.ThemeChanged(visualTheme);
            toNameTxt.ThemeChanged(visualTheme);
            fromLocationTxt.ThemeChanged(visualTheme);
            toLocationTxt.ThemeChanged(visualTheme);
            selectedCategoriesTxt.ThemeChanged(visualTheme);

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
            get
            {
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
            foreach (T value in iList)
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

        public string SelectedName
        {
            get { return selectedNamesTxt.Text; }
        }

        public string SelectedLocation
        {
            get { return selectedLocationsTxt.Text; }
        }

        public string FromName
        {
            get { return CanonicalName(fromNameTxt.Text); }
        }

        public string ToName
        {
            get { return toNameTxt.Text.Trim(); }
        }

        public string FromLocation
        {
            get { return CanonicalName(fromLocationTxt.Text); }
        }

        public string ToLocation
        {
            get { return toLocationTxt.Text.Trim(); }
        }

        public IActivityCategory NewCategory
        {
            get { return newCategory; }
        }

        public static string CanonicalName(string name)
        {
            return name.Trim().ToLower();
        }

        public SortedList<string, string> GetIActivityNameList(IList<IActivity> items,
            IActivity itemToSelect, out string selected, bool useNames)
        {
            SortedList<string, string> names = new SortedList<string, string>();

            selected = null;
            if (items != null)
            {
                foreach (IActivity item in items)
                {
                    string name = (useNames ? item.Name : item.Location);
                    name = name.Trim();
                    if (name.Length > 0)
                    {
                        string lowerName = name.ToLower();
                        if (!names.ContainsKey(lowerName))
                        {
                            names.Add(lowerName, name);
                        }
                        else
                        {
                            name = names[lowerName];
                        }
                        if (itemToSelect == item)
                        {
                            selected = name;
                        }
                    }
                }
            }
            return names;
        }

        public SortedList<string, string> GetIRouteNameList(IList<IRoute> items, IRoute itemToSelect, out string selected)
        {
            SortedList<string, string> names = new SortedList<string, string>();

            selected = null;
            foreach (IRoute item in items)
            {
                string name = item.Name.Trim();
                if (name.Length > 0)
                {
                    string lowerName = name.ToLower();
                    if (!names.ContainsKey(lowerName))
                    {
                        names.Add(lowerName, name);
                    }
                    else
                    {
                        name = names[lowerName];
                    }
                    if (itemToSelect == item)
                    {
                        selected = name;
                    }
                }
            }
            return names;
        }

        private static string GetCategoryName(IActivityCategory cat)
        {
            string name = cat.Name;
            IActivityCategory c = cat;
            while ((c = c.Parent) != null)
            {
                name = c.Name + " : " + name;
            }
            return name;
        }

        private static void GetCategoriesRec(SortedList<string, IActivityCategory> list, IList<IActivityCategory> cats)
        {
            foreach (IActivityCategory cat in cats)
            {
                list.Add(GetCategoryName(cat), cat);
                GetCategoriesRec(list, cat.SubCategories);
            }
        }

        private static SortedList<string, IActivityCategory> GetCategoryList()
        {
            SortedList<string, IActivityCategory> list = new SortedList<string, IActivityCategory>();

            GetCategoriesRec(list, Plugin.GetApplication().Logbook.ActivityCategories);
            return list;
        }

        private static bool IsSubCategory(IActivityCategory root, IActivityCategory sub)
        {
            while (sub != null)
            {
                if (sub == root)
                {
                    return true;
                }
                sub = sub.Parent;
            }
            return false;
        }

        private static IActivityCategory CommonCategory(IActivityCategory root, IActivityCategory sub)
        {
            while (root != null)
            {
                if (IsSubCategory(root, sub))
                {
                    return root;
                }
                root = root.Parent;
            }
            
            return null;
        }

        private IActivityCategory GetCommonCategory()
        {
            IActivityCategory common = null;
            if (activities != null)
            {
                foreach (IActivity activity in activities)
                {
                    if (common == null)
                    {
                        common = activity.Category;
                    }
                    else
                    {
                        IActivityCategory c = CommonCategory(common, activity.Category);
                        if (c == null)
                        {
                            c = CommonCategory(activity.Category, common);
                            if (c == null)
                            {
                                break;
                            }
                        }
                        common = c;
                    }
                }
            }

            return common;
        }

        private IList<IActivity> activities = null;
        private IList<IRoute> routes = null;
        private IList<IEquipmentItem> commonItems = null;
        private IActivityCategory newCategory = null;
    }
}
