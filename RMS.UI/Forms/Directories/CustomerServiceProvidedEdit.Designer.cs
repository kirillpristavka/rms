namespace RMS.UI.Forms.Directories
{
    partial class CustomerServiceProvidedEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomerServiceProvidedEdit));
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.lblDate = new DevExpress.XtraEditors.LabelControl();
            this.lblName = new DevExpress.XtraEditors.LabelControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.checkIsServicePrice = new DevExpress.XtraEditors.CheckEdit();
            this.lblPriceList = new DevExpress.XtraEditors.LabelControl();
            this.spinCount = new DevExpress.XtraEditors.SpinEdit();
            this.calcPrice = new DevExpress.XtraEditors.CalcEdit();
            this.lblCount = new DevExpress.XtraEditors.LabelControl();
            this.btnPriceList = new DevExpress.XtraEditors.ButtonEdit();
            this.lblPrice = new DevExpress.XtraEditors.LabelControl();
            this.lblStaff = new DevExpress.XtraEditors.LabelControl();
            this.btnStaff = new DevExpress.XtraEditors.ButtonEdit();
            this.lblCustomer = new DevExpress.XtraEditors.LabelControl();
            this.btnCustomer = new DevExpress.XtraEditors.ButtonEdit();
            this.dateDate = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsServicePrice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinCount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.calcPrice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPriceList.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnStaff.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(392, 183);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 23);
            this.btnSave.TabIndex = 59;
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(485, 183);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 23);
            this.btnCancel.TabIndex = 58;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblDate
            // 
            this.lblDate.Location = new System.Drawing.Point(111, 72);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(39, 16);
            this.lblDate.TabIndex = 61;
            this.lblDate.Text = "Дата:";
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(46, 100);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(104, 16);
            this.lblName.TabIndex = 63;
            this.lblName.Text = "Наименование:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(172, 97);
            this.txtName.Name = "txtName";
            this.txtName.Properties.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(400, 22);
            this.txtName.TabIndex = 62;
            // 
            // checkIsServicePrice
            // 
            this.checkIsServicePrice.EditValue = true;
            this.checkIsServicePrice.Location = new System.Drawing.Point(336, 69);
            this.checkIsServicePrice.Name = "checkIsServicePrice";
            this.checkIsServicePrice.Properties.Caption = "Услуга по прайсу";
            this.checkIsServicePrice.Size = new System.Drawing.Size(149, 20);
            this.checkIsServicePrice.TabIndex = 64;
            this.checkIsServicePrice.CheckedChanged += new System.EventHandler(this.checkIsServicePrice_CheckedChanged);
            // 
            // lblPriceList
            // 
            this.lblPriceList.Location = new System.Drawing.Point(14, 128);
            this.lblPriceList.Name = "lblPriceList";
            this.lblPriceList.Size = new System.Drawing.Size(136, 16);
            this.lblPriceList.TabIndex = 66;
            this.lblPriceList.Text = "Позиция из прайса:";
            // 
            // spinCount
            // 
            this.spinCount.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinCount.Location = new System.Drawing.Point(172, 153);
            this.spinCount.Name = "spinCount";
            this.spinCount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinCount.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.spinCount.Properties.Mask.EditMask = "d";
            this.spinCount.Size = new System.Drawing.Size(101, 22);
            this.spinCount.TabIndex = 67;
            this.spinCount.EditValueChanged += new System.EventHandler(this.spinCount_EditValueChanged);
            // 
            // calcPrice
            // 
            this.calcPrice.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.calcPrice.Location = new System.Drawing.Point(392, 153);
            this.calcPrice.Name = "calcPrice";
            this.calcPrice.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.calcPrice.Properties.DisplayFormat.FormatString = "n";
            this.calcPrice.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.calcPrice.Properties.Mask.EditMask = "n";
            this.calcPrice.Properties.ReadOnly = true;
            this.calcPrice.Size = new System.Drawing.Size(180, 22);
            this.calcPrice.TabIndex = 68;
            // 
            // lblCount
            // 
            this.lblCount.Location = new System.Drawing.Point(65, 156);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(85, 16);
            this.lblCount.TabIndex = 69;
            this.lblCount.Text = "Количество:";
            // 
            // btnPriceList
            // 
            this.btnPriceList.Location = new System.Drawing.Point(172, 125);
            this.btnPriceList.Name = "btnPriceList";
            this.btnPriceList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnPriceList.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnPriceList.Size = new System.Drawing.Size(400, 22);
            this.btnPriceList.TabIndex = 65;
            this.btnPriceList.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnPriceList_ButtonPressed);
            this.btnPriceList.EditValueChanged += new System.EventHandler(this.btnPriceList_EditValueChanged);
            // 
            // lblPrice
            // 
            this.lblPrice.Location = new System.Drawing.Point(336, 156);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(40, 16);
            this.lblPrice.TabIndex = 70;
            this.lblPrice.Text = "Цена:";
            // 
            // lblStaff
            // 
            this.lblStaff.Location = new System.Drawing.Point(72, 44);
            this.lblStaff.Name = "lblStaff";
            this.lblStaff.Size = new System.Drawing.Size(78, 16);
            this.lblStaff.TabIndex = 72;
            this.lblStaff.Text = "Сотрудник:";
            // 
            // btnStaff
            // 
            this.btnStaff.Location = new System.Drawing.Point(172, 41);
            this.btnStaff.Name = "btnStaff";
            this.btnStaff.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnStaff.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnStaff.Size = new System.Drawing.Size(400, 22);
            this.btnStaff.TabIndex = 71;
            this.btnStaff.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnStaff_ButtonPressed);
            // 
            // lblCustomer
            // 
            this.lblCustomer.Location = new System.Drawing.Point(12, 15);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(53, 16);
            this.lblCustomer.TabIndex = 74;
            this.lblCustomer.Text = "Клиент:";
            // 
            // btnCustomer
            // 
            this.btnCustomer.Location = new System.Drawing.Point(72, 12);
            this.btnCustomer.Name = "btnCustomer";
            this.btnCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnCustomer.Properties.ReadOnly = true;
            this.btnCustomer.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnCustomer.Size = new System.Drawing.Size(500, 22);
            this.btnCustomer.TabIndex = 73;
            this.btnCustomer.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnCustomer_ButtonPressed);
            // 
            // dateDate
            // 
            this.dateDate.EditValue = null;
            this.dateDate.Location = new System.Drawing.Point(172, 69);
            this.dateDate.Name = "dateDate";
            this.dateDate.Properties.Appearance.Options.UseTextOptions = true;
            this.dateDate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dateDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.dateDate.Size = new System.Drawing.Size(101, 22);
            this.dateDate.TabIndex = 75;
            this.dateDate.TabStop = false;
            // 
            // CustomerServiceProvidedEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 216);
            this.Controls.Add(this.dateDate);
            this.Controls.Add(this.lblCustomer);
            this.Controls.Add(this.btnCustomer);
            this.Controls.Add(this.lblStaff);
            this.Controls.Add(this.btnStaff);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.lblPriceList);
            this.Controls.Add(this.checkIsServicePrice);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.spinCount);
            this.Controls.Add(this.calcPrice);
            this.Controls.Add(this.btnPriceList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CustomerServiceProvidedEdit";
            this.Text = "Предоставляемая услуга";
            this.Load += new System.EventHandler(this.CustomerServiceProvidedEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsServicePrice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinCount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.calcPrice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPriceList.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnStaff.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDate.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LabelControl lblDate;
        private DevExpress.XtraEditors.LabelControl lblName;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.CheckEdit checkIsServicePrice;
        private DevExpress.XtraEditors.LabelControl lblPriceList;
        private DevExpress.XtraEditors.SpinEdit spinCount;
        private DevExpress.XtraEditors.CalcEdit calcPrice;
        private DevExpress.XtraEditors.LabelControl lblCount;
        private DevExpress.XtraEditors.ButtonEdit btnPriceList;
        private DevExpress.XtraEditors.LabelControl lblPrice;
        private DevExpress.XtraEditors.LabelControl lblStaff;
        private DevExpress.XtraEditors.ButtonEdit btnStaff;
        private DevExpress.XtraEditors.LabelControl lblCustomer;
        private DevExpress.XtraEditors.ButtonEdit btnCustomer;
        private DevExpress.XtraEditors.DateEdit dateDate;
    }
}