
namespace RMS.UI.xUI.PackagesDocument.Forms
{
    partial class PackageDocumentObjEdit
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
            this.layoutControlPackageDocument = new DevExpress.XtraLayout.LayoutControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.dateDeparture = new DevExpress.XtraEditors.DateEdit();
            this.dateReceiving = new DevExpress.XtraEditors.DateEdit();
            this.btnFile = new DevExpress.XtraEditors.ButtonEdit();
            this.checkIsScannedDocument = new DevExpress.XtraEditors.CheckEdit();
            this.checkIsOriginalDocument = new DevExpress.XtraEditors.CheckEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.tabbedControlGroupPackageDocument = new DevExpress.XtraLayout.TabbedControlGroup();
            this.layoutControlGroupOperation = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemDateDeparture = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemSave = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemCancel = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItemDate = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItemSave = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItemCancel = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItemFile = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemDateReceiving = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemIsScannedDocument = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemIsOriginalDocument = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlPackageDocument)).BeginInit();
            this.layoutControlPackageDocument.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateDeparture.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDeparture.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateReceiving.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateReceiving.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsScannedDocument.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsOriginalDocument.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedControlGroupPackageDocument)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupOperation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDateDeparture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDateReceiving)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemIsScannedDocument)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemIsOriginalDocument)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlPackageDocument
            // 
            this.layoutControlPackageDocument.AllowCustomization = false;
            this.layoutControlPackageDocument.Controls.Add(this.btnSave);
            this.layoutControlPackageDocument.Controls.Add(this.btnCancel);
            this.layoutControlPackageDocument.Controls.Add(this.dateDeparture);
            this.layoutControlPackageDocument.Controls.Add(this.dateReceiving);
            this.layoutControlPackageDocument.Controls.Add(this.btnFile);
            this.layoutControlPackageDocument.Controls.Add(this.checkIsScannedDocument);
            this.layoutControlPackageDocument.Controls.Add(this.checkIsOriginalDocument);
            this.layoutControlPackageDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlPackageDocument.Location = new System.Drawing.Point(0, 0);
            this.layoutControlPackageDocument.Margin = new System.Windows.Forms.Padding(4);
            this.layoutControlPackageDocument.Name = "layoutControlPackageDocument";
            this.layoutControlPackageDocument.OptionsFocus.AllowFocusGroups = false;
            this.layoutControlPackageDocument.OptionsFocus.AllowFocusReadonlyEditors = false;
            this.layoutControlPackageDocument.OptionsFocus.AllowFocusTabbedGroups = false;
            this.layoutControlPackageDocument.Root = this.Root;
            this.layoutControlPackageDocument.Size = new System.Drawing.Size(634, 190);
            this.layoutControlPackageDocument.TabIndex = 1;
            this.layoutControlPackageDocument.Text = "layoutControl1";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(320, 149);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(145, 22);
            this.btnSave.StyleController = this.layoutControlPackageDocument;
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(469, 149);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(146, 22);
            this.btnCancel.StyleController = this.layoutControlPackageDocument;
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dateDeparture
            // 
            this.dateDeparture.EditValue = null;
            this.dateDeparture.Location = new System.Drawing.Point(135, 97);
            this.dateDeparture.MaximumSize = new System.Drawing.Size(120, 0);
            this.dateDeparture.MinimumSize = new System.Drawing.Size(120, 0);
            this.dateDeparture.Name = "dateDeparture";
            this.dateDeparture.Properties.Appearance.Options.UseTextOptions = true;
            this.dateDeparture.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dateDeparture.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateDeparture.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateDeparture.Properties.CalendarTimeProperties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.dateDeparture.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.dateDeparture.Size = new System.Drawing.Size(120, 22);
            this.dateDeparture.StyleController = this.layoutControlPackageDocument;
            this.dateDeparture.TabIndex = 5;
            // 
            // dateReceiving
            // 
            this.dateReceiving.EditValue = null;
            this.dateReceiving.Location = new System.Drawing.Point(135, 71);
            this.dateReceiving.MaximumSize = new System.Drawing.Size(120, 0);
            this.dateReceiving.MinimumSize = new System.Drawing.Size(120, 0);
            this.dateReceiving.Name = "dateReceiving";
            this.dateReceiving.Properties.Appearance.Options.UseTextOptions = true;
            this.dateReceiving.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dateReceiving.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateReceiving.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateReceiving.Properties.CalendarTimeProperties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.dateReceiving.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.dateReceiving.Size = new System.Drawing.Size(120, 22);
            this.dateReceiving.StyleController = this.layoutControlPackageDocument;
            this.dateReceiving.TabIndex = 5;
            // 
            // btnFile
            // 
            this.btnFile.Location = new System.Drawing.Point(135, 45);
            this.btnFile.Name = "btnFile";
            this.btnFile.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.btnFile.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnFile.Size = new System.Drawing.Size(480, 22);
            this.btnFile.StyleController = this.layoutControlPackageDocument;
            this.btnFile.TabIndex = 0;
            this.btnFile.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnFile_ButtonPressed);
            this.btnFile.DoubleClick += new System.EventHandler(this.btnFile_DoubleClick);
            // 
            // checkIsScannedDocument
            // 
            this.checkIsScannedDocument.Location = new System.Drawing.Point(259, 71);
            this.checkIsScannedDocument.Name = "checkIsScannedDocument";
            this.checkIsScannedDocument.Properties.Caption = "Сканированный документ";
            this.checkIsScannedDocument.Size = new System.Drawing.Size(356, 20);
            this.checkIsScannedDocument.StyleController = this.layoutControlPackageDocument;
            this.checkIsScannedDocument.TabIndex = 8;
            // 
            // checkIsOriginalDocument
            // 
            this.checkIsOriginalDocument.Location = new System.Drawing.Point(259, 97);
            this.checkIsOriginalDocument.Name = "checkIsOriginalDocument";
            this.checkIsOriginalDocument.Properties.Caption = "Оригинальный документ";
            this.checkIsOriginalDocument.Size = new System.Drawing.Size(356, 20);
            this.checkIsOriginalDocument.StyleController = this.layoutControlPackageDocument;
            this.checkIsOriginalDocument.TabIndex = 8;
            // 
            // Root
            // 
            this.Root.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Root.AppearanceItemCaption.Options.UseFont = true;
            this.Root.AppearanceItemCaption.Options.UseTextOptions = true;
            this.Root.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.tabbedControlGroupPackageDocument});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.Root.Size = new System.Drawing.Size(634, 190);
            this.Root.TextVisible = false;
            // 
            // tabbedControlGroupPackageDocument
            // 
            this.tabbedControlGroupPackageDocument.AppearanceTabPage.HeaderActive.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.tabbedControlGroupPackageDocument.AppearanceTabPage.HeaderActive.Options.UseFont = true;
            this.tabbedControlGroupPackageDocument.HeaderAutoFill = DevExpress.Utils.DefaultBoolean.True;
            this.tabbedControlGroupPackageDocument.Location = new System.Drawing.Point(0, 0);
            this.tabbedControlGroupPackageDocument.Name = "tabbedControlGroupPackageDocument";
            this.tabbedControlGroupPackageDocument.SelectedTabPage = this.layoutControlGroupOperation;
            this.tabbedControlGroupPackageDocument.Size = new System.Drawing.Size(624, 180);
            this.tabbedControlGroupPackageDocument.TabPages.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroupOperation});
            // 
            // layoutControlGroupOperation
            // 
            this.layoutControlGroupOperation.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemDateDeparture,
            this.layoutControlItemSave,
            this.layoutControlItemCancel,
            this.emptySpaceItemDate,
            this.emptySpaceItemSave,
            this.emptySpaceItemCancel,
            this.layoutControlItemFile,
            this.layoutControlItemDateReceiving,
            this.layoutControlItemIsScannedDocument,
            this.layoutControlItemIsOriginalDocument});
            this.layoutControlGroupOperation.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupOperation.Name = "layoutControlGroupOperation";
            this.layoutControlGroupOperation.Size = new System.Drawing.Size(600, 130);
            this.layoutControlGroupOperation.Text = "Общая информация";
            // 
            // layoutControlItemDateDeparture
            // 
            this.layoutControlItemDateDeparture.Control = this.dateDeparture;
            this.layoutControlItemDateDeparture.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItemDateDeparture.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItemDateDeparture.Name = "layoutControlItemDateDeparture";
            this.layoutControlItemDateDeparture.Size = new System.Drawing.Size(240, 26);
            this.layoutControlItemDateDeparture.Text = "Дата отправления:";
            this.layoutControlItemDateDeparture.TextSize = new System.Drawing.Size(113, 13);
            // 
            // layoutControlItemSave
            // 
            this.layoutControlItemSave.Control = this.btnSave;
            this.layoutControlItemSave.Location = new System.Drawing.Point(301, 104);
            this.layoutControlItemSave.Name = "layoutControlItemSave";
            this.layoutControlItemSave.Size = new System.Drawing.Size(149, 26);
            this.layoutControlItemSave.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemSave.TextVisible = false;
            // 
            // layoutControlItemCancel
            // 
            this.layoutControlItemCancel.Control = this.btnCancel;
            this.layoutControlItemCancel.Location = new System.Drawing.Point(450, 104);
            this.layoutControlItemCancel.Name = "layoutControlItemCancel";
            this.layoutControlItemCancel.Size = new System.Drawing.Size(150, 26);
            this.layoutControlItemCancel.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemCancel.TextVisible = false;
            // 
            // emptySpaceItemDate
            // 
            this.emptySpaceItemDate.AllowHotTrack = false;
            this.emptySpaceItemDate.CustomizationFormText = "emptySpaceItemTabbed";
            this.emptySpaceItemDate.Location = new System.Drawing.Point(0, 78);
            this.emptySpaceItemDate.MinSize = new System.Drawing.Size(83, 21);
            this.emptySpaceItemDate.Name = "emptySpaceItemDate";
            this.emptySpaceItemDate.Size = new System.Drawing.Size(600, 26);
            this.emptySpaceItemDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItemDate.Text = "emptySpaceItemTabbed";
            this.emptySpaceItemDate.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItemSave
            // 
            this.emptySpaceItemSave.AllowHotTrack = false;
            this.emptySpaceItemSave.Location = new System.Drawing.Point(0, 104);
            this.emptySpaceItemSave.Name = "emptySpaceItemSave";
            this.emptySpaceItemSave.Size = new System.Drawing.Size(150, 26);
            this.emptySpaceItemSave.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItemCancel
            // 
            this.emptySpaceItemCancel.AllowHotTrack = false;
            this.emptySpaceItemCancel.Location = new System.Drawing.Point(150, 104);
            this.emptySpaceItemCancel.Name = "emptySpaceItemCancel";
            this.emptySpaceItemCancel.Size = new System.Drawing.Size(151, 26);
            this.emptySpaceItemCancel.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItemFile
            // 
            this.layoutControlItemFile.Control = this.btnFile;
            this.layoutControlItemFile.CustomizationFormText = "Документ:";
            this.layoutControlItemFile.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemFile.Name = "layoutControlItemFile";
            this.layoutControlItemFile.Size = new System.Drawing.Size(600, 26);
            this.layoutControlItemFile.Text = "Файл:";
            this.layoutControlItemFile.TextSize = new System.Drawing.Size(113, 13);
            // 
            // layoutControlItemDateReceiving
            // 
            this.layoutControlItemDateReceiving.Control = this.dateReceiving;
            this.layoutControlItemDateReceiving.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItemDateReceiving.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItemDateReceiving.Name = "layoutControlItemDateReceiving";
            this.layoutControlItemDateReceiving.Size = new System.Drawing.Size(240, 26);
            this.layoutControlItemDateReceiving.Text = "Дата получения:";
            this.layoutControlItemDateReceiving.TextSize = new System.Drawing.Size(113, 13);
            // 
            // layoutControlItemIsScannedDocument
            // 
            this.layoutControlItemIsScannedDocument.Control = this.checkIsScannedDocument;
            this.layoutControlItemIsScannedDocument.Location = new System.Drawing.Point(240, 26);
            this.layoutControlItemIsScannedDocument.Name = "layoutControlItemIsScannedDocument";
            this.layoutControlItemIsScannedDocument.Size = new System.Drawing.Size(360, 26);
            this.layoutControlItemIsScannedDocument.Text = "Оригинальный документ";
            this.layoutControlItemIsScannedDocument.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemIsScannedDocument.TextVisible = false;
            // 
            // layoutControlItemIsOriginalDocument
            // 
            this.layoutControlItemIsOriginalDocument.Control = this.checkIsOriginalDocument;
            this.layoutControlItemIsOriginalDocument.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItemIsOriginalDocument.Location = new System.Drawing.Point(240, 52);
            this.layoutControlItemIsOriginalDocument.Name = "layoutControlItemIsOriginalDocument";
            this.layoutControlItemIsOriginalDocument.Size = new System.Drawing.Size(360, 26);
            this.layoutControlItemIsOriginalDocument.Text = "Сканированный документ";
            this.layoutControlItemIsOriginalDocument.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemIsOriginalDocument.TextVisible = false;
            // 
            // PackageDocumentObjEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 190);
            this.Controls.Add(this.layoutControlPackageDocument);
            this.MinimumSize = new System.Drawing.Size(650, 230);
            this.Name = "PackageDocumentObjEdit";
            this.Text = "Документы";
            this.Load += new System.EventHandler(this.Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlPackageDocument)).EndInit();
            this.layoutControlPackageDocument.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dateDeparture.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDeparture.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateReceiving.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateReceiving.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsScannedDocument.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsOriginalDocument.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedControlGroupPackageDocument)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupOperation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDateDeparture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDateReceiving)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemIsScannedDocument)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemIsOriginalDocument)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControlPackageDocument;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemSave;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemCancel;
        private DevExpress.XtraLayout.TabbedControlGroup tabbedControlGroupPackageDocument;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupOperation;
        private DevExpress.XtraEditors.DateEdit dateDeparture;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemDateDeparture;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemDate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemFile;
        private DevExpress.XtraEditors.DateEdit dateReceiving;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemDateReceiving;
        private DevExpress.XtraEditors.ButtonEdit btnFile;
        private DevExpress.XtraEditors.CheckEdit checkIsScannedDocument;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemIsScannedDocument;
        private DevExpress.XtraEditors.CheckEdit checkIsOriginalDocument;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemIsOriginalDocument;
    }
}