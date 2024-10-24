using DevExpress.Xpo;
using RMS.Core.Extensions;
using System.Linq;

namespace RMS.Core.Model.Calculator
{
    /// <summary>
    /// Система налогообложения для калькулятора.
    /// </summary>
    public class CalculatorTaxSystem : XPObject
    {
        public CalculatorTaxSystem() { }
        public CalculatorTaxSystem(Session session) : base(session) { }

        /// <summary>
        /// Наименование.
        /// </summary>
        [Size(256)]
        [DisplayName("Наименование")]
        public string Name { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        [Size(1024)]
        [DisplayName("Описание")]
        public string Description { get; set; }

        /// <summary>
        /// Коллекция рабочих шкал для систем налогообложения.
        /// </summary>
        [Aggregated]
        [Association]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<CalculatorTaxSystemObj> CalculatorTaxSystesmObj
        {
            get
            {
                return GetCollection<CalculatorTaxSystemObj>(nameof(CalculatorTaxSystesmObj));
            }
        }

        /// <summary>
        /// Коллекция рабочих шкал для сотрудников.
        /// </summary>
        [Aggregated]
        [Association]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<TariffStaffObj> TariffStaffObj
        {
            get
            {
                return GetCollection<TariffStaffObj>(nameof(TariffStaffObj));
            }
        }

        /// <summary>
        /// Получение значения тарифной сетки по входящей позиции.
        /// </summary>
        /// <param name="position">Позиция.</param>
        /// <returns>Значение (decimal)</returns>.
        public decimal GetValueTariffStaffObj(int position)
        {
            TariffStaffObj?.Reload();
            var result = TariffStaffObj?.FirstOrDefault(f => f.Start <= position && f.End >= position)?.Value;

            if (result is null)
            {
                return default;
            }            

            return ((decimal)result * position).GetDecimalRound();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
