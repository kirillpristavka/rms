namespace RMS.UI.Forms.ReferenceBooks
{
    partial class CustomerSettingsEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomerSettingsEdit));
            this.panelFooter = new DevExpress.XtraEditors.PanelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.gpHeader = new DevExpress.XtraEditors.GroupControl();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.memoDescription = new DevExpress.XtraEditors.MemoEdit();
            this.propertyGridCustomerSettings = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelFooter)).BeginInit();
            this.panelFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gpHeader)).BeginInit();
            this.gpHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridCustomerSettings)).BeginInit();
            this.SuspendLayout();
            // 
            // panelFooter
            // 
            this.panelFooter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelFooter.Controls.Add(this.btnSave);
            this.panelFooter.Controls.Add(this.btnCancel);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 610);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(719, 50);
            this.panelFooter.TabIndex = 10;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(606, 9);
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
            this.btnCancel.Location = new System.Drawing.Point(498, 9);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // gpHeader
            // 
            this.gpHeader.AutoSize = true;
            this.gpHeader.Controls.Add(this.lblDescription);
            this.gpHeader.Controls.Add(this.lblName);
            this.gpHeader.Controls.Add(this.txtName);
            this.gpHeader.Controls.Add(this.memoDescription);
            this.gpHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpHeader.GroupStyle = DevExpress.Utils.GroupStyle.Title;
            this.gpHeader.Location = new System.Drawing.Point(0, 0);
            this.gpHeader.Name = "gpHeader";
            this.gpHeader.Size = new System.Drawing.Size(719, 133);
            this.gpHeader.TabIndex = 11;
            this.gpHeader.Text = "Настройка отображения таблицы [Клиенты]";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(61, 68);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(80, 16);
            this.lblDescription.TabIndex = 7;
            this.lblDescription.Text = "Описание:";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(29, 41);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(112, 16);
            this.lblName.TabIndex = 5;
            this.lblName.Text = "Наименование:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(164, 38);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(536, 22);
            this.txtName.TabIndex = 6;
            // 
            // memoDescription
            // 
            this.memoDescription.Location = new System.Drawing.Point(164, 66);
            this.memoDescription.Name = "memoDescription";
            this.memoDescription.Size = new System.Drawing.Size(536, 44);
            this.memoDescription.TabIndex = 8;
            // 
            // propertyGridCustomerSettings
            // 
            this.propertyGridCustomerSettings.Appearance.Category.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(226)))), ((int)(((byte)(216)))));
            this.propertyGridCustomerSettings.Appearance.Category.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(197)))), ((int)(((byte)(180)))));
            this.propertyGridCustomerSettings.Appearance.Category.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.propertyGridCustomerSettings.Appearance.Category.ForeColor = System.Drawing.Color.Black;
            this.propertyGridCustomerSettings.Appearance.Category.Options.UseBackColor = true;
            this.propertyGridCustomerSettings.Appearance.Category.Options.UseBorderColor = true;
            this.propertyGridCustomerSettings.Appearance.Category.Options.UseFont = true;
            this.propertyGridCustomerSettings.Appearance.Category.Options.UseForeColor = true;
            this.propertyGridCustomerSettings.Appearance.CategoryExpandButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(209)))), ((int)(((byte)(188)))));
            this.propertyGridCustomerSettings.Appearance.CategoryExpandButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(209)))), ((int)(((byte)(188)))));
            this.propertyGridCustomerSettings.Appearance.CategoryExpandButton.Options.UseBackColor = true;
            this.propertyGridCustomerSettings.Appearance.CategoryExpandButton.Options.UseBorderColor = true;
            this.propertyGridCustomerSettings.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(244)))), ((int)(((byte)(236)))));
            this.propertyGridCustomerSettings.Appearance.Empty.BackColor2 = System.Drawing.Color.White;
            this.propertyGridCustomerSettings.Appearance.Empty.Options.UseBackColor = true;
            this.propertyGridCustomerSettings.Appearance.ExpandButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(220)))), ((int)(((byte)(204)))));
            this.propertyGridCustomerSettings.Appearance.ExpandButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(220)))), ((int)(((byte)(204)))));
            this.propertyGridCustomerSettings.Appearance.ExpandButton.Options.UseBackColor = true;
            this.propertyGridCustomerSettings.Appearance.ExpandButton.Options.UseBorderColor = true;
            this.propertyGridCustomerSettings.Appearance.FocusedCell.BackColor = System.Drawing.Color.White;
            this.propertyGridCustomerSettings.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.propertyGridCustomerSettings.Appearance.FocusedCell.Options.UseBackColor = true;
            this.propertyGridCustomerSettings.Appearance.FocusedCell.Options.UseForeColor = true;
            this.propertyGridCustomerSettings.Appearance.FocusedRecord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.propertyGridCustomerSettings.Appearance.FocusedRecord.Options.UseBackColor = true;
            this.propertyGridCustomerSettings.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(171)))), ((int)(((byte)(177)))));
            this.propertyGridCustomerSettings.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
            this.propertyGridCustomerSettings.Appearance.FocusedRow.Options.UseBackColor = true;
            this.propertyGridCustomerSettings.Appearance.FocusedRow.Options.UseForeColor = true;
            this.propertyGridCustomerSettings.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(211)))), ((int)(((byte)(215)))));
            this.propertyGridCustomerSettings.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(130)))), ((int)(((byte)(134)))));
            this.propertyGridCustomerSettings.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.propertyGridCustomerSettings.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.propertyGridCustomerSettings.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(197)))), ((int)(((byte)(180)))));
            this.propertyGridCustomerSettings.Appearance.HorzLine.Options.UseBackColor = true;
            this.propertyGridCustomerSettings.Appearance.RecordValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(244)))), ((int)(((byte)(236)))));
            this.propertyGridCustomerSettings.Appearance.RecordValue.ForeColor = System.Drawing.Color.Black;
            this.propertyGridCustomerSettings.Appearance.RecordValue.Options.UseBackColor = true;
            this.propertyGridCustomerSettings.Appearance.RecordValue.Options.UseForeColor = true;
            this.propertyGridCustomerSettings.Appearance.RowHeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(234)))), ((int)(((byte)(221)))));
            this.propertyGridCustomerSettings.Appearance.RowHeaderPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(234)))), ((int)(((byte)(221)))));
            this.propertyGridCustomerSettings.Appearance.RowHeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.propertyGridCustomerSettings.Appearance.RowHeaderPanel.Options.UseBackColor = true;
            this.propertyGridCustomerSettings.Appearance.RowHeaderPanel.Options.UseBorderColor = true;
            this.propertyGridCustomerSettings.Appearance.RowHeaderPanel.Options.UseForeColor = true;
            this.propertyGridCustomerSettings.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(197)))), ((int)(((byte)(180)))));
            this.propertyGridCustomerSettings.Appearance.VertLine.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(136)))), ((int)(((byte)(122)))));
            this.propertyGridCustomerSettings.Appearance.VertLine.Options.UseBackColor = true;
            this.propertyGridCustomerSettings.Appearance.VertLine.Options.UseBorderColor = true;
            this.propertyGridCustomerSettings.AutoGenerateRows = false;
            this.propertyGridCustomerSettings.BandsInterval = 3;
            this.propertyGridCustomerSettings.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.propertyGridCustomerSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridCustomerSettings.Location = new System.Drawing.Point(0, 133);
            this.propertyGridCustomerSettings.Margin = new System.Windows.Forms.Padding(4);
            this.propertyGridCustomerSettings.Name = "propertyGridCustomerSettings";
            this.propertyGridCustomerSettings.OptionsView.FixedLineWidth = 3;
            this.propertyGridCustomerSettings.OptionsView.MinRowAutoHeight = 12;
            this.propertyGridCustomerSettings.RecordWidth = 130;
            this.propertyGridCustomerSettings.RowHeaderWidth = 70;
            this.propertyGridCustomerSettings.Size = new System.Drawing.Size(719, 477);
            this.propertyGridCustomerSettings.TabIndex = 12;
            // 
            // CustomerSettingsEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 660);
            this.Controls.Add(this.propertyGridCustomerSettings);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.gpHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CustomerSettingsEdit";
            this.Text = "Отображения таблицы [Клиенты]";
            this.Load += new System.EventHandler(this.PositionEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelFooter)).EndInit();
            this.panelFooter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gpHeader)).EndInit();
            this.gpHeader.ResumeLayout(false);
            this.gpHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridCustomerSettings)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.PanelControl panelFooter;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.GroupControl gpHeader;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblName;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.MemoEdit memoDescription;
        private DevExpress.XtraVerticalGrid.PropertyGridControl propertyGridCustomerSettings;
    }
}