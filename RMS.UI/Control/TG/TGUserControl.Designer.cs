
namespace RMS.UI.Control.TG
{
    partial class TGUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.layoutControlVacation = new DevExpress.XtraLayout.LayoutControl();
            this.pictureEdit = new DevExpress.XtraEditors.PictureEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemPictureEdit = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblUserName = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblMessage = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblCustomer = new DevExpress.XtraLayout.SimpleLabelItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlVacation)).BeginInit();
            this.layoutControlVacation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPictureEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblUserName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCustomer)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlVacation
            // 
            this.layoutControlVacation.AllowCustomization = false;
            this.layoutControlVacation.AutoSize = true;
            this.layoutControlVacation.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.layoutControlVacation.Controls.Add(this.pictureEdit);
            this.layoutControlVacation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlVacation.Location = new System.Drawing.Point(0, 0);
            this.layoutControlVacation.Margin = new System.Windows.Forms.Padding(0);
            this.layoutControlVacation.MinimumSize = new System.Drawing.Size(188, 79);
            this.layoutControlVacation.Name = "layoutControlVacation";
            this.layoutControlVacation.OptionsCustomizationForm.ShowLayoutTreeView = false;
            this.layoutControlVacation.OptionsCustomizationForm.ShowLoadButton = false;
            this.layoutControlVacation.OptionsCustomizationForm.ShowRedoButton = false;
            this.layoutControlVacation.OptionsCustomizationForm.ShowSaveButton = false;
            this.layoutControlVacation.OptionsCustomizationForm.ShowUndoButton = false;
            this.layoutControlVacation.OptionsFocus.AllowFocusGroups = false;
            this.layoutControlVacation.OptionsFocus.AllowFocusReadonlyEditors = false;
            this.layoutControlVacation.OptionsFocus.AllowFocusTabbedGroups = false;
            this.layoutControlVacation.Root = this.Root;
            this.layoutControlVacation.Size = new System.Drawing.Size(289, 83);
            this.layoutControlVacation.TabIndex = 40;
            this.layoutControlVacation.Text = "layoutControl1";
            // 
            // pictureEdit
            // 
            this.pictureEdit.EditValue = global::RMS.UI.Properties.Resources.employee_32x32;
            this.pictureEdit.Location = new System.Drawing.Point(2, 2);
            this.pictureEdit.Margin = new System.Windows.Forms.Padding(0);
            this.pictureEdit.MaximumSize = new System.Drawing.Size(88, 79);
            this.pictureEdit.MinimumSize = new System.Drawing.Size(88, 79);
            this.pictureEdit.Name = "pictureEdit";
            this.pictureEdit.Properties.AllowFocused = false;
            this.pictureEdit.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.False;
            this.pictureEdit.Properties.AllowScrollOnMouseWheel = DevExpress.Utils.DefaultBoolean.False;
            this.pictureEdit.Properties.AllowZoomOnMouseWheel = DevExpress.Utils.DefaultBoolean.False;
            this.pictureEdit.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.pictureEdit.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit.Properties.ErrorImageOptions.Image = global::RMS.UI.Properties.Resources.removepivotfield_32x32;
            this.pictureEdit.Properties.OptionsMask.MaskType = DevExpress.XtraEditors.Controls.PictureEditMaskType.Circle;
            this.pictureEdit.Properties.Padding = new System.Windows.Forms.Padding(10);
            this.pictureEdit.Properties.ShowMenu = false;
            this.pictureEdit.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureEdit.Properties.ZoomPercent = 80D;
            this.pictureEdit.Size = new System.Drawing.Size(88, 79);
            this.pictureEdit.StyleController = this.layoutControlVacation;
            this.pictureEdit.TabIndex = 4;
            this.pictureEdit.Click += new System.EventHandler(this.TGUserControl_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemPictureEdit,
            this.lblUserName,
            this.lblMessage,
            this.lblCustomer});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(289, 83);
            this.Root.TextVisible = false;
            // 
            // layoutControlItemPictureEdit
            // 
            this.layoutControlItemPictureEdit.Control = this.pictureEdit;
            this.layoutControlItemPictureEdit.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemPictureEdit.Name = "layoutControlItemPictureEdit";
            this.layoutControlItemPictureEdit.Size = new System.Drawing.Size(92, 83);
            this.layoutControlItemPictureEdit.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemPictureEdit.TextVisible = false;
            // 
            // lblUserName
            // 
            this.lblUserName.AllowHotTrack = false;
            this.lblUserName.AppearanceItemCaption.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblUserName.AppearanceItemCaption.Options.UseFont = true;
            this.lblUserName.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lblUserName.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.lblUserName.Location = new System.Drawing.Point(92, 0);
            this.lblUserName.MinSize = new System.Drawing.Size(1, 28);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 6, 0);
            this.lblUserName.Size = new System.Drawing.Size(197, 29);
            this.lblUserName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblUserName.Text = "UserName";
            this.lblUserName.TextSize = new System.Drawing.Size(92, 18);
            this.lblUserName.Click += new System.EventHandler(this.TGUserControl_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.AllowHotTrack = false;
            this.lblMessage.CustomizationFormText = "Message";
            this.lblMessage.Location = new System.Drawing.Point(92, 54);
            this.lblMessage.MinSize = new System.Drawing.Size(197, 28);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 6);
            this.lblMessage.Size = new System.Drawing.Size(197, 29);
            this.lblMessage.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblMessage.Text = "Message";
            this.lblMessage.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lblMessage.TextSize = new System.Drawing.Size(192, 18);
            this.lblMessage.Click += new System.EventHandler(this.TGUserControl_Click);
            // 
            // lblCustomer
            // 
            this.lblCustomer.AllowHotTrack = false;
            this.lblCustomer.AppearanceItemCaption.ForeColor = System.Drawing.Color.DarkGray;
            this.lblCustomer.AppearanceItemCaption.Options.UseForeColor = true;
            this.lblCustomer.CustomizationFormText = "Customer";
            this.lblCustomer.Location = new System.Drawing.Point(92, 29);
            this.lblCustomer.MinSize = new System.Drawing.Size(1, 22);
            this.lblCustomer.Name = "lblCistomer";
            this.lblCustomer.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblCustomer.Size = new System.Drawing.Size(197, 25);
            this.lblCustomer.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblCustomer.Text = "Customer";
            this.lblCustomer.TextSize = new System.Drawing.Size(92, 18);
            this.lblCustomer.Click += new System.EventHandler(this.TGUserControl_Click);
            // 
            // TGUserControl
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.layoutControlVacation);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MinimumSize = new System.Drawing.Size(188, 79);
            this.Name = "TGUserControl";
            this.Size = new System.Drawing.Size(289, 83);
            this.Load += new System.EventHandler(this.TGUserControl_Load);
            this.Click += new System.EventHandler(this.TGUserControl_Click);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlVacation)).EndInit();
            this.layoutControlVacation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPictureEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblUserName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCustomer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControlVacation;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.PictureEdit pictureEdit;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemPictureEdit;
        private DevExpress.XtraLayout.SimpleLabelItem lblUserName;
        private DevExpress.XtraLayout.SimpleLabelItem lblMessage;
        private DevExpress.XtraLayout.SimpleLabelItem lblCustomer;
    }
}
