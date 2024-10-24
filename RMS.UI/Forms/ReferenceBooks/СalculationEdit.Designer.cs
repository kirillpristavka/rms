namespace RMS.UI.Forms.ReferenceBooks
{
    partial class СalculationEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(СalculationEdit));
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlTaskObject = new DevExpress.XtraLayout.LayoutControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnPayoutDictionary = new DevExpress.XtraEditors.ButtonEdit();
            this.spinValue = new DevExpress.XtraEditors.SpinEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItemSave = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItemSave = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemCancel = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItemCancel = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItemPayoutDictionary = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemValue = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItemRate = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItemFooter = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlTaskObject)).BeginInit();
            this.layoutControlTaskObject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPayoutDictionary.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPayoutDictionary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemFooter)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(294, 97);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(137, 22);
            this.btnSave.StyleController = this.layoutControlTaskObject;
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // layoutControlTaskObject
            // 
            this.layoutControlTaskObject.Controls.Add(this.btnCancel);
            this.layoutControlTaskObject.Controls.Add(this.btnSave);
            this.layoutControlTaskObject.Controls.Add(this.btnPayoutDictionary);
            this.layoutControlTaskObject.Controls.Add(this.spinValue);
            this.layoutControlTaskObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlTaskObject.Location = new System.Drawing.Point(0, 0);
            this.layoutControlTaskObject.Name = "layoutControlTaskObject";
            this.layoutControlTaskObject.Root = this.Root;
            this.layoutControlTaskObject.Size = new System.Drawing.Size(584, 131);
            this.layoutControlTaskObject.TabIndex = 7;
            this.layoutControlTaskObject.Text = "layoutControl1";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(435, 97);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(137, 22);
            this.btnCancel.StyleController = this.layoutControlTaskObject;
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnPayoutDictionary
            // 
            this.btnPayoutDictionary.Location = new System.Drawing.Point(85, 12);
            this.btnPayoutDictionary.Name = "btnPayoutDictionary";
            this.btnPayoutDictionary.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnPayoutDictionary.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnPayoutDictionary.Size = new System.Drawing.Size(487, 22);
            this.btnPayoutDictionary.StyleController = this.layoutControlTaskObject;
            this.btnPayoutDictionary.TabIndex = 7;
            this.btnPayoutDictionary.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnPayoutDictionary_ButtonPressed);
            // 
            // spinValue
            // 
            this.spinValue.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinValue.Location = new System.Drawing.Point(367, 38);
            this.spinValue.Name = "spinValue";
            this.spinValue.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinValue.Properties.DisplayFormat.FormatString = "n2";
            this.spinValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinValue.Properties.EditFormat.FormatString = "n2";
            this.spinValue.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinValue.Properties.Mask.EditMask = "n2";
            this.spinValue.Size = new System.Drawing.Size(205, 22);
            this.spinValue.StyleController = this.layoutControlTaskObject;
            this.spinValue.TabIndex = 9;
            // 
            // Root
            // 
            this.Root.AppearanceItemCaption.Options.UseTextOptions = true;
            this.Root.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItemSave,
            this.layoutControlItemSave,
            this.layoutControlItemCancel,
            this.emptySpaceItemCancel,
            this.layoutControlItemPayoutDictionary,
            this.layoutControlItemValue,
            this.emptySpaceItemRate,
            this.emptySpaceItemFooter});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(584, 131);
            this.Root.TextVisible = false;
            // 
            // emptySpaceItemSave
            // 
            this.emptySpaceItemSave.AllowHotTrack = false;
            this.emptySpaceItemSave.Location = new System.Drawing.Point(0, 85);
            this.emptySpaceItemSave.Name = "emptySpaceItemSave";
            this.emptySpaceItemSave.Size = new System.Drawing.Size(141, 26);
            this.emptySpaceItemSave.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItemSave
            // 
            this.layoutControlItemSave.Control = this.btnSave;
            this.layoutControlItemSave.Location = new System.Drawing.Point(282, 85);
            this.layoutControlItemSave.Name = "layoutControlItemSave";
            this.layoutControlItemSave.Size = new System.Drawing.Size(141, 26);
            this.layoutControlItemSave.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemSave.TextVisible = false;
            // 
            // layoutControlItemCancel
            // 
            this.layoutControlItemCancel.Control = this.btnCancel;
            this.layoutControlItemCancel.Location = new System.Drawing.Point(423, 85);
            this.layoutControlItemCancel.Name = "layoutControlItemCancel";
            this.layoutControlItemCancel.Size = new System.Drawing.Size(141, 26);
            this.layoutControlItemCancel.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemCancel.TextVisible = false;
            // 
            // emptySpaceItemCancel
            // 
            this.emptySpaceItemCancel.AllowHotTrack = false;
            this.emptySpaceItemCancel.Location = new System.Drawing.Point(141, 85);
            this.emptySpaceItemCancel.Name = "emptySpaceItemCancel";
            this.emptySpaceItemCancel.Size = new System.Drawing.Size(141, 26);
            this.emptySpaceItemCancel.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItemPayoutDictionary
            // 
            this.layoutControlItemPayoutDictionary.Control = this.btnPayoutDictionary;
            this.layoutControlItemPayoutDictionary.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemPayoutDictionary.Name = "layoutControlItemPayoutDictionary";
            this.layoutControlItemPayoutDictionary.Size = new System.Drawing.Size(564, 26);
            this.layoutControlItemPayoutDictionary.Text = "Выплата:";
            this.layoutControlItemPayoutDictionary.TextSize = new System.Drawing.Size(70, 16);
            // 
            // layoutControlItemValue
            // 
            this.layoutControlItemValue.Control = this.spinValue;
            this.layoutControlItemValue.Location = new System.Drawing.Point(282, 26);
            this.layoutControlItemValue.Name = "layoutControlItemValue";
            this.layoutControlItemValue.Size = new System.Drawing.Size(282, 26);
            this.layoutControlItemValue.Text = "Значение:";
            this.layoutControlItemValue.TextSize = new System.Drawing.Size(70, 16);
            // 
            // emptySpaceItemRate
            // 
            this.emptySpaceItemRate.AllowHotTrack = false;
            this.emptySpaceItemRate.Location = new System.Drawing.Point(0, 26);
            this.emptySpaceItemRate.Name = "emptySpaceItemRate";
            this.emptySpaceItemRate.Size = new System.Drawing.Size(282, 26);
            this.emptySpaceItemRate.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItemFooter
            // 
            this.emptySpaceItemFooter.AllowHotTrack = false;
            this.emptySpaceItemFooter.Location = new System.Drawing.Point(0, 52);
            this.emptySpaceItemFooter.Name = "emptySpaceItemFooter";
            this.emptySpaceItemFooter.Size = new System.Drawing.Size(564, 33);
            this.emptySpaceItemFooter.TextSize = new System.Drawing.Size(0, 0);
            // 
            // СalculationEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 131);
            this.Controls.Add(this.layoutControlTaskObject);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "СalculationEdit";
            this.Text = "Основание";
            this.Load += new System.EventHandler(this.PositionEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlTaskObject)).EndInit();
            this.layoutControlTaskObject.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnPayoutDictionary.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPayoutDictionary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemFooter)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControl layoutControlTaskObject;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCancel;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemCancel;
        private DevExpress.XtraEditors.ButtonEdit btnPayoutDictionary;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemPayoutDictionary;
        private DevExpress.XtraEditors.SpinEdit spinValue;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemValue;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemRate;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemFooter;
    }
}