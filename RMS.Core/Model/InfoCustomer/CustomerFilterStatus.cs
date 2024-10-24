using DevExpress.Xpo;

namespace RMS.Core.Model.InfoCustomer
{
    public class CustomerFilterStatus : XPObject
    {
        public CustomerFilterStatus() { }
        public CustomerFilterStatus(Session session) : base(session) { }

        [MemberDesignTimeVisibility(false)]
        public Status Status { get; set; }

        [Association, MemberDesignTimeVisibility(false)]
        public CustomerFilter CustomerFilter { get; set; }

        public override string ToString()
        {
            return Status?.ToString();
        }
    }
}