using DevExpress.Data.Filtering;
using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using PulsLibrary.Extensions.DevForm;
using RMS.Core.Controllers.PackagesDocument;
using RMS.Core.Model.PackagesDocument;
using RMS.UI.xUI.PackagesDocument.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.xUI.PackagesDocument.Controllers
{
    public partial class PackageDocumentControl : XtraUserControl
    {
        private UnitOfWork _uof;
        private PackageDocument _packageDocument;
        private List<PackageDocument> _listObj;

        public delegate void FocusedRowChangedEventHandler(PackageDocument obj, int focusedRowHandle);
        public event FocusedRowChangedEventHandler FocusedRowChangedEvent;

        public delegate void UpdateEventHandler(object sender);
        public event UpdateEventHandler UpdateObj;

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
                    var list = new List<PackageDocument>();                    
                    for (int i = 0; i < gridView.DataRowCount; i++)
                    {
                        list.Add(gridView.GetRow(i) as PackageDocument);
                    }
                    return list;
                }
            }
        }

        public PackageDocumentControl()
        {
            InitializeComponent();
            _listObj = new List<PackageDocument>();
        }

        public PackageDocumentControl(List<PackageDocument> listObj) : this()
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
            if (listObj is List<PackageDocument> list)
            {
                var currentObjOid = -1;
                if (gridView.GetRow(gridView.FocusedRowHandle) is PackageDocument obj)
                {
                    currentObjOid = obj.Oid;
                }

                _listObj = list;
                gridControl.DataSource = _listObj;
                
                if (currentObjOid > 0)
                {
                    gridView.FocusedRowHandle = gridView.LocateByValue(nameof(PackageDocument.Oid), currentObjOid);
                }
                else
                {
                    gridView.MoveLast();
                }
            }
            else
            {
                gridControl.DataSource = new List<PackageDocument>();
            }
        }

        private void Control_Load(object sender, EventArgs e)
        {
            var repositoryItemMemoEdit = gridControl.RepositoryItems.Add(nameof(MemoEdit)) as RepositoryItemMemoEdit;
            repositoryItemMemoEdit.WordWrap = true;
            
            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref gridView);

            UpdateData(_listObj);
                        
            gridView.ColumnSetup($"{nameof(PackageDocument.Oid)}", isVisible: false, caption: "[OID]", width: 100, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
            gridView.ColumnSetup($"{nameof(PackageDocument.IsCreateByCustomer)}", isVisible: false, caption: "Создано\nклиентом", width: 125, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
            gridView.ColumnSetup($"{nameof(PackageDocument.IsShowCustomer)}", isVisible: false, caption: "Показывать\nклиенту", width: 125, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
            gridView.ColumnSetup($"{nameof(PackageDocument.CustomerName)}", caption: "Клиент", width: 300, isFixedWidth: true);
            gridView.ColumnSetup($"{nameof(PackageDocument.StaffName)}", caption: "Ответственный", width: 250, isFixedWidth: true);
            
            gridView.ColumnSetup($"{nameof(PackageDocument.DocumentsName)}", caption: "Документы", width: 275, isFixedWidth: true);
            if (gridView.Columns[$"{nameof(PackageDocument.DocumentsName)}"] is GridColumn columnDocumentsName)
            {
                columnDocumentsName.ColumnEdit = repositoryItemMemoEdit;
                columnDocumentsName.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
            }
            
            gridView.ColumnSetup($"{nameof(PackageDocument.Date)}", caption: "Дата", width: 150, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
            gridView.ColumnSetup($"{nameof(PackageDocument.Name)}", isVisible: false, caption: "Наименование", width: 250, isFixedWidth: true);
            
            gridView.ColumnSetup($"{nameof(PackageDocument.Description)}", caption: "Примечание", width: 350, isFixedWidth: true);
            if (gridView.Columns[$"{nameof(PackageDocument.Description)}"] is GridColumn columnDescription)
            {
                columnDescription.ColumnEdit = repositoryItemMemoEdit;
                columnDescription.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
            }
            
            gridView.ColumnSetup($"{nameof(PackageDocument.CustomerStaff)}", caption: "Сотрудники", width: 250, isFixedWidth: true);
            if (gridView.Columns[$"{nameof(PackageDocument.CustomerStaff)}"] is GridColumn columnCustomerStaff)
            {
                columnCustomerStaff.ColumnEdit = repositoryItemMemoEdit;
                columnCustomerStaff.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
            }
            
            gridView.ColumnSetup($"{nameof(PackageDocument.Status)}", caption: "Статус", width: 400, isFixedWidth: true);
            
            gridView.ColumnSetup($"{nameof(PackageDocument.Period)}", caption: "Период", width: 350, isFixedWidth: true); 
            if (gridView.Columns[$"{nameof(PackageDocument.Period)}"] is GridColumn columnCustomerPeriod)
            {
                columnCustomerPeriod.ColumnEdit = repositoryItemMemoEdit;
                columnCustomerPeriod.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
            }

            gridView.ColumnDelete(nameof(PackageDocument.Customer));
            gridView.ColumnDelete(nameof(PackageDocument.Staff));
            gridView.ColumnDelete(nameof(PackageDocument.PackageDocumentStatus));

            gridControl.GridControlSetup();
            gridView.GridViewSetup(isColumnAutoWidth: false, isShowFooter: false, isShowFilterPanelMode: false);
        }

        /// <summary>
        /// Открытие формы редактирования.
        /// </summary>
        /// <param name="obj">Операция для изменения.</param>
        private void OpenEditForm(object obj)
        {
            var form = new PackageDocumentEdit(obj);
            form.FormClosed += OperationEditFormClosed;
            form.XtraFormShow();
        }

        private void OperationEditFormClosed(object sender, FormClosedEventArgs e)
        {
            if (sender is PackageDocumentEdit objEdit)
            {
                if (objEdit.IsSave)
                {
                    var currentObj = objEdit.PackageDocument;
                    if (currentObj != null)
                    {
                        var obj = _listObj.FirstOrDefault(f => f.Oid == currentObj.Oid);
                        obj?.Reload();
                        
                        if (obj is null)
                        {
                            _listObj.Add(currentObj);
                            _packageDocument = currentObj;
                        }
                    }
                    
                    gridView.RefreshData();
                    gridView.FocusedRowHandle = gridView.LocateByValue(nameof(XPObject.Oid), _packageDocument?.Oid);
                    UpdateObj?.Invoke(this);
                    FocusedRowChangedEvent?.Invoke(_packageDocument, gridView.FocusedRowHandle);
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
                        if (gridView.GetRow(gridView.FocusedRowHandle) is PackageDocument obj)
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
            if (gridView.GetRow(gridView.FocusedRowHandle) is PackageDocument obj)
            {
                OpenEditForm(obj);
            }
        }

        private async void barBtnDel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridView.GetRow(gridView.FocusedRowHandle) is PackageDocument obj)
            {
                var text = $"Вы действительно хотите удалить пакет документов: {obj}?";
                var caption = $"Удаление пакета документов [OID:{obj.Oid}]";

                if (XtraMessageBox.Show(text,
                                        caption,
                                        MessageBoxButtons.OKCancel,
                                        MessageBoxIcon.Question) == DialogResult.OK)
                {

                    using var uof = new UnitOfWork();
                    var currentObj = await uof.GetObjectByKeyAsync<PackageDocument>(obj.Oid);
                    if (currentObj != null)
                    {
                        currentObj.Delete();
                        await uof.CommitTransactionAsync().ConfigureAwait(false);

                        _listObj.Remove(obj);

                        gridView.FocusedRowHandle--;
                        gridView.RefreshData();
                    }
                }
            }
        }

        private async void barBtnUpdate_ItemClick(object sender, ItemClickEventArgs e)
        {
            UpdateData(await PackagesDocumentController.GetPackagesDocumentAsync(_uof));
            UpdateObj?.Invoke(this);
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
            if (gridView.GetRow(gridView.FocusedRowHandle) is PackageDocument obj)
            {
                _packageDocument = obj;
            }
            else
            {
                _packageDocument = null;
            }
            
            FocusedRowChangedEvent?.Invoke(_packageDocument, gridView.FocusedRowHandle);
        }

        private void gridView_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (gridView.GetRow(e.RowHandle) is PackageDocument obj)
                {
                    var color = obj.GetColor();
                    if (!string.IsNullOrWhiteSpace(color))
                    {
                        e.Appearance.BackColor = ColorTranslator.FromHtml(color);
                    }
                }
            }
        }
    }
}
