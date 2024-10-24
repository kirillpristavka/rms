using DevExpress.Xpo;
using RMS.Core.Enumerator;
using RMS.Core.Interface;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Mail;
using RMS.Core.Model.Notifications;
using System;
using System.ComponentModel.DataAnnotations;

namespace RMS.Core.Model
{
    /// <summary>
    /// Сделка.
    /// </summary>
    public class Deal : XPObject, INotification
    {
        public Deal() { }
        public Deal(Session session) : base(session) { }
                
        /// <summary>
        /// Номер.
        /// </summary>
        [Size(256)]
        [DisplayName("Номер")]
        [MemberDesignTimeVisibility(false)]
        public string Number { get; set; }
        
        [DisplayName("Клиент")]
        public string CustomerString => Customer?.ToString();
        /// <summary>
        /// Клиент.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Customer Customer { get; set; }
        
        /// <summary>
        /// Статус сделки.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public DealStatus DealStatus { get; set; }

        /// <summary>
        /// Статус.
        /// </summary>
        [DisplayName(" ")]
        public int? DealStatusOidString => DealStatus?.Oid;
        /// <summary>
        /// Статус.
        /// </summary>
        [DisplayName("Статус")]
        public string StatusString => DealStatus?.ToString();

        /// <summary>
        /// Ответственный.
        /// </summary>
        [DisplayName("Ответственный")]
        public string StaffString => Staff?.ToString();
        /// <summary>
        /// Ответственный.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Staff Staff { get; set; }

        /// <summary>
        /// Дата изменения.
        /// </summary>
        [DisplayName("Дата изменения")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy (HH:mm:ss)}")]
        public DateTime DateUpdate { get; set; }

        /// <summary>
        /// Дата получения письма.
        /// </summary>
        [DisplayName("Дата получения письма")]
        public DateTime? LetterDate => Letter?.DateReceiving;        

        /// <summary>
        /// Отправитель письма.
        /// </summary>
        [DisplayName("Отправитель письма")]
        public string LetterSenderAddress => Letter?.LetterSenderAddress;

        /// <summary>
        /// Тема письма.
        /// </summary>
        [DisplayName("Тема письма")]
        public string LetterTopic => Letter?.TopicString;
                
        /// <summary>
        /// Письмо.
        /// </summary>
        [Aggregated]
        [MemberDesignTimeVisibility(false)]
        public Letter Letter { get; set; }        

        /// <summary>
        /// Наименование.
        /// </summary>
        [DisplayName("Наименование")]
        [Size(256)]
        [MemberDesignTimeVisibility(false)]
        public string Name { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        [DisplayName("Описание"), Size(1024)]
        public string Description { get; set; }
        
        /// <summary>
        /// Сюда записывается информация и том какое письмо было удалено.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        [Size(1024)]
        public string MessageDeleteLetter { get; set; }

        /// <summary>
        /// Использование задачи.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public bool IsUseTask { get; set; }

        /// <summary>
        /// Задача.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Task Task { get; set; }

        [MemberDesignTimeVisibility(false)]
        [DisplayName("Сделка")]
        public string DealToString2 
        { 
            get
            {
                var result = default(string);

                if (Letter!= null)
                {
                    result = Letter.ToString();
                }
                else if (!string.IsNullOrWhiteSpace(ToString()))
                {
                    result = ToString();
                }
                else
                {
                    result = "Не найдена информация по сделке";
                }
                
                return result;
            }
        }        

        public Notification GetNotification(TypeNotification typeNotification)
        {
            if (DateUpdate is DateTime deadline)
            {
                var name = default(string);                

                if (Customer != null)
                {
                    name += $"{Environment.NewLine}Клиент: {Customer}";
                }

                if (DealStatus != null)
                {
                    name += $"{Environment.NewLine}Статус: {DealStatus.ToString().ToLower()}";
                }                

                if (Staff != null)
                {
                    name += $"{Environment.NewLine}Исполнитель: {Staff}";
                }

                if (!string.IsNullOrWhiteSpace(Description))
                {
                    name += $"{Environment.NewLine}Описание:{Description}";
                }

                return new Notification(typeNotification, "Задачи", deadline, name, Oid, typeof(Deal));
            }
            else
            {
                return default;
            }
        }

        public override string ToString()
        {
            var result = "Сделка";

            if (LetterDate is DateTime date)
            {
                result += $" от {date.ToShortDateString()}";
            }

            if (Customer != null)
            {
                result += $" по клиенту {Customer}";
            }

            return result?.Trim();
        }
    }
}
