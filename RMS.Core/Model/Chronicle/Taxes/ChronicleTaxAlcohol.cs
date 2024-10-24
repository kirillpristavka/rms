using DevExpress.Xpo;
using RMS.Core.Enumerator;
using RMS.Core.Interface;
using RMS.Core.Model.Taxes;
using System;

namespace RMS.Core.Model.Chronicle.Taxes
{
    /// <summary>
    /// Хроника изменений налога на алкоголь.
    /// </summary>
    public class ChronicleTaxAlcohol : XPObject, ITaxChronicle
    {
        public ChronicleTaxAlcohol() { }
        public ChronicleTaxAlcohol(Session session) : base(session) { }

        /// <summary>
        /// Дата ввода.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public DateTime DateCreate { get; set; }

        /// <summary>
        /// Используется или нет.
        /// </summary>
        [DisplayName("Есть/нет")]
        public bool IsUse { get; set; }

        /// <summary>
        /// Дата ввода.
        /// </summary>
        [DisplayName("Дата")]
        public DateTime? Date { get; set; }

        /// <summary>
        /// Наличие.
        /// </summary>
        [DisplayName("Наличие")]
        public Availability? Availability { get; set; }

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
        public TaxAlcohol TaxAlcohol { get; set; }
    }
}