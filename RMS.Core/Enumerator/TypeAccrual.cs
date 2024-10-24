using System.ComponentModel;

namespace RMS.Core.Enumerator
{
    /// <summary>
    /// Тип начисления.
    /// </summary>
    public enum TypeAccrual
    {
        /// <summary>
        /// ЗП.
        /// </summary>
        [Description("ЗП")]
        Salary = 0,

        /// <summary>
        /// Аванс.
        /// </summary>
        [Description("Аванс")]
        Advance = 1
    }
}