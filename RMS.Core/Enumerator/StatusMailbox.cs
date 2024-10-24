using System.ComponentModel;

namespace RMS.Core.Enumerator
{
    /// <summary>
    /// Состояние почтового ящика. 
    /// </summary>
    public enum StateMailbox
    {
        /// <summary>
        /// Работает.
        /// </summary>
        [Description("Работает (все операции)")]
        Working = 0,

        /// <summary>
        /// Работает (только прием писем).
        /// </summary>
        [Description("Работает (только прием писем)")]
        ReceivingLetters = 1,

        /// <summary>
        /// Работает (только отправка писем).
        /// </summary>
        [Description("Работает (только отправка писем)")]
        SendingLetters = 2,

        /// <summary>
        /// Ожидание.
        /// </summary>
        [Description("Ожидание")]
        Expectation = 3,

        /// <summary>
        /// Отключен.
        /// </summary>
        [Description("Отключен")]
        Disconnected = 4,
    }
}