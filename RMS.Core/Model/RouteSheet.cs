using DevExpress.Xpo;
using RMS.Core.Extensions;
using System;
using System.Linq;

namespace RMS.Core.Model
{
    /// <summary>
    /// Маршрутный лист.
    /// </summary>
    public class RouteSheet : XPObject
    {
        public RouteSheet() { }
        public RouteSheet(Session session) : base(session) { }
        
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

        [DisplayName("Курьер")]
        public string IndividualString => Courier?.ToString();

        [MemberDesignTimeVisibility(false)]
        public Individual Courier { get; set; }
        
        /// <summary>
        /// Остаток.
        /// </summary>
        [DisplayName("Остаток на начала дня")]
        public decimal Remainder
        {
            get
            {
                if (Courier != null)
                {
                    var routeSheets = new XPQuery<RouteSheet>(Session)
                        .Where(w => w.Date < Date && w.Courier != null && w.Courier.Oid == Courier.Oid)
                        .ToList();

                    if (routeSheets != null && routeSheets.Count > 0)
                    {
                        var maxDate = routeSheets.Max(m => m.Date);
                        return routeSheets.LastOrDefault(l => l.Date == maxDate)?.Balance ?? default;
                    }
                }

                return default;
            }
        }

        private decimal value;
        /// <summary>
        /// Стоимость.
        /// </summary>
        [DisplayName("Выданная сумма")]
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

        /// <summary>
        /// Потрачено.
        /// </summary>
        [DisplayName("Потрачено")]
        public decimal Spent
        {
            get
            {
                var result = 0.00M;

                if (Payments != null && Payments.Count > 0)
                {
                    var sumPayments = Payments.Sum(s => s.Value);
                    result += sumPayments.GetDecimalRound();
                }

                if (TasksCourier != null && TasksCourier.Count > 0)
                {
                    var sumPayments = TasksCourier.Sum(s => s.Value);
                    result += sumPayments.GetDecimalRound();
                }

                return result;
            }
        }


        /// <summary>
        /// Остаток средств.
        /// </summary>
        [DisplayName("Остаток на конец дня")]
        public decimal Balance
        {
            get
            {
                return (Remainder + Value - Spent).GetDecimalRound();
            }
        }
        
        [Size(1024)]
        [DisplayName("Примечание")]
        public string Comment { get; set; }

        /// <summary>
        /// Список задач для курьера.
        /// </summary>
        [Association, MemberDesignTimeVisibility(false)]
        public XPCollection<TaskCourier> TasksCourier
        {
            get
            {
                return GetCollection<TaskCourier>(nameof(TasksCourier));
            }
        }

        /// <summary>
        /// Платежи по маршрутному листу.
        /// </summary>
        [Association, MemberDesignTimeVisibility(false)]
        public XPCollection<RouteSheetPayment> Payments
        {
            get
            {
                return GetCollection<RouteSheetPayment>(nameof(Payments));
            }
        }

        public override string ToString()
        {
            return $"[{Date.ToShortDateString()}] {IndividualString}";
        }
    }
}
