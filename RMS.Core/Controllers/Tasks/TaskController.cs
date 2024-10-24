using DevExpress.Xpo;
using RMS.Core.Model.InfoCustomer;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMS.Core.Controllers.NewFolder1
{
    public static class TaskController
    {
        public static async Task<IEnumerable<Model.Task>> GetTasksAsync()
        {
            using (var uof = new UnitOfWork())
            {
                return await new XPQuery<Model.Task>(uof)?.ToListAsync();
            }
        }

        public static async Task<IEnumerable<Model.Task>> GetTasksAsync(Session session)
        {
            return await new XPQuery<Model.Task>(session)?.ToListAsync();
        }

        public static async Task<IEnumerable<Model.Task>> GetTasksByCustomerAsync(Session session, Customer customer)
        {
            return await new XPQuery<Model.Task>(session)
                ?.Where(w => w.Customer != null 
                    && w.Customer.Oid == customer.Oid)
                ?.ToListAsync();
        }
    }
}
