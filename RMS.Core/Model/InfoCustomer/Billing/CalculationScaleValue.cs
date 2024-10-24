using DevExpress.Xpo;

namespace RMS.Core.Model.InfoCustomer.Billing
{
    public class CalculationScaleValue : Scale
    {
        public CalculationScaleValue() { }
        public CalculationScaleValue(Session session) : base(session) {}

        [Association, MemberDesignTimeVisibility(false)]
        public CalculationScale CalculationScaleValues { get; set; }
    }
}