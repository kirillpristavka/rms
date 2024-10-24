using DevExpress.Xpo;

namespace RMS.Core.Model.InfoContract.ContractAttachments
{
    /// <summary>
    /// Право устанавливающий документ.
    /// </summary>
    public class TitleDocumentContract : XPObject
    {
        public TitleDocumentContract() { }
        public TitleDocumentContract(Session session) : base(session) { }

        [DisplayName("Документ")]
        public string TitleDocumentString => TitleDocument?.Name;

        [MemberDesignTimeVisibility(false)]
        public TitleDocument TitleDocument { get; set; }

        [Association, MemberDesignTimeVisibility(false)]
        public ContractAttachment ContractAttachment { get; set; }

        public override string ToString()
        {
            return TitleDocument?.Name ?? default;
        }
    }
}
