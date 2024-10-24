using DevExpress.Xpo;
using RMS.Core.Model;
using System;
using System.Linq;

namespace RMS.UI.Forms.ReferenceBooks
{
    public partial class PriceListEdit : formEdit_BaseSpr
    {
        private Session Session { get; }
        private PriceList PriceList { get; }

        public PriceListEdit()
        {
            InitializeComponent();

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();

                var number = 1;
                var lastNunber = new XPQuery<PriceList>(Session).ToList()?.Max(m => m.Kod);
                if (lastNunber is int result)
                {
                    number = result + 1;
                }
                
                PriceList = new PriceList(Session)
                {
                    Kod = number
                };
            }
        }

        public PriceListEdit(int id) : this()
        {
            if (id > 0)
            {
                PriceList = Session.GetObjectByKey<PriceList>(id);
            }
        }

        public PriceListEdit(PriceList priceList) : this()
        {
            Session = priceList.Session;
            PriceList = priceList;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            PriceList.Kod = Convert.ToInt32(txtCode.Text);
            PriceList.Name = txtName.Text;
            PriceList.Description = memoDescription.Text;
            PriceList.Price = txtPrice.Value;

            Session.Save(PriceList);
            id = PriceList.Oid;
            flagSave = true;
            Close();
        }

        private void PositionEdit_Load(object sender, EventArgs e)
        {
            txtName.Text = PriceList.Name;
            txtCode.Text = PriceList.Kod.ToString();
            memoDescription.Text = PriceList.Description;
            txtPrice.Value = PriceList.Price;
        }
    }
}