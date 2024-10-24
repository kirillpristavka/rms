using DevExpress.Xpo;
using RMS.Core.Model.InfoCustomer;

namespace RMS.Core.Model
{
    public class BankAccess : XPObject
    {
        public BankAccess() { }
        public BankAccess(Session session) : base(session) { }

        [DisplayName("Описание"), Size(1024)]
        public string Description { get; set; }

        [DisplayName("Банк"), Size(128)]
        public string BankString => Bank?.PaymentName;
        /// <summary>
        /// Банк.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Bank Bank { get; set; }

        /// <summary>
        /// Логин.
        /// </summary>
        [Size(128)]
        [DisplayName("Логин")]
        public string Login { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        [Size(128)]
        [DisplayName("Пароль")]
        public string Password { get; set; }

        /// <summary>
        /// Ссылка.
        /// </summary>
        [DisplayName("Ссылка"), Size(1024)]
        public string Link { get; set; }

        /// <summary>
        /// Комментарий.
        /// </summary>
        [DisplayName("Комментарий"), Size(1024)]
        public string Comment { get; set; }

        [Association, MemberDesignTimeVisibility(false)]
        public Customer Customer { get; set; }

        [MemberDesignTimeVisibility(false), Aggregated]
        public Account Account { get; set; }

        public override string ToString()
        {
            if (Bank != null)
            {
                return BankString;
            }

            return $"Запись № {Oid}";
        }
    }
}
