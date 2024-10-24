using DevExpress.Xpo;
using RMS.Core.Model;
using System;

namespace RMS.UI.Forms.ReferenceBooks
{
    public partial class DocumentEdit : formEdit_BaseSpr
    {
        private Session Session { get; }
        private Document Document { get; }

        public DocumentEdit()
        {
            InitializeComponent();

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                Document = new Document(Session);
            }
        }

        public DocumentEdit(int id) : this()
        {
            if (id > 0)
            {
                Document = Session.GetObjectByKey<Document>(id);
            }
        }

        public DocumentEdit(Document obj) : this()
        {
            Session = obj.Session;
            Document = obj;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Document.Name = txtName.Text;
            Document.Description = memoDescription.Text;

            Session.Save(Document);
            id = Document.Oid;
            flagSave = true;
            Close();
        }

        private void PositionEdit_Load(object sender, EventArgs e)
        {
            memoDescription.Text = Document.Description;
            txtName.Text = Document.Name;
        }
    }
}