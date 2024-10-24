namespace RMS.UI.Forms.Directories
{
    partial class GroupPerformanceIndicatorEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GroupPerformanceIndicatorEdit));
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.xtraTabInfoGroupPerformanceIndicator = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPagePerformanceIndicator = new DevExpress.XtraTab.XtraTabPage();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControlHeader = new DevExpress.XtraEditors.PanelControl();
            this.btnDel = new DevExpress.XtraEditors.SimpleButton();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.panelControlFooter = new DevExpress.XtraEditors.PanelControl();
            this.panelControlGropUserHeader = new DevExpress.XtraEditors.PanelControl();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.memoDescription = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabInfoGroupPerformanceIndicator)).BeginInit();
            this.xtraTabInfoGroupPerformanceIndicator.SuspendLayout();
            this.xtraTabPagePerformanceIndicator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlHeader)).BeginInit();
            this.panelControlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlFooter)).BeginInit();
            this.panelControlFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlGropUserHeader)).BeginInit();
            this.panelControlGropUserHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoDescription.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(422, 9);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 23);
            this.btnSave.TabIndex = 59;
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(313, 9);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 23);
            this.btnCancel.TabIndex = 58;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // xtraTabInfoGroupPerformanceIndicator
            // 
            this.xtraTabInfoGroupPerformanceIndicator.AppearancePage.HeaderActive.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.xtraTabInfoGroupPerformanceIndicator.AppearancePage.HeaderActive.Options.UseFont = true;
            this.xtraTabInfoGroupPerformanceIndicator.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.xtraTabInfoGroupPerformanceIndicator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabInfoGroupPerformanceIndicator.Location = new System.Drawing.Point(0, 93);
            this.xtraTabInfoGroupPerformanceIndicator.Name = "xtraTabInfoGroupPerformanceIndicator";
            this.xtraTabInfoGroupPerformanceIndicator.PageImagePosition = DevExpress.XtraTab.TabPageImagePosition.None;
            this.xtraTabInfoGroupPerformanceIndicator.SelectedTabPage = this.xtraTabPagePerformanceIndicator;
            this.xtraTabInfoGroupPerformanceIndicator.ShowTabHeader = DevExpress.Utils.DefaultBoolean.True;
            this.xtraTabInfoGroupPerformanceIndicator.Size = new System.Drawing.Size(534, 277);
            this.xtraTabInfoGroupPerformanceIndicator.TabIndex = 70;
            this.xtraTabInfoGroupPerformanceIndicator.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPagePerformanceIndicator});
            // 
            // xtraTabPagePerformanceIndicator
            // 
            this.xtraTabPagePerformanceIndicator.Controls.Add(this.gridControl);
            this.xtraTabPagePerformanceIndicator.Controls.Add(this.panelControlHeader);
            this.xtraTabPagePerformanceIndicator.Name = "xtraTabPagePerformanceIndicator";
            this.xtraTabPagePerformanceIndicator.Size = new System.Drawing.Size(532, 249);
            this.xtraTabPagePerformanceIndicator.Text = "Показатели работы";
            // 
            // gridControl
            // 
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.Location = new System.Drawing.Point(0, 37);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(532, 212);
            this.gridControl.TabIndex = 37;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridViewUsers
            // 
            this.gridView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView.Appearance.Row.Options.UseTextOptions = true;
            this.gridView.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridViewUsers";
            this.gridView.OptionsBehavior.Editable = false;
            this.gridView.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gridView.OptionsView.ShowGroupPanel = false;
            // 
            // panelControlHeader
            // 
            this.panelControlHeader.AutoSize = true;
            this.panelControlHeader.Controls.Add(this.btnDel);
            this.panelControlHeader.Controls.Add(this.btnAdd);
            this.panelControlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControlHeader.Location = new System.Drawing.Point(0, 0);
            this.panelControlHeader.Name = "panelControlHeader";
            this.panelControlHeader.Size = new System.Drawing.Size(532, 37);
            this.panelControlHeader.TabIndex = 38;
            // 
            // btnDel
            // 
            this.btnDel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnUserDel.ImageOptions.Image")));
            this.btnDel.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnDel.Location = new System.Drawing.Point(37, 5);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(25, 25);
            this.btnDel.TabIndex = 2;
            this.btnDel.Text = "+";
            this.btnDel.Click += new System.EventHandler(this.btnUserDel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnUserAdd.ImageOptions.Image")));
            this.btnAdd.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnAdd.Location = new System.Drawing.Point(6, 5);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(25, 25);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "+";
            this.btnAdd.Click += new System.EventHandler(this.btnUserAdd_Click);
            // 
            // panelControlFooter
            // 
            this.panelControlFooter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControlFooter.Controls.Add(this.btnSave);
            this.panelControlFooter.Controls.Add(this.btnCancel);
            this.panelControlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControlFooter.Location = new System.Drawing.Point(0, 370);
            this.panelControlFooter.Name = "panelControlFooter";
            this.panelControlFooter.Size = new System.Drawing.Size(534, 40);
            this.panelControlFooter.TabIndex = 71;
            // 
            // panelControlGropUserHeader
            // 
            this.panelControlGropUserHeader.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControlGropUserHeader.Controls.Add(this.lblDescription);
            this.panelControlGropUserHeader.Controls.Add(this.lblName);
            this.panelControlGropUserHeader.Controls.Add(this.txtName);
            this.panelControlGropUserHeader.Controls.Add(this.memoDescription);
            this.panelControlGropUserHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControlGropUserHeader.Location = new System.Drawing.Point(0, 0);
            this.panelControlGropUserHeader.Name = "panelControlGropUserHeader";
            this.panelControlGropUserHeader.Size = new System.Drawing.Size(534, 93);
            this.panelControlGropUserHeader.TabIndex = 76;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(44, 42);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(80, 16);
            this.lblDescription.TabIndex = 78;
            this.lblDescription.Text = "Описание:";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 15);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(112, 16);
            this.lblName.TabIndex = 76;
            this.lblName.Text = "Наименование:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(142, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(380, 22);
            this.txtName.TabIndex = 77;
            // 
            // memoDescription
            // 
            this.memoDescription.Location = new System.Drawing.Point(142, 40);
            this.memoDescription.Name = "memoDescription";
            this.memoDescription.Size = new System.Drawing.Size(380, 44);
            this.memoDescription.TabIndex = 79;
            // 
            // GroupPerformanceIndicatorEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 410);
            this.Controls.Add(this.xtraTabInfoGroupPerformanceIndicator);
            this.Controls.Add(this.panelControlFooter);
            this.Controls.Add(this.panelControlGropUserHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "GroupPerformanceIndicatorEdit";
            this.Text = "Группа показателей работы";
            this.Load += new System.EventHandler(this.FormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabInfoGroupPerformanceIndicator)).EndInit();
            this.xtraTabInfoGroupPerformanceIndicator.ResumeLayout(false);
            this.xtraTabPagePerformanceIndicator.ResumeLayout(false);
            this.xtraTabPagePerformanceIndicator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlHeader)).EndInit();
            this.panelControlHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlFooter)).EndInit();
            this.panelControlFooter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlGropUserHeader)).EndInit();
            this.panelControlGropUserHeader.ResumeLayout(false);
            this.panelControlGropUserHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoDescription.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraTab.XtraTabControl xtraTabInfoGroupPerformanceIndicator;
        private DevExpress.XtraTab.XtraTabPage xtraTabPagePerformanceIndicator;
        private DevExpress.XtraEditors.PanelControl panelControlFooter;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraEditors.PanelControl panelControlHeader;
        private DevExpress.XtraEditors.SimpleButton btnDel;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.PanelControl panelControlGropUserHeader;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblName;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.MemoEdit memoDescription;
    }
}