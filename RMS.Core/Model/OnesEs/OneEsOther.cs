using DevExpress.Xpo;
using RMS.Core.Enumerator;
using RMS.Core.Interface;
using RMS.Core.Model.Chronicle.OnesEs;
using RMS.Core.Model.Chronicle.Taxes;
using System;

namespace RMS.Core.Model.OnesEs
{    
    /// <summary>
     /// 1C Иное.
     /// </summary>
    public class OneEsOther : XPObject, IOneEs
    {
        public OneEsOther() { }
        public OneEsOther(Session session) : base(session) { }

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
        /// Путь.
        /// </summary>
        [DisplayName("Путь"), Size(1024)]
        public string Path { get; set; }

        /// <summary>
        /// Наличие.
        /// </summary>
        [DisplayName("Наличие")]
        public Availability? Availability { get; set; }

        /// <summary>
        /// Хроника изменений налога на алкоголь.
        /// </summary>
        [Association, Aggregated, MemberDesignTimeVisibility(false)]
        public XPCollection<ChronicleOneEsOther> ChronicleOneEsOther
        {
            get
            {
                return GetCollection<ChronicleOneEsOther>(nameof(ChronicleOneEsOther));
            }
        }

        public override string ToString()
        {
            if (IsUse)
            {
                if (string.IsNullOrWhiteSpace(Path))
                {
                    return $"Не указан путь";
                }
                else
                {
                    return $"{Path}";
                }
            }
            else
            {
                return $"Нет";
            }
        }
    }
}