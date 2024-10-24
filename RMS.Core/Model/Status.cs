using DevExpress.Xpo;
using RMS.Core.Interface;

namespace RMS.Core.Model
{
    /// <summary>
    /// Статус клиента.
    /// </summary>
    public class Status : XPObject, IStatus
    {
        public Status() { }
        public Status(Session session) : base(session) { }

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
        [DisplayName("Статус клиента"), Size(256)]
        public string Name { get; set; }

        /// <summary>
        /// СОписание.
        /// </summary>
        [DisplayName("Описание"), Size(1024)]
        public string Description { get; set; }

        /// <summary>
        /// Использовать для формирования архивных папок.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public bool IsFormationArchiveFolder { get; set; }

        /// <summary>
        /// Использовать для формирования отчетов.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public bool IsFormationReport { get; set; }

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