using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace RMS.ParserReportingSystem.Extensions
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
    }
}
