using DevExpress.Xpo;
using RMS.Core.Interface;

namespace RMS.Core.Model
{
    /// <summary>
    /// Статус учета страховых.
    /// </summary>
    public class AccountingInsuranceStatus : XPObject, IStatus
    {
        public AccountingInsuranceStatus() { }
        public AccountingInsuranceStatus(Session session) : base(session) { }
        
        /// <summary>
        /// Индекс статуса.
        /// Используется для сортировки.
        /// </summary>
        [DisplayName("Индекс")]
        [MemberDesignTimeVisibility(false)]
        public int? Index { get; set; }

        /// <summary>
        /// Индекс иконки.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public int? IndexIcon { get; set; }

        /// <summary>
        /// Статус клиента.
        /// </summary>
        [Size(256)]
        [DisplayName("Статус договора")]
        public string Name { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        [Size(1024)]
        [DisplayName("Описание")]
        public string Description { get; set; }

        /// <summary>
        /// Цвет.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public string Color { get; set; }

        [DisplayName("По умолчанию")]
        public bool IsDefault { get; set; }

        /// <summary>
        /// Защита от удаления.
        /// </summary>
        [DisplayName("Защита от удаления")]
        [MemberDesignTimeVisibility(false)]
        public bool IsProtectionDelete { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
