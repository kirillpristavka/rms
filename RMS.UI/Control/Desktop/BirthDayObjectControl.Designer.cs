
namespace RMS.UI.Control.Desktop
{
    partial class BirthDayObjectControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BirthDayObjectControl));
            this.lblCustomer = new DevExpress.XtraEditors.LabelControl();
            this.lblDescription = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit = new DevExpress.XtraEditors.PictureEdit();
            this.btnCongratulate = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCustomer
            // 
            this.lblCustomer.Appearance.Font = new System.Drawing.Font("Bahnschrift Light", 9.75F);
            this.lblCustomer.Appearance.Options.UseFont = true;
            this.lblCustomer.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblCustomer.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCustomer.Location = new System.Drawing.Point(45, 5);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Padding = new System.Windows.Forms.Padding(1);
            this.lblCustomer.Size = new System.Drawing.Size(260, 16);
            this.lblCustomer.TabIndex = 1;
            this.lblCustomer.Text = "lblCustomer";
            this.lblCustomer.Click += new System.EventHandler(this.lblCustomer_Click);
            this.lblCustomer.MouseEnter += new System.EventHandler(this.lblTask_MouseEnter);
            this.lblCustomer.MouseLeave += new System.EventHandler(this.lblTask_MouseLeave);
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDescription.Location = new System.Drawing.Point(45, 21);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Padding = new System.Windows.Forms.Padding(1);
            this.lblDescription.Size = new System.Drawing.Size(260, 24);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "lblDescription";
            this.lblDescription.Click += new System.EventHandler(this.lblCustomer_Click);
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
            // btnCongratulate
            // 
            this.btnCongratulate.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCongratulate.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.btnCongratulate.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.btnCongratulate.Location = new System.Drawing.Point(305, 5);
            this.btnCongratulate.Margin = new System.Windows.Forms.Padding(10);
            this.btnCongratulate.Name = "btnCongratulate";
            this.btnCongratulate.Size = new System.Drawing.Size(40, 40);
            this.btnCongratulate.TabIndex = 4;
            this.btnCongratulate.ToolTip = "Отправить письмо с поздравлением";
            this.btnCongratulate.Click += new System.EventHandler(this.btnCongratulate_Click);
            // 
            // BirthDayObjectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblCustomer);
            this.Controls.Add(this.pictureEdit);
            this.Controls.Add(this.btnCongratulate);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(350, 50);
            this.Name = "BirthDayObjectControl";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(350, 50);
            this.Load += new System.EventHandler(this.TaskControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.LabelControl lblCustomer;
        private DevExpress.XtraEditors.LabelControl lblDescription;
        private DevExpress.XtraEditors.PictureEdit pictureEdit;
        private DevExpress.XtraEditors.SimpleButton btnCongratulate;
    }
}
