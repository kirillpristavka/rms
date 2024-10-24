using DevExpress.Xpo;

namespace RMS.Core.Model.InfoContract.ContractAttachments
{
    /// <summary>
    /// Первичные документы.
    /// </summary>
    public class SourceDocument : XPObject
    {
        public SourceDocument() { }
        public SourceDocument(Session session) : base(session) { }

        [DisplayName("Наименование"), Size(256)]
        public string Name { get; set; }

        [DisplayName("Описание"), Size(1024)]
        public string Description { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
