using DevExpress.Xpo;

namespace RMS.Core.Model.InfoCustomer
{
    /// <summary>
    /// Фильтр ответственного главное бухгалтера.
    /// </summary>
    public class CustomerFilterAccountantResponsible : XPObject
    {
        public CustomerFilterAccountantResponsible() { }
        public CustomerFilterAccountantResponsible(Session session) : base(session) { }

        [MemberDesignTimeVisibility(false)]
        public Staff AccountantResponsible { get; set; }

        [Association, MemberDesignTimeVisibility(false)]
        public CustomerFilter CustomerFilter { get; set; }

        public override string ToString()
        {
            return AccountantResponsible?.ToString();
        }
    }
}