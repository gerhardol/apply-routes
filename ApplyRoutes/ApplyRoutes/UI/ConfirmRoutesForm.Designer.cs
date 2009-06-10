namespace ApplyRoutesPlugin.UI
{
    partial class ConfirmRoutesForm
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
            System.Windows.Forms.Label label1;
            this.okBtn = new ZoneFiveSoftware.Common.Visuals.Button();
            this.cancelBtn = new ZoneFiveSoftware.Common.Visuals.Button();
            this.activityRouteTree = new ZoneFiveSoftware.Common.Visuals.TreeList();
            this.activityRoutePop = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.activityText = new ZoneFiveSoftware.Common.Visuals.TextBox();
            label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
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
            this.okBtn.Location = new System.Drawing.Point(275, 337);
            this.okBtn.Name = "okBtn";
            this.okBtn.PushStyle = true;
            this.okBtn.RightImage = null;
            this.okBtn.Size = new System.Drawing.Size(75, 23);
            this.okBtn.TabIndex = 0;
            this.okBtn.Text = "OK";
            this.okBtn.TextAlign = System.Drawing.StringAlignment.Center;
            this.okBtn.TextLeftMargin = 2;
            this.okBtn.TextRightMargin = 2;
            // 
            // cancelBtn
            // 
            this.cancelBtn.BackColor = System.Drawing.Color.Transparent;
            this.cancelBtn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.cancelBtn.CenterImage = null;
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.HyperlinkStyle = false;
            this.cancelBtn.ImageMargin = 2;
            this.cancelBtn.LeftImage = null;
            this.cancelBtn.Location = new System.Drawing.Point(386, 337);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.PushStyle = true;
            this.cancelBtn.RightImage = null;
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 1;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.TextAlign = System.Drawing.StringAlignment.Center;
            this.cancelBtn.TextLeftMargin = 2;
            this.cancelBtn.TextRightMargin = 2;
            // 
            // activityRouteTree
            // 
            this.activityRouteTree.BackColor = System.Drawing.Color.Transparent;
            this.activityRouteTree.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.SmallRoundShadow;
            this.activityRouteTree.CheckBoxes = false;
            this.activityRouteTree.DefaultIndent = 15;
            this.activityRouteTree.DefaultRowHeight = -1;
            this.activityRouteTree.HeaderRowHeight = 21;
            this.activityRouteTree.Location = new System.Drawing.Point(14, 63);
            this.activityRouteTree.MultiSelect = false;
            this.activityRouteTree.Name = "activityRouteTree";
            this.activityRouteTree.NumHeaderRows = ZoneFiveSoftware.Common.Visuals.TreeList.HeaderRows.Auto;
            this.activityRouteTree.NumLockedColumns = 0;
            this.activityRouteTree.RowAlternatingColors = true;
            this.activityRouteTree.RowHotlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(49)))), ((int)(((byte)(106)))), ((int)(((byte)(197)))));
            this.activityRouteTree.RowHotlightColorText = System.Drawing.SystemColors.HighlightText;
            this.activityRouteTree.RowHotlightMouse = true;
            this.activityRouteTree.RowSelectedColor = System.Drawing.SystemColors.Highlight;
            this.activityRouteTree.RowSelectedColorText = System.Drawing.SystemColors.HighlightText;
            this.activityRouteTree.RowSeparatorLines = true;
            this.activityRouteTree.ShowLines = false;
            this.activityRouteTree.ShowPlusMinus = false;
            this.activityRouteTree.Size = new System.Drawing.Size(447, 220);
            this.activityRouteTree.TabIndex = 2;
            // 
            // activityRoutePop
            // 
            this.activityRoutePop.AcceptsReturn = false;
            this.activityRoutePop.AcceptsTab = false;
            this.activityRoutePop.BackColor = System.Drawing.Color.White;
            this.activityRoutePop.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.activityRoutePop.ButtonImage = global::ApplyRoutesPlugin.Properties.Resources.DropDown8;
            this.activityRoutePop.Location = new System.Drawing.Point(230, 302);
            this.activityRoutePop.Multiline = false;
            this.activityRoutePop.Name = "activityRoutePop";
            this.activityRoutePop.ReadOnly = true;
            this.activityRoutePop.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.activityRoutePop.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.activityRoutePop.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.activityRoutePop.Size = new System.Drawing.Size(231, 19);
            this.activityRoutePop.TabIndex = 3;
            this.activityRoutePop.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // activityText
            // 
            this.activityText.AcceptsReturn = false;
            this.activityText.AcceptsTab = false;
            this.activityText.BackColor = System.Drawing.Color.White;
            this.activityText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.activityText.ButtonImage = null;
            this.activityText.Location = new System.Drawing.Point(15, 302);
            this.activityText.Multiline = false;
            this.activityText.Name = "activityText";
            this.activityText.ReadOnly = true;
            this.activityText.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.activityText.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.activityText.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.activityText.Size = new System.Drawing.Size(209, 19);
            this.activityText.TabIndex = 4;
            this.activityText.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label1
            // 
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label1.Location = new System.Drawing.Point(15, 13);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(451, 36);
            label1.TabIndex = 5;
            label1.Text = "Plugins Cannot currently create new routes. To create a new route, go to the Rout" +
                "es view and click Add Route. Then update the route using this plugin.";
            // 
            // ConfirmRoutesForm
            // 
            this.AcceptButton = this.okBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(478, 376);
            this.Controls.Add(label1);
            this.Controls.Add(this.activityText);
            this.Controls.Add(this.activityRoutePop);
            this.Controls.Add(this.activityRouteTree);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.Name = "ConfirmRoutesForm";
            this.Text = "Confirm Routes";
            this.ResumeLayout(false);

        }

        #endregion

        private ZoneFiveSoftware.Common.Visuals.Button okBtn;
        private ZoneFiveSoftware.Common.Visuals.Button cancelBtn;
        private ZoneFiveSoftware.Common.Visuals.TreeList activityRouteTree;
        private ZoneFiveSoftware.Common.Visuals.TextBox activityRoutePop;
        private ZoneFiveSoftware.Common.Visuals.TextBox activityText;
    }
}