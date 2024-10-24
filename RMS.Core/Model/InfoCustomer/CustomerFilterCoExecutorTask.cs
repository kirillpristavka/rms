using DevExpress.Xpo;

namespace RMS.Core.Model.InfoCustomer
{
    public class CustomerFilterCoExecutorTask : XPObject
    {
        public CustomerFilterCoExecutorTask() { }
        public CustomerFilterCoExecutorTask(Session session) : base(session) { }

        [Persistent(nameof(Staff))]
        [MemberDesignTimeVisibility(false)]
        public Staff CoExecutor { get; set; }        

        [Association]
        [MemberDesignTimeVisibility(false)]
        public CustomerFilter CustomerFilter { get; set; }

        public override string ToString()
        {
            return CoExecutor?.ToString();
        }
    }
}