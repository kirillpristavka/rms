using DevExpress.Xpo;
using RMS.Core.Model;
using System;

namespace RMS.UI.Forms.Vacations
{
    public partial class VacationTypeEdit : formEdit_BaseSpr
    {
        private Session Session { get; }
        private VacationType VacationType { get; }

        public VacationTypeEdit()
        {
            InitializeComponent();

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                VacationType = new VacationType(Session);
            }
        }

        public VacationTypeEdit(int id) : this()
        {
            if (id > 0)
            {
                VacationType = Session.GetObjectByKey<VacationType>(id);
            }
        }

        public VacationTypeEdit(VacationType vacationType) : this()
        {
            Session = vacationType.Session;
            VacationType = vacationType;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            VacationType.Name = txtName.Text;
            VacationType.Description = memoDescription.Text;
            
            Session.Save(VacationType);
            id = VacationType.Oid;
            flagSave = true;
            Close();
        }

        private void PositionEdit_Load(object sender, EventArgs e)
        {
            memoDescription.Text = VacationType.Description;
            txtName.Text = VacationType.Name;
        }        
    }
}