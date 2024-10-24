using DevExpress.Data.Filtering;
using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model;
using RMS.Core.Model.Chronicle;
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
    public partial class RouteSheetForm : XtraForm
    {
        private static string pathTempDirectory = "temp";
        private static string pathSaveLayoutToXmlMainGrid = $"{pathTempDirectory}\\{nameof(RouteSheetForm)}_{nameof(gridViewRouteSheet)}.xml";

        private Session _session;
        private XPCollection<RouteSheet> _routeSheets;

        private static class StatusTaskCourierColor
        {
            public static Color ColorStatusTaskCourierCanceled;
            public static Color ColorStatusTaskCourierPerformed;
            public static Color ColorStatusTaskCourierNew;
        }

        /// <summary>
        /// Настройка функционирования таблиц.
        /// </summary>
        private void FunctionalGridSetup()
        {
            BVVGlobal.oFuncXpo.FormClosed += OFuncXpo_FormClosed;
            
            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref gridViewRouteSheet);
            BVVGlobal.oFuncXpo.PressEnterGrid<RouteSheet, RouteSheetEdit>(gridViewRouteSheet);

            BVVGlobal.oFuncXpo.PressEnterGrid<TaskCourier, TaskCourierEdit>(gridViewTasksCourier);
        }

        private void OFuncXpo_FormClosed(object sender)
        {
            _routeSheets.Reload();
        }

        public RouteSheetForm(Session session)
        {
            InitializeComponent();
            FunctionalGridSetup();

            _session = session ?? BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
            _routeSheets = new XPCollection<RouteSheet>(_session, 
                null, 
                new SortProperty(nameof(RouteSheet.Date), DevExpress.Xpo.DB.SortingDirection.Ascending));
        }

        private async System.Threading.Tasks.Task GetStatusTaskCourierColor()
        {
            StatusTaskCourierColor.ColorStatusTaskCourierPerformed = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusTaskCourierPerformed", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(192, 192, 192))));
            StatusTaskCourierColor.ColorStatusTaskCourierCanceled = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusTaskCourierCanceled", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(255, 218, 185))));
            StatusTaskCourierColor.ColorStatusTaskCourierNew = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusTaskCourierNew", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(152, 251, 152))));
        }

        private async void CustomersForm_Load(object sender, EventArgs e)
        {
            await GetStatusTaskCourierColor();
            gridControlRouteSheet.DataSource = _routeSheets;

            if (gridViewRouteSheet.Columns[nameof(RouteSheet.Oid)] != null)
            {
                gridViewRouteSheet.Columns[nameof(RouteSheet.Oid)].Visible = false;
                gridViewRouteSheet.Columns[nameof(RouteSheet.Oid)].Width = 18;
                gridViewRouteSheet.Columns[nameof(RouteSheet.Oid)].OptionsColumn.FixedWidth = true;
            }

            if (gridViewRouteSheet.Columns[nameof(RouteSheet.IsClosed)] is GridColumn gridIsClosed)
            {
                gridIsClosed.Width = 100;
                gridIsClosed.OptionsColumn.FixedWidth = true;
            }

            if (gridViewRouteSheet.Columns[nameof(RouteSheet.Date)] is GridColumn gridDate)
            {
                gridDate.Width = 125;
                gridDate.OptionsColumn.FixedWidth = true;
            }

            if (gridViewRouteSheet.Columns[nameof(RouteSheet.IndividualString)] is GridColumn gridIndividual)
            {
                gridIndividual.Width = 250;
                gridIndividual.OptionsColumn.FixedWidth = true;
            }

            if (gridViewRouteSheet.Columns[nameof(RouteSheet.Remainder)] is GridColumn gridRemainder)
            {
                gridRemainder.DisplayFormat.FormatType = FormatType.Numeric;
                gridRemainder.DisplayFormat.FormatString = "n2";
                gridRemainder.Width = 150;
                gridRemainder.OptionsColumn.FixedWidth = true;
            }

            if (gridViewRouteSheet.Columns[nameof(RouteSheet.Value)] is GridColumn gridValue)
            {
                gridValue.DisplayFormat.FormatType = FormatType.Numeric;
                gridValue.DisplayFormat.FormatString = "n2";
                gridValue.Width = 150;
                gridValue.OptionsColumn.FixedWidth = true;
            }

            if (gridViewRouteSheet.Columns[nameof(RouteSheet.Spent)] is GridColumn gridSpent)
            {
                gridSpent.DisplayFormat.FormatType = FormatType.Numeric;
                gridSpent.DisplayFormat.FormatString = "n2";
                gridSpent.Width = 150;
                gridSpent.OptionsColumn.FixedWidth = true;
            }

            if (gridViewRouteSheet.Columns[nameof(RouteSheet.Balance)] is GridColumn gridBalance)
            {
                gridBalance.DisplayFormat.FormatType = FormatType.Numeric;
                gridBalance.DisplayFormat.FormatString = "n2";
                gridBalance.Width = 150;
                gridBalance.OptionsColumn.FixedWidth = true;
            }

            if (System.IO.File.Exists(pathSaveLayoutToXmlMainGrid))
            {
                gridViewRouteSheet.RestoreLayoutFromXml(pathSaveLayoutToXmlMainGrid);
            }
        }

        private void btnRouteSheet_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new RouteSheetEdit();
            form.ShowDialog();

            var routeSheet = form.RouteSheet;
            if (routeSheet?.Oid != -1)
            {
                _routeSheets.Reload();
                gridViewRouteSheet.FocusedRowHandle = gridViewRouteSheet.LocateByValue(nameof(RouteSheet.Oid), routeSheet.Oid);
            }
        }

        private void btnEditRouteSheet_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewRouteSheet.IsEmpty)
            {
                return;
            }

            var routeSheet = gridViewRouteSheet.GetRow(gridViewRouteSheet.FocusedRowHandle) as RouteSheet;
            if (routeSheet != null)
            {
                var form = new RouteSheetEdit(routeSheet);
                form.ShowDialog();
                _routeSheets?.Reload();
            }
        }

        private void btnRefreshRouteSheet_ItemClick(object sender, ItemClickEventArgs e)
        {
            _routeSheets.Reload();
        }

        private void btnDelRouteSheet_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewRouteSheet.IsEmpty)
            {
                return;
            }

            var listRouteSheet = new List<RouteSheet>();
            var msg = default(string);

            foreach (var focusedRowHandle in gridViewRouteSheet.GetSelectedRows())
            {
                var routeSheet = gridViewRouteSheet.GetRow(focusedRowHandle) as RouteSheet;

                if (routeSheet != null)
                {
                    msg += $"{routeSheet}{Environment.NewLine}";
                    listRouteSheet.Add(routeSheet);
                }
            }

            if (XtraMessageBox.Show($"Вы собираетесь удалить следующие маршрутные листы:{Environment.NewLine}{msg}Хотите продолжить?",
                    "Удаление маршрутных листов",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.OK)
            {

                if (XtraMessageBox.Show($"Удалить только маршрутный лист, не удаляя курьерскую задачу?",
                    "Сохранение задач",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    foreach (var list in listRouteSheet)
                    {
                        foreach (var taskCourier in list.TasksCourier)
                        {
                            taskCourier.ChronicleTaskCouriers.Add(new ChronicleTaskCourier(_session)
                            {
                                RouteSheet = taskCourier.RouteSheet,
                                Customer = taskCourier.Customer,
                                Courier = taskCourier.Courier,
                                AccountantResponsible = taskCourier.AccountantResponsible,
                                Address = taskCourier.Address,
                                PurposeTrip = taskCourier.PurposeTrip,
                                Date = taskCourier.Date,
                                StatusTaskCourier = taskCourier.StatusTaskCourier,
                                Value = taskCourier.Value,
                                ValueNonCash = taskCourier.ValueNonCash,
                                UserUpdate = _session.GetObjectByKey<User>(DatabaseConnection.User.Oid),
                                DateUpdate = DateTime.Now
                            });
                            taskCourier.RouteSheet = null;
                            taskCourier.Save();
                        }
                    }
                }
                else
                {
                    foreach (var list in listRouteSheet)
                    {
                        for (int i = 0; i < list.TasksCourier.Count; i++)
                        {
                            list.TasksCourier[i].Delete();
                        }
                    }
                }

                _session.Delete(listRouteSheet);
                _routeSheets?.Reload();
            }
        }

        private void gridViewRouteSheet_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridViewRouteSheet.IsEmpty)
            {
                gridControlTasksCourier.DataSource = null;
                gridViewTasksCourier.Columns.Clear();
                return;
            }

            var routeSheet = gridViewRouteSheet.GetRow(gridViewRouteSheet.FocusedRowHandle) as RouteSheet;

            if (routeSheet != null)
            {
                routeSheet.Reload();
                routeSheet.TasksCourier.Reload();
                
                gridControlTasksCourier.DataSource = routeSheet.TasksCourier;
                if (gridViewTasksCourier.Columns[nameof(TaskCourier.Oid)] != null)
                {
                    gridViewTasksCourier.Columns[nameof(TaskCourier.Oid)].Visible = false;
                    gridViewTasksCourier.Columns[nameof(TaskCourier.Oid)].Width = 18;
                    gridViewTasksCourier.Columns[nameof(TaskCourier.Oid)].OptionsColumn.FixedWidth = true;
                }

                if (gridViewTasksCourier.Columns[nameof(TaskCourier.IsUseRouteSheet)] is GridColumn gridIsUseRouteSheet)
                {
                    gridIsUseRouteSheet.Width = 105;
                    gridIsUseRouteSheet.OptionsColumn.FixedWidth = true;
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
            }
        }

        private void gridViewRouteSheet_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (e.MenuType == GridMenuType.User || e.MenuType == GridMenuType.Row)
                {
                    if (gridView.GetRow(gridView.FocusedRowHandle) is RouteSheet routeSheet)
                    {
                        btnDelCustomer.Enabled = true;
                        btnEditCustomer.Enabled = true;
                    }
                    else
                    {
                        btnDelCustomer.Enabled = false;
                        btnEditCustomer.Enabled = false;
                    }

                    popupMenuRouteSheet.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }

        private void gridViewRouteSheet_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView view = sender as GridView;

            if (view.Columns[nameof(RouteSheet.IsClosed)] != null)
            {
                var colorNotTerminated = Color.FromArgb(255, 177, 241);
                var isClosed = Convert.ToBoolean(view.GetRowCellValue
                            (e.RowHandle, view.Columns[nameof(RouteSheet.IsClosed)]));

                if (isClosed)
                {
                    e.Appearance.BackColor = colorNotTerminated;
                    e.Appearance.BackColor2 = Color.White;
                }
            }
        }

        private void barBtnFormation_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewRouteSheet.IsEmpty)
            {
                return;
            }

            var routeSheet = gridViewRouteSheet.GetRow(gridViewRouteSheet.FocusedRowHandle) as RouteSheet;

            var groupTaskCourierOperator = new GroupOperator(GroupOperatorType.And);
            var criteriaTaskCouriertDate = new BinaryOperator(nameof(RouteSheet.Date), routeSheet.Date);
            groupTaskCourierOperator.Operands.Add(criteriaTaskCouriertDate);
            var criteriaTaskCourierCourier = new BinaryOperator(nameof(RouteSheet.Courier), routeSheet.Courier);
            groupTaskCourierOperator.Operands.Add(criteriaTaskCourierCourier);

            var xpCollectionTaskCourier = new XPCollection<TaskCourier>(_session, groupTaskCourierOperator);

            if (xpCollectionTaskCourier != null && xpCollectionTaskCourier.Count > 0)
            {
                if (XtraMessageBox.Show($"Маршрутный лист {routeSheet} будет автоматически заполнен из раздела курьерских задач. Продолжить?",
                    "Автоматическое заполнение маршрутного листа.",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.OK)
                {
                    foreach (var taskCourier in xpCollectionTaskCourier)
                    {
                        if (taskCourier.RouteSheet != routeSheet)
                        {
                            taskCourier.ChronicleTaskCouriers.Add(new ChronicleTaskCourier(_session)
                            {
                                RouteSheet = taskCourier.RouteSheet,
                                Customer = taskCourier.Customer,
                                Courier = taskCourier.Courier,
                                AccountantResponsible = taskCourier.AccountantResponsible,
                                Address = taskCourier.Address,
                                PurposeTrip = taskCourier.PurposeTrip,
                                Date = taskCourier.Date,
                                StatusTaskCourier = taskCourier.StatusTaskCourier,
                                Value = taskCourier.Value,
                                ValueNonCash = taskCourier.ValueNonCash,
                                UserUpdate = _session.GetObjectByKey<User>(DatabaseConnection.User.Oid),
                                DateUpdate = DateTime.Now
                            });
                            taskCourier.Save();
                        }
                    }
                    routeSheet.TasksCourier.AddRange(xpCollectionTaskCourier);
                    routeSheet.Save();

                    XtraMessageBox.Show($"Маршрутный лист успешно сформирован, добавлено задач: {xpCollectionTaskCourier.Count}",
                        "Успешное заполнение",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            else
            {
                XtraMessageBox.Show($"Не удалось найти курьерских задач, подходящих по параметрам маршрутного листа.",
                        "Формирование прекращено",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
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
        
        private void gridViewTasksCourier_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (e.MenuType == GridMenuType.User || e.MenuType == GridMenuType.Row)
                {
                    if (gridView.GetRow(gridView.FocusedRowHandle) is TaskCourier taskCourier)
                    {
                        btnEditReportChange.Enabled = true;
                        btnDelReportChange.Enabled = true;
                    }
                    else
                    {
                        btnEditReportChange.Enabled = false;
                        btnDelReportChange.Enabled = false;
                    }

                    popupMenuTasksCourier.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }
        
        private void btnTaskCourier_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewRouteSheet.IsEmpty)
            {
                return;
            }

            var routeSheet = gridViewRouteSheet.GetRow(gridViewRouteSheet.FocusedRowHandle) as RouteSheet;

            if (routeSheet != null)
            {
                routeSheet.Reload();
                var form = new TaskCourierEdit(routeSheet);
                form.ShowDialog();

                var taskCourier = form.TaskCourier;
                if (taskCourier?.Oid != -1)
                {
                    routeSheet.TasksCourier.Reload();
                    gridViewTasksCourier.FocusedRowHandle = gridViewTasksCourier.LocateByValue(nameof(TaskCourier.Oid), taskCourier.Oid);
                }
                _routeSheets?.Reload();
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
                _routeSheets?.Reload();
            }
        }

        private void btnDelTaskCourier_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewTasksCourier.IsEmpty)
            {
                return;
            }

            var listTaskCourier = new List<TaskCourier>();

            foreach (var focusedRowHandle in gridViewTasksCourier.GetSelectedRows())
            {
                var taskCourier = gridViewTasksCourier.GetRow(focusedRowHandle) as TaskCourier;

                if (taskCourier != null)
                {
                    listTaskCourier.Add(taskCourier);
                }
            }

            _session.Delete(listTaskCourier);
            _routeSheets?.Reload();
        }

        private void btnRefreshTaskCourier_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewRouteSheet.IsEmpty)
            {
                return;
            }

            var routeSheet = gridViewRouteSheet.GetRow(gridViewRouteSheet.FocusedRowHandle) as RouteSheet;
            routeSheet.TasksCourier.Reload();
        }

        private async void btnSettingTaskCourier_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new formProgramSettings(2);
            form.ShowDialog();

            await GetStatusTaskCourierColor();
        }

        private void barBtnSaveLayoutToXmlMainGrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!Directory.Exists(pathTempDirectory))
            {
                Directory.CreateDirectory(pathTempDirectory);
            }

            gridViewRouteSheet.SaveLayoutToXml(pathSaveLayoutToXmlMainGrid);
        }

        private void btnСhooseFromAvailable_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewRouteSheet.IsEmpty)
            {
                return;
            }

            var routeSheet = gridViewRouteSheet.GetRow(gridViewRouteSheet.FocusedRowHandle) as RouteSheet;

            if (routeSheet != null)
            {
                var taskCourierId = cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.TaskCourier, -1);

                if (taskCourierId > 0)
                {
                    var taskCourier = _session.GetObjectByKey<TaskCourier>(taskCourierId);

                    if (routeSheet.TasksCourier.FirstOrDefault(f => f == taskCourier) == null)
                    {
                        routeSheet.TasksCourier.Add(taskCourier);
                        routeSheet.Save();
                    }
                    else
                    {
                        XtraMessageBox.Show($"[Задача]: {taskCourier} уже входит в текущий маршрутный лист.",
                       "Определена принадлежность",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Information);
                    }
                }
            }
        }
        
        private void barBtnTaskAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            BVVGlobal.oFuncXpo.CreateTask<TaskEdit>(_session);
        }

        private void barBtnControlSystemAddRouteSheet_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewRouteSheet.IsEmpty)
            {
                return;
            }

            var obj = gridViewRouteSheet.GetRow(gridViewRouteSheet.FocusedRowHandle) as RouteSheet;
            if (obj != null)
            {
                var form = new ControlSystemEdit(obj);
                form.ShowDialog();
            }
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

        private void gridControlRouteSheet_Load(object sender, EventArgs e)
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
    }
}