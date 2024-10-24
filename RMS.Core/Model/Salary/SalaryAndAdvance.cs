using DevExpress.Xpo;
using RMS.Core.Enumerator;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace RMS.Core.Model.Salary
{
    public class SalaryAndAdvance : XPObject
    {
        public SalaryAndAdvance() { }
        public SalaryAndAdvance(Session session) : base(session) { }

        [DisplayName("День")]
        public string CurrentObjectString => CurrentObject?.ToString();

        [MemberDesignTimeVisibility(false)]
        public int? CurrentObject => CurrentSalaryObject?.Day;

        [MemberDesignTimeVisibility(false)]
        public SalaryAndAdvanceObject CurrentSalaryObject
        {
            get
            {
                var salaryObject = default(SalaryAndAdvanceObject);

                if (SalaryObjects != null && SalaryObjects.Count > 0)
                {                    
                    var dateNow = DateTime.Now.Date;
                    salaryObject = SalaryObjects.FirstOrDefault(f =>
                        (f.DateSince == null && f.DateTo >= dateNow)
                        || (f.DateSince <= dateNow && f.DateTo >= dateNow)
                        || (f.DateSince <= dateNow && f.DateTo == null)
                        || (f.DateSince == null && f.DateTo == null)
                        );
                }

                return salaryObject;
            }
        }
        
        /// <summary>
        /// Получить дату начисления ЗП или аванса.
        /// </summary>
        /// <param name="month">Месяц.</param>
        /// <param name="year">Год.</param>
        /// <returns></returns>
        public DateTime? GetObjectDateTime(Month month, int year)
        {
            try
            {
                var dateTimeCurrentSince = new DateTime(year, (int)month, 1);
                var dateTimeCurrentTo = dateTimeCurrentSince.AddMonths(1).AddDays(-1);

                var listObj = new List<SalaryAndAdvanceObject>();

                listObj.AddRange(SalaryObjects.Where(w =>
                            (w.DateSince == null && w.DateTo >= dateTimeCurrentSince)
                            || (w.DateSince <= dateTimeCurrentSince && w.DateTo >= dateTimeCurrentSince)
                            || (w.DateSince <= dateTimeCurrentSince && w.DateTo == null)
                            || (w.DateSince == null && w.DateTo == null)
                            ));

                listObj.AddRange(SalaryObjects.Where(w =>
                            (w.DateSince == null && w.DateTo >= dateTimeCurrentTo)
                            || (w.DateSince <= dateTimeCurrentTo && w.DateTo >= dateTimeCurrentTo)
                            || (w.DateSince <= dateTimeCurrentTo && w.DateTo == null)
                            || (w.DateSince == null && w.DateTo == null)
                            ));

                if (listObj.Count == 0)
                {
                    return default;
                }
                else
                {
                    var day = listObj.FirstOrDefault(f => f.Day != null)?.Day;
                    if (day is null)
                    {
                        return default;
                    }
                    else
                    {
                        if (day == 0)
                        {
                            day = 1;
                        }

                        var dateTime = new DateTime(year, (int)month, (int)day);
                        return dateTime;
                    }
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                return default;
            }            
        }
        
        /// <summary>
        /// Комментарий.
        /// </summary>
        [DisplayName("Комментарий"), Size(1024)]
        public string Comment { get; set; }
        
        
        [Association, Aggregated, MemberDesignTimeVisibility(false)]
        public XPCollection<SalaryAndAdvanceObject> SalaryObjects
        {
            get
            {
                return GetCollection<SalaryAndAdvanceObject>(nameof(SalaryObjects));
            }
        }

        public override string ToString()
        {
            return CurrentObjectString;
        }
    }
}
