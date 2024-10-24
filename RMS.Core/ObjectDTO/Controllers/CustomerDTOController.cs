using AutoMapper;
using DevExpress.Xpo;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.ObjectDTO.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMS.Core.ObjectDTO.Controllers
{
    public static class CustomerDTOController
    {
        private static IEnumerable<CustomerDTO> _objCollection;

        public static async Task<IEnumerable<CustomerDTO>> GetCustomersAsync(bool isForceUpdate = false)
        {
            if (isForceUpdate || _objCollection is null || _objCollection.Count() == 0)
            {
                var congif = new MapperConfiguration(cfg => {
                    cfg.CreateMap<Customer, CustomerDTO>();                    
                });

                var mapper = new Mapper(congif);

                using (var uof = new UnitOfWork())
                {
                    var collection = await new XPQuery<Customer>(uof).ToListAsync();
                    _objCollection = mapper.Map<IEnumerable<Customer>, List<CustomerDTO>>(collection);
                }
            }

            return _objCollection;
        }
    }
}
