using DevExpress.Xpo;
using RMS.Core.Extensions;
using System;

namespace RMS.Core.Model.CourierService
{
    /// <summary>
    /// Маршрутный лист.
    /// </summary>
    public class RouteSheetv2 : XPObject
    {
        public RouteSheetv2() { }
        public RouteSheetv2(Session session) : base(session) { }
        
        /// <summary>
        /// Дата маршрутного листа.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public DateTime DateCreate { get; set; }

        /// <summary>
        /// Создатель маршрутного листа.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public User UserCreate { get; set; }

        /// <summary>
        /// Дата изменения.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public DateTime DateUpdate { get; set; }

        /// <summary>
        /// Пользователь внесший изменения.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public User UserUpdate { get; set; }

        [DisplayName("Закрыт")]
        public bool IsClosed { get; set; }

        [DisplayName("Дата")]
        public DateTime Date { get; set; } = DateTime.Now;
        
        private decimal value;
        /// <summary>
        /// Стоимость.
        /// </summary>
        [DisplayName("Внесено в кассу")]
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
        
        [Size(1024)]
        [DisplayName("Примечание")]
        public string Comment { get; set; }

        /// <summary>
        /// Список задач для курьера.
        /// </summary>
        [Association]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<TaskRouteSheetv2> TaskRouteSheetv2
        {
            get
            {
                return GetCollection<TaskRouteSheetv2>(nameof(TaskRouteSheetv2));
            }
        }

        /// <summary>
        /// Платежи по маршрутному листу.
        /// </summary>
        [Association]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<RouteSheetPaymentv2> Payments
        {
            get
            {
                return GetCollection<RouteSheetPaymentv2>(nameof(Payments));
            }
        }

        public override string ToString()
        {
            return $"Маршрутный лист от {Date.ToShortDateString()}";
        }
    }
}
