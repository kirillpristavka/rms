using DevExpress.Xpo;
using RMS.Core.Model.InfoCustomer;
using System;

namespace RMS.Core.Model.Chronicle
{
    /// <summary>
    /// Хроника изменений статуса клиента.
    /// </summary>
    public class ChronicleCustomerStatus : XPObject
    {
        public ChronicleCustomerStatus() { }
        public ChronicleCustomerStatus(Session session) : base(session) { }

        /// <summary>
        /// Дата ввода.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public DateTime DateCreate { get; set; }

        /// <summary>
        /// Дата начала действия статуса.
        /// </summary>
        [DisplayName("Дата начала действия")]
        public DateTime? Date { get; set; }

        /// <summary>
        /// Статус клиента.
        /// </summary>
        [DisplayName("Статус"), Size(256)]
        public string Status { get; set; }

        /// <summary>
        /// Комментарий.
        /// </summary>
        [DisplayName("Комментарий"), Size(1024)]
        public string Comment { get; set; }

        [DisplayName("Пользователь")]
        public string UserString => User?.ToString();
        /// <summary>
        /// Пользователь внесший изменения.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public User User { get; set; }

        [Association, MemberDesignTimeVisibility(false)]
        public CustomerStatus CustomerStatus { get; set; }
    }
}