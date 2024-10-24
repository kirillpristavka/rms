using DevExpress.Xpo;

namespace RMS.Core.Model.Reports
{
    /// <summary>
    /// Отчеты.
    /// </summary>
    public class ReportV2 : XPObject
    {
        public ReportV2() { }
        public ReportV2(Session session) : base(session) { }

        /// <summary>
        /// Наименование формы.
        /// </summary>
        [Size(512)]
        [DisplayName("Наименование")]
        public string Name { get; set; }

        /// <summary>
        /// Комментарий.
        /// </summary>
        [Size(4000)]
        [DisplayName("Комментарий")]
        public string Description { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
