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
            this.homePageLink = new System.Windows.Forms.LinkLabel();
            this.showCreateRoutesChk = new System.Windows.Forms.CheckBox();
            this.showApplyRoutesChk = new System.Windows.Forms.CheckBox();
            this.showUpdateEquipmentChk = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // homePageLink
            // 
            this.homePageLink.AutoSize = true;
            this.homePageLink.Location = new System.Drawing.Point(35, 106);
            this.homePageLink.Name = "homePageLink";
            this.homePageLink.Size = new System.Drawing.Size(129, 13);
            this.homePageLink.TabIndex = 0;
            this.homePageLink.TabStop = true;
            this.homePageLink.Text = "Apply Routes Home Page";
            // 
            // showCreateRoutesChk
            // 
            this.showCreateRoutesChk.AutoSize = true;
            this.showCreateRoutesChk.Location = new System.Drawing.Point(38, 24);
            this.showCreateRoutesChk.Name = "showCreateRoutesChk";
            this.showCreateRoutesChk.Size = new System.Drawing.Size(273, 17);
            this.showCreateRoutesChk.TabIndex = 1;
            this.showCreateRoutesChk.Text = "Show \'Create Routes From Activities...\' on Edit menu";
            this.showCreateRoutesChk.UseVisualStyleBackColor = true;
            // 
            // showApplyRoutesChk
            // 
            this.showApplyRoutesChk.AutoSize = true;
            this.showApplyRoutesChk.Location = new System.Drawing.Point(38, 48);
            this.showApplyRoutesChk.Name = "showApplyRoutesChk";
            this.showApplyRoutesChk.Size = new System.Drawing.Size(195, 17);
            this.showApplyRoutesChk.TabIndex = 2;
            this.showApplyRoutesChk.Text = "Show \'Apply Route...\" on Edit menu";
            this.showApplyRoutesChk.UseVisualStyleBackColor = true;
            // 
            // showUpdateEquipmentChk
            // 
            this.showUpdateEquipmentChk.AutoSize = true;
            this.showUpdateEquipmentChk.Location = new System.Drawing.Point(38, 72);
            this.showUpdateEquipmentChk.Name = "showUpdateEquipmentChk";
            this.showUpdateEquipmentChk.Size = new System.Drawing.Size(222, 17);
            this.showUpdateEquipmentChk.TabIndex = 3;
            this.showUpdateEquipmentChk.Text = "Show \'Update Equipment...\' on Edit menu";
            this.showUpdateEquipmentChk.UseVisualStyleBackColor = true;
            // 
            // SettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.showUpdateEquipmentChk);
            this.Controls.Add(this.showApplyRoutesChk);
            this.Controls.Add(this.showCreateRoutesChk);
            this.Controls.Add(this.homePageLink);
            this.Name = "SettingsControl";
            this.Size = new System.Drawing.Size(353, 162);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel homePageLink;
        private System.Windows.Forms.CheckBox showCreateRoutesChk;
        private System.Windows.Forms.CheckBox showApplyRoutesChk;
        private System.Windows.Forms.CheckBox showUpdateEquipmentChk;

    }
}
