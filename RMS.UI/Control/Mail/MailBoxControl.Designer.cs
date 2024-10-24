namespace RMS.UI.Control.Mail
{
    partial class MailBoxControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MailBoxControl));
            this.treeListMailbox = new DevExpress.XtraTreeList.TreeList();
            this.imageCollectionMailbox = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.treeListMailbox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionMailbox)).BeginInit();
            this.SuspendLayout();
            // 
            // treeListMailbox
            // 
            this.treeListMailbox.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.treeListMailbox.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListMailbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListMailbox.HorzScrollStep = 2;
            this.treeListMailbox.Location = new System.Drawing.Point(0, 0);
            this.treeListMailbox.Name = "treeListMailbox";
            this.treeListMailbox.OptionsBehavior.Editable = false;
            this.treeListMailbox.OptionsFind.AllowFindPanel = false;
            this.treeListMailbox.OptionsMenu.EnableFooterMenu = false;
            this.treeListMailbox.OptionsMenu.ShowAutoFilterRowItem = false;
            this.treeListMailbox.OptionsMenu.ShowExpandCollapseItems = false;
            this.treeListMailbox.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.treeListMailbox.OptionsView.ShowFilterPanelMode = DevExpress.XtraTreeList.ShowFilterPanelMode.Never;
            this.treeListMailbox.OptionsView.ShowFirstLines = false;
            this.treeListMailbox.OptionsView.ShowHierarchyIndentationLines = DevExpress.Utils.DefaultBoolean.False;
            this.treeListMailbox.OptionsView.ShowHorzLines = false;
            this.treeListMailbox.OptionsView.ShowVertLines = false;
            this.treeListMailbox.Size = new System.Drawing.Size(320, 178);
            this.treeListMailbox.TabIndex = 7;
            this.treeListMailbox.VertScrollVisibility = DevExpress.XtraTreeList.ScrollVisibility.Always;
            this.treeListMailbox.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeListMailbox_FocusedNodeChanged);
            // 
            // imageCollectionMailbox
            // 
            this.imageCollectionMailbox.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollectionMailbox.ImageStream")));
            this.imageCollectionMailbox.Images.SetKeyName(0, "boperson_16x16.png");
            this.imageCollectionMailbox.Images.SetKeyName(1, "inbox_16x16.png");
            this.imageCollectionMailbox.Images.SetKeyName(2, "outbox_16x16.png");
            this.imageCollectionMailbox.Images.SetKeyName(3, "trash_16x16.png");
            this.imageCollectionMailbox.Images.SetKeyName(4, "warning_16x16.png");
            this.imageCollectionMailbox.Images.SetKeyName(5, "paste_16x16.png");
            // 
            // MailBoxControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeListMailbox);
            this.MinimumSize = new System.Drawing.Size(320, 178);
            this.Name = "MailBoxControl";
            this.Size = new System.Drawing.Size(320, 178);
            ((System.ComponentModel.ISupportInitialize)(this.treeListMailbox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionMailbox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList treeListMailbox;
        private DevExpress.Utils.ImageCollection imageCollectionMailbox;
    }
}
