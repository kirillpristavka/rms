namespace RMS.UI.Forms.ReferenceBooks
{
    partial class TaskObjectEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskObjectEdit));
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlTaskObject = new DevExpress.XtraLayout.LayoutControl();
            this.checkedComboBoxEditTypeTasks = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.memoDescription = new DevExpress.XtraEditors.MemoEdit();
            this.checkIsUse = new DevExpress.XtraEditors.CheckEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemName = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItemDescription = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemIsUse = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemSave = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemCancel = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemTypeTasks = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlTaskObject)).BeginInit();
            this.layoutControlTaskObject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkedComboBoxEditTypeTasks.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsUse.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDescription)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemIsUse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTypeTasks)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(225, 126);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(146, 22);
            this.btnSave.StyleController = this.layoutControlTaskObject;
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // layoutControlTaskObject
            // 
            this.layoutControlTaskObject.Controls.Add(this.checkedComboBoxEditTypeTasks);
            this.layoutControlTaskObject.Controls.Add(this.btnCancel);
            this.layoutControlTaskObject.Controls.Add(this.btnSave);
            this.layoutControlTaskObject.Controls.Add(this.txtName);
            this.layoutControlTaskObject.Controls.Add(this.memoDescription);
            this.layoutControlTaskObject.Controls.Add(this.checkIsUse);
            this.layoutControlTaskObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlTaskObject.Location = new System.Drawing.Point(0, 0);
            this.layoutControlTaskObject.Name = "layoutControlTaskObject";
            this.layoutControlTaskObject.Root = this.Root;
            this.layoutControlTaskObject.Size = new System.Drawing.Size(534, 160);
            this.layoutControlTaskObject.TabIndex = 7;
            this.layoutControlTaskObject.Text = "layoutControl1";
            // 
            // checkedComboBoxEditTypeTasks
            // 
            this.checkedComboBoxEditTypeTasks.Location = new System.Drawing.Point(119, 76);
            this.checkedComboBoxEditTypeTasks.Name = "checkedComboBoxEditTypeTasks";
            this.checkedComboBoxEditTypeTasks.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.checkedComboBoxEditTypeTasks.Size = new System.Drawing.Size(403, 22);
            this.checkedComboBoxEditTypeTasks.StyleController = this.layoutControlTaskObject;
            this.checkedComboBoxEditTypeTasks.TabIndex = 8;
            this.checkedComboBoxEditTypeTasks.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.checkedComboBoxEditTypeTasks_ButtonPressed);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(375, 126);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(147, 22);
            this.btnCancel.StyleController = this.layoutControlTaskObject;
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(119, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(403, 22);
            this.txtName.StyleController = this.layoutControlTaskObject;
            this.txtName.TabIndex = 2;
            // 
            // memoDescription
            // 
            this.memoDescription.Location = new System.Drawing.Point(119, 38);
            this.memoDescription.Name = "memoDescription";
            this.memoDescription.Size = new System.Drawing.Size(403, 34);
            this.memoDescription.StyleController = this.layoutControlTaskObject;
            this.memoDescription.TabIndex = 4;
            // 
            // checkIsUse
            // 
            this.checkIsUse.Location = new System.Drawing.Point(12, 102);
            this.checkIsUse.Name = "checkIsUse";
            this.checkIsUse.Properties.Caption = "Использовать для формирования списка задач";
            this.checkIsUse.Size = new System.Drawing.Size(510, 20);
            this.checkIsUse.StyleController = this.layoutControlTaskObject;
            this.checkIsUse.TabIndex = 5;
            // 
            // Root
            // 
            this.Root.AppearanceItemCaption.Options.UseTextOptions = true;
            this.Root.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemName,
            this.emptySpaceItem,
            this.layoutControlItemDescription,
            this.layoutControlItemIsUse,
            this.layoutControlItemSave,
            this.layoutControlItemCancel,
            this.layoutControlItemTypeTasks});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(534, 160);
            this.Root.TextVisible = false;
            // 
            // layoutControlItemName
            // 
            this.layoutControlItemName.Control = this.txtName;
            this.layoutControlItemName.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemName.Name = "layoutControlItemName";
            this.layoutControlItemName.Size = new System.Drawing.Size(514, 26);
            this.layoutControlItemName.Text = "Наименование:";
            this.layoutControlItemName.TextSize = new System.Drawing.Size(104, 16);
            // 
            // emptySpaceItem
            // 
            this.emptySpaceItem.AllowHotTrack = false;
            this.emptySpaceItem.Location = new System.Drawing.Point(0, 114);
            this.emptySpaceItem.Name = "emptySpaceItem";
            this.emptySpaceItem.Size = new System.Drawing.Size(213, 26);
            this.emptySpaceItem.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItemDescription
            // 
            this.layoutControlItemDescription.Control = this.memoDescription;
            this.layoutControlItemDescription.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItemDescription.Name = "layoutControlItemDescription";
            this.layoutControlItemDescription.Size = new System.Drawing.Size(514, 38);
            this.layoutControlItemDescription.Text = "Описание:";
            this.layoutControlItemDescription.TextSize = new System.Drawing.Size(104, 16);
            // 
            // layoutControlItemIsUse
            // 
            this.layoutControlItemIsUse.Control = this.checkIsUse;
            this.layoutControlItemIsUse.Location = new System.Drawing.Point(0, 90);
            this.layoutControlItemIsUse.Name = "layoutControlItemIsUse";
            this.layoutControlItemIsUse.Size = new System.Drawing.Size(514, 24);
            this.layoutControlItemIsUse.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemIsUse.TextVisible = false;
            // 
            // layoutControlItemSave
            // 
            this.layoutControlItemSave.Control = this.btnSave;
            this.layoutControlItemSave.Location = new System.Drawing.Point(213, 114);
            this.layoutControlItemSave.MaxSize = new System.Drawing.Size(150, 26);
            this.layoutControlItemSave.MinSize = new System.Drawing.Size(150, 26);
            this.layoutControlItemSave.Name = "layoutControlItemSave";
            this.layoutControlItemSave.Size = new System.Drawing.Size(150, 26);
            this.layoutControlItemSave.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemSave.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemSave.TextVisible = false;
            // 
            // layoutControlItemCancel
            // 
            this.layoutControlItemCancel.Control = this.btnCancel;
            this.layoutControlItemCancel.Location = new System.Drawing.Point(363, 114);
            this.layoutControlItemCancel.MaxSize = new System.Drawing.Size(151, 26);
            this.layoutControlItemCancel.MinSize = new System.Drawing.Size(151, 26);
            this.layoutControlItemCancel.Name = "layoutControlItemCancel";
            this.layoutControlItemCancel.Size = new System.Drawing.Size(151, 26);
            this.layoutControlItemCancel.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemCancel.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemCancel.TextVisible = false;
            // 
            // layoutControlItemTypeTasks
            // 
            this.layoutControlItemTypeTasks.Control = this.checkedComboBoxEditTypeTasks;
            this.layoutControlItemTypeTasks.Location = new System.Drawing.Point(0, 64);
            this.layoutControlItemTypeTasks.Name = "layoutControlItemTypeTasks";
            this.layoutControlItemTypeTasks.Size = new System.Drawing.Size(514, 26);
            this.layoutControlItemTypeTasks.Text = "Типы задач:";
            this.layoutControlItemTypeTasks.TextSize = new System.Drawing.Size(104, 16);
            // 
            // TaskObjectEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 160);
            this.Controls.Add(this.layoutControlTaskObject);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TaskObjectEdit";
            this.Text = "Задача";
            this.Load += new System.EventHandler(this.PositionEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlTaskObject)).EndInit();
            this.layoutControlTaskObject.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkedComboBoxEditTypeTasks.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsUse.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDescription)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemIsUse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTypeTasks)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.MemoEdit memoDescription;
        private DevExpress.XtraLayout.LayoutControl layoutControlTaskObject;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.CheckEdit checkIsUse;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemName;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemDescription;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemIsUse;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCancel;
        private DevExpress.XtraEditors.CheckedComboBoxEdit checkedComboBoxEditTypeTasks;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemTypeTasks;
    }
}