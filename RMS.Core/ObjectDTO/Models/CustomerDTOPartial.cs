namespace RMS.Core.ObjectDTO.Models
{
    public class CustomerDTOPartial
    {
        public CustomerDTOPartial(int oid, string name, string defaultName = default)
        {
            Oid = oid;
            Name = name;
            DefaultName = defaultName;
        }

        public void SetDefaultName(string obj)
        {
            DefaultName = obj;
        }

        public int Oid { get; set; }
        public string Name { get; set; }
        public string DefaultName { get; private set; }

        public override string ToString()
        {
            return Name;
        }
    }
}