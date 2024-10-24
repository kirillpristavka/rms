
namespace RMS.UI.Control.Desktop
{
    partial class TaskControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskControl));
            this.lblTask = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlTask = new DevExpress.XtraLayout.LayoutControl();
            this.lblTaskCount = new DevExpress.XtraEditors.LabelControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutItemTask = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemTaskCount = new DevExpress.XtraLayout.LayoutControlItem();
            this.eLayoutItemTaskCount = new DevExpress.XtraLayout.EmptySpaceItem();
            this.simpleSeparator1 = new DevExpress.XtraLayout.SimpleSeparator();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlTaskObject = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlTask)).BeginInit();
            this.layoutControlTask.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemTask)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemTaskCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eLayoutItemTaskCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlTaskObject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTask
            // 
            this.lblTask.Appearance.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTask.Appearance.Options.UseFont = true;
            this.lblTask.Appearance.Options.UseTextOptions = true;
            this.lblTask.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblTask.Location = new System.Drawing.Point(46, 12);
            this.lblTask.Name = "lblTask";
            this.lblTask.Size = new System.Drawing.Size(78, 25);
            this.lblTask.StyleController = this.layoutControlTask;
            this.lblTask.TabIndex = 0;
            this.lblTask.Text = "Задачи";
            // 
            // layoutControlTask
            // 
            this.layoutControlTask.Controls.Add(this.layoutControlTaskObject);
            this.layoutControlTask.Controls.Add(this.lblTaskCount);
            this.layoutControlTask.Controls.Add(this.lblTask);
            this.layoutControlTask.Controls.Add(this.btnClear);
            this.layoutControlTask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlTask.Location = new System.Drawing.Point(0, 0);
            this.layoutControlTask.Name = "layoutControlTask";
            this.layoutControlTask.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(479, 225, 650, 400);
            this.layoutControlTask.Root = this.Root;
            this.layoutControlTask.Size = new System.Drawing.Size(350, 150);
            this.layoutControlTask.TabIndex = 2;
            this.layoutControlTask.Text = "layoutControl1";
            // 
            // lblTaskCount
            // 
            this.lblTaskCount.Appearance.Font = new System.Drawing.Font("Book Antiqua", 9.75F);
            this.lblTaskCount.Appearance.Options.UseFont = true;
            this.lblTaskCount.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTaskCount.Location = new System.Drawing.Point(128, 12);
            this.lblTaskCount.Name = "lblTaskCount";
            this.lblTaskCount.Size = new System.Drawing.Size(210, 17);
            this.lblTaskCount.StyleController = this.layoutControlTask;
            this.lblTaskCount.TabIndex = 1;
            this.lblTaskCount.Text = "999+";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutItemTask,
            this.layoutItemTaskCount,
            this.eLayoutItemTaskCount,
            this.simpleSeparator1,
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(350, 150);
            this.Root.TextVisible = false;
            // 
            // layoutItemTask
            // 
            this.layoutItemTask.Control = this.lblTask;
            this.layoutItemTask.Location = new System.Drawing.Point(34, 0);
            this.layoutItemTask.Name = "layoutItemTask";
            this.layoutItemTask.Size = new System.Drawing.Size(82, 34);
            this.layoutItemTask.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.SupportHorzAlignment;
            this.layoutItemTask.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutItemTask.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItemTask.TextToControlDistance = 0;
            this.layoutItemTask.TextVisible = false;
            // 
            // layoutItemTaskCount
            // 
            this.layoutItemTaskCount.Control = this.lblTaskCount;
            this.layoutItemTaskCount.Location = new System.Drawing.Point(116, 0);
            this.layoutItemTaskCount.Name = "layoutItemTaskCount";
            this.layoutItemTaskCount.Size = new System.Drawing.Size(214, 21);
            this.layoutItemTaskCount.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutItemTaskCount.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItemTaskCount.TextToControlDistance = 0;
            this.layoutItemTaskCount.TextVisible = false;
            // 
            // eLayoutItemTaskCount
            // 
            this.eLayoutItemTaskCount.AllowHotTrack = false;
            this.eLayoutItemTaskCount.Location = new System.Drawing.Point(116, 21);
            this.eLayoutItemTaskCount.Name = "eLayoutItemTaskCount";
            this.eLayoutItemTaskCount.Size = new System.Drawing.Size(214, 13);
            this.eLayoutItemTaskCount.TextSize = new System.Drawing.Size(0, 0);
            // 
            // simpleSeparator1
            // 
            this.simpleSeparator1.AllowHotTrack = false;
            this.simpleSeparator1.Location = new System.Drawing.Point(0, 34);
            this.simpleSeparator1.Name = "simpleSeparator1";
            this.simpleSeparator1.Size = new System.Drawing.Size(330, 1);
            // 
            // btnClear
            // 
            this.btnClear.AutoWidthInLayoutControl = true;
            this.btnClear.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.ImageOptions.Image")));
            this.btnClear.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
            this.btnClear.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnClear.Location = new System.Drawing.Point(12, 12);
            this.btnClear.MaximumSize = new System.Drawing.Size(30, 30);
            this.btnClear.MinimumSize = new System.Drawing.Size(30, 30);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(30, 30);
            this.btnClear.StyleController = this.layoutControlTask;
            this.btnClear.TabIndex = 4;
            this.btnClear.ToolTip = "После нажатия, выполненные задачи НЕ будут отображены в списке";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnClear;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(34, 34);
            this.layoutControlItem1.Text = "Hide";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlTaskObject
            // 
            this.layoutControlTaskObject.Location = new System.Drawing.Point(12, 47);
            this.layoutControlTaskObject.Name = "layoutControlTaskObject";
            this.layoutControlTaskObject.Root = this.layoutControlGroup1;
            this.layoutControlTaskObject.Size = new System.Drawing.Size(326, 91);
            this.layoutControlTaskObject.TabIndex = 5;
            this.layoutControlTaskObject.Text = "layoutControl1";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.layoutControlTaskObject;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 35);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(330, 95);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(326, 91);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // TaskControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControlTask);
            this.MinimumSize = new System.Drawing.Size(350, 150);
            this.Name = "TaskControl";
            this.Size = new System.Drawing.Size(350, 150);
            this.Load += new System.EventHandler(this.TaskControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlTask)).EndInit();
            this.layoutControlTask.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemTask)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemTaskCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eLayoutItemTaskCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlTaskObject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblTask;
        private DevExpress.XtraEditors.LabelControl lblTaskCount;
        private DevExpress.XtraLayout.LayoutControl layoutControlTask;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemTask;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemTaskCount;
        private DevExpress.XtraLayout.EmptySpaceItem eLayoutItemTaskCount;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator1;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControl layoutControlTaskObject;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}
