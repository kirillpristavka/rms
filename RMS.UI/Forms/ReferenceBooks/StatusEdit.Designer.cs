namespace RMS.UI.Forms.ReferenceBooks
{
    partial class StatusEdit<T>
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
            this.cmbIcon = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.lblIcon = new DevExpress.XtraEditors.LabelControl();
            this.checkIsFormationArchiveFolder = new DevExpress.XtraEditors.CheckEdit();
            this.panelControlFooter = new DevExpress.XtraEditors.PanelControl();
            this.txtIndex = new DevExpress.XtraEditors.TextEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.panelControlHeader = new DevExpress.XtraEditors.PanelControl();
            this.checkIsDefault = new DevExpress.XtraEditors.CheckEdit();
            this.lblColor = new DevExpress.XtraEditors.LabelControl();
            this.colorStatus = new DevExpress.XtraEditors.ColorEdit();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.memoDescription = new DevExpress.XtraEditors.MemoEdit();
            this.panelControlOption = new DevExpress.XtraEditors.PanelControl();
            this.checkIsFormationReport = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbIcon.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsFormationArchiveFolder.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlFooter)).BeginInit();
            this.panelControlFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtIndex.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlHeader)).BeginInit();
            this.panelControlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsDefault.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlOption)).BeginInit();
            this.panelControlOption.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsFormationReport.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbIcon
            // 
            this.cmbIcon.Location = new System.Drawing.Point(150, 10);
            this.cmbIcon.Name = "cmbIcon";
            this.cmbIcon.Properties.AutoHeight = false;
            this.cmbIcon.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.cmbIcon.Size = new System.Drawing.Size(75, 25);
            this.cmbIcon.TabIndex = 31;
            this.cmbIcon.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.cmbIcon_ButtonPressed);
            // 
            // lblIcon
            // 
            this.lblIcon.Location = new System.Drawing.Point(81, 14);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(55, 16);
            this.lblIcon.TabIndex = 34;
            this.lblIcon.Text = "Иконка:";
            // 
            // checkIsFormationArchiveFolder
            // 
            this.checkIsFormationArchiveFolder.Location = new System.Drawing.Point(150, 5);
            this.checkIsFormationArchiveFolder.Name = "checkIsFormationArchiveFolder";
            this.checkIsFormationArchiveFolder.Properties.AutoHeight = false;
            this.checkIsFormationArchiveFolder.Properties.Caption = "Использовать для формирования архивных папок";
            this.checkIsFormationArchiveFolder.Size = new System.Drawing.Size(422, 25);
            this.checkIsFormationArchiveFolder.TabIndex = 35;
            // 
            // panelControlFooter
            // 
            this.panelControlFooter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControlFooter.Controls.Add(this.txtIndex);
            this.panelControlFooter.Controls.Add(this.btnSave);
            this.panelControlFooter.Controls.Add(this.btnCancel);
            this.panelControlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControlFooter.Location = new System.Drawing.Point(0, 245);
            this.panelControlFooter.Name = "panelControlFooter";
            this.panelControlFooter.Size = new System.Drawing.Size(584, 35);
            this.panelControlFooter.TabIndex = 72;
            // 
            // txtIndex
            // 
            this.txtIndex.Location = new System.Drawing.Point(12, 5);
            this.txtIndex.Name = "txtIndex";
            this.txtIndex.Properties.Appearance.Options.UseTextOptions = true;
            this.txtIndex.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtIndex.Properties.AutoHeight = false;
            this.txtIndex.Size = new System.Drawing.Size(50, 25);
            this.txtIndex.TabIndex = 83;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(366, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 25);
            this.btnSave.TabIndex = 59;
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(472, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 25);
            this.btnCancel.TabIndex = 58;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panelControlHeader
            // 
            this.panelControlHeader.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControlHeader.Controls.Add(this.checkIsDefault);
            this.panelControlHeader.Controls.Add(this.lblColor);
            this.panelControlHeader.Controls.Add(this.colorStatus);
            this.panelControlHeader.Controls.Add(this.lblDescription);
            this.panelControlHeader.Controls.Add(this.lblName);
            this.panelControlHeader.Controls.Add(this.txtName);
            this.panelControlHeader.Controls.Add(this.lblIcon);
            this.panelControlHeader.Controls.Add(this.memoDescription);
            this.panelControlHeader.Controls.Add(this.cmbIcon);
            this.panelControlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControlHeader.Location = new System.Drawing.Point(0, 0);
            this.panelControlHeader.Name = "panelControlHeader";
            this.panelControlHeader.Size = new System.Drawing.Size(584, 152);
            this.panelControlHeader.TabIndex = 77;
            // 
            // checkIsDefault
            // 
            this.checkIsDefault.Location = new System.Drawing.Point(150, 128);
            this.checkIsDefault.Name = "checkIsDefault";
            this.checkIsDefault.Properties.Caption = "По умолчанию";
            this.checkIsDefault.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.checkIsDefault.Size = new System.Drawing.Size(126, 20);
            this.checkIsDefault.TabIndex = 82;
            // 
            // lblColor
            // 
            this.lblColor.Location = new System.Drawing.Point(327, 14);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(39, 16);
            this.lblColor.TabIndex = 81;
            this.lblColor.Text = "Цвет:";
            // 
            // colorStatus
            // 
            this.colorStatus.EditValue = System.Drawing.Color.Empty;
            this.colorStatus.Location = new System.Drawing.Point(372, 10);
            this.colorStatus.Name = "colorStatus";
            this.colorStatus.Properties.AutoHeight = false;
            this.colorStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.colorStatus.Size = new System.Drawing.Size(200, 25);
            this.colorStatus.TabIndex = 80;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(56, 74);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(80, 16);
            this.lblDescription.TabIndex = 78;
            this.lblDescription.Text = "Описание:";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(24, 45);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(112, 16);
            this.lblName.TabIndex = 76;
            this.lblName.Text = "Наименование:";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(150, 41);
            this.txtName.Name = "txtName";
            this.txtName.Properties.AutoHeight = false;
            this.txtName.Size = new System.Drawing.Size(422, 25);
            this.txtName.TabIndex = 77;
            // 
            // memoDescription
            // 
            this.memoDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.memoDescription.Location = new System.Drawing.Point(150, 72);
            this.memoDescription.Name = "memoDescription";
            this.memoDescription.Properties.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.memoDescription.Size = new System.Drawing.Size(422, 50);
            this.memoDescription.TabIndex = 79;
            // 
            // panelControlOption
            // 
            this.panelControlOption.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControlOption.Controls.Add(this.checkIsFormationReport);
            this.panelControlOption.Controls.Add(this.checkIsFormationArchiveFolder);
            this.panelControlOption.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControlOption.Location = new System.Drawing.Point(0, 152);
            this.panelControlOption.Name = "panelControlOption";
            this.panelControlOption.Size = new System.Drawing.Size(584, 70);
            this.panelControlOption.TabIndex = 78;
            this.panelControlOption.Visible = false;
            // 
            // checkIsFormationReport
            // 
            this.checkIsFormationReport.Location = new System.Drawing.Point(150, 36);
            this.checkIsFormationReport.Name = "checkIsFormationReport";
            this.checkIsFormationReport.Properties.AutoHeight = false;
            this.checkIsFormationReport.Properties.Caption = "Использовать для формирования отчетов";
            this.checkIsFormationReport.Size = new System.Drawing.Size(422, 25);
            this.checkIsFormationReport.TabIndex = 36;
            // 
            // StatusEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(584, 280);
            this.Controls.Add(this.panelControlOption);
            this.Controls.Add(this.panelControlHeader);
            this.Controls.Add(this.panelControlFooter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MinimumSize = new System.Drawing.Size(600, 40);
            this.Name = "StatusEdit";
            this.Text = "Статус";
            this.Load += new System.EventHandler(this.PositionEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cmbIcon.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsFormationArchiveFolder.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlFooter)).EndInit();
            this.panelControlFooter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtIndex.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlHeader)).EndInit();
            this.panelControlHeader.ResumeLayout(false);
            this.panelControlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsDefault.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlOption)).EndInit();
            this.panelControlOption.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkIsFormationReport.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbIcon;
        private DevExpress.XtraEditors.LabelControl lblIcon;
        private DevExpress.XtraEditors.CheckEdit checkIsFormationArchiveFolder;
        private DevExpress.XtraEditors.PanelControl panelControlFooter;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.PanelControl panelControlHeader;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblName;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.MemoEdit memoDescription;
        private DevExpress.XtraEditors.PanelControl panelControlOption;
        private DevExpress.XtraEditors.CheckEdit checkIsFormationReport;
        private DevExpress.XtraEditors.LabelControl lblColor;
        private DevExpress.XtraEditors.ColorEdit colorStatus;
        private DevExpress.XtraEditors.CheckEdit checkIsDefault;
        private DevExpress.XtraEditors.TextEdit txtIndex;
    }
}