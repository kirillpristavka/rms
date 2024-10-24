﻿namespace RMS.UI.Forms.ReferenceBooks
{
    partial class ArchiveFolderEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ArchiveFolderEdit));
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.lblDescription = new System.Windows.Forms.Label();
            this.memoDescription = new DevExpress.XtraEditors.MemoEdit();
            this.cmbPeriodArchiveFolder = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblPeriodArchiveFolder = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPeriodArchiveFolder.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(266, 159);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(372, 159);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
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
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(147, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(325, 22);
            this.txtName.TabIndex = 2;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(44, 42);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(80, 16);
            this.lblDescription.TabIndex = 3;
            this.lblDescription.Text = "Описание:";
            // 
            // memoDescription
            // 
            this.memoDescription.Location = new System.Drawing.Point(147, 40);
            this.memoDescription.Name = "memoDescription";
            this.memoDescription.Size = new System.Drawing.Size(325, 85);
            this.memoDescription.TabIndex = 4;
            // 
            // cmbPeriodArchiveFolder
            // 
            this.cmbPeriodArchiveFolder.EditValue = "";
            this.cmbPeriodArchiveFolder.Location = new System.Drawing.Point(147, 131);
            this.cmbPeriodArchiveFolder.Name = "cmbPeriodArchiveFolder";
            this.cmbPeriodArchiveFolder.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPeriodArchiveFolder.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbPeriodArchiveFolder.Size = new System.Drawing.Size(325, 22);
            this.cmbPeriodArchiveFolder.TabIndex = 7;
            // 
            // lblPeriodArchiveFolder
            // 
            this.lblPeriodArchiveFolder.AutoSize = true;
            this.lblPeriodArchiveFolder.Location = new System.Drawing.Point(61, 134);
            this.lblPeriodArchiveFolder.Name = "lblPeriodArchiveFolder";
            this.lblPeriodArchiveFolder.Size = new System.Drawing.Size(63, 16);
            this.lblPeriodArchiveFolder.TabIndex = 8;
            this.lblPeriodArchiveFolder.Text = "Период:";
            // 
            // ArchiveFolderEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 190);
            this.Controls.Add(this.lblPeriodArchiveFolder);
            this.Controls.Add(this.cmbPeriodArchiveFolder);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.memoDescription);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ArchiveFolderEdit";
            this.Text = "Архивные папки";
            this.Load += new System.EventHandler(this.PositionEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPeriodArchiveFolder.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private System.Windows.Forms.Label lblName;
        private DevExpress.XtraEditors.TextEdit txtName;
        private System.Windows.Forms.Label lblDescription;
        private DevExpress.XtraEditors.MemoEdit memoDescription;
        private DevExpress.XtraEditors.ComboBoxEdit cmbPeriodArchiveFolder;
        private System.Windows.Forms.Label lblPeriodArchiveFolder;
    }
}