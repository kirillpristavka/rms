namespace RMS.UI.Forms.ReferenceBooks
{
    partial class CalculationScaleEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CalculationScaleEdit));
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new DevExpress.XtraEditors.TextEdit();
            this.panelControlHeader = new DevExpress.XtraEditors.PanelControl();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControlHeaderGrid = new DevExpress.XtraEditors.PanelControl();
            this.btnDel = new DevExpress.XtraEditors.SimpleButton();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.panelControlFooter = new DevExpress.XtraEditors.PanelControl();
            this.xtraTab = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage = new DevExpress.XtraTab.XtraTabPage();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlHeader)).BeginInit();
            this.panelControlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlHeaderGrid)).BeginInit();
            this.panelControlHeaderGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlFooter)).BeginInit();
            this.panelControlFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTab)).BeginInit();
            this.xtraTab.SuspendLayout();
            this.xtraTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(225, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 25);
            this.btnSave.TabIndex = 26;
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(331, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 25);
            this.btnCancel.TabIndex = 25;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 15);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(112, 16);
            this.lblName.TabIndex = 28;
            this.lblName.Text = "Наименование:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(140, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(632, 22);
            this.txtName.TabIndex = 27;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(44, 43);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(80, 16);
            this.lblDescription.TabIndex = 30;
            this.lblDescription.Text = "Описание:";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(140, 40);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(632, 22);
            this.txtDescription.TabIndex = 29;
            // 
            // panelControlHeader
            // 
            this.panelControlHeader.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControlHeader.Controls.Add(this.txtDescription);
            this.panelControlHeader.Controls.Add(this.txtName);
            this.panelControlHeader.Controls.Add(this.lblName);
            this.panelControlHeader.Controls.Add(this.lblDescription);
            this.panelControlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControlHeader.Location = new System.Drawing.Point(0, 0);
            this.panelControlHeader.Name = "panelControlHeader";
            this.panelControlHeader.Size = new System.Drawing.Size(434, 70);
            this.panelControlHeader.TabIndex = 31;
            // 
            // gridControl
            // 
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.Location = new System.Drawing.Point(0, 37);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(432, 190);
            this.gridControl.TabIndex = 37;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            this.gridView.OptionsView.ShowGroupPanel = false;
            // 
            // panelControlHeaderGrid
            // 
            this.panelControlHeaderGrid.AutoSize = true;
            this.panelControlHeaderGrid.Controls.Add(this.btnDel);
            this.panelControlHeaderGrid.Controls.Add(this.btnAdd);
            this.panelControlHeaderGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControlHeaderGrid.Location = new System.Drawing.Point(0, 0);
            this.panelControlHeaderGrid.Name = "panelControlHeaderGrid";
            this.panelControlHeaderGrid.Size = new System.Drawing.Size(432, 37);
            this.panelControlHeaderGrid.TabIndex = 38;
            // 
            // btnDel
            // 
            this.btnDel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnTaxSystemReportsDel.ImageOptions.Image")));
            this.btnDel.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnDel.Location = new System.Drawing.Point(37, 5);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(25, 25);
            this.btnDel.TabIndex = 2;
            this.btnDel.Text = "+";
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnTaxSystemReportsAdd.ImageOptions.Image")));
            this.btnAdd.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnAdd.Location = new System.Drawing.Point(6, 5);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(25, 25);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "+";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // panelControlFooter
            // 
            this.panelControlFooter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControlFooter.Controls.Add(this.btnCancel);
            this.panelControlFooter.Controls.Add(this.btnSave);
            this.panelControlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControlFooter.Location = new System.Drawing.Point(0, 325);
            this.panelControlFooter.Name = "panelControlFooter";
            this.panelControlFooter.Size = new System.Drawing.Size(434, 35);
            this.panelControlFooter.TabIndex = 32;
            // 
            // xtraTab
            // 
            this.xtraTab.AppearancePage.HeaderActive.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.xtraTab.AppearancePage.HeaderActive.Options.UseFont = true;
            this.xtraTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTab.Location = new System.Drawing.Point(0, 70);
            this.xtraTab.Name = "xtraTab";
            this.xtraTab.PageImagePosition = DevExpress.XtraTab.TabPageImagePosition.None;
            this.xtraTab.SelectedTabPage = this.xtraTabPage;
            this.xtraTab.Size = new System.Drawing.Size(434, 255);
            this.xtraTab.TabIndex = 91;
            this.xtraTab.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage});
            // 
            // xtraTabPage
            // 
            this.xtraTabPage.Controls.Add(this.gridControl);
            this.xtraTabPage.Controls.Add(this.panelControlHeaderGrid);
            this.xtraTabPage.Name = "xtraTabPage";
            this.xtraTabPage.Size = new System.Drawing.Size(432, 227);
            this.xtraTabPage.Text = "Список шкал";
            // 
            // CalculationScaleEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 360);
            this.Controls.Add(this.xtraTab);
            this.Controls.Add(this.panelControlFooter);
            this.Controls.Add(this.panelControlHeader);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CalculationScaleEdit";
            this.Text = "Шкалы расчета";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CalculationScaleEdit_FormClosed);
            this.Load += new System.EventHandler(this.TaxSystemEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlHeader)).EndInit();
            this.panelControlHeader.ResumeLayout(false);
            this.panelControlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlHeaderGrid)).EndInit();
            this.panelControlHeaderGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlFooter)).EndInit();
            this.panelControlFooter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTab)).EndInit();
            this.xtraTab.ResumeLayout(false);
            this.xtraTabPage.ResumeLayout(false);
            this.xtraTabPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private System.Windows.Forms.Label lblName;
        private DevExpress.XtraEditors.TextEdit txtName;
        private System.Windows.Forms.Label lblDescription;
        private DevExpress.XtraEditors.TextEdit txtDescription;
        private DevExpress.XtraEditors.PanelControl panelControlHeader;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraEditors.PanelControl panelControlHeaderGrid;
        private DevExpress.XtraEditors.SimpleButton btnDel;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.PanelControl panelControlFooter;
        private DevExpress.XtraTab.XtraTabControl xtraTab;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage;
    }
}