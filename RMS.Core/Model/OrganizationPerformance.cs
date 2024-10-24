using DevExpress.Xpo;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model.InfoCustomer;
using System;
using System.Linq;

namespace RMS.Core.Model
{
    /// <summary>
    /// Показатели работы организации.
    /// </summary>
    public class OrganizationPerformance : XPObject
    {
        public OrganizationPerformance() { }
        public OrganizationPerformance(Session session) : base(session) { }

        [DisplayName("Включена в счет")]
        public bool IsInvoice
        {
            get
            {
                if (Invoice is null)
                {
                    return false;
                }
                else if (Invoice.IsDeleted)
                {
                    Invoice = null;
                    Save();
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        [Persistent]
        [MemberDesignTimeVisibility(false)]
        public DateTime Date => new DateTime(Year, (int)Month, 1);

        /// <summary>
        /// Показатели входящие в отчет.
        /// </summary>
        [Association, MemberDesignTimeVisibility(false)]
        public XPCollection<CustomerPerformanceIndicator> CustomerPerformanceIndicators
        {
            get
            {
                return GetCollection<CustomerPerformanceIndicator>(nameof(CustomerPerformanceIndicators));
            }
        }
        
        /// <summary>
        /// Счет.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Invoice Invoice { get; set; }

        /// <summary>
        /// Период.
        /// </summary>
        [DisplayName("Период")]
        public string Period => $"{Month.GetEnumDescription()} {Year}г.";

        /// <summary>
        /// Учитываемые операции.
        /// </summary>
        [DisplayName("Учитываемые операции")]
        public string Operation
        {
            get
            {
                var result = default(string);

                var count = 0;
                foreach (var item in CustomerPerformanceIndicators.Where(w => w.Value != null
                    && !string.IsNullOrWhiteSpace(w.Value)
                    && (w.PerformanceIndicator.TypePerformanceIndicator == TypePerformanceIndicator.Base
                    || w.PerformanceIndicator.TypePerformanceIndicator == TypePerformanceIndicator.Analysis)
                    && w.GroupPerformanceIndicatorString.Contains("Операции")))
                {
                    if (int.TryParse(item.Value, out int value))
                    {
                        count += value;
                    }
                }

                if (count > 0)
                {
                    result = count.ToString();
                }

                return result;
            }
        }

        /// <summary>
        /// Нюансы.
        /// </summary>
        [DisplayName("Нюансы")]
        public string Nuances
        {
            get
            {
                var result = default(string);
                                                
                foreach (var item in CustomerPerformanceIndicators?.Where(w => w.Value != null
                    && !string.IsNullOrWhiteSpace(w.Value)
                    && bool.TryParse(w.Value, out bool boolResult)
                    && boolResult is true
                    && w.PerformanceIndicator.TypePerformanceIndicator == TypePerformanceIndicator.Percent))
                {
                    result += $"{item}{Environment.NewLine}";
                }               

                return result?.Trim();
            }
        }
        
        /// <summary>
        /// Сотрудники.
        /// </summary>
        [DisplayName("Сотрудники")]
        public string Staff
        {
            get
            {
                var result = default(string);

                foreach (var item in CustomerPerformanceIndicators?.Where(w => w.Value != null
                    && !string.IsNullOrWhiteSpace(w.Value)
                    && int.TryParse(w.Value, out int intResult)
                    && intResult > 0
                    && w.GroupPerformanceIndicatorString.Contains("Сотрудник")))
                {
                    item?.PerformanceIndicator?.Reload();
                    var name = item?.PerformanceIndicatorAbbreviatedName ?? item?.ToString();
                    result += $"{name}-{item.Value}{Environment.NewLine}";
                }

                return result?.Trim();
            }
        }

        /// <summary>
        /// Стоимость.
        /// </summary>
        [DisplayName("Стоимость")]
        public string Price
        {
            get
            {
                var result = default(string);

                if (Invoice != null)
                {
                    result += $"{Invoice.Value} руб.";
                }
                
                return result;
            }
        }

        [DisplayName("Комментарий")]
        [Size(1024)]
        public string Comment { get; set; }        
        
        /// <summary>
        /// Месяц.
        /// </summary>
        [DisplayName("Месяц")]
        [MemberDesignTimeVisibility(false)]
        [System.ComponentModel.Browsable(false)]
        public Month Month { get; set; }

        /// <summary>
        /// Год.
        /// </summary>
        [DisplayName("Год")]
        [MemberDesignTimeVisibility(false)]
        public int Year { get; set; } = DateTime.Now.Year;

        /// <summary>
        /// Квартал.
        /// </summary>
        [DisplayName("Квартал")]
        [MemberDesignTimeVisibility(false)]
        public PeriodReportChange PeriodReportChangeQuarter { get; set; }

        /// <summary>
        /// Полугодие.
        /// </summary>
        [DisplayName("Полугодие")]
        [MemberDesignTimeVisibility(false)]
        public PeriodReportChange PeriodReportChangeHalfYear { get; set; }        

        [Association]
        [MemberDesignTimeVisibility(false)]
        public Customer Customer { get; set; }

        public override string ToString()
        {
            return $"{Month} {Year}г.";
        }
    }
}
