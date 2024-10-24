namespace RMS.UI.Control.Mail
{
    partial class CustomerLetterControl
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
            this.checkBtnIsUse = new DevExpress.XtraEditors.CheckButton();
            this.panelControlCount = new DevExpress.XtraEditors.PanelControl();
            this.lblCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlCount)).BeginInit();
            this.panelControlCount.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBtnIsUse
            // 
            this.checkBtnIsUse.AllowFocus = false;
            this.checkBtnIsUse.Appearance.Options.UseTextOptions = true;
            this.checkBtnIsUse.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.checkBtnIsUse.AppearanceDisabled.Options.UseTextOptions = true;
            this.checkBtnIsUse.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.checkBtnIsUse.AppearanceHovered.Options.UseTextOptions = true;
            this.checkBtnIsUse.AppearanceHovered.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.checkBtnIsUse.AppearancePressed.Options.UseTextOptions = true;
            this.checkBtnIsUse.AppearancePressed.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.checkBtnIsUse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBtnIsUse.Location = new System.Drawing.Point(0, 0);
            this.checkBtnIsUse.Name = "checkBtnIsUse";
            this.checkBtnIsUse.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.checkBtnIsUse.Size = new System.Drawing.Size(295, 25);
            this.checkBtnIsUse.TabIndex = 0;
            this.checkBtnIsUse.Text = "CustomerName";
            this.checkBtnIsUse.Click += new System.EventHandler(this.checkBtnIsUse_Click);
            // 
            // panelControlCount
            // 
            this.panelControlCount.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControlCount.Controls.Add(this.lblCount);
            this.panelControlCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControlCount.Location = new System.Drawing.Point(295, 0);
            this.panelControlCount.Name = "panelControlCount";
            this.panelControlCount.Size = new System.Drawing.Size(55, 25);
            this.panelControlCount.TabIndex = 1;
            // 
            // lblCount
            // 
            this.lblCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCount.Location = new System.Drawing.Point(0, 0);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(55, 25);
            this.lblCount.TabIndex = 0;
            this.lblCount.Text = "0";
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CustomerLetterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBtnIsUse);
            this.Controls.Add(this.panelControlCount);
            this.MaximumSize = new System.Drawing.Size(0, 25);
            this.MinimumSize = new System.Drawing.Size(350, 25);
            this.Name = "CustomerLetterControl";
            this.Size = new System.Drawing.Size(350, 25);
            this.Load += new System.EventHandler(this.CustomerLetterControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlCount)).EndInit();
            this.panelControlCount.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.CheckButton checkBtnIsUse;
        private DevExpress.XtraEditors.PanelControl panelControlCount;
        private System.Windows.Forms.Label lblCount;
    }
}
