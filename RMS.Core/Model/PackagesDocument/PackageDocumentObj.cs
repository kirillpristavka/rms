using DevExpress.Xpo;
using System;

namespace RMS.Core.Model.PackagesDocument
{
    /// <summary>
    /// Дополнительные объекты для пакета документов.
    /// </summary>
    public class PackageDocumentObj : XPObject
    {
        public PackageDocumentObj() { }
        public PackageDocumentObj(Session session) : base(session) { }

        /// <summary>
        /// Сканированный документ.
        /// </summary>
        public bool IsScannedDocument { get; set; }

        /// <summary>
        /// Оригинальный документ.
        /// </summary>
        public bool IsOriginalDocument { get; set; }

        public string FileName => File?.FileName;
        public File File { get; set; }

        /// <summary>
        /// Дата получения.
        /// </summary>
        public DateTime? DateReceiving { get; set; }

        /// <summary>
        /// Дата отправления.
        /// </summary>
        public DateTime? DateDeparture { get; set; }
        
        [Association]
        [MemberDesignTimeVisibility(false)]
        public PackageDocument PackageDocument { get; set; }

        public override string ToString()
        {
            return FileName ?? base.ToString();
        }
    }
}