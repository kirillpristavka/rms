using DevExpress.Office.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using System;
using System.Text.RegularExpressions;

namespace RMS.UI.xUI.BaseControl
{
    public partial class RichEditBaseControl : XtraUserControl
    {
        private static Regex urlRegex = new Regex(@"((?:[a-z][\w-]+:(?:/{1,3}|[a-z0-9%])|www\d{0,3}[.]|ftp[.]|[a-z0-9.\-]+[.][a-z]{2,4}/)(?:[^\s()<>]+|\(([^\s()<>]+|(\([^\s()<>]+\)))*\))+(?:\(([^\s()<>]+|(\([^\s()<>]+\)))*\)|[^\s`!()\[\]{};:'"".,<>?«»“”‘’]))", RegexOptions.IgnoreCase);

        public RichEditBaseControl()
        {
            InitializeComponent();

            AdjustSimpleViewPadding();
            AdjustDraftViewPadding();
            richEdit.ActiveViewType = RichEditViewType.Simple;
        }

        public void SetValue(object value)
        {
            richEdit.Text = default;
            richEdit.HtmlText = default;

            if (value is byte[] byteObj && byteObj != null)
            {
                richEdit.HtmlText = RMS.Core.Extensions.Extensions.ByteToString(byteObj);
            }
            else if (value is string stringObj && string.IsNullOrWhiteSpace(stringObj))
            {
                richEdit.Text = stringObj;
            }
        }

        private void richEditNotes_DocumentLoaded(object sender, EventArgs e)
        {
            AdjustMargins();
        }

        private void AdjustMargins()
        {
            richEdit.Document.Sections[0].Margins.Left = Units.InchesToDocumentsF(2f);
        }

        private void AdjustSimpleViewPadding()
        {
            richEdit.Views.SimpleView.Padding = new DevExpress.Portable.PortablePadding(0);
        }

        private void AdjustDraftViewPadding()
        {
            richEdit.Views.DraftView.Padding = new DevExpress.Portable.PortablePadding(0);
        }

        private void AdjustParagraphIndent()
        {
            richEdit.Document.Paragraphs[0].LeftIndent = Units.InchesToDocumentsF(0.5f);
            richEdit.Document.Paragraphs[0].FirstLineIndentType = ParagraphFirstLineIndent.Indented;
            richEdit.Document.Paragraphs[0].FirstLineIndent = Units.InchesToDocumentsF(0.5f);
        }

        private bool RangeHasHyperlink(DocumentRange documentRange)
        {
            foreach (Hyperlink h in richEdit.Document.Hyperlinks)
            {
                if (documentRange.Contains(h.Range.Start))
                    return true;
            }

            return false;
        }
    }
}
