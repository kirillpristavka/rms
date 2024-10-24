using System;
using System.ComponentModel;

namespace RMS.Core.Enumerator
{
    /// <summary>
    /// Статус отчета.
    /// </summary>
    public enum StatusReport
    {
        /// <summary>
        /// Новый.
        /// </summary>
        [Description("Новый")]
        NEW = 0,

        /// <summary>
        /// Отправлен.
        /// </summary>
        [Description("Отправлен")]
        SENT = 1,

        /// <summary>
        /// Сдан.
        /// </summary>
        [Description("Сдан")]
        SURRENDERED = 2,

        /// <summary>
        /// Не сдан.
        /// </summary>
        [Description("Не сдан")]
        NOTSURRENDERED = 3,

        /// <summary>
        /// Подготовлен.
        /// </summary>
        [Description("Подготовлен")]
        PREPARED = 4,

        /// <summary>
        /// Не принят
        /// </summary>
        [Description("Не принят")]
        NOTACCEPTED = 5,

        /// <summary>
        /// Требует корректировки (наша вина).
        /// </summary>
        [Obsolete]
        [Description("Наша вина")]
        NEEDSADJUSTMENTOURFAULT = 6,

        /// <summary>
        /// Требует корректировки (вина заказчика).
        /// </summary>
        [Obsolete]
        [Description("Вина заказчика")]
        NEEDSADJUSTMENTCUSTOMERFAULT = 7,

        /// <summary>
        /// Не сдает.
        /// </summary>
        [Description("Не сдает")]
        DOESNOTGIVEUP = 8,

        /// <summary>
        /// Коррекция.
        /// </summary>
        [Obsolete]
        [Description("Коррекция")]
        CORRECTION = 9,
        
        /// <summary>
        /// Коррекция.
        /// </summary>
        [Description("Требуется корректировка")]
        ADJUSTMENTREQUIRED = 10,

        /// <summary>
        /// Коррекция готова.
        /// </summary>
        [Description("Корректировка готова")]

        ADJUSTMENTISREADY = 11,

        /// <summary>
        /// Коррекция отправлена.
        /// </summary>
        [Description("Корректировка отправлена")]
        CORRECTIONSENT = 12,

        /// <summary>
        /// Коррекция сдана.
        /// </summary>
        [Description("Корректировка сдана")]
        CORRECTIONSUBMITTED = 13,

        /// <summary>
        /// Сдает клиент.
        /// </summary>
        [Description("Сдает клиент")]
        RENTEDBYTHECLIENT = 14
    }
}