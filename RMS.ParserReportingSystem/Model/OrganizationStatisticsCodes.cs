using System.Linq;

namespace RMS.ParserReportingSystem.Model
{
    /// <summary>
    /// Данные о кодах статистики организации.
    /// </summary>
    public class OrganizationStatisticsCodes
    {
        public OrganizationStatisticsCodes() { }

        /// <summary>
        /// Заполнение данных о кодах статистики организаций.
        /// </summary>
        /// <param name="okpo">ОКПО.</param>
        /// <param name="psrn">ОГРН.</param>
        /// <param name="inn">ИНН.</param>
        /// <param name="actualOKATO">ОКАТО фактический.</param>
        /// <param name="registrationOKATO">ОКАТО регистрации.</param>
        /// <param name="actualOKTMO">ОКТМО фактический.</param>
        /// <param name="registrationOKTMO">ОКТМО регистрации.</param>
        /// <param name="okogu">ОКОГУ.</param>
        /// <param name="okfs">ОКФС.</param>
        /// <param name="okopf">ОКОПФ.</param>
        public OrganizationStatisticsCodes(string okpo, string psrn, string inn, string actualOKATO, string registrationOKATO, 
            string actualOKTMO, string registrationOKTMO, string okogu, string okfs, string okopf)
        {
            OKPO = okpo;
            PSRN = psrn;
            INN = inn;

            if (!string.IsNullOrWhiteSpace(actualOKATO) && actualOKATO.Length >= 12)
            {
                actualOKATO = actualOKATO.Substring(0, 12);
            }

            if (!string.IsNullOrWhiteSpace(registrationOKATO) && registrationOKATO.Length >= 12)
            {
                registrationOKATO = registrationOKATO.Substring(0, 12);
            }

            if (!string.IsNullOrWhiteSpace(actualOKTMO) && actualOKTMO.Length >= 12)
            {
                actualOKTMO = actualOKTMO.Substring(0, 12);
            }

            if (!string.IsNullOrWhiteSpace(registrationOKTMO) && registrationOKTMO.Length >= 12)
            {
                registrationOKTMO = registrationOKTMO.Substring(0, 12);
            }

            ActualOKATO = actualOKATO;
            RegistrationOKATO = registrationOKATO;
            ActualOKTMO = actualOKTMO;
            RegistrationOKTMO = registrationOKTMO;
            OKOGU = okogu;
            OKFS = okfs;
            OKOPF = okopf;
        }

        /// <summary>
        /// ОКПО.
        /// </summary>
        public string OKPO { get; }

        /// <summary>
        /// ОГРН.
        /// </summary>
        public string PSRN { get; }

        /// <summary>
        /// ИНН.
        /// </summary>
        public string INN { get; }

        /// <summary>
        /// ОКАТО фактический.
        /// </summary>
        public string ActualOKATO { get; }

        /// <summary>
        /// ОКАТО регистрации.
        /// </summary>
        public string RegistrationOKATO { get; }

        /// <summary>
        /// ОКТМО фактический.
        /// </summary>
        public string ActualOKTMO { get; }

        /// <summary>
        /// ОКТМО регистрации.
        /// </summary>
        public string RegistrationOKTMO { get; }

        /// <summary>
        /// ОКОГУ.
        /// </summary>
        public string OKOGU { get; }

        /// <summary>
        /// ОКФС.
        /// </summary>
        public string OKFS { get; }

        /// <summary>
        /// ОКОПФ.
        /// </summary>
        public string OKOPF { get; }

        public override string ToString()
        {
            return $"ИНН: {INN}";
        }
    }
}
