using DevExpress.Xpo;

namespace RMS.Core.Model.CourierService
{
    /// <summary>
    /// Платежи по маршрутному листу.
    /// </summary>
    public class RouteSheetPaymentv2 : Payment
    {
        public RouteSheetPaymentv2() { }
        public RouteSheetPaymentv2(Session session) : base(session) { }
        
        [Association]
        [MemberDesignTimeVisibility(false)]
        public RouteSheetv2 RouteSheetv2 { get; set; }

        public override string Description => CostItem?.ToString();

        [MemberDesignTimeVisibility(false)]
        public CostItem CostItem { get; set; }

        public override string ToString()
        {
            var result = default(string);

            if (!string.IsNullOrWhiteSpace(Description))
            {
                result += $"{Description} ";
            }

            result += $"{Value:n2}";
            
            if (string.IsNullOrWhiteSpace(result))
            {
                return base.ToString();
            }
            else
            {
                return result;
            }
        }
    }
}