namespace RMS.Core.ModelSbis
{
    public class Glossary
    {
        public Glossary(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; private set; }
        public object Value { get; private set; }
        public string ParentName { get; private set; }

        public void SetParentName(string parentName)
        {
            ParentName = parentName;
        }

        public override string ToString()
        {
            return $"{Name} : {Value}";
        }
    }
}
