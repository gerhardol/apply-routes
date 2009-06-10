namespace ApplyRoutesPlugin.UI
{
    partial class ApplyRouteForm
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
            this.routeList = new ZoneFiveSoftware.Common.Visuals.TreeList();
            this.okBtn = new ZoneFiveSoftware.Common.Visuals.Button();
            this.cancelBtn = new ZoneFiveSoftware.Common.Visuals.Button();
            this.numActLbl = new System.Windows.Forms.Label();
            this.numActTxt = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.minDistTxt = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.minDistLbl = new System.Windows.Forms.Label();
            this.avgDistTxt = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.avgDistLbl = new System.Windows.Forms.Label();
            this.maxDistTxt = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.maxDistLbl = new System.Windows.Forms.Label();
            this.routeLbl = new System.Windows.Forms.Label();
            this.ignoreGPSActChk = new System.Windows.Forms.CheckBox();
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
            this.equipmentList.Location = new System.Drawing.Point(133, 232);
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
            // routeList
            // 
            this.routeList.AutoSize = true;
            this.routeList.BackColor = System.Drawing.Color.Transparent;
            this.routeList.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.SmallRoundShadow;
            this.routeList.CheckBoxes = false;
            this.routeList.DefaultIndent = 15;
            this.routeList.DefaultRowHeight = -1;
            this.routeList.HeaderRowHeight = 21;
            this.routeList.Location = new System.Drawing.Point(133, 56);
            this.routeList.MultiSelect = false;
            this.routeList.Name = "routeList";
            this.routeList.NumHeaderRows = ZoneFiveSoftware.Common.Visuals.TreeList.HeaderRows.Auto;
            this.routeList.NumLockedColumns = 0;
            this.routeList.RowAlternatingColors = true;
            this.routeList.RowHotlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(49)))), ((int)(((byte)(106)))), ((int)(((byte)(197)))));
            this.routeList.RowHotlightColorText = System.Drawing.SystemColors.HighlightText;
            this.routeList.RowHotlightMouse = true;
            this.routeList.RowSelectedColor = System.Drawing.SystemColors.Highlight;
            this.routeList.RowSelectedColorText = System.Drawing.SystemColors.HighlightText;
            this.routeList.RowSeparatorLines = true;
            this.routeList.ShowLines = false;
            this.routeList.ShowPlusMinus = false;
            this.routeList.Size = new System.Drawing.Size(425, 170);
            this.routeList.TabIndex = 7;
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
            this.okBtn.Location = new System.Drawing.Point(361, 362);
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
            this.cancelBtn.Location = new System.Drawing.Point(468, 362);
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
            // numActLbl
            // 
            this.numActLbl.AutoSize = true;
            this.numActLbl.Location = new System.Drawing.Point(50, 12);
            this.numActLbl.Name = "numActLbl";
            this.numActLbl.Size = new System.Drawing.Size(77, 13);
            this.numActLbl.TabIndex = 11;
            this.numActLbl.Text = "Num Activities:";
            this.numActLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numActTxt
            // 
            this.numActTxt.AcceptsReturn = false;
            this.numActTxt.AcceptsTab = false;
            this.numActTxt.BackColor = System.Drawing.Color.White;
            this.numActTxt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.numActTxt.ButtonImage = null;
            this.numActTxt.Location = new System.Drawing.Point(133, 12);
            this.numActTxt.Multiline = false;
            this.numActTxt.Name = "numActTxt";
            this.numActTxt.ReadOnly = true;
            this.numActTxt.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.numActTxt.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.numActTxt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numActTxt.Size = new System.Drawing.Size(100, 19);
            this.numActTxt.TabIndex = 12;
            this.numActTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // minDistTxt
            // 
            this.minDistTxt.AcceptsReturn = false;
            this.minDistTxt.AcceptsTab = false;
            this.minDistTxt.BackColor = System.Drawing.Color.White;
            this.minDistTxt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.minDistTxt.ButtonImage = null;
            this.minDistTxt.Location = new System.Drawing.Point(133, 31);
            this.minDistTxt.Multiline = false;
            this.minDistTxt.Name = "minDistTxt";
            this.minDistTxt.ReadOnly = true;
            this.minDistTxt.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.minDistTxt.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.minDistTxt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.minDistTxt.Size = new System.Drawing.Size(100, 19);
            this.minDistTxt.TabIndex = 14;
            this.minDistTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // minDistLbl
            // 
            this.minDistLbl.AutoSize = true;
            this.minDistLbl.Location = new System.Drawing.Point(79, 34);
            this.minDistLbl.Name = "minDistLbl";
            this.minDistLbl.Size = new System.Drawing.Size(48, 13);
            this.minDistLbl.TabIndex = 13;
            this.minDistLbl.Text = "Min Dist:";
            this.minDistLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // avgDistTxt
            // 
            this.avgDistTxt.AcceptsReturn = false;
            this.avgDistTxt.AcceptsTab = false;
            this.avgDistTxt.BackColor = System.Drawing.Color.White;
            this.avgDistTxt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.avgDistTxt.ButtonImage = null;
            this.avgDistTxt.Location = new System.Drawing.Point(293, 31);
            this.avgDistTxt.Multiline = false;
            this.avgDistTxt.Name = "avgDistTxt";
            this.avgDistTxt.ReadOnly = true;
            this.avgDistTxt.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.avgDistTxt.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.avgDistTxt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.avgDistTxt.Size = new System.Drawing.Size(100, 19);
            this.avgDistTxt.TabIndex = 16;
            this.avgDistTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // avgDistLbl
            // 
            this.avgDistLbl.AutoSize = true;
            this.avgDistLbl.Location = new System.Drawing.Point(239, 34);
            this.avgDistLbl.Name = "avgDistLbl";
            this.avgDistLbl.Size = new System.Drawing.Size(50, 13);
            this.avgDistLbl.TabIndex = 15;
            this.avgDistLbl.Text = "Avg Dist:";
            this.avgDistLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // maxDistTxt
            // 
            this.maxDistTxt.AcceptsReturn = false;
            this.maxDistTxt.AcceptsTab = false;
            this.maxDistTxt.BackColor = System.Drawing.Color.White;
            this.maxDistTxt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.maxDistTxt.ButtonImage = null;
            this.maxDistTxt.Location = new System.Drawing.Point(458, 31);
            this.maxDistTxt.Multiline = false;
            this.maxDistTxt.Name = "maxDistTxt";
            this.maxDistTxt.ReadOnly = true;
            this.maxDistTxt.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.maxDistTxt.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.maxDistTxt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.maxDistTxt.Size = new System.Drawing.Size(100, 19);
            this.maxDistTxt.TabIndex = 18;
            this.maxDistTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // maxDistLbl
            // 
            this.maxDistLbl.AutoSize = true;
            this.maxDistLbl.Location = new System.Drawing.Point(404, 34);
            this.maxDistLbl.Name = "maxDistLbl";
            this.maxDistLbl.Size = new System.Drawing.Size(51, 13);
            this.maxDistLbl.TabIndex = 17;
            this.maxDistLbl.Text = "Max Dist:";
            this.maxDistLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // routeLbl
            // 
            this.routeLbl.AutoSize = true;
            this.routeLbl.Location = new System.Drawing.Point(46, 133);
            this.routeLbl.Name = "routeLbl";
            this.routeLbl.Size = new System.Drawing.Size(81, 13);
            this.routeLbl.TabIndex = 20;
            this.routeLbl.Text = "Route To Apply";
            this.routeLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ignoreGPSActChk
            // 
            this.ignoreGPSActChk.Location = new System.Drawing.Point(23, 178);
            this.ignoreGPSActChk.Name = "ignoreGPSActChk";
            this.ignoreGPSActChk.Size = new System.Drawing.Size(104, 48);
            this.ignoreGPSActChk.TabIndex = 21;
            this.ignoreGPSActChk.Text = "Ignore activities with GPS data";
            this.ignoreGPSActChk.UseVisualStyleBackColor = true;
            // 
            // equipmentLbl
            // 
            this.equipmentLbl.AutoSize = true;
            this.equipmentLbl.Location = new System.Drawing.Point(32, 283);
            this.equipmentLbl.Name = "equipmentLbl";
            this.equipmentLbl.Size = new System.Drawing.Size(95, 13);
            this.equipmentLbl.TabIndex = 22;
            this.equipmentLbl.Text = "Update Equipment";
            this.equipmentLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ApplyRouteForm
            // 
            this.AcceptButton = this.okBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(572, 400);
            this.Controls.Add(this.equipmentLbl);
            this.Controls.Add(this.ignoreGPSActChk);
            this.Controls.Add(this.routeLbl);
            this.Controls.Add(this.maxDistTxt);
            this.Controls.Add(this.maxDistLbl);
            this.Controls.Add(this.avgDistTxt);
            this.Controls.Add(this.avgDistLbl);
            this.Controls.Add(this.minDistTxt);
            this.Controls.Add(this.minDistLbl);
            this.Controls.Add(this.numActTxt);
            this.Controls.Add(this.numActLbl);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.routeList);
            this.Controls.Add(this.equipmentList);
            this.Name = "ApplyRouteForm";
            this.Text = "Apply Route";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ZoneFiveSoftware.Common.Visuals.TreeList equipmentList;
        private ZoneFiveSoftware.Common.Visuals.TreeList routeList;
        private ZoneFiveSoftware.Common.Visuals.Button okBtn;
        private ZoneFiveSoftware.Common.Visuals.Button cancelBtn;
        private System.Windows.Forms.Label numActLbl;
        private ZoneFiveSoftware.Common.Visuals.TextBox numActTxt;
        private ZoneFiveSoftware.Common.Visuals.TextBox minDistTxt;
        private System.Windows.Forms.Label minDistLbl;
        private ZoneFiveSoftware.Common.Visuals.TextBox avgDistTxt;
        private System.Windows.Forms.Label avgDistLbl;
        private ZoneFiveSoftware.Common.Visuals.TextBox maxDistTxt;
        private System.Windows.Forms.Label maxDistLbl;
        private System.Windows.Forms.Label routeLbl;
        private System.Windows.Forms.CheckBox ignoreGPSActChk;
        private System.Windows.Forms.Label equipmentLbl;
    }
}