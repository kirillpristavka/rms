using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Core.TG.Core.Models
{
    public class TGMessage : XPObject
    {
        public TGMessage() { }
        public TGMessage(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            CreateDate = DateTime.Now;
        }

        [MemberDesignTimeVisibility(false)]
        public int Id { get; set; }

        [DisplayName("Отправитель")]
        public string UserSender => TGUserSender?.ToString();

        [DisplayName("Получатель")]
        public string UserRecipient => TGUserRecipient?.ToString();

        /// <summary>
        /// Дата записи в БД.
        /// </summary>
        [Persistent]
        [DisplayName("Дата")]
        [System.ComponentModel.DataAnnotations.DisplayFormat(DataFormatString = "{0:dd/MM/yyyy (HH:mm:ss)}")]
        public DateTime CreateDate { get; private set; }

        /// <summary>
        /// Дата получения от пользователя на сервере Telegram.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        [System.ComponentModel.DataAnnotations.DisplayFormat(DataFormatString = "{0:dd/MM/yyyy (HH:mm:ss)}")]
        public DateTime? Date { get; set; }

        [DisplayName("Сообщение")]
        public string Text => ByteToString(Obj);

        [Persistent]
        public byte[] Obj { get; private set; }

        public void SetObj(object @obj)
        {
            if (@obj is string strObj)
            {
                Obj = StringToByte(strObj);
            }
            else
            {
                Obj = null;
            }
        }
        
        [MemberDesignTimeVisibility(false)]
        public TGUser TGUserSender { get; set; }
        
        [MemberDesignTimeVisibility(false)]
        public TGUser TGUserRecipient { get; set; }

        [MemberDesignTimeVisibility(false)]
        public TGDocument TGDocument { get; set; }

        [Aggregated]
        [Association]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<TGMessagePhoto> TGMessagePhotos
        {
            get
            {
                return GetCollection<TGMessagePhoto>(nameof(TGMessagePhotos));
            }
        }

        public async Task<bool> Edit(TGMessage message)
        {
            if (message is null || message.Id <= 0 || Session is null || message.TGUserSender is null)
            {
                return false;
            }

            if (Id == 0)
            {
                Id = message.Id;
                TGUserSender = await Session.GetObjectByKeyAsync<TGUser>(message.TGUserSender?.Oid);
                TGUserRecipient = await Session.GetObjectByKeyAsync<TGUser>(message.TGUserRecipient?.Oid);
            }

            if (Date is null)
            {
                Date = message.Date;
            }

            //TODO: реализовать правильное сохранение.
            if (Text != message.Text)
            {
                Obj = message.Obj;
            }

            Save();

            return true;
        }

        public byte[] StringToByte(string msg)
        {
            if (string.IsNullOrWhiteSpace(msg))
            {
                return default;
            }
            else
            {
                return Encoding.Default.GetBytes(msg);
            }
        }

        public string ByteToString(byte[] msg)
        {
            if (msg == null)
            {
                return default;
            }
            else
            {
                return Encoding.Default.GetString(msg);
            }
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
            var result = default(string);

            if (UserSender != null)
            {
                result += UserSender?.ToString();
            }

            if (!string.IsNullOrWhiteSpace(Text))
            {
                if (!string.IsNullOrWhiteSpace(result))
                {
                    result += $" => {Text}";
                }
                else
                {
                    result = $"Message => {Text}";
                }                
            }

            if (string.IsNullOrWhiteSpace(result))
            {
                result = $"Empty message.";
            }

            return result;
        }
    }
}