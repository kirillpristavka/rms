
namespace RMS.UI.Control.Desktop
{
    partial class BlockControl<T1, T2>
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
            this.lblName = new DevExpress.XtraEditors.LabelControl();
            this.lblCount = new DevExpress.XtraEditors.LabelControl();
            this.panelControl = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).BeginInit();
            this.panelControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.Appearance.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblName.Appearance.Options.UseFont = true;
            this.lblName.Appearance.Options.UseTextOptions = true;
            this.lblName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblName.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblName.Location = new System.Drawing.Point(2, 2);
            this.lblName.MaximumSize = new System.Drawing.Size(0, 55);
            this.lblName.MinimumSize = new System.Drawing.Size(0, 55);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(186, 55);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "NameObj";
            this.lblName.Click += new System.EventHandler(this.lblName_Click);
            this.lblName.MouseEnter += new System.EventHandler(this.LabelMouseEnter);
            this.lblName.MouseLeave += new System.EventHandler(this.LabelMouseLeave);
            // 
            // lblCount
            // 
            this.lblCount.Appearance.Font = new System.Drawing.Font("Yu Gothic Light", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCount.Appearance.Options.UseFont = true;
            this.lblCount.Appearance.Options.UseTextOptions = true;
            this.lblCount.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblCount.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCount.Location = new System.Drawing.Point(2, 57);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(186, 31);
            this.lblCount.TabIndex = 1;
            this.lblCount.Text = "CountObj";
            this.lblCount.Click += new System.EventHandler(this.lblCount_Click);
            this.lblCount.MouseEnter += new System.EventHandler(this.LabelMouseEnter);
            this.lblCount.MouseLeave += new System.EventHandler(this.LabelMouseLeave);
            // 
            // panelControl
            // 
            this.panelControl.Controls.Add(this.lblCount);
            this.panelControl.Controls.Add(this.lblName);
            this.panelControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl.Location = new System.Drawing.Point(5, 5);
            this.panelControl.Margin = new System.Windows.Forms.Padding(10);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(190, 90);
            this.panelControl.TabIndex = 2;
            this.panelControl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelControl_MouseClick);
            // 
            // BlockControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl);
            this.Margin = new System.Windows.Forms.Padding(15);
            this.MaximumSize = new System.Drawing.Size(150, 100);
            this.MinimumSize = new System.Drawing.Size(150, 100);
            this.Name = "BlockControl";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(200, 100);
            this.Load += new System.EventHandler(this.BlockControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).EndInit();
            this.panelControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblName;
        private DevExpress.XtraEditors.LabelControl lblCount;
        private DevExpress.XtraEditors.PanelControl panelControl;
    }
}
