namespace RMS.UI.Forms.ReferenceBooks
{
    partial class ServiceListEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServiceListEdit));
            this.lblName = new System.Windows.Forms.Label();
            this.cmbTypeServiceList = new DevExpress.XtraEditors.ComboBoxEdit();
            this.panelFooter = new DevExpress.XtraEditors.PanelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.xtraTab = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.btnTaxSystem = new DevExpress.XtraEditors.ButtonEdit();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.btnKindActivity = new DevExpress.XtraEditors.ButtonEdit();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.memoSourceDocuments = new DevExpress.XtraEditors.MemoEdit();
            this.xtraTabPage4 = new DevExpress.XtraTab.XtraTabPage();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.memoComment = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit1 = new DevExpress.XtraEditors.SpinEdit();
            this.spinEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.xtraTabPage5 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage6 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage7 = new DevExpress.XtraTab.XtraTabPage();
            this.lblValue = new DevExpress.XtraEditors.LabelControl();
            this.calcValue = new DevExpress.XtraEditors.CalcEdit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTypeServiceList.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelFooter)).BeginInit();
            this.panelFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTab)).BeginInit();
            this.xtraTab.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnTaxSystem.Properties)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnKindActivity.Properties)).BeginInit();
            this.xtraTabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoSourceDocuments.Properties)).BeginInit();
            this.xtraTabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.calcValue.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 15);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(112, 16);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Наименование:";
            // 
            // cmbTypeServiceList
            // 
            this.cmbTypeServiceList.Location = new System.Drawing.Point(130, 12);
            this.cmbTypeServiceList.Name = "cmbTypeServiceList";
            this.cmbTypeServiceList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbTypeServiceList.Size = new System.Drawing.Size(795, 22);
            this.cmbTypeServiceList.TabIndex = 7;
            this.cmbTypeServiceList.SelectedIndexChanged += new System.EventHandler(this.cmbTypeServiceList_SelectedIndexChanged);
            // 
            // panelFooter
            // 
            this.panelFooter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelFooter.Controls.Add(this.labelControl3);
            this.panelFooter.Controls.Add(this.memoComment);
            this.panelFooter.Controls.Add(this.btnSave);
            this.panelFooter.Controls.Add(this.btnCancel);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 506);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(937, 50);
            this.panelFooter.TabIndex = 11;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(824, 9);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 28);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(716, 9);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // xtraTab
            // 
            this.xtraTab.AppearancePage.HeaderActive.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.xtraTab.AppearancePage.HeaderActive.Options.UseFont = true;
            this.xtraTab.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.xtraTab.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Left;
            this.xtraTab.HeaderOrientation = DevExpress.XtraTab.TabOrientation.Horizontal;
            this.xtraTab.Location = new System.Drawing.Point(0, 68);
            this.xtraTab.Name = "xtraTab";
            this.xtraTab.PageImagePosition = DevExpress.XtraTab.TabPageImagePosition.None;
            this.xtraTab.SelectedTabPage = this.xtraTabPage1;
            this.xtraTab.Size = new System.Drawing.Size(937, 438);
            this.xtraTab.TabIndex = 14;
            this.xtraTab.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2,
            this.xtraTabPage3,
            this.xtraTabPage4,
            this.xtraTabPage5,
            this.xtraTabPage6,
            this.xtraTabPage7});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.btnTaxSystem);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.PageVisible = false;
            this.xtraTabPage1.Size = new System.Drawing.Size(594, 436);
            this.xtraTabPage1.Text = "Система налогооблажения";
            // 
            // btnTaxSystem
            // 
            this.btnTaxSystem.Location = new System.Drawing.Point(26, 16);
            this.btnTaxSystem.Name = "btnTaxSystem";
            this.btnTaxSystem.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnTaxSystem.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnTaxSystem.Size = new System.Drawing.Size(680, 22);
            this.btnTaxSystem.TabIndex = 14;
            this.btnTaxSystem.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnTaxSystem_ButtonPressed);
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.btnKindActivity);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.PageVisible = false;
            this.xtraTabPage2.Size = new System.Drawing.Size(594, 436);
            this.xtraTabPage2.Text = "Вид деятельности";
            // 
            // btnKindActivity
            // 
            this.btnKindActivity.Location = new System.Drawing.Point(29, 25);
            this.btnKindActivity.Name = "btnKindActivity";
            this.btnKindActivity.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnKindActivity.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnKindActivity.Size = new System.Drawing.Size(635, 22);
            this.btnKindActivity.TabIndex = 8;
            this.btnKindActivity.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnKindActivity_ButtonPressed);
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Controls.Add(this.memoSourceDocuments);
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.Size = new System.Drawing.Size(594, 436);
            this.xtraTabPage3.Text = "Первичные документы кто вносит в 1С  ";
            // 
            // memoSourceDocuments
            // 
            this.memoSourceDocuments.Location = new System.Drawing.Point(3, 3);
            this.memoSourceDocuments.Name = "memoSourceDocuments";
            this.memoSourceDocuments.Size = new System.Drawing.Size(580, 131);
            this.memoSourceDocuments.TabIndex = 0;
            // 
            // xtraTabPage4
            // 
            this.xtraTabPage4.Controls.Add(this.labelControl2);
            this.xtraTabPage4.Controls.Add(this.labelControl1);
            this.xtraTabPage4.Controls.Add(this.textEdit1);
            this.xtraTabPage4.Controls.Add(this.spinEdit1);
            this.xtraTabPage4.Name = "xtraTabPage4";
            this.xtraTabPage4.Size = new System.Drawing.Size(594, 436);
            this.xtraTabPage4.Text = "Банковские счета и операции";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(15, 10);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(95, 16);
            this.labelControl3.TabIndex = 5;
            this.labelControl3.Text = "Комментарий:";
            // 
            // memoComment
            // 
            this.memoComment.Location = new System.Drawing.Point(116, 8);
            this.memoComment.Name = "memoComment";
            this.memoComment.Size = new System.Drawing.Size(593, 29);
            this.memoComment.TabIndex = 4;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(87, 49);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(73, 16);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Кто ведет:";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(74, 16);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(145, 16);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Колличество счетов:";
            // 
            // textEdit1
            // 
            this.textEdit1.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.textEdit1.Location = new System.Drawing.Point(245, 10);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.textEdit1.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.textEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.textEdit1.Size = new System.Drawing.Size(100, 22);
            this.textEdit1.TabIndex = 0;
            this.textEdit1.EditValueChanged += new System.EventHandler(this.textEdit1_EditValueChanged);
            // 
            // spinEdit1
            // 
            this.spinEdit1.EditValue = "Заказчик";
            this.spinEdit1.Location = new System.Drawing.Point(166, 46);
            this.spinEdit1.Name = "spinEdit1";
            this.spinEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEdit1.Properties.Items.AddRange(new object[] {
            "Заказчик",
            "Исполнитель"});
            this.spinEdit1.Size = new System.Drawing.Size(179, 22);
            this.spinEdit1.TabIndex = 3;
            this.spinEdit1.SelectedIndexChanged += new System.EventHandler(this.spinEdit1_SelectedIndexChanged);
            // 
            // xtraTabPage5
            // 
            this.xtraTabPage5.Name = "xtraTabPage5";
            this.xtraTabPage5.Size = new System.Drawing.Size(594, 436);
            this.xtraTabPage5.Text = "Наличие валютных операций";
            // 
            // xtraTabPage6
            // 
            this.xtraTabPage6.Name = "xtraTabPage6";
            this.xtraTabPage6.Size = new System.Drawing.Size(594, 436);
            this.xtraTabPage6.Text = "Наличие ВЭД";
            // 
            // xtraTabPage7
            // 
            this.xtraTabPage7.Name = "xtraTabPage7";
            this.xtraTabPage7.Size = new System.Drawing.Size(594, 436);
            this.xtraTabPage7.Text = "Продажа товаров с разными ставками НДС";
            // 
            // lblValue
            // 
            this.lblValue.Location = new System.Drawing.Point(695, 43);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(49, 16);
            this.lblValue.TabIndex = 93;
            this.lblValue.Text = "Cумма:";
            // 
            // calcValue
            // 
            this.calcValue.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.calcValue.Location = new System.Drawing.Point(750, 40);
            this.calcValue.Name = "calcValue";
            this.calcValue.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.calcValue.Properties.DisplayFormat.FormatString = "n";
            this.calcValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.calcValue.Properties.Mask.EditMask = "n";
            this.calcValue.Size = new System.Drawing.Size(174, 22);
            this.calcValue.TabIndex = 92;
            // 
            // ServiceListEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 556);
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.xtraTab);
            this.Controls.Add(this.calcValue);
            this.Controls.Add(this.cmbTypeServiceList);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.panelFooter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ServiceListEdit";
            this.Text = "Должность";
            this.Load += new System.EventHandler(this.PositionEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cmbTypeServiceList.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelFooter)).EndInit();
            this.panelFooter.ResumeLayout(false);
            this.panelFooter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTab)).EndInit();
            this.xtraTab.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnTaxSystem.Properties)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnKindActivity.Properties)).EndInit();
            this.xtraTabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoSourceDocuments.Properties)).EndInit();
            this.xtraTabPage4.ResumeLayout(false);
            this.xtraTabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.calcValue.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblName;
        private DevExpress.XtraEditors.ComboBoxEdit cmbTypeServiceList;
        private DevExpress.XtraEditors.PanelControl panelFooter;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraTab.XtraTabControl xtraTab;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraEditors.ButtonEdit btnTaxSystem;
        private DevExpress.XtraEditors.LabelControl lblValue;
        private DevExpress.XtraEditors.CalcEdit calcValue;
        private DevExpress.XtraEditors.ButtonEdit btnKindActivity;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage3;
        private DevExpress.XtraEditors.MemoEdit memoSourceDocuments;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage4;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage5;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage6;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage7;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.MemoEdit memoComment;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SpinEdit textEdit1;
        private DevExpress.XtraEditors.ComboBoxEdit spinEdit1;
    }
}