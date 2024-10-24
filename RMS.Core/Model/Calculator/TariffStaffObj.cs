using DevExpress.Xpo;

namespace RMS.Core.Model.Calculator
{
    /// <summary>
    /// Значения для тарифной сетки.
    /// </summary>
    public class TariffStaffObj : CalculatorObj 
    {
        public TariffStaffObj() { }
        public TariffStaffObj(Session session) : base(session) { }
        
        /// <summary>
        /// Тарифная сетка.
        /// </summary>
        [Association]
        [MemberDesignTimeVisibility(false)]
        public CalculatorTaxSystem CalculatorTaxSystem { get; set; }
    }
}