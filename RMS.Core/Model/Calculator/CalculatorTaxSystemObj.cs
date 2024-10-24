using DevExpress.Xpo;

namespace RMS.Core.Model.Calculator
{
    /// <summary>
    /// Шкалы для системы налогообложения.
    /// </summary>
    public class CalculatorTaxSystemObj : XPObject
    {
        public CalculatorTaxSystemObj() { }
        public CalculatorTaxSystemObj(Session session) : base(session) { }
        
        /// <summary>
        /// Система налогообложения.
        /// </summary>
        [Association]
        [MemberDesignTimeVisibility(false)]
        public CalculatorTaxSystem CalculatorTaxSystem { get; set; }

        /// <summary>
        /// Тарифная сетка.
        /// </summary>
        [Aggregated]
        public TariffScale TariffScale { get; set; }

        public override string ToString()
        {
            return TariffScale?.ToString() ?? default;
        }
    }
}