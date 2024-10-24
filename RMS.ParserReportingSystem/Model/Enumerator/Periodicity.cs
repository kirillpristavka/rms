using System.ComponentModel;

namespace RMS.ParserReportingSystem.Model.Enumerator
{
    /// <summary>
    /// Периодичность.
    /// </summary>
    public enum Periodicity
    {
        /// <summary>
        /// Месячная.
        /// </summary>
        [Description("Месячная")]
        MONTHLY = 0,

        /// <summary>
        /// Квартальная.
        /// </summary>
        [Description("Квартальная")]
        QUARTERLY = 1,

        /// <summary>
        /// Полугодовая.
        /// </summary>
        [Description("Полугодовая")]
        HALFEEARLY = 2,

        /// <summary>
        /// Годовая.
        /// </summary>
        [Description("Годовая")]
        YEARLY = 3,

        /// <summary>
        /// Один раз в три года.
        /// </summary>
        [Description("1 раз в 3 года")]
        ONCEEVERYTHREEYEARS = 4,

        /// <summary>
        /// Один раз в пять лет.
        /// </summary>
        [Description("1 раз в 5 лет")]
        ONCEEVERYFIVEYEARS = 5
    }
}
