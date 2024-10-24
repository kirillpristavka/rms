using DevExpress.Xpo;

namespace RMS.Core.Model.InfoCustomer
{
    /// <summary>
    /// Таблица хранящая показатель работы и его параметр.
    /// </summary>
    public class CustomerPerformanceIndicator : XPObject
    {
        public CustomerPerformanceIndicator() { }
        public CustomerPerformanceIndicator(Session session) : base(session) { }

        [DisplayName("Группа")]
        public string GroupPerformanceIndicatorString => PerformanceIndicator?.GroupPerformanceIndicatorString;

        [DisplayName("Наименование")]
        public string PerformanceIndicatorString => ToString();

        [DisplayName("Сокращенное наименование")]
        [MemberDesignTimeVisibility(false)]
        public string PerformanceIndicatorAbbreviatedName => PerformanceIndicator?.AbbreviatedName;

        [MemberDesignTimeVisibility(false)]
        public PerformanceIndicator PerformanceIndicator { get; set; }

        /// <summary>
        /// Значение в строковой форме.
        /// </summary>
        [DisplayName("Значение")]
        public string Value { get; set; }

        [Association, MemberDesignTimeVisibility(false)]
        public OrganizationPerformance OrganizationPerformance { get; set; }

        public override string ToString()
        {
            return PerformanceIndicator?.ToString();
        }
    }
}