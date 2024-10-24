using DevExpress.Xpo;

namespace RMS.Core.Model.Salary
{
    /// <summary>
    /// Выплаты или удержания.
    /// </summary>
    public class PayoutDictionary : XPObject
    {
        public PayoutDictionary() { }
        public PayoutDictionary(Session session) : base(session) { }
        
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