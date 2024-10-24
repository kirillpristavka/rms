using DevExpress.Xpo;

namespace RMS.Core.Model.InfoCustomer.Billing
{
    public class BillingGroupPerformanceIndicator : XPObject
    {
        public BillingGroupPerformanceIndicator() { }
        public BillingGroupPerformanceIndicator(Session session) : base(session) { }

        public GroupPerformanceIndicator GroupPerformanceIndicator { get; set; }

        /// <summary>
        /// Показатель работы.
        /// </summary>
        [Association, MemberDesignTimeVisibility(false)]
        public BillingInformation BillingInformation { get; set; }

        public override string ToString()
        {
            return GroupPerformanceIndicator?.ToString();
        }
    }
}