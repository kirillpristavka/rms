using DevExpress.Xpo;

namespace RMS.Core.Model.InfoCustomer
{
    public class CustomerFilterGivenStaffTask : XPObject
    {
        public CustomerFilterGivenStaffTask() { }
        public CustomerFilterGivenStaffTask(Session session) : base(session) { }

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