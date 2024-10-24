using Dadata;
using Dadata.Model;
using RMS.Core.Enumerator;
using System;

namespace RMS.Core.Controller
{
    public class GetInfoBankFromDaData
    {
        /// <summary>
        /// Адрес банка.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Платежное наименование банка.
        /// </summary>
        public string PaymentName { get; set; }

        /// <summary>
        /// Банковский идентификационный код (БИК) ЦБ РФ.
        /// </summary>
        public string BIC { get; set; }

        /// <summary>
        /// Регистрационный номер в ЦБ РФ.
        /// </summary>
        public string RegistrationNumber { get; set; }

        /// <summary>
        /// Город для платежного поручения.
        /// </summary>
        public string Town { get; set; }

        /// <summary>
        /// ОКПО.
        /// </summary>
        public string OKPO { get; set; }

        /// <summary>
        /// Корреспондентский счет в ЦБ РФ.
        /// </summary>
        public string CorrespondentAccount { get; set; }

        /// <summary>
        /// Банковский идентификационный код в системе SWIFT.
        /// </summary>
        public string SWIFT { get; set; }

        /// <summary>
        /// Статус.
        /// </summary>
        public StatusOrganization Status { get; set; }

        /// <summary>
        /// Номер телефона.
        /// </summary>
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

        public GetInfoBankFromDaData(string token, string bic)
        {
            var bank = UpdateFromDaData(token, bic.Trim());

            if (bank != null)
            {
                Address = bank.address?.value;
                PaymentName = bank.name?.payment;
                BIC = bank.bic;
                RegistrationNumber = bank.registration_number;
                Town = bank.payment_city;
                OKPO = bank.okpo;
                CorrespondentAccount = bank.correspondent_account;
                SWIFT = bank.swift;
                Status = (StatusOrganization)Convert.ToInt32(bank.state.status);
                Telephone = bank.phone;
                DateActuality = bank.state?.actuality_date;
                DateRegistration = bank.state?.registration_date;
                DateLiquidation = bank.state?.liquidation_date;
            }
        }

        public static Bank UpdateFromDaData(string token, string bic)
        {
            var suggestClient = new SuggestClient(token);
            var response = suggestClient.FindBank(bic);

            if (response.suggestions.Count <= 0)
            {
                return null;
            }

            var bank = response.suggestions[0]?.data;
            return bank;
        }
    }
}
