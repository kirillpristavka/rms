using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.BandedGrid;
using PulsLibrary.Extensions.DevForm;
using RMS.Core.Controllers.PackagesDocument;
using RMS.Core.Model;
using RMS.Core.Model.PackagesDocument;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using RMS.Core.Model.InfoCustomer;
using DevExpress.Data.Filtering;
using RMS.Core.Extensions;

namespace RMS.UI.xUI.PackagesDocument.Controllers
{
    public partial class PackageDocumentTypeControl : XtraUserControl
    {
        private UnitOfWork _uof;
        private PackageDocument _packageDocument;
        private CustomerStaff _customerStaff;
        private Customer _customer;
        private bool _isPermissionEdit;
        private List<PackageDocumentType> _listObj;

        private RepositoryItemComboBox repositoryItemComboBoxCustomerStaff;

        public IEnumerable<PackageDocumentType> Objs => _listObj ?? new List<PackageDocumentType>();

        public PackageDocumentTypeControl()
        {
            InitializeComponent();
            _listObj = new List<PackageDocumentType>();
        }

        public PackageDocumentTypeControl(List<PackageDocumentType> listObj) : this()
        {
            _listObj = listObj;
        }

        public IEnumerable<PackageDocumentType> GetPackageDocumentType()
        {
            return _listObj ?? new List<PackageDocumentType>();
        }

        public void SetUnitOfWork(UnitOfWork uof)
        {
            _uof = uof;
        }
        
        public async void SetCustomer(Customer customer)
        {
            _customer = customer;

            repositoryItemComboBoxCustomerStaff.Items.Clear();
            if (_customer != null)
            {
                repositoryItemComboBoxCustomerStaff.Items.AddRange(await new XPQuery<CustomerStaff>(_uof)
                   ?.Where(w => w.Customer != null && w.Customer.Oid == _customer.Oid)
                   ?.ToListAsync());
            }
        }

        private CriteriaOperator _activeFilterCriteria;
        public void SetActiveFilterCriteria(CriteriaOperator criteriaOperator)
        {
            if (criteriaOperator is null)
            {
                _activeFilterCriteria = null;
                advBandedGridView.ActiveFilterCriteria = _activeFilterCriteria;
            }
            else
            {
                _activeFilterCriteria = criteriaOperator;
                advBandedGridView.ActiveFilterCriteria = _activeFilterCriteria;
            }
        }

        public void SetInfoActiveFilterCriteria(CriteriaOperator criteriaOperator)
        {
            if (criteriaOperator is null)
            {
                advBandedGridView.ActiveFilterCriteria = _activeFilterCriteria;
            }
            else
            {
                if (_activeFilterCriteria is null)
                {
                    advBandedGridView.ActiveFilterCriteria = criteriaOperator;
                }
                else
                {
                    var criteria = new GroupOperator(GroupOperatorType.And);
                    criteria.Operands.Add(_activeFilterCriteria);
                    criteria.Operands.Add(criteriaOperator);
                    advBandedGridView.ActiveFilterCriteria = criteria;
                }
            }
        }

        public void ClearListObj()
        {
            foreach (var item in _listObj)
            {
                item.CustomerStaff = null;
            }
            advBandedGridView.RefreshData();
        }

        public void SetPackageDocument(PackageDocument packageDocument)
        {
            _packageDocument = packageDocument;
        }
        
        public void SetCustomerStaff(CustomerStaff customerStaff)
        {
            _customerStaff = customerStaff;
        }

        public void SetPermissionEdit(bool isPermissionEdit)
        {
            _isPermissionEdit = isPermissionEdit;
        }

        public void UpdateData(object listObj)
        {            
            if (listObj is List<PackageDocumentType> list)
            {
                var currentObjOid = -1;
                if (advBandedGridView.GetRow(advBandedGridView.FocusedRowHandle) is PackageDocumentType obj)
                {
                    currentObjOid = obj.Oid;
                }

                _listObj = list;
                gridControl.DataSource = _listObj;

                if (currentObjOid > 0)
                {
                    advBandedGridView.FocusedRowHandle = advBandedGridView.LocateByValue(nameof(PackageDocumentType.Oid), currentObjOid);
                }
            }
            else
            {
                gridControl.DataSource = new List<PackageDocumentType>();
            }
        }

        private async void Control_Load(object sender, EventArgs e)
        {
            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref advBandedGridView);

            UpdateData(_listObj);

            advBandedGridView.OptionsBehavior.AutoPopulateColumns = false;
            gridControl.MainView = advBandedGridView;

            advBandedGridView.OptionsBehavior.Editable = _isPermissionEdit;

            var bandCustomer = new GridBand() { Caption = "", Visible = !_isPermissionEdit };
            var bandDocument = new GridBand() { Caption = "" };
            var bandStatus = new GridBand() { Caption = "", Visible = !_isPermissionEdit };
            var bandCustomerStaff = new GridBand() { Caption = "" };

            var bandBlank = new GridBand() { Caption = "Бланк" };
            var bandScan = new GridBand() { Caption = "Скан" };
            var bandOriginal = new GridBand() { Caption = "Оригинал" };

            var bandBlankRevision = new GridBand() { Caption = "Отправлено на доработку" };
            var bandScanAfterRevision = new GridBand() { Caption = "Скан после доработки" };
            var bandOriginalAfterRevision = new GridBand() { Caption = "Оригинал после доработки" };

            advBandedGridView.Bands.AddRange(new GridBand[] { bandCustomer, 
                                                 bandDocument, 
                                                 bandStatus, 
                                                 bandCustomerStaff, 
                                                 bandBlank, 
                                                 bandScan, 
                                                 bandOriginal, 
                                                 bandBlankRevision, 
                                                 bandScanAfterRevision, 
                                                 bandOriginalAfterRevision });

            var bandedGridColumnCustomer = new BandedGridColumn() { FieldName = $"{nameof(PackageDocumentType.Customer)}", Visible = !_isPermissionEdit, Caption = "Клиент", Width = 350 };
            bandedGridColumnCustomer.OptionsColumn.AllowEdit = false;
            bandedGridColumnCustomer.OwnerBand = bandCustomer;
            bandedGridColumnCustomer.AutoFillDown = true;

            var bandedGridColumnName = new BandedGridColumn() { FieldName = $"{nameof(PackageDocumentType.Name)}", Visible = true, Caption = "Документ", Width = 350 };
            bandedGridColumnName.OptionsColumn.AllowEdit = false;
            bandedGridColumnName.OwnerBand = bandDocument;
            bandedGridColumnName.AutoFillDown = true;

            var repositoryItemComboBoxStatus = new RepositoryItemComboBox();
            repositoryItemComboBoxStatus.Buttons.Add(new EditorButton() { Kind = ButtonPredefines.Ellipsis });
            repositoryItemComboBoxStatus.Buttons.Add(new EditorButton() { Kind = ButtonPredefines.Delete });
            repositoryItemComboBoxStatus.TextEditStyle = TextEditStyles.DisableTextEditor;
            repositoryItemComboBoxStatus.AllowDropDownWhenReadOnly = DefaultBoolean.True;

            var bandedGridColumnStatus = new BandedGridColumn() { FieldName = $"{nameof(PackageDocumentType.Status)}", Visible = !_isPermissionEdit, Caption = "Статус", Width = 400 };
            if (_isPermissionEdit)
            {
                bandedGridColumnStatus.OptionsColumn.AllowEdit = false;
                //repositoryItemComboBoxStatus.ButtonPressed += RepositoryItemComboBox_ButtonPressed;
                //repositoryItemComboBoxStatus.Items.AddRange(await new XPQuery<PackageDocumentStatus>(_uof)?.ToListAsync());
                //bandedGridColumnStatus.ShowButtonMode = ShowButtonModeEnum.ShowAlways;
            }
            bandedGridColumnStatus.ColumnEdit = repositoryItemComboBoxStatus;
            bandedGridColumnStatus.OwnerBand = bandStatus;
            bandedGridColumnStatus.AutoFillDown = true;

            repositoryItemComboBoxCustomerStaff = new RepositoryItemComboBox();
            repositoryItemComboBoxCustomerStaff.Buttons.Add(new EditorButton() { Kind = ButtonPredefines.Ellipsis });
            repositoryItemComboBoxCustomerStaff.Buttons.Add(new EditorButton() { Kind = ButtonPredefines.Delete });
            repositoryItemComboBoxCustomerStaff.TextEditStyle = TextEditStyles.DisableTextEditor;
            repositoryItemComboBoxCustomerStaff.AllowDropDownWhenReadOnly = DefaultBoolean.True;

            var bandedGridColumnCustomerStaff = new BandedGridColumn() { FieldName = $"{nameof(PackageDocumentType.CustomerStaff)}", Visible = true, Caption = "Сотрудник", Width = 300 };
            if (_isPermissionEdit)
            {
                repositoryItemComboBoxCustomerStaff.ButtonPressed += RepositoryItemComboBoxCustomerStaff_ButtonPressed;
                bandedGridColumnCustomerStaff.ShowButtonMode = ShowButtonModeEnum.ShowAlways;
            }
            bandedGridColumnCustomerStaff.ColumnEdit = repositoryItemComboBoxCustomerStaff;
            bandedGridColumnCustomerStaff.OwnerBand = bandCustomerStaff;
            bandedGridColumnCustomerStaff.AutoFillDown = true;

            SetBandedGridColumn(bandBlank, $"{nameof(PackageDocumentType.DateReceivingForm)}", $"{nameof(PackageDocumentType.IsFormSent)}", "Дата направления", "Отправлено");
            SetBandedGridColumn(bandScan, $"{nameof(PackageDocumentType.DateReceivingScan)}", $"{nameof(PackageDocumentType.IsSignedScanReceived)}");
            SetBandedGridColumn(bandOriginal, $"{nameof(PackageDocumentType.DateReceivingOriginal)}", $"{nameof(PackageDocumentType.IsSignedOriginalReceived)}");

            SetBandedGridColumn(bandBlankRevision, $"{nameof(PackageDocumentType.DateSentRevision)}", $"{nameof(PackageDocumentType.IsSentRevision)}", "Дата направления", "Отправлено");
            SetBandedGridColumn(bandScanAfterRevision, $"{nameof(PackageDocumentType.DateScanAfterRevision)}", $"{nameof(PackageDocumentType.IsScanAfterRevision)}");
            SetBandedGridColumn(bandOriginalAfterRevision, $"{nameof(PackageDocumentType.DateOriginalAfterRevision)}", $"{nameof(PackageDocumentType.IsOriginalAfterRevision)}");

            gridControl.GridControlSetup();

            advBandedGridView.Appearance.BandPanel.TextOptions.WordWrap = WordWrap.Wrap;
            advBandedGridView.BandPanelRowHeight = 45;
            
            advBandedGridView.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
        }

        private static void SetBandedGridColumn(GridBand gridBand, string columnDate, string columnCheck, string captionDate = "Дата получения", string captionCheck = "Получено")
        {
            var bandedGridColumnDateReceivingOriginal = new BandedGridColumn() { FieldName = columnDate, Visible = true, Caption = captionDate, Width = 150 };
            bandedGridColumnDateReceivingOriginal.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            bandedGridColumnDateReceivingOriginal.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
            if (bandedGridColumnDateReceivingOriginal.ColumnEdit != null)
            {
                bandedGridColumnDateReceivingOriginal.ColumnEdit.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                bandedGridColumnDateReceivingOriginal.ColumnEdit.Appearance.TextOptions.VAlignment = VertAlignment.Center;
            }
            bandedGridColumnDateReceivingOriginal.OwnerBand = gridBand;
            var bandedGridColumnIsSignedOriginalReceived = new BandedGridColumn() { FieldName = columnCheck, Visible = true, Caption = captionCheck };
            bandedGridColumnIsSignedOriginalReceived.OwnerBand = gridBand;
            bandedGridColumnIsSignedOriginalReceived.RowIndex = 1;
        }

        private async void RepositoryItemComboBoxCustomerStaff_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (advBandedGridView.GetRow(advBandedGridView.FocusedRowHandle) is PackageDocumentType packageDocumentType)
            {
                if (e.Button.Kind == ButtonPredefines.Delete)
                {
                    packageDocumentType.CustomerStaff = null;
                    advBandedGridView.UpdateCurrentRow();
                }
                else if (e.Button.Kind == ButtonPredefines.Ellipsis)
                {
                    if (_customer is null)
                    {
                        DevXtraMessageBox.ShowXtraMessageBox("Пожалуйста укажите клиента.");
                        return;
                    }
                    
                    var oidCustomerStaff = cls_BaseSpr.RunBaseSpr(
                        (int)cls_App.ReferenceBooks.CustomerStaff,
                        criteria: new BinaryOperator($"{nameof(CustomerStaff.Customer)}.{nameof(Customer.Oid)}", _customer?.Oid),
                        oPar3: _customer?.Oid);
                    
                    if (oidCustomerStaff > 0)
                    {
                        packageDocumentType.CustomerStaff = await new XPQuery<CustomerStaff>(_uof)
                            ?.FirstOrDefaultAsync(f => f.Oid == oidCustomerStaff);
                        advBandedGridView.UpdateCurrentRow();
                    }
                }
            }
        }

        private async void RepositoryItemComboBox_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (advBandedGridView.GetRow(advBandedGridView.FocusedRowHandle) is PackageDocumentType packageDocumentType)
            {
                if (e.Button.Kind == ButtonPredefines.Delete)
                {
                    packageDocumentType.PackageDocumentStatus = null;
                    advBandedGridView.UpdateCurrentRow();
                }
                else if (e.Button.Kind == ButtonPredefines.Ellipsis)
                {
                    var oid = cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.PackageDocumentStatus, packageDocumentType.PackageDocumentStatus?.Oid ?? -1);
                    if (oid > 0)
                    {
                        packageDocumentType.PackageDocumentStatus = await new XPQuery<PackageDocumentStatus>(_uof)
                            ?.FirstOrDefaultAsync(f => f.Oid == oid);
                        advBandedGridView.UpdateCurrentRow();
                    }
                }
            }
        }

        private void gridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (sender is AdvBandedGridView gridView)
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

                    if (_isPermissionEdit)
                    {
                        barBtnAdd.Visibility = BarItemVisibility.Always;
                        barBtnEdit.Visibility = BarItemVisibility.Always;
                        barBtnDel.Visibility = BarItemVisibility.Always;
                    }
                    else
                    {
                        barBtnAdd.Visibility = BarItemVisibility.Never;
                        barBtnEdit.Visibility = BarItemVisibility.Never;
                        barBtnDel.Visibility = BarItemVisibility.Never;
                    }

                    barCheckFindPanel.Checked = gridView.IsFindPanelVisible;
                    barCheckAutoFilterRow.Checked = gridView.OptionsView.ShowAutoFilterRow;

                    popupMenu.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }
        
        private void AddObjToList(Document currentObj)
        {
            var obj = _listObj.FirstOrDefault(f => f.Document != null && f.Document.Equals(currentObj));
            if (obj is null)
            {
                obj = new PackageDocumentType(_uof)
                {
                    Document = currentObj
                };

                _listObj.Add(obj);
            }
        }

        private async void barBtnAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            var oids = cls_BaseSpr.GetNumbersObjFromDirectory((int)cls_App.ReferenceBooks.Document);

            foreach (var oid in oids)
            {
                var document = await new XPQuery<Document>(_uof)?.FirstOrDefaultAsync(f => f.Oid == oid);
                AddObjToList(document);
            }

            advBandedGridView.RefreshData();
        }

        private async void barBtnDel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (advBandedGridView.GetRow(advBandedGridView.FocusedRowHandle) is PackageDocumentType obj)
            {
                var text = $"Вы действительно хотите удалить документа: {obj}?";
                var caption = $"Удаление документа [OID:{obj.Oid}]";

                if (XtraMessageBox.Show(text,
                                        caption,
                                        MessageBoxButtons.OKCancel,
                                        MessageBoxIcon.Question) == DialogResult.OK)
                {

                    using var uof = new UnitOfWork();
                    var currentObj = await uof.GetObjectByKeyAsync<PackageDocumentType>(obj.Oid);
                    if (currentObj != null)
                    {
                        currentObj.Delete();
                    }
                    
                    _listObj.Remove(obj);

                    advBandedGridView.FocusedRowHandle--;
                    advBandedGridView.RefreshData();
                }
            }
        }

        private async void barBtnUpdate_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (_packageDocument != null)
            {
                UpdateData(await PackagesDocumentController.GetPackagesDocumentTypeAsync(_uof, _packageDocument));
            }
            else if (_customerStaff != null)
            {
                UpdateData(await PackagesDocumentController.GetPackagesDocumentTypeAsync(_uof, _customerStaff));
            }
        }

        private void barCheckFindPanel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (advBandedGridView.IsFindPanelVisible)
            {
                advBandedGridView.HideFindPanel();
            }
            else
            {
                advBandedGridView.ShowFindPanel();
            }
        }

        private void barCheckAutoFilterRow_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (advBandedGridView.OptionsView.ShowAutoFilterRow)
            {
                advBandedGridView.OptionsView.ShowAutoFilterRow = false;
            }
            else
            {
                advBandedGridView.OptionsView.ShowAutoFilterRow = true;
            }
        }

        private void gridView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            //if (gridView.GetRow(gridView.FocusedRowHandle) is PackageDocumentType obj)
            //{
            //    _packageDocument = obj;
            //    FocusedRowChangedEvent?.Invoke(obj, gridView.FocusedRowHandle);
            //}
        }

        private void gridView_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (sender is AdvBandedGridView advBandedGridView)
            {
                if (advBandedGridView.GetRow(e.RowHandle) is PackageDocumentType obj)
                {
                    var color = obj.GetStatusColor().GetEnumDescription();
                    if (!string.IsNullOrWhiteSpace(color))
                    {
                        e.Appearance.BackColor = ColorTranslator.FromHtml(color);
                    }
                }
            }
        }

        private void gridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (sender is AdvBandedGridView gridView)
            {
                if (e.Column.FieldName == nameof(PackageDocumentType.DateReceivingForm)
                    || e.Column.FieldName == nameof(PackageDocumentType.DateReceivingScan)
                    || e.Column.FieldName == nameof(PackageDocumentType.DateReceivingOriginal)
                    || e.Column.FieldName == nameof(PackageDocumentType.DateSentRevision)
                    || e.Column.FieldName == nameof(PackageDocumentType.DateScanAfterRevision)
                    || e.Column.FieldName == nameof(PackageDocumentType.DateOriginalAfterRevision))
                {
                    if (e.CellValue is null)
                    {
                        e.Appearance.BackColor = Color.LightGray;
                    }
                }
            }
        }
    }
}
