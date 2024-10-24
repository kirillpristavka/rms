using DevExpress.Xpo;

namespace RMS.Core.Model
{
    /// <summary>
    /// Организационно-правовая форма.
    /// </summary>
    public class FormCorporation : XPObject
    {
        public FormCorporation() { }
        public FormCorporation(Session session) : base(session) { }

        /// <summary>
        /// Код.
        /// </summary>
        [DisplayName("Код"), Size(128)]
        public string Kod { get; set; }

        /// <summary>
        /// Сокращенное наименование.
        /// </summary>
        [DisplayName("Сокращенное наименование"), Size(128)]
        public string AbbreviatedName { get; set; }

        /// <summary>
        /// Полное наименование.
        /// </summary>
        [DisplayName("Сокращенное наименование"), Size(1024)]
        public string FullName { get; set; }

        /// <summary>
        /// Использование для формировании отчетов и налогов.
        /// </summary>
        [DisplayName("Формирование отчетов и налогов")]
        public bool IsUseFormingIndividualEntrepreneursTax { get; set; }

        /// <summary>
        /// Использование для формировании предварительных налогов.
        /// </summary>
        [DisplayName("Формирование предварительных налогов")]
        public bool IsUseFormingPreTax { get; set; }

        /// <summary>
        /// Отчеты.
        /// </summary>
        [Aggregated]
        [Association]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<FormCorporationReport> FormCorporationReports
        {
            get
            {
                return GetCollection<FormCorporationReport>(nameof(FormCorporationReports));
            }
        }
        
        public override string ToString()
        {
            return AbbreviatedName;
        }
    }
}