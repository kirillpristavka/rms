using System;
using System.Collections.Generic;

namespace RMS.Core.ObjectDTO.Models
{
    public class RouteSheetDTO : BaseObjectDTO
    {
        public DateTime DateCreate { get; set; }
        public UserDTO UserCreate { get; set; }
        public DateTime DateUpdate { get; set; }
        public UserDTO UserUpdate { get; set; }
        public bool IsClosed { get; set; }
        public DateTime Date { get; set; }
        public decimal Remainder { get; set; }
        public decimal Value { get; set; }
        public decimal Spent { get; set; }
        public decimal SpentAshlessPayment { get; set; }
        public decimal Balance { get; set; }
        public string Comment { get; set; }

        public IEnumerable<TaskRouteSheetDTO> TaskRouteSheet { get; set; }
        public IEnumerable<RouteSheetPaymentDTO> Payments { get; set; }

        public override string ToString()
        {
            return $"Маршрутный лист от {Date.ToShortDateString()}";
        }
    }
}
