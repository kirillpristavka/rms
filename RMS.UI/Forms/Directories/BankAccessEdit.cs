using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using System;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Forms.Directories
{
    public partial class BankAccessEdit : formEdit_BaseSpr
    {
        private Session Session { get; }
        private BankAccess BankAccess { get; }
        private Customer Customer { get; }
        private Account Account { get; set; }

        private BankAccessEdit()
        {
            InitializeComponent();
        }

        public BankAccessEdit(Customer customer) : this()
        {
            Customer = customer;
            Session = customer.Session;
            BankAccess = new BankAccess(Session);
            Account = BankAccess.Account ?? new Account(Session);
        }

        public BankAccessEdit(BankAccess bankAccess) : this()
        {
            BankAccess = bankAccess;
            Customer = bankAccess.Customer;
            Session = bankAccess.Session;
            Account = BankAccess.Account ?? new Account(Session);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var bank = btnBank.EditValue as Bank;
            if (bank != null)
            {
                BankAccess.Bank = bank;
            }
            else
            {
                XtraMessageBox.Show("Без выбранного банка сохранение невозможно.",
                    "Ошибка сохранения",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                btnBank.Focus();
                return;
            }

            BankAccess.Description = txtDescription.Text;
            BankAccess.Login = txtLogin.Text;
            BankAccess.Password = txtPassword.Text;
            BankAccess.Link = txtLink.Text;
            BankAccess.Comment = memoComment.Text;
            BankAccess.Account = Account;
            Customer.BankAccess.Add(BankAccess);

            Account.Bank = bank;
            Account.Description = txtDescription.Text;
            Account.BankAccess = BankAccess;

            if (Customer.Accounts.FirstOrDefault(f => f.Oid == Account.Oid) is null)
            {
                Customer.Accounts.Add(Account);
            }
            else
            {
                Account.Save();
            }
            
            Customer.Save();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BankEdit_Load(object sender, EventArgs e)
        {
            txtDescription.Text = BankAccess.Description;
            txtLogin.Text = BankAccess.Login;
            txtPassword.Text = BankAccess.Password;
            txtLink.Text = BankAccess.Link;
            btnBank.EditValue = BankAccess.Bank;
            memoComment.Text = BankAccess.Comment;
        }

        private void txtBank_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<Bank>(Session, buttonEdit, (int)cls_App.ReferenceBooks.Bank, 1, null, null, false, null, string.Empty, false, true);
        }

        private void txtLink_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }
            else if (e.Button.Kind == ButtonPredefines.Right)
            {
                var result = buttonEdit.Text;
                if (!string.IsNullOrWhiteSpace(result))
                {
                    if (!result.Contains("http://") || !result.Contains("https://") || !result.Contains("www."))
                    {
                        result = $"http://{result}";
                    }                    
                    System.Diagnostics.Process.Start(result);
                }
            }
        }
    }
}