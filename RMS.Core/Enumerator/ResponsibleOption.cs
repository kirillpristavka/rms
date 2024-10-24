using System.ComponentModel;

namespace RMS.Core.Enumerator
{
    /// <summary>
    /// Вариант ответственного сотрудника.
    /// </summary>
    public enum ResponsibleOption
    {
        /// <summary>
        /// Ответственный за банк.
        /// </summary>
        [Description("Ответственный за банк")]
        BankResponsible = 0,

        /// <summary>
        /// Ответственный за первичные документы.
        /// </summary>
        [Description("Ответственный за первичные документы")]
        PrimaryResponsible = 1,

        /// <summary>
        /// Ответственный за банк.
        /// </summary>
        [Description("Ответственный за банк")]
        AccountantResponsible = 2       
    }
}
