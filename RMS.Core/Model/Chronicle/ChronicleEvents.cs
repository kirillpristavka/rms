using DevExpress.Xpo;
using RMS.Core.Enumerator;
using System;
using System.ComponentModel.DataAnnotations;

namespace RMS.Core.Model.Chronicle
{
    /// <summary>
    /// Хроника событий пользователей.
    /// </summary>
    public class ChronicleEvents : XPObject
    {
        public ChronicleEvents() { }
        public ChronicleEvents(Session session) : base(session) { }

        /// <summary>
        /// Наименование.
        /// </summary>
        [Size(256)]
        [DisplayName("Наименование")]
        public string Name { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        [Size(4000)]
        [DisplayName("Описание")]
        public string Description { get; set; }

        /// <summary>
        /// Действие.
        /// </summary>
        public Act Act { get; set; }

        /// <summary>
        /// Дата.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy (HH:mm:ss)}")]
        public DateTime Date { get; set; } = DateTime.Now;

        /// <summary>
        /// Пользователь.
        /// </summary>
        public User User { get; set; }

        public override string ToString()
        {
            return $"{Date} -> {Name}";
        }
    }
}
