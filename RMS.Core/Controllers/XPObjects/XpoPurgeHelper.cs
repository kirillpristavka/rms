using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Xpo.Metadata.Helpers;
using System.Collections;

namespace RMS.Core.Controllers.XPObjects
{
    public delegate void ProcessZombieObjectCallback(object obj);
    public delegate void ProcessDeadReferenceCallback(object deadObj, object refObj, XPMemberInfo member);

    public static class XpoPurgeHelper
    {
        public static void FindDeadReferences(Session session, ProcessZombieObjectCallback zombieCallback, ProcessDeadReferenceCallback deadCallback)
        {
            if (zombieCallback == null && deadCallback == null) return;
            foreach (XPClassInfo ci in session.Dictionary.Classes)
            {
                if (ci.GetPersistentMember(GCRecordField.StaticName) == null) continue;
                XPCollection coll = new XPCollection(
                    PersistentCriteriaEvaluationBehavior.BeforeTransaction, session, ci,
                    new NotOperator(new NullOperator(new OperandProperty(GCRecordField.StaticName))), true);

                foreach (object obj in coll)
                {
                    ICollection refs = session.CollectReferencingObjects(obj);
                    if (refs.Count == 0)
                    {
                        if (zombieCallback != null)
                        {
                            zombieCallback(obj);
                        }
                    }
                    else
                    {
                        if (deadCallback != null)
                        {
                            foreach (object robj in refs)
                            {
                                XPClassInfo ciRef = session.Dictionary.GetClassInfo(robj);
                                foreach (XPMemberInfo mi in ciRef.PersistentProperties)
                                {
                                    if (mi.IsPersistent && Equals(ci, mi.ReferenceType))
                                    {
                                        if (Equals(mi.GetValue(robj), obj))
                                        {
                                            deadCallback(obj, robj, mi);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public static void ClearDeadReferences(Session session)
        {
            FindDeadReferences(session, null, new ProcessDeadReferenceCallback(delegate (object deadObj, object refObj, XPMemberInfo member) {
                member.SetValue(refObj, null);
            }));
        }
        public static ICollection FindZombies(Session session)
        {
            ArrayList list = new ArrayList();
            FindDeadReferences(session, (obj) => { list.Add(obj); }, null);
            return list;
        }
    }
}
