using System.ComponentModel;

namespace RMS.Core.Enumerator
{
    /// <summary>
    /// Тип задачи.
    /// </summary>
    public enum TypeTask
    {
        /// <summary>
        /// Задача.
        /// </summary>
        [Description("Задача")]
        Task = 0,

        /// <summary>
        /// Первичные документы.
        /// </summary>
        [Description("Первичка")]
        SourceDocuments = 1,

        /// <summary>
        /// Требование.
        /// </summary>
        [Description("Требование")]
        Demand = 2,

        /// <summary>
        /// Сверка.
        /// </summary>
        [Description("Сверка")]
        Reconciliation = 3
    }
}