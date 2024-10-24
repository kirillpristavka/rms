using System.ComponentModel;
using DevExpress.Xpo;
using RMS.Core.Model.InfoCustomer;
using DisplayNameAttribute = DevExpress.Xpo.DisplayNameAttribute;

namespace RMS.Setting.Model.CustomerSettings
{
    /// <summary>
    /// Настройка отображения таблицы клиентов.
    /// </summary>
    public class CustomerSettings : XPObject
    {
        public CustomerSettings() { }
        public CustomerSettings(Session session) : base(session) { }

        /// <summary>
        /// Наименование.
        /// </summary>
        [MemberDesignTimeVisibility(false), Browsable(false)]
        public string Name { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        [MemberDesignTimeVisibility(false), Browsable(false)]
        public string Description { get; set; }

        /// <summary>
        /// Стандартный шаблон для отображения.
        /// </summary>
        [MemberDesignTimeVisibility(false), Browsable(false)]
        public bool IsDefault { get; set; }

        [DisplayName("Статистика")]
        [Category("Общая информация")]
        public bool IsVisibleStatusStatisticalReport { get; set; }

        [DisplayName("ИНН")]
        [Category("Реквизиты")]
        public bool IsVisibleINN { get; set; }

        [DisplayName("Наименование")]
        [Category("Общая информация")]
        public bool IsVisibleName { get; set; }

        [DisplayName("Наименование (фильтрация)")]
        [Category("Общая информация")]
        public bool IsVisibleProcessedName { get; set; }

        [DisplayName("Наименование (обработанное)")]
        [Category("Общая информация")]
        public bool IsVisibleDefaultName { get; set; }

        [DisplayName("Полное наименование")]
        [Category("Общая информация")]
        public bool IsVisibleFullName { get; set; }

        [DisplayName("Сокращенное наименование")]
        [Category("Общая информация")]
        public bool IsVisibleAbbreviatedName { get; set; }

        /// <summary>
        /// Дата актуальности состояния организации.
        /// </summary>
        [DisplayName("Дата актуальности состояния организации")]
        [Category("Общая информация")]
        public bool IsVisibleDateActuality { get; set; }

        /// <summary>
        /// Дата регистрации.
        /// </summary>
        [DisplayName("Дата регистрации")]
        [Category("Общая информация")]
        public bool IsVisibleDateRegistration { get; set; }

        /// <summary>
        /// Дата ликвидации.
        /// </summary>
        [DisplayName("Дата ликвидации")]
        [Category("Общая информация")]
        public bool IsVisibleDateLiquidation { get; set; }

        /// <summary>
        /// Статус организации.
        /// </summary>
        [DisplayName("Статус организации")]
        [Category("Общая информация")]
        public bool IsVisibleOrganizationStatus { get; set; }

        [DisplayName("Руководитель [Фамилия И.О.]")]
        [Category("Контактные данные")]
        public bool IsVisibleManagementString {get; set;}

        /// <summary>
        /// Руководитель (Фамилия).
        /// </summary>
        [DisplayName("Руководитель (Фамилия)")]
        [Category("Контактные данные")]
        public bool IsVisibleManagementSurname { get; set; }

        /// <summary>
        /// Руководитель (Имя).
        /// </summary>
        [DisplayName("Руководитель (Имя)")]
        [Category("Контактные данные")]
        public bool IsVisibleManagementName { get; set; }

        /// <summary>
        /// Руководитель (Отчество).
        /// </summary>
        [DisplayName("Руководитель (Отчество)")]
        [Category("Контактные данные")]
        public bool IsVisibleManagementPatronymic { get; set; }

        /// <summary>
        /// Руководитель (Должность).
        /// </summary>
        [DisplayName("Руководитель (Должность)")]
        [Category("Контактные данные")]
        public bool IsVisibleManagementPosition { get; set; }

        /// <summary>
        /// Дата рождения руководителя".
        /// </summary>
        [DisplayName("Дата рождения руководителя")]
        [Category("Контактные данные")]
        public bool IsVisibleDateManagementBirth { get; set; }

        /// <summary>
        /// ОКПО.
        /// </summary>
        [DisplayName("ОКПО")]
        [Category("Реквизиты")]
        public bool IsVisibleOKPO { get; set; }

        /// <summary>
        /// ОКВЭД.
        /// </summary>
        [DisplayName("ОКВЭД")]
        [Category("Реквизиты")]
        public bool IsVisibleOKVED { get; set; }

        /// <summary>
        /// ОГРН.
        /// </summary>
        [DisplayName("ОГРН")]
        [Category("Реквизиты")]
        public bool IsVisiblePSRN { get; set; }

        /// <summary>
        /// Дата выдачи ОГРН.
        /// </summary>
        [DisplayName("Дата выдачи ОГРН")]
        [Category("Общая информация")]
        public bool IsVisibleDatePSRN { get; set; }

        /// <summary>
        /// Телефон.
        /// </summary>
        [DisplayName("Телефон")]
        [Category("Контактные данные")]
        public bool IsVisibleTelephone { get; set; }

        /// <summary>
        /// Email.
        /// </summary>
        [DisplayName("Email")]
        [Category("Контактные данные")]
        public bool IsVisibleEmail { get; set; }

        /// <summary>
        /// Юридический адрес.
        /// </summary>
        [DisplayName("Юридический адрес")]
        [Category("Контактные данные")]
        public bool IsVisibleLegalAddress { get; set; }

        /// <summary>
        /// Вид деятельности.
        /// </summary>
        [DisplayName("Вид деятельности")]
        [Category("Общая информация")]
        public bool IsVisibleKindActivity { get; set; }

        [DisplayName("Дата начала действия главного бухгалтера")]
        [Category("Внутренняя информация")]
        public bool IsVisibleAccountantResponsibleDate { get; set; }

        [DisplayName("Дата начала действия ответственного за первичные документы")]
        [Category("Внутренняя информация")]
        public bool IsVisiblePrimaryResponsibleDate { get; set; }

        [DisplayName("Дата начала действия ответственного за банк")]
        [Category("Внутренняя информация")]
        public bool IsVisibleBankResponsibleDate { get; set; }

        /// <summary>
        /// Ответственный главный бухгалтер.
        /// </summary>
        [DisplayName("Ответственный главный бухгалтер")]
        [Category("Внутренняя информация")]
        public bool IsVisibleAccountantResponsible { get; set; }

        /// <summary>
        /// Ответственный за банк.
        /// </summary>
        [DisplayName("Ответственный за банк")]
        [Category("Внутренняя информация")]
        public bool IsVisibleBankResponsible { get; set; }

        /// <summary>
        /// Является ли клиент ответственным за банк.
        /// </summary>
        [DisplayName("Является ли клиент ответственным за банк")]
        [Category("Внутренняя информация")]
        public bool IsVisibleCustomerIsBankResponsible { get; set; }

        /// <summary>
        /// Ответственный за первичные документы.
        /// </summary>
        [DisplayName("Ответственный за первичные документы")]
        [Category("Внутренняя информация")]
        public bool IsVisiblePrimaryResponsible { get; set; }

        /// <summary>
        /// Ответственный за ЗП.
        /// </summary>
        [DisplayName("Ответственный за ЗП")]
        [Category("Внутренняя информация")]
        public bool IsVisibleSalaryResponsible { get; set; }

        /// <summary>
        /// Детали обслуживания.
        /// </summary>
        [DisplayName("Детали обслуживания")]
        [Category("Общая информация")]
        public bool IsVisibleServiceDetails { get; set; }

        /// <summary>
        /// КПП.
        /// </summary>
        [DisplayName("КПП")]
        [Category("Реквизиты")]
        public bool IsVisibleKPP { get; set; }

        /// <summary>
        /// ОКТМО.
        /// </summary>
        [DisplayName("ОКТМО")]
        [Category("Реквизиты")]
        public bool IsVisibleOKTMO { get; set; }

        /// <summary>
        /// ОКАТО.
        /// </summary>
        [DisplayName("ОКАТО")]
        [Category("Реквизиты")]
        public bool IsVisibleOKATO { get; set; }

        /// <summary>
        /// Система налогообложения.
        /// </summary>
        [DisplayName("Система налогообложения")]
        [Category("Общая информация")]
        public bool IsVisibleTaxSystemCustomer { get; set; }

        /// <summary>
        /// Электронная отчетность.
        /// </summary>
        [DisplayName("Электронная отчетность")]
        [Category("Общая информация")]
        public bool IsVisibleElectronicReporting { get; set; }

        /// <summary>
        /// Электронная отчетность.
        /// </summary>
        [DisplayName("Электронная отчетность (даты действия)")]
        [Category("Общая информация")]
        public bool IsVisibleElectronicReportingDates { get; set; }

        /// <summary>
        /// Форма собственности.
        /// </summary>
        [DisplayName("Форма собственности")]
        [Category("Общая информация")]
        public bool IsVisibleFormCorporation { get; set; }

        /// <summary>
        /// Договор.
        /// </summary>
        [DisplayName("Договор")]
        [Category("Внутренняя информация")]
        public bool IsVisibleContract { get; set; }

        /// <summary>
        /// Состояние.
        /// </summary>
        [DisplayName("Состояние")]
        [Category("Внутренняя информация")]
        public bool IsVisibleStatus{ get; set; }

        /// <summary>
        /// Дата состояния.
        /// </summary>
        [DisplayName("Дата состояния")]
        [Category("Внутренняя информация")]
        public bool IsVisibleStateDate { get; set; }

        /// <summary>
        /// День выплаты заработной платы.
        /// </summary>
        [DisplayName("День выплаты заработной платы")]
        [Category("Бухгалтерская информация")]
        public bool IsVisibleSalaryDay { get; set; }

        /// <summary>
        /// День выплаты аванса.
        /// </summary>
        [DisplayName("День выплаты аванса")]
        [Category("Бухгалтерская информация")]
        public bool IsVisibleImprestDay { get; set; }

        /// <summary>
        /// Вид выплаты аванса и ЗП.
        /// </summary>
        [DisplayName("Вид выплаты аванса и ЗП")]
        [Category("Бухгалтерская информация")]
        public bool IsVisibleTypePayment { get; set; }

        /// <summary>
        /// Заметки.
        /// </summary>
        [DisplayName("Заметки")]
        [Category("Общая информация")]
        public bool IsVisibleNotes { get; set; }

        /// <summary>
        /// Налоги.
        /// </summary>
        [DisplayName("Налоги")]
        [Category("Бухгалтерская информация")]
        public bool IsVisibleTax { get; set; }

        /// <summary>
        /// Лизинг.
        /// </summary>
        [DisplayName("Лизинг")]
        [Category("Бухгалтерская информация")]
        public bool IsVisibleLeasing { get; set; }

        /// <summary>
        /// Список отчетов по статистике.
        /// </summary>
        [DisplayName("Список отчетов по статистике")]
        [Category("Бухгалтерская информация")]
        public bool IsVisibleStatisticalReports { get; set; }

        /// <summary>
        /// Банковские счета.
        /// </summary>
        [DisplayName("Банковские счета")]
        [Category("Бухгалтерская информация")]
        public bool IsVisibleAccounts { get; set; }

        /// <summary>
        /// Контакты.
        /// </summary>
        [DisplayName("Контакты")]
        [Category("Общая информация")]
        public bool IsVisibleContacts { get; set; }

        /// <summary>
        /// Нюансы НДФЛ.
        /// </summary>
        [DisplayName("Нюансы НДФЛ")]
        [Category("Бухгалтерская информация")]
        public bool IsVisiblePersonalIncomeTaxis { get; set; }

        /// <summary>
        /// Текущая папка.
        /// </summary>
        [DisplayName("Папка текущая")]
        [Category("Общая информация")]
        public bool IsVisiblePublic { get; set; }

        /// <summary>
        /// ЗП папка.
        /// </summary>
        [DisplayName("Папка ЗП")]
        [Category("Общая информация")]
        public bool IsVisibleSalary { get; set; }

        /// <summary>
        /// Текущая + ЗП.
        /// </summary>
        [DisplayName("Папка текущая + ЗП")]
        [Category("Общая информация")]
        public bool IsVisiblePublicAndSalary { get; set; }

        /// <summary>
        /// 1C Бухгалтерия.
        /// </summary>
        [DisplayName("1C Бухгалтерия")]
        [Category("Общая информация")]
        public bool IsVisibleOneEsBookkeepingString { get; set; }

        /// <summary>
        /// 1C Зарплата.
        /// </summary>
        [DisplayName("1C Зарплата")]
        [Category("Общая информация")]
        public bool IsVisibleOneEsSalaryString { get; set; }

        /// <summary>
        /// 1C Иное.
        /// </summary>
        [DisplayName("1C Иное")]
        [Category("Общая информация")]
        public bool IsVisibleOneEsOtherString { get; set; }

        /// <summary>
        /// 1C Иное.
        /// </summary>
        [DisplayName("Трудовые книжки")]
        [Category("Общая информация")]
        public bool IsVisibleCustomerEmploymentHistorysString { get; set; }

        /// <summary>
        /// XML файл содержащий сохранение визуализации таблицы.
        /// </summary>
        [MemberDesignTimeVisibility(false), Browsable(false)]
        public byte[] LayoutFromXml { get; set; }

        public override string ToString()
        {
            var result = string.Empty;

            if (IsVisibleStatus)
            {
                result += $"{nameof(Customer.StatusString)};";
            }

            if (IsVisibleStatusStatisticalReport)
            {
                result += $"{nameof(Customer.StatusStatisticalReport)};";
            }

            if (IsVisibleINN)
            {
                result += $"{nameof(Customer.INN)};";
            }

            if (IsVisibleName)
            {
                result += $"{nameof(Customer.Name)};";
            }

            if (IsVisibleProcessedName)
            {
                result += $"{nameof(Customer.ProcessedName)};";
            }

            if (IsVisibleDefaultName)
            {
                result += $"{nameof(Customer.DefaultName)};";
            }

            if (IsVisibleFullName)
            {
                result += $"{nameof(Customer.FullName)};";
            }

            if (IsVisibleAbbreviatedName)
            {
                result += $"{nameof(Customer.AbbreviatedName)};";
            }

            if (IsVisibleDateActuality)
            {
                result += $"{nameof(Customer.DateActuality)};";
            }

            if (IsVisibleDateRegistration)
            {
                result += $"{nameof(Customer.DateRegistration)};";
            }

            if (IsVisibleDateLiquidation)
            {
                result += $"{nameof(Customer.DateLiquidation)};";
            }

            if (IsVisibleOrganizationStatus)
            {
                result += $"{nameof(Customer.OrganizationStatusString)};";
            }

            if (IsVisibleManagementString)
            {
                result += $"{nameof(Customer.ManagementString)};";
            }

            if (IsVisibleManagementSurname)
            {
                result += $"{nameof(Customer.ManagementSurname)};";
            }

            if (IsVisibleManagementName)
            {
                result += $"{nameof(Customer.ManagementName)};";
            }

            if (IsVisibleManagementPatronymic)
            {
                result += $"{nameof(Customer.ManagementPatronymic)};";
            }

            if (IsVisibleManagementPosition)
            {
                result += $"{nameof(Customer.ManagementPositionString)};";
            }

            if (IsVisibleDateManagementBirth)
            {
                result += $"{nameof(Customer.DateManagementBirth)};";
            }

            if (IsVisibleOKPO)
            {
                result += $"{nameof(Customer.OKPO)};";
            }

            if (IsVisibleOKVED)
            {
                result += $"{nameof(Customer.OKVED)};";
            }

            if (IsVisiblePSRN)
            {
                result += $"{nameof(Customer.PSRN)};";
            }

            if (IsVisibleDatePSRN)
            {
                result += $"{nameof(Customer.DatePSRN)};";
            }

            if (IsVisibleTelephone)
            {
                result += $"{nameof(Customer.Telephone)};";
            }

            if (IsVisibleEmail)
            {
                result += $"{nameof(Customer.Email)};";
            }

            if (IsVisibleLegalAddress)
            {
                result += $"{nameof(Customer.LegalAddress)};";
            }

            if (IsVisibleKindActivity)
            {
                result += $"{nameof(Customer.KindActivityString)};";
            }

            if (IsVisibleAccountantResponsibleDate)
            {
                result += $"{nameof(Customer.AccountantResponsibleDate)};";
            }

            if (IsVisiblePrimaryResponsibleDate)
            {
                result += $"{nameof(Customer.PrimaryResponsibleDate)};";
            }

            if (IsVisibleBankResponsibleDate)
            {
                result += $"{nameof(Customer.BankResponsibleDate)};";
            }

            if (IsVisibleAccountantResponsible)
            {
                result += $"{nameof(Customer.AccountantResponsibleString)};";
            }

            if (IsVisibleBankResponsible)
            {
                result += $"{nameof(Customer.BankResponsibleString)};";
            }

            if (IsVisibleCustomerIsBankResponsible)
            {
                result += $"{nameof(Customer.CustomerIsBankResponsible)};";
            }

            if (IsVisiblePrimaryResponsible)
            {
                result += $"{nameof(Customer.PrimaryResponsibleString)};";
            }
            
            if (IsVisibleSalaryResponsible)
            {
                result += $"{nameof(Customer.SalaryResponsibleString)};";
            }

            if (IsVisibleServiceDetails)
            {
                result += $"{nameof(Customer.ServiceDetails)};";
            }

            if (IsVisibleKPP)
            {
                result += $"{nameof(Customer.KPP)};";
            }

            if (IsVisibleOKTMO)
            {
                result += $"{nameof(Customer.OKTMO)};";
            }

            if (IsVisibleOKATO)
            {
                result += $"{nameof(Customer.OKATO)};";
            }

            if (IsVisibleTaxSystemCustomer)
            {
                result += $"{nameof(Customer.TaxSystemCustomerString)};";
            }

            if (IsVisibleElectronicReporting)
            {
                result += $"{nameof(Customer.ElectronicReportingString)};";
            }

            if (IsVisibleElectronicReportingDates)
            {
                result += $"{nameof(Customer.ElectronicReportingStringDateSince)};";
                result += $"{nameof(Customer.ElectronicReportingStringDateTo)};";
            }

            if (IsVisibleFormCorporation)
            {
                result += $"{nameof(Customer.FormCorporationString)};";
            }

            if (IsVisibleContract)
            {
                result += $"{nameof(Customer.ContractsString)};";
            }

            if (IsVisibleSalaryDay)
            {
                result += $"{nameof(Customer.SalaryDay)};";
            }

            if (IsVisibleImprestDay)
            {
                result += $"{nameof(Customer.ImprestDay)};";
            }

            if (IsVisibleTypePayment)
            {
                result += $"{nameof(Customer.TypePaymentString)};";
            }

            if (IsVisibleNotes)
            {
                result += $"{nameof(Customer.Notes)};";
            }

            if (IsVisibleTax)
            {
                result += $"{nameof(Customer.TaxString)};";
            }

            if (IsVisibleLeasing)
            {
                result += $"{nameof(Customer.LeasingString)};";
            }            

            if (IsVisibleStatisticalReports)
            {
                result += $"{nameof(Customer.StatisticalReportsString)};";
            }

            if (IsVisibleAccounts)
            {
                result += $"{nameof(Customer.AccountsString)};";
            }
            
            if (IsVisibleContacts)
            {
                result += $"{nameof(Customer.ContactsString)};";
            }
                        
            if (IsVisiblePersonalIncomeTaxis)
            {
                result += $"{nameof(Customer.PersonalIncomeTaxisString)};";
            }

            if (IsVisiblePublic)
            {
                result += $"{nameof(Customer.IsPublic)};";
            }
            
            if (IsVisibleSalary)
            {
                result += $"{nameof(Customer.IsSalary)};";
            }
            
            if (IsVisiblePublicAndSalary)
            {
                result += $"{nameof(Customer.IsPublicAndSalary)};";
            }

            if (IsVisibleOneEsBookkeepingString)
            {
                result += $"{nameof(Customer.OneEsBookkeepingString)};";
            }

            if (IsVisibleOneEsSalaryString)
            {
                result += $"{nameof(Customer.OneEsSalaryString)};";
            }

            if (IsVisibleOneEsOtherString)
            {
                result += $"{nameof(Customer.OneEsOtherString)};";
            }

            if (IsVisibleCustomerEmploymentHistorysString)
            {
                result += $"{nameof(Customer.CustomerEmploymentHistorysString)};";
            }

            if (string.IsNullOrWhiteSpace(result))
            {
                return default;
            }
            else
            {
                //result = result.Insert(0, "This;");
                if (result.LastIndexOf(";") == result.Length - 1)
                {
                    result = result.Remove(result.Length - 1);
                }
            }

            return result;
        }

        public void Edit(CustomerSettings customerSettings)
        {
            Name = customerSettings.Name;
            Description = customerSettings.Description;
            IsVisibleStatusStatisticalReport = customerSettings.IsVisibleStatusStatisticalReport;
            IsVisibleINN = customerSettings.IsVisibleINN;
            IsVisibleName = customerSettings.IsVisibleName;
            IsVisibleDefaultName = customerSettings.IsVisibleDefaultName;
            IsVisibleProcessedName = customerSettings.IsVisibleProcessedName;
            IsVisibleFullName = customerSettings.IsVisibleFullName;
            IsVisibleAbbreviatedName = customerSettings.IsVisibleAbbreviatedName;
            IsVisibleDateActuality = customerSettings.IsVisibleDateActuality;
            IsVisibleDateRegistration = customerSettings.IsVisibleDateRegistration;
            IsVisibleDateLiquidation = customerSettings.IsVisibleDateLiquidation;
            IsVisibleOrganizationStatus = customerSettings.IsVisibleOrganizationStatus;
            IsVisibleManagementString = customerSettings.IsVisibleManagementString;
            IsVisibleManagementSurname = customerSettings.IsVisibleManagementSurname;
            IsVisibleManagementName = customerSettings.IsVisibleManagementName;
            IsVisibleManagementPatronymic = customerSettings.IsVisibleManagementPatronymic;
            IsVisibleManagementPosition = customerSettings.IsVisibleManagementPosition;
            IsVisibleOKPO = customerSettings.IsVisibleOKPO;
            IsVisibleOKVED = customerSettings.IsVisibleOKVED;
            IsVisiblePSRN = customerSettings.IsVisiblePSRN;
            IsVisibleDatePSRN = customerSettings.IsVisibleDatePSRN;
            IsVisibleTelephone = customerSettings.IsVisibleTelephone;
            IsVisibleEmail = customerSettings.IsVisibleEmail;
            IsVisibleLegalAddress = customerSettings.IsVisibleLegalAddress;
            IsVisibleKindActivity = customerSettings.IsVisibleKindActivity;
            IsVisibleAccountantResponsibleDate = customerSettings.IsVisibleAccountantResponsibleDate;
            IsVisiblePrimaryResponsibleDate = customerSettings.IsVisiblePrimaryResponsibleDate;
            IsVisibleBankResponsibleDate = customerSettings.IsVisibleBankResponsibleDate;
            IsVisibleAccountantResponsible = customerSettings.IsVisibleAccountantResponsible;
            IsVisibleBankResponsible = customerSettings.IsVisibleBankResponsible;
            IsVisibleCustomerIsBankResponsible = customerSettings.IsVisibleCustomerIsBankResponsible;
            IsVisiblePrimaryResponsible = customerSettings.IsVisiblePrimaryResponsible;
            IsVisibleSalaryResponsible = customerSettings.IsVisibleSalaryResponsible;
            IsVisibleServiceDetails = customerSettings.IsVisibleServiceDetails;
            IsVisibleKPP = customerSettings.IsVisibleKPP;
            IsVisibleOKTMO = customerSettings.IsVisibleOKTMO;
            IsVisibleOKATO = customerSettings.IsVisibleOKATO;
            IsVisibleTaxSystemCustomer = customerSettings.IsVisibleTaxSystemCustomer;
            IsVisibleElectronicReporting = customerSettings.IsVisibleElectronicReporting;
            IsVisibleElectronicReportingDates = customerSettings.IsVisibleElectronicReportingDates;
            IsVisibleFormCorporation = customerSettings.IsVisibleFormCorporation;
            IsVisibleContract = customerSettings.IsVisibleContract;
            IsVisibleStatus = customerSettings.IsVisibleStatus;
            IsVisibleStateDate = customerSettings.IsVisibleStateDate;
            IsVisibleSalaryDay = customerSettings.IsVisibleSalaryDay;
            IsVisibleImprestDay = customerSettings.IsVisibleImprestDay;
            IsVisibleTypePayment = customerSettings.IsVisibleTypePayment;
            IsVisibleNotes = customerSettings.IsVisibleNotes;
            IsVisibleTax = customerSettings.IsVisibleTax;
            IsVisibleLeasing = customerSettings.IsVisibleLeasing;
            IsVisibleStatisticalReports = customerSettings.IsVisibleStatisticalReports;
            IsVisibleAccounts = customerSettings.IsVisibleAccounts;
            IsVisibleContacts = customerSettings.IsVisibleContacts;
            IsVisiblePersonalIncomeTaxis = customerSettings.IsVisiblePersonalIncomeTaxis;
            IsVisiblePublic = customerSettings.IsVisiblePublic;
            IsVisibleSalary = customerSettings.IsVisibleSalary;
            IsVisiblePublicAndSalary = customerSettings.IsVisiblePublicAndSalary;
            IsVisibleOneEsBookkeepingString = customerSettings.IsVisibleOneEsBookkeepingString;
            IsVisibleOneEsSalaryString = customerSettings.IsVisibleOneEsSalaryString;
            IsVisibleOneEsOtherString = customerSettings.IsVisibleOneEsOtherString;
            IsVisibleCustomerEmploymentHistorysString = customerSettings.IsVisibleCustomerEmploymentHistorysString;
            IsVisibleDateManagementBirth = customerSettings.IsVisibleDateManagementBirth;
        }
    }
}