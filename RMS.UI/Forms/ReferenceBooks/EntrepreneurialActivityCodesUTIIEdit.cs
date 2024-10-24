using DevExpress.Xpo;
using RMS.Core.Model;
using RMS.Core.Model.OKVED;
using System;

namespace RMS.UI.Forms.ReferenceBooks
{
    public partial class EntrepreneurialActivityCodesUTIIEdit : formEdit_BaseSpr
    {
        private Session Session { get; }
        private EntrepreneurialActivityCodesUTII EntrepreneurialActivityCodesUTII { get; }

        public EntrepreneurialActivityCodesUTIIEdit()
        {
            InitializeComponent();

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                EntrepreneurialActivityCodesUTII = new EntrepreneurialActivityCodesUTII(Session);
            }
        }

        public EntrepreneurialActivityCodesUTIIEdit(int id) : this()
        {
            if (id > 0)
            {
                EntrepreneurialActivityCodesUTII = Session.GetObjectByKey<EntrepreneurialActivityCodesUTII>(id);
            }
        }

        public EntrepreneurialActivityCodesUTIIEdit(EntrepreneurialActivityCodesUTII entrepreneurialActivityCodesUtii) : this()
        {
            Session = entrepreneurialActivityCodesUtii.Session;
            EntrepreneurialActivityCodesUTII = entrepreneurialActivityCodesUtii;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            EntrepreneurialActivityCodesUTII.Code = txtCode.Text;
            EntrepreneurialActivityCodesUTII.Name = txtName.Text;
            EntrepreneurialActivityCodesUTII.Description = memoDescription.Text;

            Session.Save(EntrepreneurialActivityCodesUTII);
            id = EntrepreneurialActivityCodesUTII.Oid;
            flagSave = true;
            Close();
        }

        private void PositionEdit_Load(object sender, EventArgs e)
        {
            memoDescription.Text = EntrepreneurialActivityCodesUTII.Description;
            txtName.Text = EntrepreneurialActivityCodesUTII.Name;
            txtCode.Text = EntrepreneurialActivityCodesUTII.Code;
        }
    }
}