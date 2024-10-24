using DevExpress.Data.Filtering;
using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model;
using RMS.Core.Model.Chronicle;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Mail;
using RMS.Core.Model.Reports;
using RMS.Setting.Model.GeneralSettings;
using RMS.UI.ColorsSettings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Forms.Directories
{
    public partial class ReportChangeEdit : XtraForm
    {
        private CorrectiveReport _correctiveReport;
        private ReportChange _currentReportChange;
        private bool _isCorrective;
        private Session _session { get; }
        private Customer _customer { get; }
        
        public ReportChange ReportChange { get; }
        public List<ReportChange> ReportChanges { get; set; }
        public bool IsMassChange { get; set; }

        private int xLocation = -1;
        private int yLocation = -1;

        public void SetFormPosition(Point point)
        {
            xLocation = point.X;
            yLocation = point.Y;
        }

        private ReportChangeEdit()
        {
            InitializeComponent();

            foreach (StatusReport item in Enum.GetValues(typeof(StatusReport)))
            {
                if (item == StatusReport.NEEDSADJUSTMENTOURFAULT 
                    || item == StatusReport.NEEDSADJUSTMENTCUSTOMERFAULT
                    || item == StatusReport.CORRECTION)
                {
                    continue;
                }
                cmbStatusReport.Properties.Items.Add(item.GetEnumDescription());
            }
            cmbStatusReport.SelectedIndex = 0;

            foreach (Month item in Enum.GetValues(typeof(Month)))
            {
                cmbMonth.Properties.Items.Add(item.GetEnumDescription());
            }

            foreach (PeriodArchiveFolder item in Enum.GetValues(typeof(PeriodArchiveFolder)))
            {
                if (item != PeriodArchiveFolder.NEEDNOT)
                {
                    cmbPeriod.Properties.Items.Add(item.GetEnumDescription());
                }
            }
        }
        
        public ReportChangeEdit(Session session) : this()
        {
            if (_session is null)
            {
                _session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                ReportChange = new ReportChange(_session);
            }
        }

        public ReportChangeEdit(StatisticalReport statisticalReport) : this()
        {
            _session = statisticalReport.Session;
            _customer = statisticalReport.Customer;         

            ReportChange = new ReportChange(_session)
            {
                Year = statisticalReport.Year,
                AccountantResponsible = statisticalReport.Responsible,
                Customer = statisticalReport.Customer,
                Report = statisticalReport.Report,
                StatusReport = StatusReport.NEW
            };
            
            var dateTimeNow = DateTime.Now;
            switch (statisticalReport?.Report.Periodicity)
            {
                case Periodicity.MONTHLY:
                    ReportChange.PeriodArchiveFolder = PeriodArchiveFolder.MONTH;
                    ReportChange.PeriodReportChange = PeriodReportChange.MONTH;
                    ReportChange.PeriodChangeMonth = (Month)dateTimeNow.Month;
                    break;

                case Periodicity.QUARTERLY:
                    ReportChange.PeriodArchiveFolder = PeriodArchiveFolder.QUARTER;
                    switch (dateTimeNow.Month)
                    {
                        case 1:
                            ReportChange.PeriodReportChange = PeriodReportChange.FIRSTQUARTER;
                            break;
                        case 2:
                            ReportChange.PeriodReportChange = PeriodReportChange.FIRSTQUARTER;
                            break;
                        case 3:
                            ReportChange.PeriodReportChange = PeriodReportChange.FIRSTQUARTER;
                            break;

                        case 4:
                            ReportChange.PeriodReportChange = PeriodReportChange.SECONDQUARTER;
                            break;
                        case 5:
                            ReportChange.PeriodReportChange = PeriodReportChange.SECONDQUARTER;
                            break;
                        case 6:
                            ReportChange.PeriodReportChange = PeriodReportChange.SECONDQUARTER;
                            break;

                        case 7:
                            ReportChange.PeriodReportChange = PeriodReportChange.THIRDQUARTER;
                            break;
                        case 8:
                            ReportChange.PeriodReportChange = PeriodReportChange.THIRDQUARTER;
                            break;
                        case 9:
                            ReportChange.PeriodReportChange = PeriodReportChange.THIRDQUARTER;
                            break;

                        case 10:
                            ReportChange.PeriodReportChange = PeriodReportChange.FOURTHQUARTER;
                            break;
                        case 11:
                            ReportChange.PeriodReportChange = PeriodReportChange.FOURTHQUARTER;
                            break;
                        case 12:
                            ReportChange.PeriodReportChange = PeriodReportChange.FOURTHQUARTER;
                            break;
                    }                    
                    break;

                case Periodicity.HALFEEARLY:
                    ReportChange.PeriodArchiveFolder = PeriodArchiveFolder.QUARTER;
                    if (dateTimeNow.Month >= 1 && dateTimeNow.Month <= 6)
                    {
                        ReportChange.PeriodReportChange = PeriodReportChange.FIRSTHALFYEAR;
                    }
                    else
                    {
                        ReportChange.PeriodReportChange = PeriodReportChange.SECONDHALFYEAR;
                    }
                    break;

                default:
                    ReportChange.PeriodArchiveFolder = PeriodArchiveFolder.YEAR;
                    ReportChange.PeriodReportChange = PeriodReportChange.YEAR;
                    break;
            }

            layoutControlGroupDate.AppearanceGroup.BorderColor = Color.LightCoral;
        }

        public ReportChangeEdit(ReportChange reportChange) : this()
        {
            ReportChange = reportChange;
            _customer = reportChange.Customer;
            _session = reportChange.Session;
        }

        public ReportChangeEdit(CorrectiveReport correctiveReport) : this(correctiveReport.ReportChange)
        {
            _correctiveReport = correctiveReport;
        }
        
        public ReportChangeEdit(ReportChange reportChange, bool isCorrective) : this()
        {
            _isCorrective = isCorrective;
            _currentReportChange = reportChange;
            _customer = reportChange.Customer;
            _session = reportChange.Session;
            
            ReportChange = new ReportChange(_session)
            {
                IsCorrective = true,
                Year = reportChange.Year,
                AccountantResponsible = reportChange.AccountantResponsible,
                Customer = reportChange.Customer,
                Report = reportChange.Report,
                //DateCompletion = reportChange.DateCompletion,
                //Day = reportChange.Day,
                //Month = reportChange.Month,
                //DeliveryYear = reportChange.DeliveryYear,
                PeriodArchiveFolder = reportChange.PeriodArchiveFolder,
                PeriodChangeMonth = reportChange.PeriodChangeMonth,
                PeriodReportChange = reportChange.PeriodReportChange,
                StatusReport = StatusReport.ADJUSTMENTREQUIRED
            };
        }

        public ReportChangeEdit(Customer customer) : this()
        {           
            _customer = customer;
            _session = customer.Session; 
            ReportChange = new ReportChange(_session);
        }

        public ReportChangeEdit(Session session, List<ReportChange> reportChanges) : this()
        {
            IsMassChange = true;
            _session = session;
            ReportChanges = reportChanges;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (IsMassChange)
            {
                SaveMassReportChange();
                Close();
            }
            else
            {
                if (await SaveReportChange())
                {
                    Close();
                }
            }            
        }
        
        private async System.Threading.Tasks.Task<bool> SaveReportChange()
        {
            if (_isCorrective)
            {
                if (checkIsOurFault.Checked is false 
                    && checkIsCustomerFault.Checked is false)
                {
                    XtraMessageBox.Show("Для сохранения корректирующего отчета обязательно необходимо указать тип вины исполнения.", 
                        "Проверка сохранения",
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Information);
                    checkIsCustomerFault.Focus();
                    return false;
                }
            }
            
            var correctiveNumber = txtCorrectiveNumber.Text;
            var cause = memoCause.Text;
            
            var accountantResponsible = btnAccountantResponsible.EditValue as Staff;
            var passedStaff = btnPassedStaff.EditValue as Staff ?? await _session.GetObjectByKeyAsync<Staff>(DatabaseConnection.User?.Staff?.Oid);
            var customer = btnCustomer.EditValue as Customer;
            var report = btnReport.EditValue as Report;
            var dtCmpletion = dateCompletion.DateTime == DateTime.MinValue ? default(DateTime?) : Convert.ToDateTime(dateCompletion.EditValue);

            var isOurFault = checkIsOurFault.Checked;
            var isCustomerFault = checkIsCustomerFault.Checked;

            var statusReport = default(StatusReport);
            if (cmbStatusReport.SelectedIndex != -1)
            {
                foreach (StatusReport item in Enum.GetValues(typeof(StatusReport)))
                {
                    if (item.GetEnumDescription().Equals(cmbStatusReport.EditValue))
                    {
                        statusReport = item;
                    }
                }
            }
            
            var month = (Month)cmbMonth.SelectedIndex + 1;
            var comment = memoComment.Text;

            var periodArchiveFolder = default(PeriodArchiveFolder?);
            if (cmbPeriod.SelectedIndex != -1)
            {
                foreach (PeriodArchiveFolder item in Enum.GetValues(typeof(PeriodArchiveFolder)))
                {
                    if (item.GetEnumDescription().Equals(cmbPeriod.EditValue))
                    {
                        periodArchiveFolder = item;
                    }
                }
            }
            else
            {
                periodArchiveFolder = null;
            }

            var periodReportChange = default(PeriodReportChange?);
            if (cmbPeriodChange.SelectedIndex != -1)
            {
                foreach (PeriodReportChange item in Enum.GetValues(typeof(PeriodReportChange)))
                {
                    if (item.GetEnumDescription().Equals(cmbPeriodChange.EditValue))
                    {
                        periodReportChange = item;
                    }
                }
            }
            else
            {
                periodReportChange = null;
            }

            var periodChangeMonth = default(Month?);
            if (cmbPeriodChangeMonth.SelectedIndex != -1)
            {
                foreach (Month item in Enum.GetValues(typeof(Month)))
                {
                    if (item.GetEnumDescription().Equals(cmbPeriodChangeMonth.EditValue))
                    {
                        periodChangeMonth = item;
                    }
                }
            }
            else
            {
                periodChangeMonth = null;
            }

            if (int.TryParse(txtDay.Text, out int day))
            {
                if (day <= 0 && day >= 31)
                {
                    txtDay.Focus();
                    XtraMessageBox.Show("День не попадает в период от 1 до 31", "Ошибка сохранения дня", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            if (int.TryParse(cmbYear.Text, out int year))
            {
                if (year <= 2000 && year >= 2100)
                {
                    cmbYear.Focus();
                    XtraMessageBox.Show("Год не попадает в период от 2000 до 2100", "Ошибка сохранения дня", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            if (int.TryParse(cmbDeliveryYear.Text, out int deliveryYear))
            {
                if (deliveryYear <= 2000 && deliveryYear >= 2100)
                {
                    cmbDeliveryYear.Focus();
                    XtraMessageBox.Show("Год не попадает в период от 2000 до 2100", "Ошибка сохранения дня", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            if (ReportChange.UserCreate is null)
            {
                ReportChange.UserCreate = _session.GetObjectByKey<User>(DatabaseConnection.User.Oid);
                ReportChange.DateCreate = DateTime.Now;
            }

            if (ReportChange.Oid > 0 &&
                (
                    ReportChange.AccountantResponsible != accountantResponsible ||
                    ReportChange.Customer != customer ||
                    ReportChange.Report != report ||
                    ReportChange.DateCompletion != dtCmpletion ||
                    ReportChange.StatusReport != statusReport ||
                    ReportChange.Month != month ||
                    ReportChange.Comment != comment ||
                    ReportChange.PeriodReportChange != periodReportChange ||
                    ReportChange.PeriodArchiveFolder != periodArchiveFolder ||
                    ReportChange.PeriodChangeMonth != periodChangeMonth ||
                    ReportChange.Day != day ||
                    ReportChange.DeliveryYear != deliveryYear ||
                    ReportChange.Year != year
                ))
            {
                ReportChange.ChronicleReportChanges.Add(new ChronicleReportChange(_session)
                {
                    AccountantResponsible = ReportChange.AccountantResponsible,
                    Customer = ReportChange.Customer,
                    Report = ReportChange.Report,
                    DateCompletion = ReportChange.DateCompletion,
                    StatusReport = ReportChange.StatusReport,
                    Month = ReportChange.Month,
                    Comment = ReportChange.Comment,
                    PeriodReportChange = ReportChange.PeriodReportChange,
                    PeriodArchiveFolder = ReportChange.PeriodArchiveFolder,
                    PeriodChangeMonth = ReportChange.PeriodChangeMonth,
                    Day = ReportChange.Day,
                    Year = ReportChange.Year,
                    DeliveryYear = ReportChange.DeliveryYear,
                    UserUpdate = _session.GetObjectByKey<User>(DatabaseConnection.User.Oid),
                    DateUpdate = DateTime.Now
                });
            }
            
            if (accountantResponsible != null)
            {
                ReportChange.AccountantResponsible = accountantResponsible;
            }
            else
            {
                ReportChange.AccountantResponsible = null;
            }

            if (customer != null)
            {
                ReportChange.Customer = customer;
            }
            else
            {
                ReportChange.Customer = null;
            }

            if (report != null)
            {
                ReportChange.Report = report;
            }
            else
            {
                ReportChange.Report = null;
            }

            ReportChange.DateCompletion = dtCmpletion;
            ReportChange.StatusReport = statusReport;
            ReportChange.Month = month;
            ReportChange.Comment = comment;
            ReportChange.PeriodReportChange = periodReportChange;
            ReportChange.PeriodArchiveFolder = periodArchiveFolder;
            ReportChange.Day = day;
            ReportChange.Year = year;
            ReportChange.DeliveryYear = deliveryYear;
            ReportChange.PeriodChangeMonth = periodChangeMonth;

            ReportChange.PassedStaff = passedStaff;

            ReportChange.IsCustomerFault = isCustomerFault;
            ReportChange.IsOurFault = isOurFault;

            ReportChange.Save();

            if (_correctiveReport != null)
            {
                _correctiveReport.CorrectiveNumber = txtCorrectiveNumber.Text;
                _correctiveReport.Cause = memoCause.Text;
                _correctiveReport.PassedStaff = passedStaff;
                _correctiveReport.Save();
            }
            
            if (_isCorrective)
            {
                _currentReportChange.CorrectiveReports.Add(new CorrectiveReport(_session)
                {
                    CorrectiveNumber = correctiveNumber,
                    ReportChange = ReportChange,
                    Cause = cause,
                    PassedStaff = passedStaff
                });
                _currentReportChange.Save();
            }

            if (ReportChange.Task is null && isCustomerFault)
            {
                var user = await _session.GetObjectByKeyAsync<User>(DatabaseConnection.User.Oid);
                var task = new Task(_session)
                {
                    Name = "Выставление счета за дополнительные услуги",
                    Description = "Создание корректирующего отчета по вине заказчика",
                    Date = DateTime.Now.Date,
                    Deadline = DateTime.Now.Date.AddDays(1),
                    Customer = ReportChange.Customer,
                    TypeTask = TypeTask.Task,
                    StatusTask = StatusTask.Incoming,                    
                    GivenStaff = user?.Staff,
                    Staff = user?.Staff,
                    Comment = "Автоматически сформированная задачи при сохранении отчета",
                    TaskStatus = _session.FindObject<TaskStatus>(new BinaryOperator(nameof(TaskStatus.IsDefault), true)),
                };
                task.Save();
                
                ReportChange.Task = task;
                ReportChange.Save();

                await TaskEdit.SendMessageTelegram(task, user?.Staff, true);
                                
                var isCreateAdditionalServices = default(string);
                if (user?.Staff != null)
                {
                    var additionalServices = new AdditionalServices(_session)
                    {
                        Staff = user.Staff,
                        Month = (Month)DateTime.Now.Month,
                        Year = DateTime.Now.Year,
                        Description = Letter.StringToByte("Создание корректирующего отчета по вине заказчика"),
                        Comment = Letter.StringToByte("Автоматически сформированная услуга при сохранении отчета"),
                        Customer = ReportChange.Customer,
                        Task = task
                    };

                    var settings = await _session.FindObjectAsync<Settings>(null);
                    if (settings != null)
                    {
                        var priceList = Settings.GetDeserializeObject<List<int>>(settings.PriceList);
                        if (priceList != null && priceList.Count > 0)
                        {
                            foreach (var priceOid in priceList)
                            {
                                var obj = new AdditionalServicesObj(_session)
                                {
                                    PriceList = await _session.GetObjectByKeyAsync<PriceList>(priceOid)
                                };
                                obj.Value = obj.GetValue();                                
                                additionalServices.AdditionalServicesObj.Add(obj);
                            }
                        }
                    }

                    additionalServices.Save();

                    task.AdditionalServices = additionalServices;
                    task.Save();
                    
                    isCreateAdditionalServices = " и услуга";
                }
                
                XtraMessageBox.Show($"Сформирована автоматическая задача{isCreateAdditionalServices} по отчету",
                                    "Регламентные операции",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
            }
            
            return true;
        }
        
        private void SaveMassReportChange()
        {            
            var statusReport = default(StatusReport?); 
            var comment = memoComment.Text;
            var deliveryYear = default(int?); 
            var userUpdate = _session.GetObjectByKey<User>(DatabaseConnection.User.Oid);
            var dateUpdate = DateTime.Now;
            var dtCmpletion = default(DateTime?);
            var accountantResponsible = default(Staff);

            var month = default(Month?);
            var day = default(int?);
            var year = default(int?);

            if (cmbMonth.SelectedIndex != -1)
            {
                month = (Month)cmbMonth.SelectedIndex + 1;
            }

            if (int.TryParse(txtDay.Text, out int _day))
            {
                if (_day >= 0 && _day <= 31)
                {
                    day = _day;
                }
            }

            if (int.TryParse(cmbYear.Text, out int _year))
            {
                if (_year >= 2000 && _year <= 2100)
                {
                    year = _year;
                }
            }

            if (month is null || day is null || year is null)
            {
                month = null;
                day = null;
                year = null;
            }

            if (btnAccountantResponsible.EditValue is Staff staff)
            {
                accountantResponsible = staff;
            }            
            
            if (dateCompletion.EditValue is DateTime date)
            {
                dtCmpletion = date;
            }

            if (cmbStatusReport.SelectedIndex != -1)
            {
                foreach (StatusReport item in Enum.GetValues(typeof(StatusReport)))
                {
                    if (item.GetEnumDescription().Equals(cmbStatusReport.EditValue))
                    {
                        statusReport = item;
                    }
                }
            }          
            
            if (int.TryParse(cmbDeliveryYear.Text, out int tempYear))
            {
                if (deliveryYear <= 2000 && deliveryYear >= 2100)
                {
                    cmbDeliveryYear.Focus();
                    XtraMessageBox.Show("Год не попадает в период от 2000 до 2100", "Ошибка сохранения дня", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    deliveryYear = tempYear;
                }
            }
            
            foreach (var reportChange in ReportChanges)
            {
                if (reportChange.Oid > 0 &&
                (
                    (statusReport != null && reportChange.StatusReport != statusReport) ||
                    (month != null && reportChange.Month != month) ||
                    (day != null && reportChange.Day != day) ||
                    (year != null && reportChange.Year != year) ||
                    (accountantResponsible != null && reportChange.AccountantResponsible != accountantResponsible) ||
                    (dtCmpletion != null && reportChange.DateCompletion != dtCmpletion) ||
                    (!string.IsNullOrWhiteSpace(comment) && reportChange.Comment != comment) ||
                    (deliveryYear != null && reportChange.DeliveryYear != deliveryYear)
                ))
                {
                    reportChange.ChronicleReportChanges.Add(new ChronicleReportChange(_session)
                    {
                        AccountantResponsible = reportChange.AccountantResponsible,
                        Customer = reportChange.Customer,
                        Report = reportChange.Report,
                        DateCompletion = reportChange.DateCompletion,
                        StatusReport = reportChange.StatusReport,
                        Month = reportChange.Month,
                        Comment = reportChange.Comment,
                        PeriodReportChange = reportChange.PeriodReportChange,
                        PeriodArchiveFolder = reportChange.PeriodArchiveFolder,
                        PeriodChangeMonth = reportChange.PeriodChangeMonth,
                        Day = reportChange.Day,
                        Year = reportChange.Year,
                        DeliveryYear = reportChange.DeliveryYear,
                        UserUpdate = userUpdate,
                        DateUpdate = dateUpdate
                    });
                }

                if (month != null)
                {
                    reportChange.Month = (Month)month;
                }
                if (year != null)
                {
                    reportChange.Year = (int)year;
                }
                if (day != null)
                {
                    reportChange.Day = (int)day;
                }

                if (accountantResponsible != null)
                {
                    reportChange.AccountantResponsible = accountantResponsible;
                }
                
                if (statusReport != null)
                {
                    reportChange.StatusReport = (StatusReport)statusReport;
                }

                if (!string.IsNullOrWhiteSpace(comment))
                {
                    reportChange.Comment = comment;
                }

                if (deliveryYear != null)
                {
                    reportChange.DeliveryYear = deliveryYear;
                }

                if (dtCmpletion != null)
                {
                    reportChange.DateCompletion = dtCmpletion;
                }

                reportChange.Save();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCustomer_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            cls_BaseSpr.ButtonEditButtonClickBase<Customer>(_session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.Customer, 1, null, null, false, null, string.Empty, false, true);
        }

        private bool isEditReportChangeForm = false;
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
                            isEditReportChangeForm = accessRights.IsEditReportChangeForm;
                        }                        
                    }                    
                }

                btnSave.Enabled = isEditReportChangeForm;
                gridView.OptionsBehavior.Editable = isEditReportChangeForm;

                CustomerEdit.CloseButtons(btnCustomer, isEditReportChangeForm);
                CustomerEdit.CloseButtons(btnAccountantResponsible, isEditReportChangeForm);
                CustomerEdit.CloseButtons(btnReport, isEditReportChangeForm);
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async void ReportChangeEdit_Load(object sender, EventArgs e)
        {
            await SetAccessRights();

            BVVGlobal.oFuncXpo.SettingsSelectedRowGridView(ref gridViewCorrectiveReport);
            BVVGlobal.oFuncXpo.PressEnterGrid<CorrectiveReport, ReportChangeEdit>(gridViewCorrectiveReport);

            BVVGlobal.oFuncXpo.ButtoneEditDoubleClick<Staff>(_session, btnAccountantResponsible, cls_App.ReferenceBooks.Staff, isEnable: isEditReportChangeForm);
            BVVGlobal.oFuncXpo.ButtoneEditDoubleClick<Customer>(_session, btnCustomer, cls_App.ReferenceBooks.Customer, isEnable: isEditReportChangeForm);
            BVVGlobal.oFuncXpo.ButtoneEditDoubleClick<Customer>(_session, btnReport, cls_App.ReferenceBooks.Report, isEnable: isEditReportChangeForm);

            if (IsMassChange)
            {
                LoadMassReportChange();
            }
            else
            {
                LoadReportChange();
            }

            if (ReportChange?.IsCorrective is true)
            {
                layoutControlItemCreateCorrectiveReport.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlGroupCorrectiveReport.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                
                layoutControlGroupCorrectiveReportInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                tabbedControlGroupReportChange.SelectedTabPage = layoutControlGroupCorrectiveReportInfo;
            }
            else
            {
                ShowCorrectiveReport();
            }

            if (xLocation > 0)
            {
                Location = new Point(xLocation, yLocation);
            }
        }

        private void ShowCorrectiveReport()
        {
            if (ReportChange?.CorrectiveReports != null && ReportChange?.CorrectiveReports?.Count > 0)
            {
                layoutControlGroupCorrectiveReport.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                tabbedControlGroupReportChange.SelectedTabPage = layoutControlGroupCorrectiveReport;
            }
        }

        private RepositoryItemCheckEdit repositoryItemCheckEdit;
        
        private void LoadReportChange()
        {
            if (ReportChange.PeriodArchiveFolder == PeriodArchiveFolder.MONTH
                && ReportChange?.Report?.ReportPerformanceIndicators != null
                && ReportChange?.Report?.ReportPerformanceIndicators.Count > 0)
            {
                ReportChange?.Reload();
                ReportChange?.OrganizationPerformance?.Reload();
                ReportChange?.OrganizationPerformance?.CustomerPerformanceIndicators?.Reload();                
                
                if (ReportChange.OrganizationPerformance is null)
                {
                    var groupOperator = new GroupOperator(GroupOperatorType.And);
                    var criteriaYear = new BinaryOperator(nameof(OrganizationPerformance.Year), ReportChange.Year);
                    groupOperator.Operands.Add(criteriaYear);
                    var criteriaMonth = new BinaryOperator(nameof(OrganizationPerformance.Month), ReportChange.PeriodChangeMonth ?? ReportChange.Month);
                    groupOperator.Operands.Add(criteriaMonth);
                    var criteriaCustomer = new BinaryOperator(nameof(OrganizationPerformance.Customer), ReportChange.Customer);
                    groupOperator.Operands.Add(criteriaCustomer);
                    
                    var organizationPerformance = _session.FindObject<OrganizationPerformance>(groupOperator);
                    if (organizationPerformance is null)
                    {
                        organizationPerformance = new OrganizationPerformance(_session)
                        {
                            Year = ReportChange.Year,
                            Month = ReportChange.PeriodChangeMonth ?? ReportChange.Month,
                            Customer = ReportChange.Customer
                        };
                    }

                    ReportChange.OrganizationPerformance = organizationPerformance;
                    ReportChange.OrganizationPerformance.Save();
                }

                layoutControlGroupGridControlPerformanceIndicator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                tabbedControlGroupReportChange.SelectedTabPage = layoutControlGroupGridControlPerformanceIndicator;
                
                repositoryItemCheckEdit = new RepositoryItemCheckEdit();
                repositoryItemCheckEdit.ValueChecked = "true";
                repositoryItemCheckEdit.ValueUnchecked = "false";
                repositoryItemCheckEdit.CheckStyle = CheckStyles.Standard;

                ReportChange.OrganizationPerformance.CustomerPerformanceIndicators.Filter = null;
                var xpCollectionGroupPerformanceIndicator = new XPCollection<GroupPerformanceIndicator>(_session);
                foreach (var group in xpCollectionGroupPerformanceIndicator)
                {
                    group.PerformanceIndicators.Reload();
                    foreach (var indicator in group.PerformanceIndicators.Where(w => w.IsUseWhenGeneratingInformationOnEmployees == true))
                    {
                        if (ReportChange?.Report?.ReportPerformanceIndicators.FirstOrDefault(f => f.PerformanceIndicator != null && f.PerformanceIndicator.Oid == indicator.Oid) != null)
                        {
                            if (ReportChange.OrganizationPerformance.CustomerPerformanceIndicators.FirstOrDefault(f => f.PerformanceIndicator == indicator) is null)
                            {
                                ReportChange.OrganizationPerformance.CustomerPerformanceIndicators.Add(new CustomerPerformanceIndicator(_session)
                                {
                                    PerformanceIndicator = indicator,
                                    Value = null
                                });
                            }
                        }                        
                    }
                }
                
                gridControlPerformanceIndicator.DataSource = ReportChange.OrganizationPerformance.CustomerPerformanceIndicators;
                var groupOperatorIndicator = new GroupOperator(GroupOperatorType.Or);
                foreach (var item in ReportChange?.Report?.ReportPerformanceIndicators)
                {
                    var criteria = new BinaryOperator($"{nameof(CustomerPerformanceIndicator.PerformanceIndicator)}.{nameof(PerformanceIndicator.Oid)}", item.PerformanceIndicator.Oid);
                    groupOperatorIndicator.Operands.Add(criteria);
                }
                ReportChange.OrganizationPerformance.CustomerPerformanceIndicators.Filter = groupOperatorIndicator;
                
                foreach (GridColumn column in gridView.Columns)
                {
                    column.OptionsColumn.AllowEdit = false;
                    column.OptionsColumn.ReadOnly = true;
                }

                if (gridView.Columns[nameof(CustomerPerformanceIndicator.Oid)] != null)
                {
                    gridView.Columns[nameof(CustomerPerformanceIndicator.Oid)].Visible = false;
                    gridView.Columns[nameof(CustomerPerformanceIndicator.Oid)].Width = 18;
                    gridView.Columns[nameof(CustomerPerformanceIndicator.Oid)].OptionsColumn.FixedWidth = true;
                }

                if (gridView.Columns[nameof(CustomerPerformanceIndicator.GroupPerformanceIndicatorString)] != null)
                {
                    gridView.Columns[nameof(CustomerPerformanceIndicator.GroupPerformanceIndicatorString)].Group();
                    gridView.ExpandAllGroups();
                }

                if (gridView.Columns[nameof(CustomerPerformanceIndicator.Value)] != null)
                {
                    gridView.Columns[nameof(CustomerPerformanceIndicator.Value)].OptionsColumn.AllowEdit = true;
                    gridView.Columns[nameof(CustomerPerformanceIndicator.Value)].OptionsColumn.ReadOnly = false;
                    gridView.Columns[nameof(CustomerPerformanceIndicator.Value)].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                }
                
                if (ReportChange.OrganizationPerformance.Invoice != null)
                {
                    gridView.OptionsBehavior.Editable = false;
                }
            }            

            if (ReportChange.AccountantResponsible != null)
            {
                btnAccountantResponsible.EditValue = ReportChange.AccountantResponsible;
            }
            else
            {
                btnAccountantResponsible.EditValue = ReportChange.Customer?.AccountantResponsible ?? _customer?.AccountantResponsible;
            }

            btnCustomer.EditValue = _customer;
            btnReport.EditValue = ReportChange.Report;
            txtDay.EditValue = (ReportChange.Day == 0) ? 1 : ReportChange.Day;
            cmbMonth.SelectedIndex = ((int)ReportChange.Month - 1 == -1) ? 0 : (int)ReportChange.Month - 1;
            cmbYear.EditValue = (ReportChange.Year == 0) ? DateTime.Now.Year : ReportChange.Year;
            cmbDeliveryYear.EditValue = (ReportChange.DeliveryYear is null) ? DateTime.Now.Year : ReportChange.DeliveryYear;
            dateCompletion.EditValue = ReportChange.DateCompletion;
            cmbStatusReport.EditValue = ReportChange.StatusReport.GetEnumDescription();
            btnPassedStaff.EditValue = ReportChange.PassedStaff;

            checkIsCustomerFault.EditValue = ReportChange.IsCustomerFault;
            checkIsOurFault.EditValue = ReportChange.IsOurFault;

            if (ReportChange.PeriodArchiveFolder != null)
            {
                cmbPeriod.EditValue = ReportChange.PeriodArchiveFolder.GetEnumDescription();

                if (ReportChange.PeriodArchiveFolder == PeriodArchiveFolder.QUARTER || ReportChange.PeriodArchiveFolder == PeriodArchiveFolder.HALFYEAR)
                {
                    cmbPeriodChange.EditValue = ReportChange.PeriodReportChange?.GetEnumDescription();
                }
                else if (ReportChange.PeriodArchiveFolder == PeriodArchiveFolder.MONTH)
                {
                    cmbPeriodChangeMonth.EditValue = (ReportChange.PeriodChangeMonth).GetEnumDescription();                   
                }
            }
            else
            {
                if (ReportChange.PeriodReportChange != null)
                {
                    var periodArchiveFolder = default(PeriodArchiveFolder);

                    if (ReportChange.PeriodReportChange == PeriodReportChange.YEAR)
                    {
                        periodArchiveFolder = PeriodArchiveFolder.YEAR;
                    }
                    else if (ReportChange.PeriodReportChange == PeriodReportChange.FIRSTHALFYEAR ||
                            ReportChange.PeriodReportChange == PeriodReportChange.SECONDHALFYEAR)
                    {
                        periodArchiveFolder = PeriodArchiveFolder.HALFYEAR;
                    }
                    else if (ReportChange.PeriodReportChange == PeriodReportChange.FIRSTQUARTER ||
                             ReportChange.PeriodReportChange == PeriodReportChange.SECONDQUARTER ||
                             ReportChange.PeriodReportChange == PeriodReportChange.THIRDQUARTER ||
                             ReportChange.PeriodReportChange == PeriodReportChange.FOURTHQUARTER)
                    {
                        periodArchiveFolder = PeriodArchiveFolder.QUARTER;
                    }
                    else if (ReportChange.PeriodReportChange == PeriodReportChange.MONTH)
                    {
                        periodArchiveFolder = PeriodArchiveFolder.MONTH;
                    }

                    cmbPeriod.EditValue = periodArchiveFolder.GetEnumDescription();

                    if (ReportChange.PeriodReportChange == PeriodReportChange.MONTH)
                    {
                        if (ReportChange.PeriodChangeMonth is null)
                        {
                            if (ReportChange.Month == Month.January)
                            {
                                cmbPeriodChangeMonth.EditValue = Month.December.GetEnumDescription();
                            }
                            else
                            {
                                cmbPeriodChangeMonth.EditValue = (ReportChange.Month - 1).GetEnumDescription();
                            }
                        }
                        else
                        {
                            cmbPeriodChangeMonth.EditValue = ReportChange.PeriodChangeMonth?.GetEnumDescription();
                        }
                    }
                    else
                    {
                        cmbPeriodChange.EditValue = ReportChange.PeriodReportChange.GetEnumDescription();
                    }
                }
            }

            memoComment.EditValue = ReportChange.Comment;
            if (_correctiveReport != null)
            {
                txtCorrectiveNumber.EditValue = _correctiveReport.CorrectiveNumber;
                memoCause.EditValue = _correctiveReport.Cause;
            }           
            
            gridControlCorrectiveReport.DataSource = ReportChange.CorrectiveReports;
            gridViewCorrectiveReport.OptionsView.ColumnAutoWidth = false;

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
                columnCustomerString.Width = 250;
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

            if (gridViewCorrectiveReport.Columns[nameof(CorrectiveReport.ReportCustomer)] is GridColumn columnReportCustomer)
            {
                columnReportCustomer.Width = 200;
                columnReportCustomer.OptionsColumn.FixedWidth = true;
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
                columnCause.Width = 300;
                columnCause.OptionsColumn.FixedWidth = true;
            }

            if (gridViewCorrectiveReport.Columns[nameof(CorrectiveReport.Comment)] is GridColumn columnComment)
            {
                columnComment.Width = 300;
                columnComment.OptionsColumn.FixedWidth = true;
            }

            gridControlChronicleReportChange.DataSource = ReportChange.ChronicleReportChanges;

            if (gridViewChronicleReportChange.Columns[nameof(ChronicleReportChange.Oid)] != null)
            {
                gridViewChronicleReportChange.Columns[nameof(ChronicleReportChange.Oid)].Visible = false;
                gridViewChronicleReportChange.Columns[nameof(ChronicleReportChange.Oid)].Width = 18;
                gridViewChronicleReportChange.Columns[nameof(ChronicleReportChange.Oid)].OptionsColumn.FixedWidth = true;
            }            

            if (gridViewChronicleReportChange.Columns[nameof(ChronicleReportChange.CustomerString)] != null)
            {
                gridViewChronicleReportChange.Columns[nameof(ChronicleReportChange.CustomerString)].Visible = false;
            }

            if (gridViewChronicleReportChange.Columns[nameof(ChronicleReportChange.CustomerTaxSystem)] != null)
            {
                gridViewChronicleReportChange.Columns[nameof(ChronicleReportChange.CustomerTaxSystem)].Visible = false;
            }

            if (gridViewChronicleReportChange.Columns[nameof(ChronicleReportChange.PeriodString)] != null)
            {
                gridViewChronicleReportChange.Columns[nameof(ChronicleReportChange.PeriodString)].Visible = false;
            }
            
            if (gridViewChronicleReportChange.Columns[nameof(ChronicleReportChange.Comment)] != null)
            {
                gridViewChronicleReportChange.Columns[nameof(ChronicleReportChange.Comment)].Visible = false;
            }
            gridViewChronicleReportChange.BestFitColumns();

            if (ReportChange.Oid > 0)
            {
                layoutControlItemCreateCorrectiveReport.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layoutControlItemTask.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                if (ReportChange.Task != null && !ReportChange.Task.IsDeleted)
                {
                    btnTask.Text = "Показать задачу";
                }
            }

            if (_correctiveReport is null)
            {
                cmbStatusReport.Properties.Items.Remove(StatusReport.ADJUSTMENTISREADY.GetEnumDescription());
                cmbStatusReport.Properties.Items.Remove(StatusReport.CORRECTIONSENT.GetEnumDescription());
                cmbStatusReport.Properties.Items.Remove(StatusReport.CORRECTIONSUBMITTED.GetEnumDescription());

                HtmlText = "<color=red><b>Первичный отчет</b></color>";
                layoutControlGroupChronicleReportChange.Text = "Хроника первичного отчета";
            }
            else
            {
                HtmlText = "<color=blue><b>Корректирующий отчет</b></color>";
                layoutControlGroupChronicleReportChange.Text = "Хроника корректирующего отчета";

                cmbStatusReport.Properties.Items.Clear();

                AddStatusReport(StatusReport.NOTACCEPTED.GetEnumDescription());
                AddStatusReport(StatusReport.ADJUSTMENTREQUIRED.GetEnumDescription());
                AddStatusReport(StatusReport.ADJUSTMENTISREADY.GetEnumDescription());
                AddStatusReport(StatusReport.CORRECTIONSENT.GetEnumDescription());
                AddStatusReport(StatusReport.CORRECTIONSUBMITTED.GetEnumDescription());
            }

            if (_isCorrective)
            {
                HtmlText = "<color=blue><b>Корректирующий отчет</b></color>";
                layoutControlGroupChronicleReportChange.Text = "Хроника корректирующего отчета";

                cmbStatusReport.Properties.Items.Clear();

                AddStatusReport(StatusReport.NOTACCEPTED.GetEnumDescription());
                AddStatusReport(StatusReport.ADJUSTMENTREQUIRED.GetEnumDescription());
                AddStatusReport(StatusReport.ADJUSTMENTISREADY.GetEnumDescription());
                AddStatusReport(StatusReport.CORRECTIONSENT.GetEnumDescription());
                AddStatusReport(StatusReport.CORRECTIONSUBMITTED.GetEnumDescription());
            }
        }
        
        private void AddStatusReport(string name)
        {
            if (!cmbStatusReport.Properties.Items.Contains(name))
            {
                cmbStatusReport.Properties.Items.Add(name);
            }
        }
        
        private void LoadMassReportChange()
        {
            layoutControlItemCustomer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlItemReport.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlItemPeriod.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            tabbedControlGroupReportChange.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            
            cmbDeliveryYear.SelectedIndex = -1;
            cmbStatusReport.SelectedIndex = -1;
            
            cmbYear.SelectedIndex = -1;
            cmbMonth.SelectedIndex = -1;
            txtDay.EditValue = null;
        }

        private void btnReport_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<Report>(_session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.Report, 1, null, null, false, null, string.Empty, false, true);
        }

        private void btnStaff_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (sender is ButtonEdit buttonEdit)
            {
                if (e.Button.Kind == ButtonPredefines.Delete)
                {
                    buttonEdit.EditValue = null;
                    return;
                }
                else if (e.Button.Kind == ButtonPredefines.Ellipsis)
                {
                    cls_BaseSpr.ButtonEditButtonClickBase<Staff>(_session, buttonEdit, (int)cls_App.ReferenceBooks.Staff, 1, new NullOperator(nameof(Staff.DateDismissal)), null, false, null, string.Empty, false, true);
                }
            }
        }

        private void cmbPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBoxEdit = sender as ComboBoxEdit;

            if (comboBoxEdit != null && comboBoxEdit.SelectedIndex != -1)
            {
                layoutControlItemPeriodChangeMonth.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                //cmbPeriodChange.Visible = false;
                cmbPeriodChange.SelectedIndex = -1;
                layoutControlItemPeriodChange.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                //cmbPeriodChangeMonth.Visible = false;
                cmbPeriodChangeMonth.SelectedIndex = -1;

                var period = default(PeriodArchiveFolder);

                foreach (PeriodArchiveFolder item in Enum.GetValues(typeof(PeriodArchiveFolder)))
                {
                    if (item.GetEnumDescription().Equals(cmbPeriod.EditValue))
                    {
                        period = item;
                    }
                }

                if (period == PeriodArchiveFolder.QUARTER)
                {
                    layoutControlItemPeriodChange.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    //cmbPeriodChange.Visible = true;
                    cmbPeriodChange.Properties.Items.Clear(); 
                    layoutControlItemPeriodChangeMonth.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    //cmbPeriodChangeMonth.Visible = false;
                    cmbPeriodChangeMonth.Properties.Items.Clear();
                    foreach (PeriodReportChange item in Enum.GetValues(typeof(PeriodReportChange)))
                    {
                        if (item == PeriodReportChange.FIRSTQUARTER ||
                            item == PeriodReportChange.SECONDQUARTER ||
                            item == PeriodReportChange.THIRDQUARTER ||
                            item == PeriodReportChange.FOURTHQUARTER)
                        {
                            cmbPeriodChange.Properties.Items.Add(item.GetEnumDescription());
                        }
                        cmbPeriodChange.SelectedIndex = 0;
                    }
                }
                else if (period == PeriodArchiveFolder.HALFYEAR)
                {
                    layoutControlItemPeriodChange.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    //cmbPeriodChange.Visible = true;
                    cmbPeriodChange.Properties.Items.Clear();
                    layoutControlItemPeriodChangeMonth.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    //cmbPeriodChangeMonth.Visible = false;
                    cmbPeriodChangeMonth.Properties.Items.Clear();
                    foreach (PeriodReportChange item in Enum.GetValues(typeof(PeriodReportChange)))
                    {
                        if (item == PeriodReportChange.FIRSTHALFYEAR ||
                            item == PeriodReportChange.SECONDHALFYEAR)
                        {
                            cmbPeriodChange.Properties.Items.Add(item.GetEnumDescription());
                        }
                        cmbPeriodChange.SelectedIndex = 0;
                    }
                }
                else if (period == PeriodArchiveFolder.MONTH)
                {
                    layoutControlItemPeriodChange.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    //cmbPeriodChange.Visible = false;
                    cmbPeriodChange.Properties.Items.Clear();
                    layoutControlItemPeriodChangeMonth.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    cmbPeriodChangeMonth.Visible = true;
                    cmbPeriodChangeMonth.Properties.Items.Clear();
                    foreach (Month item in Enum.GetValues(typeof(Month)))
                    {
                        cmbPeriodChangeMonth.Properties.Items.Add(item.GetEnumDescription());
                        cmbPeriodChangeMonth.SelectedIndex = 0;
                    }
                }
            }
        }

        private void gridView_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName != nameof(CustomerPerformanceIndicator.Value))
            {
                return;
            }

            var gridview = sender as GridView;

            if (gridview != null)
            {
                var customerPerformanceIndicator = gridview.GetRow(e.RowHandle) as CustomerPerformanceIndicator;

                if (customerPerformanceIndicator != null
                    && customerPerformanceIndicator.PerformanceIndicator != null
                    && customerPerformanceIndicator.PerformanceIndicator.TypePerformanceIndicator == TypePerformanceIndicator.Percent)
                {
                    e.RepositoryItem = repositoryItemCheckEdit;
                }
            }
        }

        private void btnTask_Click(object sender, EventArgs e)
        {
            var task = ReportChange?.Task;
            
            var form = default(TaskEdit);
            if (task is null || (task != null && task.IsDeleted))
            {
                form = new TaskEdit(ReportChange, "Сформированная задача из модуля [Отчеты]");                
            }
            else
            {
                form = new TaskEdit(task);
            }
            form.ShowDialog();

            if (form.Task?.Oid > 0)
            {
                ReportChange.Task = form.Task;
                ReportChange.Save();

                if (ReportChange.Task != null && !ReportChange.Task.IsDeleted)
                {
                    btnTask.Text = "Показать задачу";
                }
            }
        }

        private async void btnCreateCorrectiveReport_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show(
                "Перед созданием корректирующего отчета необходимо сохранить текущий отчет. Продолжить?",
                "Операция с отчетом",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question) == DialogResult.OK)
            {
                cmbStatusReport.EditValue = StatusReport.ADJUSTMENTREQUIRED.GetEnumDescription();
                if (await SaveReportChange())
                {                    
                    var form = new ReportChangeEdit(ReportChange, true);
                    form.SetFormPosition(new Point(Location.X + 450, Location.Y + 50));
                    form.ShowDialog();

                    ShowCorrectiveReport();
                }
            }
        }

        private void barBtnDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

        private void barBtnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridViewCorrectiveReport.GetRow(gridViewCorrectiveReport.FocusedRowHandle) is CorrectiveReport obj && obj.ReportChange != null)
            {
                var form = new ReportChangeEdit(obj);
                form.ShowDialog();
            }
        }

        private void gridViewCorrectiveReport_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
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
                    
                    popupMenuCorrectiveReport.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }

        private void checkIsOurFault_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckEdit checkEdit)
            {
                if (checkEdit.Checked)
                {
                    checkIsCustomerFault.Checked = false;
                }
            }
        }

        private void checkIsCustomerFault_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckEdit checkEdit)
            {
                if (checkEdit.Checked)
                {
                    checkIsOurFault.Checked = false;
                }
            }
        }

        private void cmbStatusReport_SelectedValueChanged(object sender, EventArgs e)
        {
            if (sender is ComboBoxEdit cmb)
            {
                var statusReport = default(string);
                if (cmb.SelectedIndex != -1)
                {
                    foreach (StatusReport item in Enum.GetValues(typeof(StatusReport)))
                    {
                        if (item.GetEnumDescription().Equals(cmbStatusReport.EditValue))
                        {
                            statusReport = item.GetEnumDescription();
                            break;
                        }
                    }
                }

                if (string.IsNullOrWhiteSpace(statusReport))
                {
                    cmb.BackColor = default;
                    return;
                }
                
                if (statusReport.Equals(StatusReport.NEW.GetEnumDescription()))
                {
                    cmb.BackColor = StatusReportColor.ColorStatusReportNew;
                    //cmb.BackColor2 = Color.White;
                }
                else if (statusReport.Equals(StatusReport.SENT.GetEnumDescription()))
                {
                    cmb.BackColor = StatusReportColor.ColorStatusReportSent;
                    //cmb.BackColor2 = Color.White;
                }
                else if (statusReport.Equals(StatusReport.SURRENDERED.GetEnumDescription()))
                {
                    cmb.BackColor = StatusReportColor.ColorStatusReportSurrendered;
                    //cmb.BackColor2 = Color.White;
                }
                else if (statusReport.Equals(StatusReport.NOTSURRENDERED.GetEnumDescription()))
                {
                    cmb.BackColor = StatusReportColor.ColorStatusReportNotSurrendered;
                    //cmb.BackColor2 = Color.White;
                }
                else if (statusReport.Equals(StatusReport.PREPARED.GetEnumDescription()))
                {
                    cmb.BackColor = StatusReportColor.ColorStatusReportPrepared;
                    //cmb.BackColor2 = Color.White;
                }
                else if (statusReport.Equals(StatusReport.NOTACCEPTED.GetEnumDescription()))
                {
                    cmb.BackColor = StatusReportColor.ColorStatusReportNotAccepted;
                    //cmb.BackColor2 = Color.White;
                }
                else if (statusReport.Equals(StatusReport.DOESNOTGIVEUP.GetEnumDescription()))
                {
                    cmb.BackColor = StatusReportColor.ColorStatusReportDoesnotgiveup;
                    //cmb.BackColor2 = Color.White;
                }

                else if (statusReport.Equals(StatusReport.ADJUSTMENTREQUIRED.GetEnumDescription()))
                {
                    cmb.BackColor = StatusReportColor.ColorStatusReportAdjustmentRequired;
                    //cmb.BackColor2 = Color.White;
                }
                else if (statusReport.Equals(StatusReport.ADJUSTMENTISREADY.GetEnumDescription()))
                {
                    cmb.BackColor = StatusReportColor.ColorStatusReportAdjustmentIsReady;
                    //cmb.BackColor2 = Color.White;
                }
                else if (statusReport.Equals(StatusReport.CORRECTIONSENT.GetEnumDescription()))
                {
                    cmb.BackColor = StatusReportColor.ColorStatusReportCorrectionSent;
                    //cmb.BackColor2 = Color.White;
                }
                else if (statusReport.Equals(StatusReport.CORRECTIONSUBMITTED.GetEnumDescription()))
                {
                    cmb.BackColor = StatusReportColor.ColorStatusReportCorrectionSubmitted;
                    //cmb.BackColor2 = Color.White;
                }
                else if (statusReport.Equals(StatusReport.RENTEDBYTHECLIENT.GetEnumDescription()))
                {
                    cmb.BackColor = StatusReportColor.ColorStatusReportRentedByTheClient;
                    //e.Appearance.BackColor2 = Color.White;
                }
            }
        }
    }
}