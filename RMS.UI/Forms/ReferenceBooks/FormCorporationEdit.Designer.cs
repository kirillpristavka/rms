namespace RMS.UI.Forms.ReferenceBooks
{
    partial class FormCorporationEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCorporationEdit));
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.checkIsUseFormingPreTax = new DevExpress.XtraEditors.CheckEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.txtKod = new DevExpress.XtraEditors.TextEdit();
            this.txtFullName = new DevExpress.XtraEditors.MemoEdit();
            this.txtAbbreviatedName = new DevExpress.XtraEditors.MemoEdit();
            this.checkIsUseFormingIndividualEntrepreneursTax = new DevExpress.XtraEditors.CheckEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.tabbedControlGroup = new DevExpress.XtraLayout.TabbedControlGroup();
            this.layoutControlGroupAllowedReports = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroupInfo = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemIsUseFormingPreTax = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemIsUseFormingIndividualEntrepreneursTax = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.popupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.barBtnAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnDel = new DevExpress.XtraBars.BarButtonItem();
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsUseFormingPreTax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKod.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFullName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAbbreviatedName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsUseFormingIndividualEntrepreneursTax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupAllowedReports)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemIsUseFormingPreTax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemIsUseFormingIndividualEntrepreneursTax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(371, 318);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(242, 25);
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.TabIndex = 26;
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridControl);
            this.layoutControl1.Controls.Add(this.checkIsUseFormingPreTax);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.txtKod);
            this.layoutControl1.Controls.Add(this.txtFullName);
            this.layoutControl1.Controls.Add(this.txtAbbreviatedName);
            this.layoutControl1.Controls.Add(this.checkIsUseFormingIndividualEntrepreneursTax);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(855, 349);
            this.layoutControl1.TabIndex = 33;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gridControl
            // 
            this.gridControl.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gridControl.Location = new System.Drawing.Point(15, 43);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(825, 264);
            this.gridControl.TabIndex = 38;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.DetailHeight = 394;
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.Editable = false;
            this.gridView.OptionsView.ShowGroupPanel = false;
            this.gridView.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gridViewTaxSystemReports_PopupMenuShowing);
            // 
            // checkIsUseFormingPreTax
            // 
            this.checkIsUseFormingPreTax.Location = new System.Drawing.Point(276, 71);
            this.checkIsUseFormingPreTax.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkIsUseFormingPreTax.Name = "checkIsUseFormingPreTax";
            this.checkIsUseFormingPreTax.Properties.Caption = "Использование для формировании предварительных налогов";
            this.checkIsUseFormingPreTax.Size = new System.Drawing.Size(564, 22);
            this.checkIsUseFormingPreTax.StyleController = this.layoutControl1;
            this.checkIsUseFormingPreTax.TabIndex = 33;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(617, 318);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(231, 25);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 25;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtKod
            // 
            this.txtKod.Location = new System.Drawing.Point(266, 43);
            this.txtKod.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtKod.Name = "txtKod";
            this.txtKod.Size = new System.Drawing.Size(574, 24);
            this.txtKod.StyleController = this.layoutControl1;
            this.txtKod.TabIndex = 27;
            // 
            // txtFullName
            // 
            this.txtFullName.Location = new System.Drawing.Point(266, 198);
            this.txtFullName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(574, 109);
            this.txtFullName.StyleController = this.layoutControl1;
            this.txtFullName.TabIndex = 31;
            // 
            // txtAbbreviatedName
            // 
            this.txtAbbreviatedName.Location = new System.Drawing.Point(266, 125);
            this.txtAbbreviatedName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtAbbreviatedName.Name = "txtAbbreviatedName";
            this.txtAbbreviatedName.Size = new System.Drawing.Size(574, 69);
            this.txtAbbreviatedName.StyleController = this.layoutControl1;
            this.txtAbbreviatedName.TabIndex = 29;
            // 
            // checkIsUseFormingIndividualEntrepreneursTax
            // 
            this.checkIsUseFormingIndividualEntrepreneursTax.Location = new System.Drawing.Point(276, 98);
            this.checkIsUseFormingIndividualEntrepreneursTax.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkIsUseFormingIndividualEntrepreneursTax.Name = "checkIsUseFormingIndividualEntrepreneursTax";
            this.checkIsUseFormingIndividualEntrepreneursTax.Properties.Caption = "Использовать при формировании ИП страховые/патенты/УСН";
            this.checkIsUseFormingIndividualEntrepreneursTax.Size = new System.Drawing.Size(564, 22);
            this.checkIsUseFormingIndividualEntrepreneursTax.StyleController = this.layoutControl1;
            this.checkIsUseFormingIndividualEntrepreneursTax.TabIndex = 32;
            // 
            // Root
            // 
            this.Root.AppearanceItemCaption.Options.UseTextOptions = true;
            this.Root.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.tabbedControlGroup});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 4, 4);
            this.Root.Size = new System.Drawing.Size(855, 349);
            this.Root.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 312);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(364, 29);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnSave;
            this.layoutControlItem4.Location = new System.Drawing.Point(364, 312);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(246, 29);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(246, 29);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(246, 29);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnCancel;
            this.layoutControlItem5.Location = new System.Drawing.Point(610, 312);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(235, 29);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(235, 29);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(235, 29);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // tabbedControlGroup
            // 
            this.tabbedControlGroup.AppearanceTabPage.HeaderActive.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.tabbedControlGroup.AppearanceTabPage.HeaderActive.Options.UseFont = true;
            this.tabbedControlGroup.Location = new System.Drawing.Point(0, 0);
            this.tabbedControlGroup.Name = "tabbedControlGroup";
            this.tabbedControlGroup.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 4, 4);
            this.tabbedControlGroup.SelectedTabPage = this.layoutControlGroupInfo;
            this.tabbedControlGroup.Size = new System.Drawing.Size(845, 312);
            this.tabbedControlGroup.TabPages.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroupInfo,
            this.layoutControlGroupAllowedReports});
            // 
            // layoutControlGroupAllowedReports
            // 
            this.layoutControlGroupAllowedReports.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem6});
            this.layoutControlGroupAllowedReports.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupAllowedReports.Name = "layoutControlGroupAllowedReports";
            this.layoutControlGroupAllowedReports.Size = new System.Drawing.Size(829, 268);
            this.layoutControlGroupAllowedReports.Text = "Разрешенные отчеты";
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.gridControl;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(829, 268);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlGroupInfo
            // 
            this.layoutControlGroupInfo.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItemIsUseFormingPreTax,
            this.layoutControlItemIsUseFormingIndividualEntrepreneursTax,
            this.emptySpaceItem3,
            this.emptySpaceItem2,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.layoutControlGroupInfo.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupInfo.Name = "layoutControlGroupInfo";
            this.layoutControlGroupInfo.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 4, 4);
            this.layoutControlGroupInfo.Size = new System.Drawing.Size(829, 268);
            this.layoutControlGroupInfo.Text = "Общая информация";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtKod;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(829, 28);
            this.layoutControlItem1.Text = "Код:";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(248, 18);
            // 
            // layoutControlItemIsUseFormingPreTax
            // 
            this.layoutControlItemIsUseFormingPreTax.Control = this.checkIsUseFormingPreTax;
            this.layoutControlItemIsUseFormingPreTax.Location = new System.Drawing.Point(261, 28);
            this.layoutControlItemIsUseFormingPreTax.Name = "layoutControlItemIsUseFormingPreTax";
            this.layoutControlItemIsUseFormingPreTax.Size = new System.Drawing.Size(568, 27);
            this.layoutControlItemIsUseFormingPreTax.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemIsUseFormingPreTax.TextVisible = false;
            // 
            // layoutControlItemIsUseFormingIndividualEntrepreneursTax
            // 
            this.layoutControlItemIsUseFormingIndividualEntrepreneursTax.Control = this.checkIsUseFormingIndividualEntrepreneursTax;
            this.layoutControlItemIsUseFormingIndividualEntrepreneursTax.Location = new System.Drawing.Point(261, 55);
            this.layoutControlItemIsUseFormingIndividualEntrepreneursTax.Name = "layoutControlItemIsUseFormingIndividualEntrepreneursTax";
            this.layoutControlItemIsUseFormingIndividualEntrepreneursTax.Size = new System.Drawing.Size(568, 27);
            this.layoutControlItemIsUseFormingIndividualEntrepreneursTax.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemIsUseFormingIndividualEntrepreneursTax.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 28);
            this.emptySpaceItem3.MaxSize = new System.Drawing.Size(261, 27);
            this.emptySpaceItem3.MinSize = new System.Drawing.Size(261, 27);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(261, 27);
            this.emptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem3.Text = "emptySpaceItem2";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 55);
            this.emptySpaceItem2.MaxSize = new System.Drawing.Size(261, 27);
            this.emptySpaceItem2.MinSize = new System.Drawing.Size(261, 27);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(261, 27);
            this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtAbbreviatedName;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 82);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(829, 73);
            this.layoutControlItem2.Text = "Сокращенное наименование:";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(248, 18);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtFullName;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 155);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(829, 113);
            this.layoutControlItem3.Text = "Полное наименование:";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(248, 18);
            // 
            // popupMenu
            // 
            this.popupMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnAdd),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnDel)});
            this.popupMenu.Manager = this.barManager;
            this.popupMenu.Name = "popupMenu";
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
            // barManager
            // 
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barBtnAdd,
            this.barBtnDel});
            this.barManager.MaxItemId = 2;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager;
            this.barDockControlTop.Size = new System.Drawing.Size(855, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 349);
            this.barDockControlBottom.Manager = this.barManager;
            this.barDockControlBottom.Size = new System.Drawing.Size(855, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 349);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(855, 0);
            this.barDockControlRight.Manager = this.barManager;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 349);
            // 
            // FormCorporationEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 349);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(700, 350);
            this.Name = "FormCorporationEdit";
            this.Text = "Организационно-правовая форма";
            this.Load += new System.EventHandler(this.PositionEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsUseFormingPreTax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKod.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFullName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAbbreviatedName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsUseFormingIndividualEntrepreneursTax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupAllowedReports)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemIsUseFormingPreTax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemIsUseFormingIndividualEntrepreneursTax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.TextEdit txtKod;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.MemoEdit txtFullName;
        private DevExpress.XtraEditors.MemoEdit txtAbbreviatedName;
        private DevExpress.XtraEditors.CheckEdit checkIsUseFormingIndividualEntrepreneursTax;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemIsUseFormingIndividualEntrepreneursTax;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.CheckEdit checkIsUseFormingPreTax;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemIsUseFormingPreTax;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.TabbedControlGroup tabbedControlGroup;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupInfo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupAllowedReports;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraBars.PopupMenu popupMenu;
        private DevExpress.XtraBars.BarButtonItem barBtnAdd;
        private DevExpress.XtraBars.BarButtonItem barBtnDel;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
    }
}