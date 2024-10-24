using DevExpress.Data.Filtering;
using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using PulsLibrary.Extensions.DevForm;
using RMS.Core.Controllers.Accounts;
using RMS.Core.Model.Accounts;
using RMS.UI.xUI.Accounts.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.xUI.Accounts.Controllers
{
    public partial class AccountStatementControl : XtraUserControl
    {
        private UnitOfWork _uof;
        private AccountStatement _accountStatement;
        private List<AccountStatement> _listObj;

        public delegate void FocusedRowChangedEventHandler(AccountStatement obj, int focusedRowHandle);
        public event FocusedRowChangedEventHandler FocusedRowChangedEvent;

        public AccountStatementControl()
        {
            InitializeComponent();
            _listObj = new List<AccountStatement>();
        }

        public AccountStatementControl(List<AccountStatement> listObj) : this()
        {
            _listObj = listObj;
        }

        public void SetUnitOfWork(UnitOfWork uof)
        {
            _uof = uof;
        }

        public IEnumerable<AccountStatement> GetListObj()
        {
            return _listObj ?? new List<AccountStatement>();
        }

        public void SetFilter(CriteriaOperator criteriaOperator)
        {
            if (string.IsNullOrWhiteSpace(criteriaOperator?.LegacyToString()?.Replace("()","")))
            {
                gridView.ActiveFilterCriteria = null;
            }
            else
            {
                gridView.ActiveFilterCriteria = criteriaOperator;
            }
        }

        public void UpdateData(object listObj)
        {            
            if (listObj is List<AccountStatement> list)
            {
                var currentObjOid = -1;
                if (gridView.GetRow(gridView.FocusedRowHandle) is AccountStatement obj)
                {
                    currentObjOid = obj.Oid;
                }

                _listObj = list;
                gridControl.DataSource = this._listObj;

                if (currentObjOid > 0)
                {
                    gridView.FocusedRowHandle = gridView.LocateByValue(nameof(AccountStatement.Oid), currentObjOid);
                }
            }
            else
            {
                gridControl.DataSource = new List<AccountStatement>();
            }
        }

        private void Control_Load(object sender, EventArgs e)
        {
            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref gridView);

            UpdateData(_listObj);
            
            gridView.ColumnSetup($"{nameof(AccountStatement.Oid)}", isVisible: false, caption: "[OID]", width: 100, isFixedWidth: true, horzAlignment: HorzAlignment.Center);

            gridView.ColumnSetup($"{nameof(AccountStatement.StaffName)}", caption: "Сортировка\nпо\nпервичнику", width: 225, isFixedWidth: true);
            gridView.ColumnSetup($"{nameof(AccountStatement.CustomerName)}", caption: "Клиент", width: 325, isFixedWidth: true);
            gridView.ColumnSetup($"{nameof(AccountStatement.AccountDescription)}", caption: "Описание", width: 300, isFixedWidth: true);
            gridView.ColumnSetup($"{nameof(AccountStatement.AccountScore)}", caption: "Счет", width: 275, isFixedWidth: true);
            gridView.ColumnSetup($"{nameof(AccountStatement.AccountCurrencyISO)}", caption: "Валюта", width: 100, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
            gridView.ColumnSetup($"{nameof(AccountStatement.AccountBank)}", caption: "Банк", width: 350, isFixedWidth: true);
            gridView.ColumnSetup($"{nameof(AccountStatement.Month)}", caption: "Месяц", width: 125, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
            gridView.ColumnSetup($"{nameof(AccountStatement.Year)}", caption: "Год", width: 100, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
            gridView.ColumnSetup($"{nameof(AccountStatement.Status)}", caption: "Статус", width: 200, isFixedWidth: true);
            gridView.ColumnSetup($"{nameof(AccountStatement.DateDischarge)}", caption: "Дата\nвыписки", width: 150, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
            gridView.ColumnSetup($"{nameof(AccountStatement.Value)}", caption: "Остаток\nпо\nвыписке", width: 150, isFixedWidth: true, horzAlignment: HorzAlignment.Far, formatString: "n2");

            gridView.ColumnSetup($"{nameof(AccountStatement.Description)}", caption: "Примечание", width: 350, isFixedWidth: true);

            gridView.ColumnDelete(nameof(AccountStatement.Customer));
            gridView.ColumnDelete(nameof(AccountStatement.Staff));
            gridView.ColumnDelete(nameof(AccountStatement.Account));
            gridView.ColumnDelete(nameof(AccountStatement.AccountStatementStatus));

            gridControl.GridControlSetup();
            gridView.GridViewSetup(isColumnAutoWidth: false, isShowFooter: false, isShowFilterPanelMode: false);
        }

        /// <summary>
        /// Открытие формы редактирования.
        /// </summary>
        /// <param name="obj">Операция для изменения.</param>
        private void OpenEditForm(object obj)
        {
            var form = new AccountStatementEdit(obj, _uof);
            form.FormClosed += OperationEditFormClosed;
            form.XtraFormShow();
        }

        private void OperationEditFormClosed(object sender, FormClosedEventArgs e)
        {
            if (sender is AccountStatementEdit objEdit)
            {
                if (objEdit.IsSave)
                {
                    var currentObj = objEdit.AccountStatement;
                    if (currentObj != null)
                    {
                        var obj = _listObj.FirstOrDefault(f => f.Oid == currentObj.Oid);
                        if (obj is null)
                        {
                            _listObj.Add(currentObj);
                            _accountStatement = currentObj;
                        }

                        gridView.RefreshData();
                        gridView.FocusedRowHandle = gridView.LocateByValue(nameof(XPObject.Oid), _accountStatement?.Oid);
                    }
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
                        if (gridView.GetRow(gridView.FocusedRowHandle) is AccountStatement obj)
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
            if (gridView.GetRow(gridView.FocusedRowHandle) is AccountStatement obj)
            {
                OpenEditForm(obj);
            }
        }

        private async void barBtnDel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridView.GetRow(gridView.FocusedRowHandle) is AccountStatement obj)
            {
                var text = $"Вы действительно хотите удалить выписку по счету: {obj}?";
                var caption = $"Удаление пакета документов [OID:{obj.Oid}]";

                if (XtraMessageBox.Show(text,
                                        caption,
                                        MessageBoxButtons.OKCancel,
                                        MessageBoxIcon.Question) == DialogResult.OK)
                {

                    using var uof = new UnitOfWork();
                    var currentObj = await uof.GetObjectByKeyAsync<AccountStatement>(obj.Oid);
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
            UpdateData(await AccountStatementController.GetAccountsStatementAsync(_uof));
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
            if (gridView.GetRow(gridView.FocusedRowHandle) is AccountStatement obj)
            {
                _accountStatement = obj;
                FocusedRowChangedEvent?.Invoke(obj, gridView.FocusedRowHandle);
            }
        }

        private void gridView_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (gridView.GetRow(e.RowHandle) is AccountStatement obj)
                {
                    if (obj.AccountStatementStatus != null)
                    {
                        var color = obj.AccountStatementStatus?.Color;
                        if (!string.IsNullOrWhiteSpace(color))
                        {
                            e.Appearance.BackColor = ColorTranslator.FromHtml(color);
                        }
                    }
                }
            }
        }

        private async void barBtnFillYear_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                var collection = await AccountStatementController.GetAccountsStatementAsync(_uof);
                var accountsStatements = collection.Where(w => w.DateDischarge != null && w.Year is null);
                var count = 0;
                foreach (var accountsStatement in accountsStatements)
                {
                    accountsStatement.Year = accountsStatement.DateDischarge.Value.Year;
                    accountsStatement.Save();
                    count++;
                }
                await _uof.CommitTransactionAsync();
                UpdateData(await AccountStatementController.GetAccountsStatementAsync(_uof));
                DevXtraMessageBox.ShowXtraMessageBox($"Успешно обработано счетов: {count}");
            }
            catch (Exception ex)
            {
                DevXtraMessageBox.ShowXtraMessageBox(ex?.ToString());
            }
        }

        private void barBtnReport_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new AccountStatementReport();
            form.ShowDialog();
        }
    }
}
