using DevExpress.Xpo;
using RMS.Core.Enumerator;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.OnesEs;
using System;

namespace RMS.Core.Model.Chronicle.OnesEs
{
    /// <summary>
    /// Хроника изменений 1C Зарплата.
    /// </summary>
    public class ChronicleOneEsSalary : XPObject
    {
        public ChronicleOneEsSalary() { }
        public ChronicleOneEsSalary(Session session) : base(session) { }

        /// <summary>
        /// Дата ввода.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public DateTime DateCreate { get; set; }

        /// <summary>
        /// Используется или нет.
        /// </summary>
        [DisplayName("Есть/нет")]
        public bool IsUse { get; set; }

        /// <summary>
        /// Дата ввода.
        /// </summary>
        [DisplayName("Дата")]
        public DateTime? Date { get; set; }

        /// <summary>
        /// Комментарий.
        /// </summary>
        [DisplayName("Комментарий"), Size(1024)]
        public string Comment { get; set; }

        /// <summary>
        /// Путь.
        /// </summary>
        [DisplayName("Путь"), Size(1024)]
        public string Path { get; set; }

        /// <summary>
        /// Наличие.
        /// </summary>
        [DisplayName("Наличие")]
        public Availability? Availability { get; set; }

        [DisplayName("Пользователь")]
        public string UserString => User?.ToString();
        /// <summary>
        /// Пользователь внесший изменения.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public User User { get; set; }

        [Association, MemberDesignTimeVisibility(false)]
        public OneEsSalary OneEsSalary { get; set; }
    }
}