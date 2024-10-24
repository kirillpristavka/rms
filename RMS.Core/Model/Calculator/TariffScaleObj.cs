using DevExpress.Xpo;

namespace RMS.Core.Model.Calculator
{
    /// <summary>
    /// Значения для тарифной сетки.
    /// </summary>
    public class TariffScaleObj : CalculatorObj 
    {
        public TariffScaleObj() { }
        public TariffScaleObj(Session session) : base(session) { }

        /// <summary>
        /// Тарифная сетка.
        /// </summary>
        [Association]
        [MemberDesignTimeVisibility(false)]
        public TariffScale TariffScale { get; set; }
    }
}