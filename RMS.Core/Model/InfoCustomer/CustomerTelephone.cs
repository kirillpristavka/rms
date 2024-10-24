using DevExpress.Xpo;

namespace RMS.Core.Model.InfoCustomer
{
    /// <summary>
    /// Телефоны клиента.
    /// </summary>
    public class CustomerTelephone : CustomerContact
    {
        public CustomerTelephone() { }
        public CustomerTelephone(Session session) : base(session) { }

        [DisplayName("Телефон"), Size(32)]
        public string Telephone { get; set; }

        /// <summary>
        /// Используется по умолчанию.
        /// </summary>
        [DisplayName("По умолчанию")]
        [MemberDesignTimeVisibility(false)]
        public bool IsDefault { get; set; }

        [Association]
        [MemberDesignTimeVisibility(false)]
        public Customer Customer { get; set; }

        public override string ToString()
        {
            return Telephone;
        }
    }
}
