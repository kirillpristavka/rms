namespace RMS.UI.Forms
{
    partial class ArchiveFolderChangeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ArchiveFolderChangeForm));
            this.gridControlArchiveFolders = new DevExpress.XtraGrid.GridControl();
            this.gridViewArchiveFolders = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControlHeader = new DevExpress.XtraEditors.PanelControl();
            this.btnFreeBoxes = new DevExpress.XtraEditors.SimpleButton();
            this.lblPeriod = new DevExpress.XtraEditors.LabelControl();
            this.lblAccountantResponsible = new DevExpress.XtraEditors.LabelControl();
            this.btnAccountantResponsible = new DevExpress.XtraEditors.ButtonEdit();
            this.lblStatusArchiveFolder = new DevExpress.XtraEditors.LabelControl();
            this.lblCustomer = new DevExpress.XtraEditors.LabelControl();
            this.cmbStatusArchiveFolder = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnCustomer = new DevExpress.XtraEditors.ButtonEdit();
            this.lblArchiveFolder = new DevExpress.XtraEditors.LabelControl();
            this.btnArchiveFolder = new DevExpress.XtraEditors.ButtonEdit();
            this.cmbPeriod = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbYear = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnArchiveFolderChangeForm = new DevExpress.XtraEditors.SimpleButton();
            this.barArchiveFolderChange = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.btnAddReportChange = new DevExpress.XtraBars.BarButtonItem();
            this.btnEditReportChange = new DevExpress.XtraBars.BarButtonItem();
            this.btnDelReportChange = new DevExpress.XtraBars.BarButtonItem();
            this.btnRefreshReportChange = new DevExpress.XtraBars.BarButtonItem();
            this.btnSettingArchiveFolderChange = new DevExpress.XtraBars.BarButtonItem();
            this.barSubPrint = new DevExpress.XtraBars.BarSubItem();
            this.btnPrintAcceptanceCertificate = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnSaveLayoutToXmlMainGrid = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnTaskAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnControlSystemAdd = new DevExpress.XtraBars.BarButtonItem();
            this.popupArchiveFolderChange = new DevExpress.XtraBars.PopupMenu(this.components);
            this.barBtnBulkReplacement = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlArchiveFolders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewArchiveFolders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlHeader)).BeginInit();
            this.panelControlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAccountantResponsible.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStatusArchiveFolder.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnArchiveFolder.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPeriod.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbYear.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barArchiveFolderChange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupArchiveFolderChange)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlArchiveFolders
            // 
            this.gridControlArchiveFolders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlArchiveFolders.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gridControlArchiveFolders.Location = new System.Drawing.Point(0, 112);
            this.gridControlArchiveFolders.MainView = this.gridViewArchiveFolders;
            this.gridControlArchiveFolders.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gridControlArchiveFolders.Name = "gridControlArchiveFolders";
            this.gridControlArchiveFolders.Size = new System.Drawing.Size(1260, 406);
            this.gridControlArchiveFolders.TabIndex = 23;
            this.gridControlArchiveFolders.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewArchiveFolders});
            // 
            // gridViewArchiveFolders
            // 
            this.gridViewArchiveFolders.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridViewArchiveFolders.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridViewArchiveFolders.DetailHeight = 394;
            this.gridViewArchiveFolders.GridControl = this.gridControlArchiveFolders;
            this.gridViewArchiveFolders.Name = "gridViewArchiveFolders";
            this.gridViewArchiveFolders.OptionsBehavior.Editable = false;
            this.gridViewArchiveFolders.OptionsSelection.MultiSelect = true;
            this.gridViewArchiveFolders.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewArchiveFolders.OptionsView.ShowGroupPanel = false;
            this.gridViewArchiveFolders.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridViewReports_RowStyle);
            this.gridViewArchiveFolders.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridViewReports_MouseDown);
            // 
            // panelControlHeader
            // 
            this.panelControlHeader.Controls.Add(this.btnFreeBoxes);
            this.panelControlHeader.Controls.Add(this.lblPeriod);
            this.panelControlHeader.Controls.Add(this.lblAccountantResponsible);
            this.panelControlHeader.Controls.Add(this.btnAccountantResponsible);
            this.panelControlHeader.Controls.Add(this.lblStatusArchiveFolder);
            this.panelControlHeader.Controls.Add(this.lblCustomer);
            this.panelControlHeader.Controls.Add(this.cmbStatusArchiveFolder);
            this.panelControlHeader.Controls.Add(this.btnCustomer);
            this.panelControlHeader.Controls.Add(this.lblArchiveFolder);
            this.panelControlHeader.Controls.Add(this.btnArchiveFolder);
            this.panelControlHeader.Controls.Add(this.cmbPeriod);
            this.panelControlHeader.Controls.Add(this.cmbYear);
            this.panelControlHeader.Controls.Add(this.btnArchiveFolderChangeForm);
            this.panelControlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControlHeader.Location = new System.Drawing.Point(0, 0);
            this.panelControlHeader.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panelControlHeader.Name = "panelControlHeader";
            this.panelControlHeader.Size = new System.Drawing.Size(1260, 112);
            this.panelControlHeader.TabIndex = 35;
            // 
            // btnFreeBoxes
            // 
            this.btnFreeBoxes.Location = new System.Drawing.Point(965, 45);
            this.btnFreeBoxes.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnFreeBoxes.Name = "btnFreeBoxes";
            this.btnFreeBoxes.Size = new System.Drawing.Size(280, 26);
            this.btnFreeBoxes.TabIndex = 17;
            this.btnFreeBoxes.Text = "Свободные коробки";
            this.btnFreeBoxes.Click += new System.EventHandler(this.btnFreeBoxes_Click);
            // 
            // lblPeriod
            // 
            this.lblPeriod.Location = new System.Drawing.Point(94, 80);
            this.lblPeriod.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new System.Drawing.Size(69, 18);
            this.lblPeriod.TabIndex = 15;
            this.lblPeriod.Text = "Период:";
            // 
            // lblAccountantResponsible
            // 
            this.lblAccountantResponsible.Location = new System.Drawing.Point(358, 17);
            this.lblAccountantResponsible.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.lblAccountantResponsible.Name = "lblAccountantResponsible";
            this.lblAccountantResponsible.Size = new System.Drawing.Size(132, 18);
            this.lblAccountantResponsible.TabIndex = 7;
            this.lblAccountantResponsible.Text = "Ответственный:";
            // 
            // btnAccountantResponsible
            // 
            this.btnAccountantResponsible.Location = new System.Drawing.Point(508, 14);
            this.btnAccountantResponsible.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnAccountantResponsible.Name = "btnAccountantResponsible";
            this.btnAccountantResponsible.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.btnAccountantResponsible.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnAccountantResponsible.Size = new System.Drawing.Size(438, 24);
            this.btnAccountantResponsible.TabIndex = 8;
            this.btnAccountantResponsible.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnAccountantResponsible_ButtonPressed);
            this.btnAccountantResponsible.EditValueChanged += new System.EventHandler(this.Filter);
            // 
            // lblStatusArchiveFolder
            // 
            this.lblStatusArchiveFolder.Location = new System.Drawing.Point(965, 17);
            this.lblStatusArchiveFolder.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.lblStatusArchiveFolder.Name = "lblStatusArchiveFolder";
            this.lblStatusArchiveFolder.Size = new System.Drawing.Size(60, 18);
            this.lblStatusArchiveFolder.TabIndex = 11;
            this.lblStatusArchiveFolder.Text = "Статус:";
            // 
            // lblCustomer
            // 
            this.lblCustomer.Location = new System.Drawing.Point(358, 48);
            this.lblCustomer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(65, 18);
            this.lblCustomer.TabIndex = 9;
            this.lblCustomer.Text = "Клиент:";
            // 
            // cmbStatusArchiveFolder
            // 
            this.cmbStatusArchiveFolder.Location = new System.Drawing.Point(1050, 14);
            this.cmbStatusArchiveFolder.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbStatusArchiveFolder.Name = "cmbStatusArchiveFolder";
            this.cmbStatusArchiveFolder.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.cmbStatusArchiveFolder.Properties.DropDownRows = 12;
            this.cmbStatusArchiveFolder.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbStatusArchiveFolder.Size = new System.Drawing.Size(195, 24);
            this.cmbStatusArchiveFolder.TabIndex = 12;
            this.cmbStatusArchiveFolder.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.cmb_ButtonPressed);
            this.cmbStatusArchiveFolder.EditValueChanged += new System.EventHandler(this.Filter);
            // 
            // btnCustomer
            // 
            this.btnCustomer.Location = new System.Drawing.Point(445, 45);
            this.btnCustomer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCustomer.Name = "btnCustomer";
            this.btnCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.btnCustomer.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnCustomer.Size = new System.Drawing.Size(500, 24);
            this.btnCustomer.TabIndex = 10;
            this.btnCustomer.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnCustomer_ButtonPressed);
            this.btnCustomer.EditValueChanged += new System.EventHandler(this.Filter);
            // 
            // lblArchiveFolder
            // 
            this.lblArchiveFolder.Location = new System.Drawing.Point(365, 80);
            this.lblArchiveFolder.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.lblArchiveFolder.Name = "lblArchiveFolder";
            this.lblArchiveFolder.Size = new System.Drawing.Size(58, 18);
            this.lblArchiveFolder.TabIndex = 13;
            this.lblArchiveFolder.Text = "Папка:";
            // 
            // btnArchiveFolder
            // 
            this.btnArchiveFolder.Location = new System.Drawing.Point(445, 76);
            this.btnArchiveFolder.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnArchiveFolder.Name = "btnArchiveFolder";
            this.btnArchiveFolder.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.btnArchiveFolder.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnArchiveFolder.Size = new System.Drawing.Size(500, 24);
            this.btnArchiveFolder.TabIndex = 14;
            this.btnArchiveFolder.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnArchiveFolder_ButtonPressed);
            this.btnArchiveFolder.EditValueChanged += new System.EventHandler(this.Filter);
            // 
            // cmbPeriod
            // 
            this.cmbPeriod.EditValue = "";
            this.cmbPeriod.Location = new System.Drawing.Point(176, 76);
            this.cmbPeriod.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbPeriod.Name = "cmbPeriod";
            this.cmbPeriod.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.cmbPeriod.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbPeriod.Size = new System.Drawing.Size(154, 24);
            this.cmbPeriod.TabIndex = 5;
            this.cmbPeriod.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.cmb_ButtonPressed);
            this.cmbPeriod.EditValueChanged += new System.EventHandler(this.Filter);
            // 
            // cmbYear
            // 
            this.cmbYear.Location = new System.Drawing.Point(176, 6);
            this.cmbYear.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbYear.Name = "cmbYear";
            this.cmbYear.Properties.Appearance.Options.UseTextOptions = true;
            this.cmbYear.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.cmbYear.Properties.AppearanceDropDown.Options.UseTextOptions = true;
            this.cmbYear.Properties.AppearanceDropDown.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.cmbYear.Properties.AppearanceItemSelected.Options.UseTextOptions = true;
            this.cmbYear.Properties.AppearanceItemSelected.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.cmbYear.Properties.AutoHeight = false;
            this.cmbYear.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.cmbYear.Properties.DropDownRows = 10;
            this.cmbYear.Properties.Items.AddRange(new object[] {
            "2025",
            "2024",
            "2023",
            "2022",
            "2021",
            "2020",
            "2019",
            "2018",
            "2017",
            "2016",
            "2015",
            "2014",
            "2013",
            "2012",
            "2011",
            "2010"});
            this.cmbYear.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbYear.Size = new System.Drawing.Size(154, 56);
            this.cmbYear.TabIndex = 2;
            this.cmbYear.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.cmb_ButtonPressed);
            this.cmbYear.EditValueChanged += new System.EventHandler(this.Filter);
            // 
            // btnArchiveFolderChangeForm
            // 
            this.btnArchiveFolderChangeForm.Location = new System.Drawing.Point(6, 6);
            this.btnArchiveFolderChangeForm.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnArchiveFolderChangeForm.Name = "btnArchiveFolderChangeForm";
            this.btnArchiveFolderChangeForm.Size = new System.Drawing.Size(156, 56);
            this.btnArchiveFolderChangeForm.TabIndex = 1;
            this.btnArchiveFolderChangeForm.Text = "Сформировать\r\nархивные папки";
            this.btnArchiveFolderChangeForm.Click += new System.EventHandler(this.btnArchiveFolderForm_Click);
            // 
            // barArchiveFolderChange
            // 
            this.barArchiveFolderChange.DockControls.Add(this.barDockControlTop);
            this.barArchiveFolderChange.DockControls.Add(this.barDockControlBottom);
            this.barArchiveFolderChange.DockControls.Add(this.barDockControlLeft);
            this.barArchiveFolderChange.DockControls.Add(this.barDockControlRight);
            this.barArchiveFolderChange.Form = this;
            this.barArchiveFolderChange.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnAddReportChange,
            this.btnEditReportChange,
            this.btnDelReportChange,
            this.btnRefreshReportChange,
            this.btnSettingArchiveFolderChange,
            this.barSubPrint,
            this.btnPrintAcceptanceCertificate,
            this.barBtnSaveLayoutToXmlMainGrid,
            this.barBtnTaskAdd,
            this.barBtnControlSystemAdd,
            this.barBtnBulkReplacement});
            this.barArchiveFolderChange.MaxItemId = 14;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barArchiveFolderChange;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.barDockControlTop.Size = new System.Drawing.Size(1260, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 518);
            this.barDockControlBottom.Manager = this.barArchiveFolderChange;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.barDockControlBottom.Size = new System.Drawing.Size(1260, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barArchiveFolderChange;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 518);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1260, 0);
            this.barDockControlRight.Manager = this.barArchiveFolderChange;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 518);
            // 
            // btnAddReportChange
            // 
            this.btnAddReportChange.Caption = "Добавить";
            this.btnAddReportChange.Id = 0;
            this.btnAddReportChange.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAddReportChange.ImageOptions.Image")));
            this.btnAddReportChange.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnAddReportChange.ImageOptions.LargeImage")));
            this.btnAddReportChange.Name = "btnAddReportChange";
            this.btnAddReportChange.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAddReportChange_ItemClick);
            // 
            // btnEditReportChange
            // 
            this.btnEditReportChange.Caption = "Изменить";
            this.btnEditReportChange.Id = 2;
            this.btnEditReportChange.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEditReportChange.ImageOptions.Image")));
            this.btnEditReportChange.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnEditReportChange.ImageOptions.LargeImage")));
            this.btnEditReportChange.Name = "btnEditReportChange";
            this.btnEditReportChange.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEditReportChange_ItemClick);
            // 
            // btnDelReportChange
            // 
            this.btnDelReportChange.Caption = "Удалить";
            this.btnDelReportChange.Id = 3;
            this.btnDelReportChange.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDelReportChange.ImageOptions.Image")));
            this.btnDelReportChange.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnDelReportChange.ImageOptions.LargeImage")));
            this.btnDelReportChange.Name = "btnDelReportChange";
            this.btnDelReportChange.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDelReportChange_ItemClick);
            // 
            // btnRefreshReportChange
            // 
            this.btnRefreshReportChange.Caption = "Обновить";
            this.btnRefreshReportChange.Id = 5;
            this.btnRefreshReportChange.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnRefreshReportChange.ImageOptions.Image")));
            this.btnRefreshReportChange.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnRefreshReportChange.ImageOptions.LargeImage")));
            this.btnRefreshReportChange.Name = "btnRefreshReportChange";
            this.btnRefreshReportChange.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRefreshReportChange_ItemClick);
            // 
            // btnSettingArchiveFolderChange
            // 
            this.btnSettingArchiveFolderChange.Caption = "Настройка";
            this.btnSettingArchiveFolderChange.Id = 6;
            this.btnSettingArchiveFolderChange.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSettingArchiveFolderChange.ImageOptions.Image")));
            this.btnSettingArchiveFolderChange.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnSettingArchiveFolderChange.ImageOptions.LargeImage")));
            this.btnSettingArchiveFolderChange.Name = "btnSettingArchiveFolderChange";
            this.btnSettingArchiveFolderChange.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSettingArchiveFolderChange_ItemClick);
            // 
            // barSubPrint
            // 
            this.barSubPrint.Caption = "Печать";
            this.barSubPrint.Id = 8;
            this.barSubPrint.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barSubPrint.ImageOptions.Image")));
            this.barSubPrint.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barSubPrint.ImageOptions.LargeImage")));
            this.barSubPrint.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnPrintAcceptanceCertificate)});
            this.barSubPrint.Name = "barSubPrint";
            // 
            // btnPrintAcceptanceCertificate
            // 
            this.btnPrintAcceptanceCertificate.Caption = "АКТ приема-передачи документов";
            this.btnPrintAcceptanceCertificate.Id = 9;
            this.btnPrintAcceptanceCertificate.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPrintAcceptanceCertificate.ImageOptions.Image")));
            this.btnPrintAcceptanceCertificate.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnPrintAcceptanceCertificate.ImageOptions.LargeImage")));
            this.btnPrintAcceptanceCertificate.Name = "btnPrintAcceptanceCertificate";
            this.btnPrintAcceptanceCertificate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnPrintAcceptanceCertificate_ItemClick);
            // 
            // barBtnSaveLayoutToXmlMainGrid
            // 
            this.barBtnSaveLayoutToXmlMainGrid.Caption = "Сохранить отображение";
            this.barBtnSaveLayoutToXmlMainGrid.Id = 10;
            this.barBtnSaveLayoutToXmlMainGrid.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnSaveLayoutToXmlMainGrid.ImageOptions.Image")));
            this.barBtnSaveLayoutToXmlMainGrid.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnSaveLayoutToXmlMainGrid.ImageOptions.LargeImage")));
            this.barBtnSaveLayoutToXmlMainGrid.Name = "barBtnSaveLayoutToXmlMainGrid";
            this.barBtnSaveLayoutToXmlMainGrid.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnSaveLayoutToXmlMainGrid_ItemClick);
            // 
            // barBtnTaskAdd
            // 
            this.barBtnTaskAdd.Caption = "Добавить задачу";
            this.barBtnTaskAdd.Id = 11;
            this.barBtnTaskAdd.Name = "barBtnTaskAdd";
            this.barBtnTaskAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnTaskAdd_ItemClick);
            // 
            // barBtnControlSystemAdd
            // 
            this.barBtnControlSystemAdd.Caption = "Контроль";
            this.barBtnControlSystemAdd.Id = 12;
            this.barBtnControlSystemAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnControlSystemAdd.ImageOptions.Image")));
            this.barBtnControlSystemAdd.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnControlSystemAdd.ImageOptions.LargeImage")));
            this.barBtnControlSystemAdd.Name = "barBtnControlSystemAdd";
            this.barBtnControlSystemAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnControlSystemAdd_ItemClick);
            // 
            // popupArchiveFolderChange
            // 
            this.popupArchiveFolderChange.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnAddReportChange),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnEditReportChange),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnDelReportChange),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnBulkReplacement, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnRefreshReportChange, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubPrint, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnSaveLayoutToXmlMainGrid, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSettingArchiveFolderChange),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnTaskAdd, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnControlSystemAdd, true)});
            this.popupArchiveFolderChange.Manager = this.barArchiveFolderChange;
            this.popupArchiveFolderChange.Name = "popupArchiveFolderChange";
            // 
            // barBtnBulkReplacement
            // 
            this.barBtnBulkReplacement.Caption = "Массовая замена параметров";
            this.barBtnBulkReplacement.Id = 13;
            this.barBtnBulkReplacement.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnBulkReplacement.ImageOptions.Image")));
            this.barBtnBulkReplacement.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnBulkReplacement.ImageOptions.LargeImage")));
            this.barBtnBulkReplacement.Name = "barBtnBulkReplacement";
            this.barBtnBulkReplacement.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnBulkReplacement_ItemClick);
            // 
            // ArchiveFolderChangeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1260, 518);
            this.Controls.Add(this.gridControlArchiveFolders);
            this.Controls.Add(this.panelControlHeader);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ArchiveFolderChangeForm";
            this.Text = "Архивные папки";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlArchiveFolders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewArchiveFolders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlHeader)).EndInit();
            this.panelControlHeader.ResumeLayout(false);
            this.panelControlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAccountantResponsible.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStatusArchiveFolder.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnArchiveFolder.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPeriod.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbYear.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barArchiveFolderChange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupArchiveFolderChange)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlArchiveFolders;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewArchiveFolders;
        private DevExpress.XtraEditors.PanelControl panelControlHeader;
        private DevExpress.XtraEditors.ComboBoxEdit cmbYear;
        private DevExpress.XtraEditors.SimpleButton btnArchiveFolderChangeForm;
        private DevExpress.XtraEditors.ComboBoxEdit cmbPeriod;
        private DevExpress.XtraEditors.LabelControl lblStatusArchiveFolder;
        private DevExpress.XtraEditors.LabelControl lblCustomer;
        private DevExpress.XtraEditors.ComboBoxEdit cmbStatusArchiveFolder;
        private DevExpress.XtraEditors.ButtonEdit btnCustomer;
        private DevExpress.XtraEditors.LabelControl lblArchiveFolder;
        private DevExpress.XtraEditors.ButtonEdit btnArchiveFolder;
        private DevExpress.XtraBars.PopupMenu popupArchiveFolderChange;
        private DevExpress.XtraBars.BarManager barArchiveFolderChange;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btnAddReportChange;
        private DevExpress.XtraBars.BarButtonItem btnEditReportChange;
        private DevExpress.XtraBars.BarButtonItem btnDelReportChange;
        private DevExpress.XtraEditors.LabelControl lblAccountantResponsible;
        private DevExpress.XtraEditors.ButtonEdit btnAccountantResponsible;
        private DevExpress.XtraBars.BarButtonItem btnRefreshReportChange;
        private DevExpress.XtraEditors.LabelControl lblPeriod;
        private DevExpress.XtraBars.BarButtonItem btnSettingArchiveFolderChange;
        private DevExpress.XtraBars.BarSubItem barSubPrint;
        private DevExpress.XtraBars.BarButtonItem btnPrintAcceptanceCertificate;
        private DevExpress.XtraBars.BarButtonItem barBtnSaveLayoutToXmlMainGrid;
        private DevExpress.XtraEditors.SimpleButton btnFreeBoxes;
        private DevExpress.XtraBars.BarButtonItem barBtnTaskAdd;
        private DevExpress.XtraBars.BarButtonItem barBtnControlSystemAdd;
        private DevExpress.XtraBars.BarButtonItem barBtnBulkReplacement;
    }
}