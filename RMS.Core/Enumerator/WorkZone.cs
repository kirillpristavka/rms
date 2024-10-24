using System.ComponentModel;

namespace RMS.Core.Enumerator
{
    /// <summary>
    /// Рабочая зона.
    /// </summary>
    public enum WorkZone
    {
        /// <summary>
        /// Разное.
        /// </summary>
        [Description("Разное")]
        ModuleOther,
        
        /// <summary>
        /// Модуль клиенты.
        /// </summary>
        [Description("Клиенты")]
        ModuleCustomer,

        /// <summary>
        /// Модуль договора.
        /// </summary>
        [Description("Договора")]
        ModuleContract,
        
        /// <summary>
        /// Модуль задачи.
        /// </summary>
        [Description("Задачи")]
        ModuleTask,

        /// <summary>
        /// Модуль сотрудники.
        /// </summary>
        [Description("Сотрудники")]
        ModuleStaff,

        /// <summary>
        /// Модуль отчеты и налоги.
        /// </summary>
        [Description("Отчеты и налоги")]
        ModuleReport,

        /// <summary>
        /// Модуль сделки.
        /// </summary>
        [Description("Сделки")]
        ModuleDeal,

        /// <summary>
        /// Модуль счета.
        /// </summary>
        [Description("Счета")]
        ModuleInvoice,

        /// <summary>
        /// Модуль почта.
        /// </summary>
        [Description("Почта")]
        ModuleMail,

        /// <summary>
        /// Модуль заработная плата.
        /// </summary>
        [Description("Заработная плата")]
        ModuleSalary,

        /// <summary>
        /// Модуль архивные папки.
        /// </summary>
        [Description("Архивные папки")]
        ModuleArchiveFolder,
        
        /// <summary>
        /// Модуль маршрутные листы.
        /// </summary>
        [Description("Маршрутные листы")]
        ModuleRouteSheet,

        /// <summary>
        /// Модуль курьерские задачи.
        /// </summary>
        [Description("Курьерские задачи")]
        ModuleTaskCourier
    }
}
