using DevExpress.Xpo;
using RMS.Core.Model.InfoContract;

namespace RMS.Core.Model.InfoCustomer
{
    /// <summary>
    /// Фильтр статусов договора.
    /// </summary>
    public class CustomerFilterContractStatus : XPObject
    {
        public CustomerFilterContractStatus() { }
        public CustomerFilterContractStatus(Session session) : base(session) { }

        [MemberDesignTimeVisibility(false)]
        public ContractStatus ContractStatus { get; set; }

        [Association, MemberDesignTimeVisibility(false)]
        public CustomerFilter CustomerFilter { get; set; }

        public override string ToString()
        {
            return ContractStatus?.ToString();
        }
    }
}
