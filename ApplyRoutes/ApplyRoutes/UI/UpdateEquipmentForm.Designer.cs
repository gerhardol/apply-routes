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
            this.equipmentList = new ZoneFiveSoftware.Common.Visuals.TreeList();
            this.okBtn = new ZoneFiveSoftware.Common.Visuals.Button();
            this.cancelBtn = new ZoneFiveSoftware.Common.Visuals.Button();
            this.updateTab = new System.Windows.Forms.TabControl();
            this.renameTab = new System.Windows.Forms.TabPage();
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
            this.selectedCategoriesTxt = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.label10 = new System.Windows.Forms.Label();
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
            this.equipmentList.Location = new System.Drawing.Point(9, 6);
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
            this.equipmentList.Size = new System.Drawing.Size(525, 185);
            this.equipmentList.TabIndex = 4;
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
            this.okBtn.Location = new System.Drawing.Point(378, 244);
            this.okBtn.Name = "okBtn";
            this.okBtn.PushStyle = true;
            this.okBtn.RightImage = null;
            this.okBtn.Size = new System.Drawing.Size(75, 23);
            this.okBtn.TabIndex = 8;
            this.okBtn.Text = "OK";
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
            this.cancelBtn.Location = new System.Drawing.Point(485, 244);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.PushStyle = true;
            this.cancelBtn.RightImage = null;
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 9;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.TextAlign = System.Drawing.StringAlignment.Center;
            this.cancelBtn.TextLeftMargin = 2;
            this.cancelBtn.TextRightMargin = 2;
            // 
            // updateTab
            // 
            this.updateTab.Controls.Add(this.renameTab);
            this.updateTab.Controls.Add(this.globalRenameTab);
            this.updateTab.Controls.Add(this.updateEquipmentTab);
            this.updateTab.Location = new System.Drawing.Point(12, 12);
            this.updateTab.Name = "updateTab";
            this.updateTab.SelectedIndex = 0;
            this.updateTab.Size = new System.Drawing.Size(548, 226);
            this.updateTab.TabIndex = 23;
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
            this.renameTab.Location = new System.Drawing.Point(4, 22);
            this.renameTab.Name = "renameTab";
            this.renameTab.Padding = new System.Windows.Forms.Padding(3);
            this.renameTab.Size = new System.Drawing.Size(540, 200);
            this.renameTab.TabIndex = 1;
            this.renameTab.Text = "Rename";
            this.renameTab.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(159, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "For all selected activities/routes:";
            // 
            // selectedLocationsTxt
            // 
            this.selectedLocationsTxt.AcceptsReturn = false;
            this.selectedLocationsTxt.AcceptsTab = false;
            this.selectedLocationsTxt.BackColor = System.Drawing.Color.White;
            this.selectedLocationsTxt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.selectedLocationsTxt.ButtonImage = global::ApplyRoutesPlugin.Properties.Resources.DropDown8;
            this.selectedLocationsTxt.Location = new System.Drawing.Point(123, 98);
            this.selectedLocationsTxt.Multiline = false;
            this.selectedLocationsTxt.Name = "selectedLocationsTxt";
            this.selectedLocationsTxt.ReadOnly = false;
            this.selectedLocationsTxt.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.selectedLocationsTxt.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.selectedLocationsTxt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.selectedLocationsTxt.Size = new System.Drawing.Size(411, 19);
            this.selectedLocationsTxt.TabIndex = 7;
            this.selectedLocationsTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Change Locations to:";
            // 
            // selectedNamesTxt
            // 
            this.selectedNamesTxt.AcceptsReturn = false;
            this.selectedNamesTxt.AcceptsTab = false;
            this.selectedNamesTxt.BackColor = System.Drawing.Color.White;
            this.selectedNamesTxt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.selectedNamesTxt.ButtonImage = global::ApplyRoutesPlugin.Properties.Resources.DropDown8;
            this.selectedNamesTxt.Location = new System.Drawing.Point(123, 49);
            this.selectedNamesTxt.Multiline = false;
            this.selectedNamesTxt.Name = "selectedNamesTxt";
            this.selectedNamesTxt.ReadOnly = false;
            this.selectedNamesTxt.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.selectedNamesTxt.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.selectedNamesTxt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.selectedNamesTxt.Size = new System.Drawing.Size(411, 19);
            this.selectedNamesTxt.TabIndex = 1;
            this.selectedNamesTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Change Names to:";
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
            this.globalRenameTab.Location = new System.Drawing.Point(4, 22);
            this.globalRenameTab.Name = "globalRenameTab";
            this.globalRenameTab.Size = new System.Drawing.Size(540, 200);
            this.globalRenameTab.TabIndex = 2;
            this.globalRenameTab.Text = "Global Rename";
            this.globalRenameTab.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 105);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(248, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Change \"Location\" field of all Activities and Routes";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(235, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Change \"Name\" field of all Activities and Routes";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 157);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "To";
            // 
            // toLocationTxt
            // 
            this.toLocationTxt.AcceptsReturn = false;
            this.toLocationTxt.AcceptsTab = false;
            this.toLocationTxt.BackColor = System.Drawing.Color.White;
            this.toLocationTxt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.toLocationTxt.ButtonImage = global::ApplyRoutesPlugin.Properties.Resources.DropDown8;
            this.toLocationTxt.Location = new System.Drawing.Point(45, 157);
            this.toLocationTxt.Multiline = false;
            this.toLocationTxt.Name = "toLocationTxt";
            this.toLocationTxt.ReadOnly = false;
            this.toLocationTxt.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.toLocationTxt.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.toLocationTxt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toLocationTxt.Size = new System.Drawing.Size(487, 19);
            this.toLocationTxt.TabIndex = 14;
            this.toLocationTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // fromLocationTxt
            // 
            this.fromLocationTxt.AcceptsReturn = false;
            this.fromLocationTxt.AcceptsTab = false;
            this.fromLocationTxt.BackColor = System.Drawing.Color.White;
            this.fromLocationTxt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.fromLocationTxt.ButtonImage = global::ApplyRoutesPlugin.Properties.Resources.DropDown8;
            this.fromLocationTxt.Location = new System.Drawing.Point(45, 132);
            this.fromLocationTxt.Multiline = false;
            this.fromLocationTxt.Name = "fromLocationTxt";
            this.fromLocationTxt.ReadOnly = true;
            this.fromLocationTxt.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.fromLocationTxt.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.fromLocationTxt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fromLocationTxt.Size = new System.Drawing.Size(487, 19);
            this.fromLocationTxt.TabIndex = 13;
            this.fromLocationTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 134);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "From";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "To";
            // 
            // toNameTxt
            // 
            this.toNameTxt.AcceptsReturn = false;
            this.toNameTxt.AcceptsTab = false;
            this.toNameTxt.BackColor = System.Drawing.Color.White;
            this.toNameTxt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.toNameTxt.ButtonImage = global::ApplyRoutesPlugin.Properties.Resources.DropDown8;
            this.toNameTxt.Location = new System.Drawing.Point(45, 75);
            this.toNameTxt.Multiline = false;
            this.toNameTxt.Name = "toNameTxt";
            this.toNameTxt.ReadOnly = false;
            this.toNameTxt.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.toNameTxt.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.toNameTxt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toNameTxt.Size = new System.Drawing.Size(487, 19);
            this.toNameTxt.TabIndex = 8;
            this.toNameTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // fromNameTxt
            // 
            this.fromNameTxt.AcceptsReturn = false;
            this.fromNameTxt.AcceptsTab = false;
            this.fromNameTxt.BackColor = System.Drawing.Color.White;
            this.fromNameTxt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.fromNameTxt.ButtonImage = global::ApplyRoutesPlugin.Properties.Resources.DropDown8;
            this.fromNameTxt.Location = new System.Drawing.Point(45, 50);
            this.fromNameTxt.Multiline = false;
            this.fromNameTxt.Name = "fromNameTxt";
            this.fromNameTxt.ReadOnly = true;
            this.fromNameTxt.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.fromNameTxt.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.fromNameTxt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fromNameTxt.Size = new System.Drawing.Size(487, 19);
            this.fromNameTxt.TabIndex = 7;
            this.fromNameTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "From";
            // 
            // updateEquipmentTab
            // 
            this.updateEquipmentTab.Controls.Add(this.equipmentList);
            this.updateEquipmentTab.Location = new System.Drawing.Point(4, 22);
            this.updateEquipmentTab.Name = "updateEquipmentTab";
            this.updateEquipmentTab.Padding = new System.Windows.Forms.Padding(3);
            this.updateEquipmentTab.Size = new System.Drawing.Size(540, 200);
            this.updateEquipmentTab.TabIndex = 0;
            this.updateEquipmentTab.Text = "Update Equipment";
            this.updateEquipmentTab.UseVisualStyleBackColor = true;
            // 
            // selectedCategoriesTxt
            // 
            this.selectedCategoriesTxt.AcceptsReturn = false;
            this.selectedCategoriesTxt.AcceptsTab = false;
            this.selectedCategoriesTxt.BackColor = System.Drawing.Color.White;
            this.selectedCategoriesTxt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.selectedCategoriesTxt.ButtonImage = global::ApplyRoutesPlugin.Properties.Resources.DropDown8;
            this.selectedCategoriesTxt.Location = new System.Drawing.Point(123, 152);
            this.selectedCategoriesTxt.Multiline = false;
            this.selectedCategoriesTxt.Name = "selectedCategoriesTxt";
            this.selectedCategoriesTxt.ReadOnly = false;
            this.selectedCategoriesTxt.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.selectedCategoriesTxt.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.selectedCategoriesTxt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.selectedCategoriesTxt.Size = new System.Drawing.Size(411, 19);
            this.selectedCategoriesTxt.TabIndex = 10;
            this.selectedCategoriesTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 152);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(104, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "Change Category to:";
            // 
            // UpdateEquipmentForm
            // 
            this.AcceptButton = this.okBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(569, 280);
            this.Controls.Add(this.updateTab);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.Name = "UpdateEquipmentForm";
            this.Text = "Update History";
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