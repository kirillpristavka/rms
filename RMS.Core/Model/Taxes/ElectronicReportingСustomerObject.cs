using DevExpress.Xpo;
using System;

namespace RMS.Core.Model.Taxes
{
    /// <summary>
    /// Объекты клиентской электронной отчетности.
    /// </summary>
    public class ElectronicReportingСustomerObject : XPObject
    {
        public ElectronicReportingСustomerObject() { }
        public ElectronicReportingСustomerObject(Session session) : base(session) { }

        [DisplayName("Электронная отчетность\n(провайдер)")]
        public string ElectronicReportingString => ElectronicReporting?.ToString();

        [MemberDesignTimeVisibility(false)]
        public ElectronicReporting ElectronicReporting { get; set; }

        /// <summary>
        /// Пользователь, который создал.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public User UserCreate { get; set; }

        /// <summary>
        /// Дата создания.
        /// </summary>        
        [DisplayName("Дата создания")]
        [MemberDesignTimeVisibility(false)]
        public DateTime DateCreate { get; set; } = DateTime.Now;

        /// <summary>
        /// Комментарий.
        /// </summary>
        [Size(1024)]
        [DisplayName("Комментарий")]
        public string Comment { get; set; }
        
        [DisplayName("Дата\nначала\nдействия")]
        public DateTime? DateSince { get; set; }

        [DisplayName("Дата\nокончания\nдействия")]
        public DateTime? DateTo { get; set; }

        [DisplayName("Лицензия\nначала\nдействия")]
        public DateTime? LicenseDateSince { get; set; }

        [DisplayName("Лицензия\nокончания\nдействия")]
        public DateTime? LicenseDateTo { get; set; }

        [Association]
        [MemberDesignTimeVisibility(false)]
        public ElectronicReportingCustomer ElectronicReportingCustomer { get; set; }

        public override string ToString()
        {
            var result = default(string);

            if (ElectronicReporting != null)
            {
                result += ElectronicReporting?.ToString();
            }

            if (DateSince != null)
            {
                result += $"c {DateSince.Value.ToShortDateString()}";
            }

            if (DateTo != null)
            {
                result += $"по {DateTo.Value.ToShortDateString()}";
            }

            if (!string.IsNullOrWhiteSpace(result))
            {
                return result;
            }
            
            return base.ToString();
        }
    }
}