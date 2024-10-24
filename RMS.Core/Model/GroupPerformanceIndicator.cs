using DevExpress.Xpo;

namespace RMS.Core.Model
{
    /// <summary>
    /// Группа показателя работы.
    /// </summary>
    public class GroupPerformanceIndicator : XPObject
    {
        public GroupPerformanceIndicator() { }
        public GroupPerformanceIndicator(Session session) : base(session) { }
        
        [DisplayName("Наименование"), Size(256)]
        public string Name { get; set; }

        [DisplayName("Описание"), Size(1024)]
        public string Description { get; set; }

        [Association, MemberDesignTimeVisibility(false)]
        public XPCollection<PerformanceIndicator> PerformanceIndicators
        {
            get
            {
                return GetCollection<PerformanceIndicator>(nameof(PerformanceIndicators));
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
