using DevExpress.Xpo;

namespace RMS.Core.Model
{
    /// <summary>
    /// Коды предпринимательской деятельности ЕНВД.
    /// </summary>
    public class EntrepreneurialActivityCodesUTII : XPObject
    {
        public EntrepreneurialActivityCodesUTII() { }
        public EntrepreneurialActivityCodesUTII(Session session) : base(session) { }

        /// <summary>
        /// Код.
        /// </summary>
        [DisplayName("Код")]
        public string Code { get; set; }

        /// <summary>
        /// Наименование.
        /// </summary>
        [DisplayName("Наименование"), Size(512)]
        public string Name { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        [DisplayName("Описание"), Size(2048)]
        public string Description { get; set; }
                
        public override string ToString()
        {
            return Code;
        }
    }
}
