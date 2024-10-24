using DevExpress.Xpo;
using RMS.Core.Enumerator;
using RMS.Core.Model.InfoCustomer;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace RMS.Core.Model.Salary
{
    public class CustomerSalaryAdvance : XPObject
    {
        public CustomerSalaryAdvance() { }
        public CustomerSalaryAdvance(Session session) : base(session) { }

        [MemberDesignTimeVisibility(false)]
        public int Month => Date.Month;
        
        [MemberDesignTimeVisibility(false)]
        public int Year => Date.Year;

        [DisplayName("Ответственный")]
        public string StaffString => Staff?.ToString();
        [MemberDesignTimeVisibility(false)]
        public Staff Staff { get; set; }        

        [DisplayName("Клиент")]
        public string CustomerString => Customer?.ToString();

        //[DisplayName("Система налогообложения")]
        //public string TaxSystemCustomerString => Customer?.TaxSystemCustomerString;

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
                    var dateNow = Date;
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

        [MemberDesignTimeVisibility(false)]
        public Customer Customer { get; set; }

        [DisplayName("Расчетный месяц")]
        public string SettlementMonth 
        {
            get
            {
                var result = default(string);

                try
                {
                    if (Date.Day >= 1 && 15 >= Date.Day)
                    {
                        result = DateTimeFormatInfo.CurrentInfo.GetMonthName(Date.AddMonths(-1).Month);
                    }
                    else
                    {
                        result = DateTimeFormatInfo.CurrentInfo.GetMonthName(Date.Month);
                    }

                    if (Month == 1)
                    {
                        result += $" {Year - Month} г.";
                    }
                    else
                    {
                        result += $" {Year} г.";
                    }                    
                }
                catch (Exception ex)
                {
                    RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                }                
                
                return result;
            }
        }

        [DisplayName("Дата ЗП/Аванс")]
        public DateTime Date { get; set; } = DateTime.Now;

        [DisplayName("Тип начисления")]
        public TypeAccrual TypeAccrual { get; set; }

        [DisplayName("Статус")]
        public string StatusAccrualString => StatusAccrual?.ToString();
        [MemberDesignTimeVisibility(false)]
        public StatusAccrual StatusAccrual { get; set; }

        [DisplayName("Фактическая дата")]
        public DateTime? ActualDate { get; set; }

        [Size(4000)]
        [DisplayName("Комментарий")]
        public string Comment { get; set; }

        public override string ToString()
        {
            var result = default(string);

            if (TypeAccrual == TypeAccrual.Advance)
            {
                result += "ЗП ";
            }
            else
            {
                result += "Аванс ";
            }

            if (Customer != null)
            {
                result += $"клиента {CustomerString} за {Date.ToShortDateString()}";
            }
            
            return result;
        }        
    }
}
