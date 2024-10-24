namespace RMS.UI.xUI.BaseControl
{
    partial class RichEditBaseControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.richEdit = new DevExpress.XtraRichEdit.RichEditControl();
            this.SuspendLayout();
            // 
            // richEdit
            // 
            this.richEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richEdit.Location = new System.Drawing.Point(0, 0);
            this.richEdit.Name = "richEdit";
            this.richEdit.Options.DocumentCapabilities.Hyperlinks = DevExpress.XtraRichEdit.DocumentCapability.Enabled;
            this.richEdit.Options.FormattingMarkVisibility.ParagraphMark = DevExpress.XtraRichEdit.RichEditFormattingMarkVisibility.Hidden;
            this.richEdit.Options.FormattingMarkVisibility.Separator = DevExpress.XtraRichEdit.RichEditFormattingMarkVisibility.Hidden;
            this.richEdit.Options.HorizontalRuler.ShowLeftIndent = false;
            this.richEdit.Options.HorizontalRuler.ShowRightIndent = false;
            this.richEdit.Options.HorizontalRuler.Visibility = DevExpress.XtraRichEdit.RichEditRulerVisibility.Hidden;
            this.richEdit.Options.HorizontalScrollbar.Visibility = DevExpress.XtraRichEdit.RichEditScrollbarVisibility.Hidden;
            this.richEdit.Options.VerticalRuler.Visibility = DevExpress.XtraRichEdit.RichEditRulerVisibility.Hidden;
            this.richEdit.Options.VerticalScrollbar.Visibility = DevExpress.XtraRichEdit.RichEditScrollbarVisibility.Hidden;
            this.richEdit.ReadOnly = true;
            this.richEdit.Size = new System.Drawing.Size(426, 325);
            this.richEdit.TabIndex = 70;
            this.richEdit.Views.SimpleView.Padding = new DevExpress.Portable.PortablePadding(0);
            // 
            // RichEditBaseControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.richEdit);
            this.Name = "RichEditBaseControl";
            this.Size = new System.Drawing.Size(426, 325);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraRichEdit.RichEditControl richEdit;
    }
}
