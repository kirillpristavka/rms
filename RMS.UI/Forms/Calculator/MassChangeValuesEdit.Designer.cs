
namespace RMS.UI.Forms.Calculator
{
    partial class MassChangeValuesEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MassChangeValuesEdit));
            this.layoutControlMassChangeValues = new DevExpress.XtraLayout.LayoutControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.cmbOperation = new DevExpress.XtraEditors.ComboBoxEdit();
            this.spinValue = new DevExpress.XtraEditors.SpinEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItemSave = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItemSave = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemCancel = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItemCancel = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItemOperation = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItemValue = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmbTypeCalculatorIndicator = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlItemTypeCalculatorIndicator = new DevExpress.XtraLayout.LayoutControlItem();
            this.checkIsUseTariffScaleObj = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControlItemIsUseTariffScaleObj = new DevExpress.XtraLayout.LayoutControlItem();
            this.checkIsUseTariffStaffObj = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControlItemIsUseTariffStaffObj = new DevExpress.XtraLayout.LayoutControlItem();
            this.checkIsUseTypeCalculatorIndicator = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControlItemIsUseTypeCalculatorIndicator = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlMassChangeValues)).BeginInit();
            this.layoutControlMassChangeValues.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOperation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemOperation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTypeCalculatorIndicator.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTypeCalculatorIndicator)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsUseTariffScaleObj.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemIsUseTariffScaleObj)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsUseTariffStaffObj.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemIsUseTariffStaffObj)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsUseTypeCalculatorIndicator.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemIsUseTypeCalculatorIndicator)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlMassChangeValues
            // 
            this.layoutControlMassChangeValues.Controls.Add(this.checkIsUseTypeCalculatorIndicator);
            this.layoutControlMassChangeValues.Controls.Add(this.btnCancel);
            this.layoutControlMassChangeValues.Controls.Add(this.btnSave);
            this.layoutControlMassChangeValues.Controls.Add(this.cmbOperation);
            this.layoutControlMassChangeValues.Controls.Add(this.spinValue);
            this.layoutControlMassChangeValues.Controls.Add(this.cmbTypeCalculatorIndicator);
            this.layoutControlMassChangeValues.Controls.Add(this.checkIsUseTariffScaleObj);
            this.layoutControlMassChangeValues.Controls.Add(this.checkIsUseTariffStaffObj);
            this.layoutControlMassChangeValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlMassChangeValues.Location = new System.Drawing.Point(0, 0);
            this.layoutControlMassChangeValues.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.layoutControlMassChangeValues.Name = "layoutControlMassChangeValues";
            this.layoutControlMassChangeValues.Root = this.Root;
            this.layoutControlMassChangeValues.Size = new System.Drawing.Size(684, 160);
            this.layoutControlMassChangeValues.TabIndex = 0;
            this.layoutControlMassChangeValues.Text = "layoutControl1";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(513, 132);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(165, 22);
            this.btnCancel.StyleController = this.layoutControlMassChangeValues;
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(344, 132);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(165, 22);
            this.btnSave.StyleController = this.layoutControlMassChangeValues;
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Применить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cmbOperation
            // 
            this.cmbOperation.Location = new System.Drawing.Point(114, 6);
            this.cmbOperation.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cmbOperation.Name = "cmbOperation";
            this.cmbOperation.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbOperation.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbOperation.Size = new System.Drawing.Size(226, 22);
            this.cmbOperation.StyleController = this.layoutControlMassChangeValues;
            this.cmbOperation.TabIndex = 6;
            // 
            // spinValue
            // 
            this.spinValue.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinValue.Location = new System.Drawing.Point(452, 6);
            this.spinValue.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.spinValue.Name = "spinValue";
            this.spinValue.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinValue.Properties.DisplayFormat.FormatString = "n2";
            this.spinValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinValue.Properties.EditFormat.FormatString = "n2";
            this.spinValue.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinValue.Properties.Mask.EditMask = "n2";
            this.spinValue.Size = new System.Drawing.Size(226, 22);
            this.spinValue.StyleController = this.layoutControlMassChangeValues;
            this.spinValue.TabIndex = 7;
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
            this.emptySpaceItemSave,
            this.layoutControlItemSave,
            this.layoutControlItemCancel,
            this.emptySpaceItemCancel,
            this.layoutControlItemOperation,
            this.layoutControlItemValue,
            this.layoutControlItemIsUseTypeCalculatorIndicator,
            this.layoutControlItemTypeCalculatorIndicator,
            this.layoutControlItemIsUseTariffScaleObj,
            this.emptySpaceItem,
            this.layoutControlItemIsUseTariffStaffObj});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4);
            this.Root.Size = new System.Drawing.Size(684, 160);
            this.Root.TextVisible = false;
            // 
            // emptySpaceItemSave
            // 
            this.emptySpaceItemSave.AllowHotTrack = false;
            this.emptySpaceItemSave.Location = new System.Drawing.Point(0, 126);
            this.emptySpaceItemSave.Name = "emptySpaceItemSave";
            this.emptySpaceItemSave.Size = new System.Drawing.Size(169, 26);
            this.emptySpaceItemSave.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItemSave
            // 
            this.layoutControlItemSave.Control = this.btnSave;
            this.layoutControlItemSave.Location = new System.Drawing.Point(338, 126);
            this.layoutControlItemSave.Name = "layoutControlItemSave";
            this.layoutControlItemSave.Size = new System.Drawing.Size(169, 26);
            this.layoutControlItemSave.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemSave.TextVisible = false;
            // 
            // layoutControlItemCancel
            // 
            this.layoutControlItemCancel.Control = this.btnCancel;
            this.layoutControlItemCancel.Location = new System.Drawing.Point(507, 126);
            this.layoutControlItemCancel.Name = "layoutControlItemCancel";
            this.layoutControlItemCancel.Size = new System.Drawing.Size(169, 26);
            this.layoutControlItemCancel.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemCancel.TextVisible = false;
            // 
            // emptySpaceItemCancel
            // 
            this.emptySpaceItemCancel.AllowHotTrack = false;
            this.emptySpaceItemCancel.Location = new System.Drawing.Point(169, 126);
            this.emptySpaceItemCancel.Name = "emptySpaceItemCancel";
            this.emptySpaceItemCancel.Size = new System.Drawing.Size(169, 26);
            this.emptySpaceItemCancel.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItemOperation
            // 
            this.layoutControlItemOperation.Control = this.cmbOperation;
            this.layoutControlItemOperation.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemOperation.Name = "layoutControlItemOperation";
            this.layoutControlItemOperation.Size = new System.Drawing.Size(338, 26);
            this.layoutControlItemOperation.Text = "Операция:";
            this.layoutControlItemOperation.TextSize = new System.Drawing.Size(105, 16);
            // 
            // emptySpaceItem
            // 
            this.emptySpaceItem.AllowHotTrack = false;
            this.emptySpaceItem.Location = new System.Drawing.Point(0, 100);
            this.emptySpaceItem.Name = "emptySpaceItem";
            this.emptySpaceItem.Size = new System.Drawing.Size(676, 26);
            this.emptySpaceItem.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItemValue
            // 
            this.layoutControlItemValue.Control = this.spinValue;
            this.layoutControlItemValue.Location = new System.Drawing.Point(338, 0);
            this.layoutControlItemValue.Name = "layoutControlItemValue";
            this.layoutControlItemValue.Size = new System.Drawing.Size(338, 26);
            this.layoutControlItemValue.Text = "Значение:";
            this.layoutControlItemValue.TextSize = new System.Drawing.Size(105, 16);
            // 
            // cmbTypeCalculatorIndicator
            // 
            this.cmbTypeCalculatorIndicator.Location = new System.Drawing.Point(452, 32);
            this.cmbTypeCalculatorIndicator.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cmbTypeCalculatorIndicator.Name = "cmbTypeCalculatorIndicator";
            this.cmbTypeCalculatorIndicator.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbTypeCalculatorIndicator.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbTypeCalculatorIndicator.Size = new System.Drawing.Size(226, 22);
            this.cmbTypeCalculatorIndicator.StyleController = this.layoutControlMassChangeValues;
            this.cmbTypeCalculatorIndicator.TabIndex = 6;
            // 
            // layoutControlItemTypeCalculatorIndicator
            // 
            this.layoutControlItemTypeCalculatorIndicator.Control = this.cmbTypeCalculatorIndicator;
            this.layoutControlItemTypeCalculatorIndicator.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItemTypeCalculatorIndicator.CustomizationFormText = "Операция:";
            this.layoutControlItemTypeCalculatorIndicator.Location = new System.Drawing.Point(338, 26);
            this.layoutControlItemTypeCalculatorIndicator.Name = "layoutControlItemTypeCalculatorIndicator";
            this.layoutControlItemTypeCalculatorIndicator.Size = new System.Drawing.Size(338, 26);
            this.layoutControlItemTypeCalculatorIndicator.Text = "Тип операции:";
            this.layoutControlItemTypeCalculatorIndicator.TextSize = new System.Drawing.Size(105, 16);
            this.layoutControlItemTypeCalculatorIndicator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // checkIsUseTariffScaleObj
            // 
            this.checkIsUseTariffScaleObj.Location = new System.Drawing.Point(6, 58);
            this.checkIsUseTariffScaleObj.Name = "checkIsUseTariffScaleObj";
            this.checkIsUseTariffScaleObj.Properties.Caption = "Изменить значения тарифной сетки";
            this.checkIsUseTariffScaleObj.Size = new System.Drawing.Size(672, 20);
            this.checkIsUseTariffScaleObj.StyleController = this.layoutControlMassChangeValues;
            this.checkIsUseTariffScaleObj.TabIndex = 8;
            // 
            // layoutControlItemIsUseTariffScaleObj
            // 
            this.layoutControlItemIsUseTariffScaleObj.Control = this.checkIsUseTariffScaleObj;
            this.layoutControlItemIsUseTariffScaleObj.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItemIsUseTariffScaleObj.Name = "layoutControlItemIsUseTariffScaleObj";
            this.layoutControlItemIsUseTariffScaleObj.Size = new System.Drawing.Size(676, 24);
            this.layoutControlItemIsUseTariffScaleObj.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemIsUseTariffScaleObj.TextVisible = false;
            this.layoutControlItemIsUseTariffScaleObj.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // checkIsUseTariffStaffObj
            // 
            this.checkIsUseTariffStaffObj.Location = new System.Drawing.Point(6, 82);
            this.checkIsUseTariffStaffObj.Name = "checkIsUseTariffStaffObj";
            this.checkIsUseTariffStaffObj.Properties.Caption = "Изменить значения шкалы цен для сотрудников";
            this.checkIsUseTariffStaffObj.Size = new System.Drawing.Size(672, 20);
            this.checkIsUseTariffStaffObj.StyleController = this.layoutControlMassChangeValues;
            this.checkIsUseTariffStaffObj.TabIndex = 9;
            // 
            // layoutControlItemIsUseTariffStaffObj
            // 
            this.layoutControlItemIsUseTariffStaffObj.Control = this.checkIsUseTariffStaffObj;
            this.layoutControlItemIsUseTariffStaffObj.Location = new System.Drawing.Point(0, 76);
            this.layoutControlItemIsUseTariffStaffObj.Name = "layoutControlItemIsUseTariffStaffObj";
            this.layoutControlItemIsUseTariffStaffObj.Size = new System.Drawing.Size(676, 24);
            this.layoutControlItemIsUseTariffStaffObj.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemIsUseTariffStaffObj.TextVisible = false;
            this.layoutControlItemIsUseTariffStaffObj.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // checkIsUseTypeCalculatorIndicator
            // 
            this.checkIsUseTypeCalculatorIndicator.Location = new System.Drawing.Point(6, 32);
            this.checkIsUseTypeCalculatorIndicator.Name = "checkIsUseTypeCalculatorIndicator";
            this.checkIsUseTypeCalculatorIndicator.Properties.Caption = "Изменить значения показателей по их типу";
            this.checkIsUseTypeCalculatorIndicator.Size = new System.Drawing.Size(334, 20);
            this.checkIsUseTypeCalculatorIndicator.StyleController = this.layoutControlMassChangeValues;
            this.checkIsUseTypeCalculatorIndicator.TabIndex = 10;
            // 
            // layoutControlItemIsUseTypeCalculatorIndicator
            // 
            this.layoutControlItemIsUseTypeCalculatorIndicator.Control = this.checkIsUseTypeCalculatorIndicator;
            this.layoutControlItemIsUseTypeCalculatorIndicator.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItemIsUseTypeCalculatorIndicator.Name = "layoutControlItemIsUseTypeCalculatorIndicator";
            this.layoutControlItemIsUseTypeCalculatorIndicator.Size = new System.Drawing.Size(338, 26);
            this.layoutControlItemIsUseTypeCalculatorIndicator.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemIsUseTypeCalculatorIndicator.TextVisible = false;
            this.layoutControlItemIsUseTypeCalculatorIndicator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // MassChangeValuesEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 160);
            this.Controls.Add(this.layoutControlMassChangeValues);
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("MassChangeValuesEdit.IconOptions.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MinimumSize = new System.Drawing.Size(650, 150);
            this.Name = "MassChangeValuesEdit";
            this.Text = "Массовая замена значений";
            this.Load += new System.EventHandler(this.MassChangeValuesEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlMassChangeValues)).EndInit();
            this.layoutControlMassChangeValues.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbOperation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemOperation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTypeCalculatorIndicator.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTypeCalculatorIndicator)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsUseTariffScaleObj.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemIsUseTariffScaleObj)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsUseTariffStaffObj.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemIsUseTariffStaffObj)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsUseTypeCalculatorIndicator.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemIsUseTypeCalculatorIndicator)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControlMassChangeValues;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.ComboBoxEdit cmbOperation;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCancel;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemOperation;
        private DevExpress.XtraEditors.SpinEdit spinValue;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemValue;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem;
        private DevExpress.XtraEditors.ComboBoxEdit cmbTypeCalculatorIndicator;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemTypeCalculatorIndicator;
        private DevExpress.XtraEditors.CheckEdit checkIsUseTariffScaleObj;
        private DevExpress.XtraEditors.CheckEdit checkIsUseTariffStaffObj;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemIsUseTariffStaffObj;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemIsUseTariffScaleObj;
        private DevExpress.XtraEditors.CheckEdit checkIsUseTypeCalculatorIndicator;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemIsUseTypeCalculatorIndicator;
    }
}