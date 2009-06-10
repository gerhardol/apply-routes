namespace ApplyRoutesPlugin.UI
{
    partial class UpdateEquipmentForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateEquipmentForm));
            this.equipmentList = new ZoneFiveSoftware.Common.Visuals.TreeList();
            this.okBtn = new ZoneFiveSoftware.Common.Visuals.Button();
            this.cancelBtn = new ZoneFiveSoftware.Common.Visuals.Button();
            this.updateTab = new System.Windows.Forms.TabControl();
            this.renameTab = new System.Windows.Forms.TabPage();
            this.selectedCategoriesTxt = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.selectedLocationsTxt = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.selectedNamesTxt = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.globalRenameTab = new System.Windows.Forms.TabPage();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.toLocationTxt = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.fromLocationTxt = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.toNameTxt = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.fromNameTxt = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.updateEquipmentTab = new System.Windows.Forms.TabPage();
            this.updateTab.SuspendLayout();
            this.renameTab.SuspendLayout();
            this.globalRenameTab.SuspendLayout();
            this.updateEquipmentTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // equipmentList
            // 
            this.equipmentList.BackColor = System.Drawing.Color.Transparent;
            this.equipmentList.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.SmallRoundShadow;
            this.equipmentList.CheckBoxes = true;
            this.equipmentList.DefaultIndent = 15;
            this.equipmentList.DefaultRowHeight = -1;
            this.equipmentList.HeaderRowHeight = 42;
            resources.ApplyResources(this.equipmentList, "equipmentList");
            this.equipmentList.MultiSelect = false;
            this.equipmentList.Name = "equipmentList";
            this.equipmentList.NumHeaderRows = ZoneFiveSoftware.Common.Visuals.TreeList.HeaderRows.One;
            this.equipmentList.NumLockedColumns = 0;
            this.equipmentList.RowAlternatingColors = true;
            this.equipmentList.RowHotlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(49)))), ((int)(((byte)(106)))), ((int)(((byte)(197)))));
            this.equipmentList.RowHotlightColorText = System.Drawing.SystemColors.HighlightText;
            this.equipmentList.RowHotlightMouse = true;
            this.equipmentList.RowSelectedColor = System.Drawing.SystemColors.Highlight;
            this.equipmentList.RowSelectedColorText = System.Drawing.SystemColors.HighlightText;
            this.equipmentList.RowSeparatorLines = true;
            this.equipmentList.ShowLines = false;
            this.equipmentList.ShowPlusMinus = false;
            // 
            // okBtn
            // 
            this.okBtn.BackColor = System.Drawing.Color.Transparent;
            this.okBtn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.okBtn.CenterImage = null;
            this.okBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okBtn.HyperlinkStyle = false;
            this.okBtn.ImageMargin = 2;
            this.okBtn.LeftImage = null;
            resources.ApplyResources(this.okBtn, "okBtn");
            this.okBtn.Name = "okBtn";
            this.okBtn.PushStyle = true;
            this.okBtn.RightImage = null;
            this.okBtn.TextAlign = System.Drawing.StringAlignment.Center;
            this.okBtn.TextLeftMargin = 2;
            this.okBtn.TextRightMargin = 2;
            // 
            // cancelBtn
            // 
            this.cancelBtn.BackColor = System.Drawing.Color.Transparent;
            this.cancelBtn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.cancelBtn.CausesValidation = false;
            this.cancelBtn.CenterImage = null;
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.HyperlinkStyle = false;
            this.cancelBtn.ImageMargin = 2;
            this.cancelBtn.LeftImage = null;
            resources.ApplyResources(this.cancelBtn, "cancelBtn");
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.PushStyle = true;
            this.cancelBtn.RightImage = null;
            this.cancelBtn.TextAlign = System.Drawing.StringAlignment.Center;
            this.cancelBtn.TextLeftMargin = 2;
            this.cancelBtn.TextRightMargin = 2;
            // 
            // updateTab
            // 
            this.updateTab.Controls.Add(this.renameTab);
            this.updateTab.Controls.Add(this.globalRenameTab);
            this.updateTab.Controls.Add(this.updateEquipmentTab);
            resources.ApplyResources(this.updateTab, "updateTab");
            this.updateTab.Name = "updateTab";
            this.updateTab.SelectedIndex = 0;
            // 
            // renameTab
            // 
            this.renameTab.Controls.Add(this.selectedCategoriesTxt);
            this.renameTab.Controls.Add(this.label10);
            this.renameTab.Controls.Add(this.label7);
            this.renameTab.Controls.Add(this.selectedLocationsTxt);
            this.renameTab.Controls.Add(this.label6);
            this.renameTab.Controls.Add(this.selectedNamesTxt);
            this.renameTab.Controls.Add(this.label1);
            resources.ApplyResources(this.renameTab, "renameTab");
            this.renameTab.Name = "renameTab";
            this.renameTab.UseVisualStyleBackColor = true;
            // 
            // selectedCategoriesTxt
            // 
            this.selectedCategoriesTxt.AcceptsReturn = false;
            this.selectedCategoriesTxt.AcceptsTab = false;
            this.selectedCategoriesTxt.BackColor = System.Drawing.Color.White;
            this.selectedCategoriesTxt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.selectedCategoriesTxt.ButtonImage = global::ApplyRoutesPlugin.Properties.Resources.DropDown8;
            resources.ApplyResources(this.selectedCategoriesTxt, "selectedCategoriesTxt");
            this.selectedCategoriesTxt.MaxLength = 32767;
            this.selectedCategoriesTxt.Multiline = false;
            this.selectedCategoriesTxt.Name = "selectedCategoriesTxt";
            this.selectedCategoriesTxt.ReadOnly = false;
            this.selectedCategoriesTxt.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.selectedCategoriesTxt.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.selectedCategoriesTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // selectedLocationsTxt
            // 
            this.selectedLocationsTxt.AcceptsReturn = false;
            this.selectedLocationsTxt.AcceptsTab = false;
            this.selectedLocationsTxt.BackColor = System.Drawing.Color.White;
            this.selectedLocationsTxt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.selectedLocationsTxt.ButtonImage = global::ApplyRoutesPlugin.Properties.Resources.DropDown8;
            resources.ApplyResources(this.selectedLocationsTxt, "selectedLocationsTxt");
            this.selectedLocationsTxt.MaxLength = 32767;
            this.selectedLocationsTxt.Multiline = false;
            this.selectedLocationsTxt.Name = "selectedLocationsTxt";
            this.selectedLocationsTxt.ReadOnly = false;
            this.selectedLocationsTxt.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.selectedLocationsTxt.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.selectedLocationsTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // selectedNamesTxt
            // 
            this.selectedNamesTxt.AcceptsReturn = false;
            this.selectedNamesTxt.AcceptsTab = false;
            this.selectedNamesTxt.BackColor = System.Drawing.Color.White;
            this.selectedNamesTxt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.selectedNamesTxt.ButtonImage = global::ApplyRoutesPlugin.Properties.Resources.DropDown8;
            resources.ApplyResources(this.selectedNamesTxt, "selectedNamesTxt");
            this.selectedNamesTxt.MaxLength = 32767;
            this.selectedNamesTxt.Multiline = false;
            this.selectedNamesTxt.Name = "selectedNamesTxt";
            this.selectedNamesTxt.ReadOnly = false;
            this.selectedNamesTxt.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.selectedNamesTxt.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.selectedNamesTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // globalRenameTab
            // 
            this.globalRenameTab.Controls.Add(this.label9);
            this.globalRenameTab.Controls.Add(this.label8);
            this.globalRenameTab.Controls.Add(this.label4);
            this.globalRenameTab.Controls.Add(this.toLocationTxt);
            this.globalRenameTab.Controls.Add(this.fromLocationTxt);
            this.globalRenameTab.Controls.Add(this.label5);
            this.globalRenameTab.Controls.Add(this.label3);
            this.globalRenameTab.Controls.Add(this.toNameTxt);
            this.globalRenameTab.Controls.Add(this.fromNameTxt);
            this.globalRenameTab.Controls.Add(this.label2);
            resources.ApplyResources(this.globalRenameTab, "globalRenameTab");
            this.globalRenameTab.Name = "globalRenameTab";
            this.globalRenameTab.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // toLocationTxt
            // 
            this.toLocationTxt.AcceptsReturn = false;
            this.toLocationTxt.AcceptsTab = false;
            this.toLocationTxt.BackColor = System.Drawing.Color.White;
            this.toLocationTxt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.toLocationTxt.ButtonImage = global::ApplyRoutesPlugin.Properties.Resources.DropDown8;
            resources.ApplyResources(this.toLocationTxt, "toLocationTxt");
            this.toLocationTxt.MaxLength = 32767;
            this.toLocationTxt.Multiline = false;
            this.toLocationTxt.Name = "toLocationTxt";
            this.toLocationTxt.ReadOnly = false;
            this.toLocationTxt.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.toLocationTxt.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.toLocationTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // fromLocationTxt
            // 
            this.fromLocationTxt.AcceptsReturn = false;
            this.fromLocationTxt.AcceptsTab = false;
            this.fromLocationTxt.BackColor = System.Drawing.Color.White;
            this.fromLocationTxt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.fromLocationTxt.ButtonImage = global::ApplyRoutesPlugin.Properties.Resources.DropDown8;
            resources.ApplyResources(this.fromLocationTxt, "fromLocationTxt");
            this.fromLocationTxt.MaxLength = 32767;
            this.fromLocationTxt.Multiline = false;
            this.fromLocationTxt.Name = "fromLocationTxt";
            this.fromLocationTxt.ReadOnly = true;
            this.fromLocationTxt.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.fromLocationTxt.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.fromLocationTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // toNameTxt
            // 
            this.toNameTxt.AcceptsReturn = false;
            this.toNameTxt.AcceptsTab = false;
            this.toNameTxt.BackColor = System.Drawing.Color.White;
            this.toNameTxt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.toNameTxt.ButtonImage = global::ApplyRoutesPlugin.Properties.Resources.DropDown8;
            resources.ApplyResources(this.toNameTxt, "toNameTxt");
            this.toNameTxt.MaxLength = 32767;
            this.toNameTxt.Multiline = false;
            this.toNameTxt.Name = "toNameTxt";
            this.toNameTxt.ReadOnly = false;
            this.toNameTxt.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.toNameTxt.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.toNameTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // fromNameTxt
            // 
            this.fromNameTxt.AcceptsReturn = false;
            this.fromNameTxt.AcceptsTab = false;
            this.fromNameTxt.BackColor = System.Drawing.Color.White;
            this.fromNameTxt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.fromNameTxt.ButtonImage = global::ApplyRoutesPlugin.Properties.Resources.DropDown8;
            resources.ApplyResources(this.fromNameTxt, "fromNameTxt");
            this.fromNameTxt.MaxLength = 32767;
            this.fromNameTxt.Multiline = false;
            this.fromNameTxt.Name = "fromNameTxt";
            this.fromNameTxt.ReadOnly = true;
            this.fromNameTxt.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.fromNameTxt.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.fromNameTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // updateEquipmentTab
            // 
            this.updateEquipmentTab.Controls.Add(this.equipmentList);
            resources.ApplyResources(this.updateEquipmentTab, "updateEquipmentTab");
            this.updateEquipmentTab.Name = "updateEquipmentTab";
            this.updateEquipmentTab.UseVisualStyleBackColor = true;
            // 
            // UpdateEquipmentForm
            // 
            this.AcceptButton = this.okBtn;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBtn;
            this.Controls.Add(this.updateTab);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.Name = "UpdateEquipmentForm";
            this.updateTab.ResumeLayout(false);
            this.renameTab.ResumeLayout(false);
            this.renameTab.PerformLayout();
            this.globalRenameTab.ResumeLayout(false);
            this.globalRenameTab.PerformLayout();
            this.updateEquipmentTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ZoneFiveSoftware.Common.Visuals.TreeList equipmentList;
        private ZoneFiveSoftware.Common.Visuals.Button okBtn;
        private ZoneFiveSoftware.Common.Visuals.Button cancelBtn;
        private System.Windows.Forms.TabControl updateTab;
        private System.Windows.Forms.TabPage updateEquipmentTab;
        private System.Windows.Forms.TabPage renameTab;
        private System.Windows.Forms.Label label1;
        private ZoneFiveSoftware.Common.Visuals.TextBox selectedNamesTxt;
        private ZoneFiveSoftware.Common.Visuals.TextBox selectedLocationsTxt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabPage globalRenameTab;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private ZoneFiveSoftware.Common.Visuals.TextBox toLocationTxt;
        private ZoneFiveSoftware.Common.Visuals.TextBox fromLocationTxt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private ZoneFiveSoftware.Common.Visuals.TextBox toNameTxt;
        private ZoneFiveSoftware.Common.Visuals.TextBox fromNameTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
        private ZoneFiveSoftware.Common.Visuals.TextBox selectedCategoriesTxt;
        private System.Windows.Forms.Label label10;
    }
}