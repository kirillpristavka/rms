
namespace RMS.UI.Control.Desktop
{
    partial class ControlSystemObjectControl
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
            this.lblTask = new DevExpress.XtraEditors.LabelControl();
            this.lblDescription = new DevExpress.XtraEditors.LabelControl();
            this.SuspendLayout();
            // 
            // lblTask
            // 
            this.lblTask.Appearance.Font = new System.Drawing.Font("Bahnschrift Light", 9.75F);
            this.lblTask.Appearance.Options.UseFont = true;
            this.lblTask.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTask.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTask.Location = new System.Drawing.Point(5, 5);
            this.lblTask.Name = "lblTask";
            this.lblTask.Padding = new System.Windows.Forms.Padding(1);
            this.lblTask.Size = new System.Drawing.Size(340, 16);
            this.lblTask.TabIndex = 1;
            this.lblTask.Text = "lblCustomer";
            this.lblTask.Click += new System.EventHandler(this.lblCustomer_Click);
            this.lblTask.MouseEnter += new System.EventHandler(this.lblTask_MouseEnter);
            this.lblTask.MouseLeave += new System.EventHandler(this.lblTask_MouseLeave);
            // 
            // lblDescription
            // 
            this.lblDescription.Appearance.Options.UseTextOptions = true;
            this.lblDescription.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.lblDescription.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDescription.Location = new System.Drawing.Point(5, 21);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Padding = new System.Windows.Forms.Padding(1);
            this.lblDescription.Size = new System.Drawing.Size(340, 14);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "lblDescription";
            this.lblDescription.Click += new System.EventHandler(this.lblCustomer_Click);
            this.lblDescription.MouseEnter += new System.EventHandler(this.lblTask_MouseEnter);
            this.lblDescription.MouseLeave += new System.EventHandler(this.lblTask_MouseLeave);
            // 
            // TaskNowObjectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblTask);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(350, 40);
            this.Name = "TaskNowObjectControl";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(350, 40);
            this.Load += new System.EventHandler(this.TaskControl_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.LabelControl lblTask;
        private DevExpress.XtraEditors.LabelControl lblDescription;
    }
}
