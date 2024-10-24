using DevExpress.Xpo;
using RMS.Core.Enumerator;

namespace RMS.Core.Model
{
    /// <summary>
    /// Общий словарь.
    /// </summary>
    public class GeneralVocabulary : XPObject
    {
        public GeneralVocabulary() { }
        public GeneralVocabulary(Session session) : base(session) { }

        [DisplayName("Наименование"), Size(256)]
        public string Name { get; set; }

        [DisplayName("Описание"), Size(1024)]
        public string Description { get; set; }

        [DisplayName("Тип")]
        [MemberDesignTimeVisibility(false)]
        public GeneralVocabularyType GeneralVocabularyType { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}