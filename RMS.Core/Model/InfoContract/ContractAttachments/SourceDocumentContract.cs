using DevExpress.Xpo;

namespace RMS.Core.Model.InfoContract.ContractAttachments
{
    /// <summary>
    /// Первичные документы.
    /// </summary>
    public class SourceDocumentContract : XPObject
    {
        public SourceDocumentContract() { }
        public SourceDocumentContract(Session session) : base(session) { }

        [DisplayName("Документ")]
        public string SourceDocumentDocumentString => SourceDocument?.Name;

        [MemberDesignTimeVisibility(false)]
        public SourceDocument SourceDocument { get; set; }

        [Association, MemberDesignTimeVisibility(false)]
        public ContractAttachment ContractAttachment { get; set; }

        public override string ToString()
        {
            return SourceDocument?.Name ?? default;
        }
    }
}
