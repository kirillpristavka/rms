using DevExpress.Xpo;
using RMS.Core.Enumerator;

namespace RMS.Core.Model.InfoContract.ContractAttachments
{
    /// <summary>
    /// Услуга или операция из списка.
    /// </summary>
    public class ServiceList : XPObject
    {
        public ServiceList() { }
        public ServiceList(Session session) : base(session) { }

        [DisplayName("Наименование")]
        public TypeServiceList TypeServiceList { get; set; }

        [DisplayName("Показатель"), Size(1024)]
        public string Mark { get; set; }

        [DisplayName("Сумма")]
        public decimal Value { get; set; }

        [DisplayName("Комментарий"), Size(1024)]
        public string Comment { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
