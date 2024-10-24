using DevExpress.Xpo;
using RMS.Core.Extensions;
using RMS.Core.Model.InfoCustomer;

namespace RMS.Core.Model.Reports
{
    /// <summary>
    /// Клиентский отчет.
    /// </summary>
    public class CustomerReport : XPObject
    {
        public CustomerReport() { }
        public CustomerReport(Session session) : base(session) { }

        [DisplayName("Индекс формы"), Size(128)]
        public string ReportFormIndex => Report?.FormIndex;

        [DisplayName("Наименование формы"), Size(256)]
        public string ReportName => Report?.Name;

        [DisplayName("Периодичность"), Size(128)]
        public string ReportPeriodicity => Report?.Periodicity.GetEnumDescription();

        [DisplayName("Сроки сдачи"), Size(512)]
        public string ReportDeadline => Report?.Deadline;

        [DisplayName("Комментарий"), Size(1024)]
        public string ReportComment => Report?.Comment;

        [DisplayName("ОКУД"), Size(128)]
        public string ReportOKUD => Report?.OKUD;
        /// <summary>
        /// Отчет.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Report Report { get; set; }

        [Association]
        [MemberDesignTimeVisibility(false)]
        public Customer Customer { get; set; }

        public override string ToString()
        {
            return Report?.FormIndex;
        }
    }
}
