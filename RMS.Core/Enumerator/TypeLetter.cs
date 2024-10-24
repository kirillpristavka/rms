using System.ComponentModel;

namespace RMS.Core.Enumerator
{
    /// <summary>
    /// Тип письма.
    /// </summary>
    public enum TypeLetter
    {
        /// <summary>
        /// Входящие.
        /// </summary>
        [Description("Входящие")]
        InBox = 0,

        /// <summary>
        /// Отправленные.
        /// </summary>
        [Description("Отправленные")]
        Outgoing = 1,

        /// <summary>
        /// Корзина.
        /// </summary>
        [Description("Корзина")]
        Basket = 2,

        /// <summary>
        /// Спам.
        /// </summary>
        [Description("Спам")]
        Spam = 3,

        /// <summary>
        /// Черновик.
        /// </summary>
        [Description("Черновик")]
        Draft = 4
    }
}