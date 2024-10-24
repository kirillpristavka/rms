using DevExpress.Xpo;

namespace RMS.Core.Model
{
    public class Country : XPObject
    {
        public Country() { }
        public Country(Session session) : base(session) { }

        [Size(256)]
        [DisplayName("Страна")]
        public string Name { get; set; }

        [Size(2048)]
        [DisplayName("Описание")]
        public string Description { get; set; }

        public override string ToString()
        {
            return Name ?? base.ToString();
        }
    }
}
