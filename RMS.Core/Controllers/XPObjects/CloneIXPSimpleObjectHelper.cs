using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using System.Collections;
using System.Collections.Generic;

namespace RMS.Core.Controllers.XPObjects
{
    public class CloneIXPSimpleObjectHelper
    {
        /// <summary>
        /// A dictionary containing objects from the source session as key and objects from the 
        /// target session as values
        /// </summary>
        /// <returns></returns>
        private Dictionary<object, object> _clonedObjects;
        private Session _sourceSession;
        private Session _targetSession;

        /// <summary>
        /// Initializes a new instance of the CloneIXPSimpleObjectHelper class.
        /// </summary>
        public CloneIXPSimpleObjectHelper(Session source, Session target)
        {
            this._clonedObjects = new Dictionary<object, object>();
            this._sourceSession = source;
            this._targetSession = target;
        }

        public T Clone<T>(T source) where T : IXPSimpleObject
        {
            return Clone<T>(source, _targetSession, false);
        }

        public T Clone<T>(T source, bool synchronize) where T : IXPSimpleObject
        {
            return (T)Clone(source as IXPSimpleObject, _targetSession, synchronize, false);
        }

        public T Clone<T>(T source, bool synchronize, bool child_synchronize_no) where T : IXPSimpleObject
        {
            return (T)Clone(source as IXPSimpleObject, _targetSession, synchronize, child_synchronize_no);
        }

        public object Clone(IXPSimpleObject source)
        {
            return Clone(source, _targetSession, false, false);
        }

        public object Clone(IXPSimpleObject source, bool synchronize)
        {
            return Clone(source, _targetSession, synchronize, false);
        }
        
        public T Clone<T>(T source, Session targetSession, bool synchronize) where T : IXPSimpleObject
        {
            return (T)Clone(source as IXPSimpleObject, targetSession, synchronize, false);
        }

        /// <summary>
        /// Clones and / or synchronizes the given IXPSimpleObject.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="targetSession"></param>
        /// <param name="synchronize">If set to true, reference properties are only cloned in case
        /// the reference object does not exist in the targetsession. Otherwise the exising object will be
        /// reused and synchronized with the source. Set this property to false when knowing at forehand 
        /// that the targetSession will not contain any of the objects of the source.</param>
        /// <returns></returns>
        public object Clone(IXPSimpleObject source, Session targetSession, bool synchronize, bool child_synchronize_no)
        {
            if (source == null)
                return null;

            if (_clonedObjects.ContainsKey(source))
                return _clonedObjects[source];

            XPClassInfo targetClassInfo = targetSession.GetClassInfo(source.GetType());

            object clone = null;

            if (synchronize)
                clone = targetSession.GetObjectByKey(targetClassInfo, source.Session.GetKeyValue(source));

            if (clone == null)
                clone = targetClassInfo.CreateNewObject(targetSession);

            _clonedObjects.Add(source, clone);

            foreach (XPMemberInfo m in targetClassInfo.PersistentProperties)
            {
                if (m is DevExpress.Xpo.Metadata.Helpers.ServiceField || m.IsKey)
                    continue;

                object val;

                if (m.ReferenceType != null && !(child_synchronize_no))
                {
                    object createdByClone = m.GetValue(clone);

                    if ((createdByClone != null) && synchronize == false)
                        val = createdByClone;
                    else
                        val = Clone((IXPSimpleObject)m.GetValue(source), targetSession, synchronize);  // не делать этого, когда не нужно клонировать детей
                }
                else
                    val = m.GetValue(source);

                m.SetValue(clone, val);
            }

            foreach (XPMemberInfo m in targetClassInfo.CollectionProperties)
            {
                if (m.HasAttribute(typeof(AggregatedAttribute)))
                {
                    XPBaseCollection col = (XPBaseCollection)m.GetValue(clone);
                    XPBaseCollection colSource = (XPBaseCollection)m.GetValue(source);

                    foreach (IXPSimpleObject obj in new ArrayList(colSource))
                        col.BaseAdd(Clone(obj, targetSession, synchronize));
                }
            }

            return clone;
        }

        public object Clone_Global(Session sess, IXPSimpleObject o_o)
        {
            _ = o_o.GetType();

            return null;
        }
    }
}
