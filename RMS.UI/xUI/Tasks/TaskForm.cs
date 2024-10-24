using DevExpress.XtraEditors;
using RMS.Core.Model.InfoCustomer;
using RMS.UI.xUI.ObjUIControllers;
using System;

namespace RMS.UI.xUI.Tasks
{
    public partial class TaskForm : XtraForm
    {
        public TaskForm()
        {
            InitializeComponent();
        }

        private void TaskForm_Load(object sender, EventArgs e)
        {
            ObjUIController.CreateObjUIControl<Customer>(layoutControlGroupTask);
            ObjUIController.CreateObjUIControl<CustomerFilter>(layoutControlGroupCustomerFilter);
        }
    }
}