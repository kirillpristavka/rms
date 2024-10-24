using Core.Rent.Import.PulsPro.PulsLibrary.Models.Counterparty;
using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraRichEdit.Fields;
using PulsLibrary.Extensions.DevForm;
using PulsLibrary.Methods;
using RMS.Core.Controllers.Accounts;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model;
using RMS.Core.Model.Accounts;
using RMS.Core.Model.InfoCustomer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static RMS.UI.xUI.Accounts.Controllers.AccountStatementReport;

namespace RMS.UI.xUI.Accounts.Controllers
{
    public partial class AccountStatementReport : XtraForm
    {
        private AccountStatementReportAccountBase _accountBase;

        private DateTime _currentDateTime => DateTime.Now;
        private UnitOfWork _uof;

        public AccountStatementReport()
        {
            InitializeComponent();
            Icon = Properties.Resources.IconRMS;

            _uof = new UnitOfWork();
        }

        private void AccountStatementReport_Load(object sender, EventArgs e)
        {
            txtYear.EditValue = _currentDateTime.Year;
            foreach (Month item in Enum.GetValues(typeof(Month)))
            {
                cmbMonth.Properties.Items.Add(item.GetEnumDescription());
            }
            cmbMonth.SelectedIndex = _currentDateTime.Month - 1;
        }

        private async void btnGet_Click(object sender, EventArgs e)
        {
            var year = default(int?);
            if (!string.IsNullOrWhiteSpace(txtYear.Text))
            {
                year = Objects.GetIntObject(txtYear.EditValue);
                if (year == 0)
                {
                    year = default;
                }
            }

            var month = default(Month?);
            if (cmbMonth.EditValue != null)
            {
                month = cmbMonth.GetEnumItem<Month>();
            }

            await FillAccountStatementReportAccount(year, month);
        }


        public class LoadCalculationReport
        {
            public DateTime? Date { get; set; }
            public string Responsible { get; set; }
            public string Customer { get; set; }
            public decimal? Value { get; set; } = 0.00M;
            public string Period { get; set; }
            public decimal? RespValue { get; set; } = 0.00M;
            public string AdditionalName { get; set; }
            public decimal? AdditionalValue { get; set; } = 0.00M;
            public decimal? Prize { get; set; } = 0.00M;
            public decimal? Retention { get; set; } = 0.00M;
            public string Other { get; set; }
            public string Comment { get; set; }

            public LoadCalculationReport LoadCalculationReportBase { get; set; }    
    
        }


        public class LoadCalculation
        {
            public Month Month { get; private set; }
            public int? Year { get; private set; }

            public LoadCalculation(AccountStatement accountStatement)
            {
                Month = accountStatement.Month;

                Date = accountStatement.DateDischarge;
                Period = $"{accountStatement.Month.GetEnumDescription()} {accountStatement.Year}"?.Trim();
                CounterParty = accountStatement.Customer?.ToString()?.Trim();
                CounterPartyInn = accountStatement.Customer?.INN?.ToString()?.Trim();

                LoadCalculationStaffs = new List<LoadCalculationStaff>();
            }

            public string Period { get; private set; }
            public DateTime? Date { get; private set; }
            public string CounterParty { get; private set; }
            public string CounterPartyInn { get; private set; }


            public List<LoadCalculationStaff> LoadCalculationStaffs { get; set; }

            public void AddStaff(Staff staff, decimal? value = default)
            {
                if (staff != null)
                {
                    var loadCalculationStaff = new LoadCalculationStaff(staff);
                    loadCalculationStaff.SetValue(value);
                    loadCalculationStaff.SetLoadCalculation(this);
                    loadCalculationStaff.FillLoadCalculationStaffObjS();
                    LoadCalculationStaffs.Add(loadCalculationStaff);
                }
            }
        }

        public class LoadCalculationStaff 
        {
            private LoadCalculation _loadCalculation;

            public LoadCalculationStaff(Staff staff)
            {
                Staff = staff;
                LoadCalculationStaffObjS = new List<LoadCalculationStaffObj>();
            }

            public void SetValue(decimal? obj)
            {
                Value = obj ?? 0.00M;
            }

            public void SetLoadCalculation(LoadCalculation obj)
            {
                _loadCalculation = obj;
            }

            public Staff Staff { get; private set; }
            public decimal? Value { get; private set; } = 0.00M;
            public decimal? Prize { get; set; }
            public decimal? Retention { get; set; }
            public string Other { get; set; }
            public string Comment { get; set; }

            public async void FillLoadCalculationStaffObjS()
            {
                using (var uof = new UnitOfWork())
                {
                    if (Staff != null)
                    {
                        var dateSince = new DateTime(_loadCalculation.Year ?? DateTime.Now.Year, (int)_loadCalculation.Month, 1);
                        var dateTo = new DateTime(_loadCalculation.Year ?? DateTime.Now.Year, (int)_loadCalculation.Month, dateSince.AddMonths(1).AddDays(-1).Day);
                        var invoices = await new XPQuery<Invoice>(uof)
                            .Where(w => w.Customer != null
                                && w.Customer.INN == _loadCalculation.CounterPartyInn
                                && dateSince >= w.Date
                                && w.Date <= dateTo
                                && w.ApprovedStaff != null
                                && w.ApprovedStaff.Oid == Staff.Oid)
                            .ToListAsync();

                        foreach (var invoice in invoices)
                        {
                            foreach (var item in invoice.InvoiceInformations)
                            {
                                var name = item.Name;
                                var value = item.Sum;

                                LoadCalculationStaffObjS.Add(new LoadCalculationStaffObj(name, value));
                            }
                        }
                    }                    
                }
            }

            public List<LoadCalculationStaffObj> LoadCalculationStaffObjS { get; set; }
        }

        public class LoadCalculationStaffObj
        {
            public LoadCalculationStaffObj(string name, decimal? value)
            {
                Name = name;
                Value = value;
            }

            public string Name { get; private set; }
            public decimal? Value { get; private set; }

            public override string ToString()
            {
                return Name;
            }
        }

        private async void tabbedControlGroup_SelectedPageChanged(object sender, DevExpress.XtraLayout.LayoutTabPageChangedEventArgs e)
        {
            if (e.Page.Name != "layoutControlGroupLoadCalculation" && _accountBase is null)
            {
                return;
            }

            var year = default(int?);
            if (!string.IsNullOrWhiteSpace(txtYear.Text))
            {
                year = Objects.GetIntObject(txtYear.EditValue);
                if (year == 0)
                {
                    year = default;
                }
            }

            var month = default(Month?);
            if (cmbMonth.EditValue != null)
            {
                month = cmbMonth.GetEnumItem<Month>();
            }

            var result = new List<LoadCalculation>();
            var collection = await AccountStatementController.GetAccountsStatementAsync(_uof, month, year);
            
            foreach (var accountStatement in collection)
            {
                var customer = accountStatement.Customer;

                var accountResponsible = customer.AccountantResponsible;
                var bankResponsible = customer.BankResponsible;
                var primaryResponsible = customer.PrimaryResponsible;
                var salaryResponsible = customer.SalaryResponsible;

                var loadCalculation = new LoadCalculation(accountStatement);
                loadCalculation.AddStaff(accountResponsible, _accountBase.AccountStatementReportAccounts?.FirstOrDefault(f => f.Id == accountStatement.Oid && f.Responsible == accountResponsible?.ToString())?.Value1);
                loadCalculation.AddStaff(bankResponsible, _accountBase.AccountStatementReportAccounts?.FirstOrDefault(f => f.Id == accountStatement.Oid && f.Responsible == bankResponsible?.ToString())?.Value2);
                loadCalculation.AddStaff(primaryResponsible, _accountBase.AccountStatementReportAccounts?.FirstOrDefault(f => f.Id == accountStatement.Oid && f.Responsible == primaryResponsible?.ToString())?.Value3);
                loadCalculation.AddStaff(salaryResponsible, _accountBase.AccountStatementReportAccounts?.FirstOrDefault(f => f.Id == accountStatement.Oid && f.Responsible == salaryResponsible?.ToString())?.Value4);

                result.Add(loadCalculation);
            }

            var res = new List<LoadCalculationReport>();
            
            foreach (var report in result)
            {
                var l1 = new LoadCalculationReport()
                {
                    Customer = report.CounterParty,
                    Period = report.Period,
                    Date = report.Date,
                };
                res.Add(l1);

                foreach (var staff in report.LoadCalculationStaffs)
                {
                    var l2 = new LoadCalculationReport()
                    {
                        Responsible = staff.Staff?.ToString(),
                        RespValue = staff.Value,
                        LoadCalculationReportBase = l1
                    };
                    res.Add(l2);

                    foreach (var obj in staff.LoadCalculationStaffObjS)
                    {
                        res.Add(new LoadCalculationReport()
                        {
                            AdditionalName = obj.Name,
                            AdditionalValue = obj.Value ?? 0.00M,
                            LoadCalculationReportBase = l2
                        });
                    }
                }
            }


            gridControlLoadCalculation.DataSource = res;
        }


        public bool _firstFillGridControlAccount; 
        private async System.Threading.Tasks.Task FillAccountStatementReportAccount(int? year, Month? month)
        {
            var collection = await AccountStatementController.GetAccountsStatementAsync(_uof, month, year);

            _accountBase = new AccountStatementReportAccountBase();
            _accountBase.SetCollection(collection);
            gridControlAccount.DataSource = _accountBase.AccountStatementReportAccounts;            

            if (_firstFillGridControlAccount is true)
            {
                return;
            }

            gridViewAccount.ColumnSetup($"{nameof(AccountStatementReportAccount.Period)}", caption: "Период счета", isReadOnly: true, width: 135, isFixedWidth: true);
            gridViewAccount.ColumnSetup($"{nameof(AccountStatementReportAccount.Responsible)}", caption: "Ответственный", isReadOnly: true, width: 150, isFixedWidth: true);
            gridViewAccount.ColumnSetup($"{nameof(AccountStatementReportAccount.CounterParty)}", caption: "Контрагент", isReadOnly: true, width: 350, isFixedWidth: true);
            gridViewAccount.ColumnSetup($"{nameof(AccountStatementReportAccount.Value)}", caption: "Сумма", horzAlignment: HorzAlignment.Far, isGridGroupSum: true, width: 125, isFixedWidth: true, formatString: "n2");
            gridViewAccount.ColumnSetup($"{nameof(AccountStatementReportAccount.Level)}", caption: "Уровень компании", horzAlignment: HorzAlignment.Center, width: 85, isFixedWidth: true);

            gridViewAccount.ColumnSetup($"{nameof(AccountStatementReportAccount.Percent1)}", caption: "%\nглав\nбух", horzAlignment: HorzAlignment.Center, width: 75, isFixedWidth: true, formatString: "n2");
            gridViewAccount.ColumnSetup($"{nameof(AccountStatementReportAccount.Percent2)}", caption: "%\nза\nперв", horzAlignment: HorzAlignment.Center, width: 75, isFixedWidth: true, formatString: "n2");
            gridViewAccount.ColumnSetup($"{nameof(AccountStatementReportAccount.Percent3)}", caption: "%\nза\nЗП", horzAlignment: HorzAlignment.Center, width: 75, isFixedWidth: true, formatString: "n2");
            gridViewAccount.ColumnSetup($"{nameof(AccountStatementReportAccount.Percent4)}", caption: "%\nза\nбанк", horzAlignment: HorzAlignment.Center, width: 75, isFixedWidth: true, formatString: "n2");
            gridViewAccount.ColumnSetup($"{nameof(AccountStatementReportAccount.Percent5)}", caption: "%\nза\nадми", horzAlignment: HorzAlignment.Center, width: 75, isFixedWidth: true, formatString: "n2");

            gridViewAccount.ColumnSetup($"{nameof(AccountStatementReportAccount.Value1)}", caption: "Сумма\nглав\nбух", horzAlignment: HorzAlignment.Far, isGridGroupSum: true, isReadOnly: true, width: 125, isFixedWidth: true);
            gridViewAccount.ColumnSetup($"{nameof(AccountStatementReportAccount.Value2)}", caption: "Сумма\nза\nперв", horzAlignment: HorzAlignment.Far, isGridGroupSum: true, isReadOnly: true, width: 125, isFixedWidth: true);
            gridViewAccount.ColumnSetup($"{nameof(AccountStatementReportAccount.Value3)}", caption: "Сумма\nза\nЗП", horzAlignment: HorzAlignment.Far, isGridGroupSum: true, isReadOnly: true, width: 125, isFixedWidth: true);
            gridViewAccount.ColumnSetup($"{nameof(AccountStatementReportAccount.Value4)}", caption: "Сумма\nза\nбанк", horzAlignment: HorzAlignment.Far, isGridGroupSum: true, isReadOnly: true, width: 125, isFixedWidth: true);
            gridViewAccount.ColumnSetup($"{nameof(AccountStatementReportAccount.Value5)}", caption: "Сумма\nза\nадмин", horzAlignment: HorzAlignment.Far, isGridGroupSum: true, isReadOnly: true, width: 125, isFixedWidth: true);

            gridViewAccount.ColumnDelete($"{nameof(AccountStatementReportAccount.CounterPartyInn)}");
            gridViewAccount.ColumnDelete($"{nameof(AccountStatementReportAccount.Id)}");
            gridControlAccount.GridControlSetup();
            gridViewAccount.GridViewSetup(isEditTable: true, isColumnAutoWidth: false, isShowFooter: true, isShowFilterPanelMode: false);

            _firstFillGridControlAccount = true;
        }

        private void gridViewAccount_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            txtTotalAmountCharged.EditValue = _accountBase?.TotalAmountCharged;
            txtTotalPayoutPercentage.EditValue = _accountBase?.PayoutPercentage;
        }

        private void cmbMonth_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (sender is ComboBoxEdit comboBoxEdit)
            {
                if (e.Button.Kind == ButtonPredefines.Delete)
                {
                    comboBoxEdit.EditValue = null;
                }
            }
        }

        private void btnExcelLoad_Click(object sender, EventArgs e)
        {
            barBtnExcelLoad_ItemClick(null, null);
        }

        private void barBtnExcelLoad_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (_accountBase != null 
                && _accountBase?.AccountStatementReportAccounts != null 
                || _accountBase?.AccountStatementReportAccounts.Count > 0)
            {
                using (var ofd = new XtraOpenFileDialog() { Filter = "" })
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        FillSum(ofd.FileName);
                    }
                }

                gridViewAccount_CellValueChanged(null, null);
                gridViewAccount.RefreshData();
            }
            else
            {
                DevXtraMessageBox.ShowXtraMessageBox($"Для заполнения сумма из Excel необходимо сформировать таблицу.{Environment.NewLine}" +
                    $"Данные из Excel подтягиваются по ИНН клиента.{Environment.NewLine}" +
                    $"Столбец A (1) - ИНН{Environment.NewLine}" +
                    $"Столбец Б (2) - сумма", btnGet);
            }
        }

        private void FillSum(string path)
        {
            try
            {
                var dictionary = Core.Extensions.Extensions.GetValuesByExcel(path);

                foreach (var item in dictionary)
                {
                    var obj = _accountBase.AccountStatementReportAccounts.FirstOrDefault(f => f.CounterPartyInn == item.Key);
                    if (obj != null)
                    {
                        obj.Value = item.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                DevXtraMessageBox.ShowXtraMessageBox(ex.Message);
            }
        }

        private void gridViewAccount_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (e.MenuType == GridMenuType.User || e.MenuType == GridMenuType.Row)
                {
                    popupMenuAccount?.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }
    }
}