namespace RMS.UI.Forms.Directories
{
    partial class SubdivisionEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubdivisionEdit));
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.dateInput = new DevExpress.XtraEditors.DateEdit();
            this.lblCustomer = new DevExpress.XtraEditors.LabelControl();
            this.btnCustomer = new DevExpress.XtraEditors.ButtonEdit();
            this.lblInputDate = new DevExpress.XtraEditors.LabelControl();
            this.lblName = new DevExpress.XtraEditors.LabelControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.lblOKTMO = new DevExpress.XtraEditors.LabelControl();
            this.txtOKTMO = new DevExpress.XtraEditors.TextEdit();
            this.lblKPP = new DevExpress.XtraEditors.LabelControl();
            this.txtKPP = new DevExpress.XtraEditors.TextEdit();
            this.lblIFNS = new DevExpress.XtraEditors.LabelControl();
            this.txtIFNS = new DevExpress.XtraEditors.TextEdit();
            this.checkIsSeparateDivision = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.dateInput.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateInput.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOKTMO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKPP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIFNS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsSeparateDivision.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(392, 123);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 23);
            this.btnSave.TabIndex = 26;
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(485, 123);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 23);
            this.btnCancel.TabIndex = 25;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dateInput
            // 
            this.dateInput.EditValue = null;
            this.dateInput.Location = new System.Drawing.Point(471, 40);
            this.dateInput.Name = "dateInput";
            this.dateInput.Properties.Appearance.Options.UseTextOptions = true;
            this.dateInput.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dateInput.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateInput.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateInput.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.dateInput.Size = new System.Drawing.Size(101, 22);
            this.dateInput.TabIndex = 79;
            this.dateInput.TabStop = false;
            // 
            // lblCustomer
            // 
            this.lblCustomer.Location = new System.Drawing.Point(12, 15);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(53, 16);
            this.lblCustomer.TabIndex = 78;
            this.lblCustomer.Text = "Клиент:";
            // 
            // btnCustomer
            // 
            this.btnCustomer.Location = new System.Drawing.Point(81, 12);
            this.btnCustomer.Name = "btnCustomer";
            this.btnCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnCustomer.Properties.ReadOnly = true;
            this.btnCustomer.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnCustomer.Size = new System.Drawing.Size(491, 22);
            this.btnCustomer.TabIndex = 77;
            // 
            // lblInputDate
            // 
            this.lblInputDate.Location = new System.Drawing.Point(426, 43);
            this.lblInputDate.Name = "lblInputDate";
            this.lblInputDate.Size = new System.Drawing.Size(39, 16);
            this.lblInputDate.TabIndex = 76;
            this.lblInputDate.Text = "Дата:";
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(12, 43);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(104, 16);
            this.lblName.TabIndex = 81;
            this.lblName.Text = "Наименование:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(132, 40);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(278, 22);
            this.txtName.TabIndex = 80;
            // 
            // lblOKTMO
            // 
            this.lblOKTMO.Location = new System.Drawing.Point(62, 71);
            this.lblOKTMO.Name = "lblOKTMO";
            this.lblOKTMO.Size = new System.Drawing.Size(54, 16);
            this.lblOKTMO.TabIndex = 83;
            this.lblOKTMO.Text = "ОКТМО:";
            // 
            // txtOKTMO
            // 
            this.txtOKTMO.Location = new System.Drawing.Point(132, 68);
            this.txtOKTMO.Name = "txtOKTMO";
            this.txtOKTMO.Size = new System.Drawing.Size(150, 22);
            this.txtOKTMO.TabIndex = 82;
            // 
            // lblKPP
            // 
            this.lblKPP.Location = new System.Drawing.Point(374, 71);
            this.lblKPP.Name = "lblKPP";
            this.lblKPP.Size = new System.Drawing.Size(32, 16);
            this.lblKPP.TabIndex = 85;
            this.lblKPP.Text = "КПП:";
            // 
            // txtKPP
            // 
            this.txtKPP.Location = new System.Drawing.Point(422, 68);
            this.txtKPP.Name = "txtKPP";
            this.txtKPP.Size = new System.Drawing.Size(150, 22);
            this.txtKPP.TabIndex = 84;
            // 
            // lblIFNS
            // 
            this.lblIFNS.Location = new System.Drawing.Point(72, 99);
            this.lblIFNS.Name = "lblIFNS";
            this.lblIFNS.Size = new System.Drawing.Size(44, 16);
            this.lblIFNS.TabIndex = 87;
            this.lblIFNS.Text = "ИФНС:";
            // 
            // txtIFNS
            // 
            this.txtIFNS.Location = new System.Drawing.Point(132, 96);
            this.txtIFNS.Name = "txtIFNS";
            this.txtIFNS.Size = new System.Drawing.Size(150, 22);
            this.txtIFNS.TabIndex = 86;
            // 
            // checkIsSeparateDivision
            // 
            this.checkIsSeparateDivision.EditValue = true;
            this.checkIsSeparateDivision.Location = new System.Drawing.Point(297, 97);
            this.checkIsSeparateDivision.Name = "checkIsSeparateDivision";
            this.checkIsSeparateDivision.Properties.Caption = "Обособленное подразделение";
            this.checkIsSeparateDivision.Properties.ReadOnly = true;
            this.checkIsSeparateDivision.Size = new System.Drawing.Size(275, 20);
            this.checkIsSeparateDivision.TabIndex = 88;
            // 
            // SubdivisionEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 155);
            this.Controls.Add(this.checkIsSeparateDivision);
            this.Controls.Add(this.lblIFNS);
            this.Controls.Add(this.txtIFNS);
            this.Controls.Add(this.lblKPP);
            this.Controls.Add(this.txtKPP);
            this.Controls.Add(this.lblOKTMO);
            this.Controls.Add(this.txtOKTMO);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.dateInput);
            this.Controls.Add(this.lblCustomer);
            this.Controls.Add(this.btnCustomer);
            this.Controls.Add(this.lblInputDate);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SubdivisionEdit";
            this.Text = "Подразделение";
            this.Load += new System.EventHandler(this.AccountEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dateInput.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateInput.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOKTMO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKPP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIFNS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsSeparateDivision.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.DateEdit dateInput;
        private DevExpress.XtraEditors.LabelControl lblCustomer;
        private DevExpress.XtraEditors.ButtonEdit btnCustomer;
        private DevExpress.XtraEditors.LabelControl lblInputDate;
        private DevExpress.XtraEditors.LabelControl lblName;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.LabelControl lblOKTMO;
        private DevExpress.XtraEditors.TextEdit txtOKTMO;
        private DevExpress.XtraEditors.LabelControl lblKPP;
        private DevExpress.XtraEditors.TextEdit txtKPP;
        private DevExpress.XtraEditors.LabelControl lblIFNS;
        private DevExpress.XtraEditors.TextEdit txtIFNS;
        private DevExpress.XtraEditors.CheckEdit checkIsSeparateDivision;
    }
}