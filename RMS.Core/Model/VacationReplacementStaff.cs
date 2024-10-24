using DevExpress.Xpo;
using System;

namespace RMS.Core.Model
{
    /// <summary>
    /// Сотрудник для замены на время отпуска.
    /// </summary>
    public class VacationReplacementStaff : XPObject
    {
        public VacationReplacementStaff() { }
        public VacationReplacementStaff(Session session) : base(session) { }

        [DisplayName("Сотрудник")]
        public string StaffName => Staff?.ToString();
        /// <summary>
        /// Сотрудник.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Staff Staff { get; set; }

        [DisplayName("Дата начала замены")]
        public DateTime? DateSince { get; set; }

        [DisplayName("Дата окончания замены")]
        public DateTime? DateTo { get; set; }

        /// <summary>
        /// Комментарий.
        /// </summary>
        [Size(2048)]
        [DisplayName("Комментарий")]
        public string Comment { get; set; }
        [Association]
        [MemberDesignTimeVisibility(false)]
        public Vacation Vacation { get; set; }

        public override string ToString()
        {
            var result = default(string);            

            result += Staff?.ToString();

            if (DateSince is DateTime dateSince)
            {
                result += $" c {dateSince.ToShortDateString()}";
            }

            if (DateTo is DateTime dateTo)
            {
                result += $" по {dateTo.ToShortDateString()}";
            }

            if (string.IsNullOrWhiteSpace(result))
            {
                result = Oid.ToString();
            }
            
            return result;
        }
    }
}