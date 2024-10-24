namespace RMS.UI.Forms.Taxes
{
    partial class TaxImportEAEUEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaxImportEAEUEdit));
            this.lblDate = new DevExpress.XtraEditors.LabelControl();
            this.dateDate = new DevExpress.XtraEditors.DateEdit();
            this.gpComment = new DevExpress.XtraEditors.GroupControl();
            this.memoComment = new DevExpress.XtraEditors.MemoEdit();
            this.panelControlFooter = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnChronicle = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.lblAvailability = new DevExpress.XtraEditors.LabelControl();
            this.cmbAvailability = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblCustomer = new DevExpress.XtraEditors.LabelControl();
            this.btnCustomer = new DevExpress.XtraEditors.ButtonEdit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpComment)).BeginInit();
            this.gpComment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlFooter)).BeginInit();
            this.panelControlFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAvailability.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCustomer.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDate
            // 
            this.lblDate.Location = new System.Drawing.Point(534, 48);
            this.lblDate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(46, 18);
            this.lblDate.TabIndex = 3;
            this.lblDate.Text = "Дата:";
            // 
            // dateDate
            // 
            this.dateDate.EditValue = null;
            this.dateDate.Location = new System.Drawing.Point(590, 45);
            this.dateDate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dateDate.Name = "dateDate";
            this.dateDate.Properties.Appearance.Options.UseTextOptions = true;
            this.dateDate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dateDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.dateDate.Size = new System.Drawing.Size(125, 24);
            this.dateDate.TabIndex = 4;
            this.dateDate.TabStop = false;
            // 
            // gpComment
            // 
            this.gpComment.Controls.Add(this.memoComment);
            this.gpComment.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gpComment.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.gpComment.Location = new System.Drawing.Point(0, 82);
            this.gpComment.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gpComment.Name = "gpComment";
            this.gpComment.Size = new System.Drawing.Size(730, 70);
            this.gpComment.TabIndex = 16;
            this.gpComment.Text = "Комментарий";
            // 
            // memoComment
            // 
            this.memoComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoComment.EditValue = "";
            this.memoComment.Location = new System.Drawing.Point(2, 26);
            this.memoComment.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.memoComment.Name = "memoComment";
            this.memoComment.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.memoComment.Size = new System.Drawing.Size(726, 42);
            this.memoComment.TabIndex = 12;
            // 
            // panelControlFooter
            // 
            this.panelControlFooter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControlFooter.Controls.Add(this.btnCancel);
            this.panelControlFooter.Controls.Add(this.btnChronicle);
            this.panelControlFooter.Controls.Add(this.btnSave);
            this.panelControlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControlFooter.Location = new System.Drawing.Point(0, 152);
            this.panelControlFooter.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panelControlFooter.Name = "panelControlFooter";
            this.panelControlFooter.Size = new System.Drawing.Size(730, 39);
            this.panelControlFooter.TabIndex = 17;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(590, 7);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(125, 26);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnChronicle
            // 
            this.btnChronicle.Location = new System.Drawing.Point(4, 7);
            this.btnChronicle.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnChronicle.Name = "btnChronicle";
            this.btnChronicle.Size = new System.Drawing.Size(250, 26);
            this.btnChronicle.TabIndex = 5;
            this.btnChronicle.Text = "История изменений";
            this.btnChronicle.Click += new System.EventHandler(this.btnChronicle_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(458, 7);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(125, 26);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblAvailability
            // 
            this.lblAvailability.Location = new System.Drawing.Point(15, 48);
            this.lblAvailability.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.lblAvailability.Name = "lblAvailability";
            this.lblAvailability.Size = new System.Drawing.Size(79, 18);
            this.lblAvailability.TabIndex = 19;
            this.lblAvailability.Text = "Наличие:";
            // 
            // cmbAvailability
            // 
            this.cmbAvailability.Location = new System.Drawing.Point(121, 45);
            this.cmbAvailability.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbAvailability.Name = "cmbAvailability";
            this.cmbAvailability.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbAvailability.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbAvailability.Size = new System.Drawing.Size(125, 24);
            this.cmbAvailability.TabIndex = 18;
            // 
            // lblCustomer
            // 
            this.lblCustomer.Location = new System.Drawing.Point(28, 17);
            this.lblCustomer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(65, 18);
            this.lblCustomer.TabIndex = 20;
            this.lblCustomer.Text = "Клиент:";
            // 
            // btnCustomer
            // 
            this.btnCustomer.Location = new System.Drawing.Point(121, 14);
            this.btnCustomer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCustomer.Name = "btnCustomer";
            this.btnCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnCustomer.Properties.ReadOnly = true;
            this.btnCustomer.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnCustomer.Size = new System.Drawing.Size(594, 24);
            this.btnCustomer.TabIndex = 21;
            this.btnCustomer.TabStop = false;
            this.btnCustomer.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnCustomer_ButtonPressed);
            // 
            // TaxImportEAEUEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 191);
            this.Controls.Add(this.lblCustomer);
            this.Controls.Add(this.btnCustomer);
            this.Controls.Add(this.lblAvailability);
            this.Controls.Add(this.cmbAvailability);
            this.Controls.Add(this.gpComment);
            this.Controls.Add(this.panelControlFooter);
            this.Controls.Add(this.dateDate);
            this.Controls.Add(this.lblDate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "TaxImportEAEUEdit";
            this.Text = "Импорт ЕАЭС";
            this.Load += new System.EventHandler(this.Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dateDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpComment)).EndInit();
            this.gpComment.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlFooter)).EndInit();
            this.panelControlFooter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbAvailability.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCustomer.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.LabelControl lblDate;
        private DevExpress.XtraEditors.DateEdit dateDate;
        private DevExpress.XtraEditors.GroupControl gpComment;
        private DevExpress.XtraEditors.MemoEdit memoComment;
        private DevExpress.XtraEditors.PanelControl panelControlFooter;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnChronicle;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.LabelControl lblAvailability;
        private DevExpress.XtraEditors.ComboBoxEdit cmbAvailability;
        private DevExpress.XtraEditors.LabelControl lblCustomer;
        private DevExpress.XtraEditors.ButtonEdit btnCustomer;
    }
}