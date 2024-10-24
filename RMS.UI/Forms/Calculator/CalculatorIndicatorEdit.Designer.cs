namespace RMS.UI.Forms.Calculator
{
    partial class CalculatorIndicatorEdit
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CalculatorIndicatorEdit));
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlCalculatorIndicator = new DevExpress.XtraLayout.LayoutControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.memoDescription = new DevExpress.XtraEditors.MemoEdit();
            this.txtValue = new DevExpress.XtraEditors.TextEdit();
            this.cmbTypeCalculatorIndicator = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.checkIsUseWhenForming = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemName = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItemDescription = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemTypeCalculatorIndicator = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemValue = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemSave = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemCancel = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemIsUseWhenForming = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem = new DevExpress.XtraLayout.EmptySpaceItem();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlCalculatorIndicator)).BeginInit();
            this.layoutControlCalculatorIndicator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTypeCalculatorIndicator.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsUseWhenForming.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDescription)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTypeCalculatorIndicator)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemIsUseWhenForming)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(269, 176);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(124, 22);
            this.btnSave.StyleController = this.layoutControlCalculatorIndicator;
            this.btnSave.TabIndex = 59;
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // layoutControlCalculatorIndicator
            // 
            this.layoutControlCalculatorIndicator.AllowCustomization = false;
            this.layoutControlCalculatorIndicator.Controls.Add(this.btnCancel);
            this.layoutControlCalculatorIndicator.Controls.Add(this.btnSave);
            this.layoutControlCalculatorIndicator.Controls.Add(this.memoDescription);
            this.layoutControlCalculatorIndicator.Controls.Add(this.txtValue);
            this.layoutControlCalculatorIndicator.Controls.Add(this.cmbTypeCalculatorIndicator);
            this.layoutControlCalculatorIndicator.Controls.Add(this.txtName);
            this.layoutControlCalculatorIndicator.Controls.Add(this.checkIsUseWhenForming);
            this.layoutControlCalculatorIndicator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlCalculatorIndicator.Location = new System.Drawing.Point(0, 0);
            this.layoutControlCalculatorIndicator.Name = "layoutControlCalculatorIndicator";
            this.layoutControlCalculatorIndicator.Root = this.layoutControlGroup1;
            this.layoutControlCalculatorIndicator.Size = new System.Drawing.Size(534, 210);
            this.layoutControlCalculatorIndicator.TabIndex = 81;
            this.layoutControlCalculatorIndicator.Text = "layoutControl1";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(397, 176);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(125, 22);
            this.btnCancel.StyleController = this.layoutControlCalculatorIndicator;
            this.btnCancel.TabIndex = 58;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // memoDescription
            // 
            this.memoDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.memoDescription.Location = new System.Drawing.Point(129, 38);
            this.memoDescription.Name = "memoDescription";
            this.memoDescription.Size = new System.Drawing.Size(393, 58);
            this.memoDescription.StyleController = this.layoutControlCalculatorIndicator;
            this.memoDescription.TabIndex = 79;
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(129, 126);
            this.txtValue.Name = "txtValue";
            this.txtValue.Properties.DisplayFormat.FormatString = "n";
            this.txtValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtValue.Properties.EditFormat.FormatString = "n";
            this.txtValue.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtValue.Properties.Mask.EditMask = "n";
            this.txtValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtValue.Size = new System.Drawing.Size(393, 22);
            this.txtValue.StyleController = this.layoutControlCalculatorIndicator;
            this.txtValue.TabIndex = 6;
            // 
            // cmbTypeCalculatorIndicator
            // 
            this.cmbTypeCalculatorIndicator.Location = new System.Drawing.Point(129, 100);
            this.cmbTypeCalculatorIndicator.Name = "cmbTypeCalculatorIndicator";
            this.cmbTypeCalculatorIndicator.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbTypeCalculatorIndicator.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbTypeCalculatorIndicator.Size = new System.Drawing.Size(393, 22);
            this.cmbTypeCalculatorIndicator.StyleController = this.layoutControlCalculatorIndicator;
            this.cmbTypeCalculatorIndicator.TabIndex = 5;
            this.cmbTypeCalculatorIndicator.SelectedIndexChanged += new System.EventHandler(this.cmbTypePerformanceIndicator_SelectedIndexChanged);
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(129, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(393, 22);
            this.txtName.StyleController = this.layoutControlCalculatorIndicator;
            this.txtName.TabIndex = 77;
            // 
            // checkIsUseWhenForming
            // 
            this.checkIsUseWhenForming.Location = new System.Drawing.Point(269, 152);
            this.checkIsUseWhenForming.Name = "checkIsUseWhenForming";
            this.checkIsUseWhenForming.Properties.Caption = "Использовать при расчете";
            this.checkIsUseWhenForming.Size = new System.Drawing.Size(253, 20);
            this.checkIsUseWhenForming.StyleController = this.layoutControlCalculatorIndicator;
            this.checkIsUseWhenForming.TabIndex = 8;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.AppearanceItemCaption.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.layoutControlGroup1.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlGroup1.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlGroup1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemName,
            this.emptySpaceItem2,
            this.layoutControlItemDescription,
            this.layoutControlItemTypeCalculatorIndicator,
            this.layoutControlItemValue,
            this.layoutControlItemSave,
            this.layoutControlItemCancel,
            this.layoutControlItemIsUseWhenForming,
            this.emptySpaceItem1,
            this.emptySpaceItem});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(534, 210);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItemName
            // 
            this.layoutControlItemName.Control = this.txtName;
            this.layoutControlItemName.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemName.Name = "layoutControlItemName";
            this.layoutControlItemName.Size = new System.Drawing.Size(514, 26);
            this.layoutControlItemName.Text = "Наименование:";
            this.layoutControlItemName.TextSize = new System.Drawing.Size(114, 16);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 164);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(128, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItemDescription
            // 
            this.layoutControlItemDescription.Control = this.memoDescription;
            this.layoutControlItemDescription.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItemDescription.Name = "layoutControlItemDescription";
            this.layoutControlItemDescription.Size = new System.Drawing.Size(514, 62);
            this.layoutControlItemDescription.Text = "Описание:";
            this.layoutControlItemDescription.TextSize = new System.Drawing.Size(114, 16);
            // 
            // layoutControlItemTypeCalculatorIndicator
            // 
            this.layoutControlItemTypeCalculatorIndicator.Control = this.cmbTypeCalculatorIndicator;
            this.layoutControlItemTypeCalculatorIndicator.Location = new System.Drawing.Point(0, 88);
            this.layoutControlItemTypeCalculatorIndicator.Name = "layoutControlItemTypeCalculatorIndicator";
            this.layoutControlItemTypeCalculatorIndicator.Size = new System.Drawing.Size(514, 26);
            this.layoutControlItemTypeCalculatorIndicator.Text = "Тип записи:";
            this.layoutControlItemTypeCalculatorIndicator.TextSize = new System.Drawing.Size(114, 16);
            // 
            // layoutControlItemValue
            // 
            this.layoutControlItemValue.Control = this.txtValue;
            this.layoutControlItemValue.Location = new System.Drawing.Point(0, 114);
            this.layoutControlItemValue.Name = "layoutControlItemValue";
            this.layoutControlItemValue.Size = new System.Drawing.Size(514, 26);
            this.layoutControlItemValue.Text = "Цена:";
            this.layoutControlItemValue.TextSize = new System.Drawing.Size(114, 16);
            // 
            // layoutControlItemSave
            // 
            this.layoutControlItemSave.Control = this.btnSave;
            this.layoutControlItemSave.Location = new System.Drawing.Point(257, 164);
            this.layoutControlItemSave.Name = "layoutControlItemSave";
            this.layoutControlItemSave.Size = new System.Drawing.Size(128, 26);
            this.layoutControlItemSave.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemSave.TextVisible = false;
            // 
            // layoutControlItemCancel
            // 
            this.layoutControlItemCancel.Control = this.btnCancel;
            this.layoutControlItemCancel.Location = new System.Drawing.Point(385, 164);
            this.layoutControlItemCancel.Name = "layoutControlItemCancel";
            this.layoutControlItemCancel.Size = new System.Drawing.Size(129, 26);
            this.layoutControlItemCancel.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemCancel.TextVisible = false;
            // 
            // layoutControlItemIsUseWhenForming
            // 
            this.layoutControlItemIsUseWhenForming.Control = this.checkIsUseWhenForming;
            this.layoutControlItemIsUseWhenForming.Location = new System.Drawing.Point(257, 140);
            this.layoutControlItemIsUseWhenForming.Name = "layoutControlItemIsUseWhenForming";
            this.layoutControlItemIsUseWhenForming.Size = new System.Drawing.Size(257, 24);
            this.layoutControlItemIsUseWhenForming.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemIsUseWhenForming.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(128, 164);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(129, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem
            // 
            this.emptySpaceItem.AllowHotTrack = false;
            this.emptySpaceItem.Location = new System.Drawing.Point(0, 140);
            this.emptySpaceItem.Name = "emptySpaceItem";
            this.emptySpaceItem.Size = new System.Drawing.Size(257, 24);
            this.emptySpaceItem.TextSize = new System.Drawing.Size(0, 0);
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(856, 289);
            // 
            // CalculatorIndicatorEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 210);
            this.Controls.Add(this.layoutControlCalculatorIndicator);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(550, 250);
            this.Name = "CalculatorIndicatorEdit";
            this.Text = "Показатели для работы калькулятора";
            this.Load += new System.EventHandler(this.FormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlCalculatorIndicator)).EndInit();
            this.layoutControlCalculatorIndicator.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTypeCalculatorIndicator.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsUseWhenForming.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDescription)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTypeCalculatorIndicator)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemIsUseWhenForming)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.MemoEdit memoDescription;
        private DevExpress.XtraEditors.ComboBoxEdit cmbTypeCalculatorIndicator;
        private DevExpress.XtraEditors.TextEdit txtValue;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraEditors.CheckEdit checkIsUseWhenForming;
        private DevExpress.XtraLayout.LayoutControl layoutControlCalculatorIndicator;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemName;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemDescription;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemTypeCalculatorIndicator;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemValue;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemIsUseWhenForming;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem;
    }
}