using DevExpress.Xpo;
using RMS.Core.Model.OKVED;
using System;

namespace RMS.UI.Forms.ReferenceBooks
{
    public partial class SectionOKVED2Edit : formEdit_BaseSpr
    {
        private Session Session { get; }
        private SectionOKVED2 SectionOKVED2 { get; }

        public SectionOKVED2Edit()
        {
            InitializeComponent();

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                SectionOKVED2 = new SectionOKVED2(Session);
            }
        }

        public SectionOKVED2Edit(int id) : this()
        {
            if (id > 0)
            {
                SectionOKVED2 = Session.GetObjectByKey<SectionOKVED2>(id);
            }
        }

        public SectionOKVED2Edit(SectionOKVED2 sectionOKVED2) : this()
        {
            Session = sectionOKVED2.Session;
            SectionOKVED2 = sectionOKVED2;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SectionOKVED2.Code = txtCode.Text;
            SectionOKVED2.Name = txtName.Text;
            SectionOKVED2.Description = memoDescription.Text;

            Session.Save(SectionOKVED2);
            id = SectionOKVED2.Oid;
            flagSave = true;
            Close();
        }

        private void PositionEdit_Load(object sender, EventArgs e)
        {
            memoDescription.Text = SectionOKVED2.Description;
            txtName.Text = SectionOKVED2.Name;
            txtCode.Text = SectionOKVED2.Code;
        }
    }
}