namespace RMS.Core.Controller.Print
{
    static class ReportVariables
    {
        /// <summary>
        /// Адрес.
        /// </summary>
        public static string ADDRESS { get; } = "<ADDRESS>";

        /// <summary>
        /// Телефон/факс.
        /// </summary>
        public static string TELEPHONE { get; } = "<TELEPHONE>";

        /// <summary>
        /// E-mail.
        /// </summary>
        public static string EMAIL { get; } = "<EMAIL>";

        /// <summary>
        /// ИНН.
        /// </summary>
        public static string INN { get; } = "<INN>";

        /// <summary>
        /// КПП.
        /// </summary>
        public static string KPP { get; } = "<KPP>";

        public static string OKPO { get; } = "<OKPO>";
        public static string OKVED { get; } = "<OKVED>";
        public static string OKTMO { get; } = "<OKTMO>";
        public static string OKATO { get; } = "<OKATO>";

        public static string FormCorporationKod { get; } = "<FORMCORPORATIONKOD>";
        public static string FormCorporationAbbreviatedName { get; } = "<FORMCORPORATIONABBREVIATEDNAME>";
        public static string FormCorporationFullName { get; } = "<FORMCORPORATIONFULLNAME>";

        public static string KindActivity { get; } = "<KINDACTIVITY>";

        public static string AccountantResponsible { get; } = "<ACCOUNTANTRESPONSIBLE>";
        public static string BankResponsible { get; } = "<BANKRESPONSIBLE>";
        public static string PrimaryResponsible { get; } = "<PRIMARYRESPONSIBLE>";

        public static string ServiceDetails { get; } = "<SERVICEDETAILS>";

        public static string TaxSystemCustomer { get; } = "<TAXSYSTEMCUSTOMER>";

        public static string ElectronicReporting { get; } = "<ELECTRONICREPORTING>";

        public static string Status { get; } = "<STATUS>";

        public static string StatisticalReports { get; } = "<STATISTICALREPORTS>";

        public static string Reports { get; } = "<REPORTS>";

        public static string Contract { get; } = "<CONTRACT>";

        /// <summary>
        /// ОГРН.
        /// </summary>
        public static string OGRN { get; } = "<OGRN>";

        /// <summary>
        /// День.
        /// </summary>
        public static string DAY { get; } = "<DAY>";

        /// <summary>
        /// Месяц.
        /// </summary>
        public static string MONTH { get; } = "<MONTH>";

        /// <summary>
        /// Год.
        /// </summary>
        public static string YEAR { get; } = "<YEAR>";

        /// <summary>
        /// Дата.
        /// </summary>
        public static string DATETIME { get; } = "<DATETIME>";

        /// <summary>
        /// Наименование клиента.
        /// </summary>
        public static string NAMEENTITY { get; } = "<NAMEENTITY>";

        /// <summary>
        /// Ф.И.О. составителя.
        /// </summary>
        public static string EMPLOYEEFIO { get; } = "<EMPLOYEEFIO>";

        /// <summary>
        /// Ф.И.О. лица утвердившего протокол.
        /// </summary>
        public static string APPROVEDPROTOCOLFIO { get; } = "<APPROVEDPROTOCOLFIO>";

        /// <summary>
        /// Клиент (наименование).
        /// </summary>
        public static string CUSTOMER { get; } = "<CUSTOMER>";

        /// <summary>
        /// Клиент (полное наименование).
        /// </summary>
        public static string CUSTOMERFULLNAME { get; } = "<CUSTOMERFULLNAME>";
        
        /// <summary>
        /// Клиент (сокращенное наименование).
        /// </summary>
        public static string CUSTOMERABBREVIATEDNAME { get; } = "<CUSTOMERABBREVIATEDNAME>";

        /// <summary>
        /// Представитель клиента без сокращений.
        /// </summary>
        public static string CUSTOMERMANAGEMENTFULL { get; } = "<CUSTOMERMANAGEMENTFULL>";

        /// <summary>
        /// Представитель клиента.
        /// </summary>
        public static string CUSTOMERMANAGEMENT { get; } = "<CUSTOMERMANAGEMENT>";

        /// <summary>
        /// Должность клиента.
        /// </summary>
        public static string CUSTOMERMANAGEMENTPOSITION { get; } = "<CUSTOMERMANAGEMENTPOSITION>";

        /// <summary>
        /// Город.
        /// </summary>
        public static string TOWN { get; } = "<TOWN>";

        /// <summary>
        /// Номер счета.
        /// </summary>
        public static string INVOICENUMBER { get; } = "<INVOICENUMBER>";

        /// <summary>
        /// Сумма счета.
        /// </summary>
        public static string INVOICESUM { get; } = "<INVOICESUM>";

        /// <summary>
        /// Сумма счета прописью.
        /// </summary>
        public static string INVOICESUMSTRING { get; } = "<INVOICESUMSTRING>";

        /// <summary>
        /// Дата оплаты до.
        /// </summary>
        public static string INVOICEDATETO { get; } = "<INVOICEDATETO>";

        /// <summary>
        /// Наименование организации.
        /// </summary>
        public static string ORGANIZATIONNAME { get; } = "<ORGANIZATIONNAME>";

        /// <summary>
        /// Наименование организации.
        /// </summary>
        public static string ORGANIZATIONFULLNAME { get; } = "<ORGANIZATIONFULLNAME>";

        /// <summary>
        /// Наименование организации.
        /// </summary>
        public static string ORGANIZATIONABBREVIATEDNAME { get; } = "<ORGANIZATIONABBREVIATEDNAME>";

        /// <summary>
        /// Руководитель организации.
        /// </summary>
        public static string ORGANIZATIONMANAGMENT { get; } = "<ORGANIZATIONMANAGMENT>";

        /// <summary>
        /// Должность руководителя организации.
        /// </summary>
        public static string ORGANIZATIONMANAGMENTPOSITION { get; } = "<ORGANIZATIONMANAGMENTPOSITION>";

        /// <summary>
        /// ИНН организации.
        /// </summary>
        public static string ORGANIZATIOINN { get; } = "<ORGANIZATIOINN>";

        /// <summary>
        /// КПП организации.
        /// </summary>
        public static string ORGANIZATIONKPP { get; } = "<ORGANIZATIONKPP>";

        /// <summary>
        /// Телефон организации.
        /// </summary>
        public static string ORGANIZATIONTELEPHONE { get; } = "<ORGANIZATIONTELEPHONE>";

        /// <summary>
        /// Дата с.
        /// </summary>
        public static string DATESINCE { get; } = "<DATESINCE>";

        /// <summary>
        /// Дата по.
        /// </summary>
        public static string DATETO { get; } = "<DATETO>";

        public static string CUSTOMERADDRESS { get; } = "<CUSTOMERADDRESS>";
        public static string CUSTOMERINN { get; } = "<CUSTOMERINN>";
        public static string CUSTOMERPSRN { get; } = "<CUSTOMERPSRN>";
        public static string CUSTOMERBANKINFO { get; } = "<CUSTOMERBANKINFO>";
        public static string CUSTOMERPHONES { get; } = "<CUSTOMERPHONES>";

        public static string ORGANIZATIONADDRESS { get; } = "<ORGANIZATIONADDRESS>";
        public static string ORGANIZATIONINN { get; } = "<ORGANIZATIONINN>";
        public static string ORGANIZATIONPSRN { get; } = "<ORGANIZATIONPSRN>";
        public static string ORGANIZATIONBANKINFO { get; } = "<ORGANIZATIONBANKINFO>";
        public static string ORGANIZATIONPHONES { get; } = "<ORGANIZATIONPHONES>";
        public static string ORGANIZATIONMANAGEMENTPOSITION { get; } = "<ORGANIZATIONMANAGEMENTPOSITION>";

        public static string CONTRACTNUMBER { get; } = "<CONTRACTNUMBER>";
    }
}
