using DevExpress.Xpo;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model.InfoCustomer;
using System;

namespace RMS.Core.Model.Reports
{
    public class ReportChangeCustomerV2 : XPObject
    {
        public ReportChangeCustomerV2() { }
        public ReportChangeCustomerV2(Session session) : base(session) { }

        [DisplayName("Год")]
        [MemberDesignTimeVisibility(false)]
        public int Year { get; set; }

        [DisplayName("Год")]
        public int? YearString
        {
            get
            {
                if (DeliveryTime is DateTime deliveryTime && deliveryTime != DateTime.MinValue)
                {
                    return deliveryTime.Year;
                }
                else if (Year > 0)
                {
                    return Year;
                }

                return default;
            }
        }

        [DisplayName("Ответственный")]
        public string AccountantResponsibleString => AccountantResponsible?.ToString();
        /// <summary>
        /// Ответственный.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Staff AccountantResponsible { get; set; }

        [DisplayName("Клиент")]
        [MemberDesignTimeVisibility(false)]
        public string CustomerString
        {
            get
            {
                if (Customer != null)
                {
                    var result = Customer?.ToString();
                    if (!string.IsNullOrWhiteSpace(Customer?.TaxSystemCustomerString))
                    {
                        result += $" ({Customer?.TaxSystemCustomerString})";
                    }
                    return result;
                }

                return default;
            }
        }
        /// <summary>
        /// Клиент.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Customer Customer { get; set; }

        [DisplayName("Срок предоставления (уплаты)")]
        public DateTime DeliveryTime { get; set; }

        [DisplayName("Отчет")]
        public string ReportString => ReportV2?.ToString();
        /// <summary>
        /// Отчет.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public ReportV2 ReportV2 { get; set; }

        [DisplayName("Период")]
        public string Period { get; set; }

        [DisplayName("Статус")]
        public string StatusString
        {
            get
            {
                return Status;
                return StatusReport.GetEnumDescription();
            }
        }

        [MemberDesignTimeVisibility(false)]
        public string Status { get; set; }

        [MemberDesignTimeVisibility(false)]
        public StatusReport? StatusReport { get; set; }

        /// <summary>
        /// Дата сдачи.
        /// </summary>
        [DisplayName("Дата сдачи")]
        public DateTime? DateCompletion { get; set; }

        [Size(1024)]
        [DisplayName("Комментарий")]
        public string Comment { get; set; }

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
