namespace RMS.UI.Forms
{
    partial class SalaryStaffForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SalaryStaffForm));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gridControlСalculation = new DevExpress.XtraGrid.GridControl();
            this.gridViewCalculation = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridControlBasis = new DevExpress.XtraGrid.GridControl();
            this.gridViewBasis = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cmbYear = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbMonth = new DevExpress.XtraEditors.ComboBoxEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            this.splitterItem2 = new DevExpress.XtraLayout.SplitterItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.popupMenuBasis = new DevExpress.XtraBars.PopupMenu(this.components);
            this.barBtnAddBasis = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnEditBasis = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnDelBasis = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnCalculation = new DevExpress.XtraBars.BarButtonItem();
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barBtnEditCalculation = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnDelCalculation = new DevExpress.XtraBars.BarButtonItem();
            this.popupMenuCalculation = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlСalculation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCalculation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlBasis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBasis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbYear.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuBasis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuCalculation)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridControlСalculation);
            this.layoutControl1.Controls.Add(this.gridControlBasis);
            this.layoutControl1.Controls.Add(this.gridControl);
            this.layoutControl1.Controls.Add(this.cmbYear);
            this.layoutControl1.Controls.Add(this.cmbMonth);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(874, 442);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gridControlСalculation
            // 
            this.gridControlСalculation.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.gridControlСalculation.Location = new System.Drawing.Point(442, 225);
            this.gridControlСalculation.MainView = this.gridViewCalculation;
            this.gridControlСalculation.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.gridControlСalculation.Name = "gridControlСalculation";
            this.gridControlСalculation.Size = new System.Drawing.Size(420, 205);
            this.gridControlСalculation.TabIndex = 26;
            this.gridControlСalculation.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewCalculation});
            // 
            // gridViewCalculation
            // 
            this.gridViewCalculation.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridViewCalculation.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridViewCalculation.DetailHeight = 311;
            this.gridViewCalculation.GridControl = this.gridControlСalculation;
            this.gridViewCalculation.Name = "gridViewCalculation";
            this.gridViewCalculation.OptionsBehavior.Editable = false;
            this.gridViewCalculation.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewCalculation.OptionsView.ShowFooter = true;
            this.gridViewCalculation.OptionsView.ShowGroupPanel = false;
            this.gridViewCalculation.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gridViewCalculation_PopupMenuShowing);
            // 
            // gridControlBasis
            // 
            this.gridControlBasis.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.gridControlBasis.Location = new System.Drawing.Point(12, 225);
            this.gridControlBasis.MainView = this.gridViewBasis;
            this.gridControlBasis.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.gridControlBasis.Name = "gridControlBasis";
            this.gridControlBasis.Size = new System.Drawing.Size(414, 205);
            this.gridControlBasis.TabIndex = 25;
            this.gridControlBasis.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewBasis});
            // 
            // gridViewBasis
            // 
            this.gridViewBasis.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridViewBasis.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridViewBasis.DetailHeight = 311;
            this.gridViewBasis.GridControl = this.gridControlBasis;
            this.gridViewBasis.Name = "gridViewBasis";
            this.gridViewBasis.OptionsBehavior.Editable = false;
            this.gridViewBasis.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewBasis.OptionsView.ShowGroupPanel = false;
            this.gridViewBasis.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gridViewBasis_PopupMenuShowing);
            // 
            // gridControl
            // 
            this.gridControl.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.gridControl.Location = new System.Drawing.Point(12, 38);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(850, 171);
            this.gridControl.TabIndex = 24;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView.DetailHeight = 311;
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.Editable = false;
            this.gridView.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gridView.OptionsView.ShowGroupPanel = false;
            this.gridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView_FocusedRowChanged);
            // 
            // cmbYear
            // 
            this.cmbYear.EditValue = "2022";
            this.cmbYear.Location = new System.Drawing.Point(300, 12);
            this.cmbYear.MaximumSize = new System.Drawing.Size(100, 0);
            this.cmbYear.Name = "cmbYear";
            this.cmbYear.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbYear.Properties.Items.AddRange(new object[] {
            "2021",
            "2022",
            "2023"});
            this.cmbYear.Size = new System.Drawing.Size(100, 22);
            this.cmbYear.StyleController = this.layoutControl1;
            this.cmbYear.TabIndex = 27;
            this.cmbYear.SelectedValueChanged += new System.EventHandler(this.EditPeriod_SelectedValueChanged);
            // 
            // cmbMonth
            // 
            this.cmbMonth.EditValue = "";
            this.cmbMonth.Location = new System.Drawing.Point(146, 12);
            this.cmbMonth.MaximumSize = new System.Drawing.Size(150, 0);
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbMonth.Properties.DropDownRows = 12;
            this.cmbMonth.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbMonth.Size = new System.Drawing.Size(150, 22);
            this.cmbMonth.StyleController = this.layoutControl1;
            this.cmbMonth.TabIndex = 28;
            this.cmbMonth.SelectedValueChanged += new System.EventHandler(this.EditPeriod_SelectedValueChanged);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.splitterItem1,
            this.splitterItem2,
            this.layoutControlItem5,
            this.layoutControlItem4});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(874, 442);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(854, 175);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gridControlBasis;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 213);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(418, 209);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gridControlСalculation;
            this.layoutControlItem3.Location = new System.Drawing.Point(430, 213);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(424, 209);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new System.Drawing.Point(0, 201);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(854, 12);
            // 
            // splitterItem2
            // 
            this.splitterItem2.AllowHotTrack = true;
            this.splitterItem2.Location = new System.Drawing.Point(418, 213);
            this.splitterItem2.Name = "splitterItem2";
            this.splitterItem2.Size = new System.Drawing.Size(12, 209);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.cmbMonth;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(288, 26);
            this.layoutControlItem5.Text = "Расчетный период:";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(131, 16);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.cmbYear;
            this.layoutControlItem4.Location = new System.Drawing.Point(288, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(566, 26);
            this.layoutControlItem4.Text = "Год:";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // popupMenuBasis
            // 
            this.popupMenuBasis.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnAddBasis),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnEditBasis),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnDelBasis),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnCalculation, true)});
            this.popupMenuBasis.Manager = this.barManager;
            this.popupMenuBasis.Name = "popupMenuBasis";
            // 
            // barBtnAddBasis
            // 
            this.barBtnAddBasis.Caption = "Добавить";
            this.barBtnAddBasis.Id = 0;
            this.barBtnAddBasis.ImageOptions.Image = global::RMS.UI.Properties.Resources.addfile_16x16;
            this.barBtnAddBasis.Name = "barBtnAddBasis";
            this.barBtnAddBasis.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnAddBasis_ItemClick);
            // 
            // barBtnEditBasis
            // 
            this.barBtnEditBasis.Caption = "Изменить";
            this.barBtnEditBasis.Id = 1;
            this.barBtnEditBasis.ImageOptions.Image = global::RMS.UI.Properties.Resources.edit_16x16;
            this.barBtnEditBasis.Name = "barBtnEditBasis";
            this.barBtnEditBasis.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnEditBasis_ItemClick);
            // 
            // barBtnDelBasis
            // 
            this.barBtnDelBasis.Caption = "Удалить";
            this.barBtnDelBasis.Id = 2;
            this.barBtnDelBasis.ImageOptions.Image = global::RMS.UI.Properties.Resources.deletelist_16x16;
            this.barBtnDelBasis.Name = "barBtnDelBasis";
            this.barBtnDelBasis.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnDelBasis_ItemClick);
            // 
            // barBtnCalculation
            // 
            this.barBtnCalculation.Caption = "Расчитать";
            this.barBtnCalculation.Id = 3;
            this.barBtnCalculation.ImageOptions.Image = global::RMS.UI.Properties.Resources.calculatenow_16x16;
            this.barBtnCalculation.ImageOptions.LargeImage = global::RMS.UI.Properties.Resources.calculatenow_32x32;
            this.barBtnCalculation.Name = "barBtnCalculation";
            this.barBtnCalculation.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnCalculation_ItemClick);
            // 
            // barManager
            // 
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barBtnAddBasis,
            this.barBtnEditBasis,
            this.barBtnDelBasis,
            this.barBtnCalculation,
            this.barBtnEditCalculation,
            this.barBtnDelCalculation});
            this.barManager.MaxItemId = 6;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager;
            this.barDockControlTop.Size = new System.Drawing.Size(874, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 442);
            this.barDockControlBottom.Manager = this.barManager;
            this.barDockControlBottom.Size = new System.Drawing.Size(874, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 442);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(874, 0);
            this.barDockControlRight.Manager = this.barManager;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 442);
            // 
            // barBtnEditCalculation
            // 
            this.barBtnEditCalculation.Caption = "Изменить";
            this.barBtnEditCalculation.Id = 4;
            this.barBtnEditCalculation.ImageOptions.Image = global::RMS.UI.Properties.Resources.edit_16x16;
            this.barBtnEditCalculation.Name = "barBtnEditCalculation";
            this.barBtnEditCalculation.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnEditCalculation_ItemClick);
            // 
            // barBtnDelCalculation
            // 
            this.barBtnDelCalculation.Caption = "Удалить";
            this.barBtnDelCalculation.Id = 5;
            this.barBtnDelCalculation.ImageOptions.Image = global::RMS.UI.Properties.Resources.deletelist_16x16;
            this.barBtnDelCalculation.Name = "barBtnDelCalculation";
            this.barBtnDelCalculation.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnDelCalculation_ItemClick);
            // 
            // popupMenuCalculation
            // 
            this.popupMenuCalculation.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnEditCalculation),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnDelCalculation)});
            this.popupMenuCalculation.Manager = this.barManager;
            this.popupMenuCalculation.Name = "popupMenuCalculation";
            // 
            // SalaryStaffForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 442);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SalaryStaffForm";
            this.Text = "Зароботная плата";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.TaskForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlСalculation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCalculation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlBasis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBasis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbYear.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuBasis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuCalculation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gridControlСalculation;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewCalculation;
        private DevExpress.XtraGrid.GridControl gridControlBasis;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewBasis;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.XtraLayout.SplitterItem splitterItem2;
        private DevExpress.XtraEditors.ComboBoxEdit cmbYear;
        private DevExpress.XtraEditors.ComboBoxEdit cmbMonth;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraBars.PopupMenu popupMenuBasis;
        private DevExpress.XtraBars.BarButtonItem barBtnAddBasis;
        private DevExpress.XtraBars.BarButtonItem barBtnEditBasis;
        private DevExpress.XtraBars.BarButtonItem barBtnDelBasis;
        private DevExpress.XtraBars.BarButtonItem barBtnCalculation;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barBtnEditCalculation;
        private DevExpress.XtraBars.BarButtonItem barBtnDelCalculation;
        private DevExpress.XtraBars.PopupMenu popupMenuCalculation;
    }
}