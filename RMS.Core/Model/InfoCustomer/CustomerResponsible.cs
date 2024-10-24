using DevExpress.Xpo;
using RMS.Core.Enumerator;
using System;
using System.Linq;

namespace RMS.Core.Model.InfoCustomer
{
    /// <summary>
    /// Ответственные сотрудники.
    /// </summary>
    public class CustomerResponsible : XPObject
    {
        public CustomerResponsible() { }
        public CustomerResponsible(Session session) : base(session) { }

        public Staff GetAccountantResponsible => GetCustomerResponsibleObject(ResponsibleOption.AccountantResponsible);
        public Staff GetBankResponsible => GetCustomerResponsibleObject(ResponsibleOption.BankResponsible);
        public Staff GetPrimaryResponsible => GetCustomerResponsibleObject(ResponsibleOption.PrimaryResponsible);

        private Staff GetCustomerResponsibleObject(ResponsibleOption responsibleOption)
        {
            var obj = default(Staff);

            if (CustomerResponsibleObjects != null && CustomerResponsibleObjects.Count > 0)
            {
                var dateNow = DateTime.Now.Date;
                obj = CustomerResponsibleObjects.Where(w => w.ResponsibleOption == responsibleOption)
                    .FirstOrDefault(f => (f.DateSince == null && f.DateTo >= dateNow)
                    || (f.DateSince <= dateNow && f.DateTo >= dateNow)
                    || (f.DateSince <= dateNow && f.DateTo == null)
                    || (f.DateSince == null && f.DateTo == null)
                    )?.Staff;
            }

            return obj;
        }

        /// <summary>
        /// Комментарий.
        /// </summary>
        [DisplayName("Комментарий"), Size(1024)]
        public string Comment { get; set; }

        /// <summary>
        /// Коллекция всех возможных систем налогообложения клиента.
        /// </summary>
        [Association, Aggregated, MemberDesignTimeVisibility(false)]
        public XPCollection<CustomerResponsibleObject> CustomerResponsibleObjects
        {
            get
            {
                return GetCollection<CustomerResponsibleObject>(nameof(CustomerResponsibleObjects));
            }
        }
    }
}
