using DevExpress.Data.Filtering;
using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Forms.Directories
{
    public partial class ArchiveFolderChangeEdit : XtraForm
    {
        private Session Session { get; }
        private Customer Customer { get; }
        public ArchiveFolderChange ArchiveFolderChange { get; private set; }
        public List<ArchiveFolderChange> ArchiveFolderChanges { get; }
        public bool IsMassChange { get; set; }

        public ArchiveFolderChangeEdit(Session session = null)
        {
            InitializeComponent();

            foreach (StatusArchiveFolder item in Enum.GetValues(typeof(StatusArchiveFolder)))
            {
                if (item == StatusArchiveFolder.CURRENT)
                {
                    continue;
                }
                
                cmbStatusArchiveFolder.Properties.Items.Add(item.GetEnumDescription());
            }
            cmbStatusArchiveFolder.SelectedIndex = 0;

            foreach (PeriodArchiveFolder item in Enum.GetValues(typeof(PeriodArchiveFolder)))
            {
                cmbPeriod.Properties.Items.Add(item.GetEnumDescription());
            }

            foreach (Month item in Enum.GetValues(typeof(Month)))
            {
                cmbMonth.Properties.Items.Add(item.GetEnumDescription());
            }
            
            Session = session ?? BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
            XPObject.AutoSaveOnEndEdit = false;
        }

        public ArchiveFolderChangeEdit(ArchiveFolderChange archiveFolderChange) : this(archiveFolderChange.Session)
        {
            ArchiveFolderChange = archiveFolderChange;
            Customer = archiveFolderChange.Customer;
        }

        public ArchiveFolderChangeEdit(Customer customer) : this(customer.Session)
        {            
            Customer = customer;
            ArchiveFolderChange = new ArchiveFolderChange(Session);
        }
        
        public ArchiveFolderChangeEdit(Session session, List<ArchiveFolderChange> obj) : this(session)
        {
            IsMassChange = true;
            Session = session;
            ArchiveFolderChanges = obj;
        }
        
        private async void ReportChangeEdit_Load(object sender, EventArgs e)
        {
            await SetAccessRights();

            if (ArchiveFolderChange is null)
            {
                ArchiveFolderChange = new ArchiveFolderChange(Session);
            }

            btnCustomer.EditValue = Customer;
            btnArchiveFolder.EditValue = ArchiveFolderChange.ArchiveFolder;
            cmbYear.EditValue = (ArchiveFolderChange.Year == 0) ? DateTime.Now.Year : ArchiveFolderChange.Year;
            dateIssue.EditValue = ArchiveFolderChange.DateIssue;
            cmbStatusArchiveFolder.EditValue = ArchiveFolderChange.StatusArchiveFolder.GetEnumDescription();

            if (ArchiveFolderChange.Period != null)
            {
                cmbPeriod.SelectedIndex = (int)ArchiveFolderChange.Period;

                if (ArchiveFolderChange.Period == PeriodArchiveFolder.QUARTER || ArchiveFolderChange.Period == PeriodArchiveFolder.HALFYEAR)
                {
                    cmbPeriodChange.EditValue = ArchiveFolderChange.PeriodChange?.GetEnumDescription();
                }
            }

            if (ArchiveFolderChange.Month is null)
            {
                cmbMonth.SelectedIndex = -1;
            }
            else
            {
                cmbMonth.SelectedIndex = (int)ArchiveFolderChange.Month - 1;
            }

            if (ArchiveFolderChange.Oid == -1 && ArchiveFolderChange.Staffed is null)
            {
                btnStaffed.EditValue = Customer?.PrimaryResponsible;
            }
            else
            {
                btnStaffed.EditValue = ArchiveFolderChange.Staffed;
            }

            if (ArchiveFolderChange.Oid == -1 && ArchiveFolderChange.AccountantResponsible is null)
            {
                btnAccountantResponsible.EditValue = Customer?.AccountantResponsible;
            }
            else
            {
                btnAccountantResponsible.EditValue = ArchiveFolderChange.AccountantResponsible;
            }

            spinBoxNumber.EditValue = ArchiveFolderChange.BoxNumber ?? 1;
            memoComment.EditValue = ArchiveFolderChange.Comment;

            gridControl.DataSource = ArchiveFolderChange.ArchiveFolderChangePeriods;

            foreach (GridColumn column in gridView.Columns)
            {
                column.OptionsColumn.AllowEdit = false;
                column.OptionsColumn.ReadOnly = true;
            }

            if (gridView.Columns[nameof(ArchiveFolderChangePeriod.Oid)] != null)
            {
                gridView.Columns[nameof(ArchiveFolderChangePeriod.Oid)].Visible = false;
                gridView.Columns[nameof(ArchiveFolderChangePeriod.Oid)].Width = 18;
                gridView.Columns[nameof(ArchiveFolderChangePeriod.Oid)].OptionsColumn.FixedWidth = true;
            }

            if (gridView.Columns[nameof(ArchiveFolderChangePeriod.Year)] != null)
            {
                gridView.Columns[nameof(ArchiveFolderChangePeriod.Year)].OptionsColumn.AllowEdit = true;
                gridView.Columns[nameof(ArchiveFolderChangePeriod.Year)].OptionsColumn.ReadOnly = false;
            }

            if (gridView.Columns[nameof(ArchiveFolderChangePeriod.PeriodReportChange)] != null)
            {
                gridView.Columns[nameof(ArchiveFolderChangePeriod.PeriodReportChange)].OptionsColumn.AllowEdit = true;
                gridView.Columns[nameof(ArchiveFolderChangePeriod.PeriodReportChange)].OptionsColumn.ReadOnly = false;
            }

            if (gridView.Columns[nameof(ArchiveFolderChangePeriod.Month)] != null)
            {
                gridView.Columns[nameof(ArchiveFolderChangePeriod.Month)].OptionsColumn.AllowEdit = true;
                gridView.Columns[nameof(ArchiveFolderChangePeriod.Month)].OptionsColumn.ReadOnly = false;
            }

            if (IsMassChange)
            {
                btnCustomer.Properties.ReadOnly = true;
                btnArchiveFolder.Properties.ReadOnly = true;
                spinBoxNumber.Properties.ReadOnly = true;

                cmbYear.EditValue = null;
                cmbStatusArchiveFolder.EditValue = null;
            }
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            var isSave = false;

            if (IsMassChange)
            {
                isSave = SaveMass();
            }
            else
            {
                isSave = Save();
            }

            if (isSave)
            {
                Close();
            }
        }
        
        private bool SaveMass()
        {
            var isUpdate = false;
            
            var accountantResponsible = default(Staff);
            var staffed = default(Staff);
            var currentDateIssue = default(DateTime?);
            var statusArchiveFolder = default(StatusArchiveFolder?);
            var comment = default(string);
            var periodArchiveFolder = default(PeriodArchiveFolder?);
            var month = default(Month?);
            var periodReportChange = default(PeriodReportChange?);
            var year = default(int?);
            
            var userUpdate = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid);
            var dateUpdate = DateTime.Now;

            if (btnAccountantResponsible.EditValue is Staff _accountantResponsible)
            {
                accountantResponsible = _accountantResponsible;
                isUpdate = true;
            }

            if (dateIssue.EditValue is DateTime _dateIssue)
            {
                currentDateIssue = _dateIssue;
                isUpdate = true;
            }
            
            if (btnStaffed.EditValue is Staff _staffed)
            {
                staffed = _staffed;
                isUpdate = true;
            }

            if (!string.IsNullOrWhiteSpace(memoComment.Text))
            {
                comment = memoComment.Text;
                isUpdate = true;
            }            

            if (cmbPeriod.SelectedIndex != -1)
            {
                periodArchiveFolder = (PeriodArchiveFolder)cmbPeriod.SelectedIndex;
                isUpdate = true;
            }
            
            if (cmbStatusArchiveFolder.EditValue != null)
            {
                foreach (StatusArchiveFolder item in Enum.GetValues(typeof(StatusArchiveFolder)))
                {
                    if (item.GetEnumDescription().Equals(cmbStatusArchiveFolder.Text))
                    {
                        statusArchiveFolder = item;
                        break;
                    }
                }
                
                isUpdate = true;
            }

            if (cmbMonth.SelectedIndex == -1)
            {
                if (periodArchiveFolder == PeriodArchiveFolder.MONTH)
                {
                    XtraMessageBox.Show("Невозможно сохранить без месяца.",
                                    "Ошибка сохранения",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    cmbMonth.Focus();
                    return false;
                }
                else
                {
                    month = null;
                }
            }
            else
            {
                month = (Month)cmbMonth.SelectedIndex + 1;
                isUpdate = true;
            }

            if (periodArchiveFolder == PeriodArchiveFolder.QUARTER || periodArchiveFolder == PeriodArchiveFolder.HALFYEAR)
            {
                foreach (PeriodReportChange item in Enum.GetValues(typeof(PeriodReportChange)))
                {
                    if (item.GetEnumDescription().Equals(cmbPeriodChange.EditValue))
                    {
                        periodReportChange = item;
                        isUpdate = true;
                        break;
                    }
                }
            }
            else
            {
                periodReportChange = null;
            }

            if (int.TryParse(cmbYear.Text, out int _year))
            {
                if (_year > 2000 && _year <= 2100)
                {
                    year = _year; 
                    isUpdate = true;
                }
            }

            if (isUpdate is false)
            {
                return true;
            }
            
            foreach (var obj in ArchiveFolderChanges)
            {
                if (accountantResponsible != null)
                {
                    obj.AccountantResponsible = accountantResponsible;
                }

                if (staffed != null)
                {
                    obj.Staffed = staffed;
                }

                if (currentDateIssue != null)
                {
                    obj.DateIssue = currentDateIssue;
                }

                if (statusArchiveFolder != null)
                {
                    obj.StatusArchiveFolder = (StatusArchiveFolder)statusArchiveFolder;
                }

                if (periodArchiveFolder != null)
                {
                    obj.Period = periodArchiveFolder;
                }

                if (month != null)
                {
                    obj.Month = month;
                }

                if (periodReportChange != null)
                {
                    obj.PeriodChange = periodReportChange;
                }

                if (year != null)
                {
                    obj.Year = (int)year;
                }

                if (!string.IsNullOrWhiteSpace(comment))
                {
                    obj.Comment = $"{obj.Comment} {comment}"?.Trim();
                }
                
                obj.UserUpdate = userUpdate;
                obj.DateUpdate = dateUpdate;

                obj.Save();
            }
            
            return true;
        }

        private bool Save()
        {
            var accountantResponsible = btnAccountantResponsible.EditValue as Staff;
            if (accountantResponsible != null)
            {
                ArchiveFolderChange.AccountantResponsible = accountantResponsible;
            }
            else
            {
                ArchiveFolderChange.AccountantResponsible = null;
            }

            var customer = btnCustomer.EditValue as Customer;
            if (customer != null)
            {
                ArchiveFolderChange.Customer = customer;
            }
            else
            {
                XtraMessageBox.Show("Сохранение не возможно без указанного клиента.", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnCustomer.Focus();
                return false;
            }

            var archiveFolder = btnArchiveFolder.EditValue as ArchiveFolder;
            if (archiveFolder != null)
            {
                ArchiveFolderChange.ArchiveFolder = archiveFolder;
            }
            else
            {
                ArchiveFolderChange.ArchiveFolder = null;
            }

            ArchiveFolderChange.DateIssue = dateIssue.DateTime == DateTime.MinValue ? default(DateTime?) : Convert.ToDateTime(dateIssue.EditValue);

            if (cmbStatusArchiveFolder.EditValue != null)
            {
                foreach (StatusArchiveFolder item in Enum.GetValues(typeof(StatusArchiveFolder)))
                {
                    if (item.GetEnumDescription().Equals(cmbStatusArchiveFolder.Text))
                    {
                        ArchiveFolderChange.StatusArchiveFolder = item;
                        break;
                    }
                }
            }
            else
            {
                XtraMessageBox.Show("Укажите статус архивной папки", "Операция с объектом", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            var staffed = btnStaffed.EditValue as Staff;
            if (staffed != null)
            {
                ArchiveFolderChange.Staffed = staffed;
            }
            else
            {
                ArchiveFolderChange.Staffed = null;
            }

            ArchiveFolderChange.BoxNumber = Convert.ToInt32(spinBoxNumber.EditValue);

            ArchiveFolderChange.Comment = memoComment.Text;

            if (cmbPeriod.SelectedIndex != -1)
            {
                ArchiveFolderChange.Period = (PeriodArchiveFolder)cmbPeriod.SelectedIndex;
            }
            else
            {
                XtraMessageBox.Show("Невозможно сохранить без периода.",
                                    "Ошибка сохранения",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                cmbPeriod.Focus();
                return false;
            }

            if (cmbMonth.SelectedIndex == -1)
            {
                if (ArchiveFolderChange.Period == PeriodArchiveFolder.MONTH)
                {
                    XtraMessageBox.Show("Невозможно сохранить без месяца.",
                                    "Ошибка сохранения",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    cmbMonth.Focus(); 
                    return false;
                }
                else
                {
                    ArchiveFolderChange.Month = null;
                }
            }
            else
            {
                ArchiveFolderChange.Month = (Month)cmbMonth.SelectedIndex + 1;
            }

            if (ArchiveFolderChange.Period == PeriodArchiveFolder.QUARTER || ArchiveFolderChange.Period == PeriodArchiveFolder.HALFYEAR)
            {
                foreach (PeriodReportChange item in Enum.GetValues(typeof(PeriodReportChange)))
                {
                    if (item.GetEnumDescription().Equals(cmbPeriodChange.EditValue))
                    {
                        ArchiveFolderChange.PeriodChange = item;
                    }
                }
            }
            else
            {
                ArchiveFolderChange.PeriodChange = null;
            }

            if (int.TryParse(cmbYear.Text, out int year))
            {
                if (year > 2000 && year <= 2100)
                {
                    ArchiveFolderChange.Year = year;
                }
                else
                {
                    XtraMessageBox.Show("Год не попадает в период от 2000 до 2100", "Ошибка сохранения дня", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            if (ArchiveFolderChange.UserCreate is null)
            {
                ArchiveFolderChange.UserCreate = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid);
                ArchiveFolderChange.DateCreate = DateTime.Now;
            }
            else
            {
                ArchiveFolderChange.UserUpdate = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid);
                ArchiveFolderChange.DateUpdate = DateTime.Now;
            }

            ArchiveFolderChange.Save();
            Session.Save(ArchiveFolderChange.ArchiveFolderChangePeriods);

            return true;
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCustomer_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            cls_BaseSpr.ButtonEditButtonClickBase<Customer>(Session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.Customer, 1, null, null, false, null, string.Empty, false, true);
        }

        private bool isEditArchiveFolderChangeForm = false;
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
                            isEditArchiveFolderChangeForm = accessRights.IsEditArchiveFolderChangeForm;
                        }

                        btnSave.Enabled = isEditArchiveFolderChangeForm;
                        barBtnAdd.Enabled = isEditArchiveFolderChangeForm;
                        barBtnDel.Enabled = isEditArchiveFolderChangeForm;

                        CustomerEdit.CloseButtons(btnAccountantResponsible, isEditArchiveFolderChangeForm);
                        CustomerEdit.CloseButtons(btnCustomer, isEditArchiveFolderChangeForm);
                        CustomerEdit.CloseButtons(btnStaffed, isEditArchiveFolderChangeForm);
                        CustomerEdit.CloseButtons(btnArchiveFolder, isEditArchiveFolderChangeForm);
                    }
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }        

        private void btnArchiveFolder_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            cls_BaseSpr.ButtonEditButtonClickBase<ArchiveFolder>(Session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.ArchiveFolder, 1, null, null, false, null, string.Empty, false, true);
        }

        private void btnAccountantResponsible_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;
            cls_BaseSpr.ButtonEditButtonClickBase<Staff>(Session, buttonEdit, (int)cls_App.ReferenceBooks.Staff, 1, new NullOperator(nameof(Staff.DateDismissal)), null, false, null, string.Empty, false, true);
        }

        private void btnStaffed_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;
            cls_BaseSpr.ButtonEditButtonClickBase<Staff>(Session, buttonEdit, (int)cls_App.ReferenceBooks.Staff, 1, new NullOperator(nameof(Staff.DateDismissal)), null, false, null, string.Empty, false, true);
        }

        private void cmbMonth_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }
        }

        private void cmbPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBoxEdit = sender as ComboBoxEdit;

            if (comboBoxEdit != null && comboBoxEdit.SelectedIndex != -1)
            {
                lblMonth.Visible = false;
                cmbMonth.Visible = false;
                cmbMonth.SelectedIndex = -1;
                cmbPeriodChange.Visible = false;
                cmbPeriodChange.SelectedIndex = -1;

                var period = (PeriodArchiveFolder)cmbPeriod.SelectedIndex;

                if (period == PeriodArchiveFolder.QUARTER)
                {
                    cmbPeriodChange.Visible = true;
                    cmbPeriodChange.Properties.Items.Clear();
                    foreach (PeriodReportChange item in Enum.GetValues(typeof(PeriodReportChange)))
                    {
                        if (item == PeriodReportChange.FIRSTQUARTER ||
                            item == PeriodReportChange.SECONDQUARTER ||
                            item == PeriodReportChange.THIRDQUARTER ||
                            item == PeriodReportChange.FOURTHQUARTER)
                        {
                            cmbPeriodChange.Properties.Items.Add(item.GetEnumDescription());
                        }
                        cmbPeriodChange.SelectedIndex = 0;
                    }
                }
                else if (period == PeriodArchiveFolder.HALFYEAR)
                {
                    cmbPeriodChange.Visible = true;
                    cmbPeriodChange.Properties.Items.Clear();
                    foreach (PeriodReportChange item in Enum.GetValues(typeof(PeriodReportChange)))
                    {
                        if (item == PeriodReportChange.FIRSTHALFYEAR ||
                            item == PeriodReportChange.SECONDHALFYEAR)
                        {
                            cmbPeriodChange.Properties.Items.Add(item.GetEnumDescription());
                        }
                        cmbPeriodChange.SelectedIndex = 0;
                    }
                }
                else if (period == PeriodArchiveFolder.MONTH) 
                {
                    lblMonth.Visible = true;
                    cmbMonth.Visible = true;
                    cmbMonth.SelectedIndex = 0;
                }
            }
        }

        private void gridView_MouseDown(object sender, MouseEventArgs e)
        {
            DXMouseEventArgs dxMouseEventArgs = e as DXMouseEventArgs;
            GridView gridview = sender as GridView;
            _ = gridview.CalcHitInfo(dxMouseEventArgs.Location);
            //if ((gridHitInfo.InRow || gridHitInfo.InRowCell) && dxMouseEventArgs.Button == MouseButtons.Right)
            if (dxMouseEventArgs.Button == MouseButtons.Right)
            {
                popupMenu.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
            }
        }

        private void barBtnAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            ArchiveFolderChange.ArchiveFolderChangePeriods.Add(new ArchiveFolderChangePeriod(Session));
        }

        private void barBtnDel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridView.IsEmpty)
            {
                return;
            }

            var archiveFolderChangePeriod = gridView.GetRow(gridView.FocusedRowHandle) as ArchiveFolderChangePeriod;
            if (archiveFolderChangePeriod != null)
            {
                ArchiveFolderChange.ArchiveFolderChangePeriods.Remove(archiveFolderChangePeriod);
            }
        }

        private void ArchiveFolderChangeEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            ArchiveFolderChange.Reload();
            ArchiveFolderChange.ArchiveFolderChangePeriods.Reload();
            foreach (var period in ArchiveFolderChange.ArchiveFolderChangePeriods)
            {
                period.Reload();
            }
            XPObject.AutoSaveOnEndEdit = true;
        }
    }
}