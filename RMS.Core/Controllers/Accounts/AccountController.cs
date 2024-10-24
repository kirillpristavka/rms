using DevExpress.Xpo;
using RMS.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMS.Core.Controllers.Accounts
{
    /// <summary>
    /// Контроллер управления выписками по расчетному счету.
    /// </summary>
    public static class AccountController
    {
        public static async Task<List<Account>> GetAccountsAsync()
        {
            using (var uof = new UnitOfWork())
            {
                return await new XPQuery<Account>(uof)?.ToListAsync();
            }
        }

        public static async Task<List<Account>> GetAccountsAsync(Session session)
        {
            return await new XPQuery<Account>(session).ToListAsync();
        }

        public static async Task<List<Account>> GetAccountsCustomerAsync(int oidCustomer, bool isCurrentlyForce = true)
        {
            using (var uof = new UnitOfWork())
            {
                var result = await new XPQuery<Account>(uof)
                    ?.Where(w => w.Customer != null && w.Customer.Oid == oidCustomer)
                    ?.ToListAsync();

                if (isCurrentlyForce)
                {
                    var dateNow = DateTime.Now.Date;
                    result = result
                        ?.Where(f => (f.OpeningDate == null && f.ClosingDate >= dateNow)
                            || (f.OpeningDate <= dateNow && f.ClosingDate >= dateNow)
                            || (f.OpeningDate <= dateNow && f.ClosingDate == null)
                            || (f.OpeningDate == null && f.ClosingDate == null))
                        ?.ToList();
                }

                return result;
            }
        }

        public static async Task<List<Account>> GetAccountsCustomerAsync(Session session, int oidCustomer, bool isCurrentlyForce = true)
        {
            var result = await new XPQuery<Account>(session)
                ?.Where(w => w.Customer != null && w.Customer.Oid == oidCustomer)
                ?.ToListAsync();

            if (isCurrentlyForce)
            {
                var dateNow = DateTime.Now.Date;
                result = result
                    ?.Where(f => (f.OpeningDate == null && f.ClosingDate >= dateNow)
                        || (f.OpeningDate <= dateNow && f.ClosingDate >= dateNow)
                        || (f.OpeningDate <= dateNow && f.ClosingDate == null)
                        || (f.OpeningDate == null && f.ClosingDate == null))
                    ?.ToList();
            }

            return result;
        }
    }
}
