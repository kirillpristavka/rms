using System.ComponentModel;

namespace RMS.Core.Enumerator
{
    public enum Act
    {
        /// <summary>
        /// Обновление информации по клиентам.
        /// </summary>
        [Description("Обновление информации по клиентам (автоматическое)")]
        UPDATING_CUSTOMER_INFORMATION_AUTO = 1,

        /// <summary>
        /// Обновление информации по клиентам.
        /// </summary>
        [Description("Обновление информации по клиентам (ручное)")]
        UPDATING_CUSTOMER_INFORMATION_HAND = 2,

        /// <summary>
        /// Изменение ответственного лица у клиента.
        /// </summary>
        [Description("Изменение ответственного лица")]
        CHANGE_RESPONSIBLE_PERSON = 3,

        /// <summary>
        /// Изменение состояния клиента.
        /// </summary>
        [Description("Изменение состояния клиента")]
        CUSTOMER_STATE_CHANGE = 4,

        /// <summary>
        /// Обновление информации по банку (ручное)".
        /// </summary>
        [Description("Обновление информации по банку (ручное)")]
        UPDATING_BANK_INFORMATION_HAND = 5,

        /// <summary>
        /// Обновление информации из системы сбора отчетности (ручное)".
        /// </summary>
        [Description("Обновление информации из системы сбора отчетности (ручное)")]
        UPDATING_INFORMATION_REPORTING_SYSTEM_HAND = 6,

        /// <summary>
        /// Обновление информации из системы сбора отчетности (авто)".
        /// </summary>
        [Description("Обновление информации из системы сбора отчетности (авто)")]
        UPDATING_INFORMATION_REPORTING_SYSTEM_AUTO = 7,

        /// <summary>
        /// Загрузка информации об организации из файла Excel (ручное).
        /// </summary>
        [Description("Загрузка информации об организации из файла Excel (ручное)")]
        IMPORT_FROM_EXCEL = 8,

        /// <summary>
        /// Изменение статуса клиента.
        /// </summary>
        [Description("Изменение статуса клиента")]
        CUSTOMER_STATUS_CHANGE = 9,

        /// <summary>
        /// Изменение системы налогообложения клиента.
        /// </summary>
        [Description("Изменение системы налогообложения клиента")]
        CUSTOMER_TAXSYSTEM_CHANGE = 10,

        /// <summary>
        /// Изменение системы налогообложения клиента.
        /// </summary>
        [Description("Обновление нюансов учета")]
        UPDATING_ACCOUNTING_NUANCES = 11,

        /// <summary>
        /// Обновление вида деятельности.
        /// </summary>
        [Description("Обновление вида деятельности")]
        UPDATING_ACTIVITIES = 12,

        /// <summary>
        /// Изменение дня заработной платы.
        /// </summary>
        [Description("Изменение дня заработной платы")]
        CHANGE_SALARY_DAY = 13,

        /// <summary>
        /// Изменение дня аванса.
        /// </summary>
        [Description("Изменение дня аванса")]
        CHANGE_ADVANCE_DAY = 14,

        /// <summary>
        /// Принятие задачи пользователем.
        /// </summary>
        [Description("Принятие задачи пользователем")]
        ACCEPTANCE_TASK = 15,

        /// <summary>
        /// Обновление сделок из писем.
        /// </summary>
        [Description("Обновление сделок из писем")]
        UPDATING_DEALS_FROM_LETTERS = 16,

        /// <summary>
        /// Удаление пустых сделок.
        /// </summary>
        [Description("Удаление пустых сделок")]
        REMOVING_EMPTY_TRADES = 17,

        /// <summary>
        /// Удаление клиента.
        /// </summary>
        [Description("Удаление клиента")]
        CUSTOMER_DELETED = 18,

        /// <summary>
        /// Удаление объекта.
        /// </summary>
        [Description("Удаление объекта")]
        OBJECT_DELETED = 19,

        /// <summary>
        /// Поздравление с днем рождения.
        /// </summary>
        [Description("Поздравление с днем рождения")]
        HAPPY_BIRTHDAY = 20,

        /// <summary>
        /// Изменение вида деятельности.
        /// </summary>
        [Description("Изменение вида деятельности")]
        CHANGE_ACTIVITIES = 21
    }
}
