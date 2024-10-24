namespace RMS.UI.Forms.Mail
{
    partial class LetterFilterEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LetterFilterEdit));
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.lblEmal = new System.Windows.Forms.Label();
            this.txtEmail = new DevExpress.XtraEditors.TextEdit();
            this.lblLetterCatalog = new System.Windows.Forms.Label();
            this.btnLetterCatalog = new DevExpress.XtraEditors.ButtonEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnLetterCatalog.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(213, 79);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(125, 26);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(346, 79);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(125, 26);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblEmal
            // 
            this.lblEmal.AutoSize = true;
            this.lblEmal.Location = new System.Drawing.Point(28, 15);
            this.lblEmal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEmal.Name = "lblEmal";
            this.lblEmal.Size = new System.Drawing.Size(65, 20);
            this.lblEmal.TabIndex = 9;
            this.lblEmal.Text = "Email:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(101, 12);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(370, 24);
            this.txtEmail.TabIndex = 10;
            // 
            // lblLetterCatalog
            // 
            this.lblLetterCatalog.AutoSize = true;
            this.lblLetterCatalog.Location = new System.Drawing.Point(7, 45);
            this.lblLetterCatalog.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLetterCatalog.Name = "lblLetterCatalog";
            this.lblLetterCatalog.Size = new System.Drawing.Size(86, 20);
            this.lblLetterCatalog.TabIndex = 12;
            this.lblLetterCatalog.Text = "Каталог:";
            // 
            // btnLetterCatalog
            // 
            this.btnLetterCatalog.Location = new System.Drawing.Point(101, 42);
            this.btnLetterCatalog.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnLetterCatalog.Name = "btnLetterCatalog";
            this.btnLetterCatalog.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnLetterCatalog.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnLetterCatalog.Size = new System.Drawing.Size(370, 24);
            this.btnLetterCatalog.TabIndex = 11;
            this.btnLetterCatalog.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnLetterCatalog_ButtonPressed);
            this.btnLetterCatalog.DoubleClick += new System.EventHandler(this.btnLetterCatalog_DoubleClick);
            // 
            // LetterFilterEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 117);
            this.Controls.Add(this.lblLetterCatalog);
            this.Controls.Add(this.lblEmal);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnLetterCatalog);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LetterFilterEdit";
            this.Text = "Фильтр для писем";
            this.Load += new System.EventHandler(this.Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnLetterCatalog.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private System.Windows.Forms.Label lblEmal;
        private DevExpress.XtraEditors.TextEdit txtEmail;
        private System.Windows.Forms.Label lblLetterCatalog;
        private DevExpress.XtraEditors.ButtonEdit btnLetterCatalog;
    }
}