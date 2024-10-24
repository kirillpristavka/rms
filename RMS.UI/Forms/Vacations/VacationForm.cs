using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using RMS.Core.Model;
using System;
using System.Windows.Forms;
using DevExpress.XtraScheduler;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.XtraScheduler.Drawing;
using System.Diagnostics;
using DevExpress.Data.Filtering;
using DevExpress.XtraEditors.Repository;
using TelegramBotRMS.Core.Models;
using Telegram.Bot;

namespace RMS.UI.Forms.Vacations
{
    public partial class VacationForm : XtraForm
    {
        private DateTime _currentDateTime = DateTime.Now;
        
        private Session _session { get; }
        private XPCollection<Vacation> Vacations { get; set; }        

        /// <summary>
        /// Настройка функционирования таблиц.
        /// </summary>
        private void FunctionalGridSetup()
        {
            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref gridView);
            BVVGlobal.oFuncXpo.PressEnterGrid<Vacation, VacationEdit>(gridView, action: () => UpdateDate());
        }

        public VacationForm(Session session)
        {
            InitializeComponent();
            FunctionalGridSetup();

            _session = session ?? BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
        }

        public async void UpdateObj() 
        {
            foreach (var item in Vacations)
            {
                if (item.ReplacementStaff != null)
                {
                    item.VacationReplacementStaffs.Add(new VacationReplacementStaff(_session)
                    {
                        Staff = await _session.GetObjectByKeyAsync<Staff>(item.ReplacementStaff.Oid)
                    });
                    item.ReplacementStaff = null;
                    item.Save();
                }
            }
        }       

        private async void Form_Load(object sender, EventArgs e)
        {            
            var criteria = default(CriteriaOperator);

            var user = await _session.GetObjectByKeyAsync<User>(DatabaseConnection.User?.Oid);
            user?.Reload();

            if (!user.flagAdministrator && user.Staff != null)
            {
                //criteria = new BinaryOperator($"{nameof(Vacation.Staff)}.{nameof(Staff.Oid)}", user.Staff?.Oid);
            }

            Vacations = new XPCollection<Vacation>(_session, criteria, new SortProperty(nameof(Vacation.DateSince), DevExpress.Xpo.DB.SortingDirection.Ascending));
            gridControl.DataSource = Vacations;

            //TODO: удалить после преобразования.
            UpdateObj();


            if (gridView.Columns[nameof(Vacation.Oid)] != null)
            {
                gridView.Columns[nameof(Vacation.Oid)].Visible = false;
                gridView.Columns[nameof(Vacation.Oid)].Width = 18;
                gridView.Columns[nameof(Vacation.Oid)].OptionsColumn.FixedWidth = true;
            }

            if (gridView.Columns[nameof(Vacation.IsConfirm)] != null)
            {
                gridView.Columns[nameof(Vacation.IsConfirm)].Caption = " ";
                gridView.Columns[nameof(Vacation.IsConfirm)].Width = 25;
                gridView.Columns[nameof(Vacation.IsConfirm)].OptionsColumn.FixedWidth = true;
            }

            if (gridView.Columns[nameof(Vacation.DateSince)] != null)
            {
                gridView.Columns[nameof(Vacation.DateSince)].Width = 125;
                gridView.Columns[nameof(Vacation.DateSince)].OptionsColumn.FixedWidth = true;
            }

            if (gridView.Columns[nameof(Vacation.Period)] != null)
            {
                gridView.Columns[nameof(Vacation.Period)].Width = 195;
                gridView.Columns[nameof(Vacation.Period)].OptionsColumn.FixedWidth = true;
            }

            if (gridView.Columns[nameof(Vacation.DurationValue)] != null)
            {
                gridView.Columns[nameof(Vacation.DurationValue)].Width = 80;
                gridView.Columns[nameof(Vacation.DurationValue)].OptionsColumn.FixedWidth = true;
            }
            
            if (gridView.Columns[nameof(Vacation.UseDaysVacation)] != null)
            {
                gridView.Columns[nameof(Vacation.UseDaysVacation)].Width = 80;
                gridView.Columns[nameof(Vacation.UseDaysVacation)].OptionsColumn.FixedWidth = true;
            }
            
            if (gridView.Columns[nameof(Vacation.RemainingDaysVacation)] != null)
            {
                gridView.Columns[nameof(Vacation.RemainingDaysVacation)].Width = 80;
                gridView.Columns[nameof(Vacation.RemainingDaysVacation)].OptionsColumn.FixedWidth = true;
            }

            if (gridView.Columns[nameof(Vacation.DateTo)] != null)
            {
                gridView.Columns[nameof(Vacation.DateTo)].Width = 125;
                gridView.Columns[nameof(Vacation.DateTo)].OptionsColumn.FixedWidth = true;
            }

            if (gridView.Columns[nameof(Vacation.ReplacementStaffName)] != null)
            {
                var repositoryItemMemoEdit = gridControl.RepositoryItems.Add(nameof(MemoEdit)) as RepositoryItemMemoEdit;
                repositoryItemMemoEdit.WordWrap = true;
                gridView.Columns[nameof(Vacation.ReplacementStaffName)].ColumnEdit = repositoryItemMemoEdit;
            }

            if (gridView.Columns[nameof(Vacation.Comment)] != null)
            {
                var repositoryItemMemoEdit = gridControl.RepositoryItems.Add(nameof(MemoEdit)) as RepositoryItemMemoEdit;
                repositoryItemMemoEdit.WordWrap = true;
                gridView.Columns[nameof(Vacation.Comment)].ColumnEdit = repositoryItemMemoEdit;
            }

            UpdateScheduler(Vacations);
        }

        private Dictionary<int, Color> staffColors = new Dictionary<int, Color>();
        
        private void UpdateScheduler(IEnumerable<Vacation> collection)
        {
            try
            {
                var vacantions = collection
                    .Where(w => /*w.IsConfirm && */
                            (w.DateSince.Year == _currentDateTime.Year
                            || w.DateTo.Year == _currentDateTime.Year));


                schedulerControl.DataStorage.BeginUpdate();
                staffColors.Clear();
                schedulerControl.DataStorage.Appointments.Clear();

                if (vacantions != null && vacantions.Count() > 0)
                {
                    Appointment[] appArray = new Appointment[vacantions.Count()];
                    AppointmentCollection appCollection = new AppointmentCollection();

                    foreach (var item in vacantions)
                    {
                        try
                        {
                            GetAppointment(appCollection, item);
                        }
                        catch (Exception ex)
                        {
                            RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                        }
                    }

                    appCollection.CopyTo(appArray, 0);

                    schedulerControl.DataStorage.Appointments.AddRange(appArray);
                }

                schedulerControl.DataStorage.EndUpdate();
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private void GetAppointment(AppointmentCollection appCollection, Vacation item)
        {
            var staffName = "Имя не определено";
            var staffOid = -1;
            if (item.Staff != null)
            {
                staffOid = item.Staff.Oid;
                staffName = item.Staff.ToString();
            }

            var appointment = schedulerControl.DataStorage.CreateAppointment(AppointmentType.Normal);
            appointment.Start = item.DateSince;
            appointment.End = item.DateTo.AddDays(1);
            appointment.Subject = $"{staffName} ({item.Duration})";
            appointment.LabelKey = staffOid;

            appCollection.Add(appointment);

            if (!staffColors.ContainsKey(staffOid))
            {
                FillStaffColors(staffOid);
            }
        }

        private void FillStaffColors(int staffOid)
        {
            var rnd = new Random(DateTime.Now.Millisecond);
            var randomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));

            if (!staffColors.ContainsValue(randomColor))
            {
                staffColors.Add(staffOid, randomColor);
            }
            else
            {
                FillStaffColors(staffOid);
            }
        }

        private void barBtnRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            Vacations?.Reload();
        }        
        
        private void schedulerControl_AppointmentViewInfoCustomizing(object sender, AppointmentViewInfoCustomizingEventArgs e)
        {
            try
            {
                if (int.TryParse(e.ViewInfo.Appointment.LabelKey?.ToString(), out int result))
                {
                    if (staffColors.ContainsKey(result))
                    {
                        e.ViewInfo.Appearance.BackColor = staffColors[result];
                    }
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async void gridView_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (e.MenuType == GridMenuType.User || e.MenuType == GridMenuType.Row)
                {
                    if (e.MenuType != GridMenuType.Row)
                    {
                        barBtnEdit.Enabled = false;
                        barBtnDel.Enabled = false;
                    }
                    else
                    {
                        barBtnEdit.Enabled = true;
                        barBtnDel.Enabled = true;

                        var currentUserOid = DatabaseConnection.User?.Oid ?? -1;
                        var user = await new XPQuery<User>(_session)?.FirstOrDefaultAsync(f => f.Oid == currentUserOid);
                        if (user != null && user.flagAdministrator)
                        {
                            if (gridView.GetRow(gridView.FocusedRowHandle) is Vacation vacation)
                            {
                                if (vacation.IsConfirm is false)
                                {
                                    barBntConfirm.Caption = "Утвердить";
                                }
                                else
                                {
                                    barBntConfirm.Caption = "Снять подтверждение";
                                }
                            }

                            barBntConfirm.Enabled = true;
                        }
                        else
                        {
                            barBntConfirm.Enabled = false;
                        }
                    }

                    popupMenu.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }

        private void UpdateDate()
        {
            Vacations?.Reload();
            UpdateScheduler(Vacations);
        }
        
        private async void barBtnAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            var currentUserOid = DatabaseConnection.User?.Oid ?? -1;
            var user = await new XPQuery<User>(_session)?.FirstOrDefaultAsync(f => f.Oid == currentUserOid);
            user?.Reload();

            if (user.Staff != null)
            {
                var form = new VacationEdit(user.Staff);
                form.ShowDialog();
                UpdateDate();
            }
        }

        private void barBtnEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridView.GetRow(gridView.FocusedRowHandle) is Vacation vacation)
            {
                var form = new VacationEdit(vacation);
                form.ShowDialog();
                UpdateDate();
            }
        }

        private async void barBtnDel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridView.GetRow(gridView.FocusedRowHandle) is Vacation vacation)
            {
                var currentUserOid = DatabaseConnection.User?.Oid ?? -1;
                var user = await new XPQuery<User>(_session)?.FirstOrDefaultAsync(f => f.Oid == currentUserOid);
                user?.Reload();
                
                if (user.flagAdministrator || vacation.Staff == user.Staff)
                {
                    if (vacation.IsConfirm is false)
                    {
                        vacation.Delete();
                        UpdateDate();
                    }
                    else
                    {
                        XtraMessageBox.Show("Для удаления отпуска необходимо снять его подтверждение.", "Удаление отпуска", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    XtraMessageBox.Show("Удаление чужих отпусков доступно только администраторам.", "Удаление отпуска", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        public static async System.Threading.Tasks.Task SendMessageTelegram(Vacation vacation)
        {
            try
            {
                if (vacation is null)
                {
                    return;
                }

                var staff = vacation.Staff;
                if (staff is null)
                {
                    return;
                }

                var dispatchSheet = new List<Staff>();

                var message = default(string);

                var currentUserOid = DatabaseConnection.User?.Oid ?? -1;
                var activeUser = await new XPQuery<User>(vacation.Session)?.FirstOrDefaultAsync(f => f.Oid == currentUserOid);

                if (activeUser?.Staff != null && activeUser?.flagAdministrator is true)
                {
                    message = $"💢 Пользователь {activeUser}{Environment.NewLine}{Environment.NewLine}";

                    if (vacation.IsConfirm)
                    {
                        message += "✅ УТВЕРДИЛ ОТПУСК ✅";
                    }
                    else
                    {
                        message += "❌ ОТМЕНИЛ ПОДТВЕРЖДЕНИЕ ОТПУСКА ❌";
                    }

                    message += $"{Environment.NewLine}{Environment.NewLine}Сотрудник: <b>{staff}</b>";

                    if (!string.IsNullOrWhiteSpace(vacation.VacationTypeName))
                    {
                        message += $"{Environment.NewLine}Вид отпуска: <b>{vacation.VacationTypeName}</b>";
                    }

                    message += $"{Environment.NewLine}{Environment.NewLine}Период: <b>{vacation.DateSince.ToShortDateString()} - {vacation.DateTo.ToShortDateString()}</b>";
                    message += $"{Environment.NewLine}Продолжительность: <b>{vacation.Duration} (дн.)</b>";

                    if (vacation.VacationReplacementStaffs != null && vacation.VacationReplacementStaffs.Count > 0)
                    {
                        message += $"{Environment.NewLine}{Environment.NewLine}Сотрудники замены:{Environment.NewLine}";

                        var i = 1;
                        foreach (var replacementStaff in vacation.VacationReplacementStaffs)
                        {
                            if (replacementStaff.Staff != null)
                            {
                                var replacementStaffString = $"[{i}] {replacementStaff.Staff} ";

                                var dateString = default(string);
                                if (replacementStaff.DateSince is DateTime dateSince)
                                {
                                    dateString += $"{dateSince.ToShortDateString()} ";
                                }

                                if (replacementStaff.DateTo is DateTime dateTo)
                                {
                                    dateString += $"{dateTo.ToShortDateString()}";
                                }

                                if (!string.IsNullOrWhiteSpace(dateString))
                                {
                                    replacementStaffString += $"({dateString?.Trim()?.Replace(" ", " - ")})";
                                    dispatchSheet.Add(replacementStaff.Staff);
                                    message += $"<b>{replacementStaffString}</b>{Environment.NewLine}";
                                    i++;
                                }
                            }
                        }
                    }

                    dispatchSheet.Add(staff);
                    dispatchSheet.Add(activeUser.Staff);
                }

                await SendTelegramAdminMessageAsync(message, dispatchSheet).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private static async System.Threading.Tasks.Task SendTelegramAdminMessageAsync(string message, List<Staff> dispatchSheet = default)
        {
            try
            {
                if (dispatchSheet is null)
                {
                    dispatchSheet = new List<Staff>();
                }

                using var uof = new UnitOfWork();
                var administrations = await new XPQuery<User>(uof)?.Where(w => w.flagAdministrator)?.ToListAsync();
                if (administrations != null)
                {
                    foreach (var admin in administrations)
                    {
                        var adminStaff = admin?.Staff;
                        if (adminStaff != null)
                        {
                            dispatchSheet.Add(adminStaff);
                        }
                    }
                }

                var client = TelegramBot.GetTelegramBotClient(uof);
                var distinctStaffs = dispatchSheet.Distinct();
                foreach (var obj in distinctStaffs)
                {
                    if (obj?.TelegramUserId != null)
                    {
                        await client.SendTextMessageAsync(obj.TelegramUserId, message, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html).ConfigureAwait(false);
                    }
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async void barBntConfirm_ItemClick(object sender, ItemClickEventArgs e)
        {
            var user = DatabaseConnection.User;

            if (user != null
                && (user.flagAdministrator || user.UserGroups.FirstOrDefault(f => f.UserGroup?.Name != null
                    && (f.UserGroup?.Name.Equals("руководители", StringComparison.OrdinalIgnoreCase) is true 
                    || f.UserGroup?.Name.Equals("администратор", StringComparison.OrdinalIgnoreCase) is true)) != null))
            {
                if (gridView.GetRow(gridView.FocusedRowHandle) is Vacation vacation)
                {
                    if (vacation.IsConfirm is false)
                    {
                        vacation.IsConfirm = true;
                    }
                    else
                    {
                        vacation.IsConfirm = false;
                    }

                    vacation.Save();
                    UpdateDate();

                    await SendMessageTelegram(vacation).ConfigureAwait(false);
                }
            }
            else
            {
                XtraMessageBox.Show("Операции подтверждения доступны только администратором системы.",
                                    "Информационное сообщение",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                if (user != null)
                {
                    var message = $"Пользователь {user} попытался подтвердить свой отпуск, но операция была отменена. У пользователя не хватает прав.";
                    await SendTelegramAdminMessageAsync(message).ConfigureAwait(false);
                }                
            }
        }        

        private void toolTipController_BeforeShow(object sender, DevExpress.Utils.ToolTipControllerShowEventArgs e)
        {
            if (sender is ToolTipController toolTipController)
            {
                if (toolTipController.ActiveObject is AppointmentViewInfo appointmentViewInfo)
                {
                    if (toolTipController.ToolTipType == ToolTipType.Standard)
                    {
                        e.IconType = ToolTipIconType.Information;
                        e.ToolTip = appointmentViewInfo.Description;
                    }

                    if (toolTipController.ToolTipType == ToolTipType.SuperTip)
                    {
                        SuperToolTip SuperTip = new SuperToolTip();
                        SuperToolTipSetupArgs args = new SuperToolTipSetupArgs();
                        args.Title.Text = "Info";
                        args.Contents.Text = appointmentViewInfo.Description;
                        args.ShowFooterSeparator = true;
                        args.Footer.Text = "SuperTip";
                        SuperTip.Setup(args);
                        e.SuperTip = SuperTip;
                    }
                }
            }
        }

        private void schedulerControl_PopupMenuShowing(object sender, DevExpress.XtraScheduler.PopupMenuShowingEventArgs e)
        {
            e.Menu.Items.Clear();
        }

        private void gridView_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (gridView.GetRow(e.RowHandle) is Vacation vacation)
                {
                    if (vacation.IsConfirm)
                    {
                        e.Appearance.BackColor = Color.LightGreen;
                    }
                }
            }
        }

        private void barBtnUpdate_ItemClick(object sender, ItemClickEventArgs e)
        {
            Vacations?.Reload();
        }

        private async void barBtnCustomerVacationStatistics_ItemClick(object sender, ItemClickEventArgs e)
        {
            var customerVacationStatistics = new Core.Controller.Print.CustomerVacationStatistics();
            var path = await customerVacationStatistics.GetReportAsync();
            Process.Start(path);
        }
    }
}