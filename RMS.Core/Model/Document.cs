using DevExpress.Xpo;
using System;
using System.Collections.Generic;

namespace RMS.Core.Model
{
    public class Document : XPObject, IEquatable<Document>
    {
        public Document() { }
        public Document(Session session) : base(session) { }

        [Size(256)]
        [DisplayName("Наименование")]
        public string Name { get; set; }

        [Size(1024)]
        [DisplayName("Описание")]
        public string Description { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Document);
        }

        public bool Equals(Document other)
        {
            return other != null &&
                   Name == other.Name &&
                   Description == other.Description;
        }

        public override int GetHashCode()
        {
            int hashCode = -1598788603;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            return hashCode;
        }

        public override string ToString()
        {
            return Name;
        }

        public static bool operator ==(Document left, Document right)
        {
            return EqualityComparer<Document>.Default.Equals(left, right);
        }

        public static bool operator !=(Document left, Document right)
        {
            return !(left == right);
        }
    }
}
