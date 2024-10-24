using DevExpress.Xpo;
using RMS.Core.Model;
using System;

namespace RMS.UI.Forms.ReferenceBooks
{
    public partial class PhysicalIndicatorEdit : formEdit_BaseSpr
    {
        private Session Session { get; }
        private PhysicalIndicator PhysicalIndicator { get; }

        public PhysicalIndicatorEdit()
        {
            InitializeComponent();

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                PhysicalIndicator = new PhysicalIndicator(Session);
            }
        }

        public PhysicalIndicatorEdit(int id) : this()
        {
            if (id > 0)
            {
                PhysicalIndicator = Session.GetObjectByKey<PhysicalIndicator>(id);
            }
        }

        public PhysicalIndicatorEdit(PhysicalIndicator physicalIndicator) : this()
        {
            Session = physicalIndicator.Session;
            PhysicalIndicator = physicalIndicator;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            PhysicalIndicator.Name = txtName.Text;
            PhysicalIndicator.Description = memoDescription.Text;

            Session.Save(PhysicalIndicator);
            id = PhysicalIndicator.Oid;
            flagSave = true;
            Close();
        }

        private void PositionEdit_Load(object sender, EventArgs e)
        {
            memoDescription.Text = PhysicalIndicator.Description;
            txtName.Text = PhysicalIndicator.Name;
        }
    }
}