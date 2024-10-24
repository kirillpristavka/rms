namespace RMS.Core.ObjectDTO.Models
{
    public class RouteSheetPaymentDTO : PaymentDTO
    {
        public RouteSheetDTO RouteSheet { get; set; }
        public CostItemDTO CostItem { get; set; }
    }
}