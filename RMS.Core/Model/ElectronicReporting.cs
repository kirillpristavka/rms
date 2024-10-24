using DevExpress.Xpo;

namespace RMS.Core.Model
{
    /// <summary>
    /// Электронная отчетность.
    /// </summary>
    public class ElectronicReporting : XPObject
    {
        public ElectronicReporting() { }
        public ElectronicReporting(Session session) : base(session) { }
        
        [Size(256)]
        [DisplayName("Наименование")]
        public string Name { get; set; }
        
        [Size(1024)]
        [DisplayName("Описание")]
        public string Description { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
