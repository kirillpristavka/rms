using DevExpress.Xpo;

namespace RMS.Core.Model.Reports
{
    public class StatisticalReportObj : XPObject
    {
        public StatisticalReportObj() { }
        public StatisticalReportObj(Session session) : base(session) { }
        
        [MemberDesignTimeVisibility(false)]
        public ReportChange ReportChange { get; set; }

        [Association]
        [MemberDesignTimeVisibility(false)]
        public StatisticalReport StatisticalReport { get; set; }

        public override string ToString()
        {
            if (ReportChange != null || ReportChange?.IsDeleted is false)
            {
                return ReportChange?.ToString();
            }
            
            return base.ToString();
        }
    }
}