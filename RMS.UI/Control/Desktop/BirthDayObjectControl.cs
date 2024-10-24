using DevExpress.Data.Filtering;
using DevExpress.XtraEditors;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Mail;
using RMS.UI.Forms.Directories;
using RMS.UI.Forms.Mail;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Control.Desktop
{
    public partial class BirthDayObjectControl : UserControl
    {
        private Customer customer;
        public bool IsCongratulate { get; private set; }

        public BirthDayObjectControl(Customer customer)
        {
            InitializeComponent();
            this.customer = customer;
        }

        private void TaskControl_Load(object sender, EventArgs e)
        {
            var dateTime = DateTime.Now;
            
            lblCustomer.Text = customer.ToString();
            lblDescription.Text = customer.DateManagementBirth.Value.ToShortDateString();
            pictureEdit.Visible = false;

            if (dateTime.Date.Month == customer.DateManagementBirth.Value.Date.Month 
                && dateTime.Date.Day == customer.DateManagementBirth.Value.Date.Day)
            {
                var chronicleCustomer = customer.ChronicleCustomers
                    ?.FirstOrDefault(f => 
                    f.Act == Core.Enumerator.Act.HAPPY_BIRTHDAY 
                    && f.Date.Date == dateTime.Date);
                
                if (chronicleCustomer is null)
                {
                    btnCongratulate.Visible = true;
                    IsCongratulate = true;
                }              
            }
            else
            {
                btnCongratulate.Visible = false;
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

        private async void btnCongratulate_Click(object sender, EventArgs e)
        {
            var letterTemplate = await customer.Session.FindObjectAsync<LetterTemplate>(new BinaryOperator(nameof(LetterTemplate.IsUseCongratulationsTemplate), true));
            if (letterTemplate is null)
            {
                if (XtraMessageBox.Show("Пожалуйста укажите шаблон для поздравлений в шаблонах писем.",
                                        "Не найден шаблон",
                                        MessageBoxButtons.OKCancel,
                                        MessageBoxIcon.Information) == DialogResult.OK)
                {
                    cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.LetterTemplate, -1);
                }
                
                return;
            }
            
            var form = new LetterEdit(customer, "С Днем Рождения!", letterTemplate);
            form.ShowDialog();
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
