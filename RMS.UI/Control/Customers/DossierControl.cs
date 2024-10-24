using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout;
using MySqlX.XDevAPI;
using RMS.Core.Controllers.Customers;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Salary;
using RMS.Core.Model.Taxes;
using RMS.UI.Forms.ReferenceBooks;
using RMS.UI.Forms.Taxes;
using RMS.UI.xUI.PackagesDocument.Controllers;
using System;
using System.Drawing;
using System.Linq;

namespace RMS.UI.Control.Customers
{
    public partial class DossierControl : XtraUserControl
    {
        private UnitOfWork _uof;
        private Customer _customer;
        
        private CustomerStaffControl _customerStaffControl;
        private CustomerImportEEUControl _customerImportEEUControl;

        private ImageCollection _imageCollection;

        public DossierControl()
        {
            InitializeComponent();
            _uof = new UnitOfWork();
        }

        private async void DossierControl_Load(object sender, EventArgs e)
        {
            CreateCustomerImportEEUControl();
            CreateCustomerStaffControl();
        }

        public void SetImageCollection(ImageCollection imageCollection)
        {
            _imageCollection = imageCollection;
        }

        private void CreateCustomerImportEEUControl()
        {
            _customerImportEEUControl = default(CustomerImportEEUControl);
            var baseLayoutItem = layoutControlGroupCustomerStaff.Items.FirstOrDefault(f => f.Text.Equals(nameof(_customerImportEEUControl)));
            if (baseLayoutItem is null)
            {
                _customerImportEEUControl = new CustomerImportEEUControl();
                _customerImportEEUControl.SetUnitOfWork(_uof);
                //_customerStaffControl.UpdateObj += CustomerStaffControl_UpdateObj;
                var item = layoutControlGroupCustomerImportEEU.AddItem(nameof(_customerImportEEUControl));
                item.Control = _customerImportEEUControl;
                item.TextVisible = false;
            }
            else
            {
                _customerImportEEUControl = (CustomerImportEEUControl)((LayoutControlItem)baseLayoutItem).Control;
            }
        }

        private void CreateCustomerStaffControl()
        {
            _customerStaffControl = default(CustomerStaffControl);
            var baseLayoutItem = layoutControlGroupCustomerStaff.Items.FirstOrDefault(f => f.Text.Equals(nameof(_customerStaffControl)));
            if (baseLayoutItem is null)
            {
                _customerStaffControl = new CustomerStaffControl();
                _customerStaffControl.SetUnitOfWork(_uof);
                //_customerStaffControl.UpdateObj += CustomerStaffControl_UpdateObj;
                var item = layoutControlGroupCustomerStaff.AddItem(nameof(_customerStaffControl));
                item.Control = _customerStaffControl;
                item.TextVisible = false;
            }
            else
            {
                _customerStaffControl = (CustomerStaffControl)((LayoutControlItem)baseLayoutItem).Control;
            }
        }


        public async System.Threading.Tasks.Task UpdateAsync(Customer customer)
        {
            _customer = customer;
            await UpdateAsync(customer.Oid);
        }

        public async System.Threading.Tasks.Task UpdateAsync(int? oid)
        {
            var customer = await new XPQuery<Customer>(_uof)
                .FirstOrDefaultAsync(f => f.Oid == oid);

            if (customer != null)
            {
                _customerStaffControl?.UpdateData(await CustomerStaffController.GetCustomersStaffAsync(_uof, customer));
                _customerStaffControl?.SetCustomer(customer);

                _customerImportEEUControl?.UpdateData(await CustomerImportEEUController.GetCustomersImportEEUAsync(_uof, customer));
                _customerImportEEUControl?.SetCustomer(customer);

                txtCustomerFullName.EditValue = customer.FullName ?? customer.ToString();
                txtFormCorporation.EditValue = customer.FormCorporationString;

                //txtSalaryDayObject.EditValue = customer.SalaryDay;
                //txtAdvanceDayObject.EditValue = customer.ImprestDay;

                var subdivisions = customer.Subdivisions;
                if (subdivisions.Count > 0)
                {
                    var result = default(string);
                    foreach (var subdivision in subdivisions)
                    {
                        result += $"{subdivision.Name}";
                        if (subdivision.InputDate is DateTime date)
                        {
                            result += $" ({date.ToShortDateString()})";
                        }
                        result += Environment.NewLine;
                    }
                    memoSubdivisions.EditValue = result?.Trim();
                }
                else
                {
                    memoSubdivisions.EditValue = default;
                }

                var сustomerEmploymentHistorys = customer.CustomerEmploymentHistorys;
                if (сustomerEmploymentHistorys.Count > 0)
                {
                    var result = default(string);
                    foreach (var сustomerEmploymentHistory in сustomerEmploymentHistorys)
                    {
                        result += $"{сustomerEmploymentHistory}";
                        result += Environment.NewLine;
                    }
                    //memoCustomerEmploymentHistorys.EditValue = result?.Trim();
                }
                else
                {
                    //memoCustomerEmploymentHistorys.EditValue = default;
                }

                txtState.Text = $"{_customer?.CustomerStatus?.ToString()}";
                txtOrganizationStatus.Text = $"{_customer?.OrganizationStatusString} на {Convert.ToDateTime(_customer?.DateActuality).ToShortDateString()}";
                btnTaxSystem.EditValue = _customer?.TaxSystemCustomer;
                txtKindActivity.Text = _customer?.KindActivity?.ToString();
                //txtAdvanceDayObject.EditValue = _customer?.AdvanceDayObject;
                //txtSalaryDayObject.EditValue = _customer?.SalaryDayObject;
                btnElectronicReporting.EditValue = _customer?.ElectronicReportingString;
                btnElectronicDocumentManagement.EditValue = _customer?.ElectronicDocumentManagementCustomer;
                btnLeasing.EditValue = _customer?.Leasing;
                btnPatent.EditValue = _customer?.Tax?.Patent;

                gridControlSubdivisions.DataSource = _customer.Subdivisions;
                if (gridViewSubdivisions.Columns[nameof(Subdivision.Oid)] != null)
                {
                    gridViewSubdivisions.Columns[nameof(Subdivision.Oid)].Visible = false;
                    gridViewSubdivisions.Columns[nameof(Subdivision.Oid)].Width = 18;
                    gridViewSubdivisions.Columns[nameof(Subdivision.Oid)].OptionsColumn.FixedWidth = true;
                }

                buttonEdit1.EditValue = _customer.AdvanceDayObject;
                buttonEdit2.EditValue = _customer.SalaryDayObject;

                memoAdditionalInformationSalary.Text = _customer.DOP1;
                memoEdit8.Text = _customer.DOP2;

                btnTaxProperty.EditValue = _customer.Tax?.TaxProperty;
                if (_customer.Tax?.TaxProperty?.IsUse is true)
                {
                    btnTaxProperty.Properties.ContextImageOptions.Image = _imageCollection.Images[0];
                }
                else if (_customer.Tax?.TaxProperty?.IsUse is false)
                {
                    btnTaxProperty.Properties.ContextImageOptions.Image = _imageCollection.Images[1];
                }
                else
                {
                    btnTaxProperty.Properties.ContextImageOptions.Image = null;
                }

                btnTaxAlcohol.EditValue = _customer.Tax?.TaxAlcohol;
                if (_customer.Tax?.TaxAlcohol?.IsUse is true)
                {
                    btnTaxAlcohol.Properties.ContextImageOptions.Image = _imageCollection.Images[0];
                }
                else if (_customer.Tax?.TaxAlcohol?.IsUse is false)
                {
                    btnTaxAlcohol.Properties.ContextImageOptions.Image = _imageCollection.Images[1];
                }
                else
                {
                    btnTaxAlcohol.Properties.ContextImageOptions.Image = null;
                }

                //btnImportEAEU.EditValue = _customer.Tax?.TaxImportEAEU;
                //if (_customer.Tax?.TaxImportEAEU?.IsUse is true)
                //{
                //    btnImportEAEU.Properties.ContextImageOptions.Image = _imageCollection.Images[0];
                //}
                //else if (_customer.Tax?.TaxImportEAEU?.IsUse is false)
                //{
                //    btnImportEAEU.Properties.ContextImageOptions.Image = _imageCollection.Images[1];
                //}
                //else
                //{
                //    btnImportEAEU.Properties.ContextImageOptions.Image = null;
                //}

                btnTaxTransport.EditValue = _customer.Tax?.TaxTransport;
                if (_customer.Tax?.TaxTransport?.IsUse is true)
                {
                    btnTaxTransport.Properties.ContextImageOptions.Image = _imageCollection.Images[0];
                }
                else if (_customer.Tax?.TaxTransport?.IsUse is false)
                {
                    btnTaxTransport.Properties.ContextImageOptions.Image = _imageCollection.Images[1];
                }
                else
                {
                    btnTaxTransport.Properties.ContextImageOptions.Image = null;
                }

                btnTaxLand.EditValue = _customer.Tax?.TaxLand;
                if (_customer.Tax?.TaxLand?.IsUse is true)
                {
                    btnTaxLand.Properties.ContextImageOptions.Image = _imageCollection.Images[0];
                }
                else if (_customer.Tax?.TaxLand?.IsUse is false)
                {
                    btnTaxLand.Properties.ContextImageOptions.Image = _imageCollection.Images[1];
                }
                else
                {
                    btnTaxLand.Properties.ContextImageOptions.Image = null;
                }

                btnLeasing.EditValue = _customer.Leasing;
                if (_customer.Leasing?.IsUse is true)
                {
                    btnLeasing.Properties.ContextImageOptions.Image = _imageCollection.Images[0];
                }
                else if (_customer.Leasing?.IsUse is false)
                {
                    btnLeasing.Properties.ContextImageOptions.Image = _imageCollection.Images[1];
                }
                else
                {
                    btnLeasing.Properties.ContextImageOptions.Image = null;
                }
            }
        }

        private void txtImprestDay_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (_customer is null)
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
                if (_customer.AdvanceDayObject is null)
                {
                    _customer.AdvanceDayObject = new SalaryAndAdvance(_uof);
                    _customer.AdvanceDayObject.Save();
                }

                var form = new SalaryEdit(_customer, _customer.AdvanceDayObject);
                form.ShowDialog();
                buttonEdit.EditValue = form.SalaryAndAdvance;
            }
        }

        private void txtSalaryDay_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (_customer is null)
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
                if (_customer.SalaryDayObject is null)
                {
                    _customer.SalaryDayObject = new SalaryAndAdvance(_uof);
                    _customer.SalaryDayObject.Save();
                }

                var form = new SalaryEdit(_customer, _customer.SalaryDayObject);
                form.ShowDialog();
                buttonEdit.EditValue = form.SalaryAndAdvance;
            }
        }

        private void btnImportEEUView_Click(object sender, EventArgs e)
        {

        }

        private void btnTaxSystem_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (sender is ButtonEdit buttonEdit)
            {
                if (_customer != null)
                {
                    _customer.Reload();
                    _customer.TaxSystemCustomer?.Reload();
                    _customer.TaxSystemCustomer?.TaxSystem?.Reload();

                    var form = new TaxSystemCustomerEdit(_customer);
                    form.ShowDialog();

                    buttonEdit.Text = _customer.TaxSystemCustomer?.ToString();
                    buttonEdit.EditValue = _customer.TaxSystemCustomer;
                }
            }
        }

        private void btnElectronicReporting_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (sender is ButtonEdit buttonEdit) 
            {
                if (_customer != null)
                {
                    if (e.Button.Kind == ButtonPredefines.Delete)
                    {
                        buttonEdit.EditValue = null;
                        return;
                    }

                    var form = new ElectronicReportingCustomerEdit2(_customer);
                    form.ShowDialog();

                    buttonEdit.EditValue = _customer?.ElectronicReportingString;

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
                }
            }
        }

        private void btnElectronicDocumentManagement_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (sender is ButtonEdit buttonEdit) 
            {
                if (_customer != null)
                {
                    if (e.Button.Kind == ButtonPredefines.Delete)
                    {
                        buttonEdit.EditValue = null;
                        return;
                    }

                    var form = new ElectronicDocumentManagementCustomerEdit(_customer);
                    form.ShowDialog();

                    buttonEdit.EditValue = _customer?.ElectronicDocumentManagementCustomer;

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
            }
        }

        private void btnLeasing_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (sender is ButtonEdit buttonEdit)                
            {
                if (_customer != null)
                {
                    var form = new LeasingEdit(_customer);
                    form.ShowDialog();

                    buttonEdit.EditValue = _customer.Leasing;                    
                }               
            }
        }

        private void btnPatent_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (sender is ButtonEdit buttonEdit)
            {
                if (_customer != null)
                {
                    if (_customer.Tax is null)
                    {
                        _customer.Tax = new Tax(_uof);
                        _customer.Tax.Save();
                    }

                    if (_customer.Tax.Patent is null)
                    {
                        _customer.Tax.Patent = new Patent(_uof);
                        _customer.Tax.Patent.Save();
                    }

                    _customer.Tax?.Patent?.Reload();
                    _customer.Tax?.Patent?.PatentObjects?.Reload();
                    foreach (var patentObjects in _customer.Tax.Patent.PatentObjects)
                    {
                        patentObjects?.Reload();
                        patentObjects?.PatentStatus?.Reload();
                    }

                    var form = new PatentEdit2(_customer.Tax.Patent, _customer);
                    form.ShowDialog();

                    btnPatent.EditValue = _customer.Tax?.Patent;                   
                }
            }
        }


        private void btnTaxAlcohol_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (_customer != null)
            {
                var form = new TaxAlcoholEdit(_customer);
                form.ShowDialog();

                buttonEdit.EditValue = _customer.Tax?.TaxAlcohol;

                if (_customer.Tax?.TaxAlcohol?.IsUse is true)
                {
                    buttonEdit.Properties.ContextImageOptions.Image = _imageCollection.Images[0];
                }
                else if (_customer.Tax?.TaxAlcohol?.IsUse is false)
                {
                    buttonEdit.Properties.ContextImageOptions.Image = _imageCollection.Images[1];
                }
                else
                {
                    buttonEdit.Properties.ContextImageOptions.Image = null;
                }
            }
        }

        private void btnTaxTransport_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;
            if (_customer != null)
            {
                var form = new TaxTransportEdit(_customer);
                form.ShowDialog();

                buttonEdit.EditValue = _customer.Tax?.TaxTransport;

                if (_customer.Tax?.TaxTransport?.IsUse is true)
                {
                    buttonEdit.Properties.ContextImageOptions.Image = _imageCollection.Images[0];
                }
                else if (_customer.Tax?.TaxTransport?.IsUse is false)
                {
                    buttonEdit.Properties.ContextImageOptions.Image = _imageCollection.Images[1];
                }
                else
                {
                    buttonEdit.Properties.ContextImageOptions.Image = null;
                }
            }
        }

        private void btnTaxLand_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (_customer != null)
            {
                var form = new TaxLandEdit(_customer);
                form.ShowDialog();

                buttonEdit.EditValue = _customer.Tax?.TaxLand;

                if (_customer.Tax?.TaxLand?.IsUse is true)
                {
                    buttonEdit.Properties.ContextImageOptions.Image = _imageCollection.Images[0];
                }
                else if (_customer.Tax?.TaxLand?.IsUse is false)
                {
                    buttonEdit.Properties.ContextImageOptions.Image = _imageCollection.Images[1];
                }
                else
                {
                    buttonEdit.Properties.ContextImageOptions.Image = null;
                }
            }
        }

        private void btnTaxIndirect_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;
            if (_customer != null)
            {
                var form = new TaxIndirectEdit(_customer);
                form.ShowDialog();

                buttonEdit.EditValue = _customer.Tax?.TaxIndirect;

                if (_customer.Tax?.TaxIndirect?.IsUse is true)
                {
                    buttonEdit.Properties.ContextImageOptions.Image = _imageCollection.Images[0];
                }
                else if (_customer.Tax?.TaxIndirect?.IsUse is false)
                {
                    buttonEdit.Properties.ContextImageOptions.Image = _imageCollection.Images[1];
                }
                else
                {
                    buttonEdit.Properties.ContextImageOptions.Image = null;
                }
            }
        }

        private void btnTaxProperty_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;
            if (_customer != null)
            {
                var form = new TaxPropertyEdit(_customer);
                form.ShowDialog();

                buttonEdit.EditValue = _customer.Tax?.TaxProperty;

                if (_customer.Tax?.TaxProperty?.IsUse is true)
                {
                    buttonEdit.Properties.ContextImageOptions.Image = _imageCollection.Images[0];
                }
                else if (_customer.Tax?.TaxProperty?.IsUse is false)
                {
                    buttonEdit.Properties.ContextImageOptions.Image = _imageCollection.Images[1];
                }
                else
                {
                    buttonEdit.Properties.ContextImageOptions.Image = null;
                }
            }
        }

        private void btnTaxTransport_ButtonPressed_1(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;
            if (_customer != null)
            {
                var form = new TaxTransportEdit(_customer);
                form.ShowDialog();

                buttonEdit.EditValue = _customer.Tax?.TaxTransport;

                if (_customer.Tax?.TaxTransport?.IsUse is true)
                {
                    buttonEdit.Properties.ContextImageOptions.Image = _imageCollection.Images[0];
                }
                else if (_customer.Tax?.TaxTransport?.IsUse is false)
                {
                    buttonEdit.Properties.ContextImageOptions.Image = _imageCollection.Images[1];
                }
                else
                {
                    buttonEdit.Properties.ContextImageOptions.Image = null;
                }
            }
        }
    }
}
