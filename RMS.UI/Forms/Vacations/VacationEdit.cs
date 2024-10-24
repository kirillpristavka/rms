using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using PulsLibrary.Extensions.DevForm;
using RMS.Core.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Telegram.Bot;
using TelegramBotRMS.Core.Models;

namespace RMS.UI.Forms.Vacations
{
    public partial class VacationEdit : XtraForm
    {
        private List<Vacation> _vacantions;
        private XPCollection<VacationReplacementStaff> _vacationReplacementStaffs;

        private Session Session { get; }
        public Vacation Vacation { get; private set; }
        
        public VacationEdit()
        {
            InitializeComponent();

#if true
            layoutControlItemReplacementStaff.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
#endif

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                Vacation = new Vacation(Session);
                Vacation.DateSince = DateTime.Now.Date;
                Vacation.DateTo = Vacation.DateSince.AddDays(1);
            }
        }

        public VacationEdit(int id) : this()
        {
            if (id > 0)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                Vacation = Session.GetObjectByKey<Vacation>(id);
            }
        }

        public VacationEdit(Vacation vacation) : this()
        {
            Session = vacation.Session;
            Vacation = vacation;
        }

        public VacationEdit(Staff staff) : this()
        {
            Vacation.Staff = Session.GetObjectByKey<Staff>(staff.Oid);
        }

        private async System.Threading.Tasks.Task<List<Vacation>> GetVacantionsAsync(Session session, Vacation vacation)
        {
            if (vacation.Staff != null)
            {
                var vacantions = await new XPQuery<Vacation>(session).Where(w => w.Staff == vacation.Staff).ToListAsync();
                vacantions = vacantions
                    .Where(w => w.Oid != vacation.Oid && w.Period == vacation.Period)
                    .OrderBy(o => o.DateSince)
                    .ToList();
                
                foreach (var item in vacantions)
                {
                    var count = layoutControlGroup.Items.Count / 2;
                    var dateSince = CreateDateEdit(layoutControlGroup, "Начало отпуска:", $"DateSince{count}_ID_{item.Oid}", value: item.DateSince);
                    var dateTo = CreateDateEdit(layoutControlGroup, "Окончание отпуска:", $"DateTo{count}_ID_{item.Oid}", dateSince, value: item.DateTo);
                }
            }
            
            return new List<Vacation>();
        }
        
        private bool isSentMessage = false;
        private async void VacationEdit_Load(object sender, EventArgs e)
        {
            if (Vacation?.Oid <= 0)
            {
                isSentMessage = true;
            }

            ClearGroupLayoutWithDate();

            isUpdateDate = true;

            BVVGlobal.oFuncXpo.ButtoneEditDoubleClick<Staff>(Session, btnStaff, cls_App.ReferenceBooks.Staff);
            BVVGlobal.oFuncXpo.ButtoneEditDoubleClick<Staff>(Session, btnReplacementStaff, cls_App.ReferenceBooks.Staff);

            btnStaff.EditValue = Vacation.Staff;
            dateSince.EditValue = Vacation.DateSince;
            dateTo.EditValue = Vacation.DateTo;
            spinDuration.EditValue = Vacation.Duration;
            btnReplacementStaff.EditValue = Vacation.ReplacementStaff;
            btnVacationType.EditValue = Vacation.VacationType;
            memoComment.EditValue = Vacation.Comment;

            _vacantions = await GetVacantionsAsync(Session, Vacation);

            calendarControl.SelectedRanges.Clear();
            calendarControl.SelectedRanges.Add(new DateRange()
            {
                StartDate = Vacation.DateSince,
                EndDate = Vacation.DateTo
            });

            if (Vacation.IsConfirm)
            {
                btnSave.Enabled = false;
            }

            if (!string.IsNullOrWhiteSpace(Vacation.Period))
            {
                emptySpaceItemUseDaysVacation.Text = $"За период {Vacation.Period}";
                emptySpaceItemUseDaysVacation.AppearanceItemCaption.ForeColor = Color.Red;
                emptySpaceItemUseDaysVacation.TextVisible = true;
            }

            _vacationReplacementStaffs = Vacation.VacationReplacementStaffs;
            gridControl.DataSource = _vacationReplacementStaffs;

            if (gridView.Columns[nameof(VacationReplacementStaff.Oid)] != null)
            {
                gridView.Columns[nameof(VacationReplacementStaff.Oid)].Visible = false;
            }

            if (gridView.Columns[nameof(VacationReplacementStaff.StaffName)] != null)
            {
                var repositoryItemButtonEdit = gridControl.RepositoryItems.Add(nameof(ButtonEdit)) as RepositoryItemButtonEdit;
                repositoryItemButtonEdit.TextEditStyle = TextEditStyles.DisableTextEditor;
                repositoryItemButtonEdit.ButtonPressed += RepositoryItemMemoEdit_ButtonPressed;

                gridView.Columns[nameof(VacationReplacementStaff.StaffName)].ColumnEdit = repositoryItemButtonEdit;

                if (Vacation.IsConfirm is true)
                {
                    repositoryItemButtonEdit.Buttons[0].Visible = false;
                }
            }

            if (gridView.Columns[nameof(VacationReplacementStaff.DateSince)] != null)
            {
                gridView.Columns[nameof(VacationReplacementStaff.DateSince)].Width = 125;
                gridView.Columns[nameof(VacationReplacementStaff.DateSince)].OptionsColumn.FixedWidth = true;
                gridView.Columns[nameof(VacationReplacementStaff.DateSince)].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            }

            if (gridView.Columns[nameof(VacationReplacementStaff.DateTo)] != null)
            {
                gridView.Columns[nameof(VacationReplacementStaff.DateTo)].Width = 125;
                gridView.Columns[nameof(VacationReplacementStaff.DateTo)].OptionsColumn.FixedWidth = true;
                gridView.Columns[nameof(VacationReplacementStaff.DateTo)].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            }

            if (gridView.Columns[nameof(VacationReplacementStaff.Comment)] != null)
            {
                var repositoryItemMemoEdit = gridControl.RepositoryItems.Add(nameof(MemoEdit)) as RepositoryItemMemoEdit;
                repositoryItemMemoEdit.WordWrap = true;
                gridView.Columns[nameof(VacationReplacementStaff.Comment)].ColumnEdit = repositoryItemMemoEdit;
            }

            isUpdateDate = false;
        }

        private void ClearGroupLayoutWithDate()
        {
            var count = layoutControlGroup.Items.Count / 2;
            for (int i = count; i > 1; i--)
            {
                RemoveBaseLayoutControl(layoutControlGroup);
                RemoveBaseLayoutControl(layoutControlGroup);
            }
        }

        private async void RepositoryItemMemoEdit_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (gridView.GetRow(gridView.FocusedRowHandle) is VacationReplacementStaff obj)
            {                
                var buttonEdit = sender as ButtonEdit;

                if (e.Button.Kind == ButtonPredefines.Delete)
                {
                    buttonEdit.EditValue = null;
                    return;
                }

                var oidStaff = cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.Staff, obj.Staff?.Oid ?? -1);
               
                if (oidStaff != -1)
                {
                    obj.Staff = await Session.GetObjectByKeyAsync<Staff>(oidStaff);
                    obj.Save();
                }

                btnSave.Focus();
            }
        }

        private async void GetVacationPeriod(Staff staff)
        {
            if (staff?.DateReceipt is DateTime dateSince)
            {
                var dateEnd = dateSince.AddYears(1).AddDays(-1);
                var currentYear = DateTime.Now.Year;

                GetDateVacationPeriod(ref dateSince, ref dateEnd, currentYear);
                
                var vacationPeriod = new VacationPeriod(Session)
                {
                    DateSince = dateSince,
                    DateTo = dateEnd,
                    Staff = staff
                };

                var _vacationPeriod = await new XPQuery<VacationPeriod>(Session)
                    .Where(w => w.DateSince == vacationPeriod.DateSince
                            && w.DateTo == vacationPeriod.DateTo
                            && w.Staff == vacationPeriod.Staff)
                    .FirstOrDefaultAsync();
                
                if (_vacationPeriod != null)
                {
                    vacationPeriod = _vacationPeriod;
                }

                btnVacationPeriod.EditValue = vacationPeriod;
                Vacation.VacationPeriod = vacationPeriod;
            }
            else
            {
                btnVacationPeriod.EditValue = null;
                Vacation.VacationPeriod = null;
            }
        }

        private static void GetDateVacationPeriod(ref DateTime dateStart, ref DateTime dateEnd, int currentYear)
        {
            if (dateStart.Year <= currentYear && dateEnd.Year >= currentYear)
            {
                
            }
            else
            {
                dateStart = dateStart.AddYears(1);
                dateEnd = dateEnd.AddYears(1);
                GetDateVacationPeriod(ref dateStart, ref dateEnd, currentYear);
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            var dateSinceV = default(DateTime?);
            var dateToV = default(DateTime?);

            if (string.IsNullOrWhiteSpace(btnVacationPeriod.Text))
            {
                DevXtraMessageBox.ShowXtraMessageBox("Задайте правильный период отпуска (Пример: 01.01.2000-01.01.2050)", btnVacationPeriod);
                return;
            }
            else
            {
                var splits = btnVacationPeriod.Text?.Split('-');
                if (splits.Length != 2)
                {
                    DevXtraMessageBox.ShowXtraMessageBox("Задайте правильный период отпуска (Пример: 01.01.2000-01.01.2050)", btnVacationPeriod);
                    return;
                }

                if (DateTime.TryParse(splits[0], out DateTime _dateSinceV))
                {
                    dateSinceV = _dateSinceV;
                }


                if (DateTime.TryParse(splits[1], out DateTime _dateToV))
                {
                    dateToV = _dateToV;
                }

                if (dateSinceV is null || dateToV is null)
                {
                    DevXtraMessageBox.ShowXtraMessageBox("Задайте правильный период отпуска (Пример: 01.01.2000-01.01.2050)", btnVacationPeriod);
                    return;
                }
            }

            if (btnStaff.EditValue is Staff staff)
            {

            }
            else
            {
                btnStaff.Focus();
                return;
            }

            if (dateSince.EditValue is DateTime _dateSince)
            {

            }
            else
            {
                dateSince.Focus();
                return;
            }

            if (dateTo.EditValue is DateTime _dateTo)
            {

            }
            else
            {
                dateTo.Focus();
                return;
            }

            Vacation.Staff = staff;
            Vacation.DateSince = _dateSince;
            Vacation.DateTo = _dateTo;

            if (btnReplacementStaff.EditValue is Staff replacementStaff)
            {
                Vacation.ReplacementStaff = replacementStaff;
            }
            else
            {
                Vacation.ReplacementStaff = null;
            }

            //if (btnVacationPeriod.EditValue is VacationPeriod vacationPeriod)
            //{
            //    var _vacationPeriod = await new XPQuery<VacationPeriod>(Session)
            //        .Where(w => w.DateSince == vacationPeriod.DateSince
            //                && w.DateTo == vacationPeriod.DateTo
            //                && w.Staff == vacationPeriod.Staff)
            //        .FirstOrDefaultAsync();

            //    if (_vacationPeriod is null)
            //    {
            //        Vacation.VacationPeriod = vacationPeriod;
            //    }
            //    else
            //    {
            //        Vacation.VacationPeriod = _vacationPeriod;
            //    }
            //}
            //else
            //{
            //    Vacation.VacationPeriod = null;
            //}

            var _vacationPeriod = await new XPQuery<VacationPeriod>(Session)
                    .Where(w => w.DateSince == dateSinceV
                            && w.DateTo == dateToV
                            && w.Staff == staff)
                    .FirstOrDefaultAsync();

            if (_vacationPeriod is null)
            {
                Vacation.VacationPeriod = new VacationPeriod(Session)
                {
                    DateSince = dateSinceV.Value,
                    DateTo = dateToV.Value,
                    Staff = staff
                };
                Vacation.VacationPeriod.Save();
            }
            else
            {
                Vacation.VacationPeriod = _vacationPeriod;
            }

            if (btnVacationType.EditValue is VacationType vacationType)
            {
                Vacation.VacationType = vacationType;
            }
            else
            {
                Vacation.VacationType = null;
            }

            Vacation.Comment = memoComment.Text;

            var remainingDaysVacation = Convert.ToInt32(spinRemainingDaysVacation.EditValue);
            var duration = Convert.ToInt32(spinDuration.EditValue);

            if (duration > remainingDaysVacation)
            {
                if (XtraMessageBox.Show("Продолжительность отпуска превышает количества оставшихся дней в периода. Хотите продолжить?", 
                    "Информация по отпуску",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.Cancel)
                {
                    return;
                }
            }
            Vacation.Save();

            var vacations = new List<Vacation>();
            var count = layoutControlGroup.Items.Count;
            for (int i = 0; i < count; i += 2)
            {
                if (i == 0)
                {
                    continue;
                }

                var oid = -1;
                
                var dateSince = default(DateTime);
                var dateTo = default(DateTime);

                var dateSinceControl = ((LayoutControlItem)layoutControlGroup.Items[i]).Control as DateEdit;
                if (dateSinceControl != null)
                {
                    if (dateSinceControl.Name.Contains("ID"))
                    {
                        if (int.TryParse(dateSinceControl.Name.Split('_').LastOrDefault(), out int resultOid))
                        {
                            oid = resultOid;
                        }
                    }
                    
                    if (dateSinceControl.EditValue is DateTime _dateSinceObj)
                    {
                        dateSince = _dateSinceObj;
                    }
                    else
                    {
                        dateSinceControl.Focus();
                        return;
                    }
                }

                var dateToControl = ((LayoutControlItem)layoutControlGroup.Items[i + 1]).Control as DateEdit;
                if (dateToControl != null)
                {
                    if (dateToControl.EditValue is DateTime _dateToObj)
                    {
                        dateTo = _dateToObj;
                    }
                    else
                    {
                        dateToControl.Focus();
                        return;
                    }
                }

                if (dateSince > dateTo)
                {
                    dateToControl.EditValue = dateTo.AddDays(1);
                }

                var vacation = default(Vacation);
                if (oid > 0)
                {
                    vacation = await Session.GetObjectByKeyAsync<Vacation>(oid);
                }

                if (vacation is null)
                {
                    vacation = new Vacation(Session);
                    
                    vacations.Add(vacation);
                }

                vacation.Staff = Vacation.Staff;
                vacation.DateSince = dateSince;
                vacation.DateTo = dateTo;
                vacation.ReplacementStaff = Vacation.ReplacementStaff;
                vacation.VacationPeriod = Vacation.VacationPeriod;
                vacation.Comment = Vacation.Comment;
                vacation.Save();
            }

            if (isSentMessage)
            {
                await SendMessageTelegram(Vacation, vacations);
            }
            
            Close();
        }

        public static async System.Threading.Tasks.Task SendMessageTelegram(Vacation vacation, List<Vacation> vacations = null)
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

                var activeUser = DatabaseConnection.User;
                if (activeUser?.Staff != null && activeUser?.flagAdministrator is true)
                {
                    message = $"🆕 Пользователь {activeUser} добавил отпуск{Environment.NewLine}";
                    message += $"{Environment.NewLine}Сотрудник: <b>{staff}</b>";

                    if (!string.IsNullOrWhiteSpace(vacation.VacationTypeName))
                    {
                        message += $"{Environment.NewLine}Вид отпуска: <b>{vacation.VacationTypeName}</b>";
                    }

                    message += $"{Environment.NewLine}{Environment.NewLine}Период: <b>{vacation.DateSince.ToShortDateString()} - {vacation.DateTo.ToShortDateString()}</b>";
                    message += $"{Environment.NewLine}Продолжительность: <b>{vacation.Duration} (дн.)</b>{Environment.NewLine}";
                    
                    if (vacations != null)
                    {
                        for (int i = 0; i < vacations.Count; i++)
                        {
                            message += $"{Environment.NewLine}Период [{i + 1}]: <b>{vacations[i].DateSince.ToShortDateString()} - {vacations[i].DateTo.ToShortDateString()}</b>";
                            message += $"{Environment.NewLine}Продолжительность [{i + 1}]: <b>{vacations[i].Duration} (дн.)</b>{Environment.NewLine}";
                        }

                        message = message.Trim();
                    }

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

                using (var uof = new UnitOfWork())
                {
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

        private bool isUpdateDate;        
        private void dateChanged_EditValueChanged(object sender, EventArgs e)
        {
            if (isUpdateDate)
            {
                return;
            }
            
            isUpdateDate = true;

            var count = layoutControlGroup.Items.Count;
            var duration = 0;
            var dateRange = new DateRange[count / 2];
            
            for (int i = 0; i < count; i += 2)
            {
                var dateSince = default(DateTime);
                var dateTo = default(DateTime);

                var dateSinceControl = ((LayoutControlItem)layoutControlGroup.Items[i]).Control as DateEdit;
                if (dateSinceControl != null)
                {
                    if (dateSinceControl.EditValue is DateTime _dateSince)
                    {
                        dateSince = _dateSince;
                    }
                    else
                    {
                        isUpdateDate = false;
                        dateSinceControl.Focus();
                        return;
                    }
                }

                var dateToControl = ((LayoutControlItem)layoutControlGroup.Items[i + 1]).Control as DateEdit;
                if (dateToControl != null)
                {
                    if (dateToControl.EditValue is DateTime _dateTo)
                    {
                        dateTo = _dateTo;
                    }
                    else
                    {
                        isUpdateDate = false;
                        dateToControl.Focus();
                        return;
                    }
                }                

                if (dateSince > dateTo)
                {
                    dateToControl.EditValue = dateTo.AddDays(1);
                }

                dateRange[i / 2] = new DateRange(dateSince, dateTo);

                duration = (dateTo - dateSince).Days + 1;
            }

            calendarControl.SelectedRanges.Clear();
            calendarControl.SelectedRanges.AddRange(dateRange);

            spinDuration.EditValue = duration;

            isUpdateDate = false;
        }

        private void calendarControl_SelectionChanged(object sender, EventArgs e)
        {
            if (isUpdateDate)
            {
                return;
            }

            isUpdateDate = true;
            if (sender is CalendarControl calendarControl)
            {
                var selectedRange = calendarControl.SelectedRanges?.FirstOrDefault();
                if (selectedRange != null)
                {
                    dateSince.EditValue = selectedRange.StartDate;
                    dateTo.EditValue = selectedRange.EndDate.AddDays(-1);

                    spinDuration.EditValue = (selectedRange.EndDate - selectedRange.StartDate).Days;
                }
            }
            isUpdateDate = false;
        }
        
        private void spinDuration_EditValueChanged(object sender, EventArgs e)
        {
            if (isUpdateDate)
            {
                return;
            }

            isUpdateDate = true;
            if (sender is SpinEdit spinEdit)
            {
                if (dateSince.EditValue is DateTime _dateSince)
                {

                }
                else
                {
                    isUpdateDate = false;
                    dateSince.Focus();
                    return;
                }

                dateTo.EditValue = _dateSince.AddDays((int)spinEdit.Value - 1);

                if (dateTo.EditValue is DateTime _dateTo)
                {

                }
                else
                {
                    isUpdateDate = false;
                    dateTo.Focus();
                    return;
                }

                calendarControl.SelectedRanges.Clear();
                calendarControl.SelectedRanges.Add(new DateRange()
                {
                    StartDate = _dateSince,
                    EndDate = _dateTo.AddDays(1)
                });                
            }
            isUpdateDate = false;
        }


        private void btnStaff_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<Staff>(Session, buttonEdit, (int)cls_App.ReferenceBooks.Staff, 1, null, null, false, null, string.Empty, false, true);            
        }

        private async void btnStaff_EditValueChanged(object sender, EventArgs e)
        {
            if (sender is ButtonEdit buttonEdit)
            {
                if (buttonEdit.EditValue is Staff staff)
                {
                    if (Vacation.Staff != null && Vacation.Staff != staff)
                    {
                        Vacation = new Vacation(Session);
                        Vacation.Staff = await Session.GetObjectByKeyAsync<Staff>(staff.Oid);
                        Vacation.DateSince = DateTime.Now.Date;
                        Vacation.DateTo = Vacation.DateSince.AddDays(1);
                        VacationEdit_Load(null, null);                       
                    }
                    
                    GetVacationPeriod(staff);
                    await GetRemainingDaysVacation(staff);
                }
                else
                {
                    GetVacationPeriod(null);
                }                
            }            
        }

        private async System.Threading.Tasks.Task GetRemainingDaysVacation(Staff staff)
        {
            if (staff != null)
            {
                var vacantion = await new XPQuery<Vacation>(Session)
                                        .Where(w => w.Staff == staff
                                                && w.VacationPeriod == Vacation.VacationPeriod)
                                        .FirstOrDefaultAsync();
                if (vacantion != null)
                {
                    spinRemainingDaysVacation.EditValue = vacantion.RemainingDaysVacation;
                    spinUseDaysVacation.EditValue = vacantion.UseDaysVacation;
                    return;
                }
            }

            spinUseDaysVacation.EditValue = 0;
            spinRemainingDaysVacation.EditValue = 0;
        }

        private void layoutControlGroup_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (sender is LayoutControlGroup layoutGroup)
            {
                var count = layoutGroup.Items.Count / 2;
                if (e.Button.Properties.Caption == "Add")
                {
                    var dateSince = CreateDateEdit(layoutGroup, "Начало отпуска:", $"DateSince{count}");
                    var dateTo = CreateDateEdit(layoutGroup, "Окончание отпуска:", $"DateTo{count}", dateSince);

                }
                else if (e.Button.Properties.Caption == "Del")
                {
                    if (count > 1)
                    {
                        RemoveBaseLayoutControl(layoutGroup);
                        RemoveBaseLayoutControl(layoutGroup);
                    }
                }
            }            
        }

        private void RemoveBaseLayoutControl(LayoutControlGroup layoutGroup)
        {
            var baseLayoutControl = layoutGroup.Items[layoutGroup.Items.Count - 1];
            var control = ((LayoutControlItem)baseLayoutControl).Control;
            layoutGroup.BeginUpdate();
            layoutGroup.Remove(baseLayoutControl);
            control?.Dispose();
            layoutGroup.EndUpdate();
        }

        private LayoutControlItem CreateDateEdit(LayoutControlGroup layoutControlGroup, string text, string name, LayoutControlItem layoutControlItemBase = null, object value = null)
        {
            var dateEdit = new DateEdit();
            
            dateEdit.EditValue = value;
            dateEdit.Name = name;
            dateEdit.Properties.Appearance.Options.UseTextOptions = true;
            dateEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            dateEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            dateEdit.StyleController = layoutControlVacation;
            dateEdit.EditValueChanged += new EventHandler(dateChanged_EditValueChanged);

            var layoutControlItem = layoutControlGroup.AddItem();
            layoutControlItem.Control = dateEdit;
            layoutControlItem.Name = $"layoutControlItem{name}";
            layoutControlItem.Text = text;

            if (layoutControlItemBase != null)
            {
                layoutControlItem.Move(layoutControlItemBase, DevExpress.XtraLayout.Utils.InsertType.Right);
            }

            return layoutControlItem;
        }

        private void btnVacationType_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<VacationType>(Session, buttonEdit, (int)cls_App.ReferenceBooks.VacationType, 1, null, null, false, null, string.Empty, false, true);
        }

        private void barBtnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Vacation.IsConfirm is false)
            {
                var dateSince = default(DateTime?);
                var dateTo = default(DateTime?);

                if (this.dateSince.EditValue is DateTime _dateSince)
                {
                    dateSince = _dateSince;
                }
                
                if (this.dateTo.EditValue is DateTime _dateTo)
                {
                    dateTo = _dateTo;
                }

                Vacation.VacationReplacementStaffs.Add(new VacationReplacementStaff(Session)
                {
                    DateSince = dateSince,
                    DateTo = dateTo
                });
            }
        }

        private void barBtnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barBtnDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView.GetRow(gridView.FocusedRowHandle) is VacationReplacementStaff obj)
            {
                var text = $"Вы действительно хотите удалить заменяющего сотрудника: {obj}?";
                var caption = $"Удаление [OID:{obj.Oid}]";

                if (XtraMessageBox.Show(text,
                                        caption,
                                        MessageBoxButtons.OKCancel,
                                        MessageBoxIcon.Question) == DialogResult.OK)
                {
                    if (obj.Vacation != null && obj.Vacation.IsConfirm is false)
                    {
                        obj.Delete();
                    }
                }
            }
        }

        private void gridView_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (e.MenuType == GridMenuType.User || e.MenuType == GridMenuType.Row)
                {
                    barBtnEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    
                    if (e.MenuType != GridMenuType.Row)
                    {
                        barBtnEdit.Enabled = false;
                        barBtnDel.Enabled = false;
                    }
                    else
                    {
                        barBtnEdit.Enabled = true;
                        barBtnDel.Enabled = true;
                    }                    

                    popupMenu.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }
    }
}