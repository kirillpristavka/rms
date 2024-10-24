using DevExpress.Xpo;
using RMS.Core.Enumerator;

namespace RMS.Core.Model.InfoCustomer
{
    /// <summary>
    /// Архивные папки клиента.
    /// </summary>
    public class CustomerArchiveFolder : XPObject
    {
        public CustomerArchiveFolder() { }
        public CustomerArchiveFolder(Session session) : base(session) { }

        [DisplayName("Наименование папки"), Size(256)]
        public string ArchiveFolderString => ArchiveFolder?.ToString();

        /// <summary>
        /// Архивная папка.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public ArchiveFolder ArchiveFolder { get; set; }

        /// <summary>
        /// Периодичность.
        /// </summary>
        [DisplayName("Периодичность")]
        public PeriodArchiveFolder PeriodArchiveFolder { get; set; }

        [Association, MemberDesignTimeVisibility(false)]
        public Customer Customer { get; set; }

        public override string ToString()
        {
            return ArchiveFolderString;
        }
    }
}
