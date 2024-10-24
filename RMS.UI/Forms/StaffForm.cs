using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using RMS.UI.Forms.Directories;
using RMS.UI.Forms.ReferenceBooks;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Forms
{
    public partial class StaffForm : XtraForm
    {
        private Session _session;
        private XPCollection<Staff> _staffs;

        /// <summary>
        /// Настройка функционирования таблиц.
        /// </summary>
        private void FunctionalGridSetup()
        {
            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref gridView);
            BVVGlobal.oFuncXpo.PressEnterGrid<Staff, StaffEdit>(gridView);

            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref gridViewAdditionalServices);
            BVVGlobal.oFuncXpo.PressEnterGrid<AdditionalServices, AdditionalServicesEdit>(gridViewAdditionalServices);
        }

        public StaffForm(Session session)
        {
            InitializeComponent();
            FunctionalGridSetup();

            _session = session ?? DatabaseConnection.GetWorkSession();
        }

        private void StaffForm_Load(object sender, EventArgs e)
        {
            _staffs = new XPCollection<Staff>(_session, null, new SortProperty(nameof(Staff.DateDismissal), DevExpress.Xpo.DB.SortingDirection.Ascending));
            gridView.OptionsView.ColumnAutoWidth = false;
            gridControl.DataSource = _staffs;

            if (gridView.Columns[nameof(Staff.Oid)] is GridColumn columnOid)
            {
                columnOid.Visible = false;
                columnOid.Width = 18;
                columnOid.OptionsColumn.FixedWidth = true;
            }

            if (gridView.Columns[nameof(Staff.Surname)] is GridColumn columnSurname)
            {
                columnSurname.Width = 150;
                columnSurname.OptionsColumn.FixedWidth = true;
            }
            
            if (gridView.Columns[nameof(Staff.Name)] is GridColumn columnName)
            {
                columnName.Width = 150;
                columnName.OptionsColumn.FixedWidth = true;
            }

            if (gridView.Columns[nameof(Staff.Patronymic)] is GridColumn columnPatronymic)
            {
                columnPatronymic.Width = 150;
                columnPatronymic.OptionsColumn.FixedWidth = true;
            }

            if (gridView.Columns[nameof(Staff.DateBirth)] is GridColumn columnDateBirth)
            {
                columnDateBirth.Width = 100;
                columnDateBirth.OptionsColumn.FixedWidth = true;
                columnDateBirth.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            }

            if (gridView.Columns[nameof(Staff.DateDismissal)] is GridColumn columnDateDismissal)
            {
                columnDateDismissal.Width = 100;
                columnDateDismissal.OptionsColumn.FixedWidth = true;
                columnDateDismissal.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            }

            if (gridView.Columns[nameof(Staff.DateReceipt)] is GridColumn columnDateReceipt)
            {
                columnDateReceipt.Width = 100;
                columnDateReceipt.OptionsColumn.FixedWidth = true;
                columnDateReceipt.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            }

            if (gridView.Columns[nameof(Staff.PhoneNumber)] is GridColumn columnPhoneNumber)
            {
                columnPhoneNumber.Width = 150;
                columnPhoneNumber.OptionsColumn.FixedWidth = true;
            }

            if (gridView.Columns[nameof(Staff.PositionString)] is GridColumn columnPositionString)
            {
                columnPositionString.Width = 250;
                columnPositionString.OptionsColumn.FixedWidth = true;
            }
        }
        
        private static async System.Threading.Tasks.Task<string> CheckingStaff(int id, string message)
        {
            using (var uof = new UnitOfWork())
            {
                var staff = await uof.GetObjectByKeyAsync<Staff>(id);
                if (staff != null)
                {
                    var customerObj = await new XPQuery<Customer>(uof)
                        .Where(w => (w.AccountantResponsible != null && w.AccountantResponsible.Oid == staff.Oid)
                                    || (w.BankResponsible != null && w.BankResponsible.Oid == staff.Oid)
                                    || (w.PrimaryResponsible != null && w.PrimaryResponsible.Oid == staff.Oid)
                                    || (w.SalaryResponsible != null && w.SalaryResponsible.Oid == staff.Oid))
                        .FirstOrDefaultAsync();

                    if (customerObj != null)
                    {
                        message = $"Удаление сотрудника [{staff}] невозможно.{Environment.NewLine}Запись используется у следующего клиента: {customerObj}";
                    }
                }
            }

            return message;
        }

        private void barBtnTaskAdd_ItemClick(object sender, ItemClickEventArgs e)
        {

            BVVGlobal.oFuncXpo.CreateTask<TaskEdit>(_session);
        }

        private void barBtnRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            _staffs?.Reload();
        }

        private void barBtnControlSystemAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridView.IsEmpty)
            {
                return;
            }

            var obj = gridView.GetRow(gridView.FocusedRowHandle) as Staff;
            if (obj != null)
            {
                var form = new ControlSystemEdit(obj);
                form.ShowDialog();
            }
        }
        
        private void gridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
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
                    }

                    popupMenu?.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }
        
        private void barBtnAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new StaffEdit();
            form.ShowDialog();

            _staffs = new XPCollection<Staff>(_session);
            gridControl.DataSource = _staffs;
        }

        private void barBtnEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridView.IsEmpty)
            {
                return;
            }

            var staff = gridView.GetRow(gridView.FocusedRowHandle) as Staff;
            if (staff != null)
            {
                var form = new StaffEdit(staff);
                form.ShowDialog();
            }
        }

        private async void barBtnDel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridView.IsEmpty)
            {
                return;
            }

            if (gridView.GetRow(gridView.FocusedRowHandle) is Staff staff)
            {
                var message = await cls_BaseSpr.CheckingStaff(staff.Oid, default);
                if (string.IsNullOrWhiteSpace(message))
                {
                    staff.Delete();
                }
                else
                {
                    XtraMessageBox.Show(message, "Оповещение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void gridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView.GetRow(gridView.FocusedRowHandle) is Staff staff)
            {
                gridViewAdditionalServices.OptionsView.ColumnAutoWidth = false;

                staff.AdditionalServices?.Reload();
                gridControlAdditionalServices.DataSource = staff.AdditionalServices;

                if (gridViewAdditionalServices.Columns[nameof(AdditionalServices.Oid)] is GridColumn columnOid)
                {
                    columnOid.Visible = false;
                    columnOid.Width = 18;
                    columnOid.OptionsColumn.FixedWidth = true;
                }

                if (gridViewAdditionalServices.Columns[nameof(AdditionalServices.Period)] is GridColumn columnPeriod)
                {
                    columnPeriod.Width = 125;
                    columnPeriod.OptionsColumn.FixedWidth = true;
                    columnPeriod.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                }

                if (gridViewAdditionalServices.Columns[nameof(AdditionalServices.CustomerName)] is GridColumn columnCustomerName)
                {
                    columnCustomerName.Width = 200;
                }

                if (gridViewAdditionalServices.Columns[nameof(AdditionalServices.DescriptionString)] is GridColumn columnDescriptionString)
                {
                    var repositoryItemMemoEdit = gridControlAdditionalServices.RepositoryItems.Add(nameof(MemoEdit)) as RepositoryItemMemoEdit;
                    repositoryItemMemoEdit.WordWrap = true;
                    repositoryItemMemoEdit.AllowHtmlDraw = DefaultBoolean.True;
                    
                    columnDescriptionString.Width = 300;
                    columnDescriptionString.ColumnEdit = repositoryItemMemoEdit;

                    columnDescriptionString.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
                }

                if (gridViewAdditionalServices.Columns[nameof(AdditionalServices.Value)] is GridColumn gridValue)
                {
                    gridValue.DisplayFormat.FormatType = FormatType.Numeric;
                    gridValue.DisplayFormat.FormatString = "n2";
                    gridValue.RealColumnEdit.EditFormat.FormatType = FormatType.Numeric;
                    gridValue.RealColumnEdit.EditFormat.FormatString = "n2";
                    gridValue.Width = 150;
                    gridValue.OptionsColumn.FixedWidth = true;
                }

                if (gridViewAdditionalServices.Columns[nameof(AdditionalServices.IsPaid)] is GridColumn columnIsPaid)
                {
                    columnIsPaid.Width = 100;
                    columnIsPaid.OptionsColumn.FixedWidth = true;
                }

                if (gridViewAdditionalServices.Columns[nameof(AdditionalServices.ValueStaff)] is GridColumn gridValueStaff)
                {
                    gridValueStaff.DisplayFormat.FormatType = FormatType.Numeric;
                    gridValueStaff.DisplayFormat.FormatString = "n2";
                    gridValueStaff.RealColumnEdit.EditFormat.FormatType = FormatType.Numeric;
                    gridValueStaff.RealColumnEdit.EditFormat.FormatString = "n2";
                    gridValueStaff.Width = 150;
                    gridValueStaff.OptionsColumn.FixedWidth = true;
                }

                if (gridViewAdditionalServices.Columns[nameof(AdditionalServices.DatePaid)] is GridColumn columnDatePaid)
                {
                    columnDatePaid.Width = 125;
                    columnDatePaid.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    columnDatePaid.OptionsColumn.FixedWidth = true;
                }

                if (gridViewAdditionalServices.Columns[nameof(AdditionalServices.IsPaidStaff)] is GridColumn columnIsPaidStaff)
                {
                    columnIsPaidStaff.Width = 100;
                    columnIsPaidStaff.OptionsColumn.FixedWidth = true;
                }

                if (gridViewAdditionalServices.Columns[nameof(AdditionalServices.CommentString)] is GridColumn columnCommentString)
                {
                    var repositoryItemMemoEdit = gridControlAdditionalServices.RepositoryItems.Add(nameof(MemoEdit)) as RepositoryItemMemoEdit;
                    repositoryItemMemoEdit.WordWrap = true;
                    repositoryItemMemoEdit.AllowHtmlDraw = DefaultBoolean.True;

                    columnCommentString.Width = 300;
                    columnCommentString.ColumnEdit  = repositoryItemMemoEdit;
                    columnCommentString.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
                }

                if (gridViewAdditionalServices.Columns[nameof(AdditionalServices.PriceList)] is GridColumn columnPriceList)
                {
                    var repositoryItemMemoEdit = gridControlAdditionalServices.RepositoryItems.Add(nameof(MemoEdit)) as RepositoryItemMemoEdit;
                    repositoryItemMemoEdit.WordWrap = true;
                    repositoryItemMemoEdit.AllowHtmlDraw = DefaultBoolean.True;

                    columnPriceList.Width = 300;
                    columnPriceList.ColumnEdit = repositoryItemMemoEdit;
                    //columnPriceList.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
                }
            }
        }

        private void gridViewAdditionalServices_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (e.MenuType == GridMenuType.User || e.MenuType == GridMenuType.Row)
                {
                    if (e.MenuType != GridMenuType.Row)
                    {
                        barBtnAdditionalServicesEdit.Enabled = false;
                        barBtnAdditionalServicesDel.Enabled = false;
                    }
                    else
                    {
                        barBtnAdditionalServicesEdit.Enabled = true;
                        barBtnAdditionalServicesDel.Enabled = true;
                    }

                    popupMenuAdditionalServices?.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }

        private void barBtnAdditionalServicesAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridView.GetRow(gridView.FocusedRowHandle) is Staff obj)
            {
                var form = new AdditionalServicesEdit(obj);
                form.ShowDialog();

                obj.AdditionalServices?.Reload();
            }            
        }

        private void barBtnAdditionalServicesEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewAdditionalServices.GetRow(gridViewAdditionalServices.FocusedRowHandle) is AdditionalServices obj)
            {
                var form = new AdditionalServicesEdit(obj);
                form.ShowDialog();
            }
        }

        private async void barBtnAdditionalServicesDel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewAdditionalServices.GetRow(gridViewAdditionalServices.FocusedRowHandle) is AdditionalServices obj)
            {
                if (XtraMessageBox.Show($"Вы действительно хотите удалить следующий объект: {obj}",
                                        "Удаление объекта",
                                        MessageBoxButtons.OKCancel,
                                        MessageBoxIcon.Question) == DialogResult.OK)
                {

                    if (obj.IsPaidStaff)
                    {
                        XtraMessageBox.Show($"Удаление невозможно если услуга оплачена сотруднику.",
                                           "Удаление объекта",
                                           MessageBoxButtons.OK,
                                           MessageBoxIcon.Exclamation);
                        return;
                    }
                    
                    using (var uof = new UnitOfWork())
                    {
                        var collection = await new XPQuery<Task>(uof)?.Where(w => w.AdditionalServices != null && w.AdditionalServices.Oid == obj.Oid)?.ToListAsync();
                        foreach (var item in collection)
                        {                            
                            try
                            {
                                item.AdditionalServices = null;
                                item.Save();
                            }
                            catch (Exception ex)
                            {
                                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                            }
                        }

                        await uof.CommitTransactionAsync();
                    }

                    obj.Delete();
                    gridControlAdditionalServices?.RefreshDataSource();
                }
            }
        }

        private void barBtnAdditionalServicesUpdate_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridControlAdditionalServices?.RefreshDataSource();
        }

        private void gridViewAdditionalServices_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (gridView.GetRow(e.RowHandle) is AdditionalServices obj)
                {
                    if (obj.IsPaidStaff)
                    {
                        e.Appearance.BackColor = Color.LightGreen;
                    }
                }
            }
        }

        private void gridView_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (gridView.GetRow(e.RowHandle) is Staff obj)
                {
                    if (obj.DateDismissal is DateTime)
                    {
                        e.Appearance.BackColor = Color.LightCoral;
                    }
                }
            }
        }
    }
}