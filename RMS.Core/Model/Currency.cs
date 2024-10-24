using DevExpress.Xpo;

namespace RMS.Core.Model
{
    /// <summary>
    /// Валюта.
    /// </summary>
    public class Currency : XPObject
    {
        public Currency() { }
        public Currency(Session session) : base(session) { }

        public string Name { get; set; }
        public string ISO { get; set; }
        public string OKW { get; set; }

        public override string ToString()
        {
            return ISO;
        }
    }
}
