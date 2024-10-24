using DevExpress.Xpo;
using RMS.Core.Model.Salary;
using System;

namespace RMS.UI.Forms.Salary
{
    public partial class PayoutDictionaryEdit : formEdit_BaseSpr
    {
        private Session Session { get; }
        private PayoutDictionary PayoutDictionary { get; }

        public PayoutDictionaryEdit()
        {
            InitializeComponent();

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                PayoutDictionary = new PayoutDictionary(Session);
            }
        }

        public PayoutDictionaryEdit(int id) : this()
        {
            if (id > 0)
            {
                PayoutDictionary = Session.GetObjectByKey<PayoutDictionary>(id);
            }
        }

        public PayoutDictionaryEdit(PayoutDictionary payoutDictionary) : this()
        {
            Session = payoutDictionary.Session;
            PayoutDictionary = payoutDictionary;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            PayoutDictionary.Name = txtName.Text;
            PayoutDictionary.Description = memoDescription.Text;

            Session.Save(PayoutDictionary);
            id = PayoutDictionary.Oid;
            flagSave = true;
            Close();
        }

        private void PayoutDictionaryEdit_Load(object sender, EventArgs e)
        {
            memoDescription.Text = PayoutDictionary.Description;
            txtName.Text = PayoutDictionary.Name;
        }
    }
}