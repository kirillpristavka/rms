using DevExpress.Xpo;
using RMS.Core.Model.InfoContract;
using System;

namespace RMS.Core.Model
{
    /// <summary>
    /// Дополнительное соглашение.
    /// </summary>
    public class SupplementaryAgreement : XPObject
    {
        public SupplementaryAgreement() { }
        public SupplementaryAgreement(Session session) : base(session) { }

        public int Number { get; set; }

        public DateTime Date { get; set; }

        [DisplayName("Описание"), Size(1024)]
        public string Description { get; set; }

        [Association, MemberDesignTimeVisibility(false)]
        public Contract Contract { get; set; }
    }
}