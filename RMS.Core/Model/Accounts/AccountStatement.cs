using DevExpress.Xpo;
using RMS.Core.Enumerator;
using RMS.Core.Model.InfoCustomer;
using System;

namespace RMS.Core.Model.Accounts
{
    /// <summary>
    /// Выписка по расчетному счету.
    /// </summary>
    public class AccountStatement : XPObject
    {
        public AccountStatement() { }
        public AccountStatement(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Month = (Month)DateTime.Now.Month;
        }

        public string StaffName => Staff?.ToString();
        public Staff Staff { get; set; }

        public string CustomerName => Customer?.ToString();
        public Customer Customer { get; set; }

        public string AccountDescription => Account?.Description;
        public string AccountScore => Account?.AccountNumber;
        public string AccountCurrencyISO => Account?.CurrencyISO;
        public string AccountBank => Account?.Bank?.ToString();
        public Account Account { get; set; }

        public Month Month { get; set; }
        public int? Year { get; set; }

        /// <summary>
        /// Дата выписки.
        /// </summary>
        public DateTime? DateDischarge { get; set; }

        public decimal? Value { get; set; }

        public string Status => AccountStatementStatus?.ToString();
        public AccountStatementStatus AccountStatementStatus { get; set; }

        [Size(1024)]
        [DisplayName("Описание")]
        public string Description { get; set; }

        public void Edit(AccountStatement accountStatement)
        {
            if (accountStatement is null || accountStatement.Customer is null)
            {
                return;
            }

            Staff = accountStatement.Staff;
            Customer = accountStatement.Customer;
            Account = accountStatement.Account;
            Month = accountStatement.Month;
            Year = accountStatement.Year;
            DateDischarge = accountStatement.DateDischarge;
            AccountStatementStatus = accountStatement.AccountStatementStatus;
            Description = accountStatement.Description;
            Value = accountStatement.Value;

            Save();
        }

        public override string ToString()
        {
            var result = default(string);

            if (!string.IsNullOrWhiteSpace(AccountScore))
            {
                result += AccountScore;
            }

            if (DateDischarge is DateTime date)
            {
                result += $" от {date.ToShortDateString()}";
            }

            if (Customer != null)
            {
                result += $" ({Customer})";
            }

            return result?.Trim() ?? base.ToString();
        }
    }
}
