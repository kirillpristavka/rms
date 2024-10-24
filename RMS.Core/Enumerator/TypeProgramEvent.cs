using System.ComponentModel;

namespace RMS.Core.Enumerator
{
    /// <summary>
    /// Тип программного события (задачи).
    /// </summary>
    public enum TypeProgramEvent
    {
        /// <summary>
        /// Ежедневно.
        /// </summary>
        [Description("Ежедневно")]
        DAILY = 1,
        
        /// <summary>
        /// Еженедельно.
        /// </summary>
        [Description("Еженедельно")]
        WEEKLY = 2,

        /// <summary>
        /// Ежемесячно.
        /// </summary>
        [Description("Ежемесячно")]
        MONTHLY = 3,
        
        /// <summary>
        /// Однократно.
        /// </summary>
        [Description("Однократно")]
        ONCE = 4
    }
}
