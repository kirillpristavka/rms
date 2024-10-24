using DevExpress.Xpo;

namespace RMS.Core.Model
{
    /// <summary>
    /// Виды выплат.
    /// </summary>
    public class TypePayment : XPObject
    {
        public TypePayment() { }
        public TypePayment(Session session) : base(session) { }

        [DisplayName("Наименование"), Size(256)]
        public string Name { get; set; }

        [DisplayName("Описание"), Size(1024)]
        public string Description { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}