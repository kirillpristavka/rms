using DevExpress.Xpo;
using RMS.Core.Model.InfoCustomer;
using System;

namespace RMS.Core.Model
{
    /// <summary>
    /// Подразделение.
    /// </summary>
    public class Subdivision : XPObject
    {
        public Subdivision() { }
        public Subdivision(Session session) : base(session) { }

        /// <summary>
        /// Дата ввода подразделения.
        /// </summary>
        [DisplayName("Дата ввода")]
        public DateTime? InputDate { get; set; }

        [DisplayName("Наименование"), Size(256)]
        public string Name { get; set; }

        [DisplayName("Полное наименование"), MemberDesignTimeVisibility(false), Size(1024)]
        public string FullName { get; set; }

        [DisplayName("Сокращенное наименование"), MemberDesignTimeVisibility(false), Size(256)]
        public string AbbreviatedName { get; set; }

        /// <summary>
        /// ОКТМО.
        /// </summary>
        [DisplayName("ОКТМО"), MemberDesignTimeVisibility(false), Size(128)]
        public string OKTMO { get; set; }

        /// <summary>
        /// КПП.
        /// </summary>
        [DisplayName("КПП"), MemberDesignTimeVisibility(false), Size(32)]
        public string KPP { get; set; }

        /// <summary>
        /// ИФНС.
        /// </summary>
        [DisplayName("ИФНС"), MemberDesignTimeVisibility(false), Size(128)]
        public string IFNS { get; set; }

        /// <summary>
        /// Обособленное подразделение.
        /// </summary>
        [DisplayName("Обособленное подразделение"), MemberDesignTimeVisibility(false)]
        public bool IsSeparateDivision { get; set; }

        [Association, MemberDesignTimeVisibility(false)]
        public Customer Customer { get; set; }

        public override string ToString()
        {
            if (!string.IsNullOrWhiteSpace(Name))
            {
                return Name;
            }

            return $"Запись № {Oid}";
        }
    }
}
