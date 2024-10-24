namespace RMS.UI.Forms.Directories
{
    partial class OrganizationPerformanceEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrganizationPerformanceEdit));
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.panelControlFooter = new DevExpress.XtraEditors.PanelControl();
            this.lblFAQ = new DevExpress.XtraEditors.LabelControl();
            this.btnGenerateInvoicePayment = new DevExpress.XtraEditors.SimpleButton();
            this.memoComment = new DevExpress.XtraEditors.MemoEdit();
            this.gpComment = new DevExpress.XtraEditors.GroupControl();
            this.cmbYear = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbMonth = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblPeriod = new DevExpress.XtraEditors.LabelControl();
            this.panelControlHeader = new DevExpress.XtraEditors.PanelControl();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblCustomer = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlFooter)).BeginInit();
            this.panelControlFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpComment)).BeginInit();
            this.gpComment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbYear.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlHeader)).BeginInit();
            this.panelControlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(644, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 25);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(750, 10);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 25);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panelControlFooter
            // 
            this.panelControlFooter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControlFooter.Controls.Add(this.lblFAQ);
            this.panelControlFooter.Controls.Add(this.btnGenerateInvoicePayment);
            this.panelControlFooter.Controls.Add(this.btnSave);
            this.panelControlFooter.Controls.Add(this.btnCancel);
            this.panelControlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControlFooter.Location = new System.Drawing.Point(0, 565);
            this.panelControlFooter.Name = "panelControlFooter";
            this.panelControlFooter.Size = new System.Drawing.Size(862, 45);
            this.panelControlFooter.TabIndex = 4;
            // 
            // lblFAQ
            // 
            this.lblFAQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFAQ.Appearance.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblFAQ.Appearance.Options.UseFont = true;
            this.lblFAQ.Appearance.Options.UseTextOptions = true;
            this.lblFAQ.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblFAQ.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblFAQ.AppearanceDisabled.Options.UseTextOptions = true;
            this.lblFAQ.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblFAQ.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblFAQ.AppearanceHovered.Options.UseTextOptions = true;
            this.lblFAQ.AppearanceHovered.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblFAQ.AppearanceHovered.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblFAQ.AppearancePressed.Options.UseTextOptions = true;
            this.lblFAQ.AppearancePressed.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblFAQ.AppearancePressed.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblFAQ.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblFAQ.Location = new System.Drawing.Point(218, 6);
            this.lblFAQ.Name = "lblFAQ";
            this.lblFAQ.Size = new System.Drawing.Size(420, 32);
            this.lblFAQ.TabIndex = 2;
            // 
            // btnGenerateInvoicePayment
            // 
            this.btnGenerateInvoicePayment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGenerateInvoicePayment.Location = new System.Drawing.Point(12, 10);
            this.btnGenerateInvoicePayment.Name = "btnGenerateInvoicePayment";
            this.btnGenerateInvoicePayment.Size = new System.Drawing.Size(200, 25);
            this.btnGenerateInvoicePayment.TabIndex = 5;
            this.btnGenerateInvoicePayment.Text = "Сформировать счет";
            this.btnGenerateInvoicePayment.Click += new System.EventHandler(this.btnGenerateInvoicePayment_Click);
            // 
            // memoComment
            // 
            this.memoComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoComment.EditValue = "";
            this.memoComment.Location = new System.Drawing.Point(0, 20);
            this.memoComment.Name = "memoComment";
            this.memoComment.Size = new System.Drawing.Size(862, 45);
            this.memoComment.TabIndex = 3;
            // 
            // gpComment
            // 
            this.gpComment.AppearanceCaption.Options.UseTextOptions = true;
            this.gpComment.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gpComment.Controls.Add(this.memoComment);
            this.gpComment.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gpComment.GroupStyle = DevExpress.Utils.GroupStyle.Title;
            this.gpComment.Location = new System.Drawing.Point(0, 500);
            this.gpComment.Name = "gpComment";
            this.gpComment.Size = new System.Drawing.Size(862, 65);
            this.gpComment.TabIndex = 3;
            this.gpComment.Text = "Комментарий";
            // 
            // cmbYear
            // 
            this.cmbYear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbYear.EditValue = "2020";
            this.cmbYear.Location = new System.Drawing.Point(785, 10);
            this.cmbYear.Name = "cmbYear";
            this.cmbYear.Properties.AutoHeight = false;
            this.cmbYear.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbYear.Properties.DropDownRows = 12;
            this.cmbYear.Properties.Items.AddRange(new object[] {
            "2015",
            "2016",
            "2017",
            "2018",
            "2019",
            "2020",
            "2021",
            "2022",
            "2023",
            "2024",
            "2025",
            "2026"});
            this.cmbYear.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbYear.Size = new System.Drawing.Size(65, 25);
            this.cmbYear.TabIndex = 1;
            // 
            // cmbMonth
            // 
            this.cmbMonth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbMonth.EditValue = "";
            this.cmbMonth.Location = new System.Drawing.Point(643, 10);
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.Properties.AutoHeight = false;
            this.cmbMonth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbMonth.Properties.DropDownRows = 12;
            this.cmbMonth.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbMonth.Size = new System.Drawing.Size(136, 25);
            this.cmbMonth.TabIndex = 1;
            // 
            // lblPeriod
            // 
            this.lblPeriod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPeriod.Location = new System.Drawing.Point(572, 14);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new System.Drawing.Size(55, 16);
            this.lblPeriod.TabIndex = 1;
            this.lblPeriod.Text = "Период:";
            // 
            // panelControlHeader
            // 
            this.panelControlHeader.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControlHeader.Controls.Add(this.lblCustomer);
            this.panelControlHeader.Controls.Add(this.lblPeriod);
            this.panelControlHeader.Controls.Add(this.cmbMonth);
            this.panelControlHeader.Controls.Add(this.cmbYear);
            this.panelControlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControlHeader.Location = new System.Drawing.Point(0, 0);
            this.panelControlHeader.Name = "panelControlHeader";
            this.panelControlHeader.Size = new System.Drawing.Size(862, 45);
            this.panelControlHeader.TabIndex = 1;
            // 
            // gridControl
            // 
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.Location = new System.Drawing.Point(0, 45);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(862, 455);
            this.gridControl.TabIndex = 40;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            this.gridView.OptionsView.ShowGroupPanel = false;
            this.gridView.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gridView_CustomRowCellEdit);
            // 
            // lblCustomer
            // 
            this.lblCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCustomer.Appearance.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblCustomer.Appearance.Options.UseFont = true;
            this.lblCustomer.Location = new System.Drawing.Point(12, 14);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(57, 16);
            this.lblCustomer.TabIndex = 2;
            this.lblCustomer.Text = "Клиент:";
            // 
            // OrganizationPerformanceEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 610);
            this.Controls.Add(this.gridControl);
            this.Controls.Add(this.panelControlHeader);
            this.Controls.Add(this.gpComment);
            this.Controls.Add(this.panelControlFooter);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OrganizationPerformanceEdit";
            this.Text = "Показатели работы организации";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OrganizationPerformanceEdit_FormClosed);
            this.Load += new System.EventHandler(this.Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlFooter)).EndInit();
            this.panelControlFooter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpComment)).EndInit();
            this.gpComment.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbYear.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlHeader)).EndInit();
            this.panelControlHeader.ResumeLayout(false);
            this.panelControlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.PanelControl panelControlFooter;
        private DevExpress.XtraEditors.MemoEdit memoComment;
        private DevExpress.XtraEditors.GroupControl gpComment;
        private DevExpress.XtraEditors.ComboBoxEdit cmbYear;
        private DevExpress.XtraEditors.ComboBoxEdit cmbMonth;
        private DevExpress.XtraEditors.LabelControl lblPeriod;
        private DevExpress.XtraEditors.PanelControl panelControlHeader;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraEditors.SimpleButton btnGenerateInvoicePayment;
        private DevExpress.XtraEditors.LabelControl lblFAQ;
        private DevExpress.XtraEditors.LabelControl lblCustomer;
    }
}