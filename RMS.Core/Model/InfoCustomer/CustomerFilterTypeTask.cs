using DevExpress.Xpo;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;

namespace RMS.Core.Model.InfoCustomer
{
    public class CustomerFilterTypeTask : XPObject
    {
        public CustomerFilterTypeTask() { }
        public CustomerFilterTypeTask(Session session) : base(session) { }

        [MemberDesignTimeVisibility(false)]
        public TypeTask TypeTask { get; set; }
        
        [Association]
        [MemberDesignTimeVisibility(false)]
        public CustomerFilter CustomerFilter { get; set; }

        public override string ToString()
        {
            return TypeTask.GetEnumDescription();
        }
    }
}