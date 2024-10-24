using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout.Utils;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model;
using RMS.Core.Model.Chronicle.Taxes;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Taxes;
using RMS.UI.Forms.Directories;
using RMS.UI.Forms.Taxes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace RMS.UI.Forms.ReferenceBooks
{
    public partial class IndividualEntrepreneursTaxEdit : formEdit_BaseSpr
    {
        private Session Session { get; }
        private IndividualEntrepreneursTax IndividualEntrepreneursTax { get; }
        private PatentObject PatentObject { get; set; }
        private bool IsAddPatentObject { get; set; }
        public List<IndividualEntrepreneursTax> IndividualEntrepreneursTaxs { get; set; }
        public bool IsMassChange { get; set; }

        public IndividualEntrepreneursTaxEdit(Session session)
        {
            InitializeComponent();

            foreach (PeriodReportChange item in Enum.GetValues(typeof(PeriodReportChange)))
            {
                if (item != PeriodReportChange.MONTH &&
                    item != PeriodReportChange.YEAR &&
                    item != PeriodReportChange.FIRSTHALFYEAR &&
                    item != PeriodReportChange.SECONDHALFYEAR)
                {
                    cmbPeriodReportChange.Properties.Items.Add(item.GetEnumDescription());
                }
            }

            Session = session;
            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();                
            }

            if (IndividualEntrepreneursTax is null)
            {
                IndividualEntrepreneursTax = new IndividualEntrepreneursTax(Session)
                {
                    IndividualEntrepreneursTaxStatus = session.FindObject<IndividualEntrepreneursTaxStatus>(new BinaryOperator(nameof(IndividualEntrepreneursTaxStatus.IsDefault), true))
                };                
            }            
        }
        
        public IndividualEntrepreneursTaxEdit(Session session, bool isAddPatentObject) : this(session)
        {
            IsAddPatentObject = isAddPatentObject;
        }
        
        public IndividualEntrepreneursTaxEdit(Session session, List<IndividualEntrepreneursTax> individualEntrepreneursTaxs, bool isAddPatentObject) : this(session)
        {
            IndividualEntrepreneursTaxs = individualEntrepreneursTaxs;
            IsAddPatentObject = isAddPatentObject;
            IsMassChange = true;
            btnSave.Text = "Применить";
        }

        public IndividualEntrepreneursTaxEdit(IndividualEntrepreneursTax individualEntrepreneursTax) : this(individualEntrepreneursTax.Session)
        {
            Session = individualEntrepreneursTax.Session;
            IndividualEntrepreneursTax = individualEntrepreneursTax;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SaveMassIndividualEntrepreneursTax()
        {
            var comment = $"Массовая замена [{DateTime.Now}]";

            if (PatentObject is null && IsAddPatentObject is false)
            {
                var individualEntrepreneursTaxStatus = default(IndividualEntrepreneursTaxStatus);
                if (btnStatus.EditValue is IndividualEntrepreneursTaxStatus _individualEntrepreneursTaxStatus)
                {
                    individualEntrepreneursTaxStatus = _individualEntrepreneursTaxStatus;
                }
                else
                {
                    individualEntrepreneursTaxStatus = null;
                }

                foreach (var individualEntrepreneursTax in IndividualEntrepreneursTaxs)
                {
                    individualEntrepreneursTax.IndividualEntrepreneursTaxStatus = individualEntrepreneursTaxStatus;
                    individualEntrepreneursTax.Comment = $"{comment}{Environment.NewLine}{individualEntrepreneursTax.Comment}";
                    individualEntrepreneursTax.Save();
                }
                
            }
            else
            {
                var _patentStatus = default(PatentStatus);
                var _accountingInsuranceStatus1 = default(AccountingInsuranceStatus);
                var _accountingInsuranceStatus2 = default(AccountingInsuranceStatus);

                if (btnPatentStatus.EditValue is PatentStatus patentStatus)
                {
                    _patentStatus = patentStatus;
                }
                else
                {
                    _patentStatus = null;
                }

                if (btnAccountingInsuranceStatus1.EditValue is AccountingInsuranceStatus accountingInsuranceStatus)
                {
                    _accountingInsuranceStatus1 = accountingInsuranceStatus;
                }
                else
                {
                    _accountingInsuranceStatus1 = null;
                }

                if (btnAccountingInsuranceStatus2.EditValue is AccountingInsuranceStatus accountingInsuranceStatus2)
                {
                    _accountingInsuranceStatus2 = accountingInsuranceStatus2;
                }
                else
                {
                    _accountingInsuranceStatus2 = null;
                }

                foreach (var individualEntrepreneursTax in IndividualEntrepreneursTaxs)
                {
                    var patentObj = individualEntrepreneursTax.PatentObj;
                    if (patentObj != null)
                    {
                        if (patentObj.Oid != -1 
                            && (patentObj.PatentStatus != _patentStatus 
                                || patentObj.AccountingInsuranceStatus != _accountingInsuranceStatus1
                                || patentObj.AccountingInsuranceStatus2 != _accountingInsuranceStatus2))
                        {
                            var chroniclePatent = new ChroniclePatent(Session)
                            {
                                Comment = memoComment.Text,
                                DateSince = patentObj.DateSince,
                                DateTo = patentObj.DateTo,
                                ActualPaymentDate = patentObj.ActualPaymentDate,
                                AdvancePaymentDate = patentObj.AdvancePaymentDate,
                                KindActivity = patentObj.KindActivity,
                                Name = patentObj.Name,
                                TaxAuthority = patentObj.TaxAuthority,
                                PatentStatus = patentObj.PatentStatus,
                                User = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid),
                                ClassOKVED2 = patentObj.ClassOKVED2,
                                ActualPaymentValue = patentObj.ActualPaymentValue,
                                AdvancePaymentValue = patentObj.AdvancePaymentValue,
                                IsPaymentActualPaymentValue = patentObj.IsPaymentActualPaymentValue,
                                IsPaymentAdvancePaymentValue = patentObj.IsPaymentAdvancePaymentValue,
                                AccountingInsuranceStatus = patentObj.AccountingInsuranceStatus,
                                DateAccountingInsuranceStatus = patentObj.DateAccountingInsuranceStatus,
                                AccountingInsuranceStatus2 = patentObj.AccountingInsuranceStatus2,
                                DateAccountingInsuranceStatus2 = patentObj.DateAccountingInsuranceStatus2
                            };

                            patentObj.Patent.ChroniclePatents.Add(chroniclePatent);
                            chroniclePatent.Save();
                        }

                        patentObj.PatentStatus = _patentStatus;
                        patentObj.AccountingInsuranceStatus = _accountingInsuranceStatus1;
                        patentObj.AccountingInsuranceStatus2 = _accountingInsuranceStatus2;
                        patentObj.Save();
                        
                        individualEntrepreneursTax.Comment = $"{comment}{Environment.NewLine}{individualEntrepreneursTax.Comment}"; 
                        individualEntrepreneursTax.Save();
                    }
                }                
            }
        }

        private void SaveIndividualEntrepreneursTax()
        {
            if (btnCustomer.EditValue is Customer customer)
            {
                IndividualEntrepreneursTax.Customer = customer;
            }
            else
            {
                IndividualEntrepreneursTax.Customer = null;
            }

            if (btnStaff.EditValue is Staff staff)
            {
                IndividualEntrepreneursTax.Staff = staff;
            }
            else
            {
                IndividualEntrepreneursTax.Staff = null;
            }

            var periodReportChange = default(PeriodReportChange?);
            foreach (PeriodReportChange period in Enum.GetValues(typeof(PeriodReportChange)))
            {
                if (period.GetEnumDescription().Equals(cmbPeriodReportChange.Text))
                {
                    periodReportChange = period;
                    IndividualEntrepreneursTax.PeriodReportChange = period;
                    break;
                }
            }

            if (periodReportChange is null)
            {
                throw new Exception();
            }

            IndividualEntrepreneursTax.DateCreate = dateCreate.DateTime;
            IndividualEntrepreneursTax.Comment = memoComment.Text;

            if (!string.IsNullOrWhiteSpace(txtYear.Text))
            {
                IndividualEntrepreneursTax.Year = Convert.ToInt32(txtYear.Text);
            }

            if (PatentObject is null && IsAddPatentObject is false)
            {
                if (dateDateDelivery.EditValue is DateTime dateDelivery)
                {
                    IndividualEntrepreneursTax.DateDelivery = dateDelivery;
                }
                else
                {
                    IndividualEntrepreneursTax.DateDelivery = null;
                }

                IndividualEntrepreneursTax.Name = memoName.Text;
                IndividualEntrepreneursTax.IsPaid = checkIsPaid.Checked;

                if (dateDatePaid.EditValue is DateTime datePaid)
                {
                    IndividualEntrepreneursTax.DatePaid = datePaid;
                }
                else
                {
                    IndividualEntrepreneursTax.DatePaid = null;
                }

                if (!string.IsNullOrWhiteSpace(txtSum.Text))
                {
                    IndividualEntrepreneursTax.Sum = Convert.ToDecimal(txtSum.Text);
                }

                if (btnStatus.EditValue is IndividualEntrepreneursTaxStatus individualEntrepreneursTaxStatus)
                {
                    IndividualEntrepreneursTax.IndividualEntrepreneursTaxStatus = individualEntrepreneursTaxStatus;
                }
                else
                {
                    IndividualEntrepreneursTax.IndividualEntrepreneursTaxStatus = null;
                }
            }
            else
            {
                if (PatentObject is null)
                {
                    XtraMessageBox.Show(
                        "Для сохранения необходимо указать патент.",
                        "Укажите патент",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    btnPatent.Focus();
                    return;
                }

                var advancePaymentValue = default(decimal?);
                var actualPaymentValue = default(decimal?);
                var _patentStatus = default(PatentStatus);
                var _dateSince = default(DateTime?);
                var _dateTo = default(DateTime?);
                var _actualPaymentDate = default(DateTime?);
                var _advancePaymentDate = default(DateTime?);
                var isPaymentAdvancePaymentValue = checkIsPaymentAdvancePaymentValue.Checked;
                var isPaymentActualPaymentValue = checkIsPaymentActualPaymentValue.Checked;
                var _accountingInsuranceStatus1 = default(AccountingInsuranceStatus);
                var _accountingInsuranceStatus2 = default(AccountingInsuranceStatus);
                var _dateAccountingInsuranceStatus1 = default(DateTime?);
                var _dateAccountingInsuranceStatus2 = default(DateTime?);

                if (!string.IsNullOrWhiteSpace(txtActualPaymentValue.Text))
                {
                    actualPaymentValue = Convert.ToDecimal(txtActualPaymentValue.Text);
                }
                else
                {
                    actualPaymentValue = null;
                }

                if (!string.IsNullOrWhiteSpace(txtAdvancePaymentValue.Text))
                {
                    advancePaymentValue = Convert.ToDecimal(txtAdvancePaymentValue.Text);
                }
                else
                {
                    advancePaymentValue = null;
                }

                if (dateDateSince.EditValue is DateTime dateSince)
                {
                    _dateSince = dateSince;
                }
                else
                {
                    _dateSince = null;
                }

                if (dateDateTo.EditValue is DateTime dateTo)
                {
                    _dateTo = dateTo;
                }
                else
                {
                    _dateTo = null;
                }

                if (dateActualPaymentDate.EditValue is DateTime actualPaymentDate)
                {
                    _actualPaymentDate = actualPaymentDate;
                }
                else
                {
                    _actualPaymentDate = null;
                }

                if (dateAdvancePaymentDate.EditValue is DateTime advancePaymentDate)
                {
                    _advancePaymentDate = advancePaymentDate;
                }
                else
                {
                    _advancePaymentDate = null;
                }

                if (btnPatentStatus.EditValue is PatentStatus patentStatus)
                {
                    _patentStatus = patentStatus;
                }
                else
                {
                    _patentStatus = null;
                }

                if (btnAccountingInsuranceStatus1.EditValue is AccountingInsuranceStatus accountingInsuranceStatus)
                {
                    _accountingInsuranceStatus1 = accountingInsuranceStatus;
                }
                else
                {
                    _accountingInsuranceStatus1 = null;
                }

                if (btnAccountingInsuranceStatus2.EditValue is AccountingInsuranceStatus accountingInsuranceStatus2)
                {
                    _accountingInsuranceStatus2 = accountingInsuranceStatus2;
                }
                else
                {
                    _accountingInsuranceStatus2 = null;
                }

                if (dateAccountingInsuranceStatus1.EditValue is DateTime dAccountingInsuranceStatus)
                {
                    _dateAccountingInsuranceStatus1 = dAccountingInsuranceStatus;
                }
                else
                {
                    _dateAccountingInsuranceStatus1 = null;
                }

                if (dateAccountingInsuranceStatus2.EditValue is DateTime dAccountingInsuranceStatus2)
                {
                    _dateAccountingInsuranceStatus2 = dAccountingInsuranceStatus2;
                }
                else
                {
                    _dateAccountingInsuranceStatus2 = null;
                }

                if (PatentObject.Oid != -1 &&
                (PatentObject.DateSince != _dateSince
                || PatentObject.DateTo != _dateTo
                || PatentObject.ActualPaymentDate != _actualPaymentDate
                || PatentObject.AdvancePaymentDate != _advancePaymentDate
                || PatentObject.PatentStatus != _patentStatus
                || PatentObject.ActualPaymentValue != actualPaymentValue
                || PatentObject.AdvancePaymentValue != advancePaymentValue
                || PatentObject.IsPaymentActualPaymentValue != isPaymentActualPaymentValue
                || PatentObject.IsPaymentAdvancePaymentValue != isPaymentAdvancePaymentValue
                || PatentObject.AccountingInsuranceStatus != _accountingInsuranceStatus1
                || PatentObject.DateAccountingInsuranceStatus != _dateAccountingInsuranceStatus1
                || PatentObject.AccountingInsuranceStatus2 != _accountingInsuranceStatus2
                || PatentObject.DateAccountingInsuranceStatus2 != _dateAccountingInsuranceStatus2))
                {

                    var chroniclePatent = new ChroniclePatent(Session)
                    {
                        Comment = memoComment.Text,
                        DateSince = PatentObject.DateSince,
                        DateTo = PatentObject.DateTo,
                        ActualPaymentDate = PatentObject.ActualPaymentDate,
                        AdvancePaymentDate = PatentObject.AdvancePaymentDate,
                        KindActivity = PatentObject.KindActivity,
                        Name = PatentObject.Name,
                        TaxAuthority = PatentObject.TaxAuthority,
                        PatentStatus = PatentObject.PatentStatus,
                        User = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid),
                        ClassOKVED2 = PatentObject.ClassOKVED2,
                        ActualPaymentValue = PatentObject.ActualPaymentValue,
                        AdvancePaymentValue = PatentObject.AdvancePaymentValue,
                        IsPaymentActualPaymentValue = PatentObject.IsPaymentActualPaymentValue,
                        IsPaymentAdvancePaymentValue = PatentObject.IsPaymentAdvancePaymentValue,
                        AccountingInsuranceStatus = PatentObject.AccountingInsuranceStatus,
                        DateAccountingInsuranceStatus = PatentObject.DateAccountingInsuranceStatus,
                        AccountingInsuranceStatus2 = PatentObject.AccountingInsuranceStatus2,
                        DateAccountingInsuranceStatus2 = PatentObject.DateAccountingInsuranceStatus2
                    };

                    PatentObject.Patent.ChroniclePatents.Add(chroniclePatent);
                    chroniclePatent.Save();
                }

                PatentObject.PatentStatus = _patentStatus;
                PatentObject.DateSince = _dateSince;
                PatentObject.DateTo = _dateTo;
                PatentObject.ActualPaymentDate = _actualPaymentDate;
                PatentObject.AdvancePaymentDate = _advancePaymentDate;
                PatentObject.ActualPaymentValue = actualPaymentValue;
                PatentObject.AdvancePaymentValue = advancePaymentValue;
                PatentObject.IsPaymentActualPaymentValue = isPaymentActualPaymentValue;
                PatentObject.IsPaymentAdvancePaymentValue = isPaymentAdvancePaymentValue;
                PatentObject.AccountingInsuranceStatus = _accountingInsuranceStatus1;
                PatentObject.DateAccountingInsuranceStatus = _dateAccountingInsuranceStatus1;
                PatentObject.AccountingInsuranceStatus2 = _accountingInsuranceStatus2;
                PatentObject.DateAccountingInsuranceStatus2 = _dateAccountingInsuranceStatus2;
                PatentObject.Save();


                IndividualEntrepreneursTax.PatentObj = PatentObject;
            }

            Session.Save(IndividualEntrepreneursTax);
            id = IndividualEntrepreneursTax.Oid;
            flagSave = true;
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsMassChange)
            {
                SaveMassIndividualEntrepreneursTax();
            }
            else
            {
                SaveIndividualEntrepreneursTax();
            }
            
            Close();
        }
        
        private bool isEditIndividualReportForm = false;
        private bool isEditIndividualPatentReportForm = false;
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
                            isEditIndividualReportForm = accessRights.IsEditIndividualReportForm;
                            isEditIndividualPatentReportForm = accessRights.IsEditIndividualPatentReportForm;
                        }
                    }
                }                
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            await SetAccessRights();

            IndividualEntrepreneursTax?.Reload();
            IndividualEntrepreneursTax?.PatentObj?.Reload();
            
            PatentObject = IndividualEntrepreneursTax.PatentObj;
            
            btnCustomer.EditValue = IndividualEntrepreneursTax.Customer;
            btnStaff.EditValue = IndividualEntrepreneursTax.Staff;
            txtTaxSystem.EditValue = IndividualEntrepreneursTax.Customer?.TaxSystemCustomer;
            cmbPeriodReportChange.EditValue = IndividualEntrepreneursTax.PeriodReportChange.GetEnumDescription();
            txtYear.EditValue = IndividualEntrepreneursTax.Year;
            memoComment.EditValue = IndividualEntrepreneursTax.Comment;
            dateCreate.EditValue = IndividualEntrepreneursTax.DateCreate;
            
            if (IndividualEntrepreneursTax.PatentObj is null && IsAddPatentObject is false)
            {
                btnSave.Enabled = isEditIndividualReportForm;
                CustomerEdit.CloseButtons(btnCustomer, isEditIndividualReportForm);
                CustomerEdit.CloseButtons(btnStaff, isEditIndividualReportForm);
                CustomerEdit.CloseButtons(btnStatus, isEditIndividualReportForm);

                memoName.EditValue = IndividualEntrepreneursTax.Name;
                dateDateDelivery.EditValue = IndividualEntrepreneursTax.DateDelivery;
                dateDatePaid.EditValue = IndividualEntrepreneursTax.DatePaid;
                checkIsPaid.EditValue = IndividualEntrepreneursTax.IsPaid;
                txtSum.EditValue = IndividualEntrepreneursTax.Sum;
                btnStatus.EditValue = IndividualEntrepreneursTax.IndividualEntrepreneursTaxStatus;                
                
                layoutControlGroupPatent.Visibility = LayoutVisibility.Never;
                memoName.Properties.ReadOnly = false;
            }
            else
            {
                if (IsAddPatentObject)
                {
                    layoutControlItemPatent.Visibility = LayoutVisibility.Always;
                }

                LoadPatentObject();

                btnSave.Enabled = isEditIndividualPatentReportForm;
                CustomerEdit.CloseButtons(btnCustomer, isEditIndividualPatentReportForm);
                CustomerEdit.CloseButtons(btnStaff, isEditIndividualPatentReportForm);
                CustomerEdit.CloseButtons(btnStatus, isEditIndividualPatentReportForm);
                CustomerEdit.CloseButtons(btnPatent, isEditIndividualPatentReportForm);
                CustomerEdit.CloseButtons(btnAccountingInsuranceStatus1, isEditIndividualPatentReportForm);
                CustomerEdit.CloseButtons(btnAccountingInsuranceStatus2, isEditIndividualPatentReportForm);
                CustomerEdit.CloseButtons(btnPatentStatus, isEditIndividualPatentReportForm);

                layoutControlItemPeriodReportChange.Visibility = LayoutVisibility.Never;
                layoutControlItemDateCreate.Visibility = LayoutVisibility.Never;
                layoutControlItemDateDelivery.Visibility = LayoutVisibility.Never;
                layoutControlItemStatus.Visibility = LayoutVisibility.Never;

                layoutControlItemDatePaid.Visibility = LayoutVisibility.Never;
                layoutControlItemSum.Visibility = LayoutVisibility.Never;
                layoutControlItemIsPaid.Visibility = LayoutVisibility.Never;
                memoName.Properties.ReadOnly = true;
            }
        }

        private void LoadPatentObject()
        {
            btnPatent.EditValue = PatentObject;
            memoName.EditValue = PatentObject?.Name;
            dateDateSince.EditValue = PatentObject?.DateSince;
            dateDateTo.EditValue = PatentObject?.DateTo;
            btnPatentStatus.EditValue = PatentObject?.PatentStatus;
            btnAccountingInsuranceStatus1.EditValue = PatentObject?.AccountingInsuranceStatus;
            dateAccountingInsuranceStatus1.EditValue = PatentObject?.DateAccountingInsuranceStatus;
            btnAccountingInsuranceStatus2.EditValue = PatentObject?.AccountingInsuranceStatus2;
            dateAccountingInsuranceStatus2.EditValue = PatentObject?.DateAccountingInsuranceStatus2;

            dateAdvancePaymentDate.EditValue = PatentObject?.AdvancePaymentDate;
            txtAdvancePaymentValue.EditValue = PatentObject?.AdvancePaymentValue;
            checkIsPaymentAdvancePaymentValue.EditValue = PatentObject?.IsPaymentAdvancePaymentValue;

            dateActualPaymentDate.EditValue = PatentObject?.ActualPaymentDate;
            txtActualPaymentValue.EditValue = PatentObject?.ActualPaymentValue;
            checkIsPaymentActualPaymentValue.EditValue = PatentObject?.IsPaymentActualPaymentValue;
        }

        private void btnCustomer_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<Customer>(Session, buttonEdit, (int)cls_App.ReferenceBooks.Customer, 1, null, null, false, null, string.Empty, false, true);

            if (buttonEdit.EditValue is Customer customer)
            {
                txtTaxSystem.EditValue = customer.TaxSystemCustomerString;
            }
        }

        private void btnStaff_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<Staff>(Session, buttonEdit, (int)cls_App.ReferenceBooks.Staff, 1, new NullOperator(nameof(Staff.DateDismissal)), null, false, null, string.Empty, false, true);
        }

        private void btnPatentStatus_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<PatentStatus>(Session, buttonEdit, (int)cls_App.ReferenceBooks.PatentStatus, 1, null, null, false, null, string.Empty, false, true);
        }

        private void btnAccountingInsuranceStatus_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (sender is ButtonEdit buttonEdit)
            {
                if (e.Button.Kind == ButtonPredefines.Delete)
                {
                    buttonEdit.EditValue = null;
                    return;
                }

                cls_BaseSpr.ButtonEditButtonClickBase<AccountingInsuranceStatus>(Session, buttonEdit, (int)cls_App.ReferenceBooks.AccountingInsuranceStatus, 1, null, null, false, null, string.Empty, false, true);
            }
        }
        
        private void btnPatent_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {            
            if (PatentObject != null)
            {
                var form = new PatentObjectEdit(PatentObject);
                form.ShowDialog();
            }
            else
            {
                if (btnCustomer.EditValue is Customer customer)
                {
                    customer.Tax.Reload();
                    customer.Tax?.Patent?.Reload();
                    customer.Tax?.Patent?.PatentObjects?.Reload();

                    var patent = customer.Tax?.Patent;
                    if (patent != null)
                    {
                        var form = new PatentEdit2(customer.Tax.Patent, customer, "Выбрать");
                        form.ShowDialog();

                        if (form.CurrentPatentObject != null)
                        {
                            PatentObject = form.CurrentPatentObject;
                            LoadPatentObject();
                        }
                    }
                }
                else
                {
                    XtraMessageBox.Show(
                        "Для выбора патента необходимо указать клиента",
                        "Укажите клиента",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    btnCustomer.Focus();
                }
            }
        }

        private void btnStatus_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<IndividualEntrepreneursTaxStatus>(Session, buttonEdit, (int)cls_App.ReferenceBooks.IndividualEntrepreneursTaxStatus, 1, null, null, false, null, string.Empty, false, true);
        }
    }
}