using DevExpress.Xpo;

namespace RMS.Core.Model.XPO
{
    public class BaseObjectXPO<T> : XPObject
    {
        public BaseObjectXPO() { }
        public BaseObjectXPO(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            TypeName = typeof(T).Name;
        }

        public string TypeName { get; private set; }

        public override string ToString()
        {
            return $"[{Oid}] {TypeName}"?.Trim();
        }
    }
}
