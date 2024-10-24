using DevExpress.Xpo;

namespace RMS.Core.Model.InfoCustomer
{
    public class CustomerFilterStaffDeal : XPObject
    {
        public CustomerFilterStaffDeal() { }
        public CustomerFilterStaffDeal(Session session) : base(session) { }

        [MemberDesignTimeVisibility(false)]
        public Staff Staff { get; set; }

        [Association]
        [MemberDesignTimeVisibility(false)]
        public CustomerFilter CustomerFilter { get; set; }

        public override string ToString()
        {
            return Staff?.ToString();
        }
    }
}