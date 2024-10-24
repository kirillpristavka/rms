namespace RMS.UI.Control.Mail
{
    partial class SystemCatalogControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SystemCatalogControl));
            this.treeListCustomerLetter = new DevExpress.XtraTreeList.TreeList();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barBtnAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnDel = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnFilter = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnEdit = new DevExpress.XtraBars.BarButtonItem();
            this.popupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.imageCollectionCustomerLetter = new DevExpress.Utils.ImageCollection(this.components);
            this.panelControlFooter = new DevExpress.XtraEditors.PanelControl();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.treeListCustomerLetter)).BeginInit();
            this.treeListCustomerLetter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionCustomerLetter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlFooter)).BeginInit();
            this.panelControlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeListCustomerLetter
            // 
            this.treeListCustomerLetter.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.treeListCustomerLetter.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListCustomerLetter.Controls.Add(this.barDockControlLeft);
            this.treeListCustomerLetter.Controls.Add(this.barDockControlRight);
            this.treeListCustomerLetter.Controls.Add(this.barDockControlBottom);
            this.treeListCustomerLetter.Controls.Add(this.barDockControlTop);
            this.treeListCustomerLetter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListCustomerLetter.HorzScrollStep = 2;
            this.treeListCustomerLetter.Location = new System.Drawing.Point(0, 0);
            this.treeListCustomerLetter.MenuManager = this.barManager;
            this.treeListCustomerLetter.Name = "treeListCustomerLetter";
            this.treeListCustomerLetter.OptionsBehavior.EditorShowMode = DevExpress.XtraTreeList.TreeListEditorShowMode.DoubleClick;
            this.treeListCustomerLetter.OptionsDragAndDrop.DragNodesMode = DevExpress.XtraTreeList.DragNodesMode.Single;
            this.treeListCustomerLetter.OptionsMenu.ShowExpandCollapseItems = false;
            this.treeListCustomerLetter.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.treeListCustomerLetter.OptionsView.ShowFirstLines = false;
            this.treeListCustomerLetter.OptionsView.ShowHierarchyIndentationLines = DevExpress.Utils.DefaultBoolean.False;
            this.treeListCustomerLetter.OptionsView.ShowHorzLines = false;
            this.treeListCustomerLetter.Size = new System.Drawing.Size(320, 320);
            this.treeListCustomerLetter.TabIndex = 8;
            this.treeListCustomerLetter.VertScrollVisibility = DevExpress.XtraTreeList.ScrollVisibility.Always;
            this.treeListCustomerLetter.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.treeListCustomerLetter_NodeCellStyle);
            this.treeListCustomerLetter.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeListCustomerLetter_FocusedNodeChanged);
            this.treeListCustomerLetter.PopupMenuShowing += new DevExpress.XtraTreeList.PopupMenuShowingEventHandler(this.treeListCustomerLetter_PopupMenuShowing);
            this.treeListCustomerLetter.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeListCustomerLetter_MouseClick);
            this.treeListCustomerLetter.MouseLeave += new System.EventHandler(this.treeListCustomerLetter_MouseLeave);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 320);
            // 
            // barManager
            // 
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barBtnAdd,
            this.barBtnDel,
            this.barBtnFilter,
            this.barBtnRefresh,
            this.barBtnEdit});
            this.barManager.MaxItemId = 7;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.barDockControlTop.Size = new System.Drawing.Size(320, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 320);
            this.barDockControlBottom.Manager = this.barManager;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.barDockControlBottom.Size = new System.Drawing.Size(320, 0);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(320, 0);
            this.barDockControlRight.Manager = this.barManager;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 320);
            // 
            // barBtnAdd
            // 
            this.barBtnAdd.Caption = "Добавить";
            this.barBtnAdd.Id = 0;
            this.barBtnAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnAdd.ImageOptions.Image")));
            this.barBtnAdd.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnAdd.ImageOptions.LargeImage")));
            this.barBtnAdd.Name = "barBtnAdd";
            this.barBtnAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnAdd_ItemClick);
            // 
            // barBtnDel
            // 
            this.barBtnDel.Caption = "Удалить";
            this.barBtnDel.Id = 2;
            this.barBtnDel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnDel.ImageOptions.Image")));
            this.barBtnDel.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnDel.ImageOptions.LargeImage")));
            this.barBtnDel.Name = "barBtnDel";
            this.barBtnDel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnDel_ItemClick);
            // 
            // barBtnFilter
            // 
            this.barBtnFilter.Caption = "Фильтры";
            this.barBtnFilter.Id = 3;
            this.barBtnFilter.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnFilter.ImageOptions.Image")));
            this.barBtnFilter.Name = "barBtnFilter";
            this.barBtnFilter.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnFilter_ItemClick);
            // 
            // barBtnRefresh
            // 
            this.barBtnRefresh.Caption = "Обновить";
            this.barBtnRefresh.Id = 5;
            this.barBtnRefresh.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnRefresh.ImageOptions.Image")));
            this.barBtnRefresh.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnRefresh.ImageOptions.LargeImage")));
            this.barBtnRefresh.Name = "barBtnRefresh";
            this.barBtnRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnRefresh_ItemClick);
            // 
            // barBtnEdit
            // 
            this.barBtnEdit.Caption = "Изменить";
            this.barBtnEdit.Id = 6;
            this.barBtnEdit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnEdit.ImageOptions.Image")));
            this.barBtnEdit.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnEdit.ImageOptions.LargeImage")));
            this.barBtnEdit.Name = "barBtnEdit";
            this.barBtnEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnEdit_ItemClick);
            // 
            // popupMenu
            // 
            this.popupMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnAdd),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnEdit),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnDel),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnRefresh, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnFilter, true)});
            this.popupMenu.Manager = this.barManager;
            this.popupMenu.Name = "popupMenu";
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
            // panelControlFooter
            // 
            this.panelControlFooter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControlFooter.Controls.Add(this.btnOK);
            this.panelControlFooter.Controls.Add(this.btnClose);
            this.panelControlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControlFooter.Location = new System.Drawing.Point(0, 320);
            this.panelControlFooter.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panelControlFooter.Name = "panelControlFooter";
            this.panelControlFooter.Size = new System.Drawing.Size(320, 36);
            this.panelControlFooter.TabIndex = 9;
            this.panelControlFooter.Visible = false;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(151, 4);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 27);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "Ок";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(236, 4);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 27);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Отмена";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // SystemCatalogControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeListCustomerLetter);
            this.Controls.Add(this.panelControlFooter);
            this.MinimumSize = new System.Drawing.Size(320, 178);
            this.Name = "SystemCatalogControl";
            this.barManager.SetPopupContextMenu(this, this.popupMenu);
            this.Size = new System.Drawing.Size(320, 356);
            ((System.ComponentModel.ISupportInitialize)(this.treeListCustomerLetter)).EndInit();
            this.treeListCustomerLetter.ResumeLayout(false);
            this.treeListCustomerLetter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionCustomerLetter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlFooter)).EndInit();
            this.panelControlFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList treeListCustomerLetter;
        private DevExpress.Utils.ImageCollection imageCollectionCustomerLetter;
        private DevExpress.XtraBars.PopupMenu popupMenu;
        private DevExpress.XtraBars.BarButtonItem barBtnAdd;
        private DevExpress.XtraBars.BarButtonItem barBtnDel;
        private DevExpress.XtraBars.BarButtonItem barBtnFilter;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        public DevExpress.XtraEditors.PanelControl panelControlFooter;
        private DevExpress.XtraBars.BarButtonItem barBtnRefresh;
        private DevExpress.XtraBars.BarButtonItem barBtnEdit;
    }
}
