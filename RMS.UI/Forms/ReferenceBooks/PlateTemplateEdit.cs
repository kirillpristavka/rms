using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit;
using RMS.Core.Model;
using RMS.Core.Model.Mail;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace RMS.UI.Forms.ReferenceBooks
{
    public partial class PlateTemplateEdit : formEdit_BaseSpr
    {
        private Session Session { get; }
        private PlateTemplate PlateTemplate { get; }

        public PlateTemplateEdit()
        {
            InitializeComponent();

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                PlateTemplate = new PlateTemplate(Session);
            }
        }

        public PlateTemplateEdit(int id) : this()
        {
            if (id > 0)
            {
                PlateTemplate = Session.GetObjectByKey<PlateTemplate>(id);
            }
        }

        public PlateTemplateEdit(PlateTemplate plateTemplate) : this()
        {
            Session = plateTemplate.Session;
            PlateTemplate = plateTemplate;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (PlateTemplate.IsDefault)
            {
                XtraMessageBox.Show("Изменение стандартного шаблона запрещено.",
                    "Запрет сохранения",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            PlateTemplate.Name = txtName.Text;
            PlateTemplate.Description = memoDescription.Text;

            if (!string.IsNullOrWhiteSpace(richPlateTemplate.HtmlText))
            {
                PlateTemplate.HtmlBody = Letter.StringToByte(richPlateTemplate.HtmlText);
                PlateTemplate.TextBody = Letter.StringToByte(richPlateTemplate.Text);
            }
            else if (!string.IsNullOrWhiteSpace(richPlateTemplate.Text))
            {
                PlateTemplate.TextBody = Letter.StringToByte(richPlateTemplate.Text);
                PlateTemplate.HtmlBody = Letter.StringToByte(richPlateTemplate.HtmlText);
            }

            var tempFile = Path.GetTempFileName();
            richPlateTemplate.SaveDocument(tempFile, DocumentFormat.OpenXml);
            var byteFile = System.IO.File.ReadAllBytes(tempFile);
            PlateTemplate.FileWord = byteFile;
            System.IO.File.Delete(tempFile);

            Session.Save(PlateTemplate);
            id = PlateTemplate.Oid;
            flagSave = true;
            Close();
        }

        private void PositionEdit_Load(object sender, EventArgs e)
        {
            memoDescription.Text = PlateTemplate.Description;
            txtName.Text = PlateTemplate.Name;

            try
            {
                var plateTemplateBody = Letter.ByteToString(PlateTemplate.TextBody);
                var plateTemplateHtmlBody = Letter.ByteToString(PlateTemplate.HtmlBody);

                if (string.IsNullOrWhiteSpace(plateTemplateBody))
                {
                    richPlateTemplate.HtmlText = plateTemplateBody;
                }
                else
                {
                    richPlateTemplate.HtmlText = plateTemplateHtmlBody;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}