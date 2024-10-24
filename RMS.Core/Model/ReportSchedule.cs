using DevExpress.Xpo;
using RMS.Core.Enumerator;
using RMS.Core.Model.Reports;

namespace RMS.Core.Model
{
    /// <summary>
    /// График сдачи отчета.
    /// </summary>
    public class ReportSchedule : XPObject
    {
        public ReportSchedule() { }
        public ReportSchedule(Session session) : base(session) { }

        [DisplayName("День")]
        public int Day { get; set; }

        [DisplayName("Месяц")]
        public Month Month { get; set; }

        [DisplayName("Период")]
        public PeriodReportChange? Period { get; set; }

        [Association]
        [MemberDesignTimeVisibility(false)]
        public Report Report { get; set; }
    }
}