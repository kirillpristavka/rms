using DevExpress.Xpo;

namespace RMS.Core.Model.Salary
{
    /// <summary>
    /// Начисления с выплатами.
    /// </summary>
    public class StatementCustomerPayment : Payment
    {
        public StatementCustomerPayment() { }
        public StatementCustomerPayment(Session session) : base(session) { }

        [DisplayName("Выплаты или удержания")]
        public string PayoutDictionaryString => PayoutDictionary?.ToString();
        
        /// <summary>
        /// Выплаты или удержания.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public PayoutDictionary PayoutDictionary { get; set; }

        [Association]
        [MemberDesignTimeVisibility(false)]
        public StatementCustomer StatementCustomer { get; set; }
    }
}