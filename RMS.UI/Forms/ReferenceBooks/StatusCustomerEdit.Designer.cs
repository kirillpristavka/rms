namespace RMS.UI.Forms.ReferenceBooks
{
    partial class StatusCustomerEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatusCustomerEdit));
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.lblDate = new DevExpress.XtraEditors.LabelControl();
            this.lblCustomer = new DevExpress.XtraEditors.LabelControl();
            this.btnCustomer = new DevExpress.XtraEditors.ButtonEdit();
            this.dateDate = new DevExpress.XtraEditors.DateEdit();
            this.gpStatus = new DevExpress.XtraEditors.GroupControl();
            this.btnStatus = new DevExpress.XtraEditors.ButtonEdit();
            this.btnChronicle = new DevExpress.XtraEditors.SimpleButton();
            this.memoComment = new DevExpress.XtraEditors.MemoEdit();
            this.gpComment = new DevExpress.XtraEditors.GroupControl();
            ((System.ComponentModel.ISupportInitialize)(this.btnCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpStatus)).BeginInit();
            this.gpStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpComment)).BeginInit();
            this.gpComment.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(376, 210);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(482, 210);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblDate
            // 
            this.lblDate.Location = new System.Drawing.Point(40, 47);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(39, 16);
            this.lblDate.TabIndex = 3;
            this.lblDate.Text = "Дата:";
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
            this.dateDate.Location = new System.Drawing.Point(91, 44);
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
            // gpStatus
            // 
            this.gpStatus.Controls.Add(this.btnStatus);
            this.gpStatus.Location = new System.Drawing.Point(13, 72);
            this.gpStatus.Name = "gpStatus";
            this.gpStatus.Size = new System.Drawing.Size(569, 64);
            this.gpStatus.TabIndex = 11;
            this.gpStatus.Text = "Статус";
            // 
            // btnStatus
            // 
            this.btnStatus.Location = new System.Drawing.Point(14, 24);
            this.btnStatus.Name = "btnStatus";
            this.btnStatus.Properties.Appearance.BackColor2 = System.Drawing.Color.MintCream;
            this.btnStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnStatus.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnStatus.Size = new System.Drawing.Size(546, 22);
            this.btnStatus.TabIndex = 13;
            this.btnStatus.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnStatus_ButtonPressed);
            this.btnStatus.EditValueChanged += new System.EventHandler(this.btnStatus_EditValueChanged);
            // 
            // btnChronicle
            // 
            this.btnChronicle.Location = new System.Drawing.Point(212, 43);
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
            this.gpComment.Location = new System.Drawing.Point(13, 142);
            this.gpComment.Name = "gpComment";
            this.gpComment.Size = new System.Drawing.Size(569, 62);
            this.gpComment.TabIndex = 17;
            this.gpComment.Text = "Комментарий";
            // 
            // StatusCustomerEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 241);
            this.Controls.Add(this.gpComment);
            this.Controls.Add(this.btnChronicle);
            this.Controls.Add(this.gpStatus);
            this.Controls.Add(this.dateDate);
            this.Controls.Add(this.lblCustomer);
            this.Controls.Add(this.btnCustomer);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "StatusCustomerEdit";
            this.Text = "Статус клиента";
            this.Load += new System.EventHandler(this.StatusCustomerEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpStatus)).EndInit();
            this.gpStatus.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpComment)).EndInit();
            this.gpComment.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LabelControl lblDate;
        private DevExpress.XtraEditors.LabelControl lblCustomer;
        private DevExpress.XtraEditors.ButtonEdit btnCustomer;
        private DevExpress.XtraEditors.DateEdit dateDate;
        private DevExpress.XtraEditors.GroupControl gpStatus;
        private DevExpress.XtraEditors.ButtonEdit btnStatus;
        private DevExpress.XtraEditors.SimpleButton btnChronicle;
        private DevExpress.XtraEditors.MemoEdit memoComment;
        private DevExpress.XtraEditors.GroupControl gpComment;
    }
}