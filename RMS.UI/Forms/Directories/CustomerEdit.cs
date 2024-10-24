using DevExpress.Data.Filtering;
using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using PulsLibrary.Extensions.DevForm;
using RMS.Core.Controller;
using RMS.Core.Controllers.Letters;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model;
using RMS.Core.Model.Chronicle;
using RMS.Core.Model.InfoContract;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.OKVED;
using RMS.Core.Model.Reports;
using RMS.Core.Model.Taxes;
using RMS.UI.Forms.ReferenceBooks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Telegram.Bot;
using TelegramBotRMS.Core.Models;

namespace RMS.UI.Forms.Directories
{
    public partial class CustomerEdit : formEdit_BaseSpr
    {
        private Session Session { get; }
        public Customer Customer { get; }

        public CustomerEdit()
        {
            InitializeComponent();

            BVVGlobal.oFuncXpo.SetClipboardGridView(ref gridViewAddress);
            

            foreach (StatusOrganization item in Enum.GetValues(typeof(StatusOrganization)))
            {
                cmbOrganizationStatus.Properties.Items.Add(item.GetEnumDescription());
            }
            cmbOrganizationStatus.SelectedIndex = 0;          

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                Customer = new Customer(Session);
            }
        }

        public CustomerEdit(int id) : this()
        {
            if (id > 0)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                Customer = Session.GetObjectByKey<Customer>(id);
            }
        }

        public CustomerEdit(Customer customer) : this()
        {
            Session = customer.Session;
            Customer = customer;
        }

        private string GetLongObjectFromValue(object value)
        {
            var stringValue = value?.ToString();
            if (!string.IsNullOrWhiteSpace(stringValue))
            {
                if (long.TryParse(stringValue, out long resultInn))
                {
                    return stringValue;
                }
            }
            return default;
        }

        private string customerStatusString;
        private async void btnSave_Click(object sender, EventArgs e)
        {
            var message = default(string);
            
            if (txtAccountantResponsible.EditValue != null)
            {
                if (dateAccountantResponsibleDate.EditValue == null || dateAccountantResponsibleDate.DateTime == DateTime.MinValue)
                {
                    XtraMessageBox.Show("Запись ответственного бухгалтера без даты не возможна.", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    xtraTabControlCustomer.SelectedTabPage = xtraTabPageResponsible;
                    dateAccountantResponsibleDate.Focus();
                    return;
                }
            }

            if (!checkEditBankResponsible.Checked)
            {
                if (txtBankResponsible.EditValue == null)
                {
                    XtraMessageBox.Show("Запись ответственного за банк не может быть пустой, при условии что он сам не ответственный", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    xtraTabControlCustomer.SelectedTabPage = xtraTabPageResponsible;
                    txtBankResponsible.Focus();
                    return;
                }
            }

            if (!checkEditPrimaryResponsible.Checked)
            {
                if (txtPrimaryResponsible.EditValue == null)
                {
                    XtraMessageBox.Show("Запись ответственного за первичку не может быть пустой, при условии что он сам не ответственный", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    xtraTabControlCustomer.SelectedTabPage = xtraTabPageResponsible;
                    txtPrimaryResponsible.Focus();
                    return;
                }
            }

            if (!checkEditSalaryResponsible.Checked)
            {
                if (txtSalaryResponsible.EditValue == null)
                {
                    XtraMessageBox.Show("Запись ответственного за ЗП не может быть пустой, при условии что он сам не ответственный", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    xtraTabControlCustomer.SelectedTabPage = xtraTabPageResponsible;
                    txtSalaryResponsible.Focus();
                    return;
                }
            }

            if (dateBankResponsibleDate.EditValue == null || dateBankResponsibleDate.DateTime == DateTime.MinValue)
            {
                XtraMessageBox.Show("Запись ответственного за банк без даты не возможна.", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                xtraTabControlCustomer.SelectedTabPage = xtraTabPageResponsible;
                dateBankResponsibleDate.Focus();
                return;
            }

            Customer.TaxType = cmbTaxType.EditValue?.ToString();
            Customer.TaxTypePercent = txtPercent.EditValue?.ToString();

            Customer.OKTMO = txtOKTMO.Text;
            Customer.OKATO = txtOKATO.Text;
            Customer.Name = txtName.Text;
            Customer.INN = GetLongObjectFromValue(txtINN.Text);
            Customer.KPP = GetLongObjectFromValue(txtKPP.Text);
            Customer.ManagementSurname = txtManagementSurname.Text;
            Customer.ManagementName = txtManagementName.Text;
            Customer.ManagementPatronymic = txtManagementPatronymic.Text;
            Customer.ServiceDetails = txtServiceDetails.Text;
            Customer.FullName = txtFullName.Text;
            Customer.AbbreviatedName = txtAbbreviatedName.Text;
            Customer.DateActuality = dateActuality.DateTime == DateTime.MinValue ? default(DateTime?) : Convert.ToDateTime(dateActuality.EditValue);
            Customer.DateLiquidation = dateLiquidation.DateTime == DateTime.MinValue ? default(DateTime?) : Convert.ToDateTime(dateLiquidation.EditValue);
            Customer.DateRegistration = dateRegistration.DateTime == DateTime.MinValue ? default(DateTime?) : Convert.ToDateTime(dateRegistration.EditValue);
            Customer.DatePSRN = dateOGRN.DateTime == DateTime.MinValue ? default(DateTime?) : Convert.ToDateTime(dateOGRN.EditValue);
            Customer.OrganizationStatus = (StatusOrganization)cmbOrganizationStatus.SelectedIndex;
            Customer.OKPO = txtOKPO.Text;
            Customer.OKVED = txtOKVED.Text;
            Customer.PSRN = txtOGRN.Text;

            var kindActivity = txtKindActivity.EditValue as KindActivity;
            if (kindActivity != null)
            {
                Customer.KindActivity = kindActivity;
            }
            else
            {
                Customer.KindActivity = null;
            }

            var taxSystemCustomer = btnTaxSystemCustomer.EditValue as TaxSystemCustomer;
            if (taxSystemCustomer != null)
            {
                Customer.TaxSystemCustomer = taxSystemCustomer;
            }
            else
            {
                Customer.TaxSystemCustomer = null;
            }

            var customerStatus = txtStatus.EditValue as CustomerStatus;
            if (customerStatus != null)
            {
                Customer.CustomerStatus = customerStatus;
            }
            else
            {
                Customer.CustomerStatus = null;
            }

            if (dateManagementBirth.EditValue is DateTime managementBirth)
            {
                Customer.DateManagementBirth = managementBirth;
            }
            else
            {
                Customer.DateManagementBirth = null;
            }

            var isEditAccountantResponsible = false;

            var accountantResponsibleOld = Customer.AccountantResponsible;
            var accountantResponsible = txtAccountantResponsible.EditValue as Staff;
            if (accountantResponsible != null)
            {
                var date = dateAccountantResponsibleDate.DateTime == DateTime.MinValue ? default(DateTime?) : Convert.ToDateTime(dateAccountantResponsibleDate.EditValue);
                var dateString = Convert.ToDateTime(date).Date.ToShortDateString();
                if (Customer.AccountantResponsible != null && !Customer.AccountantResponsible.Equals(accountantResponsible))
                {
                    var description = $"Ответственный главный бухгалтер изменен с [{Customer.AccountantResponsible}] на [{accountantResponsible}] c [{dateString}]";

                    Customer.ChronicleCustomers.Add(new ChronicleCustomer(Session)
                    {
                        Act = Act.CHANGE_RESPONSIBLE_PERSON,
                        Date = DateTime.Now,
                        Description = description,
                        User = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid)
                    });

                    if (Customer.AccountantResponsible != null)
                    {
                        var groupOperator = new GroupOperator(GroupOperatorType.And);
                        var criteriaAccountantResponsible = new BinaryOperator(nameof(ReportChange.AccountantResponsible), Customer.AccountantResponsible);
                        groupOperator.Operands.Add(criteriaAccountantResponsible);
                        var criteriaCustomer = new BinaryOperator(nameof(ReportChange.Customer), Customer);
                        groupOperator.Operands.Add(criteriaCustomer);
                        var criteriaStatusReport = new BinaryOperator(nameof(ReportChange.StatusReport), StatusReport.NEW);
                        groupOperator.Operands.Add(criteriaStatusReport);

                        using (var reports = new XPCollection<ReportChange>(Session, groupOperator))
                        {
                            foreach (var report in reports)
                            {
                                report.AccountantResponsible = accountantResponsible;
                                report.Save();
                            }

                            if (reports.Count > 0)
                            {
                                description = $"Обновлено отчетов: [{reports.Count()}] (изменен ответственный сотрудник с [{Customer.AccountantResponsible}] на [{accountantResponsible}])";

                                Customer.ChronicleCustomers.Add(new ChronicleCustomer(Session)
                                {
                                    Act = Act.CHANGE_RESPONSIBLE_PERSON,
                                    Date = DateTime.Now,
                                    Description = description,
                                    User = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid)
                                });
                            }
                        }
                    }
                    isEditAccountantResponsible = true;
                }
                else if (Customer.AccountantResponsible != null && Customer.AccountantResponsibleDate != date)
                {
                    var description = $"Изменена дата ответственности главного бухгалтера с [{Convert.ToDateTime(Customer.AccountantResponsibleDate).Date.ToShortDateString()}] на [{dateString}]";

                    Customer.ChronicleCustomers.Add(new ChronicleCustomer(Session)
                    {
                        Act = Act.CHANGE_RESPONSIBLE_PERSON,
                        Date = DateTime.Now,
                        Description = description,
                        User = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid)
                    });
                }

                Customer.AccountantResponsible = accountantResponsible;
                Customer.AccountantResponsibleDate = date;
            }

            var dateBankResponsible = dateBankResponsibleDate.DateTime == DateTime.MinValue ? default(DateTime?) : Convert.ToDateTime(dateBankResponsibleDate.EditValue);
            var dateBankResponsibleString = Convert.ToDateTime(dateBankResponsible).ToShortDateString();            
            var bankResponsible = txtBankResponsible.EditValue as Staff;
            if (bankResponsible != null)
            {
                if (Customer.CustomerIsBankResponsible == checkEditBankResponsible.Checked)
                {
                    if (Customer.BankResponsible != null && !Customer.BankResponsible.Equals(bankResponsible))
                    {
                        var description = $"Ответственный за банк изменен с [{Customer.BankResponsible}] на [{bankResponsible}] c [{dateBankResponsibleString}]";

                        Customer.ChronicleCustomers.Add(new ChronicleCustomer(Session)
                        {
                            Act = Act.CHANGE_RESPONSIBLE_PERSON,
                            Date = DateTime.Now,
                            Description = description,
                            User = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid)
                        });
                    }
                    else if (Customer.BankResponsible != null && Customer.BankResponsibleDate != dateBankResponsible)
                    {
                        var description = $"Изменена дата ответственного за банк [{Customer.BankResponsible}] с [{Convert.ToDateTime(Customer.BankResponsibleDate).ToShortDateString()}] на [{dateBankResponsibleString}]";

                        Customer.ChronicleCustomers.Add(new ChronicleCustomer(Session)
                        {
                            Act = Act.CHANGE_RESPONSIBLE_PERSON,
                            Date = DateTime.Now,
                            Description = description,
                            User = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid)
                        });
                    }
                }
                else
                {
                    if (bankResponsible != null && checkEditBankResponsible.Checked == false)
                    {
                        var description = $"Ответственным за банк вместо клиента стал [{bankResponsible}] c [{dateBankResponsibleString}]";
                        Customer.ChronicleCustomers.Add(new ChronicleCustomer(Session)
                        {
                            Act = Act.CHANGE_RESPONSIBLE_PERSON,
                            Date = DateTime.Now,
                            Description = description,
                            User = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid)
                        });
                    }
                }
            }
            else
            {
                if (Customer.CustomerIsBankResponsible == true && Customer.BankResponsibleDate != null && Customer.BankResponsibleDate != dateBankResponsible)
                {
                    var description = $"Дата ответственности за банк клиента изменена с [{Convert.ToDateTime(Customer.BankResponsibleDate).Date.ToShortDateString()}] на [{dateBankResponsibleString}]";

                    Customer.ChronicleCustomers.Add(new ChronicleCustomer(Session)
                    {
                        Act = Act.CHANGE_RESPONSIBLE_PERSON,
                        Date = DateTime.Now,
                        Description = description,
                        User = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid)
                    });
                }
                else if (Customer.BankResponsible != null)
                {
                    var description = $"Ответственность за банк перешла клиенту от [{Customer.BankResponsible}] с [{dateBankResponsibleString}]";

                    Customer.ChronicleCustomers.Add(new ChronicleCustomer(Session)
                    {
                        Act = Act.CHANGE_RESPONSIBLE_PERSON,
                        Date = DateTime.Now,
                        Description = description,
                        User = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid)
                    });
                }
            }
            Customer.BankResponsible = bankResponsible;
            Customer.CustomerIsBankResponsible = checkEditBankResponsible.Checked;
            Customer.BankResponsibleDate = dateBankResponsibleDate.DateTime == DateTime.MinValue ? default(DateTime?) : Convert.ToDateTime(dateBankResponsibleDate.EditValue);
                        
            var datePrimaryResponsible = datePrimaryResponsibleDate.DateTime == DateTime.MinValue ? default(DateTime?) : Convert.ToDateTime(datePrimaryResponsibleDate.EditValue);
            var datePrimaryResponsibleString = Convert.ToDateTime(datePrimaryResponsible).ToShortDateString();
            var primaryResponsible = txtPrimaryResponsible.EditValue as Staff;
            if (primaryResponsible != null)
            {
                if (Customer.CustomerIsPrimaryResponsible == checkEditPrimaryResponsible.Checked)
                {
                    if (Customer.PrimaryResponsible != null && !Customer.PrimaryResponsible.Equals(primaryResponsible))
                    {
                        var description = $"Ответственный за первичку изменен с [{Customer.PrimaryResponsible}] на [{primaryResponsible}] c [{datePrimaryResponsibleString}]";

                        Customer.ChronicleCustomers.Add(new ChronicleCustomer(Session)
                        {
                            Act = Act.CHANGE_RESPONSIBLE_PERSON,
                            Date = DateTime.Now,
                            Description = description,
                            User = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid)
                        });
                    }
                    else if (Customer.PrimaryResponsible != null && Customer.PrimaryResponsibleDate != datePrimaryResponsible)
                    {
                        var description = $"Изменена дата ответственного за первичку [{Customer.PrimaryResponsible}] с [{Convert.ToDateTime(Customer.PrimaryResponsibleDate).ToShortDateString()}] на [{datePrimaryResponsible}]";

                        Customer.ChronicleCustomers.Add(new ChronicleCustomer(Session)
                        {
                            Act = Act.CHANGE_RESPONSIBLE_PERSON,
                            Date = DateTime.Now,
                            Description = description,
                            User = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid)
                        });
                    }
                }
                else
                {
                    if (primaryResponsible != null && checkEditPrimaryResponsible.Checked == false)
                    {
                        var description = $"Ответственным за первичку вместо клиента стал [{primaryResponsible}] c [{datePrimaryResponsibleString}]";
                        Customer.ChronicleCustomers.Add(new ChronicleCustomer(Session)
                        {
                            Act = Act.CHANGE_RESPONSIBLE_PERSON,
                            Date = DateTime.Now,
                            Description = description,
                            User = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid)
                        });
                    }
                }
            }
            else
            {
                if (Customer.CustomerIsPrimaryResponsible == true && Customer.PrimaryResponsibleDate != null && Customer.PrimaryResponsibleDate != datePrimaryResponsible)
                {
                    var description = $"Дата ответственности за первичку изменена с [{Convert.ToDateTime(Customer.PrimaryResponsibleDate).Date.ToShortDateString()}] на [{datePrimaryResponsibleString}]";

                    Customer.ChronicleCustomers.Add(new ChronicleCustomer(Session)
                    {
                        Act = Act.CHANGE_RESPONSIBLE_PERSON,
                        Date = DateTime.Now,
                        Description = description,
                        User = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid)
                    });
                }
                else if (Customer.PrimaryResponsible != null)
                {
                    var description = $"Ответственность за первичку перешла клиенту от [{Customer.PrimaryResponsible}] с [{datePrimaryResponsibleString}]";

                    Customer.ChronicleCustomers.Add(new ChronicleCustomer(Session)
                    {
                        Act = Act.CHANGE_RESPONSIBLE_PERSON,
                        Date = DateTime.Now,
                        Description = description,
                        User = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid)
                    });
                }
            }
            Customer.PrimaryResponsible = primaryResponsible;
            Customer.CustomerIsPrimaryResponsible = checkEditPrimaryResponsible.Checked;
            Customer.PrimaryResponsibleDate = datePrimaryResponsibleDate.DateTime == DateTime.MinValue ? default(DateTime?) : Convert.ToDateTime(datePrimaryResponsibleDate.EditValue);

            var dateSalaryResponsible = dateSalaryResponsibleDate.DateTime == DateTime.MinValue ? default(DateTime?) : Convert.ToDateTime(dateSalaryResponsibleDate.EditValue);
            var dateSalaryResponsibleString = Convert.ToDateTime(dateSalaryResponsible).ToShortDateString();
            var SalaryResponsible = txtSalaryResponsible.EditValue as Staff;
            if (SalaryResponsible != null)
            {
                if (Customer.CustomerIsSalaryResponsible == checkEditSalaryResponsible.Checked)
                {
                    if (Customer.SalaryResponsible != null && !Customer.SalaryResponsible.Equals(SalaryResponsible))
                    {
                        var description = $"Ответственный за ЗП изменен с [{Customer.SalaryResponsible}] на [{SalaryResponsible}] c [{dateSalaryResponsibleString}]";

                        Customer.ChronicleCustomers.Add(new ChronicleCustomer(Session)
                        {
                            Act = Act.CHANGE_RESPONSIBLE_PERSON,
                            Date = DateTime.Now,
                            Description = description,
                            User = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid)
                        });
                    }
                    else if (Customer.SalaryResponsible != null && Customer.SalaryResponsibleDate != dateSalaryResponsible)
                    {
                        var description = $"Изменена дата ответственного за ЗП [{Customer.SalaryResponsible}] с [{Convert.ToDateTime(Customer.SalaryResponsibleDate).ToShortDateString()}] на [{dateSalaryResponsible}]";

                        Customer.ChronicleCustomers.Add(new ChronicleCustomer(Session)
                        {
                            Act = Act.CHANGE_RESPONSIBLE_PERSON,
                            Date = DateTime.Now,
                            Description = description,
                            User = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid)
                        });
                    }
                }
                else
                {
                    if (SalaryResponsible != null && checkEditSalaryResponsible.Checked == false)
                    {
                        var description = $"Ответственным за ЗП вместо клиента стал [{SalaryResponsible}] c [{dateSalaryResponsibleString}]";
                        Customer.ChronicleCustomers.Add(new ChronicleCustomer(Session)
                        {
                            Act = Act.CHANGE_RESPONSIBLE_PERSON,
                            Date = DateTime.Now,
                            Description = description,
                            User = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid)
                        });
                    }
                }
            }
            else
            {
                if (Customer.CustomerIsSalaryResponsible == true && Customer.SalaryResponsibleDate != null && Customer.SalaryResponsibleDate != dateSalaryResponsible)
                {
                    var description = $"Дата ответственности за ЗП изменена с [{Convert.ToDateTime(Customer.SalaryResponsibleDate).Date.ToShortDateString()}] на [{dateSalaryResponsibleString}]";

                    Customer.ChronicleCustomers.Add(new ChronicleCustomer(Session)
                    {
                        Act = Act.CHANGE_RESPONSIBLE_PERSON,
                        Date = DateTime.Now,
                        Description = description,
                        User = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid)
                    });
                }
                else if (Customer.SalaryResponsible != null)
                {
                    var description = $"Ответственность за ЗП перешла клиенту от [{Customer.SalaryResponsible}] с [{dateSalaryResponsibleString}]";

                    Customer.ChronicleCustomers.Add(new ChronicleCustomer(Session)
                    {
                        Act = Act.CHANGE_RESPONSIBLE_PERSON,
                        Date = DateTime.Now,
                        Description = description,
                        User = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid)
                    });
                }
            }
            Customer.SalaryResponsible = SalaryResponsible;
            Customer.CustomerIsSalaryResponsible = checkEditSalaryResponsible.Checked;
            Customer.SalaryResponsibleDate = dateSalaryResponsibleDate.DateTime == DateTime.MinValue ? default(DateTime?) : Convert.ToDateTime(dateSalaryResponsibleDate.EditValue);

            var formCorporation = btnFormCorporation.EditValue as FormCorporation;
            if (formCorporation != null)
            {
                Customer.FormCorporation = formCorporation;
            }
            else
            {
                Customer.FormCorporation = null;
            }

            var managementPosition = txtManagementPosition.EditValue as Position;
            if (managementPosition != null)
            {
                Customer.ManagementPosition = managementPosition;
            }
            else
            {
                Customer.ManagementPosition = null;
            }

            var isNewCustomer = false;
            if (Customer.Oid <= 0)
            {
                isNewCustomer = true;
            }

            Customer.PercentAccountantResponsible = GetPercent(txtPercentAccountantResponsible.EditValue);
            Customer.PercentPrimaryResponsible = GetPercent(txtPercentPrimaryResponsible.EditValue);
            Customer.PercentBankResponsible = GetPercent(txtPercentBankResponsible.EditValue);
            Customer.PercentSalaryResponsible = GetPercent(txtPercentSalaryResponsible.EditValue);
            Customer.PercentAdministrator = GetPercent(txtPercentAdmin.EditValue);

            Customer.Save();

            if (Customer.ElectronicReportingCustomer is null)
            {
                Customer.ElectronicReportingCustomer = new ElectronicReportingCustomer(Session) { Customer = Customer };
                Customer.ElectronicReportingCustomer.ElectronicReportingСustomerObjects.Add(new ElectronicReportingСustomerObject(Session));
                Customer.ElectronicReportingCustomer.Save();
                Customer.Save();
            }

            id = Customer.Oid;
            flagSave = true;

            if (customerStatusString != Customer.StatusString)
            {
                message = $"<u>Статус</u> клиента изменился";

                if (!string.IsNullOrWhiteSpace(customerStatusString))
                {
                    message += $" c [{customerStatusString}]";
                }
                else
                {
                    message += $" с пустого значения";
                }

                if (!string.IsNullOrWhiteSpace(Customer.StatusString))
                {
                    message += $" на [{Customer.StatusString}]";
                }
                else
                {
                    message += $" на пустое значение";
                }
            }
            
            await SendMessageTelegram(Customer, isNewCustomer, message);
            
            if (isEditAccountantResponsible)
            {
                if (XtraMessageBox.Show($"При сохранении информации о клиенту был изменен ответственный главный бухгалтер.{Environment.NewLine}" +
                    $"Хотите перенести все задачи данного клиента на нового ответственного?",
                        "Информационное сообщение",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question) == DialogResult.OK)
                {
                    using (var uof = new UnitOfWork())
                    {
                        var accountantResponsibleDate = Customer.AccountantResponsibleDate;
                        var staffOid = Customer.AccountantResponsible?.Oid;
                        var staff = await new XPQuery<Staff>(uof).FirstOrDefaultAsync(f => f.Oid == staffOid);
                        
                        if (accountantResponsibleOld != null && staffOid > 0 && staff != null)
                        {
                            var tasks = await new XPQuery<Task>(uof)
                                .Where(w => w.Customer != null && w.Customer.Oid == Customer.Oid)
                                .Where(w => w.Staff != null && w.Staff.Oid == accountantResponsibleOld.Oid)
                                .ToListAsync();

                            //if (accountantResponsibleDate is DateTime date)
                            //{
                            //    tasks = tasks.Where(w => w.Date != null && w.Date.Value > date).ToList();
                            //}

                            tasks = tasks.Where(w => !string.IsNullOrWhiteSpace(w.StatusString) && !w.StatusString.ToLower().Contains("выполн")).ToList();

                            var count = 0;
                            foreach (var task in tasks)
                            {
                                task.Staff = staff;
                                task.Save();
                                count++;
                            }

                            if (count > 0)
                            {
                                await uof.CommitTransactionAsync();
                            }

                            XtraMessageBox.Show($"Перенесено задач на нового ответственного: {count}",
                                "Информационное сообщение",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }                            
                    }
                    
                };
            }
            
            Close();
        }

        private decimal? GetPercent(object obj)
        {
            if (decimal.TryParse(obj?.ToString()?.Replace(".", ","), out decimal result))
            {

            }

            return result;
        }


        private async System.Threading.Tasks.Task SendMessageTelegram(Customer customer, bool isNewCustomer, string message = default)
        {
            try
            {
                if (customer is null)
                {
                    return;
                }

                var isSend = false;
                var client = TelegramBot.GetTelegramBotClient(customer.Session);

                var staffs = new List<Staff>();

                if (customer.AccountantResponsible != null)
                {
                    staffs.Add(customer.AccountantResponsible);
                }
                
                if (customer.BankResponsible != null)
                {
                    staffs.Add(customer.BankResponsible);
                }

                if (customer.PrimaryResponsible != null)
                {
                    staffs.Add(customer.PrimaryResponsible);
                }

                if (customer.SalaryResponsible != null)
                {
                    staffs.Add(customer.SalaryResponsible);
                }

                staffs = staffs.Distinct().ToList();
                foreach (var staff in staffs)
                {
                    try
                    {
                        if (staff != null && staff.TelegramUserId != null)
                        {
                            staff.Reload();
                            var text = $"[OID]: {customer.Oid}{Environment.NewLine}";

                            if (isNewCustomer)
                            {
                                text += $"В базу данных СКиД добавлен новый клиент";
                                if (!string.IsNullOrWhiteSpace(customer.INN))
                                {
                                    text += $"{Environment.NewLine}<u>ИНН</u>: {customer.INN}{Environment.NewLine}";
                                }
                                else
                                {
                                    text += $":{Environment.NewLine}";
                                }

                                text += $"<u>Наименование</u>: <i>{customer}</i>{Environment.NewLine}";

                                if (string.IsNullOrWhiteSpace(message))
                                {
                                    if (!string.IsNullOrWhiteSpace(customer.StatusString))
                                    {
                                        text += $"<u>Статус</u>: {customer.StatusString}{Environment.NewLine}";
                                    }
                                }

                                isSend = true;
                            }

                            if (!string.IsNullOrWhiteSpace(message))
                            {
                                text += $"Изменения клиента: {customer}";
                                text += $"{Environment.NewLine}{message}{Environment.NewLine}";
                                
                                isSend = true;
                            }

                            var user = DatabaseConnection.User?.Staff;
                            if (user != null)
                            {
                                text += $"Сотрудник применивший изменения: <u>{user}</u>{Environment.NewLine}";
                            }

                            if (isSend)
                            {
                                await client.SendTextMessageAsync(staff.TelegramUserId, text, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                    }
                }
                
            }
            catch (Exception ex)
            {
                Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        public static void CloseButtons(ButtonEdit buttonEdit, bool isEnable)
        {
            try
            {
                foreach (EditorButton btn in buttonEdit.Properties.Buttons)
                {
                    btn.Enabled = isEnable;
                }
            }
            catch (Exception ex)
            {
                Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private bool isEditCustomersForm = false;
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
                            xtraTabPageContact.PageVisible = accessRights.IsViewCustomersFormPageContact;
                            btnBillingInformation.Visible = accessRights.IsShowCustomersFormBillingInformation;
                            
                            isEditCustomersForm = accessRights.IsEditCustomersForm;
                        }
                        else
                        {
                            xtraTabPageContact.PageVisible = false;
                        }
                        
                        btnSave.Enabled = isEditCustomersForm;

                        CloseButtons(btnTaxSystemCustomer, isEditCustomersForm);
                        CloseButtons(txtStatus, isEditCustomersForm);
                        CloseButtons(btnFormCorporation, isEditCustomersForm);
                        CloseButtons(txtManagementPosition, isEditCustomersForm);
                        CloseButtons(txtKindActivity, isEditCustomersForm);

                        CloseButtons(txtAccountantResponsible, isEditCustomersForm);
                        CloseButtons(txtBankResponsible, isEditCustomersForm);
                        CloseButtons(txtPrimaryResponsible, isEditCustomersForm);
                        CloseButtons(txtSalaryResponsible, isEditCustomersForm);
                    }
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async void CustomerEdit_Load(object sender, EventArgs e)
        {
            await SetAccessRights();

            BVVGlobal.oFuncXpo.ButtoneEditDoubleClick<Staff>(Session, txtBankResponsible, cls_App.ReferenceBooks.Staff, isEnable: isEditCustomersForm);
            BVVGlobal.oFuncXpo.ButtoneEditDoubleClick<Staff>(Session, txtPrimaryResponsible, cls_App.ReferenceBooks.Staff, isEnable: isEditCustomersForm);
            BVVGlobal.oFuncXpo.ButtoneEditDoubleClick<Staff>(Session, txtSalaryResponsible, cls_App.ReferenceBooks.Staff, isEnable: isEditCustomersForm);
            BVVGlobal.oFuncXpo.ButtoneEditDoubleClick<Staff>(Session, txtAccountantResponsible, cls_App.ReferenceBooks.Staff, isEnable: isEditCustomersForm);

            cmbTaxType.EditValue = Customer.TaxType;
            txtPercent.EditValue = Customer.TaxTypePercent;

            txtName.Text = Customer.Name;
            txtKindActivity.EditValue = Customer.KindActivity;
            txtINN.Text = Customer.INN;
            txtKPP.Text = Customer.KPP;
            txtOKTMO.Text = Customer.OKTMO;
            txtOKATO.Text = Customer.OKATO;
            txtManagementSurname.Text = Customer.ManagementSurname;
            txtManagementName.Text = Customer.ManagementName;
            txtManagementPatronymic.Text = Customer.ManagementPatronymic;
            txtServiceDetails.Text = Customer.ServiceDetails;
            txtFullName.Text = Customer.FullName;
            txtAbbreviatedName.Text = Customer.AbbreviatedName;
            btnTaxSystemCustomer.EditValue = Customer.TaxSystemCustomer;
            txtStatus.EditValue = Customer.CustomerStatus;
            txtAccountantResponsible.EditValue = Customer.AccountantResponsible;

            checkEditBankResponsible.Checked = Customer.CustomerIsBankResponsible;
            txtBankResponsible.EditValue = Customer.BankResponsible;

            checkEditPrimaryResponsible.Checked = Customer.CustomerIsPrimaryResponsible;
            txtPrimaryResponsible.EditValue = Customer.PrimaryResponsible;
            
            checkEditSalaryResponsible.Checked = Customer.CustomerIsSalaryResponsible;
            txtSalaryResponsible.EditValue = Customer.SalaryResponsible;
            if (Customer.SalaryResponsible is null)
            {
                checkEditSalaryResponsible.Checked = true;
            }

            btnFormCorporation.EditValue = Customer.FormCorporation;
            btnFormCorporation.ToolTip = Customer.FormCorporation?.FullName;

            if (Customer.FormCorporation != null)
            {
                txtKodOPF.Text = Customer.FormCorporation.Kod;
                memoFullNameOPF.Text = Customer.FormCorporation.FullName;
            }

            dateManagementBirth.EditValue = Customer.DateManagementBirth;
            txtManagementPosition.EditValue = Customer.ManagementPosition;
            dateActuality.EditValue = Customer.DateActuality ?? DateTime.Now.Date;
            dateLiquidation.EditValue = Customer.DateLiquidation;
            dateRegistration.EditValue = Customer.DateRegistration;
            dateOGRN.EditValue = Customer.DatePSRN;

            dateAccountantResponsibleDate.EditValue = Customer.AccountantResponsibleDate;
            datePrimaryResponsibleDate.EditValue = Customer.PrimaryResponsibleDate ?? DateTime.Now.Date;
            dateBankResponsibleDate.EditValue = Customer.BankResponsibleDate ?? DateTime.Now.Date;
            dateSalaryResponsibleDate.EditValue = Customer.SalaryResponsibleDate ?? DateTime.Now.Date;

            txtPercentAccountantResponsible.EditValue = Customer.PercentAccountantResponsible;
            txtPercentPrimaryResponsible.EditValue = Customer.PercentPrimaryResponsible;
            txtPercentBankResponsible.EditValue = Customer.PercentBankResponsible;
            txtPercentSalaryResponsible.EditValue = Customer.PercentSalaryResponsible;
            txtPercentAdmin.EditValue = Customer.PercentAdministrator;

            cmbOrganizationStatus.SelectedIndex = Convert.ToInt32(Customer.OrganizationStatus);

            txtOKPO.Text = Customer.OKPO;
            txtOKVED.Text = Customer.OKVED;
            txtOGRN.Text = Customer.PSRN;

            gridControlAddress.DataSource = Customer.CustomerAddress;
            if (gridViewAddress.Columns[nameof(CustomerAddress.Oid)] != null)
            {
                gridViewAddress.Columns[nameof(CustomerAddress.Oid)].Visible = false;
                gridViewAddress.Columns[nameof(CustomerAddress.Oid)].Width = 18;
                gridViewAddress.Columns[nameof(CustomerAddress.Oid)].OptionsColumn.FixedWidth = true;
            }

            Customer.CustomerTelephones.DisplayableProperties = $"{nameof(CustomerTelephone.Telephone)};" +
                $"{nameof(CustomerTelephone.FullName)};" +
                $"{nameof(CustomerTelephone.Comment)}";
            gridControlContactsTelephone.DataSource = Customer.CustomerTelephones;
            gridViewContactsTelephone.ColumnSetup(nameof(CustomerTelephone.Oid), caption: "OID", width: 30, isFixedWidth: true, isVisible: false);
            gridViewContactsTelephone.GridViewSetup(isShowFooter: false);


            Customer.CustomerEmails.DisplayableProperties = $"{nameof(CustomerEmail.IsAuthorizationTelegram)};" +
                $"{nameof(CustomerEmail.Email)};" +
                $"{nameof(CustomerEmail.FullName)};" +
                $"{nameof(CustomerEmail.Comment)};" +
                $"{nameof(CustomerEmail.UsersTG)}";
            gridControlContactsEmail.DataSource = Customer.CustomerEmails;
            gridViewContactsEmail.ColumnSetup(nameof(CustomerEmail.Oid), caption: "OID", width: 30, isFixedWidth: true, isVisible: false);
            gridViewContactsEmail.ColumnSetup(nameof(CustomerEmail.IsAuthorizationTelegram), caption: "...", width: 30, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
            if (gridViewContactsEmail.Columns[nameof(CustomerEmail.IsAuthorizationTelegram)] is GridColumn columnIsAuthorizationTelegram)
            {
                var repositoryItemImageComboBox = gridControlContactsEmail.RepositoryItems.Add(nameof(ImageComboBoxEdit)) as RepositoryItemImageComboBox;
                repositoryItemImageComboBox.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                repositoryItemImageComboBox.Appearance.TextOptions.VAlignment = VertAlignment.Center;
                repositoryItemImageComboBox.GlyphAlignment = HorzAlignment.Center;

                var imageCollection = new ImageCollection();
                imageCollection.AddImage(Properties.Resources.TelegramIcon16x16);
                repositoryItemImageComboBox.SmallImages = imageCollection;

                repositoryItemImageComboBox.Items.Add(new ImageComboBoxItem() { Value = true, ImageIndex = 0 });
                repositoryItemImageComboBox.Items.Add(new ImageComboBoxItem() { Value = false, ImageIndex = -1 });

                columnIsAuthorizationTelegram.ColumnEdit = repositoryItemImageComboBox;
            }
            if (gridViewContactsEmail.Columns[nameof(CustomerEmail.UsersTG)] is GridColumn columnUsersTG)
            {
                var repositoryItemMemoEdit = gridControlContactsEmail.RepositoryItems.Add(nameof(MemoEdit)) as RepositoryItemMemoEdit;
                repositoryItemMemoEdit.WordWrap = true;

                columnUsersTG.ColumnEdit = repositoryItemMemoEdit;
            }

            gridViewContactsEmail.GridViewSetup(isShowFooter: false);

            gridControlAccount.DataSource = Customer.Accounts;
            gridControlAccount.GridControlSetup();
            gridViewAccount.GridViewSetup(isColumnAutoWidth: false);
            if (gridViewAccount.Columns[nameof(Account.Oid)] != null)
            {
                gridViewAccount.Columns[nameof(Account.Oid)].Visible = false;
                gridViewAccount.Columns[nameof(Account.Oid)].Width = 18;
                gridViewAccount.Columns[nameof(Account.Oid)].OptionsColumn.FixedWidth = true;
            }

            gridControlContract.GridControlSetup();
            gridControlContract.DataSource = Customer.Contracts;
            foreach (GridColumn column in gridViewContract.Columns)
            {
                column.Visible = false;
            }
            gridViewContract.ColumnSetup(nameof(Contract.PlateTemplateString), caption: "Шаблон печати", width: 200, isFixedWidth: true);
            gridViewContract.ColumnSetup(nameof(Contract.Comment), caption: "Комментарий", width: 250, isFixedWidth: true);
            gridViewContract.ColumnSetup(nameof(Contract.Town), caption: "Город", width: 150, isFixedWidth: true);
            gridViewContract.ColumnSetup(nameof(Contract.DateTermination), caption: "Дата\nрасторжения", width: 125, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
            gridViewContract.ColumnSetup(nameof(Contract.DateTo), caption: "Дата\nокончания", width: 125, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
            gridViewContract.ColumnSetup(nameof(Contract.DateSince), caption: "Дата\nначала", width: 125, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
            gridViewContract.ColumnSetup(nameof(Contract.Date), caption: "Дата\nзаключения", width: 125, isFixedWidth: true, horzAlignment: HorzAlignment.Center);
            gridViewContract.ColumnSetup(nameof(Contract.NumberString), caption: "Номер\nдоговора", width: 175, isFixedWidth: true);
            gridViewContract.ColumnSetup(nameof(Contract.OrganizationString), caption: "Организация", width: 200, isFixedWidth: true);
            gridViewContract.GridViewSetup(isColumnAutoWidth: false, isShowFooter: false);

            gridViewAddress.BestFitColumns();
            gridViewContactsTelephone.BestFitColumns();
            gridViewAccount.BestFitColumns();

            customerStatusString = Customer?.StatusString;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        private void btnTaxSystemCustomer_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            var form = new TaxSystemCustomerEdit(Customer);
            form.ShowDialog();
            buttonEdit.EditValue = Customer.TaxSystemCustomer;
        }

        private void txtManagementPosition_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<Position>(Session, buttonEdit, (int)cls_App.ReferenceBooks.Position, 1, null, null, false, null, string.Empty, false, true);
        }

        private void btnFormCorporation_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                buttonEdit.ToolTip = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<FormCorporation>(Session, buttonEdit, (int)cls_App.ReferenceBooks.FormCorporation, 1, null, null, false, null, string.Empty, false, true);

            var formCorporation = ((ButtonEdit)sender).EditValue as FormCorporation;

            if (formCorporation != null)
            {
                txtKodOPF.Text = formCorporation.Kod;
                memoFullNameOPF.Text = formCorporation.FullName;
                buttonEdit.ToolTip = formCorporation.FullName;
            }
        }

        private void txtKindActivity_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            KindActivityEdit kindActivityEdit = default;
            if (buttonEdit.EditValue is KindActivity kindActivity)
            {
                kindActivityEdit = new KindActivityEdit(kindActivity);
            }
            else
            {
                kindActivityEdit = new KindActivityEdit(Customer);
            }
            kindActivityEdit.ShowDialog();

            if (kindActivityEdit.FlagSave)
            {
                buttonEdit.EditValue = kindActivityEdit.KindActivity;
            }
        }

        private void checkEditBankResponsible_CheckedChanged(object sender, EventArgs e)
        {
            var checkEdit = sender as CheckEdit;

            if (checkEdit.Checked)
            {
                txtBankResponsible.Enabled = false;
                txtBankResponsible.EditValue = null;
            }
            else
            {
                txtBankResponsible.Enabled = true;
            }
        }
        
        private async void txtINN_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }
            else if (e.Button.Kind == ButtonPredefines.Search)
            {
                if (XtraMessageBox.Show("Если вы нажмете ОК, будет обновлена вся доступная информация по организации. Продолжить?",
                "Запрос на обновление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    var getInfoFromDaData = new GetInfoOrganizationFromDaData(
                    "a8cfa20b99fb660c53b8e0fa2d3de0f2204fb580", "080aefa3543cb56dfe122f26a16a04703cacb128", GetLongObjectFromValue(txtINN.Text));
                    await getInfoFromDaData.GetDataAsync();

                    if (string.IsNullOrWhiteSpace(txtName.Text))
                    {
                        txtName.Text = getInfoFromDaData.Name;
                    }
                    
                    txtFullName.Text = getInfoFromDaData.FullName ?? txtFullName.Text;
                    txtAbbreviatedName.Text = getInfoFromDaData.AbbreviatedName ?? txtAbbreviatedName.Text;

                    txtOKPO.Text = getInfoFromDaData.OKPO ?? txtOKPO.Text;
                    dateOGRN.EditValue = getInfoFromDaData.DateOGRN ?? dateOGRN.EditValue;
                    txtOGRN.Text = getInfoFromDaData.OGRN ?? txtOGRN.Text;
                    txtKPP.Text = GetLongObjectFromValue(getInfoFromDaData.KPP) ?? GetLongObjectFromValue(txtKPP.Text);
                    txtManagementSurname.Text = getInfoFromDaData.ManagementSurname ?? txtManagementSurname.Text;
                    txtManagementName.Text = getInfoFromDaData.ManagementName ?? txtManagementName.Text;
                    txtManagementPatronymic.Text = getInfoFromDaData.ManagementPatronymic ?? txtManagementPatronymic.Text;

                    if (!string.IsNullOrWhiteSpace(getInfoFromDaData.ManagementPosition))
                    {
                        var position = Session.FindObject<Position>(new BinaryOperator(nameof(Position.Name), getInfoFromDaData.ManagementPosition));
                        if (position is null)
                        {
                            position = new Position(Session)
                            {
                                Name = getInfoFromDaData.ManagementPosition,
                                Description = getInfoFromDaData.ManagementPosition
                            };
                            position.Save();
                        }
                        txtManagementPosition.EditValue = position;
                    }

                    txtOKVED.Text = getInfoFromDaData.OKVED ?? txtOKVED.Text;

                    if (!string.IsNullOrWhiteSpace(getInfoFromDaData.Address))
                    {
                        if (Customer.CustomerAddress.FirstOrDefault(f => f.IsLegal) == null)
                        {
                            var suggestResponse = GetInfoAddressFromDaData.UpdateFromDaData("a8cfa20b99fb660c53b8e0fa2d3de0f2204fb580", getInfoFromDaData.Address);
                            var addressString = suggestResponse?.suggestions[0]?.unrestricted_value;

                            if (!string.IsNullOrWhiteSpace(addressString))
                            {
                                var customerAddress = new CustomerAddress(Session)
                                {
                                    IsLegal = true,
                                    AddressString = addressString
                                };
                                Customer.CustomerAddress.Add(customerAddress);
                            }
                        }
                    }
                    
                    if (!string.IsNullOrWhiteSpace(getInfoFromDaData.AbbreviatedNameOPF))
                    {
                        var formCorporation = Session.FindObject<FormCorporation>(new BinaryOperator(nameof(FormCorporation.AbbreviatedName), getInfoFromDaData.AbbreviatedNameOPF));
                        if (formCorporation is null)
                        {
                            formCorporation = new FormCorporation(Session)
                            {
                                Kod = getInfoFromDaData.OKOPF,
                                AbbreviatedName = getInfoFromDaData.AbbreviatedNameOPF,
                                FullName = getInfoFromDaData.FullNameOPF,
                            };

                        }
                        else
                        {
                            formCorporation.Kod = getInfoFromDaData.OKOPF;
                            formCorporation.AbbreviatedName = getInfoFromDaData.AbbreviatedNameOPF;
                            formCorporation.FullName = getInfoFromDaData.FullNameOPF;
                        }
                        formCorporation.Save();
                        btnFormCorporation.EditValue = formCorporation;
                        btnFormCorporation.ToolTip = formCorporation.FullName;
                    }

                    txtKodOPF.Text = getInfoFromDaData.OKOPF;
                    memoFullNameOPF.Text = getInfoFromDaData.FullNameOPF;

                    dateActuality.EditValue = getInfoFromDaData.DateActuality;
                    dateRegistration.EditValue = getInfoFromDaData.DateRegistration;
                    dateLiquidation.EditValue = getInfoFromDaData.DateLiquidation;
                    cmbOrganizationStatus.SelectedIndex = (int)getInfoFromDaData.OrganizationStatus;

                    var classOKVED2 = default(ClassOKVED2);
                    if (!string.IsNullOrWhiteSpace(txtOKVED.Text))
                    {
                        classOKVED2 = Session.FindObject<ClassOKVED2>(new BinaryOperator(nameof(ClassOKVED2.Code), txtOKVED.Text));

                        if (classOKVED2 is null)
                        {
                            var okvedRecord = GetInfoOrganizationFromDaData.GetOkved2("a8cfa20b99fb660c53b8e0fa2d3de0f2204fb580", txtOKVED.Text);
                            if (okvedRecord != null)
                            {
                                var sectionOKVED2 = Session.FindObject<SectionOKVED2>(new BinaryOperator(nameof(SectionOKVED2.Code), okvedRecord.razdel));

                                if (sectionOKVED2 is null)
                                {
                                    sectionOKVED2 = new SectionOKVED2(Session)
                                    {
                                        Code = okvedRecord.razdel,
                                        Name = $"Раздел {okvedRecord.razdel}"
                                    };
                                    sectionOKVED2.Save();
                                }

                                classOKVED2 = sectionOKVED2.ClassesOKVED.FirstOrDefault(f => f.Code.Equals(okvedRecord.kod));
                                if (classOKVED2 is null)
                                {
                                    classOKVED2 = new ClassOKVED2(Session)
                                    {
                                        Code = okvedRecord.kod,
                                        Name = okvedRecord.name
                                    };
                                    sectionOKVED2.ClassesOKVED.Add(classOKVED2);
                                    classOKVED2.Save();
                                }
                            }
                        }

                        var kindActivity = Customer.KindActivity;
                        if (kindActivity is null)
                        {
                            kindActivity = new KindActivity(Customer.Session);
                        }
                        kindActivity.ClassOKVED2 = classOKVED2;

                        txtKindActivity.EditValue = kindActivity;
                    }

                    var description = $"Автоматическое заполнение карточки клиента по ИНН [{GetLongObjectFromValue(txtINN.Text)}]";
                    Customer.ChronicleCustomers.Add(new ChronicleCustomer(Session)
                    {
                        Act = Act.UPDATING_CUSTOMER_INFORMATION_HAND,
                        Date = DateTime.Now,
                        Description = description,
                        User = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid)
                    });
                }
            }
        }

        private void txtStatus_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (sender is ButtonEdit buttonEdit)
            {
                if (e.Button.Kind == ButtonPredefines.Delete)
                {
                    buttonEdit.EditValue = null;
                }
                else if (e.Button.Kind == ButtonPredefines.Ellipsis)
                {
                    var form = new StatusCustomerEdit(Customer);
                    form.ShowDialog();
                    buttonEdit.EditValue = Customer.CustomerStatus;
                }

                txtStatus_EditValueChanged(sender, e);
            }           
        }
        
        private void txtStatus_EditValueChanged(object sender, EventArgs e)
        {
            if (sender is ButtonEdit buttonEdit)
            {
                if (buttonEdit.EditValue is CustomerStatus customerStatus)
                {
                    if (!string.IsNullOrWhiteSpace(customerStatus.Status?.Color))
                    {
                        var color = ColorTranslator.FromHtml(customerStatus.Status.Color);

                        buttonEdit.BackColor = color;
                    }
                }
                else
                {
                    buttonEdit.BackColor = default;
                }
            }
        }

        private void checkEditPrimaryResponsible_CheckedChanged(object sender, EventArgs e)
        {
            var checkEdit = sender as CheckEdit;

            if (checkEdit.Checked)
            {
                txtPrimaryResponsible.Enabled = false;
                txtPrimaryResponsible.EditValue = null;
            }
            else
            {
                txtPrimaryResponsible.Enabled = true;
            }
        }

        private void btnBillingInformation_Click(object sender, EventArgs e)
        {
            var form = new BillingInformationEdit(Customer);
            form.ShowDialog();
        }

        private void txtName_EditValueChanged(object sender, EventArgs e)
        {
            var textEdit = sender as TextEdit;

            if (textEdit != null && Customer != null && !textEdit.Text.Equals(Customer.Name))
            {
                Customer.Name = textEdit.Text;
            }
        }

        private void OpenResponsibleForm(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;
            if (buttonEdit != null && e.Button.Kind == ButtonPredefines.Ellipsis)
            {
                cls_BaseSpr.ButtonEditButtonClickBase<Staff>(Session, buttonEdit, (int)cls_App.ReferenceBooks.Staff, 1, new NullOperator(nameof(Staff.DateDismissal)), null, false, null, string.Empty, false, true);
            }
        }

        private void OpenResponsibleForm(object sender, ButtonPressedEventArgs e, ResponsibleOption responsibleOption)
        {
            var buttonEdit = sender as ButtonEdit;
            if (buttonEdit != null && e.Button.Kind == ButtonPredefines.Ellipsis)
            {
                var form = new ResponsibleCustomerEdit(Customer, responsibleOption);
                form.ShowDialog();

                var obj = default(Staff);

                buttonEdit.Text = obj?.ToString();
                buttonEdit.EditValue = obj;
            }            
        }

        private void txtAccountantResponsible_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            OpenResponsibleForm(sender, e);
            //OpenResponsibleForm(sender, e, ResponsibleOption.AccountantResponsible);
        }

        private void txtPrimaryResponsible_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            OpenResponsibleForm(sender, e);
            //OpenResponsibleForm(sender, e, ResponsibleOption.PrimaryResponsible);
        }

        private void txtBankResponsible_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            OpenResponsibleForm(sender, e);
            //OpenResponsibleForm(sender, e, ResponsibleOption.BankResponsible);
        }
        private void txtSalaryResponsible_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            OpenResponsibleForm(sender, e);
        }

        private void dateManagementBirth_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var dateEdit = sender as DateEdit;
            if (dateEdit != null)
            {
                if (e.Button.Kind == ButtonPredefines.Delete)
                {
                    dateEdit.EditValue = null;
                    return;
                }
            }            
        }

        private void checkEditSalaryResponsible_CheckedChanged(object sender, EventArgs e)
        {
            var checkEdit = sender as CheckEdit;

            if (checkEdit.Checked)
            {
                txtSalaryResponsible.Enabled = false;
                txtSalaryResponsible.EditValue = null;
            }
            else
            {
                txtSalaryResponsible.Enabled = true;
            }
        }

        private void gridViewContactsTelephone_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (sender is GridView)
            {
                if (e.MenuType == GridMenuType.User || e.MenuType == GridMenuType.Row)
                {
                    if (e.MenuType != GridMenuType.Row)
                    {
                        barBtnTelephoneEdit.Enabled = false;
                        barBtnTelephoneDel.Enabled = false;
                    }
                    else
                    {
                        barBtnTelephoneEdit.Enabled = true;
                        barBtnTelephoneDel.Enabled = true;
                    }

                    if (isEditCustomersForm is false)
                    {
                        barBtnTelephoneAdd.Enabled = false;
                        barBtnTelephoneEdit.Enabled = false;
                        barBtnTelephoneDel.Enabled = false;
                    }

                    popupMenuTelephone.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }

        private void barBtnTelephoneAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new ContactEdit(Customer, false);
            form.ShowDialog();
        }

        private void barBtnTelephoneEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewContactsTelephone.GetRow(gridViewContactsTelephone.FocusedRowHandle) is CustomerTelephone customerTelephone)
            {
                var form = new ContactEdit(customerTelephone);
                form.ShowDialog();
            }
        }

        private void barBtnTelephoneDel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewContactsTelephone.GetRow(gridViewContactsTelephone.FocusedRowHandle) is CustomerTelephone customerTelephone)
            {
                if (XtraMessageBox.Show($"Вы действительно хотите удалить следующий объект: {customerTelephone}",
                    "Удаление объекта",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.OK)
                {
                    customerTelephone.Delete();
                }
            }
        }

        private void gridViewContactsEmail_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (sender is GridView gridView)
            {
                if (e.MenuType == GridMenuType.User || e.MenuType == GridMenuType.Row)
                {
                    if (e.MenuType != GridMenuType.Row)
                    {
                        barBtnEmailEdit.Enabled = false;
                        barBtnEmailDel.Enabled = false;
                        barBtnAuthorizationTelegram.Enabled = false;
                        barBtnAuthorizationTelegramDelete.Enabled = false;
                    }
                    else
                    {
                        barBtnEmailEdit.Enabled = true;
                        barBtnEmailDel.Enabled = true;
                        barBtnAuthorizationTelegram.Enabled = true;
                        barBtnAuthorizationTelegramDelete.Enabled = true;
                    }

                    if (isEditCustomersForm is false)
                    {
                        barBtnEmailAdd.Enabled = false;
                        barBtnEmailEdit.Enabled = false;
                        barBtnEmailDel.Enabled = false;
                        barBtnAuthorizationTelegram.Enabled = false;
                        barBtnAuthorizationTelegramDelete.Enabled = false;
                    }

                    popupMenuEmail.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }

        private void barBtnEmailAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new ContactEdit(Customer, true);
            form.ShowDialog();
        }

        private void barBtnEmailEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewContactsEmail.GetRow(gridViewContactsEmail.FocusedRowHandle) is CustomerEmail customerEmail)
            {
                var form = new ContactEdit(customerEmail);
                form.ShowDialog();
            }
        }

        private void barBtnEmailDel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewContactsEmail.GetRow(gridViewContactsEmail.FocusedRowHandle) is CustomerEmail customerEmail)
            {
                if (XtraMessageBox.Show($"Вы действительно хотите удалить следующий объект: {customerEmail}",
                   "Удаление объекта",
                   MessageBoxButtons.OKCancel,
                   MessageBoxIcon.Question) == DialogResult.OK)
                {
                    customerEmail.Delete();
                }
            }
        }

        private async void barBtnAuthorizationTelegram_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewContactsEmail.GetRow(gridViewContactsEmail.FocusedRowHandle) is CustomerEmail customerEmail)
            {                
                var email = customerEmail.Email;
                
                if (string.IsNullOrWhiteSpace(email))
                {
                    DevXtraMessageBox.ShowXtraMessageBox("У выбранного контакта не указан email.", gridViewContactsEmail.FocusedRowHandle);
                    return;
                }

                var guid = customerEmail.Guid;
                if (!string.IsNullOrWhiteSpace(guid))
                {
                    if (DevXtraMessageBox.ShowQuestionXtraMessageBox($"Контакту {customerEmail} уже было отослано письмо с приглашением.{Environment.NewLine}" +
                        $"Хотите отправить код повторно?"))
                    {
                        await SendMessageAsync(email, guid);
                        return;
                    }
                    else
                    {
                        return;
                    }
                }                

                if (DevXtraMessageBox.ShowQuestionXtraMessageBox($"Отправить письмо с авторизацией контакту [{customerEmail}]?", caption: "Регистрация пользователя"))
                {
                    if (string.IsNullOrWhiteSpace(guid))
                    {
                        guid = Guid.NewGuid().ToString();
                        customerEmail.Guid = guid;
                    }

                    await SendMessageAsync(email, guid);
                    customerEmail.Save();
                }
            }
        }

        private static async System.Threading.Tasks.Task SendMessageAsync(string email, string guid)
        {
            var message = await LettersController.CreateAuthorizationLetterAsync(guid,
                                    BVVGlobal.oApp.User,
                                    email,
                                    await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, nameof(cls_AppParam.MailboxForSending), ""));

            DevXtraMessageBox.ShowXtraMessageBox(message);
        }

        private async void barBtnAuthorizationTelegramDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewContactsEmail.GetRow(gridViewContactsEmail.FocusedRowHandle) is CustomerEmail customerEmail)
            {
                if (DevXtraMessageBox.ShowQuestionXtraMessageBox($"При деактивации, все пользователи, которые авторизовались по этому Email будут отключены от бота.{Environment.NewLine}" +
                    $"Продолжить?"))
                {
                    using (var uof = new UnitOfWork())
                    {
                        var users = await new XPQuery<CustomerTelegramUser>(uof)
                            ?.Where(w => w.Guid == customerEmail.Guid)
                            ?.ToListAsync();

                        if (users != null)
                        {
                            uof.Delete(users);
                        }

                        await uof.CommitTransactionAsync();
                    }

                    customerEmail.Guid = default;
                    customerEmail.Save();

                    customerEmail?.Reload();
                }
            }
        }

        private void cmbTaxType_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (sender is ButtonEdit buttonEdit)
            {
                if (e.Button.Kind == ButtonPredefines.Delete)
                {
                    buttonEdit.EditValue = null;
                }
            }
        }

        private void txtPercent_CustomDisplayText(object sender, CustomDisplayTextEventArgs e)
        {
            if (sender is TextEdit textEdit)
            {
                var value = e.Value?.ToString();

                if (!string.IsNullOrWhiteSpace(value) && int.TryParse(value, out int result))
                {
                    e.DisplayText = $"{result} %";
                }
                else
                {
                    textEdit.EditValue = null;
                }
            }
        }

        private void gridViewContract_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (sender is GridView)
            {
                if (e.MenuType == GridMenuType.User || e.MenuType == GridMenuType.Row)
                {
                    if (e.MenuType != GridMenuType.Row)
                    {
                        barBtnContractEdit.Enabled = false;
                        barBtnContractDel.Enabled = false;
                    }
                    else
                    {
                        barBtnContractEdit.Enabled = true;
                        barBtnContractDel.Enabled = true;
                    }

                    if (isEditCustomersForm is false)
                    {
                        barBtnContractAdd.Enabled = false;
                        barBtnContractEdit.Enabled = false;
                        barBtnContractDel.Enabled = false;
                    }

                    popupMenuContract.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }

        private void barBtnContractAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            Customer.Name = txtName.Text;
            Customer.ManagementSurname = txtManagementSurname.Text;
            Customer.ManagementName = txtManagementName.Text;
            Customer.ManagementPatronymic = txtManagementPatronymic.Text;
            Customer.FullName = txtFullName.Text;

            var form = new ContractEdit(Customer);
            form.ShowDialog();

            if (form.Contract.Oid != -1)
            {
                Customer.Contracts.Add(form.Contract);
            }
        }

        private void barBtnContractEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewContract.GetRow(gridViewContract.FocusedRowHandle) is Contract contract)
            {
                var form = new ContractEdit(contract);
                form.ShowDialog();
                Customer.Contracts.Add(form.Contract);
            }
        }

        private void barBtnContractDel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewContract.GetRow(gridViewContract.FocusedRowHandle) is Contract contract)
            {
                if (XtraMessageBox.Show($"Вы действительно хотите удалить договор: {contract}",
                    "Информационное сообщение",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.OK)
                {
                    contract.Delete();
                }
            }
        }

        private void gridViewAccount_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (sender is GridView)
            {
                if (e.MenuType == GridMenuType.User || e.MenuType == GridMenuType.Row)
                {
                    if (e.MenuType != GridMenuType.Row)
                    {
                        barBtnAccountEdit.Enabled = false;
                        barBtnAccountDel.Enabled = false;
                    }
                    else
                    {
                        barBtnAccountEdit.Enabled = true;
                        barBtnAccountDel.Enabled = true;
                    }

                    if (isEditCustomersForm is false)
                    {
                        barBtnAccountAdd.Enabled = false;
                        barBtnAccountEdit.Enabled = false;
                        barBtnAccountDel.Enabled = false;
                    }

                    popupMenuAccount.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }

        private void barBtnAccountAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new AccountEdit(Customer);
            form.ShowDialog();
        }

        private void barBtnAccountEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewAccount.GetRow(gridViewAccount.FocusedRowHandle) is Account account)
            {
                var form = new AccountEdit(account);
                form.ShowDialog();
            }
        }

        private void barBtnAccountDel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewAccount.GetRow(gridViewAccount.FocusedRowHandle) is Account account)
            {
                if (XtraMessageBox.Show($"Вы действительно хотите удалить выбранный счет?",
                    "Информационное сообщение",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.OK)
                {
                    account.Delete();
                }
            }
        }

        private void gridViewAddress_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (sender is GridView)
            {
                if (e.MenuType == GridMenuType.User || e.MenuType == GridMenuType.Row)
                {
                    if (e.MenuType != GridMenuType.Row)
                    {
                        barBtnAddressEdit.Enabled = false;
                        barBtnAddressDel.Enabled = false;
                    }
                    else
                    {
                        barBtnAddressEdit.Enabled = true;
                        barBtnAddressDel.Enabled = true;
                    }

                    if (isEditCustomersForm is false)
                    {
                        barBtnAddressAdd.Enabled = false;
                        barBtnAddressEdit.Enabled = false;
                        barBtnAddressDel.Enabled = false;
                    }

                    popupMenuAddress.ShowPopup(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }

        private void barBtnAddressAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new AddressFIASEdit(Customer);
            form.ShowDialog();
        }

        private void barBtnAddressEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewAddress.GetRow(gridViewAddress.FocusedRowHandle) is CustomerAddress customerAddress)
            {
                var form = new AddressFIASEdit(customerAddress);
                form.ShowDialog();
            }
        }

        private void barBtnAddressDel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridViewAddress.GetRow(gridViewAddress.FocusedRowHandle) is CustomerAddress customerAddress)
            {
                if (XtraMessageBox.Show($"Вы действительно хотите удалить выбранный адрес?",
                    "Информационное сообщение",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.OK)
                {
                    customerAddress.Delete();
                }
            }
        }
    }
}