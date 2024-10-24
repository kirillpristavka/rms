using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout.Utils;
using RMS.Core.Controllers;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model;
using RMS.Core.Model.Chronicle;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Reports;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Telegram.Bot;
using TelegramBotRMS.Core.Models;

namespace RMS.UI.Forms.Directories
{
    public partial class TaskEdit : XtraForm
    {
        private Session _session { get; }
        private Customer Customer { get; }
        private string Comment { get; }
        private TypeTask TypeTask { get; }

        public Task Task { get; private set; }
        private bool IsReadOnly { get; }
        public List<Task> Tasks { get; set; }
        public bool IsMassChange { get; set; }

        public TaskEdit(Session session)
        {
            XPBaseObject.AutoSaveOnEndEdit = false;
            
            InitializeComponent();

            foreach (TypeTask item in Enum.GetValues(typeof(TypeTask)))
            {
                cmbTypeTask.Properties.Items.Add(item.GetEnumDescription());
            }

            foreach (StatusTask item in Enum.GetValues(typeof(StatusTask)))
            {
                cmbStatus.Properties.Items.Add(item.GetEnumDescription());
            }
            cmbStatus.SelectedIndex = 0;

            if (session is null)
            {
                _session = DatabaseConnection.GetWorkSession();                
            }
            else
            {
                _session = session;
            }            
        }

        public TaskEdit(Session session, object @obj, TypeTask typeTask) : this(session)
        {
            if (@obj is string comment && !string.IsNullOrWhiteSpace(comment))
            {
                Comment = comment;
            }
            
            TypeTask = typeTask;
        }

        public TaskEdit(Session session, TypeTask typeTask) : this(session)
        {
            TypeTask = typeTask;
        }

        public TaskEdit(Session session, List<Task> task) : this(session)
        {
            IsMassChange = true;
            _session = session;
            Tasks = task;
        }

        public TaskEdit(Customer customer, bool isReadOnly = false) : this(customer.Session)
        {
            Customer = customer;
            IsReadOnly = isReadOnly;
            _session = customer.Session;
        }

        public TaskEdit(Task task, bool isReadOnly) : this(task.Session)
        {
            Task = task;
            IsReadOnly = isReadOnly;
            Customer = task.Customer;
            _session = task.Session;
        }

        public TaskEdit(Task task) : this(task.Session)
        {
            Task = task;
            IsReadOnly = true;
            Customer = task.Customer;
            _session = task.Session;
        }

        public TaskEdit(ReportChange reportChange, string comment) : this(reportChange.Session)
        {            
            Customer = reportChange.Customer;
            _session = reportChange.Session;
            TypeTask = TypeTask.Task;
            Comment = comment;
        }

        private async void CheckingExecutor()
        {
            try
            {
                var statusName = Task?.TaskStatus?.Name;
                if (!string.IsNullOrWhiteSpace(statusName) && statusName.Equals(TaskStatus.TaskNewName))
                {
                    var user = DatabaseConnection.User;

                    if (Task.Staff?.Oid == user?.Staff?.Oid)
                    {
                        Task.IsTook = true;
                        Task.TaskStatus = await _session.FindObjectAsync<TaskStatus>(new BinaryOperator(nameof(TaskStatus.Name), TaskStatus.TaskTook));
                        Task.Save();

                        var chronicleEvents = new ChronicleEvents(_session)
                        {
                            Act = Act.ACCEPTANCE_TASK,
                            Date = DateTime.Now,
                            Name = Act.ACCEPTANCE_TASK.GetEnumDescription(),
                            User = _session.GetObjectByKey<User>(user.Oid)
                        };
                        chronicleEvents.Save();
                    }
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }            
        }

        private bool isEditTaskForm = false;
        private async System.Threading.Tasks.Task SetAccessRights()
        {
            try
            {
                using (var uof = new UnitOfWork())
                {
                    var user = await uof.GetObjectByKeyAsync<User>(DatabaseConnection.User.Oid);
                    if (user != null)
                    {
                        var accessRights = user.AccessRights;
                        if (accessRights != null)
                        {
                            isEditTaskForm = accessRights.IsEditTaskForm;
                        }                        
                    }
                }

                btnSave.Enabled = isEditTaskForm;
                btnTaskObjectListAdd.Enabled = isEditTaskForm;
                btnTaskObjectListDel.Enabled = isEditTaskForm;

                CustomerEdit.CloseButtons(btnCustomer, isEditTaskForm);
                CustomerEdit.CloseButtons(btnGeneralVocabularyDemand, isEditTaskForm);
                CustomerEdit.CloseButtons(btnGeneralVocabulary, isEditTaskForm);
                CustomerEdit.CloseButtons(btnTaskStatus, isEditTaskForm);
                CustomerEdit.CloseButtons(btnGiv, isEditTaskForm);
                CustomerEdit.CloseButtons(btnStaff, isEditTaskForm);
                CustomerEdit.CloseButtons(btnCoExecutor, isEditTaskForm);
                CustomerEdit.CloseButtons(btnFile, isEditTaskForm);
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async void TaskEdit_Load(object sender, EventArgs e)
        {
            await SetAccessRights();

            if (Task is null)
            {
                Task = new Task(_session)
                {
                    TaskStatus = await _session.FindObjectAsync<TaskStatus>(new BinaryOperator(nameof(TaskStatus.IsDefault), true)),
                    GivenStaff = await _session.GetObjectByKeyAsync<Staff>(DatabaseConnection.User?.Staff?.Oid),
                    TypeTask = TypeTask
                };

                if (Customer != null)
                {
                    Task.Staff = Customer.AccountantResponsible;
                }

                if (!string.IsNullOrWhiteSpace(Comment))
                {
                    Task.Comment = Comment;
                }
            }
            
            Task?.TaskObjectList?.Reload();

            if (Task.Oid <= 0)
            {
                var taskObjects = new XPCollection<TaskObject>(_session);
                taskObjects?.Reload();
                
                foreach (var taskObject in taskObjects.Where(w => w.IsUse && w.GetTypeTasks().FirstOrDefault(f => f.TypeTask == Task.TypeTask) != null))
                {
                    Task.TaskObjectList.Add(new TaskObjectList(_session) { TaskObject = taskObject });
                }
            }
            else
            {
                CheckingExecutor();
            }
            
            btnCustomer.Properties.ReadOnly = IsReadOnly;

            if (Customer is null)
            {
                btnCustomer.Properties.ReadOnly = false;
            }

            cmbTypeTask.EditValue = Task.TypeTask?.GetEnumDescription();
            btnTaskStatus.EditValue = Task.TaskStatus;
            txtName.Text = Task.Name;
            txtDescription.Text = Task.Description;
            dateDate.EditValue = Task.Date ?? DateTime.Now.Date;
            dateDeadline.EditValue = Task.Deadline;
            btnStaff.EditValue = Task.Staff;
            btnCoExecutor.EditValue = Task.CoExecutor;
            btnFile.EditValue = Task.File;
            cmbStatus.SelectedIndex = (int)Task.StatusTask;
            btnCustomer.EditValue = Customer;
            btnGiv.EditValue = Task.GivenStaff;
            dateConfirmationDate.EditValue = Task.ConfirmationDate;
            dateReplyDate.EditValue = Task.ReplyDate;

            checkedListBoxTaskObjectList.DataSource = Task.TaskObjectList;
            checkedListBoxTaskObjectList.CheckMember = nameof(TaskObjectList.IsPerformed);
            memoComment.EditValue = Task.Comment;
            
            btnGeneralVocabulary.EditValue = Task.GeneralVocabulary;
            
            btnGeneralVocabularyDemand.EditValue = Task.GeneralVocabularyDemand;
            txtDemandNumber.EditValue = Task.DemandNumber;
            dateConfirmationActual.EditValue = Task.DateConfirmationActual;
            dateDispathchActual.EditValue = Task.DateDispathchActual;
            
            dateCompletionActual.EditValue = Task.DateCompletionActual;

            if (Customer == null)
            {
                btnCustomer.EditValue = Customer;
            }

            if (IsMassChange)
            {
                btnTaskObjectListAdd.Enabled = false;
                btnTaskObjectListDel.Enabled = false;
                checkedListBoxTaskObjectList.Enabled = false;

                cmbTypeTask.Properties.ReadOnly = true;
                txtName.Properties.ReadOnly = true;
                cmbStatus.Properties.ReadOnly = true;
                btnGiv.Properties.ReadOnly = true;
                txtDescription.Properties.ReadOnly = true;
                btnCustomer.Properties.ReadOnly = true;
                btnFile.Properties.ReadOnly = true;
                dateConfirmationDate.Properties.ReadOnly = true;
                dateReplyDate.Properties.ReadOnly = true;
                btnGeneralVocabulary.Properties.ReadOnly = true;
                btnGeneralVocabularyDemand.Properties.ReadOnly = true;
                txtDemandNumber.Properties.ReadOnly = true;
                dateConfirmationActual.Properties.ReadOnly = true;
                dateDispathchActual.Properties.ReadOnly = true;

                dateDate.EditValue = null;
                //dateCompletionActual.Properties.ReadOnly = true;
                //dateDate.Properties.ReadOnly = true;
                //dateDeadline.Properties.ReadOnly = true;
                //btnStaff.Properties.ReadOnly = true;
                //btnCoExecutor.Properties.ReadOnly = true;
            }

            var generalVocabularyType = GetGeneralVocabularyType();
            BVVGlobal.oFuncXpo.ButtoneEditDoubleClick<GeneralVocabulary>(_session, btnGeneralVocabularyDemand, cls_App.ReferenceBooks.GeneralVocabulary, new BinaryOperator(nameof(GeneralVocabulary.GeneralVocabularyType), generalVocabularyType), isEnable: isEditTaskForm);
            BVVGlobal.oFuncXpo.ButtoneEditDoubleClick<GeneralVocabulary>(_session, btnGeneralVocabulary, cls_App.ReferenceBooks.GeneralVocabulary, new BinaryOperator(nameof(GeneralVocabulary.GeneralVocabularyType), generalVocabularyType), isEnable: isEditTaskForm);
            
            BVVGlobal.oFuncXpo.ButtoneEditDoubleClick<TaskStatus>(_session, btnTaskStatus, cls_App.ReferenceBooks.TaskStatus, isEnable: isEditTaskForm);
            BVVGlobal.oFuncXpo.ButtoneEditDoubleClick<Staff>(_session, btnGiv, cls_App.ReferenceBooks.Staff, isEnable: isEditTaskForm);
            BVVGlobal.oFuncXpo.ButtoneEditDoubleClick<Customer>(_session, btnCustomer, cls_App.ReferenceBooks.Customer, isEnable: isEditTaskForm);
            BVVGlobal.oFuncXpo.ButtoneEditDoubleClick<Staff>(_session, btnStaff, cls_App.ReferenceBooks.Staff, isEnable: isEditTaskForm);
            BVVGlobal.oFuncXpo.ButtoneEditDoubleClick<Staff>(_session, btnCoExecutor, cls_App.ReferenceBooks.Staff, isEnable: isEditTaskForm);

            if (Task.AdditionalServices != null)
            {
                layoutControlItemAdditionalServices.Visibility = LayoutVisibility.Always;
            }

            _tempTask = new Task(Task);
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var isSave = false;

                if (IsMassChange)
                {
                    isSave = SaveMassTask();
                }
                else
                {
                    isSave = await SaveTask();
                }

                if (isSave)
                {
                    Close();
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private bool SaveMassTask()
        {
            try
            {
                var taskStatus = default(TaskStatus);
                if (btnTaskStatus.EditValue is TaskStatus _taskStatus)
                {
                    taskStatus = _taskStatus;
                }

                var comment = memoComment.Text;

                var objDateCompletionActual = default(DateTime?);
                if (dateCompletionActual.EditValue is DateTime _objDateCompletionActual)
                {
                    objDateCompletionActual = _objDateCompletionActual;
                }

                var objDate = default(DateTime?);
                if (dateDate.EditValue is DateTime _objDate)
                {
                    objDate = _objDate;
                }

                var objDateDeadline = default(DateTime?);
                if (dateDeadline.EditValue is DateTime _objDateDeadline)
                {
                    objDateDeadline = _objDateDeadline;
                }

                var staff = default(Staff);
                if (btnStaff.EditValue is Staff _staff)
                {
                    staff = _staff;
                }

                var coExecutor = default(Staff);
                if (btnCoExecutor.EditValue is Staff _coExecutor)
                {
                    coExecutor = _coExecutor;
                }

                foreach (var task in Tasks)
                {
                    task.TaskStatus = taskStatus;

                    if (!string.IsNullOrWhiteSpace(comment))
                    {
                        task.Comment = $"{task.Comment} {comment}"?.Trim();
                    }

                    if (objDate != null)
                    {
                        task.Date = objDate;
                    }

                    if (objDateDeadline != null)
                    {
                        task.Deadline = objDateDeadline;
                    }

                    if (objDateCompletionActual != null)
                    {
                        task.DateCompletionActual = objDateCompletionActual;
                    }

                    if (staff != null)
                    {
                        task.Staff = staff;
                    }

                    if (coExecutor != null)
                    {
                        task.CoExecutor = coExecutor;
                    }


                    task.Save();
                }

                return true;
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                return false;
            }
        }

        private Task _tempTask;

        private async System.Threading.Tasks.Task<bool> SaveTask()
        {
            var messageTG = default(string);
            
            var typeTask = default(TypeTask?);
            foreach (TypeTask item in Enum.GetValues(typeof(TypeTask)))
            {
                if (item.GetEnumDescription().Equals(cmbTypeTask.Text))
                {
                    typeTask = item;
                    break;
                }
            }

            if (typeTask == TypeTask.Demand)
            {
                
            }
            else
            {
                if (dateDeadline.EditValue is DateTime deadline)
                {
                    if (Task.Deadline != deadline)
                    {
                        if (Task.Deadline != null)
                        {
                            messageTG += $"<u>Дата окончания</u> изменена с [{((DateTime)Task.Deadline).ToShortDateString()}] на [{deadline.ToShortDateString()}]{Environment.NewLine}";
                        }
                        else
                        {
                            messageTG += $"<u>Дата окончания</u> изменена с пустого значения на [{deadline.ToShortDateString()}]{Environment.NewLine}";
                        }                        
                    }
                    Task.Deadline = deadline;
                }
                else
                {
                    XtraMessageBox.Show("Сохранение не возможно без указания даты дедлайна.",
                                        "Ошибка сохранения",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);
                    dateDeadline.Focus();
                    return false;
                }
            }

            if (!string.IsNullOrWhiteSpace(Task.Name) && Task.Name != txtName.Text)
            {
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    messageTG += $"<u>Наименование</u> изменено с [{Task.Name}] на пустое значение{Environment.NewLine}";
                }
                else
                {
                    messageTG += $"<u>Наименование</u> изменено с [{Task.Name}] на [{txtName.Text}]{Environment.NewLine}";
                }                
            }            
            Task.Name = txtName.Text;

            if (!string.IsNullOrWhiteSpace(Task.Description) && Task.Description != txtDescription.Text)
            {
                if (string.IsNullOrWhiteSpace(txtDescription.Text))
                {
                    messageTG += $"<u>Описание</u> изменено с [{Task.Description}] на пустое значение{Environment.NewLine}";
                }
                else
                {
                    messageTG += $"<u>Описание</u> изменено с [{Task.Description}] на [{txtDescription.Text}]{Environment.NewLine}";
                }
            }
            Task.Description = txtDescription.Text;

            if (!string.IsNullOrWhiteSpace(Task.Comment) && Task.Comment != memoComment.Text)
            {
                if (string.IsNullOrWhiteSpace(memoComment.Text))
                {
                    messageTG += $"<u>Комментарий</u> изменен с [{Task.Comment}] на пустое значение{Environment.NewLine}";
                }
                else
                {
                    messageTG += $"<u>Комментарий</u> изменен с [{Task.Comment}] на [{memoComment.Text}]{Environment.NewLine}";
                }
            }
            Task.Comment = memoComment.Text;

            if (dateDate.EditValue is DateTime date)
            {
                if (Task.Date != date)
                {
                    if (Task.Date != null)
                    {
                        messageTG += $"<u>Дата постановки</u> изменена с [{((DateTime)Task.Date).ToShortDateString()}] на [{date.ToShortDateString()}]{Environment.NewLine}";
                    }
                    else
                    {
                        messageTG += $"<u>Дата постановки</u> изменена с пустого значения на [{date.ToShortDateString()}]{Environment.NewLine}";
                    }                   
                }
                Task.Date = date;
            }
            else
            {
                if (Task.Date != null)
                {
                    messageTG += $"<u>Дата постановки</u> изменена на пустое значение {Environment.NewLine}";
                }
                Task.Date = null;
            }

            if (cmbStatus.EditValue != null)
            {
                if (Task.StatusTask != (StatusTask)cmbStatus.SelectedIndex)
                {
                    messageTG += $"<u>Статус задачи</u> изменен с [{Task.StatusTask.GetEnumDescription()}] на [{((StatusTask)cmbStatus.SelectedIndex).GetEnumDescription()}] {Environment.NewLine}";
                }
                Task.StatusTask = (StatusTask)cmbStatus.SelectedIndex;
            }
                        
            if (Task.TypeTask != typeTask)
            {
                if (Task.TypeTask != null)
                {
                    messageTG += $"<u>Тип задачи</u> изменен с [{Task.TypeTask.GetEnumDescription()}] на [{typeTask.GetEnumDescription()}] {Environment.NewLine}";
                }                
            }
            Task.TypeTask = typeTask;

            if (btnGeneralVocabulary.EditValue is GeneralVocabulary generalVocabulary)
            {
                Task.GeneralVocabulary = generalVocabulary;
            }
            else
            {
                Task.GeneralVocabulary = null;
            }
            
            if (btnTaskStatus.EditValue is TaskStatus taskStatus)
            {
                if (Task.TaskStatus != taskStatus)
                {
                    messageTG += $"<u>Статус задачи</u> изменен с [{Task.TaskStatus}] на [{taskStatus}]{Environment.NewLine}";
                }
                Task.TaskStatus = taskStatus;

                if (!taskStatus.Equals(TaskStatus.TaskNewName))
                {
                    Task.IsTook = true;
                }
            }
            else
            {
                if (Task.TaskStatus != null)
                {
                    messageTG += $"<u>Статус задачи</u> изменен на пустое значение {Environment.NewLine}";
                }
                Task.TaskStatus = null;
            }
            
            var staff = btnStaff.EditValue as Staff;
            if (staff != null)
            {
                if (Task.Staff != staff)
                {
                    messageTG += $"<u>Ответственный</u> сотрудник изменен с [{Task.Staff}] на [{staff}]{Environment.NewLine}";
                }
                Task.Staff = staff;
            }
            else
            {
                if (Task.Staff != null)
                {
                    messageTG += $"<u>Ответственный</u> сотрудник изменен на пустое значение {Environment.NewLine}";
                }
                Task.Staff = null;
            }

            var coExecutor = btnCoExecutor.EditValue as Staff;
            if (coExecutor != null)
            {
                if (Task.CoExecutor != coExecutor)
                {
                    messageTG += $"<u>Соисполнитель</u> изменен с [{Task.CoExecutor}] на [{coExecutor}]{Environment.NewLine}";
                }
                Task.CoExecutor = coExecutor;
            }
            else
            {
                if (Task.CoExecutor != null)
                {
                    messageTG += $"<u>Соисполнитель</u> изменен на пустое значение {Environment.NewLine}";
                }
                Task.CoExecutor = null;
            }

            var customer = btnCustomer.EditValue as Customer;
            if (customer != null)
            {
                if (Task.Customer != customer)
                {
                    messageTG += $"<u>Клиент</u> изменен с [{Task.Customer}] на [{customer}]{Environment.NewLine}";
                }
                Task.Customer = customer;
            }
            else
            {
                if (Task.Customer != null)
                {
                    messageTG += $"<u>Клиент</u> изменен на пустое значение {Environment.NewLine}";
                }
                Task.Customer = null;
            }

            var file = btnFile.EditValue as File;
            if (file != null)
            {
                Task.File = file;
            }
            else
            {
                Task.File = null;
            }

            var givenStaff = btnGiv.EditValue as Staff;
            if (givenStaff != null)
            {
                if (Task.GivenStaff != givenStaff)
                {
                    messageTG += $"<u>Постановщик</u> задачи изменен с [{Task.Customer}] на [{givenStaff}]{Environment.NewLine}";
                }
                Task.GivenStaff = givenStaff;
            }
            else
            {
                if (Task.GivenStaff != null)
                {
                    messageTG += $"<u>Постановщик</u> задачи изменен на пустое значение {Environment.NewLine}";
                }
                Task.GivenStaff = null;
            }

            if (dateConfirmationDate.EditValue is DateTime confirmationDate)
            {
                if (Task.ConfirmationDate != confirmationDate)
                {
                    if (Task.ConfirmationDate != null)
                    {
                        messageTG += $"<u>Дата подтверждения</u> изменена с [{((DateTime)Task.ConfirmationDate).ToShortDateString()}] на [{confirmationDate.ToShortDateString()}]{Environment.NewLine}";
                    }
                    else
                    {
                        messageTG += $"<u>Дата подтверждения</u> изменена с пустого значения на [{confirmationDate.ToShortDateString()}]{Environment.NewLine}";
                    }                    
                }
                Task.ConfirmationDate = confirmationDate;
            }
            else
            {
                if (Task.ConfirmationDate != null)
                {
                    messageTG += $"<u>Дата подтверждения</u> изменена на пустое значение {Environment.NewLine}";
                }
                Task.ConfirmationDate = null;
            }

            if (dateReplyDate.EditValue is DateTime replyDate)
            {
                if (Task.ReplyDate != replyDate)
                {
                    if (Task.ReplyDate != null)
                    {
                        messageTG += $"<u>Дата ответа</u> изменена с [{((DateTime)Task.ReplyDate).ToShortDateString()}] на [{replyDate.ToShortDateString()}]{Environment.NewLine}";
                    }
                    else
                    {
                        messageTG += $"<u>Дата ответа</u> изменена с пустого значения на [{replyDate.ToShortDateString()}]{Environment.NewLine}";
                    }                    
                }
                Task.ReplyDate = replyDate;
            }
            else
            {
                if (Task.ReplyDate != null)
                {
                    messageTG += $"<u>Дата ответа</u> изменена на пустое значение {Environment.NewLine}";
                }
                Task.ReplyDate = null;
            }

            if (!string.IsNullOrWhiteSpace(Task.DemandNumber) && Task.DemandNumber != txtDemandNumber.Text)
            {
                if (string.IsNullOrWhiteSpace(txtDemandNumber.Text))
                {
                    messageTG += $"<u>Номер требования</u> изменен с [{Task.DemandNumber}] на пустое значение{Environment.NewLine}";
                }
                else
                {
                    messageTG += $"<u>Номер требования</u> изменен с [{Task.DemandNumber}] на [{txtDemandNumber.Text}]{Environment.NewLine}";
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(txtDemandNumber.Text) && Task.DemandNumber != txtDemandNumber.Text)
                {
                    messageTG += $"<u>Номер требования</u> изменен с пустого значения на [{txtDemandNumber.Text}]{Environment.NewLine}";
                }                
            }
            Task.DemandNumber = txtDemandNumber.Text;
            
            if (dateConfirmationActual.EditValue is DateTime confirmationActual)
            {
                if (Task.DateConfirmationActual != confirmationActual)
                {
                    if (Task.DateConfirmationActual != null)
                    {
                        messageTG += $"<u>Дата фактического подтверждения</u> изменена с [{((DateTime)Task.DateConfirmationActual).ToShortDateString()}] на [{confirmationActual.ToShortDateString()}]{Environment.NewLine}";
                    }
                    else
                    {
                        messageTG += $"<u>Дата фактического подтверждения</u> изменена с пустого значения на [{confirmationActual.ToShortDateString()}]{Environment.NewLine}";
                    }                    
                }
                Task.DateConfirmationActual = confirmationActual;
            }
            else
            {
                if (Task.DateConfirmationActual != null)
                {
                    messageTG += $"<u>Дата фактического подтверждения</u> изменена на пустое значение {Environment.NewLine}";
                }
                Task.DateConfirmationActual = null;
            }

            if (dateDispathchActual.EditValue is DateTime dispathchActual)
            {
                if (Task.DateDispathchActual != dispathchActual)
                {
                    if (Task.DateDispathchActual != null)
                    {
                        messageTG += $"<u>Дата фактической отправки</u> изменена с [{((DateTime)Task.DateDispathchActual).ToShortDateString()}] на [{dispathchActual.ToShortDateString()}]{Environment.NewLine}";
                    }
                    else
                    {
                        messageTG += $"<u>Дата фактической отправки</u> изменена с пустого значения на [{dispathchActual.ToShortDateString()}]{Environment.NewLine}";
                    }
                }
                Task.DateDispathchActual = dispathchActual;
            }
            else
            {
                if (Task.DateDispathchActual != null)
                {
                    messageTG += $"<u>Дата фактической отправки</u> изменена на пустое значение {Environment.NewLine}";
                }
                Task.DateDispathchActual = null;
            }

            if (dateCompletionActual.EditValue is DateTime completionActua)
            {
                if (Task.DateCompletionActual != completionActua)
                {
                    if (Task.DateCompletionActual != null)
                    {
                        messageTG += $"<u>Дата фактического исполнения</u> изменена с [{((DateTime)Task.DateCompletionActual).ToShortDateString()}] на [{completionActua.ToShortDateString()}]{Environment.NewLine}";
                    }
                    else
                    {
                        messageTG += $"<u>Дата фактического исполнения</u> изменена с пустого значения на [{completionActua.ToShortDateString()}]{Environment.NewLine}";
                    }
                }
                Task.DateCompletionActual = completionActua;
            }
            else
            {
                if (Task.DateCompletionActual != null)
                {
                    messageTG += $"<u>Дата фактического исполнения</u> изменена на пустое значение {Environment.NewLine}";
                }
                Task.DateCompletionActual = null;
            }

            if (btnGeneralVocabularyDemand.EditValue is GeneralVocabulary generalVocabularyDemand)
            {
                Task.GeneralVocabularyDemand = generalVocabularyDemand;
            }
            else
            {
                Task.GeneralVocabularyDemand = null;
            }

            if (Customer != null)
            {
                Customer.Tasks.Add(Task);
            }

            //TODO: временное решение, в дальнейшем наверное необходимо будет переделать.
            if (Task?.TaskObjectList != null && Task?.TaskObjectList.Count > 0 && Task?.TaskObjectList.FirstOrDefault(f => !f.IsPerformed) is null)
            {
                Task.StatusTask = StatusTask.Done;
            }

            var isNewTask = true;

            if (Task.Oid > 0)
            {
                isNewTask = false;
            }
            
            Task.Save();
            
            if (staff == coExecutor)
            {
                await SendMessageTelegram(Task, staff, isNewTask, messageTG).ConfigureAwait(false);
            }
            else
            {
                await SendMessageTelegram(Task, staff, isNewTask, messageTG).ConfigureAwait(false);
                await SendMessageTelegram(Task, coExecutor, isNewTask, messageTG).ConfigureAwait(false);
            }

            if (Task.Oid > 0)
            {
                if (!_tempTask.Equals(Task))
                {
                    var userOid = DatabaseConnection.User?.Oid ?? -1;
                    var chronicle = new ChronicleTask(_session);
                    chronicle.Fiil(Task.Oid, _tempTask, await new XPQuery<User>(_session).FirstOrDefaultAsync(f => f.Oid == userOid));
                    chronicle.Save();
                }
            }

            return true;
        }

        public static async System.Threading.Tasks.Task SendMessageTelegram(Task task, Staff staff, bool isNewTask, string message = default)
        {
            try
            {
                if (task is null)
                {
                    return;
                }

                var status = task.StatusString;
                if (!string.IsNullOrWhiteSpace(status)
                    && (status.Equals("Снята задача") || status.Equals("Выполнена")))
                {
                    return;
                }

                var isSend = false;
                
                var givenStaff = DatabaseConnection.User?.Staff ?? task.GivenStaff;
                var customer = task.Customer;

                var client = TelegramBot.GetTelegramBotClient(task.Session);                
                
                if (staff != null && staff.TelegramUserId != null)
                {
                    staff.Reload();
                    var text = $"[OID]: {task.Oid}{Environment.NewLine}";
                                        
                    if (isNewTask)
                    {
                        text += $"Получена новая задача";
                        
                        if (task.TypeTask != null)
                        {
                            text += $"{Environment.NewLine}<u>Тип задачи</u>: {task.TypeTaskString}";
                        }

                        if (customer != null)
                        {
                            text += $"{Environment.NewLine}<u>Клиент</u>: {customer}";
                        }

                        if (!string.IsNullOrWhiteSpace(task.Description))
                        {
                            text += $"{Environment.NewLine}<u>Описание</u>: {task.Description}";
                        }

                        text += $"{Environment.NewLine}";
                        
                        isSend = true;
                    }
                    else
                    {
                        text += $"Изменены параметры задачи:";

                        if (customer != null)
                        {
                            var isAddText = true;
                            if (!string.IsNullOrWhiteSpace(message) && message.Contains("Клиент"))
                            {
                                isAddText = false;
                            }

                            if (isAddText)
                            {
                                text += $"{Environment.NewLine}<u>Клиент</u>: {customer}";
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(task.Description))
                        {
                            var isAddText = true;
                            if (!string.IsNullOrWhiteSpace(message) && message.Contains("Описание"))
                            {
                                isAddText = false;
                            }

                            if (isAddText)
                            {
                                text += $"{Environment.NewLine}<u>Описание</u>: {task.Description}";
                            }                            
                        }

                        if (!string.IsNullOrWhiteSpace(message))
                        {
                            text += $"{Environment.NewLine}{message}";
                            isSend = true;
                        }
                    }

                    if (givenStaff != null)
                    {
                        text += $"Сотрудник применивший изменения: <u>{givenStaff}</u>{Environment.NewLine}";
                    }

                    if (isSend)
                    {
                        await client.SendTextMessageAsync(staff.TelegramUserId, text, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
                    }                    
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCustomer_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e?.Button?.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }
            
            cls_BaseSpr.ButtonEditButtonClickBase<Customer>(_session, buttonEdit, (int)cls_App.ReferenceBooks.Customer, 1, null, null, false, null, string.Empty, false, true);

            if (buttonEdit.EditValue is Customer customer && btnStaff.EditValue is null)
            {
                btnStaff.EditValue = customer.AccountantResponsible;
            }
        }

        private void btnStaff_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e?.Button?.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }
            
            cls_BaseSpr.ButtonEditButtonClickBase<Staff>(_session, buttonEdit, (int)cls_App.ReferenceBooks.Staff, 1, new NullOperator(nameof(Staff.DateDismissal)), null, false, null, string.Empty, false, true);
        }        

        private void btnCoExecutor_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e?.Button?.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }
            
            cls_BaseSpr.ButtonEditButtonClickBase<Staff>(_session, buttonEdit, (int)cls_App.ReferenceBooks.Staff, 1, new NullOperator(nameof(Staff.DateDismissal)), null, false, null, string.Empty, false, true);
        }

        private void btnFile_DoubleClick(object sender, EventArgs e)
        {
            btnFile_ButtonPressed(sender, null);
        }

        private async void btnFile_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e?.Button?.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            if (buttonEdit.EditValue is File file)
            {
                try
                {
                    using (var fbd = new XtraFolderBrowserDialog() { Description = "Сохранение файла" })
                    {
                        if (fbd.ShowDialog() == DialogResult.OK)
                        {
                            var fullPathFile = file.WriteFile(fbd.SelectedPath);

                            if (!string.IsNullOrWhiteSpace(fullPathFile))
                            {
                                if (XtraMessageBox.Show("Файл успешно сохранен. Открыть директорию?", string.Empty, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                                {
                                    Process.Start("explorer", fbd.SelectedPath);
                                }

                                Process.Start(fullPathFile);
                            }
                            else
                            {
                                XtraMessageBox.Show("Ошибка сохранения файла, возможно такой файл уже имеется в директории", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    await LoggerController.WriteLogBaseAsync(ex.ToString());

                    using (var fbd = new FolderBrowserDialog() { Description = "Сохранение файла" })
                    {
                        if (fbd.ShowDialog() == DialogResult.OK)
                        {
                            var fullPathFile = file.WriteFile(fbd.SelectedPath);

                            if (!string.IsNullOrWhiteSpace(fullPathFile))
                            {
                                if (XtraMessageBox.Show("Файл успешно сохранен. Открыть директорию?", string.Empty, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                                {
                                    Process.Start("explorer", fbd.SelectedPath);
                                }

                                Process.Start(fullPathFile);
                            }
                            else
                            {
                                XtraMessageBox.Show("Ошибка сохранения файла, возможно такой файл уже имеется в директории", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            else
            {
                try
                {
                    using (var ofd = new XtraOpenFileDialog())
                    {
                        if (ofd.ShowDialog() == DialogResult.OK)
                        {
                            file = new File(_session, ofd.FileName);
                            buttonEdit.EditValue = file;
                        }
                    }
                }
                catch (Exception ex)
                {
                    await LoggerController.WriteLogBaseAsync(ex.ToString());

                    using (var ofd = new OpenFileDialog())
                    {
                        if (ofd.ShowDialog() == DialogResult.OK)
                        {
                            file = new File(_session, ofd.FileName);
                            buttonEdit.EditValue = file;
                        }
                    }
                }
            }
        }

        private void btnGiv_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e?.Button?.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }
            
            cls_BaseSpr.ButtonEditButtonClickBase<Staff>(_session, buttonEdit, (int)cls_App.ReferenceBooks.Staff, 1, new NullOperator(nameof(Staff.DateDismissal)), null, false, null, string.Empty, false, true);
        }

        private async void btnTaskObjectListAdd_Click(object sender, EventArgs e)
        {
            //var criteriaOperator = default(CriteriaOperator);
            
            //foreach (TypeTask item in Enum.GetValues(typeof(TypeTask)))
            //{
            //    if (item.GetEnumDescription().Equals(cmbTypeTask.Text))
            //    {
            //        criteriaOperator = new ContainsOperator(nameof(TaskObject.TypeTasksList), new BinaryOperator(nameof(TypeTasks.TypeTask), item));
            //        break;
            //    }
            //}

            //var id = cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.TaskObject, -1, criteria: criteriaOperator);
            
            var id = cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.TaskObject, -1);

            if (id > 0)
            {
                var taskObject = await _session.GetObjectByKeyAsync<TaskObject>(id);
                if (taskObject != null && Task.TaskObjectList.FirstOrDefault(f => f.TaskObject?.Oid == taskObject.Oid) is null)
                {
                    Task.TaskObjectList.Add(new TaskObjectList(_session) { TaskObject = taskObject });
                }
            }
        }

        private void btnTaskObjectListDel_Click(object sender, EventArgs e)
        {
            if (checkedListBoxTaskObjectList.SelectedItem is TaskObjectList taskObjectList)
            {
                if (true)
                {
                    Task.TaskObjectList.Remove(taskObjectList);
                }
            }            
        }

        private void TaskEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            XPBaseObject.AutoSaveOnEndEdit = true;
            Task?.Reload();
            Task?.TaskObjectList?.Reload();
        }

        private void checkedListBoxTaskObjectList_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (checkedListBoxTaskObjectList.SelectedItem is TaskObjectList taskObjectList)
            {
                var isPerformed = false;
                if (e.State == CheckState.Checked)
                {
                    isPerformed = true;
                }

                taskObjectList.IsPerformed = isPerformed;
            }
        }

        private void cmbTypeTask_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var comboBoxEdit = sender as ComboBoxEdit;
            if (comboBoxEdit != null)
            {
                if (e.Button.Kind == ButtonPredefines.Delete)
                {
                    comboBoxEdit.EditValue = null;
                }
            }
        }

        private void btnTaskStatus_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            var criteria = default(CriteriaOperator);
            if (Task.IsTook)
            {
                criteria = new NotOperator(new BinaryOperator(nameof(TaskStatus.Name), TaskStatus.TaskNewName));
            }

            cls_BaseSpr.ButtonEditButtonClickBase<TaskStatus>(_session, buttonEdit, (int)cls_App.ReferenceBooks.TaskStatus, 1, criteria, null, false, null, string.Empty, false, true);
        }

        private void cmbTypeTask_SelectedIndexChanged(object sender, EventArgs e)
        {
            var typeTask = default(TypeTask?);
            foreach (TypeTask item in Enum.GetValues(typeof(TypeTask)))
            {
                if (item.GetEnumDescription().Equals(cmbTypeTask.Text))
                {
                    typeTask = item;
                    break;
                }
            }
                        
            if (typeTask == TypeTask.Demand)
            {
                layoutControlItemDate.Visibility = LayoutVisibility.Never;
                layoutControlItemDeadline.Visibility = LayoutVisibility.Never;
                layoutControlItemDateCompletionActual.Visibility = LayoutVisibility.Never;
                
                //layoutControlItemTaskObjectListAdd.Visibility = LayoutVisibility.Never;
                //layoutControlItemTaskObjectListDel.Visibility = LayoutVisibility.Never;
                //layoutControlItemTaskObjectList.Visibility = LayoutVisibility.Never;
                //splitterItem1.Visibility = LayoutVisibility.Never;

                layoutControlItemConfirmationDate.Visibility = LayoutVisibility.Always;
                layoutControlItemReplyDate.Visibility = LayoutVisibility.Always;
                layoutControlItemGeneralVocabulary.Visibility = LayoutVisibility.Never;
                
                layoutControlItemDateConfirmationActual.Visibility = LayoutVisibility.Always;
                layoutControlItemDateDispathchActual.Visibility = LayoutVisibility.Always;
                layoutControlItemDemandNumber.Visibility = LayoutVisibility.Always;
                layoutControlItemGeneralVocabularyDemand.Visibility = LayoutVisibility.Always;
            }
            else if (typeTask == TypeTask.SourceDocuments)
            {
                layoutControlItemDate.Visibility = LayoutVisibility.Always;
                layoutControlItemDeadline.Visibility = LayoutVisibility.Always;
                layoutControlItemDateCompletionActual.Visibility = LayoutVisibility.Always;

                //layoutControlItemTaskObjectListAdd.Visibility = LayoutVisibility.Always;
                //layoutControlItemTaskObjectListDel.Visibility = LayoutVisibility.Always;
                //layoutControlItemTaskObjectList.Visibility = LayoutVisibility.Always;
                //splitterItem1.Visibility = LayoutVisibility.Always;

                layoutControlItemGeneralVocabulary.Visibility = LayoutVisibility.Always;
                dateConfirmationDate.EditValue = null;
                dateReplyDate.EditValue = null;
                layoutControlItemConfirmationDate.Visibility = LayoutVisibility.Never;
                layoutControlItemReplyDate.Visibility = LayoutVisibility.Never;                

                layoutControlItemDateConfirmationActual.Visibility = LayoutVisibility.Never;
                layoutControlItemDateDispathchActual.Visibility = LayoutVisibility.Never;
                layoutControlItemDemandNumber.Visibility = LayoutVisibility.Never;
                layoutControlItemGeneralVocabularyDemand.Visibility = LayoutVisibility.Never;
            }
            else
            {
                layoutControlItemDate.Visibility = LayoutVisibility.Always;
                layoutControlItemDeadline.Visibility = LayoutVisibility.Always;
                layoutControlItemDateCompletionActual.Visibility = LayoutVisibility.Always;

                //layoutControlItemTaskObjectListAdd.Visibility = LayoutVisibility.Always;
                //layoutControlItemTaskObjectListDel.Visibility = LayoutVisibility.Always;
                //layoutControlItemTaskObjectList.Visibility = LayoutVisibility.Always;
                //splitterItem1.Visibility = LayoutVisibility.Always;

                dateConfirmationDate.EditValue = null;
                dateReplyDate.EditValue = null;
                layoutControlItemGeneralVocabulary.Visibility = LayoutVisibility.Never;
                layoutControlItemConfirmationDate.Visibility = LayoutVisibility.Never;
                layoutControlItemReplyDate.Visibility = LayoutVisibility.Never;

                layoutControlItemDateConfirmationActual.Visibility = LayoutVisibility.Never;
                layoutControlItemDateDispathchActual.Visibility = LayoutVisibility.Never;
                layoutControlItemDemandNumber.Visibility = LayoutVisibility.Never;
                layoutControlItemGeneralVocabularyDemand.Visibility = LayoutVisibility.Never;
            }

            layoutControlTask.BestFit();
        }

        private void btnGeneralVocabulary_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            var generalVocabularyType = GetGeneralVocabularyType();
            cls_BaseSpr.ButtonEditButtonClickBase<GeneralVocabulary>(_session, buttonEdit, (int)cls_App.ReferenceBooks.GeneralVocabulary, 1, new BinaryOperator(nameof(GeneralVocabulary.GeneralVocabularyType), generalVocabularyType), null, false, null, string.Empty, false, true);
        }

        private GeneralVocabularyType? GetGeneralVocabularyType()
        {
            try
            {
                var typeTask = default(TypeTask?);
                foreach (TypeTask item in Enum.GetValues(typeof(TypeTask)))
                {
                    if (item.GetEnumDescription().Equals(cmbTypeTask.Text))
                    {
                        typeTask = item;
                        break;
                    }
                }

                var generalVocabularyType = default(GeneralVocabularyType?);
                switch (typeTask)
                {
                    case TypeTask.Demand:
                        generalVocabularyType = GeneralVocabularyType.OptionDemand;
                        break;

                    case TypeTask.SourceDocuments:
                        generalVocabularyType = GeneralVocabularyType.OptionObtainingPrimaryDocuments;
                        break;
                }

                return generalVocabularyType;
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                return default;
            }            
        }

        private void btnAdditionalServices_Click(object sender, EventArgs e)
        {
            if (Task.AdditionalServices != null)
            {
                if (Task.AdditionalServices.IsDeleted)
                {
                    Task.AdditionalServices = null;
                    return;
                }
                
                var form = new AdditionalServicesEdit(Task.AdditionalServices);
                form.ShowDialog();                
            }
        }

        private void btnTaskStatus_EditValueChanged(object sender, EventArgs e)
        {
            if (sender is ButtonEdit buttonEdit)
            {
                if (buttonEdit.EditValue is TaskStatus taskStatus)
                {
                    if (!string.IsNullOrWhiteSpace(taskStatus.Color))
                    {
                        var color = ColorTranslator.FromHtml(taskStatus.Color);
                        
                        buttonEdit.BackColor = color;
                        cmbStatus.BackColor = color;

                        layoutControlItemTaskStatus.AppearanceItemCaption.BackColor = color;
                        layoutControlItemStatus.AppearanceItemCaption.BackColor = color;
                    }
                }
                else
                {
                    buttonEdit.BackColor = default;
                    cmbStatus.BackColor = default;

                    layoutControlItemTaskStatus.AppearanceItemCaption.BackColor = default;
                    layoutControlItemStatus.AppearanceItemCaption.BackColor = default;
                }
            }
        }
    }
}