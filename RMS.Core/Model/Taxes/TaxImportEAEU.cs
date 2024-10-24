using DevExpress.Xpo;
using RMS.Core.Enumerator;
using RMS.Core.Interface;
using RMS.Core.Model.Chronicle.Taxes;
using System;

namespace RMS.Core.Model.Taxes
{
    /// <summary>
    /// Импорт ЕАЭС.
    /// </summary>
    public class TaxImportEAEU : XPObject, ITax
    {
        public TaxImportEAEU() { }
        public TaxImportEAEU(Session session) : base(session) { }

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
        /// Дата ввода.
        /// </summary>
        [DisplayName("Дата")]
        public DateTime? Date { get; set; }

        /// <summary>
        /// Комментарий.
        /// </summary>
        [DisplayName("Комментарий"), Size(1024)]
        public string Comment { get; set; }

        /// <summary>
        /// Наличие.
        /// </summary>
        [DisplayName("Наличие")]
        public Availability? Availability { get; set; }

        /// <summary>
        /// Хроника.
        /// </summary>
        [Aggregated]
        [Association]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<ChronicleTaxImportEAEU> ChronicleTaxImportEAEU
        {
            get
            {
                return GetCollection<ChronicleTaxImportEAEU>(nameof(ChronicleTaxImportEAEU));
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
