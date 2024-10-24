using DevExpress.Xpo;
using System;

namespace RMS.Core.Model.ElectronicDocumentsManagement
{
    /// <summary>
    /// Объекты клиентской электронной отчетности.
    /// </summary>
    public class ElectronicDocumentManagementCustomerObject : XPObject
    {
        public ElectronicDocumentManagementCustomerObject() { }
        public ElectronicDocumentManagementCustomerObject(Session session) : base(session) { }

        [DisplayName("ЭДО\n(провайдер)")]
        public string ElectronicDocumentManagementString => ElectronicDocumentManagement?.ToString();

        [MemberDesignTimeVisibility(false)]
        public ElectronicDocumentManagement ElectronicDocumentManagement { get; set; }

        /// <summary>
        /// Пользователь, который создал.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public User UserCreate { get; set; }

        /// <summary>
        /// Дата создания.
        /// </summary>        
        [DisplayName("Дата создания")]
        [MemberDesignTimeVisibility(false)]
        public DateTime DateCreate { get; set; } = DateTime.Now;

        /// <summary>
        /// Комментарий.
        /// </summary>
        [Size(2045)]
        [DisplayName("Комментарий")]
        public string Comment { get; set; }
        
        [DisplayName("Дата\nначала\nдействия")]
        public DateTime? DateSince { get; set; }

        [DisplayName("Дата\nокончания\nдействия")]
        public DateTime? DateTo { get; set; }

        [Size(2045)]
        [DisplayName("Получатель")]
        public string Recipient { get; set; }

        [Size(2045)]
        [DisplayName("Отправитель")]
        public string Sender { get; set; }

        [Size(2045)]
        [DisplayName("Полномочия")]
        public string Powers { get; set; }

        [Size(2045)]
        [DisplayName("Логин")]
        public string Login { get; set; }

        [Size(2045)]
        [DisplayName("Пароль")]
        public string Password { get; set; }

        [Size(2045)]
        [DisplayName("Нюансы")]
        public string Nuances { get; set; }

        [Association]
        [MemberDesignTimeVisibility(false)]
        public ElectronicDocumentManagementCustomer ElectronicDocumentManagementCustomer { get; set; }

        public override string ToString()
        {
            var result = default(string);

            if (ElectronicDocumentManagement != null)
            {
                result += ElectronicDocumentManagement?.ToString();
            }

            if (DateSince != null)
            {
                result += $"c {DateSince.Value.ToShortDateString()}";
            }

            if (DateTo != null)
            {
                result += $"по {DateTo.Value.ToShortDateString()}";
            }

            if (!string.IsNullOrWhiteSpace(result))
            {
                return result;
            }
            
            return base.ToString();
        }
    }
}