using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using RMS.Core.Controllers;
using RMS.Core.Controllers.Banks;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using System;
using System.Linq;

namespace RMS.UI.Forms.Directories
{
    public partial class AccountEdit : XtraForm
    {
        private Session _session;
        private Customer _customer;
        private Account _account;
        private BankAccess _bankAccess;

        private AccountEdit()
        {
            InitializeComponent();
        }

        public AccountEdit(Customer customer) : this()
        {
            _customer = customer;
            _session = customer.Session;
            _account = new Account(_session);
            _bankAccess = _account.BankAccess ?? new BankAccess(_session);
        }

        public AccountEdit(Account account) : this()
        {
            _account = account;
            _customer = account.Customer;
            _session = account.Session;
            _bankAccess = _account.BankAccess ?? new BankAccess(_session);
        }

        /// <summary>
        /// Заполнение объектов на форме.
        /// </summary>
        /// <returns></returns>
        private async System.Threading.Tasks.Task FillingFormObjectsAsync()
        {
            var banks = await BankController.GetBanksAsync(_session);
            cmbBank.Properties.Items.AddRange(banks?.ToArray());

            var currencies = await СurrencyController.GetCurrenciesAsync(_session);
            cmbCurrency.Properties.Items.AddRange(currencies?.ToArray());
        }

        private async void AccountEdit_Load(object sender, System.EventArgs e)
        {
            await FillingFormObjectsAsync();

            txtDescription.Text = _account.Description;
            txtAccountNumber.Text = _account.AccountNumber;
            txtBankCardNumber.Text = _account.BankCardNumber;
            dateOpening.EditValue = _account.OpeningDate ?? null;
            cmbBank.EditValue = _account.Bank;
            cmbCurrency.EditValue = _account.Currency;
            txtBankBIC.Text = _account.Bank?.BIC;
            txtBankName.Text = _account.Bank?.PaymentName;
            dateClosing.EditValue = _account.ClosingDate;
            txtCorrespondentAccountNumber.Text = _account.CorrespondentAccountNumber;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var accountNumber = txtAccountNumber.Text?.Trim();
            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                PulsLibrary.Extensions.DevForm.DevXtraMessageBox.ShowXtraMessageBox("Невозможно сохранение формы без указания счета.", txtAccountNumber);
                return;
            }

            _account.Description = txtDescription.Text;
            _account.AccountNumber = accountNumber;
            _account.BankCardNumber = txtBankCardNumber.Text;
            _account.OpeningDate = dateOpening.DateTime == DateTime.MinValue ? default(DateTime?) : Convert.ToDateTime(dateOpening.EditValue);
            _account.ClosingDate = dateClosing.DateTime == DateTime.MinValue ? default(DateTime?) : Convert.ToDateTime(dateClosing.EditValue);
            _account.CorrespondentAccountNumber = txtCorrespondentAccountNumber.Text;
            _account.BankAccess = _bankAccess;

            var bank = cmbBank.EditValue as Bank;
            if (bank != null)
            {
                _account.Bank = bank;
            }
            else
            {
                _account.Bank = null;
            }

            if (cmbCurrency.EditValue is Currency currency)
            {
                _account.Currency = currency;
            }
            else
            {
                _account.Currency = null;
            }

            _customer.Accounts.Add(_account);

            _bankAccess.Bank = bank;
            _bankAccess.Description = txtDescription.Text;
            _bankAccess.Account = _account;

            if (_customer.BankAccess.FirstOrDefault(f => f.Oid == _bankAccess.Oid) is null)
            {
                _customer.BankAccess.Add(_bankAccess);
            }
            else
            {
                _bankAccess.Save();
            }

            _customer.Save();
            Close();
        }

        private void cmbBank_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (sender is ComboBoxEdit cmb)
            {
                if (e?.Button?.Kind == ButtonPredefines.Delete)
                {
                    cmb.EditValue = null;
                }
                else if (e?.Button?.Kind == ButtonPredefines.Ellipsis)
                {
                    cls_BaseSpr.ButtonEditButtonClickBase<Bank>(_session, cmb, (int)cls_App.ReferenceBooks.Bank, 1, null, null, false, null, string.Empty, null, null, null);
                }
            }
        }

        private void cmbBank_EditValueChanged(object sender, EventArgs e)
        {
            if (sender is ComboBoxEdit cmb)
            {
                if (cmb.EditValue is Bank bank)
                {
                    cmb.ToolTip = bank.CorrespondentAccount;
                    txtBankBIC.EditValue = bank.BIC;
                    txtBankName.EditValue = bank.PaymentName;
                    txtCorrespondentAccountNumber.EditValue = bank.CorrespondentAccount;
                }
                else
                {
                    cmb.ToolTip = null;
                    txtBankBIC.EditValue = null;
                    txtBankName.EditValue = null;
                    txtCorrespondentAccountNumber.EditValue = null;
                }
            }
        }

        private void cmbCurrency_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (sender is ComboBoxEdit cmb)
            {
                if (e?.Button?.Kind == ButtonPredefines.Delete)
                {
                    cmb.EditValue = null;
                }
                else if (e?.Button?.Kind == ButtonPredefines.Ellipsis)
                {
                    //cls_BaseSpr.ButtonEditButtonClickBase<Bank>(_session, cmb, (int)cls_App.ReferenceBooks.Currency, 1, null, null, false, null, string.Empty, null, null, null);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}