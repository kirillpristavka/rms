using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using PulsLibrary.Extensions.DevForm;
using RMS.Core.Model.PackagesDocument;
using RMS.UI.xUI.PackagesDocument.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.xUI.PackagesDocument.Controllers
{
    public partial class PackageDocumentObjControl : XtraUserControl
    {
        private UnitOfWork _uof;
        private PackageDocumentObj _packageDocumentObj;
        private List<PackageDocumentObj> _listObj;

        public delegate void FocusedRowChangedEventHandler(PackageDocumentObj obj, int focusedRowHandle);
        public event FocusedRowChangedEventHandler FocusedRowChangedEvent;

        public List<PackageDocumentObj> PackageDocumentObjs => _listObj;

        public PackageDocumentObjControl()
        {
            InitializeComponent();
            _listObj = new List<PackageDocumentObj>();
        }
        
        public PackageDocumentObjControl(List<PackageDocumentObj> listObj) : this()
        {
            _listObj = listObj;
        }

        public void SetUnitOfWork(UnitOfWork uof)
        {
            _uof = uof;
        }

        public void UpdateData(object listObj)
        {            
            if (listObj is List<PackageDocumentObj> list)
            {
                _listObj = list; 
                gridControl.DataSource = this._listObj;
            }
            else
            {
                gridControl.DataSource = new List<PackageDocumentObj>();
            }
        }

        private void Control_Load(object sender, EventArgs e)
        {
            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref gridView);

            UpdateData(_listObj);
            
            gridView.ColumnSetup($"{nameof(PackageDocumentObj.Oid)}", isVisible: false, caption: "[OID]", width: 100, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
            gridView.ColumnSetup($"{nameof(PackageDocumentObj.IsOriginalDocument)}", isVisible: false, caption: "Оригинальный\nдокумент", width: 175, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
            gridView.ColumnSetup($"{nameof(PackageDocumentObj.IsScannedDocument)}", isVisible: false, caption: "Сканированный\nдокумент", width: 175, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
            gridView.ColumnSetup($"{nameof(PackageDocumentObj.FileName)}", caption: "Имя файла", width: 200, isFixedWidth: true);
            gridView.ColumnSetup($"{nameof(PackageDocumentObj.DateReceiving)}", caption: "Дата\nполучения", width: 150, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
            gridView.ColumnSetup($"{nameof(PackageDocumentObj.DateDeparture)}", caption: "Дата\nотправления", width: 150, isFixedWidth: true, horzAlignment: HorzAlignment.Center);

            gridView.ColumnDelete(nameof(PackageDocumentObj.PackageDocument));
            gridView.ColumnDelete(nameof(PackageDocumentObj.File));

            gridControl.GridControlSetup();
            gridView.GridViewSetup(isColumnAutoWidth: false, isShowFooter: false);
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

        /// <summary>
        /// Открытие формы редактирования.
        /// </summary>
        /// <param name="obj">Операция для изменения.</param>
        private void OpenEditForm(object obj)
        {
            var form = new PackageDocumentObjEdit(obj, _uof);
            form.FormClosed += OperationEditFormClosed;
            form.XtraFormShow();
        }

        private void OperationEditFormClosed(object sender, FormClosedEventArgs e)
        {
            if (sender is PackageDocumentObjEdit objEdit)
            {
                if (objEdit.IsSave)
                {
                    var obj = _listObj.FirstOrDefault(f => f.File != null && f.FileName.Equals(objEdit.PackageDocumentObj?.FileName ?? default));
                    if (obj is null)
                    {
                        _listObj.Add(objEdit.PackageDocumentObj);
                    }
                    
                    gridView.RefreshData();
                    gridView.FocusedRowHandle = gridView.LocateByValue(nameof(XPObject.Oid), objEdit.PackageDocumentObj?.Oid);
                }
            }
        }

        private void barBtnAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenEditForm(null);
        }

        private void barBtnEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridView.GetRow(gridView.FocusedRowHandle) is PackageDocumentObj obj
                && obj.File != null)
            {
                OpenEditForm(obj);
            }
        }

        private async void barBtnDel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridView.GetRow(gridView.FocusedRowHandle) is PackageDocumentObj obj)
            {
                var text = $"Вы действительно хотите удалить документ: {obj}?";
                var caption = $"Удаление пакета документов [OID:{obj.Oid}]";

                if (XtraMessageBox.Show(text,
                                        caption,
                                        MessageBoxButtons.OKCancel,
                                        MessageBoxIcon.Question) == DialogResult.OK)
                {

                    using var uof = new UnitOfWork();
                    var currentObj = await uof.GetObjectByKeyAsync<PackageDocumentObj>(obj.Oid);
                    if (currentObj != null)
                    {
                        currentObj.Delete();
                    }

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
            if (gridView.GetRow(gridView.FocusedRowHandle) is PackageDocumentObj obj)
            {
                _packageDocumentObj = obj;
                FocusedRowChangedEvent?.Invoke(obj, gridView.FocusedRowHandle);
            }
        }

        private void gridView_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (gridView.GetRow(e.RowHandle) is PackageDocument obj)
                {
                    if (obj.PackageDocumentStatus != null)
                    {
                        var color = obj.PackageDocumentStatus?.Color;
                        if (!string.IsNullOrWhiteSpace(color))
                        {
                            e.Appearance.BackColor = ColorTranslator.FromHtml(color);
                        }
                    }
                }
            }
        }
    }
}
