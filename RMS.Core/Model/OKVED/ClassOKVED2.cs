using DevExpress.Xpo;
using System;
using System.Collections.Generic;

namespace RMS.Core.Model.OKVED
{
    /// <summary>
    /// Класс ОКВЭД2.
    /// </summary>
    public class ClassOKVED2 : XPObject, IEquatable<ClassOKVED2>
    {
        public ClassOKVED2() { }
        public ClassOKVED2(Session session) : base(session) { }

        /// <summary>
        /// Наименование раздела ОКВЭД.
        /// </summary>
        [DisplayName("Код")]
        public string Code { get; set; }

        /// <summary>
        /// Наименование раздела ОКВЭД.
        /// </summary>
        [DisplayName("Наименование"), Size(512)]
        public string Name { get; set; }

        /// <summary>
        /// Описание раздела ОКВЭД.
        /// </summary>
        [DisplayName("Описание"), Size(2048)]
        public string Description { get; set; }

        [Association, MemberDesignTimeVisibility(false)]
        public SectionOKVED2 SectionOKVED { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as ClassOKVED2);
        }

        public bool Equals(ClassOKVED2 other)
        {
            return other != null &&
                   base.Equals(other) &&
                   Oid == other.Oid &&
                   Code == other.Code;
        }

        public override int GetHashCode()
        {
            int hashCode = 105768661;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + Oid.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Code);
            return hashCode;
        }

        public override string ToString()
        {
            var result = default(string);
            if (!string.IsNullOrWhiteSpace(Code) && !string.IsNullOrWhiteSpace(Name))
            {
                result = $"[{Code}] - {Name}";
            }
            else if (!string.IsNullOrWhiteSpace(Code))
            {
                result = $"[{Code}]";
            }
            else if (!string.IsNullOrWhiteSpace(Name))
            {
                result = $"[{Name}]";
            }
            else
            {
                result = $"Класс {nameof(ClassOKVED2)} не содержит кода и наименования";
            }
            return result;
        }
        
        public static bool operator ==(ClassOKVED2 left, ClassOKVED2 right)
        {
            return EqualityComparer<ClassOKVED2>.Default.Equals(left, right);
        }

        public static bool operator !=(ClassOKVED2 left, ClassOKVED2 right)
        {
            return !(left == right);
        }
    }
}