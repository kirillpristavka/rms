using DevExpress.Xpo;

namespace RMS.Core.Model.InfoCustomer
{
    public class CustomerFilterStatusDeal : XPObject
    {
        public CustomerFilterStatusDeal() { }
        public CustomerFilterStatusDeal(Session session) : base(session) { }

        [MemberDesignTimeVisibility(false)]
        public DealStatus DealStatus { get; set; }

        [Association]
        [MemberDesignTimeVisibility(false)]
        public CustomerFilter CustomerFilter { get; set; }

        public override string ToString()
        {
            return DealStatus?.ToString();
        }
    }
}