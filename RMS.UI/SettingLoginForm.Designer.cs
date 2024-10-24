namespace RMS.UI
{
    partial class SettingLoginForm
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
            this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
            this.xtraTabControl = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControlOptions = new DevExpress.XtraLayout.LayoutControl();
            this.btnRemoveAutomaticOpeningForms = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroupOptions = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemRemoveAutomaticOpeningForms = new DevExpress.XtraLayout.LayoutControlItem();
            this.treeList = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumnName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItemSave = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItemCancel = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItemTreeList = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem = new DevExpress.XtraLayout.SplitterItem();
            this.layoutControlItemTabPage = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemSave = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemCancel = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnClearUserSettingsView = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItemClearUserSettingsView = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
            this.layoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
            this.xtraTabControl.SuspendLayout();
            this.xtraTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlOptions)).BeginInit();
            this.layoutControlOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupOptions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemRemoveAutomaticOpeningForms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTreeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTabPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemClearUserSettingsView)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl
            // 
            this.layoutControl.AllowCustomization = false;
            this.layoutControl.Controls.Add(this.xtraTabControl);
            this.layoutControl.Controls.Add(this.treeList);
            this.layoutControl.Controls.Add(this.btnCancel);
            this.layoutControl.Controls.Add(this.btnSave);
            this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl.Location = new System.Drawing.Point(0, 0);
            this.layoutControl.Name = "layoutControl";
            this.layoutControl.Root = this.Root;
            this.layoutControl.Size = new System.Drawing.Size(794, 482);
            this.layoutControl.TabIndex = 0;
            this.layoutControl.Text = "layoutControl1";
            // 
            // xtraTabControl
            // 
            this.xtraTabControl.Location = new System.Drawing.Point(223, 7);
            this.xtraTabControl.Name = "xtraTabControl";
            this.xtraTabControl.SelectedTabPage = this.xtraTabPage;
            this.xtraTabControl.Size = new System.Drawing.Size(564, 442);
            this.xtraTabControl.TabIndex = 7;
            this.xtraTabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage});
            // 
            // xtraTabPage
            // 
            this.xtraTabPage.Controls.Add(this.layoutControlOptions);
            this.xtraTabPage.Name = "xtraTabPage";
            this.xtraTabPage.Size = new System.Drawing.Size(562, 414);
            this.xtraTabPage.Text = "Параметры";
            // 
            // layoutControlOptions
            // 
            this.layoutControlOptions.AllowCustomization = false;
            this.layoutControlOptions.Controls.Add(this.btnRemoveAutomaticOpeningForms);
            this.layoutControlOptions.Controls.Add(this.btnClearUserSettingsView);
            this.layoutControlOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlOptions.Location = new System.Drawing.Point(0, 0);
            this.layoutControlOptions.Name = "layoutControlOptions";
            this.layoutControlOptions.Root = this.layoutControlGroupOptions;
            this.layoutControlOptions.Size = new System.Drawing.Size(562, 414);
            this.layoutControlOptions.TabIndex = 0;
            this.layoutControlOptions.Text = "layoutControl1";
            // 
            // btnRemoveAutomaticOpeningForms
            // 
            this.btnRemoveAutomaticOpeningForms.AllowFocus = false;
            this.btnRemoveAutomaticOpeningForms.Location = new System.Drawing.Point(7, 7);
            this.btnRemoveAutomaticOpeningForms.Name = "btnRemoveAutomaticOpeningForms";
            this.btnRemoveAutomaticOpeningForms.Size = new System.Drawing.Size(548, 22);
            this.btnRemoveAutomaticOpeningForms.StyleController = this.layoutControlOptions;
            this.btnRemoveAutomaticOpeningForms.TabIndex = 4;
            this.btnRemoveAutomaticOpeningForms.Text = "Убрать автоматическое открытие вкладок";
            this.btnRemoveAutomaticOpeningForms.Click += new System.EventHandler(this.btnClearLocalSettings_Click);
            // 
            // layoutControlGroupOptions
            // 
            this.layoutControlGroupOptions.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupOptions.GroupBordersVisible = false;
            this.layoutControlGroupOptions.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemRemoveAutomaticOpeningForms,
            this.layoutControlItemClearUserSettingsView});
            this.layoutControlGroupOptions.Name = "layoutControlGroupOptions";
            this.layoutControlGroupOptions.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroupOptions.Size = new System.Drawing.Size(562, 414);
            this.layoutControlGroupOptions.TextVisible = false;
            // 
            // layoutControlItemRemoveAutomaticOpeningForms
            // 
            this.layoutControlItemRemoveAutomaticOpeningForms.Control = this.btnRemoveAutomaticOpeningForms;
            this.layoutControlItemRemoveAutomaticOpeningForms.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemRemoveAutomaticOpeningForms.Name = "layoutControlItemRemoveAutomaticOpeningForms";
            this.layoutControlItemRemoveAutomaticOpeningForms.Size = new System.Drawing.Size(552, 26);
            this.layoutControlItemRemoveAutomaticOpeningForms.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemRemoveAutomaticOpeningForms.TextVisible = false;
            // 
            // treeList
            // 
            this.treeList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumnName});
            this.treeList.CustomizationFormBounds = new System.Drawing.Rectangle(583, 348, 266, 274);
            this.treeList.Location = new System.Drawing.Point(7, 7);
            this.treeList.MaximumSize = new System.Drawing.Size(400, 0);
            this.treeList.MinimumSize = new System.Drawing.Size(200, 0);
            this.treeList.Name = "treeList";
            this.treeList.BeginUnboundLoad();
            this.treeList.AppendNode(new object[] {
            "Параметры"}, -1);
            this.treeList.EndUnboundLoad();
            this.treeList.OptionsBehavior.Editable = false;
            this.treeList.OptionsPrint.PrintHorzLines = false;
            this.treeList.OptionsPrint.PrintImages = false;
            this.treeList.OptionsPrint.PrintPageHeader = false;
            this.treeList.OptionsPrint.PrintReportFooter = false;
            this.treeList.OptionsPrint.PrintVertLines = false;
            this.treeList.OptionsView.ShowColumns = false;
            this.treeList.Size = new System.Drawing.Size(200, 442);
            this.treeList.TabIndex = 6;
            // 
            // treeListColumnName
            // 
            this.treeListColumnName.Caption = "Наименование";
            this.treeListColumnName.FieldName = "Наименование";
            this.treeListColumnName.Name = "treeListColumnName";
            this.treeListColumnName.OptionsColumn.AllowSort = true;
            this.treeListColumnName.Visible = true;
            this.treeListColumnName.VisibleIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(595, 453);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(192, 22);
            this.btnCancel.StyleController = this.layoutControl;
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Отмена";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(399, 453);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(192, 22);
            this.btnSave.StyleController = this.layoutControl;
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Сохранить";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItemSave,
            this.emptySpaceItemCancel,
            this.layoutControlItemTreeList,
            this.splitterItem,
            this.layoutControlItemTabPage,
            this.layoutControlItemSave,
            this.layoutControlItemCancel});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.Root.Size = new System.Drawing.Size(794, 482);
            this.Root.TextVisible = false;
            // 
            // emptySpaceItemSave
            // 
            this.emptySpaceItemSave.AllowHotTrack = false;
            this.emptySpaceItemSave.Location = new System.Drawing.Point(0, 446);
            this.emptySpaceItemSave.Name = "emptySpaceItemSave";
            this.emptySpaceItemSave.Size = new System.Drawing.Size(195, 26);
            this.emptySpaceItemSave.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItemCancel
            // 
            this.emptySpaceItemCancel.AllowHotTrack = false;
            this.emptySpaceItemCancel.Location = new System.Drawing.Point(195, 446);
            this.emptySpaceItemCancel.Name = "emptySpaceItemCancel";
            this.emptySpaceItemCancel.Size = new System.Drawing.Size(197, 26);
            this.emptySpaceItemCancel.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItemTreeList
            // 
            this.layoutControlItemTreeList.Control = this.treeList;
            this.layoutControlItemTreeList.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemTreeList.Name = "layoutControlItemTreeList";
            this.layoutControlItemTreeList.Size = new System.Drawing.Size(204, 446);
            this.layoutControlItemTreeList.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemTreeList.TextVisible = false;
            // 
            // splitterItem
            // 
            this.splitterItem.AllowHotTrack = true;
            this.splitterItem.Location = new System.Drawing.Point(204, 0);
            this.splitterItem.Name = "splitterItem";
            this.splitterItem.Size = new System.Drawing.Size(12, 446);
            // 
            // layoutControlItemTabPage
            // 
            this.layoutControlItemTabPage.Control = this.xtraTabControl;
            this.layoutControlItemTabPage.Location = new System.Drawing.Point(216, 0);
            this.layoutControlItemTabPage.Name = "layoutControlItemTabPage";
            this.layoutControlItemTabPage.Size = new System.Drawing.Size(568, 446);
            this.layoutControlItemTabPage.Text = "layoutControlItem";
            this.layoutControlItemTabPage.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemTabPage.TextVisible = false;
            // 
            // layoutControlItemSave
            // 
            this.layoutControlItemSave.Control = this.btnSave;
            this.layoutControlItemSave.Location = new System.Drawing.Point(392, 446);
            this.layoutControlItemSave.Name = "layoutControlItemSave";
            this.layoutControlItemSave.Size = new System.Drawing.Size(196, 26);
            this.layoutControlItemSave.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemSave.TextVisible = false;
            // 
            // layoutControlItemCancel
            // 
            this.layoutControlItemCancel.Control = this.btnCancel;
            this.layoutControlItemCancel.Location = new System.Drawing.Point(588, 446);
            this.layoutControlItemCancel.Name = "layoutControlItemCancel";
            this.layoutControlItemCancel.Size = new System.Drawing.Size(196, 26);
            this.layoutControlItemCancel.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemCancel.TextVisible = false;
            // 
            // btnClearUserSettingsView
            // 
            this.btnClearUserSettingsView.Location = new System.Drawing.Point(7, 33);
            this.btnClearUserSettingsView.Name = "btnClearUserSettingsView";
            this.btnClearUserSettingsView.Size = new System.Drawing.Size(548, 22);
            this.btnClearUserSettingsView.StyleController = this.layoutControlOptions;
            this.btnClearUserSettingsView.TabIndex = 5;
            this.btnClearUserSettingsView.Text = "Очистить пользовательские настройки интерфейса";
            this.btnClearUserSettingsView.Click += new System.EventHandler(this.btnClearUserSettingsView_Click);
            // 
            // layoutControlItemClearUserSettingsView
            // 
            this.layoutControlItemClearUserSettingsView.Control = this.btnClearUserSettingsView;
            this.layoutControlItemClearUserSettingsView.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItemClearUserSettingsView.Name = "layoutControlItemClearUserSettingsView";
            this.layoutControlItemClearUserSettingsView.Size = new System.Drawing.Size(552, 378);
            this.layoutControlItemClearUserSettingsView.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemClearUserSettingsView.TextVisible = false;
            // 
            // SettingLoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 482);
            this.Controls.Add(this.layoutControl);
            this.Name = "SettingLoginForm";
            this.Text = "Настрокий СКиД";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
            this.layoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
            this.xtraTabControl.ResumeLayout(false);
            this.xtraTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlOptions)).EndInit();
            this.layoutControlOptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupOptions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemRemoveAutomaticOpeningForms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTreeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTabPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemClearUserSettingsView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCancel;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemCancel;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage;
        private DevExpress.XtraTreeList.TreeList treeList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemTreeList;
        private DevExpress.XtraLayout.SplitterItem splitterItem;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemTabPage;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnName;
        private DevExpress.XtraLayout.LayoutControl layoutControlOptions;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupOptions;
        private DevExpress.XtraEditors.SimpleButton btnRemoveAutomaticOpeningForms;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemRemoveAutomaticOpeningForms;
        private DevExpress.XtraEditors.SimpleButton btnClearUserSettingsView;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemClearUserSettingsView;
    }
}