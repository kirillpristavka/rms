using DevExpress.Xpo;
using RMS.Core.Extensions;
using RMS.Core.Model.Reports;

namespace RMS.Core.Model
{
    public class TaxSystemReport : XPObject
    {
        public TaxSystemReport() { }
        public TaxSystemReport(Session session) : base(session) { }

        [DisplayName("ОКУД")]
        public string ReportOKUDString => Report?.OKUD;

        [DisplayName("Наименование")]
        public string ReportString => Report?.Name;

        [DisplayName("Периодичность")]
        public string ReportPeriodicityString => Report?.Periodicity.GetEnumDescription();

        [MemberDesignTimeVisibility(false)]
        public Report Report { get; set; }

        [Association]
        [MemberDesignTimeVisibility(false)]
        public TaxSystem TaxSystem { get; set; }
    }
}