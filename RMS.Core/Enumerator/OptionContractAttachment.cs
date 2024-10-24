using System.ComponentModel;

namespace RMS.Core.Enumerator
{
    /// <summary>
    /// Вариант приложения к договору.
    /// </summary>
    public enum OptionContractAttachment
    {
        /// <summary>
        /// Перечень передаваемых документов и сведений.
        /// </summary>
        [Description("Перечень передаваемых документов и сведений")]
        ListTransmittedDocumentsAndInformation = 0,

        /// <summary>
        /// Перечень оказываемых услуг и расчет стоимости бухгалтерского обслуживания.
        /// </summary>
        [Description("Перечень оказываемых услуг и расчет стоимости бухгалтерского обслуживания")]
        ListRenderedServicesAndAccountingServices = 1
    }
}
