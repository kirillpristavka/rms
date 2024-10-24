using DevExpress.Xpo;

namespace RMS.Core.Model.InfoCustomer
{
    public class CustomerFilterFormCorporation : XPObject
    {
        public CustomerFilterFormCorporation() { }
        public CustomerFilterFormCorporation(Session session) : base(session) { }

        [MemberDesignTimeVisibility(false)]
        public FormCorporation FormCorporation { get; set; }

        [Association, MemberDesignTimeVisibility(false)]
        public CustomerFilter CustomerFilter { get; set; }

        public override string ToString()
        {
            return FormCorporation?.ToString();
        }
    }
}