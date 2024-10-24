using DevExpress.Xpo;

namespace RMS.Core.Model.InfoCustomer
{
    public class CustomerFilterUser : XPObject
    {
        public CustomerFilterUser() { }
        public CustomerFilterUser(Session session) : base(session) { }

        [MemberDesignTimeVisibility(false)]
        public User User { get; set; }

        [Association, MemberDesignTimeVisibility(false)]
        public CustomerFilter CustomerFilter { get; set; }

        public override string ToString()
        {
            return User?.ToString();
        }
    }
}
