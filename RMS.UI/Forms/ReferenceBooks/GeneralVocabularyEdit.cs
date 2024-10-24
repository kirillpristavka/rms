using DevExpress.Xpo;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model;
using System;

namespace RMS.UI.Forms.ReferenceBooks
{
    public partial class GeneralVocabularyEdit : formEdit_BaseSpr
    {
        private Session Session { get; }
        private GeneralVocabulary GeneralVocabulary { get; }

        public GeneralVocabularyEdit()
        {
            InitializeComponent();

            foreach (GeneralVocabularyType item in Enum.GetValues(typeof(GeneralVocabularyType)))
            {
                cmbGeneralVocabularyType.Properties.Items.Add(item.GetEnumDescription());
            }           

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                GeneralVocabulary = new GeneralVocabulary(Session);
            }
        }

        public GeneralVocabularyEdit(int id) : this()
        {
            if (id > 0)
            {
                GeneralVocabulary = Session.GetObjectByKey<GeneralVocabulary>(id);
            }
        }

        public GeneralVocabularyEdit(GeneralVocabulary generalVocabulary) : this()
        {
            Session = generalVocabulary.Session;
            GeneralVocabulary = generalVocabulary;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            GeneralVocabulary.Name = txtName.Text;
            GeneralVocabulary.Description = memoDescription.Text;

            foreach (GeneralVocabularyType type in Enum.GetValues(typeof(GeneralVocabularyType)))
            {
                if (type.GetEnumDescription().Equals(cmbGeneralVocabularyType.Text))
                {
                    GeneralVocabulary.GeneralVocabularyType = type;
                    break;
                }
            }

            Session.Save(GeneralVocabulary);
            id = GeneralVocabulary.Oid;
            flagSave = true;
            Close();
        }

        private void PositionEdit_Load(object sender, EventArgs e)
        {
            if (GeneralVocabulary.GeneralVocabularyType == 0)
            {
                cmbGeneralVocabularyType.SelectedIndex = 0;
            }
            else
            {
                cmbGeneralVocabularyType.EditValue = GeneralVocabulary.GeneralVocabularyType.GetEnumDescription();
            }
            
            memoDescription.EditValue = GeneralVocabulary.Description;
            txtName.EditValue = GeneralVocabulary.Name;            
        }
    }
}