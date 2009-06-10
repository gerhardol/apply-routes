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
    public struct SettingsInfo
    {
        public SettingsInfo(bool sar, bool scr, bool sue)
        {
            showApplyRoutes = sar;
            showCreateRoutes = scr;
            showUpdateEquipment = sue;
        }

        public static SettingsInfo Get()
        {
            byte[] data = Plugin.GetApplication().Logbook.GetExtensionData(Plugin.thePlugin.Id);
            if (data == null || data.Length == 0)
            {
                data = new byte[] { 1, 1, 0 };
            }
            return new SettingsInfo(data[0] != 0, data[1] != 0, data[2] != 0);
        }

        public static void Set(SettingsInfo info)
        {
            byte[] data = new byte[] {
                (byte)(info.showApplyRoutes ? 1 : 0),
                (byte)(info.showCreateRoutes ? 1 : 0),
                (byte)(info.showUpdateEquipment ? 1 : 0)
            };
            Plugin.GetApplication().Logbook.SetExtensionData(Plugin.thePlugin.Id, data);
        }

        public bool showApplyRoutes;
        public bool showCreateRoutes;
        public bool showUpdateEquipment;
    };

    public partial class SettingsControl : UserControl
    {
        

        public SettingsControl()
        {
            InitializeComponent();
            SettingsInfo info = SettingsInfo.Get();
            
            RefreshPage();
            
            ThemeChanged(Plugin.GetApplication().VisualTheme);

            EventHandler chkChange = delegate(object sender, EventArgs e)
            {
                bool chk = ((CheckBox)sender).Checked;

                if (sender == showApplyRoutesChk)
                {
                    info.showApplyRoutes = chk;
                }
                else if (sender == showCreateRoutesChk)
                {
                    info.showCreateRoutes = chk;
                }
                else if (sender == showUpdateEquipmentChk)
                {
                    info.showUpdateEquipment = chk;
                }
                else
                {
                    return;
                }

                SettingsInfo.Set(info);
            };

            showApplyRoutesChk.CheckedChanged += chkChange;
            showCreateRoutesChk.CheckedChanged += chkChange;
            showUpdateEquipmentChk.CheckedChanged += chkChange;

            homePageLink.Click += homePageLink_Click;
        }

        public void RefreshPage()
        {
            SettingsInfo info = SettingsInfo.Get();
            
            showApplyRoutesChk.Checked = info.showApplyRoutes;
            showCreateRoutesChk.Checked = info.showCreateRoutes;
            showUpdateEquipmentChk.Checked = info.showUpdateEquipment;
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
