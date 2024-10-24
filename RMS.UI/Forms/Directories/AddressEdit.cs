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
    public partial class AddressEdit : XtraForm
    {
        private Session Session { get; }
        private Customer Customer { get; }
        public CustomerAddress CustomerAddress { get; }

        private AddressEdit()
        {
            InitializeComponent();
        }

        public AddressEdit(Customer customer) : this()
        {
            Customer = customer;
            Session = customer.Session;
            CustomerAddress = new CustomerAddress(Session);
        }

        public AddressEdit(CustomerAddress customerAddress) : this()
        {
            CustomerAddress = customerAddress;
            Customer = customerAddress.Customer;
            Session = customerAddress.Session;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            CustomerAddress.AddressString = txtAddress.Text;
            CustomerAddress.Region = txtRegion.Text;
            CustomerAddress.Area = txtArea.Text;
            CustomerAddress.Locality = txtLocality.Text;
            //Address.House = txtHome.Text;
            CustomerAddress.Home = txtHome.Text;
            CustomerAddress.Street = txtStructure.Text;
            //Address.Flat = txtApartment.Text;
            CustomerAddress.Apartment = txtApartment.Text;
            //Address.City = txtTown.Text;
            CustomerAddress.Town = txtTown.Text;
            CustomerAddress.Street = txtStreet.Text;
            CustomerAddress.Body = txtBody.Text;
            CustomerAddress.Postcode = txtPostcode.Text;
            CustomerAddress.Office = txtOffice.Text;            
            CustomerAddress.Date = txtDate.DateTime == DateTime.MinValue ? default(DateTime?) : Convert.ToDateTime(txtDate.EditValue);

            Customer.CustomerAddress.Add(CustomerAddress);
            Customer.Save();
            Close();
        }

        private void AddressEdit_Load(object sender, EventArgs e)
        {
            FillAddress();
            txtAddress.TextChanged += lookUpAddress_TextChanged;
            txtAddress.EditValueChanged += txtAddress_EditValueChanged;
        }
        
        private void FillAddress(bool isUpdateAddress = true)
        {
            foreach (var item in gpAddress.Controls)
            {
                if (item is TextEdit textEdit)
                {
                    textEdit.EditValue = null;
                }
                else if (item is ButtonEdit buttonEdit)
                {
                    buttonEdit.EditValue = null;
                }
            }
            
            if (!string.IsNullOrEmpty(CustomerAddress.CityDistrict))
            {
                txtArea.Text = CustomerAddress.CityDistrict;
                buttonEdit2.Text = CustomerAddress.CityDistrictType;
            }
            else
            {
                txtArea.Text = CustomerAddress.Area;
                buttonEdit2.Text = CustomerAddress.AreaType;
            }

            if (!string.IsNullOrWhiteSpace(CustomerAddress.Street))
            {
                txtStreet.Text = CustomerAddress.Street;
                buttonEdit4.EditValue = CustomerAddress.StreetType;
            }
            else
            {
                txtStructure.Text = CustomerAddress.Street;
            }

            if (!string.IsNullOrWhiteSpace(CustomerAddress.City))
            {
                txtTown.Text = CustomerAddress.City;
                buttonEdit5.EditValue = CustomerAddress.CityType;
            }
            else if (!string.IsNullOrWhiteSpace(CustomerAddress.Settlement))
            {
                txtLocality.Text = CustomerAddress.Settlement;
                buttonEdit3.EditValue = CustomerAddress.SettlementType;
            }
            
            txtRegion.Text = CustomerAddress.Region;
            
            txtHome.Text = CustomerAddress.Home;
            
            txtApartment.Text = CustomerAddress.Apartment;
            txtBody.Text = CustomerAddress.Block;
            txtPostcode.Text = CustomerAddress.Postcode;
            txtOffice.Text = CustomerAddress.Office;
            txtDate.EditValue = CustomerAddress.Date;

            txtAccountantResponsible.EditValue = CustomerAddress.RegionType;
            

            if (CustomerAddress != null && CustomerAddress.RegionKladrId?.Length > 2)
            {
                buttonEdit1.EditValue = CustomerAddress.RegionKladrId.Substring(0,2);
            }

            if (isUpdateAddress)
            {
                txtAddress.Text = CustomerAddress.AddressString;
            }
        }

        private void txtAddress_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonedit = sender as ButtonEdit;

            if (e?.Button?.Kind == ButtonPredefines.Search)
            {
                var suggestResponse = GetInfoAddressFromDaData.UpdateFromDaData("a8cfa20b99fb660c53b8e0fa2d3de0f2204fb580", buttonedit.Text);
                var address = suggestResponse?.suggestions[0]?.data;

                FillingAddress(address, suggestResponse);
            }
        }

        private void FillingAddress(Dadata.Model.Address address, Dadata.Model.SuggestResponse<Dadata.Model.Address> suggestResponse = null)
        {
            if (address != null)
            {
                CustomerAddress.PostalCode = address.postal_code;
                CustomerAddress.Country = address.country;
                CustomerAddress.CountryIsoCode = address.country_iso_code;
                CustomerAddress.FederalDistrict = address.federal_district;
                CustomerAddress.RegionFiasId = address.region_fias_id;
                CustomerAddress.RegionKladrId = address.region_kladr_id;
                CustomerAddress.RegionIsoCode = address.region_iso_code;
                CustomerAddress.RegionWithType = address.region_with_type;
                CustomerAddress.RegionType = address.region_type;
                CustomerAddress.RegionTypeFull = address.region_type_full;
                CustomerAddress.Region = address.region;
                CustomerAddress.AreaFiasId = address.area_fias_id;
                CustomerAddress.AreaKladrId = address.area_kladr_id;
                CustomerAddress.AreaWithType = address.area_with_type;
                CustomerAddress.AreaType = address.area_type;
                CustomerAddress.AreaTypeFull = address.area_type_full;
                CustomerAddress.Area = address.area;
                CustomerAddress.CityFiasId = address.city_fias_id;
                CustomerAddress.CityKladrId = address.city_kladr_id;
                CustomerAddress.CityWithType = address.city_with_type;
                CustomerAddress.CityType = address.city_type;
                CustomerAddress.CityTypeFull = address.city_type_full;
                CustomerAddress.City = address.city;
                CustomerAddress.СityArea = address.city_area;
                CustomerAddress.CityDistrictFiasId = address.city_district_fias_id;
                CustomerAddress.CityDistrictKladrId = address.city_district_kladr_id;
                CustomerAddress.CityDistrictWithType = address.city_district_with_type;
                CustomerAddress.CityDistrictType = address.city_district_type;
                CustomerAddress.CityDistrictTypeFull = address.city_district_type_full;
                CustomerAddress.CityDistrict = address.city_district;
                CustomerAddress.SettlementFiasId = address.settlement_fias_id;
                CustomerAddress.SettlementKladrId = address.settlement_kladr_id;
                CustomerAddress.SettlementType = address.settlement_type;
                CustomerAddress.SettlementTypeFull = address.settlement_type_full;
                CustomerAddress.Settlement = address.settlement;
                CustomerAddress.StreetFiasId = address.street_fias_id;
                CustomerAddress.StreetKladrId = address.street_kladr_id;
                CustomerAddress.StreetWithType = address.street_with_type;
                CustomerAddress.StreetType = address.street_type;
                CustomerAddress.StreetTypeFull = address.street_type_full;
                CustomerAddress.Street = address.street;
                CustomerAddress.HouseFiasId = address.house_fias_id;
                CustomerAddress.HouseKladrId = address.house_kladr_id;
                CustomerAddress.HouseType = address.house_type;
                CustomerAddress.HouseTypeFull = address.house_type_full;
                CustomerAddress.House = address.house;
                CustomerAddress.BlockType = address.block_type;
                CustomerAddress.BlockTypeFull = address.block_type_full;
                CustomerAddress.Block = address.block;
                CustomerAddress.FlatType = address.flat_type;
                CustomerAddress.FlatTypeFull = address.flat_type_full;
                CustomerAddress.Flat = address.flat;
                CustomerAddress.SquareMeterPrice = address.square_meter_price;
                CustomerAddress.FlatPrice = address.flat_price;
                CustomerAddress.PostalBox = address.postal_box;
                CustomerAddress.FiasId = address.fias_id;
                CustomerAddress.FiasCode = address.fias_code;
                CustomerAddress.FiasLevel = address.fias_level;
                CustomerAddress.FiasActualityState = address.fias_actuality_state;
                CustomerAddress.KladrId = address.kladr_id;
                CustomerAddress.CapitalMarker = address.capital_marker;
                CustomerAddress.Okato = address.okato;
                CustomerAddress.Oktmo = address.oktmo;
                CustomerAddress.TaxOffice = address.tax_office;
                CustomerAddress.TaxOfficeLegal = address.tax_office_legal;
                CustomerAddress.Timezone = address.timezone;
                CustomerAddress.GeoLat = address.geo_lat;
                CustomerAddress.GeoLon = address.geo_lon;
                CustomerAddress.BeltwayHit = address.beltway_hit;
                CustomerAddress.BeltwayDistance = address.beltway_distance;
                CustomerAddress.QcGeo = address.qc_geo;

                CustomerAddress.Town = address.city;
                CustomerAddress.Home = address.house;
                CustomerAddress.Apartment = address.flat;
                CustomerAddress.Postcode = address.postal_code;

                CustomerAddress.Locality = address.city;
                //Address.Structure = addres.flat;
                //Address.Body = addres.flat;
                //Address.Office = addres.flat;

                if (suggestResponse != null)
                {
                    CustomerAddress.AddressString = suggestResponse.suggestions[0].unrestricted_value;
                    FillAddress();
                }
                else
                {
                    CustomerAddress.AddressString = txtAddress.Text;
                    FillAddress(false);
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
                    FillingAddress(s);
                    isEditValueChanged = true;
                }
            }
        }
    }
}