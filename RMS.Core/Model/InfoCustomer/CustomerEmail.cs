using DevExpress.Xpo;
using RMS.Core.TG.Core.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RMS.Core.Model.InfoCustomer
{
    /// <summary>
    /// Email клиента.
    /// </summary>
    public class CustomerEmail : CustomerContact
    {
        public CustomerEmail() { }
        public CustomerEmail(Session session) : base(session) { }

        protected async override void OnLoaded()
        {
            base.OnLoaded();

            UsersTG = await GetUsersTGAsync();
        }

        protected async override void OnSaved()
        {
            base.OnSaved();

            UsersTG = await GetUsersTGAsync();
        }

        [DisplayName("Telegram")]
        public bool IsAuthorizationTelegram
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Guid))
                {
                    return false;
                }

                return true;
            }
        }

        [DisplayName("Пользователи Telegram")]
        public string UsersTG { get; private set; }

        private async Task<string> GetUsersTGAsync()
        {
            var result = default(string);
            var users = await new XPQuery<CustomerTelegramUser>(Session)
                ?.Where(w => w.Guid == Guid)
                ?.ToListAsync();

            if (users != null)
            {
                foreach (var user in users)
                {
                    result += $"{user}{Environment.NewLine}";
                }
            }

            return result?.Trim();
        }

        [Size(32)]
        [Persistent("Email")]
        [DisplayName("E-mail")]
        [MemberDesignTimeVisibility(false)]
        public string Email2 { get; set; }

        [Size(256)]
        [Persistent("Email2")]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        /// <summary>
        /// Используется по умолчанию.
        /// </summary>
        [DisplayName("По умолчанию")]
        [MemberDesignTimeVisibility(false)]
        public bool IsDefault { get; set; }

        /// <summary>
        /// Уникальный идентификатор для бота.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public string Guid { get; set; }

        /// <summary>
        /// Пользователь телеграмм.
        /// </summary>
        [Obsolete]
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
            return Email;
        }
    }
}
