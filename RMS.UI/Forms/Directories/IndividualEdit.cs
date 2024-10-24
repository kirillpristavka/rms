using DevExpress.Xpo;
using RMS.Core.Model;
using System;

namespace RMS.UI.Forms.Directories
{
    public partial class IndividualEdit : formEdit_BaseSpr
    {
        private Session Session { get; }
        private Individual Individual { get; }

        public IndividualEdit()
        {
            InitializeComponent();

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                Individual = new Individual(Session);
            }
        }

        public IndividualEdit(int id) : this()
        {
            if (id > 0)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                Individual = Session.GetObjectByKey<Individual>(id);
            }
        }

        public IndividualEdit(Individual individual) : this()
        {
            Session = individual.Session;
            Individual = individual;
        }

        private void IndividualEdit_Load(object sender, EventArgs e)
        {
            txtPatronymic.Text = Individual.Patronymic;
            txtName.Text = Individual.Name;
            txtSurname.Text = Individual.Surname;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Individual.Surname = txtSurname.Text;
            Individual.Name = txtName.Text;
            Individual.Patronymic = txtPatronymic.Text;

            Session.Save(Individual);
            id = Individual.Oid;
            flagSave = true;
            Close();
        }
    }
}