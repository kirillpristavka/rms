using DevExpress.Xpo;
using System.Linq;

namespace RMS.Core.Model.InfoCustomer.Billing
{
    /// <summary>
    /// Шкалы расчета.
    /// </summary>
    public class CalculationScale : XPObject
    {
        public CalculationScale() { }
        public CalculationScale(Session session) : base(session) { }

        /// <summary>
        /// Наименование.
        /// </summary>
        [DisplayName("Наименование"), Size(256)]
        public string Name { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        [DisplayName("Описание"), Size(1024)]
        public string Description { get; set; }

        /// <summary>
        /// Шкала.
        /// </summary>
        [Association, MemberDesignTimeVisibility(false), Aggregated]
        public XPCollection<CalculationScaleValue> CalculationScaleValues
        {
            get
            {
                return GetCollection<CalculationScaleValue>(nameof(CalculationScaleValues));
            }
        }

        /// <summary>
        /// Получить значение которое удовлетворяет входящему количеству.
        /// </summary>
        /// <param name="count">Количество.</param>
        /// <returns>Значение удовлетворяющее условию отбора.</returns>
        public CalculationScaleValue GetPerformanceIndicatorValue(int count)
        {
            return CalculationScaleValues.FirstOrDefault(f => f.NumberWith <= count && f.NumberOf >= count);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
