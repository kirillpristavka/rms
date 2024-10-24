using DevExpress.Data.Filtering;
using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using PulsLibrary.Extensions.DevForm;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.PackagesDocument;
using RMS.UI.xUI.PackagesDocument.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.xUI.PackagesDocument.Controllers
{
    public partial class PackageDocumentCustomerStaffObjControl : XtraUserControl
    {
        private UnitOfWork _uof;
        private Customer _customer;
        private PackageDocument _packageDocument;

        private PackageDocumentCustomerStaffObj _packageDocumentCustomerStaffObj;
        private List<PackageDocumentCustomerStaffObj> _listObj;

        public delegate void FocusedRowChangedEventHandler(PackageDocumentCustomerStaffObj obj, int focusedRowHandle);
        public event FocusedRowChangedEventHandler FocusedRowChangedEvent;

        public List<CustomerStaff> CustomerStaffs => _listObj?.Select(s => s.CustomerStaff)?.ToList();

        public PackageDocumentCustomerStaffObjControl()
        {
            InitializeComponent();
            _listObj = new List<PackageDocumentCustomerStaffObj>();
        }
        
        public PackageDocumentCustomerStaffObjControl(List<PackageDocumentCustomerStaffObj> listObj) : this()
        {
            _listObj = listObj;
        }

        public void SetUnitOfWork(UnitOfWork uof)
        {
            _uof = uof;
        }

        public void SetCustomer(Customer customer)
        {
            _customer = customer;
        }

        public void ClearListObj()
        {
            _listObj.Clear();
            gridView.RefreshData();
        }

        public void SetPackageDocument(PackageDocument packageDocument)
        {
            _packageDocument = packageDocument;
        }

        public void UpdateData(object listObj)
        {            
            if (listObj is List<PackageDocumentCustomerStaffObj> list)
            {
                var currentObjOid = -1;
                if (gridView.GetRow(gridView.FocusedRowHandle) is PackageDocumentCustomerStaffObj obj)
                {
                    currentObjOid = obj.Oid;
                }

                _listObj = list;
                gridControl.DataSource = _listObj;

                if (currentObjOid > 0)
                {
                    gridView.FocusedRowHandle = gridView.LocateByValue(nameof(PackageDocumentCustomerStaffObj.Oid), currentObjOid);
                }
            }
            else
            {
                gridControl.DataSource = new List<PackageDocumentCustomerStaffObj>();
            }
        }

        private void Control_Load(object sender, EventArgs e)
        {
            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref gridView);

            UpdateData(_listObj);
            
            gridView.ColumnSetup($"{nameof(PackageDocumentCustomerStaffObj.Oid)}", isVisible: false, caption: "[OID]", width: 100, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
            gridView.ColumnSetup($"{nameof(PackageDocumentCustomerStaffObj.Staff)}", caption: "Сотрудник", width: 350, isFixedWidth: true);
            gridView.ColumnSetup($"{nameof(PackageDocumentCustomerStaffObj.StaffDateBirth)}", caption: "Дата\nрождения", width: 150, isFixedWidth: true, horzAlignment: HorzAlignment.Center);

            gridView.ColumnDelete(nameof(PackageDocumentCustomerStaffObj.PackageDocument));
            gridView.ColumnDelete(nameof(PackageDocumentCustomerStaffObj.CustomerStaff));

            gridControl.GridControlSetup();
            gridView.GridViewSetup(isColumnAutoWidth: false, isShowFooter: false);
        }

        /// <summary>
        /// Открытие формы редактирования.
        /// </summary>
        /// <param name="obj">Операция для изменения.</param>
        private void OpenEditForm(object obj)
        {
            var form = new CustomerStaffEdit(obj, _uof);
            form.SetCustomer(_customer);
            form.FormClosed += OperationEditFormClosed;
            form.XtraFormShow();
        }

        private void OperationEditFormClosed(object sender, FormClosedEventArgs e)
        {
            if (sender is CustomerStaffEdit objEdit)
            {
                if (objEdit.IsSave)
                {
                    var currentObj = objEdit.CustomerStaff;
                    if (currentObj != null)
                    {
                        AddObjToList(currentObj);
                    }
                }
            }
        }

        private void AddObjToList(CustomerStaff currentObj)
        {
            var obj = _listObj.FirstOrDefault(f => f.CustomerStaff != null && f.CustomerStaff.Equals(currentObj));
            if (obj is null)
            {
                obj = new PackageDocumentCustomerStaffObj(_uof)
                {
                    CustomerStaff = currentObj,
                    PackageDocument = _packageDocument
                };

                _listObj.Add(obj);
                _packageDocumentCustomerStaffObj = obj;
            }

            gridView.RefreshData();
            gridView.FocusedRowHandle = gridView.LocateByValue(nameof(XPObject.Oid), _packageDocumentCustomerStaffObj?.Oid);
        }

        private void gridView_DoubleClick(object sender, EventArgs e)
        {
            if (e is DXMouseEventArgs ea)
            {
                if (sender is GridView gridView)
                {
                    var info = gridView.CalcHitInfo(ea.Location);
                    if (info.InRow)
                    {
                        if (gridView.GetRow(gridView.FocusedRowHandle) is PackageDocumentCustomerStaffObj obj
                            && obj.CustomerStaff != null)
                        {
                            obj?.Reload();
                            OpenEditForm(obj.CustomerStaff);
                        }
                    }
                }
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

                    barCheckFindPanel.Checked = gridView.IsFindPanelVisible;
                    barCheckAutoFilterRow.Checked = gridView.OptionsView.ShowAutoFilterRow;

                    popupMenu.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }

        private async void barBtnAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (_customer != null)
            {
                var oidCustomerStaff = cls_BaseSpr.RunBaseSpr(
                    (int)cls_App.ReferenceBooks.CustomerStaff,
                    criteria: new BinaryOperator($"{nameof(CustomerStaff.Customer)}.{nameof(Customer.Oid)}", _customer?.Oid));

                if (oidCustomerStaff > 0)
                {
                    var customerStaff = await new XPQuery<CustomerStaff>(_uof)?.FirstOrDefaultAsync(f => f.Oid == oidCustomerStaff);
                    AddObjToList(customerStaff);
                }
            }
        }

        private void barBtnEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridView.GetRow(gridView.FocusedRowHandle) is PackageDocumentCustomerStaffObj obj
                && obj.CustomerStaff != null)
            {
                OpenEditForm(obj.CustomerStaff);
            }
        }

        private void barBtnDel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridView.GetRow(gridView.FocusedRowHandle) is PackageDocumentCustomerStaffObj obj)
            {
                var text = $"Вы действительно хотите удалить сотрудника: {obj}?";
                var caption = $"Удаление сотрудника [OID:{obj.Oid}]";

                if (XtraMessageBox.Show(text,
                                        caption,
                                        MessageBoxButtons.OKCancel,
                                        MessageBoxIcon.Question) == DialogResult.OK)
                {

                    _listObj.Remove(obj);

                    gridView.FocusedRowHandle--;
                    gridView.RefreshData();
                }
            }
        }

        private async void barBtnUpdate_ItemClick(object sender, ItemClickEventArgs e)
        {
            //UpdateData(await PackagesDocumentController.GetPackagesDocumentAsync(_uof));
        }

        private void barCheckFindPanel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridView.IsFindPanelVisible)
            {
                gridView.HideFindPanel();
            }
            else
            {
                gridView.ShowFindPanel();
            }
        }

        private void barCheckAutoFilterRow_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridView.OptionsView.ShowAutoFilterRow)
            {
                gridView.OptionsView.ShowAutoFilterRow = false;
            }
            else
            {
                gridView.OptionsView.ShowAutoFilterRow = true;
            }
        }

        private void gridView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (gridView.GetRow(gridView.FocusedRowHandle) is PackageDocumentCustomerStaffObj obj)
            {
                _packageDocumentCustomerStaffObj = obj;
                FocusedRowChangedEvent?.Invoke(obj, gridView.FocusedRowHandle);
            }
        }
    }
}
