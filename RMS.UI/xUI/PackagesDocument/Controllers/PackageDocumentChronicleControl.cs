using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using PulsLibrary.Extensions.DevForm;
using RMS.Core.Controllers.PackagesDocument;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.PackagesDocument;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RMS.UI.xUI.PackagesDocument.Controllers
{
    public partial class PackageDocumentChronicleControl : XtraUserControl
    {
        private UnitOfWork _uof = new UnitOfWork();
        private PackageDocument _packageDocument;
        private CustomerStaff _customerStaff;
        private List<PackageDocumentChronicle> _listObj;

        public PackageDocumentChronicleControl()
        {
            InitializeComponent();
            _listObj = new List<PackageDocumentChronicle>();
        }

        public PackageDocumentChronicleControl(List<PackageDocumentChronicle> listObj) : this()
        {
            _listObj = listObj;
        }

        public void SetPackageDocument(PackageDocument document)
        {
            _packageDocument = document;
        }

        public void SetCustomerStaff(CustomerStaff customerStaff)
        {
            _customerStaff = customerStaff;
        }

        public async void UpdateData()
        {
            if (_packageDocument != null)
            {
                UpdateData(await PackagesDocumentController.GetPackageDocumentChronicleAsync(_uof, _packageDocument));
            }
            else if (_customerStaff != null)
            {
                UpdateData(await PackagesDocumentController.GetPackageDocumentChronicleAsync(_uof, _customerStaff));
            }
        }

        public void UpdateData(object listObj)
        {            
            if (listObj is List<PackageDocumentChronicle> list)
            {
                var currentObjOid = -1;
                if (gridView.GetRow(gridView.FocusedRowHandle) is PackageDocumentChronicle obj)
                {
                    currentObjOid = obj.Oid;
                }

                _listObj = list;
                gridControl.DataSource = _listObj;
                
                if (currentObjOid > 0)
                {
                    gridView.FocusedRowHandle = gridView.LocateByValue(nameof(PackageDocumentChronicle.Oid), currentObjOid);
                }
                else
                {
                    gridView.MoveFirst();
                }
            }
            else
            {
                gridControl.DataSource = new List<PackageDocumentChronicle>();
            }
        }

        private void Control_Load(object sender, EventArgs e)
        {
            var repositoryItemMemoEdit = gridControl.RepositoryItems.Add(nameof(MemoEdit)) as RepositoryItemMemoEdit;
            repositoryItemMemoEdit.WordWrap = true;
            
            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref gridView);

            UpdateData(_listObj);
                        
            gridView.ColumnSetup($"{nameof(PackageDocumentChronicle.Oid)}", isVisible: false, caption: "[OID]", width: 100, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
            gridView.ColumnSetup($"{nameof(PackageDocumentChronicle.Date)}", caption: "Дата", width: 150, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
            if (gridView.Columns[$"{nameof(PackageDocumentChronicle.Date)}"] is GridColumn columnDate)
            {
                columnDate.DisplayFormat.FormatType = FormatType.DateTime;
                columnDate.DisplayFormat.FormatString = "dd.MM.yyyy\n(HH:mm:ss)";
                columnDate.RealColumnEdit.EditFormat.FormatType = FormatType.DateTime;
                columnDate.RealColumnEdit.EditFormat.FormatString = "dd.MM.yyyy\n(HH:mm:ss)";

                columnDate.ColumnEdit = repositoryItemMemoEdit;
                columnDate.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
            }

            gridView.ColumnSetup($"{nameof(PackageDocumentChronicle.DocumentName)}", caption: "Документ", width: 275, isFixedWidth: true);
            gridView.ColumnSetup($"{nameof(PackageDocumentChronicle.UserName)}", caption: "Пользователь", width: 250, isFixedWidth: true);
            gridView.ColumnSetup($"{nameof(PackageDocumentChronicle.Name)}", caption: "Событие", width: 325, isFixedWidth: true);
            if (gridView.Columns[$"{nameof(PackageDocumentChronicle.Name)}"] is GridColumn columnName)
            {
                columnName.ColumnEdit = repositoryItemMemoEdit;
                columnName.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
            }

            gridView.ColumnSetup($"{nameof(PackageDocumentChronicle.EventString)}", caption: "Описание", width: 750, isFixedWidth: true);
            if (gridView.Columns[$"{nameof(PackageDocumentChronicle.EventString)}"] is GridColumn columnEventString)
            {
                columnEventString.ColumnEdit = repositoryItemMemoEdit;
                columnEventString.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
            }            

            gridView.ColumnDelete(nameof(PackageDocumentChronicle.Document));
            gridView.ColumnDelete(nameof(PackageDocumentChronicle.User));
            gridView.ColumnDelete(nameof(PackageDocumentChronicle.Staff));
            gridView.ColumnDelete(nameof(PackageDocumentChronicle.Event));
            gridView.ColumnDelete(nameof(PackageDocumentChronicle.PackageDocumentStatusNew));
            gridView.ColumnDelete(nameof(PackageDocumentChronicle.PackageDocumentStatusOld));
            gridView.ColumnDelete(nameof(PackageDocumentChronicle.PackageDocumentType));

            gridControl.GridControlSetup();
            gridView.GridViewSetup(isColumnAutoWidth: false, isShowFooter: false, isShowFilterPanelMode: false);
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

                    barBtnAdd.Visibility = BarItemVisibility.Never;
                    barBtnEdit.Visibility = BarItemVisibility.Never;
                    barBtnDel.Visibility = BarItemVisibility.Never;

                    barCheckFindPanel.Checked = gridView.IsFindPanelVisible;
                    barCheckAutoFilterRow.Checked = gridView.OptionsView.ShowAutoFilterRow;

                    popupMenu.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }
        
        private void barBtnUpdate_ItemClick(object sender, ItemClickEventArgs e)
        {
            UpdateData();
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
    }
}
