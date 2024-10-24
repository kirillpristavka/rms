using DevExpress.Xpo;
using RMS.Core.Model.InfoCustomer;
using System;
using System.Linq;

namespace RMS.Core.Model.Taxes
{
    /// <summary>
    /// Электронная отчетность клиента.
    /// </summary>
    public class ElectronicReportingCustomer : XPObject
    {
        public ElectronicReportingCustomer() { }
        public ElectronicReportingCustomer(Session session) : base(session) { }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            ElectronicReportingСustomerObject = GetCurrentElectronicReporting();
        }

        protected override void OnSaved()
        {
            base.OnSaved(); 
            ElectronicReportingСustomerObject = GetCurrentElectronicReporting();
        }

        [DisplayName("Клиент")]
        public string CustomerName => Customer?.ToString();
        [MemberDesignTimeVisibility(false)]
        public Customer Customer { get; set; }

        [DisplayName("Электронная отчетность\n(провайдер)")]
        public string CurrentElectronicReportingString => CurrentElectronicReporting?.ToString();

        [MemberDesignTimeVisibility(false)]
        public ElectronicReportingСustomerObject ElectronicReportingСustomerObject { get; private set; }

        /// <summary>
        /// Дата начала действия.
        /// </summary>
        [DisplayName("Дата\nначала\nдействия")]
        public DateTime? DateSince => ElectronicReportingСustomerObject?.DateSince;

        /// <summary>
        /// Дата окончания действия.
        /// </summary>
        [DisplayName("Дата\nокончания\nдействия")]
        public DateTime? DateTo => ElectronicReportingСustomerObject?.DateTo;

        [DisplayName("Лицензия\nначала\nдействия")]
        public DateTime? LicenseDateSince => ElectronicReportingСustomerObject?.LicenseDateSince;

        [DisplayName("Лицензия\nокончания\nдействия")]
        public DateTime? LicenseDateTo => ElectronicReportingСustomerObject?.LicenseDateTo;

        [MemberDesignTimeVisibility(false)]
        public ElectronicReporting CurrentElectronicReporting => ElectronicReportingСustomerObject?.ElectronicReporting;
        
        public ElectronicReportingСustomerObject GetCurrentElectronicReporting()
        {
            var dateNow = DateTime.Now.Date;
            return ElectronicReportingСustomerObjects?.LastOrDefault(f =>
                f.ElectronicReporting != null &&
                ((f.DateSince == null && f.DateTo >= dateNow)
                    || (f.DateSince <= dateNow && f.DateTo >= dateNow)
                    || (f.DateSince <= dateNow && f.DateTo == null)
                    || (f.DateSince == null && f.DateTo == null)));
        }
        
        /// <summary>
        /// Комментарий.
        /// </summary>
        [Size(1024)]
        [DisplayName("Комментарий")]
        public string Comment { get; set; }

        /// <summary>
        /// Провайдеры для ЭЦП.
        /// </summary>
        [Aggregated]
        [Association]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<ElectronicReportingСustomerObject> ElectronicReportingСustomerObjects
        {
            get
            {
                return GetCollection<ElectronicReportingСustomerObject>(nameof(ElectronicReportingСustomerObjects));
            }
        }

        [DisplayName("Оповещения")]
        public string Notifications
        {
            get
            {   
                var result = default(string);

                if (ElectronicReportingСustomerNotifications != null && ElectronicReportingСustomerNotifications.Count > 0)
                {
                    foreach (var item in ElectronicReportingСustomerNotifications)
                    {
                        result += $"{item}{Environment.NewLine}";
                    }
                }

                return result?.Trim();
            }
        }

        /// <summary>
        /// Оповещение об ЭЦП.
        /// </summary>
        [Aggregated]
        [Association]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<ElectronicReportingСustomerNotification> ElectronicReportingСustomerNotifications
        {
            get
            {
                return GetCollection<ElectronicReportingСustomerNotification>(nameof(ElectronicReportingСustomerNotifications));
            }
        }

        public override string ToString()
        {
            if (CurrentElectronicReporting is null)
            {
                return "Электронная отчетность отсутствует";
            }
            else
            {
                return CurrentElectronicReporting?.ToString();
            }
        }
    }
}
