using DevExpress.Xpo;
using RMS.Core.Enumerator;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Mail;
using RMS.Core.Model.Reports;
using RMS.Core.Model.Salary;
using RMS.Core.Model.Taxes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace RMS.Core.Model
{
    /// <summary>
    /// Программное событие (задача).
    /// </summary>
    public class ProgramEvent : XPObject
    {
        public ProgramEvent() { }
        public ProgramEvent(Session session) : base(session) { }

        /// <summary>
        /// Статус программного события.
        /// </summary>
        [DisplayName("Статус")]
        public StatusProgramEvent? Status
        {
            get
            {
                if (DateTo is null || DateTo >= DateTime.Now.Date)
                {
                    return StatusProgramEvent.Performed;
                }

                return StatusProgramEvent.Done;
            }
        }

        /// <summary>
        /// Наименование.
        /// </summary>
        [Size(2048)]
        [DisplayName("Наименование")]
        public string Name { get; set; }

        [DisplayName("Описание")]
        public string DescriptionString => Letter.ByteToString(Description);
        /// <summary>
        /// Комментарий.
        /// </summary>
        public byte[] Description { get; set; }

        /// <summary>
        /// Начало контроля.
        /// </summary>
        [DisplayName("Начало действия")]
        public DateTime? DateSince { get; set; }

        /// <summary>
        /// Окончание контроля.
        /// </summary>
        [DisplayName("Окончание действия")]
        public DateTime? DateTo { get; set; }

        /// <summary>
        /// Тип программного события (задачи). 
        /// </summary>
        [DisplayName("Триггер")]
        public TypeProgramEvent? TypeProgramEvent { get; set; }

        /// <summary>
        /// Действие для программного события (задачи).
        /// </summary>
        [DisplayName("Действие")]
        public ActionProgramEvent? ActionProgramEvent { get; set; }

        /// <summary>
        /// Модуль.
        /// </summary>
        [Size(2048)]
        [DisplayName("Модуль")]
        public string NameModel { get; set; }
        
        /// <summary>
        /// Уникальный идентификатор объекта.
        /// </summary>
        [DisplayName("Уникальный идентификатор объекта")]
        [MemberDesignTimeVisibility(false)]
        public int ControlSystemObjectId { get; set; }

        /// <summary>
        /// Тип объекта.
        /// </summary>
        [Size(2048)]
        [DisplayName("Тип")]
        [MemberDesignTimeVisibility(false)]
        public string TypeName { get; set; }

        [DisplayName("Постановщик")]
        public string StaffToString => Staff?.ToString();
        /// <summary>
        /// Постановщик.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Staff Staff { get; set; }

        [MemberDesignTimeVisibility(false)]
        public byte[] ExecutionDatesObj { get; set; }

        /// <summary>
        /// Просчитанные даты событий.
        /// </summary>
        public List<DateTime> GetDatesUse()
        {
            var list = new List<DateTime>();

            try
            {
                var dateTime = DateTime.Now.Date;
                var dateSine = DateSince ?? new DateTime(dateTime.Year, 1, 1);
                var dateTo = DateTo ?? new DateTime(dateTime.Year, 12, 31);
                do
                {
                    list.Add(dateSine);
                    if (TypeProgramEvent == Enumerator.TypeProgramEvent.DAILY)
                    {
                        dateSine = dateSine.AddDays(1);
                    }
                    else if (TypeProgramEvent == Enumerator.TypeProgramEvent.MONTHLY)
                    {
                        dateSine = dateSine.AddMonths(1);
                    }
                    else if (TypeProgramEvent == Enumerator.TypeProgramEvent.WEEKLY)
                    {
                        dateSine = dateSine.AddDays(7);
                    }
                    else if (TypeProgramEvent == Enumerator.TypeProgramEvent.ONCE)
                    {
                        break;
                    }
                }
                while (dateTo >= dateSine);
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }

            return list;
        }

        public Type GetTypeObj()
        {
            switch (TypeName)
            {
                case nameof(Customer):
                    return typeof(Customer);

                case nameof(BankAccess):
                    return typeof(BankAccess);

                case nameof(ForeignEconomicActivity):
                    return typeof(ForeignEconomicActivity);

                case nameof(OrganizationPerformance):
                    return typeof(OrganizationPerformance);

                case nameof(StatisticalReport):
                    return typeof(StatisticalReport);

                case nameof(CustomerEmploymentHistory):
                    return typeof(CustomerEmploymentHistory);

                case nameof(Contract):
                    return typeof(Contract);

                case nameof(Task):
                    return typeof(Task);

                case nameof(Staff):
                    return typeof(Staff);

                case nameof(ReportChange):
                    return typeof(ReportChange);

                case nameof(PreTax):
                    return typeof(PreTax);

                case nameof(CustomerSalaryAdvance):
                    return typeof(CustomerSalaryAdvance);

                case nameof(IndividualEntrepreneursTax):
                    return typeof(IndividualEntrepreneursTax);

                case nameof(Deal):
                    return typeof(Deal);

                case nameof(Invoice):
                    return typeof(Invoice);

                case nameof(Letter):
                    return typeof(Letter);

                case nameof(ArchiveFolderChange):
                    return typeof(ArchiveFolderChange);

                case nameof(RouteSheet):
                    return typeof(RouteSheet);

                case nameof(TaskCourier):
                    return typeof(TaskCourier);

                default:
                    return null;
            }
        }
    }
}
