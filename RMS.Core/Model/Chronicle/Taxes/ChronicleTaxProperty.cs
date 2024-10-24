using DevExpress.Xpo;
using RMS.Core.Enumerator;
using RMS.Core.Interface;
using RMS.Core.Model.Taxes;
using System;

namespace RMS.Core.Model.Chronicle.Taxes
{
    /// <summary>
    /// Хроника изменений налога на имущество.
    /// </summary>
    public class ChronicleTaxProperty : XPObject, ITaxChronicle
    {
        public ChronicleTaxProperty() { }
        public ChronicleTaxProperty(Session session) : base(session) { }

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

        [DisplayName("Общая налоговая ставка")]
        public string TotalRateString => $"{TotalRate * 100}%";
        /// <summary>
        /// Общая налоговая ставка.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public decimal TotalRate { get; set; }

        /// <summary>
        /// Наличие.
        /// </summary>
        [DisplayName("Наличие")]
        public Availability? Availability { get; set; }

        /// <summary>
        /// Это сниженная ставка
        /// </summary>
        [DisplayName("Сниженная ставка")]
        public bool IsReducedRate { get; set; }

        /// <summary>
        /// Все имущество освобождено от налога, необходим код льготы.
        /// </summary>
        [DisplayName("Освобождено от налога")]
        public bool IsPropertyExemptFromTax { get; set; }

        [DisplayName("Код льготы")]
        public string PrivilegeString => Privilege?.ToString();
        /// <summary>
        /// Льгота.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Privilege Privilege { get; set; }

        [DisplayName("Налог уменьшен на")]
        public string ReducedByString
        {
            get
            {
                if (IsTaxReduced)
                {
                    return $"{ReducedBy * 100}%";
                }
                else
                {
                    return "-";
                }
            }
        }

        /// <summary>
        /// Налог уменьшен.
        /// </summary>
        [DisplayName("Налог уменьшен"), MemberDesignTimeVisibility(false)]
        public bool IsTaxReduced { get; set; }

        /// <summary>
        /// Процент, на который уменьшен налог.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public decimal ReducedBy { get; set; }

        /// <summary>
        /// Заполнение второго раздела при наличие одной льготы.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public bool IsFillingSecondSectionWithOnePrivilege { get; set; }

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
        public TaxProperty TaxProperty { get; set; }
    }
}
