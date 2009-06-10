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

    File: ApplyRoutes/UI/SettingsControl.cs
***********************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using ZoneFiveSoftware.Common.Visuals;
using System.Diagnostics;

namespace ApplyRoutesPlugin.UI
{
    public partial class SettingsControl : UserControl
    {
        public SettingsControl()
        {
            InitializeComponent();
            byte[] data = Plugin.GetApplication().Logbook.GetExtensionData(Plugin.thePlugin.Id);
            if (data == null || data.Length == 0)
            {
                data = new byte[] { 1, 1, 0 };
                Plugin.GetApplication().Logbook.SetExtensionData(Plugin.thePlugin.Id, data);
            }

            RefreshPage();
            
            ThemeChanged(Plugin.GetApplication().VisualTheme);

            EventHandler chkChange = delegate(object sender, EventArgs e)
            {
                int i = -1;
                if (sender == showApplyRoutesChk)
                {
                    i = 0;
                }
                else if (sender == showCreateRoutesChk)
                {
                    i = 1;
                }
                else if (sender == showUpdateEquipmentChk)
                {
                    i = 2;
                }

                if (i >= 0)
                {
                    data[i] = (byte)(((CheckBox)sender).Checked ? 1 : 0);
                    Plugin.GetApplication().Logbook.SetExtensionData(Plugin.thePlugin.Id, data);
                }
            };

            showApplyRoutesChk.CheckedChanged += chkChange;
            showCreateRoutesChk.CheckedChanged += chkChange;
            showUpdateEquipmentChk.CheckedChanged += chkChange;

            homePageLink.Click += homePageLink_Click;
        }

        public void RefreshPage()
        {
            byte[] data = Plugin.GetApplication().Logbook.GetExtensionData(Plugin.thePlugin.Id);
            if (data == null || data.Length == 0)
            {
                data = new byte[] { 1, 1, 0 };
                Plugin.GetApplication().Logbook.SetExtensionData(Plugin.thePlugin.Id, data);
            }

            showApplyRoutesChk.Checked = data[0] != 0;
            showCreateRoutesChk.Checked = data[1] != 0;
            showUpdateEquipmentChk.Checked = data[2] != 0;
        }

        public void ThemeChanged(ITheme theme)
        {
            Plugin.ThemeChanged(showApplyRoutesChk, theme);
            Plugin.ThemeChanged(showCreateRoutesChk, theme);
            Plugin.ThemeChanged(showUpdateEquipmentChk, theme);
        }

        public void UICultureChanged(CultureInfo culture)
        {
        }

        private void homePageLink_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessStartInfo procStartInfo = new ProcessStartInfo("http://sporttracks.myosotissp.com/applyroutesplugin.html");
                Process.Start(procStartInfo);
            }
            catch // (Exception ex)
            {
                MessageBox.Show("Exception encountered launching browser", "Launching other application", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
    }
}
