using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using RMS.Core.Controllers;
using RMS.Core.Enumerator;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Mail;
using RMS.Core.Model.Notifications;
using RMS.Core.Model.Taxes;
using RMS.UI.Forms.ReferenceBooks;
using System;
using System.Diagnostics;
using System.Linq;

namespace RMS.UI.Forms.Directories
{
    public partial class ElectronicReportingСustomerNotificationEdit : formEdit_BaseSpr
    {
        private ElectronicReportingCustomer _electronicReportingCustomer;
        private ElectronicReportingСustomerNotification _electronicReportingСustomerNotification;
        
        private Customer _customer;
        private Staff _staff;
        private Session _session;

        public ElectronicReportingСustomerNotificationEdit()
        {
            InitializeComponent();

            if (_session is null)
            {
                _session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                _electronicReportingСustomerNotification = new ElectronicReportingСustomerNotification(_session);
            }
        }

        public ElectronicReportingСustomerNotificationEdit(ElectronicReportingCustomer electronicReportingCustomer) : this()
        {
            _electronicReportingCustomer = electronicReportingCustomer;
            
            _session = _electronicReportingCustomer.Session;
            _electronicReportingСustomerNotification = new ElectronicReportingСustomerNotification(_session);
            _customer = _electronicReportingCustomer.Customer;
        }

        public ElectronicReportingСustomerNotificationEdit(ElectronicReportingСustomerNotification electronicReportingСustomerNotification) : this()
        {
            _session = electronicReportingСustomerNotification.Session;
            _electronicReportingСustomerNotification = electronicReportingСustomerNotification;
            _customer = electronicReportingСustomerNotification.Customer;
        }
        
        private void PositionEdit_Load(object sender, EventArgs e)
        {            
            if (_electronicReportingСustomerNotification.Oid > 0)
            {
                _staff = _electronicReportingСustomerNotification.Staff;
                _customer = _electronicReportingСustomerNotification.Customer;

                FillParametrs(checkIsUseEmail, _electronicReportingСustomerNotification.IsUseEmail, dateEmail, _electronicReportingСustomerNotification.DateEmail, cmbRecipientEmail, _electronicReportingСustomerNotification.RecipientEmail, true);
                FillParametrs(checkIsUseTelegram, _electronicReportingСustomerNotification.IsUseTelegram, dateTelegram, _electronicReportingСustomerNotification.DateTelegram, cmbRecipientTelegram, _electronicReportingСustomerNotification.RecipientTelegram);
                FillParametrs(checkIsUseWhatsApp, _electronicReportingСustomerNotification.IsUseWhatsApp, dateWhatsApp, _electronicReportingСustomerNotification.DateWhatsApp, cmbRecipientWhatsApp, _electronicReportingСustomerNotification.RecipientWhatsApp);
                FillParametrs(checkIsUsePhone, _electronicReportingСustomerNotification.IsUsePhone, datePhone, _electronicReportingСustomerNotification.DatePhone, cmbRecipientPhone, _electronicReportingСustomerNotification.RecipientPhone);

                memoMessage.Properties.ReadOnly = true;
                var message = Letter.ByteToString(_electronicReportingСustomerNotification.Message);
                if (!string.IsNullOrWhiteSpace(message))
                {
                    message = message.Replace("<br>", Environment.NewLine);
                }
                memoMessage.EditValue = message;

                txtTopic.EditValue = Letter.ByteToString(_electronicReportingСustomerNotification.Topic);
                
                SetBtnControlSystemText();
            }
            else
            {
                btnControlSystem.Enabled = false;
                btnControlSystem.ToolTip = "Доступно только после сохранения формы";

                _staff = _session.GetObjectByKey<Staff>(DatabaseConnection.User?.Staff?.Oid);
                _staff?.Reload();
                
                try
                {
                    cmbRecipientEmail.Properties.Items.AddRange(_customer.CustomerEmails?.Select(s => s.Email)?.ToList());
                    cmbRecipientEmail.EditValue = _customer.Email;

                    dateEmail.EditValue = DateTime.Now;
                    dateTelegram.EditValue = DateTime.Now;
                    dateWhatsApp.EditValue = DateTime.Now;
                    datePhone.EditValue = DateTime.Now;

                    cmbRecipientPhone.Properties.Items.AddRange(_customer.CustomerTelephones?.Select(s => s.Telephone)?.ToList());
                    cmbRecipientPhone.EditValue = _customer.Telephone;
                }
                catch (Exception ex)
                {
                    LoggerController.WriteLog(ex?.ToString());
                }
                
                var message = "Добрый день";
                
                var customerManagementName = _customer.ManagementNameAndPatronymicString;
                if (string.IsNullOrWhiteSpace(customerManagementName))
                {
                    message += $"!{Environment.NewLine}";
                }
                else
                {
                    message += $", {customerManagementName}!{Environment.NewLine}";
                }

                var customerName = _customer.FullName;
                if (string.IsNullOrWhiteSpace(customerName))
                {
                    customerName = _customer.ToString();
                }

                message += $"{Environment.NewLine}Обращаем ваше внимание, что <dateTo>у Вас заканчивается срок действия электронной подписи (ЭП):{Environment.NewLine}" +
                    $"{customerName}{Environment.NewLine}{Environment.NewLine}" +
                    $"Необходимо получить ЭП и предоставить в офис \"БК АЛЬГРАС\" по адресу:{Environment.NewLine}" +
                    $"г. Санкт-Петербург, ул. Ключевая, д. 30, литер А, офис 207.{Environment.NewLine}{Environment.NewLine}" +
                    $"*** C 01.01.2022 получение ЭП осуществляется лично руководителем в МИФНС №15 по адресу:{Environment.NewLine}" +
                    $"    г. Санкт-Петербург, ул. Красного Текстильщика, д. 10-12 литер О.{Environment.NewLine}" +
                    $"*** С собой необходимо иметь паспорт, СНИЛС, ИНН и печать организации.{Environment.NewLine}" +
                    $"*** При необходимости, Вы можете приехать к нам в офис и взять носитель, чтобы не покупать в налоговой.{Environment.NewLine}" +
                    $"Спасибо!";
                
                var dateTo = _electronicReportingCustomer.DateTo;
                if (dateTo is DateTime)
                {
                    message = message.Replace("<dateTo>", $"{dateTo.Value.ToShortDateString()} ");
                }
                else
                {
                    message = message.Replace("<dateTo>", "");
                }

                memoMessage.EditValue = message;
            }

            btnCustomer.EditValue = _customer;
            btnStaff.EditValue = _staff;            
        }

        private void SetBtnControlSystemText()
        {
            var controlSystem = _electronicReportingСustomerNotification.ControlSystem;
            if (controlSystem != null)
            {
                if (controlSystem.DateTo is DateTime date)
                {
                    btnControlSystem.Text = $"Контроль до {date.ToShortDateString()}";
                }
                else
                {
                    btnControlSystem.Text = $"Контроль";
                }
            }
        }

        private void FillParametrs(CheckEdit checkEditUse,
                                   bool isUse,
                                   DateEdit date,
                                   DateTime? dateValue,
                                   ComboBoxEdit comboBox,
                                   string comboBoxValue,
                                   bool isReadOnly = false)
        {
            checkEditUse.ReadOnly = isReadOnly;
            checkEditUse.EditValue = isUse;

            date.ReadOnly = isReadOnly;
            date.EditValue = dateValue;

            comboBox.ReadOnly = isReadOnly;
            comboBox.Properties.Buttons[0].Visible = !isReadOnly;
            comboBox.EditValue = comboBoxValue;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void btnSent_Click(object sender, EventArgs e)
        {
            var customer = default(Customer);
            var staff = default(Staff);
            var topic = txtTopic.Text;
            var recipientEmail = cmbRecipientEmail.Text;
            var message = memoMessage.Text;

            if (btnCustomer.EditValue is Customer _customer)
            {
                customer = _customer;
            }
            else
            {
                XtraMessageBox.Show("Отправка без указанного клиента невозможна.",
                                    "Ошибка операции",
                                    System.Windows.Forms.MessageBoxButtons.OK,
                                    System.Windows.Forms.MessageBoxIcon.Information);
                btnCustomer.Focus();
                return;
            }

            if (btnStaff.EditValue is Staff _staff)
            {
                staff = _staff;
            }
            else
            {
                XtraMessageBox.Show("Отправка без указанного сотрудника невозможна.",
                                    "Ошибка операции",
                                    System.Windows.Forms.MessageBoxButtons.OK,
                                    System.Windows.Forms.MessageBoxIcon.Information);
                btnStaff.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(topic))
            {
                XtraMessageBox.Show("Отправка без темы невозможна.",
                                    "Ошибка операции",
                                    System.Windows.Forms.MessageBoxButtons.OK,
                                    System.Windows.Forms.MessageBoxIcon.Information);
                txtTopic.Focus();
                return;
            }            

            if (string.IsNullOrWhiteSpace(message))
            {
                XtraMessageBox.Show("Отправка без сообщения невозможна.",
                                    "Ошибка операции",
                                    System.Windows.Forms.MessageBoxButtons.OK,
                                    System.Windows.Forms.MessageBoxIcon.Information);
                memoMessage.Focus();
                return;
            }

            message = message.Replace("\r\n", "<br>");

            _electronicReportingСustomerNotification.IsUseEmail = checkIsUseEmail.Checked;
            if (_electronicReportingСustomerNotification.IsUseEmail)
            {
                if (string.IsNullOrWhiteSpace(recipientEmail))
                {
                    XtraMessageBox.Show("Отправка без получателя невозможна.",
                                        "Ошибка операции",
                                        System.Windows.Forms.MessageBoxButtons.OK,
                                        System.Windows.Forms.MessageBoxIcon.Information);
                    cmbRecipientEmail.Focus();
                    return;
                }
                else
                {
                    _electronicReportingСustomerNotification.RecipientEmail = recipientEmail;
                }

                if (dateEmail.EditValue is DateTime date)
                {
                    _electronicReportingСustomerNotification.DateEmail = date;
                }
                else
                {
                    _electronicReportingСustomerNotification.DateEmail = DateTime.Now;
                }
            }
            else
            {
                _electronicReportingСustomerNotification.DateEmail = default;
                _electronicReportingСustomerNotification.RecipientEmail = default;
            }

            _electronicReportingСustomerNotification.IsUseTelegram = checkIsUseTelegram.Checked;
            _electronicReportingСustomerNotification.DateTelegram = GetDateTime(dateTelegram.EditValue);
            _electronicReportingСustomerNotification.RecipientTelegram = cmbRecipientTelegram.Text;

            _electronicReportingСustomerNotification.IsUseWhatsApp = checkIsUseWhatsApp.Checked;
            _electronicReportingСustomerNotification.DateWhatsApp = GetDateTime(dateWhatsApp.EditValue);
            _electronicReportingСustomerNotification.RecipientWhatsApp = cmbRecipientWhatsApp.Text;

            _electronicReportingСustomerNotification.IsUsePhone = checkIsUsePhone.Checked;
            _electronicReportingСustomerNotification.DatePhone = GetDateTime(datePhone.EditValue);
            _electronicReportingСustomerNotification.RecipientPhone = cmbRecipientPhone.Text;

            _electronicReportingСustomerNotification.Customer = customer;
            _electronicReportingСustomerNotification.Staff = staff;
            _electronicReportingСustomerNotification.Topic = Letter.StringToByte(topic);
            _electronicReportingСustomerNotification.Message = Letter.StringToByte(message);
            _electronicReportingСustomerNotification.ElectronicReportingCustomer = customer.ElectronicReportingCustomer;

            if (_electronicReportingСustomerNotification.Oid <= 0 && _electronicReportingСustomerNotification.IsUseEmail)
            {
                await SentLetter(recipientEmail, message);
            }
            
            _session.Save(_electronicReportingСustomerNotification); 
            id = _electronicReportingСustomerNotification.Oid;

            if (_electronicReportingСustomerNotification.Oid <= 0)
            {
                AddComment();
            }
            
            flagSave = true;
            Close();
        }
        
        private DateTime? GetDateTime(object obj)
        {
            if (obj is DateTime date)
            {
                return date;
            }
            
            return default;
        }

        private void AddComment()
        {
            try
            {
                _electronicReportingСustomerNotification.ElectronicReportingCustomer.Comment =
                                    $"{_electronicReportingСustomerNotification.ElectronicReportingCustomer.Comment.Trim()}{Environment.NewLine}{_electronicReportingСustomerNotification}"?.Trim();
                _electronicReportingСustomerNotification.ElectronicReportingCustomer.Save();
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async System.Threading.Tasks.Task<bool> SentLetter(string recipient, string message)
        {
            var mailbox = default(Mailbox);
            var mailingAddressName = await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, nameof(cls_AppParam.MailboxForSending), "");

            if (string.IsNullOrWhiteSpace(mailingAddressName))
            {
                mailbox = _session.FindObject<Mailbox>(new BinaryOperator(nameof(Mailbox.StateMailbox), StateMailbox.SendingLetters));
            }

            if (mailbox is null)
            {
                mailbox = _session.FindObject<Mailbox>(new BinaryOperator(nameof(Mailbox.MailingAddress), mailingAddressName));
            }

            try
            {
                Letter.SendMailKit(recipient,
                    txtTopic.Text,
                    message,
                    mailbox.MailingAddress,
                    BVVGlobal.oApp.User,
                    Mailbox.Decrypt(mailbox.Password),
                    mailbox.MailboxSetup.OutgoingMailServerSMTP,
                    mailbox.MailboxSetup.PortSMTP,
                    null);

                return true;
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
            }

            return false;
        }

        private async void btnControlSystem_Click(object sender, EventArgs e)
        {
            var form = default(ControlSystemEdit);

            if (_electronicReportingСustomerNotification.ControlSystem is null)
            {
                form = new ControlSystemEdit(_electronicReportingСustomerNotification);
            }
            else
            {
                form = new ControlSystemEdit(_electronicReportingСustomerNotification.ControlSystem);
            }
            form.ShowDialog();

            if (form.ControlSystem != null && form.ControlSystem.Oid > 0)
            {
                _electronicReportingСustomerNotification.ControlSystem = await _session.GetObjectByKeyAsync<ControlSystem>(form.ControlSystem.Oid);

                if (_electronicReportingСustomerNotification.Oid > 0)
                {
                    _electronicReportingСustomerNotification.Save();
                    SetBtnControlSystemText();
                }
            }
        }
    }
}