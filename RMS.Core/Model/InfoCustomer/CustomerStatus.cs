using DevExpress.Xpo;
using RMS.Core.Model.Chronicle;
using System;

namespace RMS.Core.Model.InfoCustomer
{
    /// <summary>
    /// Статусы клиента.
    /// </summary>
    public class CustomerStatus : XPObject
    {
        public CustomerStatus() { }
        public CustomerStatus(Session session) : base(session) { }

        [DisplayName("Статус"), Size(256)]
        public string StatusString => Status?.ToString();

        /// <summary>
        /// Статус.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Status Status { get; set; }

        /// <summary>
        /// Дата начала действия статуса.
        /// </summary>
        [DisplayName("Дата")]
        public DateTime? Date { get; set; }

        /// <summary>
        /// Комментарий.
        /// </summary>
        [DisplayName("Комментарий"), Size(1024)]
        public string Comment { get; set; }

        /// <summary>
        /// Хроника изменений статуса клиента.
        /// </summary>
        [Association, Aggregated, MemberDesignTimeVisibility(false)]
        public XPCollection<ChronicleCustomerStatus> ChronicleCustomerStatus
        {
            get
            {
                return GetCollection<ChronicleCustomerStatus>(nameof(ChronicleCustomerStatus));
            }
        }

        public override string ToString()
        {
            var result = default(string);

            if (Date != null)
            {
                result = $"{Convert.ToDateTime(Date).ToShortDateString()}";
            }

            if (Status != null)
            {
                if (string.IsNullOrWhiteSpace(result))
                {
                    result = $"{Status}";
                }
                else
                {
                    result = $"{Status} с {result}";
                }
            }

            return result;
        }
    }
}
