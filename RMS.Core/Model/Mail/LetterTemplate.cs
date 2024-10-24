using DevExpress.Xpo;

namespace RMS.Core.Model.Mail
{
    public class LetterTemplate : XPObject
    {
        public LetterTemplate() { }
        public LetterTemplate(Session session) : base(session) { }

        /// <summary>
        /// Наименование.
        /// </summary>
        [DisplayName("Наименование"), Size(256)]
        public string Name { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        [DisplayName("Описание"), Size(1024)]
        public string Description { get; set; }

        /// <summary>
        /// Текст письма.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public byte[] TextBody { get; set; }

        // <summary>
        /// Текст письма HTML.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public byte[] HtmlBody { get; set; }

        /// <summary>
        /// Используется для шаблона поздравлений.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public bool IsUseCongratulationsTemplate { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
