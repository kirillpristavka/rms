using DevExpress.Xpo;
using RMS.Core.Enumerator;
using System;
using System.Linq;

namespace RMS.Core.Model.Salary
{
    /// <summary>
    /// Ведомость.
    /// </summary>
    public class Statement : XPObject
    {
        public Statement() { }
        public Statement(Session session) : base(session) { }

        /// <summary>
        /// Дата создания.
        /// </summary>
        [DisplayName("Дата создания")]
        public DateTime Date { get; set; } = DateTime.Now;

        [DisplayName("Месяц")]
        public Month Month { get; set; } = (Month)DateTime.Now.Month;

        [DisplayName("Год")]
        public int Year { get; set; } = DateTime.Now.Year;

        [DisplayName("Количество")]
        public int? CountCustomer => StatementCustomers?.Count;

        [DisplayName("Сумма")]
        public decimal? Summa => StatementCustomers?.Sum(s => s.Summa);

        /// <summary>
        /// Клиенты с начислениями по ведомости.
        /// </summary>
        [Association]
        [MemberDesignTimeVisibility(false)]
        [Aggregated]
        public XPCollection<StatementCustomer> StatementCustomers
        {
            get
            {
                return GetCollection<StatementCustomer>(nameof(StatementCustomers));
            }
        }
    }
}
