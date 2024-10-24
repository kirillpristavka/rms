using DevExpress.Xpo;
using RMS.Core.Model.Exchange;
using RMS.Core.Model.InfoCustomer;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMS.Core.Controllers.Customers
{
    public static class CustomerImportEEUController
    {
        private async static Task<List<ImportEEU>> GetCustomersImportEEUAsync(Session session)
        {
            return await new XPQuery<ImportEEU>(session)?.ToListAsync();
        }

        public async static Task<List<ImportEEU>> GetCustomersImportEEUAsync(Session session, object obj = null)
        {
            var customerOid = -1;

            if (obj is Customer customer)
            {
                customerOid = customer.Oid;
            }
            else if (int.TryParse(obj?.ToString(), out int result))
            {
                customerOid = result;
            }

            if (customerOid == -1)
            {
                return await GetCustomersImportEEUAsync(session);
            }

            return await new XPQuery<ImportEEU>(session)
                ?.Where(w => w.Customer != null && w.Customer.Oid == customerOid)
                ?.ToListAsync();
        }
    }
}
