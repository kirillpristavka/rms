using DevExpress.Xpo;

namespace RMS.Core.Model.InfoCustomer
{
    public class CustomerFilterUserGroup : XPObject
    {
        public CustomerFilterUserGroup() { }
        public CustomerFilterUserGroup(Session session) : base(session) { }

        [MemberDesignTimeVisibility(false)]
        public UserGroup UserGroup { get; set; }

        [Association, MemberDesignTimeVisibility(false)]
        public CustomerFilter CustomerFilter { get; set; }

        public override string ToString()
        {
            return UserGroup?.ToString();
        }
    }
}
