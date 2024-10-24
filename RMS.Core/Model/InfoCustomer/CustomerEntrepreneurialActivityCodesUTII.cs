using DevExpress.Xpo;
using RMS.Core.Model.Taxes;

namespace RMS.Core.Model.InfoCustomer
{
    /// <summary>
    /// Коды предпринимательской деятельности клиента для ЕНВД.
    /// </summary>
    public class CustomerEntrepreneurialActivityCodesUTII : XPObject
    {
        public CustomerEntrepreneurialActivityCodesUTII() { }
        public CustomerEntrepreneurialActivityCodesUTII(Session session) : base(session) { }

        [MemberDesignTimeVisibility(false)]
        public EntrepreneurialActivityCodesUTII EntrepreneurialActivityCodesUTII { get; set; }

        [Association, MemberDesignTimeVisibility(false)]
        public TaxSingleTemporaryIncome TaxSingleTemporaryIncome { get; set; }

        public override string ToString()
        {
            return EntrepreneurialActivityCodesUTII?.ToString();
        }
    }
}