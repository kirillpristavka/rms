using DevExpress.Xpo;

namespace RMS.Core.Model.InfoCustomer
{
    /// <summary>
    /// Фильтр ответственного за первичку.
    /// </summary>
    public class CustomerFilterPrimaryResponsible : XPObject
    {
        public CustomerFilterPrimaryResponsible() { }
        public CustomerFilterPrimaryResponsible(Session session) : base(session) { }

        [MemberDesignTimeVisibility(false)]
        public Staff PrimaryResponsible { get; set; }

        [Association, MemberDesignTimeVisibility(false)]
        public CustomerFilter CustomerFilter { get; set; }

        public override string ToString()
        {
            return PrimaryResponsible?.ToString();
        }
    }
}