using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using RMS.Core.Model;
using RMS.Core.Model.Mail;
using System;
using System.Windows.Forms;

namespace RMS.UI.Forms.Mail
{
    public partial class LetterFilterEdit : formEdit_BaseSpr
    {
        private Session Session { get; }
        private LetterFilter LetterFilter { get; }

        public LetterFilterEdit()
        {
            InitializeComponent();

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                LetterFilter = new LetterFilter(Session);
            }
        }

        public LetterFilterEdit(int id) : this()
        {
            if (id > 0)
            {
                LetterFilter = Session.GetObjectByKey<LetterFilter>(id);
            }
        }

        public LetterFilterEdit(LetterFilter letterFilter) : this()
        {
            Session = letterFilter.Session;
            LetterFilter = letterFilter;
        }

        public LetterFilterEdit(LetterCatalog letterCatalog) : this()
        {
            Session = letterCatalog.Session;
            LetterFilter = new LetterFilter(Session)
            {
                LetterCatalog = letterCatalog
            };
        }

        public LetterFilterEdit(string email) : this()
        {
            LetterFilter.Email = email;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                return;
            }
            else
            {
                LetterFilter.Email = txtEmail.Text;
            }

            if (btnLetterCatalog.EditValue is LetterCatalog letterCatalog)
            {
                LetterFilter.LetterCatalog = Session.GetObjectByKey<LetterCatalog>(letterCatalog.Oid);
            }
            else
            {
                return;
            }

            Session.Save(LetterFilter);
            id = LetterFilter.Oid;
            flagSave = true;

            if (XtraMessageBox.Show($"Найти и перенести все письма удовлетворяющие условию в указанный каталог?",
                               "Обновление каталога",
                               MessageBoxButtons.OKCancel,
                               MessageBoxIcon.Question) == DialogResult.OK)
            {
                var letters = new XPCollection<Letter>(Session, new BinaryOperator(nameof(Letter.LetterSenderAddress), LetterFilter.Email));

                foreach (var letter in letters)
                {
                    letter.LetterCatalog = LetterFilter.LetterCatalog;
                    letter.Save();
                }
            }
            
            Close();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            txtEmail.EditValue = LetterFilter.Email;
            btnLetterCatalog.EditValue = LetterFilter.LetterCatalog;
        }

        private void btnLetterCatalog_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var form = new SystemCatalogEdit(Session);
            form.ShowDialog();

            if (form.LetterCatalog != null)
            {
                btnLetterCatalog.EditValue = form.LetterCatalog;
            }
        }

        private void btnLetterCatalog_DoubleClick(object sender, EventArgs e)
        {
            var form = new SystemCatalogEdit(Session);
            form.ShowDialog();

            if (form.LetterCatalog != null)
            {
                btnLetterCatalog.EditValue = form.LetterCatalog;
            }
        }
    }
}