
namespace RMS.UI.Control.Desktop
{
    partial class LoggerObjectControl
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblUser = new DevExpress.XtraEditors.LabelControl();
            this.lblDescription = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblUser
            // 
            this.lblUser.Appearance.Font = new System.Drawing.Font("Bahnschrift Light", 9.75F);
            this.lblUser.Appearance.Options.UseFont = true;
            this.lblUser.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblUser.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblUser.Location = new System.Drawing.Point(45, 5);
            this.lblUser.Name = "lblUser";
            this.lblUser.Padding = new System.Windows.Forms.Padding(1);
            this.lblUser.Size = new System.Drawing.Size(300, 16);
            this.lblUser.TabIndex = 1;
            this.lblUser.Text = "lblUser";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDescription.Location = new System.Drawing.Point(45, 21);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Padding = new System.Windows.Forms.Padding(1);
            this.lblDescription.Size = new System.Drawing.Size(300, 24);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "lblDescription";
            this.lblDescription.MouseEnter += new System.EventHandler(this.lblTask_MouseEnter);
            this.lblDescription.MouseLeave += new System.EventHandler(this.lblTask_MouseLeave);
            // 
            // pictureEdit
            // 
            this.pictureEdit.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureEdit.Location = new System.Drawing.Point(5, 5);
            this.pictureEdit.Name = "pictureEdit";
            this.pictureEdit.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit.Size = new System.Drawing.Size(40, 40);
            this.pictureEdit.TabIndex = 3;
            // 
            // LoggerObjectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.pictureEdit);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(350, 50);
            this.Name = "LoggerObjectControl";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(350, 50);
            this.Load += new System.EventHandler(this.TaskControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.LabelControl lblUser;
        private DevExpress.XtraEditors.LabelControl lblDescription;
        private DevExpress.XtraEditors.PictureEdit pictureEdit;
    }
}
