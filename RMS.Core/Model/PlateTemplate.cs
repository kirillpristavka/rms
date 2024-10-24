using DevExpress.Xpo;

namespace RMS.Core.Model
{
    /// <summary>
    /// Шаблон печатной формы.
    /// </summary>
    public class PlateTemplate : XPObject
    {
        public PlateTemplate() { }
        public PlateTemplate(Session session) : base(session) { }

        [DisplayName("Наименование"), Size(256)]
        public string Name { get; set; }

        [DisplayName("Описание"), Size(1024)]
        public string Description { get; set; }

        /// <summary>
        /// Стандартный шаблон для отображения.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public bool IsDefault { get; set; }

        /// <summary>
        /// Текст письма.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public byte[] TextBody { get; set; }

        /// <summary>
        /// Текст письма HTML.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public byte[] HtmlBody { get; set; }

        /// <summary>
        /// Файл Word.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public byte[] FileWord { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
