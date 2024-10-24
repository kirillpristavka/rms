using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using RMS.Core.Controller;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RMS.UI.Forms.Directories
{
    public partial class AddressFIASEdit : XtraForm
    {
        private Session Session { get; }
        private Customer Customer { get; }
        public Address Address { get; }
        public CustomerAddress CustomerAddress { get; }

        private AddressFIASEdit()
        {
            InitializeComponent();
        }

        public AddressFIASEdit(Address address) : this()
        {
            Session = address.Session;
            Address = address ?? new Address(Session);
        }

        public AddressFIASEdit(Customer customer) : this()
        {
            Customer = customer;
            Session = customer.Session;
            CustomerAddress = new CustomerAddress(Session);
        }

        public AddressFIASEdit(CustomerAddress customerAddress) : this()
        {
            CustomerAddress = customerAddress;
            Customer = customerAddress.Customer;
            Session = customerAddress.Session;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CustomerAddress is null)
            {
                Address.AddressString = txtAddress.Text;
                Address.IsLegal = checkIsLegal.Checked;
                Address.IsActual = checkIsActual.Checked;
                Address.IsExpress = checkIsExpress.Checked;
                Address.Save();
            } 
            else
            {
                CustomerAddress.AddressString = txtAddress.Text;
                CustomerAddress.IsLegal = checkIsLegal.Checked;
                CustomerAddress.IsActual = checkIsActual.Checked;
                CustomerAddress.IsExpress = checkIsExpress.Checked;

                Customer.CustomerAddress.Add(CustomerAddress);
                Customer.Save();
            }
            
            Close();
        }

        private void AddressFIASEdit_Load(object sender, EventArgs e)
        {
            txtAddress.TextChanged += lookUpAddress_TextChanged;
            txtAddress.EditValueChanged += txtAddress_EditValueChanged;

            if (CustomerAddress is null)
            {
                txtAddress.Text = Address.AddressString;
                checkIsLegal.Checked = Address.IsLegal;
                checkIsActual.Checked = Address.IsActual;
                checkIsExpress.Checked = Address.IsExpress;
            }
            else
            {
                txtAddress.Text = CustomerAddress.AddressString;
                checkIsLegal.Checked = CustomerAddress.IsLegal;
                checkIsActual.Checked = CustomerAddress.IsActual;
                checkIsExpress.Checked = CustomerAddress.IsExpress;
            }            
        }
        
        private void txtAddress_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonedit = sender as ButtonEdit;

            if (e?.Button?.Kind == ButtonPredefines.Search)
            {
                var suggestResponse = GetInfoAddressFromDaData.UpdateFromDaData("a8cfa20b99fb660c53b8e0fa2d3de0f2204fb580", buttonedit.Text);
                var address = suggestResponse?.suggestions[0]?.data;

                if (address != null)
                {
                    buttonedit.EditValue = suggestResponse.suggestions[0].unrestricted_value;
                }
            }
        }

        private Dictionary<string, Dadata.Model.Address> dictionaryAddress = new Dictionary<string, Dadata.Model.Address>();
        
        private async void lookUpAddress_TextChanged(object sender, EventArgs e)
        {
            if (isEditValueChanged)
            {
                isEditValueChanged = false;
                return;
            }

            var lookUpEdit = sender as LookUpEdit;

            if (lookUpEdit != null && lookUpEdit.Text.Length > 10)
            {
                await System.Threading.Tasks.Task.Run(() => 
                {                    
                    try
                    {
                        var suggestResponse = GetInfoAddressFromDaData.UpdateFromDaData("a8cfa20b99fb660c53b8e0fa2d3de0f2204fb580", lookUpEdit.Text);

                        if (suggestResponse != null)
                        {
                            Invoke((Action)delegate
                            {
                                var count = suggestResponse.suggestions.Count();
                                if (count > 0)
                                {
                                    lookUpEdit.Properties.DataSource = suggestResponse.suggestions.Select(s => s.unrestricted_value) ?? null;

                                    dictionaryAddress.Clear();
                                    foreach (var item in suggestResponse.suggestions)
                                    {
                                        dictionaryAddress.Add(item.unrestricted_value, item.data);
                                    }

                                    lookUpEdit.Properties.DropDownRows = count;
                                    lookUpEdit.ShowPopup();
                                }
                            });
                        }
                    }
                    catch (Exception) { }                    
                });                
            }
        }
        
        private bool isEditValueChanged = false;
        
        private void txtAddress_EditValueChanged(object sender, EventArgs e)
        {
            var lookUpEdit = sender as LookUpEdit;

            if (lookUpEdit != null && dictionaryAddress.Count > 0)
            {
                var s = dictionaryAddress.FirstOrDefault(f => f.Key == lookUpEdit.Text).Value;

                if (s != null)
                {
                    isEditValueChanged = true;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}