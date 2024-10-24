
namespace RMS.UI.xUI.PackagesDocument.Controllers
{
    partial class PackageDocumentInfoControl
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
            this.checkedListInfo = new DevExpress.XtraEditors.CheckedListBoxControl();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // checkedListInfo
            // 
            this.checkedListInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListInfo.HotTrackSelectMode = DevExpress.XtraEditors.HotTrackSelectMode.SelectItemOnClick;
            this.checkedListInfo.Location = new System.Drawing.Point(0, 0);
            this.checkedListInfo.Name = "checkedListInfo";
            this.checkedListInfo.Size = new System.Drawing.Size(250, 50);
            this.checkedListInfo.TabIndex = 148;
            this.checkedListInfo.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.checkedListInfo_ItemCheck);
            // 
            // PackageDocumentInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.checkedListInfo);
            this.MaximumSize = new System.Drawing.Size(350, 0);
            this.MinimumSize = new System.Drawing.Size(250, 50);
            this.Name = "PackageDocumentInfoControl";
            this.Size = new System.Drawing.Size(250, 50);
            ((System.ComponentModel.ISupportInitialize)(this.checkedListInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.CheckedListBoxControl checkedListInfo;
    }
}
