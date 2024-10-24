using DevExpress.Xpo;

namespace RMS.Core.Model.InfoContract.ContractAttachments
{
    /// <summary>
    /// Анкетные данные сотрудников.
    /// </summary>
    public class EmployeeDetailsDocumentContract : XPObject
    {
        public EmployeeDetailsDocumentContract() { }
        public EmployeeDetailsDocumentContract(Session session) : base(session) { }

        [DisplayName("Документ")]
        public string EmployeeDetailsDocumentString => EmployeeDetailsDocument?.Name;

        [MemberDesignTimeVisibility(false)]
        public EmployeeDetailsDocument EmployeeDetailsDocument { get; set; }

        [Association, MemberDesignTimeVisibility(false)]
        public ContractAttachment ContractAttachment { get; set; }

        public override string ToString()
        {
            return EmployeeDetailsDocument?.Name ?? default;
        }
    }
}
