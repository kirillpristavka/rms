using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using RMS.Setting.Model.CustomerSettings;
using System;
using System.Windows.Forms;

namespace RMS.UI.Forms.ReferenceBooks
{
    public partial class CustomerSettingsEdit : formEdit_BaseSpr
    {
        private Session Session { get; }
        private CustomerSettings CustomerSettings { get; }

        public CustomerSettingsEdit()
        {
            InitializeComponent();

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                CustomerSettings = new CustomerSettings(Session);
            }
        }

        public CustomerSettingsEdit(int id) : this()
        {
            if (id > 0)
            {
                CustomerSettings = Session.GetObjectByKey<CustomerSettings>(id);
            }
        }

        public CustomerSettingsEdit(CustomerSettings customerSettings) : this()
        {
            Session = customerSettings.Session;
            CustomerSettings = customerSettings;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CustomerSettings.IsDefault)
            {
                XtraMessageBox.Show("Изменение стандартного шаблона запрещено.",
                    "Запрет сохранения",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            CustomerSettings.Name = txtName.Text;
            CustomerSettings.Description = memoDescription.Text;

            Session.Save(CustomerSettings);
            id = CustomerSettings.Oid;
            flagSave = true;
            Close();
        }

        private void PositionEdit_Load(object sender, EventArgs e)
        {
            memoDescription.Text = CustomerSettings.Description;
            txtName.Text = CustomerSettings.Name;

            propertyGridCustomerSettings.SelectedObject = CustomerSettings;
            RepositoryItemCheckEdit riCheckEdit = new RepositoryItemCheckEdit();
            riCheckEdit.CheckStyle = CheckStyles.Standard;
            propertyGridCustomerSettings.DefaultEditors.Add(typeof(bool), riCheckEdit);
        }
    }
}