using DevExpress.Xpo;
using RMS.Core.TG.Core.Models;

namespace RMS.Core.Model.InfoCustomer
{
    /// <summary>
    /// Email клиента.
    /// </summary>
    public class CustomerTelegramUser : CustomerContact
    {
        public CustomerTelegramUser() { }
        public CustomerTelegramUser(Session session) : base(session) { }

        /// <summary>
        /// Уникальный идентификатор для бота.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public string Guid { get; set; }

        /// <summary>
        /// Пользователь телеграмм.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public TGUser TGUser { get; set; }

        [Association]
        [MemberDesignTimeVisibility(false)]
        public Customer Customer { get; set; }

        public string GetContactName()
        {
            var result = GetString();

            if (string.IsNullOrWhiteSpace(result))
            {
                if (!string.IsNullOrWhiteSpace(FullName))
                {
                    var splits = FullName.Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
                    if (splits.Length == 3)
                    {
                        result = $"{splits[1]} {splits[2]}";
                    }
                    else if (splits.Length == 2)
                    {
                        result = $"{splits[0]} {splits[1]}";
                    }

                    if (string.IsNullOrWhiteSpace(result))
                    {
                        result = FullName;
                    }
                }
                else
                {
                    result = ToString();
                }
            }
            return result;
        }

        public override string ToString()
        {
            return TGUser?.ToString();
        }
    }
}
