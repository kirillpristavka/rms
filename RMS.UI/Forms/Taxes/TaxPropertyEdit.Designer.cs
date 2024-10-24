namespace RMS.UI.Forms.Taxes
{
    partial class TaxPropertyEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaxPropertyEdit));
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.lblDate = new DevExpress.XtraEditors.LabelControl();
            this.checkIsReducedRate = new DevExpress.XtraEditors.CheckEdit();
            this.lblCustomer = new DevExpress.XtraEditors.LabelControl();
            this.btnCustomer = new DevExpress.XtraEditors.ButtonEdit();
            this.dateDate = new DevExpress.XtraEditors.DateEdit();
            this.lblTotalRate = new DevExpress.XtraEditors.LabelControl();
            this.spinTotalRate = new DevExpress.XtraEditors.SpinEdit();
            this.gpRate = new DevExpress.XtraEditors.GroupControl();
            this.gpPrivilege = new DevExpress.XtraEditors.GroupControl();
            this.checkIsFillingSecondSectionWithOnePrivilege = new DevExpress.XtraEditors.CheckEdit();
            this.checkIsTaxReduced = new DevExpress.XtraEditors.CheckEdit();
            this.btnPrivilege = new DevExpress.XtraEditors.ButtonEdit();
            this.spinReducedBy = new DevExpress.XtraEditors.SpinEdit();
            this.checkIsPropertyExemptFromTax = new DevExpress.XtraEditors.CheckEdit();
            this.btnChronicle = new DevExpress.XtraEditors.SimpleButton();
            this.memoComment = new DevExpress.XtraEditors.MemoEdit();
            this.gpComment = new DevExpress.XtraEditors.GroupControl();
            this.lblAvailability = new DevExpress.XtraEditors.LabelControl();
            this.cmbAvailability = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsReducedRate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTotalRate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpRate)).BeginInit();
            this.gpRate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gpPrivilege)).BeginInit();
            this.gpPrivilege.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsFillingSecondSectionWithOnePrivilege.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsTaxReduced.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrivilege.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinReducedBy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsPropertyExemptFromTax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpComment)).BeginInit();
            this.gpComment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAvailability.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(375, 331);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(481, 331);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblDate
            // 
            this.lblDate.Location = new System.Drawing.Point(421, 47);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(39, 16);
            this.lblDate.TabIndex = 3;
            this.lblDate.Text = "Дата:";
            // 
            // checkIsReducedRate
            // 
            this.checkIsReducedRate.Location = new System.Drawing.Point(283, 26);
            this.checkIsReducedRate.Name = "checkIsReducedRate";
            this.checkIsReducedRate.Properties.Caption = "Это сниженная ставка";
            this.checkIsReducedRate.Size = new System.Drawing.Size(195, 20);
            this.checkIsReducedRate.TabIndex = 9;
            // 
            // lblCustomer
            // 
            this.lblCustomer.Location = new System.Drawing.Point(26, 15);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(53, 16);
            this.lblCustomer.TabIndex = 1;
            this.lblCustomer.Text = "Клиент:";
            // 
            // btnCustomer
            // 
            this.btnCustomer.Location = new System.Drawing.Point(91, 12);
            this.btnCustomer.Name = "btnCustomer";
            this.btnCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnCustomer.Properties.ReadOnly = true;
            this.btnCustomer.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnCustomer.Size = new System.Drawing.Size(481, 22);
            this.btnCustomer.TabIndex = 2;
            this.btnCustomer.TabStop = false;
            this.btnCustomer.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnCustomer_ButtonPressed);
            // 
            // dateDate
            // 
            this.dateDate.EditValue = null;
            this.dateDate.Location = new System.Drawing.Point(472, 44);
            this.dateDate.Name = "dateDate";
            this.dateDate.Properties.Appearance.Options.UseTextOptions = true;
            this.dateDate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dateDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.dateDate.Size = new System.Drawing.Size(100, 22);
            this.dateDate.TabIndex = 4;
            this.dateDate.TabStop = false;
            // 
            // lblTotalRate
            // 
            this.lblTotalRate.Location = new System.Drawing.Point(98, 28);
            this.lblTotalRate.Name = "lblTotalRate";
            this.lblTotalRate.Size = new System.Drawing.Size(52, 16);
            this.lblTotalRate.TabIndex = 7;
            this.lblTotalRate.Text = "Общая:";
            // 
            // spinTotalRate
            // 
            this.spinTotalRate.EditValue = new decimal(new int[] {
            22,
            0,
            0,
            196608});
            this.spinTotalRate.Location = new System.Drawing.Point(167, 24);
            this.spinTotalRate.Name = "spinTotalRate";
            this.spinTotalRate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinTotalRate.Properties.DisplayFormat.FormatString = "p";
            this.spinTotalRate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinTotalRate.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.spinTotalRate.Properties.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.spinTotalRate.Properties.Mask.EditMask = "p";
            this.spinTotalRate.Size = new System.Drawing.Size(100, 22);
            this.spinTotalRate.TabIndex = 8;
            // 
            // gpRate
            // 
            this.gpRate.Controls.Add(this.spinTotalRate);
            this.gpRate.Controls.Add(this.lblTotalRate);
            this.gpRate.Controls.Add(this.checkIsReducedRate);
            this.gpRate.Enabled = false;
            this.gpRate.Location = new System.Drawing.Point(12, 72);
            this.gpRate.Name = "gpRate";
            this.gpRate.Size = new System.Drawing.Size(569, 62);
            this.gpRate.TabIndex = 6;
            this.gpRate.Text = "Ставка налога";
            // 
            // gpPrivilege
            // 
            this.gpPrivilege.Controls.Add(this.checkIsFillingSecondSectionWithOnePrivilege);
            this.gpPrivilege.Controls.Add(this.checkIsTaxReduced);
            this.gpPrivilege.Controls.Add(this.btnPrivilege);
            this.gpPrivilege.Controls.Add(this.spinReducedBy);
            this.gpPrivilege.Controls.Add(this.checkIsPropertyExemptFromTax);
            this.gpPrivilege.Enabled = false;
            this.gpPrivilege.Location = new System.Drawing.Point(12, 140);
            this.gpPrivilege.Name = "gpPrivilege";
            this.gpPrivilege.Size = new System.Drawing.Size(569, 117);
            this.gpPrivilege.TabIndex = 11;
            this.gpPrivilege.Text = "Льготы";
            // 
            // checkIsFillingSecondSectionWithOnePrivilege
            // 
            this.checkIsFillingSecondSectionWithOnePrivilege.Location = new System.Drawing.Point(26, 81);
            this.checkIsFillingSecondSectionWithOnePrivilege.Name = "checkIsFillingSecondSectionWithOnePrivilege";
            this.checkIsFillingSecondSectionWithOnePrivilege.Properties.Caption = "При наличии одной льготы заполнять один лист раздела 2 декларации";
            this.checkIsFillingSecondSectionWithOnePrivilege.Size = new System.Drawing.Size(534, 20);
            this.checkIsFillingSecondSectionWithOnePrivilege.TabIndex = 16;
            // 
            // checkIsTaxReduced
            // 
            this.checkIsTaxReduced.Location = new System.Drawing.Point(26, 53);
            this.checkIsTaxReduced.Name = "checkIsTaxReduced";
            this.checkIsTaxReduced.Properties.Caption = "Налог уменьшен на:";
            this.checkIsTaxReduced.Size = new System.Drawing.Size(168, 20);
            this.checkIsTaxReduced.TabIndex = 14;
            this.checkIsTaxReduced.CheckedChanged += new System.EventHandler(this.checkIsTaxReduced_CheckedChanged);
            // 
            // btnPrivilege
            // 
            this.btnPrivilege.Enabled = false;
            this.btnPrivilege.Location = new System.Drawing.Point(427, 24);
            this.btnPrivilege.Name = "btnPrivilege";
            this.btnPrivilege.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnPrivilege.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnPrivilege.Size = new System.Drawing.Size(133, 22);
            this.btnPrivilege.TabIndex = 13;
            this.btnPrivilege.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnPrivilege_ButtonPressed);
            // 
            // spinReducedBy
            // 
            this.spinReducedBy.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinReducedBy.Enabled = false;
            this.spinReducedBy.Location = new System.Drawing.Point(200, 52);
            this.spinReducedBy.Name = "spinReducedBy";
            this.spinReducedBy.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinReducedBy.Properties.DisplayFormat.FormatString = "p";
            this.spinReducedBy.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinReducedBy.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.spinReducedBy.Properties.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.spinReducedBy.Properties.Mask.EditMask = "p";
            this.spinReducedBy.Size = new System.Drawing.Size(101, 22);
            this.spinReducedBy.TabIndex = 15;
            // 
            // checkIsPropertyExemptFromTax
            // 
            this.checkIsPropertyExemptFromTax.Location = new System.Drawing.Point(26, 25);
            this.checkIsPropertyExemptFromTax.Name = "checkIsPropertyExemptFromTax";
            this.checkIsPropertyExemptFromTax.Properties.Caption = "Все имущество освободжено от налога, код льготы:";
            this.checkIsPropertyExemptFromTax.Size = new System.Drawing.Size(395, 20);
            this.checkIsPropertyExemptFromTax.TabIndex = 12;
            this.checkIsPropertyExemptFromTax.CheckedChanged += new System.EventHandler(this.checkIsPropertyExemptFromTax_CheckedChanged);
            // 
            // btnChronicle
            // 
            this.btnChronicle.Location = new System.Drawing.Point(12, 331);
            this.btnChronicle.Name = "btnChronicle";
            this.btnChronicle.Size = new System.Drawing.Size(200, 23);
            this.btnChronicle.TabIndex = 5;
            this.btnChronicle.Text = "История изменений";
            this.btnChronicle.Click += new System.EventHandler(this.btnChronicle_Click);
            // 
            // memoComment
            // 
            this.memoComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoComment.EditValue = "";
            this.memoComment.Location = new System.Drawing.Point(2, 21);
            this.memoComment.Name = "memoComment";
            this.memoComment.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.memoComment.Size = new System.Drawing.Size(565, 39);
            this.memoComment.TabIndex = 18;
            // 
            // gpComment
            // 
            this.gpComment.Controls.Add(this.memoComment);
            this.gpComment.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.gpComment.Location = new System.Drawing.Point(12, 263);
            this.gpComment.Name = "gpComment";
            this.gpComment.Size = new System.Drawing.Size(569, 62);
            this.gpComment.TabIndex = 17;
            this.gpComment.Text = "Комментарий";
            // 
            // lblAvailability
            // 
            this.lblAvailability.Location = new System.Drawing.Point(16, 47);
            this.lblAvailability.Name = "lblAvailability";
            this.lblAvailability.Size = new System.Drawing.Size(63, 16);
            this.lblAvailability.TabIndex = 22;
            this.lblAvailability.Text = "Наличие:";
            // 
            // cmbAvailability
            // 
            this.cmbAvailability.Location = new System.Drawing.Point(91, 44);
            this.cmbAvailability.Name = "cmbAvailability";
            this.cmbAvailability.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbAvailability.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbAvailability.Size = new System.Drawing.Size(100, 22);
            this.cmbAvailability.TabIndex = 21;
            this.cmbAvailability.SelectedIndexChanged += new System.EventHandler(this.cmbAvailability_SelectedIndexChanged);
            // 
            // TaxPropertyEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 360);
            this.Controls.Add(this.lblAvailability);
            this.Controls.Add(this.cmbAvailability);
            this.Controls.Add(this.gpComment);
            this.Controls.Add(this.btnChronicle);
            this.Controls.Add(this.gpPrivilege);
            this.Controls.Add(this.gpRate);
            this.Controls.Add(this.dateDate);
            this.Controls.Add(this.lblCustomer);
            this.Controls.Add(this.btnCustomer);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "TaxPropertyEdit";
            this.Text = "Налог на имущество";
            this.Load += new System.EventHandler(this.CustomerServiceProvidedEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.checkIsReducedRate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTotalRate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpRate)).EndInit();
            this.gpRate.ResumeLayout(false);
            this.gpRate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gpPrivilege)).EndInit();
            this.gpPrivilege.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkIsFillingSecondSectionWithOnePrivilege.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsTaxReduced.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrivilege.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinReducedBy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsPropertyExemptFromTax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpComment)).EndInit();
            this.gpComment.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbAvailability.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LabelControl lblDate;
        private DevExpress.XtraEditors.CheckEdit checkIsReducedRate;
        private DevExpress.XtraEditors.LabelControl lblCustomer;
        private DevExpress.XtraEditors.ButtonEdit btnCustomer;
        private DevExpress.XtraEditors.DateEdit dateDate;
        private DevExpress.XtraEditors.LabelControl lblTotalRate;
        private DevExpress.XtraEditors.SpinEdit spinTotalRate;
        private DevExpress.XtraEditors.GroupControl gpRate;
        private DevExpress.XtraEditors.GroupControl gpPrivilege;
        private DevExpress.XtraEditors.CheckEdit checkIsFillingSecondSectionWithOnePrivilege;
        private DevExpress.XtraEditors.CheckEdit checkIsTaxReduced;
        private DevExpress.XtraEditors.ButtonEdit btnPrivilege;
        private DevExpress.XtraEditors.SpinEdit spinReducedBy;
        private DevExpress.XtraEditors.CheckEdit checkIsPropertyExemptFromTax;
        private DevExpress.XtraEditors.SimpleButton btnChronicle;
        private DevExpress.XtraEditors.MemoEdit memoComment;
        private DevExpress.XtraEditors.GroupControl gpComment;
        private DevExpress.XtraEditors.LabelControl lblAvailability;
        private DevExpress.XtraEditors.ComboBoxEdit cmbAvailability;
    }
}