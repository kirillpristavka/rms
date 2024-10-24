using DevExpress.Xpo;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Interface;
using RMS.Core.Model.Chronicle;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Notifications;
using System;
using System.Linq;

namespace RMS.Core.Model.Reports
{
    /// <summary>
    /// Отчеты для сдачи.
    /// </summary>
    public class ReportChange : XPObject, INotification
    {
        public ReportChange() { }
        public ReportChange(Session session) : base(session) { }

        /// <summary>
        /// Признак корректирующего отчета.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public bool IsCorrective { get; set; }

        /// <summary>
        /// Наша вина за коррекционный отчет.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public bool IsOurFault { get; set; }

        /// <summary>
        /// Вина клиента за коррекционный отчет.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public bool IsCustomerFault { get; set; }

        /// <summary>
        /// Год сдачи.
        /// </summary>
        [DisplayName("Год отчета")]
        public int? DeliveryYear { get; set; }

        [DisplayName("Ответственный")]
        public string AccountantResponsibleString => AccountantResponsible?.ToString();

        /// <summary>
        /// Ответственный.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Staff AccountantResponsible { get; set; }

        [DisplayName("Клиент")]
        public string CustomerString => Customer?.ToString();
        
        [DisplayName("Система налогообложения")]
        public string CustomerTaxSystem
        {
            get
            {
                var taxSystem = default(TaxSystem);
                var TaxSystemCustomerObjects = Customer?.TaxSystemCustomer?.TaxSystemCustomerObjects;

                if (TaxSystemCustomerObjects != null && TaxSystemCustomerObjects.Count > 0)
                {
                    //TODO: тут надо подумать как оптимизировать поиск интервалов дат.
                    var dateNow = DateCreate;
                    taxSystem = TaxSystemCustomerObjects.FirstOrDefault(f =>
                        (f.DateSince == null && f.DateTo >= dateNow)
                        || (f.DateSince <= dateNow && f.DateTo >= dateNow)
                        || (f.DateSince <= dateNow && f.DateTo == null)
                        || (f.DateSince == null && f.DateTo == null)
                        )?.TaxSystem;
                }

                return taxSystem?.ToString();
            }
        }

        /// <summary>
        /// Клиент.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Customer Customer { get; set; }

        [DisplayName("Отчет"), Size(256)]
        public string ReportString => Report?.ToString();

        /// <summary>
        /// Отчет.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Report Report { get; set; }

        [DisplayName("Период")]
        public string PeriodString
        {
            get
            {
                if (PeriodArchiveFolder != null)
                {
                    if (PeriodArchiveFolder == Enumerator.PeriodArchiveFolder.MONTH)
                    {
                        if (PeriodChangeMonth is null)
                        {
                            if (Month == Month.January)
                            {
                                return Month.December.GetEnumDescription();
                            }
                            else
                            {
                                return (Month - 1).GetEnumDescription();
                            }
                        }
                        else
                        {
                            return PeriodChangeMonth?.GetEnumDescription();
                        }
                    }
                    else if (PeriodArchiveFolder == Enumerator.PeriodArchiveFolder.YEAR)
                    {
                        return PeriodArchiveFolder?.GetEnumDescription();
                    }
                    else
                    {
                        return PeriodReportChange?.GetEnumDescription();
                    }
                }
                else
                {
                    if (PeriodReportChange == Enumerator.PeriodReportChange.MONTH)
                    {
                        if (PeriodChangeMonth is null)
                        {
                            if (Month == Month.January)
                            {
                                return Month.December.GetEnumDescription();
                            }
                            else
                            {
                                return (Month - 1).GetEnumDescription();
                            }
                        }
                        else
                        {
                            return PeriodChangeMonth?.GetEnumDescription();
                        }
                    }
                    else
                    {
                        return PeriodReportChange?.GetEnumDescription();
                    }
                }
            }
        }

        [MemberDesignTimeVisibility(false)]
        public PeriodReportChange? PeriodReportChange { get; set; }

        [MemberDesignTimeVisibility(false)]
        public PeriodArchiveFolder? PeriodArchiveFolder { get; set; }

        [MemberDesignTimeVisibility(false)]
        public Month? PeriodChangeMonth { get; set; }

        /// <summary>
        /// Сдать до.
        /// </summary>
        [DisplayName("Сдать до")]
        public DateTime? LastDayDelivery
        {
            get
            {
                var date = default(DateTime?);

                if (Day >= 1 && Day <= 31)
                {
                    if ((int)Month > 0 && (int)Month <= 12)
                    {
                        if (Year > 1900 && Year < 3000)
                        {
                            date = new DateTime(Year, (int)Month, Day);
                        }
                    }
                }

                return date;
            }
        }        

        [DisplayName("День")]
        [MemberDesignTimeVisibility(false)]
        public int Day { get; set; }

        [DisplayName("Год")]
        [MemberDesignTimeVisibility(false)]
        public int Year { get; set; }

        [DisplayName("Месяц")]
        [MemberDesignTimeVisibility(false)]
        public string MonthString => Month.GetEnumDescription();
        [MemberDesignTimeVisibility(false)]
        public Month Month { get; set; }

        [DisplayName("Статус")]
        public string StatusString
        {
            get
            {
                /* Сказали просто брать статус текущего отчета. */
                
                //if (CorrectiveReports != null && CorrectiveReports.Count > 0)
                //{
                //    var report = CorrectiveReports?.LastOrDefault();
                //    if (report != null && report.ReportChange != null)
                //    {
                //        return report?.ReportChange?.StatusString;
                //    }
                //}
                
                return StatusReport.GetEnumDescription();
            }
        }
        [MemberDesignTimeVisibility(false)]
        public StatusReport StatusReport { get; set; }

        /// <summary>
        /// Дата сдачи.
        /// </summary>
        [DisplayName("Дата сдачи")]
        public DateTime? DateCompletion { get; set; }

        [DisplayName("Сдал")]
        public string PassedStaffString => PassedStaff?.ToString();
        /// <summary>
        /// Пользователь, сдавший отчет.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Staff PassedStaff { get; set; }

        [Size(1024)]
        [DisplayName("Комментарий")]
        public string Comment { get; set; }

        [MemberDesignTimeVisibility(false)]
        public User UserCreate { get; set; }

        [MemberDesignTimeVisibility(false)]
        public DateTime DateCreate { get; set; }

        /// <summary>
        /// Задача по отчету.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Task Task { get; set; }

        /// <summary>
        /// Показатель работы организации.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public OrganizationPerformance OrganizationPerformance { get; set; }

        /// <summary>
        /// Хроника изменений.
        /// </summary>
        [Aggregated]
        [Association]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<ChronicleReportChange> ChronicleReportChanges
        {
            get
            {
                return GetCollection<ChronicleReportChange>(nameof(ChronicleReportChanges));
            }
        }

        [DisplayName("Корректирующие отчеты")]
        public string CorrectiveReportsString
        {
            get
            {
                var result = default(string);
                if (CorrectiveReports != null && CorrectiveReports.Count > 0)
                {
                    var number = 1;
                    result += $"{StatusReport.GetEnumDescription()}";

                    var dateCompletion = default(string);
                    if (DateCompletion is DateTime date)
                    {
                        dateCompletion = $"СДАН <color=red>{date.ToShortDateString()}</color> ";
                    }
                    
                    //result = $"<color=red>ПЕРВ</color> {dateCompletion}{number}. {result.Trim()}";
                    result = $"<color=red>ПЕРВ</color> {dateCompletion}";
                    //if (IsCustomerFault)
                    //{
                    //    result += " <color=green><b>(вина клиента)</b></color> ";
                    //}
                    //else if (IsOurFault)
                    //{
                    //    result += " <color=coral><b>(наша вина)</b></color> ";
                    //}                    
                    result += Environment.NewLine;

                    var lastCorrectiveReport = CorrectiveReports?.LastOrDefault();
                    if (lastCorrectiveReport != null)
                    {
                        result += $"<color=blue>КОРР</color> {lastCorrectiveReport}";

                        if (lastCorrectiveReport.ReportChange != null)
                        {
                            if (lastCorrectiveReport.ReportChange.IsCustomerFault)
                            {
                                result += " <color=green><b>(вина клиента)</b></color> ";
                            }
                            else if (lastCorrectiveReport.ReportChange.IsOurFault)
                            {
                                result += " <color=coral><b>(наша вина)</b></color> ";
                            }
                        }
                    }

                    //for (int i = 0; i < CorrectiveReports.Count; i++)
                    //{
                    //    number++;
                    //    result += $"<color=blue>КОРР</color> {number}. {CorrectiveReports[i]}";

                    //    if (CorrectiveReports[i].ReportChange != null)
                    //    {
                    //        if (CorrectiveReports[i].ReportChange.IsCustomerFault)
                    //        {
                    //            result += " <color=green><b>(вина клиента)</b></color> ";
                    //        }
                    //        else if (CorrectiveReports[i].ReportChange.IsOurFault)
                    //        {
                    //            result += " <color=coral><b>(наша вина)</b></color> ";
                    //        }
                    //    }

                    //    result += Environment.NewLine;
                    //}
                    return result?.Trim();
                }

                return default;
            }
        }

        /// <summary>
        /// Корректирующие отчеты.
        /// </summary>
        [Association]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<CorrectiveReport> CorrectiveReports
        {
            get
            {
                return GetCollection<CorrectiveReport>(nameof(CorrectiveReports));
            }
        }

        public Notification GetNotification(TypeNotification typeNotification)
        {
            if (LastDayDelivery is DateTime deadline)
            {
                var name = default(string);
                
                if (Customer != null)
                {
                    name += $"Клиент: {Customer}";
                }

                if (Report != null)
                {
                    name += $"{Environment.NewLine}Отчет: {Report}";
                }

                name += $"{Environment.NewLine}Статус: {StatusReport.GetEnumDescription()}";

                return new Notification(typeNotification, "Отчеты", deadline, name, Oid, typeof(ReportChange));
            }
            else
            {
                return default;
            }
        }

        public override string ToString()
        {
            var result = $"Отчет {ReportString} за {Year} г.";
            
            if (!string.IsNullOrWhiteSpace(CustomerString))
            {
                result += $" ({CustomerString})";
            }

            return result;
        }
    }
}
