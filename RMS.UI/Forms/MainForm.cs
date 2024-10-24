using AutoUpdaterDotNET;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using PulsLibrary.Extensions.WinForm;
using RMS.Core.CodeFirst;
using RMS.Core.Controller;
using RMS.Core.Controllers;
using RMS.Core.Controllers.Customers;
using RMS.Core.Controllers.PackagesDocument;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Interface;
using RMS.Core.Model;
using RMS.Core.Model.Chronicle;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Mail;
using RMS.Core.Model.Reports;
using RMS.Parser.Core.Models.WebsborGksRu.Controller;
using RMS.ParserReportingSystem.Controller;
using RMS.UI.Forms.CourierService.v1;
using RMS.UI.Forms.CourierService.v2;
using RMS.UI.Forms.Directories;
using RMS.UI.Forms.Mail;
using RMS.UI.Forms.Vacations;
using RMS.UI.xUI.PackagesDocument.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Forms
{
    public delegate void RefreshChildForm();

    public partial class MainForm : RibbonForm
    {
        private static Session Session => DatabaseConnection.GetWorkSession();
                    
        public static RefreshChildForm OnRefreshChildForm;

        public MainForm()
        {
            InitializeComponent();
            SkinHelper.InitSkinGallery(barBtnSkins, true);

            Text = "ООО БК Альграс - Система коммуникаций и данных (СКиД)";
            BVVGlobal.oApp.ProgramVersion = ProductVersion;

            #if DEBUG
                barBtnCustomerFormShow.Visibility = BarItemVisibility.Always;
                barBtnTaskFormShow.Visibility = BarItemVisibility.Always;
                barBtnDealFormShow.Visibility = BarItemVisibility.Always;
                barBtnReportV2.Visibility = BarItemVisibility.Always;
                barBtnReportSBIS.Visibility = BarItemVisibility.Always;
            #endif
        }

        private async static void UpdateCustomerInformationAuto()
        {
            await System.Threading.Tasks.Task.Run(async () => 
            {
                try
                {
                    using (var session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer())
                    {
                        var dateSince = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 1);
                        var dateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
                        var criteriaOperatorDate = GroupOperator.Combine(GroupOperatorType.And,
                            new BinaryOperator(nameof(ChronicleEvents.Date), dateSince, BinaryOperatorType.Greater),
                            new BinaryOperator(nameof(ChronicleEvents.Date), dateTo, BinaryOperatorType.Less),
                            new BinaryOperator(nameof(ChronicleEvents.Act), Act.UPDATING_CUSTOMER_INFORMATION_AUTO));

                        var chronicleEvents = session.FindObject<ChronicleEvents>(criteriaOperatorDate);

                        if (chronicleEvents is null)
                        {
                            var xpcollectionCustomer = new XPCollection<Customer>(session);

                            foreach (var item in xpcollectionCustomer)
                            {
                                var getInfoFromDaData = new GetInfoOrganizationFromDaData(
                                "a8cfa20b99fb660c53b8e0fa2d3de0f2204fb580", "080aefa3543cb56dfe122f26a16a04703cacb128", item.INN);
                                await getInfoFromDaData.GetDataAsync();

                                if (getInfoFromDaData != null)
                                {
                                    item.DateActuality = getInfoFromDaData.DateActuality;
                                    item.DateLiquidation = getInfoFromDaData.DateLiquidation;
                                    item.OrganizationStatus = (StatusOrganization)getInfoFromDaData.OrganizationStatus;
                                    item.Save();
                                }
                            }

                            chronicleEvents = new ChronicleEvents(session)
                            {
                                Act = Act.UPDATING_CUSTOMER_INFORMATION_AUTO,
                                Date = DateTime.Now,
                                Name = Act.UPDATING_CUSTOMER_INFORMATION_AUTO.GetEnumDescription(),
                                User = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid)
                            };
                            chronicleEvents.Save();
                        }
                    }
                }
                catch (Exception) { }
            });
        }

        /// <summary>
        /// Автоматическое обновление отчетов клиентов раз в 3 дня.
        /// </summary>
        private static async void UpdateReportInformationAuto()
        {
            await System.Threading.Tasks.Task.Run(async () =>
            {
                try
                {
                    var dateSince = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 1);
                    var dateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 3, 23, 59, 59);
                    var criteriaOperatorDate = GroupOperator.Combine(GroupOperatorType.And,
                        new BinaryOperator(nameof(ChronicleEvents.Date), dateSince, BinaryOperatorType.Greater),
                        new BinaryOperator(nameof(ChronicleEvents.Date), dateTo, BinaryOperatorType.Less),
                        new BinaryOperator(nameof(ChronicleEvents.Act), Act.UPDATING_INFORMATION_REPORTING_SYSTEM_AUTO));

                    var chronicleEvents = Session.FindObject<ChronicleEvents>(criteriaOperatorDate);

                    if (chronicleEvents is null)
                    {
                        var xpcollectionCustomer = new XPCollection<Customer>(Session);
                        var inns = xpcollectionCustomer.Select(s => s.INN).ToArray();

                        if (inns.Length > 0)
                        {
                            using (var reportingSystem = new ReportingSystem())
                            {
                                reportingSystem.ErrorEvent += ReportingSystem_ErrorEvent;
                                if (reportingSystem.StartDriver() is false)
                                {
                                    return;
                                };

                                foreach (var customer in xpcollectionCustomer)
                                {
                                    reportingSystem.FillTable(customer.INN, customer.OKPO, customer.PSRN);
                                    var organization = reportingSystem.GetOrganizationStatisticsCodesAsync().Result;

                                    if (organization != null)
                                    {
                                        if (string.IsNullOrWhiteSpace(customer.OKPO))
                                        {
                                            customer.OKPO = organization.OKPO;
                                        }

                                        if (string.IsNullOrWhiteSpace(customer.PSRN))
                                        {
                                            customer.PSRN = organization.PSRN;
                                        }

                                        if (string.IsNullOrWhiteSpace(customer.OKTMO))
                                        {
                                            customer.OKTMO = organization.ActualOKTMO;
                                        }

                                        if (string.IsNullOrWhiteSpace(customer.OKATO))
                                        {
                                            customer.OKATO = organization.ActualOKATO;
                                        }

                                        var reports = reportingSystem.GetReportsAsync().Result;

                                        var isUpdate = false;

                                        foreach (var item in reports)
                                        {
                                            var report = Session.FindObject<Report>(new BinaryOperator(nameof(Report.OKUD), item.OKUD));
                                            if (report is null)
                                            {
                                                report = new Report(Session)
                                                {
                                                    FormIndex = item.FormIndex,
                                                    Name = item.Name,
                                                    Periodicity = (Periodicity)Convert.ToInt32(item.Periodicity),
                                                    Deadline = item.Deadline,
                                                    Comment = item.Comment,
                                                    OKUD = item.OKUD
                                                };
                                                report.Save();
                                            }

                                            if (customer.StatisticalReports.FirstOrDefault(f => f.Report.OKUD == report.OKUD) is null)
                                            {
                                                var statisticalReport = new StatisticalReport(Session)
                                                {
                                                    Report = report,
                                                    Year = DateTime.Now.Year
                                                };
                                                statisticalReport.SetCreateObj(await Session.GetObjectByKeyAsync<User>(DatabaseConnection.User?.Oid));
                                                customer.StatisticalReports.Add(statisticalReport);
                                                isUpdate = true;
                                            }
                                        }

                                        if (isUpdate)
                                        {
                                            customer.ChronicleCustomers.Add(new ChronicleCustomer(Session)
                                            {
                                                Act = Act.UPDATING_INFORMATION_REPORTING_SYSTEM_AUTO,
                                                Date = DateTime.Now,
                                                Description = Act.UPDATING_INFORMATION_REPORTING_SYSTEM_AUTO.GetEnumDescription(),
                                                User = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid)
                                            });
                                        }

                                        customer.Save();
                                    }
                                }
                            }
                        }

                        chronicleEvents = new ChronicleEvents(Session)
                        {
                            Act = Act.UPDATING_INFORMATION_REPORTING_SYSTEM_AUTO,
                            Date = DateTime.Now,
                            Name = Act.UPDATING_INFORMATION_REPORTING_SYSTEM_AUTO.GetEnumDescription(),
                            User = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid)
                        };
                        chronicleEvents.Save();
                    }
                }
                catch (Exception) { }
            });
        }

        private static void ReportingSystem_ErrorEvent(object sender, string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                XtraMessageBox.Show(message, "Необходимо обновить ChromeDriver", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async System.Threading.Tasks.Task UpdateReportInformationHandAsync()
        {
            using var uof = new UnitOfWork();
            var customers = await new XPQuery<Customer>(uof).ToListAsync();

            foreach (var customer in customers)
            {
                await GetStatisticalReportAsync(uof, customer);
            }

            var chronicleEvents = new ChronicleEvents(uof)
            {
                Act = Act.UPDATING_INFORMATION_REPORTING_SYSTEM_AUTO,
                Date = DateTime.Now,
                Name = Act.UPDATING_INFORMATION_REPORTING_SYSTEM_AUTO.GetEnumDescription(),
                User = await uof.GetObjectByKeyAsync<User>(DatabaseConnection.User.Oid)
            };
            chronicleEvents.Save();

            await uof.CommitTransactionAsync();

            //await System.Threading.Tasks.Task.Run(async () =>
            //{
            //    try
            //    {
            //        var xpcollectionCustomer = new XPCollection<Customer>(Session);
            //        var inns = xpcollectionCustomer.Select(s => s.INN).ToArray();

            //        if (inns.Length > 0)
            //        {
            //            using (var reportingSystem = new ReportingSystem())
            //            {
            //                reportingSystem.ErrorEvent += ReportingSystem_ErrorEvent;
            //                if (reportingSystem.StartDriver() is false)
            //                {
            //                    return;
            //                };

            //                foreach (var customer in xpcollectionCustomer)
            //                {
            //                    reportingSystem.FillTable(customer.INN, customer.OKPO, customer.PSRN);
            //                    var organization = reportingSystem.GetOrganizationStatisticsCodesAsync().Result;

            //                    if (organization != null)
            //                    {
            //                        if (string.IsNullOrWhiteSpace(customer.OKPO))
            //                        {
            //                            customer.OKPO = organization.OKPO;
            //                        }

            //                        if (string.IsNullOrWhiteSpace(customer.PSRN))
            //                        {
            //                            customer.PSRN = organization.PSRN;
            //                        }

            //                        if (string.IsNullOrWhiteSpace(customer.OKTMO))
            //                        {
            //                            customer.OKTMO = organization.ActualOKTMO;
            //                        }

            //                        if (string.IsNullOrWhiteSpace(customer.OKATO))
            //                        {
            //                            customer.OKATO = organization.ActualOKATO;
            //                        }

            //                        var reports = reportingSystem.GetReportsAsync().Result;

            //                        var isUpdate = false;

            //                        if (reports != null && reports.Count > 0)
            //                        {
            //                            var addList = new List<Report>();
            //                            var add = 1;
            //                            var del = 1;

            //                            var addMessage = default(string);
            //                            var delMessage = default(string);

            //                            foreach (var item in reports)
            //                            {
            //                                var report = customer.Session.FindObject<Report>(new BinaryOperator(nameof(Report.OKUD), item.OKUD));
            //                                if (report is null)
            //                                {
            //                                    report = new Report(customer.Session)
            //                                    {
            //                                        FormIndex = item.FormIndex,
            //                                        Name = item.Name,
            //                                        Periodicity = (Periodicity)Convert.ToInt32(item.Periodicity),
            //                                        Deadline = item.Deadline,
            //                                        Comment = item.Comment,
            //                                        OKUD = item.OKUD
            //                                    };
            //                                    report.Save();
            //                                }

            //                                var customerStatisticaReport = customer.StatisticalReports.FirstOrDefault(f => f.Report.OKUD == report.OKUD);
            //                                if (customerStatisticaReport is null)
            //                                {
            //                                    var statisticalReport = new StatisticalReport(Session)
            //                                    {
            //                                        Report = report,
            //                                        Year = DateTime.Now.Year
            //                                    };
            //                                    statisticalReport.SetCreateObj(await Session.GetObjectByKeyAsync<User>(DatabaseConnection.User?.Oid));
            //                                    customer.StatisticalReports.Add(statisticalReport);
            //                                    isUpdate = true;

            //                                    addMessage += $"[{add}] {statisticalReport}{Environment.NewLine}";
            //                                    add++;
            //                                }
            //                                else
            //                                {
            //                                    if (customerStatisticaReport.Year != DateTime.Now.Year)
            //                                    {
            //                                        customerStatisticaReport.Year = DateTime.Now.Year;
            //                                        customerStatisticaReport.Save();
            //                                    }
            //                                }

            //                                addList.Add(report);
            //                            }

            //                            //if (!string.IsNullOrWhiteSpace(addMessage))
            //                            //{
            //                            //    message += $"{Environment.NewLine}{Environment.NewLine}Добавлены следующие отчеты:{Environment.NewLine}{addMessage.Trim()}";
            //                            //}

            //                            var deleteList = new List<StatisticalReport>();
            //                            foreach (var item in customer.StatisticalReports)
            //                            {
            //                                var obj = addList.FirstOrDefault(f => f?.Oid == item.Report?.Oid);
            //                                if (obj is null)
            //                                {
            //                                    deleteList.Add(item);
            //                                    delMessage += $"[{del}] {item}{Environment.NewLine}";
            //                                    del++;
            //                                }
            //                            }

            //                            if (deleteList.Count > 0)
            //                            {
            //                                Session.Delete(deleteList);
            //                            }

            //                            //if (!string.IsNullOrWhiteSpace(delMessage))
            //                            //{
            //                            //    message += $"{Environment.NewLine}{Environment.NewLine}Удалены следующие отчеты:{Environment.NewLine}{delMessage.Trim()}";
            //                            //}

            //                            if (isUpdate)
            //                            {
            //                                var chronicle = new ChronicleCustomer(customer.Session)
            //                                {
            //                                    Act = Act.UPDATING_INFORMATION_REPORTING_SYSTEM_HAND,
            //                                    Date = DateTime.Now,
            //                                    Description = Act.UPDATING_INFORMATION_REPORTING_SYSTEM_HAND.GetEnumDescription(),
            //                                    User = customer.Session.GetObjectByKey<User>(DatabaseConnection.User.Oid),
            //                                    Customer = customer                                              
            //                                };
            //                                chronicle.Save();
            //                                customer.ChronicleCustomers.Add(chronicle);
            //                            }
            //                        }
            //                        else
            //                        {
            //                            if (customer.StatisticalReports != null && customer.StatisticalReports.Count > 0)
            //                            {
            //                                //var i = 1;
            //                                //foreach (var item in customer.StatisticalReports)
            //                                //{
            //                                //    message += $"[{i}] {item}{Environment.NewLine}";
            //                                //    i++;
            //                                //}

            //                                //if (!string.IsNullOrWhiteSpace(message))
            //                                //{
            //                                //    message = $"{Environment.NewLine}{Environment.NewLine}Удалены следующие отчеты:{Environment.NewLine}{message.Trim()}";
            //                                //}

            //                                Session.Delete(customer.StatisticalReports);
            //                            }
            //                        }

            //                        customer.Save();
            //                    }
            //                }
            //            }
            //        }

            //        var chronicleEvents = new ChronicleEvents(Session)
            //        {
            //            Act = Act.UPDATING_INFORMATION_REPORTING_SYSTEM_HAND,
            //            Date = DateTime.Now,
            //            Name = Act.UPDATING_INFORMATION_REPORTING_SYSTEM_HAND.GetEnumDescription(),
            //            User = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid)
            //        };
            //        chronicleEvents.Save();

            //        XtraMessageBox.Show("Обновление успешно окончено", "Успешное окончание обновления", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //    catch (Exception ex) 
            //    {
            //        RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            //    }
            //});
        }

        private static async System.Threading.Tasks.Task GetStatisticalReportAsync(UnitOfWork uof, Customer customer, int delay = 1, int count = 1)
        {
            await System.Threading.Tasks.Task.Delay(delay * 1000);

            try
            {
                if (!string.IsNullOrWhiteSpace(customer.INN))
                {
                    await GetReportAsync(uof, customer);
                    await uof.CommitTransactionAsync();
                }
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
                if (count <= 10)
                {
                    count++;
                    delay += 5;
                    await GetStatisticalReportAsync(uof, customer, delay, count);
                }
            }
        }

        private static async System.Threading.Tasks.Task GetReportAsync(UnitOfWork uof, Customer customer)
        {
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
                    var report = await uof.FindObjectAsync<Report>(new BinaryOperator(nameof(Report.OKUD), item.Okud));
                    if (report is null)
                    {
                        report = new Report(uof)
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
                        var statisticalReport = new StatisticalReport(uof)
                        {
                            Report = report,
                            Year = DateTime.Now.Year
                        };
                        statisticalReport.SetCreateObj(await uof.GetObjectByKeyAsync<User>(DatabaseConnection.User?.Oid));
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
                    uof.Delete(deleteList);
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

                    uof.Delete(customer.StatisticalReports);
                }
            }

            var description = $"Ручное заполнение отчетов клиента по ИНН [{customer.INN}]";
            var chronicle = new ChronicleCustomer(uof)
            {
                Act = Act.UPDATING_INFORMATION_REPORTING_SYSTEM_HAND,
                Date = DateTime.Now,
                Description = description,
                User = uof.GetObjectByKey<User>(DatabaseConnection.User.Oid),
                Customer = customer
            };
            chronicle.Save();
            customer.ChronicleCustomers.Add(chronicle);
            customer.Save();
        }

        private static async void UpdateCustomerInformationHand()
        {
            await System.Threading.Tasks.Task.Run(async () =>
            {
                try
                {
                    using var uof = new UnitOfWork();
                    var customers = await new XPQuery<Customer>(uof).ToListAsync();

                    foreach (var customer in customers)
                    {
                        if (!string.IsNullOrWhiteSpace(customer.INN))
                        {
                            var getInfoFromDaData = new GetInfoOrganizationFromDaData("a8cfa20b99fb660c53b8e0fa2d3de0f2204fb580", "080aefa3543cb56dfe122f26a16a04703cacb128", customer.INN);
                            await getInfoFromDaData.GetDataAsync();

                            if (getInfoFromDaData != null)
                            {
                                customer.DateActuality = getInfoFromDaData.DateActuality;
                                customer.DateLiquidation = getInfoFromDaData.DateLiquidation;
                                customer.OrganizationStatus = (StatusOrganization)getInfoFromDaData.OrganizationStatus;
                                customer.Save();

                                await uof.CommitTransactionAsync().ConfigureAwait(false);
                            }
                        }
                    }

                    var chronicleEvents = new ChronicleEvents(uof)
                    {
                        Act = Act.UPDATING_CUSTOMER_INFORMATION_HAND,
                        Date = DateTime.Now,
                        Name = Act.UPDATING_CUSTOMER_INFORMATION_HAND.GetEnumDescription(),
                        User = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid)
                    };
                    chronicleEvents.Save();
                    await uof.CommitTransactionAsync().ConfigureAwait(false);

                    XtraMessageBox.Show("Обновление успешно окончено", "Успешное окончание обновления", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception) { }
            });
        }

        private async System.Threading.Tasks.Task GetLetters()
        {
            var dayString = await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, nameof(cls_AppParam.CountOfDaysToAcceptLetter), "1", user: BVVGlobal.oApp.User);
            var countLetterToSave = 0; 
            var countString = await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, nameof(cls_AppParam.CountOfLetterToSave), "0", user: BVVGlobal.oApp.User);

            if (int.TryParse(countString, out int count))
            {
                countLetterToSave = count;
            }

            if (int.TryParse(dayString, out int day))
            {
                await GetLetter(DateTime.Now.AddDays(day * -1), countLetterToSave).ConfigureAwait(false);
            }
            else
            {
                await GetLetter(DateTime.Now.AddDays(-3), countLetterToSave).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Получение писем всех активных почтовых ящиков.
        /// </summary>
        private async System.Threading.Tasks.Task GetLetter(object obj = default, int countToSave = 0)
        {
            var groupOperatorStateMailbox = new GroupOperator(GroupOperatorType.Or);

            var criteriaStateMailboxWorking = new BinaryOperator(nameof(Mailbox.StateMailbox), StateMailbox.Working);
            groupOperatorStateMailbox.Operands.Add(criteriaStateMailboxWorking);

            var criteriaStateMailboxReceivingLetters = new BinaryOperator(nameof(Mailbox.StateMailbox), StateMailbox.ReceivingLetters);
            groupOperatorStateMailbox.Operands.Add(criteriaStateMailboxReceivingLetters);

            var xpcollectionMailbox = new XPCollection<Mailbox>(Session, groupOperatorStateMailbox);
            MailClients.FillingListMailClients(xpcollectionMailbox);

            foreach (var mailClients in MailClients.ListMailClients)
            {
                if (mailClients.IsReceivingLetters is false)
                {
                    mailClients.GetLetter += MailClients_GetLetter;
                    mailClients.SaveLetter += MailClients_SaveLetter;

                    if (obj is DateTime date)
                    {
                        await mailClients.GetAllEmailsByDate(date.Date, countToSave, await LetterForm.GetEmailFilteringDate()).ConfigureAwait(false);
                    }
                    else
                    {
                        await mailClients.GetAllEmailsByDate(countToSave: countToSave, emailFilteringDate: await LetterForm.GetEmailFilteringDate()).ConfigureAwait(false);
                    }
                    
                    mailClients.GetLetter -= MailClients_GetLetter;
                    mailClients.SaveLetter -= MailClients_SaveLetter;
                }               
            }
        }

        private async void MailClients_SaveLetter(object sender, Letter letter)
        {
            await LetterForm.SendTelegramMessageLetter(letter);
            
            if (bool.TryParse(await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "formProgramSettings_IsUsePopupWindow", "false", false, false, 1, BVVGlobal.oApp.User), out bool result) && result is true)
            {
                Invoke((Action)delegate
                {
                    LetterForm.GetPopupForm(letter);
                });
            }
        }

        private void MailClients_GetLetter(object sender, string folderName, int count, int number)
        {
            try
            {
                var mailClient = sender as MailClients;
                var form = Program.MainForm;

                if (mailClient != null && form != null && form.IsDisposed is false)
                {
                    form.Progress_Start(0, count, $"Обработка почтового ящика {mailClient.Mailbox} <-> Каталог: {folderName} ");
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

        private void UseSplashScreenManager(bool isHide = true)
        {
            try
            {
                if (isHide)
                {
                    SplashScreenManager.HideImage();
                }
                else
                {
                    var image = Image.FromFile("images//rms.png");
                    SplashScreenManager.ShowImage(image);
                }
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async System.Threading.Tasks.Task BirthdayAlertAsync()
        {
            var dateTimeNow = DateTime.Now.Date;
            var staffs = await new XPQuery<Staff>(DatabaseConnection.GetWorkSession())
                .Where(w => w.DateDismissal == null)
                .Where(w => w.DateBirth != null)
                .ToListAsync();

            var message = $"Дни рождения сотрудников:{Environment.NewLine}{Environment.NewLine}";
            var isUseMessage = false;
            foreach (var staff in staffs)
            {
                if (staff.DateBirth is DateTime dateBirthDay)
                {
                    var currentDateBirthday = new DateTime(dateTimeNow.Year, dateBirthDay.Month, dateBirthDay.Day);
                    var countDay = (currentDateBirthday - dateTimeNow).Days;
                    if (countDay >= 0 && countDay <= 3)
                    {
                        message += $"{staff} <-> {currentDateBirthday.ToShortDateString()}{Environment.NewLine}";
                        isUseMessage = true;
                    }
                }
            }

            if (isUseMessage)
            {
                XtraMessageBox.Show(message, "Праздник к нам приходит", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private async void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                UseSplashScreenManager(false);
                await InitFirstSettings();
                await CodeFirst.SetFirstData();
                await BVVGlobal.oFuncXpo.FillSettingsParameters();

                BVVGlobal.oApp.AppParam = await BVVGlobal.oFuncXpo.FillAppParam();

                formLogin frm = new formLogin();
                UseSplashScreenManager();
                frm.ShowDialog();

                if (frm.FlagOK == false)
                {
                    Close();
                }
                else
                {
                    WindowState = FormWindowState.Maximized;

                    var user = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid);

                    if (user != null)
                    {
                        if (user.Staff != null)
                        {
                            txtUser.NullText = user.Staff.ToString();
                        }
                        else
                        {
                            txtUser.NullText = user.ToString();
                        }
                    }

                    ribbonControl.SelectedPage = rbPageMain;

                    await SetAccessRights();
                    await OpenForms();
                    
                    var enableOrDisableGetEmails = await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, nameof(cls_AppParam.EnableOrDisableGetEmails), user: BVVGlobal.oApp.User);
                    if (!string.IsNullOrWhiteSpace(enableOrDisableGetEmails))
                    {
                        if (enableOrDisableGetEmails.Equals("1"))
                        {
                            await GetLetters().ConfigureAwait(false);
                        }
                    }

                    await GetProgramEvent().ConfigureAwait(false);
                    await PackagesDocumentController.CheckingDocumentStatusAsync().ConfigureAwait(false);
                    await BirthdayAlertAsync().ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, " Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetRibbonPageGroupVisible(RibbonPageGroup ribbonPageGroup)
        {
            try
            {
                var result = false;
                foreach (BarItemLink item in ribbonPageGroup.ItemLinks)
                {
                    if (item.Item is BarButtonItem barButtonItem)
                    {
                        if (barButtonItem.Visibility == BarItemVisibility.Always)
                        {
                            result = true;
                            break;
                        }
                    }
                }
                ribbonPageGroup.Visible = result;
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
            }
        }
        
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
                            barBtnDesktop.Visibility = GetBarItemVisibility(accessRights.IsViewDesktopForm);
                            barBtnCustomers.Visibility = GetBarItemVisibility(accessRights.IsViewCustomersForm);
                            barBtnContract.Visibility = GetBarItemVisibility(accessRights.IsViewContractForm);
                            barBtnTasks.Visibility = GetBarItemVisibility(accessRights.IsViewTaskForm);
                            barBtnStaff.Visibility = GetBarItemVisibility(accessRights.IsViewStaffForm);
                            barBtnReports.Visibility = GetBarItemVisibility(accessRights.IsViewReportChangeForm);
                            barBtnDeals.Visibility = GetBarItemVisibility(accessRights.IsViewDealForm);
                            barBtnInvoices.Visibility = GetBarItemVisibility(accessRights.IsViewInvoiceForm);
                            barBtnLetter.Visibility = GetBarItemVisibility(accessRights.IsViewLetterForm);
                            barBtnSalary.Visibility = GetBarItemVisibility(accessRights.IsViewSalaryForm);
                            barBtnArchiveFolders.Visibility = GetBarItemVisibility(accessRights.IsViewArchiveFolderChangeForm);
                            
                            barBtnProgramEvent.Visibility = GetBarItemVisibility(accessRights.IsViewProgramEventForm2);
                            barBtnControlSystem.Visibility = GetBarItemVisibility(accessRights.IsViewControlSystemForm);

                            barBtnRouteSheet.Visibility = GetBarItemVisibility(accessRights.IsViewRouteSheetForm);
                            barBtnTaskCourier.Visibility = GetBarItemVisibility(accessRights.IsViewTaskCourierForm);
                            
                            SetRibbonPageGroupVisible(rbPageGroupSystemControl);
                            SetRibbonPageGroupVisible(rbPageGroupMain);
                            SetRibbonPageGroupVisible(rbArchiveFolder);
                            SetRibbonPageGroupVisible(ribbonPageGroupRouteSheet);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
            }
        }

        private BarItemVisibility GetBarItemVisibility(bool isVisibility = false)
        {
            if (isVisibility)
            {
                return BarItemVisibility.Always;
            }
            else
            {
                return BarItemVisibility.Never;
            }
        }

        private async System.Threading.Tasks.Task OpenForms()
        {            
            await CheckAccessRights<DesktopForm>(barBtnDesktop);
            await CheckAccessRights<CustomersForm>(barBtnCustomers);
            await CheckAccessRights<ContractForm>(barBtnContract);
            await CheckAccessRights<TaskForm>(barBtnTasks);
            await CheckAccessRights<StaffForm>(barBtnStaff);
            await CheckAccessRights<ReportChangeForm>(barBtnReports);
            await CheckAccessRights<DealForm>(barBtnDeals);
            //await CheckAccessRights<InvoiceForm>(barBtnInvoices);
            //await CheckAccessRights<LetterForm>(barBtnLetter);
            await CheckAccessRights<SalaryForm>(barBtnSalary);
            await CheckAccessRights<ArchiveFolderChangeForm>(barBtnArchiveFolders);
            await CheckAccessRights<RouteSheetv2Form>(barBtnRouteSheet, nameof(RouteSheetForm));
            await CheckAccessRights<TaskRouteSheetv2Form>(barBtnTaskCourier, nameof(TaskCourierForm));
            await CheckAccessRights<ControlSystemForm>(barBtnControlSystem);
            await CheckAccessRights<ProgramEventForm2>(barBtnProgramEvent);
        }
        
        private async System.Threading.Tasks.Task CheckAccessRights<T>(BarButtonItem barButtonItem, string formName = null) where T : Form
        {
            try
            {
                if (barButtonItem.Visibility == BarItemVisibility.Always)
                {
                    await CheckFormForOpening<T>(formName);
                }
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async System.Threading.Tasks.Task CheckFormForOpening<T>(string formName = null) where T : Form
        {
            try
            {
                var name = typeof(T).Name;
                if (!string.IsNullOrWhiteSpace(formName))
                {
                    name = formName;
                }
                
                var isOpen = await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, $"{this.Name}_{nameof(OpenForms)}_{name}", null, user: BVVGlobal.oApp.User);
                if (bool.TryParse(isOpen, out bool result) && result is true)
                {
                    OpensForm<T>(formName: formName);
                }
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
            }
        }

        private void OpensForm<T>(bool isForcedOpening = false, string formName = null, bool isUseSessionActivator = true) where T : Form
        {
            try
            {
                var isOpen = true;

                var accessRights = DatabaseConnection.User.AccessRights;
                if (accessRights != null)
                {
                    isOpen = accessRights.ReturnValueView<T>(formName);
                }

                if (isForcedOpening)
                {
                    isOpen = true;
                }
                
                if (isOpen)
                {
                    var form = default(T);

                    if (isUseSessionActivator)
                    {
                        form = (T)Activator.CreateInstance(typeof(T), Session);
                    }
                    else
                    {
                        form = (T)Activator.CreateInstance(typeof(T));
                    }

                    if (string.IsNullOrWhiteSpace(formName))
                    {
                        OpenForm(this, form);
                    }
                    else
                    {
                        OpenForm(this, form, typeof(T).Name);
                    }
                }                
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
            }
        }

        public static void OpenForm(Form parentForm, Form openForm, string formName = null)
        {
            var nameForm = openForm.Name;

            if (!string.IsNullOrWhiteSpace(formName))
            {
                nameForm = formName;
            }
            
            try
            {
                bool isOpenForm = false;
                if (parentForm.MdiChildren.Length > 0)
                {
                    for (int i = 0; i < parentForm.MdiChildren.Length; i++)
                    {
                        if (parentForm.MdiChildren[i].GetType().Name == nameForm)
                        {
                            parentForm.MdiChildren[i].Activate();
                            isOpenForm = true;
                        }
                    }
                }

                if (!isOpenForm)
                {
                    openForm.MdiParent = parentForm;
                    openForm.Show();
                }
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async System.Threading.Tasks.Task GetProgramEvent()
        {
            var dateTime = DateTime.Now.Date;
            using var uof = new UnitOfWork();
            await GetNotifyUserMessageAsync(dateTime, uof).ConfigureAwait(false);
            await GetProgramEventTaskAsync(dateTime, uof).ConfigureAwait(false);
        }
        
        private static async System.Threading.Tasks.Task GetProgramEventTaskAsync(DateTime dateTime, UnitOfWork uof)
        {
            var programEventNotify = await new XPQuery<ProgramEvent>(uof)
                ?.Where(w =>
                    w.ActionProgramEvent == ActionProgramEvent.CREATE_NEW_OBJECT
                        && ((w.DateSince == null && w.DateTo >= dateTime)
                            || (w.DateSince <= dateTime && w.DateTo >= dateTime)
                            || (w.DateSince <= dateTime && w.DateTo == null)
                            || (w.DateSince == null && w.DateTo == null)))
                ?.ToListAsync();

            var result = default(string);
            var i = 1;
            foreach (var programEvent in programEventNotify)
            {
                if (programEvent.GetDatesUse().Contains(dateTime))
                {
                    var executionDates = PulsLibrary.Methods.ConversionObjects.Converter.DeserializeObjectByteToObject<List<DateTime>>(programEvent.ExecutionDatesObj);

                    if (executionDates is null)
                    {
                        executionDates = new List<DateTime>();
                    }
                    else if (executionDates.Contains(dateTime) is true)
                    {
                        continue;
                    }

                    try
                    {
                        var obj = await uof.GetObjectByKeyAsync(programEvent.GetTypeObj(), programEvent?.ControlSystemObjectId);
                        if (obj is IProgramEvent eventObj)
                        {
                            eventObj.ObjCreate();

                            executionDates.Add(dateTime);
                            programEvent.ExecutionDatesObj = PulsLibrary.Methods.ConversionObjects.Converter.SerializeObjectToJsonByte(executionDates);
                            programEvent.Save();
                        }
                    }
                    catch (Exception ex)
                    {
                        LoggerController.WriteLog(ex?.ToString());
                    }
                }

                await uof.CommitTransactionAsync().ConfigureAwait(false);
            }
        }

        private static async System.Threading.Tasks.Task GetNotifyUserMessageAsync(DateTime dateTime, UnitOfWork uof)
        {
            var programEventNotify = await new XPQuery<ProgramEvent>(uof)
                ?.Where(w => 
                    w.ActionProgramEvent == ActionProgramEvent.NOTIFY
                        && ((w.DateSince == null && w.DateTo >= dateTime)
                            || (w.DateSince <= dateTime && w.DateTo >= dateTime)
                            || (w.DateSince <= dateTime && w.DateTo == null)
                            || (w.DateSince == null && w.DateTo == null)))
                ?.ToListAsync(); 
            
            var result = default(string);
            var i = 1;
            foreach (var programEvent in programEventNotify)
            {
                if (programEvent.GetDatesUse().Contains(dateTime))
                {
                    try
                    {
                        var obj = await uof.GetObjectByKeyAsync(programEvent.GetTypeObj(), programEvent?.ControlSystemObjectId);
                        if (obj is IProgramEvent eventObj)
                        {
                            result += $"{i}. {eventObj.Message()}{Environment.NewLine}";
                            i++;
                        }
                    }
                    catch (Exception ex)
                    {
                        LoggerController.WriteLog(ex?.ToString());
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(result))
            {
                XtraMessageBox.Show(result, $"Оповещение на {dateTime.ToShortDateString()}", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        /// <summary>
        /// Получить доступ к БД
        /// </summary>
        private async System.Threading.Tasks.Task InitFirstSettings()
        {
            try
            {
                string baseDir = BVVGlobal.oApp.BaseDirectory;

                string Connection_stringSQL = await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "Connection_stringSQL", string.Empty);
                if (string.Compare(Connection_stringSQL, String.Empty, StringComparison.Ordinal) == 0)
                {
                    cls_XpoSettings oXpoSettingsDB = new cls_XpoSettings();
                    string s_db_name = string.Empty; // "SQLite"; // "SQLServer"; // "Access";
                    string s_data_source = string.Empty, s_user_name = string.Empty, s_user_password = string.Empty;
                    string s_path_to_settings = $@"{baseDir}\settings\XpoSettingsDB.xml";
                    string path_to_db = string.Empty;
                    if (oXpoSettingsDB.LoadXpoSettingsDB(s_path_to_settings, s_db_name, s_data_source, s_user_name, s_user_password, path_to_db))
                    {
                        if (oXpoSettingsDB.WebServiceFlag)
                        {
                            
                        }
                        else
                        {
                            DatabaseConnection.BaseConnectionString = oXpoSettingsDB.ConnectionString;
                            BVVGlobal.oXpo.Connection_stringSQL = oXpoSettingsDB.ConnectionString;
                        }
                    }
                    else
                    {
                        throw new Exception("Error for read Settings File!");
                    }

                }
                else
                {
                    DatabaseConnection.BaseConnectionString = Connection_stringSQL;
                    BVVGlobal.oXpo.Connection_stringSQL = Connection_stringSQL;
                }
            }
            catch (Exception e)
            {
                XtraMessageBox.Show(e.Message, " Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        

        public void Progress_Start(int iBegin, int iEnd, string caption = default)
        {
            repositoryProgress.Minimum = iBegin;
            repositoryProgress.Maximum = iEnd;
            barProgress.Visibility = BarItemVisibility.Always;
            barProgress.Caption = caption;
        }

        public void Progress_Position(int iValue, bool flCaption = false)
        {
            barProgress.EditValue = iValue;
            if (flCaption)
            {
                barProgress.Caption = iValue.ToString() + " ";
            }
        }

        public void Progress_Stop()
        {
            barProgress.Reset();
            barProgress.Visibility = BarItemVisibility.Never;
            barProgress.Caption = string.Empty;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (BVVGlobal.oApp.Get_flagProcess())
            {
                e.Cancel = true;
                XtraMessageBox.Show(this, "This operation in not completed yet. Please wait.", "Cannot close this form");
            }
        }

        private void barBtnProgramClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            Close();
        }

        private void barBtnSettings_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (formProgramSettings frm = new formProgramSettings())
            {
                frm.ShowDialog();
            }
        }

        public void SetRefreshChildForm(RefreshChildForm funcRefresh)
        {
            if (funcRefresh == null)
            {
                OnRefreshChildForm = null;
            }
            else
            {
                OnRefreshChildForm += funcRefresh;
            }
        }

        private async void rgbiSkins_GalleryItemClick(object sender, GalleryItemClickEventArgs e)
        {
            await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(null, "tek_skin", (e.Item.Tag).ToString(), true, true, 1);
        }

        private void barBtnDesktop_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpensForm<DesktopForm>();
        }

        private void barBtnCustomers_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpensForm<CustomersForm>();
        }

        private void barBtnTasks_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpensForm<TaskForm>();
        }

        private void barBtnContract_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpensForm<ContractForm>();
        }

        private void barBtnImport_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new ImportForm(Session);
            form.ShowDialog();
        }

        private void barBtnStaff_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpensForm<StaffForm>();
        }

        private void barBtnCustomerUpdate_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("Информация о статусе клиентов обновляется каждый день при входе в программу. Желаете обновить в ручном режима?",
                "Обновление статуса клиентов", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                UpdateCustomerInformationHand();
            }
        }

        private async void barBtnReportUpdate_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("Информация об отчетах клиентов обновляется каждый день при входе в программу. Желаете обновить в ручном режима?",
                "Обновление клиентских отчетов", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                await UpdateReportInformationHandAsync();
            }
        }
        private void barSubBtnPayoutDictionary_ItemClick(object sender, ItemClickEventArgs e)
        {
            
        }
        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.PayoutDictionary, -1);
        }

        private void barBtnUser_ItemClick(object sender, ItemClickEventArgs e)
        {
            cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.User, -1);
        }

        private void barBtnUserGroup_ItemClick(object sender, ItemClickEventArgs e)
        {
            cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.UserGroup, -1);
        }

        private void barBtnCustomersFilter_ItemClick(object sender, ItemClickEventArgs e)
        {
            cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.CustomerFilter, -1);
        }

        private void barBtnFormCorporation_ItemClick(object sender, ItemClickEventArgs e)
        {
            cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.FormCorporation, -1);
        }

        private void barBtnPosition_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.Position, -1);
        }

        private void barBtnStatus_ItemClick(object sender, ItemClickEventArgs e)
        {
            cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.Status, -1);
        }

        private void barBtnContractStatus_ItemClick(object sender, ItemClickEventArgs e)
        {
            cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.ContractStatus, -1);
        }

        private void btnContractPlateTemplate_ItemClick(object sender, ItemClickEventArgs e)
        {
            cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.PlateTemplate, -1);
        }

        private void barBtnTaxSystem_ItemClick(object sender, ItemClickEventArgs e)
        {
            cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.TaxSystem, -1);
        }

        private void barBtnTypePayment_ItemClick(object sender, ItemClickEventArgs e)
        {
            cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.TypePayment, -1);
        }

        private void barBtnPriceList_ItemClick(object sender, ItemClickEventArgs e)
        {
            cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.PriceList, -1);
        }

        private void barBtnPrivilege_ItemClick(object sender, ItemClickEventArgs e)
        {
            cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.Privilege, -1);
        }

        private void barBtnReport_ItemClick(object sender, ItemClickEventArgs e)
        {
            cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.Report, -1);
        }

        private void barBtnElectronicReporting_ItemClick(object sender, ItemClickEventArgs e)
        {
            cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.ElectronicReporting, -1);
        }

        private void barBtnPhysicalIndicator_ItemClick(object sender, ItemClickEventArgs e)
        {
            cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.PhysicalIndicator, -1);
        }

        private void barBtnArchiveFolder_ItemClick(object sender, ItemClickEventArgs e)
        {
            cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.ArchiveFolder, -1);
        }

        private void barBtnLetterTemplate_ItemClick(object sender, ItemClickEventArgs e)
        {
            cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.LetterTemplate, -1);
        }
        
        private void barBtnPerfomanceIndicator_ItemClick(object sender, ItemClickEventArgs e)
        {
            cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.PerformanceIndicator, -1);
        }
        
        private void barBtnGroupPerformanceIndicator_ItemClick(object sender, ItemClickEventArgs e)
        {
            cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.GroupPerformanceIndicator, -1);
        }

        private void barBtnMail_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpensForm<LetterForm>();
        }
        
        private void barBtnAccounts_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpensForm<InvoiceForm>();
        }

        private void batBtnServiceCalculator_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new ServiceCalculatorForm();
            form.Show();
        }
        
        private void batBtnServiceCalculator2_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new ServiceCalculatorForm2();
            form.Show();
        }

        private void barBtnReports_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpensForm<ReportChangeForm>();
        }

        private void barBtnArchiveFolders_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpensForm<ArchiveFolderChangeForm>();
        }

        private async void barBtnUpdater_ItemClick(object sender, ItemClickEventArgs e)
        {
            await System.Threading.Tasks.Task.Run(async () =>
            {
                AutoUpdater.Mandatory = true;
                var path = await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(
                    DatabaseConnection.LocalSession,
                    nameof(cls_AppParam.PathUpdateService),
                    "ftp://81.28.221.114/other/RMS/RMS.Update.xml");
                AutoUpdater.Start(path);
            });
        }

        private void barBtnRouteSheet_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpensForm<RouteSheetv2Form>(formName: nameof(RouteSheetForm));
        }

        private void barBtnTaskCourier_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpensForm<TaskRouteSheetv2Form>(formName: nameof(TaskCourierForm));
        }

        private void barBtnDeals_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpensForm<DealForm>();
        }

        private void barBtnAffairs_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barBtnAdditionalServices_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barBtnDocuments_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barBtnFAQ_ItemClick(object sender, ItemClickEventArgs e)
        {
            var workZone = default(WorkZone?);
            var formActive = xtraTabbedMdiManager?.SelectedPage;
            
            if (formActive != null)
            {
                if (formActive.MdiChild.GetType() == typeof(CustomersForm))
                {
                    workZone = WorkZone.ModuleCustomer;
                }
                else if (formActive.MdiChild.GetType() == typeof(ContractForm))
                {
                    workZone = WorkZone.ModuleContract;
                }
                else if (formActive.MdiChild.GetType() == typeof(TaskForm))
                {
                    workZone = WorkZone.ModuleTask;
                }
                else if (formActive.MdiChild.GetType() == typeof(StaffForm))
                {
                    workZone = WorkZone.ModuleStaff;
                }
                else if (formActive.MdiChild.GetType() == typeof(ReportChangeForm))
                {
                    workZone = WorkZone.ModuleReport;
                }
                else if (formActive.MdiChild.GetType() == typeof(DealForm))
                {
                    workZone = WorkZone.ModuleDeal;
                }
                else if (formActive.MdiChild.GetType() == typeof(InvoiceForm))
                {
                    workZone = WorkZone.ModuleInvoice;
                }
                else if (formActive.MdiChild.GetType() == typeof(LetterForm))
                {
                    workZone = WorkZone.ModuleMail;
                }
                else if (formActive.MdiChild.GetType() == typeof(ArchiveFolderChangeForm))
                {
                    workZone = WorkZone.ModuleArchiveFolder;
                }
                else if (formActive.MdiChild.GetType() == typeof(RouteSheetForm))
                {
                    workZone = WorkZone.ModuleRouteSheet;
                }
                else if (formActive.MdiChild.GetType() == typeof(TaskCourierForm))
                {
                    workZone = WorkZone.ModuleTaskCourier;
                }                
            }
            
            var form = new FormFAQEdit(Session, workZone);
            form.Show();
        }

        private void barBtnSalary_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpensForm<SalaryForm>();
        }

        private void barBtnControlSystem_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpensForm<ControlSystemForm>();
        }

        private void barBtnProgramEvent_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpensForm<ProgramEventForm2>();
        }

        private void barBtnVacation_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpensForm<VacationForm>();
        }

        private void barBtnSalaryStaff_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpensForm<SalaryStaffForm>();
        }

        private void barBtnTelegramChat_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpensForm<TelegramForm>(true);
        }

        private void barBtnTGLog_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpensForm<LoggerForm>(true);
        }

        private void barBtnDigitalSignature_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpensForm<DigitalSignatureForm>(true);            
        }
        
        private void barButtonItemRouteSheetForm_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpensForm<RouteSheetForm>();
        }

        private void barButtonItemTaskCourierForm_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpensForm<TaskCourierForm>();
        }
        
        private void barButtonItemTaskRouteSheetv2Form_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpensForm<TaskRouteSheetv2Form>(formName: nameof(TaskCourierForm));
        }

        private void barButtonItemRouteSheetv2Form_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpensForm<RouteSheetv2Form>(formName: nameof(RouteSheetForm));
        }

        private async void barBtnUpdateStatusFromDadataRu_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show($"Данные о статусе организации будут получены с платформы dadata.ru{Environment.NewLine}{Environment.NewLine}" +
                            $"Продолжить?",
                        "Обновление статусов организации",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question) != DialogResult.OK)
            {
                return;
            }

            using (var uof = new UnitOfWork())
            {
                var customers = await new XPQuery<Customer>(uof)?.ToListAsync();

                Progress_Start(0, customers?.Count ?? 0, "Обновление статусов организаций клиентов ");

                var i = 0;
                foreach (var customer in customers)
                {
                    try
                    {
                        i++;
                        Progress_Position(i);

                        if (string.IsNullOrWhiteSpace(customer.INN))
                        {
                            continue;
                        }

                        var getInfoFromDaData = new GetInfoOrganizationFromDaData("a8cfa20b99fb660c53b8e0fa2d3de0f2204fb580", "080aefa3543cb56dfe122f26a16a04703cacb128", customer.INN);
                        await getInfoFromDaData.GetDataAsync();

                        //item.OrganizationStatus = (StatusOrganization)getInfoFromDaData.OrganizationStatus;
                        //item.Save();
                        var status = getInfoFromDaData.OrganizationStatus.ToString();
                        if (customer.StatusOrganizationUpdate(status, "dadata.ru", await uof.GetObjectByKeyAsync<User>(DatabaseConnection.User?.Oid)))
                        {
                            await uof.CommitTransactionAsync().ConfigureAwait(false);
                        }
                    }
                    catch (Exception ex)
                    {
                        LoggerController.WriteLog(ex?.ToString());
                    }
                }
            }

            Progress_Stop();
        }


        private async void barBtnUpdateStatusFromFTS_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show($"Данные о статусе организации будут получены с платформы ФНС{Environment.NewLine}{Environment.NewLine}" +
                            $"Продолжить?",
                        "Обновление статусов организации",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question) != DialogResult.OK)
            {
                return;
            }

            using (var uof = new UnitOfWork())
            {
                var customers = await new XPQuery<Customer>(uof)?.ToListAsync();

                Progress_Start(0, customers?.Count ?? 0, "Обновление статусов организаций клиентов ");

                var i = 0;
                foreach (var customer in customers)
                {
                    try
                    {
                        i++;
                        Progress_Position(i);

                        if (string.IsNullOrWhiteSpace(customer.INN))
                        {
                            continue;
                        }

                        var getInfoFromDaData = new GetInfoOrganizationFromDaData("a8cfa20b99fb660c53b8e0fa2d3de0f2204fb580", "080aefa3543cb56dfe122f26a16a04703cacb128", customer.INN);
                        await getInfoFromDaData.GetDataAsync();

                        //item.OrganizationStatus = (StatusOrganization)getInfoFromDaData.OrganizationStatus;
                        //item.Save();
                        var status = getInfoFromDaData.OrganizationStatus.ToString();
                        if (customer.StatusOrganizationUpdate(status, "dadata.ru", await uof.GetObjectByKeyAsync<User>(DatabaseConnection.User?.Oid)))
                        {
                            await uof.CommitTransactionAsync().ConfigureAwait(false);
                        }
                    }
                    catch (Exception ex)
                    {
                        LoggerController.WriteLog(ex?.ToString());
                    }
                }
            }

            Progress_Stop();
        }

        private async void barBtnUpdateStatusFromSbusRu_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show($"Данные о статусе организации будут получены с платформы sbis.ru{Environment.NewLine}{Environment.NewLine}" +
                                        $"Задержка перед запросами - 7 секунд{Environment.NewLine}{Environment.NewLine}" +
                                        $"Продолжить?",
                                    "Обновление статусов организации",
                                    MessageBoxButtons.OKCancel,
                                    MessageBoxIcon.Question) != DialogResult.OK)
            {
                return;
            }

            using (var uof = new UnitOfWork())
            {
                var customers = await new XPQuery<Customer>(uof)?.ToListAsync();

                Progress_Start(0, customers?.Count ?? 0, "Обновление статусов организаций клиентов ");

                var i = 0;
                foreach (var customer in customers)
                {
                    try
                    {
                        i++;
                        Progress_Position(i);
                        var status = await Parser.Core.Models.SbisRu.Controller.MainObjectController.GetStatusByWebHtmlAsync(customer.INN);
                        if (customer.StatusOrganizationUpdate(status, "sbis.ru", await uof.GetObjectByKeyAsync<User>(DatabaseConnection.User?.Oid)))
                        {
                            await uof.CommitTransactionAsync().ConfigureAwait(false);
                        }
                    }
                    catch (Exception ex)
                    {
                        LoggerController.WriteLog(ex?.ToString());
                    }

                    await System.Threading.Tasks.Task.Delay(7000);
                }
            }

            Progress_Stop();
        }

        private void barBtnPackagesDocument_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpensForm<PackageDocumentForm>(isForcedOpening: true);
        }

        private void barBtnCustomerStaff_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpensForm<CustomerStaffForm>(isForcedOpening: true);
        }

        private void barBtnReportV2_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpensForm<ReportV2Form>(isForcedOpening: true);
        }

        private void barBtnReportSBIS_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpensForm<ReportSBISForm>(true);
        }

        private void barBtnOpenMailClientForm_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new xUI.PostOffice.Forms.MailClientForm();
            this.ActivateMDIForm(form);
        }

        private void barBtnCustomerFormShow_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpensForm<RMS.UI.xUI.Customers.CustomerForm>(true, isUseSessionActivator: false);
        }

        private void barBtnTaskFormShow_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpensForm<RMS.UI.xUI.Tasks.TaskForm>(true, isUseSessionActivator: false);
        }

        private void barBtnDealFormShow_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpensForm<RMS.UI.xUI.Deals.DealForm>(true, isUseSessionActivator: false);
        }
    }
}