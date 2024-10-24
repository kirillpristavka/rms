using DevExpress.Xpo;
using RMS.Core.Extensions;
using RMS.Core.Model.InfoCustomer;

namespace RMS.Core.Model
{
    /// <summary>
    /// Информация по счету.
    /// </summary>
    public class InvoiceInformation : XPObject
    {
        public InvoiceInformation() { }
        public InvoiceInformation(Session session) : base(session) { }

        [DisplayName("Наименование")]
        [Size(512)]
        public string Name { get; set; }

        [DisplayName("Количество")]
        public int Count { get; set; }

        [DisplayName("Единицы измерения")]
        public string Unit { get; set; }

        private decimal price;
        [DisplayName("Цена")]
        public decimal Price 
        {
            get
            {
                return price;
            }
            set
            {
                price = value.GetDecimalRound();
            }
        }

        private decimal sum;
        [DisplayName("Сумма")]
        public decimal Sum 
        {
            get
            {
                return sum;
            }
            set
            {
                sum = value.GetDecimalRound();
            }
        }

        [DisplayName("Описание")]
        [Size(1024)]
        public string Description { get; set; }

        [DisplayName("Комментарий")]
        [Size(1024)]
        public string Comment { get; set; }

        [MemberDesignTimeVisibility(false)]
        public CustomerPerformanceIndicator CustomerPerformanceIndicator { get; set; }

        [MemberDesignTimeVisibility(false)]
        public CustomerServiceProvided CustomerServiceProvided { get; set; }

        [Association, MemberDesignTimeVisibility(false)]
        public Invoice Invoice { get; set; }
    }
}