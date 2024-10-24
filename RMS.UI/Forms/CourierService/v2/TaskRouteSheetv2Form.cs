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
using RMS.Core.Model.CourierService;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.ObjectDTO.Controllers;
using RMS.Setting.Model.ColorSettings;
using RMS.UI.Forms.Directories;
using RMS.UI.Forms.ReferenceBooks;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Forms.CourierService.v2
{
    public partial class TaskRouteSheetv2Form : XtraForm
    {
        private static string pathTempDirectory = "temp";
        private static string pathSaveLayoutToXmlMainGrid = $"{pathTempDirectory}\\{nameof(TaskRouteSheetv2Form)}_{nameof(gridViewTasksCourier)}.xml";

        private Session Session { get; }
        private XPCollection<TaskRouteSheetv2> _taskRouteSheetv2 { get; }

        /// <summary>
        /// Настройка функционирования таблиц.
        /// </summary>
        private void FunctionalGridSetup()
        {
            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref gridViewTasksCourier);
            BVVGlobal.oFuncXpo.PressEnterGrid<TaskRouteSheetv2, TaskRouteSheetv2Edit>(gridViewTasksCourier);
        }

        private static class StatusTaskCourierColor
        {
            public static Color ColorStatusTaskCourierCanceled;
            public static Color ColorStatusTaskCourierPerformed;
            public static Color ColorStatusTaskCourierNew;
        }

        public TaskRouteSheetv2Form(Session session)
        {
            InitializeComponent();
            FunctionalGridSetup();

            foreach (StatusTaskCourier item in Enum.GetValues(typeof(StatusTaskCourier)))
            {
                cmbStatus.Properties.Items.Add(item.GetEnumDescription());
            }

            Session = session ?? BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
            _taskRouteSheetv2 = new XPCollection<TaskRouteSheetv2>(Session);

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
            _taskRouteSheetv2.Criteria = await cls_BaseSpr.GetCustomerCriteria(null, nameof(TaskRouteSheetv2.Customer));

            await GetStatusTaskCourierColor();
            gridControlTasksCourier.DataSource = _taskRouteSheetv2;

            if (gridViewTasksCourier.Columns[nameof(TaskRouteSheetv2.Oid)] != null)
            {
                gridViewTasksCourier.Columns[nameof(TaskRouteSheetv2.Oid)].Visible = false;
                gridViewTasksCourier.Columns[nameof(TaskRouteSheetv2.Oid)].Width = 18;
                gridViewTasksCourier.Columns[nameof(TaskRouteSheetv2.Oid)].OptionsColumn.FixedWidth = true;
            }

            if (gridViewTasksCourier.Columns[nameof(TaskRouteSheetv2.IsUseRouteSheet)] != null)
            {
                RepositoryItemImageComboBox imgStatusStatisticalReport = gridControlTasksCourier.RepositoryItems.Add(nameof(ImageComboBoxEdit)) as RepositoryItemImageComboBox;
                imgStatusStatisticalReport.SmallImages = imgTaskCourier;
                imgStatusStatisticalReport.Items.Add(new ImageComboBoxItem() { Value = true, ImageIndex = 0 });
                imgStatusStatisticalReport.Items.Add(new ImageComboBoxItem() { Value = false, ImageIndex = 1 });

                imgStatusStatisticalReport.GlyphAlignment = HorzAlignment.Center;
                gridViewTasksCourier.Columns[nameof(TaskRouteSheetv2.IsUseRouteSheet)].ColumnEdit = imgStatusStatisticalReport;
                gridViewTasksCourier.Columns[nameof(TaskRouteSheetv2.IsUseRouteSheet)].Width = 18;
                gridViewTasksCourier.Columns[nameof(TaskRouteSheetv2.IsUseRouteSheet)].OptionsColumn.FixedWidth = true;
            }

            if (gridViewTasksCourier.Columns[nameof(TaskRouteSheetv2.Date)] is GridColumn gridDate)
            {
                gridDate.Width = 125;
                gridDate.OptionsColumn.FixedWidth = true;
            }

            if (gridViewTasksCourier.Columns[nameof(TaskRouteSheetv2.DateTransfer)] is GridColumn gridDateTransfer)
            {
                gridDateTransfer.Width = 125;
                gridDateTransfer.OptionsColumn.FixedWidth = true;
            }

            if (gridViewTasksCourier.Columns[nameof(TaskRouteSheetv2.StatusTaskCourierString)] is GridColumn gridStatus)
            {
                gridStatus.Width = 150;
                gridStatus.OptionsColumn.FixedWidth = true;
            }

            if (gridViewTasksCourier.Columns[nameof(TaskRouteSheetv2.Value)] is GridColumn gridValue)
            {
                gridValue.DisplayFormat.FormatType = FormatType.Numeric;
                gridValue.DisplayFormat.FormatString = "n2";
                gridValue.Width = 150;
                gridValue.OptionsColumn.FixedWidth = true;
            }

            if (gridViewTasksCourier.Columns[nameof(TaskRouteSheetv2.ValueNonCash)] is GridColumn gridValueNonCash)
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

            if (view.Columns[nameof(TaskRouteSheetv2.StatusTaskCourierString)] != null)
            {
                var statusTaskCourier = view.GetRowCellValue
                            (e.RowHandle, view.Columns[nameof(TaskRouteSheetv2.StatusTaskCourierString)])?.ToString();

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
                var criteriaCustomer = new BinaryOperator(nameof(TaskRouteSheetv2.Customer), customer);
                groupOperator.Operands.Add(criteriaCustomer);
            }
            
            if (btnCourier.EditValue is Individual courier)
            {
                var criteriaCourier = new BinaryOperator(nameof(TaskCourier.Courier), courier);
                groupOperator.Operands.Add(criteriaCourier);
            }

            if (btnAccountantResponsible.EditValue is Staff accountantResponsible)
            {
                var criteriaAccountantResponsible = new BinaryOperator(nameof(TaskRouteSheetv2.AccountantResponsible), accountantResponsible);
                groupOperator.Operands.Add(criteriaAccountantResponsible);
            }

            if (btnRouteSheet.EditValue is RouteSheetv2 routeSheet)
            {
                var criteriaRouteSheet = new BinaryOperator(nameof(TaskRouteSheetv2.RouteSheetv2), routeSheet);
                groupOperator.Operands.Add(criteriaRouteSheet);
            }

            if (cmbStatus.EditValue != null)
            {
                foreach (StatusTaskCourier statusTaskCourier in Enum.GetValues(typeof(StatusTaskCourier)))
                {
                    if (statusTaskCourier.GetEnumDescription().Equals(cmbStatus.Text))
                    {
                        var criteriaStatusTaskCourier = new BinaryOperator(nameof(TaskRouteSheetv2.StatusTaskCourier), statusTaskCourier);
                        groupOperator.Operands.Add(criteriaStatusTaskCourier);
                        break;
                    }
                }
            }

            if (dateDate.EditValue is DateTime date)
            {
                var criteriaDate = new BinaryOperator(nameof(TaskRouteSheetv2.Date), date);
                groupOperator.Operands.Add(criteriaDate);
            }

            if (_taskRouteSheetv2 != null)
            {
                if (groupOperator.Operands.Count == 0)
                {
                    _taskRouteSheetv2.Filter = null;
                }
                else
                {
                    _taskRouteSheetv2.Filter = groupOperator;
                }
            }
        }
        
        private void btnTaskCourier_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new TaskRouteSheetv2Edit();
            form.ShowDialog();

            var taskCourier = form.TaskRouteSheetv2;
            if (taskCourier?.Oid != -1)
            {
                _taskRouteSheetv2.Reload();
                gridViewTasksCourier.FocusedRowHandle = gridViewTasksCourier.LocateByValue(nameof(TaskRouteSheetv2.Oid), taskCourier.Oid);
            }
        }

        private void btnEditTaskCourier_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewTasksCourier.IsEmpty)
            {
                return;
            }

            var taskCourier = gridViewTasksCourier.GetRow(gridViewTasksCourier.FocusedRowHandle) as TaskRouteSheetv2;
            if (taskCourier != null)
            {
                var form = new TaskRouteSheetv2Edit(taskCourier);
                form.ShowDialog();
            }
        }

        private void btnDelTaskCourier_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewTasksCourier.IsEmpty)
            {
                return;
            }

            var listTaskCourier = new List<TaskRouteSheetv2>();
            var msg = default(string);

            foreach (var focusedRowHandle in gridViewTasksCourier.GetSelectedRows())
            {
                var taskCourier = gridViewTasksCourier.GetRow(focusedRowHandle) as TaskRouteSheetv2;

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
            _taskRouteSheetv2.Reload();
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

            cls_BaseSpr.ButtonEditButtonClickBase<RouteSheetv2>(Session, buttonEdit, (int)cls_App.ReferenceBooks.RouteSheetv2, 1, null, null, false, null, string.Empty, false, true);
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
                var taskCouriers = new XPCollection<TaskRouteSheetv2>(uof);

                foreach (var task in taskCouriers.Where(w => w.RouteSheetv2 is null
                    && w.Date != null))
                {
                    await CreateRouteSheetAsync(uof, task);
                }

                foreach (var task in taskCouriers.Where(w => w.RouteSheetv2 != null
                    && w.DateTransfer != null
                    && w.Date != null))
                {
                    await CreateRouteSheetAsync(uof, task, true);
                }
            }

            Invoke((Action)delegate
            {
                _taskRouteSheetv2.Reload();
            });
        }

        /// <summary>
        /// Создание или изменение маршрутного листа.
        /// </summary>
        /// <param name="uof">Текущая сессия.</param>
        /// <param name="task">Курьерская задача.</param>
        /// <returns></returns>
        private async System.Threading.Tasks.Task CreateRouteSheetAsync(UnitOfWork uof, TaskRouteSheetv2 task, bool isTransfer = false)
        {
            var groupOperator = new GroupOperator(GroupOperatorType.And);
            
            var date = task.Date;
            if (isTransfer)
            {
                if (task.DateTransfer is null)
                {
                    return;
                }
                
                date = task.DateTransfer;
                task.RouteSheetv2 = null;
                task.Save();
            }

            var criteriaDate = new BinaryOperator($"{nameof(RouteSheetv2.Date)}", date);
            groupOperator.Operands.Add(criteriaDate);

            var routeSheet = await uof.FindObjectAsync<RouteSheetv2>(groupOperator);
            if (routeSheet is null)
            {
                routeSheet = new RouteSheetv2(uof)
                {
                    Date = Convert.ToDateTime(date),
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

            routeSheet.TaskRouteSheetv2.Add(task);
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

            var obj = gridViewTasksCourier.GetRow(gridViewTasksCourier.FocusedRowHandle) as TaskRouteSheetv2;
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
                    if (gridView.GetRow(gridView.FocusedRowHandle) is TaskRouteSheetv2 taskCourier)
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
    }
}