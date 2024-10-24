using DevExpress.Xpo;
using DevExpress.XtraEditors;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using System;
using System.Windows.Forms;

namespace RMS.UI.Forms.Directories
{
    public partial class ForeignEconomicActivityEdit : XtraForm
    {
        private Session Session { get; }
        private Customer Customer { get; }
        public ForeignEconomicActivity ForeignEconomicActivity { get; }

        public ForeignEconomicActivityEdit()
        {
            InitializeComponent();

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                ForeignEconomicActivity = new ForeignEconomicActivity(Session);
            }
        }

        public ForeignEconomicActivityEdit(Customer customer) : this()
        {
            Customer = customer;
            Session = customer.Session;
            ForeignEconomicActivity = new ForeignEconomicActivity(Session);
        }

        public ForeignEconomicActivityEdit(ForeignEconomicActivity foreignEconomicActivity) : this()
        {
            ForeignEconomicActivity = foreignEconomicActivity;
            Customer = foreignEconomicActivity.Customer;
            Session = foreignEconomicActivity.Session;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dateSince.EditValue is DateTime since)
            {
                ForeignEconomicActivity.DateSince = since;
            }
            else
            {
                XtraMessageBox.Show("Невозможно сохранить без заполненного поля [Дата с].", "Установите дату", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dateSince.Focus();
                return;
            }

            if (dateTo.EditValue is DateTime to)
            {
                if (to < since)
                {
                    ForeignEconomicActivity.DateTo = new DateTime(since.Year, since.Month, 1).AddMonths(1).AddDays(-1);
                }
                else
                {
                    ForeignEconomicActivity.DateTo = to;
                }
                
            }
            else
            {
                ForeignEconomicActivity.DateTo = new DateTime(since.Year, since.Month, 1).AddMonths(1).AddDays(-1);
            }

            ForeignEconomicActivity.Description = txtDescription.Text;
            ForeignEconomicActivity.WhereToOrFrom = txtWhereToOrFrom.Text;
            ForeignEconomicActivity.Act = txtAct.Text;

            if (Customer != null)
            {
                Customer.ForeignEconomicActivities.Add(ForeignEconomicActivity);
            }

            ForeignEconomicActivity.Save();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TaskEdit_Load(object sender, EventArgs e)
        {
            var dateNow = DateTime.Now.Date;
            
            txtAct.Text = ForeignEconomicActivity.Act;
            txtDescription.Text = ForeignEconomicActivity.Description;

            if (ForeignEconomicActivity.DateSince is DateTime since)
            {
                dateSince.EditValue = since;
            }
            else
            {
                dateSince.EditValue = dateNow;
            }            

            if (ForeignEconomicActivity.DateTo is DateTime to)
            {
                dateTo.EditValue = to;
            }
            else
            {
                if (dateSince.EditValue is DateTime _since)
                {
                    dateTo.EditValue = new DateTime(_since.Year, _since.Month, 1).AddMonths(1).AddDays(-1);
                }
                else
                {
                    dateTo.EditValue = dateNow;
                }
            }           
            
            txtWhereToOrFrom.Text = ForeignEconomicActivity.WhereToOrFrom;
        }
    }
}