using DevExpress.Xpo;
using RMS.Core.Model.Reports;
using System;

namespace RMS.UI.Forms.Directories
{
    public partial class ReportEditV2 : formEdit_BaseSpr
    {
        private Session Session { get; }
        public ReportV2 Report { get; }

        public ReportEditV2()
        {
            InitializeComponent();

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                Report = new ReportV2(Session);
            }
        }

        public ReportEditV2(int id) : this()
        {
            if (id > 0)
            {
                Report = Session.GetObjectByKey<ReportV2>(id);
            }
        }

        public ReportEditV2(ReportV2 report) : this()
        {
            Session = report.Session;
            Report = report;
        }


        private void ReportEdit_Load(object sender, EventArgs e)
        {
            memoName.EditValue = Report.Name;
            memoDescription.EditValue = Report.Description;            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveReport())
            {
                id = Report.Oid;
                flagSave = true;
                Close();
            }
        }

        /// <summary>
        /// Сохранение отчета.
        /// </summary>
        private bool SaveReport()
        {
            Report.Name = memoName.Text;
            Report.Description = memoDescription.Text;

            Session.Save(Report);

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}