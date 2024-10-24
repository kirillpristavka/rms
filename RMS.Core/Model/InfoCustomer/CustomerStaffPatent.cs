using DevExpress.Xpo;
using System;

namespace RMS.Core.Model.InfoCustomer
{
    public class CustomerStaffPatent : XPObject
    {
        public CustomerStaffPatent() { }
        public CustomerStaffPatent(Session session) : base(session) { }

        [DisplayName("Дата начала")]
        public DateTime DateSince { get; set; }

        [DisplayName("Дата окончания")]
        public DateTime DateTo { get; set; }

        [Association]
        [MemberDesignTimeVisibility(false)]
        public CustomerStaff CustomerStaff { get; set; }
    }
}