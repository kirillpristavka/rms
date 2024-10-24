namespace RMS.UI.Forms
{
    partial class ImportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportForm));
            this.txtPath = new DevExpress.XtraEditors.ButtonEdit();
            this.lbl_Out = new DevExpress.XtraEditors.LabelControl();
            this.barProgress = new DevExpress.XtraBars.BarEditItem();
            this.groupControlPath = new DevExpress.XtraEditors.GroupControl();
            this.btnStart = new DevExpress.XtraEditors.SimpleButton();
            this.groupControlSettings = new DevExpress.XtraEditors.GroupControl();
            this.txtCellRangeSince = new DevExpress.XtraEditors.TextEdit();
            this.checkEditCellRange = new DevExpress.XtraEditors.CheckEdit();
            this.lblListNumber = new System.Windows.Forms.Label();
            this.spinEditListNumber = new DevExpress.XtraEditors.SpinEdit();
            this.txtCellRangeTo = new DevExpress.XtraEditors.TextEdit();
            this.rgOption = new DevExpress.XtraEditors.RadioGroup();
            this.groupControlInfo = new DevExpress.XtraEditors.GroupControl();
            this.progressBarControl = new DevExpress.XtraEditors.ProgressBarControl();
            this.txtLogger = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlPath)).BeginInit();
            this.groupControlPath.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlSettings)).BeginInit();
            this.groupControlSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCellRangeSince.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditCellRange.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditListNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCellRangeTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgOption.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlInfo)).BeginInit();
            this.groupControlInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLogger.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPath
            // 
            this.txtPath.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtPath.Location = new System.Drawing.Point(0, 79);
            this.txtPath.Margin = new System.Windows.Forms.Padding(4);
            this.txtPath.Name = "txtPath";
            this.txtPath.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtPath.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.txtPath.Size = new System.Drawing.Size(645, 22);
            this.txtPath.TabIndex = 1;
            this.txtPath.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txt_PathToDir_ButtonClick);
            // 
            // lbl_Out
            // 
            this.lbl_Out.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_Out.Location = new System.Drawing.Point(16, 481);
            this.lbl_Out.Margin = new System.Windows.Forms.Padding(4);
            this.lbl_Out.Name = "lbl_Out";
            this.lbl_Out.Size = new System.Drawing.Size(0, 16);
            this.lbl_Out.TabIndex = 5;
            // 
            // barProgress
            // 
            this.barProgress.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barProgress.Edit = null;
            this.barProgress.EditWidth = 100;
            this.barProgress.Id = 64;
            this.barProgress.Name = "barProgress";
            this.barProgress.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // groupControlPath
            // 
            this.groupControlPath.AutoSize = true;
            this.groupControlPath.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupControlPath.Controls.Add(this.btnStart);
            this.groupControlPath.Controls.Add(this.groupControlSettings);
            this.groupControlPath.Controls.Add(this.txtPath);
            this.groupControlPath.Controls.Add(this.rgOption);
            this.groupControlPath.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControlPath.GroupStyle = DevExpress.Utils.GroupStyle.Title;
            this.groupControlPath.Location = new System.Drawing.Point(0, 0);
            this.groupControlPath.Margin = new System.Windows.Forms.Padding(4);
            this.groupControlPath.Name = "groupControlPath";
            this.groupControlPath.Size = new System.Drawing.Size(645, 208);
            this.groupControlPath.TabIndex = 7;
            this.groupControlPath.Text = "Путь до источника";
            // 
            // btnStart
            // 
            this.btnStart.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnStart.Location = new System.Drawing.Point(0, 180);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(645, 28);
            this.btnStart.TabIndex = 15;
            this.btnStart.Text = "Начать";
            this.btnStart.Click += new System.EventHandler(this.btn_StartImport_Click);
            // 
            // groupControlSettings
            // 
            this.groupControlSettings.AutoSize = true;
            this.groupControlSettings.Controls.Add(this.txtCellRangeSince);
            this.groupControlSettings.Controls.Add(this.checkEditCellRange);
            this.groupControlSettings.Controls.Add(this.lblListNumber);
            this.groupControlSettings.Controls.Add(this.spinEditListNumber);
            this.groupControlSettings.Controls.Add(this.txtCellRangeTo);
            this.groupControlSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControlSettings.Location = new System.Drawing.Point(0, 101);
            this.groupControlSettings.Margin = new System.Windows.Forms.Padding(4);
            this.groupControlSettings.Name = "groupControlSettings";
            this.groupControlSettings.Size = new System.Drawing.Size(645, 79);
            this.groupControlSettings.TabIndex = 5;
            this.groupControlSettings.Text = "Настройка импорта";
            this.groupControlSettings.Visible = false;
            // 
            // txtCellRangeSince
            // 
            this.txtCellRangeSince.EditValue = "";
            this.txtCellRangeSince.Location = new System.Drawing.Point(408, 30);
            this.txtCellRangeSince.Margin = new System.Windows.Forms.Padding(4);
            this.txtCellRangeSince.Name = "txtCellRangeSince";
            this.txtCellRangeSince.Size = new System.Drawing.Size(107, 22);
            this.txtCellRangeSince.TabIndex = 29;
            this.txtCellRangeSince.ToolTip = "Номер листа, на котором находится информация по арендной плате";
            this.txtCellRangeSince.Visible = false;
            // 
            // checkEditCellRange
            // 
            this.checkEditCellRange.Location = new System.Drawing.Point(197, 31);
            this.checkEditCellRange.Margin = new System.Windows.Forms.Padding(4);
            this.checkEditCellRange.Name = "checkEditCellRange";
            this.checkEditCellRange.Properties.Caption = "Указать диапазон ячеек";
            this.checkEditCellRange.Size = new System.Drawing.Size(201, 20);
            this.checkEditCellRange.TabIndex = 6;
            this.checkEditCellRange.CheckedChanged += new System.EventHandler(this.checkEditCellRange_CheckedChanged);
            // 
            // lblListNumber
            // 
            this.lblListNumber.AutoSize = true;
            this.lblListNumber.Location = new System.Drawing.Point(16, 33);
            this.lblListNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblListNumber.Name = "lblListNumber";
            this.lblListNumber.Size = new System.Drawing.Size(100, 16);
            this.lblListNumber.TabIndex = 4;
            this.lblListNumber.Text = "Номер листа:";
            // 
            // spinEditListNumber
            // 
            this.spinEditListNumber.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinEditListNumber.Location = new System.Drawing.Point(123, 30);
            this.spinEditListNumber.Margin = new System.Windows.Forms.Padding(4);
            this.spinEditListNumber.Name = "spinEditListNumber";
            this.spinEditListNumber.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEditListNumber.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.spinEditListNumber.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.spinEditListNumber.Size = new System.Drawing.Size(67, 22);
            this.spinEditListNumber.TabIndex = 3;
            this.spinEditListNumber.ToolTip = "Номер листа, на котором находится информация по арендной плате";
            // 
            // txtCellRangeTo
            // 
            this.txtCellRangeTo.EditValue = "";
            this.txtCellRangeTo.Location = new System.Drawing.Point(523, 30);
            this.txtCellRangeTo.Margin = new System.Windows.Forms.Padding(4);
            this.txtCellRangeTo.Name = "txtCellRangeTo";
            this.txtCellRangeTo.Size = new System.Drawing.Size(107, 22);
            this.txtCellRangeTo.TabIndex = 5;
            this.txtCellRangeTo.ToolTip = "Номер листа, на котором находится информация по арендной плате";
            this.txtCellRangeTo.Visible = false;
            // 
            // rgOption
            // 
            this.rgOption.Dock = System.Windows.Forms.DockStyle.Top;
            this.rgOption.Location = new System.Drawing.Point(0, 20);
            this.rgOption.Margin = new System.Windows.Forms.Padding(4);
            this.rgOption.Name = "rgOption";
            this.rgOption.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.rgOption.Properties.Columns = 2;
            this.rgOption.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.rgOption.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Клиенты (.xls|.xlsx|.csv)", true, "Option1")});
            this.rgOption.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.rgOption.Size = new System.Drawing.Size(645, 59);
            this.rgOption.TabIndex = 2;
            this.rgOption.SelectedIndexChanged += new System.EventHandler(this.rgOption_SelectedIndexChanged);
            // 
            // groupControlInfo
            // 
            this.groupControlInfo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupControlInfo.Controls.Add(this.progressBarControl);
            this.groupControlInfo.Controls.Add(this.txtLogger);
            this.groupControlInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControlInfo.GroupStyle = DevExpress.Utils.GroupStyle.Title;
            this.groupControlInfo.Location = new System.Drawing.Point(0, 208);
            this.groupControlInfo.Margin = new System.Windows.Forms.Padding(4);
            this.groupControlInfo.Name = "groupControlInfo";
            this.groupControlInfo.Size = new System.Drawing.Size(645, 298);
            this.groupControlInfo.TabIndex = 13;
            this.groupControlInfo.Text = "Информация";
            // 
            // progressBarControl
            // 
            this.progressBarControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBarControl.Location = new System.Drawing.Point(0, 276);
            this.progressBarControl.Margin = new System.Windows.Forms.Padding(4);
            this.progressBarControl.Name = "progressBarControl";
            this.progressBarControl.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.progressBarControl.Properties.Maximum = 0;
            this.progressBarControl.Properties.ShowTitle = true;
            this.progressBarControl.Properties.Step = 1;
            this.progressBarControl.Size = new System.Drawing.Size(645, 22);
            this.progressBarControl.TabIndex = 14;
            this.progressBarControl.Visible = false;
            // 
            // txtLogger
            // 
            this.txtLogger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLogger.Location = new System.Drawing.Point(0, 20);
            this.txtLogger.Margin = new System.Windows.Forms.Padding(4);
            this.txtLogger.Name = "txtLogger";
            this.txtLogger.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtLogger.Properties.ReadOnly = true;
            this.txtLogger.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtLogger.Size = new System.Drawing.Size(645, 278);
            this.txtLogger.TabIndex = 13;
            // 
            // ImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 506);
            this.Controls.Add(this.groupControlInfo);
            this.Controls.Add(this.groupControlPath);
            this.Controls.Add(this.lbl_Out);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ImportForm";
            this.Text = "Импорт";
            ((System.ComponentModel.ISupportInitialize)(this.txtPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlPath)).EndInit();
            this.groupControlPath.ResumeLayout(false);
            this.groupControlPath.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlSettings)).EndInit();
            this.groupControlSettings.ResumeLayout(false);
            this.groupControlSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCellRangeSince.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditCellRange.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditListNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCellRangeTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgOption.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlInfo)).EndInit();
            this.groupControlInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLogger.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.ButtonEdit txtPath;
        private DevExpress.XtraEditors.LabelControl lbl_Out;
        private DevExpress.XtraBars.BarEditItem barProgress;
        private DevExpress.XtraEditors.GroupControl groupControlPath;
        private DevExpress.XtraEditors.RadioGroup rgOption;
        private DevExpress.XtraEditors.GroupControl groupControlInfo;
        private DevExpress.XtraEditors.ProgressBarControl progressBarControl;
        private DevExpress.XtraEditors.MemoEdit txtLogger;
        private System.Windows.Forms.Label lblListNumber;
        private DevExpress.XtraEditors.SpinEdit spinEditListNumber;
        private DevExpress.XtraEditors.SimpleButton btnStart;
        private DevExpress.XtraEditors.GroupControl groupControlSettings;
        private DevExpress.XtraEditors.CheckEdit checkEditCellRange;
        private DevExpress.XtraEditors.TextEdit txtCellRangeTo;
        private DevExpress.XtraEditors.TextEdit txtCellRangeSince;
    }
}