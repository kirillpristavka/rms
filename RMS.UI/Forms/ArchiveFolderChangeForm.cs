using DevExpress.Data.Filtering;
using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using RMS.Core.Controller.Print;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Reports;
using RMS.Setting.Model.ColorSettings;
using RMS.UI.Forms.Directories;
using RMS.UI.Forms.ReferenceBooks;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Forms
{
    public partial class ArchiveFolderChangeForm : XtraForm
    {
        private static string pathTempDirectory = "temp";
        private static string pathSaveLayoutToXmlMainGrid = $"{pathTempDirectory}\\{nameof(ArchiveFolderChangeForm)}_{nameof(gridViewArchiveFolders)}.xml";

        private Session Session { get; }
        private XPCollection<ArchiveFolderChange> _archiveFoldersChange { get; }

        private static class StatusArchiveFolderColor
        {
            public static Color ColorStatusArchiveFolderNew;
            public static Color ColorStatusArchiveFolderIsCompleted;
            public static Color ColorStatusArchiveFolderSortout;
            public static Color ColorStatusArchiveFolderIssued;
            public static Color ColorStatusArchiveFolderDone;
            public static Color ColorStatusArchiveFolderReturned;

            public static Color ColorStatusArchiveFolderDestroyed;

            public static Color ColorStatusArchiveFolderIsSuedEDS;
            public static Color ColorStatusArchiveFolderReceivedEDS;
            public static Color ColorStatusArchiveFolderIsSuedTK;
            public static Color ColorStatusArchiveFolderReceivedTK;
        }

        /// <summary>
        /// Настройка функционирования таблиц.
        /// </summary>
        private void FunctionalGridSetup()
        {
            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref gridViewArchiveFolders);
            BVVGlobal.oFuncXpo.PressEnterGrid<ArchiveFolderChange, ArchiveFolderChangeEdit>(gridViewArchiveFolders);
        }

        public ArchiveFolderChangeForm(Session session)
        {
            InitializeComponent();
            FunctionalGridSetup();

            foreach (StatusArchiveFolder item in Enum.GetValues(typeof(StatusArchiveFolder)))
            {
                if (item == StatusArchiveFolder.CURRENT)
                {
                    continue;
                }

                cmbStatusArchiveFolder.Properties.Items.Add(item.GetEnumDescription());
            }

            foreach (PeriodArchiveFolder item in Enum.GetValues(typeof(PeriodArchiveFolder)))
            {
                cmbPeriod.Properties.Items.Add(item.GetEnumDescription());
            }

            Session = session ?? BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
            _archiveFoldersChange = new XPCollection<ArchiveFolderChange>(Session);
        }

        private async System.Threading.Tasks.Task GetStatusArchiveFolderColor()
        {
            var colorStatus = await Session.FindObjectAsync<ColorStatus>(new BinaryOperator(nameof(ColorStatus.IsDefault), true));
            if (colorStatus != null)
            {
                StatusArchiveFolderColor.ColorStatusArchiveFolderNew = ColorTranslator.FromHtml(colorStatus.StatusArchiveFolderNew);
                StatusArchiveFolderColor.ColorStatusArchiveFolderIsCompleted = ColorTranslator.FromHtml(colorStatus.StatusArchiveFolderIsCompleted);
                StatusArchiveFolderColor.ColorStatusArchiveFolderSortout = ColorTranslator.FromHtml(colorStatus.StatusArchiveFolderSortout);
                StatusArchiveFolderColor.ColorStatusArchiveFolderIssued = ColorTranslator.FromHtml(colorStatus.StatusArchiveFolderIssued);
                StatusArchiveFolderColor.ColorStatusArchiveFolderDone = ColorTranslator.FromHtml(colorStatus.StatusArchiveFolderDone);
                StatusArchiveFolderColor.ColorStatusArchiveFolderReturned = ColorTranslator.FromHtml(colorStatus.StatusArchiveFolderReturned);

                StatusArchiveFolderColor.ColorStatusArchiveFolderDestroyed = ColorTranslator.FromHtml(colorStatus.StatusArchiveFolderDestroyed);

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

                StatusArchiveFolderColor.ColorStatusArchiveFolderDestroyed = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusArchiveFolderDestroyed", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(192, 192, 192))));

                StatusArchiveFolderColor.ColorStatusArchiveFolderIsSuedEDS = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusArchiveFolderIsSuedEDS", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(192, 192, 192))));
                StatusArchiveFolderColor.ColorStatusArchiveFolderReceivedEDS = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusArchiveFolderReceivedEDS", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(192, 192, 192))));
                StatusArchiveFolderColor.ColorStatusArchiveFolderIsSuedTK = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusArchiveFolderIsSuedTK", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(192, 192, 192))));
                StatusArchiveFolderColor.ColorStatusArchiveFolderReceivedTK = ColorTranslator.FromHtml(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_colorStatusArchiveFolderReceivedTK", workingField: 1, user: BVVGlobal.oApp.User, obj: ColorTranslator.ToHtml(Color.FromArgb(192, 192, 192))));
            }
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            _archiveFoldersChange.Criteria = await cls_BaseSpr.GetCustomerCriteria(null, nameof(ArchiveFolderChange.Customer));

            await GetStatusArchiveFolderColor();
            gridControlArchiveFolders.DataSource = _archiveFoldersChange;

            if (gridViewArchiveFolders.Columns[nameof(Customer.Oid)] != null)
            {
                //gridViewArchiveFolders.Columns[nameof(Customer.Oid)].Visible = false;
                gridViewArchiveFolders.Columns[nameof(Customer.Oid)].Caption = "№";
                gridViewArchiveFolders.Columns[nameof(Customer.Oid)].Width = 25;
                gridViewArchiveFolders.Columns[nameof(Customer.Oid)].OptionsColumn.FixedWidth = true;
            }

            if (System.IO.File.Exists(pathSaveLayoutToXmlMainGrid))
            {
                gridViewArchiveFolders.RestoreLayoutFromXml(pathSaveLayoutToXmlMainGrid);
            }
        }

        private void btnArchiveFolderForm_Click(object sender, EventArgs e)
        {
            var year = default(int?);

            if (!string.IsNullOrWhiteSpace(cmbYear.Text) && cmbYear.SelectedIndex != -1 && int.TryParse(cmbYear.Text, out int result))
            {
                if (result > 1900 && result < 3000)
                {
                    year = result;
                }
                else
                {
                    cmbYear.Focus();
                    XtraMessageBox.Show("Значение года вышло за период 1900 < год < 3000",
                        "Ошибка формирования",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                cmbYear.Focus();
                XtraMessageBox.Show("Укажите год для формирования архивных папок.",
                    "Ошибка формирования",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            var accountantResponsible = default(Staff);
            var customer = default(Customer);
            var archiveFolder = default(ArchiveFolder);

            if (btnAccountantResponsible.EditValue is Staff)
            {
                accountantResponsible = btnAccountantResponsible.EditValue as Staff;
            }

            if (btnCustomer.EditValue is Customer)
            {
                customer = btnCustomer.EditValue as Customer;
            }

            if (btnArchiveFolder.EditValue is ArchiveFolder)
            {
                archiveFolder = btnArchiveFolder.EditValue as ArchiveFolder;
            }

            if (!string.IsNullOrWhiteSpace(cmbPeriod.Text) && cmbPeriod.SelectedIndex != -1)
            {
                var period = default(PeriodArchiveFolder);

                foreach (PeriodArchiveFolder item in Enum.GetValues(typeof(PeriodArchiveFolder)))
                {
                    if (item.GetEnumDescription().Equals(cmbPeriod.Text))
                    {
                        period = item;
                        break;
                    }
                }

                GetArchiveFolderPeriod(period, Convert.ToInt32(year), accountantResponsible, customer, archiveFolder);
            }
            else
            {
                cmbPeriod.Focus();
                XtraMessageBox.Show("Укажите период для формирования архивных папок.", "Ошибка формирования", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void GetArchiveFolderPeriod(PeriodArchiveFolder period, int year, Staff accountantResponsible, Customer customer, ArchiveFolder archiveFolder)
        {
            if (XtraMessageBox.Show($"После нажатия будет сформирован список архивных папок за {period.GetEnumDescription()} {year}",
                    "Формирование отчетов",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.OK)
            {

                var groupOperatorCutomer = new GroupOperator(GroupOperatorType.And);

                var criteriaCustomerStatus = new BinaryOperator($"{nameof(Customer.CustomerStatus)}.{nameof(CustomerStatus.Status)}.{nameof(Status.IsFormationArchiveFolder)}", true);
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

                var collectionCustomer = new XPCollection<Customer>(Session, groupOperatorCutomer);

                if (collectionCustomer != null && collectionCustomer.Count > 0)
                {
                    foreach (var item in collectionCustomer)
                    {
                        var customerArchiveFolders = new List<CustomerArchiveFolder>();

                        if (archiveFolder == null)
                        {
                            customerArchiveFolders = item.CustomerArchiveFolders.ToList();
                        }
                        else
                        {
                            customerArchiveFolders = item.CustomerArchiveFolders.Where(w => w.ArchiveFolder == archiveFolder).ToList();
                        }

                        foreach (var customerArchiveFolder in customerArchiveFolders)
                        {
                            if (customerArchiveFolder.PeriodArchiveFolder == period)
                            {
                                CreateArchiveFolderPeriod(period, year, item, customerArchiveFolder.ArchiveFolder);
                            }
                        }
                    }
                    Session.Save(_archiveFoldersChange);

                    _archiveFoldersChange?.Reload();
                }
            }
        }

        /// <summary>
        /// Создание отчетности за период.
        /// </summary>
        /// <param name="period">Период отчетности.</param>
        /// <param name="year">Год.</param>
        /// <param name="customer">Клиент.</param>
        private void CreateArchiveFolderPeriod(PeriodArchiveFolder period, int year, Customer customer, ArchiveFolder archiveFolder)
        {
            if (archiveFolder != null)
            {
                var groupCriteriaArchiveFolderChange = new GroupOperator(GroupOperatorType.And);

                var criteriaCustomer = new BinaryOperator(nameof(ArchiveFolderChange.Customer), customer);
                groupCriteriaArchiveFolderChange.Operands.Add(criteriaCustomer);

                var criteriaReport = new BinaryOperator(nameof(ArchiveFolderChange.ArchiveFolder), archiveFolder);
                groupCriteriaArchiveFolderChange.Operands.Add(criteriaReport);

                var criteriaYear = new BinaryOperator(nameof(ArchiveFolderChange.Year), year);
                groupCriteriaArchiveFolderChange.Operands.Add(criteriaYear);

                var criteriaPeriod = new BinaryOperator(nameof(ArchiveFolderChange.Period), period);
                groupCriteriaArchiveFolderChange.Operands.Add(criteriaPeriod);

                var reportChange = Session.FindObject<ArchiveFolderChange>(groupCriteriaArchiveFolderChange);

                if (reportChange is null)
                {
                    reportChange = new ArchiveFolderChange(Session)
                    {
                        AccountantResponsible = customer.AccountantResponsible,
                        Staffed = customer.PrimaryResponsible,
                        Customer = customer,
                        Year = year,
                        Period = period,
                        ArchiveFolder = archiveFolder,
                        StatusArchiveFolder = StatusArchiveFolder.NEW,
                        UserCreate = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid),
                        DateCreate = DateTime.Now
                    };
                    _archiveFoldersChange.Add(reportChange);
                }
            }
        }

        private void gridViewReports_RowStyle(object sender, RowStyleEventArgs e)
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
                    else if (statusArchiveFolderChange.Equals(StatusArchiveFolder.DESTROYED.GetEnumDescription()))
                    {
                        e.Appearance.BackColor = StatusArchiveFolderColor.ColorStatusArchiveFolderDestroyed;
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

        private void btnCustomer_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<Customer>(Session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.Customer, 1, null, null, false, null, string.Empty, false, true);
        }

        private void btnArchiveFolder_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<ArchiveFolder>(Session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.ArchiveFolder, 1, null, null, false, null, string.Empty, false, true);
        }

        private void btnAccountantResponsible_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<Staff>(Session, buttonEdit, (int)cls_App.ReferenceBooks.Staff, 1, new NullOperator(nameof(Staff.DateDismissal)), null, false, null, string.Empty, false, true);
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

        private void Filter(object sender, EventArgs e)
        {
            var groupOperator = new GroupOperator(GroupOperatorType.And);

            if (btnCustomer.EditValue is Customer customer)
            {
                var criteriaCustomer = new BinaryOperator(nameof(ArchiveFolderChange.Customer), customer);
                groupOperator.Operands.Add(criteriaCustomer);
            }

            if (btnArchiveFolder.EditValue is ArchiveFolder archiveFolder)
            {
                var criteriaReport = new BinaryOperator(nameof(ArchiveFolderChange.ArchiveFolder), archiveFolder);
                groupOperator.Operands.Add(criteriaReport);
            }

            if (btnAccountantResponsible.EditValue is Staff accountantResponsible)
            {
                var criteriaAccountantResponsible = new BinaryOperator(nameof(ArchiveFolderChange.AccountantResponsible), accountantResponsible);
                groupOperator.Operands.Add(criteriaAccountantResponsible);
            }

            if (cmbStatusArchiveFolder.EditValue != null)
            {
                foreach (StatusArchiveFolder statusReport in Enum.GetValues(typeof(StatusArchiveFolder)))
                {
                    if (statusReport.GetEnumDescription().Equals(cmbStatusArchiveFolder.Text))
                    {
                        var criteriaStatusReport = new BinaryOperator(nameof(ArchiveFolderChange.StatusArchiveFolder), statusReport);
                        groupOperator.Operands.Add(criteriaStatusReport);
                        break;
                    }
                }
            }

            if (cmbPeriod.EditValue != null)
            {
                var groupOperatorPeriod = new GroupOperator(GroupOperatorType.Or);

                foreach (PeriodArchiveFolder period in Enum.GetValues(typeof(PeriodArchiveFolder)))
                {
                    if (period.GetEnumDescription().Equals(cmbPeriod.Text))
                    {
                        var criteriaPeriod = new BinaryOperator(nameof(ArchiveFolderChange.Period), period);
                        groupOperatorPeriod.Operands.Add(criteriaPeriod);
                        groupOperatorPeriod.Operands.Add(GetGroupOperator(period));

                        groupOperator.Operands.Add(groupOperatorPeriod);
                        break;
                    }
                }
            }

            if (cmbYear.EditValue != null)
            {
                if (int.TryParse(cmbYear.Text, out int year))
                {
                    var groupOperatorYear = new GroupOperator(GroupOperatorType.Or);

                    var criteriaYear = new BinaryOperator(nameof(ReportChange.Year), year);
                    groupOperatorYear.Operands.Add(criteriaYear);

                    var criteriaContainsYear = new ContainsOperator(nameof(ArchiveFolderChange.ArchiveFolderChangePeriods),
                            new BinaryOperator(nameof(ArchiveFolderChangePeriod.Year), year));
                    groupOperatorYear.Operands.Add(criteriaContainsYear);

                    groupOperator.Operands.Add(groupOperatorYear);
                }
            }

            if (_archiveFoldersChange != null)
            {
                if (groupOperator.Operands.Count == 0)
                {
                    _archiveFoldersChange.Filter = null;
                }
                else
                {
                    _archiveFoldersChange.Filter = groupOperator;
                }
            }
        }

        private GroupOperator GetGroupOperator(PeriodArchiveFolder periodArchiveFolder)
        {
            var groupOperator = new GroupOperator(GroupOperatorType.And);

            foreach (PeriodReportChange period in Enum.GetValues(typeof(PeriodReportChange)))
            {
                if (periodArchiveFolder == PeriodArchiveFolder.YEAR)
                {
                    if (period != PeriodReportChange.YEAR)
                    {
                        continue;
                    }
                }
                else if (periodArchiveFolder == PeriodArchiveFolder.HALFYEAR)
                {
                    if (period != PeriodReportChange.FIRSTHALFYEAR || period != PeriodReportChange.SECONDHALFYEAR)
                    {
                        continue;
                    }
                }
                else if (periodArchiveFolder == PeriodArchiveFolder.QUARTER)
                {
                    if (period != PeriodReportChange.FIRSTQUARTER
                        || period != PeriodReportChange.SECONDQUARTER
                        || period != PeriodReportChange.THIRDQUARTER
                        || period != PeriodReportChange.FOURTHQUARTER)
                    {
                        continue;
                    }
                }
                else if (periodArchiveFolder == PeriodArchiveFolder.MONTH)
                {
                    if (period != PeriodReportChange.MONTH)
                    {
                        continue;
                    }
                }

                var criteria = new ContainsOperator(nameof(ArchiveFolderChange.ArchiveFolderChangePeriods),
                            new BinaryOperator(nameof(ArchiveFolderChangePeriod.PeriodReportChange), period));

                groupOperator.Operands.Add(criteria);
            }

            if (groupOperator.Operands.Count > 0)
            {
                return groupOperator;
            }
            else
            {
                return default;
            }

        }

        private void gridViewReports_MouseDown(object sender, MouseEventArgs e)
        {
            DXMouseEventArgs dxMouseEventArgs = e as DXMouseEventArgs;
            GridView gridview = sender as GridView;
            _ = gridview.CalcHitInfo(dxMouseEventArgs.Location);
            //if ((gridHitInfo.InRow || gridHitInfo.InRowCell) && dxMouseEventArgs.Button == MouseButtons.Right)
            if (dxMouseEventArgs.Button == MouseButtons.Right)
            {
                popupArchiveFolderChange.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
            }
        }

        private void btnAddReportChange_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new ArchiveFolderChangeEdit();
            form.ShowDialog();

            var archiveFolderChange = form.ArchiveFolderChange;
            if (archiveFolderChange?.Oid != -1)
            {
                _archiveFoldersChange.Reload();
                gridViewArchiveFolders.FocusedRowHandle = gridViewArchiveFolders.LocateByValue(nameof(ArchiveFolderChange.Oid), archiveFolderChange.Oid);
            }
        }

        private void btnEditReportChange_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewArchiveFolders.IsEmpty)
            {
                return;
            }

            var archiveFolderChange = gridViewArchiveFolders.GetRow(gridViewArchiveFolders.FocusedRowHandle) as ArchiveFolderChange;
            if (archiveFolderChange != null)
            {
                var form = new ArchiveFolderChangeEdit(archiveFolderChange);
                form.ShowDialog();
            }
        }

        private void btnDelReportChange_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewArchiveFolders.IsEmpty)
            {
                return;
            }

            var listArchiveFolderChange = new List<ArchiveFolderChange>();
            var msg = default(string);

            foreach (var focusedRowHandle in gridViewArchiveFolders.GetSelectedRows())
            {
                var archiveFolderChange = gridViewArchiveFolders.GetRow(focusedRowHandle) as ArchiveFolderChange;

                if (archiveFolderChange != null)
                {
                    msg += $"{archiveFolderChange}{Environment.NewLine}";
                    listArchiveFolderChange.Add(archiveFolderChange);
                }
            }

            if (XtraMessageBox.Show($"Вы собираетесь удалить следующие архивы папок:{Environment.NewLine}{msg}Хотите продолжить?",
                    "Удаление архивных папок",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.OK)
            {

                Session.Delete(listArchiveFolderChange);
            }
        }

        private void btnRefreshReportChange_ItemClick(object sender, ItemClickEventArgs e)
        {
            _archiveFoldersChange.Reload();
        }

        private async void btnSettingArchiveFolderChange_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new formProgramSettings(2);
            form.ShowDialog();

            await GetStatusArchiveFolderColor();
        }

        private void btnPrintAcceptanceCertificate_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewArchiveFolders.IsEmpty)
            {
                return;
            }

            var listArchiveFolderChange = new List<ArchiveFolderChange>();

            foreach (var focusedRowHandle in gridViewArchiveFolders.GetSelectedRows())
            {
                var archiveFolderChange = gridViewArchiveFolders.GetRow(focusedRowHandle) as ArchiveFolderChange;

                if (archiveFolderChange != null)
                {
                    listArchiveFolderChange.Add(archiveFolderChange);
                }
            }

            if (listArchiveFolderChange.Count > 0)
            {
                _ = new AcceptanceCertificate(listArchiveFolderChange);
            }
        }

        private void barBtnSaveLayoutToXmlMainGrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!Directory.Exists(pathTempDirectory))
            {
                Directory.CreateDirectory(pathTempDirectory);
            }

            gridViewArchiveFolders.SaveLayoutToXml(pathSaveLayoutToXmlMainGrid);
        }

        private void btnFreeBoxes_Click(object sender, EventArgs e)
        {
            using (var collection = new XPCollection<ArchiveFolderChange>())
            {
                var arrayNumber = collection.Where(w => w.BoxNumber != null && w.StatusArchiveFolder != StatusArchiveFolder.ISSUED).Select(s => s.BoxNumber).ToList();

                var intList = new List<int>();
                for (int i = 1; i < 100; i++)
                {
                    if (!arrayNumber.Contains(i))
                    {
                        intList.Add(i);
                    }
                }

                var arrayNumberDistinct = intList.OrderBy(o => o).ToList();

                var result = "Свободные номера коробок: ";

                foreach (var item in arrayNumberDistinct)
                {
                    result += $"{item}; ";
                }

                XtraMessageBox.Show(result);
            }
        }

        private void barBtnTaskAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            BVVGlobal.oFuncXpo.CreateTask<TaskEdit>(Session);
        }

        private void barBtnControlSystemAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewArchiveFolders.IsEmpty)
            {
                return;
            }

            var obj = gridViewArchiveFolders.GetRow(gridViewArchiveFolders.FocusedRowHandle) as ArchiveFolderChange;
            if (obj != null)
            {
                var form = new ControlSystemEdit(obj);
                form.ShowDialog();
            }
        }

        private void barBtnBulkReplacement_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewArchiveFolders.IsEmpty)
            {
                return;
            }

            var objList = new List<ArchiveFolderChange>();

            foreach (var focusedRowHandle in gridViewArchiveFolders.GetSelectedRows())
            {
                if (gridViewArchiveFolders.GetRow(focusedRowHandle) is ArchiveFolderChange obj)
                {
                    objList.Add(obj);
                }
            }

            if (objList != null && objList.Count > 0)
            {
                var form = new ArchiveFolderChangeEdit(Session, objList);
                form.ShowDialog();
            }
        }
    }
}