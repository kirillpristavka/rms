using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RMS.Core.Extensions
{
    /// <summary>
    /// Методы расширений.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Получить описание элемента перечислителя.
        /// </summary>
        /// <param name="genericEnum"></param>
        /// <returns>Описание перечислителя.</returns>
        public static string GetEnumDescription(this Enum genericEnum)
        {
            if (genericEnum is null)
            {
                return default;
            }
            
            Type genericEnumType = genericEnum.GetType();
            MemberInfo[] memberInfo = genericEnumType.GetMember(genericEnum.ToString());
            if ((memberInfo != null && memberInfo.Length > 0))
            {
                var _Attribs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if ((_Attribs != null && _Attribs.Count() > 0))
                {
                    return ((DescriptionAttribute)_Attribs.ElementAt(0)).Description;
                }
            }
            return genericEnum.ToString();
        }

        /// <summary>
        /// Округление Decimal с заданной точностью знаков после запятой [По умолчанию 2 знака].
        /// </summary>
        /// <param name="genericDecimal">Округляемое число.</param>
        /// <param name="characters">Точность округления (количество знаков после запятой).</param>
        /// <returns>Число с указанной точностью.</returns>
        public static decimal GetDecimalRound(this decimal genericDecimal, int characters = 2)
        {
            if (characters < 0)
            {
                characters = 2;
            }

            genericDecimal = decimal.Round(genericDecimal, characters, MidpointRounding.AwayFromZero);
            return genericDecimal;
        }

        public static Dictionary<string, decimal> GetValuesByExcel(string filePath)
        {
            var result = new Dictionary<string, decimal>();

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    do
                    {
                        while (reader.Read())
                        {
                            var inn = reader?.GetValue(0)?.ToString();
                            var value = reader?.GetValue(1)?.ToString();

                            if (!string.IsNullOrWhiteSpace(inn) && inn.Length > 8)
                            {
                                if (decimal.TryParse(value, out decimal valueDecimal))
                                {
                                    if (!result.ContainsKey(inn))
                                    {
                                        result.Add(inn, valueDecimal);
                                    }
                                }
                            }

                        }
                    } while (reader.NextResult());
                }
            }

            return result;
        }

        public static byte[] StringToByte(string msg)
        {
            if (string.IsNullOrWhiteSpace(msg))
            {
                return default;
            }
            else
            {
                return Encoding.Default.GetBytes(msg);
            }
        }

        public static string ByteToString(byte[] msg)
        {
            if (msg == null)
            {
                return default;
            }
            else
            {
                return Encoding.Default.GetString(msg);
            }
        }
    }
}
