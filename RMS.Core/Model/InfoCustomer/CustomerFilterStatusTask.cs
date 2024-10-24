using DevExpress.Xpo;

namespace RMS.Core.Model.InfoCustomer
{
    public class CustomerFilterStatusTask : XPObject
    {
        public CustomerFilterStatusTask() { }
        public CustomerFilterStatusTask(Session session) : base(session) { }
        
        [MemberDesignTimeVisibility(false)]
        public TaskStatus TaskStatus { get; set; }

        [Association]
        [MemberDesignTimeVisibility(false)]
        public CustomerFilter CustomerFilter { get; set; }

        public override string ToString()
        {
            return TaskStatus?.ToString();
        }
    }
}