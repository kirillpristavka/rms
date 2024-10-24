namespace RMS.UI.Forms.Directories
{
    partial class StatisticalReportEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatisticalReportEdit));
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.lblReport = new DevExpress.XtraEditors.LabelControl();
            this.btnReport = new DevExpress.XtraEditors.ButtonEdit();
            this.lblResponsible = new DevExpress.XtraEditors.LabelControl();
            this.btnResponsible = new DevExpress.XtraEditors.ButtonEdit();
            this.lblCustomer = new DevExpress.XtraEditors.LabelControl();
            this.lblOKUD = new DevExpress.XtraEditors.LabelControl();
            this.txtOKUD = new DevExpress.XtraEditors.TextEdit();
            this.lblComment = new DevExpress.XtraEditors.LabelControl();
            this.lblDeadline = new DevExpress.XtraEditors.LabelControl();
            this.lblPeriodicity = new DevExpress.XtraEditors.LabelControl();
            this.lblName = new DevExpress.XtraEditors.LabelControl();
            this.lblFormIndex = new DevExpress.XtraEditors.LabelControl();
            this.txtFormIndex = new DevExpress.XtraEditors.TextEdit();
            this.memoName = new DevExpress.XtraEditors.MemoEdit();
            this.memoComment = new DevExpress.XtraEditors.MemoEdit();
            this.memoDeadline = new DevExpress.XtraEditors.MemoEdit();
            this.txtPeriodicity = new DevExpress.XtraEditors.TextEdit();
            this.btnCustomer = new DevExpress.XtraEditors.ButtonEdit();
            this.lblYear = new DevExpress.XtraEditors.LabelControl();
            this.txtYear = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.btnReport.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnResponsible.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOKUD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFormIndex.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoDeadline.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPeriodicity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYear.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(416, 320);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 23);
            this.btnSave.TabIndex = 59;
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(522, 320);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 23);
            this.btnCancel.TabIndex = 58;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblReport
            // 
            this.lblReport.Location = new System.Drawing.Point(206, 43);
            this.lblReport.Name = "lblReport";
            this.lblReport.Size = new System.Drawing.Size(46, 16);
            this.lblReport.TabIndex = 66;
            this.lblReport.Text = "Отчет:";
            // 
            // btnReport
            // 
            this.btnReport.Location = new System.Drawing.Point(272, 40);
            this.btnReport.Name = "btnReport";
            this.btnReport.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.btnReport.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnReport.Size = new System.Drawing.Size(350, 22);
            this.btnReport.TabIndex = 65;
            this.btnReport.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnReport_ButtonPressed);
            // 
            // lblResponsible
            // 
            this.lblResponsible.Location = new System.Drawing.Point(12, 71);
            this.lblResponsible.Name = "lblResponsible";
            this.lblResponsible.Size = new System.Drawing.Size(110, 16);
            this.lblResponsible.TabIndex = 72;
            this.lblResponsible.Text = "Ответственный:";
            // 
            // btnResponsible
            // 
            this.btnResponsible.Location = new System.Drawing.Point(140, 68);
            this.btnResponsible.Name = "btnResponsible";
            this.btnResponsible.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.btnResponsible.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnResponsible.Size = new System.Drawing.Size(482, 22);
            this.btnResponsible.TabIndex = 71;
            this.btnResponsible.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnResponsible_ButtonPressed);
            // 
            // lblCustomer
            // 
            this.lblCustomer.Location = new System.Drawing.Point(69, 15);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(53, 16);
            this.lblCustomer.TabIndex = 74;
            this.lblCustomer.Text = "Клиент:";
            // 
            // lblOKUD
            // 
            this.lblOKUD.Location = new System.Drawing.Point(307, 99);
            this.lblOKUD.Name = "lblOKUD";
            this.lblOKUD.Size = new System.Drawing.Size(43, 16);
            this.lblOKUD.TabIndex = 85;
            this.lblOKUD.Text = "ОКУД:";
            // 
            // txtOKUD
            // 
            this.txtOKUD.Location = new System.Drawing.Point(366, 96);
            this.txtOKUD.Name = "txtOKUD";
            this.txtOKUD.Properties.ReadOnly = true;
            this.txtOKUD.Size = new System.Drawing.Size(150, 22);
            this.txtOKUD.TabIndex = 86;
            // 
            // lblComment
            // 
            this.lblComment.Location = new System.Drawing.Point(27, 266);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(95, 16);
            this.lblComment.TabIndex = 83;
            this.lblComment.Text = "Комментарий:";
            // 
            // lblDeadline
            // 
            this.lblDeadline.Location = new System.Drawing.Point(38, 210);
            this.lblDeadline.Name = "lblDeadline";
            this.lblDeadline.Size = new System.Drawing.Size(84, 16);
            this.lblDeadline.TabIndex = 81;
            this.lblDeadline.Text = "Срок сдачи:";
            // 
            // lblPeriodicity
            // 
            this.lblPeriodicity.Location = new System.Drawing.Point(13, 183);
            this.lblPeriodicity.Name = "lblPeriodicity";
            this.lblPeriodicity.Size = new System.Drawing.Size(109, 16);
            this.lblPeriodicity.TabIndex = 79;
            this.lblPeriodicity.Text = "Периодичность:";
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(18, 126);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(104, 16);
            this.lblName.TabIndex = 77;
            this.lblName.Text = "Наименование:";
            // 
            // lblFormIndex
            // 
            this.lblFormIndex.Location = new System.Drawing.Point(17, 99);
            this.lblFormIndex.Name = "lblFormIndex";
            this.lblFormIndex.Size = new System.Drawing.Size(105, 16);
            this.lblFormIndex.TabIndex = 75;
            this.lblFormIndex.Text = "Индекс формы:";
            // 
            // txtFormIndex
            // 
            this.txtFormIndex.Location = new System.Drawing.Point(140, 96);
            this.txtFormIndex.Name = "txtFormIndex";
            this.txtFormIndex.Properties.ReadOnly = true;
            this.txtFormIndex.Size = new System.Drawing.Size(150, 22);
            this.txtFormIndex.TabIndex = 76;
            // 
            // memoName
            // 
            this.memoName.EditValue = "";
            this.memoName.Location = new System.Drawing.Point(140, 124);
            this.memoName.Name = "memoName";
            this.memoName.Properties.ReadOnly = true;
            this.memoName.Size = new System.Drawing.Size(482, 50);
            this.memoName.TabIndex = 78;
            // 
            // memoComment
            // 
            this.memoComment.EditValue = "";
            this.memoComment.Location = new System.Drawing.Point(140, 264);
            this.memoComment.Name = "memoComment";
            this.memoComment.Properties.ReadOnly = true;
            this.memoComment.Size = new System.Drawing.Size(482, 50);
            this.memoComment.TabIndex = 84;
            // 
            // memoDeadline
            // 
            this.memoDeadline.Location = new System.Drawing.Point(140, 208);
            this.memoDeadline.Name = "memoDeadline";
            this.memoDeadline.Properties.ReadOnly = true;
            this.memoDeadline.Size = new System.Drawing.Size(482, 50);
            this.memoDeadline.TabIndex = 82;
            // 
            // txtPeriodicity
            // 
            this.txtPeriodicity.Location = new System.Drawing.Point(140, 180);
            this.txtPeriodicity.Name = "txtPeriodicity";
            this.txtPeriodicity.Properties.ReadOnly = true;
            this.txtPeriodicity.Size = new System.Drawing.Size(150, 22);
            this.txtPeriodicity.TabIndex = 80;
            // 
            // btnCustomer
            // 
            this.btnCustomer.Location = new System.Drawing.Point(140, 12);
            this.btnCustomer.Name = "btnCustomer";
            this.btnCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnCustomer.Properties.ReadOnly = true;
            this.btnCustomer.Size = new System.Drawing.Size(482, 22);
            this.btnCustomer.TabIndex = 73;
            this.btnCustomer.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnCustomer_ButtonPressed);
            // 
            // lblYear
            // 
            this.lblYear.Location = new System.Drawing.Point(93, 43);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(29, 16);
            this.lblYear.TabIndex = 87;
            this.lblYear.Text = "Год:";
            // 
            // txtYear
            // 
            this.txtYear.EditValue = "";
            this.txtYear.Location = new System.Drawing.Point(140, 40);
            this.txtYear.Name = "txtYear";
            this.txtYear.Properties.Appearance.Options.UseTextOptions = true;
            this.txtYear.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtYear.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtYear.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtYear.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtYear.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtYear.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtYear.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtYear.Properties.Mask.EditMask = "####";
            this.txtYear.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtYear.Size = new System.Drawing.Size(50, 22);
            this.txtYear.TabIndex = 88;
            // 
            // StatisticalReportEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 350);
            this.Controls.Add(this.txtYear);
            this.Controls.Add(this.lblYear);
            this.Controls.Add(this.lblOKUD);
            this.Controls.Add(this.txtOKUD);
            this.Controls.Add(this.lblComment);
            this.Controls.Add(this.lblDeadline);
            this.Controls.Add(this.lblPeriodicity);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblFormIndex);
            this.Controls.Add(this.txtFormIndex);
            this.Controls.Add(this.memoName);
            this.Controls.Add(this.memoComment);
            this.Controls.Add(this.memoDeadline);
            this.Controls.Add(this.lblCustomer);
            this.Controls.Add(this.lblResponsible);
            this.Controls.Add(this.btnResponsible);
            this.Controls.Add(this.lblReport);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.txtPeriodicity);
            this.Controls.Add(this.btnCustomer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "StatisticalReportEdit";
            this.Text = "Отчет клиента";
            this.Load += new System.EventHandler(this.CustomerReportEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnReport.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnResponsible.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOKUD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFormIndex.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoDeadline.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPeriodicity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYear.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LabelControl lblReport;
        private DevExpress.XtraEditors.ButtonEdit btnReport;
        private DevExpress.XtraEditors.LabelControl lblResponsible;
        private DevExpress.XtraEditors.ButtonEdit btnResponsible;
        private DevExpress.XtraEditors.LabelControl lblCustomer;
        private DevExpress.XtraEditors.LabelControl lblOKUD;
        private DevExpress.XtraEditors.TextEdit txtOKUD;
        private DevExpress.XtraEditors.LabelControl lblComment;
        private DevExpress.XtraEditors.LabelControl lblDeadline;
        private DevExpress.XtraEditors.LabelControl lblPeriodicity;
        private DevExpress.XtraEditors.LabelControl lblName;
        private DevExpress.XtraEditors.LabelControl lblFormIndex;
        private DevExpress.XtraEditors.TextEdit txtFormIndex;
        private DevExpress.XtraEditors.MemoEdit memoName;
        private DevExpress.XtraEditors.MemoEdit memoComment;
        private DevExpress.XtraEditors.MemoEdit memoDeadline;
        private DevExpress.XtraEditors.TextEdit txtPeriodicity;
        private DevExpress.XtraEditors.ButtonEdit btnCustomer;
        private DevExpress.XtraEditors.LabelControl lblYear;
        private DevExpress.XtraEditors.TextEdit txtYear;
    }
}