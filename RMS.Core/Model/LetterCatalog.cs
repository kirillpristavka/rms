using DevExpress.Xpo;
using RMS.Core.Model.Mail;

namespace RMS.Core.Model
{
    /// <summary>
    /// Каталоги для писем.
    /// </summary>
    public class LetterCatalog : SystemCatalog
    {
        public LetterCatalog() { }
        public LetterCatalog(Session session) : base(session) { }

        protected async override void OnLoaded()
        {
            base.OnLoaded();

            await GetQuantity();
        }
        
        protected async override void OnSaved()
        {
            base.OnSaving();

            await GetQuantity();
        }

        /// <summary>
        /// Ответственный на папке.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Staff Staff { get; set; }

        /// <summary>
        /// Не прочитано.
        /// </summary>
        [DisplayName("Не прочитано")]
        public int? NotReadLettersCount { get; private set; }

        /// <summary>
        /// Общее количество.
        /// </summary>
        [DisplayName("Всего")]
        public int? LettersCount { get; private set; }

        public async System.Threading.Tasks.Task GetQuantity()
        {
            NotReadLettersCount = await new XPQuery<Letter>(Session)
                ?.CountAsync(c =>
                    c.LetterCatalog != null
                    && c.LetterCatalog.Oid == Oid
                    && c.IsRead == false);

            LettersCount = await new XPQuery<Letter>(Session)
                ?.CountAsync(c =>
                    c.LetterCatalog != null
                    && c.LetterCatalog.Oid == Oid);
        }
        
        [Delayed]
        [Association]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<Letter> Letters
        {
            get
            {
                return GetCollection<Letter>(nameof(Letters));
            }
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
