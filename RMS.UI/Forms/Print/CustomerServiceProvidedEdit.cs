using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using RMS.Core.Controller.Print;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using System;

namespace RMS.UI.Forms.Print
{
    public partial class OrganizationPerformancePrint : XtraForm
    {
        private Session Session { get; }
        private Customer Customer { get; }
        public OrganizationPerformance OrganizationPerformance { get; }

        public OrganizationPerformancePrint()
        {
            InitializeComponent();

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                OrganizationPerformance = new OrganizationPerformance(Session);
            }
            
            foreach (Month item in Enum.GetValues(typeof(Month)))
            {
                cmbMonthSince.Properties.Items.Add(item.GetEnumDescription());
                cmbMonthTo.Properties.Items.Add(item.GetEnumDescription());
            }
            cmbMonthSince.SelectedIndex = 0;
            cmbMonthTo.SelectedIndex = 0;

            cmbYearSince.EditValue = DateTime.Now.Year;
            cmbYearTo.EditValue = DateTime.Now.Year;
        }

        public OrganizationPerformancePrint(Customer customer) : this()
        {
            Customer = customer;
            Session = customer.Session;
            OrganizationPerformance = new OrganizationPerformance(Session);
        }

        public OrganizationPerformancePrint(OrganizationPerformance organizationPerformance) : this()
        {
            OrganizationPerformance = organizationPerformance;
            Customer = organizationPerformance.Customer;
            Session = organizationPerformance.Session;
        }

        private void btnViewing_Click(object sender, EventArgs e)
        {
            var captionSince = string.Empty;
            var captionTo = string.Empty;

            var groupOperator= new GroupOperator(GroupOperatorType.And);

            if (btnCustomer.EditValue is Customer customer)
            {
                var criteria = new BinaryOperator($"{nameof(OrganizationPerformance.Customer)}.{nameof(OrganizationPerformance.Customer.Oid)}", customer.Oid);
                groupOperator.Operands.Add(criteria);
            }

            if (cmbMonthSince.EditValue != null && cmbMonthSince.SelectedIndex != -1)
            {
                var monthSince = (Month)cmbMonthSince.SelectedIndex + 1;
                var criteria = CriteriaOperator.Parse($"GetMonth({nameof(OrganizationPerformance.Date)}) >= {Convert.ToInt32(monthSince)}");
                groupOperator.Operands.Add(criteria);

                captionSince += $"{monthSince.GetEnumDescription()} ";
            }
            
            if (cmbYearSince.EditValue != null)
            {
                var yearSince = Convert.ToInt32(cmbYearSince.EditValue);
                var criteria = CriteriaOperator.Parse($"GetYear({nameof(OrganizationPerformance.Date)}) >= {yearSince}");
                groupOperator.Operands.Add(criteria);

                captionSince += $"{yearSince} ";
            }

            if (cmbMonthTo.EditValue != null && cmbMonthTo.SelectedIndex != -1)
            {
                var monthTo = (Month)cmbMonthTo.SelectedIndex + 1;
                var criteria = CriteriaOperator.Parse($"GetMonth({nameof(OrganizationPerformance.Date)}) <= {Convert.ToInt32(monthTo)}");
                groupOperator.Operands.Add(criteria);

                captionTo += $"{monthTo.GetEnumDescription()} ";
            }

            if (cmbYearTo.EditValue != null)
            {
                var yearTo = Convert.ToInt32(cmbYearTo.EditValue);
                var criteria = CriteriaOperator.Parse($"GetYear({nameof(OrganizationPerformance.Date)}) <= {yearTo}");
                groupOperator.Operands.Add(criteria);

                captionTo += $"{yearTo}";
            }

            OrganizationPerformanceExcel organizationPerformanceExcel = new OrganizationPerformanceExcel(groupOperator, captionSince.Trim(), captionTo.Trim());

            if (radioGroup.SelectedIndex == 0)
            {
                organizationPerformanceExcel.Print();
            }
            else
            {
                organizationPerformanceExcel.Print2();
            }            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CustomerServiceProvidedEdit_Load(object sender, EventArgs e)
        {
            btnCustomer.EditValue = Customer;
        }

        private void btnCustomer_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            cls_BaseSpr.ButtonEditButtonClickBase<Customer>(Session, (ButtonEdit)sender, (int)cls_App.ReferenceBooks.Customer, 1, null, null, false, null, string.Empty, false, true);
        }

        private void checkEditIsAllCustomer_CheckedChanged(object sender, EventArgs e)
        {
            var checkEdit = sender as CheckEdit;

            if (checkEdit.Checked)
            {
                btnCustomer.EditValue = null;
                btnCustomer.Properties.ReadOnly = true;
            }
            else
            {
                btnCustomer.Properties.ReadOnly = false;
            }
        }

        private void ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
                return;
            }
        }
    }
}