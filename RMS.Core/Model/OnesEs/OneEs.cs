using DevExpress.Xpo;

namespace RMS.Core.Model.OnesEs
{
    public class OneEs : XPObject
    {
        public OneEs() { }
        public OneEs(Session session) : base(session) { }

        /// <summary>
        /// 1C Бухгалтерия.
        /// </summary>
        [Aggregated]
        public OneEsBookkeeping OneEsBookkeeping { get; set; }

        /// <summary>
        /// 1C Зарплата.
        /// </summary>
        [Aggregated]
        public OneEsSalary OneEsSalary { get; set; }

        /// <summary>
        /// 1C Иное.
        /// </summary>
        [Aggregated]
        public OneEsOther OneEsOther { get; set; }
    }
}