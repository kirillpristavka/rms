
namespace RMS.UI.Control.Desktop
{
    partial class NotepadControl
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
            this.layoutControlNotepad = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControlNotepadObject = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblName = new DevExpress.XtraEditors.LabelControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutItemName = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleSeparator = new DevExpress.XtraLayout.SimpleSeparator();
            this.layoutControlItemObject = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlNotepad)).BeginInit();
            this.layoutControlNotepad.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlNotepadObject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemObject)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlNotepad
            // 
            this.layoutControlNotepad.Controls.Add(this.layoutControlNotepadObject);
            this.layoutControlNotepad.Controls.Add(this.lblName);
            this.layoutControlNotepad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlNotepad.Location = new System.Drawing.Point(0, 0);
            this.layoutControlNotepad.Name = "layoutControlNotepad";
            this.layoutControlNotepad.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(479, 225, 650, 400);
            this.layoutControlNotepad.Root = this.Root;
            this.layoutControlNotepad.Size = new System.Drawing.Size(350, 150);
            this.layoutControlNotepad.TabIndex = 2;
            this.layoutControlNotepad.Text = "layoutControl1";
            // 
            // layoutControlNotepadObject
            // 
            this.layoutControlNotepadObject.Location = new System.Drawing.Point(12, 42);
            this.layoutControlNotepadObject.Name = "layoutControlNotepadObject";
            this.layoutControlNotepadObject.Root = this.layoutControlGroup1;
            this.layoutControlNotepadObject.Size = new System.Drawing.Size(326, 96);
            this.layoutControlNotepadObject.TabIndex = 5;
            this.layoutControlNotepadObject.Text = "layoutControl1";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(326, 96);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lblName
            // 
            this.lblName.Appearance.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblName.Appearance.Options.UseFont = true;
            this.lblName.Appearance.Options.UseTextOptions = true;
            this.lblName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblName.Location = new System.Drawing.Point(12, 12);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(217, 25);
            this.lblName.StyleController = this.layoutControlNotepad;
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Блокнот бухгалтера";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutItemName,
            this.simpleSeparator,
            this.layoutControlItemObject});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(350, 150);
            this.Root.TextVisible = false;
            // 
            // layoutItemName
            // 
            this.layoutItemName.Control = this.lblName;
            this.layoutItemName.Location = new System.Drawing.Point(0, 0);
            this.layoutItemName.Name = "layoutItemName";
            this.layoutItemName.Size = new System.Drawing.Size(330, 29);
            this.layoutItemName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.SupportHorzAlignment;
            this.layoutItemName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutItemName.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItemName.TextToControlDistance = 0;
            this.layoutItemName.TextVisible = false;
            // 
            // simpleSeparator
            // 
            this.simpleSeparator.AllowHotTrack = false;
            this.simpleSeparator.Location = new System.Drawing.Point(0, 29);
            this.simpleSeparator.Name = "simpleSeparator";
            this.simpleSeparator.Size = new System.Drawing.Size(330, 1);
            // 
            // layoutControlItemObject
            // 
            this.layoutControlItemObject.Control = this.layoutControlNotepadObject;
            this.layoutControlItemObject.Location = new System.Drawing.Point(0, 30);
            this.layoutControlItemObject.Name = "layoutControlItemObject";
            this.layoutControlItemObject.Size = new System.Drawing.Size(330, 100);
            this.layoutControlItemObject.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemObject.TextVisible = false;
            // 
            // NotepadControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControlNotepad);
            this.MinimumSize = new System.Drawing.Size(350, 150);
            this.Name = "NotepadControl";
            this.Size = new System.Drawing.Size(350, 150);
            this.Load += new System.EventHandler(this.TaskControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlNotepad)).EndInit();
            this.layoutControlNotepad.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlNotepadObject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemObject)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraLayout.LayoutControl layoutControlNotepad;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator;
        private DevExpress.XtraLayout.LayoutControl layoutControlNotepadObject;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemObject;
        private DevExpress.XtraEditors.LabelControl lblName;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemName;
    }
}
