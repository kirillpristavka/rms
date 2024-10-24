using DevExpress.Xpo;
using RMS.Core.Model;
using System;

namespace RMS.UI.Forms.ReferenceBooks
{
    public partial class CostItemEdit : formEdit_BaseSpr
    {
        private Session Session { get; }
        private CostItem CostItem { get; }

        public CostItemEdit()
        {
            InitializeComponent();

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                CostItem = new CostItem(Session);
            }
        }

        public CostItemEdit(int id) : this()
        {
            if (id > 0)
            {
                CostItem = Session.GetObjectByKey<CostItem>(id);
            }
        }

        public CostItemEdit(CostItem costItem) : this()
        {
            Session = costItem.Session;
            CostItem = costItem;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            CostItem.Name = txtName.Text;
            CostItem.Description = memoDescription.Text;

            Session.Save(CostItem);
            id = CostItem.Oid;
            flagSave = true;
            Close();
        }

        private void PositionEdit_Load(object sender, EventArgs e)
        {
            memoDescription.Text = CostItem.Description;
            txtName.Text = CostItem.Name;
        }
    }
}