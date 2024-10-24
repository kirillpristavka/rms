using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using RMS.Core.Enumerator;
using RMS.Core.Model;
using RMS.Core.Model.Chronicle;
using RMS.Core.Model.InfoCustomer;
using RMS.UI.Forms.Chronicle;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace RMS.UI.Forms.ReferenceBooks
{
    public partial class StatusCustomerEdit : XtraForm
    {
        private Session Session { get; }
        private Customer Customer { get; }
        private CustomerStatus CustomerStatus { get; }

        private StatusCustomerEdit()
        {
            InitializeComponent();
        }

        public StatusCustomerEdit(Customer customer) : this()
        {
            Customer = customer;
            Session = customer.Session;
            CustomerStatus = customer.CustomerStatus ?? new CustomerStatus(Session);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dateDate.EditValue is null)
            {
                dateDate.Focus();
                XtraMessageBox.Show("Сохранение не возможно без указания даты.", "Не указана дата", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!(btnStatus.EditValue is Status status))
            {
                btnStatus.Focus();
                XtraMessageBox.Show("Сохранение не возможно без указания системы налогообложения.", "Не указана система налогообложения", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (Customer.Oid != -1 && CustomerStatus.Oid != -1 &&
                (
                    CustomerStatus.Date != dateDate.DateTime ||
                    CustomerStatus.Status != status ||
                    CustomerStatus.Comment != memoComment.Text
                ))
            {
                var user = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid);

                CustomerStatus.ChronicleCustomerStatus.Add(new ChronicleCustomerStatus(Session)
                {
                    Date = CustomerStatus.Date,
                    Status = CustomerStatus.Status.ToString(),
                    Comment = CustomerStatus.Comment,
                    User = user,
                    DateCreate = DateTime.Now
                });

                var description = $"Пользователь {user} изменил статус клиента с [{CustomerStatus.Status}] на [{status}].";
                Customer.ChronicleCustomers.Add(new ChronicleCustomer(Session)
                {
                    Act = Act.CUSTOMER_STATUS_CHANGE,
                    Date = DateTime.Now,
                    Description = description,
                    User = user
                });
            }

            CustomerStatus.Date = dateDate.DateTime;
            CustomerStatus.Status = status;
            CustomerStatus.Comment = memoComment.Text;
            CustomerStatus.Save();
            Customer.CustomerStatus = CustomerStatus;
            if (Customer.Oid != -1)
            {
                Customer.Save();
            }
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void StatusCustomerEdit_Load(object sender, EventArgs e)
        {
            btnCustomer.EditValue = Customer;
            dateDate.EditValue = CustomerStatus.Date ?? DateTime.Now;
            btnStatus.EditValue = CustomerStatus.Status;
            memoComment.EditValue = CustomerStatus.Comment;
        }

        private void btnCustomer_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            cls_BaseSpr.ButtonEditButtonClickBase<Customer>(Session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.Customer, 1, null, null, false, null, string.Empty, false, true);
        }
        
        private void btnStatus_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            cls_BaseSpr.ButtonEditButtonClickBase<Status>(Session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.Status, 1, null, null, false, null, string.Empty, false, true);
        }

        private void btnChronicle_Click(object sender, EventArgs e)
        {
            var form = new ChronicleTaxesForm<ChronicleCustomerStatus>(CustomerStatus.ChronicleCustomerStatus, "Статусы клиента");
            form.ShowDialog();
        }

        private void btnStatus_EditValueChanged(object sender, EventArgs e)
        {
            if (sender is ButtonEdit buttonEdit)
            {
                if (buttonEdit.EditValue is Status status)
                {
                    if (!string.IsNullOrWhiteSpace(status.Color))
                    {
                        var color = ColorTranslator.FromHtml(status.Color);
                        buttonEdit.BackColor = color;
                    }
                }
                else
                {
                    buttonEdit.BackColor = default;
                }
            }
        }
    }
}