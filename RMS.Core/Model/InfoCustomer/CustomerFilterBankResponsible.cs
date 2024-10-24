using DevExpress.Xpo;

namespace RMS.Core.Model.InfoCustomer
{
    /// <summary>
    /// Фильтр ответственного за банк.
    /// </summary>
    public class CustomerFilterBankResponsible : XPObject
    {
        public CustomerFilterBankResponsible() { }
        public CustomerFilterBankResponsible(Session session) : base(session) { }

        [MemberDesignTimeVisibility(false)]
        public Staff BankResponsible { get; set; }

        [Association, MemberDesignTimeVisibility(false)]
        public CustomerFilter CustomerFilter { get; set; }

        public override string ToString()
        {
            return BankResponsible?.ToString();
        }
    }
}