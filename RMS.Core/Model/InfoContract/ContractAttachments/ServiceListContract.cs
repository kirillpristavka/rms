using DevExpress.Xpo;
using RMS.Core.Extensions;

namespace RMS.Core.Model.InfoContract.ContractAttachments
{
    /// <summary>
    /// Перечень услуг.
    /// </summary>
    public class ServiceListContract : XPObject
    {
        public ServiceListContract() { }
        public ServiceListContract(Session session) : base(session) { }

        [DisplayName("Наименование")]
        public string TypeServiceList => ServiceList?.TypeServiceList.GetEnumDescription();

        [DisplayName("Показатель")]
        public string Mark => ServiceList?.Mark;

        [DisplayName("Сумма")]
        public string Value => ServiceList?.Value.ToString();

        [DisplayName("Комментарий")]
        public string Comment => ServiceList?.Comment;

        [MemberDesignTimeVisibility(false)]
        public ServiceList ServiceList { get; set; }

        [Association, MemberDesignTimeVisibility(false)]
        public ContractAttachment ContractAttachment { get; set; }

        public override string ToString()
        {
            return default;
        }
    }
}
