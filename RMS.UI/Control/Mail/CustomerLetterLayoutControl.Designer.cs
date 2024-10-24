namespace RMS.UI.Control.Mail
{
    partial class CustomerLetterLayoutControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomerLetterLayoutControl));
            this.treeListCustomerLetter = new DevExpress.XtraTreeList.TreeList();
            this.imageCollectionCustomerLetter = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.treeListCustomerLetter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionCustomerLetter)).BeginInit();
            this.SuspendLayout();
            // 
            // treeListCustomerLetter
            // 
            this.treeListCustomerLetter.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.treeListCustomerLetter.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListCustomerLetter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListCustomerLetter.Location = new System.Drawing.Point(0, 0);
            this.treeListCustomerLetter.Name = "treeListCustomerLetter";
            this.treeListCustomerLetter.OptionsBehavior.Editable = false;
            this.treeListCustomerLetter.OptionsFind.AllowFindPanel = false;
            this.treeListCustomerLetter.OptionsMenu.EnableFooterMenu = false;
            this.treeListCustomerLetter.OptionsMenu.ShowAutoFilterRowItem = false;
            this.treeListCustomerLetter.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.treeListCustomerLetter.OptionsView.ShowFilterPanelMode = DevExpress.XtraTreeList.ShowFilterPanelMode.Never;
            this.treeListCustomerLetter.OptionsView.ShowFirstLines = false;
            this.treeListCustomerLetter.OptionsView.ShowHierarchyIndentationLines = DevExpress.Utils.DefaultBoolean.False;
            this.treeListCustomerLetter.OptionsView.ShowHorzLines = false;
            this.treeListCustomerLetter.OptionsView.ShowVertLines = false;
            this.treeListCustomerLetter.Size = new System.Drawing.Size(462, 340);
            this.treeListCustomerLetter.TabIndex = 8;
            this.treeListCustomerLetter.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeListCustomerLetter_FocusedNodeChanged);
            // 
            // imageCollectionCustomerLetter
            // 
            this.imageCollectionCustomerLetter.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollectionCustomerLetter.ImageStream")));
            this.imageCollectionCustomerLetter.Images.SetKeyName(0, "boperson_16x16.png");
            this.imageCollectionCustomerLetter.Images.SetKeyName(1, "inbox_16x16.png");
            this.imageCollectionCustomerLetter.Images.SetKeyName(2, "outbox_16x16.png");
            this.imageCollectionCustomerLetter.Images.SetKeyName(3, "trash_16x16.png");
            this.imageCollectionCustomerLetter.Images.SetKeyName(4, "warning_16x16.png");
            this.imageCollectionCustomerLetter.Images.SetKeyName(5, "paste_16x16.png");
            // 
            // CustomerLetterLayoutControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeListCustomerLetter);
            this.MinimumSize = new System.Drawing.Size(350, 25);
            this.Name = "CustomerLetterLayoutControl";
            this.Size = new System.Drawing.Size(462, 340);
            ((System.ComponentModel.ISupportInitialize)(this.treeListCustomerLetter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionCustomerLetter)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList treeListCustomerLetter;
        private DevExpress.Utils.ImageCollection imageCollectionCustomerLetter;
    }
}
