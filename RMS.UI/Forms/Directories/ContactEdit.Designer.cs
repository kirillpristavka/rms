namespace RMS.UI.Forms.Directories
{
    partial class ContactEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContactEdit));
            this.gpInformation = new DevExpress.XtraEditors.GroupControl();
            this.lblName = new DevExpress.XtraEditors.LabelControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.lblPatronymic = new DevExpress.XtraEditors.LabelControl();
            this.lblAddress = new DevExpress.XtraEditors.LabelControl();
            this.txtPatronymic = new DevExpress.XtraEditors.TextEdit();
            this.lblSurname = new DevExpress.XtraEditors.LabelControl();
            this.txtSurname = new DevExpress.XtraEditors.TextEdit();
            this.txtPosition = new DevExpress.XtraEditors.ButtonEdit();
            this.lblPosition = new DevExpress.XtraEditors.LabelControl();
            this.txtAddress = new DevExpress.XtraEditors.ButtonEdit();
            this.txtTelephone = new DevExpress.XtraEditors.TextEdit();
            this.txtEmail = new DevExpress.XtraEditors.TextEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.panelControlFooter = new DevExpress.XtraEditors.PanelControl();
            this.gpComment = new DevExpress.XtraEditors.GroupControl();
            this.memoComment = new DevExpress.XtraEditors.MemoEdit();
            this.gpTelephone = new DevExpress.XtraEditors.GroupControl();
            this.gpEmail = new DevExpress.XtraEditors.GroupControl();
            this.gpFullName = new DevExpress.XtraEditors.GroupControl();
            this.txtFullName = new DevExpress.XtraEditors.TextEdit();
            this.checkIsDefault = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gpInformation)).BeginInit();
            this.gpInformation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPatronymic.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSurname.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPosition.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelephone.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlFooter)).BeginInit();
            this.panelControlFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gpComment)).BeginInit();
            this.gpComment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpTelephone)).BeginInit();
            this.gpTelephone.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gpEmail)).BeginInit();
            this.gpEmail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gpFullName)).BeginInit();
            this.gpFullName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFullName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsDefault.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gpInformation
            // 
            this.gpInformation.AutoSize = true;
            this.gpInformation.Controls.Add(this.lblName);
            this.gpInformation.Controls.Add(this.txtName);
            this.gpInformation.Controls.Add(this.lblPatronymic);
            this.gpInformation.Controls.Add(this.lblAddress);
            this.gpInformation.Controls.Add(this.txtPatronymic);
            this.gpInformation.Controls.Add(this.lblSurname);
            this.gpInformation.Controls.Add(this.txtSurname);
            this.gpInformation.Controls.Add(this.txtPosition);
            this.gpInformation.Controls.Add(this.lblPosition);
            this.gpInformation.Controls.Add(this.txtAddress);
            this.gpInformation.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpInformation.GroupStyle = DevExpress.Utils.GroupStyle.Title;
            this.gpInformation.Location = new System.Drawing.Point(0, 42);
            this.gpInformation.Name = "gpInformation";
            this.gpInformation.Size = new System.Drawing.Size(584, 182);
            this.gpInformation.TabIndex = 1;
            this.gpInformation.Text = "Информация о контакте";
            this.gpInformation.Visible = false;
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(66, 56);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(32, 16);
            this.lblName.TabIndex = 4;
            this.lblName.Text = "Имя:";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(120, 53);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(454, 22);
            this.txtName.TabIndex = 5;
            // 
            // lblPatronymic
            // 
            this.lblPatronymic.Location = new System.Drawing.Point(29, 84);
            this.lblPatronymic.Name = "lblPatronymic";
            this.lblPatronymic.Size = new System.Drawing.Size(70, 16);
            this.lblPatronymic.TabIndex = 6;
            this.lblPatronymic.Text = "Отчество:";
            // 
            // lblAddress
            // 
            this.lblAddress.Location = new System.Drawing.Point(52, 140);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(47, 16);
            this.lblAddress.TabIndex = 10;
            this.lblAddress.Text = "Адрес:";
            this.lblAddress.Visible = false;
            // 
            // txtPatronymic
            // 
            this.txtPatronymic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPatronymic.Location = new System.Drawing.Point(120, 81);
            this.txtPatronymic.Name = "txtPatronymic";
            this.txtPatronymic.Size = new System.Drawing.Size(454, 22);
            this.txtPatronymic.TabIndex = 7;
            // 
            // lblSurname
            // 
            this.lblSurname.Location = new System.Drawing.Point(33, 28);
            this.lblSurname.Name = "lblSurname";
            this.lblSurname.Size = new System.Drawing.Size(66, 16);
            this.lblSurname.TabIndex = 2;
            this.lblSurname.Text = "Фамилия:";
            // 
            // txtSurname
            // 
            this.txtSurname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSurname.Location = new System.Drawing.Point(120, 25);
            this.txtSurname.Name = "txtSurname";
            this.txtSurname.Size = new System.Drawing.Size(454, 22);
            this.txtSurname.TabIndex = 3;
            // 
            // txtPosition
            // 
            this.txtPosition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPosition.Location = new System.Drawing.Point(120, 109);
            this.txtPosition.Name = "txtPosition";
            this.txtPosition.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtPosition.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.txtPosition.Size = new System.Drawing.Size(454, 22);
            this.txtPosition.TabIndex = 9;
            this.txtPosition.Visible = false;
            this.txtPosition.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtPosition_ButtonClick);
            // 
            // lblPosition
            // 
            this.lblPosition.Location = new System.Drawing.Point(18, 112);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(81, 16);
            this.lblPosition.TabIndex = 8;
            this.lblPosition.Text = "Должность:";
            this.lblPosition.Visible = false;
            // 
            // txtAddress
            // 
            this.txtAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAddress.Location = new System.Drawing.Point(120, 137);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtAddress.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.txtAddress.Size = new System.Drawing.Size(454, 22);
            this.txtAddress.TabIndex = 11;
            this.txtAddress.Visible = false;
            this.txtAddress.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtAddress_ButtonClick);
            // 
            // txtTelephone
            // 
            this.txtTelephone.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtTelephone.Location = new System.Drawing.Point(0, 20);
            this.txtTelephone.Name = "txtTelephone";
            this.txtTelephone.Properties.Appearance.Options.UseTextOptions = true;
            this.txtTelephone.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTelephone.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtTelephone.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTelephone.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtTelephone.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTelephone.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtTelephone.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTelephone.Properties.Mask.EditMask = "\\+\\7-(\\d?\\d?\\d?)-\\d\\d\\d-\\d\\d\\d\\d";
            this.txtTelephone.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Regular;
            this.txtTelephone.Size = new System.Drawing.Size(584, 22);
            this.txtTelephone.TabIndex = 13;
            // 
            // txtEmail
            // 
            this.txtEmail.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtEmail.Location = new System.Drawing.Point(0, 20);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Properties.Appearance.Options.UseTextOptions = true;
            this.txtEmail.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtEmail.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtEmail.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtEmail.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtEmail.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtEmail.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtEmail.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtEmail.Properties.Mask.EditMask = resources.GetString("txtEmail.Properties.Mask.EditMask");
            this.txtEmail.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtEmail.Size = new System.Drawing.Size(584, 22);
            this.txtEmail.TabIndex = 15;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(375, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 25);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(481, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 25);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panelControlFooter
            // 
            this.panelControlFooter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControlFooter.Controls.Add(this.checkIsDefault);
            this.panelControlFooter.Controls.Add(this.btnCancel);
            this.panelControlFooter.Controls.Add(this.btnSave);
            this.panelControlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControlFooter.Location = new System.Drawing.Point(0, 396);
            this.panelControlFooter.Name = "panelControlFooter";
            this.panelControlFooter.Size = new System.Drawing.Size(584, 35);
            this.panelControlFooter.TabIndex = 18;
            // 
            // gpComment
            // 
            this.gpComment.AppearanceCaption.Options.UseTextOptions = true;
            this.gpComment.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gpComment.Controls.Add(this.memoComment);
            this.gpComment.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpComment.GroupStyle = DevExpress.Utils.GroupStyle.Title;
            this.gpComment.Location = new System.Drawing.Point(0, 308);
            this.gpComment.Name = "gpComment";
            this.gpComment.Size = new System.Drawing.Size(584, 62);
            this.gpComment.TabIndex = 16;
            this.gpComment.Text = "Комментарий";
            // 
            // memoComment
            // 
            this.memoComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoComment.EditValue = "";
            this.memoComment.Location = new System.Drawing.Point(0, 20);
            this.memoComment.Name = "memoComment";
            this.memoComment.Size = new System.Drawing.Size(584, 42);
            this.memoComment.TabIndex = 17;
            // 
            // gpTelephone
            // 
            this.gpTelephone.AppearanceCaption.Options.UseTextOptions = true;
            this.gpTelephone.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gpTelephone.AutoSize = true;
            this.gpTelephone.Controls.Add(this.txtTelephone);
            this.gpTelephone.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpTelephone.GroupStyle = DevExpress.Utils.GroupStyle.Title;
            this.gpTelephone.Location = new System.Drawing.Point(0, 224);
            this.gpTelephone.Name = "gpTelephone";
            this.gpTelephone.Size = new System.Drawing.Size(584, 42);
            this.gpTelephone.TabIndex = 12;
            this.gpTelephone.Text = "Телефон";
            this.gpTelephone.Visible = false;
            // 
            // gpEmail
            // 
            this.gpEmail.AppearanceCaption.Options.UseTextOptions = true;
            this.gpEmail.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gpEmail.AutoSize = true;
            this.gpEmail.Controls.Add(this.txtEmail);
            this.gpEmail.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpEmail.GroupStyle = DevExpress.Utils.GroupStyle.Title;
            this.gpEmail.Location = new System.Drawing.Point(0, 266);
            this.gpEmail.Name = "gpEmail";
            this.gpEmail.Size = new System.Drawing.Size(584, 42);
            this.gpEmail.TabIndex = 14;
            this.gpEmail.Text = "E-mail";
            this.gpEmail.Visible = false;
            // 
            // gpFullName
            // 
            this.gpFullName.AppearanceCaption.Options.UseTextOptions = true;
            this.gpFullName.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gpFullName.AutoSize = true;
            this.gpFullName.Controls.Add(this.txtFullName);
            this.gpFullName.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpFullName.GroupStyle = DevExpress.Utils.GroupStyle.Title;
            this.gpFullName.Location = new System.Drawing.Point(0, 0);
            this.gpFullName.Name = "gpFullName";
            this.gpFullName.Size = new System.Drawing.Size(584, 42);
            this.gpFullName.TabIndex = 1;
            this.gpFullName.Text = "ФИО";
            // 
            // txtFullName
            // 
            this.txtFullName.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtFullName.Location = new System.Drawing.Point(0, 20);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Properties.Appearance.Options.UseTextOptions = true;
            this.txtFullName.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtFullName.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtFullName.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtFullName.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtFullName.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtFullName.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtFullName.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtFullName.Size = new System.Drawing.Size(584, 22);
            this.txtFullName.TabIndex = 1;
            // 
            // checkIsDefault
            // 
            this.checkIsDefault.Location = new System.Drawing.Point(18, 8);
            this.checkIsDefault.Name = "checkIsDefault";
            this.checkIsDefault.Properties.Caption = "Использовать по умолчанию";
            this.checkIsDefault.Size = new System.Drawing.Size(351, 20);
            this.checkIsDefault.TabIndex = 21;
            // 
            // ContactEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(584, 431);
            this.Controls.Add(this.gpComment);
            this.Controls.Add(this.gpEmail);
            this.Controls.Add(this.gpTelephone);
            this.Controls.Add(this.panelControlFooter);
            this.Controls.Add(this.gpInformation);
            this.Controls.Add(this.gpFullName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MinimumSize = new System.Drawing.Size(600, 40);
            this.Name = "ContactEdit";
            this.Text = "Контакт";
            this.Load += new System.EventHandler(this.ContactEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gpInformation)).EndInit();
            this.gpInformation.ResumeLayout(false);
            this.gpInformation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPatronymic.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSurname.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPosition.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelephone.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlFooter)).EndInit();
            this.panelControlFooter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gpComment)).EndInit();
            this.gpComment.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpTelephone)).EndInit();
            this.gpTelephone.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gpEmail)).EndInit();
            this.gpEmail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gpFullName)).EndInit();
            this.gpFullName.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtFullName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsDefault.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl gpInformation;
        private DevExpress.XtraEditors.LabelControl lblName;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.LabelControl lblPatronymic;
        private DevExpress.XtraEditors.TextEdit txtPatronymic;
        private DevExpress.XtraEditors.LabelControl lblSurname;
        private DevExpress.XtraEditors.TextEdit txtSurname;
        private DevExpress.XtraEditors.LabelControl lblPosition;
        private DevExpress.XtraEditors.ButtonEdit txtPosition;
        private DevExpress.XtraEditors.TextEdit txtTelephone;
        private DevExpress.XtraEditors.TextEdit txtEmail;
        private DevExpress.XtraEditors.LabelControl lblAddress;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.ButtonEdit txtAddress;
        private DevExpress.XtraEditors.PanelControl panelControlFooter;
        private DevExpress.XtraEditors.GroupControl gpComment;
        private DevExpress.XtraEditors.MemoEdit memoComment;
        private DevExpress.XtraEditors.GroupControl gpTelephone;
        private DevExpress.XtraEditors.GroupControl gpEmail;
        private DevExpress.XtraEditors.GroupControl gpFullName;
        private DevExpress.XtraEditors.TextEdit txtFullName;
        private DevExpress.XtraEditors.CheckEdit checkIsDefault;
    }
}