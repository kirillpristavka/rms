using DevExpress.Xpo;

namespace RMS.Core.Model.InfoContract.ContractAttachments
{
    /// <summary>
    /// Документы по налоговой отчетности.
    /// </summary>
    public class TaxReportingDocumentContract : XPObject
    {
        public TaxReportingDocumentContract() { }
        public TaxReportingDocumentContract(Session session) : base(session) { }

        [DisplayName("Документ")]
        public string TaxReportingDocumentString => TaxReportingDocument?.Name;

        [MemberDesignTimeVisibility(false)]
        public TaxReportingDocument TaxReportingDocument { get; set; }

        [Association, MemberDesignTimeVisibility(false)]
        public ContractAttachment ContractAttachment { get; set; }

        public override string ToString()
        {
            return TaxReportingDocument?.Name ?? default;
        }
    }
}
