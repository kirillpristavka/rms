using System.ComponentModel;

namespace RMS.Core.Enumerator
{
    /// <summary>
    /// Периоды архивных папок.
    /// </summary>
    public enum PeriodArchiveFolder
    {
        /// <summary>
        /// Месяц.
        /// </summary>
        [Description("Месяц")]
        MONTH = 0,

        /// <summary>
        /// Квартал.
        /// </summary>
        [Description("Квартал")]
        QUARTER = 1,

        /// <summary>
        /// Год.
        /// </summary>
        [Description("Год")]
        YEAR = 2,

        /// <summary>
        /// Полугодие.
        /// </summary>
        [Description("Полугодие")]
        HALFYEAR = 4,

        /// <summary>
        /// Не нужна.
        /// </summary>
        [Description("Не нужна")]
        NEEDNOT = 3
    }
}
