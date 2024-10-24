namespace RMS.UI.Forms
{
    partial class DealForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DealForm));
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.popupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.barBtnDel = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnRemovingEmptyTrades = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnRefreshFromLetter = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnBulkReplacement = new DevExpress.XtraBars.BarButtonItem();
            this.btnCustomerSettings = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnSaveLayoutToXmlMainGrid = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnTaskAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnControlSystemAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barCheckIsCompleted = new DevExpress.XtraBars.BarCheckItem();
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barBtnDefault = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.panelControlFilter = new DevExpress.XtraEditors.PanelControl();
            this.layoutControlDeal = new DevExpress.XtraLayout.LayoutControl();
            this.treeListCustomerFilter = new DevExpress.XtraTreeList.TreeList();
            this.btnDirectoryCustomerFilter = new DevExpress.XtraEditors.SimpleButton();
            this.btnDirectoryAdd = new DevExpress.XtraEditors.SimpleButton();
            this.cmbStatusDeal = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnCustomer = new DevExpress.XtraEditors.ButtonEdit();
            this.btnStaff = new DevExpress.XtraEditors.ButtonEdit();
            this.s = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemStatusDeal = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItemStaff = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemCustomer = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.popupMenuTreeList = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlFilter)).BeginInit();
            this.panelControlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlDeal)).BeginInit();
            this.layoutControlDeal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListCustomerFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStatusDeal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnStaff.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.s)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemStatusDeal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemStaff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuTreeList)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl
            // 
            this.gridControl.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gridControl.Location = new System.Drawing.Point(342, 13);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(900, 789);
            this.gridControl.TabIndex = 36;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.Appearance.FooterPanel.Options.UseTextOptions = true;
            this.gridView.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView.DetailHeight = 394;
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.Editable = false;
            this.gridView.OptionsSelection.MultiSelect = true;
            this.gridView.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gridView.OptionsView.ShowFooter = true;
            this.gridView.OptionsView.ShowGroupPanel = false;
            this.gridView.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView_RowStyle);
            this.gridView.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gridView_PopupMenuShowing);
            this.gridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView_FocusedRowChanged);
            // 
            // popupMenu
            // 
            this.popupMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnDel),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnRemovingEmptyTrades),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnRefresh, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnRefreshFromLetter),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnBulkReplacement, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCustomerSettings, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnSaveLayoutToXmlMainGrid),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnTaskAdd, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnControlSystemAdd, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barCheckIsCompleted, true)});
            this.popupMenu.Manager = this.barManager;
            this.popupMenu.Name = "popupMenu";
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
            // barBtnRemovingEmptyTrades
            // 
            this.barBtnRemovingEmptyTrades.Caption = "Удаление пустых сделок";
            this.barBtnRemovingEmptyTrades.Id = 7;
            this.barBtnRemovingEmptyTrades.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnRemovingEmptyTrades.ImageOptions.Image")));
            this.barBtnRemovingEmptyTrades.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnRemovingEmptyTrades.ImageOptions.LargeImage")));
            this.barBtnRemovingEmptyTrades.Name = "barBtnRemovingEmptyTrades";
            this.barBtnRemovingEmptyTrades.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnRemovingEmptyTrades_ItemClick);
            // 
            // barBtnRefresh
            // 
            this.barBtnRefresh.Caption = "Обновить";
            this.barBtnRefresh.Id = 1;
            this.barBtnRefresh.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnRefresh.ImageOptions.Image")));
            this.barBtnRefresh.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnRefresh.ImageOptions.LargeImage")));
            this.barBtnRefresh.Name = "barBtnRefresh";
            this.barBtnRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnRefresh_ItemClick);
            // 
            // barBtnRefreshFromLetter
            // 
            this.barBtnRefreshFromLetter.Caption = "Обновить клиентов из писем";
            this.barBtnRefreshFromLetter.Id = 4;
            this.barBtnRefreshFromLetter.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnRefreshFromLetter.ImageOptions.Image")));
            this.barBtnRefreshFromLetter.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnRefreshFromLetter.ImageOptions.LargeImage")));
            this.barBtnRefreshFromLetter.Name = "barBtnRefreshFromLetter";
            this.barBtnRefreshFromLetter.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnRefreshFromLetter_ItemClick);
            // 
            // barBtnBulkReplacement
            // 
            this.barBtnBulkReplacement.Caption = "Массовая замена параметров";
            this.barBtnBulkReplacement.Id = 3;
            this.barBtnBulkReplacement.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnBulkReplacement.ImageOptions.Image")));
            this.barBtnBulkReplacement.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnBulkReplacement.ImageOptions.LargeImage")));
            this.barBtnBulkReplacement.Name = "barBtnBulkReplacement";
            this.barBtnBulkReplacement.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnBulkReplacement_ItemClick);
            // 
            // btnCustomerSettings
            // 
            this.btnCustomerSettings.Caption = "Настройка отображения";
            this.btnCustomerSettings.Id = 2;
            this.btnCustomerSettings.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCustomerSettings.ImageOptions.Image")));
            this.btnCustomerSettings.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnCustomerSettings.ImageOptions.LargeImage")));
            this.btnCustomerSettings.Name = "btnCustomerSettings";
            this.btnCustomerSettings.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSettings_ItemClick);
            // 
            // barBtnSaveLayoutToXmlMainGrid
            // 
            this.barBtnSaveLayoutToXmlMainGrid.Caption = "Сохранить отображение";
            this.barBtnSaveLayoutToXmlMainGrid.Id = 5;
            this.barBtnSaveLayoutToXmlMainGrid.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnSaveLayoutToXmlMainGrid.ImageOptions.Image")));
            this.barBtnSaveLayoutToXmlMainGrid.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnSaveLayoutToXmlMainGrid.ImageOptions.LargeImage")));
            this.barBtnSaveLayoutToXmlMainGrid.Name = "barBtnSaveLayoutToXmlMainGrid";
            this.barBtnSaveLayoutToXmlMainGrid.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnSaveLayoutToXmlMainGrid_ItemClick);
            // 
            // barBtnTaskAdd
            // 
            this.barBtnTaskAdd.Caption = "Добавить задачу";
            this.barBtnTaskAdd.Id = 6;
            this.barBtnTaskAdd.Name = "barBtnTaskAdd";
            this.barBtnTaskAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnTaskAdd_ItemClick);
            // 
            // barBtnControlSystemAdd
            // 
            this.barBtnControlSystemAdd.Caption = "Контроль";
            this.barBtnControlSystemAdd.Id = 8;
            this.barBtnControlSystemAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnControlSystemAdd.ImageOptions.Image")));
            this.barBtnControlSystemAdd.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnControlSystemAdd.ImageOptions.LargeImage")));
            this.barBtnControlSystemAdd.Name = "barBtnControlSystemAdd";
            this.barBtnControlSystemAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnControlSystemAdd_ItemClick);
            // 
            // barCheckIsCompleted
            // 
            this.barCheckIsCompleted.Caption = "Отобразить выполненые";
            this.barCheckIsCompleted.Id = 11;
            this.barCheckIsCompleted.Name = "barCheckIsCompleted";
            this.barCheckIsCompleted.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCheckIsCompleted_ItemClick);
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
            this.barBtnRefresh,
            this.btnCustomerSettings,
            this.barBtnBulkReplacement,
            this.barBtnRefreshFromLetter,
            this.barBtnSaveLayoutToXmlMainGrid,
            this.barBtnTaskAdd,
            this.barBtnRemovingEmptyTrades,
            this.barBtnControlSystemAdd,
            this.barBtnDefault,
            this.barButtonItem1,
            this.barCheckIsCompleted});
            this.barManager.MaxItemId = 12;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.barDockControlTop.Size = new System.Drawing.Size(1260, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 819);
            this.barDockControlBottom.Manager = this.barManager;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.barDockControlBottom.Size = new System.Drawing.Size(1260, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 819);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1260, 0);
            this.barDockControlRight.Manager = this.barManager;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 819);
            // 
            // barBtnDefault
            // 
            this.barBtnDefault.Caption = "По умолчанию...";
            this.barBtnDefault.Id = 9;
            this.barBtnDefault.Name = "barBtnDefault";
            this.barBtnDefault.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnDefault_ItemClick);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barCheckIsCompleted";
            this.barButtonItem1.Id = 10;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // panelControlFilter
            // 
            this.panelControlFilter.Controls.Add(this.layoutControlDeal);
            this.panelControlFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControlFilter.Location = new System.Drawing.Point(0, 0);
            this.panelControlFilter.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panelControlFilter.Name = "panelControlFilter";
            this.panelControlFilter.Size = new System.Drawing.Size(1260, 819);
            this.panelControlFilter.TabIndex = 41;
            // 
            // layoutControlDeal
            // 
            this.layoutControlDeal.Controls.Add(this.treeListCustomerFilter);
            this.layoutControlDeal.Controls.Add(this.btnDirectoryCustomerFilter);
            this.layoutControlDeal.Controls.Add(this.btnDirectoryAdd);
            this.layoutControlDeal.Controls.Add(this.gridControl);
            this.layoutControlDeal.Controls.Add(this.cmbStatusDeal);
            this.layoutControlDeal.Controls.Add(this.btnCustomer);
            this.layoutControlDeal.Controls.Add(this.btnStaff);
            this.layoutControlDeal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlDeal.Location = new System.Drawing.Point(2, 2);
            this.layoutControlDeal.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.layoutControlDeal.Name = "layoutControlDeal";
            this.layoutControlDeal.Root = this.s;
            this.layoutControlDeal.Size = new System.Drawing.Size(1256, 815);
            this.layoutControlDeal.TabIndex = 12;
            this.layoutControlDeal.Text = "Сделка";
            // 
            // treeListCustomerFilter
            // 
            this.treeListCustomerFilter.Location = new System.Drawing.Point(14, 155);
            this.treeListCustomerFilter.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.treeListCustomerFilter.MinWidth = 25;
            this.treeListCustomerFilter.Name = "treeListCustomerFilter";
            this.treeListCustomerFilter.BeginUnboundLoad();
            this.treeListCustomerFilter.AppendNode(new object[0], -1);
            this.treeListCustomerFilter.AppendNode(new object[0], -1);
            this.treeListCustomerFilter.AppendNode(new object[0], -1);
            this.treeListCustomerFilter.EndUnboundLoad();
            this.treeListCustomerFilter.OptionsBehavior.Editable = false;
            this.treeListCustomerFilter.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.RowFocus;
            this.treeListCustomerFilter.OptionsView.ShowHorzLines = false;
            this.treeListCustomerFilter.OptionsView.ShowTreeLines = DevExpress.Utils.DefaultBoolean.True;
            this.treeListCustomerFilter.OptionsView.ShowVertLines = false;
            this.treeListCustomerFilter.Size = new System.Drawing.Size(309, 647);
            this.treeListCustomerFilter.TabIndex = 135;
            this.treeListCustomerFilter.TreeLevelWidth = 22;
            this.treeListCustomerFilter.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeListCustomerFilter_FocusedNodeChanged);
            this.treeListCustomerFilter.PopupMenuShowing += new DevExpress.XtraTreeList.PopupMenuShowingEventHandler(this.treeListCustomerFilter_PopupMenuShowing);
            this.treeListCustomerFilter.DoubleClick += new System.EventHandler(this.treeListCustomerFilter_DoubleClick);
            // 
            // btnDirectoryCustomerFilter
            // 
            this.btnDirectoryCustomerFilter.Location = new System.Drawing.Point(14, 124);
            this.btnDirectoryCustomerFilter.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnDirectoryCustomerFilter.Name = "btnDirectoryCustomerFilter";
            this.btnDirectoryCustomerFilter.Size = new System.Drawing.Size(155, 27);
            this.btnDirectoryCustomerFilter.StyleController = this.layoutControlDeal;
            this.btnDirectoryCustomerFilter.TabIndex = 136;
            this.btnDirectoryCustomerFilter.Text = "Справочник";
            this.btnDirectoryCustomerFilter.Click += new System.EventHandler(this.btnDirectoryCustomerFilter_Click);
            // 
            // btnDirectoryAdd
            // 
            this.btnDirectoryAdd.Location = new System.Drawing.Point(173, 124);
            this.btnDirectoryAdd.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnDirectoryAdd.Name = "btnDirectoryAdd";
            this.btnDirectoryAdd.Size = new System.Drawing.Size(150, 27);
            this.btnDirectoryAdd.StyleController = this.layoutControlDeal;
            this.btnDirectoryAdd.TabIndex = 137;
            this.btnDirectoryAdd.Text = "Добавить";
            this.btnDirectoryAdd.Click += new System.EventHandler(this.btnDirectoryAdd_Click);
            // 
            // cmbStatusDeal
            // 
            this.cmbStatusDeal.Location = new System.Drawing.Point(111, 69);
            this.cmbStatusDeal.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cmbStatusDeal.Name = "cmbStatusDeal";
            this.cmbStatusDeal.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.cmbStatusDeal.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbStatusDeal.Size = new System.Drawing.Size(212, 24);
            this.cmbStatusDeal.StyleController = this.layoutControlDeal;
            this.cmbStatusDeal.TabIndex = 10;
            this.cmbStatusDeal.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.cmbStatusDeal_ButtonPressed);
            this.cmbStatusDeal.EditValueChanged += new System.EventHandler(this.Filter);
            // 
            // btnCustomer
            // 
            this.btnCustomer.Location = new System.Drawing.Point(111, 13);
            this.btnCustomer.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnCustomer.Name = "btnCustomer";
            this.btnCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.btnCustomer.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnCustomer.Size = new System.Drawing.Size(212, 24);
            this.btnCustomer.StyleController = this.layoutControlDeal;
            this.btnCustomer.TabIndex = 11;
            this.btnCustomer.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnCustomer_ButtonPressed);
            this.btnCustomer.EditValueChanged += new System.EventHandler(this.Filter);
            // 
            // btnStaff
            // 
            this.btnStaff.Location = new System.Drawing.Point(111, 41);
            this.btnStaff.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnStaff.Name = "btnStaff";
            this.btnStaff.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.btnStaff.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnStaff.Size = new System.Drawing.Size(212, 24);
            this.btnStaff.StyleController = this.layoutControlDeal;
            this.btnStaff.TabIndex = 12;
            this.btnStaff.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnStaff_ButtonPressed);
            this.btnStaff.EditValueChanged += new System.EventHandler(this.Filter);
            // 
            // s
            // 
            this.s.AppearanceItemCaption.Options.UseTextOptions = true;
            this.s.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.s.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.s.GroupBordersVisible = false;
            this.s.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemStatusDeal,
            this.emptySpaceItem1,
            this.layoutControlItemStaff,
            this.layoutControlItemCustomer,
            this.layoutControlItem1,
            this.splitterItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.s.Name = "Root";
            this.s.Size = new System.Drawing.Size(1256, 815);
            this.s.TextVisible = false;
            // 
            // layoutControlItemStatusDeal
            // 
            this.layoutControlItemStatusDeal.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItemStatusDeal.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlItemStatusDeal.Control = this.cmbStatusDeal;
            this.layoutControlItemStatusDeal.Location = new System.Drawing.Point(0, 56);
            this.layoutControlItemStatusDeal.Name = "layoutControlItemStatusDeal";
            this.layoutControlItemStatusDeal.Size = new System.Drawing.Size(313, 28);
            this.layoutControlItemStatusDeal.Text = "Статус:";
            this.layoutControlItemStatusDeal.TextSize = new System.Drawing.Size(93, 18);
            this.layoutControlItemStatusDeal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 84);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(130, 27);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(313, 27);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            this.emptySpaceItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItemStaff
            // 
            this.layoutControlItemStaff.Control = this.btnStaff;
            this.layoutControlItemStaff.CustomizationFormText = "Менеджер:";
            this.layoutControlItemStaff.Location = new System.Drawing.Point(0, 28);
            this.layoutControlItemStaff.Name = "layoutControlItemStaff";
            this.layoutControlItemStaff.Size = new System.Drawing.Size(313, 28);
            this.layoutControlItemStaff.Text = "Менеджер:";
            this.layoutControlItemStaff.TextSize = new System.Drawing.Size(93, 18);
            this.layoutControlItemStaff.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItemCustomer
            // 
            this.layoutControlItemCustomer.Control = this.btnCustomer;
            this.layoutControlItemCustomer.CustomizationFormText = "Клиент:";
            this.layoutControlItemCustomer.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemCustomer.Name = "layoutControlItemCustomer";
            this.layoutControlItemCustomer.Size = new System.Drawing.Size(313, 28);
            this.layoutControlItemCustomer.Text = "Клиент:";
            this.layoutControlItemCustomer.TextSize = new System.Drawing.Size(93, 18);
            this.layoutControlItemCustomer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl;
            this.layoutControlItem1.Location = new System.Drawing.Point(328, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(904, 793);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new System.Drawing.Point(313, 0);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(15, 793);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.treeListCustomerFilter;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 142);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(313, 651);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnDirectoryCustomerFilter;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 111);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(159, 31);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnDirectoryAdd;
            this.layoutControlItem4.Location = new System.Drawing.Point(159, 111);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(154, 31);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // popupMenuTreeList
            // 
            this.popupMenuTreeList.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnDefault)});
            this.popupMenuTreeList.Manager = this.barManager;
            this.popupMenuTreeList.Name = "popupMenuTreeList";
            // 
            // DealForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1260, 819);
            this.Controls.Add(this.panelControlFilter);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "DealForm";
            this.Text = "Сделки";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DealForm_FormClosing);
            this.Load += new System.EventHandler(this.DealForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlFilter)).EndInit();
            this.panelControlFilter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlDeal)).EndInit();
            this.layoutControlDeal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListCustomerFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStatusDeal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnStaff.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.s)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemStatusDeal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemStaff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuTreeList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraBars.PopupMenu popupMenu;
        private DevExpress.XtraBars.BarButtonItem barBtnDel;
        private DevExpress.XtraBars.BarButtonItem barBtnRefresh;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.PanelControl panelControlFilter;
        private DevExpress.XtraLayout.LayoutControl layoutControlDeal;
        private DevExpress.XtraEditors.ComboBoxEdit cmbStatusDeal;
        private DevExpress.XtraEditors.ButtonEdit btnCustomer;
        private DevExpress.XtraEditors.ButtonEdit btnStaff;
        private DevExpress.XtraLayout.LayoutControlGroup s;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemStatusDeal;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemStaff;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCustomer;
        private DevExpress.XtraBars.BarButtonItem btnCustomerSettings;
        private DevExpress.XtraBars.BarButtonItem barBtnBulkReplacement;
        private DevExpress.XtraBars.BarButtonItem barBtnRefreshFromLetter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.XtraTreeList.TreeList treeListCustomerFilter;
        private DevExpress.XtraEditors.SimpleButton btnDirectoryCustomerFilter;
        private DevExpress.XtraEditors.SimpleButton btnDirectoryAdd;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraBars.BarButtonItem barBtnSaveLayoutToXmlMainGrid;
        private DevExpress.XtraBars.BarButtonItem barBtnTaskAdd;
        private DevExpress.XtraBars.BarButtonItem barBtnRemovingEmptyTrades;
        private DevExpress.XtraBars.BarButtonItem barBtnControlSystemAdd;
        private DevExpress.XtraBars.BarButtonItem barBtnDefault;
        private DevExpress.XtraBars.PopupMenu popupMenuTreeList;
        private DevExpress.XtraBars.BarCheckItem barCheckIsCompleted;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
    }
}