namespace RMS.UI.Forms.CourierService.v1
{
    partial class TaskCourierForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskCourierForm));
            this.gridControlTasksCourier = new DevExpress.XtraGrid.GridControl();
            this.gridViewTasksCourier = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnRouteSheet = new DevExpress.XtraEditors.ButtonEdit();
            this.layoutControlTaskCourier = new DevExpress.XtraLayout.LayoutControl();
            this.dateDate = new DevExpress.XtraEditors.DateEdit();
            this.btnAccountantResponsible = new DevExpress.XtraEditors.ButtonEdit();
            this.btnCourier = new DevExpress.XtraEditors.ButtonEdit();
            this.cmbStatusTaskCourier = new DevExpress.XtraEditors.ComboBoxEdit();
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
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemInfo = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.imgTaskCourier = new DevExpress.Utils.ImageCollection(this.components);
            this.popupMenuTasksCourier = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlTasksCourier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTasksCourier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRouteSheet.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlTaskCourier)).BeginInit();
            this.layoutControlTaskCourier.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAccountantResponsible.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCourier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStatusTaskCourier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoInfo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManagerTasksCourier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgTaskCourier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuTasksCourier)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlTasksCourier
            // 
            this.gridControlTasksCourier.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.gridControlTasksCourier.Location = new System.Drawing.Point(10, 89);
            this.gridControlTasksCourier.MainView = this.gridViewTasksCourier;
            this.gridControlTasksCourier.Name = "gridControlTasksCourier";
            this.gridControlTasksCourier.Size = new System.Drawing.Size(988, 312);
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
            this.btnRouteSheet.Location = new System.Drawing.Point(648, 63);
            this.btnRouteSheet.Name = "btnRouteSheet";
            this.btnRouteSheet.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.btnRouteSheet.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnRouteSheet.Size = new System.Drawing.Size(350, 22);
            this.btnRouteSheet.StyleController = this.layoutControlTaskCourier;
            this.btnRouteSheet.TabIndex = 57;
            this.btnRouteSheet.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnRouteSheet_ButtonPressed);
            this.btnRouteSheet.EditValueChanged += new System.EventHandler(this.Filter);
            // 
            // layoutControlTaskCourier
            // 
            this.layoutControlTaskCourier.AllowCustomization = false;
            this.layoutControlTaskCourier.Controls.Add(this.btnRouteSheet);
            this.layoutControlTaskCourier.Controls.Add(this.gridControlTasksCourier);
            this.layoutControlTaskCourier.Controls.Add(this.dateDate);
            this.layoutControlTaskCourier.Controls.Add(this.btnAccountantResponsible);
            this.layoutControlTaskCourier.Controls.Add(this.btnCourier);
            this.layoutControlTaskCourier.Controls.Add(this.cmbStatusTaskCourier);
            this.layoutControlTaskCourier.Controls.Add(this.btnCustomer);
            this.layoutControlTaskCourier.Controls.Add(this.memoInfo);
            this.layoutControlTaskCourier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlTaskCourier.Location = new System.Drawing.Point(0, 0);
            this.layoutControlTaskCourier.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.layoutControlTaskCourier.Name = "layoutControlTaskCourier";
            this.layoutControlTaskCourier.Root = this.Root;
            this.layoutControlTaskCourier.Size = new System.Drawing.Size(1008, 460);
            this.layoutControlTaskCourier.TabIndex = 40;
            this.layoutControlTaskCourier.Text = "layoutControl1";
            // 
            // dateDate
            // 
            this.dateDate.EditValue = null;
            this.dateDate.Location = new System.Drawing.Point(152, 11);
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
            this.dateDate.StyleController = this.layoutControlTaskCourier;
            this.dateDate.TabIndex = 55;
            this.dateDate.TabStop = false;
            this.dateDate.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.cmb_ButtonPressed);
            this.dateDate.EditValueChanged += new System.EventHandler(this.Filter);
            // 
            // btnAccountantResponsible
            // 
            this.btnAccountantResponsible.Location = new System.Drawing.Point(648, 37);
            this.btnAccountantResponsible.Name = "btnAccountantResponsible";
            this.btnAccountantResponsible.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.btnAccountantResponsible.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnAccountantResponsible.Size = new System.Drawing.Size(350, 22);
            this.btnAccountantResponsible.StyleController = this.layoutControlTaskCourier;
            this.btnAccountantResponsible.TabIndex = 8;
            this.btnAccountantResponsible.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnAccountantResponsible_ButtonPressed);
            this.btnAccountantResponsible.EditValueChanged += new System.EventHandler(this.Filter);
            // 
            // btnCourier
            // 
            this.btnCourier.Location = new System.Drawing.Point(152, 63);
            this.btnCourier.Name = "btnCourier";
            this.btnCourier.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.btnCourier.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnCourier.Size = new System.Drawing.Size(350, 22);
            this.btnCourier.StyleController = this.layoutControlTaskCourier;
            this.btnCourier.TabIndex = 14;
            this.btnCourier.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnCourier_ButtonPressed);
            this.btnCourier.EditValueChanged += new System.EventHandler(this.Filter);
            // 
            // cmbStatusTaskCourier
            // 
            this.cmbStatusTaskCourier.Location = new System.Drawing.Point(458, 11);
            this.cmbStatusTaskCourier.MaximumSize = new System.Drawing.Size(160, 0);
            this.cmbStatusTaskCourier.Name = "cmbStatusTaskCourier";
            this.cmbStatusTaskCourier.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.cmbStatusTaskCourier.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbStatusTaskCourier.Size = new System.Drawing.Size(160, 22);
            this.cmbStatusTaskCourier.StyleController = this.layoutControlTaskCourier;
            this.cmbStatusTaskCourier.TabIndex = 12;
            this.cmbStatusTaskCourier.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.cmb_ButtonPressed);
            this.cmbStatusTaskCourier.EditValueChanged += new System.EventHandler(this.Filter);
            // 
            // btnCustomer
            // 
            this.btnCustomer.Location = new System.Drawing.Point(152, 37);
            this.btnCustomer.Name = "btnCustomer";
            this.btnCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.btnCustomer.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnCustomer.Size = new System.Drawing.Size(350, 22);
            this.btnCustomer.StyleController = this.layoutControlTaskCourier;
            this.btnCustomer.TabIndex = 10;
            this.btnCustomer.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnCustomer_ButtonPressed);
            this.btnCustomer.EditValueChanged += new System.EventHandler(this.Filter);
            // 
            // memoInfo
            // 
            this.memoInfo.EditValue = "Как работать с разделом: добавляем курьерские задачи. Нажимаем ПКМ -> сформироват" +
    "ь маршрутный лист.";
            this.memoInfo.Location = new System.Drawing.Point(10, 405);
            this.memoInfo.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.memoInfo.MaximumSize = new System.Drawing.Size(0, 44);
            this.memoInfo.MenuManager = this.barManagerTasksCourier;
            this.memoInfo.Name = "memoInfo";
            this.memoInfo.Properties.Appearance.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.memoInfo.Properties.Appearance.Options.UseBackColor = true;
            this.memoInfo.Properties.ReadOnly = true;
            this.memoInfo.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.memoInfo.Size = new System.Drawing.Size(988, 44);
            this.memoInfo.StyleController = this.layoutControlTaskCourier;
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
            // Root
            // 
            this.Root.AppearanceItemCaption.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.Root.AppearanceItemCaption.Options.UseFont = true;
            this.Root.AppearanceItemCaption.Options.UseTextOptions = true;
            this.Root.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItemInfo,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1008, 460);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControlTasksCourier;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 78);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(992, 316);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.dateDate;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(306, 26);
            this.layoutControlItem2.Text = "Дата:";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(140, 16);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnAccountantResponsible;
            this.layoutControlItem5.Location = new System.Drawing.Point(496, 26);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(496, 26);
            this.layoutControlItem5.Text = "Ответственный:";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(140, 16);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnCourier;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(496, 26);
            this.layoutControlItem6.Text = "Курьер:";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(140, 16);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnRouteSheet;
            this.layoutControlItem7.Location = new System.Drawing.Point(496, 52);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(496, 26);
            this.layoutControlItem7.Text = "Маршрутный лист:";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(140, 16);
            // 
            // layoutControlItemInfo
            // 
            this.layoutControlItemInfo.Control = this.memoInfo;
            this.layoutControlItemInfo.Location = new System.Drawing.Point(0, 394);
            this.layoutControlItemInfo.Name = "layoutControlItemInfo";
            this.layoutControlItemInfo.Size = new System.Drawing.Size(992, 48);
            this.layoutControlItemInfo.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemInfo.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.cmbStatusTaskCourier;
            this.layoutControlItem3.Location = new System.Drawing.Point(306, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(686, 26);
            this.layoutControlItem3.Text = "Статус:";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(140, 16);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnCustomer;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(496, 26);
            this.layoutControlItem4.Text = "Клиент:";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(140, 16);
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
            // TaskCourierForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 460);
            this.Controls.Add(this.layoutControlTaskCourier);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TaskCourierForm";
            this.Text = "Задачи для курьерской службы";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.TaskCourierForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlTasksCourier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTasksCourier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRouteSheet.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlTaskCourier)).EndInit();
            this.layoutControlTaskCourier.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dateDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAccountantResponsible.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCourier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStatusTaskCourier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoInfo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManagerTasksCourier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgTaskCourier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuTasksCourier)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlTasksCourier;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewTasksCourier;
        private DevExpress.XtraEditors.ComboBoxEdit cmbStatusTaskCourier;
        private DevExpress.XtraEditors.ButtonEdit btnCustomer;
        private DevExpress.XtraEditors.ButtonEdit btnCourier;
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
        private DevExpress.XtraLayout.LayoutControl layoutControlTaskCourier;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemInfo;
        private DevExpress.XtraEditors.MemoEdit memoInfo;
    }
}