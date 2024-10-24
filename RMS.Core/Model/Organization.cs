using DevExpress.Xpo;
using System;

namespace RMS.Core.Model
{
    public class Organization : XPObject
    {
        public Organization() { }
        public Organization(Session session) : base(session) { }

        [DisplayName("Префикс"), MemberDesignTimeVisibility(false), Size(32)]
        public string Prefix { get; set; }

        [DisplayName("ИНН"), Size(32)]
        public string INN { get; set; }

        [DisplayName("КПП"), MemberDesignTimeVisibility(false), Size(32)]
        public string KPP { get; set; }

        [DisplayName("Наименование"), Size(256)]
        public string Name { get; set; }

        [DisplayName("Полное наименование"), MemberDesignTimeVisibility(false), Size(1024)]
        public string FullName { get; set; }

        [DisplayName("Сокращенное наименование"), MemberDesignTimeVisibility(false), Size(256)]
        public string AbbreviatedName { get; set; }

        /// <summary>
        /// Дата регистрации.
        /// </summary>
        [DisplayName("Дата регистрации")]
        public DateTime? DateRegistration { get; set; }

        /// <summary>
        /// Дата ликвидации.
        /// </summary>
        [DisplayName("Дата ликвидации")]
        public DateTime? DateLiquidation { get; set; }

        [DisplayName("Руководитель"), Size(256)]
        public string ManagementString
        {
            get
            {
                var result = default(string);

                if (!string.IsNullOrWhiteSpace(ManagementSurname))
                {
                    result += ManagementSurname;

                    if (!string.IsNullOrWhiteSpace(ManagementName))
                    {
                        result += $" {ManagementName.Substring(0, 1).ToUpper()}.";
                    }

                    if (!string.IsNullOrWhiteSpace(ManagementPatronymic))
                    {
                        result += $" {ManagementPatronymic.Substring(0, 1).ToUpper()}.";
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// Руководитель (Фамилия).
        /// </summary>
        [MemberDesignTimeVisibility(false), Size(128), DisplayName("Руководитель (Фамилия)")]
        public string ManagementSurname { get; set; }

        /// <summary>
        /// Руководитель (Имя).
        /// </summary>
        [MemberDesignTimeVisibility(false), Size(128), DisplayName("Руководитель (Имя)")]
        public string ManagementName { get; set; }

        /// <summary>
        /// Руководитель (Отчество).
        /// </summary>
        [MemberDesignTimeVisibility(false), Size(128), DisplayName("Руководитель (Отчество)")]
        public string ManagementPatronymic { get; set; }

        [MemberDesignTimeVisibility(false), DisplayName("Руководитель (Должность)")]
        public string ManagementPositionString => ManagementPosition?.Name;
        /// <summary>
        /// Руководитель (Должность).
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Position ManagementPosition { get; set; }

        /// <summary>
        /// ОКПО.
        /// </summary>
        [DisplayName("ОКПО"), MemberDesignTimeVisibility(false), Size(32)]
        public string OKPO { get; set; }

        /// <summary>
        /// ОКВЭД.
        /// </summary>
        [DisplayName("ОКВЭД"), MemberDesignTimeVisibility(false), Size(32)]
        public string OKVED { get; set; }

        /// <summary>
        /// ОГРН.
        /// </summary>
        [DisplayName("ОГРН"), MemberDesignTimeVisibility(false), Size(32)]
        public string PSRN { get; set; }
        
        /// <summary>
        /// Дата выдачи ОГРН.
        /// </summary>
        [DisplayName("Дата выдачи ОГРН"), MemberDesignTimeVisibility(false)]
        public DateTime? DatePSRN { get; set; }

        /// <summary>
        /// Телефон.
        /// </summary>
        [DisplayName("Телефон"), MemberDesignTimeVisibility(false)]
        public string Telephone { get; set; }

        public override string ToString()
        {
            if (!string.IsNullOrWhiteSpace(Name))
            {
                return Name;
            }
            else if (!string.IsNullOrWhiteSpace(AbbreviatedName))
            {
                return AbbreviatedName;
            }
            else
            {
                return FullName;
            }
        }
    }
}
