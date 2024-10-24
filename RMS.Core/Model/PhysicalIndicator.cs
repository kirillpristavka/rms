using DevExpress.Xpo;

namespace RMS.Core.Model
{
    /// <summary>
    /// Физический показатель.
    /// </summary>
    public class PhysicalIndicator : XPObject
    {
        public PhysicalIndicator() { }
        public PhysicalIndicator(Session session) : base(session) { }

        /// <summary>
        /// Наименование вида деятельности.
        /// </summary>
        [DisplayName("Наименование"), Size(256)]
        public string Name { get; set; }

        /// <summary>
        /// Описание деятельности.
        /// </summary>
        [DisplayName("Описание"), Size(1024)]
        public string Description { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}