using System.ComponentModel;

namespace RMS.Core.Enumerator
{
    /// <summary>
    /// Статус статистических отчетов.
    /// </summary>
    public enum StatusStatisticalReport
    {
        /// <summary>
        /// Информация не обновлялась.
        /// </summary>
        [Description("Информация не обновлялась")]
        INFORMATIONNOTUPDATED = 0,

        /// <summary>
        /// Имеются статистические отчеты.
        /// </summary>
        [Description("Имеются статистические отчеты")]
        STATISTICALREPORTSAVAILABLE = 1,

        /// <summary>
        /// Нет статистических отчетов.
        /// </summary>
        [Description("Нет статистических отчетов")]
        STATISTICALREPORTSNOTAVAILABLE = 2
    }
}