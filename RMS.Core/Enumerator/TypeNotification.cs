using System.ComponentModel;

namespace RMS.Core.Enumerator
{
    /// <summary>
    /// Тип уведомления.
    /// </summary>
    public enum TypeNotification
    {
        /// <summary>
        /// Просрочены.
        /// </summary>
        [Description("Просрочены")]
        Expired = 0,

        /// <summary>
        /// Горящие.
        /// </summary>
        [Description("Горящие")]
        Burning = 1,

        /// <summary>
        /// Разные.
        /// </summary>
        [Description("Разные")]
        Different = 2
    }
}