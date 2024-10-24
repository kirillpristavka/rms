using DevExpress.Xpo;
using RMS.Core.Model.InfoCustomer;

namespace RMS.Core.Model
{
    /// <summary>
    /// Адрес.
    /// </summary>
    public class CustomerAddress : Address
    {
        public CustomerAddress() { }
        public CustomerAddress(Session session) : base(session) { }
                
        [Association, MemberDesignTimeVisibility(false)]
        public Customer Customer { get; set; }

        public override string ToString()
        {
            return AddressString;
        }              
    }
}