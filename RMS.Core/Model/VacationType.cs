using DevExpress.Xpo;

namespace RMS.Core.Model
{
    /// <summary>
    /// Вид отпуска.
    /// </summary>
    public class VacationType : XPObject
    {
        public VacationType() { }
        public VacationType(Session session) : base(session) { }
        
        [Size(256)]
        [DisplayName("Наименование")]
        public string Name { get; set; }
        
        [Size(1024)]
        [DisplayName("Описание")]
        public string Description { get; set; }
        
        public override string ToString()
        {
            return Name;
        }
    }
}