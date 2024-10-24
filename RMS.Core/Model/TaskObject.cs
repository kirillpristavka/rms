using DevExpress.Xpo;
using Newtonsoft.Json;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace RMS.Core.Model
{
    /// <summary>
    /// Задача для списка.
    /// </summary>
    public class TaskObject : XPObject, IEquatable<TaskObject>
    {
        private TaskObject() { }
        public TaskObject(Session session) : base(session) { }

        /// <summary>
        /// Используется ли для формирования списка задач.
        /// </summary>
        public bool IsUse { get; set; }

        [DisplayName("Наименование")]
        [Size(256)]
        public string Name { get; set; }

        [DisplayName("Описание")]
        [Size(1024)]
        public string Description { get; set; }

        [DisplayName("Список типов")]
        public List<TypeTasks> TypeTasksList => GetTypeTasks();

        /// <summary>
        /// Типы задач в которых используется задача.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public byte[] TypeTasks { get; set; }
        
        public List<TypeTasks> GetTypeTasks()
        {
            var result = new List<TypeTasks>();
            
            if (TypeTasks != null)
            {
                var json = Model.TypeTasks.GetStringOfByte(TypeTasks);
                var list = Model.TypeTasks.Deserialize<List<TypeTasks>>(json);
                result = list;
            }

            return result;
        }

        public void SaveTypeTasks(List<TypeTasks> list)
        {
            if (list != null && list.Count > 0)
            {
                var typeTasks = list;
                var json = Model.TypeTasks.Serialize(typeTasks);
                TypeTasks = Model.TypeTasks.GetByteOfString(json);
            }
            else
            {
                TypeTasks = null;
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TaskObject);
        }

        public bool Equals(TaskObject other)
        {
            return other != null &&
                   IsUse == other.IsUse &&
                   Name == other.Name &&
                   Description == other.Description &&
                   EqualityComparer<List<TypeTasks>>.Default.Equals(TypeTasksList, other.TypeTasksList) &&
                   EqualityComparer<byte[]>.Default.Equals(TypeTasks, other.TypeTasks);
        }

        public override int GetHashCode()
        {
            int hashCode = 217232215;
            hashCode = hashCode * -1521134295 + IsUse.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<TypeTasks>>.Default.GetHashCode(TypeTasksList);
            hashCode = hashCode * -1521134295 + EqualityComparer<byte[]>.Default.GetHashCode(TypeTasks);
            return hashCode;
        }

        public static bool operator ==(TaskObject left, TaskObject right)
        {
            return EqualityComparer<TaskObject>.Default.Equals(left, right);
        }

        public static bool operator !=(TaskObject left, TaskObject right)
        {
            return !(left == right);
        }
    }
    
    public class TypeTasks : IEquatable<TypeTasks>
    {
        public TypeTasks() { }
        public TypeTasks(TypeTask typeTask)
        {
            TypeTask = typeTask;
        }
        
        /// <summary>
        /// Типы задач в которых используется задача.
        /// </summary>
        public TypeTask? TypeTask { get; set; }

        public override string ToString()
        {
            return TypeTask?.GetEnumDescription();
        }


        public static string Serialize<T>(T data)
        {
            if (data == null)
            {
                return default;
            }
            else
            {
                return JsonConvert.SerializeObject(data);
            }
        }

        public static T Deserialize<T>(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return default;
            }
            else
            {
                try
                {
                    return JsonConvert.DeserializeObject<T>(json);
                }
                catch (Exception ex)
                {
                    RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                    return default;
                }                
            }
        }

        /// <summary>
        /// Получение строки из байтов.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string GetStringOfByte(byte[] data)
        {
            if (data is null)
            {
                return default;
            }
            else
            {
                return Encoding.UTF8.GetString(data);
            }
        }

        /// <summary>
        /// Получение байтов из строки.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] GetByteOfString(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                return default;
            }
            else
            {
                return Encoding.UTF8.GetBytes(data);
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TypeTasks);
        }

        public bool Equals(TypeTasks other)
        {
            return other != null &&
                   TypeTask == other.TypeTask;
        }

        public override int GetHashCode()
        {
            return -877305212 + TypeTask.GetHashCode();
        }

        public static bool operator ==(TypeTasks left, TypeTasks right)
        {
            return EqualityComparer<TypeTasks>.Default.Equals(left, right);
        }

        public static bool operator !=(TypeTasks left, TypeTasks right)
        {
            return !(left == right);
        }
    }
}