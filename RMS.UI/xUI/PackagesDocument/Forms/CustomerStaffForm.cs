using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout;
using PulsLibrary.Methods;
using RMS.Core.Controllers.Customers;
using RMS.Core.Controllers.PackagesDocument;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.PackagesDocument;
using RMS.UI.xUI.PackagesDocument.Controllers;
using System;
using System.Linq;

namespace RMS.UI.xUI.PackagesDocument.Forms
{
    public partial class CustomerStaffForm : XtraForm
    {
        private UnitOfWork _uof = new UnitOfWork();
        private CustomerStaff _currentCustomerStaff;
        
        private CustomerStaffControl _customerStaffControl;
        private PackageDocumentTypeControl _packageDocumentTypeControl;
        private PackageDocumentInfoControl _packageDocumentInfoControl;
        private PackageDocumentChronicleControl _packageDocumentChronicleControl;

        public CustomerStaffForm(Session session)
        {
            InitializeComponent();
            Icon = Properties.Resources.IconRMS;
        }

        /// <summary>
        /// Заполнение объектов на форме.
        /// </summary>
        /// <returns></returns>
        private async System.Threading.Tasks.Task FillingFormObjectsAsync()
        {
            cmbCustomer.Properties.Items.AddRange(await CustomerController.GetCustomersAsync(_uof));
            cmbDocument.Properties.Items.AddRange(await new XPQuery<Document>(_uof)?.ToListAsync());
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            await CustomerController.GetCustomersAsync(_uof, isForceUpdate: true, isConfigureAwait: false)
                .ConfigureAwait(false);

            CreateCustomerStaffControl();
            CreatePackageDocumentTypeControl();
            CreatePackageDocumentInfoControl();
            CreatePackageDocumentChronicleControl();

            _customerStaffControl?.UpdateData(await CustomerStaffController.GetCustomersStaffAsync(_uof));
            _packageDocumentInfoControl.UpdateData(_customerStaffControl.GetGridViewObj);
        }

        private void CreateCustomerStaffControl()
        {
            _customerStaffControl = default(CustomerStaffControl);
            var baseLayoutItem = layoutControlGroupCustomerStaff.Items.FirstOrDefault(f => f.Text.Equals(nameof(_customerStaffControl)));
            if (baseLayoutItem is null)
            {
                _customerStaffControl = new CustomerStaffControl();
                _customerStaffControl.SetUnitOfWork(_uof);
                _customerStaffControl.FocusedRowChangedEvent += CustomerStaffControl_FocusedRowChangedEvent;
                _customerStaffControl.UpdateObj += CustomerStaffControl_UpdateObj;
                var item = layoutControlGroupCustomerStaff.AddItem(nameof(_customerStaffControl));
                item.Control = _customerStaffControl;
            }
            else
            {
                _customerStaffControl = (CustomerStaffControl)((LayoutControlItem)baseLayoutItem).Control;
            }
        }

        private void CreatePackageDocumentInfoControl()
        {
            _packageDocumentInfoControl = default(PackageDocumentInfoControl);
            var baseLayoutItem = layoutControlGroupInfo.Items.FirstOrDefault(f => f.Text.Equals(nameof(_packageDocumentInfoControl)));
            if (baseLayoutItem is null)
            {
                _packageDocumentInfoControl = new PackageDocumentInfoControl();
                _packageDocumentInfoControl.SetUnitOfWork(_uof);
                _packageDocumentInfoControl.ListItemCheckForCustomerStaffEvent += _packageDocumentInfoControl_ListItemCheckForCustomerStaffEvent; ;
                var item = layoutControlGroupInfo.AddItem(nameof(_packageDocumentInfoControl));
                item.Control = _packageDocumentInfoControl;
                item.TextVisible = false;
            }
            else
            {
                _packageDocumentInfoControl = (PackageDocumentInfoControl)((LayoutControlItem)baseLayoutItem).Control;
            }
        }

        private void _packageDocumentInfoControl_ListItemCheckForCustomerStaffEvent(object sender, CriteriaOperator criteriaDocument, CriteriaOperator criteriaDocumentType)
        {
            _customerStaffControl?.SetInfoActiveFilterCriteria(criteriaDocument);
            _packageDocumentTypeControl?.SetActiveFilterCriteria(criteriaDocumentType);
        }

        private void CreatePackageDocumentChronicleControl()
        {
            _packageDocumentChronicleControl = default(PackageDocumentChronicleControl);
            var baseLayoutItem = layoutControlGroupChronicle.Items.FirstOrDefault(f => f.Text.Equals(nameof(_packageDocumentChronicleControl)));
            if (baseLayoutItem is null)
            {
                _packageDocumentChronicleControl = new PackageDocumentChronicleControl();
                var item = layoutControlGroupChronicle.AddItem(nameof(_packageDocumentChronicleControl));
                item.Control = _packageDocumentChronicleControl;
                item.TextVisible = false;
            }
            else
            {
                _packageDocumentChronicleControl = (PackageDocumentChronicleControl)((LayoutControlItem)baseLayoutItem).Control;
            }
        }

        private async void CustomerStaffControl_UpdateObj(object sender)
        {
            _packageDocumentTypeControl?.UpdateData(await PackagesDocumentController.GetPackagesDocumentTypeAsync(_uof, _currentCustomerStaff));
        }

        private void CreatePackageDocumentTypeControl()
        {
            _packageDocumentTypeControl = default(PackageDocumentTypeControl);
            var baseLayoutItem = layoutControlGroupCustomerStaff.Items.FirstOrDefault(f => f.Text.Equals(nameof(_packageDocumentTypeControl)));
            if (baseLayoutItem is null)
            {
                _packageDocumentTypeControl = new PackageDocumentTypeControl();
                _packageDocumentTypeControl.SetUnitOfWork(_uof);
                var item = layoutControlGroupPackageDocumentType.AddItem(nameof(_packageDocumentTypeControl));
                item.Control = _packageDocumentTypeControl;
            }
            else
            {
                _packageDocumentTypeControl = (PackageDocumentTypeControl)((LayoutControlItem)baseLayoutItem).Control;
            }
        }

        private async void CustomerStaffControl_FocusedRowChangedEvent(CustomerStaff obj, int focusedRowHandle)
        {
            _currentCustomerStaff = obj;

            _packageDocumentTypeControl.SetCustomerStaff(_currentCustomerStaff);
            _packageDocumentTypeControl.SetCustomer(_currentCustomerStaff?.Customer);
            
            _packageDocumentTypeControl?.UpdateData(await PackagesDocumentController.GetPackagesDocumentTypeAsync(_uof, _currentCustomerStaff));

            _packageDocumentChronicleControl?.SetCustomerStaff(_currentCustomerStaff);
            _packageDocumentChronicleControl?.UpdateData();
        }

        private async void checkBtnFilter_CheckedChanged(object sender, EventArgs e)
        {
            var checkButton = Objects.GetRequiredObject<CheckButton>(sender);
            if (checkButton.Checked)
            {
                if (cmbCustomer.Properties.Items.Count == 0)
                {
                    await FillingFormObjectsAsync();
                }

                layoutControlGroupFilter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                splitterItemFilter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                layoutControlGroupFilter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                splitterItemFilter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        private void cmbCustomer_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = Objects.GetRequiredObject<ButtonEdit>(sender);
            if (buttonEdit != null)
            {
                if (e.Button.Kind == ButtonPredefines.Delete)
                {
                    buttonEdit.EditValue = null;
                }
                else if (e.Button.Kind == ButtonPredefines.Ellipsis)
                {
                    cls_BaseSpr.ButtonEditButtonClickBase<Customer>(null, buttonEdit, (int)cls_App.ReferenceBooks.Customer, 1, null, null, false, null, string.Empty, false, true);
                }
            }
        }

        private void cmbCustomer_SelectedValueChanged(object sender, EventArgs e)
        {
            var cmb = Objects.GetRequiredObject<ComboBoxEdit>(sender);
            if (cmb != null)
            {
                SetCriteria();
            }
        }

        private void cmbCustomerStaff_SelectedValueChanged(object sender, EventArgs e)
        {
            var cmb = Objects.GetRequiredObject<ComboBoxEdit>(sender);
            if (cmb != null)
            {
                if (cmb.EditValue is CustomerStaff customerStaff)
                {
                    if (cmbCustomer.EditValue is null)
                    {
                        cmbCustomer.EditValue = customerStaff.Customer;
                    }
                }

                SetCriteria();
            }
        }
        
        public void SetCriteria()
        {
            var criteriaOperatorDocument = default(CriteriaOperator);
            var criteriaOperatorDocumentType = default(CriteriaOperator);
            
            var groupOperatorDocument = new GroupOperator(GroupOperatorType.And);
            var groupOperatorDocumentType = new GroupOperator(GroupOperatorType.And);

            var customer = Objects.GetRequiredObject<Customer>(cmbCustomer.EditValue);
            if (customer != null)
            {
                groupOperatorDocument.Operands.Add(new BinaryOperator($"{nameof(PackageDocument.Customer)}.{nameof(XPObject.Oid)}", customer.Oid));
            }
                        
            var document = Objects.GetRequiredObject<Document>(cmbDocument.EditValue);
            if (document != null)
            {
                var criteria = new BinaryOperator($"{nameof(PackageDocumentType.Document)}.{nameof(XPObject.Oid)}", document.Oid);
                groupOperatorDocumentType.Operands.Add(criteria);
            }

            if (groupOperatorDocument.Operands.Count > 0)
            {
                criteriaOperatorDocument = groupOperatorDocument;
            }

            if (groupOperatorDocumentType.Operands.Count > 0)
            {
                criteriaOperatorDocumentType = groupOperatorDocumentType;
            }

            _customerStaffControl.SetActiveFilterCriteria(criteriaOperatorDocument);
            _packageDocumentTypeControl.SetActiveFilterCriteria(criteriaOperatorDocumentType);

            _packageDocumentInfoControl?.UpdateData(_customerStaffControl.GetGridViewObj);
        }

        private void cmbStaff_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = Objects.GetRequiredObject<ButtonEdit>(sender);
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
            }
            else if (e.Button.Kind == ButtonPredefines.Ellipsis)
            {
                cls_BaseSpr.ButtonEditButtonClickBase<Staff>(null, buttonEdit, (int)cls_App.ReferenceBooks.Staff, 1, null, null, false, null, string.Empty, false, true);
            }
        }

        private void cmbStaff_SelectedValueChanged(object sender, EventArgs e)
        {
            var cmb = Objects.GetRequiredObject<ComboBoxEdit>(sender);
            if (cmb != null)
            {
                SetCriteria();
            }
        }

        private void cmbDocument_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = Objects.GetRequiredObject<ButtonEdit>(sender);
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
            }
            else if (e.Button.Kind == ButtonPredefines.Ellipsis)
            {
                cls_BaseSpr.ButtonEditButtonClickBase<Document>(null, buttonEdit, (int)cls_App.ReferenceBooks.Document, 1, null, null, false, null, string.Empty, false, true);
            }
        }

        private void cmbDocument_SelectedValueChanged(object sender, EventArgs e)
        {
            var cmb = Objects.GetRequiredObject<ComboBoxEdit>(sender);
            if (cmb != null)
            {
                SetCriteria();
            }
        }

        private void checkBtnInfo_CheckedChanged(object sender, EventArgs e)
        {
            var checkButton = Objects.GetRequiredObject<CheckButton>(sender);
            if (checkButton.Checked)
            {
                layoutControlGroupInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                splitterItemInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                layoutControlGroupInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                splitterItemInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }
    }
}