using DevExpress.Xpo;

namespace RMS.Core.Model.InfoCustomer
{
    public class CustomerFilterElectronicReporting : XPObject
    {
        public CustomerFilterElectronicReporting() { }
        public CustomerFilterElectronicReporting(Session session) : base(session) { }

        [MemberDesignTimeVisibility(false)]
        public ElectronicReporting ElectronicReporting { get; set; }

        [Association]
        [MemberDesignTimeVisibility(false)]
        public CustomerFilter CustomerFilter { get; set; }

        public override string ToString()
        {
            return ElectronicReporting?.Name;
        }
    }
}