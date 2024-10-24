
namespace RMS.UI.Forms.Directories
{
    partial class FormFAQEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFAQEdit));
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.richEditControl = new DevExpress.XtraRichEdit.RichEditControl();
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barBtnDel = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnBlock = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnCatalogEdit = new DevExpress.XtraBars.BarButtonItem();
            this.popupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.imageCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.layoutControlFAQ = new DevExpress.XtraLayout.LayoutControl();
            this.treeList = new DevExpress.XtraTreeList.TreeList();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.btnDel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemGridControl = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            this.layoutControlItemRichEdit = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemTreeList = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem2 = new DevExpress.XtraLayout.SplitterItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmbWorkZone = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlItemWorkZone = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlFAQ)).BeginInit();
            this.layoutControlFAQ.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemRichEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTreeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWorkZone.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemWorkZone)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl
            // 
            this.gridControl.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.gridControl.Location = new System.Drawing.Point(289, 38);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(627, 198);
            this.gridControl.TabIndex = 0;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.DetailHeight = 311;
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gridView.OptionsView.ShowGroupPanel = false;
            this.gridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView_FocusedRowChanged);
            this.gridView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridView_MouseDown);
            // 
            // richEditControl
            // 
            this.richEditControl.Location = new System.Drawing.Point(12, 252);
            this.richEditControl.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.richEditControl.MenuManager = this.barManager;
            this.richEditControl.Name = "richEditControl";
            this.richEditControl.Options.HorizontalRuler.Visibility = DevExpress.XtraRichEdit.RichEditRulerVisibility.Hidden;
            this.richEditControl.Options.VerticalRuler.Visibility = DevExpress.XtraRichEdit.RichEditRulerVisibility.Hidden;
            this.richEditControl.Size = new System.Drawing.Size(904, 309);
            this.richEditControl.TabIndex = 0;
            // 
            // barManager
            // 
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barBtnDel,
            this.barBtnAdd,
            this.barBtnBlock,
            this.barBtnCatalogEdit});
            this.barManager.MaxItemId = 7;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.barDockControlTop.Size = new System.Drawing.Size(928, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 599);
            this.barDockControlBottom.Manager = this.barManager;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.barDockControlBottom.Size = new System.Drawing.Size(928, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 599);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(928, 0);
            this.barDockControlRight.Manager = this.barManager;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 599);
            // 
            // barBtnDel
            // 
            this.barBtnDel.Caption = "Удалить";
            this.barBtnDel.Id = 0;
            this.barBtnDel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnDel.ImageOptions.Image")));
            this.barBtnDel.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnDel.ImageOptions.LargeImage")));
            this.barBtnDel.Name = "barBtnDel";
            this.barBtnDel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnDel_ItemClick);
            // 
            // barBtnAdd
            // 
            this.barBtnAdd.Caption = "Добавить";
            this.barBtnAdd.Id = 1;
            this.barBtnAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnAdd.ImageOptions.Image")));
            this.barBtnAdd.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnAdd.ImageOptions.LargeImage")));
            this.barBtnAdd.Name = "barBtnAdd";
            this.barBtnAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnAdd_ItemClick);
            // 
            // barBtnBlock
            // 
            this.barBtnBlock.Caption = "Заблокировать";
            this.barBtnBlock.Id = 5;
            this.barBtnBlock.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnBlock.ImageOptions.Image")));
            this.barBtnBlock.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnBlock.ImageOptions.LargeImage")));
            this.barBtnBlock.Name = "barBtnBlock";
            this.barBtnBlock.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnBlock_ItemClick);
            // 
            // barBtnCatalogEdit
            // 
            this.barBtnCatalogEdit.Caption = "Каталог";
            this.barBtnCatalogEdit.Id = 6;
            this.barBtnCatalogEdit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnCatalogEdit.ImageOptions.Image")));
            this.barBtnCatalogEdit.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnCatalogEdit.ImageOptions.LargeImage")));
            this.barBtnCatalogEdit.Name = "barBtnCatalogEdit";
            this.barBtnCatalogEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnCatalogEdit_ItemClick);
            // 
            // popupMenu
            // 
            this.popupMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnAdd),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnDel),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnCatalogEdit, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnBlock, true)});
            this.popupMenu.Manager = this.barManager;
            this.popupMenu.Name = "popupMenu";
            // 
            // imageCollection
            // 
            this.imageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection.ImageStream")));
            this.imageCollection.Images.SetKeyName(0, "encryptdocument_16x16.png");
            // 
            // layoutControlFAQ
            // 
            this.layoutControlFAQ.Controls.Add(this.treeList);
            this.layoutControlFAQ.Controls.Add(this.richEditControl);
            this.layoutControlFAQ.Controls.Add(this.gridControl);
            this.layoutControlFAQ.Controls.Add(this.btnAdd);
            this.layoutControlFAQ.Controls.Add(this.btnDel);
            this.layoutControlFAQ.Controls.Add(this.btnSave);
            this.layoutControlFAQ.Controls.Add(this.cmbWorkZone);
            this.layoutControlFAQ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlFAQ.Location = new System.Drawing.Point(0, 0);
            this.layoutControlFAQ.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.layoutControlFAQ.Name = "layoutControlFAQ";
            this.layoutControlFAQ.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(983, 339, 650, 400);
            this.layoutControlFAQ.Root = this.Root;
            this.layoutControlFAQ.Size = new System.Drawing.Size(928, 599);
            this.layoutControlFAQ.TabIndex = 42;
            this.layoutControlFAQ.Text = "layoutControl1";
            // 
            // treeList
            // 
            this.treeList.Location = new System.Drawing.Point(12, 38);
            this.treeList.Name = "treeList";
            this.treeList.BeginUnboundLoad();
            this.treeList.AppendNode(new object[0], -1);
            this.treeList.AppendNode(new object[0], -1);
            this.treeList.AppendNode(new object[0], -1);
            this.treeList.EndUnboundLoad();
            this.treeList.OptionsBehavior.EditorShowMode = DevExpress.XtraTreeList.TreeListEditorShowMode.DoubleClick;
            this.treeList.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.RowFocus;
            this.treeList.OptionsView.ShowHorzLines = false;
            this.treeList.OptionsView.ShowTreeLines = DevExpress.Utils.DefaultBoolean.True;
            this.treeList.OptionsView.ShowVertLines = false;
            this.treeList.Size = new System.Drawing.Size(261, 198);
            this.treeList.TabIndex = 134;
            this.treeList.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeList_FocusedNodeChanged);
            this.treeList.ValidateNode += new DevExpress.XtraTreeList.ValidateNodeEventHandler(this.treeList_ValidateNode);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(12, 12);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(128, 22);
            this.btnAdd.StyleController = this.layoutControlFAQ;
            this.btnAdd.TabIndex = 135;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(144, 12);
            this.btnDel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(129, 22);
            this.btnDel.StyleController = this.layoutControlFAQ;
            this.btnDel.TabIndex = 136;
            this.btnDel.Text = "Удалить";
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 565);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(904, 22);
            this.btnSave.StyleController = this.layoutControlFAQ;
            this.btnSave.TabIndex = 137;
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // Root
            // 
            this.Root.AppearanceItemCaption.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.Root.AppearanceItemCaption.Options.UseFont = true;
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemGridControl,
            this.splitterItem1,
            this.layoutControlItemRichEdit,
            this.layoutControlItemTreeList,
            this.splitterItem2,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItemWorkZone});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(928, 599);
            this.Root.TextVisible = false;
            // 
            // layoutControlItemGridControl
            // 
            this.layoutControlItemGridControl.Control = this.gridControl;
            this.layoutControlItemGridControl.Location = new System.Drawing.Point(277, 26);
            this.layoutControlItemGridControl.Name = "layoutControlItemGridControl";
            this.layoutControlItemGridControl.Size = new System.Drawing.Size(631, 202);
            this.layoutControlItemGridControl.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemGridControl.TextVisible = false;
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new System.Drawing.Point(0, 228);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(908, 12);
            // 
            // layoutControlItemRichEdit
            // 
            this.layoutControlItemRichEdit.Control = this.richEditControl;
            this.layoutControlItemRichEdit.Location = new System.Drawing.Point(0, 240);
            this.layoutControlItemRichEdit.Name = "layoutControlItemRichEdit";
            this.layoutControlItemRichEdit.Size = new System.Drawing.Size(908, 313);
            this.layoutControlItemRichEdit.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemRichEdit.TextVisible = false;
            // 
            // layoutControlItemTreeList
            // 
            this.layoutControlItemTreeList.Control = this.treeList;
            this.layoutControlItemTreeList.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItemTreeList.Name = "layoutControlItemTreeList";
            this.layoutControlItemTreeList.Size = new System.Drawing.Size(265, 202);
            this.layoutControlItemTreeList.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemTreeList.TextVisible = false;
            // 
            // splitterItem2
            // 
            this.splitterItem2.AllowHotTrack = true;
            this.splitterItem2.Location = new System.Drawing.Point(265, 0);
            this.splitterItem2.Name = "splitterItem2";
            this.splitterItem2.Size = new System.Drawing.Size(12, 228);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnAdd;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(132, 26);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnDel;
            this.layoutControlItem2.Location = new System.Drawing.Point(132, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(133, 26);
            this.layoutControlItem2.Text = "Удалить";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnSave;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 553);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(908, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // cmbWorkZone
            // 
            this.cmbWorkZone.Location = new System.Drawing.Point(425, 12);
            this.cmbWorkZone.MenuManager = this.barManager;
            this.cmbWorkZone.Name = "cmbWorkZone";
            this.cmbWorkZone.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.cmbWorkZone.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbWorkZone.Size = new System.Drawing.Size(491, 22);
            this.cmbWorkZone.StyleController = this.layoutControlFAQ;
            this.cmbWorkZone.TabIndex = 138;
            this.cmbWorkZone.SelectedIndexChanged += new System.EventHandler(this.cmbWorkZone_SelectedIndexChanged);
            this.cmbWorkZone.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.cmbWorkZone_ButtonPressed);
            // 
            // layoutControlItemWorkZone
            // 
            this.layoutControlItemWorkZone.Control = this.cmbWorkZone;
            this.layoutControlItemWorkZone.Location = new System.Drawing.Point(277, 0);
            this.layoutControlItemWorkZone.Name = "layoutControlItemWorkZone";
            this.layoutControlItemWorkZone.Size = new System.Drawing.Size(631, 26);
            this.layoutControlItemWorkZone.Text = "Вариант раздела:";
            this.layoutControlItemWorkZone.TextSize = new System.Drawing.Size(133, 16);
            // 
            // FormFAQEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 599);
            this.Controls.Add(this.layoutControlFAQ);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "FormFAQEdit";
            this.Text = "Справочник по программе";
            this.Load += new System.EventHandler(this.FormFAQEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlFAQ)).EndInit();
            this.layoutControlFAQ.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemRichEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTreeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWorkZone.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemWorkZone)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraRichEdit.RichEditControl richEditControl;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barBtnDel;
        private DevExpress.XtraBars.BarButtonItem barBtnAdd;
        private DevExpress.XtraBars.PopupMenu popupMenu;
        private DevExpress.XtraBars.BarButtonItem barBtnBlock;
        private DevExpress.Utils.ImageCollection imageCollection;
        private DevExpress.XtraLayout.LayoutControl layoutControlFAQ;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemGridControl;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemRichEdit;
        private DevExpress.XtraTreeList.TreeList treeList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemTreeList;
        private DevExpress.XtraLayout.SplitterItem splitterItem2;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.SimpleButton btnDel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraBars.BarButtonItem barBtnCatalogEdit;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.ComboBoxEdit cmbWorkZone;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemWorkZone;
    }
}