using DevExpress.Xpo;

namespace RMS.Core.Model.OKVED
{
    /// <summary>
    /// Раздел ОКВЭД2.
    /// </summary>
    public class SectionOKVED2 : XPObject
    {
        public SectionOKVED2() { }
        public SectionOKVED2(Session session) : base(session) { }

        /// <summary>
        /// Наименование раздела ОКВЭД.
        /// </summary>
        [DisplayName("Код")]
        public string Code { get; set; }

        /// <summary>
        /// Наименование раздела ОКВЭД.
        /// </summary>
        [DisplayName("Наименование"), Size(512)]
        public string Name { get; set; }

        /// <summary>
        /// Описание раздела ОКВЭД.
        /// </summary>
        [DisplayName("Описание"), Size(2048)]
        public string Description { get; set; }

        /// <summary>
        /// Классы ОКВЭД.
        /// </summary>
        [Association, Aggregated, MemberDesignTimeVisibility(false)]
        public XPCollection<ClassOKVED2> ClassesOKVED
        {
            get
            {
                return GetCollection<ClassOKVED2>(nameof(ClassesOKVED));
            }
        }

        public override string ToString()
        {
            return Code;
        }
    }
}