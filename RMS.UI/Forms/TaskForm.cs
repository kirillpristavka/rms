using DevExpress.Data.Filtering;
using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using PulsLibrary.Extensions.DevForm;
using RMS.Core.Enumerator;
using RMS.Core.Model;
using RMS.Core.Model.Chronicle;
using RMS.Core.Model.InfoCustomer;
using RMS.Setting.Model.GeneralSettings;
using RMS.UI.Control.Customers;
using RMS.UI.Forms.Chronicle;
using RMS.UI.Forms.Directories;
using RMS.UI.Forms.ReferenceBooks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Forms
{
    public partial class TaskForm : XtraForm
    {
        private Session _session => DatabaseConnection.GetWorkSession();
        private XPCollection<Task> Tasks { get; set; }
        private XPCollection<Task> TasksSourceDocuments { get; set; }
        private XPCollection<Task> TasksDemand { get; set; }

        /// <summary>
        /// Настройка функционирования таблиц.
        /// </summary>
        private void FunctionalGridSetup()
        {
            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref gridViewTask);
            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref gridViewSourceDocuments);
            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref gridViewDemand);

            BVVGlobal.oFuncXpo.PressEnterGrid<Task, TaskEdit>(gridViewTask, action: async () => await TreeListNodeUpdate());
            BVVGlobal.oFuncXpo.PressEnterGrid<Task, TaskEdit>(gridViewSourceDocuments, action: async () => await TreeListNodeUpdate());
            BVVGlobal.oFuncXpo.PressEnterGrid<Task, TaskEdit>(gridViewDemand, action: async () => await TreeListNodeUpdate());
        }

        public TaskForm(Session session)
        {
            InitializeComponent();
            FunctionalGridSetup();
        }

        private bool isEditTaskForm = false;
        private bool isDeleteTaskForm = false;

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
                            isDeleteTaskForm = accessRights.IsDeleteTaskForm;
                        }
                    }
                }

                btnMassChange.Enabled = isEditTaskForm;
                btnDeleteTask.Enabled = isDeleteTaskForm;
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async void TaskForm_Load(object sender, EventArgs e)
        {
            await SetAccessRights();
            var fitler = await GetFilter();

            Tasks = await GetTask(gridControlTask, gridViewTask, new BinaryOperator(nameof(Task.TypeTask), TypeTask.Task), fitler);
            TasksSourceDocuments = await GetTask(gridControlSourceDocuments, gridViewSourceDocuments, new BinaryOperator(nameof(Task.TypeTask), TypeTask.SourceDocuments), fitler);

            var hideDisplayablePropertiesTasksDemand = new string[] { nameof(Task.Deadline), nameof(Task.DateCompletionActual), nameof(Task.Date) };
            TasksDemand = await GetTask(gridControlDemand,
                                  gridViewDemand,
                                  new BinaryOperator(nameof(Task.TypeTask), TypeTask.Demand),
                                  fitler,
                                  $";{nameof(Task.DemandNumber)};{nameof(Task.ConfirmationDate)};{nameof(Task.ReplyDate)};{nameof(Task.DateConfirmationActual)};{nameof(Task.DateDispathchActual)}",
                                  hideDisplayablePropertiesTasksDemand);

            var focusedNode = await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"{this.Name}_{nameof(treeListCustomerFilter)}_{nameof(treeListCustomerFilter.FocusedNode)}", null, user: BVVGlobal.oApp.User);
            await TreeListUpdate(focusedNode);

            await LoadSettingsForm();

            GetNewCountTask(Tasks, "Задачи", layoutControlGroupTask);
            GetNewCountTask(TasksSourceDocuments, "Первичные", layoutControlGroupSourceDocuments);
            GetNewCountTask(TasksDemand, "Требования", layoutControlGroupDemand);
        }

        private async System.Threading.Tasks.Task LoadSettingsForm()
        {
            try
            {
                //TODO: активировать после обновления.
                //await BVVGlobal.oFuncXpo.RestoreLayoutToStreamAsync(layoutControlTask, $"{this.Name}_{nameof(layoutControlTask)}");
                //await BVVGlobal.oFuncXpo.RestoreLayoutToStreamAsync(gridViewDemand, $"{this.Name}_{nameof(gridViewDemand)}");
                //await BVVGlobal.oFuncXpo.RestoreLayoutToStreamAsync(gridViewSourceDocuments, $"{this.Name}_{nameof(gridViewSourceDocuments)}");
                //await BVVGlobal.oFuncXpo.RestoreLayoutToStreamAsync(gridViewTask, $"{this.Name}_{nameof(gridViewTask)}");

                layoutControlItemInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                //TODO: убрать после обновления.
                treeListCustomerFilter.MaximumSize = new Size(350, 0);
                treeListCustomerFilter.MinimumSize = new Size(250, 0);
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async System.Threading.Tasks.Task SaveSettingsForms()
        {
            try
            {
                await BVVGlobal.oFuncXpo.SaveLayoutToStreamAsync(layoutControlTask, $"{this.Name}_{nameof(layoutControlTask)}");
                await BVVGlobal.oFuncXpo.SaveLayoutToStreamAsync(gridViewDemand, $"{this.Name}_{nameof(gridViewDemand)}");
                await BVVGlobal.oFuncXpo.SaveLayoutToStreamAsync(gridViewSourceDocuments, $"{this.Name}_{nameof(gridViewSourceDocuments)}");
                await BVVGlobal.oFuncXpo.SaveLayoutToStreamAsync(gridViewTask, $"{this.Name}_{nameof(gridViewTask)}");
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async System.Threading.Tasks.Task<XPCollection<Task>> GetTask(GridControl gridControl,
                                           GridView gridView,
                                           CriteriaOperator criteriaOperator,
                                           CriteriaOperator filter,
                                           string addDisplayableProperties = default,
                                           string[] hideDisplayableProperties = default)
        {
            var tasks = new XPCollection<Task>(_session, criteriaOperator);
            tasks.Sorting = new SortingCollection(new SortProperty($"{nameof(Task.TaskStatus)}.{nameof(TaskStatus.Index)}", SortingDirection.Ascending),
                                                  new SortProperty(nameof(Task.Date), SortingDirection.Descending));
            tasks.Criteria = await cls_BaseSpr.GetCustomerCriteria(tasks.Criteria, nameof(Task.Customer));

            if (!string.IsNullOrWhiteSpace(addDisplayableProperties))
            {
                tasks.DisplayableProperties += addDisplayableProperties;
            }

            gridControl.DataSource = tasks;

            gridView.ColumnSetup($"{nameof(Task.Oid)}", isVisible: false, caption: "Задача (№)", width: 75, isFixedWidth: true, horzAlignment: HorzAlignment.Center, visibleIndex: 0);
            gridView.ColumnSetup($"{nameof(Task.DemandNumber)}", caption: "Номер\nтребования", width: 200, isFixedWidth: true, visibleIndex: 2);

            gridView.ColumnSetup($"{nameof(Task.StatusString)}", caption: "Статус задачи", width: 125, isFixedWidth: true, horzAlignment: HorzAlignment.Center, isGridGroupCount: true);
            gridView.ColumnSetup($"{nameof(Task.TypeTaskString)}", caption: "Вид задачи", width: 100, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
            gridView.ColumnSetup($"{nameof(Task.CustomerString)}", caption: "Клиент", width: 250, isFixedWidth: true);
            gridView.ColumnSetup($"{nameof(Task.MyProperty)}", caption: "Описание", width: 250, isFixedWidth: true);

            gridView.ColumnSetup($"{nameof(Task.GivenStaffString)}", caption: "Постановщик", width: 150, isFixedWidth: true);
            gridView.ColumnSetup($"{nameof(Task.StaffString)}", caption: "Ответственный", width: 150, isFixedWidth: true);
            gridView.ColumnSetup($"{nameof(Task.CoExecutorString)}", caption: "Соисполнитель", width: 150, isFixedWidth: true);

            gridView.ColumnSetup($"{nameof(Task.Date)}", caption: "Дата\nпостановки", width: 125, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
            gridView.ColumnSetup($"{nameof(Task.Deadline)}", caption: "Дата\nокончания", width: 125, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
            gridView.ColumnSetup($"{nameof(Task.DateCompletionActual)}", caption: "Дата\nфактического\nвыполнения", width: 125, isFixedWidth: true, horzAlignment: HorzAlignment.Center);

            gridView.ColumnSetup($"{nameof(Task.ConfirmationDate)}", caption: "Дата\nподтверждение", width: 125, isFixedWidth: true, horzAlignment: HorzAlignment.Center, visibleIndex: 5);
            gridView.ColumnSetup($"{nameof(Task.ReplyDate)}", caption: "Дата\nответа", width: 125, isFixedWidth: true, horzAlignment: HorzAlignment.Center, visibleIndex: 6);
            gridView.ColumnSetup($"{nameof(Task.DateConfirmationActual)}", caption: "Дата\nфактического\nподтверждения", width: 125, isFixedWidth: true, horzAlignment: HorzAlignment.Center, visibleIndex: 7);
            gridView.ColumnSetup($"{nameof(Task.DateDispathchActual)}", caption: "Дата\nфактической\nотправки", width: 125, isFixedWidth: true, horzAlignment: HorzAlignment.Center, visibleIndex: 8);
            gridView.ColumnSetup($"{nameof(Task.CommentString)}", caption: "Комментарий", width: 300);

            if (hideDisplayableProperties != null)
            {
                foreach (var item in hideDisplayableProperties)
                {
                    gridView.ColumnSetup(item, isVisible: false);
                }
            }
            gridControl.GridControlSetup();
            gridView.GridViewSetup(isColumnAutoWidth: false, isShowFooter: true);

            tasks.Filter = filter;
            return tasks;
        }

        private void gridViewTasks_RowStyle(object sender, RowStyleEventArgs e)
        {
            var gridView = sender as GridView;
            var obj = gridView.GetRow(e.RowHandle) as Task;

            if (obj != null)
            {
                var color = obj.TaskStatus?.Color;
                if (!string.IsNullOrWhiteSpace(color))
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(color);
                }

                if (obj.DateCompletionActual is null
                    && !obj.StatusString.Equals("Выполнена")
                    && obj.Deadline is DateTime date
                    && DateTime.Now.Date.AddDays(-2) >= date)
                {
                    e.Appearance.ForeColor = Color.Red;
                }
            }
        }

        private async void btnAddTask_ItemClick(object sender, ItemClickEventArgs e)
        {
            var typeTask = TypeTask.Task;

            try
            {
                if (currentGridView.Name.Equals(nameof(gridViewSourceDocuments)))
                {
                    typeTask = TypeTask.SourceDocuments;
                }
                else if (currentGridView.Name.Equals(nameof(gridViewDemand)))
                {
                    typeTask = TypeTask.Demand;
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }

            BVVGlobal.oFuncXpo.CreateTask<TaskEdit>(_session, null, typeTask);

            Tasks?.Reload();
            TasksSourceDocuments?.Reload();
            TasksDemand?.Reload();

            await TreeListNodeUpdate();
        }

        private void btnEditTask_ItemClick(object sender, ItemClickEventArgs e)
        {
            var gridView = GetFocusedGridView();
            if (gridView != null)
            {
                if (gridView.IsEmpty)
                {
                    return;
                }

                var task = gridView.GetRow(gridView.FocusedRowHandle) as Task;
                if (task != null)
                {
                    var form = new TaskEdit(task, false);
                    form.ShowDialog();
                }
            }
        }

        private GridView GetFocusedGridView()
        {
            if (gridControlTask.IsFocused)
            {
                return gridViewTask;
            }
            else if (gridControlSourceDocuments.IsFocused)
            {
                return gridViewSourceDocuments;
            }
            else if (gridControlDemand.IsFocused)
            {
                return gridViewDemand;
            }

            return default;
        }

        private async void btnDeleteTask_ItemClick(object sender, ItemClickEventArgs e)
        {
            var gridView = GetFocusedGridView();

            if (gridView is null || gridView.IsEmpty)
            {
                return;
            }

            var list = new List<Task>();

            foreach (var focusedRowHandle in gridView.GetSelectedRows())
            {
                if (gridView.GetRow(focusedRowHandle) is Task task)
                {
                    list.Add(task);
                }
            }

            if (XtraMessageBox.Show($"Будет удалено задач: {list.Count()}{Environment.NewLine}Хотите продолжить?",
                    "Удаление задач",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.OK)
            {

                using (var uof = new UnitOfWork())
                {
                    foreach (var task in list)
                    {
                        try
                        {
                            var deals = await new XPQuery<Deal>(uof)?.Where(w => w.Task != null && w.Task.Oid == task.Oid)?.ToListAsync();
                            foreach (var deal in deals)
                            {
                                deal.Task = null;
                                deal.Save();
                            }

                            var additionalServices = await new XPQuery<AdditionalServices>(uof)?.Where(w => w.Task != null && w.Task.Oid == task.Oid)?.ToListAsync();
                            foreach (var additionalServic in additionalServices)
                            {
                                additionalServic.Task = null;
                                additionalServic.Save();
                            }

                            task.Delete();
                        }
                        catch (Exception ex)
                        {
                            RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                        }
                    }

                    await uof.CommitTransactionAsync();
                }
                await TreeListNodeUpdate();
            }
        }

        private async void barBtnRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            Tasks?.Reload();
            TasksSourceDocuments?.Reload();
            TasksDemand?.Reload();

            await TreeListNodeUpdate();
        }

        private async void barBtnSaveLayoutToXmlMainGrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            await SaveSettingsForms();
        }

        private async void btnDirectoryAdd_Click(object sender, EventArgs e)
        {
            var form = new CustomersFilterForm(_session, WorkZone.ModuleTask);
            form.ShowDialog();

            if (form.FlagSave)
            {
                await TreeListUpdate(form?.CustomerFilter?.Oid);
            }
        }

        private async void btnDirectoryCustomerFilter_Click(object sender, EventArgs e)
        {
            cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.CustomerFilter, -1);
            await TreeListUpdate();
        }

        private async System.Threading.Tasks.Task TreeListNodeUpdate()
        {
            var treeList = treeListCustomerFilter;

            foreach (TreeListNode node in treeList.Nodes)
            {
                try
                {
                    if (node.GetValue(0) is CustomerFilter customerFilter)
                    {
                        var count = 0;
                        var criteria = customerFilter.GetGroupOperatorTask();

                        using (var uof = new UnitOfWork())
                        {
                            if (criteria is null)
                            {
                                if (customerFilter.Name.Equals("*Все"))
                                {

                                }
                                else if (customerFilter.Name.Equals("Мои задачи (получены)"))
                                {
                                    var groupOperatorMyTaskIn = new GroupOperator(GroupOperatorType.Or);

                                    var user = await uof.GetObjectByKeyAsync<User>(DatabaseConnection.User.Oid);
                                    if (user.Staff != null)
                                    {
                                        var criteriaTaskStaff = new BinaryOperator(nameof(Task.Staff), user.Staff);
                                        groupOperatorMyTaskIn.Operands.Add(criteriaTaskStaff);

                                        var criteriaTaskCoExecutor = new BinaryOperator(nameof(Task.CoExecutor), user.Staff);
                                        groupOperatorMyTaskIn.Operands.Add(criteriaTaskCoExecutor);
                                    }

                                    if (groupOperatorMyTaskIn.Operands.Count > 0)
                                    {
                                        criteria = new GroupOperator();
                                        criteria.Operands.Add(groupOperatorMyTaskIn);
                                    }
                                }
                                else if (customerFilter.Name.Equals("Мои задачи (поставлены)"))
                                {
                                    var groupOperatorMyTaskOut = new GroupOperator(GroupOperatorType.Or);

                                    var user = await uof.GetObjectByKeyAsync<User>(DatabaseConnection.User.Oid);
                                    if (user.Staff != null)
                                    {
                                        var criteriaTaskGivenStaff = new BinaryOperator(nameof(Task.GivenStaff), user.Staff);
                                        groupOperatorMyTaskOut.Operands.Add(criteriaTaskGivenStaff);
                                    }

                                    if (groupOperatorMyTaskOut.Operands.Count > 0)
                                    {
                                        criteria = new GroupOperator();
                                        criteria.Operands.Add(groupOperatorMyTaskOut);
                                    }
                                }
                            }

                            using (var collection = new XPCollection<Task>(uof, criteria))
                            {
                                count = collection.Count();
                            }
                        }

                        node.SetValue(1, count);
                    }
                }
                catch (Exception ex)
                {
                    RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                }
            }


            GetNewCountTask(Tasks, "Задачи", layoutControlGroupTask);
            GetNewCountTask(TasksSourceDocuments, "Первичные", layoutControlGroupSourceDocuments);
            GetNewCountTask(TasksDemand, "Требования", layoutControlGroupDemand);
        }

        private int GetNewCountTask(XPCollection<Task> tasks, string caption, DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup)
        {
            try
            {
                var count = tasks.Count(c => c.StatusString == "Новая");
                if (count > 0)
                {
                    layoutControlGroup.Text = $"{caption} ({count})";
                }
                else
                {
                    layoutControlGroup.Text = $"{caption}";
                }

                return count;
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                return -1;
            }
        }

        private XPCollection<CustomerFilter> XpCollectionCustomerFilter { get; set; }

        private async System.Threading.Tasks.Task TreeListUpdate(object obj = null)
        {
            var task = new XPCollection<Task>(_session);
            XpCollectionCustomerFilter = new XPCollection<CustomerFilter>(_session);
            var sessionUser = _session.GetObjectByKey<User>(DatabaseConnection.User.Oid);

            treeListCustomerFilter.Columns.Clear();
            treeListCustomerFilter.Columns.Add();
            treeListCustomerFilter.Columns.Add();

            treeListCustomerFilter.Columns[0].Visible = true;
            treeListCustomerFilter.Columns[0].Width = 100;

            treeListCustomerFilter.Columns[1].Visible = true;
            treeListCustomerFilter.Columns[1].Width = 50;
            treeListCustomerFilter.Columns[1].OptionsColumn.FixedWidth = true;

            treeListCustomerFilter.OptionsView.AutoWidth = true;
            treeListCustomerFilter.ClearNodes();

            var count = default(int);

            var settings = await _session.FindObjectAsync<Settings>(null);
            if (sessionUser?.UserGroups?.Select(s => s.UserGroup)?.FirstOrDefault(f => f.Oid == settings?.UserGroupEverything?.Oid) != null)
            {
                count = task.Count();
                treeListCustomerFilter.AppendNode(new object[] { new CustomerFilter() { Name = "*Все" }, count }, -1, -1, -1, -1);
            }

            var groupOperatorMyTaskIn = new GroupOperator(GroupOperatorType.Or);
            groupOperatorMyTaskIn = await cls_BaseSpr.GetStaffCriteria(_session, groupOperatorMyTaskIn, nameof(Task.Staff));
            groupOperatorMyTaskIn = await cls_BaseSpr.GetStaffCriteria(_session, groupOperatorMyTaskIn, nameof(Task.CoExecutor));
            task.Filter = groupOperatorMyTaskIn;
            count = task.Count();

            //var user = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid);
            //if (user.Staff != null)
            //{
            //    var criteriaTaskStaff = new BinaryOperator(nameof(Task.Staff), user.Staff);
            //    groupOperatorMyTaskIn.Operands.Add(criteriaTaskStaff);

            //    var criteriaTaskCoExecutor = new BinaryOperator(nameof(Task.CoExecutor), user.Staff);
            //    groupOperatorMyTaskIn.Operands.Add(criteriaTaskCoExecutor);

            //    task.Filter = groupOperatorMyTaskIn;
            //    count = task.Count();
            //}
            treeListCustomerFilter.AppendNode(new object[] { new CustomerFilter() { Name = "Мои задачи (получены)" }, count }, -1, -1, -1, -1);

            var groupOperatorMyTaskOut = new GroupOperator(GroupOperatorType.Or);

            groupOperatorMyTaskOut = await cls_BaseSpr.GetStaffCriteria(_session, groupOperatorMyTaskOut, nameof(Task.GivenStaff));
            task.Filter = groupOperatorMyTaskOut;
            count = task.Count();

            //if (user.Staff != null)
            //{
            //    var criteriaTaskGivenStaff = new BinaryOperator(nameof(Task.GivenStaff), user.Staff);
            //    groupOperatorMyTaskOut.Operands.Add(criteriaTaskGivenStaff);

            //    task.Filter = groupOperatorMyTaskOut;
            //    count = task.Count();
            //}
            treeListCustomerFilter.AppendNode(new object[] { new CustomerFilter() { Name = "Мои задачи (поставлены)" }, count }, -1, -1, -1, -1);

            foreach (var customerFilter in XpCollectionCustomerFilter.OrderBy(o => o.Number))
            {
                if (customerFilter.IsVisibleTask)
                {
                    customerFilter.Users.Reload();
                    customerFilter.UserGroups.Reload();

                    if (customerFilter.Users.FirstOrDefault(f => f.User.Oid == sessionUser.Oid) != null ||
                        customerFilter.UserGroups.FirstOrDefault(f => sessionUser.UserGroups.FirstOrDefault(sf => sf.UserGroup.Oid == f.UserGroup.Oid) != null) != null)
                    {
                        task.Filter = customerFilter.GetGroupOperatorTask();
                        count = task.Count();
                        treeListCustomerFilter.AppendNode(new object[] { customerFilter, count }, -1, -1, -1, -1);
                        task.Filter = null;
                    }
                }
                task.Filter = null;
            }
            task.Filter = null;

            try
            {
                if (obj != null)
                {
                    var node = default(TreeListNode);

                    if (int.TryParse(obj?.ToString(), out int oid))
                    {
                        node = treeListCustomerFilter.Nodes?.FirstOrDefault(f => f.RootNode?.GetValue(0) is CustomerFilter customerFilter && customerFilter.Oid == oid);
                    }
                    else if (obj is string name && !string.IsNullOrWhiteSpace(name))
                    {
                        node = treeListCustomerFilter.Nodes?.FirstOrDefault(f => f.RootNode?.GetValue(0) is string objName
                        && objName == name
                        || f.RootNode?.GetValue(0) is CustomerFilter customerFilter
                        && customerFilter.Name == name);
                    }

                    if (node != null)
                    {
                        treeListCustomerFilter.SetFocusedNode(node);
                    }
                }
                else
                {
                    task.Filter = await GetFilter();
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async System.Threading.Tasks.Task<GroupOperator> GetFilter()
        {
            var groupOperator = new GroupOperator(GroupOperatorType.And);

            if (treeListCustomerFilter != null && treeListCustomerFilter.FocusedNode?.GetValue(0) is CustomerFilter customerFilter)
            {
                if (customerFilter.Name.Equals("*Все"))
                {

                }
                else if (customerFilter.Name.Equals("Мои задачи (получены)"))
                {
                    var groupOperatorMyTaskIn = new GroupOperator(GroupOperatorType.Or);

                    groupOperatorMyTaskIn = await cls_BaseSpr.GetStaffCriteria(_session, groupOperatorMyTaskIn, nameof(Task.Staff));
                    groupOperatorMyTaskIn = await cls_BaseSpr.GetStaffCriteria(_session, groupOperatorMyTaskIn, nameof(Task.CoExecutor));

                    if (groupOperatorMyTaskIn.Operands.Count > 0)
                    {
                        groupOperator.Operands.Add(groupOperatorMyTaskIn);
                    }
                }
                else if (customerFilter.Name.Equals("Мои задачи (поставлены)"))
                {
                    var groupOperatorMyTaskOut = new GroupOperator(GroupOperatorType.Or);

                    groupOperatorMyTaskOut = await cls_BaseSpr.GetStaffCriteria(_session, groupOperatorMyTaskOut, nameof(Task.GivenStaff));

                    if (groupOperatorMyTaskOut.Operands.Count > 0)
                    {
                        groupOperator.Operands.Add(groupOperatorMyTaskOut);
                    }
                }
                else
                {
                    groupOperator.Operands.Add(customerFilter.GetGroupOperatorTask());
                }
            }

            var taskStatus = await _session.FindObjectAsync<TaskStatus>(new BinaryOperator(nameof(TaskStatus.Name), "Выполнена"));
            if (isDisplayCompletedTasks is false)
            {
                if (taskStatus != null)
                {
                    var criteriaTaskStatus = new NotOperator(new BinaryOperator($"{nameof(Task.TaskStatus)}.{nameof(TaskStatus.Oid)}", taskStatus.Oid));
                    groupOperator.Operands.Add(criteriaTaskStatus);
                }
            }
            else
            {
                if (taskStatus != null)
                {
                    try
                    {
                        var baseGroupOperator = default(GroupOperator);
                        foreach (var item in groupOperator.Operands)
                        {
                            baseGroupOperator = GetBaseGroupOperator(groupOperator.Operands[0]);
                        }

                        if (baseGroupOperator != null)
                        {
                            var binaryOperator = new BinaryOperator($"{nameof(Task.TaskStatus)}.{nameof(TaskStatus.Oid)}", taskStatus.Oid);
                            baseGroupOperator.Operands.Add(binaryOperator);
                        }
                    }
                    catch (Exception ex)
                    {
                        RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                    }
                }
            }

            if (groupOperator.Operands.Count > 0)
            {
                return groupOperator;
            }
            else
            {
                return null;
            }
        }

        private GroupOperator GetBaseGroupOperator(CriteriaOperator criteriaOperator)
        {
            try
            {
                var groupOperator = (GroupOperator)criteriaOperator;

                foreach (var obj in groupOperator.Operands)
                {
                    try
                    {
                        var binaryOperator = (BinaryOperator)obj;
                        if (binaryOperator.LeftOperand.ToString() == $"[{nameof(TaskStatus)}.{nameof(TaskStatus.Oid)}]")
                        {
                            return groupOperator;
                        }
                    }
                    catch (Exception ex)
                    {
                        RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                        return GetBaseGroupOperator(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }

            return default;
        }

        private async void treeListCustomerFilter_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            var fitler = await GetFilter();

            Tasks.Filter = fitler;
            TasksSourceDocuments.Filter = fitler;
            TasksDemand.Filter = fitler;

            gridView_FocusedRowChanged(gridViewTask, new FocusedRowChangedEventArgs(-1, gridViewTask.FocusedRowHandle));
            gridView_FocusedRowChanged(gridViewSourceDocuments, new FocusedRowChangedEventArgs(-1, gridViewSourceDocuments.FocusedRowHandle));
            gridView_FocusedRowChanged(gridViewDemand, new FocusedRowChangedEventArgs(-1, gridViewDemand.FocusedRowHandle));
        }

        private void gridView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            var gridView = sender as GridView;
            if (gridView is null || gridView.IsEmpty)
            {
                var currentTask = gridView.GetRow(gridView.FocusedRowHandle) as Task;
                if (currentTask != null)
                {
                    currentTask.Reload();
                }
            }
        }

        private async void treeListCustomerFilter_DoubleClick(object sender, EventArgs e)
        {
            var treList = sender as TreeList;

            if (treList != null && treList.FocusedNode?.GetValue(0) is CustomerFilter customerFilter)
            {
                if (customerFilter.Oid > 0)
                {
                    var form = new CustomersFilterForm(customerFilter, WorkZone.ModuleTask);
                    form.ShowDialog();

                    if (form.FlagSave)
                    {
                        await TreeListNodeUpdate();
                    }
                }
            }
        }

        private void btnMassChange_ItemClick(object sender, ItemClickEventArgs e)
        {
            var gridView = GetFocusedGridView();

            if (gridView is null || gridView.IsEmpty)
            {
                return;
            }

            var objList = new List<Task>();

            foreach (var focusedRowHandle in gridView.GetSelectedRows())
            {
                var obj = gridView.GetRow(focusedRowHandle) as Task;

                if (obj != null)
                {
                    objList.Add(obj);
                }
            }

            if (objList != null && objList.Count > 0)
            {
                var form = new TaskEdit(_session, objList);
                form.ShowDialog();
            }
        }

        private void barBtnControlSystemAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            var gridView = GetFocusedGridView();

            if (gridView is null || gridView.IsEmpty)
            {
                return;
            }

            var obj = gridView.GetRow(gridView.FocusedRowHandle) as Task;
            if (obj != null)
            {
                var form = new ControlSystemEdit(obj);
                form.ShowDialog();
            }
        }

        private void barBtnProgramEventAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            var gridView = GetFocusedGridView();

            if (gridView is null || gridView.IsEmpty)
            {
                return;
            }

            var obj = gridView.GetRow(gridView.FocusedRowHandle) as Task;
            if (obj != null)
            {
                var form = new ProgramEventEdit(obj);
                form.ShowDialog();
            }
        }

        private async void TaskForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            await SaveSettingsForms();
        }

        private async void barBtnDefault_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                var node = treeListCustomerFilter.FocusedNode;
                if (node != null)
                {
                    var obj = default(object);
                    if (node.GetValue(0) is string name)
                    {
                        obj = name;
                    }
                    else if (node.GetValue(0) is CustomerFilter customerFilter)
                    {
                        if (customerFilter?.Oid > 0)
                        {
                            obj = customerFilter?.Oid;
                        }
                        else
                        {
                            obj = customerFilter?.Name;
                        }
                    }

                    await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"{this.Name}_{nameof(treeListCustomerFilter)}_{nameof(treeListCustomerFilter.FocusedNode)}", obj?.ToString(), true, user: BVVGlobal.oApp.User);
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private void treeListCustomerFilter_PopupMenuShowing(object sender, DevExpress.XtraTreeList.PopupMenuShowingEventArgs e)
        {
            //if (sender is GridView gridView)
            //{
            //    if (e.MenuType == GridMenuType.User || e.MenuType == GridMenuType.Row)
            //    {
            //        if (e.MenuType != GridMenuType.Row)
            //        {
            //            barBtnEdit.Enabled = false;
            //            barBtnDel.Enabled = false;
            //        }
            //        else
            //        {
            //            barBtnEdit.Enabled = true;
            //            barBtnDel.Enabled = true;
            //        }

            //        barCheckFindPanel.Checked = gridView.IsFindPanelVisible;
            //        barCheckAutoFilterRow.Checked = gridView.OptionsView.ShowAutoFilterRow;

            //        popupMenu.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
            //    }
            //}

            try
            {
                popupMenuTreeList.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private bool isDisplayCompletedTasks;
        private void barCheckIsCompleted_ItemClick(object sender, ItemClickEventArgs e)
        {
            isDisplayCompletedTasks = !isDisplayCompletedTasks;
            treeListCustomerFilter_FocusedNodeChanged(null, null);
        }

        private GridView currentGridView;
        private void gridViewTask_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (e.MenuType == GridMenuType.User || e.MenuType == GridMenuType.Row)
                {
                    barCheckIsCompleted.Checked = isDisplayCompletedTasks;

                    gridView.Focus();
                    currentGridView = gridView;

                    if (currentGridView?.GetRow(currentGridView?.FocusedRowHandle ?? -1) is Task task)
                    {
                        task?.Reload();
                    }

                    popupMenu.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }

        private async void barBtnChronicle_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                var gridView = GetFocusedGridView();
                if (gridView != null)
                {
                    if (gridView.IsEmpty)
                    {
                        return;
                    }

                    var task = gridView.GetRow(gridView.FocusedRowHandle) as Task;
                    if (task != null)
                    {
                        using (var chronicle = new XPCollection<ChronicleTask>(_session, new BinaryOperator(nameof(ChronicleTask.TaskOid), task.Oid)))
                        {
                            var form = new ChronicleTaxesForm<ChronicleTask>(chronicle, "Задачи");
                            form.ShowDialog();
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }
    }
}