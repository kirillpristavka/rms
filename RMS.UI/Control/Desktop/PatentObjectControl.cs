using DevExpress.XtraEditors;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Taxes;
using RMS.UI.Forms.Directories;
using RMS.UI.Forms.Taxes;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Control.Desktop
{
    public partial class PatentObjectControl : UserControl
    {
        private DateTime dateTo;
        private Customer customer;
        private PatentObject patentObject;
        
        public bool IsCongratulate { get; private set; }
        
        public delegate void PatentEditEventHandler(object sender);
        public event PatentEditEventHandler PatentEditEvent;

        public PatentObjectControl(Customer customer, PatentObject patentObject, DateTime dateTo)
        {
            InitializeComponent();
            
            this.customer = customer;
            this.patentObject = patentObject;
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
                patentObject?.Reload();

                lblCustomer.Text = $"{customer}";

                if (patentObject.DateTo is DateTime dateTime)
                {
                    lblCustomer.Text += $" до ({dateTime.ToShortDateString()})";
                }

                var description = patentObject.ClassOKVED2?.ToString();
                if (string.IsNullOrWhiteSpace(description))
                {
                    lblDescription.Text = "Не задан вид деятельности";
                }
                else
                {
                    lblDescription.Text = description;
                }

                pictureEdit.Visible = false;
                btnCongratulate.Visible = true;

                var lastObj = patentObject.Patent.PatentObjects.Where(w => w.ClassOKVED2 == patentObject.ClassOKVED2).FirstOrDefault(f => f.DateTo is null || f.DateTo > dateTo);
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
            var form = new PatentEdit2(customer.Tax.Patent, customer);
            form.ShowDialog();
            PatentEditEvent?.Invoke(this);
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
