using DevExpress.Data.Filtering;
using DevExpress.Xpo;

namespace RMS.Core.Model
{
    /// <summary>
    /// Платежи по маршрутному листу.
    /// </summary>
    public class RouteSheetPayment : Payment
    {
        public RouteSheetPayment() { }
        public RouteSheetPayment(Session session) : base(session) { }

        [Association, MemberDesignTimeVisibility(false)]
        public RouteSheet RouteSheet { get; set; }

        public override string Description => CostItem?.ToString();

        [MemberDesignTimeVisibility(false)]
        public CostItem CostItem { get; set; }
    }
}