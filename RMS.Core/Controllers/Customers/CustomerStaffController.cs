using DevExpress.Xpo;
using RMS.Core.Model.InfoCustomer;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMS.Core.Controllers.Customers
{
    public static class CustomerStaffController
    {
        private async static Task<List<CustomerStaff>> GetCustomersStaffAsync(Session session)
        {
            return await new XPQuery<CustomerStaff>(session)?.ToListAsync();
        }
        
        public async static Task<List<CustomerStaff>> GetCustomersStaffAsync(Session session, object obj = null)
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
                return await GetCustomersStaffAsync(session);
            }
            
            return await new XPQuery<CustomerStaff>(session)
                ?.Where(w => w.Customer != null && w.Customer.Oid == customerOid)
                ?.ToListAsync();
        }
    }
}
