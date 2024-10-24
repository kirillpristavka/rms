using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout;
using PulsLibrary.Methods;
using RMS.Core.Controllers.Customers;
using RMS.Core.Controllers.PackagesDocument;
using RMS.Core.Controllers.Staffs;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.PackagesDocument;
using RMS.UI.xUI.PackagesDocument.Controllers;
using System;
using System.Linq;

namespace RMS.UI.xUI.PackagesDocument.Forms
{
    public partial class PackageDocumentForm : XtraForm
    {
        private UnitOfWork _uof = new UnitOfWork();
        private PackageDocument _currentPackageDocument;
        
        private PackageDocumentControl _packageDocumentControl;
        private PackageDocumentTypeControl _packageDocumentTypeControl;
        private PackageDocumentInfoControl _packageDocumentInfoControl;
        private PackageDocumentChronicleControl _packageDocumentChronicleControl;

        public PackageDocumentForm(Session session)
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
            cmbStaff.Properties.Items.AddRange(await StaffController.GetStaffsAsync(_uof));
            cmbDocument.Properties.Items.AddRange(await new XPQuery<Document>(_uof)?.ToListAsync());

            await FillingComboboxCustomerStaff();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            await CustomerController.GetCustomersAsync(_uof, isForceUpdate: true, isConfigureAwait: false)
                .ConfigureAwait(false);

            CreatePackageDocumentControl();
            CreatePackageDocumentTypeControl();
            CreatePackageDocumentInfoControl();
            CreatePackageDocumentChronicleControl();

            _packageDocumentControl?.UpdateData(await PackagesDocumentController.GetPackagesDocumentAsync(_uof));
            _packageDocumentInfoControl.UpdateData(_packageDocumentControl.GetGridViewObj);
        }

        private void CreatePackageDocumentControl()
        {
            _packageDocumentControl = default(PackageDocumentControl);
            var baseLayoutItem = layoutControlGroupPackageDocument.Items.FirstOrDefault(f => f.Text.Equals(nameof(_packageDocumentControl)));
            if (baseLayoutItem is null)
            {
                _packageDocumentControl = new PackageDocumentControl();
                _packageDocumentControl.SetUnitOfWork(_uof);
                _packageDocumentControl.FocusedRowChangedEvent += PackageDocumentControl_FocusedRowChangedEvent;
                _packageDocumentControl.UpdateObj += _packageDocumentControl_UpdateObj;
                var item = layoutControlGroupPackageDocument.AddItem(nameof(_packageDocumentControl));
                item.Control = _packageDocumentControl;
            }
            else
            {
                _packageDocumentControl = (PackageDocumentControl)((LayoutControlItem)baseLayoutItem).Control;
            }
        }

        private void CreatePackageDocumentInfoControl()
        {
            _packageDocumentInfoControl = default(PackageDocumentInfoControl);
            var baseLayoutItem = layoutControlGroupInfo.Items.FirstOrDefault(f => f.Text.Equals(nameof(_packageDocumentInfoControl)));
            if (baseLayoutItem is null)
            {
                _packageDocumentInfoControl = new PackageDocumentInfoControl();
                _packageDocumentInfoControl.ListItemCheckForDocumentEvent += _packageDocumentInfoControl_ListItemCheckForDocumentEvent;
                var item = layoutControlGroupInfo.AddItem(nameof(_packageDocumentInfoControl));
                item.Control = _packageDocumentInfoControl;
                item.TextVisible = false;
            }
            else
            {
                _packageDocumentInfoControl = (PackageDocumentInfoControl)((LayoutControlItem)baseLayoutItem).Control;
            }
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

        private async void _packageDocumentControl_UpdateObj(object sender)
        {
            _packageDocumentTypeControl?.UpdateData(await PackagesDocumentController.GetPackagesDocumentTypeAsync(_uof, _currentPackageDocument));
            _packageDocumentInfoControl?.UpdateData(_packageDocumentControl.GetGridViewObj);
        }

        private void CreatePackageDocumentTypeControl()
        {
            _packageDocumentTypeControl = default(PackageDocumentTypeControl);
            var baseLayoutItem = layoutControlGroupPackageDocument.Items.FirstOrDefault(f => f.Text.Equals(nameof(_packageDocumentTypeControl)));
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

        private async void PackageDocumentControl_FocusedRowChangedEvent(PackageDocument obj, int focusedRowHandle)
        {
            _currentPackageDocument = obj;

            _packageDocumentTypeControl.SetPackageDocument(_currentPackageDocument);
            _packageDocumentTypeControl.SetCustomer(_currentPackageDocument?.Customer);

            _packageDocumentTypeControl?.UpdateData(await PackagesDocumentController.GetPackagesDocumentTypeAsync(_uof, _currentPackageDocument));

            _packageDocumentChronicleControl.SetPackageDocument(_currentPackageDocument);
            _packageDocumentChronicleControl?.UpdateData();
        }

        private async void checkBtnFilter_CheckedChanged(object sender, EventArgs e)
        {
            var checkButton = Objects.GetRequiredObject<CheckButton>(sender);
            if (checkButton.Checked)
            {
                if (cmbCustomer.Properties.Items.Count == 0 && cmbCustomerStaff.Properties.Items.Count == 0)
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
                    cmbCustomerStaff.EditValue = null;
                }
                else if (e.Button.Kind == ButtonPredefines.Ellipsis)
                {
                    cls_BaseSpr.ButtonEditButtonClickBase<Customer>(null, buttonEdit, (int)cls_App.ReferenceBooks.Customer, 1, null, null, false, null, string.Empty, false, true);
                }
            }
        }

        private void cmbCustomerStaff_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = Objects.GetRequiredObject<ButtonEdit>(sender);
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;
            }
            else if (e.Button.Kind == ButtonPredefines.Ellipsis)
            {
                cls_BaseSpr.ButtonEditButtonClickBase<CustomerStaff>(null, buttonEdit, (int)cls_App.ReferenceBooks.CustomerStaff, 1, null, null, false, null, string.Empty, false, true);
            }
        }

        private async void cmbCustomer_SelectedValueChanged(object sender, EventArgs e)
        {
            var cmb = Objects.GetRequiredObject<ComboBoxEdit>(sender);
            if (cmb != null)
            {
                if (cmb.EditValue is Customer customer)
                {
                    await FillingComboboxCustomerStaff(customer);
                }
                else
                {
                    await FillingComboboxCustomerStaff();
                }

                SetCriteria();
            }
        }

        private async System.Threading.Tasks.Task FillingComboboxCustomerStaff(object @obj = null)
        {
            cmbCustomerStaff.Properties.Items.Clear();
            cmbCustomerStaff.Properties.Items.AddRange(await CustomerStaffController.GetCustomersStaffAsync(_uof, @obj));
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

        private void _packageDocumentInfoControl_ListItemCheckForDocumentEvent(object sender, CriteriaOperator criteriaDocument, CriteriaOperator criteriaDocumentType)
        {
            _packageDocumentControl?.SetInfoActiveFilterCriteria(criteriaDocument);
            _packageDocumentTypeControl?.SetActiveFilterCriteria(criteriaDocumentType);
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

            var staff = Objects.GetRequiredObject<Staff>(cmbStaff.EditValue);
            if (staff != null)
            {
                groupOperatorDocument.Operands.Add(new BinaryOperator($"{nameof(PackageDocument.Staff)}.{nameof(XPObject.Oid)}", staff.Oid));
            }

            var customerStaff = Objects.GetRequiredObject<CustomerStaff>(cmbCustomerStaff.EditValue);
            if (customerStaff != null)
            {
                var criteria = new BinaryOperator($"{nameof(PackageDocumentType.CustomerStaff)}.{nameof(XPObject.Oid)}", customerStaff.Oid);
                groupOperatorDocument.Operands.Add(new ContainsOperator(nameof(PackageDocument.PackageDocumentsType), criteria));
                groupOperatorDocumentType.Operands.Add(criteria);
            }

            var document = Objects.GetRequiredObject<Document>(cmbDocument.EditValue);
            if (document != null)
            {
                var criteria = new BinaryOperator($"{nameof(PackageDocumentType.Document)}.{nameof(XPObject.Oid)}", document.Oid);
                groupOperatorDocument.Operands.Add(new ContainsOperator(nameof(PackageDocument.PackageDocumentsType), criteria));
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

            _packageDocumentControl?.SetActiveFilterCriteria(criteriaOperatorDocument);
            _packageDocumentTypeControl?.SetActiveFilterCriteria(criteriaOperatorDocumentType);

            _packageDocumentInfoControl?.UpdateData(_packageDocumentControl.GetGridViewObj);
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