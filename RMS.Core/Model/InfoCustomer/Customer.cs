using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Interface;
using RMS.Core.Model.Chronicle;
using RMS.Core.Model.ElectronicDocumentsManagement;
using RMS.Core.Model.InfoContract;
using RMS.Core.Model.InfoCustomer.Billing;
using RMS.Core.Model.Notifications;
using RMS.Core.Model.OnesEs;
using RMS.Core.Model.Reports;
using RMS.Core.Model.Salary;
using RMS.Core.Model.Taxes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RMS.Core.Model.InfoCustomer
{
    /// <summary>
    /// Клиент.
    /// </summary>
    public class Customer : XPObject, INotification
    {
        public Customer() { }
        public Customer(Session session) : base(session) { }

        /// <summary>
        /// Дата создания.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public DateTime? DateCreate { get; } = DateTime.Now;

        [DisplayName("Статус")]
        public string StatusString
        {
            get
            {
                return CustomerStatus?.Status?.ToString();
            }
        }

        [DisplayName("Статистика")]
        public StatusStatisticalReport StatusStatisticalReport
        {
            get
            {
                var yearStartDate = new DateTime(DateTime.Now.Year, 1, 1);
                var yearEndDate = new DateTime(DateTime.Now.Year, 12, 31);

                var actUpdateReport = ChronicleCustomers?.FirstOrDefault(w =>
                           (w.Act == Act.UPDATING_INFORMATION_REPORTING_SYSTEM_AUTO
                           || w.Act == Act.UPDATING_INFORMATION_REPORTING_SYSTEM_HAND)
                               && w.Date >= yearStartDate && w.Date <= yearEndDate);

                if (actUpdateReport is null)
                {
                    return StatusStatisticalReport.INFORMATIONNOTUPDATED;
                }
                else
                {
                    if (StatisticalReports != null && StatisticalReports.Where(w => w.Year == DateTime.Now.Year).Count() > 0)
                    {
                        return StatusStatisticalReport.STATISTICALREPORTSAVAILABLE;
                    }
                    else
                    {
                        return StatusStatisticalReport.STATISTICALREPORTSNOTAVAILABLE;
                    }
                }
            }
        }

        [Size(32)]
        [DisplayName("ИНН")]
        public string INN { get; set; }

        [Size(256)]
        [DisplayName("Наименование")]
        public string Name { get; set; }
        
        [DisplayName("Наименование")]
        public string ProcessedName
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Name))
                {
                    if (Name.Contains("ООО"))
                    {
                        return $"{Name?.Replace("ООО", "").Replace("\"", "").Trim()} ООО";
                    }

                    if (Name.Contains("ОО"))
                    {
                        return $"{Name?.Replace("ОО", "").Replace("\"", "").Trim()} ОО";
                    }

                    if (Name.Contains("ИП"))
                    {
                        return $"{Name?.Replace("ИП", "").Replace("\"", "").Trim()} ИП";
                    }

                    if (Name.Contains("НП"))
                    {
                        return $"{Name?.Replace("НП", "").Replace("\"", "").Trim()} НП";
                    }

                    if (Name.Contains("БФ"))
                    {
                        return $"{Name?.Replace("БФ", "").Replace("\"", "").Trim()} БФ";
                    }

                    if (Name.Contains("АНО"))
                    {
                        return $"{Name?.Replace("АНО", "").Replace("\"", "").Trim()} АНО";
                    }

                    return Name;
                }

                return default;
            }
        }

        [DisplayName("Наименование")]
        public string DefaultName
        {
            get
            {
                var result = Name?
                    .Replace("ООО", "")
                    .Replace("ОО", "")
                    .Replace("НП", "")
                    .Replace("ИП", "")
                    .Replace("БФ", "")
                    .Replace("АНО", "")
                    .Replace("\"", "")
                    .Trim();
                return result;
            }
        }

        [DisplayName("Полное наименование"), MemberDesignTimeVisibility(false), Size(1024)]
        public string FullName { get; set; }

        [Size(256)]
        [MemberDesignTimeVisibility(false)]
        [DisplayName("Сокращенное наименование")]
        public string AbbreviatedName { get; set; }

        /// <summary>
        /// Дата актуальности состояния организации.
        /// </summary>
        [MemberDesignTimeVisibility(false), DisplayName("Дата актуальности состояния")]
        public DateTime? DateActuality { get; set; }

        /// <summary>
        /// Дата регистрации.
        /// </summary>
        [DisplayName("Дата регистрации")]
        public DateTime? DateRegistration { get; set; }

        /// <summary>
        /// Дата ликвидации.
        /// </summary>
        [DisplayName("Дата ликвидации")]
        public DateTime? DateLiquidation { get; set; }

        [DisplayName("Статус организации"), Size(128)]
        public string OrganizationStatusString => OrganizationStatus.GetEnumDescription();
        /// <summary>
        /// Статус организации.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public StatusOrganization OrganizationStatus { get; set; }

        [DisplayName("Руководитель")]
        public string ManagementString
        {
            get
            {
                var result = default(string);

                if (!string.IsNullOrWhiteSpace(ManagementSurname))
                {
                    result += ManagementSurname;

                    if (!string.IsNullOrWhiteSpace(ManagementName))
                    {
                        result += $" {ManagementName.Substring(0, 1).ToUpper()}.";
                    }

                    if (!string.IsNullOrWhiteSpace(ManagementPatronymic))
                    {
                        result += $" {ManagementPatronymic.Substring(0, 1).ToUpper()}.";
                    }
                }

                return result;
            }
        }
        
        /// <summary>
        /// Имя и отчество руководителя.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public string ManagementNameAndPatronymicString
        {
            get
            {
                var result = default(string);

                if (!string.IsNullOrWhiteSpace(ManagementName))
                {
                    result += ManagementName;

                    if (!string.IsNullOrWhiteSpace(ManagementPatronymic))
                    {
                        result += $" {ManagementPatronymic}";
                    }
                }

                return result;
            }
        }


        [DisplayName("Руководитель")]
        public string ManagementFullString
        {
            get
            {
                var result = default(string);

                if (!string.IsNullOrWhiteSpace(ManagementSurname))
                {
                    result += ManagementSurname;

                    if (!string.IsNullOrWhiteSpace(ManagementName))
                    {
                        result += $" {ManagementName}";
                    }

                    if (!string.IsNullOrWhiteSpace(ManagementPatronymic))
                    {
                        result += $" {ManagementPatronymic}";
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// Руководитель (Фамилия).
        /// </summary>
        [MemberDesignTimeVisibility(false), Size(128), DisplayName("Руководитель (Фамилия)")]
        public string ManagementSurname { get; set; }

        /// <summary>
        /// Руководитель (Имя).
        /// </summary>
        [MemberDesignTimeVisibility(false), Size(128), DisplayName("Руководитель (Имя)")]
        public string ManagementName { get; set; }

        /// <summary>
        /// Руководитель (Отчество).
        /// </summary>
        [MemberDesignTimeVisibility(false), Size(128), DisplayName("Руководитель (Отчество)")]
        public string ManagementPatronymic { get; set; }

        [MemberDesignTimeVisibility(false)]
        [DisplayName("Руководитель (Должность)")]
        public string ManagementPositionString => ManagementPosition?.Name;
        /// <summary>
        /// Руководитель (Должность).
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Position ManagementPosition { get; set; }

        /// <summary>
        /// Дата рождения руководителя.
        /// </summary>
        [DisplayName("Дата рождения руководителя")]
        [MemberDesignTimeVisibility(false)]
        public DateTime? DateManagementBirth { get; set; }

        /// <summary>
        /// ОКПО.
        /// </summary>
        [DisplayName("ОКПО"), MemberDesignTimeVisibility(false), Size(32)]
        public string OKPO { get; set; }

        /// <summary>
        /// ОКВЭД.
        /// </summary>
        [DisplayName("ОКВЭД"), MemberDesignTimeVisibility(false), Size(32)]
        public string OKVED { get; set; }

        /// <summary>
        /// ОГРН.
        /// </summary>
        [DisplayName("ОГРН"), MemberDesignTimeVisibility(false), Size(32)]
        public string PSRN { get; set; }

        /// <summary>
        /// Дата выдачи ОГРН.
        /// </summary>
        [DisplayName("Дата выдачи ОГРН"), MemberDesignTimeVisibility(false)]
        public DateTime? DatePSRN { get; set; }
                
        /// <summary>
        /// Телефон.
        /// </summary>
        [DisplayName("Телефон")]
        public string Telephone => CustomerTelephones.FirstOrDefault(f => f.IsDefault)?.Telephone ?? CustomerTelephones.FirstOrDefault()?.Telephone;

        /// <summary>
        /// Email.
        /// </summary>
        [DisplayName("Email")]
        public string Email => CustomerEmails?.FirstOrDefault(f => f.IsDefault)?.Email ?? CustomerEmails?.FirstOrDefault()?.Email;

        /// <summary>
        /// Юридический адрес.
        /// </summary>
        [DisplayName("Юридический адрес"), Size(1024)]
        public string LegalAddress => CustomerAddress?.FirstOrDefault(f => f.IsLegal)?.ToString();

        [MemberDesignTimeVisibility(false), DisplayName("Вид деятельности")]
        public string KindActivityString => KindActivity?.ClassOKVED2?.ToString();
        /// <summary>
        /// Вид деятельности.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public KindActivity KindActivity { get; set; }

        /// <summary>
        /// Ответственные сотрудники.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public CustomerResponsible CustomerResponsible { get; set; }

        [MemberDesignTimeVisibility(false), DisplayName("Дата начала ответственности бухгалтера")]
        public DateTime? AccountantResponsibleDate { get; set; }

        [MemberDesignTimeVisibility(false), DisplayName("Дата начала ответственности первички")]
        public DateTime? PrimaryResponsibleDate { get; set; }

        [MemberDesignTimeVisibility(false), DisplayName("Дата начала ответственности за банк")]
        public DateTime? BankResponsibleDate { get; set; }
        
        [MemberDesignTimeVisibility(false), DisplayName("Дата начала ответственности за ЗП")]
        public DateTime? SalaryResponsibleDate { get; set; }

        [MemberDesignTimeVisibility(false), DisplayName("Ответственный главный бухгалтер")]
        public string AccountantResponsibleString => AccountantResponsible?.ToString();
        /// <summary>
        /// Ответственный главный бухгалтер.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Staff AccountantResponsible { get; set; }

        [MemberDesignTimeVisibility(false), DisplayName("Ответственный за банк")]
        public string BankResponsibleString 
        { 
            get
            {
                if (CustomerIsBankResponsible)
                {
                    return "Клиент";
                }
                
                return BankResponsible?.ToString();
            }
        }
        
        /// <summary>
        /// Ответственный за банк.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Staff BankResponsible { get; set; }

        /// <summary>
        /// Является ли клиент ответственным за банк.
        /// </summary>
        [MemberDesignTimeVisibility(false), DisplayName("Является ли клиент ответственным за банк")]
        public bool CustomerIsBankResponsible { get; set; } = true;

        /// <summary>
        /// Является ли клиент ответственным за первичные документы.
        /// </summary>
        [MemberDesignTimeVisibility(false), DisplayName("Является ли клиент ответственным за первичку")]
        public bool CustomerIsPrimaryResponsible { get; set; } = true;

        [MemberDesignTimeVisibility(false), DisplayName("Ответственный за первичные документы")]
        public string PrimaryResponsibleString
        {
            get
            {
                if (CustomerIsPrimaryResponsible)
                {
                    return "Клиент";
                }

                return PrimaryResponsible?.ToString();
            }
        }
        
        /// <summary>
        /// Ответственный за первичные документы.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Staff PrimaryResponsible { get; set; }

        /// <summary>
        /// Является ли клиент ответственным за ЗП.
        /// </summary>
        [MemberDesignTimeVisibility(false), DisplayName("Является ли клиент ответственным за ЗП")]
        public bool CustomerIsSalaryResponsible { get; set; } = true;

        [MemberDesignTimeVisibility(false), DisplayName("Ответственный за ЗП")]
        public string SalaryResponsibleString
        {
            get
            {
                if (CustomerIsSalaryResponsible)
                {
                    return "Клиент";
                }

                return SalaryResponsible?.ToString();
            }
        }

        /// <summary>
        /// Ответственный за ЗП.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Staff SalaryResponsible { get; set; }

        /// <summary>
        /// Детали обслуживания.
        /// </summary>
        [DisplayName("Детали обслуживания"), MemberDesignTimeVisibility(false), Size(1024)]
        public string ServiceDetails { get; set; }

        [DisplayName("КПП"), MemberDesignTimeVisibility(false), Size(32)]
        public string KPP { get; set; }

        /// <summary>
        /// ОКТМО.
        /// </summary>
        [DisplayName("ОКТМО"), MemberDesignTimeVisibility(false), Size(32)]
        public string OKTMO { get; set; }

        /// <summary>
        /// ОКАТО.
        /// </summary>
        [DisplayName("ОКАТО"), MemberDesignTimeVisibility(false), Size(32)]
        public string OKATO { get; set; }

        [MemberDesignTimeVisibility(false)]
        [DisplayName("Система налогообложения")]
        public string TaxSystemCustomerString => TaxSystemCustomer?.TaxSystem?.Name;
        /// <summary>
        /// Система налогообложения.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public TaxSystemCustomer TaxSystemCustomer { get; set; }

        [DisplayName("Электронная отчетность (c)")]
        [MemberDesignTimeVisibility(false)]
        public string ElectronicReportingStringDateSince => ElectronicReportingCustomer?.DateSince?.ToShortDateString();        
        [DisplayName("Электронная отчетность (по)")]
        [MemberDesignTimeVisibility(false)]
        public string ElectronicReportingStringDateTo => ElectronicReportingCustomer?.DateTo?.ToShortDateString();        
        [DisplayName("Электронная отчетность")]
        [MemberDesignTimeVisibility(false)]
        public string ElectronicReportingString => ElectronicReportingCustomer?.CurrentElectronicReporting?.Name;
        /// <summary>
        /// Электронная отчетность.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public ElectronicReporting ElectronicReporting => ElectronicReportingCustomer?.CurrentElectronicReporting;
        /// <summary>
        /// Электронная отчетность.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public ElectronicReportingCustomer ElectronicReportingCustomer { get; set; }

        [DisplayName("ЭДО")]
        [MemberDesignTimeVisibility(false)]
        public string ElectronicDocumentManagementString => ElectronicDocumentManagementCustomer?.CurrentObject?.Name;
        /// <summary>
        /// <summary>
        /// ЭДО.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public ElectronicDocumentManagement ElectronicDocumentManagement => ElectronicDocumentManagementCustomer?.CurrentObject;
        /// <summary>
        /// ЭДО.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public ElectronicDocumentManagementCustomer ElectronicDocumentManagementCustomer { get; set; }

        [DisplayName("Форма собственности")]
        public string FormCorporationString => FormCorporation?.AbbreviatedName;
        /// <summary>
        /// Форма собственности.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public FormCorporation FormCorporation { get; set; }

        [DisplayName("Договора")]
        [MemberDesignTimeVisibility(false)]
        public string ContractsString
        {
            get
            {
                var result = default(string);

                foreach (var statisticalReports in Contracts)
                {
                    result += $"{statisticalReports}; ";
                }

                return result;
            }
        }
        /// <summary>
        /// Договора.
        /// </summary>
        [Aggregated]
        [Association]
        [MemberDesignTimeVisibility(false)]
        public IList<Contract> Contracts
        {
            get
            {
                return GetList<Contract>(nameof(Contracts));
            }
        }

        /// <summary>
        /// Статус.
        /// </summary>
        [MemberDesignTimeVisibility(false), Aggregated]
        public CustomerStatus CustomerStatus { get; set; }

        /// <summary>
        /// День выплаты заработной платы.
        /// </summary>
        [DisplayName("День выплаты заработной платы")]
        [MemberDesignTimeVisibility(false)]
        public int? SalaryDay => SalaryDayObject?.CurrentObject;

        /// <summary>
        /// День выплаты заработной платы.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public SalaryAndAdvance SalaryDayObject { get; set; }

        /// <summary>
        /// День выплаты аванса.
        /// </summary>
        [DisplayName("День выплаты аванса")]
        [MemberDesignTimeVisibility(false)]
        public int? ImprestDay => AdvanceDayObject?.CurrentObject;

        /// <summary>
        /// День выплаты аванса.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public SalaryAndAdvance AdvanceDayObject { get; set; }

        [MemberDesignTimeVisibility(false), DisplayName(" Вид выплаты аванса и ЗП")]
        public string TypePaymentString => TypePayment?.Name;
        /// <summary>
        /// Вид выплаты аванса и ЗП.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public TypePayment TypePayment { get; set; }

        /// <summary>
        /// Электронная папка.
        /// </summary>
        [Size(2048)]
        [DisplayName("Электронная папка")]
        [MemberDesignTimeVisibility(false)]
        public string ElectronicFolder { get; set; }

        /// <summary>
        /// Заметки.
        /// </summary>
        [DisplayName("Заметки"), MemberDesignTimeVisibility(false), Size(1024)]
        public string Notes { get; set; }
        
        /// <summary>
        /// Заметки.
        /// </summary>
        [DisplayName("Заметки"), MemberDesignTimeVisibility(false)]
        public byte[] NoteByte { get; set; }

        [DisplayName("Налоги")]
        [MemberDesignTimeVisibility(false)]
        public string TaxString => Tax?.ToString();
        /// <summary>
        /// Налоги.
        /// </summary>
        [Aggregated]
        [MemberDesignTimeVisibility(false)]
        public Tax Tax { get; set; }

        [MemberDesignTimeVisibility(false)]
        public string TaxType { get; set; }

        [MemberDesignTimeVisibility(false)]
        public string TaxTypePercent { get; set; }

        [MemberDesignTimeVisibility(false), DisplayName("1C Бухгалтерия")]
        public string OneEsBookkeepingString => OneEs?.OneEsBookkeeping?.ToString();
        [MemberDesignTimeVisibility(false), DisplayName("1C Зарплата")]
        public string OneEsSalaryString => OneEs?.OneEsSalary?.ToString();
        [MemberDesignTimeVisibility(false), DisplayName("1C Иное")]
        public string OneEsOtherString => OneEs?.OneEsOther?.ToString();
        /// <summary>
        /// 1C.
        /// </summary>
        [Aggregated]
        [MemberDesignTimeVisibility(false)]
        public OneEs OneEs { get; set; }

        [DisplayName("Лизинг")]
        [MemberDesignTimeVisibility(false)]
        public string LeasingString => Leasing?.ToString();
        /// <summary>
        /// Лизинг.
        /// </summary>
        [Aggregated]
        [MemberDesignTimeVisibility(false)]
        public Leasing Leasing { get; set; }

        [MemberDesignTimeVisibility(false)]
        [DisplayName("Отчеты по статистике")]
        public string StatisticalReportsString
        {
            get
            {
                var result = default(string);

                foreach (var statisticalReports in StatisticalReports?.Where(w => w.Year == DateTime.Now.Year))
                {
                    result += $"{statisticalReports}; ";
                }

                return result;
            }
        }
        /// <summary>
        /// Список отчетов по статистике.
        /// </summary>
        [Aggregated]
        [Association]
        [MemberDesignTimeVisibility(false)]
        public IList<StatisticalReport> StatisticalReports
        {
            get
            {
                return GetList<StatisticalReport>(nameof(StatisticalReports));
            }
        }


        [MemberDesignTimeVisibility(false), DisplayName("Отчеты")]
        public string CustomerReportsString
        {
            get
            {
                var result = default(string);

                foreach (var customerReports in CustomerReports)
                {
                    result += $"{customerReports}; ";
                }

                return result;
            }
        }
        ///// <summary>
        ///// Отчеты.
        ///// </summary>
        //[Aggregated]
        //[Association]
        //[MemberDesignTimeVisibility(false)]
        //public IList<CustomerReport> CustomerReports
        //{
        //    get
        //    {
        //        return GetList<CustomerReport>(nameof(CustomerReports));
        //    }
        //}
        /// <summary>
        /// Отчеты.
        /// </summary>
        [Aggregated]
        [Association]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<CustomerReport> CustomerReports
        {
            get
            {
                return GetCollection<CustomerReport>(nameof(CustomerReports));
            }
        }

        [DisplayName("Банковские счета")]
        [MemberDesignTimeVisibility(false)]
        public string AccountsString
        {
            get
            {
                var result = default(string);

                foreach (var account in Accounts)
                {
                    var @out = default(string);
                    if (!string.IsNullOrWhiteSpace(account.Description))
                    {
                        @out += $"{account.Description}";
                    }

                    if (!string.IsNullOrWhiteSpace(account.AccountNumber))
                    {
                        if (!string.IsNullOrWhiteSpace(@out))
                        {
                            @out += ": ";
                        }

                        @out += $"{account.AccountNumber}";
                    }

                    @out += Environment.NewLine;

                    result += @out?.Trim();
                }

                return result?.Trim();
            }
        }
        /// <summary>
        /// Банковские счета.
        /// </summary>
        [Association, Aggregated, MemberDesignTimeVisibility(false)]
        public XPCollection<Account> Accounts
        {
            get
            {
                return GetCollection<Account>(nameof(Accounts));
            }
        }

        [MemberDesignTimeVisibility(false), DisplayName("Контакты")]
        public string ContactsString
        {
            get
            {
                var result = default(string);

                foreach (var contact in CustomerTelephones)
                {
                    result += $"{contact}{Environment.NewLine}";
                }

                return result;
            }
        }

        /// <summary>
        /// Контакты.
        /// </summary>
        [Association, Aggregated, MemberDesignTimeVisibility(false)]
        public XPCollection<Contact> Contact
        {
            get
            {
                return GetCollection<Contact>(nameof(Contact));
            }
        }

        /// <summary>
        /// Контакты.
        /// </summary>
        [Aggregated]
        [Association]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<CustomerEmail> CustomerEmails
        {
            get
            {
                return GetCollection<CustomerEmail>(nameof(CustomerEmails));
            }
        }

        /// <summary>
        /// Пользователи телеграмм.
        /// </summary>
        [Aggregated]
        [Association]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<CustomerTelegramUser> CustomerTelegramUsers
        {
            get
            {
                return GetCollection<CustomerTelegramUser>(nameof(CustomerTelegramUsers));
            }
        }

        /// <summary>
        /// Контакты.
        /// </summary>
        [Aggregated]
        [Association]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<CustomerTelephone> CustomerTelephones
        {
            get
            {
                return GetCollection<CustomerTelephone>(nameof(CustomerTelephones));
            }
        }

        /// <summary>
        /// Адреса.
        /// </summary>
        [Association, Aggregated, MemberDesignTimeVisibility(false)]
        public XPCollection<CustomerAddress> CustomerAddress
        {
            get
            {
                return GetCollection<CustomerAddress>(nameof(CustomerAddress));
            }
        }

        /// <summary>
        /// Задачи.
        /// </summary>
        [Association, Aggregated, MemberDesignTimeVisibility(false)]
        public XPCollection<Task> Tasks
        {
            get
            {
                return GetCollection<Task>(nameof(Tasks));
            }
        }

        /// <summary>
        /// Внешнеэкономическая деятельность.
        /// </summary>
        [Association, Aggregated, MemberDesignTimeVisibility(false)]
        public XPCollection<ForeignEconomicActivity> ForeignEconomicActivities
        {
            get
            {
                return GetCollection<ForeignEconomicActivity>(nameof(ForeignEconomicActivities));
            }
        }

        [MemberDesignTimeVisibility(false), DisplayName("Нюансы НДФЛ")]
        public string PersonalIncomeTaxisString
        {
            get
            {
                var result = default(string);

                foreach (var personalIncomeTaxi in PersonalIncomeTaxis)
                {
                    result += $"{personalIncomeTaxi}{Environment.NewLine}";
                }

                return result;
            }
        }
        /// <summary>
        /// Нюансы НДФЛ.
        /// </summary>
        [Association, Aggregated, MemberDesignTimeVisibility(false)]
        public XPCollection<OrganizationPerformance> PersonalIncomeTaxis
        {
            get
            {
                var collection = GetCollection<OrganizationPerformance>(nameof(PersonalIncomeTaxis));
                collection.Sorting = new SortingCollection(
                    new SortProperty(nameof(OrganizationPerformance.Year), SortingDirection.Descending),
                    new SortProperty(nameof(OrganizationPerformance.Month), SortingDirection.Ascending)
                    );

                return collection;
            }
        }

        /// <summary>
        /// Хроника изменений клиента.
        /// </summary>
        [Aggregated]
        [Association]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<ChronicleCustomer> ChronicleCustomers
        {
            get
            {
                return GetCollection<ChronicleCustomer>(nameof(ChronicleCustomers));
            }
        }

        /// <summary>
        /// Доступ к банкам.
        /// </summary>
        [Association, Aggregated, MemberDesignTimeVisibility(false)]
        public XPCollection<BankAccess> BankAccess
        {
            get
            {
                return GetCollection<BankAccess>(nameof(BankAccess));
            }
        }

        /// <summary>
        /// Счета.
        /// </summary>
        [Association, Aggregated, MemberDesignTimeVisibility(false)]
        public XPCollection<Invoice> Invoices
        {
            get
            {
                return GetCollection<Invoice>(nameof(Invoices));
            }
        }

        /// <summary>
        /// Архивные папки.
        /// </summary>
        [Aggregated]
        [Association]
        [MemberDesignTimeVisibility(false)]
        public IList<CustomerArchiveFolder> CustomerArchiveFolders
        {
            get
            {
                return GetList<CustomerArchiveFolder>(nameof(CustomerArchiveFolders));
            }
        }

        /// <summary>
        /// Подразделения.
        /// </summary>
        [Aggregated]
        [Association]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<Subdivision> Subdivisions
        {
            get
            {
                return GetCollection<Subdivision>(nameof(Subdivisions));
            }
        }

        [MemberDesignTimeVisibility(false), DisplayName("Трудовые книжки")]
        public string CustomerEmploymentHistorysString
        {
            get
            {
                var result = default(string);

                foreach (var customerEmploymentHistory in CustomerEmploymentHistorys)
                {
                    result += $"{customerEmploymentHistory}{Environment.NewLine}";
                }

                return result;
            }
        }

        /// <summary>
        /// Трудовые книжки.
        /// </summary>
        [Association]
        [Aggregated]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<CustomerEmploymentHistory> CustomerEmploymentHistorys
        {
            get
            {
                return GetCollection<CustomerEmploymentHistory>(nameof(CustomerEmploymentHistorys));
            }
        }

        /// <summary>
        /// Вложенные файлы.
        /// </summary>
        [Aggregated]
        [Association]
        [MemberDesignTimeVisibility(false)]
        public IList<CustomerAttachment> Attachments
        {
            get
            {
                return GetList<CustomerAttachment>(nameof(Attachments));
            }
        }

        /// <summary>
        /// Платежная информация.
        /// </summary>
        [Aggregated, MemberDesignTimeVisibility(false)]
        public BillingInformation BillingInformation { get; set; }


        //TODO: Временные переменные, переделать 
        [MemberDesignTimeVisibility(false), Size(1024)]
        public string DOP1 { get; set; }
        [MemberDesignTimeVisibility(false), Size(1024)]
        public string DOP2 { get; set; }

        //TODO: Тут скорее всего надо переделать...
        [MemberDesignTimeVisibility(false)]
        [DisplayName("Папка текущая")]
        public Availability? IsPublic { get; set; }
        
        //TODO: Тут скорее всего надо переделать...
        [MemberDesignTimeVisibility(false)]
        [DisplayName("Папка ЗП")]
        public Availability? IsSalary { get; set; }
        
        //TODO: Тут скорее всего надо переделать...
        [MemberDesignTimeVisibility(false)]
        [DisplayName("Папка текущая + ЗП")]
        public Availability? IsPublicAndSalary { get; set; }

        [MemberDesignTimeVisibility(false)]
        public decimal? PercentAccountantResponsible { get; set; }

        [MemberDesignTimeVisibility(false)]
        public decimal? PercentPrimaryResponsible { get; set; }

        [MemberDesignTimeVisibility(false)]
        public decimal? PercentBankResponsible { get; set; }

        [MemberDesignTimeVisibility(false)]
        public decimal? PercentSalaryResponsible { get; set; }

        [MemberDesignTimeVisibility(false)]
        public decimal? PercentAdministrator { get; set; }

        public Notification GetNotification(TypeNotification typeNotification)
        {
            if (DateManagementBirth is DateTime dateManagementBirth)
            {
                var name = default(string);

                if (ToString() != null)
                {
                    name += $"Клиент: {ToString()}";
                }
                
                name += $"{Environment.NewLine}Дата рождения:{dateManagementBirth.ToShortDateString()}";

                return new Notification(typeNotification, "Дни рождения", dateManagementBirth, name, Oid, typeof(Customer));
            }
            else
            {
                return default;
            }
        }

        public override string ToString()
        {
            if (!string.IsNullOrWhiteSpace(Name))
            {
                return Name;
            }
            else if(!string.IsNullOrWhiteSpace(AbbreviatedName))
            {
                return AbbreviatedName;
            }            
            else
            {
                return FullName;
            }
        }
    }
}
