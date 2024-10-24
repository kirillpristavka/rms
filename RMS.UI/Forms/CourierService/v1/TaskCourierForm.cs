using DevExpress.Data.Filtering;
using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using RMS.Setting.Model.ColorSettings;
using RMS.UI.Forms.Directories;
using RMS.UI.Forms.ReferenceBooks;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Forms.CourierService.v1
{
    public partial class TaskCourierForm : XtraForm
    {
        private static string pathTempDirectory = "temp";
        private static string pathSaveLayoutToXmlMainGrid = $"{pathTempDirectory}\\{nameof(TaskCourierForm)}_{nameof(gridViewTasksCourier)}.xml";

        private Session Session { get; }
        private XPCollection<TaskCourier> _tasksCourier { get; }

        /// <summary>
        /// Настройка функционирования таблиц.
        /// </summary>
        private void FunctionalGridSetup()
        {
            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref gridViewTasksCourier);
            BVVGlobal.oFuncXpo.PressEnterGrid<TaskCourier, TaskCourierEdit>(gridViewTasksCourier);
        }

        private static class StatusTaskCourierColor
        {
            public static Color ColorStatusTaskCourierCanceled;
            public static Color ColorStatusTaskCourierPerformed;
            public static Color ColorStatusTaskCourierNew;
        }

        public TaskCourierForm(Session session)
        {
            InitializeComponent();
            FunctionalGridSetup();

            foreach (StatusTaskCourier item in Enum.GetValues(typeof(StatusTaskCourier)))
            {
                cmbStatusTaskCourier.Properties.Items.Add(item.GetEnumDescription());
            }

            Session = session ?? BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
            _tasksCourier = new XPCollection<TaskCourier>(Session);
        }

        private async System.Threading.Tasks.Task GetStatusTaskCourierColor()
        {
            var colorStatus = Session.FindObject<ColorStatus>(new BinaryOperator(nameof(ColorStatus.IsDefault), true));
            if (colorStatus != null)
            {
                StatusTaskCourierColor.ColorStatusTaskCourierCanceled = ColorTranslator.FromHtml(colorStatus.StatusTaskCourierCanceled);
                StatusTaskCourierColor.ColorStatusTaskCourierPerformed = ColorTranslator.FromHtml(colorStatus.StatusTaskCourierPerformed);
                StatusTaskCourierColor.ColorStatusTaskCourierNew = ColorTranslator.FromHtml(colorStatus.StatusTaskCourierNew);
            }
            else
            {
                StatusTaskCourierColor.ColorStatusTaskCourierPerformed = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusTaskCourierPerformed", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(192, 192, 192))));
                StatusTaskCourierColor.ColorStatusTaskCourierCanceled = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusTaskCourierCanceled", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(255, 218, 185))));
                StatusTaskCourierColor.ColorStatusTaskCourierNew = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusTaskCourierNew", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(152, 251, 152))));
            }
        }

        private async void TaskCourierForm_Load(object sender, EventArgs e)
        {
            _tasksCourier.Criteria = await cls_BaseSpr.GetCustomerCriteria(null, nameof(TaskCourier.Customer));

            await GetStatusTaskCourierColor();
            gridControlTasksCourier.DataSource = _tasksCourier;

            if (gridViewTasksCourier.Columns[nameof(TaskCourier.Oid)] != null)
            {
                gridViewTasksCourier.Columns[nameof(TaskCourier.Oid)].Visible = false;
                gridViewTasksCourier.Columns[nameof(TaskCourier.Oid)].Width = 18;
                gridViewTasksCourier.Columns[nameof(TaskCourier.Oid)].OptionsColumn.FixedWidth = true;
            }

            if (gridViewTasksCourier.Columns[nameof(TaskCourier.IsUseRouteSheet)] != null)
            {
                RepositoryItemImageComboBox imgStatusStatisticalReport = gridControlTasksCourier.RepositoryItems.Add(nameof(ImageComboBoxEdit)) as RepositoryItemImageComboBox;
                imgStatusStatisticalReport.SmallImages = imgTaskCourier;
                imgStatusStatisticalReport.Items.Add(new ImageComboBoxItem() { Value = true, ImageIndex = 0 });
                imgStatusStatisticalReport.Items.Add(new ImageComboBoxItem() { Value = false, ImageIndex = 1 });

                imgStatusStatisticalReport.GlyphAlignment = HorzAlignment.Center;
                gridViewTasksCourier.Columns[nameof(TaskCourier.IsUseRouteSheet)].ColumnEdit = imgStatusStatisticalReport;
                gridViewTasksCourier.Columns[nameof(TaskCourier.IsUseRouteSheet)].Width = 18;
                gridViewTasksCourier.Columns[nameof(TaskCourier.IsUseRouteSheet)].OptionsColumn.FixedWidth = true;
            }

            if (gridViewTasksCourier.Columns[nameof(TaskCourier.Date)] is GridColumn gridDate)
            {
                gridDate.Width = 125;
                gridDate.OptionsColumn.FixedWidth = true;
            }

            if (gridViewTasksCourier.Columns[nameof(TaskCourier.DateTransfer)] is GridColumn gridDateTransfer)
            {
                gridDateTransfer.Width = 125;
                gridDateTransfer.OptionsColumn.FixedWidth = true;
            }

            if (gridViewTasksCourier.Columns[nameof(TaskCourier.StatusTaskCourierString)] is GridColumn gridStatus)
            {
                gridStatus.Width = 150;
                gridStatus.OptionsColumn.FixedWidth = true;
            }

            if (gridViewTasksCourier.Columns[nameof(TaskCourier.Value)] is GridColumn gridValue)
            {
                gridValue.DisplayFormat.FormatType = FormatType.Numeric;
                gridValue.DisplayFormat.FormatString = "n2";
                gridValue.Width = 150;
                gridValue.OptionsColumn.FixedWidth = true;
            }

            if (gridViewTasksCourier.Columns[nameof(TaskCourier.ValueNonCash)] is GridColumn gridValueNonCash)
            {
                gridValueNonCash.DisplayFormat.FormatType = FormatType.Numeric;
                gridValueNonCash.DisplayFormat.FormatString = "n2";
                gridValueNonCash.Width = 150;
                gridValueNonCash.OptionsColumn.FixedWidth = true;
            }

            if (System.IO.File.Exists(pathSaveLayoutToXmlMainGrid))
            {
                gridViewTasksCourier.RestoreLayoutFromXml(pathSaveLayoutToXmlMainGrid);
            }
        }

        private void gridViewgridViewTasksCourier_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView view = sender as GridView;

            if (view.Columns[nameof(TaskCourier.StatusTaskCourierString)] != null)
            {
                var statusTaskCourier = view.GetRowCellValue
                            (e.RowHandle, view.Columns[nameof(TaskCourier.StatusTaskCourierString)])?.ToString();

                if (!string.IsNullOrWhiteSpace(statusTaskCourier))
                {
                    if (statusTaskCourier.Equals(StatusTaskCourier.New.GetEnumDescription()))
                    {
                        e.Appearance.BackColor = StatusTaskCourierColor.ColorStatusTaskCourierNew;
                        //e.Appearance.BackColor2 = Color.White;
                    }
                    else if (statusTaskCourier.Equals(StatusTaskCourier.Canceled.GetEnumDescription()))
                    {
                        e.Appearance.BackColor = StatusTaskCourierColor.ColorStatusTaskCourierCanceled;
                        //e.Appearance.BackColor2 = Color.White;
                    }
                    else if (statusTaskCourier.Equals(StatusTaskCourier.Performed.GetEnumDescription()))
                    {
                        e.Appearance.BackColor = StatusTaskCourierColor.ColorStatusTaskCourierPerformed;
                        //e.Appearance.BackColor2 = Color.White;
                    }
                }
            }
        }

        private void btnCustomer_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<Customer>(Session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.Customer, 1, null, null, false, null, string.Empty, false, true);
        }

        private void btnCourier_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (sender is ButtonEdit buttonEdit)
            {
                if (e.Button.Kind == ButtonPredefines.Delete)
                {
                    buttonEdit.EditValue = null;
                }
                else if (e.Button.Kind == ButtonPredefines.Ellipsis)
                {
                    cls_BaseSpr.ButtonEditButtonClickBase<Individual>(Session, buttonEdit, (int)cls_App.ReferenceBooks.Individual, 1, null, null, false, null, string.Empty, false, true);
                }
            }
        }

        private void btnAccountantResponsible_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<Staff>(Session, buttonEdit, (int)cls_App.ReferenceBooks.Staff, 1, new NullOperator(nameof(Staff.DateDismissal)), null, false, null, string.Empty, false, true);
        }

        private void cmb_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }
        }

        private void Filter(object sender, EventArgs e)
        {
            var groupOperator = new GroupOperator(GroupOperatorType.And);

            if (btnCustomer.EditValue is Customer customer)
            {
                var criteriaCustomer = new BinaryOperator(nameof(TaskCourier.Customer), customer);
                groupOperator.Operands.Add(criteriaCustomer);
            }

            if (btnCourier.EditValue is Individual courier)
            {
                var criteriaCourier = new BinaryOperator(nameof(TaskCourier.Courier), courier);
                groupOperator.Operands.Add(criteriaCourier);
            }

            if (btnAccountantResponsible.EditValue is Staff accountantResponsible)
            {
                var criteriaAccountantResponsible = new BinaryOperator(nameof(TaskCourier.AccountantResponsible), accountantResponsible);
                groupOperator.Operands.Add(criteriaAccountantResponsible);
            }

            if (btnRouteSheet.EditValue is RouteSheet routeSheet)
            {
                var criteriaRouteSheet = new BinaryOperator(nameof(TaskCourier.RouteSheet), routeSheet);
                groupOperator.Operands.Add(criteriaRouteSheet);
            }

            if (cmbStatusTaskCourier.EditValue != null)
            {
                foreach (StatusTaskCourier statusTaskCourier in Enum.GetValues(typeof(StatusTaskCourier)))
                {
                    if (statusTaskCourier.GetEnumDescription().Equals(cmbStatusTaskCourier.Text))
                    {
                        var criteriaStatusTaskCourier = new BinaryOperator(nameof(TaskCourier.StatusTaskCourier), statusTaskCourier);
                        groupOperator.Operands.Add(criteriaStatusTaskCourier);
                        break;
                    }
                }
            }

            if (dateDate.EditValue is DateTime date)
            {
                var criteriaDate = new BinaryOperator(nameof(TaskCourier.Date), date);
                groupOperator.Operands.Add(criteriaDate);
            }

            if (_tasksCourier != null)
            {
                if (groupOperator.Operands.Count == 0)
                {
                    _tasksCourier.Filter = null;
                }
                else
                {
                    _tasksCourier.Filter = groupOperator;
                }
            }
        }
        
        private void btnTaskCourier_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new TaskCourierEdit();
            form.ShowDialog();

            var taskCourier = form.TaskCourier;
            if (taskCourier?.Oid != -1)
            {
                _tasksCourier.Reload();
                gridViewTasksCourier.FocusedRowHandle = gridViewTasksCourier.LocateByValue(nameof(TaskCourier.Oid), taskCourier.Oid);
            }
        }

        private void btnEditTaskCourier_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewTasksCourier.IsEmpty)
            {
                return;
            }

            var taskCourier = gridViewTasksCourier.GetRow(gridViewTasksCourier.FocusedRowHandle) as TaskCourier;
            if (taskCourier != null)
            {
                var form = new TaskCourierEdit(taskCourier);
                form.ShowDialog();
            }
        }

        private void btnDelTaskCourier_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewTasksCourier.IsEmpty)
            {
                return;
            }

            var listTaskCourier = new List<TaskCourier>();
            var msg = default(string);

            foreach (var focusedRowHandle in gridViewTasksCourier.GetSelectedRows())
            {
                var taskCourier = gridViewTasksCourier.GetRow(focusedRowHandle) as TaskCourier;

                if (taskCourier != null)
                {
                    msg += $"{taskCourier}{Environment.NewLine}";
                    listTaskCourier.Add(taskCourier);
                }
            }

            if (XtraMessageBox.Show($"Вы собираетесь удалить следующие курьерские задачи:{Environment.NewLine}{msg}Хотите продолжить?",
                    "Удаление курьерских задач",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.OK)
            {
                Session.Delete(listTaskCourier);
            }
        }

        private void btnRefreshTaskCourier_ItemClick(object sender, ItemClickEventArgs e)
        {
            _tasksCourier.Reload();
        }

        private async void btnSettingTaskCourier_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new formProgramSettings(2);
            form.ShowDialog();

            await GetStatusTaskCourierColor();
        }

        private void btnRouteSheet_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<RouteSheet>(Session, buttonEdit, (int)cls_App.ReferenceBooks.RouteSheet, 1, null, null, false, null, string.Empty, false, true);
        }

        private void barBtnSaveLayoutToXmlMainGrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!Directory.Exists(pathTempDirectory))
            {
                Directory.CreateDirectory(pathTempDirectory);
            }

            gridViewTasksCourier.SaveLayoutToXml(pathSaveLayoutToXmlMainGrid);
        }

        private async void barBtnFormationRouteSheets_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show($"Вы действительно хотите сформировать маршрутные листы по курьерским задачам?" +
                $"{Environment.NewLine}1.Если маршрутный лист существует на дату задачи, то задача добавится к существующему маршрутному листу." +
                $"{Environment.NewLine}2.Если маршрутный лист не существует на дату задачи, то сформируется новый маршрутный лист." +
                $"{Environment.NewLine}3.Если в курьерской задачи появилась дата переноса, то задача будет добавлена в новый маршрутный лист на эту дату или в существующий на дату переноса.",
                    "Формирование маршрутных листов",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.OK)
            {
                await FormationRouteSheets().ConfigureAwait(false);
            }
        }

        private async System.Threading.Tasks.Task FormationRouteSheets()
        {
            using (var uof = new UnitOfWork())
            {
                var taskCouriers = new XPCollection<TaskCourier>(uof);

                foreach (var task in taskCouriers.Where(w => w.RouteSheet is null
                    && w.Courier != null
                    && w.Date != null))
                {
                    await CreateRouteSheetAsync(uof, task);
                }

                foreach (var task in taskCouriers.Where(w => w.RouteSheet != null
                    && w.Courier != null
                    && w.DateTransfer != null
                    && w.Date != null))
                {
                    await CreateRouteSheetAsync(uof, task, true);
                }
            }

            Invoke((Action)delegate
            {
                _tasksCourier.Reload();
            });
        }

        /// <summary>
        /// Создание или изменение маршрутного листа.
        /// </summary>
        /// <param name="uof">Текущая сессия.</param>
        /// <param name="task">Курьерская задача.</param>
        /// <returns></returns>
        private async System.Threading.Tasks.Task CreateRouteSheetAsync(UnitOfWork uof, TaskCourier task, bool isTransfer = false)
        {
            var groupOperator = new GroupOperator(GroupOperatorType.And);

            var criteriaCourier = new BinaryOperator($"{nameof(RouteSheet.Courier)}.{nameof(RouteSheet.Courier.Oid)}", task.Courier.Oid);
            groupOperator.Operands.Add(criteriaCourier);

            var date = task.Date;
            if (isTransfer)
            {
                if (task.DateTransfer is null)
                {
                    return;
                }
                
                date = task.DateTransfer;
                task.RouteSheet = null;
                task.Save();
            }

            var criteriaDate = new BinaryOperator($"{nameof(RouteSheet.Date)}", date);
            groupOperator.Operands.Add(criteriaDate);

            var routeSheet = await uof.FindObjectAsync<RouteSheet>(groupOperator);
            if (routeSheet is null)
            {
                routeSheet = new RouteSheet(uof)
                {
                    Date = Convert.ToDateTime(date),
                    Courier = task.Courier,
                    Comment = $"[{DateTime.Now.ToShortDateString()}] Маршрутный лист создан автоматически",
                    DateCreate = DateTime.Now,
                    UserCreate = await uof.GetObjectByKeyAsync<User>(DatabaseConnection.User.Oid)
                };
            }
            else
            {
                routeSheet.UserUpdate = await uof.GetObjectByKeyAsync<User>(DatabaseConnection.User.Oid);
                routeSheet.DateUpdate = DateTime.Now;
            }

            routeSheet.TasksCourier.Add(task);
            routeSheet.Save();

            await uof.CommitChangesAsync();
        }

        private void barBtnTaskAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            BVVGlobal.oFuncXpo.CreateTask<TaskEdit>(Session);
        }

        private void barBtnControlSystemAddTaskCourier_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewTasksCourier.IsEmpty)
            {
                return;
            }

            var obj = gridViewTasksCourier.GetRow(gridViewTasksCourier.FocusedRowHandle) as TaskCourier;
            if (obj != null)
            {
                var form = new ControlSystemEdit(obj);
                form.ShowDialog();
            }
        }

        private void gridControlTasksCourier_Load(object sender, EventArgs e)
        {
            if (sender is GridControl gridControl)
            {
                if (gridControl.MainView is GridView gridView)
                {
                    gridView.MoveFirst();
                    gridView.MoveLast();
                }
            }
        }

        private void gridViewTasksCourier_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (e.MenuType == GridMenuType.User || e.MenuType == GridMenuType.Row)
                {
                    if (gridView.GetRow(gridView.FocusedRowHandle) is TaskCourier taskCourier)
                    {
                        btnBarEdit.Enabled = true;
                        btnBarDel.Enabled = true;
                    }
                    else
                    {
                        btnBarDel.Enabled = false;
                        btnBarEdit.Enabled = false;
                    }

                    popupMenuTasksCourier.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }
    }
}