using DevExpress.Xpo;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Mail;
using System;

namespace RMS.Core.Model
{
    /// <summary>
    /// Дополнительные услуги.
    /// </summary>
    public class AdditionalServices : XPObject
    {
        public AdditionalServices() { }
        public AdditionalServices(Session session) : base(session) { }
        
        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Year = DateTime.Now.Date.Year;
            Month = (Month)DateTime.Now.Date.Month;
        }

        [DisplayName("Оплата клиента")]
        public bool IsPaid { get; set; }

        [DisplayName("Оплата сотруднику")]
        public bool IsPaidStaff { get; set; }

        [DisplayName("Период")]
        public string Period => $"{Month.GetEnumDescription()} {Year}";

        [MemberDesignTimeVisibility(false)]
        public Month Month { get; set; }

        [MemberDesignTimeVisibility(false)]
        public int Year { get; set; }

        [DisplayName("Клиент")]
        public string CustomerName => Customer?.ToString();

        [MemberDesignTimeVisibility(false)]
        public Customer Customer { get; set; }

        [DisplayName("Описание работы")]
        public string DescriptionString => Letter.ByteToString(Description);
        [MemberDesignTimeVisibility(false)]
        public byte[] Description { get; set; }

        [DisplayName("Процент оплаты сотруднику")]
        [MemberDesignTimeVisibility(false)]
        public decimal PaymentPercentage { get; set; } = 10;
        
        [DisplayName("Сумма сотруднику")]
        public decimal ValueStaff { get; set; }

        [DisplayName("Дата выплаты сотруднику")]
        public DateTime? DatePaid { get; set; }
        
        [DisplayName("Сумма")]
        public decimal Value { get; set; }
                
        [DisplayName("Комментарий")]
        public string CommentString => Letter.ByteToString(Comment);
        [MemberDesignTimeVisibility(false)]
        public byte[] Comment { get; set; }

        [DisplayName("Услуги")]
        public string PriceList
        {
            get
            {
                var result = default(string);

                if (AdditionalServicesObj != null && AdditionalServicesObj.Count > 0)
                {
                    var i = 1;
                    foreach (var item in AdditionalServicesObj)
                    {
                        if (item.PriceList is null)
                        {
                            continue;
                        }

                        result += $"{i}. {item}{Environment.NewLine}";
                        i++;
                    }
                }

                return result?.Trim();
            }
        }

        /// <summary>
        /// Задача по услуге.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Task Task { get; set; }

        [Association]
        [MemberDesignTimeVisibility(false)]
        public Staff Staff { get; set; }

        [Association]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<AdditionalServicesObj> AdditionalServicesObj
        {
            get
            {
                return GetCollection<AdditionalServicesObj>(nameof(AdditionalServicesObj));
            }
        }

        public override string ToString()
        {
            var result = "Дополнительная услуга";

            if (string.IsNullOrWhiteSpace(Period))
            {
                result += $" за {Period}";
            }

            if (Customer != null)
            {
                result += $" по клиенту: {Customer}";
            }

            if (Staff != null)
            {
                result += $" ({Staff})";
            }

            return result;
        }
    }
}