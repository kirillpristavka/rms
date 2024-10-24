using DevExpress.Office.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace RMS.UI.Control.Mail
{
    public partial class LetterEditControl : XtraUserControl
    {
        public RichEditControl RichLetter { get; set; }

        public LetterEditControl()
        {
            InitializeComponent();
            
            AdjustSimpleViewPadding();
            AdjustDraftViewPadding();

            richLetter.ActiveViewType = RichEditViewType.Simple;

            RichLetter = richLetter;
        }


        private void AdjustMargins()
        {
            richLetter.Document.Sections[0].Margins.Left = Units.InchesToDocumentsF(2f);
        }
        private void AdjustSimpleViewPadding()
        {
            richLetter.Views.SimpleView.Padding = new DevExpress.Portable.PortablePadding(0);
        }

        private void AdjustDraftViewPadding()
        {
            richLetter.Views.DraftView.Padding = new DevExpress.Portable.PortablePadding(0);
        }

        private void AdjustParagraphIndent()
        {
            richLetter.Document.Paragraphs[0].LeftIndent = Units.InchesToDocumentsF(0.5f);
            richLetter.Document.Paragraphs[0].FirstLineIndentType = ParagraphFirstLineIndent.Indented;
            richLetter.Document.Paragraphs[0].FirstLineIndent = Units.InchesToDocumentsF(0.5f);
        }

        private void richLetter_DocumentLoaded(object sender, EventArgs e)
        {
            try
            {
                AdjustMargins();

                Invoke((Action)delegate
                {
                    richLetter.Focus();
                });

                richLetter.Document.CaretPosition = richLetter.Document.Range.Start;
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }
    }
}
