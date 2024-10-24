using DevExpress.Xpo;
using RMS.Core.Enumerator;
using RMS.Core.Interface;
using RMS.Core.Model.Chronicle;
using System;

namespace RMS.Core.Model.Taxes
{
    public class Leasing : XPObject, ITax
    {
        public Leasing() { }
        public Leasing(Session session) : base(session) { }

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
        /// Комментарий.
        /// </summary>
        [DisplayName("Комментарий"), Size(1024)]
        public string Comment { get; set; }

        /// <summary>
        /// Хроника изменений налога на алкоголь.
        /// </summary>
        [Association, Aggregated, MemberDesignTimeVisibility(false)]
        public XPCollection<ChronicleLeasing> ChronicleLeasing
        {
            get
            {
                return GetCollection<ChronicleLeasing>(nameof(ChronicleLeasing));
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