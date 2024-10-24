using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using System;

namespace RMS.UI.Forms.Directories
{
    public partial class ContactEdit : XtraForm
    {
        private Session Session { get; }
        private Customer Customer { get; }
        private CustomerEmail CustomerEmail { get; }
        private CustomerTelephone CustomerTelephone { get; }
        private bool IsEmail { get; }

        private ContactEdit()
        {
            InitializeComponent();
        }

        public ContactEdit(Customer customer, bool isEmail) : this()
        {
            Customer = customer;
            IsEmail = isEmail;
            Session = customer.Session;

            if (isEmail)
            {
                CustomerEmail = new CustomerEmail(Session);
            }
            else
            {
                CustomerTelephone = new CustomerTelephone(Session);
            }            
        }

        public ContactEdit(CustomerEmail customerEmail) : this()
        {
            CustomerEmail = customerEmail;
            Customer = customerEmail.Customer;
            Session = customerEmail.Session;
            IsEmail = true;
        }

        public ContactEdit(CustomerTelephone customerTelephone) : this()
        {
            CustomerTelephone = customerTelephone;
            Customer = customerTelephone.Customer;
            Session = customerTelephone.Session;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var addres = txtAddress.EditValue as Address;
            var position = txtPosition.EditValue as Position;
            
            if (IsEmail)
            {
                if (addres != null)
                {
                    CustomerEmail.Address = addres;
                }
                else
                {
                    CustomerEmail.Address = null;
                }

                if (position != null)
                {
                    CustomerEmail.Position = position;
                }
                else
                {
                    CustomerEmail.Position = null;
                }
                
                CustomerEmail.FullName = txtFullName.Text;
                CustomerEmail.Surname = txtSurname.Text;
                CustomerEmail.Name = txtName.Text;
                CustomerEmail.Patronymic = txtPatronymic.Text;
                CustomerEmail.Email = txtEmail.Text;
                CustomerEmail.Comment = memoComment.Text;

                if (checkIsDefault.Checked)
                {
                    foreach (var item in Customer.CustomerEmails)
                    {
                        item.IsDefault = false;
                    }
                }
                CustomerEmail.IsDefault = checkIsDefault.Checked;

                Customer.CustomerEmails.Add(CustomerEmail);
                CustomerEmail.Save();
            }
            else
            {
                if (addres != null)
                {
                    CustomerTelephone.Address = addres;
                }
                else
                {
                    CustomerTelephone.Address = null;
                }

                if (position != null)
                {
                    CustomerTelephone.Position = position;
                }
                else
                {
                    CustomerTelephone.Position = null;
                }

                CustomerTelephone.FullName = txtFullName.Text;
                CustomerTelephone.Surname = txtSurname.Text;
                CustomerTelephone.Name = txtName.Text;
                CustomerTelephone.Patronymic = txtPatronymic.Text;
                CustomerTelephone.Telephone = txtTelephone.Text;
                CustomerTelephone.Comment = memoComment.Text;

                if (checkIsDefault.Checked)
                {
                    foreach (var item in Customer.CustomerTelephones)
                    {
                        item.IsDefault = false;
                    }
                }
                CustomerTelephone.IsDefault = checkIsDefault.Checked;

                Customer.CustomerTelephones.Add(CustomerTelephone);
                CustomerTelephone.Save();
            }
            
            Close();
        }

        private void ContactEdit_Load(object sender, EventArgs e)
        {
            if (IsEmail)
            {
                txtFullName.Text = CustomerEmail.FullName;
                txtSurname.Text = CustomerEmail.Surname;
                txtName.Text = CustomerEmail.Name;
                txtPatronymic.Text = CustomerEmail.Patronymic;
                txtEmail.Text = CustomerEmail.Email;
                txtAddress.EditValue = CustomerEmail.Address;
                txtPosition.EditValue = CustomerEmail.Position;
                memoComment.EditValue = CustomerEmail.Comment;
                checkIsDefault.Checked = CustomerEmail.IsDefault;

                gpEmail.Visible = true;
            }
            else
            {
                txtFullName.Text = CustomerTelephone.FullName;
                txtSurname.Text = CustomerTelephone.Surname;
                txtName.Text = CustomerTelephone.Name;
                txtPatronymic.Text = CustomerTelephone.Patronymic;
                txtTelephone.Text = CustomerTelephone.Telephone;
                txtAddress.EditValue = CustomerTelephone.Address;
                txtPosition.EditValue = CustomerTelephone.Position;
                memoComment.EditValue = CustomerTelephone.Comment;
                checkIsDefault.Checked = CustomerTelephone.IsDefault;

                gpTelephone.Visible = true;
            }
        }

        private void txtPosition_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            cls_BaseSpr.ButtonEditButtonClickBase<Position>(Session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.Position, 1, null, null, false, null, string.Empty, false, true);
        }

        private void txtAddress_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var address = ((ButtonEdit)sender).EditValue as Address;

            if (address is null)
            {
                address = new Address(Session);
            }
            
            var formAddressFIASEdit = new AddressFIASEdit(address);
            formAddressFIASEdit.ShowDialog();
            
            ((ButtonEdit)sender).EditValue = formAddressFIASEdit.Address;
        }
    }
}