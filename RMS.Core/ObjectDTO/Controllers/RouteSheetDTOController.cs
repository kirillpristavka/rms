using AutoMapper;
using DevExpress.Xpo;
using RMS.Core.Extensions;
using RMS.Core.Model;
using RMS.Core.Model.CourierService;
using RMS.Core.ObjectDTO.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMS.Core.ObjectDTO.Controllers
{
    public static class RouteSheetDTOController
    {
        public static IEnumerable<RouteSheetDTO> _routeSheetDTOs;

        public static async Task<IEnumerable<RouteSheetDTO>> GetRouteSheetsAsync(bool isForceUpdate = false)
        {
            if (isForceUpdate || _routeSheetDTOs is null || _routeSheetDTOs.Count() == 0)
            {
                var congif = new MapperConfiguration(cfg => {
                    cfg.CreateMap<Staff, StaffDTO>();
                    cfg.CreateMap<User, UserDTO>();
                    cfg.CreateMap<CostItem, CostItemDTO>();
                    cfg.CreateMap<RouteSheetPaymentv2, RouteSheetPaymentDTO>()
                        .ForMember(dest => dest.RouteSheet, act => act.MapFrom(src => src.RouteSheetv2));
                    cfg.CreateMap<TaskRouteSheetv2, TaskRouteSheetDTO>();
                    cfg.CreateMap<RouteSheetv2, RouteSheetDTO>()
                        .ForMember(dest => dest.TaskRouteSheet, act => act.MapFrom(src => src.TaskRouteSheetv2));
                });

                var mapper = new Mapper(congif);

                using (var uof = new UnitOfWork())
                {
                    var collection = await new XPQuery<RouteSheetv2>(uof).OrderBy(o => o.Date).ToListAsync();
                    _routeSheetDTOs = mapper.Map<IEnumerable<RouteSheetv2>, List<RouteSheetDTO>>(collection);

                    foreach (var routeSheet in _routeSheetDTOs)
                    {
                        routeSheet.Remainder = routeSheet.GetRemainder();
                        routeSheet.Spent = routeSheet.GetSpent();
                        routeSheet.SpentAshlessPayment = routeSheet.GetSpentAshlessPayment();
                        routeSheet.Balance = routeSheet.GetBalance();
                    }
                }
            }

            return _routeSheetDTOs;
        }

        private static decimal GetBalance(this RouteSheetDTO routeSheetDTO)
        {
            return routeSheetDTO.Remainder + routeSheetDTO.Value - routeSheetDTO.Spent;
        }

        private static decimal GetRemainder(this RouteSheetDTO routeSheetDTO)
        {
            var routeSheetDTOs = _routeSheetDTOs.Where(w => w.Date < routeSheetDTO.Date);
            if (routeSheetDTOs != null && routeSheetDTOs.Count() > 0)
            {
                var maxDate = routeSheetDTOs.Max(m => m.Date);
                return routeSheetDTOs.LastOrDefault(l => l.Date == maxDate)?.Balance ?? default;
            }

            return default;
        }

        private static decimal GetSpent(this RouteSheetDTO routeSheetDTO)
        {
            var result = 0.00M;

            if (routeSheetDTO.Payments != null && routeSheetDTO.Payments.Count() > 0)
            {
                var sumPayments = routeSheetDTO.Payments
                    .Where(w => w.TypePayment is null || w.TypePayment == Enumerator.TypePayment.CashPayment)
                    .Sum(s => s.Value);
                result += sumPayments.GetDecimalRound();
            }

            if (routeSheetDTO.TaskRouteSheet != null && routeSheetDTO.TaskRouteSheet.Count() > 0)
            {
                var sumPayments = routeSheetDTO.TaskRouteSheet.Sum(s => s.Value);
                result += sumPayments.GetDecimalRound();
            }

            return result;
        }

        private static decimal GetSpentAshlessPayment(this RouteSheetDTO routeSheetDTO)
        {
            var result = 0.00M;

            if (routeSheetDTO.Payments != null && routeSheetDTO.Payments.Count() > 0)
            {
                var sumPayments = routeSheetDTO.Payments
                    .Where(w => w.TypePayment == Enumerator.TypePayment.AshlessPayment)
                    .Sum(s => s.Value);
                result += sumPayments.GetDecimalRound();
            }

            if (routeSheetDTO.TaskRouteSheet != null && routeSheetDTO.TaskRouteSheet.Count() > 0)
            {
                var sumPayments = routeSheetDTO.TaskRouteSheet.Sum(s => s.ValueNonCash);
                result += sumPayments.GetDecimalRound();
            }

            return result;
        }
    }
}
