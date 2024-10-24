using DevExpress.Xpo;
using RMS.Core.Model;
using System;

namespace RMS.UI.Forms.ReferenceBooks
{
    public partial class ElectronicReportingEdit : formEdit_BaseSpr
    {
        private Session Session { get; }
        private ElectronicReporting ElectronicReporting { get; }

        public ElectronicReportingEdit()
        {
            InitializeComponent();

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                ElectronicReporting = new ElectronicReporting(Session);
            }
        }

        public ElectronicReportingEdit(int id) : this()
        {
            if (id > 0)
            {
                ElectronicReporting = Session.GetObjectByKey<ElectronicReporting>(id);
            }
        }

        public ElectronicReportingEdit(ElectronicReporting electronicReporting) : this()
        {
            Session = electronicReporting.Session;
            ElectronicReporting = electronicReporting;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ElectronicReporting.Name = txtName.Text;
            ElectronicReporting.Description = txtDescription.Text;

            Session.Save(ElectronicReporting);
            id = ElectronicReporting.Oid;
            flagSave = true;
            Close();
        }

        private void PositionEdit_Load(object sender, EventArgs e)
        {
            txtDescription.Text = ElectronicReporting.Description;
            txtName.Text = ElectronicReporting.Name;
        }
    }
}