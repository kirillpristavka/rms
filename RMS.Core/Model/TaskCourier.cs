using DevExpress.Xpo;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model.Chronicle;
using RMS.Core.Model.InfoCustomer;
using System;

namespace RMS.Core.Model
{
    /// <summary>
    /// Задача для курьера.
    /// </summary>
    public class TaskCourier : XPObject
    {
        public TaskCourier() { }
        public TaskCourier(Session session) : base(session) { }

        /// <summary>
        /// Дата создания задачи.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public DateTime DateCreate { get; set; }

        [DisplayName("Включен в маршрутный лист")]
        public bool IsUseRouteSheet
        {
            get
            {
                if (RouteSheet != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Создатель задачи.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public User UserCreate { get; set; }

        [DisplayName("Дата выполнения задачи")]
        public DateTime? Date { get; set; }

        [DisplayName("Дата переноса задачи")]
        public DateTime? DateTransfer { get; set; }

        [DisplayName("Ответственный")]
        public string AccountantResponsibleString => AccountantResponsible?.ToString();

        /// <summary>
        /// Ответственный.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Staff AccountantResponsible { get; set; }

        [DisplayName("Клиент")]
        public string CustomerString => Customer?.ToString();

        /// <summary>
        /// Клиент.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Customer Customer { get; set; }

        /// <summary>
        /// Адрес.
        /// </summary>
        [DisplayName("Адрес"), Size(1024)]
        public string Address { get; set; }

        /// <summary>
        /// Цель поездки.
        /// </summary>
        [DisplayName("Цель поездки"), Size(1024)]
        public string PurposeTrip { get; set; }

        [DisplayName("Состояние задачи")]
        public string StatusTaskCourierString => StatusTaskCourier.GetEnumDescription();

        [MemberDesignTimeVisibility(false)]
        public StatusTaskCourier StatusTaskCourier { get; set; }

        [DisplayName("Курьер")]
        public string CourierString => Courier?.ToString();
        /// <summary>
        /// Курьер.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Individual Courier { get; set; }

        private decimal value;
        /// <summary>
        /// Потраченная сумма.
        /// </summary>
        [DisplayName("Потраченная\nсумма\n(нал)")]
        public decimal Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value.GetDecimalRound();
            }
        }

        private decimal valueNonCash;
        /// <summary>
        /// Потраченная сумма.
        /// </summary>
        [DisplayName("Потраченная\nсумм\n(безнал)")]
        public decimal ValueNonCash
        {
            get
            {
                return valueNonCash;
            }
            set
            {
                valueNonCash = value.GetDecimalRound();
            }
        }

        /// <summary>
        /// Маршрутный лист.
        /// </summary>
        [Association, MemberDesignTimeVisibility(false)]
        public RouteSheet RouteSheet { get; set; }

        /// <summary>
        /// Хроника изменений.
        /// </summary>
        [Association, Aggregated, MemberDesignTimeVisibility(false)]
        public XPCollection<ChronicleTaskCourier> ChronicleTaskCouriers
        {
            get
            {
                return GetCollection<ChronicleTaskCourier>(nameof(ChronicleTaskCouriers));
            }
        }

        public override string ToString()
        {
            return $"[{Convert.ToDateTime(Date).ToShortDateString()}] {Courier?.IndividualString} [{StatusTaskCourierString}] {PurposeTrip}";
        }
    }
}
