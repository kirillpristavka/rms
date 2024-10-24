using DevExpress.Xpo;
using System;

namespace RMS.Core.Model
{
    public class set_SprEnumeration : XPObject
    {
        public set_SprEnumeration() { }
        public set_SprEnumeration(Session session) : base(session) { }

        public string g_id; // Guid.NewGuid().ToString();
        public int ImageIndex;
        public int Group;

        [Size(254)]
        public string Name { get; set; }
        [Size(1000)]
        public string FullName;

        public DateTime date_begin;
        public DateTime date_end;
        public string User;
        public string Org;
        public int Task;
        public int Year;
        public bool fl_sel;
        public bool fl_def;

        public override string ToString() { return Name; }
    }
}
