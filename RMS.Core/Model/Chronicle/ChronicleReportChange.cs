using DevExpress.Xpo;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Reports;
using System;
using System.ComponentModel.DataAnnotations;

namespace RMS.Core.Model.Chronicle
{
    public class ChronicleReportChange : XPObject
    {
        public ChronicleReportChange() { }
        public ChronicleReportChange(Session session) : base(session) { }

        /// <summary>
        /// Дата изменения.
        /// </summary>
        [DisplayName("Дата изменения")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy (HH:mm:ss)}")]
        public DateTime DateUpdate { get; set; }

        [DisplayName("Пользователь")]
        public string UserUpdateString => UserUpdate?.ToString();

        /// <summary>
        /// Год сдачи.
        /// </summary>
        [DisplayName("Год сдачи")]
        public int? DeliveryYear { get; set; }

        /// <summary>
        /// Пользователь внесший изменения.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public User UserUpdate { get; set; }

        [DisplayName("Ответственный")]        
        public string AccountantResponsibleString => AccountantResponsible?.ToString();

        /// <summary>
        /// Клиент.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Staff AccountantResponsible { get; set; }

        [DisplayName("Клиент"), Size(256)]
        public string CustomerString => Customer?.ToString();

        [DisplayName("Система налогообложения")]
        public string CustomerTaxSystem => Customer?.TaxSystemCustomer?.TaxSystem?.ToString();

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
                            return PeriodChangeMonth.GetEnumDescription();
                        }
                    }
                    else
                    {
                        return PeriodReportChange?.GetEnumDescription();
                    }
                }
                else
                {
                    if (PeriodReportChange == Enumerator.PeriodReportChange.YEAR)
                    {
                        return Enumerator.PeriodArchiveFolder.YEAR.GetEnumDescription();
                    }
                    else if (PeriodReportChange == Enumerator.PeriodReportChange.FIRSTHALFYEAR ||
                            PeriodReportChange == Enumerator.PeriodReportChange.SECONDHALFYEAR)
                    {
                        return Enumerator.PeriodArchiveFolder.HALFYEAR.GetEnumDescription();
                    }
                    else if (PeriodReportChange == Enumerator.PeriodReportChange.FIRSTQUARTER ||
                             PeriodReportChange == Enumerator.PeriodReportChange.SECONDQUARTER ||
                             PeriodReportChange == Enumerator.PeriodReportChange.THIRDQUARTER ||
                             PeriodReportChange == Enumerator.PeriodReportChange.FOURTHQUARTER)
                    {
                        return Enumerator.PeriodArchiveFolder.QUARTER.GetEnumDescription();
                    }
                    else if (PeriodReportChange == Enumerator.PeriodReportChange.MONTH)
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
                            return PeriodChangeMonth.GetEnumDescription();
                        }
                    }

                    return null;
                }                
            }
        }

        [MemberDesignTimeVisibility(false)]
        public PeriodReportChange? PeriodReportChange { get; set; }

        [MemberDesignTimeVisibility(false)]
        public PeriodArchiveFolder? PeriodArchiveFolder { get; set; }

        [MemberDesignTimeVisibility(false)]
        public Month? PeriodChangeMonth { get; set; }

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

        [DisplayName("День"), MemberDesignTimeVisibility(false)]
        public int Day { get; set; }

        [DisplayName("Год"), MemberDesignTimeVisibility(false)]
        public int Year { get; set; }

        [DisplayName("Месяц"), MemberDesignTimeVisibility(false)]
        public string MonthString => Month.GetEnumDescription();
        [MemberDesignTimeVisibility(false)]
        public Month Month { get; set; }

        [DisplayName("Статус")]
        public string StatusReportString => StatusReport.GetEnumDescription();
        [MemberDesignTimeVisibility(false)]
        public StatusReport StatusReport { get; set; }

        /// <summary>
        /// Дата сдачи.
        /// </summary>
        [DisplayName("Дата сдачи")]
        public DateTime? DateCompletion { get; set; }

        [DisplayName("Комментарий"), Size(1024)]
        public string Comment { get; set; }

        [DevExpress.Xpo.Association]
        [MemberDesignTimeVisibility(false)]
        public ReportChange ReportChange { get; set; }
    }
}
