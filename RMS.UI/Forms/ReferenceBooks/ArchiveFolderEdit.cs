using DevExpress.Xpo;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model;
using System;

namespace RMS.UI.Forms.ReferenceBooks
{
    public partial class ArchiveFolderEdit : formEdit_BaseSpr
    {
        private Session Session { get; }
        private ArchiveFolder ArchiveFolder { get; }

        public ArchiveFolderEdit()
        {
            InitializeComponent();

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                ArchiveFolder = new ArchiveFolder(Session);
            }

            foreach (PeriodArchiveFolder item in Enum.GetValues(typeof(PeriodArchiveFolder)))
            {
                cmbPeriodArchiveFolder.Properties.Items.Add(item.GetEnumDescription());
            }
            cmbPeriodArchiveFolder.SelectedIndex = 0;
        }

        public ArchiveFolderEdit(int id) : this()
        {
            if (id > 0)
            {
                ArchiveFolder = Session.GetObjectByKey<ArchiveFolder>(id);
            }
        }

        public ArchiveFolderEdit(ArchiveFolder archiveFolder) : this()
        {
            Session = archiveFolder.Session;
            ArchiveFolder = archiveFolder;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ArchiveFolder.Name = txtName.Text;
            ArchiveFolder.Description = memoDescription.Text;
            ArchiveFolder.PeriodArchiveFolder = (PeriodArchiveFolder)cmbPeriodArchiveFolder.SelectedIndex;

            Session.Save(ArchiveFolder);
            id = ArchiveFolder.Oid;
            flagSave = true;
            Close();
        }

        private void PositionEdit_Load(object sender, EventArgs e)
        {
            memoDescription.Text = ArchiveFolder.Description;
            txtName.Text = ArchiveFolder.Name;
            cmbPeriodArchiveFolder.SelectedIndex = Convert.ToInt32(ArchiveFolder.PeriodArchiveFolder);
        }
    }
}