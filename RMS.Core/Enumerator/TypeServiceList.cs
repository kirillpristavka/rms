using System.ComponentModel;

namespace RMS.Core.Enumerator
{
    /// <summary>
    /// Тип услуги.
    /// </summary>
    public enum TypeServiceList
    {
        /// <summary>
        /// Систем налогообложения.
        /// </summary>
        [Description("Систем налогообложения")]
        TaxSystem = 0,

        /// <summary>
        /// Вид деятельности.
        /// </summary>
        [Description("Вид деятельности")]
        KindActivity = 1,

        /// <summary>
        /// Первичные документы (кто вносит в 1С).
        /// </summary>
        [Description("Первичные документы (кто вносит в 1С)")]
        SourceDocuments = 2,

        /// <summary>
        /// Банковские счета и операции.
        /// </summary>
        [Description("Банковские счета и операции")]
        BankAccountsAndTransactions = 3,

        /// <summary>
        /// Наличие валютных операций.
        /// </summary>
        [Description("Наличие валютных операций")]
        AvailabilityCurrencyTransactions = 4,

        /// <summary>
        /// Наличие ВЭД.
        /// </summary>
        [Description("Наличие ВЭД")]
        PresenceForeignTradeActivities = 5,

        /// <summary>
        /// Продажа товаров с разными ставками НДС.
        /// </summary>
        [Description("Продажа товаров с разными ставками НДС")]
        SaleOfGoodsWithDifferentVATRates = 6
    }
}