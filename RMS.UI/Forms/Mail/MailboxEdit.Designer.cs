namespace RMS.UI.Forms.Mail
{
    partial class MailboxEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MailboxEdit));
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.panelFooter = new DevExpress.XtraEditors.PanelControl();
            this.btnMailboxTest = new DevExpress.XtraEditors.SimpleButton();
            this.gpComment = new DevExpress.XtraEditors.GroupControl();
            this.memoComment = new DevExpress.XtraEditors.MemoEdit();
            this.groupAuthorizationMailbox = new DevExpress.XtraEditors.GroupControl();
            this.txtMailingAddressCopy = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lblStateMailbox = new DevExpress.XtraEditors.LabelControl();
            this.cmbStateMailbox = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblMailingAddress = new DevExpress.XtraEditors.LabelControl();
            this.txtMailingAddress = new DevExpress.XtraEditors.TextEdit();
            this.txtLogin = new DevExpress.XtraEditors.TextEdit();
            this.lblPassword = new DevExpress.XtraEditors.LabelControl();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.lblLogin = new DevExpress.XtraEditors.LabelControl();
            this.txtAccessToken = new DevExpress.XtraEditors.ButtonEdit();
            this.cmbEncryptionProtocolIncoming = new DevExpress.XtraEditors.ComboBoxEdit();
            this.groupIncomingMailServer = new DevExpress.XtraEditors.GroupControl();
            this.lblIncomingMailServerPOP3 = new DevExpress.XtraEditors.LabelControl();
            this.lblIncomingMailServerIMAP = new DevExpress.XtraEditors.LabelControl();
            this.txtPortPOP3 = new DevExpress.XtraEditors.TextEdit();
            this.txtPortIMAP = new DevExpress.XtraEditors.TextEdit();
            this.txtIncomingMailServerPOP3 = new DevExpress.XtraEditors.TextEdit();
            this.txtIncomingMailServerIMAP = new DevExpress.XtraEditors.TextEdit();
            this.lblEncryptionProtocolIncoming = new DevExpress.XtraEditors.LabelControl();
            this.groupЩгепщштпMailServer = new DevExpress.XtraEditors.GroupControl();
            this.lblEncryptionProtocolOutgoing = new DevExpress.XtraEditors.LabelControl();
            this.cmbEncryptionProtocolOutgoing = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblOutgoingMailServerSMTP = new DevExpress.XtraEditors.LabelControl();
            this.txtOutgoingMailServerSMTP = new DevExpress.XtraEditors.TextEdit();
            this.txtPortSMTP = new DevExpress.XtraEditors.TextEdit();
            this.gpMailboxSetup = new DevExpress.XtraEditors.GroupControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelFooter)).BeginInit();
            this.panelFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gpComment)).BeginInit();
            this.gpComment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupAuthorizationMailbox)).BeginInit();
            this.groupAuthorizationMailbox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMailingAddressCopy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStateMailbox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMailingAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLogin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAccessToken.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEncryptionProtocolIncoming.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupIncomingMailServer)).BeginInit();
            this.groupIncomingMailServer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPortPOP3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPortIMAP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIncomingMailServerPOP3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIncomingMailServerIMAP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupЩгепщштпMailServer)).BeginInit();
            this.groupЩгепщштпMailServer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEncryptionProtocolOutgoing.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOutgoingMailServerSMTP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPortSMTP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpMailboxSetup)).BeginInit();
            this.gpMailboxSetup.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(502, 11);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(125, 26);
            this.btnSave.TabIndex = 59;
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(635, 11);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(125, 26);
            this.btnCancel.TabIndex = 58;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panelFooter
            // 
            this.panelFooter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelFooter.Controls.Add(this.btnMailboxTest);
            this.panelFooter.Controls.Add(this.btnCancel);
            this.panelFooter.Controls.Add(this.btnSave);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 560);
            this.panelFooter.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(792, 51);
            this.panelFooter.TabIndex = 88;
            // 
            // btnMailboxTest
            // 
            this.btnMailboxTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMailboxTest.Location = new System.Drawing.Point(14, 11);
            this.btnMailboxTest.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnMailboxTest.Name = "btnMailboxTest";
            this.btnMailboxTest.Size = new System.Drawing.Size(250, 26);
            this.btnMailboxTest.TabIndex = 60;
            this.btnMailboxTest.Text = "Отправить пробное письмо";
            this.btnMailboxTest.Click += new System.EventHandler(this.btnMailboxTest_Click);
            // 
            // gpComment
            // 
            this.gpComment.Controls.Add(this.memoComment);
            this.gpComment.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gpComment.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.gpComment.Location = new System.Drawing.Point(0, 490);
            this.gpComment.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gpComment.Name = "gpComment";
            this.gpComment.Size = new System.Drawing.Size(792, 70);
            this.gpComment.TabIndex = 89;
            this.gpComment.Text = "Комментарий";
            // 
            // memoComment
            // 
            this.memoComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoComment.EditValue = "";
            this.memoComment.Location = new System.Drawing.Point(2, 26);
            this.memoComment.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.memoComment.Name = "memoComment";
            this.memoComment.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.memoComment.Size = new System.Drawing.Size(788, 42);
            this.memoComment.TabIndex = 12;
            // 
            // groupAuthorizationMailbox
            // 
            this.groupAuthorizationMailbox.Controls.Add(this.txtMailingAddressCopy);
            this.groupAuthorizationMailbox.Controls.Add(this.labelControl2);
            this.groupAuthorizationMailbox.Controls.Add(this.labelControl1);
            this.groupAuthorizationMailbox.Controls.Add(this.lblStateMailbox);
            this.groupAuthorizationMailbox.Controls.Add(this.cmbStateMailbox);
            this.groupAuthorizationMailbox.Controls.Add(this.lblMailingAddress);
            this.groupAuthorizationMailbox.Controls.Add(this.txtMailingAddress);
            this.groupAuthorizationMailbox.Controls.Add(this.txtLogin);
            this.groupAuthorizationMailbox.Controls.Add(this.lblPassword);
            this.groupAuthorizationMailbox.Controls.Add(this.txtPassword);
            this.groupAuthorizationMailbox.Controls.Add(this.lblLogin);
            this.groupAuthorizationMailbox.Controls.Add(this.txtAccessToken);
            this.groupAuthorizationMailbox.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupAuthorizationMailbox.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.groupAuthorizationMailbox.Location = new System.Drawing.Point(0, 0);
            this.groupAuthorizationMailbox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupAuthorizationMailbox.Name = "groupAuthorizationMailbox";
            this.groupAuthorizationMailbox.Size = new System.Drawing.Size(792, 222);
            this.groupAuthorizationMailbox.TabIndex = 94;
            this.groupAuthorizationMailbox.Text = "Авторизация почтового ящика";
            // 
            // txtMailingAddressCopy
            // 
            this.txtMailingAddressCopy.Location = new System.Drawing.Point(216, 184);
            this.txtMailingAddressCopy.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtMailingAddressCopy.Name = "txtMailingAddressCopy";
            this.txtMailingAddressCopy.Size = new System.Drawing.Size(544, 24);
            this.txtMailingAddressCopy.TabIndex = 99;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(14, 188);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(182, 18);
            this.labelControl2.TabIndex = 98;
            this.labelControl2.Text = "Адрес дублирования:";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(140, 125);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(56, 18);
            this.labelControl1.TabIndex = 97;
            this.labelControl1.Text = "Токен:";
            // 
            // lblStateMailbox
            // 
            this.lblStateMailbox.Location = new System.Drawing.Point(101, 156);
            this.lblStateMailbox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lblStateMailbox.Name = "lblStateMailbox";
            this.lblStateMailbox.Size = new System.Drawing.Size(94, 18);
            this.lblStateMailbox.TabIndex = 93;
            this.lblStateMailbox.Text = "Состояние:";
            // 
            // cmbStateMailbox
            // 
            this.cmbStateMailbox.Location = new System.Drawing.Point(216, 153);
            this.cmbStateMailbox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cmbStateMailbox.Name = "cmbStateMailbox";
            this.cmbStateMailbox.Properties.Appearance.BackColor = System.Drawing.Color.LightGreen;
            this.cmbStateMailbox.Properties.Appearance.BackColor2 = System.Drawing.Color.MintCream;
            this.cmbStateMailbox.Properties.Appearance.Options.UseBackColor = true;
            this.cmbStateMailbox.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbStateMailbox.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbStateMailbox.Size = new System.Drawing.Size(544, 24);
            this.cmbStateMailbox.TabIndex = 94;
            // 
            // lblMailingAddress
            // 
            this.lblMailingAddress.Location = new System.Drawing.Point(54, 30);
            this.lblMailingAddress.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lblMailingAddress.Name = "lblMailingAddress";
            this.lblMailingAddress.Size = new System.Drawing.Size(143, 18);
            this.lblMailingAddress.TabIndex = 0;
            this.lblMailingAddress.Text = "Почтовый адрес:";
            // 
            // txtMailingAddress
            // 
            this.txtMailingAddress.Location = new System.Drawing.Point(216, 27);
            this.txtMailingAddress.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtMailingAddress.Name = "txtMailingAddress";
            this.txtMailingAddress.Size = new System.Drawing.Size(544, 24);
            this.txtMailingAddress.TabIndex = 1;
            // 
            // txtLogin
            // 
            this.txtLogin.Location = new System.Drawing.Point(216, 90);
            this.txtLogin.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(544, 24);
            this.txtLogin.TabIndex = 5;
            // 
            // lblPassword
            // 
            this.lblPassword.Location = new System.Drawing.Point(131, 62);
            this.lblPassword.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(68, 18);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "Пароль:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(216, 58);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.UseSystemPasswordChar = true;
            this.txtPassword.Size = new System.Drawing.Size(544, 24);
            this.txtPassword.TabIndex = 3;
            // 
            // lblLogin
            // 
            this.lblLogin.Location = new System.Drawing.Point(141, 93);
            this.lblLogin.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(57, 18);
            this.lblLogin.TabIndex = 4;
            this.lblLogin.Text = "Логин:";
            // 
            // txtAccessToken
            // 
            this.txtAccessToken.Location = new System.Drawing.Point(216, 122);
            this.txtAccessToken.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtAccessToken.Name = "txtAccessToken";
            this.txtAccessToken.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.txtAccessToken.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.txtAccessToken.Size = new System.Drawing.Size(544, 24);
            this.txtAccessToken.TabIndex = 96;
            this.txtAccessToken.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtAccessToken_ButtonPressed);
            // 
            // cmbEncryptionProtocolIncoming
            // 
            this.cmbEncryptionProtocolIncoming.Location = new System.Drawing.Point(559, 90);
            this.cmbEncryptionProtocolIncoming.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cmbEncryptionProtocolIncoming.Name = "cmbEncryptionProtocolIncoming";
            this.cmbEncryptionProtocolIncoming.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbEncryptionProtocolIncoming.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbEncryptionProtocolIncoming.Size = new System.Drawing.Size(188, 24);
            this.cmbEncryptionProtocolIncoming.TabIndex = 19;
            // 
            // groupIncomingMailServer
            // 
            this.groupIncomingMailServer.Controls.Add(this.lblIncomingMailServerPOP3);
            this.groupIncomingMailServer.Controls.Add(this.lblIncomingMailServerIMAP);
            this.groupIncomingMailServer.Controls.Add(this.txtPortPOP3);
            this.groupIncomingMailServer.Controls.Add(this.txtPortIMAP);
            this.groupIncomingMailServer.Controls.Add(this.txtIncomingMailServerPOP3);
            this.groupIncomingMailServer.Controls.Add(this.txtIncomingMailServerIMAP);
            this.groupIncomingMailServer.Controls.Add(this.lblEncryptionProtocolIncoming);
            this.groupIncomingMailServer.Controls.Add(this.cmbEncryptionProtocolIncoming);
            this.groupIncomingMailServer.Location = new System.Drawing.Point(14, 27);
            this.groupIncomingMailServer.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupIncomingMailServer.Name = "groupIncomingMailServer";
            this.groupIncomingMailServer.Size = new System.Drawing.Size(765, 129);
            this.groupIncomingMailServer.TabIndex = 93;
            this.groupIncomingMailServer.Text = "Сервер входящей почты";
            // 
            // lblIncomingMailServerPOP3
            // 
            this.lblIncomingMailServerPOP3.Location = new System.Drawing.Point(135, 62);
            this.lblIncomingMailServerPOP3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lblIncomingMailServerPOP3.Name = "lblIncomingMailServerPOP3";
            this.lblIncomingMailServerPOP3.Size = new System.Drawing.Size(50, 18);
            this.lblIncomingMailServerPOP3.TabIndex = 13;
            this.lblIncomingMailServerPOP3.Text = "POP3:";
            // 
            // lblIncomingMailServerIMAP
            // 
            this.lblIncomingMailServerIMAP.Location = new System.Drawing.Point(136, 30);
            this.lblIncomingMailServerIMAP.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lblIncomingMailServerIMAP.Name = "lblIncomingMailServerIMAP";
            this.lblIncomingMailServerIMAP.Size = new System.Drawing.Size(48, 18);
            this.lblIncomingMailServerIMAP.TabIndex = 12;
            this.lblIncomingMailServerIMAP.Text = "IMAP:";
            // 
            // txtPortPOP3
            // 
            this.txtPortPOP3.Location = new System.Drawing.Point(559, 58);
            this.txtPortPOP3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtPortPOP3.Name = "txtPortPOP3";
            this.txtPortPOP3.Size = new System.Drawing.Size(188, 24);
            this.txtPortPOP3.TabIndex = 15;
            // 
            // txtPortIMAP
            // 
            this.txtPortIMAP.Location = new System.Drawing.Point(559, 27);
            this.txtPortIMAP.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtPortIMAP.Name = "txtPortIMAP";
            this.txtPortIMAP.Size = new System.Drawing.Size(188, 24);
            this.txtPortIMAP.TabIndex = 17;
            // 
            // txtIncomingMailServerPOP3
            // 
            this.txtIncomingMailServerPOP3.Location = new System.Drawing.Point(202, 58);
            this.txtIncomingMailServerPOP3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtIncomingMailServerPOP3.Name = "txtIncomingMailServerPOP3";
            this.txtIncomingMailServerPOP3.Size = new System.Drawing.Size(332, 24);
            this.txtIncomingMailServerPOP3.TabIndex = 9;
            // 
            // txtIncomingMailServerIMAP
            // 
            this.txtIncomingMailServerIMAP.Location = new System.Drawing.Point(202, 27);
            this.txtIncomingMailServerIMAP.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtIncomingMailServerIMAP.Name = "txtIncomingMailServerIMAP";
            this.txtIncomingMailServerIMAP.Size = new System.Drawing.Size(332, 24);
            this.txtIncomingMailServerIMAP.TabIndex = 7;
            // 
            // lblEncryptionProtocolIncoming
            // 
            this.lblEncryptionProtocolIncoming.Location = new System.Drawing.Point(418, 93);
            this.lblEncryptionProtocolIncoming.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lblEncryptionProtocolIncoming.Name = "lblEncryptionProtocolIncoming";
            this.lblEncryptionProtocolIncoming.Size = new System.Drawing.Size(115, 18);
            this.lblEncryptionProtocolIncoming.TabIndex = 18;
            this.lblEncryptionProtocolIncoming.Text = "Шифрование:";
            // 
            // groupЩгепщштпMailServer
            // 
            this.groupЩгепщштпMailServer.Controls.Add(this.lblEncryptionProtocolOutgoing);
            this.groupЩгепщштпMailServer.Controls.Add(this.cmbEncryptionProtocolOutgoing);
            this.groupЩгепщштпMailServer.Controls.Add(this.lblOutgoingMailServerSMTP);
            this.groupЩгепщштпMailServer.Controls.Add(this.txtOutgoingMailServerSMTP);
            this.groupЩгепщштпMailServer.Controls.Add(this.txtPortSMTP);
            this.groupЩгепщштпMailServer.Location = new System.Drawing.Point(14, 163);
            this.groupЩгепщштпMailServer.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupЩгепщштпMailServer.Name = "groupЩгепщштпMailServer";
            this.groupЩгепщштпMailServer.Size = new System.Drawing.Size(765, 96);
            this.groupЩгепщштпMailServer.TabIndex = 95;
            this.groupЩгепщштпMailServer.Text = "Сервер исходящей почты";
            // 
            // lblEncryptionProtocolOutgoing
            // 
            this.lblEncryptionProtocolOutgoing.Location = new System.Drawing.Point(418, 62);
            this.lblEncryptionProtocolOutgoing.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lblEncryptionProtocolOutgoing.Name = "lblEncryptionProtocolOutgoing";
            this.lblEncryptionProtocolOutgoing.Size = new System.Drawing.Size(115, 18);
            this.lblEncryptionProtocolOutgoing.TabIndex = 20;
            this.lblEncryptionProtocolOutgoing.Text = "Шифрование:";
            // 
            // cmbEncryptionProtocolOutgoing
            // 
            this.cmbEncryptionProtocolOutgoing.Location = new System.Drawing.Point(559, 58);
            this.cmbEncryptionProtocolOutgoing.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cmbEncryptionProtocolOutgoing.Name = "cmbEncryptionProtocolOutgoing";
            this.cmbEncryptionProtocolOutgoing.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbEncryptionProtocolOutgoing.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbEncryptionProtocolOutgoing.Size = new System.Drawing.Size(188, 24);
            this.cmbEncryptionProtocolOutgoing.TabIndex = 21;
            // 
            // lblOutgoingMailServerSMTP
            // 
            this.lblOutgoingMailServerSMTP.Location = new System.Drawing.Point(131, 30);
            this.lblOutgoingMailServerSMTP.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lblOutgoingMailServerSMTP.Name = "lblOutgoingMailServerSMTP";
            this.lblOutgoingMailServerSMTP.Size = new System.Drawing.Size(50, 18);
            this.lblOutgoingMailServerSMTP.TabIndex = 10;
            this.lblOutgoingMailServerSMTP.Text = "SMTP:";
            // 
            // txtOutgoingMailServerSMTP
            // 
            this.txtOutgoingMailServerSMTP.Location = new System.Drawing.Point(202, 27);
            this.txtOutgoingMailServerSMTP.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtOutgoingMailServerSMTP.Name = "txtOutgoingMailServerSMTP";
            this.txtOutgoingMailServerSMTP.Size = new System.Drawing.Size(332, 24);
            this.txtOutgoingMailServerSMTP.TabIndex = 11;
            // 
            // txtPortSMTP
            // 
            this.txtPortSMTP.Location = new System.Drawing.Point(559, 27);
            this.txtPortSMTP.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtPortSMTP.Name = "txtPortSMTP";
            this.txtPortSMTP.Size = new System.Drawing.Size(188, 24);
            this.txtPortSMTP.TabIndex = 13;
            // 
            // gpMailboxSetup
            // 
            this.gpMailboxSetup.Controls.Add(this.groupЩгепщштпMailServer);
            this.gpMailboxSetup.Controls.Add(this.groupIncomingMailServer);
            this.gpMailboxSetup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpMailboxSetup.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.gpMailboxSetup.Location = new System.Drawing.Point(0, 222);
            this.gpMailboxSetup.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.gpMailboxSetup.Name = "gpMailboxSetup";
            this.gpMailboxSetup.Size = new System.Drawing.Size(792, 268);
            this.gpMailboxSetup.TabIndex = 97;
            this.gpMailboxSetup.Text = "Настройки почтового ящика";
            // 
            // MailboxEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 611);
            this.Controls.Add(this.gpMailboxSetup);
            this.Controls.Add(this.groupAuthorizationMailbox);
            this.Controls.Add(this.gpComment);
            this.Controls.Add(this.panelFooter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "MailboxEdit";
            this.Text = "Почтовый ящик";
            this.Load += new System.EventHandler(this.RouteSheetEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelFooter)).EndInit();
            this.panelFooter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gpComment)).EndInit();
            this.gpComment.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupAuthorizationMailbox)).EndInit();
            this.groupAuthorizationMailbox.ResumeLayout(false);
            this.groupAuthorizationMailbox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMailingAddressCopy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStateMailbox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMailingAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLogin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAccessToken.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEncryptionProtocolIncoming.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupIncomingMailServer)).EndInit();
            this.groupIncomingMailServer.ResumeLayout(false);
            this.groupIncomingMailServer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPortPOP3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPortIMAP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIncomingMailServerPOP3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIncomingMailServerIMAP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupЩгепщштпMailServer)).EndInit();
            this.groupЩгепщштпMailServer.ResumeLayout(false);
            this.groupЩгепщштпMailServer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEncryptionProtocolOutgoing.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOutgoingMailServerSMTP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPortSMTP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpMailboxSetup)).EndInit();
            this.gpMailboxSetup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.PanelControl panelFooter;
        private DevExpress.XtraEditors.GroupControl gpComment;
        private DevExpress.XtraEditors.MemoEdit memoComment;
        private DevExpress.XtraEditors.GroupControl groupAuthorizationMailbox;
        private DevExpress.XtraEditors.LabelControl lblMailingAddress;
        private DevExpress.XtraEditors.TextEdit txtMailingAddress;
        private DevExpress.XtraEditors.TextEdit txtLogin;
        private DevExpress.XtraEditors.LabelControl lblPassword;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraEditors.ComboBoxEdit cmbEncryptionProtocolIncoming;
        private DevExpress.XtraEditors.LabelControl lblLogin;
        private DevExpress.XtraEditors.GroupControl groupIncomingMailServer;
        private DevExpress.XtraEditors.LabelControl lblIncomingMailServerPOP3;
        private DevExpress.XtraEditors.LabelControl lblIncomingMailServerIMAP;
        private DevExpress.XtraEditors.TextEdit txtIncomingMailServerPOP3;
        private DevExpress.XtraEditors.TextEdit txtIncomingMailServerIMAP;
        private DevExpress.XtraEditors.GroupControl groupЩгепщштпMailServer;
        private DevExpress.XtraEditors.LabelControl lblOutgoingMailServerSMTP;
        private DevExpress.XtraEditors.TextEdit txtOutgoingMailServerSMTP;
        private DevExpress.XtraEditors.TextEdit txtPortSMTP;
        private DevExpress.XtraEditors.TextEdit txtPortIMAP;
        private DevExpress.XtraEditors.TextEdit txtPortPOP3;
        private DevExpress.XtraEditors.LabelControl lblStateMailbox;
        private DevExpress.XtraEditors.ComboBoxEdit cmbStateMailbox;
        private DevExpress.XtraEditors.GroupControl gpMailboxSetup;
        private DevExpress.XtraEditors.LabelControl lblEncryptionProtocolIncoming;
        private DevExpress.XtraEditors.LabelControl lblEncryptionProtocolOutgoing;
        private DevExpress.XtraEditors.ComboBoxEdit cmbEncryptionProtocolOutgoing;
        private DevExpress.XtraEditors.SimpleButton btnMailboxTest;
        private DevExpress.XtraEditors.ButtonEdit txtAccessToken;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtMailingAddressCopy;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}