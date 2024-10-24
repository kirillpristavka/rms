using DevExpress.XtraEditors;
using RMS.Core.Model.InfoCustomer;
using RMS.UI.xUI.ObjUIControllers;
using System;

namespace RMS.UI.xUI.Deals
{
    public partial class DealForm : XtraForm
    {
        public DealForm()
        {
            InitializeComponent();
        }

        private void DealForm_Load(object sender, EventArgs e)
        {
            ObjUIController.CreateObjUIControl<Customer>(layoutControlGroupDeal);
            ObjUIController.CreateObjUIControl<CustomerFilter>(layoutControlGroupCustomerFilter);
        }
    }
}