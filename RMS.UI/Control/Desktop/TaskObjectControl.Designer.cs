
namespace RMS.UI.Control.Desktop
{
    partial class TaskObjectControl
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
            this.lblTaskStatus = new DevExpress.XtraEditors.LabelControl();
            this.SuspendLayout();
            // 
            // lblTask
            // 
            this.lblTask.Appearance.Options.UseTextOptions = true;
            this.lblTask.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblTask.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblTask.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTask.Location = new System.Drawing.Point(87, 2);
            this.lblTask.Name = "lblTask";
            this.lblTask.Size = new System.Drawing.Size(11, 31);
            this.lblTask.TabIndex = 0;
            this.lblTask.Text = "Test #1\r\nTest #2\r\nTest #3";
            this.lblTask.Click += new System.EventHandler(this.lblTask_Click);
            this.lblTask.MouseEnter += new System.EventHandler(this.lblTask_MouseEnter);
            this.lblTask.MouseLeave += new System.EventHandler(this.lblTask_MouseLeave);
            // 
            // lblTaskStatus
            // 
            this.lblTaskStatus.Appearance.Options.UseTextOptions = true;
            this.lblTaskStatus.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblTaskStatus.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTaskStatus.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTaskStatus.Location = new System.Drawing.Point(2, 2);
            this.lblTaskStatus.Name = "lblTaskStatus";
            this.lblTaskStatus.Size = new System.Drawing.Size(85, 31);
            this.lblTaskStatus.TabIndex = 1;
            this.lblTaskStatus.Text = "1";
            this.lblTaskStatus.Click += new System.EventHandler(this.lblTask_Click);
            this.lblTaskStatus.MouseEnter += new System.EventHandler(this.lblTask_MouseEnter);
            this.lblTaskStatus.MouseLeave += new System.EventHandler(this.lblTask_MouseLeave);
            // 
            // TaskObjectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.lblTask);
            this.Controls.Add(this.lblTaskStatus);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(100, 35);
            this.Name = "TaskObjectControl";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.Size = new System.Drawing.Size(100, 35);
            this.Load += new System.EventHandler(this.TaskControl_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblTask;
        private DevExpress.XtraEditors.LabelControl lblTaskStatus;
    }
}
