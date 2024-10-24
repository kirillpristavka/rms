using DevExpress.Xpo;
using RMS.Core.Extensions;
using System;

namespace RMS.Core.Model.InfoCustomer
{
    /// <summary>
    /// Услуга предоставляемая клиенту.
    /// </summary>
    public class CustomerServiceProvided : XPObject
    {
        public CustomerServiceProvided() { }
        public CustomerServiceProvided(Session session) : base(session) { }

        /// <summary>
        /// Дата предоставления услуги.
        /// </summary>
        [DisplayName("Дата предоставления")]
        public DateTime Date { get; set; } = DateTime.Now.Date;

        /// <summary>
        /// Наименование услуги.
        /// </summary>

        [DisplayName("Наименование"), Size(256)]
        public string Name { get; set; }

        /// <summary>
        /// Услуга из прайса.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public bool IsServicePrice { get; set; } = true;

        [DisplayName("Позиция по прайсу"), Size(256)]
        public string PriceListString => PriceList?.Name;
        /// <summary>
        /// Прайс.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public PriceList PriceList { get; set; }

        /// <summary>
        /// Количество предоставляемых услуг.
        /// </summary>
        [DisplayName("Количество")]
        public decimal? Count { get; set; }

        decimal price;
        /// <summary>
        /// Цена.
        /// </summary>
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

        [DisplayName("Сотрудник"), Size(256)]
        public string StaffString => Staff?.ToString();
        /// <summary>
        /// Сотрудник который предоставил услугу.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Staff Staff { get; set; }

        [MemberDesignTimeVisibility(false)]
        public Customer Customer { get; set; }

        public override string ToString()
        {
            if (IsServicePrice)
            {
                return $"{PriceListString}: {Price}";
            }
            else
            {
                return $"{Name}: {Price}";
            }
        }
    }
}
