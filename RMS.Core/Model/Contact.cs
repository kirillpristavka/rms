using DevExpress.Xpo;
using RMS.Core.Model.InfoCustomer;

namespace RMS.Core.Model
{
    /// <summary>
    /// Контакт.
    /// </summary>
    public class Contact : XPObject
    {
        public Contact() { }
        public Contact(Session session) : base(session) { }

        [DisplayName("Фамилия"), Size(128)]
        public string Surname { get; set; }

        [DisplayName("Имя"), Size(128)]
        public string Name { get; set; }

        [DisplayName("Отчество"), Size(128)]
        public string Patronymic { get; set; }

        [DisplayName("Телефон"), Size(32)]
        public string Telephone { get; set; }

        [DisplayName("E-mail"), Size(128)]
        public string Email { get; set; }

        [DisplayName("Адрес")]
        public string AddressString => Address?.ToString();

        [DisplayName("Должность")]
        public string PositionString => Position?.ToString();

        /// <summary>
        /// Комментарий.
        /// </summary>
        [DisplayName("Комментарий"), Size(1024), MemberDesignTimeVisibility(false)]
        public string Comment { get; set; }

        [MemberDesignTimeVisibility(false)]
        public Position Position { get; set; }

        [MemberDesignTimeVisibility(false), Aggregated]
        public Address Address { get; set; }

        [Association, MemberDesignTimeVisibility(false)]
        public Customer Customer { get; set; }

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
    }
}