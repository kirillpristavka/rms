namespace RMS.UI
{
    partial class formEdit_BaseSprEnumeration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formEdit_BaseSprEnumeration));
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.groupControlPeriod = new DevExpress.XtraEditors.GroupControl();
            this.dateEditDateEnd = new DevExpress.XtraEditors.DateEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.dateEditDateBegin = new DevExpress.XtraEditors.DateEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.memoSoder = new DevExpress.XtraEditors.MemoEdit();
            this.lblSoder = new DevExpress.XtraEditors.LabelControl();
            this.lblCode = new DevExpress.XtraEditors.LabelControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.chkDefault = new DevExpress.XtraEditors.CheckEdit();
            this.imgImageIndex = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.lblImageIndex = new DevExpress.XtraEditors.LabelControl();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.lblName = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlPeriod)).BeginInit();
            this.groupControlPeriod.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditDateEnd.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditDateEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditDateBegin.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditDateBegin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoSoder.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDefault.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgImageIndex.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(267, 230);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Отмена";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(348, 230);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupControlPeriod
            // 
            this.groupControlPeriod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControlPeriod.Controls.Add(this.dateEditDateEnd);
            this.groupControlPeriod.Controls.Add(this.labelControl6);
            this.groupControlPeriod.Controls.Add(this.dateEditDateBegin);
            this.groupControlPeriod.Controls.Add(this.labelControl3);
            this.groupControlPeriod.Location = new System.Drawing.Point(143, 144);
            this.groupControlPeriod.Name = "groupControlPeriod";
            this.groupControlPeriod.Size = new System.Drawing.Size(280, 72);
            this.groupControlPeriod.TabIndex = 46;
            this.groupControlPeriod.Text = "Период действия";
            // 
            // dateEditDateEnd
            // 
            this.dateEditDateEnd.EditValue = null;
            this.dateEditDateEnd.Location = new System.Drawing.Point(138, 47);
            this.dateEditDateEnd.Name = "dateEditDateEnd";
            this.dateEditDateEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditDateEnd.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditDateEnd.Size = new System.Drawing.Size(100, 20);
            this.dateEditDateEnd.TabIndex = 4;
            this.dateEditDateEnd.TabStop = false;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(39, 50);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(87, 13);
            this.labelControl6.TabIndex = 49;
            this.labelControl6.Text = "Дата окончания:";
            // 
            // dateEditDateBegin
            // 
            this.dateEditDateBegin.EditValue = null;
            this.dateEditDateBegin.Location = new System.Drawing.Point(138, 24);
            this.dateEditDateBegin.Name = "dateEditDateBegin";
            this.dateEditDateBegin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditDateBegin.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditDateBegin.Size = new System.Drawing.Size(100, 20);
            this.dateEditDateBegin.TabIndex = 3;
            this.dateEditDateBegin.TabStop = false;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(39, 27);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(69, 13);
            this.labelControl3.TabIndex = 47;
            this.labelControl3.Text = "Дата начала:";
            // 
            // memoSoder
            // 
            this.memoSoder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.memoSoder.Location = new System.Drawing.Point(143, 94);
            this.memoSoder.Name = "memoSoder";
            this.memoSoder.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.memoSoder.Size = new System.Drawing.Size(280, 44);
            this.memoSoder.TabIndex = 2;
            // 
            // lblSoder
            // 
            this.lblSoder.Location = new System.Drawing.Point(12, 96);
            this.lblSoder.Name = "lblSoder";
            this.lblSoder.Size = new System.Drawing.Size(68, 13);
            this.lblSoder.TabIndex = 34;
            this.lblSoder.Text = "Содержание:";
            // 
            // lblCode
            // 
            this.lblCode.Location = new System.Drawing.Point(12, 45);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(24, 13);
            this.lblCode.TabIndex = 33;
            this.lblCode.Text = "Код:";
            this.lblCode.Visible = false;
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(143, 68);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(280, 20);
            this.txtName.TabIndex = 1;
            // 
            // chkDefault
            // 
            this.chkDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkDefault.Location = new System.Drawing.Point(248, 6);
            this.chkDefault.Name = "chkDefault";
            this.chkDefault.Properties.Caption = "Использовать по Умолчанию:";
            this.chkDefault.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.chkDefault.Size = new System.Drawing.Size(175, 19);
            this.chkDefault.TabIndex = 47;
            this.chkDefault.TabStop = false;
            this.chkDefault.Visible = false;
            // 
            // imgImageIndex
            // 
            this.imgImageIndex.Location = new System.Drawing.Point(11, 6);
            this.imgImageIndex.Name = "imgImageIndex";
            this.imgImageIndex.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.imgImageIndex.Properties.DropDownRows = 20;
            this.imgImageIndex.Size = new System.Drawing.Size(50, 20);
            this.imgImageIndex.TabIndex = 87;
            this.imgImageIndex.TabStop = false;
            this.imgImageIndex.Visible = false;
            // 
            // lblImageIndex
            // 
            this.lblImageIndex.Location = new System.Drawing.Point(67, 9);
            this.lblImageIndex.Name = "lblImageIndex";
            this.lblImageIndex.Size = new System.Drawing.Size(49, 13);
            this.lblImageIndex.TabIndex = 88;
            this.lblImageIndex.Text = "Картинка";
            this.lblImageIndex.Visible = false;
            // 
            // txtCode
            // 
            this.txtCode.EditValue = "";
            this.txtCode.Location = new System.Drawing.Point(143, 42);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(103, 20);
            this.txtCode.TabIndex = 89;
            this.txtCode.Visible = false;
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(12, 71);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(77, 13);
            this.lblName.TabIndex = 90;
            this.lblName.Text = "Наименование:";
            this.lblName.Visible = false;
            // 
            // formEdit_BaseSprEnumeration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(425, 254);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.lblImageIndex);
            this.Controls.Add(this.imgImageIndex);
            this.Controls.Add(this.chkDefault);
            this.Controls.Add(this.groupControlPeriod);
            this.Controls.Add(this.memoSoder);
            this.Controls.Add(this.lblSoder);
            this.Controls.Add(this.lblCode);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "formEdit_BaseSprEnumeration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = " Добавление / Изменение";
            this.Load += new System.EventHandler(this.formEdit_BaseSprEnumeration_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlPeriod)).EndInit();
            this.groupControlPeriod.ResumeLayout(false);
            this.groupControlPeriod.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditDateEnd.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditDateEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditDateBegin.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditDateBegin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoSoder.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDefault.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgImageIndex.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.GroupControl groupControlPeriod;
        private DevExpress.XtraEditors.MemoEdit memoSoder;
        private DevExpress.XtraEditors.LabelControl lblSoder;
        private DevExpress.XtraEditors.LabelControl lblCode;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.DateEdit dateEditDateEnd;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.DateEdit dateEditDateBegin;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.CheckEdit chkDefault;
        private DevExpress.XtraEditors.ImageComboBoxEdit imgImageIndex;
        private DevExpress.XtraEditors.LabelControl lblImageIndex;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.LabelControl lblName;
    }
}