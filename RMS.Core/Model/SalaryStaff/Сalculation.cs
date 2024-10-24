using DevExpress.Xpo;
using RMS.Core.Extensions;
using RMS.Core.Model.Salary;

namespace RMS.Core.Model.SalaryStaff
{
    /// <summary>
    /// Расчет.
    /// </summary>
    public class Сalculation : XPObject
    {
        public Сalculation() { }
        public Сalculation(Session session) : base(session) { }
        
        /// <summary>
        /// Основание.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Basis Basis { get; set; }

        [DisplayName("Выплата или удержание")]
        public string PayoutDictionaryName => PayoutDictionary?.ToString();
        [MemberDesignTimeVisibility(false)]
        public PayoutDictionary PayoutDictionary { get; set; }

        [DisplayName("Период")]
        public string Period => $"{Month}/{Year}";

        [DisplayName("Год")]
        [MemberDesignTimeVisibility(false)]
        public int Year { get; set; }

        [DisplayName("Месяц")]
        [MemberDesignTimeVisibility(false)]
        public int Month { get; set; }

        private decimal value;
        [DisplayName("Значение")]
        public decimal Value { get => value.GetDecimalRound(); set => this.value = value; }

        [Association]
        [MemberDesignTimeVisibility(false)]
        public Staff Staff { get; set; }

        public override string ToString()
        {
            return $"{PayoutDictionaryName} ({Value})";
        }
    }
}