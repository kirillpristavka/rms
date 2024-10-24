using DevExpress.Xpo;

namespace RMS.Core.Model
{
    /// <summary>
    /// Льготы.
    /// </summary>
    public class Privilege : XPObject
    {
        public Privilege() { }
        public Privilege(Session session) : base(session) { }

        /// <summary>
        /// Код.
        /// </summary>
        [DisplayName("Код")]
        public int Kod { get; set; }

        /// <summary>
        /// Наименование.
        /// </summary>
        [DisplayName("Наименование"), Size(256)]
        public string Name { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        [DisplayName("Описание"), Size(1024)]
        public string Description { get; set; }

        public override string ToString()
        {
            return Kod.ToString();
        }
    }
}
