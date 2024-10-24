using DevExpress.Xpo;
using DevExpress.XtraEditors;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using System;

namespace RMS.UI.Forms.Directories
{
    public partial class EmploymentHistoryEdit : XtraForm
    {
        private Session Session { get; }
        private Customer Customer { get; }
        private EmploymentHistory EmploymentHistory { get; }
        private CustomerEmploymentHistory CustomerEmploymentHistory { get; }

        private EmploymentHistoryEdit()
        {
            InitializeComponent();            
        }
        
        public EmploymentHistoryEdit(EmploymentHistory employmentHistory) : this()
        {
            Session = employmentHistory.Session;
            EmploymentHistory = employmentHistory ?? new EmploymentHistory(Session);
        }

        public EmploymentHistoryEdit(Customer customer) : this()
        {
            Customer = customer;
            Session = customer.Session;
            CustomerEmploymentHistory = new CustomerEmploymentHistory(Session);
        }

        public EmploymentHistoryEdit(CustomerEmploymentHistory customerEmploymentHistory) : this()
        {
            CustomerEmploymentHistory = customerEmploymentHistory;
            Customer = customerEmploymentHistory.Customer;
            Session = customerEmploymentHistory.Session;
        }

        private void StaffEdit_Load(object sender, EventArgs e)
        {
            if (CustomerEmploymentHistory is null)
            {
                txtSurname.Text = EmploymentHistory.Surname;
                txtName.Text = EmploymentHistory.Name;
                txtPatronymic.Text = EmploymentHistory.Patronymic;
                dateReceiving.EditValue = EmploymentHistory.DateReceiving;
                dateIssue.EditValue = EmploymentHistory.DateIssue;
            }
            else
            {
                txtSurname.Text = CustomerEmploymentHistory.Surname;
                txtName.Text = CustomerEmploymentHistory.Name;
                txtPatronymic.Text = CustomerEmploymentHistory.Patronymic;
                dateReceiving.EditValue = CustomerEmploymentHistory.DateReceiving;
                dateIssue.EditValue = CustomerEmploymentHistory.DateIssue;
            }            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CustomerEmploymentHistory is null)
            {
                EmploymentHistory.Surname = txtSurname.Text;
                EmploymentHistory.Name = txtName.Text;
                EmploymentHistory.Patronymic = txtPatronymic.Text;

                if (dateReceiving.EditValue is DateTime receiving)
                {
                    EmploymentHistory.DateReceiving = receiving;
                }
                else
                {
                    EmploymentHistory.DateReceiving = null;
                }

                if (dateIssue.EditValue is DateTime issue)
                {
                    EmploymentHistory.DateIssue = issue;
                }
                else
                {
                    EmploymentHistory.DateIssue = null;
                }

                EmploymentHistory.Save();
            }
            else
            {
                CustomerEmploymentHistory.Surname = txtSurname.Text;
                CustomerEmploymentHistory.Name = txtName.Text;
                CustomerEmploymentHistory.Patronymic = txtPatronymic.Text;

                if (dateReceiving.EditValue is DateTime receiving)
                {
                    CustomerEmploymentHistory.DateReceiving = receiving;
                }
                else
                {
                    CustomerEmploymentHistory.DateReceiving = null;
                }

                if (dateIssue.EditValue is DateTime issue)
                {
                    CustomerEmploymentHistory.DateIssue = issue;
                }
                else
                {
                    CustomerEmploymentHistory.DateIssue = null;
                }
                Customer.CustomerEmploymentHistorys.Add(CustomerEmploymentHistory);
                Customer.Save();
            }
            
            Close();
        }
    }
}