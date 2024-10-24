using DevExpress.Xpo;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Interface;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Notifications;
using RMS.Core.Model.Taxes;
using System;
using System.Linq;

namespace RMS.Core.Model
{
    /// <summary>
    /// Отчеты, ЗП/Аванс, ИП (страх, патент).
    /// </summary>
    public class IndividualEntrepreneursTax : XPObject, INotification
    {
        private IndividualEntrepreneursTax() { }
        public IndividualEntrepreneursTax(Session session) : base(session)
        {
            PeriodReportChange = PeriodReportChange.FIRSTQUARTER;
            Year = DateTime.Now.Year;
        }
        public IndividualEntrepreneursTax(Session session, int year, PeriodReportChange periodReportChange, PatentObject patentObject) : this(session)
        {
            Year = year;
            PeriodReportChange = periodReportChange;
            PatentObj = patentObject;            
        }
        public IndividualEntrepreneursTax(Session session, int year, PeriodReportChange periodReportChange, string name) : this(session)
        {
            Year = year;
            PeriodReportChange = periodReportChange;
            Name = name;

            if (periodReportChange == PeriodReportChange.FIRSTQUARTER)
            {
                DateDelivery = new DateTime(Year, 3, 25);
            }
            else if (periodReportChange == PeriodReportChange.SECONDQUARTER)
            {
                DateDelivery = new DateTime(Year, 6, 25);
            }
            else if (periodReportChange == PeriodReportChange.THIRDQUARTER)
            {
                DateDelivery = new DateTime(Year, 9, 25);
            }
            else if (periodReportChange == PeriodReportChange.FOURTHQUARTER)
            {
                DateDelivery = new DateTime(Year, 12, 25);
            }
        }
        public IndividualEntrepreneursTax(Session session, int year, PeriodReportChange periodReportChange, string name, int day) : this(session)
        {
            Year = year;
            PeriodReportChange = periodReportChange;
            Name = name;

            if (periodReportChange == PeriodReportChange.FIRSTQUARTER)
            {
                DateDelivery = new DateTime(Year, 4, day);
            }
            else if (periodReportChange == PeriodReportChange.SECONDQUARTER)
            {
                DateDelivery = new DateTime(Year, 7, day);
            }
            else if (periodReportChange == PeriodReportChange.THIRDQUARTER)
            {
                DateDelivery = new DateTime(Year, 10, day);
            }
            else if (periodReportChange == PeriodReportChange.FOURTHQUARTER)
            {
                DateDelivery = new DateTime(Year, 4, 30).AddYears(1);
            }
        }

        /// <summary>
        /// Уникальный идентификатор.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public string Guid { get; set; }

        [DisplayName("Дата создания")]
        [MemberDesignTimeVisibility(false)]
        public DateTime DateCreate { get; set; } = DateTime.Now;

        [DisplayName("Ответственный")]
        public string StaffString => Staff?.ToString();
        [MemberDesignTimeVisibility(false)]
        public Staff Staff { get; set; }
        
        [DisplayName("Клиент")]
        public string CustomerString => Customer?.ToString();
        [MemberDesignTimeVisibility(false)]
        public Customer Customer { get; set; }
        
        [DisplayName("Система налогообложения")]
        public string CustomerTaxSystem
        {
            get
            {
                var taxSystem = default(TaxSystem);
                var TaxSystemCustomerObjects = Customer?.TaxSystemCustomer?.TaxSystemCustomerObjects;

                if (TaxSystemCustomerObjects != null && TaxSystemCustomerObjects.Count > 0)
                {
                    //TODO: тут надо подумать как оптимизировать поиск интервалов дат.
                    var dateNow = DateCreate;             
                    taxSystem = TaxSystemCustomerObjects.FirstOrDefault(f =>
                        (f.DateSince == null && f.DateTo >= dateNow)
                        || (f.DateSince <= dateNow && f.DateTo >= dateNow)
                        || (f.DateSince <= dateNow && f.DateTo == null)
                        || (f.DateSince == null && f.DateTo == null)
                        )?.TaxSystem;
                }

                return taxSystem?.ToString();
            }
        }

        /// <summary>
        /// Год сдачи.
        /// </summary>
        [DisplayName("Год отчета")]
        [MemberDesignTimeVisibility(false)]
        public int Year { get; set; }

        [MemberDesignTimeVisibility(false)]
        public PeriodReportChange PeriodReportChange { get; set; }

        [DisplayName("Период")]
        public string PeriodReportChangeString
        {
            get
            {
                //TODO: надо ли делать так?
                if (PatentObj is null)
                {
                    return $"{PeriodReportChange.GetEnumDescription()} {Year} г.";                    
                }
                else
                {
                    return $"{Year} г.";
                }
            }
        }
        
        [Persistent(nameof(PatentObject))]
        [MemberDesignTimeVisibility(false)]
        public PatentObject PatentObj { get; set; }

        [DisplayName("Сдать до")]
        public DateTime? DateDelivery { get; set; }

        [Size(1024)]
        [DisplayName("Наименование")]
        public string Name { get; set; }                             

        [DisplayName("Сумма")]
        public decimal Sum { get; set; }
                
        [DisplayName("Статус")]
        public string StatusString => IndividualEntrepreneursTaxStatus?.Name;
        /// <summary>
        /// Статус.
        /// </summary>
        public IndividualEntrepreneursTaxStatus IndividualEntrepreneursTaxStatus { get; set; }

        [DisplayName("Оплата")]
        public bool IsPaid { get; set; }

        [DisplayName("Дата оплаты")]
        public DateTime? DatePaid { get; set; }

        [Size(4000)]
        [DisplayName("Комментарий")]
        public string Comment { get; set; }

        [MemberDesignTimeVisibility(false)]
        public string PatentObjStatusString => PatentObjStatus?.ToString();
        /// <summary>
        /// Статус патента.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public PatentStatus PatentObjStatus => PatentObj?.PatentStatus;
        
        /// <summary>
        /// Статус учета страховых.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public AccountingInsuranceStatus PatentObjAccountingInsuranceStatus1 => PatentObj?.AccountingInsuranceStatus;

        /// <summary>
        /// Статус учета страховых.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public AccountingInsuranceStatus PatentObjAccountingInsuranceStatus2 => PatentObj?.AccountingInsuranceStatus2;

        public static string GetDisplayableProperties(bool isPatent = false)
        {
            var result = default(string);

            if (isPatent)
            {
                result = $"{nameof(StaffString)};" +
                    $"{nameof(CustomerString)};" +
                    $"{nameof(CustomerTaxSystem)};" +
                    $"{nameof(PeriodReportChangeString)};" +
                    $"{nameof(PatentObj)}.{nameof(PatentObject.AccountingInsuranceStatusString)};" +
                    $"{nameof(PatentObj)}.{nameof(PatentObject.AccountingInsuranceStatusString2)};" +
                    $"{nameof(PatentObj)}.{nameof(PatentObject.Name)};" +
                    $"{nameof(PatentObj)}.{nameof(PatentObject.DateSince)};" +
                    $"{nameof(PatentObj)}.{nameof(PatentObject.DateTo)};" +
                    $"{nameof(PatentObj)}.{nameof(PatentObject.AdvancePaymentValue)};" +
                    $"{nameof(PatentObj)}.{nameof(PatentObject.IsPaymentAdvancePaymentValue)};" +
                    $"{nameof(PatentObj)}.{nameof(PatentObject.AdvancePaymentDate)};" +
                    $"{nameof(PatentObj)}.{nameof(PatentObject.ActualPaymentValue)};" +
                    $"{nameof(PatentObj)}.{nameof(PatentObject.IsPaymentActualPaymentValue)};" +
                    $"{nameof(PatentObj)}.{nameof(PatentObject.AdvancePaymentDate)};" +
                    $"{nameof(PatentObj)}.{nameof(PatentObject.PatentStatusString)};" + 
                    $"{nameof(Comment)}";
            }
            else
            {
                result = $"{nameof(StaffString)};" +
                    $"{nameof(CustomerString)};" +
                    $"{nameof(CustomerTaxSystem)};" +
                    $"{nameof(PeriodReportChangeString)};" +
                    $"{nameof(DateDelivery)};" +
                    $"{nameof(Name)};" +
                    $"{nameof(Sum)};" +
                    $"{nameof(StatusString)};" +
                    $"{nameof(IsPaid)};" +
                    $"{nameof(DatePaid)};" +
                    $"{nameof(Comment)}";
            }
            
            return result;
        }

        public override string ToString()
        {
            return $"[{Customer}] {Name} от {DateCreate.ToShortDateString()}";
        }

        public Notification GetNotification(TypeNotification typeNotification)
        {
            var date = DateTime.Now;

            var nameModel = default(string);
            if (PatentObj is null)
            {
                nameModel = "ИП/Страховые";
            }
            else
            {
                nameModel = "ИП/Патенты";
            }

            var name = default(string);

            if (Customer != null)
            {
                name += $"Клиент: {Customer}";
            }

            if (Staff != null)
            {
                name += $"{Environment.NewLine}Ответственный: {Staff}";
            }            

            name += $"{Environment.NewLine}Период: {PeriodReportChange.GetEnumDescription()}";

            if (DateDelivery is DateTime deadline)
            {
                date = deadline;

                if (IndividualEntrepreneursTaxStatus != null)
                {
                    name += $"{Environment.NewLine}Статус: {IndividualEntrepreneursTaxStatus}";
                }

                return new Notification(typeNotification, nameModel, date, name, Oid, typeof(IndividualEntrepreneursTax));
            }
            else if (PatentObj != null)
            {
                if (PatentObj.DateTo is DateTime dateTo)
                {
                    date = dateTo;
                }

                if (PatentObjStatus != null)
                {
                    name += $"{Environment.NewLine}Статус патента: {PatentObjStatus}";
                }

                return new Notification(typeNotification, nameModel, date, name, Oid, typeof(IndividualEntrepreneursTax));
            }            
            else
            {
                return default;
            }
        }
    }
}
