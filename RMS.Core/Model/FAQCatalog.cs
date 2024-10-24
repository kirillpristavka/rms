using DevExpress.Xpo;

namespace RMS.Core.Model
{
    /// <summary>
    /// Каталоги для FAQ.
    /// </summary>
    public class FAQCatalog : SystemCatalog
    {
        public FAQCatalog() { }
        public FAQCatalog(Session session) : base(session) { }
                
        public override string ToString()
        {
            return DisplayName;
        }
    }
}
