using System.ComponentModel;

namespace RMS.Core.Enumerator
{
    /// <summary>
    /// Наличие.
    /// </summary>
    public enum Availability
    {
        /// <summary>
        /// Имеется.
        /// </summary>
        [Description("Имеется")]
        True = 0,

        /// <summary>
        /// Нет.
        /// </summary>
        [Description("Нет")]
        False = 1
    }
}