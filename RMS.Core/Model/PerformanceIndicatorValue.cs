using DevExpress.Xpo;

namespace RMS.Core.Model
{
    /// <summary>
    /// Шкалы показателей.
    /// </summary>
    public class PerformanceIndicatorValue : Scale
    {
        public PerformanceIndicatorValue() { }
        public PerformanceIndicatorValue(Session session) : base(session) { }        

        /// <summary>
        /// Показатель.
        /// </summary>
        [Association, MemberDesignTimeVisibility(false)]
        public PerformanceIndicator PerformanceIndicator { get; set; }
    }
}