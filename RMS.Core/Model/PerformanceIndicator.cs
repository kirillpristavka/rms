using DevExpress.Xpo;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model.Reports;
using System;
using System.Linq;

namespace RMS.Core.Model
{
    /// <summary>
    /// Показатель работы.
    /// </summary>
    public class PerformanceIndicator : XPObject
    {
        public PerformanceIndicator() { }
        public PerformanceIndicator(Session session) : base(session) { }

        [DisplayName("Группа")]
        public string GroupPerformanceIndicatorString => GroupPerformanceIndicator?.ToString();

        [DisplayName("Наименование"), Size(256)]
        public string Name { get; set; }

        /// <summary>
        /// Сокращенное наименование.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        [DisplayName("Сокращенное наименование"), Size(256)]
        public string AbbreviatedName { get; set; }

        [DisplayName("Описание"), Size(1024)]
        public string Description { get; set; }

        /// <summary>
        /// Единица измерения.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        [DisplayName("Единица измерения")]
        public string Unit { get; set; }

        /// <summary>
        /// Использование при формировании счета.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public bool IsUseWhenFormingAnInvoice { get; set; } = true;

        /// <summary>
        /// Использовать при формировании информации по сотрудникам.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public bool IsUseWhenGeneratingInformationOnEmployees { get; set; } = true;

        /// <summary>
        /// Группа показателя.
        /// </summary>
        [Association, MemberDesignTimeVisibility(false)]
        public GroupPerformanceIndicator GroupPerformanceIndicator { get; set; }

        /// <summary>
        /// Тип данных для показателя работы.
        /// </summary>
        [DisplayName("Тип записи")]
        public TypePerformanceIndicator TypePerformanceIndicator { get; set; }

        [Persistent]
        [MemberDesignTimeVisibility(false)]
        [DisplayName("Значения")]
        public string ValueString 
        { 
            get 
            {
                var result = default(string);

                if (value != null)
                {
                    if (TypePerformanceIndicator == TypePerformanceIndicator.Percent)
                    {
                        result = $"{Convert.ToInt32(value)}%";
                    }
                    else
                    {
                        result = $"{Convert.ToDecimal(value).GetDecimalRound()}";
                    }
                }
                
                return result;
            } 
        }
        
        decimal? value;
        /// <summary>
        /// Стоимость.
        /// </summary>
        [DisplayName("Значение")]
        public decimal? Value
        {
            get
            {
                return value;
            }
            set
            {
                if (value is decimal dValue)
                {
                    this.value = dValue.GetDecimalRound();
                }
                else
                {
                    this.value = null;
                }
            }
        }

        /// <summary>
        /// Переменные входимости.
        /// </summary>
        [Association, MemberDesignTimeVisibility(false), Aggregated]
        public XPCollection<PerformanceIndicatorValue> PerformanceIndicatorValues
        {
            get
            {
                return GetCollection<PerformanceIndicatorValue>(nameof(PerformanceIndicatorValues));
            }
        }
        
        /// <summary>
        /// Получить значение которое удовлетворяет количеству.
        /// </summary>
        /// <param name="count">Количество.</param>
        /// <returns>Значение удовлетворяющее условию отбора.</returns>
        public PerformanceIndicatorValue GetPerformanceIndicatorValue(int count)
        {
            return PerformanceIndicatorValues.FirstOrDefault(f => f.NumberWith <= count && f.NumberOf >= count);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
