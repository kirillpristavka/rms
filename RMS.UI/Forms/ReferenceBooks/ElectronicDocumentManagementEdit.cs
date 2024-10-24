using DevExpress.Xpo;
using RMS.Core.Model.ElectronicDocumentsManagement;
using System;

namespace RMS.UI.Forms.ReferenceBooks
{
    public partial class ElectronicDocumentManagementEdit : formEdit_BaseSpr
    {
        private Session Session { get; }
        private ElectronicDocumentManagement ElectronicDocumentManagement { get; }

        public ElectronicDocumentManagementEdit()
        {
            InitializeComponent();

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                ElectronicDocumentManagement = new ElectronicDocumentManagement(Session);
            }
        }

        public ElectronicDocumentManagementEdit(int id) : this()
        {
            if (id > 0)
            {
                ElectronicDocumentManagement = Session.GetObjectByKey<ElectronicDocumentManagement>(id);
            }
        }

        public ElectronicDocumentManagementEdit(ElectronicDocumentManagement obj) : this()
        {
            Session = obj.Session;
            ElectronicDocumentManagement = obj;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ElectronicDocumentManagement.Name = txtName.Text;
            ElectronicDocumentManagement.Description = txtDescription.Text;

            Session.Save(ElectronicDocumentManagement);
            id = ElectronicDocumentManagement.Oid;
            flagSave = true;
            Close();
        }

        private void PositionEdit_Load(object sender, EventArgs e)
        {
            txtDescription.Text = ElectronicDocumentManagement.Description;
            txtName.Text = ElectronicDocumentManagement.Name;
        }
    }
}