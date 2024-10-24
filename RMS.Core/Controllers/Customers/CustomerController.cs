using DevExpress.Xpo;
using RMS.Core.Enumerator;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using System;
using System.Collections.Generic;

namespace RMS.Core.Controllers.Customers
{
    public static class CustomerController
    {
        private static List<Customer> _customers;
        public async static System.Threading.Tasks.Task<List<Customer>> GetCustomersAsync(
            Session session,
            bool isForceUpdate = false,
            bool isConfigureAwait = true)
        {
            if (_customers != null && isForceUpdate is false)
            {
                return _customers;
            }

            _customers = await new XPQuery<Customer>(session).ToListAsync().ConfigureAwait(isConfigureAwait);
            return _customers;
        }

        public static bool StatusOrganizationUpdate(this Customer customer, string status, string serviceName, User currentUser = default)
        {
            if (!string.IsNullOrWhiteSpace(status))
            {
                var statusOrganization = GetStatusOrganization(status);

                if (customer.OrganizationStatus != statusOrganization)
                {
                    customer.OrganizationStatus = statusOrganization;
                    customer.Save();

                    var chronicleCustomer = new Model.Chronicle.ChronicleCustomer(customer.Session)
                    {
                        Act = Act.CUSTOMER_STATE_CHANGE,
                        Date = DateTime.Now,
                        Description = $"Обновление статуса организации с сервиса {serviceName}",
                        User = currentUser,
                        Customer = customer
                    };
                    chronicleCustomer.Save();

                    return true;
                }
            }

            return false;
        }

        private static StatusOrganization GetStatusOrganization(string status)
        {
            switch (status)
            {
                case "В процессе ликвидации":
                    return StatusOrganization.LIQUIDATING;

                case "Не действует":
                    return StatusOrganization.LIQUIDATED;

                case "LIQUIDATING":
                    return StatusOrganization.LIQUIDATING;

                case "LIQUIDATED":
                    return StatusOrganization.LIQUIDATED;

                case "ACTIVE":
                    return StatusOrganization.ACTIVE;

                case "REORGANIZING":
                    return StatusOrganization.REORGANIZING;

                case "BANKRUPT":
                    return StatusOrganization.BANKRUPT;

                default:
                    return StatusOrganization.ACTIVE;
            }
        }
    }
}
