using DevExpress.Xpo;
using RMS.Core.Extensions;

namespace RMS.Core.Model
{
    /// <summary>
    /// Прайс на оказываемые услуги.
    /// </summary>
    public class PriceList : XPObject
    {
        public PriceList() { }
        public PriceList(Session session) : base(session) { }

        /// <summary>
        /// Код.
        /// </summary>
        [DisplayName("Код")]
        public int Kod { get; set; }

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

        decimal price;
        /// <summary>
        /// Цена за услугу.
        /// </summary>
        [DisplayName("Цена")]
        public decimal Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value.GetDecimalRound();
            }
        }

        [MemberDesignTimeVisibility(false)]
        public PriceGroup PriceGroup { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
