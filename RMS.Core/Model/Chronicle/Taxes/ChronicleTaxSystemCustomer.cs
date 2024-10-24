using DevExpress.Xpo;
using System;

namespace RMS.Core.Model.Chronicle.Taxes
{
    /// <summary>
    /// Хроника изменений налога на алкоголь.
    /// </summary>
    public class ChronicleTaxSystemCustomer : XPObject
    {
        public ChronicleTaxSystemCustomer() { }
        public ChronicleTaxSystemCustomer(Session session) : base(session) { }

        /// <summary>
        /// Дата ввода.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public DateTime DateCreate { get; set; }

        /// <summary>
        /// Дата ввода.
        /// </summary>
        [DisplayName("Дата")]
        public DateTime? Date { get; set; }

        /// <summary>
        /// Система налогообложения.
        /// </summary>
        [DisplayName("Система налогообложения"), Size(256)]
        public string TaxSystem { get; set; }

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
    }
}