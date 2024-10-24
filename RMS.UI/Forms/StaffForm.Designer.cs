namespace RMS.UI.Forms
{
    partial class StaffForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StaffForm));
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.popupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.barBtnAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnEdit = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnDel = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnTaskAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnControlSystemAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barBtnAdditionalServicesAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnAdditionalServicesEdit = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnAdditionalServicesDel = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnAdditionalServicesUpdate = new DevExpress.XtraBars.BarButtonItem();
            this.layoutControlStaff = new DevExpress.XtraLayout.LayoutControl();
            this.gridControlAdditionalServices = new DevExpress.XtraGrid.GridControl();
            this.gridViewAdditionalServices = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            this.tabbedControlGroupStaff = new DevExpress.XtraLayout.TabbedControlGroup();
            this.layoutControlGroupAdditionalServices = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.popupMenuAdditionalServices = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlStaff)).BeginInit();
            this.layoutControlStaff.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlAdditionalServices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewAdditionalServices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedControlGroupStaff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupAdditionalServices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuAdditionalServices)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl
            // 
            this.gridControl.Location = new System.Drawing.Point(7, 7);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(896, 199);
            this.gridControl.TabIndex = 36;
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
            this.gridView.OptionsView.ShowGroupPanel = false;
            this.gridView.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView_RowStyle);
            this.gridView.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gridView_PopupMenuShowing);
            this.gridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView_FocusedRowChanged);
            // 
            // popupMenu
            // 
            this.popupMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnAdd, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnEdit),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnDel),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnRefresh, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnTaskAdd, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnControlSystemAdd, true)});
            this.popupMenu.Manager = this.barManager;
            this.popupMenu.Name = "popupMenu";
            // 
            // barBtnAdd
            // 
            this.barBtnAdd.Caption = "Добавить";
            this.barBtnAdd.Id = 4;
            this.barBtnAdd.ImageOptions.Image = global::RMS.UI.Properties.Resources.addfile_16x16;
            this.barBtnAdd.Name = "barBtnAdd";
            this.barBtnAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnAdd_ItemClick);
            // 
            // barBtnEdit
            // 
            this.barBtnEdit.Caption = "Изменить";
            this.barBtnEdit.Id = 5;
            this.barBtnEdit.ImageOptions.Image = global::RMS.UI.Properties.Resources.edit_16x16;
            this.barBtnEdit.Name = "barBtnEdit";
            this.barBtnEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnEdit_ItemClick);
            // 
            // barBtnDel
            // 
            this.barBtnDel.Caption = "Удалить";
            this.barBtnDel.Id = 6;
            this.barBtnDel.ImageOptions.Image = global::RMS.UI.Properties.Resources.deletelist_16x16;
            this.barBtnDel.Name = "barBtnDel";
            this.barBtnDel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnDel_ItemClick);
            // 
            // barBtnRefresh
            // 
            this.barBtnRefresh.Caption = "Обновить";
            this.barBtnRefresh.Id = 2;
            this.barBtnRefresh.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnRefresh.ImageOptions.Image")));
            this.barBtnRefresh.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnRefresh.ImageOptions.LargeImage")));
            this.barBtnRefresh.Name = "barBtnRefresh";
            this.barBtnRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnRefresh_ItemClick);
            // 
            // barBtnTaskAdd
            // 
            this.barBtnTaskAdd.Caption = "Добавить задачу";
            this.barBtnTaskAdd.Id = 1;
            this.barBtnTaskAdd.Name = "barBtnTaskAdd";
            this.barBtnTaskAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnTaskAdd_ItemClick);
            // 
            // barBtnControlSystemAdd
            // 
            this.barBtnControlSystemAdd.Caption = "Контроль";
            this.barBtnControlSystemAdd.Id = 3;
            this.barBtnControlSystemAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnControlSystemAdd.ImageOptions.Image")));
            this.barBtnControlSystemAdd.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnControlSystemAdd.ImageOptions.LargeImage")));
            this.barBtnControlSystemAdd.Name = "barBtnControlSystemAdd";
            this.barBtnControlSystemAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnControlSystemAdd_ItemClick);
            // 
            // barManager
            // 
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barBtnTaskAdd,
            this.barBtnRefresh,
            this.barBtnControlSystemAdd,
            this.barBtnAdd,
            this.barBtnEdit,
            this.barBtnDel,
            this.barBtnAdditionalServicesAdd,
            this.barBtnAdditionalServicesEdit,
            this.barBtnAdditionalServicesDel,
            this.barBtnAdditionalServicesUpdate});
            this.barManager.MaxItemId = 11;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.barDockControlTop.Size = new System.Drawing.Size(910, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 423);
            this.barDockControlBottom.Manager = this.barManager;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.barDockControlBottom.Size = new System.Drawing.Size(910, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 423);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(910, 0);
            this.barDockControlRight.Manager = this.barManager;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 423);
            // 
            // barBtnAdditionalServicesAdd
            // 
            this.barBtnAdditionalServicesAdd.Caption = "Добавить";
            this.barBtnAdditionalServicesAdd.Id = 7;
            this.barBtnAdditionalServicesAdd.ImageOptions.Image = global::RMS.UI.Properties.Resources.addfile_16x16;
            this.barBtnAdditionalServicesAdd.Name = "barBtnAdditionalServicesAdd";
            this.barBtnAdditionalServicesAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnAdditionalServicesAdd_ItemClick);
            // 
            // barBtnAdditionalServicesEdit
            // 
            this.barBtnAdditionalServicesEdit.Caption = "Изменить";
            this.barBtnAdditionalServicesEdit.Id = 8;
            this.barBtnAdditionalServicesEdit.ImageOptions.Image = global::RMS.UI.Properties.Resources.edit_16x16;
            this.barBtnAdditionalServicesEdit.Name = "barBtnAdditionalServicesEdit";
            this.barBtnAdditionalServicesEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnAdditionalServicesEdit_ItemClick);
            // 
            // barBtnAdditionalServicesDel
            // 
            this.barBtnAdditionalServicesDel.Caption = "Удалить";
            this.barBtnAdditionalServicesDel.Id = 9;
            this.barBtnAdditionalServicesDel.ImageOptions.Image = global::RMS.UI.Properties.Resources.deletelist_16x16;
            this.barBtnAdditionalServicesDel.Name = "barBtnAdditionalServicesDel";
            this.barBtnAdditionalServicesDel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnAdditionalServicesDel_ItemClick);
            // 
            // barBtnAdditionalServicesUpdate
            // 
            this.barBtnAdditionalServicesUpdate.Caption = "Обновить";
            this.barBtnAdditionalServicesUpdate.Id = 10;
            this.barBtnAdditionalServicesUpdate.ImageOptions.Image = global::RMS.UI.Properties.Resources.recurrence_16x16;
            this.barBtnAdditionalServicesUpdate.Name = "barBtnAdditionalServicesUpdate";
            this.barBtnAdditionalServicesUpdate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnAdditionalServicesUpdate_ItemClick);
            // 
            // layoutControlStaff
            // 
            this.layoutControlStaff.AllowCustomization = false;
            this.layoutControlStaff.Controls.Add(this.gridControl);
            this.layoutControlStaff.Controls.Add(this.gridControlAdditionalServices);
            this.layoutControlStaff.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlStaff.Location = new System.Drawing.Point(0, 0);
            this.layoutControlStaff.Name = "layoutControlStaff";
            this.layoutControlStaff.Root = this.Root;
            this.layoutControlStaff.Size = new System.Drawing.Size(910, 423);
            this.layoutControlStaff.TabIndex = 42;
            this.layoutControlStaff.Text = "layoutControl1";
            // 
            // gridControlAdditionalServices
            // 
            this.gridControlAdditionalServices.Location = new System.Drawing.Point(15, 256);
            this.gridControlAdditionalServices.MainView = this.gridViewAdditionalServices;
            this.gridControlAdditionalServices.Name = "gridControlAdditionalServices";
            this.gridControlAdditionalServices.Size = new System.Drawing.Size(880, 152);
            this.gridControlAdditionalServices.TabIndex = 36;
            this.gridControlAdditionalServices.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewAdditionalServices});
            // 
            // gridViewAdditionalServices
            // 
            this.gridViewAdditionalServices.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridViewAdditionalServices.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridViewAdditionalServices.GridControl = this.gridControlAdditionalServices;
            this.gridViewAdditionalServices.Name = "gridViewAdditionalServices";
            this.gridViewAdditionalServices.OptionsBehavior.Editable = false;
            this.gridViewAdditionalServices.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewAdditionalServices.OptionsView.RowAutoHeight = true;
            this.gridViewAdditionalServices.OptionsView.ShowGroupPanel = false;
            this.gridViewAdditionalServices.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridViewAdditionalServices_RowStyle);
            this.gridViewAdditionalServices.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gridViewAdditionalServices_PopupMenuShowing);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.splitterItem1,
            this.tabbedControlGroupStaff});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.Root.Size = new System.Drawing.Size(910, 423);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(900, 203);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new System.Drawing.Point(0, 203);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(900, 12);
            // 
            // tabbedControlGroupStaff
            // 
            this.tabbedControlGroupStaff.AppearanceTabPage.HeaderActive.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.tabbedControlGroupStaff.AppearanceTabPage.HeaderActive.Options.UseFont = true;
            this.tabbedControlGroupStaff.Location = new System.Drawing.Point(0, 215);
            this.tabbedControlGroupStaff.Name = "tabbedControlGroupStaff";
            this.tabbedControlGroupStaff.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.tabbedControlGroupStaff.SelectedTabPage = this.layoutControlGroupAdditionalServices;
            this.tabbedControlGroupStaff.Size = new System.Drawing.Size(900, 198);
            this.tabbedControlGroupStaff.TabPages.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroupAdditionalServices});
            // 
            // layoutControlGroupAdditionalServices
            // 
            this.layoutControlGroupAdditionalServices.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroupAdditionalServices.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupAdditionalServices.Name = "layoutControlGroupAdditionalServices";
            this.layoutControlGroupAdditionalServices.Size = new System.Drawing.Size(884, 156);
            this.layoutControlGroupAdditionalServices.Text = "Дополнительные услуги";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gridControlAdditionalServices;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(884, 156);
            this.layoutControlItem2.Text = "layoutControlItem1";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // popupMenuAdditionalServices
            // 
            this.popupMenuAdditionalServices.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnAdditionalServicesAdd),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnAdditionalServicesEdit),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnAdditionalServicesDel),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnAdditionalServicesUpdate, true)});
            this.popupMenuAdditionalServices.Manager = this.barManager;
            this.popupMenuAdditionalServices.Name = "popupMenuAdditionalServices";
            // 
            // StaffForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 423);
            this.Controls.Add(this.layoutControlStaff);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StaffForm";
            this.Text = "Сотрудники";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.StaffForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlStaff)).EndInit();
            this.layoutControlStaff.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlAdditionalServices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewAdditionalServices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedControlGroupStaff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupAdditionalServices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuAdditionalServices)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraBars.BarButtonItem barBtnRefresh;
        private DevExpress.XtraBars.BarButtonItem barBtnTaskAdd;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barBtnControlSystemAdd;
        private DevExpress.XtraBars.BarButtonItem barBtnAdd;
        private DevExpress.XtraBars.BarButtonItem barBtnEdit;
        private DevExpress.XtraBars.BarButtonItem barBtnDel;
        private DevExpress.XtraLayout.LayoutControl layoutControlStaff;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.XtraGrid.GridControl gridControlAdditionalServices;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewAdditionalServices;
        private DevExpress.XtraBars.PopupMenu popupMenu;
        private DevExpress.XtraLayout.TabbedControlGroup tabbedControlGroupStaff;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupAdditionalServices;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraBars.BarButtonItem barBtnAdditionalServicesAdd;
        private DevExpress.XtraBars.BarButtonItem barBtnAdditionalServicesEdit;
        private DevExpress.XtraBars.BarButtonItem barBtnAdditionalServicesDel;
        private DevExpress.XtraBars.BarButtonItem barBtnAdditionalServicesUpdate;
        private DevExpress.XtraBars.PopupMenu popupMenuAdditionalServices;
    }
}