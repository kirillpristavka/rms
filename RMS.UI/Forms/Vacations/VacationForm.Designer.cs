namespace RMS.UI.Forms.Vacations
{
    partial class VacationForm
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
            DevExpress.XtraScheduler.TimeRuler timeRuler1 = new DevExpress.XtraScheduler.TimeRuler();
            DevExpress.XtraScheduler.TimeRuler timeRuler2 = new DevExpress.XtraScheduler.TimeRuler();
            DevExpress.XtraScheduler.TimeScaleYear timeScaleYear1 = new DevExpress.XtraScheduler.TimeScaleYear();
            DevExpress.XtraScheduler.TimeScaleQuarter timeScaleQuarter1 = new DevExpress.XtraScheduler.TimeScaleQuarter();
            DevExpress.XtraScheduler.TimeScaleMonth timeScaleMonth1 = new DevExpress.XtraScheduler.TimeScaleMonth();
            DevExpress.XtraScheduler.TimeScaleWeek timeScaleWeek1 = new DevExpress.XtraScheduler.TimeScaleWeek();
            DevExpress.XtraScheduler.TimeScaleDay timeScaleDay1 = new DevExpress.XtraScheduler.TimeScaleDay();
            DevExpress.XtraScheduler.TimeScaleHour timeScaleHour1 = new DevExpress.XtraScheduler.TimeScaleHour();
            DevExpress.XtraScheduler.TimeScale15Minutes timeScale15Minutes1 = new DevExpress.XtraScheduler.TimeScale15Minutes();
            DevExpress.XtraScheduler.TimeRuler timeRuler3 = new DevExpress.XtraScheduler.TimeRuler();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VacationForm));
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlVacation = new DevExpress.XtraLayout.LayoutControl();
            this.schedulerControl = new DevExpress.XtraScheduler.SchedulerControl();
            this.schedulerDataStorage = new DevExpress.XtraScheduler.SchedulerDataStorage(this.components);
            this.toolTipController = new DevExpress.Utils.ToolTipController(this.components);
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemGridControl = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemSchedulerControl = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItemSchedulerControl = new DevExpress.XtraLayout.SplitterItem();
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barBtnAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnDel = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnEdit = new DevExpress.XtraBars.BarButtonItem();
            this.barBntConfirm = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnUpdate = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItemReport = new DevExpress.XtraBars.BarSubItem();
            this.barBtnCustomerVacationStatistics = new DevExpress.XtraBars.BarButtonItem();
            this.popupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlVacation)).BeginInit();
            this.layoutControlVacation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.schedulerControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.schedulerDataStorage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSchedulerControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItemSchedulerControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl
            // 
            this.gridControl.Location = new System.Drawing.Point(12, 12);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(1072, 164);
            this.gridControl.TabIndex = 34;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.Editable = false;
            this.gridView.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gridView.OptionsView.RowAutoHeight = true;
            this.gridView.OptionsView.ShowGroupPanel = false;
            this.gridView.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView_RowStyle);
            this.gridView.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gridView_PopupMenuShowing);
            // 
            // layoutControlVacation
            // 
            this.layoutControlVacation.AllowCustomization = false;
            this.layoutControlVacation.Controls.Add(this.schedulerControl);
            this.layoutControlVacation.Controls.Add(this.gridControl);
            this.layoutControlVacation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlVacation.Location = new System.Drawing.Point(0, 0);
            this.layoutControlVacation.Name = "layoutControlVacation";
            this.layoutControlVacation.Root = this.Root;
            this.layoutControlVacation.Size = new System.Drawing.Size(1096, 686);
            this.layoutControlVacation.TabIndex = 39;
            this.layoutControlVacation.Text = "layoutControl1";
            // 
            // schedulerControl
            // 
            this.schedulerControl.ActiveViewType = DevExpress.XtraScheduler.SchedulerViewType.Year;
            this.schedulerControl.DataStorage = this.schedulerDataStorage;
            this.schedulerControl.Location = new System.Drawing.Point(12, 192);
            this.schedulerControl.Name = "schedulerControl";
            this.schedulerControl.OptionsCustomization.AllowAppointmentConflicts = DevExpress.XtraScheduler.AppointmentConflictsMode.Forbidden;
            this.schedulerControl.OptionsCustomization.AllowAppointmentCopy = DevExpress.XtraScheduler.UsedAppointmentType.None;
            this.schedulerControl.OptionsCustomization.AllowAppointmentCreate = DevExpress.XtraScheduler.UsedAppointmentType.None;
            this.schedulerControl.OptionsCustomization.AllowAppointmentDelete = DevExpress.XtraScheduler.UsedAppointmentType.None;
            this.schedulerControl.OptionsCustomization.AllowAppointmentDrag = DevExpress.XtraScheduler.UsedAppointmentType.None;
            this.schedulerControl.OptionsCustomization.AllowAppointmentDragBetweenResources = DevExpress.XtraScheduler.UsedAppointmentType.None;
            this.schedulerControl.OptionsCustomization.AllowAppointmentEdit = DevExpress.XtraScheduler.UsedAppointmentType.None;
            this.schedulerControl.OptionsCustomization.AllowAppointmentResize = DevExpress.XtraScheduler.UsedAppointmentType.Recurring;
            this.schedulerControl.OptionsCustomization.AllowDisplayAppointmentDependencyForm = DevExpress.XtraScheduler.AllowDisplayAppointmentDependencyForm.Always;
            this.schedulerControl.OptionsCustomization.AllowDisplayAppointmentFlyout = false;
            this.schedulerControl.OptionsCustomization.AllowDisplayAppointmentForm = DevExpress.XtraScheduler.AllowDisplayAppointmentForm.Never;
            this.schedulerControl.OptionsCustomization.AllowInplaceEditor = DevExpress.XtraScheduler.UsedAppointmentType.None;
            this.schedulerControl.OptionsView.NavigationButtons.Visibility = DevExpress.XtraScheduler.NavigationButtonVisibility.Always;
            this.schedulerControl.Size = new System.Drawing.Size(1072, 482);
            this.schedulerControl.Start = new System.DateTime(2023, 6, 26, 0, 0, 0, 0);
            this.schedulerControl.TabIndex = 35;
            this.schedulerControl.Text = "schedulerControl";
            this.schedulerControl.ToolTipController = this.toolTipController;
            this.schedulerControl.Views.DayView.TimeRulers.Add(timeRuler1);
            this.schedulerControl.Views.FullWeekView.Enabled = true;
            this.schedulerControl.Views.FullWeekView.TimeRulers.Add(timeRuler2);
            this.schedulerControl.Views.GanttView.AppointmentDisplayOptions.AppointmentAutoHeight = true;
            this.schedulerControl.Views.GanttView.AppointmentDisplayOptions.PercentCompleteDisplayType = DevExpress.XtraScheduler.PercentCompleteDisplayType.None;
            timeScaleYear1.Enabled = false;
            timeScaleQuarter1.Enabled = false;
            timeScaleMonth1.DisplayFormat = "MMMM (yyyy)";
            timeScaleWeek1.DisplayFormat = "dd.MM.yyyy";
            timeScaleWeek1.Enabled = false;
            timeScaleDay1.DisplayFormat = "dd";
            timeScaleDay1.Width = 30;
            timeScaleHour1.Enabled = false;
            timeScale15Minutes1.Enabled = false;
            this.schedulerControl.Views.GanttView.Scales.Add(timeScaleYear1);
            this.schedulerControl.Views.GanttView.Scales.Add(timeScaleQuarter1);
            this.schedulerControl.Views.GanttView.Scales.Add(timeScaleMonth1);
            this.schedulerControl.Views.GanttView.Scales.Add(timeScaleWeek1);
            this.schedulerControl.Views.GanttView.Scales.Add(timeScaleDay1);
            this.schedulerControl.Views.GanttView.Scales.Add(timeScaleHour1);
            this.schedulerControl.Views.GanttView.Scales.Add(timeScale15Minutes1);
            this.schedulerControl.Views.MonthView.ShowMoreButtons = false;
            this.schedulerControl.Views.WorkWeekView.TimeRulers.Add(timeRuler3);
            this.schedulerControl.Views.YearView.MonthCount = 6;
            this.schedulerControl.Views.YearView.NavigationButtonVisibility = DevExpress.XtraScheduler.NavigationButtonVisibility.Never;
            this.schedulerControl.Views.YearView.UseOptimizedScrolling = false;
            this.schedulerControl.AppointmentViewInfoCustomizing += new DevExpress.XtraScheduler.AppointmentViewInfoCustomizingEventHandler(this.schedulerControl_AppointmentViewInfoCustomizing);
            this.schedulerControl.PopupMenuShowing += new DevExpress.XtraScheduler.PopupMenuShowingEventHandler(this.schedulerControl_PopupMenuShowing);
            // 
            // schedulerDataStorage
            // 
            // 
            // 
            // 
            this.schedulerDataStorage.AppointmentDependencies.AutoReload = false;
            // 
            // 
            // 
            this.schedulerDataStorage.Appointments.Labels.CreateNewLabel(0, "None", "&None", System.Drawing.SystemColors.Window);
            this.schedulerDataStorage.Appointments.Labels.CreateNewLabel(1, "Important", "&Important", System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(194)))), ((int)(((byte)(190))))));
            this.schedulerDataStorage.Appointments.Labels.CreateNewLabel(2, "Business", "&Business", System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(213)))), ((int)(((byte)(255))))));
            this.schedulerDataStorage.Appointments.Labels.CreateNewLabel(3, "Personal", "&Personal", System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(244)))), ((int)(((byte)(156))))));
            this.schedulerDataStorage.Appointments.Labels.CreateNewLabel(4, "Vacation", "&Vacation", System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(228)))), ((int)(((byte)(199))))));
            this.schedulerDataStorage.Appointments.Labels.CreateNewLabel(5, "Must Attend", "Must &Attend", System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(206)))), ((int)(((byte)(147))))));
            this.schedulerDataStorage.Appointments.Labels.CreateNewLabel(6, "Travel Required", "&Travel Required", System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(244)))), ((int)(((byte)(255))))));
            this.schedulerDataStorage.Appointments.Labels.CreateNewLabel(7, "Needs Preparation", "&Needs Preparation", System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(219)))), ((int)(((byte)(152))))));
            this.schedulerDataStorage.Appointments.Labels.CreateNewLabel(8, "Birthday", "&Birthday", System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(207)))), ((int)(((byte)(233))))));
            this.schedulerDataStorage.Appointments.Labels.CreateNewLabel(9, "Anniversary", "&Anniversary", System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(233)))), ((int)(((byte)(223))))));
            this.schedulerDataStorage.Appointments.Labels.CreateNewLabel(10, "Phone Call", "Phone &Call", System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(247)))), ((int)(((byte)(165))))));
            // 
            // toolTipController
            // 
            this.toolTipController.BeforeShow += new DevExpress.Utils.ToolTipControllerBeforeShowEventHandler(this.toolTipController_BeforeShow);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemGridControl,
            this.layoutControlItemSchedulerControl,
            this.splitterItemSchedulerControl});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1096, 686);
            this.Root.TextVisible = false;
            // 
            // layoutControlItemGridControl
            // 
            this.layoutControlItemGridControl.Control = this.gridControl;
            this.layoutControlItemGridControl.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemGridControl.Name = "layoutControlItemGridControl";
            this.layoutControlItemGridControl.Size = new System.Drawing.Size(1076, 168);
            this.layoutControlItemGridControl.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemGridControl.TextVisible = false;
            // 
            // layoutControlItemSchedulerControl
            // 
            this.layoutControlItemSchedulerControl.Control = this.schedulerControl;
            this.layoutControlItemSchedulerControl.Location = new System.Drawing.Point(0, 180);
            this.layoutControlItemSchedulerControl.Name = "layoutControlItemSchedulerControl";
            this.layoutControlItemSchedulerControl.Size = new System.Drawing.Size(1076, 486);
            this.layoutControlItemSchedulerControl.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemSchedulerControl.TextVisible = false;
            // 
            // splitterItemSchedulerControl
            // 
            this.splitterItemSchedulerControl.AllowHotTrack = true;
            this.splitterItemSchedulerControl.Location = new System.Drawing.Point(0, 168);
            this.splitterItemSchedulerControl.Name = "splitterItemSchedulerControl";
            this.splitterItemSchedulerControl.Size = new System.Drawing.Size(1076, 12);
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
            this.barBtnEdit,
            this.barBntConfirm,
            this.barBtnUpdate,
            this.barSubItemReport,
            this.barBtnCustomerVacationStatistics});
            this.barManager.MaxItemId = 7;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager;
            this.barDockControlTop.Size = new System.Drawing.Size(1096, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 686);
            this.barDockControlBottom.Manager = this.barManager;
            this.barDockControlBottom.Size = new System.Drawing.Size(1096, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 686);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1096, 0);
            this.barDockControlRight.Manager = this.barManager;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 686);
            // 
            // barBtnAdd
            // 
            this.barBtnAdd.Caption = "Добавить";
            this.barBtnAdd.Id = 0;
            this.barBtnAdd.ImageOptions.Image = global::RMS.UI.Properties.Resources.addfile_16x16;
            this.barBtnAdd.Name = "barBtnAdd";
            this.barBtnAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnAdd_ItemClick);
            // 
            // barBtnDel
            // 
            this.barBtnDel.Caption = "Удалить";
            this.barBtnDel.Id = 1;
            this.barBtnDel.ImageOptions.Image = global::RMS.UI.Properties.Resources.deletelist_16x16;
            this.barBtnDel.Name = "barBtnDel";
            this.barBtnDel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnDel_ItemClick);
            // 
            // barBtnEdit
            // 
            this.barBtnEdit.Caption = "Изменить";
            this.barBtnEdit.Id = 2;
            this.barBtnEdit.ImageOptions.Image = global::RMS.UI.Properties.Resources.edit_16x16;
            this.barBtnEdit.ImageOptions.LargeImage = global::RMS.UI.Properties.Resources.edit_32x32;
            this.barBtnEdit.Name = "barBtnEdit";
            this.barBtnEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnEdit_ItemClick);
            // 
            // barBntConfirm
            // 
            this.barBntConfirm.Caption = "Подтвердить";
            this.barBntConfirm.Id = 3;
            this.barBntConfirm.ImageOptions.Image = global::RMS.UI.Properties.Resources.apply_16x16;
            this.barBntConfirm.ImageOptions.LargeImage = global::RMS.UI.Properties.Resources.apply_32x32;
            this.barBntConfirm.Name = "barBntConfirm";
            this.barBntConfirm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBntConfirm_ItemClick);
            // 
            // barBtnUpdate
            // 
            this.barBtnUpdate.Caption = "Обновить";
            this.barBtnUpdate.Id = 4;
            this.barBtnUpdate.ImageOptions.Image = global::RMS.UI.Properties.Resources.recurrence_16x16;
            this.barBtnUpdate.ImageOptions.LargeImage = global::RMS.UI.Properties.Resources.recurrence_32x32;
            this.barBtnUpdate.Name = "barBtnUpdate";
            this.barBtnUpdate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnUpdate_ItemClick);
            // 
            // barSubItemReport
            // 
            this.barSubItemReport.Caption = "Отчеты";
            this.barSubItemReport.Id = 5;
            this.barSubItemReport.ImageOptions.Image = global::RMS.UI.Properties.Resources.print_16x16;
            this.barSubItemReport.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnCustomerVacationStatistics)});
            this.barSubItemReport.Name = "barSubItemReport";
            // 
            // barBtnCustomerVacationStatistics
            // 
            this.barBtnCustomerVacationStatistics.Caption = "Статистика отпусков";
            this.barBtnCustomerVacationStatistics.Id = 6;
            this.barBtnCustomerVacationStatistics.ImageOptions.Image = global::RMS.UI.Properties.Resources.simpleview_16x16;
            this.barBtnCustomerVacationStatistics.Name = "barBtnCustomerVacationStatistics";
            this.barBtnCustomerVacationStatistics.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnCustomerVacationStatistics_ItemClick);
            // 
            // popupMenu
            // 
            this.popupMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnAdd),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnEdit),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnDel),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnUpdate, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItemReport, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBntConfirm, true)});
            this.popupMenu.Manager = this.barManager;
            this.popupMenu.Name = "popupMenu";
            // 
            // VacationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1096, 686);
            this.Controls.Add(this.layoutControlVacation);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("VacationForm.IconOptions.Icon")));
            this.Name = "VacationForm";
            this.Text = "Отпуска";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlVacation)).EndInit();
            this.layoutControlVacation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.schedulerControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.schedulerDataStorage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSchedulerControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItemSchedulerControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraLayout.LayoutControl layoutControlVacation;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemGridControl;
        private DevExpress.XtraScheduler.SchedulerControl schedulerControl;
        private DevExpress.XtraScheduler.SchedulerDataStorage schedulerDataStorage;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemSchedulerControl;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barBtnAdd;
        private DevExpress.XtraBars.BarButtonItem barBtnDel;
        private DevExpress.XtraBars.BarButtonItem barBtnEdit;
        private DevExpress.XtraBars.BarButtonItem barBntConfirm;
        private DevExpress.XtraBars.PopupMenu popupMenu;
        private DevExpress.Utils.ToolTipController toolTipController;
        private DevExpress.XtraLayout.SplitterItem splitterItemSchedulerControl;
        private DevExpress.XtraBars.BarButtonItem barBtnUpdate;
        private DevExpress.XtraBars.BarSubItem barSubItemReport;
        private DevExpress.XtraBars.BarButtonItem barBtnCustomerVacationStatistics;
    }
}