using DevExpress.Xpo;
using RMS.Core.Model.Access;
using System;

namespace RMS.Core.Model
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    public class User : XPObject
    {
        public User() { }
        public User(Session session) : base(session) { }

        [DisplayName("Логин"), Size(32)]
        public string Login { get; set; }

        [DisplayName("Фамилия"), Size(128)]
        public string Surname { get; set; }

        [DisplayName("Имя"), Size(128)]
        public string Name { get; set; }

        [DisplayName("Отчество"), Size(128)]
        public string Patronymic { get; set; }

        [MemberDesignTimeVisibility(false)]
        public byte[] UserPhoto { get; set; }

        [MemberDesignTimeVisibility(false)]
        public DateTime? DateBirth { get; set; }

        [MemberDesignTimeVisibility(false)]
        public DateTime? DateReceipt { get; set; }

        [MemberDesignTimeVisibility(false)]
        public DateTime? DateDismissal { get; set; }

        [DisplayName(), Size(128), MemberDesignTimeVisibility(false)]
        public string Email { get; set; }

        [DisplayName(), Size(128), MemberDesignTimeVisibility(false)]
        public string Password { get; set; }

        [DisplayName(), Size(256), MemberDesignTimeVisibility(false)]
        public string FullName { get; set; }

        [MemberDesignTimeVisibility(false)]
        public bool Gender { get; set; }

        [MemberDesignTimeVisibility(false)]
        public bool flagAdministrator { get; set; }

        [DisplayName(), Size(32), MemberDesignTimeVisibility(false)]
        public string PhoneNumber { get; set; }

        [DisplayName(), Size(1024), MemberDesignTimeVisibility(false)]
        public string Comment { get; set; }

        [MemberDesignTimeVisibility(false)]
        public Staff Staff { get; set; }

        /// <summary>
        /// Уникальный идентификатор пользователя Telegram.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public long? TelegramUserId { get; set; }

        [Association, MemberDesignTimeVisibility(false)]
        public XPCollection<UserGroups> UserGroups
        {
            get
            {
                return GetCollection<UserGroups>(nameof(UserGroups));
            }
        }

        public override string ToString()
        {
            if (Staff is null)
            {
                return Login;
            }
            else
            {
                return Staff.ToString();
            }
        }

        /// <summary>
        /// Права доступа.
        /// </summary>
        [Aggregated]
        [MemberDesignTimeVisibility(false)]
        public AccessRights AccessRights { get; set; }        
    }
}
