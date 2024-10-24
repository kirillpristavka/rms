using DevExpress.Xpo;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model.InfoCustomer;
using System;

namespace RMS.Core.Model.Chronicle
{
    public class ChronicleTask: XPObject
    {
        public ChronicleTask() { }
        public ChronicleTask(Session session) : base(session) { }

        public void Fiil(int oid, Task task, User user)
        {
            UserUpdate = user;

            TaskOid = oid;
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
                var result = default(string);
                foreach (var item in task.TaskObjectList)
                {
                    if (item.TaskObject != null)
                    {
                        if (item.IsPerformed)
                        {
                            result += $"[Выполнено] ";
                        }
                        result += $"{item.TaskObject}{Environment.NewLine}";
                    }                    
                }
                TaskObjectListString += result?.Trim();
            }
        }

        /// <summary>
        /// Дата изменения.
        /// </summary>
        [DisplayName("Дата изменения")]
        [System.ComponentModel.DataAnnotations.DisplayFormat(DataFormatString = "{0:dd/MM/yyyy (HH:mm:ss)}")]
        public DateTime DateUpdate { get; set; } = DateTime.Now;

        [DisplayName("Пользователь")]
        public string UserUpdateString => UserUpdate?.ToString();
        /// <summary>
        /// Пользователь внесший изменения.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public User UserUpdate { get; set; }

        [MemberDesignTimeVisibility(false)]
        public int TaskOid { get; set; }
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
        public TypeTask? TypeTask { get; set; }

        /// <summary>
        /// Дата ответа
        /// </summary>
        [DisplayName("Дата ответа")]
        public DateTime? ReplyDate { get; set; }

        /// <summary>
        /// Дата подтверждения.
        /// </summary>
        [DisplayName("Дата подтверждения")]
        public DateTime? ConfirmationDate { get; set; }

        [DisplayName("Клиент")]
        public string CustomerString => Customer?.ToString();

        [Size(256)]
        [DisplayName("Наименование")]
        public string Name { get; set; }

        [Size(1024)]
        [DisplayName("Описание")]
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
        public string DemandNumber { get; set; }

        /// <summary>
        /// Дата фактического подтверждения.
        /// </summary>
        [DisplayName("Подтвердили (факт)")]
        public DateTime? DateConfirmationActual { get; set; }

        /// <summary>
        /// Дата фактической отправки.
        /// </summary>
        [DisplayName("Отправили (факт)")]
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
        [Size(4000)]
        [DisplayName("Список задач")]
        public string TaskObjectListString { get; set; }

        [MemberDesignTimeVisibility(false)]
        public Customer Customer { get; set; }

        /// <summary>
        /// Услуга по задаче.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public AdditionalServices AdditionalServices { get; set; }
    }
}