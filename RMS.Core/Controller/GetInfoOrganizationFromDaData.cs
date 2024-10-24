using Dadata;
using Dadata.Model;
using System;
using System.Threading.Tasks;

namespace RMS.Core.Controller
{
    /// <summary>
    /// Получение информации о организации по ее ИНН с сервера DaData (https://dadata.ru/).
    /// </summary>
    public class GetInfoOrganizationFromDaData
    {
        public string Name;
        public string FullName { get; set; }
        public string AbbreviatedName { get; set; }

        /// <summary>
        /// Количество сотрудников.
        /// </summary>
        public string OKPO { get; private set; }

        /// <summary>
        /// Дата выдачи ОГРН.
        /// </summary>
        public DateTime? DateOGRN { get; private set; }
        public string OGRN { get; private set; }
        public string KPP { get; private set; }
        public string INN { get; private set; }

        /// <summary>
        /// Фамилия руководителя.
        /// </summary>
        public string ManagementSurname { get; set; }

        /// <summary>
        /// Имя руководителя.
        /// </summary>
        public string ManagementName { get; set; }

        /// <summary>
        /// Отчество руководителя.
        /// </summary>
        public string ManagementPatronymic { get; set; }

        /// <summary>
        /// Должность.
        /// </summary>
        public string ManagementPosition { get; set; }

        public string OKVED { get; private set; }

        public string Address { get; private set; }

        /// <summary>
        /// Код ОКОПФ.
        /// </summary>
        public string OKOPF { get; set; }

        /// <summary>
        /// Полное название ОПФ.
        /// </summary>
        public string FullNameOPF { get; set; }

        /// <summary>
        /// Короткое название ОПФ.
        /// </summary>
        public string AbbreviatedNameOPF { get; set; }

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

        public PartyStatus OrganizationStatus { get; set; }

        public string Token { get; set; }
        public string SecretKey { get; set; }

        /// <summary>
        /// Получение информации о организации по ее ИНН с сервера DaData.
        /// </summary>
        /// <param name="token">Знак доступа.</param>
        /// <param name="secretKey">Секретный ключ.</param>
        /// <param name="inn">ИНН организации.</param>
        public GetInfoOrganizationFromDaData(string token, string secretKey, string inn)
        {            
            Token = token;
            SecretKey = secretKey;
            INN = inn;
        }
        
        public async Task GetDataAsync()
        {
            var party = await UpdateFromDaData(Token, SecretKey, INN.Trim());

            if (party != null)
            {
                Name = party.name?.full ?? party.name?.short_with_opf;
                FullName = party.name?.full_with_opf;
                AbbreviatedName = party.name?.short_with_opf;

                Address = party.address?.unrestricted_value;
                OKVED = party.okved;
                INN = party.inn;
                KPP = party.kpp;
                OGRN = party.ogrn;
                DateOGRN = party.ogrn_date;
                OKPO = party.okpo;

                var managmentFullName = party.management?.name?.Split(' ');

                if (managmentFullName != null)
                {
                    if (managmentFullName.Length >= 3)
                    {
                        ManagementSurname = managmentFullName[0];
                        ManagementName = managmentFullName[1];
                        ManagementPatronymic = managmentFullName[2];
                    }
                    else
                    {
                        ManagementSurname = party.management.name;
                    }

                    var managementPosition = party.management?.post;

                    if (!string.IsNullOrWhiteSpace(managementPosition) && managementPosition.Length > 1)
                    {
                        managementPosition = $"{managementPosition.Substring(0, 1).ToUpper()}{managementPosition.Substring(1).ToLower()}";
                    }

                    ManagementPosition = managementPosition;
                }
                else if (party.type == PartyType.INDIVIDUAL)
                {
                    var individualFullName = party.name?.full.Split(' ');

                    if (individualFullName.Length == 3)
                    {
                        ManagementSurname = individualFullName[0];
                        ManagementName = individualFullName[1];
                        ManagementPatronymic = individualFullName[2];
                    }
                    else
                    {
                        ManagementSurname = party.name?.full;
                    }
                    ManagementPosition = "Руководитель";
                }

                OKOPF = party.opf?.code;
                FullNameOPF = party.opf?.full;
                AbbreviatedNameOPF = party.opf.@short;

                DateActuality = party.state?.actuality_date;
                DateRegistration = party.state?.registration_date;
                DateLiquidation = party.state?.liquidation_date;

                OrganizationStatus = party.state.status;
            }
        }

        public async static Task<Party> UpdateFromDaData(string token, string secretKey, string inn)
        {
            if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(secretKey))
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(inn) || (inn.Length < 10 && inn.Length > 12))
            {
                return null;
            }

            try
            {
                var suggestClient = new SuggestClientAsync(token);
                var response = await suggestClient.FindParty(inn);

                if (response.suggestions.Count <= 0)
                {
                    return null;
                }

                return response.suggestions[0]?.data;
            }
            catch (System.Net.WebException ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                return null;
            }
        }
        
        public static OkvedRecord GetOkved2(string token, string value)
        {
            var api = new OutwardClient(token);
            var response = api.Suggest<OkvedRecord>(value);

            if (response.suggestions.Count > 0)
            {
                return response.suggestions[0].data;
            }
            else
            {
                return default;
            }            
        }

        public override string ToString()
        {
            return $"{Address}\n{OKVED}\n\n{INN}\n{KPP}\n{OGRN}\n{DateOGRN}\n{OKPO}\n";
        }
    }
}
