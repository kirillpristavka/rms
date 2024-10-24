namespace RMS.UI.Forms
{
    partial class TelegramForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TelegramForm));
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlTelegram = new DevExpress.XtraLayout.LayoutControl();
            this.xtraScrollableControl = new DevExpress.XtraEditors.XtraScrollableControl();
            this.memoMessage = new DevExpress.XtraEditors.MemoEdit();
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barBtnAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnDel = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnEdit = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnUpdate = new DevExpress.XtraBars.BarButtonItem();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemGridControl = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItemTGUser = new DevExpress.XtraLayout.SplitterItem();
            this.layoutControlItemMessage = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItemMessage = new DevExpress.XtraLayout.SplitterItem();
            this.layoutControlItemxtraScrollableControl = new DevExpress.XtraLayout.LayoutControlItem();
            this.popupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlTelegram)).BeginInit();
            this.layoutControlTelegram.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoMessage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItemTGUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItemMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemxtraScrollableControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl
            // 
            this.gridControl.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.gridControl.Location = new System.Drawing.Point(346, 12);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(1012, 580);
            this.gridControl.TabIndex = 34;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView.DetailHeight = 394;
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.Editable = false;
            this.gridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gridView.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gridView.OptionsView.RowAutoHeight = true;
            this.gridView.OptionsView.ShowGroupPanel = false;
            this.gridView.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView_RowCellStyle);
            this.gridView.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView_RowStyle);  
            this.gridView.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gridView_PopupMenuShowing);
            // 
            // layoutControlTelegram
            // 
            this.layoutControlTelegram.Controls.Add(this.xtraScrollableControl);
            this.layoutControlTelegram.Controls.Add(this.memoMessage);
            this.layoutControlTelegram.Controls.Add(this.gridControl);
            this.layoutControlTelegram.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlTelegram.Location = new System.Drawing.Point(0, 0);
            this.layoutControlTelegram.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.layoutControlTelegram.Name = "layoutControlTelegram";
            this.layoutControlTelegram.Root = this.Root;
            this.layoutControlTelegram.Size = new System.Drawing.Size(1370, 772);
            this.layoutControlTelegram.TabIndex = 39;
            this.layoutControlTelegram.Text = "layoutControl1";
            // 
            // xtraScrollableControl
            // 
            this.xtraScrollableControl.Location = new System.Drawing.Point(12, 12);
            this.xtraScrollableControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.xtraScrollableControl.MaximumSize = new System.Drawing.Size(750, 0);
            this.xtraScrollableControl.MinimumSize = new System.Drawing.Size(312, 169);
            this.xtraScrollableControl.Name = "xtraScrollableControl";
            this.xtraScrollableControl.Size = new System.Drawing.Size(315, 748);
            this.xtraScrollableControl.TabIndex = 0;
            // 
            // memoMessage
            // 
            this.memoMessage.Location = new System.Drawing.Point(346, 611);
            this.memoMessage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.memoMessage.MenuManager = this.barManager;
            this.memoMessage.MinimumSize = new System.Drawing.Size(0, 112);
            this.memoMessage.Name = "memoMessage";
            this.memoMessage.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.memoMessage.Size = new System.Drawing.Size(1012, 149);
            this.memoMessage.StyleController = this.layoutControlTelegram;
            this.memoMessage.TabIndex = 36;
            this.memoMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.memoMessage_KeyDown);
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
            this.barBtnUpdate});
            this.barManager.MaxItemId = 5;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.barDockControlTop.Size = new System.Drawing.Size(1370, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 772);
            this.barDockControlBottom.Manager = this.barManager;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.barDockControlBottom.Size = new System.Drawing.Size(1370, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 772);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1370, 0);
            this.barDockControlRight.Manager = this.barManager;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 772);
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
            // barBtnUpdate
            // 
            this.barBtnUpdate.Caption = "Обновить";
            this.barBtnUpdate.Id = 4;
            this.barBtnUpdate.ImageOptions.Image = global::RMS.UI.Properties.Resources.recurrence_16x16;
            this.barBtnUpdate.ImageOptions.LargeImage = global::RMS.UI.Properties.Resources.recurrence_32x32;
            this.barBtnUpdate.Name = "barBtnUpdate";
            this.barBtnUpdate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnUpdate_ItemClick);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemGridControl,
            this.splitterItemTGUser,
            this.layoutControlItemMessage,
            this.splitterItemMessage,
            this.layoutControlItemxtraScrollableControl});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1370, 772);
            this.Root.TextVisible = false;
            // 
            // layoutControlItemGridControl
            // 
            this.layoutControlItemGridControl.Control = this.gridControl;
            this.layoutControlItemGridControl.Location = new System.Drawing.Point(334, 0);
            this.layoutControlItemGridControl.Name = "layoutControlItemGridControl";
            this.layoutControlItemGridControl.Size = new System.Drawing.Size(1016, 584);
            this.layoutControlItemGridControl.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemGridControl.TextVisible = false;
            // 
            // splitterItemTGUser
            // 
            this.splitterItemTGUser.AllowHotTrack = true;
            this.splitterItemTGUser.Location = new System.Drawing.Point(319, 0);
            this.splitterItemTGUser.Name = "splitterItemTGUser";
            this.splitterItemTGUser.Size = new System.Drawing.Size(15, 752);
            // 
            // layoutControlItemMessage
            // 
            this.layoutControlItemMessage.Control = this.memoMessage;
            this.layoutControlItemMessage.Location = new System.Drawing.Point(334, 599);
            this.layoutControlItemMessage.Name = "layoutControlItemMessage";
            this.layoutControlItemMessage.Size = new System.Drawing.Size(1016, 153);
            this.layoutControlItemMessage.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemMessage.TextVisible = false;
            // 
            // splitterItemMessage
            // 
            this.splitterItemMessage.AllowHotTrack = true;
            this.splitterItemMessage.Location = new System.Drawing.Point(334, 584);
            this.splitterItemMessage.Name = "splitterItemMessage";
            this.splitterItemMessage.Size = new System.Drawing.Size(1016, 15);
            // 
            // layoutControlItemxtraScrollableControl
            // 
            this.layoutControlItemxtraScrollableControl.Control = this.xtraScrollableControl;
            this.layoutControlItemxtraScrollableControl.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemxtraScrollableControl.Name = "layoutControlItemxtraScrollableControl";
            this.layoutControlItemxtraScrollableControl.Size = new System.Drawing.Size(319, 752);
            this.layoutControlItemxtraScrollableControl.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemxtraScrollableControl.TextVisible = false;
            // 
            // popupMenu
            // 
            this.popupMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnAdd),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnEdit),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnDel),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnUpdate, true)});
            this.popupMenu.Manager = this.barManager;
            this.popupMenu.Name = "popupMenu";
            // 
            // TelegramForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 772);
            this.Controls.Add(this.layoutControlTelegram);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "TelegramForm";
            this.Text = "Telegram";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlTelegram)).EndInit();
            this.layoutControlTelegram.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoMessage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItemTGUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItemMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemxtraScrollableControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraLayout.LayoutControl layoutControlTelegram;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemGridControl;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barBtnAdd;
        private DevExpress.XtraBars.BarButtonItem barBtnDel;
        private DevExpress.XtraBars.BarButtonItem barBtnEdit;
        private DevExpress.XtraBars.PopupMenu popupMenu;
        private DevExpress.XtraBars.BarButtonItem barBtnUpdate;
        private DevExpress.XtraLayout.SplitterItem splitterItemTGUser;
        private DevExpress.XtraEditors.MemoEdit memoMessage;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemMessage;
        private DevExpress.XtraLayout.SplitterItem splitterItemMessage;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemxtraScrollableControl;
    }
}