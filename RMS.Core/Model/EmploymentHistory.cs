using DevExpress.Xpo;
using System;

namespace RMS.Core.Model
{
    /// <summary>
    /// Трудовая книжка.
    /// </summary>
    public class EmploymentHistory : XPObject
    {
        public EmploymentHistory() { }
        public EmploymentHistory(Session session) : base(session) { }

        [DisplayName("Фамилия"), Size(128)]
        public string Surname { get; set; }

        [DisplayName("Имя"), Size(128)]
        public string Name { get; set; }

        [DisplayName("Отчество"), Size(128)]
        public string Patronymic { get; set; }

        /// <summary>
        /// Дата получения.
        /// </summary>
        [DisplayName("Дата получения")]
        public DateTime? DateReceiving { get; set; }

        /// <summary>
        /// Дата выдачи.
        /// </summary>
        [DisplayName("Дата выдачи")]
        public DateTime? DateIssue { get; set; }

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
    }
}
