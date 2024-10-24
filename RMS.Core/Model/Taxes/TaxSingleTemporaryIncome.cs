using DevExpress.Xpo;
using RMS.Core.Enumerator;
using RMS.Core.Interface;
using RMS.Core.Model.Chronicle.Taxes;
using RMS.Core.Model.InfoCustomer;
using System;

namespace RMS.Core.Model.Taxes
{
    /// <summary>
    /// Единый налог на временный доход (ЕНВД).
    /// </summary>
    public class TaxSingleTemporaryIncome : XPObject, ITax
    {
        public TaxSingleTemporaryIncome() { }
        public TaxSingleTemporaryIncome(Session session) : base(session) { }

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

        [DisplayName("Код ЕНВД")]
        public string EntrepreneurialActivityCodesUTIIString
        {
            get
            {
                var result = default(string);

                foreach (var item in CustomerEntrepreneurialActivityCodesUTII)
                {
                    result += $"[{item.EntrepreneurialActivityCodesUTII}] ";
                }
                
                return result.Trim();
            }
        }

        /// <summary>
        /// Физический показатель.
        /// </summary>
        [DisplayName("Физический показатель")]
        public string PhysicalIndicator { get; set; }

        /// <summary>
        /// Коды предпринимательской деятельности ЕНВД.
        /// </summary>
        [Association, Aggregated, MemberDesignTimeVisibility(false)]
        public XPCollection<CustomerEntrepreneurialActivityCodesUTII> CustomerEntrepreneurialActivityCodesUTII
        {
            get
            {
                return GetCollection<CustomerEntrepreneurialActivityCodesUTII>(nameof(CustomerEntrepreneurialActivityCodesUTII));
            }
        }

        /// <summary>
        /// Хроника изменений ЕНВД.
        /// </summary>
        [Association, Aggregated, MemberDesignTimeVisibility(false)]
        public XPCollection<ChronicleTaxSingleTemporaryIncome> ChronicleTaxSingleTemporaryIncome
        {
            get
            {
                return GetCollection<ChronicleTaxSingleTemporaryIncome>(nameof(ChronicleTaxSingleTemporaryIncome));
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
