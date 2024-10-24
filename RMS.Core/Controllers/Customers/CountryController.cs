using DevExpress.Xpo;
using RMS.Core.Model;
using System.Collections.Generic;

namespace RMS.Core.Controllers.Customers
{
    public static class CountryController
    {
        private static List<Country> _countries;
        public async static System.Threading.Tasks.Task<List<Country>> GetCountriesAsync(
            Session session,
            bool isForceUpdate = false,
            bool isConfigureAwait = true)
        {
            if (_countries != null && isForceUpdate is false)
            {
                return _countries;
            }

            _countries = await new XPQuery<Country>(session).ToListAsync().ConfigureAwait(isConfigureAwait);
            return _countries;
        }
    }
}
