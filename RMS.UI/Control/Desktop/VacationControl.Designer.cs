
namespace RMS.UI.Control.Desktop
{
    partial class VacationControl
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
            this.layoutControlLogger = new DevExpress.XtraLayout.LayoutControl();
            this.lblLogger = new DevExpress.XtraEditors.LabelControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutItemLogger = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleSeparator = new DevExpress.XtraLayout.SimpleSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlLogger)).BeginInit();
            this.layoutControlLogger.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemLogger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlLogger
            // 
            this.layoutControlLogger.Controls.Add(this.lblLogger);
            this.layoutControlLogger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlLogger.Location = new System.Drawing.Point(0, 0);
            this.layoutControlLogger.Name = "layoutControlLogger";
            this.layoutControlLogger.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(479, 225, 650, 400);
            this.layoutControlLogger.Root = this.Root;
            this.layoutControlLogger.Size = new System.Drawing.Size(350, 150);
            this.layoutControlLogger.TabIndex = 2;
            this.layoutControlLogger.Text = "layoutControlLogger";
            // 
            // lblLogger
            // 
            this.lblLogger.Appearance.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblLogger.Appearance.Options.UseFont = true;
            this.lblLogger.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblLogger.Location = new System.Drawing.Point(12, 12);
            this.lblLogger.Name = "lblLogger";
            this.lblLogger.Size = new System.Drawing.Size(326, 25);
            this.lblLogger.StyleController = this.layoutControlLogger;
            this.lblLogger.TabIndex = 0;
            this.lblLogger.Text = "Отпуска сотрудников";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutItemLogger,
            this.simpleSeparator});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(350, 150);
            this.Root.TextVisible = false;
            // 
            // layoutItemLogger
            // 
            this.layoutItemLogger.Control = this.lblLogger;
            this.layoutItemLogger.Location = new System.Drawing.Point(0, 0);
            this.layoutItemLogger.Name = "layoutItemLogger";
            this.layoutItemLogger.Size = new System.Drawing.Size(330, 29);
            this.layoutItemLogger.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutItemLogger.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItemLogger.TextToControlDistance = 0;
            this.layoutItemLogger.TextVisible = false;
            // 
            // simpleSeparator
            // 
            this.simpleSeparator.AllowHotTrack = false;
            this.simpleSeparator.Location = new System.Drawing.Point(0, 29);
            this.simpleSeparator.Name = "simpleSeparator";
            this.simpleSeparator.Size = new System.Drawing.Size(330, 101);
            // 
            // VacationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControlLogger);
            this.MinimumSize = new System.Drawing.Size(350, 100);
            this.Name = "VacationControl";
            this.Size = new System.Drawing.Size(350, 150);
            this.Load += new System.EventHandler(this.Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlLogger)).EndInit();
            this.layoutControlLogger.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemLogger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraLayout.LayoutControl layoutControlLogger;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.LabelControl lblLogger;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemLogger;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator;
    }
}
