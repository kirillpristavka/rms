using DevExpress.Xpo;
using RMS.Core.Model.InfoCustomer;
using System.Linq;

namespace RMS.Core.Model.Salary
{
    /// <summary>
    /// Выплаты.
    /// </summary>
    public class StatementCustomer : XPObject
    {
        public StatementCustomer() { }
        public StatementCustomer(Session session) : base(session) { }

        [DisplayName("Клиент")]
        public string CustomerString => Customer?.ToString();
        [MemberDesignTimeVisibility(false)]
        public Customer Customer { get; set; }

        [DisplayName("Сумма платежей")]
        public decimal? Summa => StatementCustomerPayments?.Sum(s => s.Value);

        /// <summary>
        /// Начисления с выплатами.
        /// </summary>
        [Association]
        [MemberDesignTimeVisibility(false)]
        [Aggregated]
        public XPCollection<StatementCustomerPayment> StatementCustomerPayments
        {
            get
            {
                return GetCollection<StatementCustomerPayment>(nameof(StatementCustomerPayments));
            }
        }
        
        [Association]
        [MemberDesignTimeVisibility(false)]
        public Statement Statement { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}