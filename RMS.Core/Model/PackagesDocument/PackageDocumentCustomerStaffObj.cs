using DevExpress.Xpo;
using RMS.Core.Model.InfoCustomer;

namespace RMS.Core.Model.PackagesDocument
{
    public class PackageDocumentCustomerStaffObj : XPObject
    {
        public PackageDocumentCustomerStaffObj() { }
        public PackageDocumentCustomerStaffObj(Session session) : base(session) { }

        public string Staff => CustomerStaff?.ToString();
        public string StaffDateBirth => CustomerStaff?.DateBirth?.ToShortDateString();

        public CustomerStaff CustomerStaff { get; set; }

        [Association]
        public PackageDocument PackageDocument { get; set; }

        public override string ToString()
        {
            return CustomerStaff?.ToString(); 
        }
    }
}