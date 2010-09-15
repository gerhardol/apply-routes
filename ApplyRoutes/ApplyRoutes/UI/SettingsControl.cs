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
using ApplyRoutesPlugin.MapProviders;
using System.Xml;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ApplyRoutesPlugin.Activities;

namespace ApplyRoutesPlugin.UI
{

    public partial class SettingsControl : UserControl
    {
        public SettingsControl()
        {
            InitializeComponent();
            EditMenuSettingsInfo info = EditMenuSettingsInfo.Get();

            settingsTabs.DrawMode = TabDrawMode.OwnerDrawFixed;
            settingsTabs.DrawItem += new DrawItemEventHandler(Plugin.tab_DrawItem);

            RefreshPage();

            uploadUrlTxt.Text = ExtendMapProviders.UploadURL;

            uploadUrlTxt.TextChanged += delegate(object sender, EventArgs e)
            {
                ExtendMapProviders.UploadURL = uploadUrlTxt.Text;
            };

            uploadResetBtn.Click += delegate(object sender, EventArgs e)
            {
                ExtendMapProviders.UploadURL = null;
                uploadUrlTxt.Text = ExtendMapProviders.UploadURL;
            };

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
                else if (sender == showSendToRoutesChk)
                {
                    info.showSendToRoutes = chk;
                }
                else if (sender == showJoinRoutesChk)
                {
                    info.showJoinRoutes = chk;
                }
                else
                {
                    return;
                }
            };

            showApplyRoutesChk.CheckedChanged += chkChange;
            showCreateRoutesChk.CheckedChanged += chkChange;
            showUpdateEquipmentChk.CheckedChanged += chkChange;
            showSendToRoutesChk.CheckedChanged += chkChange;
            showJoinRoutesChk.CheckedChanged += chkChange;

            int w1 = (mapProviderName.Right + mapProviderUrl.Left)/2 - mapProvidersList.Left;
            int w2 = mapProvidersList.Width - mapProvidersList.VScrollBar.Width - 2 - w1;
            mapProvidersList.Columns.Add(new TreeList.Column("Title", Properties.Resources.SettingsControl_Title, w1, StringAlignment.Near));
            mapProvidersList.Columns.Add(new TreeList.Column("Url", Properties.Resources.SettingsControl_URL, w2, StringAlignment.Near));
            mapProvidersList.RowData = ExtendMapProviders.GetMapProviders();
            mapProvidersList.CheckBoxes = true;
            foreach (GMapProvider p in ExtendMapProviders.GetMapProviders())
            {
                mapProvidersList.SetChecked(p, p.Enabled);
            }
            mapProvidersList.CheckedChanged += delegate(object sender, TreeList.ItemEventArgs e)
            {
                GMapProvider item = e.Item as GMapProvider;
                if (item != null)
                {
                    item.Enabled = mapProvidersList.CheckedElements.Contains(item);
                }
            };
#if ST_2_1
            mapProvidersList.SelectedChanged += delegate(object sender, EventArgs e)
#else
            mapProvidersList.SelectedItemsChanged += delegate(object sender, EventArgs e)
#endif 
            {
                if (mapProvidersList.SelectedItems.Count == 1)
                {
                    GMapProvider item = mapProvidersList.SelectedItems[0] as GMapProvider;
                    mapProviderName.Text = item.Title;
                    mapProviderUrl.Text = item.Url;
                    mapProviderUpdateBtn.Enabled = true;
                } else {
                    mapProviderName.Text = mapProviderUrl.Text = "";
                    mapProviderUpdateBtn.Enabled = false;
                }
            };

            mapProviderUpdateBtn.Click += delegate(object sender, EventArgs e)
            {
                if (mapProvidersList.SelectedItems.Count == 1)
                {
                    GMapProvider item = mapProvidersList.SelectedItems[0] as GMapProvider;
                    item.Title = mapProviderName.Text;
                    item.Url = mapProviderUrl.Text;
                    mapProvidersList.RefreshElements(mapProvidersList.Selected);
                    mapProvidersList.Selected.Clear();
                    GMapRouteControl.ResetMapTypes();
                }
            };

            mapProviderResetBtn.Click += delegate(object sender, EventArgs e)
            {
                ExtendMapProviders.ApplyDefaults();
                mapProvidersList.Invalidate();
                GMapRouteControl.ResetMapTypes();
            };

            mapProviderUpdateBtn.Enabled = false;

            homePageLink.Click += homePageLink_Click;
        }

        public void RefreshPage()
        {
            EditMenuSettingsInfo info = EditMenuSettingsInfo.Get();
            
            showApplyRoutesChk.Checked = info.showApplyRoutes;
            showCreateRoutesChk.Checked = info.showCreateRoutes;
            showUpdateEquipmentChk.Checked = info.showUpdateEquipment;
            showSendToRoutesChk.Checked = info.showSendToRoutes;
            showJoinRoutesChk.Checked = info.showJoinRoutes;
        }

        public void ThemeChanged(ITheme theme)
        {
            Plugin.ThemeChanged(this, theme);

            Plugin.ThemeChanged(showApplyRoutesChk, theme);
            Plugin.ThemeChanged(showCreateRoutesChk, theme);
            Plugin.ThemeChanged(showUpdateEquipmentChk, theme);
            Plugin.ThemeChanged(showSendToRoutesChk, theme);
            Plugin.ThemeChanged(showJoinRoutesChk, theme);

            mapProvidersList.ThemeChanged(theme);
            mapProviderName.ThemeChanged(theme);
            mapProviderUrl.ThemeChanged(theme);

            Plugin.ThemeChanged(mapProviderUpdateBtn, theme);
            Plugin.ThemeChanged(mapProviderResetBtn, theme);
            Plugin.ThemeChanged(this.settingsTabs, theme);
            Plugin.ThemeChanged(this.editMenuTabPage, theme);
            Plugin.ThemeChanged(this.mapProvidersTabPage, theme);
            Plugin.ThemeChanged(this.uploadTabPage, theme);

            Plugin.ThemeChanged(uploadHeadingLbl, theme);
            Plugin.ThemeChanged(uploadUrlLbl, theme);
            uploadUrlTxt.ThemeChanged(theme);
            Plugin.ThemeChanged(uploadResetBtn, theme);
        }

        public void UICultureChanged(CultureInfo culture)
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsControl));
            resources.ApplyResources(this.editMenuTabPage, "editMenuTabPage");
            resources.ApplyResources(this.uploadTabPage, "uploadTabPage");
            resources.ApplyResources(this.homePageLink, "homePageLink");
            resources.ApplyResources(this.mapProviderResetBtn, "mapProviderResetBtn");
            resources.ApplyResources(this.mapProvidersTabPage, "mapProvidersTabPage");
            resources.ApplyResources(this.mapProviderUpdateBtn, "mapProviderUpdateBtn");
            resources.ApplyResources(this.showApplyRoutesChk, "showApplyRoutesChk");
            resources.ApplyResources(this.showCreateRoutesChk, "showCreateRoutesChk");
            resources.ApplyResources(this.showJoinRoutesChk, "showJoinRoutesChk");
            resources.ApplyResources(this.showSendToRoutesChk, "showSendToRoutesChk");
            resources.ApplyResources(this.showUpdateEquipmentChk, "showUpdateEquipmentChk");
            resources.ApplyResources(this.uploadHeadingLbl, "uploadHeadingLbl");
            resources.ApplyResources(this.uploadUrlLbl, "uploadUrlLbl");
            resources.ApplyResources(this.uploadResetBtn, "uploadResetBtn");
            mapProvidersList.Columns[0].Text = Properties.Resources.SettingsControl_Title;
            mapProvidersList.Columns[1].Text = Properties.Resources.SettingsControl_URL;
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
    public class EditMenuSettingsInfo
    {
        public EditMenuSettingsInfo(bool sar, bool scr, bool sue, bool sst, bool sjr, bool srr)
        {
            showApplyRoutes = sar;
            showCreateRoutes = scr;
            showUpdateEquipment = sue;
            showSendToRoutes = sst;
            showJoinRoutes = sjr;
            showRRUpload = srr;
        }

        public static EditMenuSettingsInfo Get()
        {
            if (emsi == null)
            {
                byte[] data = null;
                IApplication app = Plugin.GetApplication();
                if (app != null && app.Logbook != null)
                {
                    data = app.Logbook.GetExtensionData(GUIDs.PluginMain);
                }
                if (data != null && data.Length == 3)
                {
                    emsi = new EditMenuSettingsInfo(data[0] != 0, data[1] != 0, data[2] != 0, true, true, true);
                }
                else
                {
                    emsi = new EditMenuSettingsInfo(true, true, false, true, true, true);
                }
            }
            return emsi;
        }

        public static void ReadOptions(XmlDocument xmlDoc, XmlNamespaceManager nsmgr, XmlElement pluginNode)
        {
            XmlAttribute sar = pluginNode.Attributes["showApplyRoutes"];
            XmlAttribute scr = pluginNode.Attributes["showCreateRoutes"];
            XmlAttribute sue = pluginNode.Attributes["showUpdateEquipment"];
            XmlAttribute sst = pluginNode.Attributes["showSendToRoutes"];
            XmlAttribute sjr = pluginNode.Attributes["showJoinRoutes"];
            XmlAttribute srr = pluginNode.Attributes["showRRUpload"];


            bool sarb = sar == null || sar.Value == "1";
            bool scrb = scr == null || scr.Value == "1";
            //Action included in ST3 but not ST2, but not visible by default
            bool sueb = sue != null && sue.Value == "1";
            bool sstb = sst == null || sst.Value == "1";
#if ST_2_1
            //Action included in ST3 but not ST2
            bool sjrb = sjr == null || sjr.Value == "1";
#else
            bool sjrb = sjr != null && sjr.Value == "1";
#endif
            //ReplayRoutes.com not working right now
            bool srrb = srr != null && srr.Value == "1";

            emsi = new EditMenuSettingsInfo(sarb, scrb, sueb, sstb, sjrb, srrb);
        }

        private static XmlAttribute GetOrCreate(XmlDocument xmlDoc, XmlNode node, string name)
        {
            XmlAttribute att = node.Attributes[name];
            if (att == null)
            {
                att = xmlDoc.CreateAttribute(name);
                node.Attributes.Append(att);
            }
            return att;
        }

        public static void WriteOptions(XmlDocument xmlDoc, XmlElement pluginNode)
        {
            XmlAttribute sar = GetOrCreate(xmlDoc, pluginNode, "showApplyRoutes");
            XmlAttribute scr = GetOrCreate(xmlDoc, pluginNode, "showCreateRoutes");
            XmlAttribute sue = GetOrCreate(xmlDoc, pluginNode, "showUpdateEquipment");
            XmlAttribute sst = GetOrCreate(xmlDoc, pluginNode, "showSendToRoutes");
            XmlAttribute sjr = GetOrCreate(xmlDoc, pluginNode, "showJoinRoutes");
            XmlAttribute srr = GetOrCreate(xmlDoc, pluginNode, "showRRUpload");

            EditMenuSettingsInfo e = Get();
            sar.Value = e.showApplyRoutes ? "1" : "0";
            scr.Value = e.showCreateRoutes ? "1" : "0";
            sue.Value = e.showUpdateEquipment ? "1" : "0";
            sst.Value = e.showSendToRoutes ? "1" : "0";
            sjr.Value = e.showJoinRoutes ? "1" : "0";
            srr.Value = e.showRRUpload ? "1" : "0";
        }

        public bool showApplyRoutes;
        public bool showCreateRoutes;
        public bool showUpdateEquipment;
        public bool showSendToRoutes;
        public bool showJoinRoutes;
        public bool showRRUpload;

        private static EditMenuSettingsInfo emsi = null;
    };
}
