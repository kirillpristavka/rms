using DevExpress.Xpo;
using DevExpress.XtraEditors;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using System;

namespace RMS.UI.Forms.Directories
{
    public partial class SubdivisionEdit : XtraForm
    {
        private Session Session { get; }
        private Customer Customer { get; }
        private Subdivision Subdivision { get; }

        private SubdivisionEdit()
        {
            InitializeComponent();
        }

        public SubdivisionEdit(Customer customer) : this()
        {
            Customer = customer;
            Session = customer.Session;
            Subdivision = new Subdivision(Session);
        }

        public SubdivisionEdit(Subdivision subdivision) : this()
        {
            Subdivision = subdivision;
            Customer = subdivision.Customer;
            Session = subdivision.Session;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Subdivision.InputDate = dateInput.DateTime == DateTime.MinValue ? default(DateTime?) : Convert.ToDateTime(dateInput.EditValue);
            Subdivision.Name = txtName.Text;
            Subdivision.OKTMO = txtOKTMO.Text;
            Subdivision.KPP = txtKPP.Text;
            Subdivision.IFNS = txtIFNS.Text;
            Subdivision.IsSeparateDivision = checkIsSeparateDivision.Checked;

            Customer.Subdivisions.Add(Subdivision);
            Customer.Save();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AccountEdit_Load(object sender, EventArgs e)
        {
            btnCustomer.EditValue = Customer;
            dateInput.EditValue = Subdivision.InputDate;
            txtName.Text = Subdivision.Name;
            txtOKTMO.Text = Subdivision.OKTMO;
            txtKPP.Text = Subdivision.KPP;
            txtIFNS.Text = Subdivision.IFNS;
        }
    }
}