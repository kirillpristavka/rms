using DevExpress.Xpo;
using RMS.Core.Model.Access;

namespace RMS.Core.Model
{
    public class UserGroup : XPObject
    {
        public UserGroup() { }
        public UserGroup(Session session) : base(session) { }

        [Size(256)]
        [DisplayName("Наименование")]
        public string Name { get; set; }

        [Size(1024)]
        [DisplayName("Описание")]
        public string Description { get; set; }
        
        /// <summary>
        /// Права доступа.
        /// </summary>
        [Aggregated]
        [MemberDesignTimeVisibility(false)]
        public AccessRights AccessRights { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
