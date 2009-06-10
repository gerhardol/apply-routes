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
            this.settingsTabs = new System.Windows.Forms.TabControl();
            this.editMenuTabPage = new System.Windows.Forms.TabPage();
            this.showApplyRoutesChk = new System.Windows.Forms.CheckBox();
            this.showCreateRoutesChk = new System.Windows.Forms.CheckBox();
            this.showUpdateEquipmentChk = new System.Windows.Forms.CheckBox();
            this.mapProvidersTabPage = new System.Windows.Forms.TabPage();
            this.mapProviderUpdateBtn = new ZoneFiveSoftware.Common.Visuals.Button();
            this.mapProviderUrl = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.mapProviderName = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.mapProvidersList = new ZoneFiveSoftware.Common.Visuals.TreeList();
            this.homePageLink = new System.Windows.Forms.LinkLabel();
            this.mapProviderResetBtn = new ZoneFiveSoftware.Common.Visuals.Button();
            this.settingsTabs.SuspendLayout();
            this.editMenuTabPage.SuspendLayout();
            this.mapProvidersTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // settingsTabs
            // 
            this.settingsTabs.Controls.Add(this.editMenuTabPage);
            this.settingsTabs.Controls.Add(this.mapProvidersTabPage);
            this.settingsTabs.Location = new System.Drawing.Point(12, 17);
            this.settingsTabs.Name = "settingsTabs";
            this.settingsTabs.SelectedIndex = 0;
            this.settingsTabs.Size = new System.Drawing.Size(456, 247);
            this.settingsTabs.TabIndex = 6;
            // 
            // editMenuTabPage
            // 
            this.editMenuTabPage.Controls.Add(this.showApplyRoutesChk);
            this.editMenuTabPage.Controls.Add(this.showCreateRoutesChk);
            this.editMenuTabPage.Controls.Add(this.showUpdateEquipmentChk);
            this.editMenuTabPage.Location = new System.Drawing.Point(4, 22);
            this.editMenuTabPage.Name = "editMenuTabPage";
            this.editMenuTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.editMenuTabPage.Size = new System.Drawing.Size(448, 221);
            this.editMenuTabPage.TabIndex = 0;
            this.editMenuTabPage.Text = "Edit Menus";
            this.editMenuTabPage.UseVisualStyleBackColor = true;
            // 
            // showApplyRoutesChk
            // 
            this.showApplyRoutesChk.AutoSize = true;
            this.showApplyRoutesChk.Location = new System.Drawing.Point(22, 45);
            this.showApplyRoutesChk.Name = "showApplyRoutesChk";
            this.showApplyRoutesChk.Size = new System.Drawing.Size(195, 17);
            this.showApplyRoutesChk.TabIndex = 2;
            this.showApplyRoutesChk.Text = "Show \'Apply Route...\" on Edit menu";
            this.showApplyRoutesChk.UseVisualStyleBackColor = true;
            // 
            // showCreateRoutesChk
            // 
            this.showCreateRoutesChk.AutoSize = true;
            this.showCreateRoutesChk.Location = new System.Drawing.Point(22, 21);
            this.showCreateRoutesChk.Name = "showCreateRoutesChk";
            this.showCreateRoutesChk.Size = new System.Drawing.Size(273, 17);
            this.showCreateRoutesChk.TabIndex = 1;
            this.showCreateRoutesChk.Text = "Show \'Create Routes From Activities...\' on Edit menu";
            this.showCreateRoutesChk.UseVisualStyleBackColor = true;
            // 
            // showUpdateEquipmentChk
            // 
            this.showUpdateEquipmentChk.AutoSize = true;
            this.showUpdateEquipmentChk.Location = new System.Drawing.Point(22, 69);
            this.showUpdateEquipmentChk.Name = "showUpdateEquipmentChk";
            this.showUpdateEquipmentChk.Size = new System.Drawing.Size(204, 17);
            this.showUpdateEquipmentChk.TabIndex = 3;
            this.showUpdateEquipmentChk.Text = "Show \'Update History...\' on Edit menu";
            this.showUpdateEquipmentChk.UseVisualStyleBackColor = true;
            // 
            // mapProvidersTabPage
            // 
            this.mapProvidersTabPage.Controls.Add(this.mapProviderResetBtn);
            this.mapProvidersTabPage.Controls.Add(this.mapProviderUpdateBtn);
            this.mapProvidersTabPage.Controls.Add(this.mapProviderUrl);
            this.mapProvidersTabPage.Controls.Add(this.mapProviderName);
            this.mapProvidersTabPage.Controls.Add(this.mapProvidersList);
            this.mapProvidersTabPage.Location = new System.Drawing.Point(4, 22);
            this.mapProvidersTabPage.Name = "mapProvidersTabPage";
            this.mapProvidersTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.mapProvidersTabPage.Size = new System.Drawing.Size(448, 221);
            this.mapProvidersTabPage.TabIndex = 1;
            this.mapProvidersTabPage.Text = "Map Providers";
            this.mapProvidersTabPage.UseVisualStyleBackColor = true;
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
            this.mapProviderUpdateBtn.Location = new System.Drawing.Point(7, 187);
            this.mapProviderUpdateBtn.Name = "mapProviderUpdateBtn";
            this.mapProviderUpdateBtn.PushStyle = true;
            this.mapProviderUpdateBtn.RightImage = null;
            this.mapProviderUpdateBtn.Size = new System.Drawing.Size(96, 23);
            this.mapProviderUpdateBtn.TabIndex = 7;
            this.mapProviderUpdateBtn.Text = "Update Provider";
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
            this.mapProviderUrl.Location = new System.Drawing.Point(132, 161);
            this.mapProviderUrl.Multiline = false;
            this.mapProviderUrl.Name = "mapProviderUrl";
            this.mapProviderUrl.ReadOnly = false;
            this.mapProviderUrl.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.mapProviderUrl.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.mapProviderUrl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.mapProviderUrl.Size = new System.Drawing.Size(292, 19);
            this.mapProviderUrl.TabIndex = 6;
            this.mapProviderUrl.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // mapProviderName
            // 
            this.mapProviderName.AcceptsReturn = false;
            this.mapProviderName.AcceptsTab = false;
            this.mapProviderName.BackColor = System.Drawing.Color.White;
            this.mapProviderName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.mapProviderName.ButtonImage = null;
            this.mapProviderName.Location = new System.Drawing.Point(7, 161);
            this.mapProviderName.Multiline = false;
            this.mapProviderName.Name = "mapProviderName";
            this.mapProviderName.ReadOnly = false;
            this.mapProviderName.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.mapProviderName.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.mapProviderName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.mapProviderName.Size = new System.Drawing.Size(119, 19);
            this.mapProviderName.TabIndex = 5;
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
            this.mapProvidersList.Location = new System.Drawing.Point(6, 6);
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
            this.mapProvidersList.Size = new System.Drawing.Size(437, 148);
            this.mapProvidersList.TabIndex = 4;
            // 
            // homePageLink
            // 
            this.homePageLink.AutoSize = true;
            this.homePageLink.Location = new System.Drawing.Point(20, 276);
            this.homePageLink.Name = "homePageLink";
            this.homePageLink.Size = new System.Drawing.Size(129, 13);
            this.homePageLink.TabIndex = 0;
            this.homePageLink.TabStop = true;
            this.homePageLink.Text = "Apply Routes Home Page";
            // 
            // mapProviderResetBtn
            // 
            this.mapProviderResetBtn.BackColor = System.Drawing.Color.Transparent;
            this.mapProviderResetBtn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.mapProviderResetBtn.CenterImage = null;
            this.mapProviderResetBtn.DialogResult = System.Windows.Forms.DialogResult.None;
            this.mapProviderResetBtn.HyperlinkStyle = false;
            this.mapProviderResetBtn.ImageMargin = 2;
            this.mapProviderResetBtn.LeftImage = null;
            this.mapProviderResetBtn.Location = new System.Drawing.Point(338, 187);
            this.mapProviderResetBtn.Name = "mapProviderResetBtn";
            this.mapProviderResetBtn.PushStyle = true;
            this.mapProviderResetBtn.RightImage = null;
            this.mapProviderResetBtn.Size = new System.Drawing.Size(104, 23);
            this.mapProviderResetBtn.TabIndex = 8;
            this.mapProviderResetBtn.Text = "Reset Defaults";
            this.mapProviderResetBtn.TextAlign = System.Drawing.StringAlignment.Center;
            this.mapProviderResetBtn.TextLeftMargin = 2;
            this.mapProviderResetBtn.TextRightMargin = 2;
            // 
            // SettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.settingsTabs);
            this.Controls.Add(this.homePageLink);
            this.Name = "SettingsControl";
            this.Size = new System.Drawing.Size(486, 310);
            this.settingsTabs.ResumeLayout(false);
            this.editMenuTabPage.ResumeLayout(false);
            this.editMenuTabPage.PerformLayout();
            this.mapProvidersTabPage.ResumeLayout(false);
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

    }
}
