using System.ComponentModel;

namespace RMS.Core.Enumerator
{
    /// <summary>
    /// Статус договоров.
    /// </summary>
    public enum StatusContract
    {
        /// <summary>
        /// Не подписан.
        /// </summary>
        [Description("Не подписан")]
        NotSigned = 0,

        /// <summary>
        /// Подписан.
        /// </summary>
        [Description("Подписан")]
        Signed = 1,

        /// <summary>
        /// Приостановлен.
        /// </summary>
        [Description("Приостановлен")]
        Suspended = 2,

        /// <summary>
        /// Восстановление.
        /// </summary>
        [Description("Восстановление")]
        Recovery = 3,
    }
}