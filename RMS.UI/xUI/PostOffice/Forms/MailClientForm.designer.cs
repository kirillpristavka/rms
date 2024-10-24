
namespace RMS.UI.xUI.PostOffice.Forms
{
    partial class MailClientForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.layoutControlMailClient = new DevExpress.XtraLayout.LayoutControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.tabbedControlGroupRenterInfo = new DevExpress.XtraLayout.TabbedControlGroup();
            this.layoutControlGroupRenter = new DevExpress.XtraLayout.LayoutControlGroup();
            this.splitterItem = new DevExpress.XtraLayout.SplitterItem();
            this.tabbedControlGroupContract = new DevExpress.XtraLayout.TabbedControlGroup();
            this.layoutControlGroupLetter = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroupLetterCatalog = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.splitterItemTreeList = new DevExpress.XtraLayout.SplitterItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlMailClient)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedControlGroupRenterInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRenter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedControlGroupContract)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupLetter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupLetterCatalog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItemTreeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlMailClient
            // 
            this.layoutControlMailClient.AllowCustomization = false;
            this.layoutControlMailClient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlMailClient.Location = new System.Drawing.Point(0, 0);
            this.layoutControlMailClient.Margin = new System.Windows.Forms.Padding(4);
            this.layoutControlMailClient.Name = "layoutControlMailClient";
            this.layoutControlMailClient.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(522, 431, 980, 490);
            this.layoutControlMailClient.OptionsFocus.AllowFocusGroups = false;
            this.layoutControlMailClient.OptionsFocus.AllowFocusReadonlyEditors = false;
            this.layoutControlMailClient.OptionsFocus.AllowFocusTabbedGroups = false;
            this.layoutControlMailClient.Root = this.Root;
            this.layoutControlMailClient.Size = new System.Drawing.Size(1053, 699);
            this.layoutControlMailClient.TabIndex = 0;
            this.layoutControlMailClient.Text = "layoutControl1";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.tabbedControlGroupRenterInfo,
            this.splitterItem,
            this.tabbedControlGroupContract,
            this.layoutControlGroupLetterCatalog,
            this.layoutControlGroup2,
            this.splitterItemTreeList,
            this.splitterItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1053, 699);
            this.Root.TextVisible = false;
            // 
            // tabbedControlGroupRenterInfo
            // 
            this.tabbedControlGroupRenterInfo.AppearanceTabPage.HeaderActive.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.tabbedControlGroupRenterInfo.AppearanceTabPage.HeaderActive.Options.UseFont = true;
            this.tabbedControlGroupRenterInfo.Location = new System.Drawing.Point(412, 529);
            this.tabbedControlGroupRenterInfo.Name = "tabbedControlGroupRenterInfo";
            this.tabbedControlGroupRenterInfo.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.tabbedControlGroupRenterInfo.SelectedTabPage = this.layoutControlGroupRenter;
            this.tabbedControlGroupRenterInfo.Size = new System.Drawing.Size(641, 170);
            this.tabbedControlGroupRenterInfo.TabPages.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroupRenter});
            this.tabbedControlGroupRenterInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.tabbedControlGroupRenterInfo.SelectedPageChanged += new DevExpress.XtraLayout.LayoutTabPageChangedEventHandler(this.tabbedControlGroupRenterInfo_SelectedPageChanged);
            // 
            // layoutControlGroupRenter
            // 
            this.layoutControlGroupRenter.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupRenter.Name = "layoutControlGroupRenter";
            this.layoutControlGroupRenter.Size = new System.Drawing.Size(625, 128);
            this.layoutControlGroupRenter.Text = "Арендаторы";
            // 
            // splitterItem
            // 
            this.splitterItem.AllowHotTrack = true;
            this.splitterItem.Location = new System.Drawing.Point(412, 517);
            this.splitterItem.Name = "splitterItem";
            this.splitterItem.Size = new System.Drawing.Size(641, 12);
            this.splitterItem.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // tabbedControlGroupContract
            // 
            this.tabbedControlGroupContract.Location = new System.Drawing.Point(412, 0);
            this.tabbedControlGroupContract.Name = "tabbedControlGroupContract";
            this.tabbedControlGroupContract.SelectedTabPage = this.layoutControlGroupLetter;
            this.tabbedControlGroupContract.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            this.tabbedControlGroupContract.Size = new System.Drawing.Size(641, 517);
            this.tabbedControlGroupContract.TabPages.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroupLetter});
            // 
            // layoutControlGroupLetter
            // 
            this.layoutControlGroupLetter.DefaultLayoutType = DevExpress.XtraLayout.Utils.LayoutType.Horizontal;
            this.layoutControlGroupLetter.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupLetter.Name = "layoutControlGroupContract";
            this.layoutControlGroupLetter.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroupLetter.Size = new System.Drawing.Size(617, 493);
            // 
            // layoutControlGroupLetterCatalog
            // 
            this.layoutControlGroupLetterCatalog.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupLetterCatalog.Name = "layoutControlGroupLetterCatalog";
            this.layoutControlGroupLetterCatalog.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroupLetterCatalog.Size = new System.Drawing.Size(400, 349);
            this.layoutControlGroupLetterCatalog.Text = "Почтовые каталоги";
            this.layoutControlGroupLetterCatalog.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "layoutControlGroup";
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 361);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(400, 338);
            this.layoutControlGroup2.Text = "layoutControlGroup";
            this.layoutControlGroup2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // splitterItemTreeList
            // 
            this.splitterItemTreeList.AllowHotTrack = true;
            this.splitterItemTreeList.Location = new System.Drawing.Point(400, 0);
            this.splitterItemTreeList.Name = "splitterItemTreeList";
            this.splitterItemTreeList.Size = new System.Drawing.Size(12, 699);
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new System.Drawing.Point(0, 349);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(400, 12);
            this.splitterItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // MailClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1053, 699);
            this.Controls.Add(this.layoutControlMailClient);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IconOptions.ShowIcon = false;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MailClientForm";
            this.Text = "Почтовый клиент";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlMailClient)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedControlGroupRenterInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRenter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedControlGroupContract)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupLetter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupLetterCatalog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItemTreeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControlMailClient;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.TabbedControlGroup tabbedControlGroupRenterInfo;
        private DevExpress.XtraLayout.SplitterItem splitterItem;
        private DevExpress.XtraLayout.TabbedControlGroup tabbedControlGroupContract;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupLetter;
        private DevExpress.XtraLayout.SplitterItem splitterItemTreeList;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupRenter;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupLetterCatalog;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
    }
}