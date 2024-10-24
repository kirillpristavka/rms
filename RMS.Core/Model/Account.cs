using DevExpress.Xpo;
using RMS.Core.Model.InfoCustomer;
using System;

namespace RMS.Core.Model
{
    /// <summary>
    /// Счет в банке.
    /// </summary>
    public class Account : XPObject
    {
        public Account() { }
        public Account(Session session) : base(session) { }

        [Size(1024)]
        [DisplayName("Описание")]
        public string Description { get; set; }

        [Size(128)]
        [DisplayName("Номер счета")]
        public string AccountNumber { get; set; }

        [DisplayName("Валюта")]
        public string CurrencyISO => Currency?.ISO;

        [MemberDesignTimeVisibility(false)]
        public Currency Currency { get; set; }

        [Size(128)]
        [DisplayName("Корреспондентский счет")]
        public string CorrespondentAccountNumber { get; set; }

        [Size(128)]
        [DisplayName("Номер банковской карты")]
        public string BankCardNumber { get; set; }

        [DisplayName("Дата открытия")]
        public DateTime? OpeningDate { get; set; }

        [DisplayName("Дата закрытия")]
        public DateTime? ClosingDate { get; set; }

        [MemberDesignTimeVisibility(false)]
        public Bank Bank { get; set; }

        [Association]
        [MemberDesignTimeVisibility(false)]
        public Customer Customer { get; set; }

        [Aggregated]
        [MemberDesignTimeVisibility(false)]
        public BankAccess BankAccess { get; set; }

        public override string ToString()
        {
            var result = default(string);
            
            if (Bank != null)
            {
                result += $"Банк: \"{Bank}\" ";
            }

            if (!string.IsNullOrWhiteSpace(AccountNumber))
            {
                result += $"{AccountNumber}";
            }
            
            return result;
        }          
    }
}