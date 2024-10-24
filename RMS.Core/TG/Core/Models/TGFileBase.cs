using DevExpress.Xpo;

namespace RMS.Core.TG.Core.Models
{
    public class TGFileBase : XPObject
    {
        public TGFileBase() { }
        public TGFileBase(Session session) : base(session) { }

        public string FileId { get; set; }
        public string FileUniqueId { get; set; }
        public long? FileSize { get; set; }
        public byte[] Obj { get; set; }
    }
}