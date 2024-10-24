using DevExpress.Xpo;

namespace RMS.Core.TG.Core.Models
{
    public class TGMessagePhoto : XPObject
    {
        public TGMessagePhoto() { }
        public TGMessagePhoto(Session session) : base(session) { }

        public TGPhoto TGPhoto { get; set; }

        [Association]
        public TGMessage TGMessage { get; set; }
    }
}