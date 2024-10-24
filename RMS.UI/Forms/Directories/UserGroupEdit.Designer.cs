namespace RMS.UI.Forms.Directories
{
    partial class UserGroupEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserGroupEdit));
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.xtraTabInfoUserGroup = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPageUsers = new DevExpress.XtraTab.XtraTabPage();
            this.gridControlUsers = new DevExpress.XtraGrid.GridControl();
            this.gridViewUsers = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControlHeader = new DevExpress.XtraEditors.PanelControl();
            this.btnUserDel = new DevExpress.XtraEditors.SimpleButton();
            this.btnUserAdd = new DevExpress.XtraEditors.SimpleButton();
            this.xtraTabPageAccessRights = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.propertyGridAccess = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.btnBanEverything = new DevExpress.XtraEditors.SimpleButton();
            this.btnAllowAll = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroupAccess = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.panelControlFooter = new DevExpress.XtraEditors.PanelControl();
            this.panelControlGropUserHeader = new DevExpress.XtraEditors.PanelControl();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.memoDescription = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabInfoUserGroup)).BeginInit();
            this.xtraTabInfoUserGroup.SuspendLayout();
            this.xtraTabPageUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlHeader)).BeginInit();
            this.panelControlHeader.SuspendLayout();
            this.xtraTabPageAccessRights.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridAccess)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupAccess)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
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
            // xtraTabInfoUserGroup
            // 
            this.xtraTabInfoUserGroup.AppearancePage.HeaderActive.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.xtraTabInfoUserGroup.AppearancePage.HeaderActive.Options.UseFont = true;
            this.xtraTabInfoUserGroup.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.xtraTabInfoUserGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabInfoUserGroup.Location = new System.Drawing.Point(0, 93);
            this.xtraTabInfoUserGroup.Name = "xtraTabInfoUserGroup";
            this.xtraTabInfoUserGroup.PageImagePosition = DevExpress.XtraTab.TabPageImagePosition.None;
            this.xtraTabInfoUserGroup.SelectedTabPage = this.xtraTabPageUsers;
            this.xtraTabInfoUserGroup.ShowTabHeader = DevExpress.Utils.DefaultBoolean.True;
            this.xtraTabInfoUserGroup.Size = new System.Drawing.Size(534, 377);
            this.xtraTabInfoUserGroup.TabIndex = 70;
            this.xtraTabInfoUserGroup.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageUsers,
            this.xtraTabPageAccessRights});
            // 
            // xtraTabPageUsers
            // 
            this.xtraTabPageUsers.Controls.Add(this.gridControlUsers);
            this.xtraTabPageUsers.Controls.Add(this.panelControlHeader);
            this.xtraTabPageUsers.Name = "xtraTabPageUsers";
            this.xtraTabPageUsers.Size = new System.Drawing.Size(532, 349);
            this.xtraTabPageUsers.Text = "Пользователи группы";
            // 
            // gridControlUsers
            // 
            this.gridControlUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlUsers.Location = new System.Drawing.Point(0, 37);
            this.gridControlUsers.MainView = this.gridViewUsers;
            this.gridControlUsers.Name = "gridControlUsers";
            this.gridControlUsers.Size = new System.Drawing.Size(532, 312);
            this.gridControlUsers.TabIndex = 37;
            this.gridControlUsers.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewUsers});
            // 
            // gridViewUsers
            // 
            this.gridViewUsers.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridViewUsers.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridViewUsers.Appearance.Row.Options.UseTextOptions = true;
            this.gridViewUsers.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridViewUsers.GridControl = this.gridControlUsers;
            this.gridViewUsers.Name = "gridViewUsers";
            this.gridViewUsers.OptionsBehavior.Editable = false;
            this.gridViewUsers.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewUsers.OptionsView.ShowGroupPanel = false;
            // 
            // panelControlHeader
            // 
            this.panelControlHeader.AutoSize = true;
            this.panelControlHeader.Controls.Add(this.btnUserDel);
            this.panelControlHeader.Controls.Add(this.btnUserAdd);
            this.panelControlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControlHeader.Location = new System.Drawing.Point(0, 0);
            this.panelControlHeader.Name = "panelControlHeader";
            this.panelControlHeader.Size = new System.Drawing.Size(532, 37);
            this.panelControlHeader.TabIndex = 38;
            // 
            // btnUserDel
            // 
            this.btnUserDel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnUserDel.ImageOptions.Image")));
            this.btnUserDel.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnUserDel.Location = new System.Drawing.Point(37, 5);
            this.btnUserDel.Name = "btnUserDel";
            this.btnUserDel.Size = new System.Drawing.Size(25, 25);
            this.btnUserDel.TabIndex = 2;
            this.btnUserDel.Text = "+";
            this.btnUserDel.Click += new System.EventHandler(this.btnUserDel_Click);
            // 
            // btnUserAdd
            // 
            this.btnUserAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnUserAdd.ImageOptions.Image")));
            this.btnUserAdd.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnUserAdd.Location = new System.Drawing.Point(6, 5);
            this.btnUserAdd.Name = "btnUserAdd";
            this.btnUserAdd.Size = new System.Drawing.Size(25, 25);
            this.btnUserAdd.TabIndex = 0;
            this.btnUserAdd.Text = "+";
            this.btnUserAdd.Click += new System.EventHandler(this.btnUserAdd_Click);
            // 
            // xtraTabPageAccessRights
            // 
            this.xtraTabPageAccessRights.Controls.Add(this.layoutControl2);
            this.xtraTabPageAccessRights.Name = "xtraTabPageAccessRights";
            this.xtraTabPageAccessRights.Size = new System.Drawing.Size(532, 349);
            this.xtraTabPageAccessRights.Text = "Права";
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.propertyGridAccess);
            this.layoutControl2.Controls.Add(this.btnBanEverything);
            this.layoutControl2.Controls.Add(this.btnAllowAll);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(0, 0);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.layoutControlGroupAccess;
            this.layoutControl2.Size = new System.Drawing.Size(532, 349);
            this.layoutControl2.TabIndex = 2;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // propertyGridAccess
            // 
            this.propertyGridAccess.Cursor = System.Windows.Forms.Cursors.Hand;
            this.propertyGridAccess.Location = new System.Drawing.Point(12, 38);
            this.propertyGridAccess.Name = "propertyGridAccess";
            this.propertyGridAccess.Size = new System.Drawing.Size(508, 299);
            this.propertyGridAccess.TabIndex = 0;
            // 
            // btnBanEverything
            // 
            this.btnBanEverything.Location = new System.Drawing.Point(12, 12);
            this.btnBanEverything.Name = "btnBanEverything";
            this.btnBanEverything.Size = new System.Drawing.Size(251, 22);
            this.btnBanEverything.StyleController = this.layoutControl2;
            this.btnBanEverything.TabIndex = 4;
            this.btnBanEverything.Text = "Запретить все";
            this.btnBanEverything.Click += new System.EventHandler(this.btnBanEverything_Click);
            // 
            // btnAllowAll
            // 
            this.btnAllowAll.Location = new System.Drawing.Point(267, 12);
            this.btnAllowAll.Name = "btnAllowAll";
            this.btnAllowAll.Size = new System.Drawing.Size(253, 22);
            this.btnAllowAll.StyleController = this.layoutControl2;
            this.btnAllowAll.TabIndex = 5;
            this.btnAllowAll.Text = "Разрешить все";
            this.btnAllowAll.Click += new System.EventHandler(this.btnAllowAll_Click);
            // 
            // layoutControlGroupAccess
            // 
            this.layoutControlGroupAccess.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupAccess.GroupBordersVisible = false;
            this.layoutControlGroupAccess.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroupAccess.Name = "Root";
            this.layoutControlGroupAccess.Size = new System.Drawing.Size(532, 349);
            this.layoutControlGroupAccess.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnBanEverything;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(255, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnAllowAll;
            this.layoutControlItem3.Location = new System.Drawing.Point(255, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(257, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.propertyGridAccess;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(512, 303);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // panelControlFooter
            // 
            this.panelControlFooter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControlFooter.Controls.Add(this.btnSave);
            this.panelControlFooter.Controls.Add(this.btnCancel);
            this.panelControlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControlFooter.Location = new System.Drawing.Point(0, 470);
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
            // UserGroupEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 510);
            this.Controls.Add(this.xtraTabInfoUserGroup);
            this.Controls.Add(this.panelControlFooter);
            this.Controls.Add(this.panelControlGropUserHeader);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(550, 1000);
            this.MinimumSize = new System.Drawing.Size(550, 550);
            this.Name = "UserGroupEdit";
            this.Text = "Группа пользователей";
            this.Load += new System.EventHandler(this.ReportEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabInfoUserGroup)).EndInit();
            this.xtraTabInfoUserGroup.ResumeLayout(false);
            this.xtraTabPageUsers.ResumeLayout(false);
            this.xtraTabPageUsers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlHeader)).EndInit();
            this.panelControlHeader.ResumeLayout(false);
            this.xtraTabPageAccessRights.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridAccess)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupAccess)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
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
        private DevExpress.XtraTab.XtraTabControl xtraTabInfoUserGroup;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageUsers;
        private DevExpress.XtraEditors.PanelControl panelControlFooter;
        private DevExpress.XtraGrid.GridControl gridControlUsers;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewUsers;
        private DevExpress.XtraEditors.PanelControl panelControlHeader;
        private DevExpress.XtraEditors.SimpleButton btnUserDel;
        private DevExpress.XtraEditors.SimpleButton btnUserAdd;
        private DevExpress.XtraEditors.PanelControl panelControlGropUserHeader;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblName;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.MemoEdit memoDescription;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageAccessRights;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraVerticalGrid.PropertyGridControl propertyGridAccess;
        private DevExpress.XtraEditors.SimpleButton btnBanEverything;
        private DevExpress.XtraEditors.SimpleButton btnAllowAll;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupAccess;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    }
}