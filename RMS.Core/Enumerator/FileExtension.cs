using System.ComponentModel;

namespace RMS.Core.Enumerator
{
    /// <summary>
    /// Форматы файлов.
    /// </summary>
    public enum FileExtension
    {
        /// <summary>
        /// Формат, предназначенный для представления табличных данных (.csv).
        /// </summary>
        [Description(".csv")]
        CSV = 1,

        /// <summary>
        /// Формат файлов программы до версии Microsoft Excel 2007 (.xls).
        /// </summary>
        [Description(".xls")]
        XLS = 2,

        /// <summary>
        /// Формат файлов программы до версии Microsoft Excel (.xlsx).
        /// </summary>
        [Description(".xlsx")]
        XLSX = 3
    }
}
