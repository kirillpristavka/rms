using System.ComponentModel;

namespace RMS.Core.Enumerator
{
    /// <summary>
    /// Действие для программного события (задачи).
    /// </summary>
    public enum ActionProgramEvent
    {
        /// <summary>
        /// Создать новый объект.
        /// </summary>
        [Description("Создать новый объект")]
        CREATE_NEW_OBJECT = 1,

        /// <summary>
        /// Оповестить.
        /// </summary>
        [Description("Оповестить")]
        NOTIFY = 2
    }
}
