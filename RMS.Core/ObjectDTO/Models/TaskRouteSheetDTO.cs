using System;

namespace RMS.Core.ObjectDTO.Models
{
    public class TaskRouteSheetDTO
    {
        public DateTime DateCreate { get; set; }
        public bool IsUseRouteSheet { get; set; }
        public UserDTO UserCreate { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? DateTransfer { get; set; }
        public string AccountantResponsibleString { get; set; }
        public string CustomerString { get; set; }
        public string CourierString { get; set; }
        public string Address { get; set; }
        public string PurposeTrip { get; set; }
        public string StatusTaskCourierString { get; set; }
        public decimal Value { get; set; }
        public decimal ValueNonCash { get; set; }
        public RouteSheetDTO RouteSheetv2 { get; set; }
    }
}