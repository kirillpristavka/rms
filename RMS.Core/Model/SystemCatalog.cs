using DevExpress.Xpo;

namespace RMS.Core.Model
{
    /// <summary>
    /// Системные каталоги.
    /// </summary>
    public class SystemCatalog : XPObject
    {
        public SystemCatalog() { }
        public SystemCatalog(Session session) : base(session) { }
        
        /// <summary>
        /// Наименование.
        /// </summary>
        [DisplayName("Наименование")]
        public string DisplayName { get; set; }
        
        /// <summary>
        /// Родительский каталог.
        /// </summary>
        [DisplayName("Родительский каталог")]
        [MemberDesignTimeVisibility(false)]
        public SystemCatalog ParentCatalog { get; set; }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
