using DevExpress.Xpo;

namespace RMS.Core.Model
{
    /// <summary>
    /// Физическое лицо.
    /// </summary>
    public class Individual : XPObject
    {
        public Individual() { }
        public Individual(Session session) : base(session) { }

        [DisplayName("Фамилия"), Size(128)]
        public string Surname { get; set; }

        [DisplayName("Имя"), Size(128)]
        public string Name { get; set; }

        [DisplayName("Отчество"), Size(128)]
        public string Patronymic { get; set; }

        [Persistent, MemberDesignTimeVisibility(false), Size(256)]
        public string IndividualString
        {
            get
            {
                return this.ToString();
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
    }
}
