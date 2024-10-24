using DevExpress.Data.Filtering;
using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using PulsLibrary.Extensions.DevForm;
using RMS.Core.Controllers.Customers;
using RMS.Core.Model.Exchange;
using RMS.Core.Model.InfoCustomer;
using RMS.UI.xUI.PackagesDocument.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.xUI.PackagesDocument.Controllers
{
    public partial class CustomerImportEEUControl : XtraUserControl
    {
        private bool _isVisibleCustomerColumn = true;
        private Customer _customer;

        private UnitOfWork _uof;
        private ImportEEU _importEEU;
        private List<ImportEEU> _listObj;

        public delegate void FocusedRowChangedEventHandler(ImportEEU obj, int focusedRowHandle);
        public event FocusedRowChangedEventHandler FocusedRowChangedEvent;

        public delegate void UpdateEventHandler(object sender);
        public event UpdateEventHandler UpdateObj;


        public void SetCustomer(Customer customer)
        {
            _customer = customer;
            _isVisibleCustomerColumn = false;
            gridView.ColumnSetup($"{nameof(CustomerStaff.CustomerName)}", isVisible: _isVisibleCustomerColumn, caption: "Клиент", width: 350, isFixedWidth: true);
        }

        public object GetGridViewObj
        {
            get
            {
                if (gridView.ActiveFilterCriteria is null)
                {
                    return gridView.DataSource;
                }
                else
                {
                    var list = new List<CustomerStaff>();
                    for (int i = 0; i < gridView.DataRowCount; i++)
                    {
                        list.Add(gridView.GetRow(i) as CustomerStaff);
                    }
                    return list;
                }
            }
        }

        public CustomerImportEEUControl()
        {
            InitializeComponent();
            _listObj = new List<ImportEEU>();
        }

        public CustomerImportEEUControl(List<ImportEEU> listObj) : this()
        {
            _listObj = listObj;
        }

        public void SetUnitOfWork(UnitOfWork uof)
        {
            _uof = uof;
        }

        private CriteriaOperator _activeFilterCriteria;
        public void SetActiveFilterCriteria(CriteriaOperator criteriaOperator)
        {
            if (criteriaOperator is null)
            {
                _activeFilterCriteria = null;
                gridView.ActiveFilterCriteria = _activeFilterCriteria;
            }
            else
            {
                _activeFilterCriteria = criteriaOperator;
                gridView.ActiveFilterCriteria = _activeFilterCriteria;
            }

            gridView_FocusedRowChanged(gridView, null);
        }

        public void SetInfoActiveFilterCriteria(CriteriaOperator criteriaOperator)
        {
            if (criteriaOperator is null)
            {
                gridView.ActiveFilterCriteria = _activeFilterCriteria;
            }
            else
            {
                if (_activeFilterCriteria is null)
                {
                    gridView.ActiveFilterCriteria = criteriaOperator;
                }
                else
                {
                    var criteria = new GroupOperator(GroupOperatorType.And);
                    criteria.Operands.Add(_activeFilterCriteria);
                    criteria.Operands.Add(criteriaOperator);
                    gridView.ActiveFilterCriteria = criteria;
                }
            }

            gridView_FocusedRowChanged(gridView, null);
        }

        public void UpdateData(object listObj)
        {            
            if (listObj is List<ImportEEU> list)
            {
                var currentObjOid = -1;
                if (gridView.GetRow(gridView.FocusedRowHandle) is ImportEEU obj)
                {
                    currentObjOid = obj.Oid;
                }

                _listObj = list;
                gridControl.DataSource = _listObj;
                
                if (currentObjOid > 0)
                {
                    gridView.FocusedRowHandle = gridView.LocateByValue(nameof(XPObject.Oid), currentObjOid);
                }
                else
                {
                    gridView.MoveLast();
                }
            }
            else
            {
                gridControl.DataSource = new List<ImportEEU>();
            }
        }

        private void Control_Load(object sender, EventArgs e)
        {
            var repositoryItemMemoEdit = gridControl.RepositoryItems.Add(nameof(MemoEdit)) as RepositoryItemMemoEdit;
            repositoryItemMemoEdit.WordWrap = true;
            
            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref gridView);

            UpdateData(_listObj);
                        
            gridView.ColumnSetup($"{nameof(ImportEEU.Oid)}", isVisible: false, caption: "[OID]", width: 100, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
            gridView.ColumnSetup($"{nameof(ImportEEU.ImportDate)}", caption: "Дата\nимпорта", width: 150, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
            gridView.ColumnSetup($"{nameof(ImportEEU.CountryName)}", isVisible: true, caption: "Страна", width: 350, isFixedWidth: true);
            gridView.ColumnSetup($"{nameof(ImportEEU.Payment)}", caption: "Платеж НДС", width: 150, isFixedWidth: true, horzAlignment: HorzAlignment.Far, formatString:"n2");

            gridView.ColumnDelete(nameof(ImportEEU.Customer));
            gridView.ColumnDelete(nameof(ImportEEU.Country));

            gridControl.GridControlSetup();
            gridView.GridViewSetup(isColumnAutoWidth: false, isShowFooter: false, isShowFilterPanelMode: false);
        }

        /// <summary>
        /// Открытие формы редактирования.
        /// </summary>
        /// <param name="obj">Операция для изменения.</param>
        private void OpenEditForm(object obj)
        {
            var form = new CustomerImportEEUEdit(obj, _uof);
            form.SetCustomer(_customer);
            form.SetDirectoryFlag();
            form.FormClosed += OperationEditFormClosed;
            form.XtraFormShow();
        }

        private void OperationEditFormClosed(object sender, FormClosedEventArgs e)
        {
            if (sender is CustomerImportEEUEdit objEdit)
            {
                if (objEdit.IsSave)
                {
                    var currentObj = objEdit.ImportEEU;
                    if (currentObj != null)
                    {
                        var obj = _listObj.FirstOrDefault(f => f.Oid == currentObj.Oid);
                        obj?.Reload();

                        if (obj is null)
                        {
                            _listObj.Add(currentObj);
                            _importEEU = currentObj;
                        }
                    }

                    gridView.RefreshData();
                    gridView.FocusedRowHandle = gridView.LocateByValue(nameof(XPObject.Oid), _importEEU?.Oid);
                    UpdateObj?.Invoke(this);
                    FocusedRowChangedEvent?.Invoke(_importEEU, gridView.FocusedRowHandle);
                }
            }
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
                        if (gridView.GetRow(gridView.FocusedRowHandle) is ImportEEU obj)
                        {
                            OpenEditForm(obj);
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

        private void barBtnAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenEditForm(null);
        }

        private void barBtnEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridView.GetRow(gridView.FocusedRowHandle) is ImportEEU obj)
            {
                OpenEditForm(obj);
            }
        }

        private async void barBtnDel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridView.GetRow(gridView.FocusedRowHandle) is ImportEEU obj)
            {
                var text = $"Вы действительно хотите удалить объект: {obj}?";
                var caption = $"Удаление объекта [OID:{obj.Oid}]";

                if (XtraMessageBox.Show(text,
                                        caption,
                                        MessageBoxButtons.OKCancel,
                                        MessageBoxIcon.Question) == DialogResult.OK)
                {

                    using var uof = new UnitOfWork();
                    var currentObj = await uof.GetObjectByKeyAsync<ImportEEU>(obj.Oid);
                    if (currentObj != null)
                    {
                        //var packagesDocumentType = await PackagesDocumentController.GetPackagesDocumentTypeAsync(_uof, currentObj);
                        //if (packagesDocumentType?.Count() > 0)
                        //{
                        //    DevXtraMessageBox.ShowXtraMessageBox("Удаление невозможно.");
                        //    return;
                        //}

                        //currentObj.Delete();
                        //await uof.CommitTransactionAsync().ConfigureAwait(false);

                        _listObj.Remove(obj);

                        gridView.FocusedRowHandle--;
                        gridView.RefreshData();
                    }
                }
            }
        }

        private async void barBtnUpdate_ItemClick(object sender, ItemClickEventArgs e)
        {
            UpdateData(await CustomerImportEEUController.GetCustomersImportEEUAsync(_uof, _customer));
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
            if (gridView.GetRow(gridView.FocusedRowHandle) is ImportEEU obj)
            {
                _importEEU = obj;
            }
            else
            {
                _importEEU = null;
            }
            
            FocusedRowChangedEvent?.Invoke(_importEEU, gridView.FocusedRowHandle);
        }

        private void gridView_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (gridView.GetRow(e.RowHandle) is CustomerStaff obj)
                {
                    //var color = obj.GetColor();
                    //if (!string.IsNullOrWhiteSpace(color))
                    //{
                    //    e.Appearance.BackColor = ColorTranslator.FromHtml(color);
                    //}
                }
            }
        }
    }
}
