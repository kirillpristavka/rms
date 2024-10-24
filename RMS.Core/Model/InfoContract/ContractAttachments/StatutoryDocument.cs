using DevExpress.Xpo;

namespace RMS.Core.Model.InfoContract.ContractAttachments
{
    /// <summary>
    /// Уставной документ.
    /// </summary>
    public class StatutoryDocument : XPObject
    {
        public StatutoryDocument() { }
        public StatutoryDocument(Session session) : base(session) { }

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
