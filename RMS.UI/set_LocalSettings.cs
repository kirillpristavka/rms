using DevExpress.Xpo;

namespace RMS.UI
{
    public class set_LocalSettings : XPObject
    {
        public set_LocalSettings() : base() { }
        public set_LocalSettings(Session session) : base(session) { }

        public string g_id { get; set; }

        [Size(254)]
        public string Name { get; set; }
        [Size(254)]
        public string FullName { get; set; }
        [Size(1000)]
        public string Value1 { get; set; }
        [Size(254)]
        public string Value2 { get; set; }
        [Size(254)]
        public string Value3 { get; set; }

        public byte[] Obj { get; set; }

        public string User { get; set; }
        public string Org { get; set; }
        public string Task { get; set; }
        public string Year { get; set; }
        public bool fl_def { get; set; }

        public override string ToString() { return FullName; }
    }
}
