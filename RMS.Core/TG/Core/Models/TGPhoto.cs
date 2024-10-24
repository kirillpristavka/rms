using DevExpress.Xpo;

namespace RMS.Core.TG.Core.Models
{
    public class TGPhoto : TGFileBase
    {
        public TGPhoto() { }
        public TGPhoto(Session session) : base(session) { }

        public int Width { get; set; }
        public int Height { get; set; }
    }
}