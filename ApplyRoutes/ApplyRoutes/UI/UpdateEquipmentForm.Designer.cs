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
            this.equipmentLbl = new System.Windows.Forms.Label();
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
            this.equipmentList.Location = new System.Drawing.Point(135, 12);
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
            this.equipmentList.Size = new System.Drawing.Size(425, 115);
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
            this.okBtn.Location = new System.Drawing.Point(363, 142);
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
            this.cancelBtn.Location = new System.Drawing.Point(470, 142);
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
            // equipmentLbl
            // 
            this.equipmentLbl.AutoSize = true;
            this.equipmentLbl.Location = new System.Drawing.Point(34, 63);
            this.equipmentLbl.Name = "equipmentLbl";
            this.equipmentLbl.Size = new System.Drawing.Size(95, 13);
            this.equipmentLbl.TabIndex = 22;
            this.equipmentLbl.Text = "Update Equipment";
            this.equipmentLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UpdateEquipmentForm
            // 
            this.AcceptButton = this.okBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(572, 183);
            this.Controls.Add(this.equipmentLbl);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.equipmentList);
            this.Name = "UpdateEquipmentForm";
            this.Text = "Update Equipment";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ZoneFiveSoftware.Common.Visuals.TreeList equipmentList;
        private ZoneFiveSoftware.Common.Visuals.Button okBtn;
        private ZoneFiveSoftware.Common.Visuals.Button cancelBtn;
        private System.Windows.Forms.Label equipmentLbl;
    }
}