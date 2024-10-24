using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using PulsLibrary.Extensions.DevForm;
using PulsLibrary.Extensions.DevXpo;
using PulsLibrary.Methods;
using RMS.Core.Controllers.Customers;
using RMS.Core.Model.InfoCustomer;
using System;

namespace RMS.UI.xUI.PackagesDocument.Forms
{
    public partial class CustomerStaffEdit : formEdit_BaseSpr
    {
        private bool isDirectory;

        private UnitOfWork _uof;
        private Customer _customer;

        //public int Id { get; private set; }
        public bool IsSave { get; private set; }
        public CustomerStaff CustomerStaff { get; private set; }

        public CustomerStaffEdit(UnitOfWork uof = null)
        {
            InitializeComponent();
            Icon = Properties.Resources.IconRMS;

            if (uof is null)
            {
                uof = new UnitOfWork();
            }
            _uof = uof;
        }
        
        public CustomerStaffEdit(int id, object customerObj = null) : this()
        {
            this.id = id;

            SetDirectoryFlag();
            SetCustomer(customerObj);
        }

        public CustomerStaffEdit(object obj, UnitOfWork uof = null) : this(uof)
        {
            if (obj is CustomerStaff customerStaff)
            {
                this.id = customerStaff.Oid;
                CustomerStaff = customerStaff;
            }
        }

        public CustomerStaffEdit(CustomerStaff obj) : this(obj.Session)
        {
            this.id = obj.Oid;
            CustomerStaff = obj;
        }        

        public void SetDirectoryFlag()
        {
            isDirectory = true;
            layoutControlItemCustomer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        }

        public async void SetCustomer(object obj)
        {
            var oid = -1;

            if (obj is Customer customer)
            {
                oid = customer.Oid;
            }
            else if (int.TryParse(obj?.ToString(), out oid)) { }

            if (oid > 0)
            {
                _customer = await new XPQuery<Customer>(_uof)?.FirstOrDefaultAsync(f => f.Oid == oid);
            }
        }        

        /// <summary>
        /// Заполнение объектов на форме.
        /// </summary>
        /// <returns></returns>
        private async System.Threading.Tasks.Task FillingFormObjectsAsync()
        {
            cmbCustomer.Properties.Items.AddRange(await CustomerController.GetCustomersAsync(_uof));
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            await FillingFormObjectsAsync();

            if (CustomerStaff is null)
            {
                var obj = await _uof.GetObjectByKeyAsync<CustomerStaff>(Id);
                if (obj is null)
                {
                    obj = new CustomerStaff(_uof);
                }
                
                CustomerStaff = obj;
            }           
            
            FillingOutTheEditForm(CustomerStaff);
        }

        /// <summary>
        /// Заполнение формы редактирования.
        /// </summary>
        /// <param name="counterparty">Объект заполнения.</param>
        private void FillingOutTheEditForm(CustomerStaff customerStaff)
        {
            if (customerStaff is null)
            {
                customerStaff = new CustomerStaff(_uof);
            }

            cmbCustomer.EditValue = customerStaff.Customer ?? _customer;

            txtSurname.EditValue = customerStaff.Surname;
            txtName.EditValue = customerStaff.Name;
            txtPatronymic.EditValue = customerStaff.Patronymic;
            dateBirth.EditValue = customerStaff.DateBirth;

            checkIsForeigner.EditValue = customerStaff.IsForeigner;
            dateDateSince.EditValue = customerStaff.DateSince;
            dateDateTo.EditValue = customerStaff.DateTo;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            var customer = await _uof.GetObjectByKeyFromValueAsync<Customer>(cmbCustomer.EditValue);
            if (customer == null)
            {
                DevXtraMessageBox.ShowXtraMessageBox("Для сохранения необходимо указать клиента.", cmbCustomer);
                return;
            }
                        
            var surname = Objects.GetRequiredObject<string>(txtSurname.EditValue);
            if (string.IsNullOrWhiteSpace(surname))
            {
                DevXtraMessageBox.ShowXtraMessageBox("Для сохранения необходимо указать фамилию.", txtSurname);
                return;
            }

            var name = Objects.GetRequiredObject<string>(txtName.EditValue);
            var patronymic = Objects.GetRequiredObject<string>(txtPatronymic.EditValue);
            var dateBirth = Objects.GetRequiredObject<DateTime?>(this.dateBirth.EditValue);
            var dateSince = Objects.GetRequiredObject<DateTime?>(this.dateDateSince.EditValue);
            var dateTo = Objects.GetRequiredObject<DateTime?>(this.dateDateTo.EditValue);

            var customerStaff = new CustomerStaff()
            {
                Customer = customer,
                Surname = surname?.Trim(),
                Name = name?.Trim(),
                Patronymic = patronymic?.Trim(),
                DateBirth = dateBirth,
                IsForeigner = checkIsForeigner.Checked,
                DateSince = dateSince,
                DateTo = dateTo
            };

            if (CustomerStaff is null)
            {
                CustomerStaff = new CustomerStaff(_uof);
            }

            CustomerStaff.Edit(customerStaff);

            if (isDirectory)
            {
                await _uof.CommitTransactionAsync();
            }

            this.id = CustomerStaff.Oid;
            IsSave = true;
            flagSave = true;
            Close();
        }        

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CustomerStaff?.Reload();
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
    }
}