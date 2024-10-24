namespace RMS.UI.Forms.ReferenceBooks
{
    partial class ClassOKVED2Edit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClassOKVED2Edit));
            this.panelControlFooter = new DevExpress.XtraEditors.PanelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.lblDescription = new DevExpress.XtraEditors.LabelControl();
            this.lblName = new DevExpress.XtraEditors.LabelControl();
            this.lblCode = new DevExpress.XtraEditors.LabelControl();
            this.memoDescription = new DevExpress.XtraEditors.MemoEdit();
            this.txtName = new DevExpress.XtraEditors.MemoEdit();
            this.lblSectionOKVED2 = new DevExpress.XtraEditors.LabelControl();
            this.btnSectionOKVED2 = new DevExpress.XtraEditors.ButtonEdit();
            this.txtCode = new DevExpress.XtraEditors.ButtonEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlFooter)).BeginInit();
            this.panelControlFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSectionOKVED2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControlFooter
            // 
            this.panelControlFooter.Controls.Add(this.btnSave);
            this.panelControlFooter.Controls.Add(this.btnCancel);
            this.panelControlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControlFooter.Location = new System.Drawing.Point(0, 263);
            this.panelControlFooter.Name = "panelControlFooter";
            this.panelControlFooter.Size = new System.Drawing.Size(584, 46);
            this.panelControlFooter.TabIndex = 93;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(399, 11);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 23);
            this.btnSave.TabIndex = 35;
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(492, 11);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 23);
            this.btnCancel.TabIndex = 34;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblDescription
            // 
            this.lblDescription.Location = new System.Drawing.Point(44, 99);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(72, 16);
            this.lblDescription.TabIndex = 99;
            this.lblDescription.Text = "Описание:";
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(12, 44);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(104, 16);
            this.lblName.TabIndex = 98;
            this.lblName.Text = "Наименование:";
            // 
            // lblCode
            // 
            this.lblCode.Location = new System.Drawing.Point(86, 16);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(30, 16);
            this.lblCode.TabIndex = 97;
            this.lblCode.Text = "Код:";
            // 
            // memoDescription
            // 
            this.memoDescription.EditValue = "";
            this.memoDescription.Location = new System.Drawing.Point(127, 96);
            this.memoDescription.Name = "memoDescription";
            this.memoDescription.Size = new System.Drawing.Size(445, 156);
            this.memoDescription.TabIndex = 96;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(127, 40);
            this.txtName.Name = "txtName";
            this.txtName.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtName.Size = new System.Drawing.Size(445, 50);
            this.txtName.TabIndex = 95;
            // 
            // lblSectionOKVED2
            // 
            this.lblSectionOKVED2.Location = new System.Drawing.Point(358, 16);
            this.lblSectionOKVED2.Name = "lblSectionOKVED2";
            this.lblSectionOKVED2.Size = new System.Drawing.Size(53, 16);
            this.lblSectionOKVED2.TabIndex = 100;
            this.lblSectionOKVED2.Text = "Раздел:";
            // 
            // btnSectionOKVED2
            // 
            this.btnSectionOKVED2.Location = new System.Drawing.Point(422, 13);
            this.btnSectionOKVED2.Name = "btnSectionOKVED2";
            this.btnSectionOKVED2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.btnSectionOKVED2.Size = new System.Drawing.Size(150, 22);
            this.btnSectionOKVED2.TabIndex = 101;
            this.btnSectionOKVED2.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnSectionOKVED2_ButtonPressed);
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(127, 13);
            this.txtCode.Name = "txtCode";
            this.txtCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search)});
            this.txtCode.Size = new System.Drawing.Size(200, 22);
            this.txtCode.TabIndex = 102;
            this.txtCode.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtCode_ButtonPressed);
            // 
            // ClassOKVED2Edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 309);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.lblSectionOKVED2);
            this.Controls.Add(this.btnSectionOKVED2);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblCode);
            this.Controls.Add(this.memoDescription);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.panelControlFooter);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ClassOKVED2Edit";
            this.Text = "Раздел ОКВЭД";
            this.Load += new System.EventHandler(this.ClassOKVED2Edit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlFooter)).EndInit();
            this.panelControlFooter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSectionOKVED2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.PanelControl panelControlFooter;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LabelControl lblDescription;
        private DevExpress.XtraEditors.LabelControl lblName;
        private DevExpress.XtraEditors.LabelControl lblCode;
        private DevExpress.XtraEditors.MemoEdit memoDescription;
        private DevExpress.XtraEditors.MemoEdit txtName;
        private DevExpress.XtraEditors.LabelControl lblSectionOKVED2;
        private DevExpress.XtraEditors.ButtonEdit btnSectionOKVED2;
        private DevExpress.XtraEditors.ButtonEdit txtCode;
    }
}