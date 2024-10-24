using DevExpress.Xpo;
using RMS.Core.Model.InfoCustomer;
using System;

namespace RMS.Core.Model
{
    /// <summary>
    /// Внешнеэкономическая деятельность.
    /// </summary>
    public class ForeignEconomicActivity : XPObject
    {
        public ForeignEconomicActivity() { }
        public ForeignEconomicActivity(Session session) : base(session) { }

        /// <summary>
        /// Дата начала.
        /// </summary>
        [DisplayName("Дата с")]
        public DateTime? DateSince { get; set; }
        
        /// <summary>
        /// Дата по.
        /// </summary>
        [DisplayName("Дата по")]
        public DateTime? DateTo { get; set; }

        /// <summary>
        /// Действие (Экспорт/импорт).
        /// </summary>
        [Size(1024)]
        [DisplayName("Экспорт/импорт")]
        public string Act { get; set; }

        /// <summary>
        /// Куда/откуда.
        /// </summary>
        [Size(1024)]
        [DisplayName("Куда/откуда")]
        public string WhereToOrFrom { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        [Size(1024)]
        [DisplayName("Описание")]
        public string Description { get; set; }

        [Association]
        [MemberDesignTimeVisibility(false)]
        public Customer Customer { get; set; }

        public override string ToString()
        {
            return $"Запись № {Oid}";
        }
    }
}
