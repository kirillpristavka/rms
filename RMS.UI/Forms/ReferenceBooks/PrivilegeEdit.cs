using DevExpress.Xpo;
using RMS.Core.Model;
using System;

namespace RMS.UI.Forms.ReferenceBooks
{
    public partial class PrivilegeEdit : formEdit_BaseSpr
    {
        private Session Session { get; }
        private Privilege Privilege { get; }

        public PrivilegeEdit()
        {
            InitializeComponent();

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                Privilege = new Privilege(Session);
            }
        }

        public PrivilegeEdit(int id) : this()
        {
            if (id > 0)
            {
                Privilege = Session.GetObjectByKey<Privilege>(id);
            }
        }

        public PrivilegeEdit(Privilege privilege) : this()
        {
            Session = privilege.Session;
            Privilege = privilege;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Privilege.Kod = Convert.ToInt32(txtKod.Text);
            Privilege.Name = txtName.Text;
            Privilege.Description = txtDescription.Text;

            Session.Save(Privilege);
            id = Privilege.Oid;
            flagSave = true;
            Close();
        }

        private void PositionEdit_Load(object sender, EventArgs e)
        {
            txtName.Text = Privilege.Name;
            txtKod.Text = Privilege.Kod.ToString();
            txtDescription.Text = Privilege.Description;
        }
    }
}