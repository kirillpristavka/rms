using DevExpress.Xpo;

namespace RMS.Core.Model.Mail
{
    /// <summary>
    /// Фильтр письма.
    /// </summary>
    public class LetterFilter : XPObject
    {
        public LetterFilter() { }
        public LetterFilter(Session session) : base(session) { }

        /// <summary>
        /// Почтовый ящик.
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Каталог
        /// </summary>
        public LetterCatalog LetterCatalog { get; set; }

        public override string ToString()
        {
            return Email;
        }
    }
}
