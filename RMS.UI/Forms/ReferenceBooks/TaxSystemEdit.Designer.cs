namespace RMS.UI.Forms.ReferenceBooks
{
    partial class TaxSystemEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaxSystemEdit));
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new DevExpress.XtraEditors.TextEdit();
            this.panelControlTaxSystemHeader = new DevExpress.XtraEditors.PanelControl();
            this.gridControlTaxSystemReports = new DevExpress.XtraGrid.GridControl();
            this.gridViewTaxSystemReports = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControlHeader = new DevExpress.XtraEditors.PanelControl();
            this.btnTaxSystemReportsDel = new DevExpress.XtraEditors.SimpleButton();
            this.btnTaxSystemReportsAdd = new DevExpress.XtraEditors.SimpleButton();
            this.panelControlFooter = new DevExpress.XtraEditors.PanelControl();
            this.xtraTabTaxSystem = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPageReport = new DevExpress.XtraTab.XtraTabPage();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblCalculationScale = new System.Windows.Forms.Label();
            this.btnCalculationScale = new DevExpress.XtraEditors.ButtonEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlTaxSystemHeader)).BeginInit();
            this.panelControlTaxSystemHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlTaxSystemReports)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTaxSystemReports)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlHeader)).BeginInit();
            this.panelControlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlFooter)).BeginInit();
            this.panelControlFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabTaxSystem)).BeginInit();
            this.xtraTabTaxSystem.SuspendLayout();
            this.xtraTabPageReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCalculationScale.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(575, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 25);
            this.btnSave.TabIndex = 26;
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(681, 5);
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
            // panelControlTaxSystemHeader
            // 
            this.panelControlTaxSystemHeader.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControlTaxSystemHeader.Controls.Add(this.txtDescription);
            this.panelControlTaxSystemHeader.Controls.Add(this.txtName);
            this.panelControlTaxSystemHeader.Controls.Add(this.lblName);
            this.panelControlTaxSystemHeader.Controls.Add(this.lblDescription);
            this.panelControlTaxSystemHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControlTaxSystemHeader.Location = new System.Drawing.Point(0, 0);
            this.panelControlTaxSystemHeader.Name = "panelControlTaxSystemHeader";
            this.panelControlTaxSystemHeader.Size = new System.Drawing.Size(784, 70);
            this.panelControlTaxSystemHeader.TabIndex = 31;
            // 
            // gridControlTaxSystemReports
            // 
            this.gridControlTaxSystemReports.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlTaxSystemReports.Location = new System.Drawing.Point(0, 37);
            this.gridControlTaxSystemReports.MainView = this.gridViewTaxSystemReports;
            this.gridControlTaxSystemReports.Name = "gridControlTaxSystemReports";
            this.gridControlTaxSystemReports.Size = new System.Drawing.Size(782, 346);
            this.gridControlTaxSystemReports.TabIndex = 37;
            this.gridControlTaxSystemReports.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewTaxSystemReports});
            // 
            // gridViewTaxSystemReports
            // 
            this.gridViewTaxSystemReports.GridControl = this.gridControlTaxSystemReports;
            this.gridViewTaxSystemReports.Name = "gridViewTaxSystemReports";
            this.gridViewTaxSystemReports.OptionsBehavior.Editable = false;
            this.gridViewTaxSystemReports.OptionsView.ShowGroupPanel = false;
            this.gridViewTaxSystemReports.DoubleClick += new System.EventHandler(this.gridViewTaxSystemReports_DoubleClick);
            // 
            // panelControlHeader
            // 
            this.panelControlHeader.AutoSize = true;
            this.panelControlHeader.Controls.Add(this.btnTaxSystemReportsDel);
            this.panelControlHeader.Controls.Add(this.btnTaxSystemReportsAdd);
            this.panelControlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControlHeader.Location = new System.Drawing.Point(0, 0);
            this.panelControlHeader.Name = "panelControlHeader";
            this.panelControlHeader.Size = new System.Drawing.Size(782, 37);
            this.panelControlHeader.TabIndex = 38;
            // 
            // btnTaxSystemReportsDel
            // 
            this.btnTaxSystemReportsDel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnTaxSystemReportsDel.ImageOptions.Image")));
            this.btnTaxSystemReportsDel.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnTaxSystemReportsDel.Location = new System.Drawing.Point(37, 5);
            this.btnTaxSystemReportsDel.Name = "btnTaxSystemReportsDel";
            this.btnTaxSystemReportsDel.Size = new System.Drawing.Size(25, 25);
            this.btnTaxSystemReportsDel.TabIndex = 2;
            this.btnTaxSystemReportsDel.Text = "+";
            this.btnTaxSystemReportsDel.Click += new System.EventHandler(this.btnTaxSystemReportsDel_Click);
            // 
            // btnTaxSystemReportsAdd
            // 
            this.btnTaxSystemReportsAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnTaxSystemReportsAdd.ImageOptions.Image")));
            this.btnTaxSystemReportsAdd.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnTaxSystemReportsAdd.Location = new System.Drawing.Point(6, 5);
            this.btnTaxSystemReportsAdd.Name = "btnTaxSystemReportsAdd";
            this.btnTaxSystemReportsAdd.Size = new System.Drawing.Size(25, 25);
            this.btnTaxSystemReportsAdd.TabIndex = 0;
            this.btnTaxSystemReportsAdd.Text = "+";
            this.btnTaxSystemReportsAdd.Click += new System.EventHandler(this.btnTaxSystemReportsAdd_Click);
            // 
            // panelControlFooter
            // 
            this.panelControlFooter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControlFooter.Controls.Add(this.btnCancel);
            this.panelControlFooter.Controls.Add(this.btnSave);
            this.panelControlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControlFooter.Location = new System.Drawing.Point(0, 525);
            this.panelControlFooter.Name = "panelControlFooter";
            this.panelControlFooter.Size = new System.Drawing.Size(784, 35);
            this.panelControlFooter.TabIndex = 32;
            // 
            // xtraTabTaxSystem
            // 
            this.xtraTabTaxSystem.AppearancePage.HeaderActive.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.xtraTabTaxSystem.AppearancePage.HeaderActive.Options.UseFont = true;
            this.xtraTabTaxSystem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabTaxSystem.Location = new System.Drawing.Point(0, 114);
            this.xtraTabTaxSystem.Name = "xtraTabTaxSystem";
            this.xtraTabTaxSystem.PageImagePosition = DevExpress.XtraTab.TabPageImagePosition.None;
            this.xtraTabTaxSystem.SelectedTabPage = this.xtraTabPageReport;
            this.xtraTabTaxSystem.Size = new System.Drawing.Size(784, 411);
            this.xtraTabTaxSystem.TabIndex = 91;
            this.xtraTabTaxSystem.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageReport});
            // 
            // xtraTabPageReport
            // 
            this.xtraTabPageReport.Controls.Add(this.gridControlTaxSystemReports);
            this.xtraTabPageReport.Controls.Add(this.panelControlHeader);
            this.xtraTabPageReport.Name = "xtraTabPageReport";
            this.xtraTabPageReport.Size = new System.Drawing.Size(782, 383);
            this.xtraTabPageReport.Text = "Доступные отчеты для данной системы налогооблажения";
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.lblCalculationScale);
            this.panelControl1.Controls.Add(this.btnCalculationScale);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 70);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(784, 44);
            this.panelControl1.TabIndex = 92;
            // 
            // lblCalculationScale
            // 
            this.lblCalculationScale.AutoSize = true;
            this.lblCalculationScale.Location = new System.Drawing.Point(65, 15);
            this.lblCalculationScale.Name = "lblCalculationScale";
            this.lblCalculationScale.Size = new System.Drawing.Size(59, 16);
            this.lblCalculationScale.TabIndex = 28;
            this.lblCalculationScale.Text = "Шкала:";
            // 
            // btnCalculationScale
            // 
            this.btnCalculationScale.Location = new System.Drawing.Point(140, 12);
            this.btnCalculationScale.Name = "btnCalculationScale";
            this.btnCalculationScale.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnCalculationScale.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnCalculationScale.Size = new System.Drawing.Size(632, 22);
            this.btnCalculationScale.TabIndex = 27;
            this.btnCalculationScale.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnCalculationScale_ButtonPressed);
            // 
            // TaxSystemEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 560);
            this.Controls.Add(this.xtraTabTaxSystem);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panelControlFooter);
            this.Controls.Add(this.panelControlTaxSystemHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "TaxSystemEdit";
            this.Text = "Система налогооблажения";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TaxSystemEdit_FormClosed);
            this.Load += new System.EventHandler(this.TaxSystemEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlTaxSystemHeader)).EndInit();
            this.panelControlTaxSystemHeader.ResumeLayout(false);
            this.panelControlTaxSystemHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlTaxSystemReports)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTaxSystemReports)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlHeader)).EndInit();
            this.panelControlHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlFooter)).EndInit();
            this.panelControlFooter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabTaxSystem)).EndInit();
            this.xtraTabTaxSystem.ResumeLayout(false);
            this.xtraTabPageReport.ResumeLayout(false);
            this.xtraTabPageReport.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCalculationScale.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private System.Windows.Forms.Label lblName;
        private DevExpress.XtraEditors.TextEdit txtName;
        private System.Windows.Forms.Label lblDescription;
        private DevExpress.XtraEditors.TextEdit txtDescription;
        private DevExpress.XtraEditors.PanelControl panelControlTaxSystemHeader;
        private DevExpress.XtraGrid.GridControl gridControlTaxSystemReports;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewTaxSystemReports;
        private DevExpress.XtraEditors.PanelControl panelControlHeader;
        private DevExpress.XtraEditors.SimpleButton btnTaxSystemReportsDel;
        private DevExpress.XtraEditors.SimpleButton btnTaxSystemReportsAdd;
        private DevExpress.XtraEditors.PanelControl panelControlFooter;
        private DevExpress.XtraTab.XtraTabControl xtraTabTaxSystem;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageReport;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.Label lblCalculationScale;
        private DevExpress.XtraEditors.ButtonEdit btnCalculationScale;
    }
}