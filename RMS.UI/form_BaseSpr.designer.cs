namespace RMS.UI
{
    partial class form_BaseSpr
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(form_BaseSpr));
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.btnCopy = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.btnNew = new DevExpress.XtraEditors.SimpleButton();
            this.chkbtnFilter = new DevExpress.XtraEditors.CheckButton();
            this.chkbtnFind = new DevExpress.XtraEditors.CheckButton();
            this.chkbtnComment = new DevExpress.XtraEditors.CheckButton();
            this.memoComment = new DevExpress.XtraEditors.MemoEdit();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.barBtnSelect = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnNew = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnEdit = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnCopy = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnPrint = new DevExpress.XtraBars.BarButtonItem();
            this.barChkFind = new DevExpress.XtraBars.BarCheckItem();
            this.barChkFilter = new DevExpress.XtraBars.BarCheckItem();
            this.barChkComment = new DevExpress.XtraBars.BarCheckItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.chkFlAllDateRange = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFlAllDateRange.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(575, 530);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Отмена";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(683, 530);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 28);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "Выбрать";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.gridControl1.Location = new System.Drawing.Point(1, 32);
            this.gridControl1.MainView = this.gridView;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(4);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(783, 494);
            this.gridControl1.TabIndex = 7;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView1
            // 
            this.gridView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView.DetailHeight = 431;
            this.gridView.GridControl = this.gridControl1;
            this.gridView.Name = "gridView1";
            this.gridView.OptionsBehavior.Editable = false;
            this.gridView.OptionsFind.ShowCloseButton = false;
            this.gridView.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gridView.OptionsView.ShowGroupPanel = false;
            this.gridView.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gridView1_PopupMenuShowing);
            this.gridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
            this.gridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridView1_KeyDown);
            this.gridView.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl1.Controls.Add(this.btnPrint);
            this.panelControl1.Controls.Add(this.btnCopy);
            this.panelControl1.Controls.Add(this.btnDelete);
            this.panelControl1.Controls.Add(this.btnEdit);
            this.panelControl1.Controls.Add(this.btnNew);
            this.panelControl1.Location = new System.Drawing.Point(1, 1);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(4);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(783, 30);
            this.panelControl1.TabIndex = 8;
            // 
            // btnPrint
            // 
            this.btnPrint.ImageOptions.ImageIndex = 7;
            this.btnPrint.ImageOptions.ImageList = this.imageCollection1;
            this.btnPrint.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnPrint.Location = new System.Drawing.Point(183, 2);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(32, 25);
            this.btnPrint.TabIndex = 7;
            this.btnPrint.ToolTip = " Печать ";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "document--plus.png");
            this.imageCollection1.Images.SetKeyName(1, "document--pencil.png");
            this.imageCollection1.Images.SetKeyName(2, "document-copy.png");
            this.imageCollection1.Images.SetKeyName(3, "document--minus.png");
            this.imageCollection1.Images.SetKeyName(4, "binocular.png");
            this.imageCollection1.Images.SetKeyName(5, "funnel.png");
            this.imageCollection1.Images.SetKeyName(6, "application-list.png");
            this.imageCollection1.Images.SetKeyName(7, "printer.png");
            this.imageCollection1.Images.SetKeyName(8, "tick.png");
            // 
            // btnCopy
            // 
            this.btnCopy.ImageOptions.ImageIndex = 2;
            this.btnCopy.ImageOptions.ImageList = this.imageCollection1;
            this.btnCopy.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnCopy.Location = new System.Drawing.Point(69, 2);
            this.btnCopy.Margin = new System.Windows.Forms.Padding(4);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(32, 25);
            this.btnCopy.TabIndex = 4;
            this.btnCopy.ToolTip = " Копировать ";
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.ImageOptions.ImageIndex = 3;
            this.btnDelete.ImageOptions.ImageList = this.imageCollection1;
            this.btnDelete.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnDelete.Location = new System.Drawing.Point(103, 2);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(32, 25);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.ToolTip = " Удалить ";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.ImageOptions.ImageIndex = 1;
            this.btnEdit.ImageOptions.ImageList = this.imageCollection1;
            this.btnEdit.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnEdit.Location = new System.Drawing.Point(36, 2);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(4);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(32, 25);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.ToolTip = " Редактировать ";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnNew
            // 
            this.btnNew.ImageOptions.ImageIndex = 0;
            this.btnNew.ImageOptions.ImageList = this.imageCollection1;
            this.btnNew.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnNew.Location = new System.Drawing.Point(3, 2);
            this.btnNew.Margin = new System.Windows.Forms.Padding(4);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(32, 25);
            this.btnNew.TabIndex = 0;
            this.btnNew.ToolTip = " Добавить ";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // chkbtnFilter
            // 
            this.chkbtnFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkbtnFilter.ImageOptions.ImageIndex = 5;
            this.chkbtnFilter.ImageOptions.ImageList = this.imageCollection1;
            this.chkbtnFilter.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.chkbtnFilter.Location = new System.Drawing.Point(716, 4);
            this.chkbtnFilter.Margin = new System.Windows.Forms.Padding(4);
            this.chkbtnFilter.Name = "chkbtnFilter";
            this.chkbtnFilter.Size = new System.Drawing.Size(32, 25);
            this.chkbtnFilter.TabIndex = 8;
            this.chkbtnFilter.ToolTip = " Строковые фильтры ";
            this.chkbtnFilter.CheckedChanged += new System.EventHandler(this.chkbtnFilter_CheckedChanged);
            // 
            // chkbtnFind
            // 
            this.chkbtnFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkbtnFind.ImageOptions.ImageIndex = 4;
            this.chkbtnFind.ImageOptions.ImageList = this.imageCollection1;
            this.chkbtnFind.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.chkbtnFind.Location = new System.Drawing.Point(683, 4);
            this.chkbtnFind.Margin = new System.Windows.Forms.Padding(4);
            this.chkbtnFind.Name = "chkbtnFind";
            this.chkbtnFind.Size = new System.Drawing.Size(32, 25);
            this.chkbtnFind.TabIndex = 9;
            this.chkbtnFind.ToolTip = " Поиск по таблице ";
            this.chkbtnFind.CheckedChanged += new System.EventHandler(this.chkbtnFind_CheckedChanged);
            // 
            // chkbtnComment
            // 
            this.chkbtnComment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkbtnComment.ImageOptions.ImageIndex = 6;
            this.chkbtnComment.ImageOptions.ImageList = this.imageCollection1;
            this.chkbtnComment.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.chkbtnComment.Location = new System.Drawing.Point(749, 4);
            this.chkbtnComment.Margin = new System.Windows.Forms.Padding(4);
            this.chkbtnComment.Name = "chkbtnComment";
            this.chkbtnComment.Size = new System.Drawing.Size(32, 25);
            this.chkbtnComment.TabIndex = 10;
            this.chkbtnComment.ToolTip = " Строковые фильтры ";
            this.chkbtnComment.CheckedChanged += new System.EventHandler(this.chkbtnComment_CheckedChanged);
            // 
            // memoComment
            // 
            this.memoComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.memoComment.Location = new System.Drawing.Point(1, 471);
            this.memoComment.Margin = new System.Windows.Forms.Padding(4);
            this.memoComment.Name = "memoComment";
            this.memoComment.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.memoComment.Properties.Appearance.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.memoComment.Properties.Appearance.Options.UseBackColor = true;
            this.memoComment.Properties.Appearance.Options.UseForeColor = true;
            this.memoComment.Properties.ReadOnly = true;
            this.memoComment.Size = new System.Drawing.Size(783, 55);
            this.memoComment.TabIndex = 11;
            this.memoComment.Visible = false;
            // 
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnSelect),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnNew, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnEdit),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnCopy),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnDelete),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barBtnPrint, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.Standard),
            new DevExpress.XtraBars.LinkPersistInfo(this.barChkFind, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barChkFilter),
            new DevExpress.XtraBars.LinkPersistInfo(this.barChkComment)});
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            // 
            // barBtnSelect
            // 
            this.barBtnSelect.Caption = "Выбрать";
            this.barBtnSelect.Id = 9;
            this.barBtnSelect.ImageOptions.ImageIndex = 8;
            this.barBtnSelect.Name = "barBtnSelect";
            this.barBtnSelect.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnSelect_ItemClick);
            // 
            // barBtnNew
            // 
            this.barBtnNew.Caption = "Добавить";
            this.barBtnNew.Id = 0;
            this.barBtnNew.ImageOptions.ImageIndex = 0;
            this.barBtnNew.Name = "barBtnNew";
            this.barBtnNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnNew_ItemClick);
            // 
            // barBtnEdit
            // 
            this.barBtnEdit.Caption = "Редактировать";
            this.barBtnEdit.Id = 1;
            this.barBtnEdit.ImageOptions.ImageIndex = 1;
            this.barBtnEdit.Name = "barBtnEdit";
            this.barBtnEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnEdit_ItemClick);
            // 
            // barBtnCopy
            // 
            this.barBtnCopy.Caption = "Копировать";
            this.barBtnCopy.Id = 2;
            this.barBtnCopy.ImageOptions.ImageIndex = 2;
            this.barBtnCopy.Name = "barBtnCopy";
            this.barBtnCopy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnCopy_ItemClick);
            // 
            // barBtnDelete
            // 
            this.barBtnDelete.Caption = "Удалить";
            this.barBtnDelete.Id = 3;
            this.barBtnDelete.ImageOptions.ImageIndex = 3;
            this.barBtnDelete.Name = "barBtnDelete";
            this.barBtnDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnDelete_ItemClick);
            // 
            // barBtnPrint
            // 
            this.barBtnPrint.Caption = "Печать";
            this.barBtnPrint.Id = 4;
            this.barBtnPrint.ImageOptions.ImageIndex = 7;
            this.barBtnPrint.Name = "barBtnPrint";
            this.barBtnPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnPrint_ItemClick);
            // 
            // barChkFind
            // 
            this.barChkFind.Caption = "Поиск";
            this.barChkFind.Id = 5;
            this.barChkFind.ImageOptions.ImageIndex = 4;
            this.barChkFind.Name = "barChkFind";
            this.barChkFind.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barChkFind_ItemClick);
            // 
            // barChkFilter
            // 
            this.barChkFilter.Caption = "Фильтр";
            this.barChkFilter.Id = 7;
            this.barChkFilter.ImageOptions.ImageIndex = 5;
            this.barChkFilter.Name = "barChkFilter";
            this.barChkFilter.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barChkFilter_ItemClick);
            // 
            // barChkComment
            // 
            this.barChkComment.Caption = "Примечание";
            this.barChkComment.Id = 8;
            this.barChkComment.ImageOptions.ImageIndex = 6;
            this.barChkComment.Name = "barChkComment";
            this.barChkComment.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barChkComment_ItemClick);
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageCollection1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barBtnNew,
            this.barBtnEdit,
            this.barBtnCopy,
            this.barBtnDelete,
            this.barBtnPrint,
            this.barChkFind,
            this.barChkFilter,
            this.barChkComment,
            this.barBtnSelect});
            this.barManager1.MaxItemId = 10;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(4);
            this.barDockControlTop.Size = new System.Drawing.Size(784, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 560);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(4);
            this.barDockControlBottom.Size = new System.Drawing.Size(784, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(4);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 560);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(784, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(4);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 560);
            // 
            // chkFlAllDateRange
            // 
            this.chkFlAllDateRange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkFlAllDateRange.Location = new System.Drawing.Point(1, 535);
            this.chkFlAllDateRange.Margin = new System.Windows.Forms.Padding(4);
            this.chkFlAllDateRange.Name = "chkFlAllDateRange";
            this.chkFlAllDateRange.Properties.Caption = " все временные диапазоны";
            this.chkFlAllDateRange.Size = new System.Drawing.Size(219, 20);
            this.chkFlAllDateRange.TabIndex = 232;
            this.chkFlAllDateRange.TabStop = false;
            this.chkFlAllDateRange.Visible = false;
            this.chkFlAllDateRange.CheckedChanged += new System.EventHandler(this.chkFlAllDateRange_CheckedChanged);
            // 
            // form_BaseSpr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(784, 560);
            this.Controls.Add(this.chkFlAllDateRange);
            this.Controls.Add(this.chkbtnComment);
            this.Controls.Add(this.chkbtnFind);
            this.Controls.Add(this.chkbtnFilter);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.memoComment);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "form_BaseSpr";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = " Справочник 2";
            this.Activated += new System.EventHandler(this.form_BaseSpr_Activated);
            this.Load += new System.EventHandler(this.form_BaseSpr_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFlAllDateRange.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnNew;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraEditors.SimpleButton btnCopy;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnEdit;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraEditors.CheckButton chkbtnFilter;
        private DevExpress.XtraEditors.CheckButton chkbtnFind;
        private DevExpress.XtraEditors.CheckButton chkbtnComment;
        private DevExpress.XtraEditors.MemoEdit memoComment;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraBars.BarButtonItem barBtnNew;
        private DevExpress.XtraBars.BarButtonItem barBtnEdit;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barBtnCopy;
        private DevExpress.XtraBars.BarButtonItem barBtnDelete;
        private DevExpress.XtraBars.BarButtonItem barBtnPrint;
        private DevExpress.XtraBars.BarCheckItem barChkFind;
        private DevExpress.XtraBars.BarCheckItem barChkFilter;
        private DevExpress.XtraBars.BarCheckItem barChkComment;
        private DevExpress.XtraBars.BarButtonItem barBtnSelect;
        private DevExpress.XtraEditors.CheckEdit chkFlAllDateRange;
    }
}