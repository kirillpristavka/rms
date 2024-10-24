using System.ComponentModel;

namespace RMS.Core.Model.Calculator
{
    public enum Operation
    {
        /// <summary>
        /// Сложение.
        /// </summary>
        [Description("Сложение")]
        Addition,

        /// <summary>
        /// Вычитание.
        /// </summary>
        [Description("Вычитание")]
        Subtraction,

        /// <summary>
        /// Умножение.
        /// </summary>
        [Description("Умножение")]
        Multiplication,

        /// <summary>
        /// Деление.
        /// </summary>
        [Description("Деление")]
        Division
    }
}
