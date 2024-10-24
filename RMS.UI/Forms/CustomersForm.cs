using DevExpress.Data.Filtering;
using DevExpress.Office.Utils;
using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Helpers;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraTab;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using PulsLibrary.Extensions.DevForm;
using RMS.Core.Controller;
using RMS.Core.Controller.Print;
using RMS.Core.Controllers.Letters;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model;
using RMS.Core.Model.Chronicle;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Mail;
using RMS.Core.Model.OKVED;
using RMS.Core.Model.Reports;
using RMS.Core.Model.Salary;
using RMS.Core.Model.Taxes;
using RMS.Parser.Core.Models.WebsborGksRu.Controller;
using RMS.Setting.Model.ColorSettings;
using RMS.Setting.Model.CustomerSettings;
using RMS.Setting.Model.GeneralSettings;
using RMS.UI.Control;
using RMS.UI.Control.Customers;
using RMS.UI.Forms.Directories;
using RMS.UI.Forms.OnesEs;
using RMS.UI.Forms.Print;
using RMS.UI.Forms.ReferenceBooks;
using RMS.UI.Forms.Taxes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace RMS.UI.Forms
{
    public partial class CustomersForm : XtraForm
    {
        private Session _session;
        private XPCollection<Customer> _customers;
        private XPCollection<CustomerFilter> _customerFilters;

        private Customer _currentCustomer;

        private static class StatusReportColor
        {
            public static Color ColorStatusReportNew;
            public static Color ColorStatusReportSent;
            public static Color ColorStatusReportSurrendered;
            public static Color ColorStatusReportNotSurrendered;
            public static Color ColorStatusReportPrepared;
            public static Color ColorStatusReportNotAccepted;
            public static Color ColorStatusReportDoesnotgiveup;
            public static Color ColorStatusReportRentedByTheClient;

            public static Color ColorStatusReportAdjustmentRequired;
            public static Color ColorStatusReportAdjustmentIsReady;
            public static Color ColorStatusReportCorrectionSent;
            public static Color ColorStatusReportCorrectionSubmitted;
        }

        private async System.Threading.Tasks.Task GetStatusReportColor()
        {
            var colorStatus = await _session.FindObjectAsync<ColorStatus>(new BinaryOperator(nameof(ColorStatus.IsDefault), true));
            if (colorStatus != null)
            {
                StatusReportColor.ColorStatusReportNew = ColorTranslator.FromHtml(colorStatus.StatusReportNew);
                StatusReportColor.ColorStatusReportSent = ColorTranslator.FromHtml(colorStatus.StatusReportSent);
                StatusReportColor.ColorStatusReportSurrendered = ColorTranslator.FromHtml(colorStatus.StatusReportSurrendered);
                StatusReportColor.ColorStatusReportNotSurrendered = ColorTranslator.FromHtml(colorStatus.StatusReportNotSurrendered);
                StatusReportColor.ColorStatusReportPrepared = ColorTranslator.FromHtml(colorStatus.StatusReportPrepared);
                StatusReportColor.ColorStatusReportNotAccepted = ColorTranslator.FromHtml(colorStatus.StatusReportNotAccepted);
                StatusReportColor.ColorStatusReportDoesnotgiveup = ColorTranslator.FromHtml(colorStatus.StatusReportDoesnotgiveup);
                StatusReportColor.ColorStatusReportRentedByTheClient = ColorTranslator.FromHtml(colorStatus.StatusReportRentedByTheClient);

                StatusReportColor.ColorStatusReportAdjustmentRequired = ColorTranslator.FromHtml(colorStatus.StatusReportAdjustmentRequired);
                StatusReportColor.ColorStatusReportAdjustmentIsReady = ColorTranslator.FromHtml(colorStatus.StatusReportAdjustmentIsReady);
                StatusReportColor.ColorStatusReportCorrectionSent = ColorTranslator.FromHtml(colorStatus.StatusReportCorrectionSent);
                StatusReportColor.ColorStatusReportCorrectionSubmitted = ColorTranslator.FromHtml(colorStatus.StatusReportCorrectionSubmitted);
            }
            else
            {
                StatusReportColor.ColorStatusReportNew = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusReportNew", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(152, 251, 152))));
                StatusReportColor.ColorStatusReportSent = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusReportSent", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(255, 250, 205))));
                StatusReportColor.ColorStatusReportSurrendered = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusReportSurrendered", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(230, 230, 250))));
                StatusReportColor.ColorStatusReportNotSurrendered = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusReportNotSurrendered", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(250, 128, 114))));
                StatusReportColor.ColorStatusReportPrepared = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusReportPrepared", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(135, 206, 235))));
                StatusReportColor.ColorStatusReportNotAccepted = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusReportNotAccepted", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(255, 0, 0))));
                StatusReportColor.ColorStatusReportDoesnotgiveup = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusReportDoesnotgiveup", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(205, 197, 191))));
                StatusReportColor.ColorStatusReportRentedByTheClient = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusReportRentedByTheClient", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(155, 100, 228))));

                StatusReportColor.ColorStatusReportAdjustmentRequired = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusReportAdjustmentRequired", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(155, 207, 206))));
                StatusReportColor.ColorStatusReportAdjustmentIsReady = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusReportAdjustmentIsReady", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(105, 205, 165))));
                StatusReportColor.ColorStatusReportCorrectionSent = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusReportCorrectionSent", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(55, 189, 136))));
                StatusReportColor.ColorStatusReportCorrectionSubmitted = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusReportCorrectionSubmitted", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(5, 255, 255))));
            }
        }

        private static class StatusArchiveFolderColor
        {
            public static Color ColorStatusArchiveFolderNew;
            public static Color ColorStatusArchiveFolderIsCompleted;
            public static Color ColorStatusArchiveFolderSortout;
            public static Color ColorStatusArchiveFolderIssued;
            public static Color ColorStatusArchiveFolderDone;
            public static Color ColorStatusArchiveFolderReturned;

            public static Color ColorStatusArchiveFolderAddStatus;
            public static Color ColorStatusArchiveFolderIsSuedEDS;
            public static Color ColorStatusArchiveFolderReceivedEDS;
            public static Color ColorStatusArchiveFolderIsSuedTK;
            public static Color ColorStatusArchiveFolderReceivedTK;
        }

        private async System.Threading.Tasks.Task GetStatusArchiveFolderColor()
        {
            var colorStatus = await _session.FindObjectAsync<ColorStatus>(new BinaryOperator(nameof(ColorStatus.IsDefault), true));
            if (colorStatus != null)
            {
                StatusArchiveFolderColor.ColorStatusArchiveFolderNew = ColorTranslator.FromHtml(colorStatus.StatusArchiveFolderNew);
                StatusArchiveFolderColor.ColorStatusArchiveFolderIsCompleted = ColorTranslator.FromHtml(colorStatus.StatusArchiveFolderIsCompleted);
                StatusArchiveFolderColor.ColorStatusArchiveFolderSortout = ColorTranslator.FromHtml(colorStatus.StatusArchiveFolderSortout);
                StatusArchiveFolderColor.ColorStatusArchiveFolderIssued = ColorTranslator.FromHtml(colorStatus.StatusArchiveFolderIssued);
                StatusArchiveFolderColor.ColorStatusArchiveFolderDone = ColorTranslator.FromHtml(colorStatus.StatusArchiveFolderDone);
                StatusArchiveFolderColor.ColorStatusArchiveFolderReturned = ColorTranslator.FromHtml(colorStatus.StatusArchiveFolderReturned);

                StatusArchiveFolderColor.ColorStatusArchiveFolderIsSuedEDS = ColorTranslator.FromHtml(colorStatus.StatusArchiveFolderIsSuedEDS);
                StatusArchiveFolderColor.ColorStatusArchiveFolderReceivedEDS = ColorTranslator.FromHtml(colorStatus.StatusArchiveFolderReceivedEDS);
                StatusArchiveFolderColor.ColorStatusArchiveFolderIsSuedTK = ColorTranslator.FromHtml(colorStatus.StatusArchiveFolderIsSuedTK);
                StatusArchiveFolderColor.ColorStatusArchiveFolderReceivedTK = ColorTranslator.FromHtml(colorStatus.StatusArchiveFolderReceivedTK);

            }
            else
            {
                StatusArchiveFolderColor.ColorStatusArchiveFolderNew = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusArchiveFolderNew", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(152, 251, 152))));
                StatusArchiveFolderColor.ColorStatusArchiveFolderIsCompleted = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusArchiveFolderIsCompleted", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(255, 250, 205))));
                StatusArchiveFolderColor.ColorStatusArchiveFolderSortout = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusArchiveFolderSortout", workingField: 1, user: BVVGlobal.oApp.User));
                StatusArchiveFolderColor.ColorStatusArchiveFolderIssued = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusArchiveFolderIssued", workingField: 1, user: BVVGlobal.oApp.User));
                StatusArchiveFolderColor.ColorStatusArchiveFolderDone = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusArchiveFolderDone", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(192, 192, 192))));
                StatusArchiveFolderColor.ColorStatusArchiveFolderReturned = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusArchiveFolderReturned", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(192, 192, 192))));

                StatusArchiveFolderColor.ColorStatusArchiveFolderAddStatus = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusArchiveFolderAddStatus", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(192, 192, 192))));
                StatusArchiveFolderColor.ColorStatusArchiveFolderIsSuedEDS = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusArchiveFolderIsSuedEDS", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(192, 192, 192))));
                StatusArchiveFolderColor.ColorStatusArchiveFolderReceivedEDS = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusArchiveFolderReceivedEDS", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(192, 192, 192))));
                StatusArchiveFolderColor.ColorStatusArchiveFolderIsSuedTK = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusArchiveFolderIsSuedTK", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(192, 192, 192))));
                StatusArchiveFolderColor.ColorStatusArchiveFolderReceivedTK = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusArchiveFolderReceivedTK", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(192, 192, 192))));
            }
        }

        /// <summary>
        /// Настройка функционирования таблиц.
        /// </summary>
        private void FunctionalGridSetup()
        {
            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref gridViewCustomer);
            BVVGlobal.oFuncXpo.PressEnterGrid<Customer, CustomerEdit>(gridViewCustomer, isUseDelete: GetAccesEditCustomersForm());
            BVVGlobal.oFuncXpo.PressEnterGrid<BankAccess, BankAccessEdit>(gridViewBankAccess, true, isUseDelete: GetAccesEditCustomersForm());
            BVVGlobal.oFuncXpo.PressEnterGrid<Task, TaskEdit>(gridViewTasks, true, isUseDelete: GetAccesEditCustomersForm());
            BVVGlobal.oFuncXpo.PressEnterGrid<ForeignEconomicActivity, ForeignEconomicActivityEdit>(gridViewForeignEconomicActivitys, true, isUseDelete: GetAccesEditCustomersForm());
            BVVGlobal.oFuncXpo.PressEnterGrid<OrganizationPerformance, OrganizationPerformanceEdit>(gridViewOrganizationPerfomance, true, isUseDelete: GetAccesEditCustomersForm());
            BVVGlobal.oFuncXpo.PressEnterGrid<Invoice, InvoiceEdit>(gridViewInvoice, true, isUseDelete: GetAccesEditCustomersForm());
            BVVGlobal.oFuncXpo.PressEnterGrid<StatisticalReport, StatisticalReportEdit>(gridViewCustomerStatisticalReports, /*true,*/ isUseDelete: GetAccesEditCustomersForm());
            BVVGlobal.oFuncXpo.PressEnterGrid<CustomerReport, ReportEdit>(gridViewCustomerReports, true, isUseDelete: GetAccesEditCustomersForm());
            BVVGlobal.oFuncXpo.PressEnterGrid<Subdivision, SubdivisionEdit>(gridViewSubdivisions, true, isUseDelete: GetAccesEditCustomersForm());

            BVVGlobal.oFuncXpo.PressEnterGrid<ReportChange, ReportChangeEdit>(viewCustomerReports, true, isUseDelete: GetAccesEditCustomersForm(isDeleteReportChangeForm));
            BVVGlobal.oFuncXpo.PressEnterGrid<ArchiveFolderChange, ArchiveFolderChangeEdit>(viewCustomerArchiveFolders, true, isUseDelete: GetAccesEditCustomersForm());

            BVVGlobal.oFuncXpo.PressEnterGrid<EmploymentHistory, EmploymentHistoryEdit>(gridViewCustomerEmploymentHistorys, true, isUseDelete: GetAccesEditCustomersForm());
            //BVVGlobal.oFuncXpo.PressEnterGrid<Patent, PatentEdit>(gridViewPatent, true);

            BVVGlobal.oFuncXpo.SetClipboardGridView(ref gridViewCustomer);
        }

        public CustomersForm(Session session)
        {
            InitializeComponent();

            _session = session ?? BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
            _customers = new XPCollection<Customer>(_session);

            foreach (Availability item in Enum.GetValues(typeof(Availability)))
            {
                cmbIsPublic.Properties.Items.Add(item.GetEnumDescription());
                cmbIsSalary.Properties.Items.Add(item.GetEnumDescription());
                cmbIsPublicAndSalary.Properties.Items.Add(item.GetEnumDescription());
            }

            AdjustSimpleViewPadding();
            AdjustDraftViewPadding();

            richEditNotes.ActiveViewType = RichEditViewType.Simple;

            #if DEBUG
                accordElementCustomerItemInfoSalary.Visible = true;
                accordionControlElementTax.Visible = true;
                accordionControlElement3.Visible = true;
            #endif
        }

        private void LoadAsyncContractsMainGridControl()
        {
            gridViewCustomer.ShowLoadingPanel();
            _customers.LoadAsync(new AsyncLoadObjectsCallback(CallBackLoadMainGridControl));
        }

        private void CallBackLoadMainGridControl(ICollection[] result, Exception ex)
        {
            gridViewCustomer.HideLoadingPanel();
            gridViewCustomer.ShowFindPanel();

            if (ex != null)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private async void UpdatePropertiesXPCollection(bool update = false)
        {
            var customerSettings = await DatabaseConnection.LocalSession.FindObjectAsync<CustomerSettings>(null);

            if (customerSettings != null)
            {
                _customers.DisplayableProperties = customerSettings.ToString();
            }

            gridControlCustomer.DataSource = null;
            gridViewCustomer.Columns.Clear();

            LoadAsyncContractsMainGridControl();
            gridControlCustomer.DataSource = _customers;

            if (gridViewCustomer.Columns[nameof(Customer.Oid)] != null)
            {
                gridViewCustomer.Columns[nameof(Customer.Oid)].Visible = false;
                gridViewCustomer.Columns[nameof(Customer.Oid)].Width = 18;
                gridViewCustomer.Columns[nameof(Customer.Oid)].OptionsColumn.FixedWidth = true;
            }

            if (gridViewCustomer.Columns[nameof(Customer.StatusString)] != null)
            {
                RepositoryItemImageComboBox imgCmbStatus = gridControlCustomer.RepositoryItems.Add(nameof(ImageComboBoxEdit)) as RepositoryItemImageComboBox;

                var imageCollectionStatus = new ImageCollectionStatus();
                imgCmbStatus.SmallImages = imageCollectionStatus.imageCollection;

                var statusCollection = new XPCollection<Status>(_session);
                foreach (var item in statusCollection)
                {
                    if (item.IndexIcon != null)
                    {
                        imgCmbStatus.Items.Add(new ImageComboBoxItem() { Value = item.ToString(), ImageIndex = Convert.ToInt32(item.IndexIcon) });
                    }
                }

                imgCmbStatus.GlyphAlignment = HorzAlignment.Center;
                gridViewCustomer.Columns[nameof(Customer.StatusString)].ColumnEdit = imgCmbStatus;
                gridViewCustomer.Columns[nameof(Customer.StatusString)].Width = 18;
                gridViewCustomer.Columns[nameof(Customer.StatusString)].OptionsColumn.FixedWidth = true;
            }

            if (gridViewCustomer.Columns[nameof(Customer.StatusStatisticalReport)] != null)
            {
                RepositoryItemImageComboBox imgStatusStatisticalReport = gridControlCustomer.RepositoryItems.Add(nameof(ImageComboBoxEdit)) as RepositoryItemImageComboBox;
                imgStatusStatisticalReport.SmallImages = imgCustomer;
                imgStatusStatisticalReport.Items.Add(new ImageComboBoxItem() { Value = StatusStatisticalReport.INFORMATIONNOTUPDATED, ImageIndex = -1 });
                imgStatusStatisticalReport.Items.Add(new ImageComboBoxItem() { Value = StatusStatisticalReport.STATISTICALREPORTSAVAILABLE, ImageIndex = 3 });
                imgStatusStatisticalReport.Items.Add(new ImageComboBoxItem() { Value = StatusStatisticalReport.STATISTICALREPORTSNOTAVAILABLE, ImageIndex = 7 });

                imgStatusStatisticalReport.GlyphAlignment = HorzAlignment.Center;
                gridViewCustomer.Columns[nameof(Customer.StatusStatisticalReport)].ColumnEdit = imgStatusStatisticalReport;
                gridViewCustomer.Columns[nameof(Customer.StatusStatisticalReport)].Width = 18;
                gridViewCustomer.Columns[nameof(Customer.StatusStatisticalReport)].OptionsColumn.FixedWidth = true;
            }


            gridViewCustomer.ColumnSetup(nameof(Customer.AccountantResponsibleString), width: 175, isFixedWidth: true);
            gridViewCustomer.ColumnSetup(nameof(Customer.AccountantResponsible), width: 175, isFixedWidth: true);

            gridViewCustomer.ColumnSetup(nameof(Customer.BankResponsibleString), width: 175, isFixedWidth: true);
            gridViewCustomer.ColumnSetup(nameof(Customer.BankResponsible), width: 175, isFixedWidth: true);

            gridViewCustomer.ColumnSetup(nameof(Customer.PrimaryResponsibleString), width: 175, isFixedWidth: true);
            gridViewCustomer.ColumnSetup(nameof(Customer.PrimaryResponsible), width: 175, isFixedWidth: true);

            gridViewCustomer.ColumnSetup(nameof(Customer.SalaryResponsibleString), width: 175, isFixedWidth: true);
            gridViewCustomer.ColumnSetup(nameof(Customer.SalaryResponsible), width: 175, isFixedWidth: true);

            gridViewCustomer.ColumnSetup(nameof(Customer.ManagementPatronymic), width: 150, isFixedWidth: true);
            gridViewCustomer.ColumnSetup(nameof(Customer.ManagementName), width: 150, isFixedWidth: true);
            gridViewCustomer.ColumnSetup(nameof(Customer.ManagementSurname), width: 150, isFixedWidth: true);
            gridViewCustomer.ColumnSetup(nameof(Customer.ManagementFullString), width: 175, isFixedWidth: true);
            gridViewCustomer.ColumnSetup(nameof(Customer.ManagementString), width: 175, isFixedWidth: true);
            gridViewCustomer.ColumnSetup(nameof(Customer.ManagementPositionString), width: 200, isFixedWidth: true);

            gridViewCustomer.ColumnSetup(nameof(Customer.Name), isVisible: false, width: 275, isFixedWidth: true);
            gridViewCustomer.ColumnSetup(nameof(Customer.ProcessedName), width: 275, isFixedWidth: true);
            gridViewCustomer.ColumnSetup(nameof(Customer.DefaultName), isVisible: false, width: 275, isFixedWidth: true);

            gridViewCustomer.ColumnSetup(nameof(Customer.AbbreviatedName), width: 275, isFixedWidth: true);
            gridViewCustomer.ColumnSetup(nameof(Customer.FullName), width: 350, isFixedWidth: true);
            gridViewCustomer.ColumnSetup(nameof(Customer.INN), width: 125, isFixedWidth: true);
            gridViewCustomer.ColumnSetup(nameof(Customer.KPP), width: 125, isFixedWidth: true);
            gridViewCustomer.ColumnSetup(nameof(Customer.OKATO), width: 175, isFixedWidth: true);
            gridViewCustomer.ColumnSetup(nameof(Customer.OKTMO), width: 175, isFixedWidth: true);
            gridViewCustomer.ColumnSetup(nameof(Customer.OKPO), width: 125, isFixedWidth: true);
            gridViewCustomer.ColumnSetup(nameof(Customer.OKVED), width: 125, isFixedWidth: true);
            gridViewCustomer.ColumnSetup(nameof(Customer.PSRN), width: 150, isFixedWidth: true);
            gridViewCustomer.ColumnSetup(nameof(Customer.Email), width: 200, isFixedWidth: true);
            gridViewCustomer.ColumnSetup(nameof(Customer.Telephone), width: 175, isFixedWidth: true);

            gridViewCustomer.ColumnSetup(nameof(Customer.LegalAddress), width: 550, isFixedWidth: true);

            gridViewCustomer.ColumnSetup(nameof(Customer.ContractsString), width: 300, isFixedWidth: true);

            gridViewCustomer.ColumnSetup(nameof(Customer.TaxSystemCustomer), width: 150, isFixedWidth: true);
            gridViewCustomer.ColumnSetup(nameof(Customer.TaxSystemCustomerString), width: 150, isFixedWidth: true);

            gridViewCustomer.ColumnSetup(nameof(Customer.ElectronicReportingString), width: 175, isFixedWidth: true);
            gridViewCustomer.ColumnSetup(nameof(Customer.ElectronicReporting), width: 175, isFixedWidth: true);

            gridViewCustomer.ColumnSetup(nameof(Customer.FormCorporation), width: 100, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
            gridViewCustomer.ColumnSetup(nameof(Customer.FormCorporationString), width: 100, isFixedWidth: true, horzAlignment: HorzAlignment.Center);

            gridViewCustomer.ColumnSetup(nameof(Customer.OrganizationStatusString), width: 150, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
            gridViewCustomer.ColumnSetup(nameof(Customer.OrganizationStatus), width: 150, isFixedWidth: true, horzAlignment: HorzAlignment.Center);

            gridViewCustomer.ColumnSetup(nameof(Customer.DateActuality), width: 125, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
            gridViewCustomer.ColumnSetup(nameof(Customer.DateCreate), width: 125, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
            gridViewCustomer.ColumnSetup(nameof(Customer.DateLiquidation), width: 125, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
            gridViewCustomer.ColumnSetup(nameof(Customer.DateManagementBirth), width: 125, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
            gridViewCustomer.ColumnSetup(nameof(Customer.DatePSRN), width: 125, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
            gridViewCustomer.ColumnSetup(nameof(Customer.DateRegistration), width: 125, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
            gridViewCustomer.ColumnSetup(nameof(Customer.AccountantResponsibleDate), width: 125, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
            gridViewCustomer.ColumnSetup(nameof(Customer.ElectronicReportingStringDateSince), width: 125, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
            gridViewCustomer.ColumnSetup(nameof(Customer.ElectronicReportingStringDateTo), width: 125, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
            gridViewCustomer.ColumnSetup(nameof(Customer.PrimaryResponsibleDate), width: 125, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
            gridViewCustomer.ColumnSetup(nameof(Customer.SalaryResponsibleDate), width: 125, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
            gridViewCustomer.ColumnSetup(nameof(Customer.BankResponsibleDate), width: 125, isFixedWidth: true, horzAlignment: HorzAlignment.Center);

            gridViewCustomer.ColumnSetup(nameof(Customer.AccountsString), width: 250, isFixedWidth: true);

            gridViewCustomer.ColumnSetup(nameof(Customer.TaxString), width: 300, isFixedWidth: true);
            gridViewCustomer.ColumnSetup(nameof(Customer.Tax), width: 300, isFixedWidth: true);

            if (update)
            {
                barBtnSaveLayoutToXmlMainGrid_ItemClick(null, null);
            }

            gridViewCustomer.ColumnSetup(nameof(Customer.DefaultName), isVisible: false, width: 275, isFixedWidth: true);
            gridViewCustomer.ColumnSetup(nameof(Customer.Name), isVisible: false, width: 275, isFixedWidth: true);

            if (gridViewCustomer.Columns[nameof(Customer.DateManagementBirth)] != null)
            {
                _customers.Sorting = new SortingCollection(new SortProperty(nameof(Customer.DateManagementBirth), SortingDirection.Descending));
                gridViewCustomer.Columns[nameof(Customer.DateManagementBirth)].SortMode = ColumnSortMode.Custom;
            }
            else
            {
                if (gridViewCustomer.Columns[nameof(Customer.DefaultName)] is GridColumn gridColumnDefaultName)
                {
                    _customers.Sorting = new SortingCollection(new SortProperty(nameof(Customer.DefaultName), SortingDirection.Descending));
                    gridColumnDefaultName.SortMode = ColumnSortMode.Custom;
                }
            }
        }

        private async System.Threading.Tasks.Task TreeListUpdate(object obj = null)
        {
            _customerFilters = new XPCollection<CustomerFilter>(_session);
            var sessionUser = _session.GetObjectByKey<User>(DatabaseConnection.User.Oid);
            sessionUser?.UserGroups?.Reload();

            treeListCustomerFilter.Columns.Clear();
            treeListCustomerFilter.Columns.Add();
            treeListCustomerFilter.Columns.Add();

            treeListCustomerFilter.Columns[0].Visible = true;
            treeListCustomerFilter.Columns[0].Width = 100;

            treeListCustomerFilter.Columns[1].Visible = true;
            treeListCustomerFilter.Columns[1].Width = 50;
            treeListCustomerFilter.Columns[1].OptionsColumn.FixedWidth = true;

            treeListCustomerFilter.OptionsView.AutoWidth = true;
            treeListCustomerFilter.ClearNodes();

            var count = default(int);

            var settings = await _session.FindObjectAsync<Settings>(null);
            if (sessionUser?.UserGroups?.Select(s => s.UserGroup)?.FirstOrDefault(f => f.Oid == settings?.UserGroupEverything?.Oid) != null)
            {
                count = _customers.Count();
                treeListCustomerFilter.AppendNode(new object[] { new CustomerFilter() { Name = "*Все" }, count }, -1, -1, -1, -1);
            }

            //var groupOperatorMyCustomer = new GroupOperator(GroupOperatorType.Or);
            //var user = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid);
            //if (user.Staff != null)
            //{
            //    var criteriaAccountantResponsible = new BinaryOperator(nameof(Customer.AccountantResponsible), user.Staff);
            //    groupOperatorMyCustomer.Operands.Add(criteriaAccountantResponsible);

            //    var criteriaBankResponsible = new BinaryOperator(nameof(Customer.BankResponsible), user.Staff);
            //    groupOperatorMyCustomer.Operands.Add(criteriaBankResponsible);

            //    var criteriaPrimaryResponsible = new BinaryOperator(nameof(Customer.PrimaryResponsible), user.Staff);
            //    groupOperatorMyCustomer.Operands.Add(criteriaPrimaryResponsible);
            //}
            //_customers.Filter = groupOperatorMyCustomer;
            //count = _customers.Count();

            _customers.Filter = await cls_BaseSpr.GetCustomerCriteria(null);
            count = _customers.Count();

            treeListCustomerFilter.AppendNode(new object[] { new CustomerFilter() { Name = "Мои клиенты" }, count }, -1, -1, -1, -1);

            foreach (var customerFilter in _customerFilters.OrderBy(o => o.Number))
            {
                if (customerFilter.IsVisibleCustomer)
                {
                    customerFilter.Users.Reload();
                    customerFilter.UserGroups.Reload();

                    if (customerFilter.Users.FirstOrDefault(f => f.User.Oid == sessionUser.Oid) != null ||
                        customerFilter.UserGroups.FirstOrDefault(f => sessionUser.UserGroups.FirstOrDefault(sf => sf.UserGroup.Oid == f.UserGroup.Oid) != null) != null)
                    {
                        _customers.Filter = customerFilter.GetGroupOperatorCustomer();
                        count = _customers.Count();
                        treeListCustomerFilter.AppendNode(new object[] { customerFilter, count }, -1, -1, -1, -1);
                        _customers.Filter = null;
                    }
                }
                _customers.Filter = null;
            }
            _customers.Filter = null;

            try
            {
                if (obj != null)
                {
                    var node = default(TreeListNode);

                    if (int.TryParse(obj?.ToString(), out int oid))
                    {
                        node = treeListCustomerFilter.Nodes?.FirstOrDefault(f => f.RootNode?.GetValue(0) is CustomerFilter customerFilter && customerFilter.Oid == oid);
                    }
                    else if (obj is string name && !string.IsNullOrWhiteSpace(name))
                    {
                        node = treeListCustomerFilter.Nodes?.FirstOrDefault(f => f.RootNode?.GetValue(0) is string objName
                            && objName == name
                            || f.RootNode?.GetValue(0) is CustomerFilter customerFilter
                            && customerFilter.Name == name);
                    }

                    if (node != null)
                    {
                        treeListCustomerFilter.SetFocusedNode(node);
                    }
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async void treeListCustomerFilter_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            var treList = sender as TreeList;

            if (treList != null && treList.FocusedNode?.GetValue(0) is CustomerFilter customerFilter)
            {
                if (customerFilter.Name.Equals("*Все"))
                {
                    _customers.Filter = null;
                }
                else if (customerFilter.Name.Equals("Мои клиенты"))
                {
                    //var groupOperatorMyCustomer = new GroupOperator(GroupOperatorType.Or);

                    //var user = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid);
                    //if (user.Staff != null)
                    //{
                    //    var criteriaAccountantResponsible = new BinaryOperator(nameof(Customer.AccountantResponsible), user.Staff);
                    //    groupOperatorMyCustomer.Operands.Add(criteriaAccountantResponsible);

                    //    var criteriaBankResponsible = new BinaryOperator(nameof(Customer.BankResponsible), user.Staff);
                    //    groupOperatorMyCustomer.Operands.Add(criteriaBankResponsible);

                    //    var criteriaPrimaryResponsible = new BinaryOperator(nameof(Customer.PrimaryResponsible), user.Staff);
                    //    groupOperatorMyCustomer.Operands.Add(criteriaPrimaryResponsible);
                    //}

                    //_customers.Filter = groupOperatorMyCustomer;

                    _customers.Filter = await cls_BaseSpr.GetCustomerCriteria(null);
                }
                else
                {
                    _customers.Filter = customerFilter.GetGroupOperatorCustomer();
                }
            }
            else
            {
                _customers.Filter = null;
            }

            gridViewCustomer_FocusedRowChanged(gridViewCustomer, new FocusedRowChangedEventArgs(-1, gridViewCustomer.FocusedRowHandle));
        }

        private async void btnDirectoryCustomerFilter_Click(object sender, EventArgs e)
        {
            cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.CustomerFilter, -1);
            await TreeListUpdate();
        }

        private async void btnDirectoryAdd_Click(object sender, EventArgs e)
        {
            var form = new CustomersFilterForm(_session, WorkZone.ModuleCustomer);
            form.ShowDialog();

            if (form.FlagSave)
            {
                await TreeListUpdate(form?.CustomerFilter?.Oid);
            }
        }

        private async void treeListCustomerFilter_DoubleClick(object sender, EventArgs e)
        {
            var treList = sender as TreeList;

            if (treList != null && treList.FocusedNode?.GetValue(0) is CustomerFilter customerFilter)
            {
                if (customerFilter.Oid > 0)
                {
                    var form = new CustomersFilterForm(customerFilter, WorkZone.ModuleCustomer);
                    form.ShowDialog();

                    if (form.FlagSave)
                    {
                        await TreeListUpdate(form?.CustomerFilter?.Oid);
                    }
                }
            }
        }

        private async void CustomersForm_Load(object sender, EventArgs e)
        {
            _customers.Criteria = await cls_BaseSpr.GetCustomerCriteria(null);

            await SetAccessRights();
            FunctionalGridSetup();

            UpdatePropertiesXPCollection();
            await GetStatusReportColor();
            await GetStatusArchiveFolderColor();

            var focusedNode = await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"{this.Name}_{nameof(treeListCustomerFilter)}_{nameof(treeListCustomerFilter.FocusedNode)}", null, user: BVVGlobal.oApp.User);
            await TreeListUpdate(focusedNode);

            await LoadSettingsForm();
            accordCustomer.StateChanged += accordCustomer_StateChanged;

            gridViewCustomer.GridViewSetup(isColumnAutoWidth: false, isShowFooter: false);
        }

        private bool GetAccesEditCustomersForm(bool? isAcces = null)
        {
            if (isEditCustomersForm)
            {
                if (isAcces is true)
                {
                    return true;
                }

                return false;
            }

            return false;
        }

        private bool isEditCustomersForm = false;
        private bool isDeleteCustomersForm = false;
        private bool isDeleteReportChangeForm = false;
        private async System.Threading.Tasks.Task SetAccessRights()
        {
            try
            {
                using (var uof = new UnitOfWork())
                {
                    var user = await uof.GetObjectByKeyAsync<User>(DatabaseConnection.User.Oid);
                    if (user != null)
                    {
                        var accessRights = user.AccessRights;
                        if (accessRights != null)
                        {
                            isDeleteReportChangeForm = accessRights.IsDeleteReportChangeForm;
                            isEditCustomersForm = accessRights.IsEditCustomersForm;
                            isDeleteCustomersForm = accessRights.IsDeleteCustomersForm;

                            xtraTabPageBankAccess.PageVisible = accessRights.IsViewCustomersFormPageBankAccess;
                            xtraTabPageTasks.PageVisible = accessRights.IsViewCustomersFormPageTasks;
                            xtraTabPageOrganizationPerfomance.PageVisible = accessRights.IsViewCustomersFormPageOrganizationPerfomance;
                            xtraTabPageInvoice.PageVisible = accessRights.IsViewCustomersFormPageInvoice;
                            xtraTabPageCustomerEploymentHistory.PageVisible = accessRights.IsViewCustomersFormPageEploymentHistory;
                        }
                    }
                }

                foreach (XtraTabPage tabPage in xtraTabCustomerInfo.TabPages)
                {
                    foreach (var obj in tabPage.Controls)
                    {
                        if (obj is PanelControl panelControl)
                        {
                            foreach (var item in panelControl.Controls)
                            {
                                if (item is SimpleButton button)
                                {
                                    button.Enabled = isEditCustomersForm;
                                }
                            }
                        }
                    }
                }

                //xtraTabCustomerInfo.Enabled = isEditCustomersForm;

                btnReportDel.Enabled = isDeleteReportChangeForm;
                btnReportDel.Visible = isDeleteReportChangeForm;

                CustomerEdit.CloseButtons(btnElectronicReporting, isEditCustomersForm);
                CustomerEdit.CloseButtons(btnElectronicDocumentManagement, isEditCustomersForm);
                CustomerEdit.CloseButtons(btnLeasing, isEditCustomersForm);
                CustomerEdit.CloseButtons(btnPatent, isEditCustomersForm);

                CustomerEdit.CloseButtons(btnOneEsBookkeeping, isEditCustomersForm);
                CustomerEdit.CloseButtons(btnOneEsSalary, isEditCustomersForm);
                CustomerEdit.CloseButtons(btnOneEsOther, isEditCustomersForm);

                cmbIsPublic.Enabled = isEditCustomersForm;
                cmbIsSalary.Enabled = isEditCustomersForm;
                cmbIsPublicAndSalary.Enabled = isEditCustomersForm;

                CustomerEdit.CloseButtons(txtAdvanceDayObject, isEditCustomersForm);
                CustomerEdit.CloseButtons(txtSalaryDayObject, isEditCustomersForm);
                btnSaveCustomerItemInfoSalary.Enabled = isEditCustomersForm;

                CustomerEdit.CloseButtons(btnTaxTransport, isEditCustomersForm);
                CustomerEdit.CloseButtons(btnTaxLand, isEditCustomersForm);
                CustomerEdit.CloseButtons(btnTaxProperty, isEditCustomersForm);
                CustomerEdit.CloseButtons(btnTaxAlcohol, isEditCustomersForm);
                CustomerEdit.CloseButtons(btnImportEAEU, isEditCustomersForm);

                txtElectronicFolder.Enabled = isEditCustomersForm;
                panelControlSubdivisions.Enabled = isEditCustomersForm;

                barBtnAddIndicators.Enabled = isEditCustomersForm;
                btnUpdateKindActivity.Enabled = isEditCustomersForm;
                btnUpdateIsLegalAddress.Enabled = isEditCustomersForm;

                btnDelCustomer.Enabled = isDeleteCustomersForm;
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async System.Threading.Tasks.Task LoadSettingsForm()
        {
            try
            {
                var accordCustomerStateString = await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"{this.Name}_{nameof(accordCustomer)}_{nameof(accordCustomer.OptionsMinimizing.State)}", ((int)accordCustomer.OptionsMinimizing.State).ToString(), user: BVVGlobal.oApp.User);
                if (int.TryParse(accordCustomerStateString, out int accordCustomerState))
                {
                    accordCustomer.OptionsMinimizing.State = (AccordionControlState)accordCustomerState;
                }

                await BVVGlobal.oFuncXpo.RestoreLayoutToStreamAsync(layoutControlCustomers, $"{this.Name}_{nameof(layoutControlCustomers)}");
                await BVVGlobal.oFuncXpo.RestoreLayoutToStreamAsync(gridViewCustomer, $"{this.Name}_{nameof(gridViewCustomer)}");
                await BVVGlobal.oFuncXpo.RestoreLayoutToStreamAsync(gridViewAttachment, $"{this.Name}_{nameof(gridViewAttachment)}");

                //TODO: времянка после обновления
                gridViewCustomer.ColumnSetup(nameof(Customer.DefaultName), isVisible: false, width: 275, isFixedWidth: true);
                gridViewCustomer.ColumnSetup(nameof(Customer.Name), isVisible: false, width: 275, isFixedWidth: true);
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
                await BVVGlobal.oFuncXpo.SaveLayoutToStreamAsync(layoutControlCustomers, $"{this.Name}_{nameof(layoutControlCustomers)}");
                await BVVGlobal.oFuncXpo.SaveLayoutToStreamAsync(gridViewCustomer, $"{this.Name}_{nameof(gridViewCustomer)}");
                await BVVGlobal.oFuncXpo.SaveLayoutToStreamAsync(gridViewAttachment, $"{this.Name}_{nameof(gridViewAttachment)}");
                await BVVGlobal.oFuncXpo.SaveLayoutToStreamAsync(gridViewBankAccess, $"{this.Name}_{nameof(gridViewBankAccess)}");
                await BVVGlobal.oFuncXpo.SaveLayoutToStreamAsync(gridViewTasks, $"{this.Name}_{nameof(gridViewTasks)}");
                await BVVGlobal.oFuncXpo.SaveLayoutToStreamAsync(gridViewForeignEconomicActivitys, $"{this.Name}_{nameof(gridControlForeignEconomicActivitys)}");
                await BVVGlobal.oFuncXpo.SaveLayoutToStreamAsync(gridViewOrganizationPerfomance, $"{this.Name}_{nameof(gridViewOrganizationPerfomance)}");
                await BVVGlobal.oFuncXpo.SaveLayoutToStreamAsync(gridViewInvoice, $"{this.Name}_{nameof(gridViewInvoice)}");
                await BVVGlobal.oFuncXpo.SaveLayoutToStreamAsync(gridViewCustomerReports, $"{this.Name}_{nameof(gridViewCustomerReports)}");
                await BVVGlobal.oFuncXpo.SaveLayoutToStreamAsync(viewCustomerReports, $"{this.Name}_{nameof(viewCustomerReports)}");
                await BVVGlobal.oFuncXpo.SaveLayoutToStreamAsync(gridViewCustomerStatisticalReports, $"{this.Name}_{nameof(gridViewCustomerStatisticalReports)}");
                await BVVGlobal.oFuncXpo.SaveLayoutToStreamAsync(gridViewCustomerArchiveFolders, $"{this.Name}_{nameof(gridViewCustomerArchiveFolders)}");
                await BVVGlobal.oFuncXpo.SaveLayoutToStreamAsync(gridViewCustomerEmploymentHistorys, $"{this.Name}_{nameof(gridViewCustomerEmploymentHistorys)}");
                await BVVGlobal.oFuncXpo.SaveLayoutToStreamAsync(gridViewChronicleCustomer, $"{this.Name}_{nameof(gridViewChronicleCustomer)}");
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private void btnAddCustomer_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new CustomerEdit();
            form.ShowDialog();

            var customer = form.Customer;
            if (customer?.Oid != -1)
            {
                _customers.Reload();
                gridViewCustomer.FocusedRowHandle = gridViewCustomer.LocateByValue(nameof(Customer.Oid), customer.Oid);
            }
        }

        private void btnEditCustomer_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewCustomer.IsEmpty)
            {
                return;
            }

            var customer = gridViewCustomer.GetRow(gridViewCustomer.FocusedRowHandle) as Customer;
            if (customer != null)
            {
                var form = new CustomerEdit(customer);
                form.ShowDialog();
            }
        }

        private void btnRefreshCustomer_ItemClick(object sender, ItemClickEventArgs e)
        {
            _customers.Reload();
        }

        private async void btnDelCustomer_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (gridViewCustomer.IsEmpty)
                {
                    return;
                }

                var customer = gridViewCustomer.GetRow(gridViewCustomer.FocusedRowHandle) as Customer;

                if (XtraMessageBox.Show($"Вы собираетесь удалить запись клиента: {customer}{Environment.NewLine}Хотите продолжить?",
                        "Удаление клиента",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question) == DialogResult.OK)
                {
                    var name = $"[{customer.INN}] {customer}";
                    customer.Delete();

                    using (var uof = new UnitOfWork())
                    {
                        var user = DatabaseConnection.User;
                        if (user != null)
                        {
                            var chronicleEvents = new ChronicleEvents(uof)
                            {
                                Act = Act.CUSTOMER_DELETED,
                                Date = DateTime.Now,
                                Name = Act.CUSTOMER_DELETED.GetEnumDescription(),
                                Description = $"Пользователь [{user}] произвел удаление клиента {name}",
                                User = await uof.GetObjectByKeyAsync<User>(user.Oid)
                            };
                            chronicleEvents.Save();
                        }
                        await uof.CommitChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        /// <summary>
        /// Очистить всю отображаемую информацию.
        /// </summary>
        private void ClearViewDate()
        {
            lblCustomerName.Text = string.Empty;
            lblCustomerName2.Text = string.Empty;

            gridControlTasks.DataSource = null;
            gridControlForeignEconomicActivitys.DataSource = null;
            gridControlOrganizationPerfomance.DataSource = null;
            gridControlChronicleCustomer.DataSource = null;
            gridControlBankAccess.DataSource = null;
            gridControlInvoice.DataSource = null;
            gridControlCustomerStatisticalReports.DataSource = null;
            gridControlSubdivisions.DataSource = null;
            gridControlCustomerReports.DataSource = null;
            gridControlCustomerArchiveFolders.DataSource = null;
            gridCustomerArchiveFolders.DataSource = null;
            gridCustomerReports.DataSource = null;
            gridControlAttachment.DataSource = null;

            txtState.EditValue = null;
            txtOrganizationStatus.EditValue = null;
            btnTaxSystem.EditValue = null;
            txtKindActivity.EditValue = null;
            txtAdvanceDayObject.EditValue = null;
            txtSalaryDayObject.EditValue = null;            
            txtElectronicFolder.EditValue = null;
            richEditNotes.Text = null;
            richEditNotes.HtmlText = null;
            SetEditValueElectronicReportingCustomer(btnElectronicReporting, null);
            SetEditValueElectronicDocumentManagementCustomer(btnElectronicDocumentManagement, null);
            btnPatent.EditValue = null;

            btnTaxProperty.EditValue = null;
            btnTaxAlcohol.EditValue = null;
            btnImportEAEU.EditValue = null;
            btnTaxTransport.EditValue = null;
            btnTaxLand.EditValue = null;
            btnLeasing.EditValue = null;
            btnOneEsBookkeeping.EditValue = null;
            btnOneEsSalary.EditValue = null;
            btnOneEsOther.EditValue = null;

            memoAdditionalInformationSalary.EditValue = null;
            memoEdit8.EditValue = null;

            lblUserUpdateNotes.Text = null;

            cmbIsPublic.SelectedIndex = -1;
            cmbIsSalary.SelectedIndex = -1;
            cmbIsPublicAndSalary.SelectedIndex = -1;
            btnSaveCustomerItemInfoSalary.Visible = false;
        }

        private bool _isLineJump = false;
        private async void gridViewCustomer_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            _isLineJump = true;

            if (e is null)
            {
                return;
            }

            _currentCustomer = null;
            ClearViewDate();

            var gridview = sender as GridView;
            if (gridview == null || gridViewCustomer.IsEmpty)
            {
                return;
            }

            _currentCustomer = gridview.GetRow(gridview.FocusedRowHandle) as Customer;

            if (_currentCustomer != null)
            {
                ReloadCustomer(_currentCustomer);

                lblCustomerName.Text = _currentCustomer.ToString();
                lblCustomerName2.Text = _currentCustomer.ToString();

                var isOpen = false;
                try
                {
                    var windowsIdentity = System.Security.Principal.WindowsIdentity.GetCurrent();
                    if (windowsIdentity != null)
                    {
                        var localSetting = await new XPQuery<set_LocalSettings>(DatabaseConnection.LocalSession)
                            .FirstOrDefaultAsync(f => f.Name == "IsOpenCustomerNotes" 
                                && f.User == DatabaseConnection.User.Login
                                && f.Value3 == windowsIdentity.Name);
                        if (localSetting != null)
                        {
                            if (bool.TryParse(localSetting.Value1, out bool result))
                            {
                                isOpen = result;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                }

                if (isOpen)
                {
                    xtraTabCustomerInfo.SelectedTabPage = xtraTabPageNotes;
                }

                gridControlSubdivisions.DataSource = _currentCustomer.Subdivisions;
                if (gridViewSubdivisions.Columns[nameof(Subdivision.Oid)] != null)
                {
                    gridViewSubdivisions.Columns[nameof(Subdivision.Oid)].Visible = false;
                    gridViewSubdivisions.Columns[nameof(Subdivision.Oid)].Width = 18;
                    gridViewSubdivisions.Columns[nameof(Subdivision.Oid)].OptionsColumn.FixedWidth = true;
                }

                gridControlAttachment.DataSource = _currentCustomer.Attachments;
                foreach (GridColumn column in gridViewAttachment.Columns)
                {
                    column.Visible = false;
                }
                gridViewAttachment.ColumnSetup(nameof(LetterAttachment.FullFileName), caption: "Файл");

                if (_currentCustomer?.Tax is null)
                {
                    _currentCustomer.Tax = new Tax(_session);
                    _currentCustomer.Tax.Save();
                }

                btnPatent.EditValue = _currentCustomer.Tax?.Patent;
                if (!string.IsNullOrWhiteSpace(_currentCustomer.Tax?.Patent?.ToString()))
                {
                    if (_currentCustomer.Tax.Patent.ToString().Equals("Нет"))
                    {
                        btnPatent.Properties.ContextImageOptions.Image = imgCustomer.Images[1];
                    }
                    else
                    {
                        btnPatent.Properties.ContextImageOptions.Image = imgCustomer.Images[0];
                    }
                }
                else
                {
                    btnPatent.Properties.ContextImageOptions.Image = null;
                }

                if (_currentCustomer.Attachments != null && _currentCustomer.Attachments.Count > 0)
                {
                    splitContainerNotes.PanelVisibility = SplitPanelVisibility.Both;
                    splitContainerNotes.SplitterPosition = splitContainerNotes.Width / 4;
                }
                else
                {
                    splitContainerNotes.PanelVisibility = SplitPanelVisibility.Panel2;
                    splitContainerNotes.SplitterPosition = 0;
                }

                txtState.Text = $"{_currentCustomer.CustomerStatus?.ToString()}";
                txtOrganizationStatus.Text = $"{_currentCustomer.OrganizationStatusString} на {Convert.ToDateTime(_currentCustomer.DateActuality).ToShortDateString()}";
                btnTaxSystem.EditValue = _currentCustomer.TaxSystemCustomer;
                txtKindActivity.Text = _currentCustomer.KindActivity?.ToString();
                txtAdvanceDayObject.EditValue = _currentCustomer.AdvanceDayObject;
                txtSalaryDayObject.EditValue = _currentCustomer.SalaryDayObject;                
                txtElectronicFolder.Text = _currentCustomer.ElectronicFolder;

                if (!string.IsNullOrWhiteSpace(_currentCustomer.Notes))
                {
                    richEditNotes.Text = _currentCustomer.Notes;
                }
                else
                {
                    richEditNotes.HtmlText = Letter.ByteToString(_currentCustomer.NoteByte);
                }

                SetEditValueElectronicReportingCustomer(btnElectronicReporting, _currentCustomer);
                SetEditValueElectronicDocumentManagementCustomer(btnElectronicDocumentManagement, _currentCustomer);
                //btnElectronicReporting.EditValue = _currentCustomer.ElectronicReportingCustomer;

                btnTaxProperty.EditValue = _currentCustomer.Tax?.TaxProperty;
                if (_currentCustomer.Tax?.TaxProperty?.IsUse is true)
                {
                    btnTaxProperty.Properties.ContextImageOptions.Image = imgCustomer.Images[0];
                }
                else if (_currentCustomer.Tax?.TaxProperty?.IsUse is false)
                {
                    btnTaxProperty.Properties.ContextImageOptions.Image = imgCustomer.Images[1];
                }
                else
                {
                    btnTaxProperty.Properties.ContextImageOptions.Image = null;
                }

                btnTaxAlcohol.EditValue = _currentCustomer.Tax?.TaxAlcohol;
                if (_currentCustomer.Tax?.TaxAlcohol?.IsUse is true)
                {
                    btnTaxAlcohol.Properties.ContextImageOptions.Image = imgCustomer.Images[0];
                }
                else if (_currentCustomer.Tax?.TaxAlcohol?.IsUse is false)
                {
                    btnTaxAlcohol.Properties.ContextImageOptions.Image = imgCustomer.Images[1];
                }
                else
                {
                    btnTaxAlcohol.Properties.ContextImageOptions.Image = null;
                }

                btnImportEAEU.EditValue = _currentCustomer.Tax?.TaxImportEAEU;
                if (_currentCustomer.Tax?.TaxImportEAEU?.IsUse is true)
                {
                    btnImportEAEU.Properties.ContextImageOptions.Image = imgCustomer.Images[0];
                }
                else if (_currentCustomer.Tax?.TaxImportEAEU?.IsUse is false)
                {
                    btnImportEAEU.Properties.ContextImageOptions.Image = imgCustomer.Images[1];
                }
                else
                {
                    btnImportEAEU.Properties.ContextImageOptions.Image = null;
                }

                btnTaxTransport.EditValue = _currentCustomer.Tax?.TaxTransport;
                if (_currentCustomer.Tax?.TaxTransport?.IsUse is true)
                {
                    btnTaxTransport.Properties.ContextImageOptions.Image = imgCustomer.Images[0];
                }
                else if (_currentCustomer.Tax?.TaxTransport?.IsUse is false)
                {
                    btnTaxTransport.Properties.ContextImageOptions.Image = imgCustomer.Images[1];
                }
                else
                {
                    btnTaxTransport.Properties.ContextImageOptions.Image = null;
                }

                btnTaxLand.EditValue = _currentCustomer.Tax?.TaxLand;
                if (_currentCustomer.Tax?.TaxLand?.IsUse is true)
                {
                    btnTaxLand.Properties.ContextImageOptions.Image = imgCustomer.Images[0];
                }
                else if (_currentCustomer.Tax?.TaxLand?.IsUse is false)
                {
                    btnTaxLand.Properties.ContextImageOptions.Image = imgCustomer.Images[1];
                }
                else
                {
                    btnTaxLand.Properties.ContextImageOptions.Image = null;
                }

                btnLeasing.EditValue = _currentCustomer.Leasing;
                if (_currentCustomer.Leasing?.IsUse is true)
                {
                    btnLeasing.Properties.ContextImageOptions.Image = imgCustomer.Images[0];
                }
                else if (_currentCustomer.Leasing?.IsUse is false)
                {
                    btnLeasing.Properties.ContextImageOptions.Image = imgCustomer.Images[1];
                }
                else
                {
                    btnLeasing.Properties.ContextImageOptions.Image = null;
                }

                btnOneEsBookkeeping.EditValue = _currentCustomer.OneEs?.OneEsBookkeeping;
                if (_currentCustomer.OneEs?.OneEsBookkeeping?.IsUse is true)
                {
                    btnOneEsBookkeeping.Properties.ContextImageOptions.Image = imgCustomer.Images[0];
                }
                else if (_currentCustomer.OneEs?.OneEsBookkeeping?.IsUse is false)
                {
                    btnOneEsBookkeeping.Properties.ContextImageOptions.Image = imgCustomer.Images[1];
                }
                else
                {
                    btnOneEsBookkeeping.Properties.ContextImageOptions.Image = null;
                }

                btnOneEsSalary.EditValue = _currentCustomer.OneEs?.OneEsSalary;
                if (_currentCustomer.OneEs?.OneEsSalary?.IsUse is true)
                {
                    btnOneEsSalary.Properties.ContextImageOptions.Image = imgCustomer.Images[0];
                }
                else if (_currentCustomer.OneEs?.OneEsSalary?.IsUse is false)
                {
                    btnOneEsSalary.Properties.ContextImageOptions.Image = imgCustomer.Images[1];
                }
                else
                {
                    btnOneEsSalary.Properties.ContextImageOptions.Image = null;
                }

                btnOneEsOther.EditValue = _currentCustomer.OneEs?.OneEsOther;
                if (_currentCustomer.OneEs?.OneEsOther?.IsUse is true)
                {
                    btnOneEsOther.Properties.ContextImageOptions.Image = imgCustomer.Images[0];
                }
                else if (_currentCustomer.OneEs?.OneEsOther?.IsUse is false)
                {
                    btnOneEsOther.Properties.ContextImageOptions.Image = imgCustomer.Images[1];
                }
                else
                {
                    btnOneEsOther.Properties.ContextImageOptions.Image = null;
                }

                memoAdditionalInformationSalary.Text = _currentCustomer.DOP1;
                memoEdit8.Text = _currentCustomer.DOP2;

                var chronicleCustomer = _currentCustomer.ChronicleCustomers.LastOrDefault(l => l.Act == Act.UPDATING_ACCOUNTING_NUANCES);
                if (chronicleCustomer != null)
                {
                    lblUserUpdateNotes.Text = $"Пользователь [{chronicleCustomer.User}] внес изменения ({chronicleCustomer.Date.ToShortTimeString()}) {chronicleCustomer.Date.ToShortDateString()}";
                }

                cmbIsPublic.SelectedIndex = GetIndex(_currentCustomer.IsPublic);
                cmbIsSalary.SelectedIndex = GetIndex(_currentCustomer.IsSalary);
                cmbIsPublicAndSalary.SelectedIndex = GetIndex(_currentCustomer.IsPublicAndSalary);

                xtraTabCustomerInfo_SelectedPageChanged(xtraTabCustomerInfo, new TabPageChangedEventArgs(xtraTabCustomerInfo.SelectedTabPage, xtraTabCustomerInfo.SelectedTabPage));
            }

            _isLineJump = false;
        }

        private int GetIndex(Availability? availability)
        {
            if (availability is null)
            {
                return -1;
            }
            else
            {
                return (int)availability;
            }
        }

        private async void xtraTabCustomerInfo_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            if (_currentCustomer != null)
            {
                var criteriaCustomer = new BinaryOperator($"{nameof(Customer)}.{nameof(Customer.Oid)}", _currentCustomer.Oid);

                if (e.Page == xtraTabPageBankAccess)
                {
                    gridControlBankAccess.DataSource = _currentCustomer.BankAccess;
                    foreach (GridColumn columns in gridViewBankAccess.Columns)
                    {
                        columns.OptionsColumn.ReadOnly = true;
                        columns.OptionsColumn.AllowEdit = false;
                    }
                    if (gridViewBankAccess.Columns[nameof(BankAccess.Oid)] != null)
                    {
                        gridViewBankAccess.Columns[nameof(BankAccess.Oid)].Visible = false;
                        gridViewBankAccess.Columns[nameof(BankAccess.Oid)].Width = 18;
                        gridViewBankAccess.Columns[nameof(BankAccess.Oid)].OptionsColumn.FixedWidth = true;
                    }
                    if (gridViewBankAccess.Columns[nameof(BankAccess.Link)] != null)
                    {
                        RepositoryItemButtonEdit btnLink = gridControlBankAccess.RepositoryItems.Add(nameof(ButtonEdit)) as RepositoryItemButtonEdit;
                        var btn = btnLink.Buttons.FirstOrDefault(f => f.Kind == ButtonPredefines.Ellipsis);
                        if (btn != null)
                        {
                            btn.Kind = ButtonPredefines.Right;
                        }
                        btnLink.ButtonPressed += BtnLink_ButtonPressed;

                        gridViewBankAccess.Columns[nameof(BankAccess.Link)].ColumnEdit = btnLink;
                        gridViewBankAccess.Columns[nameof(BankAccess.Link)].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
                        gridViewBankAccess.Columns[nameof(BankAccess.Link)].OptionsColumn.ReadOnly = false;
                        gridViewBankAccess.Columns[nameof(BankAccess.Link)].OptionsColumn.AllowEdit = true;
                    }

                    await BVVGlobal.oFuncXpo.RestoreLayoutToStreamAsync(gridViewBankAccess, $"{this.Name}_{nameof(gridViewBankAccess)}");
                }

                else if (e.Page == xtraTabPageDossier)
                {
                    var dossierControl = default(DossierControl);
                    var baseLayoutItem = xtraTabPageDossier.Controls[nameof(dossierControl)];
                    if (baseLayoutItem is null)
                    {
                        dossierControl = new DossierControl();
                        dossierControl.Dock = DockStyle.Fill;
                        dossierControl.SetImageCollection(imgCustomer);
                        xtraTabPageDossier.Controls.Add(dossierControl);
                    }
                    else
                    {
                        dossierControl = (DossierControl)baseLayoutItem;
                    }

                    await dossierControl.UpdateAsync(_currentCustomer);
                }

                else if (e.Page == xtraTabPageTasks)
                {
                    gridControlTasks.DataSource = _currentCustomer.Tasks;
                    if (gridViewTasks.Columns[nameof(Task.Oid)] != null)
                    {
                        gridViewTasks.Columns[nameof(Task.Oid)].Visible = false;
                        gridViewTasks.Columns[nameof(Task.Oid)].Width = 18;
                        gridViewTasks.Columns[nameof(Task.Oid)].OptionsColumn.FixedWidth = true;
                    }

                    await BVVGlobal.oFuncXpo.RestoreLayoutToStreamAsync(gridViewTasks, $"{this.Name}_{nameof(gridViewTasks)}");
                }
                //else if (e.Page == xtraTabPageForeignEconomicActivitys)
                //{
                //    gridControlForeignEconomicActivitys.DataSource = _currentCustomer.ForeignEconomicActivities;
                //    if (gridViewForeignEconomicActivitys.Columns[nameof(ForeignEconomicActivity.Oid)] != null)
                //    {
                //        gridViewForeignEconomicActivitys.Columns[nameof(ForeignEconomicActivity.Oid)].Visible = false;
                //        gridViewForeignEconomicActivitys.Columns[nameof(ForeignEconomicActivity.Oid)].Width = 18;
                //        gridViewForeignEconomicActivitys.Columns[nameof(ForeignEconomicActivity.Oid)].OptionsColumn.FixedWidth = true;
                //    }

                //    await BVVGlobal.oFuncXpo.RestoreLayoutToStreamAsync(gridViewForeignEconomicActivitys, $"{this.Name}_{nameof(gridControlForeignEconomicActivitys)}");
                //}
                else if (e.Page == xtraTabPageOrganizationPerfomance)
                {
                    gridControlOrganizationPerfomance.DataSource = _currentCustomer.PersonalIncomeTaxis;

                    if (gridViewOrganizationPerfomance.Columns[nameof(OrganizationPerformance.Oid)] != null)
                    {
                        gridViewOrganizationPerfomance.Columns[nameof(OrganizationPerformance.Oid)].Visible = false;
                        gridViewOrganizationPerfomance.Columns[nameof(OrganizationPerformance.Oid)].Width = 18;
                        gridViewOrganizationPerfomance.Columns[nameof(OrganizationPerformance.Oid)].OptionsColumn.FixedWidth = true;
                    }
                    if (gridViewOrganizationPerfomance.Columns[nameof(OrganizationPerformance.Period)] != null)
                    {
                        gridViewOrganizationPerfomance.Columns[nameof(OrganizationPerformance.Period)].Width = 100;
                        gridViewOrganizationPerfomance.Columns[nameof(OrganizationPerformance.Period)].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    }
                    if (gridViewOrganizationPerfomance.Columns[nameof(OrganizationPerformance.Operation)] != null)
                    {
                        gridViewOrganizationPerfomance.Columns[nameof(OrganizationPerformance.Operation)].Width = 110;
                        gridViewOrganizationPerfomance.Columns[nameof(OrganizationPerformance.Operation)].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    }
                    if (gridViewOrganizationPerfomance.Columns[nameof(OrganizationPerformance.IsInvoice)] != null)
                    {
                        RepositoryItemImageComboBox imgIsInvoice = gridControlOrganizationPerfomance.RepositoryItems.Add(nameof(ImageComboBoxEdit)) as RepositoryItemImageComboBox;
                        imgIsInvoice.SmallImages = imgCustomer;
                        imgIsInvoice.Items.Add(new ImageComboBoxItem() { Value = true, ImageIndex = 5 });
                        imgIsInvoice.Items.Add(new ImageComboBoxItem() { Value = false, ImageIndex = 6 });

                        imgIsInvoice.GlyphAlignment = HorzAlignment.Center;
                        gridViewOrganizationPerfomance.Columns[nameof(OrganizationPerformance.IsInvoice)].ColumnEdit = imgIsInvoice;
                        gridViewOrganizationPerfomance.Columns[nameof(OrganizationPerformance.IsInvoice)].Width = 18;
                        gridViewOrganizationPerfomance.Columns[nameof(OrganizationPerformance.IsInvoice)].OptionsColumn.FixedWidth = true;
                    }
                    if (gridViewOrganizationPerfomance.Columns[nameof(OrganizationPerformance.Nuances)] != null)
                    {
                        RepositoryItemMemoEdit memoEdit = gridControlOrganizationPerfomance.RepositoryItems.Add(nameof(MemoEdit)) as RepositoryItemMemoEdit;
                        gridViewOrganizationPerfomance.Columns[nameof(OrganizationPerformance.Nuances)].ColumnEdit = memoEdit;
                    }
                    if (gridViewOrganizationPerfomance.Columns[nameof(OrganizationPerformance.Staff)] != null)
                    {
                        RepositoryItemMemoEdit memoEdit = gridControlOrganizationPerfomance.RepositoryItems.Add(nameof(MemoEdit)) as RepositoryItemMemoEdit;
                        gridViewOrganizationPerfomance.Columns[nameof(OrganizationPerformance.Staff)].ColumnEdit = memoEdit;
                    }
                    if (gridViewOrganizationPerfomance.Columns[nameof(OrganizationPerformance.Price)] != null)
                    {
                        gridViewOrganizationPerfomance.Columns[nameof(OrganizationPerformance.Price)].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    }

                    await BVVGlobal.oFuncXpo.RestoreLayoutToStreamAsync(gridViewOrganizationPerfomance, $"{this.Name}_{nameof(gridViewOrganizationPerfomance)}");
                }
                else if (e.Page == xtraTabPageInvoice)
                {
                    gridControlInvoice.DataSource = _currentCustomer.Invoices;
                    if (gridViewInvoice.Columns[nameof(Invoice.Oid)] != null)
                    {
                        gridViewInvoice.Columns[nameof(Invoice.Oid)].Visible = false;
                        gridViewInvoice.Columns[nameof(Invoice.Oid)].Width = 18;
                        gridViewInvoice.Columns[nameof(Invoice.Oid)].OptionsColumn.FixedWidth = true;
                    }

                    if (gridViewInvoice.Columns[nameof(Invoice.IsSent)] != null)
                    {
                        RepositoryItemImageComboBox imgIsSent = gridControlInvoice.RepositoryItems.Add(nameof(ImageComboBoxEdit)) as RepositoryItemImageComboBox;
                        imgIsSent.SmallImages = imgInvoice;
                        imgIsSent.Items.Add(new ImageComboBoxItem() { Value = true, ImageIndex = 0 });
                        imgIsSent.Items.Add(new ImageComboBoxItem() { Value = false, ImageIndex = -1 });

                        imgIsSent.GlyphAlignment = HorzAlignment.Center;
                        gridViewInvoice.Columns[nameof(Invoice.IsSent)].ColumnEdit = imgIsSent;
                        gridViewInvoice.Columns[nameof(Invoice.IsSent)].Width = 18;
                        gridViewInvoice.Columns[nameof(Invoice.IsSent)].OptionsColumn.FixedWidth = true;
                    }

                    await BVVGlobal.oFuncXpo.RestoreLayoutToStreamAsync(gridViewInvoice, $"{this.Name}_{nameof(gridViewInvoice)}");
                }
                else if (e.Page == xtraTabPageCustomerReports)
                {
                    gridControlCustomerReports.GridControlSetup();
                    gridControlCustomerReports.DataSource = _currentCustomer.CustomerReports;

                    foreach (GridColumn column in gridViewCustomerReports.Columns)
                    {
                        column.Visible = false;
                    }

                    gridViewCustomerReports.ColumnSetup(nameof(CustomerReport.ReportComment), caption: "Комментарий", width: 300, isFixedWidth: true);
                    gridViewCustomerReports.ColumnSetup(nameof(CustomerReport.ReportDeadline), caption: "Сроки сдачи", width: 150, isFixedWidth: true);
                    gridViewCustomerReports.ColumnSetup(nameof(CustomerReport.ReportPeriodicity), caption: "Периодичность", width: 100, isFixedWidth: true);
                    gridViewCustomerReports.ColumnSetup(nameof(CustomerReport.ReportName), caption: "Наименование формы", width: 200, isFixedWidth: true);
                    gridViewCustomerReports.ColumnSetup(nameof(CustomerReport.ReportOKUD), caption: "ОКУД", width: 100, isFixedWidth: true);
                    gridViewCustomerReports.ColumnSetup(nameof(CustomerReport.ReportFormIndex), caption: "Индекс формы", width: 100, isFixedWidth: true);
                    gridViewCustomerReports.GridViewSetup(isColumnAutoWidth: true, isShowFooter: false);

                    var customerReports = new XPCollection<ReportChange>(_session, criteriaCustomer);
                    gridCustomerReports.DataSource = customerReports;
                    if (viewCustomerReports.Columns[nameof(ReportChange.Oid)] != null)
                    {
                        viewCustomerReports.Columns[nameof(ReportChange.Oid)].Visible = false;
                        viewCustomerReports.Columns[nameof(ReportChange.Oid)].Width = 18;
                        viewCustomerReports.Columns[nameof(ReportChange.Oid)].OptionsColumn.FixedWidth = true;
                    }
                    viewCustomerReports.GridViewSetup(isColumnAutoWidth: true, isShowFooter: false);

                    await BVVGlobal.oFuncXpo.RestoreLayoutToStreamAsync(gridViewCustomerReports, $"{this.Name}_{nameof(gridViewCustomerReports)}");
                    await BVVGlobal.oFuncXpo.RestoreLayoutToStreamAsync(viewCustomerReports, $"{this.Name}_{nameof(viewCustomerReports)}");
                }
                else if (e.Page == xtraTabPageStatisticalReports)
                {
                    gridControlCustomerStatisticalReports.GridControlSetup();
                    gridControlCustomerStatisticalReports.DataSource = _currentCustomer.StatisticalReports;

                    foreach (GridColumn column in gridViewCustomerStatisticalReports.Columns)
                    {
                        column.Visible = false;
                    }

                    gridViewCustomerStatisticalReports.ColumnSetup(nameof(StatisticalReport.StatisticalReportObjs), caption: "Сформированные отчеты", width: 400, isFixedWidth: true);
                    gridViewCustomerStatisticalReports.ColumnSetup(nameof(StatisticalReport.ReportComment), caption: "Комментарий", width: 400, isFixedWidth: true);
                    gridViewCustomerStatisticalReports.ColumnSetup(nameof(StatisticalReport.ResponsibleString), caption: "Ответственный", width: 150, isFixedWidth: true);
                    gridViewCustomerStatisticalReports.ColumnSetup(nameof(StatisticalReport.ReportOKUD), caption: "ОКУД", width: 150, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
                    gridViewCustomerStatisticalReports.ColumnSetup(nameof(StatisticalReport.ReportDeadline), caption: "Сроки сдачи", width: 200, isFixedWidth: true);
                    gridViewCustomerStatisticalReports.ColumnSetup(nameof(StatisticalReport.ReportPeriodicity), caption: "Периодичность", width: 125, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
                    gridViewCustomerStatisticalReports.ColumnSetup(nameof(StatisticalReport.ReportName), caption: "Номер\nдоговора", width: 200, isFixedWidth: true);
                    gridViewCustomerStatisticalReports.ColumnSetup(nameof(StatisticalReport.ReportFormIndex), caption: "Индекс формы", width: 125, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
                    gridViewCustomerStatisticalReports.ColumnSetup(nameof(StatisticalReport.Year), caption: "Год сдачи", width: 75, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
                    gridViewCustomerStatisticalReports.ColumnSetup(nameof(StatisticalReport.IsCurrentYear), caption: " ", width: 25, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
                    gridViewCustomerStatisticalReports.ColumnSetup(nameof(StatisticalReport.CreateDate), caption: "Дата\nсоздания", width: 175, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
                    gridViewCustomerStatisticalReports.ColumnSetup(nameof(StatisticalReport.User), caption: "Пользователь", width: 150, isFixedWidth: true);
                    gridViewCustomerStatisticalReports.GridViewSetup(isColumnAutoWidth: false, isShowFooter: false);

                    if (gridViewCustomerStatisticalReports.Columns[nameof(StatisticalReport.IsCurrentYear)] is GridColumn columnIsCurrentYear)
                    {
                        var imageComboBoxEdit = gridControlOrganizationPerfomance.RepositoryItems.Add(nameof(ImageComboBoxEdit)) as RepositoryItemImageComboBox;
                        imageComboBoxEdit.SmallImages = imgCustomer;
                        imageComboBoxEdit.GlyphAlignment = HorzAlignment.Center;
                        imageComboBoxEdit.Items.Add(new ImageComboBoxItem() { Value = true, ImageIndex = 0 });
                        imageComboBoxEdit.Items.Add(new ImageComboBoxItem() { Value = false, ImageIndex = 1 });

                        columnIsCurrentYear.ColumnEdit = imageComboBoxEdit;
                        columnIsCurrentYear.Width = 25;
                        columnIsCurrentYear.OptionsColumn.FixedWidth = true;
                        columnIsCurrentYear.Caption = " ";
                    }

                    if (gridViewCustomerStatisticalReports.Columns[nameof(StatisticalReport.ReportName)] is GridColumn columnReportName)
                    {
                        var repositoryItemMemoEdit = gridControlCustomerStatisticalReports.RepositoryItems.Add(nameof(MemoEdit)) as RepositoryItemMemoEdit;
                        repositoryItemMemoEdit.WordWrap = true;

                        columnReportName.ColumnEdit = repositoryItemMemoEdit;
                        columnReportName.Width = 200;
                        columnReportName.OptionsColumn.FixedWidth = true;
                        columnReportName.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                        columnReportName.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
                    }

                    if (gridViewCustomerStatisticalReports.Columns[nameof(StatisticalReport.ReportPeriodicity)] is GridColumn columnReportPeriodicity)
                    {
                        columnReportPeriodicity.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
                    }

                    if (gridViewCustomerStatisticalReports.Columns[nameof(StatisticalReport.ReportDeadline)] is GridColumn columnReportDeadline)
                    {
                        var repositoryItemMemoEdit = gridControlCustomerStatisticalReports.RepositoryItems.Add(nameof(MemoEdit)) as RepositoryItemMemoEdit;
                        repositoryItemMemoEdit.WordWrap = true;

                        columnReportDeadline.ColumnEdit = repositoryItemMemoEdit;
                        columnReportDeadline.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
                    }

                    if (gridViewCustomerStatisticalReports.Columns[nameof(StatisticalReport.ReportComment)] is GridColumn columnReportComment)
                    {
                        var repositoryItemMemoEdit = gridControlCustomerStatisticalReports.RepositoryItems.Add(nameof(MemoEdit)) as RepositoryItemMemoEdit;
                        repositoryItemMemoEdit.WordWrap = true;

                        columnReportComment.ColumnEdit = repositoryItemMemoEdit;
                        columnReportComment.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
                    }

                    if (gridViewCustomerStatisticalReports.Columns[nameof(StatisticalReport.StatisticalReportObjs)] is GridColumn columnStatisticalReportObjs)
                    {
                        var repositoryItemMemoEdit = gridControlCustomerStatisticalReports.RepositoryItems.Add(nameof(MemoEdit)) as RepositoryItemMemoEdit;
                        repositoryItemMemoEdit.WordWrap = true;

                        columnStatisticalReportObjs.ColumnEdit = repositoryItemMemoEdit;
                    }

                    //TODO: открыть после обновления.
                    //await BVVGlobal.oFuncXpo.RestoreLayoutToStreamAsync(gridViewCustomerStatisticalReports, $"{this.Name}_{nameof(gridViewCustomerStatisticalReports)}");
                }
                else if (e.Page == xtraTabPageCustomerArchiveFolder)
                {
                    gridControlCustomerArchiveFolders.GridControlSetup();
                    gridControlCustomerArchiveFolders.DataSource = _currentCustomer.CustomerArchiveFolders;

                    foreach (GridColumn column in gridViewCustomerArchiveFolders.Columns)
                    {
                        column.Visible = false;
                    }

                    gridViewCustomerArchiveFolders.ColumnSetup(nameof(CustomerArchiveFolder.ArchiveFolderString), isReadOnly: true, caption: "Наименование папки");
                    gridViewCustomerArchiveFolders.ColumnSetup(nameof(CustomerArchiveFolder.PeriodArchiveFolder), caption: "Периодичность");
                    gridViewCustomerArchiveFolders.GridViewSetup(isEditTable: true, isShowFooter: false);

                    var customerArchiveFolderChange = new XPCollection<ArchiveFolderChange>(_session, criteriaCustomer);
                    gridCustomerArchiveFolders.DataSource = customerArchiveFolderChange;
                    if (viewCustomerArchiveFolders.Columns[nameof(ArchiveFolderChange.Oid)] != null)
                    {
                        viewCustomerArchiveFolders.Columns[nameof(ArchiveFolderChange.Oid)].Visible = false;
                        viewCustomerArchiveFolders.Columns[nameof(ArchiveFolderChange.Oid)].Width = 18;
                        viewCustomerArchiveFolders.Columns[nameof(ArchiveFolderChange.Oid)].OptionsColumn.FixedWidth = true;
                    }


                    await BVVGlobal.oFuncXpo.RestoreLayoutToStreamAsync(gridViewCustomerArchiveFolders, $"{this.Name}_{nameof(gridViewCustomerArchiveFolders)}");
                }
                else if (e.Page == xtraTabPageCustomerEploymentHistory)
                {
                    gridControlCustomerEmploymentHistorys.DataSource = _currentCustomer.CustomerEmploymentHistorys;
                    if (gridViewCustomerEmploymentHistorys.Columns[nameof(CustomerEmploymentHistory.Oid)] != null)
                    {
                        gridViewCustomerEmploymentHistorys.Columns[nameof(CustomerEmploymentHistory.Oid)].Visible = false;
                        gridViewCustomerEmploymentHistorys.Columns[nameof(CustomerEmploymentHistory.Oid)].Width = 18;
                        gridViewCustomerEmploymentHistorys.Columns[nameof(CustomerEmploymentHistory.Oid)].OptionsColumn.FixedWidth = true;
                    }

                    await BVVGlobal.oFuncXpo.RestoreLayoutToStreamAsync(gridViewCustomerEmploymentHistorys, $"{this.Name}_{nameof(gridViewCustomerEmploymentHistorys)}");
                }
                else if (e.Page == xtraTabPageChronicle)
                {
                    _currentCustomer.ChronicleCustomers.Sorting = new SortingCollection(
                        new SortProperty(nameof(ChronicleCustomer.Date), SortingDirection.Descending)
                        );
                    gridControlChronicleCustomer.DataSource = _currentCustomer.ChronicleCustomers;

                    if (gridViewChronicleCustomer.Columns[nameof(ChronicleCustomer.Oid)] != null)
                    {
                        gridViewChronicleCustomer.Columns[nameof(ChronicleCustomer.Oid)].Visible = false;
                        gridViewChronicleCustomer.Columns[nameof(ChronicleCustomer.Oid)].Width = 18;
                        gridViewChronicleCustomer.Columns[nameof(ChronicleCustomer.Oid)].OptionsColumn.FixedWidth = true;
                    }

                    await BVVGlobal.oFuncXpo.RestoreLayoutToStreamAsync(gridViewChronicleCustomer, $"{this.Name}_{nameof(gridViewChronicleCustomer)}");
                }
            }
        }

        private void BtnLink_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }
            else if (e.Button.Kind == ButtonPredefines.Right)
            {
                var result = buttonEdit.Text;
                if (!string.IsNullOrWhiteSpace(result))
                {
                    if (!result.Contains("http://") || !result.Contains("https://") || !result.Contains("www."))
                    {
                        result = $"http://{result}";
                    }
                    System.Diagnostics.Process.Start(result);
                }
            }
        }

        private void btnBankAccessAdd_Click(object sender, EventArgs e)
        {
            if (gridViewCustomer.IsEmpty)
            {
                return;
            }

            var customer = gridViewCustomer.GetRow(gridViewCustomer.FocusedRowHandle) as Customer;
            if (customer != null)
            {
                var form = new BankAccessEdit(customer);
                form.ShowDialog();
            }
        }

        private void btnBankAccessEdit_Click(object sender, EventArgs e)
        {
            if (gridViewBankAccess.IsEmpty)
            {
                return;
            }

            var bankAccess = gridViewBankAccess.GetRow(gridViewBankAccess.FocusedRowHandle) as BankAccess;
            if (bankAccess != null)
            {
                var form = new BankAccessEdit(bankAccess);
                form.ShowDialog();
            }
        }

        private void btnBankAccessDel_Click(object sender, EventArgs e)
        {
            if (gridViewBankAccess.GetRow(gridViewBankAccess.FocusedRowHandle) is BankAccess bankAccess)
            {
                if (DevXtraMessageBox.ShowQuestionXtraMessageBox("Вы действительно хотите удалить банковский реквизит?", bankAccess))
                {
                    bankAccess.Delete();
                }
            }
        }

        private void btnTaskAdd_Click(object sender, EventArgs e)
        {
            if (gridViewCustomer.IsEmpty)
            {
                return;
            }

            var customer = gridViewCustomer.GetRow(gridViewCustomer.FocusedRowHandle) as Customer;
            if (customer != null)
            {
                var form = new TaskEdit(customer, true);
                form.ShowDialog();
            }
        }

        private void btnTaskEdit_Click(object sender, EventArgs e)
        {
            if (gridViewTasks.IsEmpty)
            {
                return;
            }

            var task = gridViewTasks.GetRow(gridViewTasks.FocusedRowHandle) as Task;
            if (task != null)
            {
                var form = new TaskEdit(task, true);
                form.ShowDialog();
            }
        }

        private void btnTaskDel_Click(object sender, EventArgs e)
        {
            if (gridViewTasks.GetRow(gridViewTasks.FocusedRowHandle) is Task obj)
            {
                if (DevXtraMessageBox.ShowQuestionXtraMessageBox("Вы действительно хотите удалить задачу?", obj))
                {
                    obj.Delete();
                }
            }
        }

        private void btnForeignEconomicActivityAdd_Click(object sender, EventArgs e)
        {
            if (gridViewCustomer.IsEmpty)
            {
                return;
            }

            var customer = gridViewCustomer.GetRow(gridViewCustomer.FocusedRowHandle) as Customer;
            if (customer != null)
            {
                var form = new ForeignEconomicActivityEdit(customer);
                form.ShowDialog();
            }
        }

        private void btnForeignEconomicActivityEdit_Click(object sender, EventArgs e)
        {
            if (gridViewForeignEconomicActivitys.IsEmpty)
            {
                return;
            }

            var foreignEconomicActivity = gridViewForeignEconomicActivitys.GetRow(gridViewForeignEconomicActivitys.FocusedRowHandle) as ForeignEconomicActivity;
            if (foreignEconomicActivity != null)
            {
                var form = new ForeignEconomicActivityEdit(foreignEconomicActivity);
                form.ShowDialog();
            }
        }

        private void btnForeignEconomicActivityDel_Click(object sender, EventArgs e)
        {
            if (gridViewForeignEconomicActivitys.GetRow(gridViewForeignEconomicActivitys.FocusedRowHandle) is ForeignEconomicActivity obj)
            {
                if (DevXtraMessageBox.ShowQuestionXtraMessageBox("Вы действительно хотите удалить ВЭД?", obj))
                {
                    obj.Delete();
                }
            }
        }

        private void btnPersonalIncomeTaxAdd_Click(object sender, EventArgs e)
        {
            if (gridViewCustomer.IsEmpty)
            {
                return;
            }

            var customer = gridViewCustomer.GetRow(gridViewCustomer.FocusedRowHandle) as Customer;
            if (customer != null)
            {
                var form = new OrganizationPerformanceEdit(customer);
                form.ShowDialog();
            }
        }

        private void btnPersonalIncomeTaxEdit_Click(object sender, EventArgs e)
        {
            if (gridViewOrganizationPerfomance.IsEmpty)
            {
                return;
            }

            var personalIncomeTax = gridViewOrganizationPerfomance.GetRow(gridViewOrganizationPerfomance.FocusedRowHandle) as OrganizationPerformance;
            if (personalIncomeTax != null)
            {
                var form = new OrganizationPerformanceEdit(personalIncomeTax);
                form.ShowDialog();
            }
        }

        private void btnPersonalIncomeTaxDel_Click(object sender, EventArgs e)
        {
            if (gridViewOrganizationPerfomance.GetRow(gridViewOrganizationPerfomance.FocusedRowHandle) is OrganizationPerformance obj)
            {
                if (DevXtraMessageBox.ShowQuestionXtraMessageBox("Вы действительно хотите удалить ежемесячную информацию?", obj))
                {
                    obj.Delete();
                }
            }
        }

        private void gridViewCustomer_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (e.MenuType == GridMenuType.User || e.MenuType == GridMenuType.Row)
                {
                    if (!gridViewCustomer.IsEmpty)
                    {
                        _customers.Reload();
                    }

                    popupMenuCustomer.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }

        /// <summary>
        /// Обновление всей информации по клиенту.
        /// </summary>
        /// <param name="customer">Клиент.</param>
        private void ReloadCustomer(Customer customer)
        {
            if (customer != null)
            {
                customer.Reload();
                customer.Tasks?.Reload();
                customer.ForeignEconomicActivities?.Reload();
                customer.PersonalIncomeTaxis?.Reload();
                customer.ChronicleCustomers?.Reload();
                customer.BankAccess?.Reload();
                customer.Invoices?.Reload();
                //customer.StatisticalReports?.Reload();
                customer.Subdivisions?.Reload();
                customer.TaxSystemCustomer?.Reload();
                customer.TaxSystemCustomer?.TaxSystem?.Reload();
                customer.TaxSystemCustomer?.TaxSystem?.TaxSystemReports?.Reload();
                //customer.CustomerArchiveFolders?.Reload();
            }
        }

        private void barBtnNotifyCustomerEmail_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewCustomer.IsEmpty)
            {
                return;
            }

            var customer = gridViewCustomer.GetRow(gridViewCustomer.FocusedRowHandle) as Customer;

            if (customer != null)
            {
                if (!string.IsNullOrWhiteSpace(customer.Email))
                {
                    //Letter.SendMailKit(customer.Email, $"Уведомление от {MailboxSetup.SenderName}", MailboxSetup.LetterCustomerTemplate);
                }
                else
                {
                    XtraMessageBox.Show("Не задан Email адрес клиента.", "Не задан Email", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void barBtnAddIndicators_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewCustomer.IsEmpty)
            {
                return;
            }

            var customer = gridViewCustomer.GetRow(gridViewCustomer.FocusedRowHandle) as Customer;
            if (customer != null)
            {
                var form = new CustomerServiceProvidedEdit(customer);
                form.ShowDialog();
            }
        }

        private void SaveValueAfterChange()
        {
            if (_isLineJump is false)
            {
                _currentCustomer.Save();
            }
        }

        private void gridViewCustomer_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView view = sender as GridView;

            if (view.Columns[nameof(Customer.OrganizationStatusString)] != null)
            {
                var colorNotTerminated = Color.FromArgb(255, 177, 241);
                var organizationStatus = view.GetRowCellValue
                            (e.RowHandle, view.Columns[nameof(Customer.OrganizationStatusString)])?.ToString();

                if (string.Compare(organizationStatus, StatusOrganization.ACTIVE.GetEnumDescription(), StringComparison.Ordinal) != 0)
                {
                    e.Appearance.BackColor = colorNotTerminated;
                }

                if (!string.IsNullOrWhiteSpace(organizationStatus) && organizationStatus.ToLower().Contains("ликви"))
                {
                    return;
                }
            }

            var customer = gridViewCustomer.GetRow(e.RowHandle) as Customer;
            if (customer != null)
            {
                if (customer.ChronicleCustomers?.FirstOrDefault(f => 
                    (f.Act == Act.UPDATING_ACTIVITIES || f.Act == Act.CHANGE_ACTIVITIES) 
                        && f.Date > DateTime.Now.AddDays(-1)) != null)
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 215, 0);
                }
                else
                {
                    if (customer.OrganizationStatus == StatusOrganization.ACTIVE)
                    {
                        var color = customer.CustomerStatus?.Status?.Color;
                        if (!string.IsNullOrWhiteSpace(color))
                        {
                            e.Appearance.BackColor = ColorTranslator.FromHtml(color);
                        }
                    }
                    else
                    {
                        e.Appearance.BackColor = Color.Red;
                    }
                }
            }
        }

        private void btnAddInvoice_Click(object sender, EventArgs e)
        {
            if (gridViewCustomer.IsEmpty)
            {
                return;
            }

            var customer = gridViewCustomer.GetRow(gridViewCustomer.FocusedRowHandle) as Customer;
            if (customer != null)
            {
                customer.Reload();
                var form = new InvoiceEdit(customer);
                form.ShowDialog();
            }
        }

        private void btnEditInvoice_Click(object sender, EventArgs e)
        {
            if (gridViewInvoice.IsEmpty)
            {
                return;
            }

            var invoice = gridViewInvoice.GetRow(gridViewInvoice.FocusedRowHandle) as Invoice;
            if (invoice != null)
            {
                invoice.Reload();
                var form = new InvoiceEdit(invoice);
                form.ShowDialog();
            }
        }

        private void btnDelInvoice_Click(object sender, EventArgs e)
        {
            if (gridViewInvoice.GetRow(gridViewInvoice.FocusedRowHandle) is Invoice obj)
            {
                if (DevXtraMessageBox.ShowQuestionXtraMessageBox("Вы действительно хотите удалить счет клиента?", obj))
                {
                    obj.Delete();
                }
            }
        }

        private void ParserReportingSystem_ErrorEvent(object sender, string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                XtraMessageBox.Show(message, "Необходимо обновить ChromeDriver", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Изменение размера поля MemoEdit в зависимости от значения.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MemoEditChangeHeight(object sender, EventArgs e)
        {
            var memoEdit = sender as MemoEdit;
            if (memoEdit != null)
            {
                var line = memoEdit.Text.Length / 40;
                var enter = memoEdit.Text.Split(new char[] { '\n', '\r' });

                var groupControl = memoEdit.Parent as GroupControl;

                if (groupControl is null)
                {
                    return;
                }

                var accordionContentContainer = groupControl.Parent as AccordionContentContainer;

                if (line >= 2 || enter.Length > 4)
                {
                    memoEdit.Height = 50;
                }
                else if (line == 1 || enter.Length > 2)
                {
                    memoEdit.Height = 35;
                }
                else
                {
                    memoEdit.Height = 20;
                }

                if (accordionContentContainer != null)
                {
                    var height = default(int);

                    foreach (var item in accordionContentContainer.Controls)
                    {
                        var size = (Size)item.GetType().InvokeMember(nameof(Size), System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.GetProperty, null, item, null);
                        height += size.Height;
                    }

                    accordionContentContainer.Size = new Size(accordionContentContainer.Size.Width, height);
                }
            }
        }

        private void btnSubdivisionAdd_Click(object sender, EventArgs e)
        {
            if (gridViewCustomer.IsEmpty)
            {
                return;
            }

            var customer = gridViewCustomer.GetRow(gridViewCustomer.FocusedRowHandle) as Customer;
            if (customer != null)
            {
                var form = new SubdivisionEdit(customer);
                form.ShowDialog();
            }
        }

        private void btnSubdivisionEdit_Click(object sender, EventArgs e)
        {
            if (gridViewSubdivisions.IsEmpty)
            {
                return;
            }

            var subdivision = gridViewSubdivisions.GetRow(gridViewSubdivisions.FocusedRowHandle) as Subdivision;
            if (subdivision != null)
            {
                var form = new SubdivisionEdit(subdivision);
                form.ShowDialog();
            }
        }

        private void btnSubdivisionDel_Click(object sender, EventArgs e)
        {
            if (gridViewSubdivisions.IsEmpty)
            {
                return;
            }

            var subdivision = gridViewSubdivisions.GetRow(gridViewSubdivisions.FocusedRowHandle) as Subdivision;
            subdivision.Delete();
        }

        private void btnTaxProperty_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (gridViewCustomer.IsEmpty)
            {
                return;
            }

            var buttonEdit = sender as ButtonEdit;

            var customer = gridViewCustomer.GetRow(gridViewCustomer.FocusedRowHandle) as Customer;
            if (customer != null)
            {
                var form = new TaxPropertyEdit(customer);
                form.ShowDialog();

                buttonEdit.EditValue = customer.Tax?.TaxProperty;

                if (customer.Tax?.TaxProperty?.IsUse is true)
                {
                    buttonEdit.Properties.ContextImageOptions.Image = imgCustomer.Images[0];
                }
                else if (customer.Tax?.TaxProperty?.IsUse is false)
                {
                    buttonEdit.Properties.ContextImageOptions.Image = imgCustomer.Images[1];
                }
                else
                {
                    buttonEdit.Properties.ContextImageOptions.Image = null;
                }
            }
        }

        private void btnTaxSingleTemporaryIncome_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (gridViewCustomer.IsEmpty)
            {
                return;
            }

            var buttonEdit = sender as ButtonEdit;

            var customer = gridViewCustomer.GetRow(gridViewCustomer.FocusedRowHandle) as Customer;
            if (customer != null)
            {
                var form = new TaxSingleTemporaryIncomeEdit(customer);
                form.ShowDialog();

                buttonEdit.EditValue = customer.Tax?.TaxSingleTemporaryIncome;

                if (customer.Tax?.TaxSingleTemporaryIncome?.IsUse is true)
                {
                    buttonEdit.Properties.ContextImageOptions.Image = imgCustomer.Images[0];
                }
                else if (customer.Tax?.TaxSingleTemporaryIncome?.IsUse is false)
                {
                    buttonEdit.Properties.ContextImageOptions.Image = imgCustomer.Images[1];
                }
                else
                {
                    buttonEdit.Properties.ContextImageOptions.Image = null;
                }
            }
        }

        private void btnTaxAlcohol_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (gridViewCustomer.IsEmpty)
            {
                return;
            }

            var buttonEdit = sender as ButtonEdit;

            var customer = gridViewCustomer.GetRow(gridViewCustomer.FocusedRowHandle) as Customer;
            if (customer != null)
            {
                var form = new TaxAlcoholEdit(customer);
                form.ShowDialog();

                buttonEdit.EditValue = customer.Tax?.TaxAlcohol;

                if (customer.Tax?.TaxAlcohol?.IsUse is true)
                {
                    buttonEdit.Properties.ContextImageOptions.Image = imgCustomer.Images[0];
                }
                else if (customer.Tax?.TaxAlcohol?.IsUse is false)
                {
                    buttonEdit.Properties.ContextImageOptions.Image = imgCustomer.Images[1];
                }
                else
                {
                    buttonEdit.Properties.ContextImageOptions.Image = null;
                }
            }
        }

        private void btnTaxTransport_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (gridViewCustomer.IsEmpty)
            {
                return;
            }

            var buttonEdit = sender as ButtonEdit;

            var customer = gridViewCustomer.GetRow(gridViewCustomer.FocusedRowHandle) as Customer;
            if (customer != null)
            {
                var form = new TaxTransportEdit(customer);
                form.ShowDialog();

                buttonEdit.EditValue = customer.Tax?.TaxTransport;

                if (customer.Tax?.TaxTransport?.IsUse is true)
                {
                    buttonEdit.Properties.ContextImageOptions.Image = imgCustomer.Images[0];
                }
                else if (customer.Tax?.TaxTransport?.IsUse is false)
                {
                    buttonEdit.Properties.ContextImageOptions.Image = imgCustomer.Images[1];
                }
                else
                {
                    buttonEdit.Properties.ContextImageOptions.Image = null;
                }
            }
        }

        private void btnTaxLand_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (gridViewCustomer.IsEmpty)
            {
                return;
            }

            var buttonEdit = sender as ButtonEdit;

            var customer = gridViewCustomer.GetRow(gridViewCustomer.FocusedRowHandle) as Customer;
            if (customer != null)
            {
                var form = new TaxLandEdit(customer);
                form.ShowDialog();

                buttonEdit.EditValue = customer.Tax?.TaxLand;

                if (customer.Tax?.TaxLand?.IsUse is true)
                {
                    buttonEdit.Properties.ContextImageOptions.Image = imgCustomer.Images[0];
                }
                else if (customer.Tax?.TaxLand?.IsUse is false)
                {
                    buttonEdit.Properties.ContextImageOptions.Image = imgCustomer.Images[1];
                }
                else
                {
                    buttonEdit.Properties.ContextImageOptions.Image = null;
                }
            }
        }

        private void btnTaxIndirect_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (gridViewCustomer.IsEmpty)
            {
                return;
            }

            var buttonEdit = sender as ButtonEdit;

            var customer = gridViewCustomer.GetRow(gridViewCustomer.FocusedRowHandle) as Customer;
            if (customer != null)
            {
                var form = new TaxIndirectEdit(customer);
                form.ShowDialog();

                buttonEdit.EditValue = customer.Tax?.TaxIndirect;

                if (customer.Tax?.TaxIndirect?.IsUse is true)
                {
                    buttonEdit.Properties.ContextImageOptions.Image = imgCustomer.Images[0];
                }
                else if (customer.Tax?.TaxIndirect?.IsUse is false)
                {
                    buttonEdit.Properties.ContextImageOptions.Image = imgCustomer.Images[1];
                }
                else
                {
                    buttonEdit.Properties.ContextImageOptions.Image = null;
                }
            }
        }

        private void btnLeasing_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (gridViewCustomer.IsEmpty)
            {
                return;
            }

            var buttonEdit = sender as ButtonEdit;

            var customer = gridViewCustomer.GetRow(gridViewCustomer.FocusedRowHandle) as Customer;
            if (customer != null)
            {
                var form = new LeasingEdit(customer);
                form.ShowDialog();

                buttonEdit.EditValue = customer.Leasing;

                if (customer.Leasing?.IsUse is true)
                {
                    buttonEdit.Properties.ContextImageOptions.Image = imgCustomer.Images[0];
                }
                else if (customer.Leasing?.IsUse is false)
                {
                    buttonEdit.Properties.ContextImageOptions.Image = imgCustomer.Images[1];
                }
                else
                {
                    buttonEdit.Properties.ContextImageOptions.Image = null;
                }
            }
        }

        private void HideAccordion(AccordionControlState accordionControlState, int? witdhAccordion = null)
        {
            try
            {
                if (witdhAccordion is int x)
                {
                    splitterItemInfo.Location = new Point(x, splitterItemInfo.Location.Y);
                    return;
                }

                if (accordionControlState == AccordionControlState.Minimized)
                {
                    splitterItemInfo.Location = new Point(Size.Width - 30, splitterItemInfo.Location.Y);
                }
                else
                {
                    splitterItemInfo.Location = new Point(Size.Width - 550, splitterItemInfo.Location.Y);
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async void accordCustomer_StateChanged(object sender, EventArgs e)
        {
            try
            {
                HideAccordion(accordCustomer.OptionsMinimizing.State);
                await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"{this.Name}_{nameof(accordCustomer)}_{nameof(accordCustomer.OptionsMinimizing.State)}", ((int)accordCustomer.OptionsMinimizing.State).ToString(), true, true, 1, user: BVVGlobal.oApp.User);
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private void btnCustomerArchiveFolderAdd_Click(object sender, EventArgs e)
        {
            if (gridViewCustomer.IsEmpty)
            {
                return;
            }

            var customer = gridViewCustomer.GetRow(gridViewCustomer.FocusedRowHandle) as Customer;

            if (customer != null)
            {
                if (btnCustomerArchiveFolder.Checked)
                {
                    var archiveFolderOid = cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.ArchiveFolder, -1);
                    if (archiveFolderOid > 0)
                    {
                        var archiveFolder = _session.GetObjectByKey<ArchiveFolder>(archiveFolderOid);

                        if (archiveFolder != null)
                        {
                            customer.CustomerArchiveFolders.Add(new CustomerArchiveFolder(_session)
                            {
                                ArchiveFolder = archiveFolder,
                                PeriodArchiveFolder = archiveFolder.PeriodArchiveFolder
                            });
                            customer.Save();
                        }
                    }
                }
                else
                {
                    var form = new ArchiveFolderChangeEdit(customer);
                    form.ShowDialog();

                    if (form.ArchiveFolderChange != null && form.ArchiveFolderChange.Oid > 0)
                    {
                        ((XPCollection<ArchiveFolderChange>)gridCustomerArchiveFolders.DataSource).Reload();
                        viewCustomerArchiveFolders.FocusedRowHandle = viewCustomerArchiveFolders.LocateByValue(nameof(ArchiveFolderChange.Oid), form.ArchiveFolderChange.Oid);
                    }
                }
            }
        }

        private void btnCustomerArchiveFolderDel_Click(object sender, EventArgs e)
        {
            if (btnCustomerArchiveFolder.Checked)
            {
                var customerArchiveFolder = gridViewCustomerArchiveFolders.GetRow(gridViewCustomerArchiveFolders.FocusedRowHandle) as CustomerArchiveFolder;
                if (XtraMessageBox.Show($"Вы действительно хотите удалить архивную папку: [{customerArchiveFolder}] у клиента {customerArchiveFolder.Customer}?",
                                            "Удаление архивной папки",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Question) == DialogResult.OK)
                {
                    if (customerArchiveFolder != null)
                    {
                        customerArchiveFolder.Delete();
                        customerArchiveFolder.Save();

                        XtraMessageBox.Show($"Архивная папка: {customerArchiveFolder} успешно удалена у клиента {customerArchiveFolder.Customer}",
                            "Удаление архивной папки",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                var archiveFolderChange = viewCustomerArchiveFolders.GetRow(viewCustomerArchiveFolders.FocusedRowHandle) as ArchiveFolderChange;
                if (XtraMessageBox.Show($"Вы действительно хотите удалить архивную папку: {archiveFolderChange} у клиента {archiveFolderChange.Customer}?",
                                            "Удаление архивной папки",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Question) == DialogResult.OK)
                {
                    if (archiveFolderChange != null)
                    {
                        archiveFolderChange.Delete();
                        archiveFolderChange.Save();

                        XtraMessageBox.Show($"Архивная папка: {archiveFolderChange} успешно удалена у клиента {archiveFolderChange.Customer}",
                            "Удаление архивной папки",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnCustomerArchiveFolderCopy_Click(object sender, EventArgs e)
        {
            if (gridViewCustomer.IsEmpty)
            {
                return;
            }

            var customer = gridViewCustomer.GetRow(gridViewCustomer.FocusedRowHandle) as Customer;

            if (customer != null)
            {

                if (XtraMessageBox.Show($"Вы действительно хотите скопировать все архивные папки клиенту: {customer}?",
                                                "Копирование архивных папок",
                                                MessageBoxButtons.OKCancel,
                                                MessageBoxIcon.Question) == DialogResult.OK)
                {
                    var xpcollectionArchiveFolder = new XPCollection<ArchiveFolder>(_session);

                    foreach (var archiveFolder in xpcollectionArchiveFolder)
                    {
                        if (customer.CustomerArchiveFolders.FirstOrDefault(f => f.ArchiveFolder == archiveFolder) is null)
                        {
                            customer.CustomerArchiveFolders.Add(new CustomerArchiveFolder(_session)
                            {
                                ArchiveFolder = archiveFolder,
                                PeriodArchiveFolder = archiveFolder.PeriodArchiveFolder
                            });
                        }
                    }
                    customer.Save();
                }
            }
        }

        private async void barBtnSaveLayoutToXmlMainGrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            await SaveSettingsForms();
        }

        private void btnPrintInfoCard_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewCustomer.IsEmpty)
            {
                return;
            }

            var customer = gridViewCustomer.GetRow(gridViewCustomer.FocusedRowHandle) as Customer;
            _ = new InfoCard(customer);
        }

        private void btnCustomerSettings_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new formProgramSettings(3);
            form.ShowDialog();

            UpdatePropertiesXPCollection(true);
        }

        private void btnReportAdd_Click(object sender, EventArgs e)
        {
            if (gridViewCustomer.IsEmpty)
            {
                return;
            }

            var customer = gridViewCustomer.GetRow(gridViewCustomer.FocusedRowHandle) as Customer;
            if (customer != null)
            {
                if (btnReportCustomer.Checked)
                {
                    var id = cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.Report);

                    if (id > 0)
                    {
                        var report = _session.GetObjectByKey<Report>(id);

                        if (report != null)
                        {
                            if (customer.CustomerReports.FirstOrDefault(f => f.Report == report) == null)
                            {
                                customer.CustomerReports.Add(new CustomerReport(_session)
                                {
                                    Report = report
                                });
                                customer.Save();
                            }
                        }
                    }
                }
                else
                {
                    var form = new ReportChangeEdit(customer);
                    form.ShowDialog();

                    if (form.ReportChange != null && form.ReportChange.Oid > 0)
                    {
                        ((XPCollection<ReportChange>)gridCustomerReports.DataSource).Reload();
                        viewCustomerReports.FocusedRowHandle = viewCustomerReports.LocateByValue(nameof(PreTax.Oid), form.ReportChange.Oid);
                    }
                }
            }
        }

        private void btnReportEdit_Click(object sender, EventArgs e)
        {
            if (viewCustomerReports.IsEmpty)
            {
                return;
            }

            if (btnReportCustomer.Checked)
            {
                var customerReport = viewCustomerReports.GetRow(viewCustomerReports.FocusedRowHandle) as CustomerReport;
                if (customerReport != null && customerReport.Report != null)
                {
                    var form = new ReportEdit(customerReport.Report);
                    form.ShowDialog();
                }
            }
            else
            {
                var reportChange = viewCustomerReports.GetRow(viewCustomerReports.FocusedRowHandle) as ReportChange;
                if (reportChange != null)
                {
                    var form = new ReportChangeEdit(reportChange);
                    form.ShowDialog();

                    if (form.ReportChange != null && form.ReportChange.Oid > 0)
                    {
                        ((XPCollection<ReportChange>)gridCustomerReports.DataSource).Reload();
                        viewCustomerReports.FocusedRowHandle = viewCustomerReports.LocateByValue(nameof(PreTax.Oid), form.ReportChange.Oid);
                    }
                }
            }
        }

        private void btnReportDel_Click(object sender, EventArgs e)
        {
            if (gridViewCustomerReports.IsEmpty)
            {
                return;
            }

            if (btnReportCustomer.Checked)
            {
                var customerReport = gridViewCustomerReports.GetRow(gridViewCustomerReports.FocusedRowHandle) as CustomerReport;

                if (XtraMessageBox.Show($"Вы действительно хотите удалить отчет {customerReport} у клиента {customerReport.Customer}?",
                            "Удаление отчета",
                            MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Question) == DialogResult.OK)
                {
                    if (customerReport != null)
                    {
                        customerReport.Delete();
                        customerReport.Save();

                        XtraMessageBox.Show($"Отчет {customerReport} успешно удален у клиента {customerReport.Customer}",
                            "Удаление отчета",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                var reportChange = viewCustomerReports.GetRow(viewCustomerReports.FocusedRowHandle) as ReportChange;
                if (XtraMessageBox.Show($"Вы действительно хотите удалить отчет {reportChange} у клиента {reportChange.Customer}?",
                            "Удаление отчета",
                            MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Question) == DialogResult.OK)
                {
                    if (reportChange != null)
                    {
                        reportChange.Delete();
                        reportChange.Save();

                        XtraMessageBox.Show($"Отчет {reportChange} успешно удален у клиента {reportChange.Customer}",
                            "Удаление отчета",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
        }

        private async void btnReportCopyWithTaxSystemReport_Click(object sender, EventArgs e)
        {
            if (gridViewCustomer.IsEmpty)
            {
                return;
            }
            
            if (gridViewCustomer.GetRow(gridViewCustomer.FocusedRowHandle) is Customer _customer)
            {
                if (XtraMessageBox.Show($"Вы действительно хотите произвести автоматическое заполнение отчетов из системы налогообложения?",
                            "Удаление отчета",
                            MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Question) == DialogResult.OK)
                {
                    using (var uof = new UnitOfWork())
                    {
                        var customer = await new XPQuery<Customer>(uof).FirstOrDefaultAsync(f => f.Oid == _customer.Oid);

                        if (customer != null)
                        {
                            customer.TaxSystemCustomer?.TaxSystem?.TaxSystemReports?.Reload();
                            customer.FormCorporation?.FormCorporationReports?.Reload();

                            var taxSystemReports = customer.TaxSystemCustomer?.TaxSystem?.TaxSystemReports;
                            var formCorporationReports = customer.FormCorporation?.FormCorporationReports;
                            if (taxSystemReports != null)
                            {
                                foreach (var taxSystemReport in taxSystemReports)
                                {
                                    if (formCorporationReports.Count > 0)
                                    {
                                        if (formCorporationReports.FirstOrDefault(f => f.Report == taxSystemReport.Report) != null)
                                        {
                                            if (customer.CustomerReports.FirstOrDefault(f => f.Report == taxSystemReport.Report) == null)
                                            {
                                                customer.CustomerReports.Add(new CustomerReport(uof)
                                                {
                                                    Report = taxSystemReport.Report
                                                });
                                            }
                                        }
                                        else
                                        {
                                            var customerReport = customer.CustomerReports.FirstOrDefault(f => f.Report == taxSystemReport.Report);
                                            if (customerReport != null)
                                            {
                                                customer.CustomerReports.Remove(customerReport);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (customer.CustomerReports.FirstOrDefault(f => f.Report == taxSystemReport.Report) == null)
                                        {
                                            customer.CustomerReports.Add(new CustomerReport(uof)
                                            {
                                                Report = taxSystemReport.Report
                                            });
                                        }
                                    }
                                }

                                customer.Save();

                                await uof.CommitTransactionAsync();

                                XtraMessageBox.Show($"Список отчетов клиента {customer} успешно обновлен.",
                                    "Обновление списка отчетов",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                            }
                        }
                    }

                    _customer.CustomerReports?.Reload();
                }
            }
        }

        private void btnReportCustomer_CheckedChanged(object sender, EventArgs e)
        {
            var checkButton = sender as CheckButton;

            if (checkButton != null)
            {
                gridCustomerReports.Visible = !checkButton.Checked;
                gridControlCustomerReports.Visible = checkButton.Checked;
                btnReportCopyWithTaxSystemReport.Visible = checkButton.Checked;

                if (!checkButton.Checked)
                {
                    btnReportDel.Enabled = isDeleteReportChangeForm;
                    btnReportDel.Visible = isDeleteReportChangeForm;
                }
                else
                {
                    btnReportDel.Enabled = true;
                    btnReportDel.Visible = true;
                }
            }
        }

        private void btnCustomerArchiveFolder_CheckedChanged(object sender, EventArgs e)
        {
            var checkButton = sender as CheckButton;

            if (checkButton != null)
            {
                gridCustomerArchiveFolders.Visible = !checkButton.Checked;
                gridControlCustomerArchiveFolders.Visible = checkButton.Checked;
                btnCustomerArchiveFolderCopy.Visible = checkButton.Checked;
            }
        }

        private async void btnUpdateKindActivity_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show($"Провести обновление видов деятельности по всем клиентам? (Обновление будет происходить при наличие у клиента ОКВЭД).",
                            "Обновление видов деятельности",
                            MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Question) == DialogResult.OK)
            {
                await System.Threading.Tasks.Task.Run(async () =>
                {
                    await GetKindActivityAsync();
                });
            }
        }

        private static async System.Threading.Tasks.Task GetKindActivityAsync()
        {
            var countUpdate = default(int);
            var editUpdate = default(int);

            using (var uof = new UnitOfWork())
            {
                var customers = await new XPQuery<Customer>(uof)
                    .Where(w => w.OKVED != null)
                    .ToListAsync();

                foreach (var customer in customers)
                {
                    if (!string.IsNullOrWhiteSpace(customer.OKVED))
                    {
                        var count = await UpdateKindActivityAsync(uof, customer);
                        editUpdate += count;
                        countUpdate++;
                    }
                }
            }

            XtraMessageBox.Show($"Количество проверенных клиентов: {countUpdate}.{Environment.NewLine}" +
                                    $"Обновлено клиентов: {editUpdate}",
                                "Обновление видов деятельности",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
        }

        private static async System.Threading.Tasks.Task<int> UpdateKindActivityAsync(UnitOfWork uof, Customer customer)
        {
            var result = 0;
            var classOKVED2 = await new XPQuery<ClassOKVED2>(uof).FirstOrDefaultAsync(f => f.Code == customer.OKVED);

            if (classOKVED2 is null)
            {
                customer.OKVED = customer.OKVED?.Replace(",", ".")?.Trim();
                customer.Save();

                var okvedRecord = GetInfoOrganizationFromDaData.GetOkved2("a8cfa20b99fb660c53b8e0fa2d3de0f2204fb580", customer.OKVED);
                if (okvedRecord != null)
                {
                    var sectionOKVED2 = await new XPQuery<SectionOKVED2>(uof).FirstOrDefaultAsync(f => f.Code == okvedRecord.razdel);

                    if (sectionOKVED2 is null)
                    {
                        sectionOKVED2 = new SectionOKVED2(uof)
                        {
                            Code = okvedRecord.razdel,
                            Name = $"Раздел {okvedRecord.razdel}"
                        };
                        sectionOKVED2.Save();

                        await uof.CommitTransactionAsync();
                    }

                    classOKVED2 = sectionOKVED2.ClassesOKVED.FirstOrDefault(f => f.Code.Equals(okvedRecord.kod));
                    if (classOKVED2 is null)
                    {
                        classOKVED2 = new ClassOKVED2(uof)
                        {
                            Code = okvedRecord.kod,
                            Name = okvedRecord.name
                        };
                        classOKVED2.Save();
                        sectionOKVED2.ClassesOKVED.Add(classOKVED2);

                        await uof.CommitTransactionAsync();
                    }
                }
            }

            if (customer.KindActivity is null)
            {
                customer.KindActivity = new KindActivity(uof);
            }

            var chronicleCustomer = default(ChronicleCustomer);

            if (customer.KindActivity?.ClassOKVED2 is null)
            {
                chronicleCustomer = new ChronicleCustomer(uof)
                {
                    Act = Act.UPDATING_ACTIVITIES,
                    Date = DateTime.Now,
                    Description = $"Пользователь [{customer.Session.GetObjectByKey<User>(DatabaseConnection.User.Oid)}] обновил вид деятельности клиента с помощью функции обновления видов деятельности.",
                    User = await uof.GetObjectByKeyAsync<User>(DatabaseConnection.User.Oid)
                };

                chronicleCustomer.Save();
                customer.ChronicleCustomers.Add(chronicleCustomer);

                result++;
            }
            else if (customer.KindActivity?.ClassOKVED2?.Oid != classOKVED2?.Oid)
            {
                chronicleCustomer = new ChronicleCustomer(uof)
                {
                    Act = Act.CHANGE_ACTIVITIES,
                    Date = DateTime.Now,
                    Description = $"Пользователь [{customer.Session.GetObjectByKey<User>(DatabaseConnection.User.Oid)}] изменил вид деятельности клиента с помощью функции обновления видов деятельности.",
                    User = await uof.GetObjectByKeyAsync<User>(DatabaseConnection.User.Oid)
                };

                chronicleCustomer.Save();
                customer.ChronicleCustomers.Add(chronicleCustomer);

                result++;
            }

            customer.KindActivity.ClassOKVED2 = classOKVED2;
            customer.KindActivity.Save();
            customer.Save();

            await uof.CommitTransactionAsync();

            return result;
        }

        private async void btnUpdateIsLegalAddress_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show($"Провести заполнение юридических адресов по всем клиентам? (Обновление будет происходить при наличие у клиента ИНН).",
                            "Заполнение юридических адресов",
                            MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Question) == DialogResult.OK)
            {
                var result = default(string);
                var countUpdate = default(int);

                using (var uof = new UnitOfWork())
                {
                    var customers = await new XPQuery<Customer>(uof)
                        .Where(w => w.INN != null)
                        .ToListAsync();

                    foreach (var customer in customers)
                    {
                        var getInfoFromDaData = new GetInfoOrganizationFromDaData("a8cfa20b99fb660c53b8e0fa2d3de0f2204fb580", "080aefa3543cb56dfe122f26a16a04703cacb128", customer.INN);
                        await getInfoFromDaData.GetDataAsync();

                        if (!string.IsNullOrWhiteSpace(getInfoFromDaData.Address))
                        {
                            if (customer.CustomerAddress.FirstOrDefault(f => f.IsLegal) == null)
                            {
                                var suggestResponse = GetInfoAddressFromDaData.UpdateFromDaData("a8cfa20b99fb660c53b8e0fa2d3de0f2204fb580", getInfoFromDaData.Address);
                                var addressString = suggestResponse?.suggestions[0]?.unrestricted_value;

                                if (!string.IsNullOrWhiteSpace(addressString))
                                {
                                    var customerAddress = new CustomerAddress(uof)
                                    {
                                        IsActual = true,
                                        IsLegal = true,
                                        AddressString = addressString
                                    };
                                    customerAddress.Save();
                                    customer.CustomerAddress.Add(customerAddress);
                                    customer.Save();

                                    await uof.CommitTransactionAsync().ConfigureAwait(false);

                                    result += $"[{customer.INN}] - {customer}{Environment.NewLine}";
                                    countUpdate++;
                                }
                            }
                        }
                    }
                }

                var message = $"Количество обновленных клиентов: {countUpdate}";
                if (!string.IsNullOrWhiteSpace(result))
                {
                    message += $"{Environment.NewLine}{result}";
                }

                XtraMessageBox.Show(message,
                                    "Заполнение юридических адресов",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
            }
        }

        private void btnOneEsBookkeeping_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (gridViewCustomer.IsEmpty)
            {
                return;
            }

            var buttonEdit = sender as ButtonEdit;

            var customer = gridViewCustomer.GetRow(gridViewCustomer.FocusedRowHandle) as Customer;
            if (customer != null)
            {
                var form = new OneEsBookkeepingEdit(customer);
                form.ShowDialog();

                buttonEdit.EditValue = customer.OneEs?.OneEsBookkeeping;

                if (customer.OneEs?.OneEsBookkeeping?.IsUse is true)
                {
                    buttonEdit.Properties.ContextImageOptions.Image = imgCustomer.Images[0];
                }
                else if (customer.OneEs?.OneEsBookkeeping?.IsUse is false)
                {
                    buttonEdit.Properties.ContextImageOptions.Image = imgCustomer.Images[1];
                }
                else
                {
                    buttonEdit.Properties.ContextImageOptions.Image = null;
                }
            }
        }

        private void btnOneEsSalary_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (gridViewCustomer.IsEmpty)
            {
                return;
            }

            var buttonEdit = sender as ButtonEdit;

            var customer = gridViewCustomer.GetRow(gridViewCustomer.FocusedRowHandle) as Customer;
            if (customer != null)
            {
                var form = new OneEsSalaryEdit(customer);
                form.ShowDialog();

                buttonEdit.EditValue = customer.OneEs?.OneEsSalary;

                if (customer.OneEs?.OneEsSalary?.IsUse is true)
                {
                    buttonEdit.Properties.ContextImageOptions.Image = imgCustomer.Images[0];
                }
                else if (customer.OneEs?.OneEsSalary?.IsUse is false)
                {
                    buttonEdit.Properties.ContextImageOptions.Image = imgCustomer.Images[1];
                }
                else
                {
                    buttonEdit.Properties.ContextImageOptions.Image = null;
                }
            }
        }

        private void btnOneEsOther_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (gridViewCustomer.IsEmpty)
            {
                return;
            }

            var buttonEdit = sender as ButtonEdit;

            var customer = gridViewCustomer.GetRow(gridViewCustomer.FocusedRowHandle) as Customer;
            if (customer != null)
            {
                var form = new OneEsOtherEdit(customer);
                form.ShowDialog();

                buttonEdit.EditValue = customer.OneEs?.OneEsOther;

                if (customer.OneEs?.OneEsOther?.IsUse is true)
                {
                    buttonEdit.Properties.ContextImageOptions.Image = imgCustomer.Images[0];
                }
                else if (customer.OneEs?.OneEsOther?.IsUse is false)
                {
                    buttonEdit.Properties.ContextImageOptions.Image = imgCustomer.Images[1];
                }
                else
                {
                    buttonEdit.Properties.ContextImageOptions.Image = null;
                }
            }
        }

        private void viewCustomerReports_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView view = sender as GridView;

            if (view.Columns[nameof(ReportChange.StatusString)] != null)
            {
                var statusReport = view.GetRowCellValue
                            (e.RowHandle, view.Columns[nameof(ReportChange.StatusString)])?.ToString();

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
                }
            }
        }

        private void viewCustomerArchiveFolders_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView view = sender as GridView;

            if (view.Columns[nameof(ArchiveFolderChange.StatusArchiveFolderString)] != null)
            {
                var statusArchiveFolderChange = view.GetRowCellValue
                            (e.RowHandle, view.Columns[nameof(ArchiveFolderChange.StatusArchiveFolderString)])?.ToString();

                if (!string.IsNullOrWhiteSpace(statusArchiveFolderChange))
                {
                    if (statusArchiveFolderChange.Equals(StatusArchiveFolder.DONE.GetEnumDescription()))
                    {
                        e.Appearance.BackColor = StatusArchiveFolderColor.ColorStatusArchiveFolderDone;
                        //e.Appearance.BackColor2 = Color.White;
                    }
                    else if (statusArchiveFolderChange.Equals(StatusArchiveFolder.ISCOMPLETED.GetEnumDescription()))
                    {
                        e.Appearance.BackColor = StatusArchiveFolderColor.ColorStatusArchiveFolderIsCompleted;
                        //e.Appearance.BackColor2 = Color.White;
                    }
                    else if (statusArchiveFolderChange.Equals(StatusArchiveFolder.ISSUED.GetEnumDescription()))
                    {
                        e.Appearance.BackColor = StatusArchiveFolderColor.ColorStatusArchiveFolderIssued;
                        //e.Appearance.BackColor2 = Color.White;
                    }
                    else if (statusArchiveFolderChange.Equals(StatusArchiveFolder.NEW.GetEnumDescription()))
                    {
                        e.Appearance.BackColor = StatusArchiveFolderColor.ColorStatusArchiveFolderNew;
                        //e.Appearance.BackColor2 = Color.White;
                    }
                    else if (statusArchiveFolderChange.Equals(StatusArchiveFolder.SORTOUT.GetEnumDescription()))
                    {
                        e.Appearance.BackColor = StatusArchiveFolderColor.ColorStatusArchiveFolderSortout;
                        //e.Appearance.BackColor2 = Color.White;
                    }
                    else if (statusArchiveFolderChange.Equals(StatusArchiveFolder.RETURNED.GetEnumDescription()))
                    {
                        e.Appearance.BackColor = StatusArchiveFolderColor.ColorStatusArchiveFolderReturned;
                        //e.Appearance.BackColor2 = Color.White;
                    }
                    else if (statusArchiveFolderChange.Equals(StatusArchiveFolder.ISSUEDEDS.GetEnumDescription()))
                    {
                        e.Appearance.BackColor = StatusArchiveFolderColor.ColorStatusArchiveFolderIsSuedEDS;
                        //e.Appearance.BackColor2 = Color.White;
                    }
                    else if (statusArchiveFolderChange.Equals(StatusArchiveFolder.RECEIVEDEDS.GetEnumDescription()))
                    {
                        e.Appearance.BackColor = StatusArchiveFolderColor.ColorStatusArchiveFolderReceivedEDS;
                        //e.Appearance.BackColor2 = Color.White;
                    }
                    else if (statusArchiveFolderChange.Equals(StatusArchiveFolder.RECEIVEDTK.GetEnumDescription()))
                    {
                        e.Appearance.BackColor = StatusArchiveFolderColor.ColorStatusArchiveFolderIsSuedTK;
                        //e.Appearance.BackColor2 = Color.White;
                    }
                    else if (statusArchiveFolderChange.Equals(StatusArchiveFolder.ISSUEDTK.GetEnumDescription()))
                    {
                        e.Appearance.BackColor = StatusArchiveFolderColor.ColorStatusArchiveFolderReceivedTK;
                        //e.Appearance.BackColor2 = Color.White;
                    }
                }
            }
        }

        private void txtFilter_EditValueChanged(object sender, EventArgs e)
        {
            var tetxEdit = sender as TextEdit;

            if (tetxEdit != null)
            {
                gridViewCustomer.FindFilterText = tetxEdit.Text;
                if (!string.IsNullOrWhiteSpace(tetxEdit.Text))
                {
                    gridViewCustomer_FocusedRowChanged(gridViewCustomer, null);
                }
            }
        }

        private void btnCustomerEmploymentHistoryAdd_Click(object sender, EventArgs e)
        {
            if (gridViewCustomer.IsEmpty)
            {
                return;
            }

            var customer = gridViewCustomer.GetRow(gridViewCustomer.FocusedRowHandle) as Customer;
            if (customer != null)
            {
                var form = new EmploymentHistoryEdit(customer);
                form.ShowDialog();
            }
        }

        private void btnCustomerEmploymentHistoryEdit_Click(object sender, EventArgs e)
        {
            if (gridViewCustomerEmploymentHistorys.IsEmpty)
            {
                return;
            }

            var customerEmploymentHistory = gridViewCustomerEmploymentHistorys.GetRow(gridViewCustomerEmploymentHistorys.FocusedRowHandle) as CustomerEmploymentHistory;
            if (customerEmploymentHistory != null)
            {
                var form = new EmploymentHistoryEdit(customerEmploymentHistory);
                form.ShowDialog();
            }
        }

        private void btnCustomerEmploymentHistoryDel_Click(object sender, EventArgs e)
        {
            if (gridViewCustomerEmploymentHistorys.IsEmpty)
            {
                return;
            }

            var customerEmploymentHistory = gridViewCustomerEmploymentHistorys.GetRow(gridViewCustomerEmploymentHistorys.FocusedRowHandle) as CustomerEmploymentHistory;
            if (XtraMessageBox.Show($"Вы действительно хотите удалить трудовую книжку: {customerEmploymentHistory} у клиента {customerEmploymentHistory.Customer}?",
                                        "Удаление архивной трудовой книжки",
                                        MessageBoxButtons.OKCancel,
                                        MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (customerEmploymentHistory != null)
                {
                    customerEmploymentHistory.Delete();
                    customerEmploymentHistory.Save();

                    XtraMessageBox.Show($"Трудовая книжка: {customerEmploymentHistory} успешно удалена у клиента {customerEmploymentHistory.Customer}",
                        "Удаление архивной трудовой книжки",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// Редактирование нюансов учета.
        /// </summary>
        private bool IsCustomerNotesEdit;

        /// <summary>
        /// Сохранение нюансов учета перед изменением.
        /// </summary>
        private byte[] CustomerNotes;

        /// <summary>
        /// Список вложений на удаление.
        /// </summary>
        private List<CustomerAttachment> ListCustomerAttachment = new List<CustomerAttachment>();

        private void btnCustomerNotesEdit_Click(object sender, EventArgs e)
        {
            if (!IsCustomerNotesEdit)
            {
                XPBaseObject.AutoSaveOnEndEdit = false;

                btnCustomerNotesEdit.Text = "Идет редактирование ...";
                btnCustomerNotesEdit.Enabled = false;

                btnAddLetterAttachment.Visibility = BarItemVisibility.Always;
                btnDelLetterAttachment.Visibility = BarItemVisibility.Always;

                btnAttachmentAdd.Visible = true;
                btnCustomerNotesSave.Visible = true;
                btnCustomerNotesCancel.Visible = true;

                CustomerNotes = Letter.StringToByte(richEditNotes.HtmlText);
                richEditNotes.ReadOnly = false;
                gridControlCustomer.Enabled = false;

                IsCustomerNotesEdit = true;
            }
        }

        private static Regex urlRegex = new Regex(@"((?:[a-z][\w-]+:(?:/{1,3}|[a-z0-9%])|www\d{0,3}[.]|ftp[.]|[a-z0-9.\-]+[.][a-z]{2,4}/)(?:[^\s()<>]+|\(([^\s()<>]+|(\([^\s()<>]+\)))*\))+(?:\(([^\s()<>]+|(\([^\s()<>]+\)))*\)|[^\s`!()\[\]{};:'"".,<>?«»“”‘’]))", RegexOptions.IgnoreCase);

        private bool RangeHasHyperlink(DocumentRange documentRange)
        {
            foreach (Hyperlink h in richEditNotes.Document.Hyperlinks)
            {
                if (documentRange.Contains(h.Range.Start))
                    return true;
            }

            return false;
        }

        private void SaveCustomerNotes(bool isSave)
        {
            var customer = gridViewCustomer.GetRow(gridViewCustomer.FocusedRowHandle) as Customer;

            if (isSave)
            {
                if (!gridViewCustomer.IsEmpty)
                {
                    if (customer != null)
                    {
                        customer.Notes = null;

                        var doc = richEditNotes.Document;

                        try
                        {
                            doc.BeginUpdate();
                            DocumentRange[] urls = richEditNotes.Document.FindAll(urlRegex);

                            for (int i = urls.Length - 1; i >= 0; i--)
                            {
                                if (RangeHasHyperlink(urls[i]))
                                    continue;

                                Hyperlink hyperlink = doc.Hyperlinks.Create(urls[i]);
                                hyperlink.NavigateUri = doc.GetText(hyperlink.Range);
                            }
                            doc.EndUpdate();
                        }
                        catch (Exception)
                        {

                        }
                        customer.NoteByte = Letter.StringToByte(richEditNotes.HtmlText);

                        foreach (var attachment in ListCustomerAttachment)
                        {
                            attachment.Delete();
                        }

                        customer.ChronicleCustomers.Add(new ChronicleCustomer(customer.Session)
                        {
                            Act = Act.UPDATING_ACCOUNTING_NUANCES,
                            Date = DateTime.Now,
                            Description = $"Пользователь [{customer.Session.GetObjectByKey<User>(DatabaseConnection.User.Oid)}] обновил нюансы учета.",
                            User = customer.Session.GetObjectByKey<User>(DatabaseConnection.User.Oid)
                        });

                        customer.Save();

                        var chronicleCustomer = _currentCustomer.ChronicleCustomers.LastOrDefault(l => l.Act == Act.UPDATING_ACCOUNTING_NUANCES);
                        if (chronicleCustomer != null)
                        {
                            lblUserUpdateNotes.Text = $"Пользователь [{chronicleCustomer.User}] внес изменения ({chronicleCustomer.Date.ToShortTimeString()}) {chronicleCustomer.Date.ToShortDateString()}";
                        }
                    }
                }
            }
            else
            {
                richEditNotes.HtmlText = Letter.ByteToString(CustomerNotes);
                ListCustomerAttachment = new List<CustomerAttachment>();
                if (customer != null)
                {
                    //customer.Attachments.Reload();

                    if (customer.Attachments != null && customer.Attachments.Count > 0)
                    {
                        splitContainerNotes.PanelVisibility = SplitPanelVisibility.Both;
                        splitContainerNotes.SplitterPosition = splitContainerNotes.Width / 4;
                    }
                    else
                    {
                        splitContainerNotes.PanelVisibility = SplitPanelVisibility.Panel2;
                        splitContainerNotes.SplitterPosition = 0;
                    }
                }
            }

            btnCustomerNotesEdit.Text = "Редактирование нюансов учета";
            btnCustomerNotesEdit.Enabled = true;

            btnAddLetterAttachment.Visibility = BarItemVisibility.Never;
            btnDelLetterAttachment.Visibility = BarItemVisibility.Never;

            btnAttachmentAdd.Visible = false;
            btnCustomerNotesSave.Visible = false;
            btnCustomerNotesCancel.Visible = false;

            CustomerNotes = null;
            richEditNotes.ReadOnly = true;
            gridControlCustomer.Enabled = true;
            //splitContainerCustomer.Panel1.Enabled = true;

            IsCustomerNotesEdit = false;
            XPBaseObject.AutoSaveOnEndEdit = true;
        }

        private void btnCustomerNotesSave_Click(object sender, EventArgs e)
        {
            SaveCustomerNotes(true);
        }

        private void btnCustomerNotesCancel_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show($"Вы действительно хотите отменить сохранение нюансов учета?",
                                        "Отмена сохранения",
                                        MessageBoxButtons.OKCancel,
                                        MessageBoxIcon.Question) == DialogResult.OK)
            {
                SaveCustomerNotes(false);
            }
        }

        private void xtraTabPageNotes_DragDrop(object sender, DragEventArgs e)
        {
            if (IsCustomerNotesEdit)
            {
                if (gridViewCustomer.IsEmpty)
                {
                    return;
                }

                var customer = gridViewCustomer.GetRow(gridViewCustomer.FocusedRowHandle) as Customer;
                if (customer != null)
                {
                    string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                    foreach (string file in files)
                    {
                        customer.Attachments.Add(new CustomerAttachment(_session, file));
                    }

                    if (splitContainerNotes.PanelVisibility == SplitPanelVisibility.Panel2)
                    {
                        splitContainerNotes.PanelVisibility = SplitPanelVisibility.Both;
                        splitContainerNotes.SplitterPosition = splitContainerNotes.Width / 4;
                    }
                }
            }
        }

        private void xtraTabPageNotes_DragEnter(object sender, DragEventArgs e)
        {
            if (IsCustomerNotesEdit)
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    e.Effect = DragDropEffects.Copy;
                }
            }
        }

        /// <summary>
        /// Добавление вложения к письму.
        /// </summary>
        private async void AddAttachment(Customer customer)
        {
            using (var ofd = new XtraOpenFileDialog() { KeepPosition = false, Multiselect = true })
            {
                var myFolderPath = await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, nameof(cls_AppParam.MyFolderPath), user: BVVGlobal.oApp.User);

                if (!string.IsNullOrWhiteSpace(myFolderPath))
                {
                    try
                    {
                        ofd.CustomPlaces.Add($@"{myFolderPath}");
                    }
                    catch (Exception ex)
                    {
                        RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                        ofd.CustomPlaces.Clear();
                    }
                }

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    foreach (var fileName in ofd.FileNames)
                    {
                        customer.Attachments.Add(new CustomerAttachment(_session, fileName));
                    }

                    if (splitContainerNotes.PanelVisibility == SplitPanelVisibility.Panel2)
                    {
                        splitContainerNotes.PanelVisibility = SplitPanelVisibility.Both;
                        splitContainerNotes.SplitterPosition = splitContainerNotes.Width / 4;
                    }
                }
            }
        }

        private void btnAddLetterAttachment_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewCustomer.IsEmpty)
            {
                return;
            }

            var customer = gridViewCustomer.GetRow(gridViewCustomer.FocusedRowHandle) as Customer;
            if (customer != null)
            {
                AddAttachment(customer);
            }
        }

        private void OpenOrSaveAttachment(bool isSave = false)
        {
            if (gridViewAttachment.GetRow(gridViewAttachment.FocusedRowHandle) is CustomerAttachment attachment)
            {
                if (isSave)
                {
                    using (var sfd = new XtraSaveFileDialog())
                    {
                        sfd.FileName = attachment.FullFileName;
                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            var tempPath = Path.GetTempFileName().Replace(".tmp", $"{attachment.FileExtension}");
                            var isSaveFile = attachment.WriteFile(tempPath, true);
                            if (isSaveFile)
                            {
                                System.IO.File.Copy(tempPath, sfd.FileName);
                            }
                        }
                    }
                }
                else
                {
                    var tempPath = Path.GetTempFileName().Replace(".tmp", $"{attachment.FileExtension}");
                    var isSaveFile = attachment.WriteFile(tempPath, true);

                    if (isSaveFile)
                    {
                        Process process = new Process();
                        ProcessStartInfo processStartInfo = new ProcessStartInfo();
                        processStartInfo.UseShellExecute = true;
                        processStartInfo.FileName = $"{tempPath}";
                        process.StartInfo = processStartInfo;

                        try
                        {
                            process.Start();
                        }
                        catch (Exception)
                        {
                            //MessageBox.Show(Ex.Message);
                        }
                    }
                }
            }
        }

        private void btnOpenLetterAttachment_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenOrSaveAttachment();
        }

        private void btnDelLetterAttachment_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewAttachment.IsEmpty)
            {
                return;
            }

            var attachment = gridViewAttachment.GetRow(gridViewAttachment.FocusedRowHandle) as CustomerAttachment;
            if (attachment != null)
            {
                ListCustomerAttachment.Add(attachment);
                gridViewAttachment.DeleteRow(gridViewAttachment.FocusedRowHandle);
            }
        }

        private void gridViewAttachment_DoubleClick(object sender, EventArgs e)
        {
            OpenOrSaveAttachment();
        }

        private void gridViewAttachment_MouseDown(object sender, MouseEventArgs e)
        {
            DXMouseEventArgs dxMouseEventArgs = e as DXMouseEventArgs;
            GridView gridview = sender as GridView;

            if (dxMouseEventArgs.Button == MouseButtons.Right)
            {
                popupMenuAttachment.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
            }
        }

        private void btnAttachmentAdd_Click(object sender, EventArgs e)
        {
            if (gridViewCustomer.IsEmpty)
            {
                return;
            }

            var customer = gridViewCustomer.GetRow(gridViewCustomer.FocusedRowHandle) as Customer;
            if (customer != null)
            {
                AddAttachment(customer);
            }
        }


        private void barBtnOrganizationPerformance_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewCustomer.IsEmpty)
            {
                return;
            }

            var customer = gridViewCustomer.GetRow(gridViewCustomer.FocusedRowHandle) as Customer;
            OrganizationPerformancePrint organizationPerformancePrint;

            if (customer != null)
            {
                organizationPerformancePrint = new OrganizationPerformancePrint(customer);
            }
            else
            {
                organizationPerformancePrint = new OrganizationPerformancePrint();
            }
            organizationPerformancePrint.Show();
        }

        private void cmbIsPublic_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (_currentCustomer != null)
                {
                    var comboBoxEdit = sender as ComboBoxEdit;

                    if (comboBoxEdit != null && comboBoxEdit.SelectedIndex != -1)
                    {
                        _currentCustomer.IsPublic = (Availability)comboBoxEdit.SelectedIndex;
                    }
                    else
                    {
                        _currentCustomer.IsPublic = null;
                    }
                    SaveValueAfterChange();
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private void cmbIsSalary_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (_currentCustomer != null)
                {
                    var comboBoxEdit = sender as ComboBoxEdit;

                    if (comboBoxEdit != null && comboBoxEdit.SelectedIndex != -1)
                    {
                        _currentCustomer.IsSalary = (Availability)comboBoxEdit.SelectedIndex;
                    }
                    else
                    {
                        _currentCustomer.IsSalary = null;
                    }
                    SaveValueAfterChange();
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private void cmbIsPublicAndSalary_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (_currentCustomer != null)
                {
                    var comboBoxEdit = sender as ComboBoxEdit;

                    if (comboBoxEdit != null && comboBoxEdit.SelectedIndex != -1)
                    {
                        _currentCustomer.IsPublicAndSalary = (Availability)comboBoxEdit.SelectedIndex;
                    }
                    else
                    {
                        _currentCustomer.IsPublicAndSalary = null;
                    }
                    SaveValueAfterChange();
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private void SalaryEditValueChanged(object sender, EventArgs e)
        {
            if (_currentCustomer != null)
            {
                var isEdit = false;

                if (_currentCustomer.DOP1 == null)
                {
                    _currentCustomer.DOP1 = string.Empty;
                }

                if (_currentCustomer.DOP2 == null)
                {
                    _currentCustomer.DOP2 = string.Empty;
                }

                if (!_currentCustomer.DOP1.Equals(memoAdditionalInformationSalary.Text))
                {
                    isEdit = true;
                }

                if (!_currentCustomer.DOP2.Equals(memoEdit8.Text))
                {
                    isEdit = true;
                }

                btnSaveCustomerItemInfoSalary.Visible = isEdit;
            }
        }

        private void btnSaveCustomerItemInfoSalary_Click(object sender, EventArgs e)
        {
            if (_currentCustomer != null)
            {
                var imprestDay = default(int?);
                var salaryDay = default(int?);

                if (int.TryParse(txtAdvanceDayObject.Text, out int _imprestDay))
                {
                    imprestDay = _imprestDay;
                }
                else
                {
                    imprestDay = null;
                }

                if (_currentCustomer.ImprestDay != imprestDay)
                {
                    var msg = $"Пользователь [{_session.GetObjectByKey<User>(DatabaseConnection.User.Oid)}] ";

                    if (imprestDay == null)
                    {
                        msg += $"удалил значение [{_currentCustomer.ImprestDay}] дня аванса.";
                    }
                    else if (_currentCustomer.ImprestDay == null)
                    {
                        msg += $"установил значение дня аванса на [{imprestDay}] число.";
                    }
                    else
                    {
                        msg += $"изменил день аванса с [{_currentCustomer.ImprestDay}] на [{imprestDay}].";
                    }

                    _currentCustomer.ChronicleCustomers.Add(new ChronicleCustomer(_session)
                    {
                        Act = Act.CHANGE_ADVANCE_DAY,
                        Date = DateTime.Now,
                        Description = msg,
                        User = _session.GetObjectByKey<User>(DatabaseConnection.User.Oid)
                    });
                }

                if (int.TryParse(txtSalaryDayObject.Text, out int _salaryDay))
                {
                    salaryDay = _salaryDay;
                }
                else
                {
                    salaryDay = null;
                }

                if (_currentCustomer.SalaryDay != salaryDay)
                {
                    var msg = $"Пользователь [{_session.GetObjectByKey<User>(DatabaseConnection.User.Oid)}] ";

                    if (salaryDay == null)
                    {
                        msg += $"удалил значение [{_currentCustomer.SalaryDay}] дня заработной платы.";
                    }
                    else if (_currentCustomer.SalaryDay == null)
                    {
                        msg += $"установил значение дня заработной платы на [{salaryDay}] число.";
                    }
                    else
                    {
                        msg += $"изменил день заработной платы с [{_currentCustomer.SalaryDay}] на [{salaryDay}].";
                    }

                    _currentCustomer.ChronicleCustomers.Add(new ChronicleCustomer(_session)
                    {
                        Act = Act.CHANGE_SALARY_DAY,
                        Date = DateTime.Now,
                        Description = msg,
                        User = _session.GetObjectByKey<User>(DatabaseConnection.User.Oid)
                    });
                }

                try
                {
                    _currentCustomer.DOP1 = memoAdditionalInformationSalary.Text;
                    _currentCustomer.DOP2 = memoEdit8.Text;
                    SaveValueAfterChange();
                    btnSaveCustomerItemInfoSalary.Visible = false;
                }
                catch (Exception ex)
                {
                    RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                }
            }
        }

        private void gridControlCustomer_Load(object sender, EventArgs e)
        {
            GridControl gridControl = sender as GridControl;
            GridView gridView = gridControl.MainView as GridView;
            if (gridView != null)
            {
                gridControl.BeginInvoke(new Action(gridView.ShowFindPanel));
            }
        }

        private void gridViewCustomer_ColumnFilterChanged(object sender, EventArgs e)
        {
            var gridview = sender as GridView;
            try
            {
                gridViewCustomer_FocusedRowChanged(gridview, new FocusedRowChangedEventArgs(-1, gridview.FocusedRowHandle));
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private void barBtnPrintCurrentGrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            var temp = Path.GetTempFileName();
            gridViewCustomer.ExportToXlsx(temp);
            gridViewCustomer.ShowRibbonPrintPreview();
        }

        private void richEditNotes_DocumentLoaded(object sender, EventArgs e)
        {
            AdjustMargins();
        }

        private void AdjustMargins()
        {
            richEditNotes.Document.Sections[0].Margins.Left = Units.InchesToDocumentsF(2f);
        }

        private void AdjustSimpleViewPadding()
        {
            richEditNotes.Views.SimpleView.Padding = new DevExpress.Portable.PortablePadding(0);
        }

        private void AdjustDraftViewPadding()
        {
            richEditNotes.Views.DraftView.Padding = new DevExpress.Portable.PortablePadding(0);
        }

        private void AdjustParagraphIndent()
        {
            richEditNotes.Document.Paragraphs[0].LeftIndent = Units.InchesToDocumentsF(0.5f);
            richEditNotes.Document.Paragraphs[0].FirstLineIndentType = ParagraphFirstLineIndent.Indented;
            richEditNotes.Document.Paragraphs[0].FirstLineIndent = Units.InchesToDocumentsF(0.5f);
        }

        private void ClearValueButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (buttonEdit != null && e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }
            else if (buttonEdit != null && e.Button.Kind == ButtonPredefines.Ellipsis)
            {

            }
        }

        private void btnPatent_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (gridViewCustomer.IsEmpty)
            {
                return;
            }

            var buttonEdit = sender as ButtonEdit;

            var customer = gridViewCustomer.GetRow(gridViewCustomer.FocusedRowHandle) as Customer;
            if (customer != null)
            {
                if (customer.Tax is null)
                {
                    customer.Tax = new Tax(_session);
                }

                if (customer.Tax.Patent is null)
                {
                    customer.Tax.Patent = new Patent(_session);
                }

                customer.Tax?.Patent?.Reload();
                customer.Tax?.Patent?.PatentObjects?.Reload();
                foreach (var patentObjects in customer.Tax.Patent.PatentObjects)
                {
                    patentObjects?.Reload();
                    patentObjects?.PatentStatus?.Reload();
                }

                var form = new PatentEdit2(customer.Tax.Patent, customer);
                form.ShowDialog();

                btnPatent.EditValue = customer.Tax?.Patent;
                if (!string.IsNullOrWhiteSpace(customer.Tax?.Patent?.ToString()))
                {
                    if (customer.Tax.Patent.ToString().Equals("Нет"))
                    {
                        btnPatent.Properties.ContextImageOptions.Image = imgCustomer.Images[1];
                    }
                    else
                    {
                        btnPatent.Properties.ContextImageOptions.Image = imgCustomer.Images[0];
                    }
                }
                else
                {
                    btnPatent.Properties.ContextImageOptions.Image = null;
                }
            }
        }

        private void btnPatent_DoubleClick(object sender, EventArgs e)
        {
            if (gridViewCustomer.IsEmpty)
            {
                return;
            }

            if (isEditCustomersForm is false)
            {
                return;
            }

            var buttonEdit = sender as ButtonEdit;

            var customer = gridViewCustomer.GetRow(gridViewCustomer.FocusedRowHandle) as Customer;
            if (customer != null)
            {
                if (customer.Tax is null)
                {
                    customer.Tax = new Tax(_session);
                }

                if (customer.Tax.Patent is null)
                {
                    customer.Tax.Patent = new Patent(_session);
                }

                var form = new PatentEdit2(customer.Tax.Patent, customer);
                form.ShowDialog();

                btnPatent.EditValue = customer.Tax?.Patent;
                if (!string.IsNullOrWhiteSpace(customer.Tax?.Patent?.ToString()))
                {
                    if (customer.Tax.Patent.ToString().Equals("Нет"))
                    {
                        btnPatent.Properties.ContextImageOptions.Image = imgCustomer.Images[1];
                    }
                    else
                    {
                        btnPatent.Properties.ContextImageOptions.Image = imgCustomer.Images[0];
                    }
                }
                else
                {
                    btnPatent.Properties.ContextImageOptions.Image = null;
                }
            }
        }

        private void GetElectronicReportingCustomer(ButtonEdit buttonEdit, Customer customer)
        {
            var form = new ElectronicReportingCustomerEdit2(customer);
            form.ShowDialog();
            SetEditValueElectronicReportingCustomer(buttonEdit, customer);
        }

        private void SetEditValueElectronicReportingCustomer(ButtonEdit buttonEdit, Customer customer)
        {
            buttonEdit.EditValue = customer?.ElectronicReportingString;

            if (!string.IsNullOrWhiteSpace(buttonEdit.Text) && buttonEdit.Text.Equals("Электронная отчетность отсутствует"))
            {
                buttonEdit.BackColor = Color.LightCoral;
                buttonEdit.ToolTip = $"Электронная отчетность не найдена на текущую дату: {DateTime.Now.Date.ToShortDateString()}";
            }
            else
            {
                buttonEdit.BackColor = default;
                buttonEdit.ToolTip = default;
            }

            var result = "ЭЦП";
            if (customer?.ElectronicReportingCustomer?.LicenseDateTo is DateTime licenseDateTo)
            {
                result += $" (окончание лицензии {licenseDateTo.ToShortDateString()})";
                gpElectronicReporting.AppearanceCaption.ForeColor = Color.Red;
                gpElectronicReporting.AppearanceCaption.Font = new Font(gpElectronicReporting.AppearanceCaption.Font, FontStyle.Bold);
            }
            else
            {
                gpElectronicReporting.AppearanceCaption.Font = new Font(gpElectronicReporting.AppearanceCaption.Font, FontStyle.Regular);
                gpElectronicReporting.AppearanceCaption.ForeColor = default;
            }
            gpElectronicReporting.Text = result;
        }

        private static void SetEditValueElectronicDocumentManagementCustomer(ButtonEdit buttonEdit, Customer customer)
        {
            buttonEdit.EditValue = customer?.ElectronicDocumentManagementCustomer;

            if (!string.IsNullOrWhiteSpace(buttonEdit.Text) && buttonEdit.Text.Equals("ЭДО отсутствует"))
            {
                buttonEdit.BackColor = Color.LightCoral;
                buttonEdit.ToolTip = $"ЭДО не найден на текущую дату: {DateTime.Now.Date.ToShortDateString()}";
            }
            else
            {
                buttonEdit.BackColor = default;
                buttonEdit.ToolTip = default;
            }
        }

        private void btnElectronicReporting_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (_currentCustomer != null)
            {
                if (sender is ButtonEdit buttonEdit)
                {
                    if (e.Button.Kind == ButtonPredefines.Delete)
                    {
                        buttonEdit.EditValue = null;
                        return;
                    }

                    GetElectronicReportingCustomer(buttonEdit, _currentCustomer);
                }
            }
        }

        private void btnElectronicReporting_DoubleClick(object sender, EventArgs e)
        {
            if (isEditCustomersForm is false)
            {
                return;
            }

            if (_currentCustomer != null)
            {
                if (sender is ButtonEdit buttonEdit)
                {
                    GetElectronicReportingCustomer(buttonEdit, _currentCustomer);
                }
            }
        }

        private void btnTaxSystem_DoubleClick(object sender, EventArgs e)
        {
            if (isEditCustomersForm is false)
            {
                return;
            }

            if (gridViewCustomer.IsEmpty)
            {
                return;
            }

            var buttonEdit = sender as ButtonEdit;

            var customer = gridViewCustomer.GetRow(gridViewCustomer.FocusedRowHandle) as Customer;
            if (customer != null)
            {
                customer.Reload();
                customer.TaxSystemCustomer?.Reload();
                customer.TaxSystemCustomer?.TaxSystem?.Reload();

                var form = new TaxSystemCustomerEdit(customer);
                form.ShowDialog();

                buttonEdit.Text = customer.TaxSystemCustomer?.ToString();
                buttonEdit.EditValue = customer.TaxSystemCustomer;

                gridViewCustomer_FocusedRowChanged(null, null);
            }
        }
        private void btnTaxSystem_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (gridViewCustomer.IsEmpty)
            {
                return;
            }

            var buttonEdit = sender as ButtonEdit;

            var customer = gridViewCustomer.GetRow(gridViewCustomer.FocusedRowHandle) as Customer;
            if (customer != null)
            {
                customer.Reload();
                customer.TaxSystemCustomer?.Reload();
                customer.TaxSystemCustomer?.TaxSystem?.Reload();

                var form = new TaxSystemCustomerEdit(customer);
                form.ShowDialog();

                buttonEdit.Text = customer.TaxSystemCustomer?.ToString();
                buttonEdit.EditValue = customer.TaxSystemCustomer;

                gridViewCustomer_FocusedRowChanged(null, null);
            }
        }

        private void txtImprestDay_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (_currentCustomer is null)
            {
                return;
            }

            var buttonEdit = sender as ButtonEdit;

            if (buttonEdit != null && e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }
            else if (buttonEdit != null && e.Button.Kind == ButtonPredefines.Ellipsis)
            {
                if (_currentCustomer.AdvanceDayObject is null)
                {
                    _currentCustomer.AdvanceDayObject = new SalaryAndAdvance(_session);
                    _currentCustomer.AdvanceDayObject.Save();
                }

                var form = new SalaryEdit(_currentCustomer, _currentCustomer.AdvanceDayObject);
                form.ShowDialog();
                buttonEdit.EditValue = form.SalaryAndAdvance;
            }
        }

        private void txtSalaryDay_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (_currentCustomer is null)
            {
                return;
            }

            var buttonEdit = sender as ButtonEdit;

            if (buttonEdit != null && e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }
            else if (buttonEdit != null && e.Button.Kind == ButtonPredefines.Ellipsis)
            {
                if (_currentCustomer.SalaryDayObject is null)
                {
                    _currentCustomer.SalaryDayObject = new SalaryAndAdvance(_session);
                    _currentCustomer.SalaryDayObject.Save();
                }

                var form = new SalaryEdit(_currentCustomer, _currentCustomer.SalaryDayObject);
                form.ShowDialog();
                buttonEdit.EditValue = form.SalaryAndAdvance;
            }
        }

        private void gridViewCustomer_CustomColumnSort(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
        {
            try
            {
                if (e.Column.FieldName.Equals(nameof(Customer.DateManagementBirth)))
                {
                    e.Handled = true;

                    int month1 = Convert.ToDateTime(e.Value1).Month;
                    int month2 = Convert.ToDateTime(e.Value2).Month;

                    if (month1 > month2)
                    {
                        e.Result = 1;
                    }
                    else if (month1 < month2)
                    {
                        e.Result = -1;
                    }
                    else
                    {
                        e.Result = Comparer.Default.Compare(Convert.ToDateTime(e.Value1).Day, Convert.ToDateTime(e.Value2).Day);
                    }
                }
                else
                {
                    if (e.Column.FieldName.Equals(nameof(Customer.Name)))
                    {
                        e.Handled = true;

                        int month1 = Convert.ToDateTime(e.Value1).Month;
                        int month2 = Convert.ToDateTime(e.Value2).Month;

                        if (month1 > month2)
                        {
                            e.Result = 1;
                        }
                        else if (month1 < month2)
                        {
                            e.Result = -1;
                        }
                        else
                        {
                            e.Result = Comparer.Default.Compare(Convert.ToDateTime(e.Value1).Day, Convert.ToDateTime(e.Value2).Day);
                        }
                    }
                }                
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private void btnControlSystem_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewCustomer.IsEmpty)
            {
                return;
            }

            var customer = gridViewCustomer.GetRow(gridViewCustomer.FocusedRowHandle) as Customer;
            if (customer != null)
            {
                var form = new ControlSystemEdit(customer);
                form.ShowDialog();
            }
        }

        private void gridViewBankAccess_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                var view = sender as GridView;
                if (e.Control && e.KeyCode == Keys.C)
                {
                    if (view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn) != null && view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn).ToString() != String.Empty)
                    {
                        Clipboard.SetText(view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn).ToString());
                    }
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async void CustomersForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            await SaveSettingsForms();
        }

        private void treeListCustomerFilter_PopupMenuShowing(object sender, DevExpress.XtraTreeList.PopupMenuShowingEventArgs e)
        {
            try
            {
                popupMenuTreeList.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async void barBtnDefault_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                var node = treeListCustomerFilter.FocusedNode;
                if (node != null)
                {
                    var obj = default(object);
                    if (node.GetValue(0) is string name)
                    {
                        obj = name;
                    }
                    else if (node.GetValue(0) is CustomerFilter customerFilter)
                    {
                        if (customerFilter?.Oid > 0)
                        {
                            obj = customerFilter?.Oid;
                        }
                        else
                        {
                            obj = customerFilter?.Name;
                        }
                    }

                    await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"{this.Name}_{nameof(treeListCustomerFilter)}_{nameof(treeListCustomerFilter.FocusedNode)}", obj?.ToString(), true, user: BVVGlobal.oApp.User);
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private void btnImportEAEU_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (gridViewCustomer.IsEmpty)
            {
                return;
            }

            if (sender is ButtonEdit buttonEdit)
            {
                if (gridViewCustomer.GetRow(gridViewCustomer.FocusedRowHandle) is Customer customer)
                {
                    var form = new TaxImportEAEUEdit(customer);
                    form.ShowDialog();

                    buttonEdit.EditValue = customer.Tax?.TaxImportEAEU;

                    if (customer.Tax?.TaxImportEAEU?.IsUse is true)
                    {
                        buttonEdit.Properties.ContextImageOptions.Image = imgCustomer.Images[0];
                    }
                    else if (customer.Tax?.TaxImportEAEU?.IsUse is false)
                    {
                        buttonEdit.Properties.ContextImageOptions.Image = imgCustomer.Images[1];
                    }
                    else
                    {
                        buttonEdit.Properties.ContextImageOptions.Image = null;
                    }
                }
            }
        }

        private void txtElectronicFolder_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (gridViewCustomer.IsEmpty)
                {
                    return;
                }

                if (_currentCustomer != null)
                {
                    if (sender is TextEdit txtEdit)
                    {
                        _currentCustomer.ElectronicFolder = txtEdit.Text;
                        SaveValueAfterChange();
                    }
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private void gridViewCustomerStatisticalReports_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (e.MenuType == GridMenuType.User || e.MenuType == GridMenuType.Row)
                {
                    if (e.MenuType != GridMenuType.Row)
                    {
                        barBtnStatisticalReportEdit.Enabled = false;
                        barBtnStatisticalReportDel.Enabled = false;
                    }
                    else
                    {
                        barBtnStatisticalReportEdit.Enabled = true;
                        barBtnStatisticalReportDel.Enabled = true;
                    }

                    barBtnStatisticalReportEdit.Enabled = isEditCustomersForm;
                    barBtnStatisticalReportDel.Enabled = isEditCustomersForm;

                    popupMenuStatisticalReport.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }

        private void barBtnStatisticalReportAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewCustomer.GetRow(gridViewCustomer.FocusedRowHandle) is Customer customer)
            {
                var form = new StatisticalReportEdit(customer);
                form.ShowDialog();
            }
        }

        private void barBtnStatisticalReportEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewCustomerStatisticalReports.GetRow(gridViewCustomerStatisticalReports.FocusedRowHandle) is StatisticalReport customerReport)
            {
                var form = new StatisticalReportEdit(customerReport);
                form.ShowDialog();
            }
        }

        private void barBtnStatisticalReportDel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewCustomerStatisticalReports.GetRow(gridViewCustomerStatisticalReports.FocusedRowHandle) is StatisticalReport customerReport)
            {
                if (XtraMessageBox.Show($"Вы действительно хотите удалить следующий статистический отчет: {customerReport}?",
                                        "Регламентная операция",
                                        MessageBoxButtons.OKCancel,
                                        MessageBoxIcon.Question) == DialogResult.OK)
                {
                    customerReport?.Delete();
                }
            }
        }

        private void barBtnStatisticalReportUpdate_ItemClick(object sender, ItemClickEventArgs e)
        {
            //if (gridViewCustomer.GetRow(gridViewCustomer.FocusedRowHandle) is Customer customer)
            //{
            //    customer?.StatisticalReports?.Reload();
            //}
        }

        private async void barBtnStatisticalReportGet_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (gridViewCustomer.GetRow(gridViewCustomer.FocusedRowHandle) is Customer customer)
                {
                    if (!string.IsNullOrWhiteSpace(customer.INN) || !string.IsNullOrWhiteSpace(customer.OKPO) || !string.IsNullOrWhiteSpace(customer.PSRN))
                    {
                        if (XtraMessageBox.Show("Операция получения отчетов требует соединения с интернетом. Возможно кратковременное зависание программы. Хотите продолжить?",
                                "Операция получения отчетов",
                                MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Question) != DialogResult.OK)
                        {
                            return;
                        }

                        if (string.IsNullOrWhiteSpace(customer.INN))
                        {
                            XtraMessageBox.Show($"Не задан ИНН клиента: {customer}",
                                   "Операция получения отчетов",
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Information);
                            return;
                        }

                        var mainObject = await MainObjectController.GetAsync(customer.INN);
                        var message = default(string);

                        if (mainObject != null && mainObject.ObjectForms != null && mainObject.ObjectForms.Count > 0)
                        {
                            var addList = new List<Report>();
                            var add = 1;
                            var del = 1;

                            var addMessage = default(string);
                            var delMessage = default(string);

                            foreach (var item in mainObject.ObjectForms)
                            {
                                var report = await new XPQuery<Report>(_session).FirstOrDefaultAsync(w => w.OKUD == item.Okud);
                                if (report is null)
                                {
                                    report = new Report(_session)
                                    {
                                        FormIndex = item.Index,
                                        Name = item.Name,
                                        Periodicity = Report.GetPeriodicity(item.FormPeriod),
                                        Deadline = item.EndTime,
                                        Comment = item.Comment,
                                        OKUD = item.Okud
                                    };
                                    report.Save();
                                }

                                var customerStatisticaReport = customer.StatisticalReports.FirstOrDefault(f => f.Report.OKUD == report.OKUD);
                                //TODO: Тут возможно не корректное сравнение с годом, т.к. в самой табличке год нигде не указывается на сайте кроме как отчетного периода
                                if (customerStatisticaReport is null)
                                {
                                    var statisticalReport = new StatisticalReport(_session)
                                    {
                                        Report = report,
                                        Year = DateTime.Now.Year
                                    };
                                    statisticalReport.SetCreateObj(await _session.GetObjectByKeyAsync<User>(DatabaseConnection.User?.Oid));
                                    customer.StatisticalReports.Add(statisticalReport);

                                    addMessage += $"[{add}] {statisticalReport} ({statisticalReport.Year} г.){Environment.NewLine}";
                                    add++;
                                }
                                else
                                {
                                    if (customerStatisticaReport.Year != DateTime.Now.Year)
                                    {
                                        customerStatisticaReport.Year = DateTime.Now.Year;
                                        customerStatisticaReport.Save();
                                    }
                                }

                                addList.Add(report);
                            }

                            if (!string.IsNullOrWhiteSpace(addMessage))
                            {
                                message += $"{Environment.NewLine}{Environment.NewLine}Добавлены следующие отчеты:{Environment.NewLine}{addMessage.Trim()}";
                            }

                            var deleteList = new List<StatisticalReport>();
                            foreach (var item in customer.StatisticalReports)
                            {
                                var obj = addList.FirstOrDefault(f => f?.Oid == item.Report?.Oid);
                                if (obj is null)
                                {
                                    deleteList.Add(item);
                                    delMessage += $"[{del}] {item} ({item.Year} г.){Environment.NewLine}";
                                    del++;
                                }
                            }

                            if (deleteList.Count > 0)
                            {
                                _session.Delete(deleteList);
                            }

                            if (!string.IsNullOrWhiteSpace(delMessage))
                            {
                                message += $"{Environment.NewLine}{Environment.NewLine}Удалены следующие отчеты:{Environment.NewLine}{delMessage.Trim()}";
                            }
                        }
                        else
                        {
                            if (customer.StatisticalReports != null && customer.StatisticalReports.Count > 0)
                            {
                                var i = 1;
                                foreach (var item in customer.StatisticalReports)
                                {
                                    message += $"[{i}] {item}{Environment.NewLine}";
                                    i++;
                                }

                                if (!string.IsNullOrWhiteSpace(message))
                                {
                                    message = $"{Environment.NewLine}{Environment.NewLine}Удалены следующие отчеты:{Environment.NewLine}{message.Trim()}";
                                }

                                _session.Delete(customer.StatisticalReports);
                            }
                        }

                        var description = $"Ручное заполнение отчетов клиента по ИНН [{customer.INN}]";
                        var chronicle = new ChronicleCustomer(_session)
                        {
                            Act = Act.UPDATING_INFORMATION_REPORTING_SYSTEM_HAND,
                            Date = DateTime.Now,
                            Description = description,
                            User = _session.GetObjectByKey<User>(DatabaseConnection.User.Oid),
                            Customer = customer
                        };
                        chronicle.Save();
                        customer.ChronicleCustomers.Add(chronicle);
                        customer.Save();

                        var chronicleEvents = new ChronicleEvents(_session)
                        {
                            Act = Act.UPDATING_INFORMATION_REPORTING_SYSTEM_HAND,
                            Date = DateTime.Now,
                            Name = Act.UPDATING_INFORMATION_REPORTING_SYSTEM_HAND.GetEnumDescription(),
                            User = await _session.GetObjectByKeyAsync<User>(DatabaseConnection.User.Oid)                           
                        };
                        chronicleEvents.Save();

                        XtraMessageBox.Show($"Поиск статистических отчетов окончен.{message}",
                                            "Парсинг websbor.gks.ru",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);

                        //var message = default(string);
                        //using (var parserReportingSystem = new ReportingSystem(customer.INN, customer.OKPO, customer.PSRN))
                        //{
                        //    parserReportingSystem.ErrorEvent += ParserReportingSystem_ErrorEvent;

                        //    if (parserReportingSystem.StartDriver() is false)
                        //    {
                        //        return;
                        //    }

                        //    parserReportingSystem.StartParser();

                        //    if (string.IsNullOrWhiteSpace(customer.INN))
                        //    {
                        //        customer.INN = parserReportingSystem.OrganizationStatisticsCodes?.INN;
                        //    }

                        //    if (string.IsNullOrWhiteSpace(customer.OKPO))
                        //    {
                        //        customer.OKPO = parserReportingSystem.OrganizationStatisticsCodes?.OKPO;
                        //    }

                        //    if (string.IsNullOrWhiteSpace(customer.PSRN))
                        //    {
                        //        customer.PSRN = parserReportingSystem.OrganizationStatisticsCodes?.PSRN;
                        //    }

                        //    if (string.IsNullOrWhiteSpace(customer.OKTMO))
                        //    {
                        //        customer.OKTMO = parserReportingSystem.OrganizationStatisticsCodes?.ActualOKTMO;
                        //    }

                        //    if (string.IsNullOrWhiteSpace(customer.OKATO))
                        //    {
                        //        customer.OKATO = parserReportingSystem.OrganizationStatisticsCodes?.ActualOKATO;
                        //    }

                        //    if (parserReportingSystem != null && parserReportingSystem.Reports != null && parserReportingSystem.Reports.Count > 0)
                        //    {
                        //        var addList = new List<Report>();
                        //        var add = 1;
                        //        var del = 1;

                        //        var addMessage = default(string);
                        //        var delMessage = default(string);

                        //        foreach (var item in parserReportingSystem?.Reports)
                        //        {
                        //            var report = await _session.FindObjectAsync<Report>(new BinaryOperator(nameof(Report.OKUD), item.OKUD));
                        //            if (report is null)
                        //            {
                        //                report = new Report(_session)
                        //                {
                        //                    FormIndex = item.FormIndex,
                        //                    Name = item.Name,
                        //                    Periodicity = (Periodicity)Convert.ToInt32(item.Periodicity),
                        //                    Deadline = item.Deadline,
                        //                    Comment = item.Comment,
                        //                    OKUD = item.OKUD
                        //                };
                        //                report.Save();
                        //            }

                        //            var customerStatisticaReport = customer.StatisticalReports.FirstOrDefault(f => f.Report.OKUD == report.OKUD);
                        //            //TODO: Тут возможно не корректное сравнение с годом, т.к. в самой табличке год нигде не указывается на сайте кроме как отчетного периода
                        //            if (customerStatisticaReport is null)
                        //            {
                        //                var statisticalReport = new StatisticalReport(_session)
                        //                {
                        //                    Report = report,
                        //                    Year = DateTime.Now.Year
                        //                };
                        //                statisticalReport.SetCreateObj(await _session.GetObjectByKeyAsync<User>(DatabaseConnection.User?.Oid));
                        //                customer.StatisticalReports.Add(statisticalReport);

                        //                addMessage += $"[{add}] {statisticalReport} ({statisticalReport.Year} г.){Environment.NewLine}";
                        //                add++;
                        //            }
                        //            else
                        //            {
                        //                if (customerStatisticaReport.Year != DateTime.Now.Year)
                        //                {
                        //                    customerStatisticaReport.Year = DateTime.Now.Year;
                        //                    customerStatisticaReport.Save();
                        //                }
                        //            }

                        //            addList.Add(report);
                        //        }

                        //        if (!string.IsNullOrWhiteSpace(addMessage))
                        //        {
                        //            message += $"{Environment.NewLine}{Environment.NewLine}Добавлены следующие отчеты:{Environment.NewLine}{addMessage.Trim()}";
                        //        }

                        //        var deleteList = new List<StatisticalReport>();
                        //        foreach (var item in customer.StatisticalReports)
                        //        {
                        //            var obj = addList.FirstOrDefault(f => f?.Oid == item.Report?.Oid);
                        //            if (obj is null)
                        //            {
                        //                deleteList.Add(item);
                        //                delMessage += $"[{del}] {item} ({item.Year} г.){Environment.NewLine}";
                        //                del++;
                        //            }
                        //        }

                        //        if (deleteList.Count > 0)
                        //        {
                        //            _session.Delete(deleteList);
                        //        }

                        //        if (!string.IsNullOrWhiteSpace(delMessage))
                        //        {
                        //            message += $"{Environment.NewLine}{Environment.NewLine}Удалены следующие отчеты:{Environment.NewLine}{delMessage.Trim()}";
                        //        }

                        //        var description = $"Ручное заполнение отчетов клиента по ИНН [{customer.INN}]";
                        //        customer.ChronicleCustomers.Add(new ChronicleCustomer(_session)
                        //        {
                        //            Act = Act.UPDATING_INFORMATION_REPORTING_SYSTEM_HAND,
                        //            Date = DateTime.Now,
                        //            Description = description,
                        //            User = _session.GetObjectByKey<User>(DatabaseConnection.User.Oid)
                        //        });
                        //    }
                        //    else
                        //    {
                        //        if (customer.StatisticalReports != null && customer.StatisticalReports.Count > 0)
                        //        {
                        //            var i = 1;
                        //            foreach (var item in customer.StatisticalReports)
                        //            {
                        //                message += $"[{i}] {item}{Environment.NewLine}";
                        //                i++;
                        //            }

                        //            if (!string.IsNullOrWhiteSpace(message))
                        //            {
                        //                message = $"{Environment.NewLine}{Environment.NewLine}Удалены следующие отчеты:{Environment.NewLine}{message.Trim()}";
                        //            }

                        //            _session.Delete(customer.StatisticalReports);
                        //        }
                        //    }

                        //    var chronicleEvents = new ChronicleEvents(_session)
                        //    {
                        //        Act = Act.UPDATING_INFORMATION_REPORTING_SYSTEM_HAND,
                        //        Date = DateTime.Now,
                        //        Name = Act.UPDATING_INFORMATION_REPORTING_SYSTEM_HAND.GetEnumDescription(),
                        //        User = await _session.GetObjectByKeyAsync<User>(DatabaseConnection.User.Oid)
                        //    };
                        //    chronicleEvents.Save();

                        //    customer.Save();

                        //    XtraMessageBox.Show($"Поиск статистических отчетов окончен.{message}",
                        //                        "Парсинг websbor.gks.ru",
                        //                        MessageBoxButtons.OK,
                        //                        MessageBoxIcon.Information);
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Too Many Requests"))
                {
                    XtraMessageBox.Show($"Большое количество запросов.{Environment.NewLine}" +
                        $"websbor.gks.ru отверг запрос на получении информации.{Environment.NewLine}{Environment.NewLine}" +
                        $"Подождите пару минут и попробуйте снова!",
                        "Большое количество запросов",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private void barBtnStatisticalReportForm_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewCustomerStatisticalReports.GetRow(gridViewCustomerStatisticalReports.FocusedRowHandle) is StatisticalReport customerReport)
            {
                var form = new ReportChangeEdit(customerReport);
                form.ShowDialog();

                if (form.ReportChange != null)
                {
                    customerReport.StatisticalReportObj.Add(new StatisticalReportObj(customerReport.Session)
                    {
                        ReportChange = form.ReportChange
                    });
                    customerReport.Save();
                }
            }
        }

        private void btnElectronicDocumentManagement_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (_currentCustomer != null)
            {
                if (sender is ButtonEdit buttonEdit)
                {
                    if (e.Button.Kind == ButtonPredefines.Delete)
                    {
                        buttonEdit.EditValue = null;
                        return;
                    }

                    GetElectronicDocumentManagement(buttonEdit, _currentCustomer);
                }
            }
        }

        private void btnElectronicDocumentManagement_DoubleClick(object sender, EventArgs e)
        {
            if (isEditCustomersForm is false)
            {
                return;
            }

            if (_currentCustomer != null)
            {
                if (sender is ButtonEdit buttonEdit)
                {
                    GetElectronicDocumentManagement(buttonEdit, _currentCustomer);
                }
            }
        }

        private static void GetElectronicDocumentManagement(ButtonEdit buttonEdit, Customer customer)
        {
            var form = new ElectronicDocumentManagementCustomerEdit(customer);
            form.ShowDialog();
            SetEditValueElectronicDocumentManagementCustomer(buttonEdit, customer);
        }

        private async void barBtnTelegram_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (_currentCustomer != null)
            {
                var customersEmail = _currentCustomer.CustomerEmails?.Where(w => w.TGUser == null && !string.IsNullOrWhiteSpace(w.Email));
                if (customersEmail != null && customersEmail.Count() > 0)
                {
                    var result = $"Следующим контактам клиента будет отослано приглашение на авторизацию в Telegram:{Environment.NewLine}";
                    var i = 1;
                    foreach (var customerEmail in customersEmail)
                    {
                        result += $"[{i}] {customerEmail}{Environment.NewLine}";
                    }

                    if (DevXtraMessageBox.ShowQuestionXtraMessageBox($"{result.Trim()}{Environment.NewLine}Продолжить?", caption: "Регистрация пользователя"))
                    {
                        var mailingAddressName = await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, nameof(cls_AppParam.MailboxForSending), string.Empty);
                        foreach (var customerEmail in customersEmail)
                        {
                            var email = customerEmail.Email;
                            var guid = customerEmail.Guid;
                            if (string.IsNullOrWhiteSpace(guid))
                            {
                                guid = Guid.NewGuid().ToString();
                                customerEmail.Guid = guid;
                                customerEmail.Save();
                            }

                            await LettersController.CreateAuthorizationLetterAsync(guid,
                                BVVGlobal.oApp.User,
                                email,
                                mailingAddressName);
                        }
                    }
                }
                else
                {
                    DevXtraMessageBox.ShowXtraMessageBox($"У выбранного клиента нет контактов для авторизации");
                }
            }

            if (gridViewCustomer.GetRow(gridViewCustomer.FocusedRowHandle) is Customer)
            {

            }
        }

        private void barBtnLetterAttachmentDownload_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenOrSaveAttachment(true);
        }

        private async void barBtnUpdateCustomerName_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show(
                        $"При данной операции будет произведено переименование организации по сокращенному " +
                            $"наименованию.{Environment.NewLine}Продолжить?",
                        "Информационное сообщение",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    using var uof = new UnitOfWork();
                    var customers = await new XPQuery<Customer>(uof).ToListAsync();
                    var count = customers.Count;
                    var renameCount = 0;

                    foreach (var customer in customers)
                    {
                        var abbreviatedName = customer.AbbreviatedName;

                        if (!string.IsNullOrWhiteSpace(abbreviatedName))
                        {
                            customer.Name = abbreviatedName;
                            customer.Save();

                            renameCount++;
                        }
                    }

                    await uof.CommitTransactionAsync();

                    XtraMessageBox.Show(
                        $"Успешно обработано записей: {count}{Environment.NewLine}" +
                            $"Переименовано объектов: {renameCount}",
                        "Информационное сообщение",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                }
            }                
        }

        private void barBtnDossier_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (_currentCustomer != null)
            {
                var form = new DossierEdit(_currentCustomer);
                form.SetImageCollection(imgCustomer);
                form.XtraFormShow();
            }
        }
    }
}