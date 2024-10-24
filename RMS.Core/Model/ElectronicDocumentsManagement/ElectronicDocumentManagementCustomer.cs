using DevExpress.Xpo;
using RMS.Core.Model.InfoCustomer;
using System;
using System.Linq;

namespace RMS.Core.Model.ElectronicDocumentsManagement
{
    /// <summary>
    /// Электронная отчетность клиента.
    /// </summary>
    public class ElectronicDocumentManagementCustomer : XPObject
    {
        public ElectronicDocumentManagementCustomer() { }
        public ElectronicDocumentManagementCustomer(Session session) : base(session) { }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            ElectronicDocumentManagementCustomerObjects = GetCurrentObj();
        }

        protected override void OnSaved()
        {
            base.OnSaved(); 
            ElectronicDocumentManagementCustomerObjects = GetCurrentObj();
        }

        [DisplayName("Клиент")]
        public string CustomerName => Customer?.ToString();
        [MemberDesignTimeVisibility(false)]
        public Customer Customer { get; set; }

        [DisplayName("Электронная отчетность\n(провайдер)")]
        public string CurrentObjectString => CurrentObject?.ToString();
        [MemberDesignTimeVisibility(false)]
        public ElectronicDocumentManagementCustomerObject ElectronicDocumentManagementCustomerObjects { get; private set; }       
        /// <summary>
        /// Дата начала действия.
        /// </summary>
        [DisplayName("Дата\nначала\nдействия")]
        public DateTime? DateSince => ElectronicDocumentManagementCustomerObjects?.DateSince;
        /// <summary>
        /// Дата окончания действия.
        /// </summary>
        [DisplayName("Дата\nокончания\nдействия")]
        public DateTime? DateTo => ElectronicDocumentManagementCustomerObjects?.DateTo;
        [MemberDesignTimeVisibility(false)]
        public ElectronicDocumentManagement CurrentObject => ElectronicDocumentManagementCustomerObjects?.ElectronicDocumentManagement;
        
        public ElectronicDocumentManagementCustomerObject GetCurrentObj()
        {
            var dateNow = DateTime.Now.Date;
            return ElectronicDocumentManagementCustomerObject?.LastOrDefault(f =>
                f.ElectronicDocumentManagement != null &&
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
        public XPCollection<ElectronicDocumentManagementCustomerObject> ElectronicDocumentManagementCustomerObject
        {
            get
            {
                return GetCollection<ElectronicDocumentManagementCustomerObject>(nameof(ElectronicDocumentManagementCustomerObject));
            }
        }
        
        public override string ToString()
        {
            if (CurrentObject is null)
            {
                return "ЭДО отсутствует";
            }
            else
            {
                return CurrentObject?.ToString();
            }
        }
    }
}
