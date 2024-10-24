using DevExpress.Xpo;
using RMS.Core.Extensions;
using RMS.Core.Model.Salary;
using System;

namespace RMS.Core.Model.SalaryStaff
{
    /// <summary>
    /// Основание.
    /// </summary>
    public class Basis : XPObject
    {
        public Basis() { }
        public Basis(Session session) : base(session) { }

        [DisplayName("Срок действия с")]
        public DateTime? DateSince { get; set; }

        [DisplayName("Срок действия по")]
        public DateTime? DateTo { get; set; }

        [DisplayName("Выплата или удержание")]
        public string PayoutDictionaryName => ToString();
        [MemberDesignTimeVisibility(false)]
        public PayoutDictionary PayoutDictionary { get; set; }

        private decimal rate;
        [DisplayName("Ставка")]
        public decimal Rate { get => rate.GetDecimalRound(); set => rate = value; }

        [Association]
        [MemberDesignTimeVisibility(false)]
        public Staff Staff { get; set; }

        public override string ToString()
        {
            return PayoutDictionary?.ToString();
        }
    }
}