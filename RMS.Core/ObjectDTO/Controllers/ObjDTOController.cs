using AutoMapper;
using DevExpress.Xpo;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMS.Core.ObjectDTO.Controllers
{
    public static class ObjDTOController<TDTO, TEntity>
        where TDTO : class
        where TEntity : class
    {
        private static IEnumerable<TDTO> _objCollection;

        public static async Task<IEnumerable<TDTO>> GetItemsAsync(bool isForceUpdate = false)
        {
            if (isForceUpdate || _objCollection is null || _objCollection.Count() == 0)
            {
                var congif = new MapperConfiguration(cfg => {
                    cfg.CreateMap<TEntity, TDTO>();
                });

                var mapper = new Mapper(congif);

                using (var uof = new UnitOfWork())
                {
                    var collection = await new XPQuery<TEntity>(uof).ToListAsync();
                    _objCollection = mapper.Map<IEnumerable<TEntity>, List<TDTO>>(collection);
                }
            }

            return _objCollection;
        }
    }
}
