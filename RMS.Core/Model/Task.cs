using DevExpress.Xpo;
using RMS.Core.Controllers.XPObjects;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Interface;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Notifications;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace RMS.Core.Model
{
    /// <summary>
    /// Задача.
    /// </summary>
    public class Task : XPObject, INotification, IProgramEvent, IEquatable<Task>
    {
        private Task() { }
        public Task(Session session) : base(session) { }

        public Task(Task task)
        {
            IsTook = task.IsTook;
            TypeTask = task.TypeTask;
            ReplyDate = task.ReplyDate;
            ConfirmationDate = task.ConfirmationDate;
            Name = task.Name;
            Description = task.Description;
            DemandNumber = task.DemandNumber;
            DateConfirmationActual = task.DateConfirmationActual;
            DateDispathchActual = task.DateDispathchActual;
            Date = task.Date;
            Deadline = task.Deadline;
            DateCompletionActual = task.DateCompletionActual;
            GivenStaff = task.GivenStaff;
            Staff = task.Staff;
            CoExecutor = task.CoExecutor;
            Comment = task.Comment;
            File = task.File;
            TaskStatus = task.TaskStatus;
            GeneralVocabulary = task.GeneralVocabulary;
            GeneralVocabularyDemand = task.GeneralVocabularyDemand;
            Customer = task.Customer;
            AdditionalServices = task.AdditionalServices;

            if (task.TaskObjectList != null)
            {
                foreach (var item in task.TaskObjectList)
                {
                    TaskObjectList.Add(new TaskObjectList(Session)
                    {
                        IsPerformed = item.IsPerformed,
                        Customer = item.Customer,
                        TaskObject = item.TaskObject
                    });
                }
            }
        }

        public Task(Deal deal) : this(deal.Session)
        {
            Name = $"Авто-сформированная задача по сделке [{DateTime.Now.ToString("dd.MM.yyyy (hh:mm:ss)")}]";
            Customer = deal.Customer;
            Staff = deal.Staff;

            try
            {
                using (var taskObjects = new XPCollection<TaskObject>(deal.Session))
                {
                    taskObjects?.Reload();

                    foreach (var taskObject in taskObjects.Where(w => w.IsUse))
                    {
                        TaskObjectList.Add(new TaskObjectList(Session) { TaskObject = taskObject });
                    }
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        /// <summary>
        /// Задача принята исполнителем.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public bool IsTook { get; set; }

        [DisplayName("Статус задачи")]
        public string StatusString => TaskStatus?.ToString() ?? string.Empty;

        [DisplayName("Вид задачи")]
        public string TypeTaskString => TypeTask?.GetEnumDescription();

        /// <summary>
        /// Тип задачи.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public TypeTask? TypeTask { get; set; } = Enumerator.TypeTask.Task;

        /// <summary>
        /// Дата ответа
        /// </summary>
        [DisplayName("Дата ответа")]
        [MemberDesignTimeVisibility(false)]
        public DateTime? ReplyDate { get; set; }

        /// <summary>
        /// Дата подтверждения.
        /// </summary>
        [DisplayName("Дата подтверждения")]
        [MemberDesignTimeVisibility(false)]
        public DateTime? ConfirmationDate { get; set; }

        [DisplayName("Клиент")]
        public string CustomerString => Customer?.ToString();

        [Size(256)]
        [DisplayName("Наименование")]
        [MemberDesignTimeVisibility(false)]
        public string Name { get; set; }

        [Size(1024)]
        [DisplayName("Описание")]
        [MemberDesignTimeVisibility(false)]
        public string Description { get; set; }

        [DisplayName("Описание")]
        public string MyProperty
        {
            get
            {
                var result = default(string);

                if (TypeTask is Enumerator.TypeTask.Demand)
                {
                    if (GeneralVocabularyDemand != null)
                    {
                        result += $"[{GeneralVocabularyDemand}]";
                    }
                }

                result += $" {Description}";

                if (!string.IsNullOrWhiteSpace(Name))
                {
                    result += $" {Name}";
                }

                return result.Trim();
            }
        }

        /// <summary>
        /// Номер требования.
        /// </summary>
        [Size(256)]
        [DisplayName("Номер")]
        [MemberDesignTimeVisibility(false)]
        public string DemandNumber { get; set; }

        /// <summary>
        /// Дата фактического подтверждения.
        /// </summary>
        [DisplayName("Подтвердили (факт)")]
        [MemberDesignTimeVisibility(false)]
        public DateTime? DateConfirmationActual { get; set; }

        /// <summary>
        /// Дата фактической отправки.
        /// </summary>
        [DisplayName("Отправили (факт)")]
        [MemberDesignTimeVisibility(false)]
        public DateTime? DateDispathchActual { get; set; }

        /// <summary>
        /// Дата постановки.
        /// </summary>
        [DisplayName("Дата постановки")]
        public DateTime? Date { get; set; } = DateTime.Now;

        /// <summary>
        /// Дата окончания.
        /// </summary>
        [DisplayName("Дата окончания")]
        public DateTime? Deadline { get; set; }

        /// <summary>
        /// Дата фактического выполнения.
        /// </summary>
        [DisplayName("Дата фактического выполнения")]
        public DateTime? DateCompletionActual { get; set; }

        [DisplayName("Постановщик")]
        public string GivenStaffString => GivenStaff?.ToString();

        /// <summary>
        /// Постановщик задачи.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Staff GivenStaff { get; set; }

        /// <summary>
        /// Ответственный.
        /// </summary>
        [DisplayName("Ответственный")]
        public string StaffString => Staff?.ToString();
        /// <summary>
        /// Ответственный.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Staff Staff { get; set; }

        /// <summary>
        /// Соисполнитель.
        /// </summary>
        [DisplayName("Соисполнитель")]
        public string CoExecutorString => CoExecutor?.ToString();
        /// <summary>
        /// Соисполнитель.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Staff CoExecutor { get; set; }

        /// <summary>
        /// Комментарий.
        /// </summary>
        [Size(4000)]
        [MemberDesignTimeVisibility(false)]
        public string Comment { get; set; }

        [DisplayName("Комментарий")]
        public string CommentString
        {
            get
            {
                var result = default(string);

                if (TypeTask == Enumerator.TypeTask.SourceDocuments)
                {
                    if (GeneralVocabulary != null)
                    {
                        result += GeneralVocabulary.Description;
                    }
                }

                result += $" {Comment}";

                return result;
            }
        }

        /// <summary>
        /// Файл.
        /// </summary>
        [Aggregated]
        [MemberDesignTimeVisibility(false)]
        public File File { get; set; }

        /// <summary>
        /// Статус задачи.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public StatusTask StatusTask { get; set; }

        /// <summary>
        /// Статус задачи.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public TaskStatus TaskStatus { get; set; }

        /// <summary>
        /// Общий словарь.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public GeneralVocabulary GeneralVocabulary { get; set; }

        /// <summary>
        /// Общий словарь для требований.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public GeneralVocabulary GeneralVocabularyDemand { get; set; }

        /// <summary>
        /// Задача.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        [DisplayName("Задача")]
        public string TaskString
        {
            get
            {
                var result = default(string);

                if (!string.IsNullOrWhiteSpace(Name))
                {
                    result += Name;
                }

                if (Staff != null)
                {
                    result += $" [Задачу принял {Staff}]";
                }

                if (Date != null)
                {
                    result += $" от {Convert.ToDateTime(Date)}";
                }

                return result.Trim();
            }
        }

        /// <summary>
        /// Задача.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        [DisplayName("Задача")]
        public string TaskGivenString
        {
            get
            {
                var result = default(string);

                if (!string.IsNullOrWhiteSpace(Name))
                {
                    result += Name;
                }

                if (GivenStaff != null)
                {
                    result += $" [Задачу дал {GivenStaff}]";
                }

                if (Date != null)
                {
                    result += $" от {Convert.ToDateTime(Date)}";
                }

                return result.Trim();
            }
        }

        /// <summary>
        /// Список задач.
        /// </summary>
        [Aggregated]
        [Association]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<TaskObjectList> TaskObjectList
        {
            get
            {
                return GetCollection<TaskObjectList>(nameof(TaskObjectList));
            }
        }

        [Association]
        [MemberDesignTimeVisibility(false)]
        public Customer Customer { get; set; }

        /// <summary>
        /// Услуга по задаче.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public AdditionalServices AdditionalServices { get; set; }

        public override string ToString()
        {
            var result = default(string);

            if (!string.IsNullOrWhiteSpace(TypeTaskString))
            {
                result += $"[{TypeTaskString}]";
            }

            if (!string.IsNullOrWhiteSpace(Description))
            {
                result += $" {Description.Trim()}";
            }

            if (Customer != null)
            {
                result += $"{Environment.NewLine}{Customer}";
            }

            return result;
        }

        /// <summary>
        /// Получить наименование для задач на сегодня.
        /// </summary>
        /// <returns></returns>
        public string GetTaskNowString()
        {
            var result = default(string);

            if (!string.IsNullOrWhiteSpace(TypeTaskString))
            {
                result += $"[{TypeTaskString}]";
            }

            if (Customer != null)
            {
                result += $" {Customer}";
            }

            return result;
        }

        public Notification GetNotification(TypeNotification typeNotification)
        {
            if (Deadline is DateTime deadline)
            {
                var name = default(string);

                if (TypeTask != null)
                {
                    name += $"[{TypeTask.GetEnumDescription()}]";
                }

                if (Customer != null)
                {
                    name += $"{Environment.NewLine}Клиент: {Customer}";
                }

                if (TaskStatus != null)
                {
                    name += $"{Environment.NewLine}Статус: {TaskStatus.ToString().ToLower()}";
                }

                if (GivenStaff != null)
                {
                    name += $"{Environment.NewLine}Поставил: {GivenStaff}";
                }

                if (Staff != null)
                {
                    name += $"{Environment.NewLine}Исполнитель: {Staff}";
                }

                if (!string.IsNullOrWhiteSpace(Description))
                {
                    name += $"{Environment.NewLine}Описание:{Description}";
                }

                return new Notification(typeNotification, "Задачи", deadline, name, Oid, typeof(Task));
            }
            else
            {
                return default;
            }
        }

        public async void ObjCreate()
        {
            var cloneHelper = new CloneIXPSimpleObjectHelper(Session, Session);
            var cloneObj = cloneHelper.Clone(this, false, true);
            if (cloneObj != null)
            {
                cloneObj.Date = DateTime.Now;
                cloneObj.TaskStatus = await new XPQuery<TaskStatus>(Session)
                    ?.FirstOrDefaultAsync(f =>
                        f.IsDefault || (f.Name != null && f.Name.Contains("Нов")));

                cloneObj.Save();
            }
        }

        public string Message()
        {
            var result = default(string);

            if (TypeTask != null)
            {
                result += $"[{TypeTask.GetEnumDescription()}]";
            }

            if (Customer != null)
            {
                result += $" Клиент: [{Customer}]";
            }

            if (TaskStatus != null)
            {
                result += $" Статус: [{TaskStatus.ToString().ToLower()}]";
            }

            if (!string.IsNullOrWhiteSpace(Description))
            {
                result += $" Описание:{Description}";
            }

            return result;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Task);
        }

        public bool Equals(Task other)
        {
            return other != null &&
                   IsTook == other.IsTook &&
                   TypeTask == other.TypeTask &&
                   ReplyDate == other.ReplyDate &&
                   ConfirmationDate == other.ConfirmationDate &&
                   Name == other.Name &&
                   Description == other.Description &&
                   DemandNumber == other.DemandNumber &&
                   DateConfirmationActual == other.DateConfirmationActual &&
                   DateDispathchActual == other.DateDispathchActual &&
                   Date == other.Date &&
                   Deadline == other.Deadline &&
                   DateCompletionActual == other.DateCompletionActual &&
                   GivenStaff?.Oid == other.GivenStaff?.Oid &&
                   Staff?.Oid == other.Staff?.Oid &&
                   CoExecutor?.Oid == other.CoExecutor?.Oid &&
                   Comment == other.Comment &&
                   File?.Oid == other.File?.Oid &&
                   TaskStatus?.Oid == other.TaskStatus?.Oid &&
                   GeneralVocabulary?.Oid == other.GeneralVocabulary?.Oid &&
                   GeneralVocabularyDemand?.Oid == other.GeneralVocabularyDemand?.Oid &&
                   Customer?.Oid == other.Customer?.Oid &&
                   AdditionalServices?.Oid == other.AdditionalServices?.Oid &&
                   TaskObjectList.EqualTo(other.TaskObjectList);
        }

        public static bool operator ==(Task left, Task right)
        {
            return EqualityComparer<Task>.Default.Equals(left, right);
        }

        public static bool operator !=(Task left, Task right)
        {
            return !(left == right);
        }
    }

    public static class IEnumerableExtensions
    {
        public static bool EqualTo<T>(this IEnumerable<T> enumerable, IEnumerable<T> other)
        {
            //reference equal 
            if (other == enumerable)
            {
                return true;
            }

            if (other == null)
            {
                return false;
            }

            var enumerableSorted = enumerable.ToArray();
            var otherSorted = other.ToArray();

            //No need to iterate over items if lengths are not equal
            if (otherSorted.Length != enumerableSorted.Length)
            {
                return false;
            }

            Array.Sort(enumerableSorted);
            Array.Sort(otherSorted);

            return !enumerableSorted.Where((t, i) => t.Equals(otherSorted[i])).Any();
        }
    }
}
