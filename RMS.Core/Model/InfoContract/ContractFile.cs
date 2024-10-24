using DevExpress.Xpo;
using System;

namespace RMS.Core.Model.InfoContract
{
    public class ContractFile : XPObject
    {
        public ContractFile() { }
        public ContractFile(Session session) : base(session) { }

        [DisplayName("Дата создания")]
        public DateTime? DateCreate { get; set; }

        [DisplayName("Наименование")]
        public string FileName => File?.FileName;

        [Aggregated]
        [MemberDesignTimeVisibility(false)]
        public File File { get; set; }

        [Association]
        [MemberDesignTimeVisibility(false)]
        public Contract Contract { get; set; }
    }
}