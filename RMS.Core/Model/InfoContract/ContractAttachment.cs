using DevExpress.Xpo;
using RMS.Core.Enumerator;
using RMS.Core.Model.InfoContract.ContractAttachments;

namespace RMS.Core.Model.InfoContract
{
    /// <summary>
    /// Приложение к договору.
    /// </summary>
    public class ContractAttachment : XPObject
    {
        public ContractAttachment() { }
        public ContractAttachment(Session session) : base(session) { }

        [DisplayName("Наименование"), Size(256)]
        public string Name { get; set; }

        [DisplayName("Описание"), Size(1024)]
        public string Description { get; set; }

        /// <summary>
        /// Вариант приложения к договору.
        /// </summary>
        [DisplayName("Вариант приложения к договору")]
        public OptionContractAttachment OptionContractAttachment { get; set; }

        /// <summary>
        /// Уставные документы.
        /// </summary>
        [Association, Aggregated, MemberDesignTimeVisibility(false)]
        public XPCollection<StatutoryDocumentContract> StatutoryDocumentsContract
        {
            get
            {
                return GetCollection<StatutoryDocumentContract>(nameof(StatutoryDocumentsContract));
            }
        }

        /// <summary>
        /// Право устанавливающие документы.
        /// </summary>
        [Association, Aggregated, MemberDesignTimeVisibility(false)]
        public XPCollection<TitleDocumentContract> TitleDocumentsContract
        {
            get
            {
                return GetCollection<TitleDocumentContract>(nameof(TitleDocumentsContract));
            }
        }

        /// <summary>
        /// Документы по налоговой отчетности.
        /// </summary>
        [Association, Aggregated, MemberDesignTimeVisibility(false)]
        public XPCollection<TaxReportingDocumentContract> TaxReportingDocumentsContract
        {
            get
            {
                return GetCollection<TaxReportingDocumentContract>(nameof(TaxReportingDocumentsContract));
            }
        }

        /// <summary>
        /// Первичные документы.
        /// </summary>
        [Association, Aggregated, MemberDesignTimeVisibility(false)]
        public XPCollection<SourceDocumentContract> SourceDocumentsContract
        {
            get
            {
                return GetCollection<SourceDocumentContract>(nameof(SourceDocumentsContract));
            }
        }

        /// <summary>
        /// Анкетные данные сотрудников.
        /// </summary>
        [Association, Aggregated, MemberDesignTimeVisibility(false)]
        public XPCollection<EmployeeDetailsDocumentContract> EmployeeDetailsDocumentsContract
        {
            get
            {
                return GetCollection<EmployeeDetailsDocumentContract>(nameof(EmployeeDetailsDocumentsContract));
            }
        }

        /// <summary>
        /// Перечень услуг.
        /// </summary>
        [Association, Aggregated, MemberDesignTimeVisibility(false)]
        public XPCollection<ServiceListContract> ServiceListContract
        {
            get
            {
                return GetCollection<ServiceListContract>(nameof(ServiceListContract));
            }
        }

        [Association, MemberDesignTimeVisibility(false)]
        public Contract Contract { get; set; }
    }
}