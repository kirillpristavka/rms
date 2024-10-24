using DevExpress.Xpo;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model.InfoContract;
using System;

namespace RMS.Core.Model.Chronicle
{
    public class ChronicleContract : XPObject
    {
        public ChronicleContract() { }
        public ChronicleContract(Session session) : base(session) { }

        /// <summary>
        /// Отправлено уведомление.
        /// </summary>
        [DisplayName("Уведомление")]
        public bool IsNotificationSent { get; set; }

        /// <summary>
        /// Дата хроники.
        /// </summary>
        [DisplayName("Дата")]
        [System.ComponentModel.DataAnnotations.DisplayFormat(DataFormatString = "{0:dd/MM/yyyy (HH:mm:ss)}")]
        public DateTime Date { get; set; }

        [DisplayName("Пользователь")]
        public string UserString => User?.ToString();

        /// <summary>
        /// Описание произошедшего события
        /// </summary>
        [DisplayName("Описание"), Size(1024)]
        public string Description { get; set; }

        /// <summary>
        /// Дата с.
        /// </summary>
        [DisplayName("Дата с")]
        public DateTime? DateSince { get; set; }

        /// <summary>
        /// Дата по.
        /// </summary>
        [DisplayName("Дата по")]
        public DateTime? DateTo { get; set; }

        [DisplayName("Статус")]
        public string ContractStatusString => ContractStatus?.ToString();
        /// <summary>
        /// Событие.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public ContractStatus ContractStatus { get; set; }

        /// <summary>
        /// Пользователь внесший изменения.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public User User { get; set; }

        [Association, MemberDesignTimeVisibility(false)]
        public Contract Contract { get; set; }
    }
}
