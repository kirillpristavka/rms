using DevExpress.Xpo;
using RMS.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RMS.Core.Controllers.Banks
{
    public static class BankController
    {
        public static async Task<IEnumerable<Bank>> GetBanksAsync()
        {
            using (var uof = new UnitOfWork())
            {
                return await new XPQuery<Bank>(uof)?.ToListAsync();
            }
        }

        public static async Task<IEnumerable<Bank>> GetBanksAsync(Session session)
        {
            return await new XPQuery<Bank>(session).ToListAsync();
        }
    }
}
