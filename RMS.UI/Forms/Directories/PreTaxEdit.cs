using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout.Utils;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Reports;
using RMS.Core.Model.Taxes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace RMS.UI.Forms.Directories
{
    public partial class PreTaxEdit : XtraForm
    {
        private Session Session { get; }
        private Customer Customer { get; }
        public PreTax PreTax { get; }
        public List<PreTax> PreTaxs { get; }

        public Dictionary<string, int> PeriodReportChangeDictionary = new Dictionary<string, int>();
        public bool IsMassChange { get; set; }
        
        public PreTaxEdit()
        {
            InitializeComponent();

            var i = 0;
            foreach (PeriodReportChange item in Enum.GetValues(typeof(PeriodReportChange)))
            {
                if (item != PeriodReportChange.MONTH && 
                    item != PeriodReportChange.FIRSTHALFYEAR && 
                    item != PeriodReportChange.SECONDHALFYEAR && 
                    item != PeriodReportChange.YEAR)
                {
                    cmbPeriodReportChange.Properties.Items.Add(item.GetEnumDescription());
                    PeriodReportChangeDictionary.Add(item.GetEnumDescription(), i);
                    i++;
                }
            }
            cmbPeriodReportChange.SelectedIndex = 0;

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                PreTax = new PreTax(Session);
            }
        }

        public PreTaxEdit(Session session, List<PreTax> preTaxs) : this()
        {
            Session = session;
            PreTaxs = preTaxs;
            IsMassChange = true;
            btnSave.Text = "Применить";
        }

        public PreTaxEdit(PreTax preTax) : this()
        {
            PreTax = preTax;
            Customer = preTax.Customer;
            Session = preTax.Session;
        }

        public PreTaxEdit(Customer customer) : this()
        {           
            Customer = customer;
            Session = customer.Session; 
            PreTax = new PreTax(Session);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsMassChange)
            {
                SaveMassPretax();
            }
            else
            {
                SavePretax();
            }
            Close();
        }
        
        private void SaveMassPretax()
        {
            var staff = default(Staff);
            var periodReportChange = default(PeriodReportChange?);
            var dateDelivery = default(DateTime?);
            var report = default(Report);
            var comment = default(string);
            var statusPreTax = default(StatusPreTax);

            if (btnStaff.EditValue is Staff _staff)
            {
                staff = _staff;
            }
            else
            {
                staff = null;
            }            

            if (cmbPeriodReportChange.SelectedIndex != -1)
            {
                foreach (PeriodReportChange item in Enum.GetValues(typeof(PeriodReportChange)))
                {
                    if (item.GetEnumDescription().Equals(cmbPeriodReportChange.EditValue))
                    {
                        periodReportChange = item;
                    }
                }
            }
            else
            {
                throw new Exception();
            }

            if (dateDeliveryEdit.EditValue is DateTime date)
            {
                dateDelivery = date;
            }
            else
            {
                dateDelivery = null;
            }

            if (btnReport.EditValue is Report _report)
            {
                report = _report;
            }
            else
            {
                report = null;
            }

            comment = $"Массовая замена [{DateTime.Now}]";
            
            if (btnStatusPreTax.EditValue is StatusPreTax _statusPreTax)
            {
                statusPreTax = _statusPreTax;
            }
            else
            {
                statusPreTax = null;
            }

            foreach (var preTax in PreTaxs)
            {
                if (staff != null)
                {
                    preTax.Staff = staff;
                }

                if (statusPreTax != null)
                {
                    preTax.StatusPreTax = statusPreTax;
                }
                
                preTax.Comment = $"{comment}{Environment.NewLine}{preTax.Comment}";
                preTax.Save();
            }
        }

        private async void SavePretax()
        {
            if (btnStaff.EditValue is Staff staff)
            {
                PreTax.Staff = staff;
            }
            else
            {
                PreTax.Staff = null;
            }

            if (btnCustomer.EditValue is Customer customer)
            {
                PreTax.Customer = customer;
            }
            else
            {
                PreTax.Staff = null;
            }

            if (cmbPeriodReportChange.SelectedIndex != -1)
            {
                foreach (PeriodReportChange item in Enum.GetValues(typeof(PeriodReportChange)))
                {
                    if (item.GetEnumDescription().Equals(cmbPeriodReportChange.EditValue))
                    {
                        PreTax.PeriodReportChange = item;
                    }
                }
            }
            else
            {
                throw new Exception();
            }

            if (dateDeliveryEdit.EditValue is DateTime date)
            {
                PreTax.DateDelivery = date;
            }
            else
            {
                PreTax.DateDelivery = null;
            }

            if (int.TryParse(txtYear.Text, out int resultYear))
            {
                PreTax.Year = resultYear;
            }
            else
            {
                throw new Exception();
            }

            if (btnReport.EditValue is Report report)
            {
                PreTax.Report = report;
            }
            else
            {
                PreTax.Report = null;
            }

            PreTax.Proceeds51 = GetDecimal(txtProceeds51.Text);
            PreTax.Proceeds90 = GetDecimal(txtProceeds90.Text);

            PreTax.HighestPercentage = GetDecimal(txtHighestPercentage.Text);
            PreTax.PreliminaryAmount = GetDecimal(txtPreliminaryAmount.Text);
            PreTax.AmountInDeclaration = GetDecimal(txtAmountInDeclaration.Text);            

            PreTax.IsUseDivisionBy3 = checkIsUseDivisionBy3.Checked;
            PreTax.IsUseAdvancesOnProfit = checkIsUseAdvancesOnProfit.Checked;

            PreTax.PaymentOne = checkPaymentOne.Checked;
            PreTax.PaymentTwo = checkPaymentTwo.Checked;
            PreTax.PaymentThree = checkPaymentThree.Checked;

            PreTax.DateOne = ReturnDateTime(dateOne.EditValue);
            PreTax.DateTwo = ReturnDateTime(dateTwo.EditValue);
            PreTax.DateThree = ReturnDateTime(dateThree.EditValue);

            PreTax.Comment = memoComment.Text;

            if (PreTax.IsAgreement && checkIsAgreement.Checked is false)
            {
                PreTax.IsAgreement = checkIsAgreement.Checked;
                PreTax.DateAgreement = null;
            }
            else
            {
                PreTax.IsAgreement = true;

                if (dateAgreementEdit.EditValue is DateTime dateAgreement)
                {
                    PreTax.DateAgreement = dateAgreement;
                }
                else
                {
                    PreTax.DateAgreement = DateTime.Now;
                }
            }

            if (btnStatusPreTax.EditValue is StatusPreTax statusPreTax)
            {
                PreTax.StatusPreTax = statusPreTax;
            }
            else
            {
                PreTax.StatusPreTax = null;
            }

            PreTax.Save();

            if (!string.IsNullOrWhiteSpace(PreTax.Guid))
            {
                var criteriaGuid = new BinaryOperator(nameof(PreTax.Guid), PreTax.Guid);

                using (var uof = new UnitOfWork())
                {
                    using (var pretaxs = new XPCollection<PreTax>(uof, criteriaGuid))
                    {                        
                        foreach (var tax in pretaxs)
                        {
                            if (tax.Oid != PreTax.Oid && (tax.ReportString.Equals("НДС") || tax.ReportString.Equals("Прибыль")))
                            {
                                if (tax.Proceeds51 is null)
                                {
                                    tax.Proceeds51 = PreTax.Proceeds51;
                                    tax.HighestPercentage = PreTax.HighestPercentage;
                                }

                                if (tax.Proceeds90 is null)
                                {
                                    tax.Proceeds90 = PreTax.Proceeds90;
                                }

                                tax.Save();
                            }                            
                        }
                    }

                    await uof.CommitChangesAsync();
                }
            }
        }

        private DateTime? ReturnDateTime(object obj)
        {
            if (obj is DateTime date)
            {
                return date;
            }
            else
            {
                return null;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCustomer_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            cls_BaseSpr.ButtonEditButtonClickBase<Customer>(Session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.Customer, 1, null, null, false, null, string.Empty, false, true);

            if (((ButtonEdit)sender).EditValue is Customer customer)
            {
                if (PreTax.Oid <= 0)
                {
                    FillingClassByTheLastRecord(customer);
                    FillingForm();
                }                
            }            
        }

        private void FillingClassByTheLastRecord(Customer customer = null)
        {
            if (PreTax.Oid <= 0)
            {
                if (btnCustomer.EditValue is Customer _customer)
                {
                    var pretax = default(PreTax);

                    if (customer is null)
                    {
                        pretax = new XPCollection<PreTax>(Session).LastOrDefault(l => l.Customer?.Oid == _customer?.Oid);
                    }
                    else
                    {
                        pretax = new XPCollection<PreTax>(Session).LastOrDefault(l => l.Customer?.Oid == customer?.Oid);
                    }                    

                    if (pretax != null)
                    {
                        PreTax.Staff = pretax.Staff;
                        PreTax.Customer = pretax.Customer;
                        PreTax.DateDelivery = pretax.DateDelivery;
                        PreTax.IsAgreement = pretax.IsAgreement;
                        PreTax.DateAgreement = pretax.DateAgreement;
                        PreTax.PeriodReportChange = pretax.PeriodReportChange;
                        PreTax.Year = pretax.Year;
                        PreTax.Report = pretax.Report;
                        PreTax.Proceeds51 = pretax.Proceeds51;
                        PreTax.Proceeds90 = pretax.Proceeds90;
                        PreTax.HighestPercentage = pretax.HighestPercentage;
                        PreTax.PreliminaryAmount = pretax.PreliminaryAmount;
                        PreTax.AmountInDeclaration = pretax.AmountInDeclaration;
                        PreTax.IsUseDivisionBy3 = pretax.IsUseDivisionBy3;
                        PreTax.IsUseAdvancesOnProfit = pretax.IsUseAdvancesOnProfit;
                        PreTax.PaymentOne = pretax.PaymentOne;
                        PreTax.PaymentTwo = pretax.PaymentTwo;
                        PreTax.PaymentThree = pretax.PaymentThree;
                        PreTax.DateOne = pretax.DateOne;
                        PreTax.DateTwo = pretax.DateTwo;
                        PreTax.DateThree = pretax.DateThree;
                    }
                    else
                    {
                        PreTax.Customer = customer ?? _customer;
                        
                        PreTax.DateDelivery = null;
                        PreTax.IsAgreement = false;                        
                        PreTax.DateAgreement = null;
                        PreTax.Year = DateTime.Now.Year;
                        PreTax.Report = null;
                        PreTax.Proceeds51 = null;
                        PreTax.Proceeds90 = null;
                        PreTax.HighestPercentage = null;
                        PreTax.PreliminaryAmount = null;
                        PreTax.AmountInDeclaration = null;
                        PreTax.IsUseDivisionBy3 = false;
                        PreTax.IsUseAdvancesOnProfit = false;
                        PreTax.PaymentOne = false;
                        PreTax.PaymentTwo = false;
                        PreTax.PaymentThree = false;
                        PreTax.DateOne = null;
                        PreTax.DateTwo = null;
                        PreTax.DateThree = null;
                    }
                }
            }            
        }
        
        private void FillingForm()
        {
            if (PreTax != null)
            {
                btnStaff.EditValue = PreTax.Staff;
                btnCustomer.EditValue = PreTax.Customer;

                PeriodReportChangeDictionary.TryGetValue(PreTax.PeriodReportChange.GetEnumDescription(), out int index);
                cmbPeriodReportChange.SelectedIndex = index;

                dateDeliveryEdit.EditValue = PreTax.DateDelivery;
                txtYear.EditValue = PreTax.Year;
                btnReport.EditValue = PreTax.Report;

                txtProceeds51.EditValue = PreTax.Proceeds51;
                txtProceeds90.EditValue = PreTax.Proceeds90;

                if (PreTax.HighestPercentage != null)
                {
                    txtHighestPercentage.EditValue = PreTax.HighestPercentage;
                }
                else
                {
                    layoutControlItemHighestPercentage.Visibility = LayoutVisibility.Never;
                }               
                
                txtPreliminaryAmount.EditValue = PreTax.PreliminaryAmount;
                txtAmountInDeclaration.EditValue = PreTax.AmountInDeclaration;

                checkIsUseDivisionBy3.EditValue = PreTax.IsUseDivisionBy3;
                checkIsUseAdvancesOnProfit.EditValue = PreTax.IsUseAdvancesOnProfit;

                checkPaymentOne.Checked = PreTax.PaymentOne;
                checkPaymentTwo.Checked = PreTax.PaymentTwo;
                checkPaymentThree.Checked = PreTax.PaymentThree;

                dateOne.EditValue = PreTax.DateOne;
                dateTwo.EditValue = PreTax.DateTwo;
                dateThree.EditValue = PreTax.DateThree;

                memoComment.EditValue = PreTax.Comment;

                checkIsAgreement.EditValue = PreTax.IsAgreement;
                dateAgreementEdit.EditValue = PreTax.DateAgreement;

                btnStatusPreTax.EditValue = PreTax.StatusPreTax;
            }            
        }
        
        private bool isPreTaxChange = false;
        
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
                            isPreTaxChange = accessRights.IsPreTaxChange;
                        }                        
                    }                    
                }

                btnSave.Enabled = isPreTaxChange;
                
                CustomerEdit.CloseButtons(btnStaff, isPreTaxChange);
                CustomerEdit.CloseButtons(btnCustomer, isPreTaxChange);
                CustomerEdit.CloseButtons(btnReport, isPreTaxChange);
                CustomerEdit.CloseButtons(btnStatusPreTax, isPreTaxChange);
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private async void ReportChangeEdit_Load(object sender, EventArgs e)
        {
            await SetAccessRights();
            
            BVVGlobal.oFuncXpo.ButtoneEditDoubleClick<Staff>(Session, btnStaff, cls_App.ReferenceBooks.Staff, isEnable: isPreTaxChange);
            BVVGlobal.oFuncXpo.ButtoneEditDoubleClick<Customer>(Session, btnCustomer, cls_App.ReferenceBooks.Customer, isEnable: isPreTaxChange);
            BVVGlobal.oFuncXpo.ButtoneEditDoubleClick<Customer>(Session, btnReport, cls_App.ReferenceBooks.Report, isEnable: isPreTaxChange);

            FillingClassByTheLastRecord();
            FillingForm();
        }
        
        private decimal? GetDecimal(string text)
        {
            var result = default(decimal);
            
            if (int.TryParse(text, out int intResult))
            {
                result = Convert.ToDecimal(intResult);
            }
            else if (decimal.TryParse(text, out decimal decimalResult))
            {
                result = Convert.ToDecimal(decimalResult);
            }
            else if (float.TryParse(text, out float floatResult))
            {
                result = Convert.ToDecimal(floatResult);
            }
            else if (double.TryParse(text, out double doubleResult))
            {
                result = Convert.ToDecimal(doubleResult);
            }

            result = Convert.ToDecimal(result).GetDecimalRound();

            return result;
        }

        private void btnReport_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<Report>(Session, buttonEdit, (int)cls_App.ReferenceBooks.Report, 1, null, null, false, null, string.Empty, false, true);            
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

        private void cmbPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        
        private void checkIsUseDivisionBy3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkIsUseDivisionBy3.Checked)
            {
                layoutControlGroupDivisionBy3.Visibility = LayoutVisibility.Always;

                if (PreTax.DateOne is null && PreTax.DateTwo is null && PreTax.DateThree is null)
                {
                    var periodReportChange = default(PeriodReportChange);
                    var year = DateTime.Now.Year;
                    
                    if (cmbPeriodReportChange.SelectedIndex != -1)
                    {
                        foreach (PeriodReportChange item in Enum.GetValues(typeof(PeriodReportChange)))
                        {
                            if (item.GetEnumDescription().Equals(cmbPeriodReportChange.EditValue))
                            {
                                periodReportChange = item;
                            }
                        }
                    }

                    if (int.TryParse(txtYear.Text, out int result))
                    {
                        year = result;
                    }
                    
                    switch (periodReportChange)
                    {
                        case PeriodReportChange.FIRSTQUARTER:
                            dateOne.EditValue = new DateTime(year, (int)Month.January, 25);
                            dateTwo.EditValue = new DateTime(year, (int)Month.February, 25);
                            dateThree.EditValue = new DateTime(year, (int)Month.March, 25);
                            break;
                            
                        case PeriodReportChange.SECONDQUARTER:
                            dateOne.EditValue = new DateTime(year, (int)Month.April, 25);
                            dateTwo.EditValue = new DateTime(year, (int)Month.May, 25);
                            dateThree.EditValue = new DateTime(year, (int)Month.June, 25);
                            break;
                            
                        case PeriodReportChange.THIRDQUARTER:
                            dateOne.EditValue = new DateTime(year, (int)Month.July, 25);
                            dateTwo.EditValue = new DateTime(year, (int)Month.August, 25);
                            dateThree.EditValue = new DateTime(year, (int)Month.September, 25);
                            break;
                            
                        case PeriodReportChange.FOURTHQUARTER:
                            dateOne.EditValue = new DateTime(year, (int)Month.October, 25);
                            dateTwo.EditValue = new DateTime(year, (int)Month.November, 25);
                            dateThree.EditValue = new DateTime(year, (int)Month.December, 25);
                            break;
                    }
                }
            }
            else
            {
                layoutControlGroupDivisionBy3.Visibility = LayoutVisibility.Never;

                dateOne.EditValue = null;
                dateTwo.EditValue = null;
                dateThree.EditValue = null;

                checkPaymentOne.Checked = false;
                checkPaymentTwo.Checked = false;
                checkPaymentThree.Checked = false;                
            }
        }

        private decimal GetHighestPercentageResult()
        {
            var result51 = GetDecimal(txtProceeds51.Text);
            var result90 = GetDecimal(txtProceeds90.Text);

            decimal result;
            if (result51 > result90)
            {
                result = Convert.ToDecimal(result51 * 0.01M);
            }
            else
            {
                result = Convert.ToDecimal(result90 * 0.01M);
            }

            return result.GetDecimalRound();
        }

        private void txtProceeds51_EditValueChanged(object sender, EventArgs e)
        {
            txtHighestPercentage.EditValue = GetHighestPercentageResult();
        }

        private void txtProceeds90_EditValueChanged(object sender, EventArgs e)
        {
            txtHighestPercentage.EditValue = GetHighestPercentageResult();
        }        

        private void btnStatusPreTax_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<StatusPreTax>(Session, buttonEdit, (int)cls_App.ReferenceBooks.StatusPreTax, 1, null, null, false, null, string.Empty, false, true);
        }
    }
}