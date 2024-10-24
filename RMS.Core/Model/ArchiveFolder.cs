using DevExpress.Xpo;
using RMS.Core.Enumerator;

namespace RMS.Core.Model
{
    /// <summary>
    /// Архивные папки.
    /// </summary>
    public class ArchiveFolder : XPObject
    {
        public ArchiveFolder() { }
        public ArchiveFolder(Session session) : base(session) { }

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
        /// Стандартный период.
        /// </summary>
        public PeriodArchiveFolder PeriodArchiveFolder { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
