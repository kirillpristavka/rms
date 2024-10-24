namespace RMS.UI.Forms.Directories
{
    partial class ForeignEconomicActivityEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ForeignEconomicActivityEdit));
            this.txtDescription = new DevExpress.XtraEditors.MemoEdit();
            this.dateSince = new DevExpress.XtraEditors.DateEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.txtAct = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtWhereToOrFrom = new DevExpress.XtraEditors.MemoEdit();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemDateSince = new DevExpress.XtraLayout.LayoutControlItem();
            this.dateTo = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlItemDateTo = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemWhereToOrFrom = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemAct = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemDescription = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItemSave = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemCancel = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateSince.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateSince.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAct.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWhereToOrFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDateSince)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDateTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemWhereToOrFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDescription)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCancel)).BeginInit();
            this.SuspendLayout();
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(132, 127);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(310, 95);
            this.txtDescription.StyleController = this.layoutControl1;
            this.txtDescription.TabIndex = 35;
            // 
            // dateSince
            // 
            this.dateSince.EditValue = null;
            this.dateSince.Location = new System.Drawing.Point(69, 12);
            this.dateSince.MaximumSize = new System.Drawing.Size(150, 0);
            this.dateSince.MinimumSize = new System.Drawing.Size(150, 0);
            this.dateSince.Name = "dateSince";
            this.dateSince.Properties.Appearance.Options.UseTextOptions = true;
            this.dateSince.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dateSince.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateSince.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateSince.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.dateSince.Size = new System.Drawing.Size(150, 22);
            this.dateSince.StyleController = this.layoutControl1;
            this.dateSince.TabIndex = 37;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(229, 226);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(104, 22);
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.TabIndex = 59;
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(337, 226);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(105, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 58;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtAct
            // 
            this.txtAct.Location = new System.Drawing.Point(132, 38);
            this.txtAct.Name = "txtAct";
            this.txtAct.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtAct.Properties.DropDownRows = 2;
            this.txtAct.Properties.Items.AddRange(new object[] {
            "Экспорт",
            "Импорт"});
            this.txtAct.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.txtAct.Size = new System.Drawing.Size(310, 22);
            this.txtAct.StyleController = this.layoutControl1;
            this.txtAct.TabIndex = 60;
            // 
            // txtWhereToOrFrom
            // 
            this.txtWhereToOrFrom.Location = new System.Drawing.Point(132, 64);
            this.txtWhereToOrFrom.Name = "txtWhereToOrFrom";
            this.txtWhereToOrFrom.Size = new System.Drawing.Size(310, 59);
            this.txtWhereToOrFrom.StyleController = this.layoutControl1;
            this.txtWhereToOrFrom.TabIndex = 63;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.txtWhereToOrFrom);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.dateSince);
            this.layoutControl1.Controls.Add(this.dateTo);
            this.layoutControl1.Controls.Add(this.txtDescription);
            this.layoutControl1.Controls.Add(this.txtAct);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(454, 260);
            this.layoutControl1.TabIndex = 64;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // Root
            // 
            this.Root.AppearanceItemCaption.Options.UseTextOptions = true;
            this.Root.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemDateSince,
            this.layoutControlItemDateTo,
            this.layoutControlItemWhereToOrFrom,
            this.layoutControlItemAct,
            this.layoutControlItemDescription,
            this.emptySpaceItem1,
            this.emptySpaceItem2,
            this.layoutControlItemSave,
            this.layoutControlItemCancel});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(454, 260);
            this.Root.TextVisible = false;
            // 
            // layoutControlItemDateSince
            // 
            this.layoutControlItemDateSince.Control = this.dateSince;
            this.layoutControlItemDateSince.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemDateSince.Name = "layoutControlItemDateSince";
            this.layoutControlItemDateSince.Size = new System.Drawing.Size(211, 26);
            this.layoutControlItemDateSince.Text = "Дата с:";
            this.layoutControlItemDateSince.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItemDateSince.TextSize = new System.Drawing.Size(52, 16);
            this.layoutControlItemDateSince.TextToControlDistance = 5;
            // 
            // dateTo
            // 
            this.dateTo.EditValue = null;
            this.dateTo.Location = new System.Drawing.Point(287, 12);
            this.dateTo.MaximumSize = new System.Drawing.Size(150, 0);
            this.dateTo.MinimumSize = new System.Drawing.Size(150, 0);
            this.dateTo.Name = "dateTo";
            this.dateTo.Properties.Appearance.Options.UseTextOptions = true;
            this.dateTo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dateTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.dateTo.Size = new System.Drawing.Size(150, 22);
            this.dateTo.StyleController = this.layoutControl1;
            this.dateTo.TabIndex = 37;
            // 
            // layoutControlItemDateTo
            // 
            this.layoutControlItemDateTo.Control = this.dateTo;
            this.layoutControlItemDateTo.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItemDateTo.Location = new System.Drawing.Point(211, 0);
            this.layoutControlItemDateTo.Name = "layoutControlItemDateTo";
            this.layoutControlItemDateTo.Size = new System.Drawing.Size(223, 26);
            this.layoutControlItemDateTo.Text = "Дата по:";
            this.layoutControlItemDateTo.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItemDateTo.TextSize = new System.Drawing.Size(60, 16);
            this.layoutControlItemDateTo.TextToControlDistance = 4;
            // 
            // layoutControlItemWhereToOrFrom
            // 
            this.layoutControlItemWhereToOrFrom.Control = this.txtWhereToOrFrom;
            this.layoutControlItemWhereToOrFrom.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItemWhereToOrFrom.Name = "layoutControlItemWhereToOrFrom";
            this.layoutControlItemWhereToOrFrom.Size = new System.Drawing.Size(434, 63);
            this.layoutControlItemWhereToOrFrom.Text = "Куда/откуда:";
            this.layoutControlItemWhereToOrFrom.TextSize = new System.Drawing.Size(116, 16);
            // 
            // layoutControlItemAct
            // 
            this.layoutControlItemAct.Control = this.txtAct;
            this.layoutControlItemAct.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItemAct.Name = "layoutControlItemAct";
            this.layoutControlItemAct.Size = new System.Drawing.Size(434, 26);
            this.layoutControlItemAct.Text = "Экспорт/импорт:";
            this.layoutControlItemAct.TextSize = new System.Drawing.Size(116, 16);
            // 
            // layoutControlItemDescription
            // 
            this.layoutControlItemDescription.Control = this.txtDescription;
            this.layoutControlItemDescription.Location = new System.Drawing.Point(0, 115);
            this.layoutControlItemDescription.Name = "layoutControlItemDescription";
            this.layoutControlItemDescription.Size = new System.Drawing.Size(434, 99);
            this.layoutControlItemDescription.Text = "Описание:";
            this.layoutControlItemDescription.TextSize = new System.Drawing.Size(116, 16);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 214);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(108, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(108, 214);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(109, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItemSave
            // 
            this.layoutControlItemSave.Control = this.btnSave;
            this.layoutControlItemSave.Location = new System.Drawing.Point(217, 214);
            this.layoutControlItemSave.Name = "layoutControlItemSave";
            this.layoutControlItemSave.Size = new System.Drawing.Size(108, 26);
            this.layoutControlItemSave.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemSave.TextVisible = false;
            // 
            // layoutControlItemCancel
            // 
            this.layoutControlItemCancel.Control = this.btnCancel;
            this.layoutControlItemCancel.Location = new System.Drawing.Point(325, 214);
            this.layoutControlItemCancel.Name = "layoutControlItemCancel";
            this.layoutControlItemCancel.Size = new System.Drawing.Size(109, 26);
            this.layoutControlItemCancel.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemCancel.TextVisible = false;
            // 
            // ForeignEconomicActivityEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 260);
            this.Controls.Add(this.layoutControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(470, 300);
            this.Name = "ForeignEconomicActivityEdit";
            this.Text = "Внешнеэкономическая деятельность";
            this.Load += new System.EventHandler(this.TaskEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateSince.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateSince.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAct.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWhereToOrFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDateSince)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDateTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemWhereToOrFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDescription)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCancel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.MemoEdit txtDescription;
        private DevExpress.XtraEditors.DateEdit dateSince;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.ComboBoxEdit txtAct;
        private DevExpress.XtraEditors.MemoEdit txtWhereToOrFrom;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemDateSince;
        private DevExpress.XtraEditors.DateEdit dateTo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemDateTo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemWhereToOrFrom;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemAct;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemDescription;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCancel;
    }
}