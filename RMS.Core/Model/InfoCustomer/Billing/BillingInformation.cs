using DevExpress.Xpo;
using RMS.Core.Extensions;

namespace RMS.Core.Model.InfoCustomer.Billing
{
    /// <summary>
    /// Платежная информация.
    /// </summary>
    public class BillingInformation : XPObject
    {
        public BillingInformation() { }
        public BillingInformation(Session session) : base(session) { }

        /// <summary>
        /// Использование фиксированной базовой ставки.
        /// </summary>
        public bool IsFixedBaseRate { get; set; }

        private decimal? fixedBaseRateValue;
        /// <summary>
        /// Значение фиксированной базовой ставки.
        /// </summary>
        public decimal? FixedBaseRateValue
        {
            get
            {
                return fixedBaseRateValue;
            }
            set
            {
                if (value is decimal dValue)
                {
                    fixedBaseRateValue = dValue.GetDecimalRound();
                }
            }
        }

        /// <summary>
        /// Использование расчета по операциям.
        /// </summary>
        public bool IsSettlementOfTransactions { get; set; } = true;

        /// <summary>
        /// Шкала расчета базы.
        /// Если шкала не задана, то используется стандартная шкала для системы налогообложения.
        /// </summary>
        public CalculationScale CalculationScale { get; set; }

        /// <summary>
        /// Составление первичных документов.
        /// </summary>
        public bool IsPreparationPrimaryDocuments { get; set; }

        private decimal? preparationPrimaryDocumentsValue;
        /// <summary>
        /// Ставка для составление первичных документов.
        /// </summary>
        public decimal? PreparationPrimaryDocumentsValue
        {
            get
            {
                return preparationPrimaryDocumentsValue;
            }
            set
            {
                if (value is decimal dValue)
                {
                    preparationPrimaryDocumentsValue = dValue.GetDecimalRound();
                }
            }
        }

        /// <summary>
        /// Обслуживание клиентского банка.
        /// </summary>
        public bool IsCustomerBankService { get; set; }

        private decimal? customerBankServiceValue;
        /// <summary>
        /// Ставка обслуживания клиентского банка.
        /// </summary>
        public decimal? CustomerBankServiceValue
        {
            get
            {
                return customerBankServiceValue;
            }
            set
            {
                if (value is decimal dValue)
                {
                    customerBankServiceValue = dValue.GetDecimalRound();
                }
            }
        }

        /// <summary>
        /// Показатели, чьи значения будут заменены.
        /// </summary>
        [Association, MemberDesignTimeVisibility(false), Aggregated]
        public XPCollection<BillingPerformanceIndicator> BillingPerformanceIndicators
        {
            get
            {
                return GetCollection<BillingPerformanceIndicator>(nameof(BillingPerformanceIndicators));
            }
        }

        /// <summary>
        /// Использовать группы для расчета.
        /// </summary>
        public bool IsBillingGroupPerformanceIndicators { get; set; }

        /// <summary>
        /// Группы для расчета.
        /// </summary>
        [Association, MemberDesignTimeVisibility(false), Aggregated]
        public XPCollection<BillingGroupPerformanceIndicator> BillingGroupPerformanceIndicators
        {
            get
            {
                return GetCollection<BillingGroupPerformanceIndicator>(nameof(BillingGroupPerformanceIndicators));
            }
        }
    }
}
