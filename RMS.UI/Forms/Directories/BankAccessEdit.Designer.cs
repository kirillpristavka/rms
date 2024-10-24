namespace RMS.UI.Forms.Directories
{
    partial class BankAccessEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BankAccessEdit));
            this.lblBank = new DevExpress.XtraEditors.LabelControl();
            this.lblLogin = new DevExpress.XtraEditors.LabelControl();
            this.txtLogin = new DevExpress.XtraEditors.TextEdit();
            this.lblPassword = new DevExpress.XtraEditors.LabelControl();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.lblLink = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnBank = new DevExpress.XtraEditors.ButtonEdit();
            this.lblComment = new DevExpress.XtraEditors.LabelControl();
            this.memoComment = new DevExpress.XtraEditors.MemoEdit();
            this.txtLink = new DevExpress.XtraEditors.ButtonEdit();
            this.txtDescription = new DevExpress.XtraEditors.TextEdit();
            this.lblDescription = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.txtLogin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnBank.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLink.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblBank
            // 
            this.lblBank.Location = new System.Drawing.Point(84, 47);
            this.lblBank.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lblBank.Name = "lblBank";
            this.lblBank.Size = new System.Drawing.Size(32, 16);
            this.lblBank.TabIndex = 1;
            this.lblBank.Text = "Банк";
            // 
            // lblLogin
            // 
            this.lblLogin.Location = new System.Drawing.Point(76, 75);
            this.lblLogin.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(40, 16);
            this.lblLogin.TabIndex = 3;
            this.lblLogin.Text = "Логин";
            // 
            // txtLogin
            // 
            this.txtLogin.Location = new System.Drawing.Point(134, 72);
            this.txtLogin.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(399, 22);
            this.txtLogin.TabIndex = 2;
            // 
            // lblPassword
            // 
            this.lblPassword.Location = new System.Drawing.Point(68, 103);
            this.lblPassword.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(48, 16);
            this.lblPassword.TabIndex = 5;
            this.lblPassword.Text = "Пароль";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(134, 100);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(399, 22);
            this.txtPassword.TabIndex = 4;
            // 
            // lblLink
            // 
            this.lblLink.Location = new System.Drawing.Point(66, 131);
            this.lblLink.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lblLink.Name = "lblLink";
            this.lblLink.Size = new System.Drawing.Size(50, 16);
            this.lblLink.TabIndex = 7;
            this.lblLink.Text = "Ссылка";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(353, 213);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 23);
            this.btnSave.TabIndex = 28;
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(446, 213);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 23);
            this.btnCancel.TabIndex = 27;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnBank
            // 
            this.btnBank.Location = new System.Drawing.Point(134, 44);
            this.btnBank.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnBank.Name = "btnBank";
            this.btnBank.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.btnBank.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnBank.Size = new System.Drawing.Size(399, 22);
            this.btnBank.TabIndex = 0;
            this.btnBank.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtBank_ButtonPressed);
            // 
            // lblComment
            // 
            this.lblComment.Location = new System.Drawing.Point(21, 158);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(95, 16);
            this.lblComment.TabIndex = 85;
            this.lblComment.Text = "Комментарий:";
            // 
            // memoComment
            // 
            this.memoComment.EditValue = "";
            this.memoComment.Location = new System.Drawing.Point(134, 157);
            this.memoComment.Name = "memoComment";
            this.memoComment.Size = new System.Drawing.Size(399, 50);
            this.memoComment.TabIndex = 86;
            // 
            // txtLink
            // 
            this.txtLink.Location = new System.Drawing.Point(134, 128);
            this.txtLink.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtLink.Name = "txtLink";
            this.txtLink.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Right)});
            this.txtLink.Size = new System.Drawing.Size(399, 22);
            this.txtLink.TabIndex = 6;
            this.txtLink.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtLink_ButtonPressed);
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(134, 16);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(399, 22);
            this.txtDescription.TabIndex = 88;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(36, 19);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(80, 16);
            this.lblDescription.TabIndex = 87;
            this.lblDescription.Text = "Описание:";
            // 
            // BankAccessEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 243);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblComment);
            this.Controls.Add(this.memoComment);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblLink);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblLogin);
            this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.lblBank);
            this.Controls.Add(this.btnBank);
            this.Controls.Add(this.txtLink);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximizeBox = false;
            this.Name = "BankAccessEdit";
            this.Text = "Доступ к банку";
            this.Load += new System.EventHandler(this.BankEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtLogin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnBank.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLink.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.LabelControl lblBank;
        private DevExpress.XtraEditors.LabelControl lblLogin;
        private DevExpress.XtraEditors.TextEdit txtLogin;
        private DevExpress.XtraEditors.LabelControl lblPassword;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraEditors.LabelControl lblLink;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.ButtonEdit btnBank;
        private DevExpress.XtraEditors.LabelControl lblComment;
        private DevExpress.XtraEditors.MemoEdit memoComment;
        private DevExpress.XtraEditors.ButtonEdit txtLink;
        private DevExpress.XtraEditors.TextEdit txtDescription;
        private System.Windows.Forms.Label lblDescription;
    }
}