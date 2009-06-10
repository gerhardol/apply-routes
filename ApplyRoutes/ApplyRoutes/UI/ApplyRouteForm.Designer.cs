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
            resources.ApplyResources(this.routeList, "routeList");
            this.routeList.BackColor = System.Drawing.Color.Transparent;
            this.routeList.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.SmallRoundShadow;
            this.routeList.CheckBoxes = false;
            this.routeList.DefaultIndent = 15;
            this.routeList.DefaultRowHeight = -1;
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
            // numActLbl
            // 
            resources.ApplyResources(this.numActLbl, "numActLbl");
            this.numActLbl.Name = "numActLbl";
            // 
            // numActTxt
            // 
            this.numActTxt.AcceptsReturn = false;
            this.numActTxt.AcceptsTab = false;
            this.numActTxt.BackColor = System.Drawing.Color.White;
            this.numActTxt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.numActTxt.ButtonImage = null;
            resources.ApplyResources(this.numActTxt, "numActTxt");
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
            this.minDistTxt.BackColor = System.Drawing.Color.White;
            this.minDistTxt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.minDistTxt.ButtonImage = null;
            resources.ApplyResources(this.minDistTxt, "minDistTxt");
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
            resources.ApplyResources(this.minDistLbl, "minDistLbl");
            this.minDistLbl.Name = "minDistLbl";
            // 
            // avgDistTxt
            // 
            this.avgDistTxt.AcceptsReturn = false;
            this.avgDistTxt.AcceptsTab = false;
            this.avgDistTxt.BackColor = System.Drawing.Color.White;
            this.avgDistTxt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.avgDistTxt.ButtonImage = null;
            resources.ApplyResources(this.avgDistTxt, "avgDistTxt");
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
            resources.ApplyResources(this.avgDistLbl, "avgDistLbl");
            this.avgDistLbl.Name = "avgDistLbl";
            // 
            // maxDistTxt
            // 
            this.maxDistTxt.AcceptsReturn = false;
            this.maxDistTxt.AcceptsTab = false;
            this.maxDistTxt.BackColor = System.Drawing.Color.White;
            this.maxDistTxt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.maxDistTxt.ButtonImage = null;
            resources.ApplyResources(this.maxDistTxt, "maxDistTxt");
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
            resources.ApplyResources(this.maxDistLbl, "maxDistLbl");
            this.maxDistLbl.Name = "maxDistLbl";
            // 
            // routeLbl
            // 
            resources.ApplyResources(this.routeLbl, "routeLbl");
            this.routeLbl.Name = "routeLbl";
            // 
            // ignoreGPSActChk
            // 
            resources.ApplyResources(this.ignoreGPSActChk, "ignoreGPSActChk");
            this.ignoreGPSActChk.Name = "ignoreGPSActChk";
            this.ignoreGPSActChk.UseVisualStyleBackColor = true;
            // 
            // applyTimesLinearlyChk
            // 
            resources.ApplyResources(this.applyTimesLinearlyChk, "applyTimesLinearlyChk");
            this.applyTimesLinearlyChk.Name = "applyTimesLinearlyChk";
            this.applyTimesLinearlyChk.UseVisualStyleBackColor = true;
            // 
            // preserveDistChk
            // 
            resources.ApplyResources(this.preserveDistChk, "preserveDistChk");
            this.preserveDistChk.Name = "preserveDistChk";
            this.preserveDistChk.UseVisualStyleBackColor = true;
            // 
            // ApplyRouteForm
            // 
            this.AcceptButton = this.okBtn;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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