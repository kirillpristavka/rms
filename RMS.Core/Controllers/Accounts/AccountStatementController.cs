using DevExpress.Xpo;
using RMS.Core.Controllers.Customers;
using RMS.Core.Enumerator;
using RMS.Core.Model.Accounts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMS.Core.Controllers.Accounts
{
    /// <summary>
    /// Контроллер управления выписками по расчетному счету.
    /// </summary>
    public static class AccountStatementController
    {
        public static async Task<AccountStatement> GetAccountStatementAsync(Session uof, AccountStatement accountStatement)
        {
            return await new XPQuery<AccountStatement>(uof)
                    ?.FirstOrDefaultAsync(f =>
                        f.Staff == accountStatement.Staff
                        && f.Customer == f.Customer
                        && f.Account == f.Account
                        && f.Month == accountStatement.Month
                        && f.Year == accountStatement.Year);
        }

        public static async Task<IEnumerable<AccountStatement>> GetAccountsStatementAsync()
        {
            using (var uof = new UnitOfWork())
            {
                return await new XPQuery<AccountStatement>(uof)?.ToListAsync();
            }
        }

        public static async Task<IEnumerable<AccountStatement>> GetAccountsStatementAsync(Session session)
        {
            return await new XPQuery<AccountStatement>(session)?.ToListAsync();
        }

        public static async Task<IEnumerable<AccountStatement>> GetAccountsStatementAsync(Session session, Month? month, int? year)
        {
            var result = await new XPQuery<AccountStatement>(session)?.ToListAsync();

            if (month != null)
            {
                result = result.Where(w => w.Month == month).ToList();
            }

            if (year != null)
            {
                result = result.Where(w => w.Year == year).ToList();
            }

            return result;
        }

        public static async Task CreateAccountsStatementAsync(Month month, int? year = default)
        {
            using (var uof = new UnitOfWork())
            {
                var customers = await CustomerController.GetCustomersAsync(uof, isForceUpdate: true);
                if (customers != null)
                {
                    var dateTimeNow = System.DateTime.Now;
                    foreach (var customer in customers)
                    {
                        if (string.IsNullOrWhiteSpace(customer.StatusString)
                            || customer.StatusString.Equals("не работаем", System.StringComparison.OrdinalIgnoreCase)
                            || customer.StatusString.Contains("Не работаем"))
                        {

                            if (customer.CustomerStatus?.Date is System.DateTime statusDate)
                            {
                                if (dateTimeNow.Year <= statusDate.Year && dateTimeNow.Month <= statusDate.Month) 
                                {
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                continue;
                            }
                        }

                        var accounts = await AccountController.GetAccountsCustomerAsync(uof, customer.Oid);
                        if (accounts != null)
                        {
                            foreach (var account in accounts)
                            {
                                if (string.IsNullOrWhiteSpace(account.AccountNumber))
                                {
                                    continue;
                                }

                                var accountStatement = new AccountStatement(uof)
                                {
                                    Staff = customer.PrimaryResponsible ?? customer.AccountantResponsible,
                                    Customer = customer,
                                    Account = account,
                                    Year = year,
                                    Month = month,
                                    AccountStatementStatus = await new XPQuery<AccountStatementStatus>(uof)?.FirstOrDefaultAsync(f => f.IsDefault)
                                };

                                if (await GetAccountStatementAsync(uof, accountStatement) is null)
                                {
                                    accountStatement.Save();
                                    await uof.CommitTransactionAsync();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
