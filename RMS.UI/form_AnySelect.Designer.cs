namespace RMS.UI
{
    partial class form_AnySelect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(form_AnySelect));
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
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
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(412, 392);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Отмена";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(493, 392);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "Выбрать";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.Location = new System.Drawing.Point(1, 26);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(568, 363);
            this.gridControl1.TabIndex = 7;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsFind.ShowCloseButton = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gridView1_PopupMenuShowing);
            this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
            this.gridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridView1_KeyDown);
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
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
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(568, 24);
            this.panelControl1.TabIndex = 8;
            // 
            // btnPrint
            // 
            this.btnPrint.ImageIndex = 7;
            this.btnPrint.ImageList = this.imageCollection1;
            this.btnPrint.Location = new System.Drawing.Point(137, 2);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(24, 20);
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
            this.btnCopy.ImageIndex = 2;
            this.btnCopy.ImageList = this.imageCollection1;
            this.btnCopy.Location = new System.Drawing.Point(52, 2);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(24, 20);
            this.btnCopy.TabIndex = 4;
            this.btnCopy.ToolTip = " Копировать ";
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.ImageIndex = 3;
            this.btnDelete.ImageList = this.imageCollection1;
            this.btnDelete.Location = new System.Drawing.Point(77, 2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(24, 20);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.ToolTip = " Удалить ";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.ImageIndex = 1;
            this.btnEdit.ImageList = this.imageCollection1;
            this.btnEdit.Location = new System.Drawing.Point(27, 2);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(24, 20);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.ToolTip = " Редактировать ";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnNew
            // 
            this.btnNew.ImageIndex = 0;
            this.btnNew.ImageList = this.imageCollection1;
            this.btnNew.Location = new System.Drawing.Point(2, 2);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(24, 20);
            this.btnNew.TabIndex = 0;
            this.btnNew.ToolTip = " Добавить ";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // chkbtnFilter
            // 
            this.chkbtnFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkbtnFilter.ImageIndex = 5;
            this.chkbtnFilter.ImageList = this.imageCollection1;
            this.chkbtnFilter.Location = new System.Drawing.Point(518, 3);
            this.chkbtnFilter.Name = "chkbtnFilter";
            this.chkbtnFilter.Size = new System.Drawing.Size(24, 20);
            this.chkbtnFilter.TabIndex = 8;
            this.chkbtnFilter.ToolTip = " Строковые фильтры ";
            this.chkbtnFilter.CheckedChanged += new System.EventHandler(this.chkbtnFilter_CheckedChanged);
            // 
            // chkbtnFind
            // 
            this.chkbtnFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkbtnFind.ImageIndex = 4;
            this.chkbtnFind.ImageList = this.imageCollection1;
            this.chkbtnFind.Location = new System.Drawing.Point(493, 3);
            this.chkbtnFind.Name = "chkbtnFind";
            this.chkbtnFind.Size = new System.Drawing.Size(24, 20);
            this.chkbtnFind.TabIndex = 9;
            this.chkbtnFind.ToolTip = " Поиск по таблице ";
            this.chkbtnFind.CheckedChanged += new System.EventHandler(this.chkbtnFind_CheckedChanged);
            // 
            // chkbtnComment
            // 
            this.chkbtnComment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkbtnComment.ImageIndex = 6;
            this.chkbtnComment.ImageList = this.imageCollection1;
            this.chkbtnComment.Location = new System.Drawing.Point(543, 3);
            this.chkbtnComment.Name = "chkbtnComment";
            this.chkbtnComment.Size = new System.Drawing.Size(24, 20);
            this.chkbtnComment.TabIndex = 10;
            this.chkbtnComment.ToolTip = " Строковые фильтры ";
            this.chkbtnComment.CheckedChanged += new System.EventHandler(this.chkbtnComment_CheckedChanged);
            // 
            // memoComment
            // 
            this.memoComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.memoComment.Location = new System.Drawing.Point(1, 344);
            this.memoComment.Name = "memoComment";
            this.memoComment.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.memoComment.Properties.Appearance.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.memoComment.Properties.Appearance.Options.UseBackColor = true;
            this.memoComment.Properties.Appearance.Options.UseForeColor = true;
            this.memoComment.Properties.ReadOnly = true;
            this.memoComment.Size = new System.Drawing.Size(568, 45);
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
            this.barBtnSelect.ImageIndex = 8;
            this.barBtnSelect.Name = "barBtnSelect";
            this.barBtnSelect.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnSelect_ItemClick);
            // 
            // barBtnNew
            // 
            this.barBtnNew.Caption = "Добавить";
            this.barBtnNew.Id = 0;
            this.barBtnNew.ImageIndex = 0;
            this.barBtnNew.Name = "barBtnNew";
            this.barBtnNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnNew_ItemClick);
            // 
            // barBtnEdit
            // 
            this.barBtnEdit.Caption = "Редактировать";
            this.barBtnEdit.Id = 1;
            this.barBtnEdit.ImageIndex = 1;
            this.barBtnEdit.Name = "barBtnEdit";
            this.barBtnEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnEdit_ItemClick);
            // 
            // barBtnCopy
            // 
            this.barBtnCopy.Caption = "Копировать";
            this.barBtnCopy.Id = 2;
            this.barBtnCopy.ImageIndex = 2;
            this.barBtnCopy.Name = "barBtnCopy";
            this.barBtnCopy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnCopy_ItemClick);
            // 
            // barBtnDelete
            // 
            this.barBtnDelete.Caption = "Удалить";
            this.barBtnDelete.Id = 3;
            this.barBtnDelete.ImageIndex = 3;
            this.barBtnDelete.Name = "barBtnDelete";
            this.barBtnDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnDelete_ItemClick);
            // 
            // barBtnPrint
            // 
            this.barBtnPrint.Caption = "Печать";
            this.barBtnPrint.Id = 4;
            this.barBtnPrint.ImageIndex = 7;
            this.barBtnPrint.Name = "barBtnPrint";
            // 
            // barChkFind
            // 
            this.barChkFind.Caption = "Поиск";
            this.barChkFind.Id = 5;
            this.barChkFind.ImageIndex = 4;
            this.barChkFind.Name = "barChkFind";
            this.barChkFind.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barChkFind_ItemClick);
            // 
            // barChkFilter
            // 
            this.barChkFilter.Caption = "Фильтр";
            this.barChkFilter.Id = 7;
            this.barChkFilter.ImageIndex = 5;
            this.barChkFilter.Name = "barChkFilter";
            this.barChkFilter.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barChkFilter_ItemClick);
            // 
            // barChkComment
            // 
            this.barChkComment.Caption = "Примечание";
            this.barChkComment.Id = 8;
            this.barChkComment.ImageIndex = 6;
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
            this.barButtonItem1,
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
            this.barDockControlTop.Size = new System.Drawing.Size(569, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 416);
            this.barDockControlBottom.Size = new System.Drawing.Size(569, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 416);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(569, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 416);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barButtonItem1";
            this.barButtonItem1.Id = 6;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // form_AnySelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(569, 416);
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
            this.Name = "form_AnySelect";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = " Выборка";
            this.Activated += new System.EventHandler(this.form_AnySelect_Activated);
            this.Load += new System.EventHandler(this.form_AnySelect_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
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
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barBtnSelect;
    }
}