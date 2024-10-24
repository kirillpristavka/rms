namespace RMS.UI
{
    partial class formLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formLogin));
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlLogin = new DevExpress.XtraLayout.LayoutControl();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.btnAuth = new DevExpress.XtraEditors.SimpleButton();
            this.imageComboUsers = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.btnSetting = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemLogin = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItemAuth = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItemPassword = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemAuth = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemExit = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItemSetting = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlLogin)).BeginInit();
            this.layoutControlLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageComboUsers.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemLogin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemAuth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAuth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSetting)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.AllowFocus = false;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(252, 98);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(5);
            this.btnCancel.MinimumSize = new System.Drawing.Size(125, 30);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(125, 30);
            this.btnCancel.StyleController = this.layoutControlLogin;
            this.btnCancel.TabIndex = 4;
            this.btnCancel.TabStop = false;
            this.btnCancel.Text = "Отмена";
            // 
            // layoutControlLogin
            // 
            this.layoutControlLogin.AllowCustomization = false;
            this.layoutControlLogin.Controls.Add(this.btnCancel);
            this.layoutControlLogin.Controls.Add(this.txtPassword);
            this.layoutControlLogin.Controls.Add(this.btnAuth);
            this.layoutControlLogin.Controls.Add(this.imageComboUsers);
            this.layoutControlLogin.Controls.Add(this.btnSetting);
            this.layoutControlLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlLogin.Location = new System.Drawing.Point(0, 0);
            this.layoutControlLogin.Name = "layoutControlLogin";
            this.layoutControlLogin.Root = this.Root;
            this.layoutControlLogin.Size = new System.Drawing.Size(384, 135);
            this.layoutControlLogin.TabIndex = 58;
            this.layoutControlLogin.Text = "layoutControl1";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(119, 33);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(5);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.Appearance.Options.UseTextOptions = true;
            this.txtPassword.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtPassword.Properties.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(258, 22);
            this.txtPassword.StyleController = this.layoutControlLogin;
            this.txtPassword.TabIndex = 0;
            // 
            // btnAuth
            // 
            this.btnAuth.AllowFocus = false;
            this.btnAuth.Location = new System.Drawing.Point(123, 98);
            this.btnAuth.Margin = new System.Windows.Forms.Padding(5);
            this.btnAuth.MinimumSize = new System.Drawing.Size(125, 30);
            this.btnAuth.Name = "btnAuth";
            this.btnAuth.Size = new System.Drawing.Size(125, 30);
            this.btnAuth.StyleController = this.layoutControlLogin;
            this.btnAuth.TabIndex = 3;
            this.btnAuth.TabStop = false;
            this.btnAuth.Text = "Авторизация";
            this.btnAuth.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // imageComboUsers
            // 
            this.imageComboUsers.Location = new System.Drawing.Point(119, 7);
            this.imageComboUsers.Margin = new System.Windows.Forms.Padding(5);
            this.imageComboUsers.Name = "imageComboUsers";
            this.imageComboUsers.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.imageComboUsers.Properties.DropDownRows = 20;
            this.imageComboUsers.Size = new System.Drawing.Size(258, 22);
            this.imageComboUsers.StyleController = this.layoutControlLogin;
            this.imageComboUsers.TabIndex = 1;
            // 
            // btnSetting
            // 
            this.btnSetting.AllowFocus = false;
            this.btnSetting.ImageOptions.Image = global::RMS.UI.Properties.Resources.properties_16x16;
            this.btnSetting.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnSetting.Location = new System.Drawing.Point(7, 98);
            this.btnSetting.Margin = new System.Windows.Forms.Padding(5);
            this.btnSetting.MaximumSize = new System.Drawing.Size(30, 30);
            this.btnSetting.MinimumSize = new System.Drawing.Size(30, 30);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(30, 30);
            this.btnSetting.StyleController = this.layoutControlLogin;
            this.btnSetting.TabIndex = 5;
            this.btnSetting.TabStop = false;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // Root
            // 
            this.Root.AppearanceItemCaption.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.Root.AppearanceItemCaption.Options.UseFont = true;
            this.Root.AppearanceItemCaption.Options.UseTextOptions = true;
            this.Root.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemLogin,
            this.emptySpaceItemAuth,
            this.layoutControlItemPassword,
            this.layoutControlItemAuth,
            this.layoutControlItemExit,
            this.emptySpaceItem,
            this.layoutControlItemSetting});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.Root.Size = new System.Drawing.Size(384, 135);
            this.Root.TextVisible = false;
            // 
            // layoutControlItemLogin
            // 
            this.layoutControlItemLogin.Control = this.imageComboUsers;
            this.layoutControlItemLogin.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemLogin.Name = "layoutControlItemLogin";
            this.layoutControlItemLogin.Size = new System.Drawing.Size(374, 26);
            this.layoutControlItemLogin.Text = "Пользователь";
            this.layoutControlItemLogin.TextSize = new System.Drawing.Size(108, 16);
            // 
            // emptySpaceItemAuth
            // 
            this.emptySpaceItemAuth.AllowHotTrack = false;
            this.emptySpaceItemAuth.Location = new System.Drawing.Point(0, 52);
            this.emptySpaceItemAuth.Name = "emptySpaceItemAuth";
            this.emptySpaceItemAuth.Size = new System.Drawing.Size(374, 39);
            this.emptySpaceItemAuth.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItemPassword
            // 
            this.layoutControlItemPassword.Control = this.txtPassword;
            this.layoutControlItemPassword.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItemPassword.Name = "layoutControlItemPassword";
            this.layoutControlItemPassword.Size = new System.Drawing.Size(374, 26);
            this.layoutControlItemPassword.Text = "Пароль";
            this.layoutControlItemPassword.TextSize = new System.Drawing.Size(108, 16);
            // 
            // layoutControlItemAuth
            // 
            this.layoutControlItemAuth.Control = this.btnAuth;
            this.layoutControlItemAuth.Location = new System.Drawing.Point(116, 91);
            this.layoutControlItemAuth.Name = "layoutControlItemAuth";
            this.layoutControlItemAuth.Size = new System.Drawing.Size(129, 34);
            this.layoutControlItemAuth.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemAuth.TextVisible = false;
            // 
            // layoutControlItemExit
            // 
            this.layoutControlItemExit.Control = this.btnCancel;
            this.layoutControlItemExit.Location = new System.Drawing.Point(245, 91);
            this.layoutControlItemExit.Name = "layoutControlItemExit";
            this.layoutControlItemExit.Size = new System.Drawing.Size(129, 34);
            this.layoutControlItemExit.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemExit.TextVisible = false;
            // 
            // emptySpaceItem
            // 
            this.emptySpaceItem.AllowHotTrack = false;
            this.emptySpaceItem.Location = new System.Drawing.Point(34, 91);
            this.emptySpaceItem.Name = "emptySpaceItem";
            this.emptySpaceItem.Size = new System.Drawing.Size(82, 34);
            this.emptySpaceItem.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItemSetting
            // 
            this.layoutControlItemSetting.Control = this.btnSetting;
            this.layoutControlItemSetting.Location = new System.Drawing.Point(0, 91);
            this.layoutControlItemSetting.Name = "layoutControlItemSetting";
            this.layoutControlItemSetting.Size = new System.Drawing.Size(34, 34);
            this.layoutControlItemSetting.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemSetting.TextVisible = false;
            // 
            // formLogin
            // 
            this.AcceptButton = this.btnAuth;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(384, 135);
            this.Controls.Add(this.layoutControlLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("formLogin.IconOptions.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 175);
            this.Name = "formLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Авторизация пользователя СКиД";
            this.Load += new System.EventHandler(this.formLogin_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.formLogin_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlLogin)).EndInit();
            this.layoutControlLogin.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageComboUsers.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemLogin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemAuth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAuth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSetting)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnAuth;
        private DevExpress.XtraEditors.ImageComboBoxEdit imageComboUsers;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraLayout.LayoutControl layoutControlLogin;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemLogin;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemAuth;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemPassword;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemAuth;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemExit;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem;
        private DevExpress.XtraEditors.SimpleButton btnSetting;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemSetting;
    }
}