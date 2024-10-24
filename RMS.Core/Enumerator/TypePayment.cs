using System.ComponentModel;

namespace RMS.Core.Enumerator
{
    /// <summary>
    /// Тип платежа.
    /// </summary>
    public enum TypePayment
    {
        /// <summary>
        /// Безналичный платеж.
        /// </summary>
        [Description("Безналичный платеж")]
        AshlessPayment = 0,

        /// <summary>
        /// Наличный платеж.
        /// </summary>
        [Description("Наличный платеж")]
        CashPayment = 1
    }
}