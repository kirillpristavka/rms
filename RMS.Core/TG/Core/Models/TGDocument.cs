using DevExpress.Xpo;

namespace RMS.Core.TG.Core.Models
{
    public class TGDocument : TGFileBase
    {
        public TGDocument() { }
        public TGDocument(Session session) : base(session) { }

        public string FileName { get; set; }
        public string MimeType { get; set; }
    }
}