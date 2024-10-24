using DevExpress.Data;
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
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using PulsLibrary.Extensions.DevForm;
using RMS.Core.Controller.Print;
using RMS.Core.Controllers;
using RMS.Core.Controllers.Accounts;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Interface;
using RMS.Core.Model;
using RMS.Core.Model.Accounts;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Reports;
using RMS.Core.Model.Salary;
using RMS.Core.Model.Taxes;
using RMS.Setting.Model.GeneralSettings;
using RMS.UI.ColorsSettings;
using RMS.UI.Forms.Directories;
using RMS.UI.Forms.ReferenceBooks;
using RMS.UI.xUI.Accounts.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Forms
{
    public partial class ReportChangeForm : XtraForm
    {
        private AccountStatementControl _accountStatementControl;
        private AccountStatement _currentAccountStatement;

        private static string pathTempDirectory = "temp";
        private static string pathSaveLayoutToXmlMainGrid = $"{pathTempDirectory}\\{nameof(ReportChangeForm)}_{nameof(gridViewReports)}.xml";

        private Session _session;
        private UnitOfWork _uof = new UnitOfWork();

        private XPCollection<ReportChange> _reportChanges;
        private XPCollection<PreTax> _preTaxs;
        private XPCollection<CustomerSalaryAdvance> _salaryAdvance;
        private XPCollection<IndividualEntrepreneursTax> _individualEntrepreneursTax;
        private XPCollection<IndividualEntrepreneursTax> _individualEntrepreneursTaxPatent;

        private void UpdateObj<T>(XPCollection<T> xPCollection)
        {
            xPCollection?.Reload();
        }

        /// <summary>
        /// Настройка функционирования таблиц.
        /// </summary>
        private void FunctionalGridSetup()
        {
            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref gridViewReports);
            BVVGlobal.oFuncXpo.PressEnterGrid<ReportChange, ReportChangeEdit>(gridViewReports);

            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref gridViewCorrectiveReport);
            BVVGlobal.oFuncXpo.PressEnterGrid<CorrectiveReport, ReportChangeEdit>(gridViewCorrectiveReport);

            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref gridViewTax);
            BVVGlobal.oFuncXpo.PressEnterGrid<PreTax, PreTaxEdit>(gridViewTax, action: () => UpdateObj(_preTaxs));

            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref gridViewSalary);
            BVVGlobal.oFuncXpo.PressEnterGrid<CustomerSalaryAdvance, CustomerSalaryAdvanceEdit>(
                gridViewSalary,
                true,
                true,
                actionAdd: () => OpenForm<CustomerSalaryAdvanceEdit, CustomerSalaryAdvance>(_salaryAdvance),
                isUseMassEdit: isEditSalaryReportForm,
                isUseDelete: isDeleteSalaryReportForm);

            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref gridViewIndividual);
            BVVGlobal.oFuncXpo.PressEnterGrid<IndividualEntrepreneursTax, IndividualEntrepreneursTaxEdit>(
                gridViewIndividual,
                true,
                true,
                actionAdd: () => OpenForm<IndividualEntrepreneursTaxEdit, IndividualEntrepreneursTax>(_individualEntrepreneursTax, false),
                isUseMassEdit: isEditIndividualReportForm,
                actionMassEdit: () => MassChangeIndividualEntrepreneursTax(gridViewIndividual, false),
                isUseDelete: isDeleteIndividualReportForm);

            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref gridViewIndividualPatent);
            BVVGlobal.oFuncXpo.PressEnterGrid<IndividualEntrepreneursTax, IndividualEntrepreneursTaxEdit>(
                gridViewIndividualPatent,
                true,
                true,
                actionAdd: () => OpenForm<IndividualEntrepreneursTaxEdit, IndividualEntrepreneursTax>(_individualEntrepreneursTaxPatent, true),
                isUseMassEdit: isEditIndividualPatentReportForm,
                actionMassEdit: () => MassChangeIndividualEntrepreneursTax(gridViewIndividualPatent, true),
                isUseDelete: isDeleteIndividualPatentReportForm);
        }

        private void OpenForm<T1, T2>(XPCollection<T2> xpCollection, bool? isPatent = null) where T1 : XtraForm
        {
            var form = default(T1);

            if (isPatent is null)
            {
                form = (T1)Activator.CreateInstance(typeof(T1), _session);
            }
            else
            {
                form = (T1)Activator.CreateInstance(typeof(T1), _session, isPatent);
            }

            form.ShowDialog();
            xpCollection?.Reload();
        }

        private void MassChangeIndividualEntrepreneursTax(GridView gridView, bool isPatent)
        {
            if (gridView.IsEmpty)
            {
                return;
            }

            var list = new List<IndividualEntrepreneursTax>();

            foreach (var focusedRowHandle in gridView.GetSelectedRows())
            {
                var obj = gridView.GetRow(focusedRowHandle) as IndividualEntrepreneursTax;

                if (obj != null)
                {
                    list.Add(obj);
                }
            }

            if (list != null && list.Count > 0)
            {
                var form = new IndividualEntrepreneursTaxEdit(_session, list, isPatent);
                form.ShowDialog();
            }
        }

        public ReportChangeForm(Session session)
        {
            InitializeComponent();

            foreach (Month item in Enum.GetValues(typeof(Month)))
            {
                cmbMonth.Properties.Items.Add(item.GetEnumDescription());
            }

            foreach (StatusReport item in Enum.GetValues(typeof(StatusReport)))
            {
                if (item == StatusReport.NEEDSADJUSTMENTOURFAULT
                    || item == StatusReport.NEEDSADJUSTMENTCUSTOMERFAULT
                    || item == StatusReport.CORRECTION)
                {
                    continue;
                }
                cmbStatus.Properties.Items.Add(item.GetEnumDescription());
            }

            foreach (PeriodReportChange item in Enum.GetValues(typeof(PeriodReportChange)))
            {
                if (item != PeriodReportChange.MONTH &&
                    item != PeriodReportChange.FIRSTHALFYEAR &&
                    item != PeriodReportChange.SECONDHALFYEAR)
                {
                    cmbPeriod.Properties.Items.Add(item.GetEnumDescription());
                }
            }

            _session = session ?? DatabaseConnection.GetWorkSession();
        }

        private bool isEditReportChangeForm = false;
        private bool isDeleteReportChangeForm = false;
        private bool isPreTaxChange = false;
        private bool isDeletePreTax = false;
        private bool isEditSalaryReportForm = false;
        private bool isDeleteSalaryReportForm = false;
        private bool isEditIndividualReportForm = false;
        private bool isDeleteIndividualReportForm = false;
        private bool isEditIndividualPatentReportForm = false;
        private bool isDeleteIndividualPatentReportForm = false;
        private async System.Threading.Tasks.Task SetAccessRights()
        {
            try
            {
                using (var uof = new UnitOfWork())
                {
                    var user = await uof.GetObjectByKeyAsync<User>(DatabaseConnection.User?.Oid);
                    if (user != null)
                    {
                        var accessRights = user.AccessRights;
                        if (accessRights != null)
                        {
                            isEditReportChangeForm = accessRights.IsEditReportChangeForm;
                            isDeleteReportChangeForm = accessRights.IsDeleteReportChangeForm;

                            isPreTaxChange = accessRights.IsPreTaxChange;
                            isDeletePreTax = accessRights.IsDeletePreTax;

                            isEditSalaryReportForm = accessRights.IsEditSalaryReportForm;
                            isDeleteSalaryReportForm = accessRights.IsDeleteSalaryReportForm;

                            isEditIndividualReportForm = accessRights.IsEditIndividualReportForm;
                            isDeleteIndividualReportForm = accessRights.IsDeleteIndividualReportForm;

                            isEditIndividualPatentReportForm = accessRights.IsEditIndividualPatentReportForm;
                            isDeleteIndividualPatentReportForm = accessRights.IsDeleteIndividualPatentReportForm;
                        }
                    }
                }

                btnMassReportChangeEdit.Enabled = isEditReportChangeForm;
                btnDelReportChange.Enabled = isDeleteReportChangeForm;

                btnMassChangePreTax.Enabled = isPreTaxChange;
                barBtnDelPreTax.Enabled = isDeletePreTax;
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
            }
        }

        private void CreateAccountStatementControl()
        {
            _accountStatementControl = default(AccountStatementControl);
            var baseLayoutItem = layoutControlGroupAccountsStatements.Items.FirstOrDefault(f => f.Text.Equals(nameof(_accountStatementControl)));
            if (baseLayoutItem is null)
            {
                _accountStatementControl = new AccountStatementControl();
                _accountStatementControl.SetUnitOfWork(_uof);
                _accountStatementControl.FocusedRowChangedEvent += _accountStatementControl_FocusedRowChangedEvent;
                var item = layoutControlGroupAccountsStatements.AddItem(nameof(_accountStatementControl));
                item.Control = _accountStatementControl;
            }
            else
            {
                _accountStatementControl = (AccountStatementControl)((LayoutControlItem)baseLayoutItem).Control;
            }
        }

        private void _accountStatementControl_FocusedRowChangedEvent(AccountStatement obj, int focusedRowHandle)
        {
            _currentAccountStatement = obj;
        }

        private async void ReportChangeForm_Load(object sender, EventArgs e)
        {
            CreateAccountStatementControl();
            _accountStatementControl.UpdateData(await AccountStatementController.GetAccountsStatementAsync(_uof));

            var groupOperator = new GroupOperator(GroupOperatorType.Or);
            groupOperator.Operands.Add(new BinaryOperator(nameof(ReportChange.IsCorrective), false));
            groupOperator.Operands.Add(new NullOperator(nameof(ReportChange.IsCorrective)));

            _reportChanges = new XPCollection<ReportChange>(_session, groupOperator);
            _reportChanges.Criteria = await cls_BaseSpr.GetCustomerCriteria(_reportChanges.Criteria, nameof(ReportChange.Customer));

            _preTaxs = new XPCollection<PreTax>(_session);
            _preTaxs.Criteria = await cls_BaseSpr.GetCustomerCriteria(_preTaxs.Criteria, nameof(PreTax.Customer));

            _salaryAdvance = new XPCollection<CustomerSalaryAdvance>(_session);
            _salaryAdvance.Criteria = await cls_BaseSpr.GetCustomerCriteria(_salaryAdvance.Criteria, nameof(CustomerSalaryAdvance.Customer));

            _individualEntrepreneursTax = new XPCollection<IndividualEntrepreneursTax>(_session, new NullOperator(nameof(IndividualEntrepreneursTax.PatentObj)));
            _individualEntrepreneursTax.Criteria = await cls_BaseSpr.GetCustomerCriteria(_individualEntrepreneursTax.Criteria, nameof(IndividualEntrepreneursTax.Customer));
            _individualEntrepreneursTax.DisplayableProperties = IndividualEntrepreneursTax.GetDisplayableProperties();

            _individualEntrepreneursTaxPatent = new XPCollection<IndividualEntrepreneursTax>(_session, new NotOperator(new NullOperator(nameof(IndividualEntrepreneursTax.PatentObj))));
            _individualEntrepreneursTaxPatent.Criteria = await cls_BaseSpr.GetCustomerCriteria(_individualEntrepreneursTaxPatent.Criteria, nameof(IndividualEntrepreneursTax.Customer));
            _individualEntrepreneursTaxPatent.DisplayableProperties = IndividualEntrepreneursTax.GetDisplayableProperties(true);

            await SetAccessRights();
            FunctionalGridSetup();

            BVVGlobal.oFuncXpo.ButtoneEditDoubleClick<Staff>(_session, btnAccountantResponsible, cls_App.ReferenceBooks.Staff);
            BVVGlobal.oFuncXpo.ButtoneEditDoubleClick<Customer>(_session, btnCustomer, cls_App.ReferenceBooks.Customer);
            BVVGlobal.oFuncXpo.ButtoneEditDoubleClick<Report>(_session, btnReport, cls_App.ReferenceBooks.Report);

            txtYear.EditValue = DateTime.Now.Year;
            await StatusReportColor.GetStatusReportColor();

            SetGridControlReportChangeOptions();

            gridControlSalary.DataSource = _salaryAdvance;
            if (gridViewSalary.Columns[nameof(XPObject.Oid)] != null)
            {
                gridViewSalary.Columns[nameof(XPObject.Oid)].Visible = false;
                gridViewSalary.Columns[nameof(XPObject.Oid)].Width = 18;
                gridViewSalary.Columns[nameof(XPObject.Oid)].OptionsColumn.FixedWidth = true;
            }

            gridControlTax.DataSource = _preTaxs;
            if (gridViewTax.Columns[nameof(XPObject.Oid)] != null)
            {
                gridViewTax.Columns[nameof(XPObject.Oid)].Visible = false;
                gridViewTax.Columns[nameof(XPObject.Oid)].Width = 18;
                gridViewTax.Columns[nameof(XPObject.Oid)].OptionsColumn.FixedWidth = true;
            }
            if (gridViewTax.Columns[nameof(PreTax.OutputString)] != null)
            {
                gridViewTax.Columns[nameof(PreTax.OutputString)].ColumnEdit = new RepositoryItemRichTextEdit() { DocumentFormat = DevExpress.XtraRichEdit.DocumentFormat.Html };
                gridViewTax.Columns[nameof(PreTax.OutputString)].Width = 150;
                gridViewTax.Columns[nameof(PreTax.OutputString)].OptionsColumn.FixedWidth = true;
            }
            SetGridviewColumnsFormat(gridViewTax);

            gridControlIndividual.DataSource = _individualEntrepreneursTax;
            if (gridViewIndividual.Columns[nameof(XPObject.Oid)] != null)
            {
                gridViewIndividual.Columns[nameof(XPObject.Oid)].Visible = false;
                gridViewIndividual.Columns[nameof(XPObject.Oid)].Width = 18;
                gridViewIndividual.Columns[nameof(XPObject.Oid)].OptionsColumn.FixedWidth = true;
            }
            SetGridviewColumnsFormat(gridViewIndividual);

            gridControlIndividualPatent.DataSource = _individualEntrepreneursTaxPatent;
            if (gridViewIndividualPatent.Columns[nameof(XPObject.Oid)] != null)
            {
                gridViewIndividualPatent.Columns[nameof(XPObject.Oid)].Visible = false;
                gridViewIndividualPatent.Columns[nameof(XPObject.Oid)].Width = 18;
                gridViewIndividualPatent.Columns[nameof(XPObject.Oid)].OptionsColumn.FixedWidth = true;
            }
            SetGridviewColumnsFormat(gridViewIndividualPatent);

            if (System.IO.File.Exists(pathSaveLayoutToXmlMainGrid))
            {
                //gridViewReports.RestoreLayoutFromXml(pathSaveLayoutToXmlMainGrid);
            }

            GetInfoStatusReport();
            GetMessageInformationReport();
        }

        private void SetGridControlReportChangeOptions()
        {
            gridControlReportChanges.DataSource = _reportChanges;

            gridViewReports.OptionsView.ColumnAutoWidth = false;
            gridViewReports.OptionsView.ShowFooter = true;

            if (gridViewReports.Columns[nameof(ReportChange.Oid)] != null)
            {
                gridViewReports.Columns[nameof(ReportChange.Oid)].Visible = false;
                gridViewReports.Columns[nameof(ReportChange.Oid)].Width = 18;
                gridViewReports.Columns[nameof(ReportChange.Oid)].OptionsColumn.FixedWidth = true;
            }
            if (gridViewReports.Columns[nameof(ReportChange.DeliveryYear)] is GridColumn columnDeliveryYear)
            {
                columnDeliveryYear.Width = 100;
                columnDeliveryYear.OptionsColumn.FixedWidth = true;
                columnDeliveryYear.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            }
            if (gridViewReports.Columns[nameof(ReportChange.PeriodString)] is GridColumn columnPeriod)
            {
                columnPeriod.Width = 125;
                columnPeriod.OptionsColumn.FixedWidth = true;
                columnPeriod.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            }
            if (gridViewReports.Columns[nameof(ReportChange.LastDayDelivery)] is GridColumn columnLastDayDelivery)
            {
                columnLastDayDelivery.Width = 125;
                columnLastDayDelivery.OptionsColumn.FixedWidth = true;
                columnLastDayDelivery.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            }
            if (gridViewReports.Columns[nameof(ReportChange.StatusString)] is GridColumn columnStatusString)
            {
                var repositoryItemMemoEdit = gridControlReportChanges.RepositoryItems.Add(nameof(MemoEdit)) as RepositoryItemMemoEdit;
                repositoryItemMemoEdit.WordWrap = true;

                columnStatusString.ColumnEdit = repositoryItemMemoEdit;
                columnStatusString.Width = 175;
                columnStatusString.OptionsColumn.FixedWidth = true;
                columnStatusString.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                columnStatusString.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
            }
            if (gridViewReports.Columns[nameof(ReportChange.DateCompletion)] is GridColumn columnDateCompletion)
            {
                columnDateCompletion.Width = 125;
                columnDateCompletion.OptionsColumn.FixedWidth = true;
                columnDateCompletion.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            }
            if (gridViewReports.Columns[nameof(ReportChange.ReportString)] is GridColumn columnReportString)
            {
                columnReportString.Width = 150;
                columnReportString.OptionsColumn.FixedWidth = true;
                columnReportString.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            }
            if (gridViewReports.Columns[nameof(ReportChange.CustomerTaxSystem)] is GridColumn columnCustomerTaxSystem)
            {
                columnCustomerTaxSystem.Width = 225;
                columnCustomerTaxSystem.OptionsColumn.FixedWidth = true;
                columnCustomerTaxSystem.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            }
            if (gridViewReports.Columns[nameof(ReportChange.AccountantResponsibleString)] is GridColumn columnAccountantResponsibleString)
            {
                columnAccountantResponsibleString.Width = 200;
                columnAccountantResponsibleString.OptionsColumn.FixedWidth = true;
            }
            if (gridViewReports.Columns[nameof(ReportChange.PassedStaffString)] is GridColumn columnPassedStaffString)
            {
                columnPassedStaffString.Width = 200;
                columnPassedStaffString.OptionsColumn.FixedWidth = true;
            }
            if (gridViewReports.Columns[nameof(ReportChange.CustomerString)] is GridColumn columnCustomerString)
            {
                columnCustomerString.Width = 250;
                columnCustomerString.Summary.Clear();
                columnCustomerString.Summary.Add(SummaryItemType.Count, columnCustomerString.Name, "{0}");
            }
            if (gridViewReports.Columns[nameof(ReportChange.Comment)] is GridColumn columnComment)
            {
                var repositoryItemMemoEdit = gridControlReportChanges.RepositoryItems.Add(nameof(MemoEdit)) as RepositoryItemMemoEdit;
                repositoryItemMemoEdit.WordWrap = true;

                columnComment.ColumnEdit = repositoryItemMemoEdit;
                columnComment.Width = 300;
            }
            if (gridViewReports.Columns[nameof(ReportChange.CorrectiveReportsString)] is GridColumn columnCorrectiveReportsString)
            {
                var repositoryItemMemoEdit = gridControlReportChanges.RepositoryItems.Add(nameof(MemoEdit)) as RepositoryItemMemoEdit;
                repositoryItemMemoEdit.WordWrap = true;
                repositoryItemMemoEdit.AllowHtmlDraw = DefaultBoolean.True;

                columnCorrectiveReportsString.ColumnEdit = repositoryItemMemoEdit;
                columnCorrectiveReportsString.Width = 650;
            }
        }

        private void GetMessageInformationReport()
        {
            var oldReports = _reportChanges
                .Where(w => w.StatusReport == StatusReport.NEEDSADJUSTMENTOURFAULT
                || w.StatusReport == StatusReport.NEEDSADJUSTMENTCUSTOMERFAULT
                || w.StatusReport == StatusReport.CORRECTION);

            if (oldReports.Count() > 0)
            {
                var message = $"Имеются отчеты требующие вмешательства пользователя {Environment.NewLine}";

                foreach (var report in oldReports)
                {
                    var reportMsg = $"{Environment.NewLine}Клиент: {report.Customer}{Environment.NewLine}" +
                        $"Отчет: {report.Report}{Environment.NewLine}" +
                        $"Год: {report.DeliveryYear}{Environment.NewLine}" +
                        $"Период: {report.PeriodString}{Environment.NewLine}" +
                        $"Ответственный: {report.AccountantResponsibleString}{Environment.NewLine}";

                    message += reportMsg;
                }

                XtraMessageBox.Show(message, "Тревожные отчеты", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SetGridviewColumnsFormat(GridView gridView)
        {
            foreach (GridColumn column in gridView.Columns)
            {
                if (column.ColumnType == typeof(decimal))
                {
                    column.DisplayFormat.FormatType = FormatType.Numeric;
                    column.DisplayFormat.FormatString = "n";
                }
            }
        }

        private async void btnReportChangeForm_Click(object sender, EventArgs e)
        {
            var year = default(int?);

            if (!string.IsNullOrWhiteSpace(txtYear.Text) && int.TryParse(txtYear.Text, out int result))
            {
                if (txtYear.Text.Length < 4)
                {
                    txtYear.Focus();
                    XtraMessageBox.Show("Пожалуйста укажите корректное, четырехзначное значение года.",
                         "Ошибка формирования",
                         MessageBoxButtons.OK,
                         MessageBoxIcon.Information);
                    return;
                }
                if (result < 1980 || result > 2222)
                {
                    txtYear.Focus();
                    XtraMessageBox.Show("Пожалуйста укажите корректное, четырехзначное значение года в промежутке от [1980] до [2222]",
                         "Ошибка формирования",
                         MessageBoxButtons.OK,
                         MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    year = result;
                }
            }
            else
            {
                txtYear.Focus();
                XtraMessageBox.Show("Укажите год для формирования отчетности.",
                    "Ошибка формирования",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            var accountantResponsible = default(Staff);
            var customer = default(Customer);
            var report = default(Report);

            if (btnAccountantResponsible.EditValue is Staff)
            {
                accountantResponsible = btnAccountantResponsible.EditValue as Staff;
            }

            if (btnCustomer.EditValue is Customer)
            {
                customer = btnCustomer.EditValue as Customer;
            }

            if (btnReport.EditValue is Report)
            {
                report = btnReport.EditValue as Report;
            }

            if (checkIsPeriod.Checked)
            {
                if (!string.IsNullOrWhiteSpace(cmbPeriod.Text) && cmbPeriod.SelectedIndex != -1)
                {
                    var period = default(PeriodReportChange);

                    foreach (PeriodReportChange item in Enum.GetValues(typeof(PeriodReportChange)))
                    {
                        if (item.GetEnumDescription().Equals(cmbPeriod.Text))
                        {
                            period = item;
                            break;
                        }
                    }

                    if (tabbedControlGroupReportAndTax.SelectedTabPage == layoutControlGroupReport)
                    {
                        GetReportChangePeriod(period, Convert.ToInt32(year), accountantResponsible, customer, report);
                    }
                    else if (tabbedControlGroupReportAndTax.SelectedTabPage == layoutControlGroupTax)
                    {
                        if (period != PeriodReportChange.YEAR)
                        {
                            GetPreTaxChangePeriod(period, Convert.ToInt32(year), accountantResponsible, customer, report);
                        }
                        else
                        {
                            XtraMessageBox.Show("Предварительные налоги формируются только по квартальным периодам.", "Ошибка формирования", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cmbPeriod.Focus();
                        }
                    }
                    else if (tabbedControlGroupReportAndTax.SelectedTabPage == layoutControlGroupSalary)
                    {
                        XtraMessageBox.Show("ЗП / Аванс формируются только по месяцам.",
                                           "Ошибка формирования",
                                           MessageBoxButtons.OK,
                                           MessageBoxIcon.Information);
                        cmbMonth.Focus();
                    }
                    else if (tabbedControlGroupReportAndTax.SelectedTabPage == layoutControlGroupIndividual
                        || tabbedControlGroupReportAndTax.SelectedTabPage == layoutControlGroupIndividualPatent)
                    {
                        GetIndividualEntrepreneursTaxPeriod(period, Convert.ToInt32(year), accountantResponsible, customer);
                    }
                }
                else
                {
                    cmbPeriod.Focus();
                    XtraMessageBox.Show("Укажите период для формирования.", "Ошибка формирования", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            if (checkIsMonth.Checked)
            {
                if (!string.IsNullOrWhiteSpace(cmbMonth.Text) && cmbMonth.SelectedIndex != -1)
                {
                    var month = (Month)cmbMonth.SelectedIndex + 1;

                    if (tabbedControlGroupReportAndTax.SelectedTabPage == layoutControlGroupReport)
                    {
                        GetReportChangeMonth(month, Convert.ToInt32(year), accountantResponsible, customer, report);
                    }
                    else if (tabbedControlGroupReportAndTax.SelectedTabPage == layoutControlGroupTax)
                    {
                        XtraMessageBox.Show("Предварительные налоги формируются только по периоду.",
                                            "Ошибка формирования",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);
                        cmbPeriod.Focus();
                    }
                    else if (tabbedControlGroupReportAndTax.SelectedTabPage == layoutControlGroupSalary)
                    {
                        GetSalaryMonth(month, Convert.ToInt32(year), accountantResponsible, customer, report);
                    }
                    else if (tabbedControlGroupReportAndTax.SelectedTabPage == layoutControlGroupAccountsStatements)
                    {
                        await AccountStatementController.CreateAccountsStatementAsync(month, year);
                        _accountStatementControl.UpdateData(await AccountStatementController.GetAccountsStatementAsync(_uof));
                    }
                }
                else
                {
                    cmbMonth.Focus();
                    XtraMessageBox.Show("Укажите месяц для формирования.", "Ошибка формирования", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            GetInfoStatusReport();
        }

        private void GetIndividualEntrepreneursTaxPeriod(PeriodReportChange period, int year, Staff accountantResponsible, Customer customer)
        {
            if (XtraMessageBox.Show($"После нажатия будут сформированы Индивидуальные записи для ИП за {period.GetEnumDescription()} {year}",
                    "Формирование ИП",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.OK)
            {

                var groupOperatorCutomer = new GroupOperator(GroupOperatorType.And);

                var criteriaCustomerStatus = new BinaryOperator($"{nameof(Customer.CustomerStatus)}.{nameof(CustomerStatus.Status)}.{nameof(Status.IsFormationReport)}", true);
                groupOperatorCutomer.Operands.Add(criteriaCustomerStatus);

                if (accountantResponsible != null)
                {
                    var criteriaCustomerResponsible = new GroupOperator(GroupOperatorType.Or,
                        new BinaryOperator(nameof(Customer.AccountantResponsible), accountantResponsible),
                        new BinaryOperator(nameof(Customer.BankResponsible), accountantResponsible),
                        new BinaryOperator(nameof(Customer.PrimaryResponsible), accountantResponsible));

                    groupOperatorCutomer.Operands.Add(criteriaCustomerResponsible);
                }

                if (customer != null)
                {
                    var criteriaCustomer = new BinaryOperator(nameof(Customer.Oid), customer.Oid);
                    groupOperatorCutomer.Operands.Add(criteriaCustomer);
                }

                var collectionCustomer = new XPCollection<Customer>(_session, groupOperatorCutomer);

                if (collectionCustomer != null && collectionCustomer.Count > 0)
                {
                    foreach (var item in collectionCustomer)
                    {
                        item.FormCorporation?.Reload();
                        if (item.FormCorporation?.IsUseFormingIndividualEntrepreneursTax == true)
                        {
                            CreateIndividualEntrepreneursTax(item, "Фиксированные", year, period);
                            CreateIndividualEntrepreneursTax(item, "1% ПФР", year, period);

                            var individualEntrepreneursTax = CreateIndividualEntrepreneursTax(item, "УСН Аванс", year, period, 25, Guid.NewGuid().ToString());
                            if (individualEntrepreneursTax != null)
                            {
                                var status = customer?.CustomerStatus?.Status?.IsFormationReport;
                                var isUseFormingPreTax = customer?.FormCorporation?.IsUseFormingPreTax;

                                if (status is true && isUseFormingPreTax is true)
                                {
                                    var preTax = item.Session.FindObject<PreTax>(new BinaryOperator(nameof(PreTax.Guid), individualEntrepreneursTax.Guid));
                                    if (preTax is null)
                                    {
                                        var customerReport = item.CustomerReports.FirstOrDefault(f => f.Report != null && f.Report.Name.Equals("УСН"));
                                        if (customerReport != null)
                                        {
                                            preTax = CreatePreTaxChangePeriod(period, year, item, customerReport.Report, individualEntrepreneursTax.Guid);
                                        }
                                    }

                                    if (preTax != null)
                                    {
                                        preTax.Save();
                                    }
                                }
                            }

                            var patent = item.Tax?.Patent;
                            if (patent != null)
                            {
                                patent.Reload();
                                patent.PatentObjects?.Reload();

                                foreach (var patentObj in patent.GetPatentObjects(year))
                                {
                                    CreateIndividualEntrepreneursTax(item, patentObj, year, period);
                                }
                            }
                            _session.Save(_individualEntrepreneursTax);
                        }
                    }
                    _session.Save(_individualEntrepreneursTax);

                    _individualEntrepreneursTax?.Reload();
                    _individualEntrepreneursTaxPatent?.Reload();
                }
            }
        }

        private void GetPreTaxChangePeriod(PeriodReportChange period, int year, Staff accountantResponsible, Customer customer, Report report)
        {
            if (XtraMessageBox.Show($"После нажатия будут сформированы предварительные налоги за {period.GetEnumDescription()} {year}",
                    "Формирование предварительных налогов",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.OK)
            {

                var groupOperatorCutomer = new GroupOperator(GroupOperatorType.And);

                var criteriaCustomerStatus = new BinaryOperator($"{nameof(Customer.CustomerStatus)}.{nameof(CustomerStatus.Status)}.{nameof(Status.IsFormationReport)}", true);
                groupOperatorCutomer.Operands.Add(criteriaCustomerStatus);

                if (accountantResponsible != null)
                {
                    var criteriaCustomerResponsible = new GroupOperator(GroupOperatorType.Or,
                        new BinaryOperator(nameof(Customer.AccountantResponsible), accountantResponsible),
                        new BinaryOperator(nameof(Customer.BankResponsible), accountantResponsible),
                        new BinaryOperator(nameof(Customer.PrimaryResponsible), accountantResponsible));

                    groupOperatorCutomer.Operands.Add(criteriaCustomerResponsible);
                }

                if (customer != null)
                {
                    var criteriaCustomer = new BinaryOperator(nameof(Customer.Oid), customer.Oid);
                    groupOperatorCutomer.Operands.Add(criteriaCustomer);
                }

                var collectionCustomer = new XPCollection<Customer>(_session, groupOperatorCutomer);

                if (collectionCustomer != null && collectionCustomer.Count > 0)
                {
                    foreach (var item in collectionCustomer)
                    {
                        item.FormCorporation?.Reload();

                        if (item.FormCorporation is null || item.FormCorporation.IsUseFormingPreTax is false)
                        {
                            continue;
                        }

                        if (item.CustomerReports != null && item.CustomerReports.Count > 0)
                        {
                            //item.CustomerReports.Reload();
                            var customerReports = new List<CustomerReport>();

                            if (report == null)
                            {
                                customerReports = item.CustomerReports.ToList();
                            }
                            else
                            {
                                customerReports = item.CustomerReports.Where(w => w.Report == report).ToList();
                            }

                            var guid = Guid.NewGuid().ToString();

                            foreach (var customerReport in customerReports)
                            {
                                customerReport.Report?.Reload();

                                if (customerReport.Report != null && customerReport.Report.IsInputTax)
                                {
                                    var reportSchedules = customerReport.Report?.ReportSchedules?.Where(w => w.Period == period).ToList();

                                    foreach (var reportSchedule in reportSchedules)
                                    {
                                        var preTax = CreatePreTaxChangePeriod(period, year, item, customerReport.Report, guid);

                                        var status = customer?.CustomerStatus?.Status?.IsFormationReport;
                                        var isUseFormingIndividualEntrepreneursTax = customer?.FormCorporation?.IsUseFormingIndividualEntrepreneursTax;

                                        if (status is true && isUseFormingIndividualEntrepreneursTax is true && customerReport.Report.ToString().Equals("УСН"))
                                        {
                                            var individualEntrepreneursTax = item.Session.FindObject<IndividualEntrepreneursTax>(new BinaryOperator(nameof(IndividualEntrepreneursTax.Guid), preTax.Guid));
                                            if (individualEntrepreneursTax is null)
                                            {
                                                individualEntrepreneursTax = CreateIndividualEntrepreneursTax(item, "УСН Аванс", year, period, 25, preTax.Guid);
                                            }
                                            individualEntrepreneursTax.Save();
                                        }
                                    }

                                    if (reportSchedules.Count == 0 && customerReport.Report.IsReportQuarterly)
                                    {
                                        var preTax = CreatePreTaxChangePeriod(period, year, item, customerReport.Report, guid);

                                        var status = customer?.CustomerStatus?.Status?.IsFormationReport;
                                        var isUseFormingIndividualEntrepreneursTax = customer?.FormCorporation?.IsUseFormingIndividualEntrepreneursTax;

                                        if (status is true && isUseFormingIndividualEntrepreneursTax is true && customerReport.Report.ToString().Equals("УСН"))
                                        {
                                            var individualEntrepreneursTax = item.Session.FindObject<IndividualEntrepreneursTax>(new BinaryOperator(nameof(IndividualEntrepreneursTax.Guid), preTax.Guid));
                                            if (individualEntrepreneursTax is null)
                                            {
                                                individualEntrepreneursTax = CreateIndividualEntrepreneursTax(item, "УСН Аванс", year, period, 25, preTax.Guid);
                                            }
                                            individualEntrepreneursTax.Save();
                                        }
                                    }
                                }
                            }
                        }

                        _session.Save(_preTaxs);
                    }
                    _session.Save(_preTaxs);
                }
            }
        }

        private static bool IsUsingReportInRequiredPeriod(PeriodReportChange period, CustomerReport customerReport, bool isUse)
        {
            if (period == PeriodReportChange.MONTH && customerReport.Report.IsReportMonthly)
            {
                isUse = true;
            }
            else if (customerReport.Report.IsReportQuarterly
                && (period == PeriodReportChange.FIRSTHALFYEAR || period == PeriodReportChange.SECONDHALFYEAR))
            {
                isUse = true;
            }
            else if (customerReport.Report.IsReportQuarterly
               && (period == PeriodReportChange.FIRSTQUARTER
               || period == PeriodReportChange.SECONDQUARTER
               || period == PeriodReportChange.THIRDQUARTER
               || period == PeriodReportChange.FOURTHQUARTER
               ))
            {
                isUse = true;
            }
            else if (period == PeriodReportChange.YEAR && customerReport.Report.IsReportAnnual)
            {
                isUse = true;
            }

            return isUse;
        }

        private void GetSalaryMonth(Month month, int year, Staff accountantResponsible, Customer customer, Report report)
        {
            if (XtraMessageBox.Show($"После нажатия будут сформирован аванс и ЗП за {month.GetEnumDescription()} {year}",
                    "Формирование Аванса и ЗП",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.OK)
            {

                var groupOperatorCutomer = new GroupOperator(GroupOperatorType.And);

                var criteriaCustomerStatus = new BinaryOperator($"{nameof(Customer.CustomerStatus)}.{nameof(CustomerStatus.Status)}.{nameof(Status.IsFormationReport)}", true);
                groupOperatorCutomer.Operands.Add(criteriaCustomerStatus);

                if (accountantResponsible != null)
                {
                    var criteriaCustomerResponsible = new GroupOperator(GroupOperatorType.Or,
                        new BinaryOperator(nameof(Customer.AccountantResponsible), accountantResponsible),
                        new BinaryOperator(nameof(Customer.BankResponsible), accountantResponsible),
                        new BinaryOperator(nameof(Customer.PrimaryResponsible), accountantResponsible));

                    groupOperatorCutomer.Operands.Add(criteriaCustomerResponsible);
                }

                if (customer != null)
                {
                    var criteriaCustomer = new BinaryOperator(nameof(Customer.Oid), customer.Oid);
                    groupOperatorCutomer.Operands.Add(criteriaCustomer);
                }

                var collectionCustomer = new XPCollection<Customer>(_session, groupOperatorCutomer);

                if (collectionCustomer != null && collectionCustomer.Count > 0)
                {
                    foreach (var item in collectionCustomer.Where(w => w.SalaryDayObject != null))
                    {
                        var dateTime = item.SalaryDayObject.GetObjectDateTime(month, year);
                        if (dateTime != null)
                        {
                            CreateSalaryMonth(item, dateTime, TypeAccrual.Salary);
                        }
                        _session.Save(_salaryAdvance);
                    }

                    foreach (var item in collectionCustomer.Where(w => w.AdvanceDayObject != null))
                    {
                        var dateTime = item.AdvanceDayObject.GetObjectDateTime(month, year);
                        if (dateTime != null)
                        {
                            CreateSalaryMonth(item, dateTime, TypeAccrual.Advance);
                        }
                        _session.Save(_salaryAdvance);
                    }

                    _session.Save(_salaryAdvance);
                }
            }
        }

        /// <summary>
        /// Создание аванса или ЗП.
        /// </summary>
        /// <param name="customer"></param>
        private IndividualEntrepreneursTax CreateIndividualEntrepreneursTax(Customer customer,
                                                                            string name,
                                                                            int year,
                                                                            PeriodReportChange periodReportChange,
                                                                            int? day = null,
                                                                            string guid = default)
        {
            var individualEntrepreneursTax = default(IndividualEntrepreneursTax);

            if (day != null)
            {
                individualEntrepreneursTax = new IndividualEntrepreneursTax(_session, year, periodReportChange, name, Convert.ToInt32(day));
            }
            else
            {
                individualEntrepreneursTax = new IndividualEntrepreneursTax(_session, year, periodReportChange, name);
            }

            individualEntrepreneursTax.Guid = guid;
            individualEntrepreneursTax.Staff = customer.AccountantResponsible;
            individualEntrepreneursTax.Customer = customer;
            individualEntrepreneursTax.Name = name;

            var session = customer.Session;
            var individualEntrepreneursTaxStatus = session.FindObject<IndividualEntrepreneursTaxStatus>(new BinaryOperator(nameof(AccountingInsuranceStatus.IsDefault), true));
            individualEntrepreneursTax.IndividualEntrepreneursTaxStatus = individualEntrepreneursTaxStatus;

            using (var xpcollection = new XPCollection<IndividualEntrepreneursTax>(session))
            {
                var tmp = xpcollection.FirstOrDefault(f =>
                f.DateDelivery == individualEntrepreneursTax.DateDelivery
                && f.Customer == individualEntrepreneursTax.Customer
                && f.Year == individualEntrepreneursTax.Year
                && f.PeriodReportChange == individualEntrepreneursTax.PeriodReportChange
                && f.Name == individualEntrepreneursTax.Name);
                if (tmp is null)
                {
                    _individualEntrepreneursTax.Add(individualEntrepreneursTax);
                    individualEntrepreneursTax.Save();
                }
                else
                {
                    individualEntrepreneursTax = tmp;
                }
            }

            return individualEntrepreneursTax;
        }

        /// <summary>
        /// Создание аванса или ЗП по патенту.
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="dateTime"></param>
        /// <param name="typeAccrual"></param>
        private void CreateIndividualEntrepreneursTax(Customer customer, PatentObject patentObject, int year, PeriodReportChange periodReportChange)
        {
            var individualEntrepreneursTax = new IndividualEntrepreneursTax(_session, year, periodReportChange, patentObject)
            {
                Staff = customer.AccountantResponsible,
                Customer = customer,
                Name = patentObject.Name,
                PatentObj = patentObject
            };

            var session = customer.Session;

            using (var xpcollection = new XPCollection<IndividualEntrepreneursTax>(session))
            {
                if (xpcollection.FirstOrDefault(f =>
                f.DateDelivery == individualEntrepreneursTax.DateDelivery
                && f.Customer == individualEntrepreneursTax.Customer
                && f.Year == individualEntrepreneursTax.Year
                && f.PatentObj == patentObject
                && f.Name == individualEntrepreneursTax.Name) is null)
                {
                    _individualEntrepreneursTaxPatent.Add(individualEntrepreneursTax);
                    individualEntrepreneursTax.Save();
                }
            }
        }

        /// <summary>
        /// Создание аванса или ЗП.
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="dateTime"></param>
        /// <param name="typeAccrual"></param>
        private void CreateSalaryMonth(Customer customer, DateTime? dateTime, TypeAccrual typeAccrual)
        {
            if (dateTime is DateTime date)
            {
                var groupCriteriaReportChange = new GroupOperator(GroupOperatorType.And);

                var criteriaCustomer = new BinaryOperator(nameof(CustomerSalaryAdvance.Customer), customer);
                groupCriteriaReportChange.Operands.Add(criteriaCustomer);

                var criteriaReport = new BinaryOperator(nameof(CustomerSalaryAdvance.Staff), customer.AccountantResponsible);
                groupCriteriaReportChange.Operands.Add(criteriaReport);

                var criteriaYear = new BinaryOperator(nameof(CustomerSalaryAdvance.Date), date);
                groupCriteriaReportChange.Operands.Add(criteriaYear);

                var customerSalaryAdvance = _session.FindObject<CustomerSalaryAdvance>(groupCriteriaReportChange);

                if (customerSalaryAdvance is null)
                {
                    customerSalaryAdvance = new CustomerSalaryAdvance(_session)
                    {
                        Staff = customer.AccountantResponsible,
                        Customer = customer,
                        Date = date,
                        TypeAccrual = typeAccrual,
                        StatusAccrual = _session.FindObject<StatusAccrual>(new BinaryOperator(nameof(StatusAccrual.IsDefault), true))
                    };
                    _salaryAdvance.Add(customerSalaryAdvance);
                }
            }
        }

        void GetReportChangePeriod(PeriodReportChange period, int year, Staff accountantResponsible, Customer customer, Report report)
        {
            if (XtraMessageBox.Show($"После нажатия будет сформирован список отчетов за {period.GetEnumDescription()} {year}",
                    "Формирование отчетов",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.OK)
            {

                var groupOperatorCutomer = new GroupOperator(GroupOperatorType.And);

                var criteriaCustomerStatus = new BinaryOperator($"{nameof(Customer.CustomerStatus)}.{nameof(CustomerStatus.Status)}.{nameof(Status.IsFormationReport)}", true);
                groupOperatorCutomer.Operands.Add(criteriaCustomerStatus);

                if (accountantResponsible != null)
                {
                    var criteriaCustomerResponsible = new GroupOperator(GroupOperatorType.Or,
                        new BinaryOperator(nameof(Customer.AccountantResponsible), accountantResponsible),
                        new BinaryOperator(nameof(Customer.BankResponsible), accountantResponsible),
                        new BinaryOperator(nameof(Customer.PrimaryResponsible), accountantResponsible));

                    groupOperatorCutomer.Operands.Add(criteriaCustomerResponsible);
                }

                if (customer != null)
                {
                    var criteriaCustomer = new BinaryOperator(nameof(Customer.Oid), customer.Oid);
                    groupOperatorCutomer.Operands.Add(criteriaCustomer);
                }

                var collectionCustomer = new XPCollection<Customer>(_session, groupOperatorCutomer);

                if (collectionCustomer != null && collectionCustomer.Count > 0)
                {
                    foreach (var item in collectionCustomer)
                    {
                        if (item.CustomerReports != null && item.CustomerReports.Count > 0)
                        {
                            //item.CustomerReports.Reload();
                            var customerReports = new List<CustomerReport>();

                            if (report == null)
                            {
                                customerReports = item.CustomerReports.ToList();
                            }
                            else
                            {
                                customerReports = item.CustomerReports.Where(w => w.Report == report).ToList();
                            }

                            customerReports = customerReports
                                .Where(w => w.Report != null 
                                    && (w.Report.DateTo is null || w.Report.DateTo >= DateTime.Now.Date))
                                .ToList();

                            foreach (var customerReport in customerReports)
                            {
                                var reportSchedules = customerReport.Report?.ReportSchedules?.Where(w => w.Period == period).ToList();

                                foreach (var reportSchedule in reportSchedules)
                                {
                                    CreateReportChangePeriod(period, year, item, customerReport.Report, reportSchedule);
                                }
                            }
                        }

                        if (item.StatisticalReports != null && item.StatisticalReports.Count > 0)
                        {
                            //item.StatisticalReports.Reload();
                            var statisticalReports = new List<StatisticalReport>();

                            if (report == null)
                            {
                                statisticalReports = item.StatisticalReports?.Where(w => w.Year == year).ToList();
                            }
                            else
                            {
                                statisticalReports = item.StatisticalReports?.Where(w => w.Year == year && w.Report == report).ToList();
                            }

                            foreach (var statisticalReport in statisticalReports)
                            {
                                statisticalReport.Report?.ReportSchedules?.Reload();
                                var reportSchedules = statisticalReport.Report?.ReportSchedules?.Where(w => w.Period == period).ToList();

                                foreach (var reportSchedule in reportSchedules)
                                {
                                    CreateReportChangePeriod(period, year, item, statisticalReport.Report, reportSchedule);
                                }
                            }
                        }

                        //TODO: Не очень красиво, множественное сохранение коллекции...
                        _session.Save(_reportChanges);
                        if (item.Tax != null)
                        {
                            GetTaxReport(period, year, item, item.Tax.TaxAlcohol, "НД Алкоголь");
                            GetTaxReport(period, year, item, item.Tax.TaxAlcohol, "7 - Розн прод_алкоголя");
                            GetTaxReport(period, year, item, item.Tax.TaxAlcohol, "8- Розн прод пива");

                            GetTaxReport(period, year, item, item.Tax.TaxIncome, "НД Прибыль");
                            GetTaxReport(period, year, item, item.Tax.TaxIndirect, "Косвенные налоги");
                            GetTaxReport(period, year, item, item.Tax.TaxLand, "НД Земля");
                            GetTaxReport(period, year, item, item.Tax.TaxProperty, "НД Имущество");
                            GetTaxReport(period, year, item, item.Tax.TaxTransport, "НД Транспорт");
                            GetTaxReport(period, year, item, item.Tax.TaxSingleTemporaryIncome, "ЕНВД");

                            GetTaxReport(period, year, item, item.Tax.TaxImportEAEU, "Косвенные налоги");
                            GetTaxReport(period, year, item, item.Tax.TaxImportEAEU, "Статистика в таможню");
                            GetTaxReport(period, year, item, item.Tax.TaxImportEAEU, "Заявление о ввозе");
                        }

                        GetTaxReport(period, year, item, item.Leasing, "Лизинг");
                    }
                    _session.Save(_reportChanges);
                }
            }
        }

        /// <summary>
        /// Получение отчетов по конкретным налогам.
        /// </summary>
        private void GetTaxReport<T>(PeriodReportChange period, int year, Customer item, T data, string reportName) where T : ITax
        {
            if (data != null && data.IsUse)
            {
                var reportTax = new XPQuery<Report>(_session)
                    .Where(w => w.Name == reportName || w.FormIndex == reportName)
                    .FirstOrDefault();

                if (reportTax != null)
                {
                    var reportSchedules = reportTax.ReportSchedules?
                        .Where(w => w.Period == period && new DateTime(year, (int)w.Month, w.Day) >= data.Date).ToList();

                    foreach (var reportSchedule in reportSchedules)
                    {
                        CreateReportChangePeriod(period, year, item, reportTax, reportSchedule);
                    }
                }
            }
        }

        /// <summary>
        /// Получение отчетов по конкретным налогам.
        /// </summary>
        private void GetTaxReport<T>(Month month, int year, Customer item, T data, string reportName) where T : ITax
        {
            if (data != null && data.IsUse)
            {
                var reportTax = new XPQuery<Report>(_session)
                    .Where(w => w.Name == reportName || w.FormIndex == reportName)
                    .FirstOrDefault();

                if (reportTax != null)
                {
                    var reportSchedules = reportTax.ReportSchedules?
                        .Where(w => w.Month == month && new DateTime(year, (int)w.Month, w.Day) >= data.Date)
                        .ToList();

                    foreach (var reportSchedule in reportSchedules)
                    {
                        CreateReportChangeMonth(month, year, item, reportTax, reportSchedule);
                    }
                }
            }
        }

        /// <summary>
        /// Создание отчетности за период.
        /// </summary>
        /// <param name="period">Период отчетности.</param>
        /// <param name="year">Год.</param>
        /// <param name="customer">Клиент.</param>
        /// <param name="report">Отчет.</param>
        private PreTax CreatePreTaxChangePeriod(PeriodReportChange period, int year, Customer customer, Report report, string guid = default)
        {
            var groupCriteriaReportChange = new GroupOperator(GroupOperatorType.And);

            var criteriaCustomer = new BinaryOperator(nameof(PreTax.Customer), customer);
            groupCriteriaReportChange.Operands.Add(criteriaCustomer);

            var criteriaReport = new BinaryOperator(nameof(PreTax.Report), report);
            groupCriteriaReportChange.Operands.Add(criteriaReport);

            var criteriaYear = new BinaryOperator(nameof(PreTax.Year), year);
            groupCriteriaReportChange.Operands.Add(criteriaYear);

            var criteriaPeriod = new BinaryOperator(nameof(PreTax.PeriodReportChange), period);
            groupCriteriaReportChange.Operands.Add(criteriaPeriod);

            var preTax = _session.FindObject<PreTax>(groupCriteriaReportChange);
            var statusPreTax = _session.FindObject<StatusPreTax>(new BinaryOperator(nameof(StatusPreTax.IsDefault), true));
            if (preTax is null)
            {
                preTax = new PreTax(_session, year, period, report)
                {
                    Staff = customer.AccountantResponsible,
                    Customer = customer,
                    Report = report,
                    Year = year,
                    PeriodReportChange = period,
                    StatusPreTax = statusPreTax,
                    Guid = guid
                };

                if (_preTaxs.FirstOrDefault(f => f.Customer == customer
                    && f.Report == report
                    && f.Year == year
                    && f.PeriodReportChange == period) is null)
                {
                    _preTaxs.Add(preTax);
                }
            }

            return preTax;
        }

        /// <summary>
        /// Создание отчетности за период.
        /// </summary>
        /// <param name="period">Период отчетности.</param>
        /// <param name="year">Год.</param>
        /// <param name="customer">Клиент.</param>
        /// <param name="report">Отчет.</param>
        /// <param name="reportSchedule">График сдачи отчета.</param>
        private void CreateReportChangePeriod(PeriodReportChange period, int year, Customer customer, Report report, ReportSchedule reportSchedule)
        {
            if (reportSchedule != null)
            {
                var deliveryYear = year;
                if (period == PeriodReportChange.FOURTHQUARTER || period == PeriodReportChange.YEAR)
                {
                    year++;
                }

                var groupCriteriaReportChange = new GroupOperator(GroupOperatorType.And);

                var criteriaCustomer = new BinaryOperator(nameof(ReportChange.Customer), customer);
                groupCriteriaReportChange.Operands.Add(criteriaCustomer);

                var criteriaReport = new BinaryOperator(nameof(ReportChange.Report), report);
                groupCriteriaReportChange.Operands.Add(criteriaReport);

                var criteriaYear = new BinaryOperator(nameof(ReportChange.Year), year);
                groupCriteriaReportChange.Operands.Add(criteriaYear);

                var criteriaMonth = new BinaryOperator(nameof(ReportChange.Month), reportSchedule.Month);
                groupCriteriaReportChange.Operands.Add(criteriaMonth);

                var criteriaDay = new BinaryOperator(nameof(ReportChange.Day), reportSchedule.Day);
                groupCriteriaReportChange.Operands.Add(criteriaDay);

                var reportChange = _session.FindObject<ReportChange>(groupCriteriaReportChange);

                if (reportChange is null)
                {
                    var periodArchiveFolder = default(PeriodArchiveFolder);

                    if (period == PeriodReportChange.YEAR)
                    {
                        periodArchiveFolder = PeriodArchiveFolder.YEAR;
                    }
                    else if (period == PeriodReportChange.FIRSTHALFYEAR ||
                            period == PeriodReportChange.SECONDHALFYEAR)
                    {
                        periodArchiveFolder = PeriodArchiveFolder.HALFYEAR;
                    }
                    else if (period == PeriodReportChange.FIRSTQUARTER ||
                             period == PeriodReportChange.SECONDQUARTER ||
                             period == PeriodReportChange.THIRDQUARTER ||
                             period == PeriodReportChange.FOURTHQUARTER)
                    {
                        periodArchiveFolder = PeriodArchiveFolder.QUARTER;
                    }
                    else if (period == PeriodReportChange.MONTH)
                    {
                        periodArchiveFolder = PeriodArchiveFolder.MONTH;
                    }

                    reportChange = new ReportChange(_session)
                    {
                        AccountantResponsible = customer.AccountantResponsible,
                        Customer = customer,
                        Day = reportSchedule.Day,
                        Report = report,
                        Month = reportSchedule.Month,
                        Year = year,
                        PeriodReportChange = period,
                        PeriodArchiveFolder = periodArchiveFolder,
                        StatusReport = StatusReport.NEW,
                        UserCreate = _session.GetObjectByKey<User>(DatabaseConnection.User?.Oid),
                        DateCreate = DateTime.Now,
                        DeliveryYear = deliveryYear
                    };
                    _reportChanges.Add(reportChange);
                }
            }
        }

        private void GetReportChangeMonth(Month month, int year, Staff accountantResponsible, Customer customer, Report report)
        {
            if (XtraMessageBox.Show($"После нажатия будет сформирован список отчетов за {month.GetEnumDescription()} {year}",
                    "Формирование отчетов",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.OK)
            {

                var groupOperatorCutomer = new GroupOperator(GroupOperatorType.And);

                var criteriaCustomerStatus = new BinaryOperator($"{nameof(Customer.CustomerStatus)}.{nameof(CustomerStatus.Status)}.{nameof(Status.IsFormationReport)}", true);
                groupOperatorCutomer.Operands.Add(criteriaCustomerStatus);

                if (accountantResponsible != null)
                {
                    var criteriaCustomerResponsible = new GroupOperator(GroupOperatorType.Or,
                        new BinaryOperator(nameof(Customer.AccountantResponsible), accountantResponsible),
                        new BinaryOperator(nameof(Customer.BankResponsible), accountantResponsible),
                        new BinaryOperator(nameof(Customer.PrimaryResponsible), accountantResponsible));

                    groupOperatorCutomer.Operands.Add(criteriaCustomerResponsible);
                }

                if (customer != null)
                {
                    var criteriaCustomer = new BinaryOperator(nameof(Customer.Oid), customer.Oid);
                    groupOperatorCutomer.Operands.Add(criteriaCustomer);
                }

                var collectionCustomer = new XPCollection<Customer>(_session, groupOperatorCutomer);

                if (collectionCustomer != null && collectionCustomer.Count > 0)
                {
                    foreach (var item in collectionCustomer)
                    {
                        //item.CustomerReports.Reload();
                        if (item.CustomerReports != null && item.CustomerReports.Count > 0)
                        {
                            var customerReports = new List<CustomerReport>();

                            if (report == null)
                            {
                                customerReports = item.CustomerReports.ToList();
                            }
                            else
                            {
                                customerReports = item.CustomerReports.Where(w => w.Report == report).ToList();
                            }

                            foreach (var customerReport in customerReports)
                            {
                                var reportSchedules = customerReport.Report?.ReportSchedules?.Where(w => w.Month == month && w.Period == PeriodReportChange.MONTH).FirstOrDefault();
                                CreateReportChangeMonth(month, year, item, customerReport.Report, reportSchedules);
                            }
                        }

                        if (item.StatisticalReports != null && item.StatisticalReports.Count > 0)
                        {
                            //item.StatisticalReports.Reload();
                            var statisticalReports = new List<StatisticalReport>();

                            if (report == null)
                            {
                                statisticalReports = item.StatisticalReports?.Where(w => w.Year == year).ToList();
                            }
                            else
                            {
                                statisticalReports = item.StatisticalReports?.Where(w => w.Year == year && w.Report == report).ToList();
                            }

                            foreach (var statisticalReport in statisticalReports)
                            {
                                var reportSchedules = statisticalReport.Report?.ReportSchedules?.Where(w => w.Month == month && w.Period == PeriodReportChange.MONTH).FirstOrDefault();

                                CreateReportChangeMonth(month, year, item, statisticalReport.Report, reportSchedules);
                            }
                        }

                        //TODO: Не очень красиво, множественное сохранение коллекции...
                        _session.Save(_reportChanges);
                        if (item.Tax != null)
                        {
                            GetTaxReport(month, year, item, item.Tax.TaxImportEAEU, "Косвенные налоги");
                            GetTaxReport(month, year, item, item.Tax.TaxImportEAEU, "Статистика в таможню");
                            GetTaxReport(month, year, item, item.Tax.TaxImportEAEU, "Заявление о ввозе");
                        }
                    }
                    _session.Save(_reportChanges);
                }
            }
        }

        /// <summary>
        /// Создание отчетности за месяц.
        /// </summary>
        /// <param name="month">Месяц отчетности.</param>
        /// <param name="year">Год.</param>
        /// <param name="customer">Клиент.</param>
        /// <param name="report">Отчет.</param>
        /// <param name="reportSchedule">График сдачи отчета.</param>
        private void CreateReportChangeMonth(Month month, int year, Customer customer, Report report, ReportSchedule reportSchedule)
        {
            if (reportSchedule != null)
            {
                var deliveryYear = year;

                var tempMonth = month;

                if (tempMonth == Month.December)
                {
                    year++;
                    tempMonth = Month.January;
                }
                else if(report.IsReportSubmissionMonthInSameMonthAsCreation is true)
                {
                    tempMonth = (Month)((int)month);
                }
                else
                {
                    tempMonth = (Month)((int)month + 1);
                }

                var groupCriteriaReportChange = new GroupOperator(GroupOperatorType.And);

                var criteriaCustomer = new BinaryOperator(nameof(ReportChange.Customer), customer);
                groupCriteriaReportChange.Operands.Add(criteriaCustomer);

                var criteriaReport = new BinaryOperator(nameof(ReportChange.Report), report);
                groupCriteriaReportChange.Operands.Add(criteriaReport);

                var criteriaYear = new BinaryOperator(nameof(ReportChange.Year), year);
                groupCriteriaReportChange.Operands.Add(criteriaYear);

                var criteriaMonth = new BinaryOperator(nameof(ReportChange.Month), tempMonth);
                groupCriteriaReportChange.Operands.Add(criteriaMonth);

                var criteriaDay = new BinaryOperator(nameof(ReportChange.Day), reportSchedule.Day);
                groupCriteriaReportChange.Operands.Add(criteriaDay);

                var reportChange = _session.FindObject<ReportChange>(groupCriteriaReportChange);

                if (reportChange is null)
                {
                    var periodArchiveFolder = default(PeriodArchiveFolder);

                    if (reportSchedule.Period == PeriodReportChange.YEAR)
                    {
                        periodArchiveFolder = PeriodArchiveFolder.YEAR;
                    }
                    else if (reportSchedule.Period == PeriodReportChange.FIRSTHALFYEAR ||
                            reportSchedule.Period == PeriodReportChange.SECONDHALFYEAR)
                    {
                        periodArchiveFolder = PeriodArchiveFolder.HALFYEAR;
                    }
                    else if (reportSchedule.Period == PeriodReportChange.FIRSTQUARTER ||
                             reportSchedule.Period == PeriodReportChange.SECONDQUARTER ||
                             reportSchedule.Period == PeriodReportChange.THIRDQUARTER ||
                             reportSchedule.Period == PeriodReportChange.FOURTHQUARTER)
                    {
                        periodArchiveFolder = PeriodArchiveFolder.QUARTER;
                    }
                    else if (reportSchedule.Period == PeriodReportChange.MONTH)
                    {
                        periodArchiveFolder = PeriodArchiveFolder.MONTH;
                    }

                    reportChange = new ReportChange(_session)
                    {
                        AccountantResponsible = customer.AccountantResponsible,
                        Customer = customer,
                        Day = reportSchedule.Day,
                        Report = report,
                        Month = tempMonth,
                        Year = year,
                        PeriodReportChange = reportSchedule.Period,
                        PeriodArchiveFolder = periodArchiveFolder,
                        StatusReport = StatusReport.NEW,
                        UserCreate = _session.GetObjectByKey<User>(DatabaseConnection.User?.Oid),
                        DateCreate = DateTime.Now,
                        DeliveryYear = deliveryYear,
                        PeriodChangeMonth = month
                    };
                    _reportChanges.Add(reportChange);
                }
            }
        }

        private void gridViewReports_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (gridView.GetRow(e.RowHandle) is ReportChange reportChange)
                {
                    var statusReport = reportChange.StatusString;

                    if (reportChange.CorrectiveReports != null && reportChange.CorrectiveReports.Count > 0)
                    {
                        statusReport = reportChange.CorrectiveReports?.LastOrDefault()?.StatusString;
                    }

                    if (!string.IsNullOrWhiteSpace(statusReport))
                    {
                        if (statusReport.Equals(StatusReport.NEW.GetEnumDescription()))
                        {
                            e.Appearance.BackColor = StatusReportColor.ColorStatusReportNew;
                            //e.Appearance.BackColor2 = Color.White;
                        }
                        else if (statusReport.Equals(StatusReport.SENT.GetEnumDescription()))
                        {
                            e.Appearance.BackColor = StatusReportColor.ColorStatusReportSent;
                            //e.Appearance.BackColor2 = Color.White;
                        }
                        else if (statusReport.Equals(StatusReport.SURRENDERED.GetEnumDescription()))
                        {
                            e.Appearance.BackColor = StatusReportColor.ColorStatusReportSurrendered;
                            //e.Appearance.BackColor2 = Color.White;
                        }
                        else if (statusReport.Equals(StatusReport.NOTSURRENDERED.GetEnumDescription()))
                        {
                            e.Appearance.BackColor = StatusReportColor.ColorStatusReportNotSurrendered;
                            //e.Appearance.BackColor2 = Color.White;
                        }
                        else if (statusReport.Equals(StatusReport.PREPARED.GetEnumDescription()))
                        {
                            e.Appearance.BackColor = StatusReportColor.ColorStatusReportPrepared;
                            //e.Appearance.BackColor2 = Color.White;
                        }
                        else if (statusReport.Equals(StatusReport.NOTACCEPTED.GetEnumDescription()))
                        {
                            e.Appearance.BackColor = StatusReportColor.ColorStatusReportNotAccepted;
                            //e.Appearance.BackColor2 = Color.White;
                        }
                        else if (statusReport.Equals(StatusReport.DOESNOTGIVEUP.GetEnumDescription()))
                        {
                            e.Appearance.BackColor = StatusReportColor.ColorStatusReportDoesnotgiveup;
                            //e.Appearance.BackColor2 = Color.White;
                        }

                        else if (statusReport.Equals(StatusReport.ADJUSTMENTREQUIRED.GetEnumDescription()))
                        {
                            e.Appearance.BackColor = StatusReportColor.ColorStatusReportAdjustmentRequired;
                            //e.Appearance.BackColor2 = Color.White;
                        }
                        else if (statusReport.Equals(StatusReport.ADJUSTMENTISREADY.GetEnumDescription()))
                        {
                            e.Appearance.BackColor = StatusReportColor.ColorStatusReportAdjustmentIsReady;
                            //e.Appearance.BackColor2 = Color.White;
                        }
                        else if (statusReport.Equals(StatusReport.CORRECTIONSENT.GetEnumDescription()))
                        {
                            e.Appearance.BackColor = StatusReportColor.ColorStatusReportCorrectionSent;
                            //e.Appearance.BackColor2 = Color.White;
                        }
                        else if (statusReport.Equals(StatusReport.CORRECTIONSUBMITTED.GetEnumDescription()))
                        {
                            e.Appearance.BackColor = StatusReportColor.ColorStatusReportCorrectionSubmitted;
                            //e.Appearance.BackColor2 = Color.White;
                        }
                        else if (statusReport.Equals(StatusReport.RENTEDBYTHECLIENT.GetEnumDescription()))
                        {
                            e.Appearance.BackColor = StatusReportColor.ColorStatusReportRentedByTheClient;
                            //e.Appearance.BackColor2 = Color.White;
                        }
                    }
                }
            }
        }

        private void checkIsPeriod_CheckedChanged(object sender, EventArgs e)
        {
            if (checkIsPeriod.Checked)
            {
                checkIsMonth.Checked = false;
                cmbMonth.EditValue = null;
            }
            else
            {
                checkIsMonth.Checked = true;
            }
        }

        private void checkIsMonth_CheckedChanged(object sender, EventArgs e)
        {
            if (tabbedControlGroupReportAndTax.SelectedTabPage.Name.Equals(nameof(layoutControlGroupSalary)))
            {
                checkIsPeriod.Checked = false;
                checkIsMonth.Checked = true;
                return;
            }

            if (checkIsMonth.Checked)
            {
                checkIsPeriod.Checked = false;
                cmbPeriod.EditValue = null;
            }
            else
            {
                checkIsPeriod.Checked = true;
            }
        }

        private void btnCustomer_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<Customer>(_session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.Customer, 1, null, null, false, null, string.Empty, false, true);
        }

        private void btnReport_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (sender is ButtonEdit buttonEdit)
            {
                if (e.Button.Kind == ButtonPredefines.Delete)
                {
                    buttonEdit.EditValue = null;
                }
                else if (e.Button.Kind == ButtonPredefines.Ellipsis)
                {
                    cls_BaseSpr.ButtonEditButtonClickBase<Report>(_session, buttonEdit, (int)cls_App.ReferenceBooks.Report, 1, null, null, false, null, string.Empty, false, true);

                    if (buttonEdit.EditValue is Report report && !string.IsNullOrWhiteSpace(report.Name) && report.Name.ToLower().Contains("сзв"))
                    {
                        checkIsMonth.Checked = true;
                    }
                }
            }
        }

        private void btnAccountantResponsible_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<Staff>(_session, buttonEdit, (int)cls_App.ReferenceBooks.Staff, 1, new NullOperator(nameof(Staff.DateDismissal)), null, false, null, string.Empty, false, true);
        }

        private void cmb_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }
        }

        private BinaryOperator GetBinaryOperator(string str, object obj)
        {
            return new BinaryOperator(str, obj);
        }

        private async void Filter(object sender, EventArgs e)
        {
            try
            {
                if (txtYear.EditValue != null && !string.IsNullOrWhiteSpace(txtYear.Text) && txtYear.Text.Length != 4)
                {
                    return;
                }

                var settings = await DatabaseConnection.WorkSession.FindObjectAsync<Settings>(null);
                if (settings is null)
                {
                    settings = new Settings(DatabaseConnection.WorkSession) { IsUseDeliveryYearReport = false, IsUseYearReport = false };
                    settings.Save();
                }

                var groupOperatorReportChange = new GroupOperator(GroupOperatorType.And);
                var groupOperatorPreTax = new GroupOperator(GroupOperatorType.And);
                var groupOperatorSalary = new GroupOperator(GroupOperatorType.And);
                var groupOperatorIndividualEntrepreneursTax = new GroupOperator(GroupOperatorType.And);
                var groupOperatorAccountStatement = new GroupOperator(GroupOperatorType.And);

                if (btnCustomer.EditValue is Customer customer)
                {
                    groupOperatorReportChange.Operands.Add(GetBinaryOperator($"{nameof(ReportChange.Customer)}.{nameof(Customer.Oid)}", customer.Oid));
                    groupOperatorPreTax.Operands.Add(GetBinaryOperator($"{nameof(PreTax.Customer)}.{nameof(Customer.Oid)}", customer.Oid));
                    groupOperatorSalary.Operands.Add(GetBinaryOperator($"{nameof(CustomerSalaryAdvance.Customer)}.{nameof(Customer.Oid)}", customer.Oid));
                    groupOperatorIndividualEntrepreneursTax.Operands.Add(GetBinaryOperator($"{nameof(IndividualEntrepreneursTax.Customer)}.{nameof(Customer.Oid)}", customer.Oid));
                    groupOperatorAccountStatement.Operands.Add(GetBinaryOperator($"{nameof(AccountStatement.Customer)}.{nameof(Customer.Oid)}", customer.Oid));
                }

                if (btnReport.EditValue is Report report)
                {
                    groupOperatorReportChange.Operands.Add(GetBinaryOperator($"{nameof(ReportChange.Report)}.{nameof(Report.Oid)}", report.Oid));
                    groupOperatorPreTax.Operands.Add(GetBinaryOperator($"{nameof(PreTax.Report)}.{nameof(Report.Oid)}", report.Oid));
                }

                if (btnAccountantResponsible.EditValue is Staff accountantResponsible)
                {
                    groupOperatorReportChange.Operands.Add(GetBinaryOperator($"{nameof(ReportChange.AccountantResponsible)}.{nameof(Staff.Oid)}", accountantResponsible.Oid));
                    groupOperatorPreTax.Operands.Add(GetBinaryOperator($"{nameof(PreTax.Staff)}.{nameof(Staff.Oid)}", accountantResponsible.Oid));
                    groupOperatorSalary.Operands.Add(GetBinaryOperator($"{nameof(CustomerSalaryAdvance.Staff)}.{nameof(Staff.Oid)}", accountantResponsible.Oid));
                    groupOperatorIndividualEntrepreneursTax.Operands.Add(GetBinaryOperator($"{nameof(IndividualEntrepreneursTax.Staff)}.{nameof(Staff.Oid)}", accountantResponsible.Oid));
                    groupOperatorAccountStatement.Operands.Add(GetBinaryOperator($"{nameof(AccountStatement.Staff)}.{nameof(Staff.Oid)}", accountantResponsible.Oid));
                }

                if (cmbStatus.EditValue != null)
                {
                    foreach (StatusReport statusReport in Enum.GetValues(typeof(StatusReport)))
                    {
                        if (statusReport.GetEnumDescription().Equals(cmbStatus.Text))
                        {
                            var criteriaStatusReport = new BinaryOperator(nameof(ReportChange.StatusString), statusReport.GetEnumDescription());
                            groupOperatorReportChange.Operands.Add(criteriaStatusReport);
                            break;
                        }
                    }
                }

                if (cmbPeriod.EditValue != null)
                {
                    var groupOperatorPeriodReportChange = new GroupOperator(GroupOperatorType.Or);
                    var groupOperatorPeriodPreTax = new GroupOperator(GroupOperatorType.Or);

                    foreach (PeriodReportChange period in Enum.GetValues(typeof(PeriodReportChange)))
                    {
                        if (period.GetEnumDescription().Equals(cmbPeriod.Text))
                        {
                            groupOperatorPeriodReportChange.Operands.Add(GetBinaryOperator(nameof(ReportChange.PeriodReportChange), period));
                            groupOperatorPeriodPreTax.Operands.Add(GetBinaryOperator(nameof(PreTax.PeriodReportChange), period));

                            //TODO: надо ли делать так?
                            //groupOperatorIndividualEntrepreneursTax.Operands.Add(GetBinaryOperator(nameof(IndividualEntrepreneursTax.PeriodReportChange), period));

                            break;
                        }
                    }

                    foreach (PeriodArchiveFolder period in Enum.GetValues(typeof(PeriodArchiveFolder)))
                    {
                        if (period.GetEnumDescription().Equals(cmbPeriod.Text))
                        {
                            var criteriaPeriod = new BinaryOperator(nameof(ReportChange.PeriodArchiveFolder), period);
                            groupOperatorPeriodReportChange.Operands.Add(criteriaPeriod);
                            break;
                        }
                    }

                    if (groupOperatorPeriodReportChange.Operands.Count > 0)
                    {
                        groupOperatorReportChange.Operands.Add(groupOperatorPeriodReportChange);
                    }

                    if (groupOperatorPeriodPreTax.Operands.Count > 0)
                    {
                        groupOperatorPreTax.Operands.Add(groupOperatorPeriodPreTax);
                    }
                }

                if (cmbMonth.EditValue != null)
                {
                    foreach (Month month in Enum.GetValues(typeof(Month)))
                    {
                        if (month.GetEnumDescription().Equals(cmbMonth.Text))
                        {
                            var groupoperatorMonth = new GroupOperator(GroupOperatorType.Or);

                            //var criteriaMonth = new BinaryOperator(nameof(ReportChange.Month), month);
                            //groupoperatorMonth.Operands.Add(criteriaMonth);

                            var criteriaPeriodChangeMonth = new BinaryOperator(nameof(ReportChange.PeriodChangeMonth), month);
                            groupoperatorMonth.Operands.Add(criteriaPeriodChangeMonth);
                            groupOperatorReportChange.Operands.Add(groupoperatorMonth);

                            var criteriaMonthSalary = new BinaryOperator(nameof(CustomerSalaryAdvance.Month), (int)month);
                            groupOperatorSalary.Operands.Add(criteriaMonthSalary);

                            groupOperatorAccountStatement.Operands.Add(new BinaryOperator(nameof(AccountStatement.Month), (int)month));
                            break;
                        }
                    }

                    if (cmbPeriod.EditValue == null)
                    {
                        //var criteriaPeriod = new BinaryOperator(nameof(ReportChange.PeriodReportChange), PeriodReportChange.MONTH);
                        //groupOperator.Operands.Add(criteriaPeriod);

                        var criteriaPeriod = new BinaryOperator(nameof(ReportChange.PeriodArchiveFolder), PeriodArchiveFolder.MONTH);
                        groupOperatorReportChange.Operands.Add(criteriaPeriod);
                    }
                }

                if (txtYear.EditValue != null && !string.IsNullOrWhiteSpace(txtYear.Text) && txtYear.Text.Length >= 4)
                {
                    var groupOperatorReportChangeYear = new GroupOperator(GroupOperatorType.Or);

                    if (int.TryParse(txtYear.Text, out int year))
                    {
                        if (settings?.IsUseDeliveryYearReport is true)
                        {
                            groupOperatorReportChangeYear.Operands.Add(GetBinaryOperator(nameof(ReportChange.DeliveryYear), year));
                        }

                        if (settings?.IsUseYearReport is true)
                        {
                            groupOperatorReportChangeYear.Operands.Add(GetBinaryOperator(nameof(ReportChange.Year), year));
                        }

                        if (groupOperatorReportChangeYear.Operands.Count > 0)
                        {
                            groupOperatorReportChange.Operands.Add(groupOperatorReportChangeYear);
                        }

                        groupOperatorPreTax.Operands.Add(GetBinaryOperator(nameof(PreTax.Year), year));
                        groupOperatorIndividualEntrepreneursTax.Operands.Add(GetBinaryOperator(nameof(IndividualEntrepreneursTax.Year), year));

                        var criteriaYearSalary = new BinaryOperator(nameof(CustomerSalaryAdvance.Year), year);
                        groupOperatorSalary.Operands.Add(criteriaYearSalary);

                        groupOperatorAccountStatement.Operands.Add(new BinaryOperator(nameof(AccountStatement.Year), year));
                    }
                }

                if (_reportChanges != null)
                {
                    if (groupOperatorReportChange.Operands.Count == 0)
                    {
                        _reportChanges.Filter = null;
                    }
                    else
                    {
                        _reportChanges.Filter = groupOperatorReportChange;
                    }
                }

                if (_preTaxs != null)
                {
                    if (groupOperatorPreTax.Operands.Count == 0)
                    {
                        _preTaxs.Filter = null;
                    }
                    else
                    {
                        _preTaxs.Filter = groupOperatorPreTax;
                    }
                }

                if (_salaryAdvance != null)
                {
                    if (groupOperatorSalary.Operands.Count == 0)
                    {
                        _salaryAdvance.Filter = null;
                    }
                    else
                    {
                        _salaryAdvance.Filter = groupOperatorSalary;
                    }
                }

                if (_individualEntrepreneursTax != null)
                {
                    if (groupOperatorIndividualEntrepreneursTax.Operands.Count == 0)
                    {
                        _individualEntrepreneursTax.Filter = null;
                        _individualEntrepreneursTaxPatent.Filter = null;
                    }
                    else
                    {
                        _individualEntrepreneursTax.Filter = groupOperatorIndividualEntrepreneursTax;
                        _individualEntrepreneursTaxPatent.Filter = groupOperatorIndividualEntrepreneursTax;
                    }
                }

                _accountStatementControl?.SetFilter(groupOperatorAccountStatement);
                GetInfoStatusReport();
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
            }
        }

        /// <summary>
        /// Получение информации по статусу отчетов.
        /// </summary>
        private void GetInfoStatusReport()
        {
            GetLayoutControlGroupStatus(tabbedControlGroupReportAndTax.SelectedTabPage.Name);
        }

        private void btnAddReportChange_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new ReportChangeEdit(_session);
            form.ShowDialog();

            var reportChange = form.ReportChange;
            if (reportChange?.Oid != -1)
            {
                _reportChanges.Reload();
                gridViewReports.FocusedRowHandle = gridViewReports.LocateByValue(nameof(ReportChange.Oid), reportChange.Oid);
            }

            GetInfoStatusReport();
        }

        private void btnEditReportChange_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewReports.IsEmpty)
            {
                return;
            }

            var reportChange = gridViewReports.GetRow(gridViewReports.FocusedRowHandle) as ReportChange;
            if (reportChange != null)
            {
                var form = new ReportChangeEdit(reportChange);
                form.ShowDialog();
                GetInfoStatusReport();
                gridViewReports_FocusedRowChanged(null, null);
            }
        }

        private void btnDelReportChange_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewReports.IsEmpty)
            {
                return;
            }

            var listReportChange = new List<ReportChange>();
            var msg = default(string);

            foreach (var focusedRowHandle in gridViewReports.GetSelectedRows())
            {
                var reportChange = gridViewReports.GetRow(focusedRowHandle) as ReportChange;

                if (reportChange != null)
                {
                    msg += $"{reportChange}{Environment.NewLine}";
                    listReportChange.Add(reportChange);
                }
            }

            if (XtraMessageBox.Show($"Вы собираетесь удалить следующую отчетность:{Environment.NewLine}{msg}Хотите продолжить?",
                    "Удаление отчетности",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.OK)
            {

                _session.Delete(listReportChange);
                GetInfoStatusReport();
            }
        }

        private void btnRefreshReportChange_ItemClick(object sender, ItemClickEventArgs e)
        {
            _reportChanges.Reload();
        }

        private async void btnSettingReportChange_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new formProgramSettings(2);
            form.ShowDialog();

            await StatusReportColor.GetStatusReportColor();
        }

        private void barBtnSaveLayoutToXmlMainGrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!Directory.Exists(pathTempDirectory))
            {
                Directory.CreateDirectory(pathTempDirectory);
            }

            //gridViewReports.SaveLayoutToXml(pathSaveLayoutToXmlMainGrid);
        }

        private ReportChange _currentReportChange;
        private void gridViewReports_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            _currentReportChange = null;
            if (gridViewReports.GetRow(gridViewReports.FocusedRowHandle) is ReportChange _reportChange)
            {
                _reportChange?.Reload();
                _currentReportChange = _reportChange;

                if (_currentReportChange?.CorrectiveReports != null && _currentReportChange.CorrectiveReports.Count > 0)
                {
                    splitterItemCorrectiveReport.Visibility = LayoutVisibility.Always;
                    layoutControlItemgridControlCorrectiveReport.Visibility = LayoutVisibility.Always;
                    simpleLabelItemCorrectiveReport.Visibility = LayoutVisibility.Always;

                    gridControlCorrectiveReport.DataSource = _currentReportChange.CorrectiveReports;

                    if (gridViewCorrectiveReport.Columns[nameof(CorrectiveReport.Oid)] != null)
                    {
                        gridViewCorrectiveReport.Columns[nameof(CorrectiveReport.Oid)].Visible = false;
                        gridViewCorrectiveReport.Columns[nameof(CorrectiveReport.Oid)].Width = 18;
                        gridViewCorrectiveReport.Columns[nameof(CorrectiveReport.Oid)].OptionsColumn.FixedWidth = true;
                    }

                    if (gridViewCorrectiveReport.Columns[nameof(CorrectiveReport.ReportString)] is GridColumn columnReportString)
                    {
                        columnReportString.Width = 150;
                        columnReportString.OptionsColumn.FixedWidth = true;
                        columnReportString.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    }

                    if (gridViewCorrectiveReport.Columns[nameof(ReportChange.CustomerString)] is GridColumn columnCustomerString)
                    {
                        columnCustomerString.Width = 200;
                        columnCustomerString.OptionsColumn.FixedWidth = true;
                    }

                    if (gridViewCorrectiveReport.Columns[nameof(CorrectiveReport.PassedStaffString)] is GridColumn columnPassedStaffString)
                    {
                        columnPassedStaffString.Width = 200;
                        columnPassedStaffString.OptionsColumn.FixedWidth = true;
                    }

                    if (gridViewCorrectiveReport.Columns[nameof(CorrectiveReport.Fault)] is GridColumn columnFault)
                    {
                        columnFault.Width = 150;
                        columnFault.OptionsColumn.FixedWidth = true;
                    }

                    if (gridViewCorrectiveReport.Columns[nameof(CorrectiveReport.StatusString)] is GridColumn columnStatusString)
                    {
                        columnStatusString.Width = 200;
                        columnStatusString.OptionsColumn.FixedWidth = true;
                    }

                    if (gridViewCorrectiveReport.Columns[nameof(CorrectiveReport.PeriodString)] is GridColumn columnPeriodString)
                    {
                        columnPeriodString.Width = 175;
                        columnPeriodString.OptionsColumn.FixedWidth = true;
                    }

                    if (gridViewCorrectiveReport.Columns[nameof(CorrectiveReport.CorrectiveNumber)] is GridColumn columnCorrectiveNumber)
                    {
                        columnCorrectiveNumber.Width = 125;
                        columnCorrectiveNumber.OptionsColumn.FixedWidth = true;
                    }

                    if (gridViewCorrectiveReport.Columns[nameof(CorrectiveReport.Cause)] is GridColumn columnCause)
                    {
                        columnCause.Width = 250;
                        columnCause.OptionsColumn.FixedWidth = true;
                    }

                    if (gridViewCorrectiveReport.Columns[nameof(CorrectiveReport.Comment)] is GridColumn columnComment)
                    {
                        columnComment.Width = 300;
                        //columnComment.OptionsColumn.FixedWidth = true;
                    }
                }
                else
                {
                    gridControlCorrectiveReport.DataSource = null;
                    splitterItemCorrectiveReport.Visibility = LayoutVisibility.Never;
                    layoutControlItemgridControlCorrectiveReport.Visibility = LayoutVisibility.Never;
                    simpleLabelItemCorrectiveReport.Visibility = LayoutVisibility.Never;
                }
            }
            else
            {
                gridControlCorrectiveReport.DataSource = null;
                splitterItemCorrectiveReport.Visibility = LayoutVisibility.Never;
                layoutControlItemgridControlCorrectiveReport.Visibility = LayoutVisibility.Never;
                simpleLabelItemCorrectiveReport.Visibility = LayoutVisibility.Never;
            }
        }

        private void barBtnAddPreTax_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new PreTaxEdit();
            form.ShowDialog();

            var preTax = form.PreTax;
            if (preTax?.Oid != -1)
            {
                _preTaxs.Reload();
                gridViewTax.FocusedRowHandle = gridViewTax.LocateByValue(nameof(preTax.Oid), preTax.Oid);
            }

            GetInfoStatusReport();
        }

        private void barBtnEditPreTax_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewTax.IsEmpty)
            {
                return;
            }

            var preTax = gridViewTax.GetRow(gridViewTax.FocusedRowHandle) as PreTax;
            if (preTax != null)
            {
                var form = new PreTaxEdit(preTax);
                form.ShowDialog();
            }
        }

        private void barBtnDelPreTax_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewTax.IsEmpty)
            {
                return;
            }

            var listPreTax = new List<PreTax>();
            var msg = default(string);

            foreach (var focusedRowHandle in gridViewTax.GetSelectedRows())
            {
                var preTax = gridViewTax.GetRow(focusedRowHandle) as PreTax;

                if (preTax != null)
                {
                    msg += $"{preTax}{Environment.NewLine}";
                    listPreTax.Add(preTax);
                }
            }

            if (XtraMessageBox.Show($"Вы собираетесь удалить следующую отчетность:{Environment.NewLine}{msg}Хотите продолжить?",
                    "Удаление отчетности",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.OK)
            {

                _session.Delete(listPreTax);
            }
        }

        private void barBtnRefreshPreTax_ItemClick(object sender, ItemClickEventArgs e)
        {
            _preTaxs.Reload();
        }

        private void gridViewTax_MouseDown(object sender, MouseEventArgs e)
        {
            DXMouseEventArgs dxMouseEventArgs = e as DXMouseEventArgs;
            GridView gridview = sender as GridView;
            _ = gridview.CalcHitInfo(dxMouseEventArgs.Location);
            //if ((gridHitInfo.InRow || gridHitInfo.InRowCell) && dxMouseEventArgs.Button == MouseButtons.Right)
            if (dxMouseEventArgs.Button == MouseButtons.Right)
            {
                popupMenuPreTax.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
            }
        }

        private void btnMassChange_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewReports.IsEmpty)
            {
                return;
            }

            var listReportChange = new List<ReportChange>();

            foreach (var focusedRowHandle in gridViewReports.GetSelectedRows())
            {
                var reportChange = gridViewReports.GetRow(focusedRowHandle) as ReportChange;

                if (reportChange != null)
                {
                    listReportChange.Add(reportChange);
                }
            }

            if (listReportChange != null && listReportChange.Count > 0)
            {
                var form = new ReportChangeEdit(_session, listReportChange);
                form.ShowDialog();
            }
        }

        private void barBtnTaskAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            BVVGlobal.oFuncXpo.CreateTask<TaskEdit>(_session);
        }

        private void gridViewSalary_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void tabbedControlGroupReportAndTax_SelectedPageChanged(object sender, LayoutTabPageChangedEventArgs e)
        {
            cmbMonth.Enabled = true;
            cmbPeriod.Enabled = true;
            checkIsMonth.Enabled = true;
            checkIsPeriod.Enabled = true;

            cmbPeriod.Properties.Items.Clear();

            if (e.Page.Name.Equals(nameof(layoutControlGroupIndividual))
                || e.Page.Name.Equals(nameof(layoutControlGroupIndividualPatent))
                || e.Page.Name.Equals(nameof(layoutControlGroupTax)))
            {
                foreach (PeriodReportChange period in Enum.GetValues(typeof(PeriodReportChange)))
                {
                    if (period == PeriodReportChange.YEAR && period.GetEnumDescription().Equals(cmbPeriod.Text))
                    {
                        cmbPeriod.EditValue = null;
                        break;
                    }
                }

                foreach (PeriodReportChange item in Enum.GetValues(typeof(PeriodReportChange)))
                {
                    if (item != PeriodReportChange.MONTH &&
                        item != PeriodReportChange.YEAR &&
                        item != PeriodReportChange.FIRSTHALFYEAR &&
                        item != PeriodReportChange.SECONDHALFYEAR)
                    {
                        cmbPeriod.Properties.Items.Add(item.GetEnumDescription());
                    }
                }
            }
            else
            {
                if (e.Page.Name.Equals(nameof(layoutControlGroupSalary)))
                {
                    cmbPeriod.Enabled = false;
                    checkIsMonth.Checked = true;

                    checkIsPeriod.Enabled = false;
                }

                foreach (PeriodReportChange item in Enum.GetValues(typeof(PeriodReportChange)))
                {
                    if (item != PeriodReportChange.MONTH &&
                        item != PeriodReportChange.FIRSTHALFYEAR &&
                        item != PeriodReportChange.SECONDHALFYEAR)
                    {
                        cmbPeriod.Properties.Items.Add(item.GetEnumDescription());
                    }
                }
            }

            if (e.Page.Name.Equals(nameof(layoutControlGroupReport)))
            {
                GetMessageInformationReport();
            }

            GetLayoutControlGroupStatus(e.Page.Name);
        }

        private void GetLayoutControlGroupStatus(string pageName)
        {
            layoutControlItemStatus.Visibility = LayoutVisibility.Never;
            layoutControlItemFilterReport.Visibility = LayoutVisibility.Never;

            layoutControlGroupStatus.Clear(true);
            if (pageName.Equals(nameof(layoutControlGroupReport)))
            {
                var objs = new List<BaseLayoutItem>();

                foreach (StatusReport item in Enum.GetValues(typeof(StatusReport)))
                {
                    if (item == StatusReport.NEEDSADJUSTMENTOURFAULT
                    || item == StatusReport.NEEDSADJUSTMENTCUSTOMERFAULT
                    || item == StatusReport.CORRECTION
                    /* статусы для корректировок */
                    || item == StatusReport.ADJUSTMENTISREADY
                    || item == StatusReport.CORRECTIONSENT
                    || item == StatusReport.CORRECTIONSUBMITTED)
                    {
                        continue;
                    }

                    var textEdit = new TextEdit()
                    {
                        Name = $"txtStatus{item}",
                        EditValue = _reportChanges.Where(w => w.StatusString.Equals(item.GetEnumDescription())).Count().ToString(),
                        MinimumSize = new Size(75, 0),
                        MaximumSize = new Size(75, 0)
                    };

                    textEdit.Properties.Appearance.Options.UseTextOptions = true;
                    textEdit.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                    textEdit.Properties.AppearanceDisabled.Options.UseTextOptions = true;
                    textEdit.Properties.AppearanceDisabled.TextOptions.HAlignment = HorzAlignment.Center;
                    textEdit.Properties.AppearanceFocused.Options.UseTextOptions = true;
                    textEdit.Properties.AppearanceFocused.TextOptions.HAlignment = HorzAlignment.Center;
                    textEdit.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
                    textEdit.Properties.AppearanceReadOnly.TextOptions.HAlignment = HorzAlignment.Center;
                    textEdit.Properties.ReadOnly = true;

                    var layoutControlItem = new LayoutControlItem()
                    {
                        Name = $"layoutControlItemStatus{item}",
                        Text = $"{item.GetEnumDescription()}:",
                        Control = textEdit,
                        Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 2, 2)
                    };
                    objs.Add(layoutControlItem);
                }

                if (objs.Count > 0)
                {
                    layoutControlGroupStatus.Items.AddRange(objs.ToArray());
                }
                layoutControlItemStatus.Visibility = LayoutVisibility.Always;
                layoutControlItemFilterReport.Visibility = LayoutVisibility.Always;
            }
            else if (pageName.Equals(nameof(layoutControlGroupTax)))
            {
                CreateLayoutControlItem<StatusPreTax, PreTax>(_session, _preTaxs, nameof(PreTax.StatusString));
            }
            else if (pageName.Equals(nameof(layoutControlGroupSalary)))
            {
                CreateLayoutControlItem<StatusAccrual, CustomerSalaryAdvance>(_session, _salaryAdvance, nameof(CustomerSalaryAdvance.StatusAccrualString));
            }
            else if (pageName.Equals(nameof(layoutControlGroupIndividual)))
            {
                CreateLayoutControlItem<IndividualEntrepreneursTaxStatus, IndividualEntrepreneursTax>(_session, _individualEntrepreneursTax, nameof(IndividualEntrepreneursTax.StatusString));
            }
            else if (pageName.Equals(nameof(layoutControlGroupIndividualPatent)))
            {
                CreateLayoutControlItem<PatentStatus, IndividualEntrepreneursTax>(_session, _individualEntrepreneursTaxPatent, nameof(IndividualEntrepreneursTax.PatentObjStatusString));
            }
            else if (pageName.Equals(nameof(layoutControlGroupAccountsStatements)))
            {
                CreateLayoutControlItem<AccountStatementStatus, AccountStatement>(_session, _accountStatementControl.GetListObj(), nameof(AccountStatement.Status));
            }

            layoutControlGroupStatus.BestFit();
        }

        private async void CreateLayoutControlItem<T1, T2>(Session session, IEnumerable<T2> xpcollection, string typeName = default)
            where T1 : XPObject
            where T2 : XPObject
        {
            var collection = await new XPQuery<T1>(session)?.ToListAsync();
            if (string.IsNullOrWhiteSpace(typeName))
            {
                typeName = typeof(T1).Name;
            }

            var obj = new List<BaseLayoutItem>();
            foreach (var item in collection)
            {
                try
                {
                    var name = item.GetMemberValue("Name");

                    var textEdit = new TextEdit()
                    {
                        Name = $"txtStatus{item}",
                        MinimumSize = new Size(75, 0),
                        MaximumSize = new Size(75, 0)
                    };

                    textEdit.EditValue = xpcollection.Count(w => w.GetMemberValue(typeName) != null
                                                                 && w.GetMemberValue(typeName).Equals(item.ToString())).ToString();

                    textEdit.Properties.Appearance.Options.UseTextOptions = true;
                    textEdit.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                    textEdit.Properties.AppearanceDisabled.Options.UseTextOptions = true;
                    textEdit.Properties.AppearanceDisabled.TextOptions.HAlignment = HorzAlignment.Center;
                    textEdit.Properties.AppearanceFocused.Options.UseTextOptions = true;
                    textEdit.Properties.AppearanceFocused.TextOptions.HAlignment = HorzAlignment.Center;
                    textEdit.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
                    textEdit.Properties.AppearanceReadOnly.TextOptions.HAlignment = HorzAlignment.Center;
                    textEdit.Properties.ReadOnly = true;

                    var layoutControlItem = new LayoutControlItem()
                    {
                        Name = $"layoutControlItemStatus{item}",
                        Text = $"{name}:",
                        Control = textEdit,
                        Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 2, 2)
                    };
                    obj.Add(layoutControlItem);
                }
                catch (Exception ex)
                {
                    LoggerController.WriteLog(ex?.ToString());
                }
            }

            if (obj.Count > 0)
            {
                layoutControlGroupStatus.Items.AddRange(obj.ToArray());
            }
        }

        private void gridViewIndividual_RowStyle(object sender, RowStyleEventArgs e)
        {
            var gridView = sender as GridView;
            var obj = gridView.GetRow(e.RowHandle) as IndividualEntrepreneursTax;

            if (obj != null)
            {
                if (tabbedControlGroupReportAndTax.SelectedTabPage == layoutControlGroupIndividual)
                {
                    var color = obj.IndividualEntrepreneursTaxStatus?.Color;
                    if (!string.IsNullOrWhiteSpace(color))
                    {
                        e.Appearance.BackColor = ColorTranslator.FromHtml(color);
                    }
                }
                else if (tabbedControlGroupReportAndTax.SelectedTabPage == layoutControlGroupIndividualPatent)
                {
                    var color = obj.PatentObj?.PatentStatus?.Color;
                    if (!string.IsNullOrWhiteSpace(color))
                    {
                        e.Appearance.BackColor = ColorTranslator.FromHtml(color);
                    }
                }
            }
        }

        private void gridViewSalary_RowStyle(object sender, RowStyleEventArgs e)
        {
            var gridView = sender as GridView;
            var obj = gridView.GetRow(e.RowHandle) as CustomerSalaryAdvance;

            if (obj != null)
            {
                var color = obj.StatusAccrual?.Color;
                if (!string.IsNullOrWhiteSpace(color))
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(color);
                }
            }
        }

        private void gridViewTax_RowStyle(object sender, RowStyleEventArgs e)
        {
            var gridView = sender as GridView;
            var obj = gridView.GetRow(e.RowHandle) as PreTax;

            if (obj != null)
            {
                var color = obj.StatusPreTax?.Color;
                if (!string.IsNullOrWhiteSpace(color))
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(color);
                }
            }
        }

        private void gridViewTax_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            var gridView = sender as GridView;

            if (e.Column.FieldName.Equals($"{nameof(PreTax.Proceeds90)}"))
            {
                e.Appearance.BackColor = Color.FromArgb(150, Color.LightSkyBlue);
                //e.Appearance.BackColor2 = Color.FromArgb(150, Color.AliceBlue);
            }
            else if (e.Column.FieldName.Equals($"{nameof(PreTax.Proceeds51)}"))
            {
                e.Appearance.BackColor = Color.FromArgb(150, Color.LightSteelBlue);
                //e.Appearance.BackColor2 = Color.FromArgb(150, Color.CadetBlue);
            }
            else if (e.Column.FieldName.Equals($"{nameof(PreTax.PreliminaryAmount)}"))
            {
                e.Appearance.BackColor = Color.FromArgb(150, Color.LightCoral);
                //e.Appearance.BackColor2 = Color.FromArgb(150, Color.IndianRed);
            }
            else if (e.Column.FieldName.Equals($"{nameof(PreTax.AmountInDeclaration)}"))
            {
                e.Appearance.BackColor = Color.FromArgb(150, Color.LightGoldenrodYellow);
                //e.Appearance.BackColor2 = Color.FromArgb(150, Color.OrangeRed);
            }
        }

        private void btnMassChangePreTax_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewTax.IsEmpty)
            {
                return;
            }

            var listPreTax = new List<PreTax>();

            foreach (var focusedRowHandle in gridViewTax.GetSelectedRows())
            {
                var preTax = gridViewTax.GetRow(focusedRowHandle) as PreTax;

                if (preTax != null)
                {
                    listPreTax.Add(preTax);
                }
            }

            if (listPreTax != null && listPreTax.Count > 0)
            {
                var form = new PreTaxEdit(_session, listPreTax);
                form.ShowDialog();
            }
        }

        private void txtYear_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (buttonEdit != null)
            {
                if (e.Button.Kind == ButtonPredefines.Delete)
                {
                    buttonEdit.EditValue = null;
                }
            }
        }

        private async void barBtnInformationForClient_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridControlReportChanges.DataSource is XPCollection<ReportChange> data && data.Count > 0)
            {
                var information = new InformationForClientReport(data);
                information.ProgressEvent += Information_ProgressEvent;
                await information.PrintAsync();
                information.ProgressEvent -= Information_ProgressEvent;
            }
            else
            {
                XtraMessageBox.Show("Нет информации для печати",
                                    "Информация не найдена",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
            }
        }

        private void Information_ProgressEvent(object sender, string name, int count, int number)
        {
            try
            {
                var obj = sender as InformationForClientReport;
                var form = Program.MainForm;

                if (obj != null && form != null && form.IsDisposed is false)
                {
                    form.Progress_Start(0, count, $"Сбор информации по клиентским отчетам");
                    form.Progress_Position(number);

                    if (count == number)
                    {
                        form.Progress_Stop();
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
                Program.MainForm?.Progress_Stop();
            }
        }

        private void barBtnControlSystemAddReport_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewReports.IsEmpty)
            {
                return;
            }

            var obj = gridViewReports.GetRow(gridViewReports.FocusedRowHandle) as ReportChange;
            if (obj != null)
            {
                var form = new ControlSystemEdit(obj);
                form.ShowDialog();
            }
        }

        private void barBtnControlSystemAddPreTax_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewTax.IsEmpty)
            {
                return;
            }

            var obj = gridViewTax.GetRow(gridViewTax.FocusedRowHandle) as PreTax;
            if (obj != null)
            {
                var form = new ControlSystemEdit(obj);
                form.ShowDialog();
            }
        }

        private void barBtnCorrectiveReportAdd_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barBtnCorrectiveReportEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewCorrectiveReport.GetRow(gridViewCorrectiveReport.FocusedRowHandle) is CorrectiveReport obj && obj.ReportChange != null)
            {
                var form = new ReportChangeEdit(obj);
                form.ShowDialog();
            }
        }

        private void barBtnCorrectiveReportDel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewCorrectiveReport.GetRow(gridViewCorrectiveReport.FocusedRowHandle) is CorrectiveReport obj)
            {
                if (XtraMessageBox.Show(
                   $"Удалить корректирующий отчет - {obj}?",
                   "Операция с отчетом",
                   MessageBoxButtons.OKCancel,
                   MessageBoxIcon.Question) == DialogResult.OK)
                {
                    obj.Delete();
                }
            }
        }

        private void gridViewCorrectiveReport_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (e.MenuType == GridMenuType.User || e.MenuType == GridMenuType.Row)
                {
                    barBtnCorrectiveReportAdd.Enabled = false;

                    if (e.MenuType != GridMenuType.Row)
                    {
                        barBtnCorrectiveReportEdit.Enabled = false;
                        barBtnCorrectiveReportDel.Enabled = false;
                    }
                    else
                    {
                        barBtnCorrectiveReportEdit.Enabled = true;
                        barBtnCorrectiveReportDel.Enabled = true;
                    }

                    popupMenuCorrectiveReport.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }

        private void gridViewCorrectiveReport_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (gridView.GetRow(e.RowHandle) is CorrectiveReport correctiveReport && correctiveReport.ReportChange != null)
                {
                    var statusReport = correctiveReport.ReportChange.StatusString;
                    if (!string.IsNullOrWhiteSpace(statusReport))
                    {
                        if (statusReport.Equals(StatusReport.NEW.GetEnumDescription()))
                        {
                            e.Appearance.BackColor = StatusReportColor.ColorStatusReportNew;
                            //e.Appearance.BackColor2 = Color.White;
                        }
                        else if (statusReport.Equals(StatusReport.SENT.GetEnumDescription()))
                        {
                            e.Appearance.BackColor = StatusReportColor.ColorStatusReportSent;
                            //e.Appearance.BackColor2 = Color.White;
                        }
                        else if (statusReport.Equals(StatusReport.SURRENDERED.GetEnumDescription()))
                        {
                            e.Appearance.BackColor = StatusReportColor.ColorStatusReportSurrendered;
                            //e.Appearance.BackColor2 = Color.White;
                        }
                        else if (statusReport.Equals(StatusReport.NOTSURRENDERED.GetEnumDescription()))
                        {
                            e.Appearance.BackColor = StatusReportColor.ColorStatusReportNotSurrendered;
                            //e.Appearance.BackColor2 = Color.White;
                        }
                        else if (statusReport.Equals(StatusReport.PREPARED.GetEnumDescription()))
                        {
                            e.Appearance.BackColor = StatusReportColor.ColorStatusReportPrepared;
                            //e.Appearance.BackColor2 = Color.White;
                        }
                        else if (statusReport.Equals(StatusReport.NOTACCEPTED.GetEnumDescription()))
                        {
                            e.Appearance.BackColor = StatusReportColor.ColorStatusReportNotAccepted;
                            //e.Appearance.BackColor2 = Color.White;
                        }
                        else if (statusReport.Equals(StatusReport.DOESNOTGIVEUP.GetEnumDescription()))
                        {
                            e.Appearance.BackColor = StatusReportColor.ColorStatusReportDoesnotgiveup;
                            //e.Appearance.BackColor2 = Color.White;
                        }

                        else if (statusReport.Equals(StatusReport.ADJUSTMENTREQUIRED.GetEnumDescription()))
                        {
                            e.Appearance.BackColor = StatusReportColor.ColorStatusReportAdjustmentRequired;
                            //e.Appearance.BackColor2 = Color.White;
                        }
                        else if (statusReport.Equals(StatusReport.ADJUSTMENTISREADY.GetEnumDescription()))
                        {
                            e.Appearance.BackColor = StatusReportColor.ColorStatusReportAdjustmentIsReady;
                            //e.Appearance.BackColor2 = Color.White;
                        }
                        else if (statusReport.Equals(StatusReport.CORRECTIONSENT.GetEnumDescription()))
                        {
                            e.Appearance.BackColor = StatusReportColor.ColorStatusReportCorrectionSent;
                            //e.Appearance.BackColor2 = Color.White;
                        }
                        else if (statusReport.Equals(StatusReport.CORRECTIONSUBMITTED.GetEnumDescription()))
                        {
                            e.Appearance.BackColor = StatusReportColor.ColorStatusReportCorrectionSubmitted;
                            //e.Appearance.BackColor2 = Color.White;
                        }
                        else if (statusReport.Equals(StatusReport.RENTEDBYTHECLIENT.GetEnumDescription()))
                        {
                            e.Appearance.BackColor = StatusReportColor.ColorStatusReportRentedByTheClient;
                            //e.Appearance.BackColor2 = Color.White;
                        }
                    }
                }
            }
        }

        private void gridViewReports_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (e.MenuType == GridMenuType.User || e.MenuType == GridMenuType.Row)
                {
                    if (e.MenuType != GridMenuType.Row)
                    {
                        btnEditReportChange.Enabled = false;
                        btnDelReportChange.Enabled = false;
                    }
                    else
                    {
                        if (isDeleteReportChangeForm)
                        {
                            btnDelReportChange.Enabled = true;
                        }
                        btnEditReportChange.Enabled = true;
                    }

                    popupMenuReportChange.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }

        private void btnMassReportChangeAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new MassReportChangeEdit();
            form.XtraFormShow();
        }
    }
}