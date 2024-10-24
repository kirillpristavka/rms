using DevExpress.Xpo;
using RMS.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RMS.Core.Controllers.Letters
{
    /// <summary>
    /// Контроллер управления каталогами почтовыx сообщений.
    /// </summary>
    public static class LetterCatalogsController
    {
        public static async Task<List<LetterCatalog>> GetLetterCatalogsAsync()
        {
            using (var uof = new UnitOfWork())
            {
                return await new XPQuery<LetterCatalog>(uof).ToListAsync();
            }
        }

        public static async Task<List<LetterCatalog>> GetLetterCatalogsAsync(Session session)
        {
            return await new XPQuery<LetterCatalog>(session).ToListAsync();
        }
    }
}
