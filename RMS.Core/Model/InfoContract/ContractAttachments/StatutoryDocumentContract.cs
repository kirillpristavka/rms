using DevExpress.Xpo;

namespace RMS.Core.Model.InfoContract.ContractAttachments
{
    /// <summary>
    /// Уставной документ.
    /// </summary>
    public class StatutoryDocumentContract : XPObject
    {
        public StatutoryDocumentContract() { }
        public StatutoryDocumentContract(Session session) : base(session) { }

        [DisplayName("Документ")]
        public string StatutoryDocumentString => StatutoryDocument?.Name;

        [MemberDesignTimeVisibility(false)]
        public StatutoryDocument StatutoryDocument { get; set; }

        [Association, MemberDesignTimeVisibility(false)]
        public ContractAttachment ContractAttachment { get; set; }

        public override string ToString()
        {
            return StatutoryDocument?.Name ?? default;
        }
    }
}
