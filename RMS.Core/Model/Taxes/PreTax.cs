using DevExpress.Xpo;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Interface;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Notifications;
using RMS.Core.Model.Reports;
using System;

namespace RMS.Core.Model.Taxes
{
    /// <summary>
    /// Предварительные налоги.
    /// </summary>
    public class PreTax : XPObject, INotification
    {
        private PreTax() { }
        public PreTax(Session session) : base(session) { }
        public PreTax(Session session, int year, PeriodReportChange periodReportChange, Report report, string guid = default) : base(session) 
        {
            Year = year;
            PeriodReportChange = periodReportChange;
            Report = report;
            Guid = guid;
            
            if (periodReportChange == PeriodReportChange.FIRSTQUARTER)
            {
                DateDelivery = new DateTime(Year, 3, 25);
            }
            else if (periodReportChange == PeriodReportChange.SECONDQUARTER)
            {
                DateDelivery = new DateTime(Year, 6, 25);
            }
            else if (periodReportChange == PeriodReportChange.THIRDQUARTER)
            {
                DateDelivery = new DateTime(Year, 9, 25);
            }
            else if (periodReportChange == PeriodReportChange.FOURTHQUARTER)
            {
                DateDelivery = new DateTime(Year, 12, 25);
            }
        }

        [MemberDesignTimeVisibility(false)]
        public string Guid { get; set; }

        [DisplayName("Ответственный")]
        public string StaffString => Staff?.ToString();
        /// <summary>
        /// Ответственный сотрудник
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Staff Staff { get; set; }

        [DisplayName("Клиент")]
        public string CustomerString => Customer?.ToString();
        /// <summary>
        /// Клиент.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Customer Customer { get; set; }

        /// <summary>
        /// Дата формирования.
        /// </summary>
        [DisplayName("Дата формирования")]
        [MemberDesignTimeVisibility(false)]
        public DateTime? Date { get; set; } = DateTime.Now;

        /// <summary>
        /// Сдать до.
        /// </summary>
        [DisplayName("Сдать до")]
        public DateTime? DateDelivery { get; set; }

        /// <summary>
        /// Признак согласованности.
        /// </summary>
        [DisplayName("Согласовано")]
        [MemberDesignTimeVisibility(false)]
        public bool IsAgreement { get; set; }

        /// <summary>
        /// Дата согласования.
        /// </summary>
        [DisplayName("Дата согласования")]
        [MemberDesignTimeVisibility(false)]        
        public DateTime? DateAgreement { get; set; }

        [DisplayName("Период")]
        public PeriodReportChange PeriodReportChange { get; set; }

        [DisplayName("Год")]
        public int Year { get; set; } = DateTime.Now.Year;

        [DisplayName("Отчет")]
        public string ReportString => Report?.ToString();
        /// <summary>
        /// Отчет.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Report Report { get; set; }
        
        /// <summary>
        /// Выручка 90.
        /// </summary>
        [DisplayName("Выручка 90")]
        public decimal? Proceeds90 { get; set; }
        
        /// <summary>
        /// Выручка 51.
        /// </summary>
        [DisplayName("Выручка 51")]
        public decimal? Proceeds51 { get; set; }
        
        /// <summary>
        /// Предварительная сумма.
        /// </summary>
        [DisplayName("Предварительная сумма")]
        public decimal? PreliminaryAmount { get; set; }

        [DisplayName("Статус")]
        public string StatusString => StatusPreTax?.ToString();
        /// <summary>
        /// Статус предварительных налогов.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public StatusPreTax StatusPreTax { get; set; }

        /// <summary>
        /// Наибольший процент (%).
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        [DisplayName("Наибольший 1%")]
        public decimal? HighestPercentage { get; set; }        
        
        /// <summary>
        /// Сумма в декларации.
        /// </summary>
        [DisplayName("Сумма в декларации")]
        public decimal? AmountInDeclaration { get; set; }
        
        [MemberDesignTimeVisibility(false)]
        [DisplayName("Согласовано")]
        public string DateAgreementString
        {
            get
            {
                var result = default(string);

                if (IsAgreement && DateAgreement is DateTime date)
                {
                    result = $"Согласовано: {date.ToShortDateString()}";
                }
                
                return result;
            }
        }

        [DisplayName("1/3")]
        public string OutputString
        {
            get
            {
                var result = default(string);

                result += "<html><body><center>";
                
                result += GetStr(DateOne, PaymentOne);
                result += GetStr(DateTwo, PaymentTwo);
                result += GetStr(DateThree, PaymentThree);

                result += "</center></body></html>";
                return result?.Trim();
            }
        }

        [DisplayName("Комментарий")]
        [Size(2048)]
        public string Comment { get; set; }

        /// <summary>
        /// Использовать деление на 3?
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public bool IsUseDivisionBy3 { get; set; }

        /// <summary>
        /// Использовать авансы по прибыли?
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public bool IsUseAdvancesOnProfit { get; set; }

        [MemberDesignTimeVisibility(false)]
        public bool PaymentOne { get; set; }

        [MemberDesignTimeVisibility(false)]
        public bool PaymentTwo { get; set; }

        [MemberDesignTimeVisibility(false)]
        public bool PaymentThree { get; set; }

        [MemberDesignTimeVisibility(false)]
        public DateTime? DateOne { get; set; }
        
        [MemberDesignTimeVisibility(false)]
        public DateTime? DateTwo { get; set; }

        [MemberDesignTimeVisibility(false)]
        public DateTime? DateThree { get; set; }        
        
        private string GetStr(DateTime? date, bool isPayment)
        {
            var result = default(string);
            
            if (date != null)
            {
                var _date = Convert.ToDateTime(date);
                
                result += $"{_date.ToShortDateString()} ";                
                if (isPayment is true)
                {
                    result = $"<font color=\"green\">&#9989; {result}</font>";
                }
                else
                {
                    result = $"<font color=\"red\">&#10060; {result}</font>";
                }
                result = result.Trim() + Environment.NewLine;
                result = result.Trim() + "<br>";
            }

            return result;
        }        

        public override string ToString()
        {
            var result = default(string);

            if (DateDelivery is DateTime date)
            {
                result += date.ToShortDateString();
            }
            else
            {
                result += $"{PeriodReportChange.GetEnumDescription()} {Year}";
            }

            result += $" {ReportString}";
            
            return result.Trim();
        }

        public Notification GetNotification(TypeNotification typeNotification)
        {
            if (DateDelivery is DateTime deadline)
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

                if (Staff != null)
                {
                    name += $"{Environment.NewLine}Ответственный: {Staff}";
                }

                if (StatusPreTax != null)
                {
                    name += $"{Environment.NewLine}Статус: {StatusPreTax}";
                }

                name += $"{Environment.NewLine}Период: {PeriodReportChange.GetEnumDescription()}";

                return new Notification(typeNotification, "Предварительные налоги", deadline, name, Oid, typeof(PreTax));
            }
            else
            {
                return default;
            }
        }
    }
}
