using DevExpress.Xpo;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model.InfoCustomer;
using System;

namespace RMS.Core.Model.Chronicle
{
    public class ChronicleCustomer : XPObject
    {
        public ChronicleCustomer() { }
        public ChronicleCustomer(Session session) : base(session) { }

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

        [DisplayName("Событие")]
        public string ActString => Act.GetEnumDescription();
        /// <summary>
        /// Событие.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Act Act { get; set; }

        /// <summary>
        /// Пользователь внесший изменения.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public User User { get; set; }

        [Association, MemberDesignTimeVisibility(false)]
        public Customer Customer { get; set; }
    }
}
