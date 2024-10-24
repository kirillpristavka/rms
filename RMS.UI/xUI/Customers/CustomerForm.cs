using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraRichEdit.Model;
using RMS.Core.ObjectDTO.Controllers;
using RMS.Core.ObjectDTO.Models;
using RMS.UI.Control.Customers;
using RMS.UI.xUI.BaseControl;
using RMS.UI.xUI.ObjUIControllers;
using System;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.xUI.Customers
{
    public partial class CustomerForm : XtraForm
    {
        private CustomerDTO _customer;

        private ObjUIGridController<CustomerFilterDTO> _objUIGridCustomerFilters;
        private ObjUIGridController<CustomerDTO> _objUIGridCustomers;

        public CustomerForm()
        {
            InitializeComponent();
        }

        private async void CustomerForm_Load(object sender, EventArgs e)
        {
            _objUIGridCustomerFilters = ObjUIController.CreateObjUIControl(layoutControlGroupCustomerFilter, await ObjDTOController<CustomerFilterDTO, Core.Model.InfoCustomer.CustomerFilter>.GetItemsAsync());
            _objUIGridCustomerFilters.FocusedRowChangedEvent += _objUIGridCustomerFilters_FocusedRowChangedEvent;

            _objUIGridCustomers = ObjUIController.CreateObjUIControl(layoutControlGroupCustomer, await CustomerDTOController.GetCustomersAsync());
            _objUIGridCustomers.FocusedRowChangedEvent += _objUIGridCustomers_FocusedRowChangedEvent;

            await InitialFillingOfTables();
        }


        private void _objUIGridCustomerFilters_FocusedRowChangedEvent(CustomerFilterDTO obj, int focusedRowHandle)
        {
            //throw new NotImplementedException();
        }

        private async void _objUIGridCustomers_FocusedRowChangedEvent(CustomerDTO obj, int focusedRowHandle)
        {
            _customer = obj;
            await InitialFillingOfTables();
        }

        private async void tabbedControlGroupCustomerInformation_SelectedPageChanged(object sender, LayoutTabPageChangedEventArgs e)
        {
            await InitialFillingOfTables();
        }

        private async System.Threading.Tasks.Task InitialFillingOfTables()
        {
            switch (tabbedControlGroupCustomerInformation.SelectedTabPage.Name)
            {
                case "layoutControlGroupGeneralInformation":

                    var objUIControl = default(RichEditBaseControl);
                    var baseLayoutItem = layoutControlGroupGeneralInformation.Items.FirstOrDefault(f => f.Text.Equals(nameof(objUIControl)));
                    if (baseLayoutItem is null)
                    {
                        objUIControl = (RichEditBaseControl)Activator.CreateInstance(typeof(RichEditBaseControl));
                        var item = layoutControlGroupGeneralInformation.AddItem(nameof(objUIControl));
                        item.Control = objUIControl;
                    }
                    else
                    {
                        objUIControl = (RichEditBaseControl)((LayoutControlItem)baseLayoutItem).Control;
                    }
                    objUIControl.SetValue(_customer?.Notes ?? _customer?.NoteByte);
                    break;

                case "layoutControlGroupDossier":

                    //var objUIControl1 = default(DossierControl);
                    //var baseLayoutItem1 = layoutControlGroupDossier.Items.FirstOrDefault(f => f.Text.Equals(nameof(objUIControl)));
                    //if (baseLayoutItem1 is null)
                    //{
                    //    objUIControl1 = (DossierControl)Activator.CreateInstance(typeof(DossierControl));
                    //    var item = layoutControlGroupDossier.AddItem(nameof(objUIControl));
                    //    item.Control = objUIControl1;
                    //}
                    //else
                    //{
                    //    objUIControl1 = (DossierControl)((LayoutControlItem)baseLayoutItem1).Control;
                    //}
                    //await objUIControl1.UpdateAsync(_customer?.Oid);

                    break;
            }
        }
    }
}