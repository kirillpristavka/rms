using DevExpress.Xpo;
using RMS.Core.Model;
using System;

namespace RMS.UI.Forms.ReferenceBooks
{
    public partial class TypePaymentEdit : formEdit_BaseSpr
    {
        private Session Session { get; }
        private TypePayment TypePayment { get; }

        public TypePaymentEdit()
        {
            InitializeComponent();

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                TypePayment = new TypePayment(Session);
            }
        }

        public TypePaymentEdit(int id) : this()
        {
            if (id > 0)
            {
                TypePayment = Session.GetObjectByKey<TypePayment>(id);
            }
        }

        public TypePaymentEdit(TypePayment typePayment) : this()
        {
            Session = typePayment.Session;
            TypePayment = typePayment;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            TypePayment.Name = txtName.Text;
            TypePayment.Description = txtDescription.Text;

            Session.Save(TypePayment);
            id = TypePayment.Oid;
            flagSave = true;
            Close();
        }

        private void PositionEdit_Load(object sender, EventArgs e)
        {
            txtDescription.Text = TypePayment.Description;
            txtName.Text = TypePayment.Name;
        }
    }
}