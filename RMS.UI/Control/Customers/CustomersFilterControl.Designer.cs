namespace RMS.UI.Control.Customers
{
    partial class CustomersFilterControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomersFilterControl));
            this.checkBtnIsUse = new DevExpress.XtraEditors.CheckButton();
            this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
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
            this.checkBtnIsUse.Location = new System.Drawing.Point(45, 0);
            this.checkBtnIsUse.Name = "checkBtnIsUse";
            this.checkBtnIsUse.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.checkBtnIsUse.Size = new System.Drawing.Size(255, 45);
            this.checkBtnIsUse.TabIndex = 1;
            this.checkBtnIsUse.Text = "Применить";
            this.checkBtnIsUse.Click += new System.EventHandler(this.checkBtnIsUse_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.AllowFocus = false;
            this.btnEdit.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnEdit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.ImageOptions.Image")));
            this.btnEdit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.btnEdit.Location = new System.Drawing.Point(0, 0);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnEdit.Size = new System.Drawing.Size(45, 45);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // panelControlCount
            // 
            this.panelControlCount.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControlCount.Controls.Add(this.lblCount);
            this.panelControlCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControlCount.Location = new System.Drawing.Point(245, 0);
            this.panelControlCount.Name = "panelControlCount";
            this.panelControlCount.Size = new System.Drawing.Size(55, 45);
            this.panelControlCount.TabIndex = 2;
            // 
            // lblCount
            // 
            this.lblCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCount.Location = new System.Drawing.Point(0, 0);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(55, 45);
            this.lblCount.TabIndex = 0;
            this.lblCount.Text = "0";
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CustomersFilterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.panelControlCount);
            this.Controls.Add(this.checkBtnIsUse);
            this.Controls.Add(this.btnEdit);
            this.MinimumSize = new System.Drawing.Size(250, 45);
            this.Name = "CustomersFilterControl";
            this.Size = new System.Drawing.Size(300, 45);
            this.Load += new System.EventHandler(this.CustomersFilterControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlCount)).EndInit();
            this.panelControlCount.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.CheckButton checkBtnIsUse;
        private DevExpress.XtraEditors.SimpleButton btnEdit;
        private DevExpress.XtraEditors.PanelControl panelControlCount;
        private System.Windows.Forms.Label lblCount;
    }
}
