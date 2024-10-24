namespace RMS.UI.Forms
{
    partial class ContractForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContractForm));
            DevExpress.XtraBars.Navigation.AccordionContextButton accordionContextButton1 = new DevExpress.XtraBars.Navigation.AccordionContextButton();
            DevExpress.XtraBars.Navigation.AccordionContextButton accordionContextButton2 = new DevExpress.XtraBars.Navigation.AccordionContextButton();
            this.gridControlContract = new DevExpress.XtraGrid.GridControl();
            this.gridViewContract = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.popupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.barBtnPrint = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnTaskAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.accordTape = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.accordionCustomersFilterControl = new DevExpress.XtraBars.Navigation.AccordionContentContainer();
            this.panelControlFilter = new DevExpress.XtraEditors.PanelControl();
            this.accordionControlElementFilter = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnPrintGrid = new DevExpress.XtraEditors.SimpleButton();
            this.barBtnControlSystemAdd = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlContract)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewContract)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.accordTape)).BeginInit();
            this.accordTape.SuspendLayout();
            this.accordionCustomersFilterControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControlContract
            // 
            this.gridControlContract.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlContract.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gridControlContract.Location = new System.Drawing.Point(312, 41);
            this.gridControlContract.MainView = this.gridViewContract;
            this.gridControlContract.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gridControlContract.Name = "gridControlContract";
            this.gridControlContract.Size = new System.Drawing.Size(780, 456);
            this.gridControlContract.TabIndex = 34;
            this.gridControlContract.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewContract});
            // 
            // gridViewContract
            // 
            this.gridViewContract.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridViewContract.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridViewContract.DetailHeight = 394;
            this.gridViewContract.GridControl = this.gridControlContract;
            this.gridViewContract.Name = "gridViewContract";
            this.gridViewContract.OptionsBehavior.Editable = false;
            this.gridViewContract.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewContract.OptionsView.ShowGroupPanel = false;
            this.gridViewContract.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridViewContract_RowStyle);
            this.gridViewContract.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridViewContract_MouseDown);
            // 
            // popupMenu
            // 
            this.popupMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnPrint),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnTaskAdd, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnControlSystemAdd, true)});
            this.popupMenu.Manager = this.barManager;
            this.popupMenu.Name = "popupMenu";
            // 
            // barBtnPrint
            // 
            this.barBtnPrint.Caption = "Печать";
            this.barBtnPrint.Id = 0;
            this.barBtnPrint.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnPrint.ImageOptions.Image")));
            this.barBtnPrint.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnPrint.ImageOptions.LargeImage")));
            this.barBtnPrint.Name = "barBtnPrint";
            this.barBtnPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnPrint_ItemClick);
            // 
            // barBtnTaskAdd
            // 
            this.barBtnTaskAdd.Caption = "Добавить задачу";
            this.barBtnTaskAdd.Id = 1;
            this.barBtnTaskAdd.Name = "barBtnTaskAdd";
            this.barBtnTaskAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnTaskAdd_ItemClick);
            // 
            // barManager
            // 
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barBtnPrint,
            this.barBtnTaskAdd,
            this.barBtnControlSystemAdd});
            this.barManager.MaxItemId = 3;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.barDockControlTop.Size = new System.Drawing.Size(1092, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 497);
            this.barDockControlBottom.Manager = this.barManager;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.barDockControlBottom.Size = new System.Drawing.Size(1092, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 497);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1092, 0);
            this.barDockControlRight.Manager = this.barManager;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 497);
            // 
            // accordTape
            // 
            this.accordTape.Controls.Add(this.accordionCustomersFilterControl);
            this.accordTape.Dock = System.Windows.Forms.DockStyle.Left;
            this.accordTape.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElementFilter});
            this.accordTape.Location = new System.Drawing.Point(0, 0);
            this.accordTape.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.accordTape.Name = "accordTape";
            this.accordTape.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.Touch;
            this.accordTape.Size = new System.Drawing.Size(312, 497);
            this.accordTape.TabIndex = 39;
            this.accordTape.ViewType = DevExpress.XtraBars.Navigation.AccordionControlViewType.HamburgerMenu;
            this.accordTape.ContextButtonClick += new DevExpress.Utils.ContextItemClickEventHandler(this.accordTape_ContextButtonClick);
            // 
            // accordionCustomersFilterControl
            // 
            this.accordionCustomersFilterControl.Controls.Add(this.panelControlFilter);
            this.accordionCustomersFilterControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.accordionCustomersFilterControl.Name = "accordionCustomersFilterControl";
            this.accordionCustomersFilterControl.Size = new System.Drawing.Size(364, 112);
            this.accordionCustomersFilterControl.TabIndex = 7;
            // 
            // panelControlFilter
            // 
            this.panelControlFilter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControlFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControlFilter.Location = new System.Drawing.Point(0, 0);
            this.panelControlFilter.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panelControlFilter.Name = "panelControlFilter";
            this.panelControlFilter.Size = new System.Drawing.Size(364, 112);
            this.panelControlFilter.TabIndex = 1;
            // 
            // accordionControlElementFilter
            // 
            this.accordionControlElementFilter.ContentContainer = this.accordionCustomersFilterControl;
            accordionContextButton1.AlignmentOptions.Panel = DevExpress.Utils.ContextItemPanel.Center;
            accordionContextButton1.AlignmentOptions.Position = DevExpress.Utils.ContextItemPosition.Far;
            accordionContextButton1.AppearanceHover.Options.UseImage = true;
            accordionContextButton1.AppearanceNormal.Options.UseImage = true;
            accordionContextButton1.Id = new System.Guid("9b9af064-fcc5-4128-badf-dc12c3e673db");
            accordionContextButton1.ImageOptionsCollection.ItemHovered.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            accordionContextButton1.ImageOptionsCollection.ItemNormal.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image1")));
            accordionContextButton1.ImageOptionsCollection.ItemPressed.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image2")));
            accordionContextButton1.Name = "acBtnDirectoryFilterControl";
            accordionContextButton1.Padding = new System.Windows.Forms.Padding(3);
            accordionContextButton1.ToolTip = "Справочник фильтров";
            accordionContextButton2.AlignmentOptions.Panel = DevExpress.Utils.ContextItemPanel.Center;
            accordionContextButton2.AlignmentOptions.Position = DevExpress.Utils.ContextItemPosition.Far;
            accordionContextButton2.AppearanceHover.Options.UseImage = true;
            accordionContextButton2.AppearanceNormal.Options.UseImage = true;
            accordionContextButton2.Id = new System.Guid("0dec6d0b-f701-4d77-b988-3fd8cea6f5f9");
            accordionContextButton2.ImageOptionsCollection.ItemHovered.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image3")));
            accordionContextButton2.ImageOptionsCollection.ItemNormal.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image4")));
            accordionContextButton2.ImageOptionsCollection.ItemPressed.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image5")));
            accordionContextButton2.Name = "acBtnAddCustomersFilterControl";
            accordionContextButton2.Padding = new System.Windows.Forms.Padding(3);
            accordionContextButton2.ToolTip = "Добавление фильтра";
            this.accordionControlElementFilter.ContextButtons.Add(accordionContextButton1);
            this.accordionControlElementFilter.ContextButtons.Add(accordionContextButton2);
            this.accordionControlElementFilter.Expanded = true;
            this.accordionControlElementFilter.Name = "accordionControlElementFilter";
            this.accordionControlElementFilter.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElementFilter.Text = "Фильтры";
            // 
            // panelControl1
            // 
            this.panelControl1.AutoSize = true;
            this.panelControl1.Controls.Add(this.btnPrintGrid);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(312, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(780, 41);
            this.panelControl1.TabIndex = 44;
            // 
            // btnPrintGrid
            // 
            this.btnPrintGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrintGrid.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPrintGrid.ImageOptions.Image")));
            this.btnPrintGrid.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnPrintGrid.Location = new System.Drawing.Point(742, 6);
            this.btnPrintGrid.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnPrintGrid.Name = "btnPrintGrid";
            this.btnPrintGrid.Size = new System.Drawing.Size(31, 28);
            this.btnPrintGrid.TabIndex = 4;
            this.btnPrintGrid.Text = "+";
            this.btnPrintGrid.ToolTip = "Сохранение настроек отображения таблицы";
            this.btnPrintGrid.Click += new System.EventHandler(this.btnPrintGrid_Click);
            // 
            // barBtnControlSystemAdd
            // 
            this.barBtnControlSystemAdd.Caption = "Контроль";
            this.barBtnControlSystemAdd.Id = 2;
            this.barBtnControlSystemAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barBtnControlSystemAdd.ImageOptions.Image")));
            this.barBtnControlSystemAdd.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barBtnControlSystemAdd.ImageOptions.LargeImage")));
            this.barBtnControlSystemAdd.Name = "barBtnControlSystemAdd";
            this.barBtnControlSystemAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnControlSystemAdd_ItemClick);
            // 
            // ContractForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1092, 497);
            this.Controls.Add(this.gridControlContract);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.accordTape);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "ContractForm";
            this.Text = "Договора";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.TaskForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlContract)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewContract)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.accordTape)).EndInit();
            this.accordTape.ResumeLayout(false);
            this.accordionCustomersFilterControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlContract;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewContract;
        private DevExpress.XtraBars.PopupMenu popupMenu;
        private DevExpress.XtraBars.BarButtonItem barBtnPrint;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.Navigation.AccordionControl accordTape;
        private DevExpress.XtraBars.Navigation.AccordionContentContainer accordionCustomersFilterControl;
        private DevExpress.XtraEditors.PanelControl panelControlFilter;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElementFilter;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnPrintGrid;
        private DevExpress.XtraBars.BarButtonItem barBtnTaskAdd;
        private DevExpress.XtraBars.BarButtonItem barBtnControlSystemAdd;
    }
}