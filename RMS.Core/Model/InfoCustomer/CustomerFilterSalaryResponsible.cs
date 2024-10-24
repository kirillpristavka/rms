using DevExpress.Xpo;

namespace RMS.Core.Model.InfoCustomer
{
    /// <summary>
    /// Фильтр ответственного за ЗП.
    /// </summary>
    public class CustomerFilterSalaryResponsible : XPObject
    {
        public CustomerFilterSalaryResponsible() { }
        public CustomerFilterSalaryResponsible(Session session) : base(session) { }

        [MemberDesignTimeVisibility(false)]
        public Staff SalaryResponsible { get; set; }

        [Association, MemberDesignTimeVisibility(false)]
        public CustomerFilter CustomerFilter { get; set; }

        public override string ToString()
        {
            return SalaryResponsible?.ToString();
        }
    }
}