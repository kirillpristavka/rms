using DevExpress.Xpo;

namespace RMS.Core.Model
{
    public class UserGroups : XPObject
    {
        public UserGroups() { }
        public UserGroups(Session session) : base(session) { }

        [MemberDesignTimeVisibility(false)]
        public UserGroup UserGroup { get; set; }

        [Association, MemberDesignTimeVisibility(false)]
        public User User { get; set; }

        public override string ToString()
        {
            return User?.ToString();
        }
    }
}
