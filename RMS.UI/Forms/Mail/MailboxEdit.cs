using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model.Mail;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace RMS.UI.Forms.Mail
{
    public partial class MailboxEdit : formEdit_BaseSpr
    {
        private Session Session { get; }
        public Mailbox Mailbox { get; }

        public MailboxEdit()
        {
            InitializeComponent();            

            foreach (StateMailbox item in Enum.GetValues(typeof(StateMailbox)))
            {
                cmbStateMailbox.Properties.Items.Add(item.GetEnumDescription());
            }
            cmbStateMailbox.SelectedIndex = 0;

            foreach (EncryptionProtocol item in Enum.GetValues(typeof(EncryptionProtocol)))
            {
                cmbEncryptionProtocolIncoming.Properties.Items.Add(item.GetEnumDescription());
            }
            cmbEncryptionProtocolIncoming.SelectedIndex = 0;

            foreach (EncryptionProtocol item in Enum.GetValues(typeof(EncryptionProtocol)))
            {
                cmbEncryptionProtocolOutgoing.Properties.Items.Add(item.GetEnumDescription());
            }
            cmbEncryptionProtocolOutgoing.SelectedIndex = 0;

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                Mailbox = new Mailbox(Session);
            }
        }

        public MailboxEdit(int id) : this()
        {
            if (id > 0)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                Mailbox = Session.GetObjectByKey<Mailbox>(id);
            }
        }

        public MailboxEdit(Mailbox mailbox) : this()
        {
            Mailbox = mailbox;
            Session = mailbox.Session;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveMailBox())
            {
                id = Mailbox.Oid;
                flagSave = true;
                Close();
            }            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void RouteSheetEdit_Load(object sender, EventArgs e)
        {
            txtMailingAddress.EditValue = Mailbox.MailingAddress;
            txtMailingAddressCopy.EditValue = Mailbox.MailingAddressCopy;
            
            txtPassword.EditValue = Mailbox.Decrypt(Mailbox.Password);
            txtLogin.EditValue = Mailbox.Login;
            txtAccessToken.EditValue = Mailbox.AccessToken;
            cmbStateMailbox.SelectedIndex = (int)Mailbox.StateMailbox;
            memoComment.EditValue = Mailbox.Comment;

            txtIncomingMailServerIMAP.EditValue = Mailbox.MailboxSetup.IncomingMailServerIMAP;
            txtPortIMAP.EditValue = Mailbox.MailboxSetup.PortIMAP;
            txtIncomingMailServerPOP3.EditValue = Mailbox.MailboxSetup.IncomingMailServerPOP3;
            txtPortPOP3.EditValue = Mailbox.MailboxSetup.PortPOP3;
            cmbEncryptionProtocolIncoming.SelectedIndex = (int)Mailbox.MailboxSetup.EncryptionProtocolIncoming;

            txtOutgoingMailServerSMTP.EditValue = Mailbox.MailboxSetup.OutgoingMailServerSMTP;
            txtPortSMTP.EditValue = Mailbox.MailboxSetup.PortSMTP;
            cmbEncryptionProtocolOutgoing.SelectedIndex = (int)Mailbox.MailboxSetup.EncryptionProtocolOutgoing;
        }

        /// <summary>
        /// Сохранение почтового ящика.
        /// </summary>
        private bool SaveMailBox()
        {
            if (string.IsNullOrWhiteSpace(txtMailingAddress.Text))
            {
                XtraMessageBox.Show($"Почтовый адрес не может быть пустым",
                   "Пустое значение",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
                txtMailingAddress.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                XtraMessageBox.Show($"Пароль не может быть пустым",
                   "Пустое значение",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
                txtPassword.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtIncomingMailServerIMAP.Text))
            {
                XtraMessageBox.Show($"Заполните значение сервера входящей почты: IMAP",
                   "Пустое значение",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
                txtIncomingMailServerIMAP.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPortIMAP.Text) && !int.TryParse(txtPortIMAP.Text, out _))
            {
                XtraMessageBox.Show($"Заполните значение сервера входящей почты: IMAP (порт)",
                   "Пустое значение",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
                txtPortIMAP.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtIncomingMailServerPOP3.Text))
            {
                XtraMessageBox.Show($"Заполните значение сервера входящей почты: POP3",
                   "Пустое значение",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
                txtIncomingMailServerPOP3.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPortPOP3.Text) && !int.TryParse(txtPortPOP3.Text, out _))
            {
                XtraMessageBox.Show($"Заполните значение сервера входящей почты: POP3 (порт)",
                   "Пустое значение",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
                txtPortPOP3.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtOutgoingMailServerSMTP.Text))
            {
                XtraMessageBox.Show($"Заполните значение сервера входящей почты: SMTP",
                   "Пустое значение",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
                txtOutgoingMailServerSMTP.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPortSMTP.Text) && !int.TryParse(txtPortSMTP.Text, out _))
            {
                XtraMessageBox.Show($"Заполните значение сервера входящей почты: SMTP (порт)",
                   "Пустое значение",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
                txtPortSMTP.Focus();
                return false;
            }

            var mailingAddress = txtMailingAddress.Text;

            var groupMailboxOperator = new GroupOperator(GroupOperatorType.And);
            var criteriaMailboxMailingAddress = new BinaryOperator(nameof(Mailbox.MailingAddress), mailingAddress);
            groupMailboxOperator.Operands.Add(criteriaMailboxMailingAddress);

            var mailbox = Session.FindObject<Mailbox>(groupMailboxOperator);

            if (mailbox != null && mailbox != Mailbox)
            {
                XtraMessageBox.Show($"Почтовый ящик [{mailbox}] уже существует.",
                    "Найден почтовый ящик",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return false;
            }

            try
            {
                Mailbox.MailingAddress = txtMailingAddress.Text;
                Mailbox.MailingAddressCopy = txtMailingAddressCopy.Text;
                Mailbox.Password = Mailbox.Encrypt(txtPassword.Text);
                Mailbox.Login = txtLogin.Text;
                Mailbox.StateMailbox = (StateMailbox)cmbStateMailbox.SelectedIndex;
                Mailbox.Comment = memoComment.Text;

                Mailbox.MailboxSetup.IncomingMailServerIMAP = txtIncomingMailServerIMAP.Text;
                Mailbox.MailboxSetup.PortIMAP = Convert.ToInt32(txtPortIMAP.EditValue);
                Mailbox.MailboxSetup.IncomingMailServerPOP3 = txtIncomingMailServerPOP3.Text;
                Mailbox.MailboxSetup.PortPOP3 = Convert.ToInt32(txtPortPOP3.EditValue);
                Mailbox.MailboxSetup.EncryptionProtocolIncoming = (EncryptionProtocol)cmbEncryptionProtocolIncoming.SelectedIndex;

                Mailbox.MailboxSetup.OutgoingMailServerSMTP = txtOutgoingMailServerSMTP.Text;
                Mailbox.MailboxSetup.PortSMTP = Convert.ToInt32(txtPortSMTP.EditValue);
                Mailbox.MailboxSetup.EncryptionProtocolOutgoing = (EncryptionProtocol)cmbEncryptionProtocolOutgoing.SelectedIndex;

                Mailbox.Save();
                return true;
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                return false; 
            }
        }

        private void btnMailboxTest_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show($"После нажатия кнопки OK на вашу почту [{Mailbox.MailingAddress}] придет письмо.",
                    "Проверка отправки писем",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (SaveMailBox())
                {
                    Letter.SendMailKit(Mailbox.MailingAddress,
                    "Тестовое сообщение",
                    $"Проверка отправки писем из RMS v{this.ProductVersion}",
                    Mailbox.MailingAddress,
                    BVVGlobal.oApp.User,
                    Mailbox.Decrypt(Mailbox.Password),
                    Mailbox.MailboxSetup.OutgoingMailServerSMTP,
                    Mailbox.MailboxSetup.PortSMTP,
                    null,
                    Mailbox.MailingAddressCopy);
                }
            }
        }

        private void txtAccessToken_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                Mailbox.AccessToken = null;
                buttonEdit.EditValue = null;
                return;
            }           
        }
    }
}