using System.ComponentModel;

namespace RMS.Core.Enumerator
{
    /// <summary>
    /// Статус архивной папки.
    /// </summary>
    public enum StatusArchiveFolder
    {
        /// <summary>
        /// Новая.
        /// </summary>
        [Description("Новая")]
        NEW = 0,

        /// <summary>
        /// Комплектуется.
        /// </summary>
        [Description("Комплектуется")]
        ISCOMPLETED = 1,

        /// <summary>
        /// Перебрать.
        /// </summary>
        [Description("Перебрать")]
        SORTOUT = 2,

        /// <summary>
        /// Выдали.
        /// </summary>
        [Description("Выдали")]
        ISSUED = 3,

        /// <summary>
        /// Готово.
        /// </summary>
        [Description("Готово")]
        DONE = 4,

        /// <summary>
        /// Готово.
        /// </summary>
        [Description("Вернул клиент")]
        RETURNED = 5,

        /// <summary>
        /// Текущая.
        /// </summary>
        [Description("Текущая (не использовать)")]
        CURRENT = 6,
        
        /// <summary>
        /// ЭЦП выдали.
        /// </summary>
        [Description("ЭЦП выдали")]
        ISSUEDEDS = 7,

        /// <summary>
        /// ЭЦП получена.
        /// </summary>
        [Description("ЭЦП получена")]
        RECEIVEDEDS = 8,

        /// <summary>
        /// ТК получены.
        /// </summary>
        [Description("ТК получены")]
        RECEIVEDTK = 9,

        /// <summary>
        /// ТК выданы.
        /// </summary>
        [Description("ТК выданы")]
        ISSUEDTK = 10,

        /// <summary>
        /// Уничтожили.
        /// </summary>
        [Description("Уничтожили")]
        DESTROYED = 11
    }
}