using DevExpress.Xpo;

namespace RMS.Core.Model.InfoCustomer
{
    public class CustomerFilterCustomerDeal : XPObject
    {
        public CustomerFilterCustomerDeal() { }
        public CustomerFilterCustomerDeal(Session session) : base(session) { }

        [MemberDesignTimeVisibility(false)]
        public Customer Customer { get; set; }

        [Association]
        [MemberDesignTimeVisibility(false)]
        public CustomerFilter CustomerFilter { get; set; }

        public override string ToString()
        {
            return Customer?.ToString();
        }
    }
}