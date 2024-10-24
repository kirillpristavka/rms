using DevExpress.Xpo;
using RMS.Core.Extensions;

namespace RMS.Core.Model
{
    /// <summary>
    /// Шкала.
    /// </summary>
    public class Scale : XPObject
    {
        public Scale() { }
        public Scale(Session session) : base(session) { }

        /// <summary>
        /// Количество с.
        /// </summary>
        [DisplayName("С")]
        public int NumberWith { get; set; }

        /// <summary>
        /// Количество по.
        /// </summary>
        [DisplayName("По")]
        public int NumberOf { get; set; }

        decimal value;
        /// <summary>
        /// Стоимость.
        /// </summary>
        [DisplayName("Значение")]
        public decimal Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value.GetDecimalRound();
            }
        }
    }
}
