using DevExpress.Xpo;
using RMS.Core.Model.OKVED;

namespace RMS.Core.Model.InfoCustomer
{
    public class CustomerFilterClassOKVED2 : XPObject
    {
        public CustomerFilterClassOKVED2() { }
        public CustomerFilterClassOKVED2(Session session) : base(session) { }

        [MemberDesignTimeVisibility(false)]
        public ClassOKVED2 ClassOKVED2 { get; set; }

        [Association, MemberDesignTimeVisibility(false)]
        public CustomerFilter CustomerFilter { get; set; }

        public override string ToString()
        {            
            return ClassOKVED2?.ToString(); ;
        }
    }
}