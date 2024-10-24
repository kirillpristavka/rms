using RMS.Core.Extensions;
using RMS.Core.Model.Accounts;

namespace RMS.UI.xUI.Accounts.Controllers
{
    public class AccountStatementReportAccount
    {
        public AccountStatementReportAccount(AccountStatement accountStatement)
        {
            Id = accountStatement.Oid;
            Period = $"{accountStatement.Month.GetEnumDescription()} {accountStatement.Year}"?.Trim();
            Responsible = accountStatement.Customer?.AccountantResponsible?.ToString()?.Trim();
            CounterParty = accountStatement.Customer?.ToString()?.Trim();
            CounterPartyInn = accountStatement.Customer?.INN?.ToString()?.Trim();

            Percent1 = accountStatement.Customer.PercentAccountantResponsible ?? 0.00M;
            Percent2 = accountStatement.Customer.PercentPrimaryResponsible ?? 0.00M;
            Percent3 = accountStatement.Customer.PercentBankResponsible ?? 0.00M;
            Percent4 = accountStatement.Customer.PercentSalaryResponsible ?? 0.00M;
            Percent5 = accountStatement.Customer.PercentAdministrator ?? 0.00M;
        }

        public int Id { get; private set; }
        public string Period { get; private set; }
        public string Responsible { get; private set; }
        public string CounterParty { get; private set; }
        public string CounterPartyInn { get; private set; }
        public decimal? Value { get; set; } = 0.00M;
        public string Level { get; set; }

        public decimal? Percent1 { get; set; }
        public decimal? Percent2 { get; set; }
        public decimal? Percent3 { get; set; }
        public decimal? Percent4 { get; set; }
        public decimal? Percent5 { get; set; }

        public decimal? Value1 => GetValue(Value, Percent1);
        public decimal? Value2 => GetValue(Value, Percent2);
        public decimal? Value3 => GetValue(Value, Percent3);
        public decimal? Value4 => GetValue(Value, Percent4);
        public decimal? Value5 => GetValue(Value, Percent5);

        private decimal? GetValue(decimal? obj, decimal? percent)
        {
            var result = obj * percent / 100;
            if (result != null)
            {
                return result.Value.GetDecimalRound();
            }

            return 0.00M;
        }
    }
}
