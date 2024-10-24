using System.ComponentModel;

namespace RMS.Core.Enumerator
{
    /// <summary>
    /// Статус задачи.
    /// </summary>
    public enum StatusTask
    {
        /// <summary>
        /// Выполняется.
        /// </summary>
        [Description("Выполняется")]
        Performed = 0,

        /// <summary>
        /// Приостановлена.
        /// </summary>
        [Description("Приостановлена")]
        Paused = 1,

        /// <summary>
        /// Выполнена.
        /// </summary>
        [Description("Выполнена")]
        Done = 2,

        /// <summary>
        /// Входящая.
        /// </summary>
        [Description("Входящая")]
        Incoming = 3,

        /// <summary>
        /// Исходящая.
        /// </summary>
        [Description("Исходящая")]
        Outgoing = 4
    }
}