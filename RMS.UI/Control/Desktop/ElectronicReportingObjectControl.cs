using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Taxes;
using RMS.UI.Forms.Directories;
using RMS.UI.Forms.ReferenceBooks;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Control.Desktop
{
    public partial class ElectronicReportingObjectControl : UserControl
    {
        private DateTime dateTo;
        private Customer customer;
        private ElectronicReportingСustomerObject electronicReportingCustomerObject;
        
        public bool IsCongratulate { get; private set; }        

        public delegate void ElectronicReportingСustomerObjectEditEventHandler(object sender);
        public event ElectronicReportingСustomerObjectEditEventHandler ElectronicReportingСustomerObjectEditEvent;

        public ElectronicReportingObjectControl(Customer customer, ElectronicReportingСustomerObject electronicReportingCustomerObject, DateTime dateTo)
        {
            InitializeComponent();
            
            this.customer = customer;
            this.electronicReportingCustomerObject = electronicReportingCustomerObject;
            this.dateTo = dateTo;
        }

        private void TaskControl_Load(object sender, EventArgs e)
        {
            ReloadControl();
            IsCongratulate = true;
        }

        public void ReloadControl()
        {
            try
            {
                electronicReportingCustomerObject?.Reload();

                lblCustomer.Text = customer.ToString();

                if (electronicReportingCustomerObject.DateTo is DateTime dateTime)
                {
                    lblDescription.Text = $"Окончание действия: {dateTime.ToShortDateString()}";
                }
                else
                {
                    lblDescription.Text = default;
                }

                pictureEdit.Visible = false;
                btnCongratulate.Visible = true;

                var lastObj = electronicReportingCustomerObject.ElectronicReportingCustomer.ElectronicReportingСustomerObjects.FirstOrDefault(f => f.DateTo is null || f.DateTo > dateTo);
                if (lastObj != null)
                {
                    IsCongratulate = false;
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private void lblTask_MouseEnter(object sender, EventArgs e)
        {
            var label = sender as LabelControl;
            if (label != null)
            {
                foreach (var item in Controls)
                {
                    if (item is LabelControl labelControl)
                    {
                        labelControl.Font = new Font(labelControl.Font.Name, labelControl.Font.SizeInPoints, FontStyle.Underline);
                    }
                }
            }
        }

        private void lblTask_MouseLeave(object sender, EventArgs e)
        {
            var label = sender as LabelControl;
            if (label != null)
            {
                foreach (var item in Controls)
                {
                    if (item is LabelControl labelControl)
                    {
                        labelControl.Font = new Font(labelControl.Font.Name, labelControl.Font.SizeInPoints, FontStyle.Regular);
                    }
                }
            }
        }

        private void btnCongratulate_Click(object sender, EventArgs e)
        {            
            var form = new ElectronicReportingCustomerEdit2(customer);
            form.ShowDialog();
            ElectronicReportingСustomerObjectEditEvent?.Invoke(this);
        }

        private void lblCustomer_Click(object sender, EventArgs e)
        {
            if (customer != null)
            {
                var form = new CustomerEdit(customer);
                form.ShowDialog();                
            }
        }
    }
}
