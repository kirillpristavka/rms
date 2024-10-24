using DevExpress.Data.Filtering;
using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Taxes;
using RMS.Setting.Model.GeneralSettings;
using RMS.UI.Forms.Directories;
using RMS.UI.Forms.ReferenceBooks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Forms
{
    public partial class DigitalSignatureForm : XtraForm
    {
        private Session _session;
        private XPCollection<ElectronicReportingCustomer> _electronicReportings;
        private DateTime _currentDateTime = DateTime.Now.Date;
        private DateTime _nextDateTime;

        /// <summary>
        /// Настройка функционирования таблиц.
        /// </summary>
        private void FunctionalGridSetup()
        {
            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref gridView);
            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref gridViewObj);
            BVVGlobal.oFuncXpo.PressEnterGrid<ElectronicReportingCustomer, ElectronicReportingCustomerEdit2>(gridView);
            BVVGlobal.oFuncXpo.PressEnterGrid<ElectronicReportingСustomerNotification, ElectronicReportingСustomerNotificationEdit>(gridViewNotification);
        }
                
        public DigitalSignatureForm(Session session)
        {
            InitializeComponent();
            FunctionalGridSetup();

            _session = session ?? BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
        }        

        private async System.Threading.Tasks.Task SetAccessRights()
        {
            try
            {
                //using (var uof = new UnitOfWork())
                //{
                //    var user = await uof.GetObjectByKeyAsync<User>(DatabaseConnection.User.Oid);
                //    if (user != null)
                //    {
                //        var accessRights = user.AccessRights;
                //        if (accessRights != null)
                //        {
                //            isEditDealForm = accessRights.IsEditDealForm;
                //            isDeleteDealForm = accessRights.IsDeleteDealForm;
                //        }
                //    }
                //}

                //barBtnRefreshFromLetter.Enabled = isEditDealForm;
                //barBtnBulkReplacement.Enabled = isEditDealForm;

                //barBtnDel.Enabled = isDeleteDealForm;
                //barBtnRemovingEmptyTrades.Enabled = isDeleteDealForm;
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private void GetInfo()
        {
            var result = "Информация по ЭЦП не найдена.";
            
            if (_electronicReportings != null)
            {
                checkedListInfo.Items.Clear();
                
                result = $"Информация о провайдерах:{Environment.NewLine}{Environment.NewLine}";

                var emptyObj = default(string);
                
                foreach (var item in _electronicReportings
                    .GroupBy(g => g.CurrentElectronicReportingString)
                    .OrderBy(o => o.Key))
                {
                    if (string.IsNullOrWhiteSpace(item.Key))
                    {
                        emptyObj = $"ОТСУТСТВУЕТ - {item.Count()}";
                        continue;
                    }
                    
                    var name = $"{item.Key} - {item.Count()}{Environment.NewLine}";
                    result += name;

                    checkedListInfo.Items.Add(item.Key, name);
                }

                if (!string.IsNullOrWhiteSpace(emptyObj))
                {
                    result += $"{Environment.NewLine}{Environment.NewLine}{emptyObj}";
                }
            }
            
            //memoInfo.EditValue = result;
        }
        
        private async void Form_Load(object sender, EventArgs e)
        {
            _nextDateTime = _currentDateTime.AddMonths(1);

            await SetAccessRights();

            var groupOperator = new GroupOperator(GroupOperatorType.And);
            groupOperator.Operands.Add(new NotOperator(new NullOperator(nameof(ElectronicReportingCustomer.Customer))));

            var groupOperatorOr = new GroupOperator(GroupOperatorType.Or);

            var customerWorkStatus = await new XPQuery<CustomerStatus>(_session).Where(w => w.Status != null).FirstOrDefaultAsync(f => f.Status.Name == "Обслуживаем");
            if (customerWorkStatus != null)
            {
                groupOperatorOr.Operands.Add(new BinaryOperator($"{nameof(ElectronicReportingCustomer.Customer)}.{nameof(Customer.StatusString)}", customerWorkStatus.StatusString));
            }
            
            var customerForthcomingStatus = await new XPQuery<CustomerStatus>(_session).Where(w => w.Status != null).FirstOrDefaultAsync(f => f.Status.Name == "Предстоящее расторжение");
            if (customerForthcomingStatus != null)
            {
                groupOperatorOr.Operands.Add(new BinaryOperator($"{nameof(ElectronicReportingCustomer.Customer)}.{nameof(Customer.StatusString)}", customerForthcomingStatus.StatusString));
            }

            if (groupOperatorOr.Operands.Count > 0)
            {
                groupOperator.Operands.Add(groupOperatorOr);
            }

            _electronicReportings = new XPCollection<ElectronicReportingCustomer>(_session, groupOperator);
            gridControl.DataSource = _electronicReportings;

            gridView.OptionsView.ColumnAutoWidth = false;

            if (gridView.Columns[nameof(ElectronicReportingCustomer.Oid)] is GridColumn columnOid)
            {
                columnOid.Width = 50;
                columnOid.OptionsColumn.FixedWidth = true;
                columnOid.Visible = false;
                columnOid.OptionsColumn.ReadOnly = true;
                columnOid.OptionsColumn.AllowEdit = false;
            }

            if (gridView.Columns[nameof(ElectronicReportingCustomer.CustomerName)] is GridColumn columnCustomerName)
            {
                columnCustomerName.Width = 250;
                columnCustomerName.OptionsColumn.ReadOnly = true;
                columnCustomerName.OptionsColumn.AllowEdit = false;
                columnCustomerName.Summary.Add(DevExpress.Data.SummaryItemType.Count, columnCustomerName.Name, "{0}");
            }

            if (gridView.Columns[nameof(ElectronicReportingCustomer.CurrentElectronicReportingString)] is GridColumn columnElectronicReportingString)
            {
                columnElectronicReportingString.Width = 250;
                columnElectronicReportingString.OptionsColumn.ReadOnly = true;
                columnElectronicReportingString.OptionsColumn.AllowEdit = false;
            }

            if (gridView.Columns[nameof(ElectronicReportingCustomer.DateSince)] is GridColumn columnDateSince)
            {
                columnDateSince.Width = 125;
                columnDateSince.OptionsColumn.FixedWidth = true;
                columnDateSince.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                columnDateSince.OptionsColumn.ReadOnly = true;
                columnDateSince.OptionsColumn.AllowEdit = false;
            }

            if (gridView.Columns[nameof(ElectronicReportingCustomer.DateTo)] is GridColumn columnDateTo)
            {
                columnDateTo.Width = 125;
                columnDateTo.OptionsColumn.FixedWidth = true;
                columnDateTo.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                columnDateTo.OptionsColumn.ReadOnly = true;
                columnDateTo.OptionsColumn.AllowEdit = false;
            }


            if (gridView.Columns[nameof(ElectronicReportingCustomer.LicenseDateSince)] is GridColumn columnsLicenseDateSince)
            {
                columnsLicenseDateSince.Width = 125;
                columnsLicenseDateSince.OptionsColumn.FixedWidth = true;
                columnsLicenseDateSince.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                columnsLicenseDateSince.OptionsColumn.ReadOnly = true;
                columnsLicenseDateSince.OptionsColumn.AllowEdit = false;
            }


            if (gridView.Columns[nameof(ElectronicReportingCustomer.LicenseDateTo)] is GridColumn columnLicenseDateTo)
            {
                columnLicenseDateTo.Width = 125;
                columnLicenseDateTo.OptionsColumn.FixedWidth = true;
                columnLicenseDateTo.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                columnLicenseDateTo.OptionsColumn.ReadOnly = true;
                columnLicenseDateTo.OptionsColumn.AllowEdit = false;
            }

            if (gridView.Columns[nameof(ElectronicReportingCustomer.Comment)] is GridColumn columnComment)
            {
                var repositoryItemMemoEdit = gridControl.RepositoryItems.Add(nameof(MemoEdit)) as RepositoryItemMemoEdit;
                repositoryItemMemoEdit.WordWrap = true;

                columnComment.ColumnEdit = repositoryItemMemoEdit;
                columnComment.Width = 400;
                columnComment.OptionsColumn.FixedWidth = true;
                //columnNotifications.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                //columnNotifications.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
            }

            if (gridView.Columns[nameof(ElectronicReportingCustomer.Notifications)] is GridColumn columnNotifications)
            {
                var repositoryItemMemoEdit = gridControl.RepositoryItems.Add(nameof(MemoEdit)) as RepositoryItemMemoEdit;
                repositoryItemMemoEdit.WordWrap = true;

                columnNotifications.ColumnEdit = repositoryItemMemoEdit;
                columnNotifications.Width = 400;
                columnNotifications.OptionsColumn.FixedWidth = true;
                //columnNotifications.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                //columnNotifications.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
                columnNotifications.OptionsColumn.ReadOnly = true;
                columnNotifications.OptionsColumn.AllowEdit = false;
            }

            gridView.BestFitColumns();            
            await LoadSettingsForm();

            GetInfo();
        }

        private async System.Threading.Tasks.Task LoadSettingsForm()
        {
            try
            {
                //TODO: УДАЛИТЬ ПОСЛЕ ОБНОВЛЕНИЙ И СНЯТЬ КОММЕНТАРИЙ.
                layoutControlDigitalSignature.RestoreDefaultLayout();               
                //await BVVGlobal.oFuncXpo.RestoreLayoutToStreamAsync(layoutControlDigitalSignature, $"{this.Name}_{nameof(layoutControlDigitalSignature)}");
                //await BVVGlobal.oFuncXpo.RestoreLayoutToStreamAsync(gridView, $"{this.Name}_{nameof(gridView)}");
                //await BVVGlobal.oFuncXpo.RestoreLayoutToStreamAsync(gridViewObj, $"{this.Name}_{nameof(gridViewObj)}");                
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async System.Threading.Tasks.Task SaveSettingsForms()
        {
            try
            {
                await BVVGlobal.oFuncXpo.SaveLayoutToStreamAsync(layoutControlDigitalSignature, $"{this.Name}_{nameof(layoutControlDigitalSignature)}");
                await BVVGlobal.oFuncXpo.SaveLayoutToStreamAsync(gridView, $"{this.Name}_{nameof(gridView)}");
                await BVVGlobal.oFuncXpo.SaveLayoutToStreamAsync(gridViewObj, $"{this.Name}_{nameof(gridViewObj)}");
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private bool isFirstLoadGridViewObj = false;        
        private void gridView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (gridView.GetRow(gridView.FocusedRowHandle) is ElectronicReportingCustomer obj)
                {
                    obj?.Reload();

                    LoadGridControlObj(obj);
                    LoadGridControlNotification(obj);
                }
                else
                {
                    gridControlObj.DataSource = null;
                    gridControlNotification.DataSource = null;
                }

                if (gridView.GetRow(e.PrevFocusedRowHandle) is ElectronicReportingCustomer objPrev)
                {
                    objPrev?.Reload();
                }
            }
        }

        private bool isFirstLoadGridViewNotification = false;
        private void LoadGridControlNotification(ElectronicReportingCustomer obj)
        {
            gridControlNotification.DataSource = obj.ElectronicReportingСustomerNotifications;
            if (isFirstLoadGridViewNotification is false)
            {
                gridViewNotification.OptionsBehavior.Editable = false;

                if (gridViewNotification.Columns[nameof(ElectronicReportingСustomerNotification.Oid)] is GridColumn columnOid)
                {
                    columnOid.Width = 50;
                    columnOid.OptionsColumn.FixedWidth = true;
                    columnOid.Visible = false;
                }                

                if (gridViewNotification.Columns[nameof(ElectronicReportingСustomerNotification.Date)] is GridColumn columnDate)
                {
                    columnDate.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                    columnDate.Width = 125;
                    columnDate.OptionsColumn.FixedWidth = true;
                    columnDate.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                }

                if (gridViewNotification.Columns[nameof(ElectronicReportingСustomerNotification.StaffString)] is GridColumn columnStaffString)
                {
                    columnStaffString.Width = 200;
                    columnStaffString.OptionsColumn.FixedWidth = true;
                }

                if (gridViewNotification.Columns[nameof(ElectronicReportingСustomerNotification.RecipientString)] is GridColumn columnRecipient)
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

        private void LoadGridControlObj(ElectronicReportingCustomer obj)
        {
            gridControlObj.DataSource = obj.ElectronicReportingСustomerObjects;

            var columnElectronicReportingString = default(GridColumn);

            if (gridViewObj.Columns[nameof(ElectronicReportingСustomerObject.ElectronicReportingString)] is GridColumn)
            {
                columnElectronicReportingString = gridViewObj.Columns[nameof(ElectronicReportingСustomerObject.ElectronicReportingString)];

                columnElectronicReportingString.Caption = $"Электронная отчетность{Environment.NewLine}{obj.Customer}{Environment.NewLine}(провайдер)";
                columnElectronicReportingString.Width = 450;
            }

            if (columnElectronicReportingString != null)
            {
                var buttonEdit = gridControl.RepositoryItems.Add(nameof(ButtonEdit)) as RepositoryItemButtonEdit;
                buttonEdit.TextEditStyle = TextEditStyles.DisableTextEditor;
                buttonEdit.ButtonPressed += ButtonEdit_ButtonPressed;
                buttonEdit.DoubleClick += ButtonEdit_DoubleClick;

                columnElectronicReportingString.ColumnEdit = buttonEdit;
            }

            if (isFirstLoadGridViewObj is false)
            {
                if (gridViewObj.Columns[nameof(ElectronicReportingСustomerObject.Oid)] is GridColumn columnOid)
                {
                    columnOid.Width = 50;
                    columnOid.OptionsColumn.FixedWidth = true;
                    columnOid.Visible = false;
                }

                if (gridViewObj.Columns[nameof(ElectronicReportingСustomerObject.Comment)] is GridColumn columnComment)
                {
                    columnComment.Width = 350;
                }

                if (gridViewObj.Columns[nameof(ElectronicReportingСustomerObject.DateSince)] is GridColumn columnDateSince)
                {
                    columnDateSince.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                    columnDateSince.Width = 125;
                    columnDateSince.OptionsColumn.FixedWidth = true;
                    columnDateSince.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                }

                if (gridViewObj.Columns[nameof(ElectronicReportingСustomerObject.DateTo)] is GridColumn columnDateTo)
                {
                    columnDateTo.Width = 125;
                    columnDateTo.OptionsColumn.FixedWidth = true;
                    columnDateTo.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                }


                isFirstLoadGridViewObj = true;
            }
        }

        private async void ButtonEdit_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (gridViewObj.IsEmpty)
            {
                return;
            }

            var electronicReportingСustomerObject = gridViewObj.GetRow(gridViewObj.FocusedRowHandle) as ElectronicReportingСustomerObject;
            if (electronicReportingСustomerObject != null)
            {
                var oid = electronicReportingСustomerObject.ElectronicReporting?.Oid ?? -1;
                var id = cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.ElectronicReporting, oid);
                if (id > 0)
                {
                    var electronicReporting = await _session.GetObjectByKeyAsync<ElectronicReporting>(id);
                    electronicReportingСustomerObject.ElectronicReporting = electronicReporting;

                    var buttonEdit = sender as ButtonEdit;
                    if (buttonEdit != null)
                    {
                        buttonEdit.EditValue = electronicReportingСustomerObject.ElectronicReportingString;
                        electronicReportingСustomerObject?.Save();
                    }
                }
            }
        }
        private async void ButtonEdit_DoubleClick(object sender, EventArgs e)
        {
            if (gridViewObj.IsEmpty)
            {
                return;
            }

            var electronicReportingСustomerObject = gridViewObj.GetRow(gridViewObj.FocusedRowHandle) as ElectronicReportingСustomerObject;
            if (electronicReportingСustomerObject != null)
            {
                var oid = electronicReportingСustomerObject.ElectronicReporting?.Oid ?? -1;
                var id = cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.ElectronicReporting, oid);
                if (id > 0)
                {
                    var electronicReporting = await _session.GetObjectByKeyAsync<ElectronicReporting>(id);
                    electronicReportingСustomerObject.ElectronicReporting = electronicReporting;

                    var buttonEdit = sender as ButtonEdit;
                    if (buttonEdit != null)
                    {
                        buttonEdit.EditValue = electronicReportingСustomerObject.ElectronicReportingString;
                        electronicReportingСustomerObject?.Save();
                    }
                }
            }
        }
        private void gridView_RowStyle(object sender, RowStyleEventArgs e)
        {            
            if (sender is GridView gridView)
            {
                if (gridView.GetRow(e.RowHandle) is ElectronicReportingCustomer electronicReportingCustomer)
                {
                    if (string.IsNullOrWhiteSpace(electronicReportingCustomer.CurrentElectronicReportingString))
                    {
                        e.Appearance.BackColor = Color.LightCoral;
                    }
                    else
                    {
                        if (electronicReportingCustomer.DateTo is DateTime dateTo && _nextDateTime >= dateTo)
                        {
                            e.Appearance.BackColor = Color.LightYellow;
                        }
                        else if (electronicReportingCustomer.LicenseDateTo is DateTime licenseDateTo && _nextDateTime >= licenseDateTo)
                        {
                            e.Appearance.BackColor = Color.LightYellow;
                        }
                    }
                }
            }
        }

        private void gridViewObj_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (gridView.GetRow(e.RowHandle) is ElectronicReportingСustomerObject electronicReportingСustomerObject)
                {
                    if (electronicReportingСustomerObject?.ElectronicReportingCustomer?.ElectronicReportingСustomerObject == electronicReportingСustomerObject)
                    {
                        e.Appearance.BackColor = Color.LightGreen;
                    }
                }
            }
        }

        private async void DealForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            await SaveSettingsForms();
        }
        
        private void gridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (e.MenuType == GridMenuType.User || e.MenuType == GridMenuType.Row)
                {                 
                    popupMenu.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }

        private void barBtnUpdate_ItemClick(object sender, ItemClickEventArgs e)
        {
            _electronicReportings?.Reload();
        }

        private void gridViewObj_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (e.MenuType == GridMenuType.User || e.MenuType == GridMenuType.Row)
                {
                    popupMenuObj.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }

        private async void barBtnAddObj_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridView.GetRow(gridView.FocusedRowHandle) is ElectronicReportingCustomer electronicReportingCustomer)
            {
                electronicReportingCustomer.ElectronicReportingСustomerObjects.Add(
                new ElectronicReportingСustomerObject(_session)
                    {
                        UserCreate = await _session.GetObjectByKeyAsync<User>(DatabaseConnection.User?.Oid)
                    }
                );
            }
        }

        private void barBtnDelObj_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewObj.IsEmpty)
            {
                return;
            }

            var obj = gridViewObj.GetRow(gridViewObj.FocusedRowHandle) as ElectronicReportingСustomerObject;
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
                
        private void CheckedItemFalse(CheckButton checkItem)
        {
            foreach (var item in layoutControlDigitalSignature.Controls)
            {
                if (item is CheckButton currentCheckItem)
                {
                    if (currentCheckItem == checkItem)
                    {
                        continue;
                    }
                    else
                    {
                        currentCheckItem.Checked = false;
                    }
                }
            }

            _electronicReportings.Filter = null;
        }


        private CriteriaOperator _currentCriteria;
        private void checkActive_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckButton checkItem)
            {
                if (checkItem.Checked)
                {
                    CheckedItemFalse(checkItem);
                    _currentCriteria = new NotOperator(new NullOperator(nameof(ElectronicReportingCustomer.CurrentElectronicReportingString)));
                }
                else
                {
                    _currentCriteria = null;
                }

                _electronicReportings.Filter = _currentCriteria;
                GetInfo();
            }
        }

        private async void checkNotActive_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckButton checkItem)
            {
                if (checkItem.Checked)
                {
                    CheckedItemFalse(checkItem);

                    var groupOperator = new GroupOperator(GroupOperatorType.Or);
                    groupOperator.Operands.Add(new NullOperator(nameof(ElectronicReportingCustomer.CurrentElectronicReportingString)));
                    
                    var settings =  await _session.FindObjectAsync<Settings>(null);
                    if (settings != null)
                    {
                        var listElectronicReporting = Settings.GetDeserializeObject<List<int>>(settings.ElectronicReportingList);
                        if (listElectronicReporting != null)
                        {
                            foreach (var item in listElectronicReporting)
                            {
                                var obj = await _session.GetObjectByKeyAsync<ElectronicReporting>(item);
                                if (!string.IsNullOrWhiteSpace(obj.Name))
                                {
                                    groupOperator.Operands.Add(new BinaryOperator(nameof(ElectronicReportingCustomer.CurrentElectronicReportingString), obj.Name));
                                }                                
                            }
                        }
                    }
                    
                    _currentCriteria = groupOperator;
                }
                else
                {
                    _currentCriteria = null;
                }
                
                _electronicReportings.Filter = _currentCriteria;
                GetInfo();
            }
        }

        private void checkWillSoonEnd_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckButton checkItem)
            {
                if (checkItem.Checked)
                {
                    CheckedItemFalse(checkItem);
                    var groupOperator = new GroupOperator(GroupOperatorType.And);
                    groupOperator.Operands.Add(new NotOperator(new NullOperator(nameof(ElectronicReportingCustomer.DateTo))));
                    groupOperator.Operands.Add(new BinaryOperator(nameof(ElectronicReportingCustomer.DateTo), _nextDateTime, BinaryOperatorType.LessOrEqual));
                    _currentCriteria = groupOperator;
                }
                else
                {
                    _currentCriteria = null;
                }
                
                _electronicReportings.Filter = _currentCriteria;
                GetInfo();
            }
        }

        private void checkLicenseWillSoonEnd_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckButton checkItem)
            {
                if (checkItem.Checked)
                {
                    CheckedItemFalse(checkItem);
                    var groupOperator = new GroupOperator(GroupOperatorType.And);
                    groupOperator.Operands.Add(new NotOperator(new NullOperator(nameof(ElectronicReportingCustomer.LicenseDateTo))));
                    groupOperator.Operands.Add(new BinaryOperator(nameof(ElectronicReportingCustomer.LicenseDateTo), _nextDateTime, BinaryOperatorType.LessOrEqual));
                    _currentCriteria = groupOperator;
                }
                else
                {
                    _currentCriteria = null;
                }

                _electronicReportings.Filter = _currentCriteria;
                GetInfo();
            }
        }

        private void checkedListInfo_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (sender is CheckedListBoxControl control)
            {
                var checkedItems = control.Items.Where(w => w.CheckState == CheckState.Checked);
                if (checkedItems != null)
                {
                    var groupOperator = new GroupOperator(GroupOperatorType.And);
                    
                    if (checkedItems.Count() > 0)
                    {                   
                        var groupOperatorValue = new GroupOperator(GroupOperatorType.Or);
                        foreach (var item in checkedItems)
                        {
                            groupOperatorValue.Operands.Add(new BinaryOperator(nameof(ElectronicReportingCustomer.CurrentElectronicReportingString), item.Value));
                        }

                        if (groupOperatorValue.Operands.Count > 0)
                        {
                            groupOperator.Operands.Add(groupOperatorValue);
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(_currentCriteria?.ToString()))
                    {
                        groupOperator.Operands.Add(_currentCriteria);
                    }

                    if (groupOperator.Operands.Count > 0)
                    {
                        _electronicReportings.Filter = groupOperator;
                    }
                    else
                    {
                        _electronicReportings.Filter = _currentCriteria;
                    }                   
                }
            }
        }

        private void barBtnNotification_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridView.GetRow(gridView.FocusedRowHandle) is ElectronicReportingCustomer electronicReportingСustomer)
            {
                if (electronicReportingСustomer.Customer != null)
                {
                    var form = new ElectronicReportingСustomerNotificationEdit(electronicReportingСustomer);
                    form.ShowDialog();

                    electronicReportingСustomer?.ElectronicReportingСustomerNotifications?.Reload();
                }
            }
        }

        private void barBtnCustomer_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridView.GetRow(gridView.FocusedRowHandle) is ElectronicReportingCustomer electronicReportingСustomer)
            {
                if (electronicReportingСustomer.Customer != null)
                {
                    var form = new CustomerEdit(electronicReportingСustomer.Customer);
                    form.ShowDialog();
                }
            }
        }

        private void gridViewNotification_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (e.MenuType == GridMenuType.User || e.MenuType == GridMenuType.Row)
                {
                    popupMenuNotification.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }
        
        private void barBtnView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewNotification.GetRow(gridViewNotification.FocusedRowHandle) is ElectronicReportingСustomerNotification obj)
            {
                var form = new ElectronicReportingСustomerNotificationEdit(obj);
                form.ShowDialog();
            }
        }
    }
}