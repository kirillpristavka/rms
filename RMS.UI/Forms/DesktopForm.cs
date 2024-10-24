using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Utils;
using RMS.Core.Enumerator;
using RMS.Core.Interface;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Mail;
using RMS.Core.Model.Notifications;
using RMS.Core.Model.Reports;
using RMS.Core.Model.Salary;
using RMS.Core.Model.Taxes;
using RMS.UI.Control.Desktop;
using RMS.UI.Forms.Mail;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace RMS.UI.Forms
{
    public partial class DesktopForm : XtraForm
    {
        private Session _session;
        
        public DesktopForm(Session session)
        {
            InitializeComponent();
            this._session = session;
        }
        
        private void PanelControlClear(PanelControl panelControl)
        {
            Invoke((Action) delegate
            {
                panelControl.Controls.Clear();
            });
        }

        private void PanelControlAdd(PanelControl panelControl, UserControl userControl)
        {
            Invoke((Action)delegate
            {
                panelControl.Controls.Add(userControl);
            });
        }

        private async System.Threading.Tasks.Task GetTaskControl(Staff staff, PanelControl control, string caption, bool isTaskIn = true)
        {
            await System.Threading.Tasks.Task.Run(() =>
            {
                PanelControlClear(control);

                var taskCriteria = default(GroupOperator);

                if (staff != null)
                {
                    taskCriteria = new GroupOperator(GroupOperatorType.Or);

                    if (isTaskIn)
                    {
                        var taskCriteriaStaff = new BinaryOperator(nameof(Task.Staff), staff);
                        taskCriteria.Operands.Add(taskCriteriaStaff);
                        var taskCriteriaCoExecutor = new BinaryOperator(nameof(Task.CoExecutor), staff);
                        taskCriteria.Operands.Add(taskCriteriaCoExecutor);
                    }
                    else
                    {
                        var taskCriteriaGivenStaff = new BinaryOperator(nameof(Task.GivenStaff), staff);
                        taskCriteria.Operands.Add(taskCriteriaGivenStaff);
                    }
                }

                using (var uof = new UnitOfWork())
                {
                    var taskControlIn = new TaskControl(uof, taskCriteria, caption) { Dock = DockStyle.Fill };
                    PanelControlAdd(control, taskControlIn);
                }
            });
        }

        private async System.Threading.Tasks.Task GetBlockControl<T1, T2>(Staff staff, PanelControl control, string caption) where T2 : Form
        {
            await System.Threading.Tasks.Task.Run(() =>
            {
                var criteria = default(GroupOperator);

                using (var uof = new UnitOfWork())
                {
                    var blockControl = new BlockControl<T1, T2>(uof, caption, criteria) { Dock = DockStyle.Top };
                    PanelControlAdd(control, blockControl);
                }
            });
        }

        private async void DesktopForm_Load(object sender, EventArgs e)
        {
            var user = _session.GetObjectByKey<User>(DatabaseConnection.User.Oid);
            var staff = user.Staff;

            //await GetTaskControl(staff, panelControlTaskIn, "Задачи (входящие)").ConfigureAwait(false);
            //await GetTaskControl(staff, panelControlTaskOut, "Задачи (исходящие)", false).ConfigureAwait(false);

            //PanelControlClear(panelControlBlock);
            //await GetBlockControl<IndividualEntrepreneursTax, ReportChangeForm>(staff, panelControlBlock, "Патенты").ConfigureAwait(false);
            //await GetBlockControl<Letter, LetterForm>(staff, panelControlBlock, "Письма").ConfigureAwait(false);
            //await GetBlockControl<Customer, CustomersForm>(staff, panelControlBlock, "Клиенты").ConfigureAwait(false);
            //await GetBlockControl<Deal, DealForm>(staff, panelControlBlock, "Сделки").ConfigureAwait(false);

            //PanelControlClear(panelControlChart);
            //await GetBlockControl<IndividualEntrepreneursTax, ReportChangeForm>(staff, panelControlChart, "ИП").ConfigureAwait(false);
            //await GetBlockControl<CustomerSalaryAdvance, ReportChangeForm>(staff, panelControlChart, "ЗП\nАванс").ConfigureAwait(false);
            //await GetBlockControl<ReportChange, ReportChangeForm>(staff, panelControlChart, "Новые\nотчеты").ConfigureAwait(false);
            //await GetBlockControl<ReportChange, ReportChangeForm>(staff, panelControlChart, "Отчеты\nкоррекция").ConfigureAwait(false);

            var taskCriteriaIn = default(GroupOperator);
            var taskCriteriaOut = default(GroupOperator);
            var customerCriteria = default(GroupOperator);
            var letterCriteriaAnd = default(GroupOperator);
            var dealCriteria = default(GroupOperator);
            var customerSalaryAdvanceCriteria = default(BinaryOperator);
            var reportNewCriteria = default(GroupOperator);
            var reportNeedSadjustmentCriteria = default(GroupOperator);
            var individualEntrepreneursTaxPatentCriteria = default(GroupOperator);
            var individualEntrepreneursTaxCriteria = default(GroupOperator);

            if (staff != null)
            {
                var dealStatusDone = await _session.FindObjectAsync<DealStatus>(new BinaryOperator(nameof(DealStatus.Name), "Выполнена"));
                var dealStatusCanceledTask = await _session.FindObjectAsync<DealStatus>(new BinaryOperator(nameof(DealStatus.Name), "Снята задача"));

                taskCriteriaIn = new GroupOperator(GroupOperatorType.Or);
                var taskCriteriaStaff = new BinaryOperator(nameof(Task.Staff), staff);
                taskCriteriaIn.Operands.Add(taskCriteriaStaff);
                var taskCriteriaCoExecutor = new BinaryOperator(nameof(Task.CoExecutor), staff);
                taskCriteriaIn.Operands.Add(taskCriteriaCoExecutor);

                taskCriteriaOut = new GroupOperator(GroupOperatorType.Or);
                var taskCriteriaGivenStaff = new BinaryOperator(nameof(Task.GivenStaff), staff);
                taskCriteriaOut.Operands.Add(taskCriteriaGivenStaff);

                customerSalaryAdvanceCriteria = new BinaryOperator(nameof(CustomerSalaryAdvance.Staff), staff);

                dealCriteria = new GroupOperator(GroupOperatorType.And);
                var dealCriteriaStaff = new BinaryOperator(nameof(Deal.Staff), staff);
                dealCriteria.Operands.Add(dealCriteriaStaff);                
                if (dealStatusDone != null)
                {
                    var dealCriteriaDealStatus = new NotOperator(new BinaryOperator(nameof(Deal.DealStatus), dealStatusDone));
                    dealCriteria.Operands.Add(dealCriteriaDealStatus);
                }
                //if (dealStatusCanceledTask != null)
                //{
                //    var dealCriteriaDealStatus = new NotOperator(new BinaryOperator(nameof(Deal.DealStatus), dealStatusCanceledTask));
                //    dealCriteria.Operands.Add(dealCriteriaDealStatus);
                //}

                customerCriteria = new GroupOperator(GroupOperatorType.And);

                var customerStaffCriteria = new GroupOperator(GroupOperatorType.Or);
                var criteriaAccountantResponsible = new BinaryOperator(nameof(Customer.AccountantResponsible), user.Staff);
                customerStaffCriteria.Operands.Add(criteriaAccountantResponsible);
                var criteriaBankResponsible = new BinaryOperator(nameof(Customer.BankResponsible), user.Staff);
                customerStaffCriteria.Operands.Add(criteriaBankResponsible);
                var criteriaPrimaryResponsible = new BinaryOperator(nameof(Customer.PrimaryResponsible), user.Staff);
                customerStaffCriteria.Operands.Add(criteriaPrimaryResponsible);

                var customerStatusCriteria = new GroupOperator(GroupOperatorType.Or);
                var criteriaCustomerStatus1 = new BinaryOperator($"{nameof(Customer.CustomerStatus)}.{nameof(CustomerStatus.StatusString)}", "Обслуживаем");
                customerStatusCriteria.Operands.Add(criteriaCustomerStatus1);
                var criteriaCustomerStatus2 = new BinaryOperator($"{nameof(Customer.CustomerStatus)}.{nameof(CustomerStatus.StatusString)}", "Приостановлен");
                customerStatusCriteria.Operands.Add(criteriaCustomerStatus2);
                
                customerCriteria.Operands.Add(customerStaffCriteria);
                customerCriteria.Operands.Add(customerStatusCriteria);

                letterCriteriaAnd = new GroupOperator(GroupOperatorType.And);
                var letterCriteriaStaffOr = new GroupOperator(GroupOperatorType.Or);
                var criteriaAccountantResponsibleLetter = new BinaryOperator($"{nameof(Letter.Customer)}.{nameof(Customer.AccountantResponsible)}", user.Staff);
                letterCriteriaStaffOr.Operands.Add(criteriaAccountantResponsibleLetter);
                var criteriaBankResponsibleLetter = new BinaryOperator($"{nameof(Letter.Customer)}.{nameof(Customer.BankResponsible)}", user.Staff);
                letterCriteriaStaffOr.Operands.Add(criteriaBankResponsibleLetter);
                var criteriaPrimaryResponsibleLetter = new BinaryOperator($"{nameof(Letter.Customer)}.{nameof(Customer.PrimaryResponsible)}", user.Staff);
                letterCriteriaStaffOr.Operands.Add(criteriaPrimaryResponsibleLetter);
                letterCriteriaAnd.Operands.Add(letterCriteriaStaffOr);                
                var dealGroupCriteriaLetter = new GroupOperator(GroupOperatorType.And);
                if (dealStatusDone != null)
                {                    
                    var letterCriteriaDealStatus = new NotOperator(new BinaryOperator($"{nameof(Letter.Deal)}.{nameof(Deal.DealStatus)}", dealStatusDone));
                    dealGroupCriteriaLetter.Operands.Add(letterCriteriaDealStatus);
                }                
                if (dealStatusCanceledTask != null)
                {
                    var letterCriteriaDealStatus = new NotOperator(new BinaryOperator($"{nameof(Letter.Deal)}.{nameof(Deal.DealStatus)}", dealStatusCanceledTask));
                    dealGroupCriteriaLetter.Operands.Add(letterCriteriaDealStatus);
                }

                if (dealGroupCriteriaLetter.Operands.Count > 0)
                {
                    letterCriteriaAnd.Operands.Add(dealGroupCriteriaLetter);
                }

                reportNewCriteria = new GroupOperator(GroupOperatorType.And);
                reportNeedSadjustmentCriteria = new GroupOperator(GroupOperatorType.And);
                individualEntrepreneursTaxPatentCriteria = new GroupOperator(GroupOperatorType.And);
                individualEntrepreneursTaxCriteria = new GroupOperator(GroupOperatorType.And);
                
                var reportCriteriaUser = new BinaryOperator(nameof(ReportChange.AccountantResponsible), user.Staff);                
                reportNewCriteria.Operands.Add(reportCriteriaUser);
                reportNeedSadjustmentCriteria.Operands.Add(reportCriteriaUser);
                var reportCriteriaStatusNew = new BinaryOperator(nameof(ReportChange.StatusReport), StatusReport.NEW);
                reportNewCriteria.Operands.Add(reportCriteriaStatusNew);

                var groupOperatorStatusOr = new GroupOperator(GroupOperatorType.Or);
                var reportCriteriaStatusNeedsadjustmentCustomerfault = new BinaryOperator(nameof(ReportChange.StatusReport), StatusReport.NEEDSADJUSTMENTCUSTOMERFAULT);
                groupOperatorStatusOr.Operands.Add(reportCriteriaStatusNeedsadjustmentCustomerfault);
                var reportCriteriaStatusNeedsadjustmentfault = new BinaryOperator(nameof(ReportChange.StatusReport), StatusReport.NEEDSADJUSTMENTOURFAULT);
                groupOperatorStatusOr.Operands.Add(reportCriteriaStatusNeedsadjustmentfault);
                reportNeedSadjustmentCriteria.Operands.Add(groupOperatorStatusOr);
                
                var userIndividualEntrepreneursTaxPatent = new BinaryOperator(nameof(IndividualEntrepreneursTax.Staff), user.Staff);
                individualEntrepreneursTaxPatentCriteria.Operands.Add(userIndividualEntrepreneursTaxPatent);
                individualEntrepreneursTaxCriteria.Operands.Add(userIndividualEntrepreneursTaxPatent);

                var patentCriteria = new NotOperator(new NullOperator(nameof(IndividualEntrepreneursTax.PatentObj)));
                individualEntrepreneursTaxPatentCriteria.Operands.Add(patentCriteria);

                var patentIsNullCriteria = new NullOperator(nameof(IndividualEntrepreneursTax.PatentObj));
                individualEntrepreneursTaxCriteria.Operands.Add(patentIsNullCriteria);
            }
            
            //panelControlBlock1.Controls.Clear();
            panelControlVacation.Controls.Clear();
            panelControlBirthDay.Controls.Clear();
            panelControlElectronicReporting.Controls.Clear();
            panelControlPatent.Controls.Clear();
            panelControlTaskNow.Controls.Clear();

            //var blockControlLetter = new BlockControl<Letter, LetterForm>(session, "Письма", new LetterForm(session), letterCriteriaAnd) { Dock = DockStyle.Top };
            //panelControlBlock1.Controls.Add(blockControlLetter);                        

            //var blockControlCustomerSalaryAdvance = new BlockControl<CustomerSalaryAdvance, ReportChangeForm>(session, "ЗП\nАванс", new ReportChangeForm(session), customerSalaryAdvanceCriteria) { Dock = DockStyle.Top };
            //panelControlBlock1.Controls.Add(blockControlCustomerSalaryAdvance);

            //var blockControlReportNew = new BlockControl<ReportChange, ReportChangeForm>(session, "Новые\nотчеты", new ReportChangeForm(session), reportNewCriteria) { Dock = DockStyle.Top };
            //panelControlBlock2.Controls.Add(blockControlReportNew);

            //var blockControlReportNeed = new BlockControl<ReportChange, ReportChangeForm>(session, "Отчеты\nкоррекция", new ReportChangeForm(session), reportNeedSadjustmentCriteria) { Dock = DockStyle.Top };
            //panelControlBlock2.Controls.Add(blockControlReportNeed);

            var vacationControl = new VacationControl(_session) { Dock = DockStyle.Fill };
            vacationControl.CreateControl();
            if (vacationControl.Count > 0)
            {
                panelControlVacation.Controls.Add(vacationControl);
                layoutControlItemVacation.Visibility = LayoutVisibility.Always;
                splitterItemVacation.Visibility = LayoutVisibility.Always;
            }
            else
            {
                layoutControlItemVacation.Visibility = LayoutVisibility.Never;
                splitterItemVacation.Visibility = LayoutVisibility.Never;
            }

            var birthDayControl = new BirthDayControl(_session) { Dock = DockStyle.Fill };
            birthDayControl.CreateControl();            
            if (birthDayControl.Count > 0)
            {
                panelControlBirthDay.Controls.Add(birthDayControl);
                layoutControlItemBirthDay.Visibility = LayoutVisibility.Always;
                splitterItemBirthDay.Visibility = LayoutVisibility.Always;
            }
            else
            {
                layoutControlItemBirthDay.Visibility = LayoutVisibility.Never;
                splitterItemBirthDay.Visibility = LayoutVisibility.Never;
            }
            
            var patentControl = new PatentControl(_session) { Dock = DockStyle.Fill };
            patentControl.CreateControl();
            if (user.flagAdministrator)
            {
                if (patentControl.Count > 0)
                {
                    panelControlPatent.Controls.Add(patentControl);
                    layoutControlItemPatent.Visibility = LayoutVisibility.Always;
                    splitterItemPatent.Visibility = LayoutVisibility.Always;
                }
                else
                {
                    layoutControlItemPatent.Visibility = LayoutVisibility.Never;
                    splitterItemPatent.Visibility = LayoutVisibility.Never;
                }
            }
            else
            {
                layoutControlItemPatent.Visibility = LayoutVisibility.Never;
                splitterItemPatent.Visibility = LayoutVisibility.Never;
            }            

            var electronicReportingControl = new ElectronicReportingControl(_session) { Dock = DockStyle.Fill };
            electronicReportingControl.CreateControl();
            if (electronicReportingControl.Count > 0)
            {
                panelControlElectronicReporting.Controls.Add(electronicReportingControl);
                layoutControlItemElectronicReporting.Visibility = LayoutVisibility.Always;
            }
            else
            {
                layoutControlItemElectronicReporting.Visibility = LayoutVisibility.Never;
            }


            var isVisibleTaskNowCOntrol = false;
            var taskNowControl = new TaskNowControl(_session) { Dock = DockStyle.Fill };
            taskNowControl.CreateControl();
            if (taskNowControl.Count > 0)
            {
                taskNowControl.TaskEditControlEvent += TaskNowControl_TaskEditControlEvent;
                isVisibleTaskNowCOntrol = true;
            }
            panelControlTaskNow.Controls.Add(taskNowControl);
            layoutControlItemTaskNow.Visibility = LayoutVisibility.Always;

            var controlSystemControl = new ControlSystemControl(_session) { Dock = DockStyle.Fill };
            controlSystemControl.CreateControl();
            if (controlSystemControl.Count > 0)
            {
                panelControlControlSystem.Controls.Add(controlSystemControl);
                layoutControlItemControlSystem.Visibility = LayoutVisibility.Always;

                if (isVisibleTaskNowCOntrol)
                {
                    splitterItemControlSystem.Visibility = LayoutVisibility.Always;
                }
                else
                {
                    splitterItemControlSystem.Visibility = LayoutVisibility.Never;
                }
            }
            else
            {
                splitterItemControlSystem.Visibility = LayoutVisibility.Never;
                layoutControlItemControlSystem.Visibility = LayoutVisibility.Never;
            }

            try
            {
                splitterItem.Location = new System.Drawing.Point(Width);
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }

            splitterItem.Location = new System.Drawing.Point(650, 0);
        }

        private void TaskNowControl_TaskEditControlEvent(object sender, Task task)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private void UpdatePanelTaskControl(PanelControl panelControl)
        {
            foreach (var obj in panelControl.Controls)
            {
                try
                {
                    if (obj is TaskControl taskControl)
                    {
                        Invoke((Action)delegate
                        {
                            taskControl.ReloadControl();
                            taskControl.Refresh();
                        });
                        break;
                    }
                }
                catch (Exception ex)
                {
                    RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                }
            }
        }

        private void tileBarUpdate_ItemClick(object sender, TileItemEventArgs e)
        {
            DesktopForm_Load(null, null);
        }

        private List<Notification> notificationsExpired = new List<Notification>();
        private List<Notification> notificationsBurning = new List<Notification>();
        private List<Notification> notificationsNotification = new List<Notification>();

        private async System.Threading.Tasks.Task<List<Notification>> GetCustomerBirthDay<T>(Session session,
                                                              TypeNotification typeNotification,
                                                              DateTime? dateTimeNow = null)
            where T : INotification
        {
            if (dateTimeNow is null)
            {
                dateTimeNow = DateTime.Now.Date;
            }

            var critreia = new GroupOperator(GroupOperatorType.And);

            var user = await session.GetObjectByKeyAsync<User>(DatabaseConnection.User.Oid);
            var staff = user.Staff;

            if (user != null && user.flagAdministrator is false)
            {
                var groupOperator = new GroupOperator(GroupOperatorType.And);
                if (staff != null)
                {
                    var groupOperatorStaff = new GroupOperator(GroupOperatorType.Or);
                    var accountantResponsibleCriteria = new BinaryOperator(nameof(Customer.AccountantResponsible), staff);
                    groupOperatorStaff.Operands.Add(accountantResponsibleCriteria);
                    var primaryResponsibleCriteria = new BinaryOperator(nameof(Customer.PrimaryResponsible), staff);
                    groupOperatorStaff.Operands.Add(primaryResponsibleCriteria);
                    var bankResponsibleCriteria = new BinaryOperator(nameof(Customer.BankResponsible), staff);
                    groupOperatorStaff.Operands.Add(bankResponsibleCriteria);
                    groupOperator.Operands.Add(groupOperatorStaff);

                    /* 
                     * Чтобы и администраторы видели только обслуживаемых клиентов
                     * необходимо вынести ниже.
                    */
                    
                    //var statusCustomer = await session.FindObjectAsync<Status>(new BinaryOperator(nameof(Status.Name), "Обслуживаем"));
                    //if (statusCustomer != null)
                    //{
                    //    var criteriaStatus = new BinaryOperator($"{nameof(Customer.CustomerStatus)}.{nameof(CustomerStatus.Status)}", statusCustomer);
                    //    groupOperator.Operands.Add(criteriaStatus);
                    //}

                    critreia.Operands.Add(groupOperator);
                }
            }

            var statusCustomer = await session.FindObjectAsync<Status>(new BinaryOperator(nameof(Status.Name), "Обслуживаем"));
            if (statusCustomer != null)
            {
                var criteriaStatus = new BinaryOperator($"{nameof(Customer.CustomerStatus)}.{nameof(CustomerStatus.Status)}", statusCustomer);
                critreia.Operands.Add(criteriaStatus);
            }

            var criteriaDate = CriteriaOperator.Parse($"GetMonth({nameof(Customer.DateManagementBirth)}) = {dateTimeNow.Value.Month}");
            critreia.Operands.Add(criteriaDate);
            
            return GetNotification<T>(session, critreia, typeNotification);
        }

        private List<Notification> GetDeadlineNotification<T>(Session session,
                                                              TypeNotification typeNotification,
                                                              DateTime dateSince,
                                                              string nameProperty,
                                                              DateTime? dateTo = null,
                                                              string[] namesStatus = null,
                                                              CriteriaOperator criteriaOperator = null) 
            where T : INotification
        {
            var groupOperator = new GroupOperator(GroupOperatorType.And);

            if (criteriaOperator is null)
            {
                var criteriaDeadlineNotNull = new NotOperator(new NullOperator(nameProperty));
                groupOperator.Operands.Add(criteriaDeadlineNotNull);

                if (dateTo is DateTime date)
                {
                    var criteriaDeadlineDateSince = new BinaryOperator(nameProperty, dateSince, BinaryOperatorType.Greater);
                    groupOperator.Operands.Add(criteriaDeadlineDateSince);
                    var criteriaDeadlineDateTo = new BinaryOperator(nameProperty, date, BinaryOperatorType.LessOrEqual);
                    groupOperator.Operands.Add(criteriaDeadlineDateTo);
                }
                else
                {
                    var criteriaTaskDeadlineDateSince = new BinaryOperator(nameProperty, dateSince, BinaryOperatorType.LessOrEqual);
                    groupOperator.Operands.Add(criteriaTaskDeadlineDateSince);
                }
            }
            else
            {
                groupOperator.Operands.Add(criteriaOperator);
            }            

            if (namesStatus is null)
            {
                namesStatus = new string[] { "Выполнено", "Выполнена", "Готово", "Сдан", "Согласован" };
            }            
            foreach (var name in namesStatus)
            {
                var criteriaStatus = new NotOperator(new BinaryOperator(nameof(INotification.StatusString), name));
                groupOperator.Operands.Add(criteriaStatus);
            }
            
            return GetNotification<T>(session, groupOperator, typeNotification);
        }        

        private List<Notification> GetNotification<T>(Session session,
                                                      CriteriaOperator criteria,
                                                      TypeNotification typeNotification) where T : INotification
        {
            var result = new List<Notification>();

            using (var collection = new XPCollection<T>(session, criteria))
            {
                foreach (var item in collection)
                {
                    var notification = item.GetNotification(typeNotification);
                    if (notification != null)
                    {
                        result.Add(notification);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Получение просроченных объектов из разных модулей.
        /// </summary>
        private async System.Threading.Tasks.Task GetNotifications(List<Notification> list,
                                                                   DateTime dateSince,
                                                                   DateTime? dateTo,
                                                                   TypeNotification typeNotification,
                                                                   TileBarItem tileBarItem)
        {
            await System.Threading.Tasks.Task.Run(() => 
            {
                using (var uof = new UnitOfWork())
                {
                    list.Clear();

                    list.AddRange(GetDeadlineNotification<Task>(uof, typeNotification, dateSince, nameof(Task.Deadline), dateTo));
                    list.AddRange(GetDeadlineNotification<ReportChange>(uof, typeNotification, dateSince, nameof(ReportChange.LastDayDelivery), dateTo));
                    list.AddRange(GetDeadlineNotification<IndividualEntrepreneursTax>(uof, typeNotification, dateSince, nameof(IndividualEntrepreneursTax.DateDelivery), dateTo));
                    list.AddRange(GetDeadlineNotification<IndividualEntrepreneursTax>(uof, typeNotification, dateSince, $"{nameof(IndividualEntrepreneursTax.PatentObj)}.{nameof(PatentObject.DateTo)}", dateTo));

                    list.AddRange(GetDeadlineNotification<PreTax>(uof, typeNotification, dateSince, nameof(PreTax.DateDelivery), dateTo));
                    list.AddRange(GetDeadlineNotification<Deal>(uof, typeNotification, dateSince, nameof(Deal.DateUpdate), dateTo));

                    if (list.Count > 0)
                    {
                        Invoke((Action)delegate
                        {
                            tileBarItem.Text = list.Count.ToString();
                        });
                    }
                }
            });
        }

        /// <summary>
        /// Получение просроченных объектов из разных модулей.
        /// </summary>
        private async System.Threading.Tasks.Task GetNotifications(List<Notification> list,
                                                                   DateTime? dateTimeNow,
                                                                   TypeNotification typeNotification,
                                                                   TileBarItem tileBarItem)
        {
            await System.Threading.Tasks.Task.Run(async () =>
            {
                using (var uof = new UnitOfWork())
                {
                    list.Clear();

                    list.AddRange(await GetCustomerBirthDay<Customer>(uof, typeNotification, dateTimeNow));

                    if (list.Count > 0)
                    {
                        Invoke((Action)delegate
                        {
                            tileBarItem.Text = list.Count.ToString();
                        });
                    }
                }
            });
        }

        private void tileBarExpired_ItemClick(object sender, TileItemEventArgs e)
        {
            if (notificationsExpired is null || notificationsExpired.Count == 0)
            {
                return;
            }
            
            var form = new NotificationsObjectForm(notificationsExpired, "Просроченные события");
            form.Show();
        }

        private void tileBarBurning_ItemClick(object sender, TileItemEventArgs e)
        {
            if (notificationsBurning is null || notificationsBurning.Count == 0)
            {
                return;
            }
            
            var form = new NotificationsObjectForm(notificationsBurning, "Горящие события");
            form.Show();
        }
        private void tileBarItemNotification_ItemClick(object sender, TileItemEventArgs e)
        {
            if (notificationsNotification is null || notificationsNotification.Count == 0)
            {
                return;
            }

            var form = new NotificationsObjectForm(notificationsNotification, "События");
            form.Show();
        }

        private void DesktopForm_KeyDown(object sender, KeyEventArgs e)
        {          
            if (e.KeyCode == Keys.F5)
            {
                DesktopForm_Load(null, null);
            }
        }        
    }
}