using RMS.Core.Extensions;
using RMS.Core.Model.Accounts;
using System.Collections.Generic;
using System.Linq;

namespace RMS.UI.xUI.Accounts.Controllers
{
    public class AccountStatementReportAccountBase
    {
        public AccountStatementReportAccountBase() { }

        public void SetCollection(IEnumerable<AccountStatement> collection)
        {
            AccountStatementReportAccounts = new List<AccountStatementReportAccount>();
            foreach (var accountStatement in collection)
            {
                AccountStatementReportAccounts.Add(new AccountStatementReportAccount(accountStatement));
            }
        }

        public List<AccountStatementReportAccount> AccountStatementReportAccounts { get; private set; }

        public decimal? TotalAmountCharged
        {
            get
            {
                if (AccountStatementReportAccounts != null)
                {
                    var sum1 = AccountStatementReportAccounts.Sum(s => s.Value1);
                    var sum2 = AccountStatementReportAccounts.Sum(s => s.Value2);
                    var sum3 = AccountStatementReportAccounts.Sum(s => s.Value3);
                    var sum4 = AccountStatementReportAccounts.Sum(s => s.Value4);
                    var sum5 = AccountStatementReportAccounts.Sum(s => s.Value5);

                    return (sum1 + sum2 + sum3 + sum4 + sum5).Value.GetDecimalRound();
                }

                return 0.00M;
            }
        }
        public decimal? PayoutPercentage
        {
            get
            {
                if (AccountStatementReportAccounts != null)
                {
                    var percent1 = AccountStatementReportAccounts.Max(s => s.Percent1);
                    var percent2 = AccountStatementReportAccounts.Max(s => s.Percent2);
                    var percent3 = AccountStatementReportAccounts.Max(s => s.Percent3);
                    var percent4 = AccountStatementReportAccounts.Max(s => s.Percent4);
                    var percent5 = AccountStatementReportAccounts.Max(s => s.Percent5);

                    return (percent1 + percent2 + percent3 + percent4 + percent5).Value.GetDecimalRound();
                }

                return 0.00M;
            }
        }
    }
}