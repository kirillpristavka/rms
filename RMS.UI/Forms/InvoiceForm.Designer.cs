namespace RMS.UI.Forms
{
    partial class InvoiceForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InvoiceForm));
            this.gridControlInvoices = new DevExpress.XtraGrid.GridControl();
            this.gridViewInvoices = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnTaskDel = new DevExpress.XtraEditors.SimpleButton();
            this.btnTaskEdit = new DevExpress.XtraEditors.SimpleButton();
            this.btnTaskAdd = new DevExpress.XtraEditors.SimpleButton();
            this.splitContainerControl = new DevExpress.XtraEditors.SplitContainerControl();
            this.gridControlInvoiceInformation = new DevExpress.XtraGrid.GridControl();
            this.gridViewInvoiceInformation = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.imgInvoice = new DevExpress.Utils.ImageCollection(this.components);
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barBtnSend = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnTaskAdd = new DevExpress.XtraBars.BarButtonItem();
            this.popupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.barBtnControlSystemAdd = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlInvoices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewInvoices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).BeginInit();
            this.splitContainerControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlInvoiceInformation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewInvoiceInformation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgInvoice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlInvoices
            // 
            this.gridControlInvoices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlInvoices.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gridControlInvoices.Location = new System.Drawing.Point(0, 0);
            this.gridControlInvoices.MainView = this.gridViewInvoices;
            this.gridControlInvoices.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gridControlInvoices.Name = "gridControlInvoices";
            this.gridControlInvoices.Size = new System.Drawing.Size(980, 360);
            this.gridControlInvoices.TabIndex = 34;
            this.gridControlInvoices.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewInvoices});
            // 
            // gridViewInvoices
            // 
            this.gridViewInvoices.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridViewInvoices.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridViewInvoices.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridViewInvoices.DetailHeight = 394;
            this.gridViewInvoices.GridControl = this.gridControlInvoices;
            this.gridViewInvoices.Name = "gridViewInvoices";
            this.gridViewInvoices.OptionsBehavior.Editable = false;
            this.gridViewInvoices.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewInvoices.OptionsView.ShowGroupPanel = false;
            this.gridViewInvoices.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewInvoices_FocusedRowChanged);
            this.gridViewInvoices.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridViewInvoices_MouseDown);
            // 
            // panelControl1
            // 
            this.panelControl1.AutoSize = true;
            this.panelControl1.Controls.Add(this.btnTaskDel);
            this.panelControl1.Controls.Add(this.btnTaskEdit);
            this.panelControl1.Controls.Add(this.btnTaskAdd);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(980, 41);
            this.panelControl1.TabIndex = 35;
            // 
            // btnTaskDel
            // 
            this.btnTaskDel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnTaskDel.ImageOptions.Image")));
            this.btnTaskDel.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnTaskDel.Location = new System.Drawing.Point(85, 6);
            this.btnTaskDel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnTaskDel.Name = "btnTaskDel";
            this.btnTaskDel.Size = new System.Drawing.Size(31, 28);
            this.btnTaskDel.TabIndex = 2;
            this.btnTaskDel.Text = "+";
            this.btnTaskDel.Click += new System.EventHandler(this.btnDelInvoice_Click);
            // 
            // btnTaskEdit
            // 
            this.btnTaskEdit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnTaskEdit.ImageOptions.Image")));
            this.btnTaskEdit.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnTaskEdit.Location = new System.Drawing.Point(46, 6);
            this.btnTaskEdit.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnTaskEdit.Name = "btnTaskEdit";
            this.btnTaskEdit.Size = new System.Drawing.Size(31, 28);
            this.btnTaskEdit.TabIndex = 1;
            this.btnTaskEdit.Text = "+";
            this.btnTaskEdit.Click += new System.EventHandler(this.btnEditInvoice_Click);
            // 
            // btnTaskAdd
            // 
            this.btnTaskAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnTaskAdd.ImageOptions.Image")));
            this.btnTaskAdd.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnTaskAdd.Location = new System.Drawing.Point(8, 6);
            this.btnTaskAdd.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnTaskAdd.Name = "btnTaskAdd";
            this.btnTaskAdd.Size = new System.Drawing.Size(31, 28);
            this.btnTaskAdd.TabIndex = 0;
            this.btnTaskAdd.Text = "+";
            this.btnTaskAdd.Click += new System.EventHandler(this.btnAddInvoice_Click);
            // 
            // splitContainerControl
            // 
            this.splitContainerControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl.Horizontal = false;
            this.splitContainerControl.Location = new System.Drawing.Point(0, 41);
            this.splitContainerControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.splitContainerControl.Name = "splitContainerControl";
            this.splitContainerControl.Panel1.Controls.Add(this.gridControlInvoices);
            this.splitContainerControl.Panel1.Text = "Panel1";
            this.splitContainerControl.Panel2.Controls.Add(this.gridControlInvoiceInformation);
            this.splitContainerControl.Panel2.Text = "Panel2";
            this.splitContainerControl.Size = new System.Drawing.Size(980, 589);
            this.splitContainerControl.SplitterPosition = 360;
            this.splitContainerControl.TabIndex = 36;
            // 
            // gridControlInvoiceInformation
            // 
            this.gridControlInvoiceInformation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlInvoiceInformation.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gridControlInvoiceInformation.Location = new System.Drawing.Point(0, 0);
            this.gridControlInvoiceInformation.MainView = this.gridViewInvoiceInformation;
            this.gridControlInvoiceInformation.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gridControlInvoiceInformation.Name = "gridControlInvoiceInformation";
            this.gridControlInvoiceInformation.Size = new System.Drawing.Size(980, 214);
            this.gridControlInvoiceInformation.TabIndex = 35;
            this.gridControlInvoiceInformation.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewInvoiceInformation});
            // 
            // gridViewInvoiceInformation
            // 
            this.gridViewInvoiceInformation.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridViewInvoiceInformation.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridViewInvoiceInformation.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridViewInvoiceInformation.DetailHeight = 394;
            this.gridViewInvoiceInformation.GridControl = this.gridControlInvoiceInformation;
            this.gridViewInvoiceInformation.Name = "gridViewInvoiceInformation";
            this.gridViewInvoiceInformation.OptionsBehavior.Editable = false;
            this.gridViewInvoiceInformation.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewInvoiceInformation.OptionsView.ShowFooter = true;
            this.gridViewInvoiceInformation.OptionsView.ShowGroupPanel = false;
            // 
            // imgInvoice
            // 
            this.imgInvoice.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imgInvoice.ImageStream")));
            this.imgInvoice.Images.SetKeyName(0, "sendpdf_16x16.png");
            // 
            // barManager
            // 
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barBtnSend,
            this.barBtnTaskAdd,
            this.barBtnControlSystemAdd});
            this.barManager.MaxItemId = 8;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.barDockControlTop.Size = new System.Drawing.Size(980, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 630);
            this.barDockControlBottom.Manager = this.barManager;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.barDockControlBottom.Size = new System.Drawing.Size(980, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 630);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(980, 0);
            this.barDockControlRight.Manager = this.barManager;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 630);
            // 
            // barBtnSend
            // 
            this.barBtnSend.Caption = "Отправить";
            this.barBtnSend.Id = 0;
            this.barBtnSend.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnSend.ImageOptions.Image")));
            this.barBtnSend.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnSend.ImageOptions.LargeImage")));
            this.barBtnSend.Name = "barBtnSend";
            this.barBtnSend.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnSend_ItemClick);
            // 
            // barBtnTaskAdd
            // 
            this.barBtnTaskAdd.Caption = "Добавить задачу";
            this.barBtnTaskAdd.Id = 6;
            this.barBtnTaskAdd.Name = "barBtnTaskAdd";
            this.barBtnTaskAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnTaskAdd_ItemClick);
            // 
            // popupMenu
            // 
            this.popupMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnSend),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnTaskAdd, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnControlSystemAdd, true)});
            this.popupMenu.Manager = this.barManager;
            this.popupMenu.Name = "popupMenu";
            // 
            // barBtnControlSystemAdd
            // 
            this.barBtnControlSystemAdd.Caption = "Контроль";
            this.barBtnControlSystemAdd.Id = 7;
            this.barBtnControlSystemAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnControlSystemAdd.ImageOptions.Image")));
            this.barBtnControlSystemAdd.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnControlSystemAdd.ImageOptions.LargeImage")));
            this.barBtnControlSystemAdd.Name = "barBtnControlSystemAdd";
            this.barBtnControlSystemAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnControlSystemAdd_ItemClick);
            // 
            // InvoiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 630);
            this.Controls.Add(this.splitContainerControl);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "InvoiceForm";
            this.Text = "Счета";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlInvoices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewInvoices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).EndInit();
            this.splitContainerControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlInvoiceInformation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewInvoiceInformation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgInvoice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlInvoices;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewInvoices;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnTaskDel;
        private DevExpress.XtraEditors.SimpleButton btnTaskEdit;
        private DevExpress.XtraEditors.SimpleButton btnTaskAdd;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl;
        private DevExpress.XtraGrid.GridControl gridControlInvoiceInformation;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewInvoiceInformation;
        private DevExpress.Utils.ImageCollection imgInvoice;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barBtnSend;
        private DevExpress.XtraBars.PopupMenu popupMenu;
        private DevExpress.XtraBars.BarButtonItem barBtnTaskAdd;
        private DevExpress.XtraBars.BarButtonItem barBtnControlSystemAdd;
    }
}