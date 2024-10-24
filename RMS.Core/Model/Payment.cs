using DevExpress.Xpo;
using RMS.Core.Extensions;
using System;

namespace RMS.Core.Model
{
    /// <summary>
    /// Платеж.
    /// </summary>
    public class Payment : XPObject
    {
        public Payment() { }
        public Payment(Session session) : base(session) {}

        [DisplayName("Дата")]
        public DateTime Date { get; set; } = DateTime.Now.Date;

        /// <summary>
        /// Описание платежа.
        /// </summary>
        [DisplayName("Описание")]
        public virtual string Description { get; set; }

        private decimal value = 0.00M;
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

        /// <summary>
        /// Тип платежа. 
        /// </summary>
        [DisplayName("Тип платежа")]
        public Enumerator.TypePayment? TypePayment { get; set; }

        public override string ToString()
        {
            return $"Платеж от {Date.ToShortDateString()}";
        }
    }
}