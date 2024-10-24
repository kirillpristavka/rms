using System.ComponentModel;

namespace RMS.Core.Enumerator
{
    /// <summary>
    /// Периоды отчетов (I-IV квартал, полугодия).
    /// </summary>
    public enum PeriodReportChange
    {
        /// <summary>
        /// Месяц.
        /// </summary>
        [Description("Месяц")]
        MONTH = 0,

        /// <summary>
        /// I квартал.
        /// </summary>
        [Description("I квартал")]
        FIRSTQUARTER = 1,

        /// <summary>
        /// II квартал.
        /// </summary>
        [Description("II квартал")]
        SECONDQUARTER = 2,

        /// <summary>
        /// III квартал.
        /// </summary>
        [Description("III квартал")]
        THIRDQUARTER = 3,

        /// <summary>
        /// IV квартал.
        /// </summary>
        [Description("IV квартал")]
        FOURTHQUARTER = 4,

        /// <summary>
        /// Первое полугодие.
        /// </summary>
        [Description("I полугодие")]
        FIRSTHALFYEAR = 5,

        /// <summary>
        /// Второе полугодие.
        /// </summary>
        [Description("II полугодие")]
        SECONDHALFYEAR = 6,

        /// <summary>
        /// Год.
        /// </summary>
        [Description("Год")]
        YEAR = 7
    }
}
