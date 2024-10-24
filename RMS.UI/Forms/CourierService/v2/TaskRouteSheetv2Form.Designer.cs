namespace RMS.UI.Forms.CourierService.v2
{
    partial class TaskRouteSheetv2Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskRouteSheetv2Form));
            this.gridControlTasksCourier = new DevExpress.XtraGrid.GridControl();
            this.gridViewTasksCourier = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnRouteSheet = new DevExpress.XtraEditors.ButtonEdit();
            this.layoutControlTaskRouteSheetv2 = new DevExpress.XtraLayout.LayoutControl();
            this.dateDate = new DevExpress.XtraEditors.DateEdit();
            this.btnAccountantResponsible = new DevExpress.XtraEditors.ButtonEdit();
            this.cmbStatus = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnCustomer = new DevExpress.XtraEditors.ButtonEdit();
            this.memoInfo = new DevExpress.XtraEditors.MemoEdit();
            this.barManagerTasksCourier = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.btnAddReportChange = new DevExpress.XtraBars.BarButtonItem();
            this.btnBarEdit = new DevExpress.XtraBars.BarButtonItem();
            this.btnBarDel = new DevExpress.XtraBars.BarButtonItem();
            this.btnRefreshReportChange = new DevExpress.XtraBars.BarButtonItem();
            this.btnSettingArchiveFolderChange = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnSaveLayoutToXmlMainGrid = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnFormationRouteSheets = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnTaskAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnControlSystemAddTaskCourier = new DevExpress.XtraBars.BarButtonItem();
            this.btnCourier = new DevExpress.XtraEditors.ButtonEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemGridControl = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemAccountantResponsible = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemRouteSheet = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemInfo = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemStatus = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemCustomer = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemCourier = new DevExpress.XtraLayout.LayoutControlItem();
            this.imgTaskCourier = new DevExpress.Utils.ImageCollection(this.components);
            this.popupMenuTasksCourier = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlTasksCourier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTasksCourier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRouteSheet.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlTaskRouteSheetv2)).BeginInit();
            this.layoutControlTaskRouteSheetv2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAccountantResponsible.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoInfo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManagerTasksCourier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCourier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAccountantResponsible)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemRouteSheet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCourier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgTaskCourier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuTasksCourier)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlTasksCourier
            // 
            this.gridControlTasksCourier.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.gridControlTasksCourier.Location = new System.Drawing.Point(7, 85);
            this.gridControlTasksCourier.MainView = this.gridViewTasksCourier;
            this.gridControlTasksCourier.Name = "gridControlTasksCourier";
            this.gridControlTasksCourier.Size = new System.Drawing.Size(994, 320);
            this.gridControlTasksCourier.TabIndex = 23;
            this.gridControlTasksCourier.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewTasksCourier});
            this.gridControlTasksCourier.Load += new System.EventHandler(this.gridControlTasksCourier_Load);
            // 
            // gridViewTasksCourier
            // 
            this.gridViewTasksCourier.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridViewTasksCourier.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridViewTasksCourier.GridControl = this.gridControlTasksCourier;
            this.gridViewTasksCourier.Name = "gridViewTasksCourier";
            this.gridViewTasksCourier.OptionsBehavior.Editable = false;
            this.gridViewTasksCourier.OptionsSelection.MultiSelect = true;
            this.gridViewTasksCourier.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewTasksCourier.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gridViewTasksCourier.OptionsView.ShowGroupPanel = false;
            this.gridViewTasksCourier.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridViewgridViewTasksCourier_RowStyle);
            this.gridViewTasksCourier.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gridViewTasksCourier_PopupMenuShowing);
            // 
            // btnRouteSheet
            // 
            this.btnRouteSheet.Location = new System.Drawing.Point(649, 59);
            this.btnRouteSheet.Name = "btnRouteSheet";
            this.btnRouteSheet.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.btnRouteSheet.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnRouteSheet.Size = new System.Drawing.Size(352, 22);
            this.btnRouteSheet.StyleController = this.layoutControlTaskRouteSheetv2;
            this.btnRouteSheet.TabIndex = 57;
            this.btnRouteSheet.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnRouteSheet_ButtonPressed);
            this.btnRouteSheet.EditValueChanged += new System.EventHandler(this.Filter);
            // 
            // layoutControlTaskRouteSheetv2
            // 
            this.layoutControlTaskRouteSheetv2.AllowCustomization = false;
            this.layoutControlTaskRouteSheetv2.Controls.Add(this.btnRouteSheet);
            this.layoutControlTaskRouteSheetv2.Controls.Add(this.gridControlTasksCourier);
            this.layoutControlTaskRouteSheetv2.Controls.Add(this.dateDate);
            this.layoutControlTaskRouteSheetv2.Controls.Add(this.btnAccountantResponsible);
            this.layoutControlTaskRouteSheetv2.Controls.Add(this.cmbStatus);
            this.layoutControlTaskRouteSheetv2.Controls.Add(this.btnCustomer);
            this.layoutControlTaskRouteSheetv2.Controls.Add(this.memoInfo);
            this.layoutControlTaskRouteSheetv2.Controls.Add(this.btnCourier);
            this.layoutControlTaskRouteSheetv2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlTaskRouteSheetv2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlTaskRouteSheetv2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.layoutControlTaskRouteSheetv2.Name = "layoutControlTaskRouteSheetv2";
            this.layoutControlTaskRouteSheetv2.Root = this.Root;
            this.layoutControlTaskRouteSheetv2.Size = new System.Drawing.Size(1008, 460);
            this.layoutControlTaskRouteSheetv2.TabIndex = 40;
            this.layoutControlTaskRouteSheetv2.Text = "layoutControl1";
            // 
            // dateDate
            // 
            this.dateDate.EditValue = null;
            this.dateDate.Location = new System.Drawing.Point(150, 7);
            this.dateDate.MaximumSize = new System.Drawing.Size(160, 0);
            this.dateDate.Name = "dateDate";
            this.dateDate.Properties.Appearance.Options.UseTextOptions = true;
            this.dateDate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dateDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.dateDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.dateDate.Size = new System.Drawing.Size(160, 22);
            this.dateDate.StyleController = this.layoutControlTaskRouteSheetv2;
            this.dateDate.TabIndex = 55;
            this.dateDate.TabStop = false;
            this.dateDate.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.cmb_ButtonPressed);
            this.dateDate.EditValueChanged += new System.EventHandler(this.Filter);
            // 
            // btnAccountantResponsible
            // 
            this.btnAccountantResponsible.Location = new System.Drawing.Point(649, 33);
            this.btnAccountantResponsible.Name = "btnAccountantResponsible";
            this.btnAccountantResponsible.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.btnAccountantResponsible.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnAccountantResponsible.Size = new System.Drawing.Size(352, 22);
            this.btnAccountantResponsible.StyleController = this.layoutControlTaskRouteSheetv2;
            this.btnAccountantResponsible.TabIndex = 8;
            this.btnAccountantResponsible.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnAccountantResponsible_ButtonPressed);
            this.btnAccountantResponsible.EditValueChanged += new System.EventHandler(this.Filter);
            // 
            // cmbStatus
            // 
            this.cmbStatus.Location = new System.Drawing.Point(457, 7);
            this.cmbStatus.MaximumSize = new System.Drawing.Size(160, 0);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.cmbStatus.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbStatus.Size = new System.Drawing.Size(160, 22);
            this.cmbStatus.StyleController = this.layoutControlTaskRouteSheetv2;
            this.cmbStatus.TabIndex = 12;
            this.cmbStatus.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.cmb_ButtonPressed);
            this.cmbStatus.EditValueChanged += new System.EventHandler(this.Filter);
            // 
            // btnCustomer
            // 
            this.btnCustomer.Location = new System.Drawing.Point(150, 33);
            this.btnCustomer.Name = "btnCustomer";
            this.btnCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.btnCustomer.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnCustomer.Size = new System.Drawing.Size(352, 22);
            this.btnCustomer.StyleController = this.layoutControlTaskRouteSheetv2;
            this.btnCustomer.TabIndex = 10;
            this.btnCustomer.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnCustomer_ButtonPressed);
            this.btnCustomer.EditValueChanged += new System.EventHandler(this.Filter);
            // 
            // memoInfo
            // 
            this.memoInfo.EditValue = "Как работать с разделом: добавляем курьерские задачи. Нажимаем ПКМ -> сформироват" +
    "ь маршрутный лист.";
            this.memoInfo.Location = new System.Drawing.Point(7, 409);
            this.memoInfo.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.memoInfo.MaximumSize = new System.Drawing.Size(0, 44);
            this.memoInfo.MenuManager = this.barManagerTasksCourier;
            this.memoInfo.Name = "memoInfo";
            this.memoInfo.Properties.Appearance.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.memoInfo.Properties.Appearance.Options.UseBackColor = true;
            this.memoInfo.Properties.ReadOnly = true;
            this.memoInfo.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.memoInfo.Size = new System.Drawing.Size(994, 44);
            this.memoInfo.StyleController = this.layoutControlTaskRouteSheetv2;
            this.memoInfo.TabIndex = 58;
            // 
            // barManagerTasksCourier
            // 
            this.barManagerTasksCourier.DockControls.Add(this.barDockControlTop);
            this.barManagerTasksCourier.DockControls.Add(this.barDockControlBottom);
            this.barManagerTasksCourier.DockControls.Add(this.barDockControlLeft);
            this.barManagerTasksCourier.DockControls.Add(this.barDockControlRight);
            this.barManagerTasksCourier.Form = this;
            this.barManagerTasksCourier.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnAddReportChange,
            this.btnBarEdit,
            this.btnBarDel,
            this.btnRefreshReportChange,
            this.btnSettingArchiveFolderChange,
            this.barBtnSaveLayoutToXmlMainGrid,
            this.barBtnFormationRouteSheets,
            this.barBtnTaskAdd,
            this.barBtnControlSystemAddTaskCourier});
            this.barManagerTasksCourier.MaxItemId = 11;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManagerTasksCourier;
            this.barDockControlTop.Size = new System.Drawing.Size(1008, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 460);
            this.barDockControlBottom.Manager = this.barManagerTasksCourier;
            this.barDockControlBottom.Size = new System.Drawing.Size(1008, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManagerTasksCourier;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 460);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1008, 0);
            this.barDockControlRight.Manager = this.barManagerTasksCourier;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 460);
            // 
            // btnAddReportChange
            // 
            this.btnAddReportChange.Caption = "Добавить";
            this.btnAddReportChange.Id = 0;
            this.btnAddReportChange.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAddReportChange.ImageOptions.Image")));
            this.btnAddReportChange.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnAddReportChange.ImageOptions.LargeImage")));
            this.btnAddReportChange.Name = "btnAddReportChange";
            this.btnAddReportChange.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnTaskCourier_ItemClick);
            // 
            // btnBarEdit
            // 
            this.btnBarEdit.Caption = "Изменить";
            this.btnBarEdit.Id = 2;
            this.btnBarEdit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnBarEdit.ImageOptions.Image")));
            this.btnBarEdit.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnBarEdit.ImageOptions.LargeImage")));
            this.btnBarEdit.Name = "btnBarEdit";
            this.btnBarEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEditTaskCourier_ItemClick);
            // 
            // btnBarDel
            // 
            this.btnBarDel.Caption = "Удалить";
            this.btnBarDel.Id = 3;
            this.btnBarDel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnBarDel.ImageOptions.Image")));
            this.btnBarDel.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnBarDel.ImageOptions.LargeImage")));
            this.btnBarDel.Name = "btnBarDel";
            this.btnBarDel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDelTaskCourier_ItemClick);
            // 
            // btnRefreshReportChange
            // 
            this.btnRefreshReportChange.Caption = "Обновить";
            this.btnRefreshReportChange.Id = 5;
            this.btnRefreshReportChange.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnRefreshReportChange.ImageOptions.Image")));
            this.btnRefreshReportChange.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnRefreshReportChange.ImageOptions.LargeImage")));
            this.btnRefreshReportChange.Name = "btnRefreshReportChange";
            this.btnRefreshReportChange.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRefreshTaskCourier_ItemClick);
            // 
            // btnSettingArchiveFolderChange
            // 
            this.btnSettingArchiveFolderChange.Caption = "Настройка";
            this.btnSettingArchiveFolderChange.Id = 6;
            this.btnSettingArchiveFolderChange.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSettingArchiveFolderChange.ImageOptions.Image")));
            this.btnSettingArchiveFolderChange.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnSettingArchiveFolderChange.ImageOptions.LargeImage")));
            this.btnSettingArchiveFolderChange.Name = "btnSettingArchiveFolderChange";
            this.btnSettingArchiveFolderChange.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSettingTaskCourier_ItemClick);
            // 
            // barBtnSaveLayoutToXmlMainGrid
            // 
            this.barBtnSaveLayoutToXmlMainGrid.Caption = "Сохранить отображение";
            this.barBtnSaveLayoutToXmlMainGrid.Id = 7;
            this.barBtnSaveLayoutToXmlMainGrid.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnSaveLayoutToXmlMainGrid.ImageOptions.Image")));
            this.barBtnSaveLayoutToXmlMainGrid.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnSaveLayoutToXmlMainGrid.ImageOptions.LargeImage")));
            this.barBtnSaveLayoutToXmlMainGrid.Name = "barBtnSaveLayoutToXmlMainGrid";
            this.barBtnSaveLayoutToXmlMainGrid.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnSaveLayoutToXmlMainGrid_ItemClick);
            // 
            // barBtnFormationRouteSheets
            // 
            this.barBtnFormationRouteSheets.Caption = "Сформировать маршрутные листы";
            this.barBtnFormationRouteSheets.Id = 8;
            this.barBtnFormationRouteSheets.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnFormationRouteSheets.ImageOptions.Image")));
            this.barBtnFormationRouteSheets.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnFormationRouteSheets.ImageOptions.LargeImage")));
            this.barBtnFormationRouteSheets.Name = "barBtnFormationRouteSheets";
            this.barBtnFormationRouteSheets.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnFormationRouteSheets_ItemClick);
            // 
            // barBtnTaskAdd
            // 
            this.barBtnTaskAdd.Caption = "Добавить задачу";
            this.barBtnTaskAdd.Id = 9;
            this.barBtnTaskAdd.Name = "barBtnTaskAdd";
            this.barBtnTaskAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnTaskAdd_ItemClick);
            // 
            // barBtnControlSystemAddTaskCourier
            // 
            this.barBtnControlSystemAddTaskCourier.Caption = "Контроль";
            this.barBtnControlSystemAddTaskCourier.Id = 10;
            this.barBtnControlSystemAddTaskCourier.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnControlSystemAddTaskCourier.ImageOptions.Image")));
            this.barBtnControlSystemAddTaskCourier.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnControlSystemAddTaskCourier.ImageOptions.LargeImage")));
            this.barBtnControlSystemAddTaskCourier.Name = "barBtnControlSystemAddTaskCourier";
            this.barBtnControlSystemAddTaskCourier.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnControlSystemAddTaskCourier_ItemClick);
            // 
            // btnCourier
            // 
            this.btnCourier.Location = new System.Drawing.Point(150, 59);
            this.btnCourier.Name = "btnCourier";
            this.btnCourier.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.btnCourier.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnCourier.Size = new System.Drawing.Size(352, 22);
            this.btnCourier.StyleController = this.layoutControlTaskRouteSheetv2;
            this.btnCourier.TabIndex = 14;
            this.btnCourier.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnCourier_ButtonPressed);
            this.btnCourier.EditValueChanged += new System.EventHandler(this.Filter);
            // 
            // Root
            // 
            this.Root.AppearanceItemCaption.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.Root.AppearanceItemCaption.Options.UseFont = true;
            this.Root.AppearanceItemCaption.Options.UseTextOptions = true;
            this.Root.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemGridControl,
            this.layoutControlItemDate,
            this.layoutControlItemAccountantResponsible,
            this.layoutControlItemRouteSheet,
            this.layoutControlItemInfo,
            this.layoutControlItemStatus,
            this.layoutControlItemCustomer,
            this.layoutControlItemCourier});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.Root.Size = new System.Drawing.Size(1008, 460);
            this.Root.TextVisible = false;
            // 
            // layoutControlItemGridControl
            // 
            this.layoutControlItemGridControl.Control = this.gridControlTasksCourier;
            this.layoutControlItemGridControl.Location = new System.Drawing.Point(0, 78);
            this.layoutControlItemGridControl.Name = "layoutControlItemGridControl";
            this.layoutControlItemGridControl.Size = new System.Drawing.Size(998, 324);
            this.layoutControlItemGridControl.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemGridControl.TextVisible = false;
            // 
            // layoutControlItemDate
            // 
            this.layoutControlItemDate.Control = this.dateDate;
            this.layoutControlItemDate.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemDate.Name = "layoutControlItemDate";
            this.layoutControlItemDate.Size = new System.Drawing.Size(307, 26);
            this.layoutControlItemDate.Text = "Дата:";
            this.layoutControlItemDate.TextSize = new System.Drawing.Size(140, 16);
            // 
            // layoutControlItemAccountantResponsible
            // 
            this.layoutControlItemAccountantResponsible.Control = this.btnAccountantResponsible;
            this.layoutControlItemAccountantResponsible.Location = new System.Drawing.Point(499, 26);
            this.layoutControlItemAccountantResponsible.Name = "layoutControlItemAccountantResponsible";
            this.layoutControlItemAccountantResponsible.Size = new System.Drawing.Size(499, 26);
            this.layoutControlItemAccountantResponsible.Text = "Ответственный:";
            this.layoutControlItemAccountantResponsible.TextSize = new System.Drawing.Size(140, 16);
            // 
            // layoutControlItemRouteSheet
            // 
            this.layoutControlItemRouteSheet.Control = this.btnRouteSheet;
            this.layoutControlItemRouteSheet.Location = new System.Drawing.Point(499, 52);
            this.layoutControlItemRouteSheet.Name = "layoutControlItemRouteSheet";
            this.layoutControlItemRouteSheet.Size = new System.Drawing.Size(499, 26);
            this.layoutControlItemRouteSheet.Text = "Маршрутный лист:";
            this.layoutControlItemRouteSheet.TextSize = new System.Drawing.Size(140, 16);
            // 
            // layoutControlItemInfo
            // 
            this.layoutControlItemInfo.Control = this.memoInfo;
            this.layoutControlItemInfo.Location = new System.Drawing.Point(0, 402);
            this.layoutControlItemInfo.Name = "layoutControlItemInfo";
            this.layoutControlItemInfo.Size = new System.Drawing.Size(998, 48);
            this.layoutControlItemInfo.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemInfo.TextVisible = false;
            // 
            // layoutControlItemStatus
            // 
            this.layoutControlItemStatus.Control = this.cmbStatus;
            this.layoutControlItemStatus.Location = new System.Drawing.Point(307, 0);
            this.layoutControlItemStatus.Name = "layoutControlItemStatus";
            this.layoutControlItemStatus.Size = new System.Drawing.Size(691, 26);
            this.layoutControlItemStatus.Text = "Статус:";
            this.layoutControlItemStatus.TextSize = new System.Drawing.Size(140, 16);
            // 
            // layoutControlItemCustomer
            // 
            this.layoutControlItemCustomer.Control = this.btnCustomer;
            this.layoutControlItemCustomer.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItemCustomer.Name = "layoutControlItemCustomer";
            this.layoutControlItemCustomer.Size = new System.Drawing.Size(499, 26);
            this.layoutControlItemCustomer.Text = "Клиент:";
            this.layoutControlItemCustomer.TextSize = new System.Drawing.Size(140, 16);
            // 
            // layoutControlItemCourier
            // 
            this.layoutControlItemCourier.Control = this.btnCourier;
            this.layoutControlItemCourier.CustomizationFormText = "Курьер:";
            this.layoutControlItemCourier.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItemCourier.Name = "layoutControlItemCourier";
            this.layoutControlItemCourier.Size = new System.Drawing.Size(499, 26);
            this.layoutControlItemCourier.Text = "Курьер:";
            this.layoutControlItemCourier.TextSize = new System.Drawing.Size(140, 16);
            // 
            // imgTaskCourier
            // 
            this.imgTaskCourier.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imgTaskCourier.ImageStream")));
            this.imgTaskCourier.Images.SetKeyName(0, "apply_16x16.png");
            this.imgTaskCourier.Images.SetKeyName(1, "cancel_16x16.png");
            // 
            // popupMenuTasksCourier
            // 
            this.popupMenuTasksCourier.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnAddReportChange),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnBarEdit),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnBarDel),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnFormationRouteSheets, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnRefreshReportChange, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnSaveLayoutToXmlMainGrid, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSettingArchiveFolderChange),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnTaskAdd, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnControlSystemAddTaskCourier, true)});
            this.popupMenuTasksCourier.Manager = this.barManagerTasksCourier;
            this.popupMenuTasksCourier.Name = "popupMenuTasksCourier";
            // 
            // TaskRouteSheetv2Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 460);
            this.Controls.Add(this.layoutControlTaskRouteSheetv2);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TaskRouteSheetv2Form";
            this.Text = "Задачи для курьерской службы";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.TaskCourierForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlTasksCourier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTasksCourier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRouteSheet.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlTaskRouteSheetv2)).EndInit();
            this.layoutControlTaskRouteSheetv2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dateDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAccountantResponsible.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoInfo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManagerTasksCourier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCourier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAccountantResponsible)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemRouteSheet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCourier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgTaskCourier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuTasksCourier)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlTasksCourier;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewTasksCourier;
        private DevExpress.XtraEditors.ComboBoxEdit cmbStatus;
        private DevExpress.XtraEditors.ButtonEdit btnCustomer;
        private DevExpress.XtraBars.BarManager barManagerTasksCourier;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btnAddReportChange;
        private DevExpress.XtraBars.BarButtonItem btnBarEdit;
        private DevExpress.XtraBars.BarButtonItem btnBarDel;
        private DevExpress.XtraEditors.ButtonEdit btnAccountantResponsible;
        private DevExpress.XtraBars.BarButtonItem btnRefreshReportChange;
        private DevExpress.XtraBars.BarButtonItem btnSettingArchiveFolderChange;
        private DevExpress.XtraEditors.DateEdit dateDate;
        private DevExpress.XtraEditors.ButtonEdit btnRouteSheet;
        private DevExpress.Utils.ImageCollection imgTaskCourier;
        private DevExpress.XtraBars.PopupMenu popupMenuTasksCourier;
        private DevExpress.XtraBars.BarButtonItem barBtnSaveLayoutToXmlMainGrid;
        private DevExpress.XtraBars.BarButtonItem barBtnFormationRouteSheets;
        private DevExpress.XtraBars.BarButtonItem barBtnTaskAdd;
        private DevExpress.XtraBars.BarButtonItem barBtnControlSystemAddTaskCourier;
        private DevExpress.XtraLayout.LayoutControl layoutControlTaskRouteSheetv2;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemGridControl;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemDate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemStatus;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCustomer;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemAccountantResponsible;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemRouteSheet;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemInfo;
        private DevExpress.XtraEditors.MemoEdit memoInfo;
        private DevExpress.XtraEditors.ButtonEdit btnCourier;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCourier;
    }
}