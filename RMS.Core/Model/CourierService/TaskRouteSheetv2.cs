using DevExpress.Xpo;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model.InfoCustomer;
using System;

namespace RMS.Core.Model.CourierService
{
    /// <summary>
    /// Задача для маршрутного листа.
    /// </summary>
    public class TaskRouteSheetv2 : XPObject
    {
        public TaskRouteSheetv2() { }
        public TaskRouteSheetv2(Session session) : base(session) { }

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
                if (RouteSheetv2 != null)
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

        [DisplayName("Курьер")]
        public string CourierString => Courier?.ToString();
        /// <summary>
        /// Курьер.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Individual Courier { get; set; }

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
        [Association]
        [MemberDesignTimeVisibility(false)]
        public RouteSheetv2 RouteSheetv2 { get; set; }

        /// <summary>
        /// Хроника изменений.
        /// </summary>
        [Aggregated]
        [Association]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<ChronicleTaskRouteSheetv2> ChronicleTaskRouteSheetv2
        {
            get
            {
                return GetCollection<ChronicleTaskRouteSheetv2>(nameof(ChronicleTaskRouteSheetv2));
            }
        }

        public override string ToString()
        {
            return $"[{Convert.ToDateTime(Date).ToShortDateString()}] [{StatusTaskCourierString}] {PurposeTrip}";
        }
    }
}
