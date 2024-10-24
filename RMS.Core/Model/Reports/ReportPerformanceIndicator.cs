using DevExpress.Xpo;

namespace RMS.Core.Model.Reports
{
    /// <summary>
    /// Показатели работы для отчетов.
    /// </summary>
    public class ReportPerformanceIndicator : XPObject
    {
        public ReportPerformanceIndicator() { }
        public ReportPerformanceIndicator(Session session) : base(session) { }

        [DisplayName("Наименование")]
        public string PerformanceIndicatorString => PerformanceIndicator?.ToString();

        [MemberDesignTimeVisibility(false)]
        public PerformanceIndicator PerformanceIndicator { get; set; }

        [Association]
        [MemberDesignTimeVisibility(false)]
        public Report Report { get; set; }
    }
}