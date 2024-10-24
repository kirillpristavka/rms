using DevExpress.Utils;
using DevExpress.XtraEditors;
using RMS.Core.Model.InfoCustomer;
using RMS.UI.Control.Customers;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMS.UI.Forms.Directories
{
    public partial class DossierEdit : XtraForm
    {
        private Customer _customer;
        private DossierControl _dossierControl;
        private ImageCollection _imageCollection;

        private DossierEdit()
        {
            InitializeComponent();
        }

        public DossierEdit(Customer customer) : this()
        {
            _customer = customer;
        }

        private async void DossierEdit_Load(object sender, EventArgs e)
        {
            Text += $" {_customer?.ToString()}";

            _dossierControl = new DossierControl();
            _dossierControl.Dock = DockStyle.Fill;
            _dossierControl.SetImageCollection(_imageCollection);
            await _dossierControl.UpdateAsync(_customer);
            Controls.Add(_dossierControl);
        }


        public void SetImageCollection(ImageCollection imageCollection)
        {
            _imageCollection = imageCollection;
        }

        public async Task RefreshAsync()
        {
            await _dossierControl.UpdateAsync(_customer);
        }
    }
}