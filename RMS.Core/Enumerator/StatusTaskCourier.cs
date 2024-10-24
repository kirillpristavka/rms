using System.ComponentModel;

namespace RMS.Core.Enumerator
{
    /// <summary>
    /// Статус курьерских задач.
    /// </summary>
    public enum StatusTaskCourier
    {
        /// <summary>
        /// Новый.
        /// </summary>
        [Description("Новый")]
        New = 0,

        /// <summary>
        /// Выполнено.
        /// </summary>
        [Description("Выполнено")]
        Performed = 1,

        /// <summary>
        /// Отменено.
        /// </summary>
        [Description("Отменено")]
        Canceled = 2,
    }
}