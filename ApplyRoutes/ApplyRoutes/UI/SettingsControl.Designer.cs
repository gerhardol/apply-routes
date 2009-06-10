namespace ApplyRoutesPlugin.UI
{
    partial class SettingsControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsControl));
            this.settingsTabs = new System.Windows.Forms.TabControl();
            this.editMenuTabPage = new System.Windows.Forms.TabPage();
            this.showJoinRoutesChk = new System.Windows.Forms.CheckBox();
            this.showSendToRoutesChk = new System.Windows.Forms.CheckBox();
            this.showApplyRoutesChk = new System.Windows.Forms.CheckBox();
            this.showCreateRoutesChk = new System.Windows.Forms.CheckBox();
            this.showUpdateEquipmentChk = new System.Windows.Forms.CheckBox();
            this.mapProvidersTabPage = new System.Windows.Forms.TabPage();
            this.mapProviderResetBtn = new ZoneFiveSoftware.Common.Visuals.Button();
            this.mapProviderUpdateBtn = new ZoneFiveSoftware.Common.Visuals.Button();
            this.mapProviderUrl = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.mapProviderName = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.mapProvidersList = new ZoneFiveSoftware.Common.Visuals.TreeList();
            this.homePageLink = new System.Windows.Forms.LinkLabel();
            this.settingsTabs.SuspendLayout();
            this.editMenuTabPage.SuspendLayout();
            this.mapProvidersTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // settingsTabs
            // 
            this.settingsTabs.Controls.Add(this.editMenuTabPage);
            this.settingsTabs.Controls.Add(this.mapProvidersTabPage);
            resources.ApplyResources(this.settingsTabs, "settingsTabs");
            this.settingsTabs.Name = "settingsTabs";
            this.settingsTabs.SelectedIndex = 0;
            // 
            // editMenuTabPage
            // 
            this.editMenuTabPage.Controls.Add(this.showJoinRoutesChk);
            this.editMenuTabPage.Controls.Add(this.showSendToRoutesChk);
            this.editMenuTabPage.Controls.Add(this.showApplyRoutesChk);
            this.editMenuTabPage.Controls.Add(this.showCreateRoutesChk);
            this.editMenuTabPage.Controls.Add(this.showUpdateEquipmentChk);
            resources.ApplyResources(this.editMenuTabPage, "editMenuTabPage");
            this.editMenuTabPage.Name = "editMenuTabPage";
            this.editMenuTabPage.UseVisualStyleBackColor = true;
            // 
            // showJoinRoutesChk
            // 
            resources.ApplyResources(this.showJoinRoutesChk, "showJoinRoutesChk");
            this.showJoinRoutesChk.Name = "showJoinRoutesChk";
            this.showJoinRoutesChk.UseVisualStyleBackColor = true;
            // 
            // showSendToRoutesChk
            // 
            resources.ApplyResources(this.showSendToRoutesChk, "showSendToRoutesChk");
            this.showSendToRoutesChk.Name = "showSendToRoutesChk";
            this.showSendToRoutesChk.UseVisualStyleBackColor = true;
            // 
            // showApplyRoutesChk
            // 
            resources.ApplyResources(this.showApplyRoutesChk, "showApplyRoutesChk");
            this.showApplyRoutesChk.Name = "showApplyRoutesChk";
            this.showApplyRoutesChk.UseVisualStyleBackColor = true;
            // 
            // showCreateRoutesChk
            // 
            resources.ApplyResources(this.showCreateRoutesChk, "showCreateRoutesChk");
            this.showCreateRoutesChk.Name = "showCreateRoutesChk";
            this.showCreateRoutesChk.UseVisualStyleBackColor = true;
            // 
            // showUpdateEquipmentChk
            // 
            resources.ApplyResources(this.showUpdateEquipmentChk, "showUpdateEquipmentChk");
            this.showUpdateEquipmentChk.Name = "showUpdateEquipmentChk";
            this.showUpdateEquipmentChk.UseVisualStyleBackColor = true;
            // 
            // mapProvidersTabPage
            // 
            this.mapProvidersTabPage.Controls.Add(this.mapProviderResetBtn);
            this.mapProvidersTabPage.Controls.Add(this.mapProviderUpdateBtn);
            this.mapProvidersTabPage.Controls.Add(this.mapProviderUrl);
            this.mapProvidersTabPage.Controls.Add(this.mapProviderName);
            this.mapProvidersTabPage.Controls.Add(this.mapProvidersList);
            resources.ApplyResources(this.mapProvidersTabPage, "mapProvidersTabPage");
            this.mapProvidersTabPage.Name = "mapProvidersTabPage";
            this.mapProvidersTabPage.UseVisualStyleBackColor = true;
            // 
            // mapProviderResetBtn
            // 
            resources.ApplyResources(this.mapProviderResetBtn, "mapProviderResetBtn");
            this.mapProviderResetBtn.BackColor = System.Drawing.Color.Transparent;
            this.mapProviderResetBtn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.mapProviderResetBtn.CenterImage = null;
            this.mapProviderResetBtn.DialogResult = System.Windows.Forms.DialogResult.None;
            this.mapProviderResetBtn.HyperlinkStyle = false;
            this.mapProviderResetBtn.ImageMargin = 2;
            this.mapProviderResetBtn.LeftImage = null;
            this.mapProviderResetBtn.Name = "mapProviderResetBtn";
            this.mapProviderResetBtn.PushStyle = true;
            this.mapProviderResetBtn.RightImage = null;
            this.mapProviderResetBtn.TextAlign = System.Drawing.StringAlignment.Center;
            this.mapProviderResetBtn.TextLeftMargin = 2;
            this.mapProviderResetBtn.TextRightMargin = 2;
            // 
            // mapProviderUpdateBtn
            // 
            this.mapProviderUpdateBtn.BackColor = System.Drawing.Color.Transparent;
            this.mapProviderUpdateBtn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.mapProviderUpdateBtn.CenterImage = null;
            this.mapProviderUpdateBtn.DialogResult = System.Windows.Forms.DialogResult.None;
            this.mapProviderUpdateBtn.HyperlinkStyle = false;
            this.mapProviderUpdateBtn.ImageMargin = 2;
            this.mapProviderUpdateBtn.LeftImage = null;
            resources.ApplyResources(this.mapProviderUpdateBtn, "mapProviderUpdateBtn");
            this.mapProviderUpdateBtn.Name = "mapProviderUpdateBtn";
            this.mapProviderUpdateBtn.PushStyle = true;
            this.mapProviderUpdateBtn.RightImage = null;
            this.mapProviderUpdateBtn.TextAlign = System.Drawing.StringAlignment.Center;
            this.mapProviderUpdateBtn.TextLeftMargin = 2;
            this.mapProviderUpdateBtn.TextRightMargin = 2;
            // 
            // mapProviderUrl
            // 
            this.mapProviderUrl.AcceptsReturn = false;
            this.mapProviderUrl.AcceptsTab = false;
            this.mapProviderUrl.BackColor = System.Drawing.Color.White;
            this.mapProviderUrl.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.mapProviderUrl.ButtonImage = null;
            resources.ApplyResources(this.mapProviderUrl, "mapProviderUrl");
            this.mapProviderUrl.MaxLength = 32767;
            this.mapProviderUrl.Multiline = false;
            this.mapProviderUrl.Name = "mapProviderUrl";
            this.mapProviderUrl.ReadOnly = false;
            this.mapProviderUrl.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.mapProviderUrl.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.mapProviderUrl.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // mapProviderName
            // 
            this.mapProviderName.AcceptsReturn = false;
            this.mapProviderName.AcceptsTab = false;
            this.mapProviderName.BackColor = System.Drawing.Color.White;
            this.mapProviderName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.mapProviderName.ButtonImage = null;
            resources.ApplyResources(this.mapProviderName, "mapProviderName");
            this.mapProviderName.MaxLength = 32767;
            this.mapProviderName.Multiline = false;
            this.mapProviderName.Name = "mapProviderName";
            this.mapProviderName.ReadOnly = false;
            this.mapProviderName.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.mapProviderName.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.mapProviderName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // mapProvidersList
            // 
            this.mapProvidersList.BackColor = System.Drawing.Color.Transparent;
            this.mapProvidersList.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.SmallRoundShadow;
            this.mapProvidersList.CheckBoxes = false;
            this.mapProvidersList.DefaultIndent = 15;
            this.mapProvidersList.DefaultRowHeight = -1;
            this.mapProvidersList.HeaderRowHeight = 21;
            resources.ApplyResources(this.mapProvidersList, "mapProvidersList");
            this.mapProvidersList.MultiSelect = false;
            this.mapProvidersList.Name = "mapProvidersList";
            this.mapProvidersList.NumHeaderRows = ZoneFiveSoftware.Common.Visuals.TreeList.HeaderRows.Auto;
            this.mapProvidersList.NumLockedColumns = 0;
            this.mapProvidersList.RowAlternatingColors = true;
            this.mapProvidersList.RowHotlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(49)))), ((int)(((byte)(106)))), ((int)(((byte)(197)))));
            this.mapProvidersList.RowHotlightColorText = System.Drawing.SystemColors.HighlightText;
            this.mapProvidersList.RowHotlightMouse = true;
            this.mapProvidersList.RowSelectedColor = System.Drawing.SystemColors.Highlight;
            this.mapProvidersList.RowSelectedColorText = System.Drawing.SystemColors.HighlightText;
            this.mapProvidersList.RowSeparatorLines = true;
            this.mapProvidersList.ShowLines = false;
            this.mapProvidersList.ShowPlusMinus = false;
            // 
            // homePageLink
            // 
            resources.ApplyResources(this.homePageLink, "homePageLink");
            this.homePageLink.Name = "homePageLink";
            this.homePageLink.TabStop = true;
            // 
            // SettingsControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.settingsTabs);
            this.Controls.Add(this.homePageLink);
            this.Name = "SettingsControl";
            this.settingsTabs.ResumeLayout(false);
            this.editMenuTabPage.ResumeLayout(false);
            this.editMenuTabPage.PerformLayout();
            this.mapProvidersTabPage.ResumeLayout(false);
            this.mapProvidersTabPage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel homePageLink;
        private System.Windows.Forms.CheckBox showCreateRoutesChk;
        private System.Windows.Forms.CheckBox showApplyRoutesChk;
        private System.Windows.Forms.CheckBox showUpdateEquipmentChk;
        private ZoneFiveSoftware.Common.Visuals.TreeList mapProvidersList;
        private System.Windows.Forms.TabPage editMenuTabPage;
        private System.Windows.Forms.TabPage mapProvidersTabPage;
        private ZoneFiveSoftware.Common.Visuals.Button mapProviderUpdateBtn;
        private ZoneFiveSoftware.Common.Visuals.TextBox mapProviderUrl;
        private ZoneFiveSoftware.Common.Visuals.TextBox mapProviderName;
        private System.Windows.Forms.TabControl settingsTabs;
        private ZoneFiveSoftware.Common.Visuals.Button mapProviderResetBtn;
        private System.Windows.Forms.CheckBox showSendToRoutesChk;
        private System.Windows.Forms.CheckBox showJoinRoutesChk;

    }
}
