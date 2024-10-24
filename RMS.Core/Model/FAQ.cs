using DevExpress.Xpo;
using RMS.Core.Enumerator;

namespace RMS.Core.Model
{
    /// <summary>
    /// Вопросы и ответы.
    /// </summary>
    public class FAQ : XPObject
    {
        public FAQ() { }
        public FAQ(Session session) : base(session) { }
        
        [DisplayName("Вопрос")]
        public string Question { get; set; }

        [DisplayName("Блокировка")]
        public bool IsBlock { get; set; }

        [DisplayName("Ответ")]
        [MemberDesignTimeVisibility(false)]
        public byte[] Answer { get; set; }

        [MemberDesignTimeVisibility(false)]
        public FAQCatalog FAQCatalog { get; set; }

        /// <summary>
        /// Рабочая зона.
        /// </summary>
        [DisplayName("Рабочий модуль")]
        public WorkZone? WorkZone { get; set; }

        public override string ToString()
        {
            return Question;
        }
    }
}
