namespace RMS.UI.Forms.ReferenceBooks
{
    partial class BasisEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BasisEdit));
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlTaskObject = new DevExpress.XtraLayout.LayoutControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnPayoutDictionary = new DevExpress.XtraEditors.ButtonEdit();
            this.spinRate = new DevExpress.XtraEditors.SpinEdit();
            this.dateSince = new DevExpress.XtraEditors.DateEdit();
            this.dateTo = new DevExpress.XtraEditors.DateEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItemSave = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItemSave = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemCancel = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItemCancel = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItemPayoutDictionary = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemDateSince = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemDateTo = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemRate = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItemRate = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItemFooter = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlTaskObject)).BeginInit();
            this.layoutControlTaskObject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPayoutDictionary.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinRate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateSince.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateSince.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPayoutDictionary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDateSince)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDateTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemFooter)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(294, 126);
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
            this.layoutControlTaskObject.Controls.Add(this.spinRate);
            this.layoutControlTaskObject.Controls.Add(this.dateSince);
            this.layoutControlTaskObject.Controls.Add(this.dateTo);
            this.layoutControlTaskObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlTaskObject.Location = new System.Drawing.Point(0, 0);
            this.layoutControlTaskObject.Name = "layoutControlTaskObject";
            this.layoutControlTaskObject.Root = this.Root;
            this.layoutControlTaskObject.Size = new System.Drawing.Size(584, 160);
            this.layoutControlTaskObject.TabIndex = 7;
            this.layoutControlTaskObject.Text = "layoutControl1";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(435, 126);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(137, 22);
            this.btnCancel.StyleController = this.layoutControlTaskObject;
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnPayoutDictionary
            // 
            this.btnPayoutDictionary.Location = new System.Drawing.Point(77, 12);
            this.btnPayoutDictionary.Name = "btnPayoutDictionary";
            this.btnPayoutDictionary.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnPayoutDictionary.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnPayoutDictionary.Size = new System.Drawing.Size(495, 22);
            this.btnPayoutDictionary.StyleController = this.layoutControlTaskObject;
            this.btnPayoutDictionary.TabIndex = 7;
            this.btnPayoutDictionary.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnPayoutDictionary_ButtonPressed);
            // 
            // spinRate
            // 
            this.spinRate.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinRate.Location = new System.Drawing.Point(359, 64);
            this.spinRate.Name = "spinRate";
            this.spinRate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinRate.Properties.DisplayFormat.FormatString = "n2";
            this.spinRate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinRate.Properties.EditFormat.FormatString = "n2";
            this.spinRate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinRate.Properties.Mask.EditMask = "n2";
            this.spinRate.Size = new System.Drawing.Size(213, 22);
            this.spinRate.StyleController = this.layoutControlTaskObject;
            this.spinRate.TabIndex = 9;
            // 
            // dateSince
            // 
            this.dateSince.EditValue = null;
            this.dateSince.Location = new System.Drawing.Point(77, 38);
            this.dateSince.Name = "dateSince";
            this.dateSince.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateSince.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateSince.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.dateSince.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dateSince.Size = new System.Drawing.Size(213, 22);
            this.dateSince.StyleController = this.layoutControlTaskObject;
            this.dateSince.TabIndex = 8;
            // 
            // dateTo
            // 
            this.dateTo.EditValue = null;
            this.dateTo.Location = new System.Drawing.Point(359, 38);
            this.dateTo.Name = "dateTo";
            this.dateTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.dateTo.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dateTo.Size = new System.Drawing.Size(213, 22);
            this.dateTo.StyleController = this.layoutControlTaskObject;
            this.dateTo.TabIndex = 8;
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
            this.layoutControlItemDateSince,
            this.layoutControlItemDateTo,
            this.layoutControlItemRate,
            this.emptySpaceItemRate,
            this.emptySpaceItemFooter});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(584, 160);
            this.Root.TextVisible = false;
            // 
            // emptySpaceItemSave
            // 
            this.emptySpaceItemSave.AllowHotTrack = false;
            this.emptySpaceItemSave.Location = new System.Drawing.Point(0, 114);
            this.emptySpaceItemSave.Name = "emptySpaceItemSave";
            this.emptySpaceItemSave.Size = new System.Drawing.Size(141, 26);
            this.emptySpaceItemSave.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItemSave
            // 
            this.layoutControlItemSave.Control = this.btnSave;
            this.layoutControlItemSave.Location = new System.Drawing.Point(282, 114);
            this.layoutControlItemSave.Name = "layoutControlItemSave";
            this.layoutControlItemSave.Size = new System.Drawing.Size(141, 26);
            this.layoutControlItemSave.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemSave.TextVisible = false;
            // 
            // layoutControlItemCancel
            // 
            this.layoutControlItemCancel.Control = this.btnCancel;
            this.layoutControlItemCancel.Location = new System.Drawing.Point(423, 114);
            this.layoutControlItemCancel.Name = "layoutControlItemCancel";
            this.layoutControlItemCancel.Size = new System.Drawing.Size(141, 26);
            this.layoutControlItemCancel.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemCancel.TextVisible = false;
            // 
            // emptySpaceItemCancel
            // 
            this.emptySpaceItemCancel.AllowHotTrack = false;
            this.emptySpaceItemCancel.Location = new System.Drawing.Point(141, 114);
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
            this.layoutControlItemPayoutDictionary.TextSize = new System.Drawing.Size(62, 16);
            // 
            // layoutControlItemDateSince
            // 
            this.layoutControlItemDateSince.Control = this.dateSince;
            this.layoutControlItemDateSince.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItemDateSince.Name = "layoutControlItemDateSince";
            this.layoutControlItemDateSince.Size = new System.Drawing.Size(282, 26);
            this.layoutControlItemDateSince.Text = "С:";
            this.layoutControlItemDateSince.TextSize = new System.Drawing.Size(62, 16);
            // 
            // layoutControlItemDateTo
            // 
            this.layoutControlItemDateTo.Control = this.dateTo;
            this.layoutControlItemDateTo.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItemDateTo.Location = new System.Drawing.Point(282, 26);
            this.layoutControlItemDateTo.Name = "layoutControlItemDateTo";
            this.layoutControlItemDateTo.Size = new System.Drawing.Size(282, 26);
            this.layoutControlItemDateTo.Text = "по:";
            this.layoutControlItemDateTo.TextSize = new System.Drawing.Size(62, 16);
            // 
            // layoutControlItemRate
            // 
            this.layoutControlItemRate.Control = this.spinRate;
            this.layoutControlItemRate.Location = new System.Drawing.Point(282, 52);
            this.layoutControlItemRate.Name = "layoutControlItemRate";
            this.layoutControlItemRate.Size = new System.Drawing.Size(282, 26);
            this.layoutControlItemRate.Text = "Ставка:";
            this.layoutControlItemRate.TextSize = new System.Drawing.Size(62, 16);
            // 
            // emptySpaceItemRate
            // 
            this.emptySpaceItemRate.AllowHotTrack = false;
            this.emptySpaceItemRate.Location = new System.Drawing.Point(0, 52);
            this.emptySpaceItemRate.Name = "emptySpaceItemRate";
            this.emptySpaceItemRate.Size = new System.Drawing.Size(282, 26);
            this.emptySpaceItemRate.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItemFooter
            // 
            this.emptySpaceItemFooter.AllowHotTrack = false;
            this.emptySpaceItemFooter.Location = new System.Drawing.Point(0, 78);
            this.emptySpaceItemFooter.Name = "emptySpaceItemFooter";
            this.emptySpaceItemFooter.Size = new System.Drawing.Size(564, 36);
            this.emptySpaceItemFooter.TextSize = new System.Drawing.Size(0, 0);
            // 
            // BasisEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 160);
            this.Controls.Add(this.layoutControlTaskObject);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BasisEdit";
            this.Text = "Основание";
            this.Load += new System.EventHandler(this.PositionEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlTaskObject)).EndInit();
            this.layoutControlTaskObject.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnPayoutDictionary.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinRate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateSince.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateSince.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPayoutDictionary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDateSince)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDateTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemRate)).EndInit();
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
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemDateSince;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemPayoutDictionary;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemDateTo;
        private DevExpress.XtraEditors.SpinEdit spinRate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemRate;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemRate;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemFooter;
        private DevExpress.XtraEditors.DateEdit dateSince;
        private DevExpress.XtraEditors.DateEdit dateTo;
    }
}