using DevExpress.Xpo;
using RMS.Core.Model.InfoCustomer.Billing;

namespace RMS.Core.Model
{
    /// <summary>
    /// Система налогообложения.
    /// </summary>
    public class TaxSystem : XPObject
    {
        public TaxSystem() { }
        public TaxSystem(Session session) : base(session) { }

        /// <summary>
        /// Наименование.
        /// </summary>
        [DisplayName("Наименование"), Size(256)]
        public string Name { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        [DisplayName("Описание"), Size(1024)]
        public string Description { get; set; }

        /// <summary>
        /// Шкала для расчета
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public CalculationScale CalculationScale { get; set; }

        /// <summary>
        /// Отчеты.
        /// </summary>
        [Aggregated]
        [Association]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<TaxSystemReport> TaxSystemReports
        {
            get
            {
                return GetCollection<TaxSystemReport>(nameof(TaxSystemReports));
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}