using DevExpress.Xpo;
using RMS.Core.Enumerator;
using RMS.Core.Interface;
using RMS.Core.Model.Chronicle.Taxes;
using System;

namespace RMS.Core.Model.Taxes
{
    /// <summary>
    /// Налог на имущество.
    /// </summary>
    public class TaxProperty : XPObject, ITax
    {
        public TaxProperty() { }
        public TaxProperty(Session session) : base(session) { }

        /// <summary>
        /// Используется или нет.
        /// </summary>
        [DisplayName("Есть/нет"), Persistent]
        public bool IsUse
        {
            get
            {
                if (Availability is null || Availability == Enumerator.Availability.False)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Наличие.
        /// </summary>
        [DisplayName("Наличие")]
        public Availability? Availability { get; set; }

        /// <summary>
        /// Дата ввода.
        /// </summary>
        [DisplayName("Дата")]
        public DateTime? Date { get; set; }

        /// <summary>
        /// Общая налоговая ставка.
        /// </summary>
        [DisplayName("Общая налоговая ставка")]
        public decimal TotalRate { get; set; }

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

        /// <summary>
        /// Налог уменьшен.
        /// </summary>
        [DisplayName("Налог уменьшен"), MemberDesignTimeVisibility(false)]
        public bool IsTaxReduced { get; set; }

        /// <summary>
        /// Процент, на который уменьшен налог.
        /// </summary>
        [DisplayName("Уменьшен на")]
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

        /// <summary>
        /// Хроника изменений налога на имущество.
        /// </summary>
        [Association, Aggregated, MemberDesignTimeVisibility(false)]
        public XPCollection<ChronicleTaxProperty> ChronicleTaxProperty
        {
            get
            {
                return GetCollection<ChronicleTaxProperty>(nameof(ChronicleTaxProperty));
            }
        }

        public override string ToString()
        {
            if (IsUse)
            {
                return $"Есть с {Convert.ToDateTime(Date).ToShortDateString()}";
            }
            else
            {
                if (Date is null)
                {
                    return "-";
                }
                else
                {
                    return $"Нет с {Convert.ToDateTime(Date).ToShortDateString()}";
                }
            }
        }
    }
}
