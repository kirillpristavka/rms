using System;
using System.ComponentModel;

namespace RMS.Core.ObjectDTO.Models
{
    public class ObjDTO
    {
        [Browsable(false)]
        [DisplayName("OID")]
        public int Oid { get; private set; }
    }

    public class CustomerFilterDTO : ObjDTO
    {
        [DisplayName("Наименование")]
        public string Name { get; set; }

        [DisplayName("Количество")]
        public int Count { get; set; }
    }

    [DisplayName("Клиент")]
    public class CustomerDTO : ObjDTO
    {
        [DisplayName("Дата создания")]
        public DateTime? DateCreate { get; private set; }

        [DisplayName("Статус")]
        public string StatusString { get; private set; }

        [DisplayName("ИНН")]
        public string INN { get; private set; }

        [DisplayName("Наименование")]
        public string Name { get; private set; }

        [DisplayName("Наименование")]
        public string ProcessedName { get; private set; }

        [DisplayName("Наименование")]
        public string DefaultName { get; private set; }

        [DisplayName("Полное наименование")]
        public string FullName { get; private set; }

        [DisplayName("Сокращенное наименование")]
        public string AbbreviatedName{ get; private set; }

        [DisplayName("Дата актуальности состояния")]
        public DateTime? DateActuality{ get; private set; }

        [DisplayName("Дата регистрации")]
        public DateTime? DateRegistration{ get; private set; }

        [DisplayName("Дата ликвидации")]
        public DateTime? DateLiquidation{ get; private set; }

        [DisplayName("Статус организации")]
        public string OrganizationStatusString { get; private set; }        

        [DisplayName("Руководитель")]
        public string ManagementString { get; private set; }

        public string ManagementNameAndPatronymicString { get; private set; }

        [DisplayName("Руководитель")]
        public string ManagementFullString { get; private set; }

        [DisplayName("Руководитель (Должность)")]
        public string ManagementPositionString { get; private set; }

        [DisplayName("Дата рождения руководителя")]
        public DateTime? DateManagementBirth{ get; private set; }

        [DisplayName("ОКПО")]
        public string OKPO{ get; private set; }

        [DisplayName("ОКВЭД")]
        public string OKVED{ get; private set; }

        [DisplayName("ОГРН")]
        public string PSRN{ get; private set; }

        [DisplayName("Дата выдачи ОГРН")]
        public DateTime? DatePSRN{ get; private set; }

        [DisplayName("Телефон")]
        public string Telephone { get; private set; }

        [DisplayName("Email")]
        public string Email { get; private set; }

        [DisplayName("Юридический адрес")]
        public string LegalAddress { get; private set; }

        [DisplayName("Вид деятельности")]
        public string KindActivityString { get; private set; }

        [DisplayName("Дата начала ответственности бухгалтера")]
        public DateTime? AccountantResponsibleDate{ get; private set; }

        [DisplayName("Дата начала ответственности первички")]
        public DateTime? PrimaryResponsibleDate{ get; private set; }

        [DisplayName("Дата начала ответственности за банк")]
        public DateTime? BankResponsibleDate{ get; private set; }

        [DisplayName("Дата начала ответственности за ЗП")]
        public DateTime? SalaryResponsibleDate{ get; private set; }

        [DisplayName("Ответственный главный бухгалтер")]
        public string AccountantResponsibleString { get; private set; }

        [DisplayName("Ответственный за банк")]
        public string BankResponsibleString { get; private set; }

        [DisplayName("Является ли клиент ответственным за банк")]
        public bool CustomerIsBankResponsible{ get; private set; }
        
        [DisplayName("Является ли клиент ответственным за первичку")]
        public bool CustomerIsPrimaryResponsible{ get; private set; } = true;

        [DisplayName("Ответственный за первичные документы")]
        public string PrimaryResponsibleString { get; private set; }

        [DisplayName("Является ли клиент ответственным за ЗП")]
        public bool CustomerIsSalaryResponsible{ get; private set; }

        [DisplayName("Ответственный за ЗП")]
        public string SalaryResponsibleString { get; private set; }

        [DisplayName("Детали обслуживания")]
        public string ServiceDetails{ get; private set; }

        [DisplayName("КПП")]
        public string KPP{ get; private set; }

        [DisplayName("ОКТМО")]
        public string OKTMO{ get; private set; }

        [DisplayName("ОКАТО")]
        public string OKATO{ get; private set; }

        [DisplayName("Система налогообложения")]
        public string TaxSystemCustomerString { get; private set; }

        [DisplayName("Электронная отчетность (c)")]
        public string ElectronicReportingStringDateSince { get; private set; }

        [DisplayName("Электронная отчетность (по)")]
        public string ElectronicReportingStringDateTo { get; private set; }

        [DisplayName("Электронная отчетность")]
        public string ElectronicReportingString { get; private set; }
        
        [DisplayName("ЭДО")]
        public string ElectronicDocumentManagementString { get; private set; }

        [DisplayName("Форма собственности")]
        public string FormCorporationString { get; private set; }

        [DisplayName("Договора")]
        public string ContractsString { get; private set; }
              
        [DisplayName("День выплаты заработной платы")]
        public int? SalaryDay { get; private set; }

        [DisplayName("День выплаты аванса")]
        public int? ImprestDay { get; private set; }

        [DisplayName(" Вид выплаты аванса и ЗП")]
        public string TypePaymentString { get; private set; }
        
        [DisplayName("Страховые взносы")]
        public string InsurancePremiums{ get; private set; }
                
        [DisplayName("Электронная папка")]
        public string ElectronicFolder{ get; private set; }

        [DisplayName("Заметки")]
        public string Notes { get; private set; }

        [DisplayName("Заметки")]
        public string NoteByte { get; private set; }

        [DisplayName("Налоги")]
        public string TaxString { get; private set; }
        
        public string TaxType{ get; private set; }

        public string TaxTypePercent{ get; private set; }

        [DisplayName("1C Бухгалтерия")]
        public string OneEsBookkeepingString { get; private set; }
        [DisplayName("1C Зарплата")]
        public string OneEsSalaryString { get; private set; }

        [DisplayName("1C Иное")]
        public string OneEsOtherString { get; private set; }
        
        public string LeasingString { get; private set; }        
        
        [DisplayName("Отчеты по статистике")]
        public string StatisticalReportsString { get; private set; }
        
        [DisplayName("Отчеты")]
        public string CustomerReportsString { get; private set; }
        
        [DisplayName("Банковские счета")]
        public string AccountsString { get; private set; }
        
        [DisplayName("Контакты")]
        public string ContactsString { get; private set; }        

        [DisplayName("Нюансы НДФЛ")]
        public string PersonalIncomeTaxisString { get; private set; }
        
        [DisplayName("Трудовые книжки")]
        public string CustomerEmploymentHistorysString { get; private set; }

        public override string ToString()
        {
            if (!string.IsNullOrWhiteSpace(Name))
            {
                return Name;
            }
            else if (!string.IsNullOrWhiteSpace(AbbreviatedName))
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
