using DevExpress.Xpo;

namespace RMS.Core.Model.InfoCustomer
{
    public class CustomerFilterTaxSystem : XPObject
    {
        public CustomerFilterTaxSystem() { }
        public CustomerFilterTaxSystem(Session session) : base(session) { }

        [MemberDesignTimeVisibility(false)]
        public TaxSystem TaxSystem { get; set; }

        [Association, MemberDesignTimeVisibility(false)]
        public CustomerFilter CustomerFilter { get; set; }

        public override string ToString()
        {
            return TaxSystem?.ToString();
        }
    }
}