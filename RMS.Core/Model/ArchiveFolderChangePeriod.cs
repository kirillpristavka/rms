using DevExpress.Xpo;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using System;

namespace RMS.Core.Model
{
    /// <summary>
    /// Периоды архивных папок.
    /// </summary>
    public class ArchiveFolderChangePeriod : XPObject
    {

        public ArchiveFolderChangePeriod() { }
        public ArchiveFolderChangePeriod(Session session) : base(session) { }

        [Association, MemberDesignTimeVisibility(false)]
        public ArchiveFolderChange ArchiveFolderChange { get; set; }

        [DisplayName("Период")]
        public PeriodReportChange PeriodReportChange { get; set; } = PeriodReportChange.YEAR;

        private Month? month;
        
        [DisplayName("Месяц")]
        public Month? Month 
        {
            get 
            {
                return month;
            }
            set 
            {
                if (PeriodReportChange == PeriodReportChange.MONTH)
                {
                    month = value;
                }
                else
                {
                    month = null;
                }
            }
        }

        [DisplayName("Год")]
        public int Year { get; set; } = DateTime.Now.Year;


        public override string ToString()
        {
            var result = default(string);
            
            if (PeriodReportChange == PeriodReportChange.MONTH)
            {
                if (Month is null)
                {
                    result = $"{PeriodReportChange.MONTH.GetEnumDescription()} {Year}г.";
                }
                else
                {
                    result = $"{Month.GetEnumDescription()} {Year}г.";
                }                
            }
            else if (PeriodReportChange == PeriodReportChange.YEAR)
            {

                result = $"{Year}г.";
            }
            else 
            {
                result = $"{PeriodReportChange.GetEnumDescription()} {Year}г.";
            }

            return result;
        }
    }
}