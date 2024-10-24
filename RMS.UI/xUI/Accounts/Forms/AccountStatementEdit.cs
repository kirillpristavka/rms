using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using PulsLibrary.Extensions.DevForm;
using PulsLibrary.Extensions.DevXpo;
using PulsLibrary.Extensions.GeneralType;
using PulsLibrary.Methods;
using RMS.Core.Controllers.Accounts;
using RMS.Core.Controllers.Customers;
using RMS.Core.Controllers.Staffs;
using RMS.Core.Enumerator;
using RMS.Core.Model;
using RMS.Core.Model.Accounts;
using RMS.Core.Model.InfoCustomer;
using RMS.UI.Forms.Directories;
using System;
using System.Drawing;

namespace RMS.UI.xUI.Accounts.Forms
{
    public partial class AccountStatementEdit : XtraForm
    {
        private UnitOfWork _uof;

        public int Id { get; private set; }
        public bool IsSave { get; private set; }
        public AccountStatement AccountStatement { get; private set; }

        public AccountStatementEdit(UnitOfWork uof = null)
        {
            InitializeComponent();
            Icon = Properties.Resources.IconRMS;

            if (uof is null)
            {
                uof = new UnitOfWork();
            }
            _uof = uof;
        }
        
        public AccountStatementEdit(int id) : this()
        {
            Id = id;
        }

        public AccountStatementEdit(object obj, UnitOfWork uof = null) : this(uof)
        {
            if (obj is AccountStatement accountStatement)
            {
                Id = accountStatement.Oid;
                AccountStatement = accountStatement;
            }
        }

        public AccountStatementEdit(AccountStatement obj) : this(obj.Session)
        {
            Id = obj.Oid;
            AccountStatement = obj;
        }

        /// <summary>
        /// Заполнение объектов на форме.
        /// </summary>
        /// <returns></returns>
        private async System.Threading.Tasks.Task FillingFormObjectsAsync()
        {
            cmbCustomer.Properties.Items.AddRange(await CustomerController.GetCustomersAsync(_uof));

            cmbStaff.Properties.Items.AddRange(await StaffController.GetStaffsAsync(_uof));

            var status = await new XPQuery<AccountStatementStatus>(_uof)?.ToListAsync();
            cmbAccountStatementStatus.Properties.Items.AddRange(status);

            cmbMonth.AddItemsFromEnum<Month>();
            cmbMonth.SelectedIndex = 0;
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            await FillingFormObjectsAsync();

            if (AccountStatement is null)
            {
                var obj = await _uof.GetObjectByKeyAsync<AccountStatement>(Id);
                if (obj is null)
                {
                    obj = new AccountStatement(_uof)
                    {
                        Year = DateTime.Now.Year
                    };
                }
                
                AccountStatement = obj;
            }           
            
            FillingOutTheEditForm(AccountStatement);
        }

        /// <summary>
        /// Заполнение формы редактирования.
        /// </summary>
        /// <param name="counterparty">Объект заполнения.</param>
        private void FillingOutTheEditForm(AccountStatement accountStatement)
        {
            if (accountStatement is null)
            {
                accountStatement = new AccountStatement(_uof)
                {
                    Year = DateTime.Now.Year
                };
            }

            cmbCustomer.EditValue = accountStatement.Customer;
            cmbStaff.EditValue = accountStatement.Staff;
            txtYear.EditValue = accountStatement.Year;
            cmbMonth.EditValue = accountStatement.Month.GetDescription();
            cmbAccountStatementStatus.EditValue = accountStatement.AccountStatementStatus;
            cmbAccount.EditValue = accountStatement.Account;
            txtAccountBank.EditValue = accountStatement.AccountScore;
            txtAccountBank.EditValue = accountStatement.AccountBank;
            dateDischarge.EditValue = accountStatement.DateDischarge;
            spinValue.EditValue = accountStatement.Value ?? 0;
            memoDescription.EditValue = accountStatement.Description;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            var customer = await _uof.GetObjectByKeyFromValueAsync<Customer>(cmbCustomer.EditValue);
            if (customer == null)
            {
                DevXtraMessageBox.ShowXtraMessageBox("Для сохранения необходимо указать клиента.", cmbCustomer);
                return;
            }

            var staff = await _uof.GetObjectByKeyFromValueAsync<Staff>(cmbStaff.EditValue);
            if (staff == null)
            {
                DevXtraMessageBox.ShowXtraMessageBox("Для сохранения необходимо указать сотрудника.", cmbStaff);
                return;
            }

            var accountStatementStatus = await _uof.GetObjectByKeyFromValueAsync<AccountStatementStatus>(cmbAccountStatementStatus.EditValue); 
            if (accountStatementStatus == null)
            {
                DevXtraMessageBox.ShowXtraMessageBox("Для сохранения необходимо указать статус.", cmbAccountStatementStatus);
                return;
            }

            var account = await _uof.GetObjectByKeyFromValueAsync<Account>(cmbAccount.EditValue);
            if (account == null)
            {
                DevXtraMessageBox.ShowXtraMessageBox("Для сохранения необходимо указать счет.", cmbAccount);
                return;
            }

            var year = default(int?);
            if (!string.IsNullOrWhiteSpace(txtYear.Text))
            {
                year = Objects.GetIntObject(txtYear.EditValue);
                if (year == 0)
                {
                    year = default;
                }
            }
            
            var month = cmbMonth.GetEnumItem<Month>();
            var dateDischarge = Objects.GetRequiredObject<DateTime?>(this.dateDischarge.EditValue);
            var description = Objects.GetRequiredObject<string>(memoDescription.EditValue);
            var value = Objects.GetDecimalObject(spinValue.EditValue);

            var accountStatement = new AccountStatement()
            {
                Staff = staff,
                Customer = customer,
                Account = account,
                Month = month,
                DateDischarge = dateDischarge,
                AccountStatementStatus = accountStatementStatus,
                Description = description,
                Value = value,
                Year = year
            };

            if (AccountStatement is null)
            {
                AccountStatement = new AccountStatement(_uof);
            }

            AccountStatement.Edit(accountStatement);
            await _uof.CommitTransactionAsync();
            Id = AccountStatement.Oid;
            IsSave = true;

            Close();
        }        

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmbCustomer_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = Objects.GetRequiredObject<ButtonEdit>(sender);
            if (buttonEdit != null)
            {
                if (e.Button.Kind == ButtonPredefines.Delete)
                {
                    buttonEdit.EditValue = null;
                }
                else if (e.Button.Kind == ButtonPredefines.Ellipsis)
                {
                    cls_BaseSpr.ButtonEditButtonClickBase<Customer>(null, buttonEdit, (int)cls_App.ReferenceBooks.Customer, 1, null, null, false, null, string.Empty, false, true);
                }
            }
        }
        private async void cmbCustomer_EditValueChanged(object sender, EventArgs e)
        {
            if (sender is ComboBoxEdit cmb)
            {
                if (cmb.EditValue is Customer customer)
                {
                    cmbAccount.Properties.Items.Clear();
                    cmbAccount.Properties.Items.AddRange(await AccountController.GetAccountsCustomerAsync(_uof, customer.Oid));
                }
            }
        }

        private void cmbStaff_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = Objects.GetRequiredObject<ButtonEdit>(sender);
            if (buttonEdit != null)
            {
                if (e.Button.Kind == ButtonPredefines.Delete)
                {
                    buttonEdit.EditValue = null;
                }
                else if (e.Button.Kind == ButtonPredefines.Ellipsis)
                {
                    cls_BaseSpr.ButtonEditButtonClickBase<Staff>(null, buttonEdit, (int)cls_App.ReferenceBooks.Staff, 1, new NullOperator(nameof(Staff.DateDismissal)), null, false, null, string.Empty, false, true);
                }
            }
        }

        private void cmbAccountStatementStatus_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = Objects.GetRequiredObject<ButtonEdit>(sender);
            if (buttonEdit != null)
            {
                if (e.Button.Kind == ButtonPredefines.Delete)
                {
                    buttonEdit.EditValue = null;
                }
                else if (e.Button.Kind == ButtonPredefines.Ellipsis)
                {
                    cls_BaseSpr.ButtonEditButtonClickBase<AccountStatementStatus>(null, buttonEdit, (int)cls_App.ReferenceBooks.AccountStatementStatus, 1, null, null, false, null, string.Empty, false, true);
                }
            }
        }

        private void cmbAccountStatementStatus_EditValueChanged(object sender, EventArgs e)
        {
            if (sender is ComboBoxEdit cmb)
            {
                if (cmb.EditValue is AccountStatementStatus status)
                {
                    if (!string.IsNullOrWhiteSpace(status.Color))
                    {
                        var color = ColorTranslator.FromHtml(status.Color);

                        cmb.BackColor = color;
                        layoutControlItemAccountStatementStatus.AppearanceItemCaption.BackColor = color;
                    }
                }
                else
                {
                    cmb.BackColor = default;
                    layoutControlItemAccountStatementStatus.AppearanceItemCaption.BackColor = default;
                }
            }
        }

        private void cmbAccount_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = Objects.GetRequiredObject<ButtonEdit>(sender);
            if (buttonEdit != null)
            {
                if (e.Button.Kind == ButtonPredefines.Delete)
                {
                    buttonEdit.EditValue = null;
                }
                else if (e.Button.Kind == ButtonPredefines.Ellipsis)
                {
                    if (buttonEdit.EditValue is Account account)
                    {
                        var form = new AccountEdit(account);
                        form.ShowDialog();
                        cmbAccount_EditValueChanged(sender, null);
                        txtAccountScore.Focus();
                    }
                    //cls_BaseSpr.ButtonEditButtonClickBase<Account>(null, buttonEdit, (int)cls_App.ReferenceBooks.Account, 1, null, null, false, null, string.Empty, false, true);
                }
            }
        }

        private void cmbAccount_EditValueChanged(object sender, EventArgs e)
        {
            if (sender is ComboBoxEdit cmb)
            {
                if (cmb.EditValue is Account account)
                {
                    txtAccountScore.EditValue = account.AccountNumber;
                    txtAccountBank.EditValue = account.Bank;
                    txtAccountCurrencyISO.EditValue = account.CurrencyISO;
                    memoAccountDescription.EditValue = account.Description;
                }
                else
                {
                    txtAccountScore.EditValue = null;
                    txtAccountBank.EditValue = null;
                    txtAccountCurrencyISO.EditValue = null;
                    memoAccountDescription.EditValue = null;
                }
            }
        }

        private void spinValue_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (sender is SpinEdit spinEdit)
            {
                if (e.Button.Kind == ButtonPredefines.Delete)
                {
                    spinEdit.EditValue = null;
                }
            }
        }
    }
}