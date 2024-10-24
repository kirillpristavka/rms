using DevExpress.Xpo;

namespace RMS.Core.Model
{
    /// <summary>
    /// Группы для прайса.
    /// </summary>
    public class PriceGroup : XPObject
    {
        public PriceGroup() { }
        public PriceGroup(Session session) : base(session) { }
        
        /// <summary>
        /// Наименование.
        /// </summary>
        [Size(256)]
        [DisplayName("Наименование")]
        public string Name { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        [Size(1024)]
        [DisplayName("Описание")]
        public string Description { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}