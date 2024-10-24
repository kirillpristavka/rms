using DevExpress.Xpo;
using RMS.Core.Extensions;

namespace RMS.Core.Model.InfoCustomer.Billing
{
    public class BillingPerformanceIndicator : XPObject
    {
        public BillingPerformanceIndicator() { }
        public BillingPerformanceIndicator(Session session) : base(session) { }

        [Association, MemberDesignTimeVisibility(false), Aggregated]
        public BillingInformation BillingInformation { get; set; }

        [DisplayName("Показатель")]
        public string PerformanceIndicatorString => PerformanceIndicator?.ToString();
        /// <summary>
        /// Показатель работы.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public PerformanceIndicator PerformanceIndicator { get; set; }

        decimal? value;
        /// <summary>
        /// Стоимость.
        /// </summary>
        [DisplayName("Значение")]
        public decimal? Value
        {
            get
            {
                return value;
            }
            set
            {
                if (value is decimal dValue)
                {
                    this.value = dValue.GetDecimalRound();
                }
                else
                {
                    this.value = null;
                }
            }
        }
    }
}