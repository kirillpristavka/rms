namespace RMS.UI.Forms
{
    partial class ReportV2Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportV2Form));
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.popupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.barBtnImport = new DevExpress.XtraBars.BarButtonItem();
            this.batBtnForm = new DevExpress.XtraBars.BarButtonItem();
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.panelControlFilter = new DevExpress.XtraEditors.PanelControl();
            this.layoutControlDeal = new DevExpress.XtraLayout.LayoutControl();
            this.gridControlCustomer = new DevExpress.XtraGrid.GridControl();
            this.gridViewCustomer = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtYear = new DevExpress.XtraEditors.TextEdit();
            this.s = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemYear = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.barBtnAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnEdit = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnDel = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlFilter)).BeginInit();
            this.panelControlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlDeal)).BeginInit();
            this.layoutControlDeal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYear.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.s)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl
            // 
            this.gridControl.Location = new System.Drawing.Point(236, 12);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(756, 700);
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
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.Editable = false;
            this.gridView.OptionsSelection.MultiSelect = true;
            this.gridView.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gridView.OptionsView.ShowFooter = true;
            this.gridView.OptionsView.ShowGroupPanel = false;
            this.gridView.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView_RowStyle);
            this.gridView.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gridView_PopupMenuShowing);
            // 
            // popupMenu
            // 
            this.popupMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnAdd),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnEdit),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnDel),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnImport, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.batBtnForm, true)});
            this.popupMenu.Manager = this.barManager;
            this.popupMenu.Name = "popupMenu";
            // 
            // barBtnImport
            // 
            this.barBtnImport.Caption = "Импорт";
            this.barBtnImport.Id = 13;
            this.barBtnImport.ImageOptions.Image = global::RMS.UI.Properties.Resources.drilldownonarguments_pie_16x16;
            this.barBtnImport.ImageOptions.LargeImage = global::RMS.UI.Properties.Resources.drilldownonarguments_pie_32x32;
            this.barBtnImport.Name = "barBtnImport";
            this.barBtnImport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnImport_ItemClick);
            // 
            // batBtnForm
            // 
            this.batBtnForm.Caption = "Сформировать";
            this.batBtnForm.Id = 12;
            this.batBtnForm.ImageOptions.Image = global::RMS.UI.Properties.Resources.formatwraptext_16x16;
            this.batBtnForm.ImageOptions.LargeImage = global::RMS.UI.Properties.Resources.formatwraptext_32x32;
            this.batBtnForm.Name = "batBtnForm";
            this.batBtnForm.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.batBtnForm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnForm_ItemClick);
            // 
            // barManager
            // 
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.batBtnForm,
            this.barBtnImport,
            this.barBtnAdd,
            this.barBtnEdit,
            this.barBtnDel});
            this.barManager.MaxItemId = 17;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.barDockControlTop.Size = new System.Drawing.Size(1008, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 728);
            this.barDockControlBottom.Manager = this.barManager;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.barDockControlBottom.Size = new System.Drawing.Size(1008, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 728);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1008, 0);
            this.barDockControlRight.Manager = this.barManager;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 728);
            // 
            // panelControlFilter
            // 
            this.panelControlFilter.Controls.Add(this.layoutControlDeal);
            this.panelControlFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControlFilter.Location = new System.Drawing.Point(0, 0);
            this.panelControlFilter.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panelControlFilter.Name = "panelControlFilter";
            this.panelControlFilter.Size = new System.Drawing.Size(1008, 728);
            this.panelControlFilter.TabIndex = 41;
            // 
            // layoutControlDeal
            // 
            this.layoutControlDeal.Controls.Add(this.gridControlCustomer);
            this.layoutControlDeal.Controls.Add(this.gridControl);
            this.layoutControlDeal.Controls.Add(this.txtYear);
            this.layoutControlDeal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlDeal.Location = new System.Drawing.Point(2, 2);
            this.layoutControlDeal.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.layoutControlDeal.Name = "layoutControlDeal";
            this.layoutControlDeal.Root = this.s;
            this.layoutControlDeal.Size = new System.Drawing.Size(1004, 724);
            this.layoutControlDeal.TabIndex = 12;
            this.layoutControlDeal.Text = "Сделка";
            // 
            // gridControlCustomer
            // 
            this.gridControlCustomer.Location = new System.Drawing.Point(12, 38);
            this.gridControlCustomer.MainView = this.gridViewCustomer;
            this.gridControlCustomer.Name = "gridControlCustomer";
            this.gridControlCustomer.Size = new System.Drawing.Size(208, 674);
            this.gridControlCustomer.TabIndex = 37;
            this.gridControlCustomer.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewCustomer});
            // 
            // gridViewCustomer
            // 
            this.gridViewCustomer.Appearance.FooterPanel.Options.UseTextOptions = true;
            this.gridViewCustomer.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridViewCustomer.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridViewCustomer.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridViewCustomer.GridControl = this.gridControlCustomer;
            this.gridViewCustomer.Name = "gridViewCustomer";
            this.gridViewCustomer.OptionsBehavior.Editable = false;
            this.gridViewCustomer.OptionsSelection.MultiSelect = true;
            this.gridViewCustomer.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewCustomer.OptionsView.ShowFooter = true;
            this.gridViewCustomer.OptionsView.ShowGroupPanel = false;
            this.gridViewCustomer.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewCustomer_FocusedRowChanged);
            // 
            // txtYear
            // 
            this.txtYear.Location = new System.Drawing.Point(45, 12);
            this.txtYear.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtYear.MenuManager = this.barManager;
            this.txtYear.Name = "txtYear";
            this.txtYear.Properties.Appearance.Options.UseTextOptions = true;
            this.txtYear.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtYear.Size = new System.Drawing.Size(175, 22);
            this.txtYear.StyleController = this.layoutControlDeal;
            this.txtYear.TabIndex = 0;
            this.txtYear.TabStop = false;
            // 
            // s
            // 
            this.s.AppearanceItemCaption.Options.UseTextOptions = true;
            this.s.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.s.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.s.GroupBordersVisible = false;
            this.s.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItemYear,
            this.splitterItem1,
            this.layoutControlItem2});
            this.s.Name = "Root";
            this.s.Size = new System.Drawing.Size(1004, 724);
            this.s.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl;
            this.layoutControlItem1.Location = new System.Drawing.Point(224, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(760, 704);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItemYear
            // 
            this.layoutControlItemYear.Control = this.txtYear;
            this.layoutControlItemYear.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemYear.Name = "layoutControlItemYear";
            this.layoutControlItemYear.Size = new System.Drawing.Size(212, 26);
            this.layoutControlItemYear.Text = "Год:";
            this.layoutControlItemYear.TextSize = new System.Drawing.Size(29, 16);
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new System.Drawing.Point(212, 0);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(12, 704);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gridControlCustomer;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(212, 678);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // barBtnAdd
            // 
            this.barBtnAdd.Caption = "Добавить";
            this.barBtnAdd.Id = 14;
            this.barBtnAdd.ImageOptions.Image = global::RMS.UI.Properties.Resources.addfile_16x16;
            this.barBtnAdd.Name = "barBtnAdd";
            this.barBtnAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnAdd_ItemClick);
            // 
            // barBtnEdit
            // 
            this.barBtnEdit.Caption = "Изменить";
            this.barBtnEdit.Id = 15;
            this.barBtnEdit.ImageOptions.Image = global::RMS.UI.Properties.Resources.edit_16x16;
            this.barBtnEdit.Name = "barBtnEdit";
            this.barBtnEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnEdit_ItemClick);
            // 
            // barBtnDel
            // 
            this.barBtnDel.Caption = "Удалить";
            this.barBtnDel.Id = 16;
            this.barBtnDel.ImageOptions.Image = global::RMS.UI.Properties.Resources.deletelist_16x16;
            this.barBtnDel.Name = "barBtnDel";
            this.barBtnDel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnDel_ItemClick);
            // 
            // ReportV2Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 728);
            this.Controls.Add(this.panelControlFilter);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("ReportV2Form.IconOptions.Icon")));
            this.Name = "ReportV2Form";
            this.Text = "Отчеты";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DealForm_FormClosing);
            this.Load += new System.EventHandler(this.Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlFilter)).EndInit();
            this.panelControlFilter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlDeal)).EndInit();
            this.layoutControlDeal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYear.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.s)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraBars.PopupMenu popupMenu;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.PanelControl panelControlFilter;
        private DevExpress.XtraLayout.LayoutControl layoutControlDeal;
        private DevExpress.XtraLayout.LayoutControlGroup s;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.TextEdit txtYear;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemYear;
        private DevExpress.XtraBars.BarButtonItem batBtnForm;
        private DevExpress.XtraGrid.GridControl gridControlCustomer;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewCustomer;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraBars.BarButtonItem barBtnImport;
        private DevExpress.XtraBars.BarButtonItem barBtnAdd;
        private DevExpress.XtraBars.BarButtonItem barBtnEdit;
        private DevExpress.XtraBars.BarButtonItem barBtnDel;
    }
}