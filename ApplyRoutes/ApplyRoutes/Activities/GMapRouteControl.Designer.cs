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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GMapRouteControl));
            this.fitToPageBtn = new ZoneFiveSoftware.Common.Visuals.Button();
            this.mapTypePopup = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.mapTypeLbl = new System.Windows.Forms.Label();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
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
            resources.ApplyResources(this.fitToPageBtn, "fitToPageBtn");
            this.fitToPageBtn.Name = "fitToPageBtn";
            this.fitToPageBtn.PushStyle = true;
            this.fitToPageBtn.RightImage = null;
            this.fitToPageBtn.TextAlign = System.Drawing.StringAlignment.Center;
            this.fitToPageBtn.TextLeftMargin = 2;
            this.fitToPageBtn.TextRightMargin = 2;
            // 
            // mapTypePopup
            // 
            this.mapTypePopup.AcceptsReturn = false;
            this.mapTypePopup.AcceptsTab = false;
            resources.ApplyResources(this.mapTypePopup, "mapTypePopup");
            this.mapTypePopup.BackColor = System.Drawing.Color.White;
            this.mapTypePopup.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.mapTypePopup.ButtonImage = global::ApplyRoutesPlugin.Properties.Resources.DropDown;
            this.mapTypePopup.MaxLength = 32767;
            this.mapTypePopup.Multiline = false;
            this.mapTypePopup.Name = "mapTypePopup";
            this.mapTypePopup.ReadOnly = false;
            this.mapTypePopup.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.mapTypePopup.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.mapTypePopup.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // mapTypeLbl
            // 
            resources.ApplyResources(this.mapTypeLbl, "mapTypeLbl");
            this.mapTypeLbl.Name = "mapTypeLbl";
            // 
            // webBrowser
            // 
            this.webBrowser.AllowWebBrowserDrop = false;
            resources.ApplyResources(this.webBrowser, "webBrowser");
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.ScrollBarsEnabled = false;
            // 
            // GMapRouteControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mapTypeLbl);
            this.Controls.Add(this.mapTypePopup);
            this.Controls.Add(this.fitToPageBtn);
            this.Controls.Add(this.webBrowser);
            this.Name = "GMapRouteControl";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser;
        private ZoneFiveSoftware.Common.Visuals.Button fitToPageBtn;
        private ZoneFiveSoftware.Common.Visuals.TextBox mapTypePopup;
        private System.Windows.Forms.Label mapTypeLbl;
    }
}
