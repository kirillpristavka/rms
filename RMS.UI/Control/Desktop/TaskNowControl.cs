using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraLayout;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace RMS.UI.Control.Desktop
{
    public partial class TaskNowControl : UserControl
    {
        private Session _session;
        public int Count { get; private set; }

        private DateTime dateNow = DateTime.Now.Date;
        
        private DateTime dateSince;
        private DateTime dateTo;

        public delegate void TaskEditControlEventHandler(object sender, Task task);
        public event TaskEditControlEventHandler TaskEditControlEvent;

        public TaskNowControl(Session session)
        {
            InitializeComponent();
            _session = session;
        }

        private DateTime GetMondayDate(DateTime date)
        {
            if (date.DayOfWeek != DayOfWeek.Monday)
            {
                return GetMondayDate(date.AddDays(-1));
            }
            else
            {
                return date;
            }
        }
        
        private void GetPeriodDate(bool isWeek = false)
        {
            if (isWeek)
            {
                dateSince = GetMondayDate(dateNow).AddDays(-1);
                dateTo = GetMondayDate(dateNow).AddDays(7);

                lblLogger.Text = $"Задачи на {dateSince.AddDays(1).ToShortDateString()} - {dateTo.AddDays(-1).ToShortDateString()}";
            }
            else
            {
                dateSince = dateNow.AddDays(-1);
                dateTo = dateNow.AddDays(1);

                lblLogger.Text = $"Задачи на {dateNow.ToShortDateString()}";
            }
        }
        
        private async void Form_Load(object sender, EventArgs e)
        {
            GetPeriodDate(true);
                        
            var user = await _session.GetObjectByKeyAsync<User>(DatabaseConnection.User.Oid);
            var staff = user.Staff;

            var groupOperator = new GroupOperator(GroupOperatorType.And);
            if (user != null && user.flagAdministrator is false)
            {                
                if (staff != null)
                {
                    var groupOperatorStaff = new GroupOperator(GroupOperatorType.Or);
                    var accountantResponsibleCriteria = new BinaryOperator($"{nameof(Task.Customer)}.{nameof(Customer.AccountantResponsible)}", staff);
                    groupOperatorStaff.Operands.Add(accountantResponsibleCriteria);
                    var primaryResponsibleCriteria = new BinaryOperator($"{nameof(Task.Customer)}.{nameof(Customer.PrimaryResponsible)}", staff);
                    groupOperatorStaff.Operands.Add(primaryResponsibleCriteria);
                    var bankResponsibleCriteria = new BinaryOperator($"{nameof(Task.Customer)}.{nameof(Customer.BankResponsible)}", staff);
                    groupOperatorStaff.Operands.Add(bankResponsibleCriteria);
                    groupOperator.Operands.Add(groupOperatorStaff);

                    var statusCustomer = await _session.FindObjectAsync<Status>(new BinaryOperator(nameof(Status.Name), "Обслуживаем"));
                    if (statusCustomer != null)
                    {
                        var criteriaStatus = new BinaryOperator($"{nameof(Task.Customer)}.{ nameof(Customer.CustomerStatus)}.{nameof(CustomerStatus.Status)}", statusCustomer);
                        groupOperator.Operands.Add(criteriaStatus);
                    }
                }

                var groupOperatorTaskStaff = new GroupOperator(GroupOperatorType.Or);
                var staffCriteria = new BinaryOperator(nameof(Task.Staff), staff);
                groupOperatorTaskStaff.Operands.Add(staffCriteria);
                var coExecutorCriteria = new BinaryOperator(nameof(Task.CoExecutor), staff);
                groupOperatorTaskStaff.Operands.Add(coExecutorCriteria);
                groupOperator.Operands.Add(groupOperatorTaskStaff);
            }

            var taskStatusDone = await _session.FindObjectAsync<TaskStatus>(new BinaryOperator(nameof(TaskStatus.Name), "Выполнена"));
            if (taskStatusDone != null)
            {
                var groupOperatorStatus = new GroupOperator(GroupOperatorType.Or);

                var taskCriteriaStatusNull = new NullOperator(nameof(Task.TaskStatus));
                groupOperatorStatus.Operands.Add(taskCriteriaStatusNull);

                var taskCriteriaStatus = new NotOperator(new BinaryOperator(nameof(Task.TaskStatus), taskStatusDone));
                groupOperatorStatus.Operands.Add(taskCriteriaStatus);

                groupOperator.Operands.Add(groupOperatorStatus);
            }

            var groupDateCriteria = new GroupOperator(GroupOperatorType.Or);

            var groupCriteriaDeadline = new GroupOperator(GroupOperatorType.And);
            var criteriaDateSinceDeadline = new BinaryOperator(nameof(Task.Deadline), dateSince, BinaryOperatorType.Greater);
            groupCriteriaDeadline.Operands.Add(criteriaDateSinceDeadline);
            var criteriaDateToDeadline = new BinaryOperator(nameof(Task.Deadline), dateTo, BinaryOperatorType.Less);
            groupCriteriaDeadline.Operands.Add(criteriaDateToDeadline);
            groupDateCriteria.Operands.Add(groupCriteriaDeadline);

            var groupCriteriaConfirmationDate = new GroupOperator(GroupOperatorType.And);
            var criteriaDateSinceConfirmationDate = new BinaryOperator(nameof(Task.ConfirmationDate), dateSince, BinaryOperatorType.Greater);
            groupCriteriaConfirmationDate.Operands.Add(criteriaDateSinceConfirmationDate);
            var criteriaDateToConfirmationDate = new BinaryOperator(nameof(Task.ConfirmationDate), dateTo, BinaryOperatorType.Less);
            groupCriteriaConfirmationDate.Operands.Add(criteriaDateToConfirmationDate);
            groupDateCriteria.Operands.Add(groupCriteriaConfirmationDate);

            var groupCriteriaReplyDate = new GroupOperator(GroupOperatorType.And);
            var criteriaDateSinceReplyDate = new BinaryOperator(nameof(Task.ReplyDate), dateSince, BinaryOperatorType.Greater);
            groupCriteriaReplyDate.Operands.Add(criteriaDateSinceReplyDate);
            var criteriaDateToReplyDate = new BinaryOperator(nameof(Task.ReplyDate), dateTo, BinaryOperatorType.Less);
            groupCriteriaReplyDate.Operands.Add(criteriaDateToReplyDate);
            groupDateCriteria.Operands.Add(groupCriteriaReplyDate);

            groupOperator.Operands.Add(groupDateCriteria);

            using (var tasks = new XPCollection<Task>(_session, groupOperator))
            {
                layoutControlLogger.BeginUpdate();
                foreach (var task in tasks)
                {
                    try
                    {
                        var objectControl = new TaskNowObjectControl(task) { Dock = DockStyle.Top };                       
                        objectControl.CreateControl();
                        if (objectControl.IsCongratulate)
                        {
                            var layoutControlItem = layoutControlLogger.Root.AddItem();
                            layoutControlItem.TextVisible = false;
                            layoutControlItem.AllowHotTrack = false;
                            layoutControlItem.MaxSize = new System.Drawing.Size(0, objectControl.Size.Height);
                            layoutControlItem.MinSize = new System.Drawing.Size(10, objectControl.Size.Height);
                            layoutControlItem.SizeConstraintsType = SizeConstraintsType.Custom;
                            layoutControlItem.TextSize = new System.Drawing.Size(0, 0);
                            layoutControlItem.Control = objectControl;

                            Count++;
                            
                            objectControl.TaskEditEvent += ObjectControl_TaskEditEvent;
                        }
                    }
                    catch (Exception) { }
                }

                var emptySpaceItem = new EmptySpaceItem();
                layoutControlLogger.Root.AddItem(emptySpaceItem);

                layoutControlLogger.EndUpdate();
            }
        }

        private void ObjectControl_TaskEditEvent(object sender, Task task)
        {
            var taskNowObjectControl = sender as TaskNowObjectControl;
            if (taskNowObjectControl != null)
            {
                try
                {
                    taskNowObjectControl.ReloadControl();
                    taskNowObjectControl.Refresh();

                    if (taskNowObjectControl.IsVisible is false)
                    {
                        taskNowObjectControl?.Dispose();
                        layoutControlLogger?.Refresh();                        
                    }

                    TaskEditControlEvent?.Invoke(this, task);
                }
                catch (Exception ex)
                {
                    RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                }
            }
        }
    }
}
