namespace ApplyRoutesPlugin.Activities
{
    partial class GMapRouteControl
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
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.fitToPageBtn = new ZoneFiveSoftware.Common.Visuals.Button();
            this.mapTypePopup = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.mapTypeLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // webBrowser
            // 
            this.webBrowser.AllowWebBrowserDrop = false;
            this.webBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser.Location = new System.Drawing.Point(0, 32);
            this.webBrowser.Margin = new System.Windows.Forms.Padding(0);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.ScrollBarsEnabled = false;
            this.webBrowser.Size = new System.Drawing.Size(503, 468);
            this.webBrowser.TabIndex = 0;
            // 
            // fitToPageBtn
            // 
            this.fitToPageBtn.BackColor = System.Drawing.Color.Transparent;
            this.fitToPageBtn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.fitToPageBtn.CenterImage = null;
            this.fitToPageBtn.DialogResult = System.Windows.Forms.DialogResult.None;
            this.fitToPageBtn.HyperlinkStyle = false;
            this.fitToPageBtn.ImageMargin = 2;
            this.fitToPageBtn.LeftImage = null;
            this.fitToPageBtn.Location = new System.Drawing.Point(3, 3);
            this.fitToPageBtn.Name = "fitToPageBtn";
            this.fitToPageBtn.PushStyle = true;
            this.fitToPageBtn.RightImage = null;
            this.fitToPageBtn.Size = new System.Drawing.Size(75, 23);
            this.fitToPageBtn.TabIndex = 1;
            this.fitToPageBtn.Text = "Fit to Page";
            this.fitToPageBtn.TextAlign = System.Drawing.StringAlignment.Center;
            this.fitToPageBtn.TextLeftMargin = 2;
            this.fitToPageBtn.TextRightMargin = 2;
            // 
            // mapTypePopup
            // 
            this.mapTypePopup.AcceptsReturn = false;
            this.mapTypePopup.AcceptsTab = false;
            this.mapTypePopup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mapTypePopup.BackColor = System.Drawing.Color.White;
            this.mapTypePopup.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.mapTypePopup.ButtonImage = global::ApplyRoutesPlugin.Properties.Resources.DropDown8;
            this.mapTypePopup.Location = new System.Drawing.Point(367, 7);
            this.mapTypePopup.Multiline = false;
            this.mapTypePopup.Name = "mapTypePopup";
            this.mapTypePopup.ReadOnly = false;
            this.mapTypePopup.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.mapTypePopup.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.mapTypePopup.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.mapTypePopup.Size = new System.Drawing.Size(133, 19);
            this.mapTypePopup.TabIndex = 2;
            this.mapTypePopup.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // mapTypeLbl
            // 
            this.mapTypeLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mapTypeLbl.AutoSize = true;
            this.mapTypeLbl.Location = new System.Drawing.Point(309, 10);
            this.mapTypeLbl.Name = "mapTypeLbl";
            this.mapTypeLbl.Size = new System.Drawing.Size(55, 13);
            this.mapTypeLbl.TabIndex = 3;
            this.mapTypeLbl.Text = "Map Type";
            // 
            // GMapRouteControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.mapTypeLbl);
            this.Controls.Add(this.mapTypePopup);
            this.Controls.Add(this.fitToPageBtn);
            this.Controls.Add(this.webBrowser);
            this.Name = "GMapRouteControl";
            this.Size = new System.Drawing.Size(503, 500);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser;
        private ZoneFiveSoftware.Common.Visuals.Button fitToPageBtn;
        private ZoneFiveSoftware.Common.Visuals.TextBox mapTypePopup;
        private System.Windows.Forms.Label mapTypeLbl;
    }
}
