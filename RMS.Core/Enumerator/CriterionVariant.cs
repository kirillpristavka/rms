using System.ComponentModel;

namespace RMS.Core.Enumerator
{
    /// <summary>
    /// Вариант критерия.
    /// </summary>
    public enum CriterionVariant
    {
        /// <summary>
        /// И.
        /// </summary>
        [Description("И")]
        And = 0,

        /// <summary>
        /// Или.
        /// </summary>
        [Description("Или")]
        Or = 1
    }
}
