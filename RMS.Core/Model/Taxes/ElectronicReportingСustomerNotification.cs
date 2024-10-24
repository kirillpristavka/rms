using DevExpress.Xpo;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Mail;
using RMS.Core.Model.Notifications;
using System;

namespace RMS.Core.Model.Taxes
{
    public class ElectronicReportingСustomerNotification : XPObject
    {
        public ElectronicReportingСustomerNotification() { }
        public ElectronicReportingСustomerNotification(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Date = DateTime.Now;
        }
        
        [MemberDesignTimeVisibility(false)]
        public DateTime Date { get; set; }

        [DisplayName("Уведомления")]
        public string RecipientString => ToString();

        [MemberDesignTimeVisibility(false)]
        public string Recipient { get; set; }

        [MemberDesignTimeVisibility(false)]
        public bool IsUseEmail { get; set; }
        [MemberDesignTimeVisibility(false)]
        public DateTime? DateEmail { get; set; }
        [MemberDesignTimeVisibility(false)]
        public string RecipientEmail { get; set; }

        [MemberDesignTimeVisibility(false)]
        public bool IsUseTelegram { get; set; }
        [MemberDesignTimeVisibility(false)]
        public DateTime? DateTelegram { get; set; }
        [MemberDesignTimeVisibility(false)]
        public string RecipientTelegram { get; set; }

        [MemberDesignTimeVisibility(false)]
        public bool IsUseWhatsApp { get; set; }
        [MemberDesignTimeVisibility(false)]
        public DateTime? DateWhatsApp { get; set; }
        [MemberDesignTimeVisibility(false)]
        public string RecipientWhatsApp { get; set; }

        [MemberDesignTimeVisibility(false)]
        public bool IsUsePhone { get; set; }
        [MemberDesignTimeVisibility(false)]
        public DateTime? DatePhone { get; set; }
        [MemberDesignTimeVisibility(false)]
        public string RecipientPhone { get; set; }

        [DisplayName("Сотрудник")]
        public string StaffString => Staff?.ToString();
        [MemberDesignTimeVisibility(false)]
        public Staff Staff { get; set; }
        
        [DisplayName("Тема")]
        public byte[] Topic { get; set; }

        [DisplayName("Сообщение")]
        public byte[] Message { get; set; }

        [MemberDesignTimeVisibility(false)]
        public Customer Customer { get; set; }
        
        [MemberDesignTimeVisibility(false)]
        public Letter Letter { get; set; }
        
        [Association]
        [MemberDesignTimeVisibility(false)]
        public ElectronicReportingCustomer ElectronicReportingCustomer { get; set; }
        
        [MemberDesignTimeVisibility(false)]
        public ControlSystem ControlSystem { get; set; }

        public override string ToString()
        {
            var result = default(string);

            if (IsUseEmail)
            {
                result += "Email: ";
                if (DateEmail != null)
                {
                    result += $"[{DateEmail.Value.ToShortDateString()}] ";
                }
                
                result += $"{RecipientEmail}{Environment.NewLine}";
            }

            if (IsUseTelegram)
            {
                result += "Telegram: ";
                if (DateTelegram != null)
                {
                    result += $"[{DateTelegram.Value.ToShortDateString()}] ";
                }

                result += $"{RecipientTelegram}{Environment.NewLine}";
            }

            if (IsUseWhatsApp)
            {
                result += "WhatsApp: ";
                if (DateWhatsApp != null)
                {
                    result += $"[{DateWhatsApp.Value.ToShortDateString()}] ";
                }

                result += $"{RecipientWhatsApp}{Environment.NewLine}";
            }

            if (IsUsePhone)
            {
                result += "Звонок: ";
                if (DatePhone != null)
                {
                    result += $"[{DatePhone.Value.ToShortDateString()}] ";
                }

                result += $"{RecipientPhone}{Environment.NewLine}";
            }

            if (string.IsNullOrWhiteSpace(result))
            {
                return "Пустое оповещение";
            }
            
            return result?.Trim();
        }
    }
}