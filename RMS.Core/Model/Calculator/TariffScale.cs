using DevExpress.Xpo;
using RMS.Core.Extensions;
using System.Linq;

namespace RMS.Core.Model.Calculator
{
    /// <summary>
    /// Тарифная сетка.
    /// </summary>
    public class TariffScale : XPObject
    {
        public TariffScale() { }
        public TariffScale(Session session) : base(session) { }
        
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

        [Aggregated]
        [Association]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<TariffScaleObj> TariffScalesObj
        {
            get
            {
                return GetCollection<TariffScaleObj>(nameof(TariffScalesObj));
            }
        }

        /// <summary>
        /// Получение значения тарифной сетки по входящей позиции.
        /// </summary>
        /// <param name="position">Позиция.</param>
        /// <returns>Значение (decimal)</returns>.
        public decimal GetValueTariffScaleObj(int position)
        {
            TariffScalesObj?.Reload();
            var result = TariffScalesObj?.FirstOrDefault(f => f.Start <= position && f.End >= position)?.Value;

            if (result is null)
            {
                return default;
            }

            return ((decimal)result).GetDecimalRound();
        }
        

        public override string ToString()
        {
            return Name;
        }
    }
}
