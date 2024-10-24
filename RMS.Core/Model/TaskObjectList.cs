using DevExpress.Xpo;
using System;
using System.Collections.Generic;

namespace RMS.Core.Model
{
    public class TaskObjectList : XPObject, IEquatable<TaskObjectList>
    {
        private TaskObjectList() { }
        public TaskObjectList(Session session) : base(session) { }
                
        /// <summary>
        /// Выполнение задачи.
        /// </summary>
        public bool IsPerformed { get; set; }

        /// <summary>
        /// Задача.
        /// </summary>
        public TaskObject TaskObject { get; set; }

        [Association]
        [MemberDesignTimeVisibility(false)]
        public Task Customer { get; set; }

        public override string ToString()
        {
            return TaskObject?.ToString();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TaskObjectList);
        }

        public bool Equals(TaskObjectList other)
        {
            return other != null &&
                   IsPerformed == other.IsPerformed &&
                   EqualityComparer<TaskObject>.Default.Equals(TaskObject, other.TaskObject);
        }

        public override int GetHashCode()
        {
            int hashCode = 726455516;
            hashCode = hashCode * -1521134295 + IsPerformed.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<TaskObject>.Default.GetHashCode(TaskObject);
            return hashCode;
        }

        public static bool operator ==(TaskObjectList left, TaskObjectList right)
        {
            return EqualityComparer<TaskObjectList>.Default.Equals(left, right);
        }

        public static bool operator !=(TaskObjectList left, TaskObjectList right)
        {
            return !(left == right);
        }
    }
}