using DevExpress.Xpo;
using System;

namespace RMS.Core.Model
{
    /// <summary>
    /// Банк.
    /// </summary>
    public class Bank : XPObject
    {
        public Bank() { }
        public Bank(Session session) : base(session) { }

        /// <summary>
        /// Адрес банка.
        /// </summary>
        [DisplayName("Адрес"), Size(1024)]
        public string Address { get; set; }

        /// <summary>
        /// Платежное наименование банка.
        /// </summary>
        [DisplayName("Платежное наименование"), Size(256)]
        public string PaymentName { get; set; }

        /// <summary>
        /// Банковский идентификационный код (БИК) ЦБ РФ.
        /// </summary>
        [DisplayName("БИК"), Size(32)]
        public string BIC { get; set; }

        /// <summary>
        /// Регистрационный номер в ЦБ РФ.
        /// </summary>
        [DisplayName("Регистрационный номер"), Size(32)]
        public string RegistrationNumber { get; set; }

        /// <summary>
        /// Город для платежного поручения.
        /// </summary>
        [DisplayName("Город"), Size(128)]
        public string Town { get; set; }

        /// <summary>
        /// ОКПО.
        /// </summary>
        [DisplayName("ОКПО"), Size(32)]
        public string OKPO { get; set; }

        /// <summary>
        /// Корреспондентский счет в ЦБ РФ.
        /// </summary>
        [DisplayName("Корреспондентский счет"), Size(128)]
        public string CorrespondentAccount { get; set; }

        /// <summary>
        /// Банковский идентификационный код в системе SWIFT.
        /// </summary>
        [DisplayName("SWIFT"), Size(32)]
        public string SWIFT { get; set; }

        /// <summary>
        /// Статус.
        /// </summary>
        public Enumerator.StatusOrganization Status { get; set; }

        /// <summary>
        /// Номер телефона.
        /// </summary>
        [DisplayName("Телефон"), Size(32)]
        public string Telephone { get; set; }

        /// <summary>
        /// Дата актуальности сведений.
        /// </summary>
        public DateTime? DateActuality { get; set; }

        /// <summary>
        /// Дата регистрации.
        /// </summary>
        public DateTime? DateRegistration { get; set; }

        /// <summary>
        /// Дата ликвидации.
        /// </summary>
        public DateTime? DateLiquidation { get; set; }

        public override string ToString() 
        { 
            return PaymentName; 
        }
    }
}
