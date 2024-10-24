using DevExpress.Data.Filtering;
using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using PulsLibrary.Extensions.DevForm;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model;
using RMS.Core.Model.CourierService;
using RMS.Core.ObjectDTO.Controllers;
using RMS.Core.ObjectDTO.Models;
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
    public partial class RouteSheetv2Form : XtraForm
    {
        private static string pathTempDirectory = "temp";
        private static string pathSaveLayoutToXmlMainGrid = $"{pathTempDirectory}\\{nameof(RouteSheetv2Form)}_{nameof(gridViewRouteSheet)}.xml";

        private Session _session;

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
            BVVGlobal.oFuncXpo.PressEnterGrid<RouteSheetv2, RouteSheetDTO, RouteSheetv2Edit>(gridViewRouteSheet);
            BVVGlobal.oFuncXpo.PressEnterGrid<TaskRouteSheetv2, TaskRouteSheetv2Edit>(gridViewTasksCourier);
        }

        private async System.Threading.Tasks.Task UpdateGridControlAsync(int? focusedRowHandle = null)
        {
            if (focusedRowHandle is null)
            {
                focusedRowHandle = -1; 
                if (gridViewRouteSheet.GetRow(gridViewRouteSheet.FocusedRowHandle) is RouteSheetDTO routeSheetDTO)
                {
                    focusedRowHandle = gridViewRouteSheet.LocateByValue(nameof(RouteSheetDTO.Oid), routeSheetDTO.Oid);
                }
            }

            gridControlRouteSheet.DataSource = await RouteSheetDTOController.GetRouteSheetsAsync(true);

            gridViewRouteSheet.FocusedRowHandle = focusedRowHandle.Value;
            gridViewRouteSheet.SelectRow(focusedRowHandle.Value);
        }

        private async void OFuncXpo_FormClosed(object sender)
        {
            await UpdateGridControlAsync();
        }

        public RouteSheetv2Form(Session session)
        {
            InitializeComponent();
            FunctionalGridSetup();

            _session = session ?? DatabaseConnection.GetWorkSession();
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
            await UpdateGridControlAsync();

            if (gridViewRouteSheet.Columns[nameof(RouteSheetDTO.Oid)] != null)
            {
                gridViewRouteSheet.Columns[nameof(RouteSheetDTO.Oid)].Visible = false;
                gridViewRouteSheet.Columns[nameof(RouteSheetDTO.Oid)].Width = 18;
                gridViewRouteSheet.Columns[nameof(RouteSheetDTO.Oid)].OptionsColumn.FixedWidth = true;
            }

            if (gridViewRouteSheet.Columns[nameof(RouteSheetDTO.Date)] is GridColumn gridDate)
            {
                gridDate.Caption = "Дата";
                gridDate.Width = 125;
                gridDate.OptionsColumn.FixedWidth = true;
            }

            if (gridViewRouteSheet.Columns[nameof(RouteSheetDTO.Remainder)] is GridColumn gridRemainder)
            {
                gridRemainder.Caption = "Остаток в кассе на начало дня";
                gridRemainder.DisplayFormat.FormatType = FormatType.Numeric;
                gridRemainder.DisplayFormat.FormatString = "n2";
                gridRemainder.Width = 150;
                gridRemainder.OptionsColumn.FixedWidth = true;
            }

            if (gridViewRouteSheet.Columns[nameof(RouteSheetDTO.Value)] is GridColumn gridValue)
            {
                gridValue.Caption = "Внесено в кассу";
                gridValue.DisplayFormat.FormatType = FormatType.Numeric;
                gridValue.DisplayFormat.FormatString = "n2";
                gridValue.Width = 150;
                gridValue.OptionsColumn.FixedWidth = true;
            }

            if (gridViewRouteSheet.Columns[nameof(RouteSheetDTO.Spent)] is GridColumn gridSpent)
            {
                gridSpent.Caption = "Потрачено за день (нал)";
                gridSpent.DisplayFormat.FormatType = FormatType.Numeric;
                gridSpent.DisplayFormat.FormatString = "n2";
                gridSpent.Width = 150;
                gridSpent.OptionsColumn.FixedWidth = true;
            }

            if (gridViewRouteSheet.Columns[nameof(RouteSheetDTO.SpentAshlessPayment)] is GridColumn gridSpentAshlessPayment)
            {
                gridSpentAshlessPayment.Caption = "Потрачено за день (безнал)";
                gridSpentAshlessPayment.DisplayFormat.FormatType = FormatType.Numeric;
                gridSpentAshlessPayment.DisplayFormat.FormatString = "n2";
                gridSpentAshlessPayment.Width = 150;
                gridSpentAshlessPayment.OptionsColumn.FixedWidth = true;
            }

            if (gridViewRouteSheet.Columns[nameof(RouteSheetDTO.Balance)] is GridColumn gridBalance)
            {
                gridBalance.Caption = "Остаток в кассе на конец дня";
                gridBalance.DisplayFormat.FormatType = FormatType.Numeric;
                gridBalance.DisplayFormat.FormatString = "n2";
                gridBalance.Width = 150;
                gridBalance.OptionsColumn.FixedWidth = true;
            }

            gridViewRouteSheet.ColumnSetup(nameof(RouteSheetDTO.Comment), caption: "Примечание");
            gridViewRouteSheet.ColumnSetup(nameof(RouteSheetDTO.IsClosed), caption: "Закрыт", width: 100, isFixedWidth: true);

            gridViewRouteSheet.ColumnDelete(nameof(RouteSheetDTO.Payments));
            gridViewRouteSheet.ColumnDelete(nameof(RouteSheetDTO.TaskRouteSheet));
            gridViewRouteSheet.ColumnDelete(nameof(RouteSheetDTO.DateCreate));
            gridViewRouteSheet.ColumnDelete(nameof(RouteSheetDTO.DateUpdate));
            gridViewRouteSheet.ColumnDelete(nameof(RouteSheetDTO.UserCreate));
            gridViewRouteSheet.ColumnDelete(nameof(RouteSheetDTO.UserUpdate));

            //TODO: Открыть после обновления.
            //if (System.IO.File.Exists(pathSaveLayoutToXmlMainGrid))
            //{
            //    gridViewRouteSheet.RestoreLayoutFromXml(pathSaveLayoutToXmlMainGrid);
            //}
        }

        private async void btnRouteSheet_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new RouteSheetv2Edit();
            form.ShowDialog();

            var routeSheet = form.RouteSheetv2;
            if (routeSheet?.Oid != -1)
            {
                await UpdateGridControlAsync();
                gridViewRouteSheet.FocusedRowHandle = gridViewRouteSheet.LocateByValue(nameof(RouteSheetDTO.Oid), routeSheet.Oid);
            }
        }

        private async void btnEditRouteSheet_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewRouteSheet.GetRow(gridViewRouteSheet.FocusedRowHandle) is RouteSheetDTO routeSheetDTO)
            {
                var form = new RouteSheetv2Edit(routeSheetDTO.Oid);
                form.ShowDialog();

                if (form.FlagSave)
                {
                    await UpdateGridControlAsync();                    
                }
            }
        }

        private async void btnRefreshRouteSheet_ItemClick(object sender, ItemClickEventArgs e)
        {
            await UpdateGridControlAsync();
        }

        private async void btnDelRouteSheet_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewRouteSheet.IsEmpty)
            {
                return;
            }

            var listRouteSheet = new List<RouteSheetv2>();
            var msg = default(string);

            foreach (var focusedRowHandle in gridViewRouteSheet.GetSelectedRows())
            {
                if (gridViewRouteSheet.GetRow(focusedRowHandle) is RouteSheetDTO routeSheetDTO)
                {
                    var routeSheet = await _session.GetObjectByKeyAsync<RouteSheetv2>(routeSheetDTO.Oid);
                    if (routeSheet != null)
                    {
                        msg += $"{routeSheet}{Environment.NewLine}";
                        listRouteSheet.Add(routeSheet);
                    }
                }
            }

            if (XtraMessageBox.Show($"Вы собираетесь удалить следующие объекты:{Environment.NewLine}{msg}Хотите продолжить?",
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
                        foreach (var taskCourier in list.TaskRouteSheetv2)
                        {
                            taskCourier.ChronicleTaskRouteSheetv2.Add(new ChronicleTaskRouteSheetv2(_session)
                            {
                                RouteSheetv2 = taskCourier.RouteSheetv2,
                                Customer = taskCourier.Customer,
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
                            taskCourier.RouteSheetv2 = null;
                            taskCourier.Save();
                        }
                    }
                }
                else
                {
                    foreach (var list in listRouteSheet)
                    {
                        for (int i = 0; i < list.TaskRouteSheetv2.Count; i++)
                        {
                            list.TaskRouteSheetv2[i].Delete();
                        }
                    }
                }

                _session.Delete(listRouteSheet);

                await UpdateGridControlAsync(gridViewRouteSheet.FocusedRowHandle - 1);
            }
        }

        private async void gridViewRouteSheet_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridViewRouteSheet.IsEmpty)
            {
                gridControlTasksCourier.DataSource = null;
                gridViewTasksCourier.Columns.Clear();
                return;
            }

            if (gridViewRouteSheet.GetRow(gridViewRouteSheet.FocusedRowHandle) is RouteSheetDTO routeSheetDTO)
            {
                var routeSheet = await _session.GetObjectByKeyAsync<RouteSheetv2>(routeSheetDTO.Oid);
                if (routeSheet != null)
                {
                    routeSheet.Reload();
                    routeSheet.TaskRouteSheetv2.Reload();

                    gridControlTasksCourier.DataSource = routeSheet.TaskRouteSheetv2;
                    if (gridViewTasksCourier.Columns[nameof(TaskRouteSheetv2.Oid)] != null)
                    {
                        gridViewTasksCourier.Columns[nameof(TaskRouteSheetv2.Oid)].Visible = false;
                        gridViewTasksCourier.Columns[nameof(TaskRouteSheetv2.Oid)].Width = 18;
                        gridViewTasksCourier.Columns[nameof(TaskRouteSheetv2.Oid)].OptionsColumn.FixedWidth = true;
                    }

                    if (gridViewTasksCourier.Columns[nameof(TaskRouteSheetv2.IsUseRouteSheet)] is GridColumn gridIsUseRouteSheet)
                    {
                        gridIsUseRouteSheet.Width = 105;
                        gridIsUseRouteSheet.OptionsColumn.FixedWidth = true;
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
                }
            }
        }

        private void gridViewRouteSheet_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (e.MenuType == GridMenuType.User || e.MenuType == GridMenuType.Row)
                {
                    if (gridView.GetRow(gridView.FocusedRowHandle) is RouteSheetDTO routeSheetDTO)
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

            if (view.Columns[nameof(RouteSheetv2.IsClosed)] != null)
            {
                var colorNotTerminated = Color.FromArgb(255, 177, 241);
                var isClosed = Convert.ToBoolean(view.GetRowCellValue
                            (e.RowHandle, view.Columns[nameof(RouteSheetv2.IsClosed)]));

                if (isClosed)
                {
                    e.Appearance.BackColor = colorNotTerminated;
                    e.Appearance.BackColor2 = Color.White;
                }
            }
        }

        private async void barBtnFormation_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewRouteSheet.GetRow(gridViewRouteSheet.FocusedRowHandle) is RouteSheetDTO routeSheetDTO)
            {
                var routeSheet = await _session.GetObjectByKeyAsync<RouteSheetv2>(routeSheetDTO.Oid);
                if (routeSheet != null)
                {
                    var groupTaskCourierOperator = new GroupOperator(GroupOperatorType.And);
                    var criteriaTaskCouriertDate = new BinaryOperator(nameof(RouteSheetv2.Date), routeSheet.Date);
                    groupTaskCourierOperator.Operands.Add(criteriaTaskCouriertDate);

                    var collection = new XPCollection<TaskRouteSheetv2>(_session, groupTaskCourierOperator);

                    if (collection != null && collection.Count > 0)
                    {
                        if (XtraMessageBox.Show($"{routeSheet} будет автоматически заполнен из раздела курьерских задач. Продолжить?",
                            "Автоматическое заполнение маршрутного листа.",
                            MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            foreach (var taskCourier in collection)
                            {
                                if (taskCourier.RouteSheetv2 != routeSheet)
                                {
                                    taskCourier.ChronicleTaskRouteSheetv2.Add(new ChronicleTaskRouteSheetv2(_session)
                                    {
                                        RouteSheetv2 = taskCourier.RouteSheetv2,
                                        Customer = taskCourier.Customer,
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
                            routeSheet.TaskRouteSheetv2.AddRange(collection);
                            routeSheet.Save();

                            XtraMessageBox.Show($"Маршрутный лист успешно сформирован, добавлено задач: {collection.Count}",
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
        
        private void gridViewTasksCourier_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (e.MenuType == GridMenuType.User || e.MenuType == GridMenuType.Row)
                {
                    if (gridView.GetRow(gridView.FocusedRowHandle) is TaskRouteSheetv2 taskCourier)
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
        
        private async void btnTaskCourier_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewRouteSheet.GetRow(gridViewRouteSheet.FocusedRowHandle) is RouteSheetDTO routeSheetDTO)
            {
                var routeSheet = await _session.GetObjectByKeyAsync<RouteSheetv2>(routeSheetDTO.Oid);
                if (routeSheet != null)
                {
                    routeSheet.Reload();
                    var form = new TaskRouteSheetv2Edit(routeSheet);
                    form.ShowDialog();

                    var taskCourier = form.TaskRouteSheetv2;
                    if (taskCourier?.Oid != -1)
                    {
                        routeSheet.TaskRouteSheetv2.Reload();
                        gridViewTasksCourier.FocusedRowHandle = gridViewTasksCourier.LocateByValue(nameof(TaskRouteSheetv2.Oid), taskCourier.Oid);
                    }

                    if (form.FlagSave)
                    {
                        await UpdateGridControlAsync();
                    }
                }
            }        
        }

        private async void btnEditTaskCourier_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewTasksCourier.GetRow(gridViewTasksCourier.FocusedRowHandle) is TaskRouteSheetv2 taskCourier)
            {
                var form = new TaskRouteSheetv2Edit(taskCourier);
                form.ShowDialog();

                if (form.FlagSave)
                {
                    await UpdateGridControlAsync();
                }
            }
        }

        private async void btnDelTaskCourier_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewTasksCourier.IsEmpty)
            {
                return;
            }

            var listTaskCourier = new List<TaskRouteSheetv2>();
            var msg = default(string);
            
            foreach (var focusedRowHandle in gridViewTasksCourier.GetSelectedRows())
            {
                if (gridViewTasksCourier.GetRow(focusedRowHandle) is TaskRouteSheetv2 taskCourier)
                {
                    listTaskCourier.Add(taskCourier);
                    msg += $"{taskCourier}{Environment.NewLine}";
                }
            }

            if (listTaskCourier.Count > 0)
            {
                if (XtraMessageBox.Show($"Вы собираетесь удалить следующие объекты:{Environment.NewLine}{msg}Хотите продолжить?",
                    "Удаление курьерских задач",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.OK)
                {
                    _session.Delete(listTaskCourier);
                }

                await UpdateGridControlAsync();
            }            
        }

        private async void btnRefreshTaskCourier_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewRouteSheet.GetRow(gridViewRouteSheet.FocusedRowHandle) is RouteSheetDTO routeSheetDTO)
            {
                var routeSheet = await _session.GetObjectByKeyAsync<RouteSheetv2>(routeSheetDTO.Oid);
                routeSheet?.TaskRouteSheetv2?.Reload();
            }
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

        private async void btnСhooseFromAvailable_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewRouteSheet.GetRow(gridViewRouteSheet.FocusedRowHandle) is RouteSheetDTO routeSheetDTO)
            {
                var routeSheet = await _session.GetObjectByKeyAsync<RouteSheetv2>(routeSheetDTO.Oid);
                if (routeSheet != null)
                {
                    var taskCourierId = cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.TaskRouteSheetv2, -1);

                    if (taskCourierId > 0)
                    {
                        var taskCourier = _session.GetObjectByKey<TaskRouteSheetv2>(taskCourierId);

                        if (routeSheet.TaskRouteSheetv2.FirstOrDefault(f => f == taskCourier) == null)
                        {
                            routeSheet.TaskRouteSheetv2.Add(taskCourier);
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
        }
        
        private void barBtnTaskAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            BVVGlobal.oFuncXpo.CreateTask<TaskEdit>(_session);
        }

        private async void barBtnControlSystemAddRouteSheet_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewRouteSheet.GetRow(gridViewRouteSheet.FocusedRowHandle) is RouteSheetDTO routeSheetDTO)
            {
                var routeSheet = await _session.GetObjectByKeyAsync<RouteSheetv2>(routeSheetDTO.Oid);
                if (routeSheet != null)
                {
                    var form = new ControlSystemEdit(routeSheet);
                    form.ShowDialog();
                }
            }
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