using DevExpress.Xpo;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using System;
using System.Linq;

namespace RMS.Core.Model.Calculator
{
    /// <summary>
    /// Показатели для калькулятора.
    /// </summary>
    public class CalculatorIndicator : XPObject
    {
        public CalculatorIndicator() { }
        public CalculatorIndicator(Session session) : base(session) { }

        [NonPersistent]
        [DisplayName("Использование")]
        public bool IsUse { get; set; }

        /// <summary>
        /// Наименование.
        /// </summary>
        [Size(256)]
        [DisplayName("Наименование")]
        public string Name { get; set; }
        
        /// <summary>
        /// Описание.
        /// </summary>
        [Size(1024)]
        [DisplayName("Описание")]
        [MemberDesignTimeVisibility(false)]
        public string Description { get; set; }
        
        /// <summary>
        /// Использование при формировании.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public bool IsUseWhenForming { get; set; } = true;
        
        /// <summary>
        /// Тип данных для показателя работы.
        /// </summary>
        [DisplayName("Тип записи")]
        public TypeCalculatorIndicator TypeCalculatorIndicator { get; set; }
        
        private string value;
        [DisplayName("Цена")]
        public string Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = GetStringValue(value);
            }
        }
        
        private string GetStringValue(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return "0";
            }

            var result = default(string);
            
            var splits = message.Replace('.', ',').Split(',');
            if (splits.Length == 1)
            {
                var array = message.Where(x => Char.IsDigit(x)).ToArray();
                result = string.Join(null, array);
            }
            else if (splits.Length == 2)
            {
                var array1 = splits[0].Where(x => Char.IsDigit(x)).ToArray();
                var array2 = splits[1].Where(x => Char.IsDigit(x)).ToArray();

                result = $"{string.Join(null, array1)},{string.Join(null, array2)}";

                if (decimal.TryParse(result, out decimal decimalResult))
                {
                    result = decimalResult.GetDecimalRound().ToString();
                }
            }

            if (TypeCalculatorIndicator == TypeCalculatorIndicator.Percent)
            {
                result += "%";
            }

            return result;
        }

        public decimal GetDecimalValue(string obj = default)
        {
            if (string.IsNullOrWhiteSpace(obj))
            {
                obj = value;
            }
            
            var result = default(string);

            var splits = obj.Replace('.', ',').Split(',');
            if (splits.Length == 1)
            {
                var array = obj.Where(x => Char.IsDigit(x)).ToArray();
                result = string.Join(null, array);
            }
            else if (splits.Length == 2)
            {
                var array1 = splits[0].Where(x => Char.IsDigit(x)).ToArray();
                var array2 = splits[1].Where(x => Char.IsDigit(x)).ToArray();

                result = $"{string.Join(null, array1)},{string.Join(null, array2)}";                
            }

            if (decimal.TryParse(result, out decimal decimalResult))
            {
                return decimalResult.GetDecimalRound();
            }

            return 0;
        }
        
        private int count;
        [NonPersistent]
        [DisplayName("Количество")]
        public int Count
        {
            get
            {
                if (TypeCalculatorIndicator == TypeCalculatorIndicator.Count)
                {
                    return count;
                }

                return 1;
            }
            set
            {
                count = value;
            }
        }

        private decimal basePrice;
        
        [NonPersistent]
        [DisplayName("Стоимость")]
        public decimal Sum
        {
            get
            {
                if (IsUse)
                {
                    var result = default(decimal);
                    if (TypeCalculatorIndicator == TypeCalculatorIndicator.Percent)
                    {
                        result = (GetDecimalValue() * basePrice / 100).GetDecimalRound();
                    }
                    else if (TypeCalculatorIndicator == TypeCalculatorIndicator.Count)
                    {
                        result = (GetDecimalValue() * count).GetDecimalRound();
                    }
                    else
                    {
                        result = (GetDecimalValue()).GetDecimalRound();
                    }

                    result = result.GetDecimalRound(0);
                    result += 0.00M;

                    return result;
                }

                return 0;
            }
        }
        
        public void SetBasePrice(object obj)
        {
            if (obj is null)
            {
                basePrice = 0;
            }
            else
            {
                basePrice = GetDecimalValue(obj.ToString());
            }            
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
