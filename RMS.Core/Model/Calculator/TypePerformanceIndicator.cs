using System.ComponentModel;

namespace RMS.Core.Enumerator
{
    /// <summary>
    /// Тип данных для показателей калькулятора.
    /// </summary>
    public enum TypeCalculatorIndicator
    {
        [Description("Расчет на основе фактической стоимости")]
        Value,
        
        [Description("Расчет процента от базовой стоимости")]
        Percent,
        
        [Description("Расчет стоимости на основе количества услуг")]
        Count
    }
}