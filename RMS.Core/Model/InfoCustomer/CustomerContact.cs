using DevExpress.Xpo;

namespace RMS.Core.Model.InfoCustomer
{
    /// <summary>
    /// Контакт.
    /// </summary>
    public class CustomerContact : XPObject
    {
        public CustomerContact() { }
        public CustomerContact(Session session) : base(session) { }

        [Size(1024)]
        [DisplayName("ФИО")]
        public string FullName { get; set; }

        [Size(128)]
        [DisplayName("Фамилия")]
        [MemberDesignTimeVisibility(false)]
        public string Surname { get; set; }

        [Size(128)]
        [DisplayName("Имя")]
        [MemberDesignTimeVisibility(false)]
        public string Name { get; set; }

        [Size(128)]
        [DisplayName("Отчество")]
        [MemberDesignTimeVisibility(false)]
        public string Patronymic { get; set; }

        /// <summary>
        /// Комментарий.
        /// </summary>
        [Size(1024)]
        [DisplayName("Комментарий")]
        [MemberDesignTimeVisibility(false)]
        public string Comment { get; set; }

        [MemberDesignTimeVisibility(false)]
        public Position Position { get; set; }

        [Aggregated]
        [MemberDesignTimeVisibility(false)]
        public Address Address { get; set; }
        
        /// <summary>
        /// Полное ФИО.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public string FullNameString
        {
            get
            {
                var result = default(string);

                if (!string.IsNullOrWhiteSpace(Surname))
                {
                    result += Surname;

                    if (!string.IsNullOrWhiteSpace(Name))
                    {
                        result += $" {Name}";
                    }

                    if (!string.IsNullOrWhiteSpace(Patronymic))
                    {
                        result += $" {Patronymic}";
                    }
                }

                return result;
            }
        }

        public override string ToString()
        {
            return GetString();
        }

        public string GetString()
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