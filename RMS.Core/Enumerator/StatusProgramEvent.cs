using System.ComponentModel;

namespace RMS.Core.Enumerator
{
    /// <summary>
    /// Статус программного события.
    /// </summary>
    public enum StatusProgramEvent
    {
        /// <summary>
        /// Выполняется.
        /// </summary>
        [Description("Выполняется")]
        Performed = 0,       

        /// <summary>
        /// Выполнена.
        /// </summary>
        [Description("Выполнена")]
        Done = 1
    }
}