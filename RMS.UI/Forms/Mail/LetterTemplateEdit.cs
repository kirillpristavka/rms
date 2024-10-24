using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using RMS.Core.Model.Mail;
using System;
using System.Windows.Forms;

namespace RMS.UI.Forms.Mail
{
    public partial class LetterTemplateEdit : formEdit_BaseSpr
    {
        private Session Session { get; }
        public LetterTemplate LetterTemplate { get; }

        public LetterTemplateEdit()
        {
            InitializeComponent();

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                LetterTemplate = new LetterTemplate(Session);
            }
        }

        public LetterTemplateEdit(int id) : this()
        {
            if (id > 0)
            {
                LetterTemplate = Session.GetObjectByKey<LetterTemplate>(id);
            }
        }

        public LetterTemplateEdit(LetterTemplate letterTemplate) : this()
        {
            Session = letterTemplate.Session;
            LetterTemplate = letterTemplate;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                XtraMessageBox.Show("Сохранение не возможно без указания наименования шаблона.", "Ошибка сохранения шаблона", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Focus();
                return;
            }

            LetterTemplate.Name = txtName.Text;
            LetterTemplate.Description = memoDescription.Text;
            
            if (!string.IsNullOrWhiteSpace(richLetterTemplate.HtmlText))
            {
                LetterTemplate.HtmlBody = Letter.StringToByte(richLetterTemplate.HtmlText);
                LetterTemplate.TextBody = Letter.StringToByte(richLetterTemplate.Text);
            }
            else if (!string.IsNullOrWhiteSpace(richLetterTemplate.Text))
            {
                LetterTemplate.TextBody = Letter.StringToByte(richLetterTemplate.Text);
                LetterTemplate.HtmlBody = Letter.StringToByte(richLetterTemplate.HtmlText);
            }

            if (checkIsUseCongratulationsTemplate.Checked)
            {
                using (var templates = new XPCollection<LetterTemplate>(Session, new BinaryOperator(nameof(LetterTemplate.IsUseCongratulationsTemplate), true)))
                {
                    foreach (var item in templates)
                    {
                        item.IsUseCongratulationsTemplate = false;
                        item.Save();
                    }
                }
            }

            LetterTemplate.IsUseCongratulationsTemplate = checkIsUseCongratulationsTemplate.Checked;

            Session.Save(LetterTemplate);
            id = LetterTemplate.Oid;
            flagSave = true;
            Close();
        }

        private void PositionEdit_Load(object sender, EventArgs e)
        {
            memoDescription.EditValue = LetterTemplate.Description;
            txtName.EditValue = LetterTemplate.Name;
            checkIsUseCongratulationsTemplate.EditValue = LetterTemplate.IsUseCongratulationsTemplate;
            
            if (LetterTemplate.HtmlBody != null)
            {
                richLetterTemplate.HtmlText = Letter.ByteToString(LetterTemplate.HtmlBody);
            }
            else if (LetterTemplate.TextBody != null)
            {
                richLetterTemplate.Text = Letter.ByteToString(LetterTemplate.TextBody);
            }
        }
    }
}