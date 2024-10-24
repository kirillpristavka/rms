using DevExpress.Xpo;
using System;
using System.Linq;

namespace RMS.Core.Model
{
    public class Vacation : XPObject
    {
        private readonly string _vacantionName = "жегодный";
        
        public Vacation() { }
        public Vacation(Session session) : base(session) { }

        [DisplayName("Утверждено")]
        public bool IsConfirm { get; set; }
        
        [DisplayName("Период")]
        public string Period => VacationPeriod?.ToString();
        /// <summary>
        /// Период за который предоставляется отпуск.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public VacationPeriod VacationPeriod { get; set; }

        [DisplayName("Вид отпуска")]
        public string VacationTypeName => VacationType?.Name;
        /// <summary>
        /// Вид отпуска.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public VacationType VacationType { get; set; }

        [DisplayName("Сотрудник")]
        public string StaffName => Staff?.ToString();
        /// <summary>
        /// Сотрудник.
        /// </summary>
        [Association]
        [MemberDesignTimeVisibility(false)]
        public Staff Staff { get; set; }

        [DisplayName("Дата начала отпуска")]
        public DateTime DateSince { get; set; }
        
        [DisplayName("Дата окончания отпуска")]
        public DateTime DateTo { get; set; }

        [DisplayName("Продолжительность (дней)")]
        public string DurationValue => Duration.ToString();

        /// <summary>
        /// Продолжительность (дней).
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public int Duration => (DateTo - DateSince).Days + 1;

        /// <summary>
        /// Использовано за период (дней).
        /// </summary>
        [DisplayName("Использовано за период (дней)")]
        public string UseDaysVacation
        {
            get
            {
                var result = 0;

                if (Staff.Vacations != null)
                {
                    var currentDateTime = DateTime.Now;
                    var vacantions = Staff.Vacations?
                        .Where(w => w.IsConfirm
                                && w.VacationType != null 
                                && w.VacationType.Name.Contains(_vacantionName)
                                && w.VacationPeriod == VacationPeriod
                                && w.VacationPeriod != null
                                && w.VacationPeriod.DateSince.Year <= currentDateTime.Year
                                && w.VacationPeriod.DateTo.Year >= currentDateTime.Year);

                    var days = 0;
                    foreach (var item in vacantions)
                    {
                        days += item.Duration;
                    }

                    result += days;
                }

                return result.ToString();
            }
        }

        /// <summary>
        /// Остаток дней отпуска.
        /// </summary>
        [DisplayName("Остаток за период (дней)")]
        public string RemainingDaysVacation
        {
            get
            {
                var result = 28;

                if (Staff.Vacations != null)
                {
                    var currentDateTime = DateTime.Now;
                    var vacantions = Staff.Vacations?
                        .Where(w => w.IsConfirm
                                && w.VacationType != null
                                && w.VacationType.Name.Contains(_vacantionName)
                                && w.VacationPeriod == VacationPeriod
                                && w.VacationPeriod != null
                                && w.VacationPeriod.DateSince.Year <= currentDateTime.Year
                                && w.VacationPeriod.DateTo.Year >= currentDateTime.Year);

                    var days = 0;
                    foreach (var item in vacantions)
                    {
                        days += item.Duration;
                    }

                    result -= days;
                }

                return result.ToString();
            }
        }       

        [DisplayName("Сотрудники на замену")]
        public string ReplacementStaffName
        {
            get
            {
                var result = default(string);

                if (VacationReplacementStaffs != null && VacationReplacementStaffs.Count > 0)
                {
                    foreach (var item in VacationReplacementStaffs)
                    {
                        if (item.Staff is null)
                        {
                            continue;
                        }

                        result += item.Staff?.ToString();

                        if (item.DateSince is DateTime dateSince)
                        {
                            result += $" c {dateSince.ToShortDateString()}";
                        }

                        if (item.DateTo is DateTime dateTo)
                        {
                            result += $" по {dateTo.ToShortDateString()}";
                        }

                        if (!string.IsNullOrWhiteSpace(item.Comment))
                        {
                            result += $"{Environment.NewLine}{item.Comment}";
                        }

                        result += Environment.NewLine;
                    }
                }
                
                return result?.Trim();
            }
        }
        [MemberDesignTimeVisibility(false)]
        public Staff ReplacementStaff { get; set; }

        /// <summary>
        /// Сотрудники на замену.
        /// </summary>
        [Association]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<VacationReplacementStaff> VacationReplacementStaffs 
        {
            get
            {
                return GetCollection<VacationReplacementStaff>(nameof(VacationReplacementStaffs));
            }
        }

        /// <summary>
        /// Комментарий.
        /// </summary>
        [Size(2048)]
        [DisplayName("Комментарий")]
        public string Comment { get; set; }
    }
}