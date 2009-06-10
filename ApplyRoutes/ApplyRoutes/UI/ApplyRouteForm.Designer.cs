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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplyRouteForm));
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
            this.applyTimesLinearlyChk = new System.Windows.Forms.CheckBox();
            this.preserveDistChk = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // routeList
            // 
            this.routeList.AccessibleDescription = null;
            this.routeList.AccessibleName = null;
            resources.ApplyResources(this.routeList, "routeList");
            this.routeList.BackColor = System.Drawing.Color.Transparent;
            this.routeList.BackgroundImage = null;
            this.routeList.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.SmallRoundShadow;
            this.routeList.CheckBoxes = false;
            this.routeList.DefaultIndent = 15;
            this.routeList.DefaultRowHeight = -1;
            this.routeList.Font = null;
            this.routeList.HeaderRowHeight = 21;
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
            // 
            // okBtn
            // 
            this.okBtn.AccessibleDescription = null;
            this.okBtn.AccessibleName = null;
            resources.ApplyResources(this.okBtn, "okBtn");
            this.okBtn.BackColor = System.Drawing.Color.Transparent;
            this.okBtn.BackgroundImage = null;
            this.okBtn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.okBtn.CenterImage = null;
            this.okBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okBtn.Font = null;
            this.okBtn.HyperlinkStyle = false;
            this.okBtn.ImageMargin = 2;
            this.okBtn.LeftImage = null;
            this.okBtn.Name = "okBtn";
            this.okBtn.PushStyle = true;
            this.okBtn.RightImage = null;
            this.okBtn.TextAlign = System.Drawing.StringAlignment.Center;
            this.okBtn.TextLeftMargin = 2;
            this.okBtn.TextRightMargin = 2;
            // 
            // cancelBtn
            // 
            this.cancelBtn.AccessibleDescription = null;
            this.cancelBtn.AccessibleName = null;
            resources.ApplyResources(this.cancelBtn, "cancelBtn");
            this.cancelBtn.BackColor = System.Drawing.Color.Transparent;
            this.cancelBtn.BackgroundImage = null;
            this.cancelBtn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.cancelBtn.CausesValidation = false;
            this.cancelBtn.CenterImage = null;
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Font = null;
            this.cancelBtn.HyperlinkStyle = false;
            this.cancelBtn.ImageMargin = 2;
            this.cancelBtn.LeftImage = null;
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.PushStyle = true;
            this.cancelBtn.RightImage = null;
            this.cancelBtn.TextAlign = System.Drawing.StringAlignment.Center;
            this.cancelBtn.TextLeftMargin = 2;
            this.cancelBtn.TextRightMargin = 2;
            // 
            // numActLbl
            // 
            this.numActLbl.AccessibleDescription = null;
            this.numActLbl.AccessibleName = null;
            resources.ApplyResources(this.numActLbl, "numActLbl");
            this.numActLbl.Font = null;
            this.numActLbl.Name = "numActLbl";
            // 
            // numActTxt
            // 
            this.numActTxt.AcceptsReturn = false;
            this.numActTxt.AcceptsTab = false;
            this.numActTxt.AccessibleDescription = null;
            this.numActTxt.AccessibleName = null;
            resources.ApplyResources(this.numActTxt, "numActTxt");
            this.numActTxt.BackColor = System.Drawing.Color.White;
            this.numActTxt.BackgroundImage = null;
            this.numActTxt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.numActTxt.ButtonImage = null;
            this.numActTxt.Font = null;
            this.numActTxt.MaxLength = 32767;
            this.numActTxt.Multiline = false;
            this.numActTxt.Name = "numActTxt";
            this.numActTxt.ReadOnly = true;
            this.numActTxt.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.numActTxt.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.numActTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // minDistTxt
            // 
            this.minDistTxt.AcceptsReturn = false;
            this.minDistTxt.AcceptsTab = false;
            this.minDistTxt.AccessibleDescription = null;
            this.minDistTxt.AccessibleName = null;
            resources.ApplyResources(this.minDistTxt, "minDistTxt");
            this.minDistTxt.BackColor = System.Drawing.Color.White;
            this.minDistTxt.BackgroundImage = null;
            this.minDistTxt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.minDistTxt.ButtonImage = null;
            this.minDistTxt.Font = null;
            this.minDistTxt.MaxLength = 32767;
            this.minDistTxt.Multiline = false;
            this.minDistTxt.Name = "minDistTxt";
            this.minDistTxt.ReadOnly = true;
            this.minDistTxt.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.minDistTxt.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.minDistTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // minDistLbl
            // 
            this.minDistLbl.AccessibleDescription = null;
            this.minDistLbl.AccessibleName = null;
            resources.ApplyResources(this.minDistLbl, "minDistLbl");
            this.minDistLbl.Font = null;
            this.minDistLbl.Name = "minDistLbl";
            // 
            // avgDistTxt
            // 
            this.avgDistTxt.AcceptsReturn = false;
            this.avgDistTxt.AcceptsTab = false;
            this.avgDistTxt.AccessibleDescription = null;
            this.avgDistTxt.AccessibleName = null;
            resources.ApplyResources(this.avgDistTxt, "avgDistTxt");
            this.avgDistTxt.BackColor = System.Drawing.Color.White;
            this.avgDistTxt.BackgroundImage = null;
            this.avgDistTxt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.avgDistTxt.ButtonImage = null;
            this.avgDistTxt.Font = null;
            this.avgDistTxt.MaxLength = 32767;
            this.avgDistTxt.Multiline = false;
            this.avgDistTxt.Name = "avgDistTxt";
            this.avgDistTxt.ReadOnly = true;
            this.avgDistTxt.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.avgDistTxt.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.avgDistTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // avgDistLbl
            // 
            this.avgDistLbl.AccessibleDescription = null;
            this.avgDistLbl.AccessibleName = null;
            resources.ApplyResources(this.avgDistLbl, "avgDistLbl");
            this.avgDistLbl.Font = null;
            this.avgDistLbl.Name = "avgDistLbl";
            // 
            // maxDistTxt
            // 
            this.maxDistTxt.AcceptsReturn = false;
            this.maxDistTxt.AcceptsTab = false;
            this.maxDistTxt.AccessibleDescription = null;
            this.maxDistTxt.AccessibleName = null;
            resources.ApplyResources(this.maxDistTxt, "maxDistTxt");
            this.maxDistTxt.BackColor = System.Drawing.Color.White;
            this.maxDistTxt.BackgroundImage = null;
            this.maxDistTxt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.maxDistTxt.ButtonImage = null;
            this.maxDistTxt.Font = null;
            this.maxDistTxt.MaxLength = 32767;
            this.maxDistTxt.Multiline = false;
            this.maxDistTxt.Name = "maxDistTxt";
            this.maxDistTxt.ReadOnly = true;
            this.maxDistTxt.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.maxDistTxt.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.maxDistTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // maxDistLbl
            // 
            this.maxDistLbl.AccessibleDescription = null;
            this.maxDistLbl.AccessibleName = null;
            resources.ApplyResources(this.maxDistLbl, "maxDistLbl");
            this.maxDistLbl.Font = null;
            this.maxDistLbl.Name = "maxDistLbl";
            // 
            // routeLbl
            // 
            this.routeLbl.AccessibleDescription = null;
            this.routeLbl.AccessibleName = null;
            resources.ApplyResources(this.routeLbl, "routeLbl");
            this.routeLbl.Font = null;
            this.routeLbl.Name = "routeLbl";
            // 
            // ignoreGPSActChk
            // 
            this.ignoreGPSActChk.AccessibleDescription = null;
            this.ignoreGPSActChk.AccessibleName = null;
            resources.ApplyResources(this.ignoreGPSActChk, "ignoreGPSActChk");
            this.ignoreGPSActChk.BackgroundImage = null;
            this.ignoreGPSActChk.Font = null;
            this.ignoreGPSActChk.Name = "ignoreGPSActChk";
            this.ignoreGPSActChk.UseVisualStyleBackColor = true;
            // 
            // applyTimesLinearlyChk
            // 
            this.applyTimesLinearlyChk.AccessibleDescription = null;
            this.applyTimesLinearlyChk.AccessibleName = null;
            resources.ApplyResources(this.applyTimesLinearlyChk, "applyTimesLinearlyChk");
            this.applyTimesLinearlyChk.BackgroundImage = null;
            this.applyTimesLinearlyChk.Font = null;
            this.applyTimesLinearlyChk.Name = "applyTimesLinearlyChk";
            this.applyTimesLinearlyChk.UseVisualStyleBackColor = true;
            // 
            // preserveDistChk
            // 
            this.preserveDistChk.AccessibleDescription = null;
            this.preserveDistChk.AccessibleName = null;
            resources.ApplyResources(this.preserveDistChk, "preserveDistChk");
            this.preserveDistChk.BackgroundImage = null;
            this.preserveDistChk.Font = null;
            this.preserveDistChk.Name = "preserveDistChk";
            this.preserveDistChk.UseVisualStyleBackColor = true;
            // 
            // ApplyRouteForm
            // 
            this.AcceptButton = this.okBtn;
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.CancelButton = this.cancelBtn;
            this.Controls.Add(this.preserveDistChk);
            this.Controls.Add(this.applyTimesLinearlyChk);
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
            this.Font = null;
            this.Icon = null;
            this.Name = "ApplyRouteForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

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
        private System.Windows.Forms.CheckBox applyTimesLinearlyChk;
        private System.Windows.Forms.CheckBox preserveDistChk;
    }
}