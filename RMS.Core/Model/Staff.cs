using DevExpress.Xpo;
using RMS.Core.Model.SalaryStaff;
using RMS.Core.TG.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RMS.Core.Model
{
    /// <summary>
    /// Сотрудник.
    /// </summary>
    public class Staff : XPObject, IEquatable<Staff>
    {
        public Staff() { }
        public Staff(Session session) : base(session) { }

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

        /// <summary>
        /// Уникальный идентификатор пользователя Telegram.
        /// </summary>
        [Obsolete]
        [MemberDesignTimeVisibility(false)]
        public long? TelegramUserId { get; set; }

        [Size(128)]
        [DisplayName("Фамилия")]
        public string Surname { get; set; }

        [Size(128)]
        [DisplayName("Имя")]
        public string Name { get; set; }

        [Size(128)]
        [DisplayName("Отчество")]
        public string Patronymic { get; set; }

        [DisplayName("Дата рождения")]
        public DateTime? DateBirth { get; set; }

        /// <summary>
        /// Дата принятия.
        /// </summary>
        [DisplayName("Дата принятия")]
        public DateTime? DateReceipt { get; set; }

        /// <summary>
        /// Дата увольнения.
        /// </summary>
        [DisplayName("Дата увольнения")]
        public DateTime? DateDismissal { get; set; }

        [DisplayName("№ приказа"), Size(512)]
        [MemberDesignTimeVisibility(false)]
        public string OrderNumber { get; set; }

        [DisplayName("№ договора"), Size(256)]
        [MemberDesignTimeVisibility(false)]
        public string ContractNumber { get; set; }

        [DisplayName("Телефон"), Size(128)]
        public string PhoneNumber { get; set; }

        [Size(256)]
        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Должность"), Size(256)]
        public string PositionString => Position?.ToString();
        
        /// <summary>
        /// Подпись для отправки писем.
        /// </summary>
        [Size(2048)]
        [DisplayName("Подпись")]
        [MemberDesignTimeVisibility(false)]
        public string Signature { get; set; }

        [MemberDesignTimeVisibility(false)]
        public Position Position { get; set; }

        [Association]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<Vacation> Vacations
        {
            get
            {
                return GetCollection<Vacation>(nameof(Vacations));
            }
        }

        /// <summary>
        /// Основание для выплат.
        /// </summary>
        [Association]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<Basis> Basis
        {
            get
            {
                return GetCollection<Basis>(nameof(Basis));
            }
        }
        
        /// <summary>
        /// Расчет ЗП.
        /// </summary>
        [Association]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<Сalculation> Сalculation
        {
            get
            {
                return GetCollection<Сalculation>(nameof(Сalculation));
            }
        }
        
            
        /// <summary>
        /// Дополнительные услуги.
        /// </summary>
        [Association]        
        [MemberDesignTimeVisibility(false)]
        public XPCollection<AdditionalServices> AdditionalServices
        {
            get
            {
                return GetCollection<AdditionalServices>(nameof(AdditionalServices));
            }
        }

        public override string ToString()
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

        public override bool Equals(object obj)
        {
            return Equals(obj as Staff);
        }

        public bool Equals(Staff other)
        {
            return other != null &&
                   Surname == other.Surname &&
                   Name == other.Name &&
                   Patronymic == other.Patronymic &&
                   DateBirth == other.DateBirth &&
                   DateReceipt == other.DateReceipt &&
                   DateDismissal == other.DateDismissal &&
                   OrderNumber == other.OrderNumber &&
                   ContractNumber == other.ContractNumber &&
                   PhoneNumber == other.PhoneNumber;
        }

        public override int GetHashCode()
        {
            var hashCode = -947647431;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Surname);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Patronymic);
            hashCode = hashCode * -1521134295 + DateBirth.GetHashCode();
            hashCode = hashCode * -1521134295 + DateReceipt.GetHashCode();
            hashCode = hashCode * -1521134295 + DateDismissal.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(OrderNumber);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ContractNumber);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PhoneNumber);
            return hashCode;
        }
    }
}
