namespace RMS.UI.Forms.Print
{
    partial class OrganizationPerformancePrint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrganizationPerformancePrint));
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.lblCustomer = new DevExpress.XtraEditors.LabelControl();
            this.btnCustomer = new DevExpress.XtraEditors.ButtonEdit();
            this.checkEditIsAllCustomer = new DevExpress.XtraEditors.CheckEdit();
            this.lblPeriodTo = new DevExpress.XtraEditors.LabelControl();
            this.cmbYearSince = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblPeriodSince = new DevExpress.XtraEditors.LabelControl();
            this.cmbMonthSince = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbYearTo = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbMonthTo = new DevExpress.XtraEditors.ComboBoxEdit();
            this.radioGroup = new DevExpress.XtraEditors.RadioGroup();
            ((System.ComponentModel.ISupportInitialize)(this.btnCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditIsAllCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbYearSince.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMonthSince.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbYearTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMonthTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(366, 105);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 25);
            this.btnSave.TabIndex = 59;
            this.btnSave.Text = "Просмотр";
            this.btnSave.Click += new System.EventHandler(this.btnViewing_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(472, 105);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 25);
            this.btnCancel.TabIndex = 58;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblCustomer
            // 
            this.lblCustomer.Location = new System.Drawing.Point(23, 16);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(53, 16);
            this.lblCustomer.TabIndex = 74;
            this.lblCustomer.Text = "Клиент:";
            // 
            // btnCustomer
            // 
            this.btnCustomer.Location = new System.Drawing.Point(82, 12);
            this.btnCustomer.Name = "btnCustomer";
            this.btnCustomer.Properties.AutoHeight = false;
            this.btnCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnCustomer.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnCustomer.Size = new System.Drawing.Size(490, 25);
            this.btnCustomer.TabIndex = 73;
            this.btnCustomer.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnCustomer_ButtonPressed);
            // 
            // checkEditIsAllCustomer
            // 
            this.checkEditIsAllCustomer.Location = new System.Drawing.Point(394, 74);
            this.checkEditIsAllCustomer.Name = "checkEditIsAllCustomer";
            this.checkEditIsAllCustomer.Properties.AutoHeight = false;
            this.checkEditIsAllCustomer.Properties.Caption = "Отбор всех клиентов";
            this.checkEditIsAllCustomer.Size = new System.Drawing.Size(178, 25);
            this.checkEditIsAllCustomer.TabIndex = 75;
            this.checkEditIsAllCustomer.CheckedChanged += new System.EventHandler(this.checkEditIsAllCustomer_CheckedChanged);
            // 
            // lblPeriodTo
            // 
            this.lblPeriodTo.Location = new System.Drawing.Point(319, 47);
            this.lblPeriodTo.Name = "lblPeriodTo";
            this.lblPeriodTo.Size = new System.Drawing.Size(16, 16);
            this.lblPeriodTo.TabIndex = 79;
            this.lblPeriodTo.Text = "по";
            // 
            // cmbYearSince
            // 
            this.cmbYearSince.EditValue = "2020";
            this.cmbYearSince.Location = new System.Drawing.Point(238, 43);
            this.cmbYearSince.Name = "cmbYearSince";
            this.cmbYearSince.Properties.AutoHeight = false;
            this.cmbYearSince.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.cmbYearSince.Properties.Items.AddRange(new object[] {
            "2015",
            "2016",
            "2017",
            "2018",
            "2019",
            "2020",
            "2021",
            "2022"});
            this.cmbYearSince.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbYearSince.Size = new System.Drawing.Size(75, 25);
            this.cmbYearSince.TabIndex = 78;
            this.cmbYearSince.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.ButtonPressed);
            // 
            // lblPeriodSince
            // 
            this.lblPeriodSince.Location = new System.Drawing.Point(8, 47);
            this.lblPeriodSince.Name = "lblPeriodSince";
            this.lblPeriodSince.Size = new System.Drawing.Size(68, 16);
            this.lblPeriodSince.TabIndex = 77;
            this.lblPeriodSince.Text = "Период с:";
            // 
            // cmbMonthSince
            // 
            this.cmbMonthSince.EditValue = "";
            this.cmbMonthSince.Location = new System.Drawing.Point(82, 43);
            this.cmbMonthSince.Name = "cmbMonthSince";
            this.cmbMonthSince.Properties.AutoHeight = false;
            this.cmbMonthSince.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.cmbMonthSince.Properties.DropDownRows = 12;
            this.cmbMonthSince.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbMonthSince.Size = new System.Drawing.Size(150, 25);
            this.cmbMonthSince.TabIndex = 76;
            this.cmbMonthSince.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.ButtonPressed);
            // 
            // cmbYearTo
            // 
            this.cmbYearTo.EditValue = "2020";
            this.cmbYearTo.Location = new System.Drawing.Point(497, 43);
            this.cmbYearTo.Name = "cmbYearTo";
            this.cmbYearTo.Properties.AutoHeight = false;
            this.cmbYearTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.cmbYearTo.Properties.Items.AddRange(new object[] {
            "2015",
            "2016",
            "2017",
            "2018",
            "2019",
            "2020",
            "2021",
            "2022"});
            this.cmbYearTo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbYearTo.Size = new System.Drawing.Size(75, 25);
            this.cmbYearTo.TabIndex = 81;
            this.cmbYearTo.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.ButtonPressed);
            // 
            // cmbMonthTo
            // 
            this.cmbMonthTo.EditValue = "";
            this.cmbMonthTo.Location = new System.Drawing.Point(341, 43);
            this.cmbMonthTo.Name = "cmbMonthTo";
            this.cmbMonthTo.Properties.AutoHeight = false;
            this.cmbMonthTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.cmbMonthTo.Properties.DropDownRows = 12;
            this.cmbMonthTo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbMonthTo.Size = new System.Drawing.Size(150, 25);
            this.cmbMonthTo.TabIndex = 80;
            this.cmbMonthTo.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.ButtonPressed);
            // 
            // radioGroup
            // 
            this.radioGroup.Location = new System.Drawing.Point(32, 105);
            this.radioGroup.Name = "radioGroup";
            this.radioGroup.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Вариант 1"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Вариант 2")});
            this.radioGroup.Size = new System.Drawing.Size(328, 25);
            this.radioGroup.TabIndex = 82;
            // 
            // OrganizationPerformancePrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 140);
            this.Controls.Add(this.radioGroup);
            this.Controls.Add(this.cmbYearTo);
            this.Controls.Add(this.cmbMonthTo);
            this.Controls.Add(this.lblPeriodTo);
            this.Controls.Add(this.cmbYearSince);
            this.Controls.Add(this.lblPeriodSince);
            this.Controls.Add(this.cmbMonthSince);
            this.Controls.Add(this.checkEditIsAllCustomer);
            this.Controls.Add(this.lblCustomer);
            this.Controls.Add(this.btnCustomer);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "OrganizationPerformancePrint";
            this.Text = "Печать показателей работы организации";
            this.Load += new System.EventHandler(this.CustomerServiceProvidedEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditIsAllCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbYearSince.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMonthSince.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbYearTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMonthTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LabelControl lblCustomer;
        private DevExpress.XtraEditors.ButtonEdit btnCustomer;
        private DevExpress.XtraEditors.CheckEdit checkEditIsAllCustomer;
        private DevExpress.XtraEditors.LabelControl lblPeriodTo;
        private DevExpress.XtraEditors.ComboBoxEdit cmbYearSince;
        private DevExpress.XtraEditors.LabelControl lblPeriodSince;
        private DevExpress.XtraEditors.ComboBoxEdit cmbMonthSince;
        private DevExpress.XtraEditors.ComboBoxEdit cmbYearTo;
        private DevExpress.XtraEditors.ComboBoxEdit cmbMonthTo;
        private DevExpress.XtraEditors.RadioGroup radioGroup;
    }
}