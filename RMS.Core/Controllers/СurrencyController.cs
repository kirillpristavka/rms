using DevExpress.Xpo;
using RMS.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RMS.Core.Controllers
{
    public static class СurrencyController
    {
        public static async Task<IEnumerable<Currency>> GetCurrenciesAsync()
        {
            using (var uof = new UnitOfWork())
            {
                return await new XPQuery<Currency>(uof)?.ToListAsync();
            }
        }

        public static async Task<IEnumerable<Currency>> GetCurrenciesAsync(Session session)
        {
            return await new XPQuery<Currency>(session)?.ToListAsync();
        }
    }
}
