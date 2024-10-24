using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using RMS.Core.Model;
using RMS.Core.Model.ElectronicDocumentsManagement;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Taxes;
using RMS.UI.Forms.Directories;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace RMS.UI.Forms.ReferenceBooks
{
    public partial class ElectronicDocumentManagementCustomerEdit : XtraForm
    {
        private Session Session { get; }
        private Customer Customer { get; }
        private ElectronicDocumentManagementCustomer ElectronicDocumentManagementCustomer { get; }

        private ElectronicDocumentManagementCustomerEdit()
        {
            InitializeComponent();
            XPObject.AutoSaveOnEndEdit = false;
        }

        public ElectronicDocumentManagementCustomerEdit(ElectronicDocumentManagementCustomer obj) : this()
        {
            Customer = obj.Customer;
            Session = obj.Session;
            ElectronicDocumentManagementCustomer = obj;
        }

        public ElectronicDocumentManagementCustomerEdit(Customer customer) : this()
        {
            Customer = customer;
            Session = customer.Session;
            ElectronicDocumentManagementCustomer = customer.ElectronicDocumentManagementCustomer ?? new ElectronicDocumentManagementCustomer(Session) { Customer = customer };
        }

        private void btnSave_Click(object sender, EventArgs e)
        {            
            ElectronicDocumentManagementCustomer.Comment = memoComment.Text;
            ElectronicDocumentManagementCustomer.Save();
            Customer.ElectronicDocumentManagementCustomer = ElectronicDocumentManagementCustomer;
            
            if (Customer.Oid != -1)
            {
                Customer.Save();
            }
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TaxSystemCustomerEdit_Load(object sender, EventArgs e)
        {
            layoutControlGroup.Text = $"ЭДО на {DateTime.Now.ToShortDateString()}";

            btnCustomer.EditValue = Customer;
            btnElectronicReporting.EditValue = ElectronicDocumentManagementCustomer.CurrentObject;
            memoComment.EditValue = ElectronicDocumentManagementCustomer.Comment;
            
            gridControl.DataSource = ElectronicDocumentManagementCustomer.ElectronicDocumentManagementCustomerObject;
            gridView.CellValueChanged += GridView_CellValueChanged;

            if (gridView.Columns[nameof(ElectronicDocumentManagementCustomerObject.Oid)] != null)
            {
                gridView.Columns[nameof(ElectronicDocumentManagementCustomerObject.Oid)].Visible = false;
                gridView.Columns[nameof(ElectronicDocumentManagementCustomerObject.Oid)].Width = 18;
                gridView.Columns[nameof(ElectronicDocumentManagementCustomerObject.Oid)].OptionsColumn.FixedWidth = true;
            }

            if (gridView.Columns[nameof(ElectronicDocumentManagementCustomerObject.ElectronicDocumentManagementString)] != null)
            {
                var buttonEdit = gridControl.RepositoryItems.Add(nameof(ButtonEdit)) as RepositoryItemButtonEdit;
                buttonEdit.TextEditStyle = TextEditStyles.DisableTextEditor;
                buttonEdit.ButtonPressed += ButtonEdit_ButtonPressed;
                buttonEdit.DoubleClick += ButtonEdit_DoubleClick;

                gridView.Columns[nameof(ElectronicDocumentManagementCustomerObject.ElectronicDocumentManagementString)].ColumnEdit = buttonEdit;
            }

            if (gridView.Columns[nameof(ElectronicDocumentManagementCustomerObject.Comment)] is GridColumn columnComment)
            {
                columnComment.Visible = false;
            }

            if (gridView.Columns[nameof(ElectronicDocumentManagementCustomerObject.DateSince)] is GridColumn columnDateSince)
            {
                columnDateSince.Width = 125;
                columnDateSince.OptionsColumn.FixedWidth = true;
                columnDateSince.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            }

            if (gridView.Columns[nameof(ElectronicDocumentManagementCustomerObject.DateTo)] is GridColumn columnDateTo)
            {
                columnDateTo.Width = 125;
                columnDateTo.OptionsColumn.FixedWidth = true;
                columnDateTo.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            }

            var repositoryItemMemoEdit = gridControl.RepositoryItems.Add(nameof(MemoEdit)) as RepositoryItemMemoEdit;
            repositoryItemMemoEdit.WordWrap = true;

            foreach (GridColumn item in gridView.Columns)
            {
                if (item.FieldName == nameof(ElectronicDocumentManagementCustomerObject.Recipient) 
                    || item.FieldName == nameof(ElectronicDocumentManagementCustomerObject.Sender)
                    || item.FieldName == nameof(ElectronicDocumentManagementCustomerObject.Powers)
                    || item.FieldName == nameof(ElectronicDocumentManagementCustomerObject.Login)
                    || item.FieldName == nameof(ElectronicDocumentManagementCustomerObject.Password)
                    || item.FieldName == nameof(ElectronicDocumentManagementCustomerObject.Nuances)
                    )
                {
                    item.ColumnEdit = repositoryItemMemoEdit;
                }
            }
        }
        

        private void GridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            btnElectronicReporting.EditValue = ElectronicDocumentManagementCustomer.CurrentObjectString;
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

            var obj = gridView.GetRow(gridView.FocusedRowHandle) as ElectronicDocumentManagementCustomerObject;
            if (obj != null)
            {
                var oid = obj.ElectronicDocumentManagement?.Oid ?? -1;
                var id = cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.ElectronicDocumentManagement, oid);
                if (id > 0)
                {
                    var currentObj = Session.GetObjectByKey<ElectronicDocumentManagement>(id);
                    obj.ElectronicDocumentManagement = currentObj;

                    var buttonEdit = sender as ButtonEdit;
                    if (buttonEdit != null)
                    {
                        buttonEdit.EditValue = obj.ElectronicDocumentManagementString;
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
            cls_BaseSpr.ButtonEditButtonClickBase<ElectronicDocumentManagement>(Session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.ElectronicDocumentManagement, 1, null, null, false, null, string.Empty, false, true);
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
            ElectronicDocumentManagementCustomer.ElectronicDocumentManagementCustomerObject.Add(
                new ElectronicDocumentManagementCustomerObject(Session) 
                { 
                    UserCreate = Session.GetObjectByKey<User>(DatabaseConnection.User?.Oid)
                }
            );
        }

        private void TaxSystemCustomerEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            XPObject.AutoSaveOnEndEdit = true;
            ElectronicDocumentManagementCustomer?.Reload();
            ElectronicDocumentManagementCustomer?.ElectronicDocumentManagementCustomerObject?.Reload();
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

        //private void gridViewObj_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        //{
        //    if (sender is GridView gridView)
        //    {
        //        if (e.MenuType == GridMenuType.User || e.MenuType == GridMenuType.Row)
        //        {
        //            if (e.MenuType != GridMenuType.Row)
        //            {
        //                barBtnNotificationEdit.Enabled = false;
        //            }
        //            else
        //            {
        //                barBtnNotificationEdit.Enabled = true;
        //            }

        //            popupMenuNotification.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
        //        }
        //    }
        //}        
    }
}