using DevExpress.Xpo;
using System.Linq;

namespace RMS.Core.TG.Core.Models
{
    public class TGUser : XPObject
    {
        public TGUser() { }
        public TGUser(Session session) : base(session) { }

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public bool IsBot { get; set; }
        public byte[] Avatar { get; set; }
        
        public bool Edit(TGUser user) 
        {
            var result = false;
            
            if (user is null || user.Id <= 0)
            {
                return result;
            }

            if (Id == 0)
            {
                Id = user.Id;
                IsBot = user.IsBot;
            }

            if (FirstName != user.FirstName 
                || LastName != user.LastName 
                || UserName != user.UserName)
            {
                FirstName = user.FirstName;
                LastName = user.LastName;
                UserName = user.UserName;                
                
                result = true;
            }

            if (SetAvatar(user.Avatar))
            {
                result = true;
            }

            if (result)
            {
                Save();
            }

            return result;
        }
        
        public bool SetAvatar(byte[] obj)
        {
            if (obj is null)
            {
                return false;
            }

            Avatar = obj;
            return true;
        }

        protected override void OnSaving()
        {
            if (Id <= 0)
            {
                return;
            }

            base.OnSaving();
        }
        
        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(UserName))
            {
                var result = default(string);
                if (!string.IsNullOrWhiteSpace(FirstName))
                {
                    result += FirstName;
                }

                if (!string.IsNullOrWhiteSpace(LastName))
                {
                    result += $" {LastName}";
                }

                if (string.IsNullOrWhiteSpace(result))
                {
                    return $"TGUser_ID: {Id}";
                }
                else
                {
                    return $"{result.Trim()} ({Id})";
                }
            }
            else
            {
                return $"{UserName} ({Id})";
            }
        }
    }
}
