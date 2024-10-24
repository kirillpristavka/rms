using DevExpress.Xpo;
using System;

namespace RMS.Core.Model
{
    public class VacationPeriod : XPObject
    {
        public VacationPeriod() { }
        public VacationPeriod(Session session) : base(session) { }

        [DisplayName("Дата начала")]
        public DateTime DateSince { get; set; }

        [DisplayName("Дата окончания")]
        public DateTime DateTo { get; set; }

        /// <summary>
        /// Сотрудник.
        /// </summary>
        public Staff Staff { get; set; }

        public override string ToString()
        {
            return $"{DateSince.ToShortDateString()} - {DateTo.ToShortDateString()}";
        }
    }
}