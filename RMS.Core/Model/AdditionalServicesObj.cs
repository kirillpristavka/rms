using DevExpress.Xpo;

namespace RMS.Core.Model
{
    public class AdditionalServicesObj : XPObject
    {

        public AdditionalServicesObj() { }
        public AdditionalServicesObj(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            count = 1;
        }
        
        [DisplayName("Наименование")]
        public string Name => PriceList?.ToString();

        private int count;
        [DisplayName("Количество")]
        public int Count 
        {
            get 
            {
                return count;
            }

            set
            {
                if (value <= 0)
                {
                    count = 1;
                }
                else
                {
                    count = value;
                }
            }
        }

        public decimal GetValue()
        {
            if (PriceList != null)
            {
                return count * PriceList.Price;
            }
            
            return default;
        }
        
        [DisplayName("Цена")]
        public decimal Value { get; set; }

        private PriceList priceList;
        [MemberDesignTimeVisibility(false)]
        public PriceList PriceList
        {
            get
            {
                return priceList;
            }
            set
            {
                priceList = value;
            }
        }

        [Association]
        [MemberDesignTimeVisibility(false)]
        public AdditionalServices AdditionalServices { get; set; }

        public override string ToString()
        {
            if (PriceList != null)
            {
                return $"{Name} - {Value}";
            }

            return base.ToString();
        }
    }
}