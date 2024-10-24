using System;

namespace RMS.Core.ObjectDTO.Models
{
    public class UserDTO : BaseObjectDTO
    {
        public string Login { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public byte[] UserPhoto { get; set; }
        public DateTime? DateBirth { get; set; }
        public DateTime? DateReceipt { get; set; }
        public DateTime? DateDismissal { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public bool Gender { get; set; }
        public bool flagAdministrator { get; set; }
        public string PhoneNumber { get; set; }
        public string Comment { get; set; }
        public StaffDTO Staff { get; set; }

        /// <summary>
        /// Уникальный идентификатор пользователя Telegram.
        /// </summary>
        public long? TelegramUserId { get; set; }

        
        //public IEnumerable<UserGroupsDTO> UserGroups
        //{
        //    get
        //    {
        //        return GetCollection<UserGroupsDTO>(nameof(UserGroups));
        //    }
        //}

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

        ///// <summary>
        ///// Права доступа.
        ///// </summary>
        //[Aggregated]
        //[MemberDesignTimeVisibility(false)]
        //public AccessRights AccessRights { get; set; }
    }
}
