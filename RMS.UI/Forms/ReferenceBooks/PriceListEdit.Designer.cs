namespace RMS.UI.Forms.ReferenceBooks
{
    partial class PriceListEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PriceListEdit));
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.txtPrice = new DevExpress.XtraEditors.CalcEdit();
            this.layoutControlPriceList = new DevExpress.XtraLayout.LayoutControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemCode = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItemSave = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItemName = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemDescription = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemPrice = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemSave = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemCancel = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItemCancel = new DevExpress.XtraLayout.EmptySpaceItem();
            this.memoDescription = new DevExpress.XtraEditors.MemoEdit();
            this.emptySpaceItemCode = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItemPrice = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlPriceList)).BeginInit();
            this.layoutControlPriceList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDescription)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemPrice)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(269, 181);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(127, 22);
            this.btnSave.StyleController = this.layoutControlPriceList;
            this.btnSave.TabIndex = 26;
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(400, 181);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(127, 22);
            this.btnCancel.StyleController = this.layoutControlPriceList;
            this.btnCancel.TabIndex = 25;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(125, 33);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(402, 22);
            this.txtName.StyleController = this.layoutControlPriceList;
            this.txtName.TabIndex = 29;
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(387, 7);
            this.txtCode.MaximumSize = new System.Drawing.Size(150, 0);
            this.txtCode.MinimumSize = new System.Drawing.Size(100, 0);
            this.txtCode.Name = "txtCode";
            this.txtCode.Properties.Appearance.Options.UseTextOptions = true;
            this.txtCode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtCode.Properties.Mask.EditMask = "d";
            this.txtCode.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtCode.Size = new System.Drawing.Size(140, 22);
            this.txtCode.StyleController = this.layoutControlPriceList;
            this.txtCode.TabIndex = 27;
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(387, 155);
            this.txtPrice.MaximumSize = new System.Drawing.Size(150, 0);
            this.txtPrice.MinimumSize = new System.Drawing.Size(100, 0);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtPrice.Properties.DisplayFormat.FormatString = "n2";
            this.txtPrice.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtPrice.Properties.EditFormat.FormatString = "n2";
            this.txtPrice.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtPrice.Properties.Mask.EditMask = "n2";
            this.txtPrice.Size = new System.Drawing.Size(140, 22);
            this.txtPrice.StyleController = this.layoutControlPriceList;
            this.txtPrice.TabIndex = 33;
            // 
            // layoutControlPriceList
            // 
            this.layoutControlPriceList.AllowCustomization = false;
            this.layoutControlPriceList.Controls.Add(this.btnCancel);
            this.layoutControlPriceList.Controls.Add(this.btnSave);
            this.layoutControlPriceList.Controls.Add(this.txtCode);
            this.layoutControlPriceList.Controls.Add(this.txtName);
            this.layoutControlPriceList.Controls.Add(this.txtPrice);
            this.layoutControlPriceList.Controls.Add(this.memoDescription);
            this.layoutControlPriceList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlPriceList.Location = new System.Drawing.Point(0, 0);
            this.layoutControlPriceList.Name = "layoutControlPriceList";
            this.layoutControlPriceList.Root = this.Root;
            this.layoutControlPriceList.Size = new System.Drawing.Size(534, 210);
            this.layoutControlPriceList.TabIndex = 35;
            this.layoutControlPriceList.Text = "layoutControl1";
            // 
            // Root
            // 
            this.Root.AppearanceItemCaption.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.Root.AppearanceItemCaption.Options.UseFont = true;
            this.Root.AppearanceItemCaption.Options.UseTextOptions = true;
            this.Root.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemCode,
            this.emptySpaceItemSave,
            this.layoutControlItemName,
            this.layoutControlItemDescription,
            this.layoutControlItemPrice,
            this.layoutControlItemSave,
            this.layoutControlItemCancel,
            this.emptySpaceItemCancel,
            this.emptySpaceItemCode,
            this.emptySpaceItemPrice});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.Root.Size = new System.Drawing.Size(534, 210);
            this.Root.TextVisible = false;
            // 
            // layoutControlItemCode
            // 
            this.layoutControlItemCode.Control = this.txtCode;
            this.layoutControlItemCode.Location = new System.Drawing.Point(262, 0);
            this.layoutControlItemCode.Name = "layoutControlItemCode";
            this.layoutControlItemCode.Size = new System.Drawing.Size(262, 26);
            this.layoutControlItemCode.Text = "Код:";
            this.layoutControlItemCode.TextSize = new System.Drawing.Size(114, 16);
            // 
            // emptySpaceItemSave
            // 
            this.emptySpaceItemSave.AllowHotTrack = false;
            this.emptySpaceItemSave.Location = new System.Drawing.Point(0, 174);
            this.emptySpaceItemSave.Name = "emptySpaceItemSave";
            this.emptySpaceItemSave.Size = new System.Drawing.Size(131, 26);
            this.emptySpaceItemSave.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItemName
            // 
            this.layoutControlItemName.Control = this.txtName;
            this.layoutControlItemName.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItemName.Name = "layoutControlItemName";
            this.layoutControlItemName.Size = new System.Drawing.Size(524, 26);
            this.layoutControlItemName.Text = "Наименование:";
            this.layoutControlItemName.TextSize = new System.Drawing.Size(114, 16);
            // 
            // layoutControlItemDescription
            // 
            this.layoutControlItemDescription.Control = this.memoDescription;
            this.layoutControlItemDescription.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItemDescription.Name = "layoutControlItemDescription";
            this.layoutControlItemDescription.Size = new System.Drawing.Size(524, 96);
            this.layoutControlItemDescription.Text = "Описание:";
            this.layoutControlItemDescription.TextSize = new System.Drawing.Size(114, 16);
            // 
            // layoutControlItemPrice
            // 
            this.layoutControlItemPrice.Control = this.txtPrice;
            this.layoutControlItemPrice.Location = new System.Drawing.Point(262, 148);
            this.layoutControlItemPrice.Name = "layoutControlItemPrice";
            this.layoutControlItemPrice.Size = new System.Drawing.Size(262, 26);
            this.layoutControlItemPrice.Text = "Цена:";
            this.layoutControlItemPrice.TextSize = new System.Drawing.Size(114, 16);
            // 
            // layoutControlItemSave
            // 
            this.layoutControlItemSave.Control = this.btnSave;
            this.layoutControlItemSave.Location = new System.Drawing.Point(262, 174);
            this.layoutControlItemSave.Name = "layoutControlItemSave";
            this.layoutControlItemSave.Size = new System.Drawing.Size(131, 26);
            this.layoutControlItemSave.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemSave.TextVisible = false;
            // 
            // layoutControlItemCancel
            // 
            this.layoutControlItemCancel.Control = this.btnCancel;
            this.layoutControlItemCancel.Location = new System.Drawing.Point(393, 174);
            this.layoutControlItemCancel.Name = "layoutControlItemCancel";
            this.layoutControlItemCancel.Size = new System.Drawing.Size(131, 26);
            this.layoutControlItemCancel.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemCancel.TextVisible = false;
            // 
            // emptySpaceItemCancel
            // 
            this.emptySpaceItemCancel.AllowHotTrack = false;
            this.emptySpaceItemCancel.Location = new System.Drawing.Point(131, 174);
            this.emptySpaceItemCancel.Name = "emptySpaceItemCancel";
            this.emptySpaceItemCancel.Size = new System.Drawing.Size(131, 26);
            this.emptySpaceItemCancel.TextSize = new System.Drawing.Size(0, 0);
            // 
            // memoDescription
            // 
            this.memoDescription.Location = new System.Drawing.Point(125, 59);
            this.memoDescription.Name = "memoDescription";
            this.memoDescription.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.memoDescription.Size = new System.Drawing.Size(402, 92);
            this.memoDescription.StyleController = this.layoutControlPriceList;
            this.memoDescription.TabIndex = 31;
            // 
            // emptySpaceItemCode
            // 
            this.emptySpaceItemCode.AllowHotTrack = false;
            this.emptySpaceItemCode.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItemCode.Name = "emptySpaceItemCode";
            this.emptySpaceItemCode.Size = new System.Drawing.Size(262, 26);
            this.emptySpaceItemCode.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItemPrice
            // 
            this.emptySpaceItemPrice.AllowHotTrack = false;
            this.emptySpaceItemPrice.Location = new System.Drawing.Point(0, 148);
            this.emptySpaceItemPrice.Name = "emptySpaceItemPrice";
            this.emptySpaceItemPrice.Size = new System.Drawing.Size(262, 26);
            this.emptySpaceItemPrice.TextSize = new System.Drawing.Size(0, 0);
            // 
            // PriceListEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 210);
            this.Controls.Add(this.layoutControlPriceList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(550, 250);
            this.Name = "PriceListEdit";
            this.Text = "Прайс";
            this.Load += new System.EventHandler(this.PositionEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlPriceList)).EndInit();
            this.layoutControlPriceList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDescription)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemPrice)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.CalcEdit txtPrice;
        private DevExpress.XtraLayout.LayoutControl layoutControlPriceList;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCode;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemDescription;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemPrice;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCancel;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemCancel;
        private DevExpress.XtraEditors.MemoEdit memoDescription;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemCode;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemPrice;
    }
}