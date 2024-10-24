using DevExpress.Xpo;

namespace RMS.Core.Model.ElectronicDocumentsManagement
{
    /// <summary>
    /// Электронная отчетность.
    /// </summary>
    public class ElectronicDocumentManagement : XPObject
    {
        public ElectronicDocumentManagement() { }
        public ElectronicDocumentManagement(Session session) : base(session) { }
        
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
