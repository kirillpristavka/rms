using DevExpress.Xpo;
using RMS.Core.Extensions;

namespace RMS.Core.Model.Calculator
{
    /// <summary>
    /// Значения для тарифов.
    /// </summary>
    public class CalculatorObj : XPObject
    {
        public CalculatorObj() { }
        public CalculatorObj(Session session) : base(session) { }

        /// <summary>
        /// Начальное значение отсчета.
        /// </summary>
        [DisplayName("С")]
        public int Start { get; set; }

        /// <summary>
        /// Окончательное значение отсчета.
        /// </summary>
        [DisplayName("ПО")]
        public int End { get; set; }

        private decimal value;
        /// <summary>
        /// Значение.
        /// </summary>
        [DisplayName("Значение")]
        public decimal Value
        {
            get
            {
                return value.GetDecimalRound();
            }
            set
            {
                this.value = value;
            }
        }

        public override string ToString()
        {
            return $"{Start} <-> {End} ({Value})";
        }
    }
}