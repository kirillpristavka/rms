using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout;
using PulsLibrary.Extensions.DevForm;
using PulsLibrary.Extensions.DevXpo;
using PulsLibrary.Methods;
using RMS.Core.Controllers.Customers;
using RMS.Core.Controllers.PackagesDocument;
using RMS.Core.Controllers.Staffs;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.PackagesDocument;
using RMS.UI.xUI.PackagesDocument.Controllers;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace RMS.UI.xUI.PackagesDocument.Forms
{
    public partial class PackageDocumentEdit : XtraForm
    {
        private UnitOfWork _uof = new UnitOfWork();

        private PackageDocumentObjControl _packageDocumentObjControl;
        private PackageDocumentTypeControl _packageDocumentTypeControl;
        private PackageDocumentObj _currentPackageDocumentObj;

        private PackageDocumentCustomerStaffObjControl _packageDocumentCustomerStaffObjControl;
        private PackageDocumentCustomerStaffObj _currentPackageDocumentCustomerStaffObj;

        public int Id { get; private set; }
        public bool IsSave { get; private set; }
        public PackageDocument PackageDocument { get; private set; }

        public PackageDocumentEdit()
        {
            InitializeComponent();
            Icon = Properties.Resources.IconRMS;
        }
        
        public PackageDocumentEdit(object obj) : this()
        {
            if (obj is PackageDocument packageDocument)
            {
                Id = packageDocument.Oid;
            }
            else if (int.TryParse(obj?.ToString(), out int id))
            {
                Id = id;
            }
        }
        
        /// <summary>
        /// Заполнение объектов на форме.
        /// </summary>
        /// <returns></returns>
        private async System.Threading.Tasks.Task FillingFormObjectsAsync()
        {
            cmbCustomer.Properties.Items.AddRange(await CustomerController.GetCustomersAsync(_uof));
            cmbStaff.Properties.Items.AddRange(await StaffController.GetStaffsAsync(_uof));
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            await FillingFormObjectsAsync();

            if (PackageDocument is null)
            {
                var obj = await _uof.GetObjectByKeyAsync<PackageDocument>(Id);
                if (obj is null)
                {
                    obj = new PackageDocument(_uof);
                }
                
                PackageDocument = obj;
            }
            
            CreatePackageDocumentTypeControl();
            splitterItem.Location = new Point(0, 400);
            
            CreatePackageDocumentControl();
            CreatePackageDocumentCustomerStaffObjControl();
            
            FillingOutTheEditForm(PackageDocument);
        }

        
        private void CreatePackageDocumentTypeControl()
        {
            _packageDocumentTypeControl = default(PackageDocumentTypeControl);
            var baseLayoutItem = layoutControlGroupObj.Items.FirstOrDefault(f => f.Text.Equals(nameof(_packageDocumentTypeControl)));
            if (baseLayoutItem is null)
            {
                _packageDocumentTypeControl = new PackageDocumentTypeControl();
                _packageDocumentTypeControl.SetUnitOfWork(_uof);
                _packageDocumentTypeControl.SetPermissionEdit(true);
                _packageDocumentTypeControl.SetPackageDocument(PackageDocument);
                var item = layoutControlGroupDocuments.AddItem(nameof(_packageDocumentTypeControl));
                item.Control = _packageDocumentTypeControl;
            }
            else
            {
                _packageDocumentTypeControl = (PackageDocumentTypeControl)((LayoutControlItem)baseLayoutItem).Control;
            }
        }

        private void CreatePackageDocumentControl()
        {
            _packageDocumentObjControl = default(PackageDocumentObjControl);
            var baseLayoutItem = layoutControlGroupObj.Items.FirstOrDefault(f => f.Text.Equals(nameof(_packageDocumentObjControl)));
            if (baseLayoutItem is null)
            {
                _packageDocumentObjControl = new PackageDocumentObjControl();
                _packageDocumentObjControl.SetUnitOfWork(_uof);
                _packageDocumentObjControl.FocusedRowChangedEvent += _packageDocumentObjControl_FocusedRowChangedEvent;
                var item = layoutControlGroupObj.AddItem(nameof(_packageDocumentObjControl));
                item.Control = _packageDocumentObjControl;
            }
            else
            {
                _packageDocumentObjControl = (PackageDocumentObjControl)((LayoutControlItem)baseLayoutItem).Control;
            }
        }

        private void CreatePackageDocumentCustomerStaffObjControl()
        {
            _packageDocumentCustomerStaffObjControl = default(PackageDocumentCustomerStaffObjControl);
            var baseLayoutItem = layoutControlGroupCustomerStaff.Items.FirstOrDefault(f => f.Text.Equals(nameof(_packageDocumentCustomerStaffObjControl)));
            if (baseLayoutItem is null)
            {
                _packageDocumentCustomerStaffObjControl = new PackageDocumentCustomerStaffObjControl();
                _packageDocumentCustomerStaffObjControl.SetUnitOfWork(_uof);
                _packageDocumentCustomerStaffObjControl.FocusedRowChangedEvent += _packageDocumentCustomerStaffObjControl_FocusedRowChangedEvent;
                var item = layoutControlGroupCustomerStaff.AddItem(nameof(_packageDocumentCustomerStaffObjControl));
                item.Control = _packageDocumentCustomerStaffObjControl;
            }
            else
            {
                _packageDocumentCustomerStaffObjControl = (PackageDocumentCustomerStaffObjControl)((LayoutControlItem)baseLayoutItem).Control;
            }
        }

        private void _packageDocumentCustomerStaffObjControl_FocusedRowChangedEvent(PackageDocumentCustomerStaffObj obj, int focusedRowHandle)
        {
            _currentPackageDocumentCustomerStaffObj = obj;
        }

        private void _packageDocumentObjControl_FocusedRowChangedEvent(PackageDocumentObj obj, int focusedRowHandle)
        {
            _currentPackageDocumentObj = obj;
        }

        /// <summary>
        /// Заполнение формы редактирования.
        /// </summary>
        /// <param name="counterparty">Объект заполнения.</param>
        private async void FillingOutTheEditForm(PackageDocument packageDocument)
        {
            if (packageDocument is null)
            {
                packageDocument = new PackageDocument(_uof);
            }

            date.EditValue = packageDocument.Date;
            txtName.EditValue = packageDocument.Name;
            cmbCustomer.EditValue = packageDocument.Customer;
            cmbStaff.EditValue = packageDocument.Staff;
            checkIsShowCustomer.EditValue = packageDocument.IsShowCustomer;
            memoDescription.EditValue = packageDocument.Description;
            memoPeriod.EditValue = packageDocument.Period;

            packageDocument.PackageDocumentCustomerStaffsObj?.Reload();
            _packageDocumentCustomerStaffObjControl.UpdateData(packageDocument.PackageDocumentCustomerStaffsObj?.ToList());

            packageDocument.PackageDocumentObjs?.Reload();
            _packageDocumentObjControl.UpdateData(packageDocument.PackageDocumentObjs?.ToList());

            _packageDocumentTypeControl.UpdateData(await PackagesDocumentController.GetPackagesDocumentTypeAsync(_uof, packageDocument));

            if (_packageDocumentTypeControl?.Objs?.Count() > 0)
            {
                cmbCustomer.Enabled = false;
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            var customer = await _uof.GetObjectByKeyFromValueAsync<Customer>(cmbCustomer.EditValue);
            if (customer == null)
            {
                DevXtraMessageBox.ShowXtraMessageBox("Для сохранения необходимо указать клиента.", cmbCustomer);
                return;
            }

            var _documents = _packageDocumentTypeControl?.GetPackageDocumentType();
            if (_documents is null || _documents.Count() <= 0)
            {
                DevXtraMessageBox.ShowXtraMessageBox("Для сохранения необходимо указать документ.", _packageDocumentTypeControl);
                return;
            }
            
            var staff = await _uof.GetObjectByKeyFromValueAsync<Staff>(cmbStaff.EditValue);
            if (staff == null)
            {
                DevXtraMessageBox.ShowXtraMessageBox("Для сохранения необходимо указать сотрудника.", cmbStaff);
                return;
            }

            var date = Objects.GetRequiredObject<DateTime?>(this.date.EditValue);
            var name = Objects.GetRequiredObject<string>(txtName.EditValue);

            var isShowCustomer = Objects.GetRequiredObject<bool>(checkIsShowCustomer.EditValue);
            
            var description = Objects.GetRequiredObject<string>(memoDescription.EditValue);
            var period = Objects.GetRequiredObject<string>(memoPeriod.EditValue);
            
            if (PackageDocument is null)
            {
                PackageDocument = new PackageDocument(_uof);
            }

            PackageDocument.IsShowCustomer = isShowCustomer;
            PackageDocument.Customer = customer;
            PackageDocument.Staff = staff;
            PackageDocument.Date = date;
            PackageDocument.Name = name;
            PackageDocument.Description = description;
            PackageDocument.Period = period;
            
            PackageDocument.FillDocuments(_documents, DatabaseConnection.User); 
            PackageDocument.FillPackageDocumentObj(_packageDocumentObjControl.PackageDocumentObjs);

            try
            {
                await _uof?.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                _uof?.RollbackTransaction();
            }

            Id = PackageDocument.Oid;
            IsSave = true;
            Close();
        }        

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
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

        private void cmbCustomer_EditValueChanged(object sender, EventArgs e)
        {
            var comboBoxEdit = Objects.GetRequiredObject<ComboBoxEdit>(sender);
            if (comboBoxEdit?.EditValue is Customer customer)
            {
                _packageDocumentCustomerStaffObjControl?.SetCustomer(customer);
                _packageDocumentTypeControl?.SetCustomer(customer);

                if (_packageDocumentCustomerStaffObjControl.CustomerStaffs
                    .FirstOrDefault(f =>
                        f.Customer != null
                        && f.Customer.Oid == customer.Oid) is null)
                {
                    _packageDocumentCustomerStaffObjControl?.ClearListObj();
                    _packageDocumentTypeControl?.ClearListObj();
                }

                return;
            }

            _packageDocumentCustomerStaffObjControl?.ClearListObj();
            _packageDocumentCustomerStaffObjControl?.SetCustomer(null);

            _packageDocumentTypeControl?.ClearListObj();
            _packageDocumentTypeControl?.SetCustomer(null);
        }

        private void cmbStaff_ButtonPressed(object sender, ButtonPressedEventArgs e)
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
                    cls_BaseSpr.ButtonEditButtonClickBase<Staff>(null, buttonEdit, (int)cls_App.ReferenceBooks.Staff, 1, new NullOperator(nameof(Staff.DateDismissal)), null, false, null, string.Empty, false, true);
                }
            }
        }
    }
}