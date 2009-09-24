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

    File: ApplyRoutes/Plugin.cs
***********************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Drawing;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Visuals;
using ApplyRoutesPlugin.MapProviders;
using ApplyRoutesPlugin.UI;
using ZoneFiveSoftware.Common.Data;

namespace ApplyRoutesPlugin
{
    public delegate void ItemSelectHandler<T>(T item);

    class Plugin : IPlugin
    {
        public Plugin()
        {
            plugin = this;
        }

        #region IPlugin Members

        public IApplication Application
        {
            set { application = value; }
        }

        public Guid Id
        {
            get { return new Guid("{a2dcf469-3d83-4690-8702-a21cd18ff7b3}"); }
        }

        public string Name
        {
            get { return Properties.Resources.Edit_ApplyRoutesPlugin_Text; }
        }

        public void ReadOptions(XmlDocument xmlDoc, XmlNamespaceManager nsmgr, XmlElement pluginNode)
        {
            ExtendMapProviders.ReadOptions(xmlDoc, nsmgr, pluginNode);
            EditMenuSettingsInfo.ReadOptions(xmlDoc, nsmgr, pluginNode);
        }

        public string Version
        {
            get { return GetType().Assembly.GetName().Version.ToString(3); }
        }

        public void WriteOptions(XmlDocument xmlDoc, XmlElement pluginNode)
        {
            ExtendMapProviders.WriteOptions(xmlDoc, pluginNode);
            EditMenuSettingsInfo.WriteOptions(xmlDoc, pluginNode);
        }

        #endregion

        public static Plugin thePlugin
        {
            get { return plugin; }
        }
        public static IApplication GetApplication()
        {
            return application;
        }

        public static void dialogButton_Click(object sender, EventArgs e)
        {
            if (typeof(IButtonControl).IsInstanceOfType(sender) &&
                typeof(Control).IsInstanceOfType(sender))
            {
                IButtonControl ibtn = (IButtonControl)sender;
                Control ctrl = (Control)sender;
                if (typeof(Form).IsInstanceOfType(ctrl.Parent))
                {
                    if (ibtn.DialogResult != DialogResult.None)
                    {
                        Form form = (Form)ctrl.Parent;
                        form.DialogResult = ibtn.DialogResult;
                        form.Close();
                    }
                }
            }
        }

        public static void SetIButtonClick(IButtonControl button)
        {
            if (typeof(Control).IsInstanceOfType(button))
            {
                ((Control)button).Click += new System.EventHandler(dialogButton_Click);
            }
        }

        public static void registerDefaultBtns(Form form)
        {
            SetIButtonClick(form.AcceptButton);
            SetIButtonClick(form.CancelButton);
            SetIButtonClick(null);
        }

        static public void ThemeChanged(Control control, ITheme visualTheme)
        {
            control.ForeColor = visualTheme.ControlText;
            control.BackColor = visualTheme.Control;
        }

        internal static void tab_DrawItem(object sender, DrawItemEventArgs e)
        {
            ITheme theme = Plugin.GetApplication().VisualTheme;
            Brush backBrush;
            Brush foreBrush = new SolidBrush(theme.ControlText);
            TabControl tc = sender as TabControl;
            if (tc == null) return;

            if (e.Index == tc.SelectedIndex)
            {
                backBrush = new SolidBrush(theme.Control);
            }
            else
            {
                backBrush = new SolidBrush(theme.Window);
            }

            string tabName = tc.TabPages[e.Index].Text;
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            e.Graphics.FillRectangle(backBrush, e.Bounds);
            Rectangle r = e.Bounds;
            r = new Rectangle(r.X, r.Y + 3, r.Width, r.Height - 3);
            e.Graphics.DrawString(tabName, e.Font, foreBrush, r, sf);

            sf.Dispose();
            backBrush.Dispose();
            foreBrush.Dispose();

            e.DrawFocusRectangle();
        }

        public static void OpenListPopup<T>(ITheme theme, IList<T> items, System.Windows.Forms.Control control, string Id, T selected, ItemSelectHandler<T> selectHandler)
        {
            TreeListPopup popup = new TreeListPopup();
            popup.ThemeChanged(theme);
            popup.Tree.Columns.Add(new TreeList.Column(Id));
            popup.Tree.RowData = items;
            if (selected != null)
            {
                popup.Tree.Selected = new object[] { selected };
            }
            popup.ItemSelected += delegate(object sender, TreeListPopup.ItemSelectedEventArgs e)
            {
                if (e.Item is T)
                {
                    selectHandler((T)e.Item);
                }
            };
            popup.Popup(control.Parent.RectangleToScreen(control.Bounds));
        }

        public static string NumberedActivityText(string tmplt, int num)
        {
            string act_text = num == 1 ? Properties.Resources.Generic_Activity_Text :
                    Properties.Resources.Generic_Activities_Text;
            string num_text = num == 1 ? Properties.Resources.Generic_One_Text :
                num == 2 ? Properties.Resources.Generic_Two_Text :
                num == 3 ? Properties.Resources.Generic_Three_Text :
                num.ToString();

            return string.Format(tmplt, num_text, act_text);
        }

        public static long GetElapsedSeconds<T>(ITimeDataSeries<T> s, ITimeValueEntry<T> e)
        {
            TimeSpan span = s.EntryDateTime(e).Subtract(s.StartTime);
            return Convert.ToInt64(span.TotalSeconds);
        }

        public static long GetTotalElapsedSeconds<T>(ITimeDataSeries<T> s)
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

        #region Private members
        private static IApplication application;
        private static Plugin plugin;
        #endregion   
    }
}
