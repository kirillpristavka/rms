using DevExpress.Xpo;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model.InfoCustomer;
using System;

namespace RMS.Core.Model.Chronicle
{
    public class ChronicleTaskCourier : XPObject
    {
        public ChronicleTaskCourier() { }
        public ChronicleTaskCourier(Session session) : base(session) { }

        /// <summary>
        /// Дата изменения.
        /// </summary>
        [DisplayName("Дата изменения")]
        [System.ComponentModel.DataAnnotations.DisplayFormat(DataFormatString = "{0:dd/MM/yyyy (HH:mm:ss)}")]
        public DateTime DateUpdate { get; set; }

        [DisplayName("Пользователь")]
        public string UserUpdateString => UserUpdate?.ToString();
        /// <summary>
        /// Пользователь внесший изменения.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public User UserUpdate { get; set; }

        [DisplayName("Дата")]
        public DateTime? Date { get; set; }

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


        [DisplayName("Статус")]
        public string StatusTaskCourierString => StatusTaskCourier?.GetEnumDescription();

        [MemberDesignTimeVisibility(false)]
        public StatusTaskCourier? StatusTaskCourier { get; set; }
        
        [DisplayName("Курьер")]
        public string CourierString => Courier?.ToString();
        /// <summary>
        /// Курьер.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Individual Courier { get; set; }

        /// <summary>
        /// Потраченная сумма.
        /// </summary>
        [DisplayName("Потраченная сумма (нал)")]
        public decimal Value { get; set; }

        /// <summary>
        /// Потраченная сумма.
        /// </summary>
        [DisplayName("Потраченная сумма (безнал)")]
        public decimal ValueNonCash { get; set; }

        /// <summary>
        /// Маршрутный лист.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public RouteSheet RouteSheet { get; set; }

        [Association, MemberDesignTimeVisibility(false)]
        public TaskCourier TaskCourier { get; set; }
    }
}
