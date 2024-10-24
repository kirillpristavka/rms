using DevExpress.Xpo;
using System;
using System.Collections.Generic;

namespace RMS.Core.Model.InfoCustomer
{
    /// <summary>
    /// Сотрудники клиента.
    /// </summary>
    public class CustomerStaff : XPObject, IEquatable<CustomerStaff>
    {
        public CustomerStaff() { }
        public CustomerStaff(Session session) : base(session) { }

        public string CustomerName => Customer?.ToString();
        public Customer Customer { get; set; }

        [Size(128)]
        [DisplayName("Фамилия")]
        public string Surname { get; set; }

        [Size(128)]
        [DisplayName("Имя")]
        public string Name { get; set; }

        [Size(128)]
        [DisplayName("Отчество")]
        public string Patronymic { get; set; }

        [DisplayName("Иностранец")]
        public bool IsForeigner { get; set; }

        [DisplayName("Дата рождения")]
        public DateTime? DateBirth { get; set; }

        [DisplayName("Начало патента")]
        public DateTime? DateSince { get; set; }

        [DisplayName("Окончание патента")]
        public DateTime? DateTo { get; set; }

        public void Edit(CustomerStaff customerStaff)
        {
            if (customerStaff is null || customerStaff.Customer is null)
            {
                return;
            }

            Customer = customerStaff.Customer;
            Surname = customerStaff.Surname;
            Name = customerStaff.Name;
            Patronymic = customerStaff.Patronymic;
            DateBirth = customerStaff.DateBirth;
            IsForeigner = customerStaff.IsForeigner;
            DateSince = customerStaff.DateSince;
            DateTo = customerStaff.DateTo;

            Save();
        }

        [Aggregated]
        [Association]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<CustomerStaffPatent> Patents
        {
            get
            {
                return GetCollection<CustomerStaffPatent>(nameof(Patents));
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as CustomerStaff);
        }

        public bool Equals(CustomerStaff other)
        {
            return other != null &&
                   Surname == other.Surname &&
                   Name == other.Name &&
                   Patronymic == other.Patronymic &&
                   DateBirth == other.DateBirth;
        }

        public override int GetHashCode()
        {
            int hashCode = 185259352;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Surname);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Patronymic);
            hashCode = hashCode * -1521134295 + DateBirth.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            var result = default(string);

            if (!string.IsNullOrWhiteSpace(Surname))
            {
                result += Surname;

                if (!string.IsNullOrWhiteSpace(Name))
                {
                    result += $" {Name.Substring(0, 1).ToUpper()}.";
                }

                if (!string.IsNullOrWhiteSpace(Patronymic))
                {
                    result += $" {Patronymic.Substring(0, 1).ToUpper()}.";
                }
            }

            return result;
        }

        public static bool operator ==(CustomerStaff left, CustomerStaff right)
        {
            return EqualityComparer<CustomerStaff>.Default.Equals(left, right);
        }

        public static bool operator !=(CustomerStaff left, CustomerStaff right)
        {
            return !(left == right);
        }
    }
}
