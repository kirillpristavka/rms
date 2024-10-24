using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using PulsLibrary.Extensions.DevForm;
using PulsLibrary.Extensions.DevXpo;
using PulsLibrary.Methods;
using RMS.Core.Controllers.Customers;
using RMS.Core.Model;
using RMS.Core.Model.Exchange;
using RMS.Core.Model.InfoCustomer;
using System;

namespace RMS.UI.xUI.PackagesDocument.Forms
{
    public partial class CustomerImportEEUEdit : formEdit_BaseSpr
    {
        private bool isDirectory;

        private UnitOfWork _uof;
        private Customer _customer;

        //public int Id { get; private set; }
        public bool IsSave { get; private set; }
        public ImportEEU ImportEEU { get; private set; }

        public CustomerImportEEUEdit(UnitOfWork uof = null)
        { 
            InitializeComponent();
            Icon = Properties.Resources.IconRMS;

            if (uof is null)
            {
                uof = new UnitOfWork();
            }
            _uof = uof;
        }
        
        public CustomerImportEEUEdit(int id, object customerObj = null) : this()
        {
            this.id = id;

            SetDirectoryFlag();
            SetCustomer(customerObj);
        }

        public CustomerImportEEUEdit(object obj, UnitOfWork uof = null) : this(uof)
        {
            if (obj is ImportEEU importEEU)
            {
                this.id = importEEU.Oid;
                ImportEEU = importEEU;
            }
        }

        public CustomerImportEEUEdit(ImportEEU obj) : this(obj.Session)
        {
            this.id = obj.Oid;
            ImportEEU = obj;
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
            cmbCountry.Properties.Items.AddRange(await CountryController.GetCountriesAsync(_uof));            
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            await FillingFormObjectsAsync();

            if (ImportEEU is null)
            {
                var obj = await _uof.GetObjectByKeyAsync<ImportEEU>(Id);
                if (obj is null)
                {
                    obj = new ImportEEU(_uof);
                }
                
                ImportEEU = obj;
            }           
            
            FillingOutTheEditForm(ImportEEU);
        }

        /// <summary>
        /// Заполнение формы редактирования.
        /// </summary>
        /// <param name="counterparty">Объект заполнения.</param>
        private void FillingOutTheEditForm(ImportEEU obj)
        {
            if (obj is null)
            {
                obj = new ImportEEU(_uof);
            }

            cmbCustomer.EditValue = obj.Customer ?? _customer;
            dateImportDate.EditValue = obj.ImportDate;
            cmbCountry.EditValue = obj.Country;
            textEditCountry.EditValue = obj.Country?.ToString();
            spinEditPayment.EditValue = obj.Payment;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            var customer = await _uof.GetObjectByKeyFromValueAsync<Customer>(cmbCustomer.EditValue);
            if (customer == null)
            {
                DevXtraMessageBox.ShowXtraMessageBox("Для сохранения необходимо указать клиента.", cmbCustomer);
                return;
            }
                        
            
            var importDate = Objects.GetRequiredObject<DateTime?>(this.dateImportDate.EditValue);
            var payment = Objects.GetRequiredObject<decimal?>(this.spinEditPayment.EditValue);
            //var country = await _uof.GetObjectByKeyFromValueAsync<Country>(cmbCountry.EditValue);
            
            
            var country = default(Country);
            var name = textEditCountry.Text?.Trim();
            if (!string.IsNullOrWhiteSpace(name))
            {
                country = await new XPQuery<Country>(_uof).FirstOrDefaultAsync(f => f.Name == name);

                if (country is null)
                {
                    using (var s = new UnitOfWork())
                    {
                        s.Save(new Country(s) { Name = name });
                        await s.CommitTransactionAsync();
                    }

                    country = await new XPQuery<Country>(_uof).FirstOrDefaultAsync(f => f.Name == name);
                }
            }

            var customerStaff = new ImportEEU()
            {
                Customer = customer,
                ImportDate = importDate,
                Country = country,
                Payment = payment
            };

            if (ImportEEU is null)
            {
                ImportEEU = new ImportEEU(_uof);
            }

            ImportEEU.Edit(customerStaff);

            if (isDirectory)
            {
                await _uof.CommitTransactionAsync();
            }

            this.id = ImportEEU.Oid;
            IsSave = true;
            flagSave = true;
            Close();
        }        

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ImportEEU?.Reload();
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