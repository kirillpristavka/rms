using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Taxes;
using RMS.UI.Forms.Directories;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace RMS.UI.Forms.ReferenceBooks
{
    public partial class ElectronicReportingCustomerEdit2 : XtraForm
    {
        private Session Session { get; }
        private Customer Customer { get; }
        private ElectronicReportingCustomer ElectronicReportingCustomer { get; }

        private ElectronicReportingCustomerEdit2()
        {
            InitializeComponent();
            XPObject.AutoSaveOnEndEdit = false;
        }

        public ElectronicReportingCustomerEdit2(ElectronicReportingCustomer electronicReportingCustomer) : this()
        {
            Customer = electronicReportingCustomer.Customer;
            Session = electronicReportingCustomer.Session;
            ElectronicReportingCustomer = electronicReportingCustomer;
        }

        public ElectronicReportingCustomerEdit2(Customer customer) : this()
        {
            Customer = customer;
            Session = customer.Session;
            ElectronicReportingCustomer = customer.ElectronicReportingCustomer ?? new ElectronicReportingCustomer(Session) { Customer = customer };
        }

        private void btnSave_Click(object sender, EventArgs e)
        {            
            ElectronicReportingCustomer.Comment = memoComment.Text;
            ElectronicReportingCustomer.Save();
            Customer.ElectronicReportingCustomer = ElectronicReportingCustomer;
            
            if (Customer.Oid != -1)
            {
                try
                {
                    Customer?.Save();
                }
                catch (Exception ex)
                {
                    RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                }
            }
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TaxSystemCustomerEdit_Load(object sender, EventArgs e)
        {
            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref gridViewObj);
            BVVGlobal.oFuncXpo.PressEnterGrid<ElectronicReportingСustomerNotification, ElectronicReportingСustomerNotificationEdit>(gridViewObj);

            layoutControlGroup.Text = $"Система электронной отчетности на {DateTime.Now.ToShortDateString()}";

            btnCustomer.EditValue = Customer;
            btnElectronicReporting.EditValue = ElectronicReportingCustomer.CurrentElectronicReporting;
            memoComment.EditValue = ElectronicReportingCustomer.Comment;
            
            gridControl.DataSource = ElectronicReportingCustomer.ElectronicReportingСustomerObjects;
            gridView.CellValueChanged += GridView_CellValueChanged;

            if (gridView.Columns[nameof(ElectronicReportingСustomerObject.Oid)] != null)
            {
                gridView.Columns[nameof(ElectronicReportingСustomerObject.Oid)].Visible = false;
                gridView.Columns[nameof(ElectronicReportingСustomerObject.Oid)].Width = 18;
                gridView.Columns[nameof(ElectronicReportingСustomerObject.Oid)].OptionsColumn.FixedWidth = true;
            }

            if (gridView.Columns[nameof(ElectronicReportingСustomerObject.ElectronicReportingString)] != null)
            {
                var buttonEdit = gridControl.RepositoryItems.Add(nameof(ButtonEdit)) as RepositoryItemButtonEdit;
                buttonEdit.TextEditStyle = TextEditStyles.DisableTextEditor;
                buttonEdit.ButtonPressed += ButtonEdit_ButtonPressed;
                buttonEdit.DoubleClick += ButtonEdit_DoubleClick;

                gridView.Columns[nameof(ElectronicReportingСustomerObject.ElectronicReportingString)].ColumnEdit = buttonEdit;
            }

            if (gridView.Columns[nameof(ElectronicReportingСustomerObject.Comment)] is GridColumn columnComment)
            {
                columnComment.Visible = false;
            }

            if (gridView.Columns[nameof(ElectronicReportingСustomerObject.DateSince)] is GridColumn columnDateSince)
            {
                columnDateSince.Width = 125;
                columnDateSince.OptionsColumn.FixedWidth = true;
                columnDateSince.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            }

            if (gridView.Columns[nameof(ElectronicReportingСustomerObject.DateTo)] is GridColumn columnDateTo)
            {
                columnDateTo.Width = 125;
                columnDateTo.OptionsColumn.FixedWidth = true;
                columnDateTo.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            }

            if (gridView.Columns[nameof(ElectronicReportingСustomerObject.LicenseDateSince)] is GridColumn columnLicenseDateSince)
            {
                columnLicenseDateSince.Width = 125;
                columnLicenseDateSince.OptionsColumn.FixedWidth = true;
                columnLicenseDateSince.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            }

            if (gridView.Columns[nameof(ElectronicReportingСustomerObject.LicenseDateTo)] is GridColumn columnLicenseDateTo)
            {
                columnLicenseDateTo.Width = 125;
                columnLicenseDateTo.OptionsColumn.FixedWidth = true;
                columnLicenseDateTo.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            }

            LoadGridControlNotification(ElectronicReportingCustomer);
        }


        private bool isFirstLoadGridViewNotification = false;
        private void LoadGridControlNotification(ElectronicReportingCustomer obj)
        {
            gridControlObj.DataSource = obj.ElectronicReportingСustomerNotifications;
            if (isFirstLoadGridViewNotification is false)
            {
                gridViewObj.OptionsBehavior.Editable = false;

                if (gridViewObj.Columns[nameof(ElectronicReportingСustomerNotification.Oid)] is GridColumn columnOid)
                {
                    columnOid.Width = 50;
                    columnOid.OptionsColumn.FixedWidth = true;
                    columnOid.Visible = false;
                }

                if (gridViewObj.Columns[nameof(ElectronicReportingСustomerNotification.Date)] is GridColumn columnDate)
                {
                    columnDate.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                    columnDate.Width = 125;
                    columnDate.OptionsColumn.FixedWidth = true;
                    columnDate.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                }

                if (gridViewObj.Columns[nameof(ElectronicReportingСustomerNotification.StaffString)] is GridColumn columnStaffString)
                {
                    columnStaffString.Width = 200;
                    columnStaffString.OptionsColumn.FixedWidth = true;
                }

                if (gridViewObj.Columns[nameof(ElectronicReportingСustomerNotification.RecipientString)] is GridColumn columnRecipient)
                {
                    var repositoryItemMemoEdit = gridControl.RepositoryItems.Add(nameof(MemoEdit)) as RepositoryItemMemoEdit;
                    repositoryItemMemoEdit.WordWrap = true;

                    columnRecipient.ColumnEdit = repositoryItemMemoEdit;

                    columnRecipient.Width = 350;
                    columnRecipient.OptionsColumn.FixedWidth = true;
                }

                isFirstLoadGridViewNotification = true;
            }
        }

        private void GridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            btnElectronicReporting.EditValue = ElectronicReportingCustomer.CurrentElectronicReporting;
        }

        private void ButtonEdit_DoubleClick(object sender, EventArgs e)
        {
            if (gridView.IsEmpty)
            {
                return;
            }

            var electronicReportingСustomerObject = gridView.GetRow(gridView.FocusedRowHandle) as ElectronicReportingСustomerObject;
            if (electronicReportingСustomerObject != null)
            {
                var oid = electronicReportingСustomerObject.ElectronicReporting?.Oid ?? -1;
                var id = cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.ElectronicReporting, oid);
                if (id > 0)
                {
                    var electronicReporting = Session.GetObjectByKey<ElectronicReporting>(id);
                    electronicReportingСustomerObject.ElectronicReporting = electronicReporting;

                    var buttonEdit = sender as ButtonEdit;
                    if (buttonEdit != null)
                    {
                        buttonEdit.EditValue = electronicReportingСustomerObject.ElectronicReportingString;
                    }
                }
            }
        }

        private void ButtonEdit_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (gridView.IsEmpty)
            {
                return;
            }

            var electronicReportingСustomerObject = gridView.GetRow(gridView.FocusedRowHandle) as ElectronicReportingСustomerObject;
            if (electronicReportingСustomerObject != null)
            {
                var oid = electronicReportingСustomerObject.ElectronicReporting?.Oid ?? -1;
                var id = cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.ElectronicReporting, oid);
                if (id > 0)
                {
                    var electronicReporting = Session.GetObjectByKey<ElectronicReporting>(id);
                    electronicReportingСustomerObject.ElectronicReporting = electronicReporting;

                    var buttonEdit = sender as ButtonEdit;
                    if (buttonEdit != null)
                    {
                        buttonEdit.EditValue = electronicReportingСustomerObject.ElectronicReportingString;
                    }
                }
            }
        }

        private void btnCustomer_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            cls_BaseSpr.ButtonEditButtonClickBase<Customer>(Session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.Customer, 1, null, null, false, null, string.Empty, false, true);
        }
        
        private void btnTaxSystem_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            cls_BaseSpr.ButtonEditButtonClickBase<ElectronicReporting>(Session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.ElectronicReporting, 1, null, null, false, null, string.Empty, false, true);
        }        

        private void gridControl_MouseDown(object sender, MouseEventArgs e)
        {
            DXMouseEventArgs dxMouseEventArgs = e as DXMouseEventArgs;
            if (dxMouseEventArgs.Button == MouseButtons.Right)
            {
                popupMenu.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
            }
        }

        private void barBtnAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            ElectronicReportingCustomer.ElectronicReportingСustomerObjects.Add(
                new ElectronicReportingСustomerObject(Session) 
                { 
                    UserCreate = Session.GetObjectByKey<User>(DatabaseConnection.User?.Oid)
                }
            );
        }

        private void TaxSystemCustomerEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            XPObject.AutoSaveOnEndEdit = true;
            ElectronicReportingCustomer?.Reload();
            ElectronicReportingCustomer?.ElectronicReportingСustomerObjects?.Reload();
        }

        private void barBtnDel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridView.IsEmpty)
            {
                return;
            }

            var obj = gridView.GetRow(gridView.FocusedRowHandle) as ElectronicReportingСustomerObject;
            if (obj != null)
            {
                if (XtraMessageBox.Show($"Вы точно хотите удалить объект: {obj}",
                                        "Удаление объекта",
                                        MessageBoxButtons.OKCancel,
                                        MessageBoxIcon.Question) == DialogResult.OK)
                {
                    obj.Delete();
                }                
            }
        }

        private void gridViewObj_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (e.MenuType == GridMenuType.User || e.MenuType == GridMenuType.Row)
                {
                    if (e.MenuType != GridMenuType.Row)
                    {
                        barBtnNotificationEdit.Enabled = false;
                    }
                    else
                    {
                        barBtnNotificationEdit.Enabled = true;
                    }

                    popupMenuNotification.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }
        
        private void barBtnNotificationAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (ElectronicReportingCustomer != null
                && ElectronicReportingCustomer.Oid > 0 
                && ElectronicReportingCustomer.Customer != null)
            {
                var form = new ElectronicReportingСustomerNotificationEdit(ElectronicReportingCustomer);
                form.ShowDialog();

                ElectronicReportingCustomer?.ElectronicReportingСustomerNotifications?.Reload();
            }
        }

        private void barBtnNotificationEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewObj.GetRow(gridViewObj.FocusedRowHandle) is ElectronicReportingСustomerNotification obj)
            {
                var form = new ElectronicReportingСustomerNotificationEdit(obj);
                form.ShowDialog();
            }
        }
    }
}