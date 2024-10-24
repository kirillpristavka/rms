namespace RMS.UI.Forms.Directories
{
    partial class AddressFIASEdit
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddressFIASEdit));
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.txtAddress = new DevExpress.XtraEditors.LookUpEdit();
            this.gpAddress = new DevExpress.XtraEditors.GroupControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.checkIsExpress = new DevExpress.XtraEditors.CheckEdit();
            this.checkIsActual = new DevExpress.XtraEditors.CheckEdit();
            this.checkIsLegal = new DevExpress.XtraEditors.CheckEdit();
            this.panelFooter = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpAddress)).BeginInit();
            this.gpAddress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsExpress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsActual.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsLegal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelFooter)).BeginInit();
            this.panelFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(452, 6);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(109, 26);
            this.btnSave.TabIndex = 57;
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(569, 6);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(109, 26);
            this.btnCancel.TabIndex = 56;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtAddress
            // 
            this.txtAddress.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtAddress.Location = new System.Drawing.Point(0, 25);
            this.txtAddress.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search)});
            this.txtAddress.Properties.NullText = "";
            this.txtAddress.Properties.PopupSizeable = false;
            this.txtAddress.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.txtAddress.Size = new System.Drawing.Size(684, 24);
            this.txtAddress.TabIndex = 61;
            this.txtAddress.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtAddress_ButtonPressed);
            // 
            // gpAddress
            // 
            this.gpAddress.AutoSize = true;
            this.gpAddress.Controls.Add(this.txtAddress);
            this.gpAddress.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpAddress.GroupStyle = DevExpress.Utils.GroupStyle.Title;
            this.gpAddress.Location = new System.Drawing.Point(0, 0);
            this.gpAddress.Name = "gpAddress";
            this.gpAddress.Size = new System.Drawing.Size(684, 49);
            this.gpAddress.TabIndex = 62;
            this.gpAddress.Text = "Адрес ФИАС";
            // 
            // groupControl1
            // 
            this.groupControl1.AutoSize = true;
            this.groupControl1.Controls.Add(this.checkIsExpress);
            this.groupControl1.Controls.Add(this.checkIsActual);
            this.groupControl1.Controls.Add(this.checkIsLegal);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.GroupStyle = DevExpress.Utils.GroupStyle.Card;
            this.groupControl1.Location = new System.Drawing.Point(0, 49);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(684, 94);
            this.groupControl1.TabIndex = 63;
            // 
            // checkIsExpress
            // 
            this.checkIsExpress.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkIsExpress.Location = new System.Drawing.Point(2, 70);
            this.checkIsExpress.Name = "checkIsExpress";
            this.checkIsExpress.Properties.Caption = "Для курьера";
            this.checkIsExpress.Size = new System.Drawing.Size(680, 22);
            this.checkIsExpress.TabIndex = 2;
            // 
            // checkIsActual
            // 
            this.checkIsActual.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkIsActual.Location = new System.Drawing.Point(2, 48);
            this.checkIsActual.Name = "checkIsActual";
            this.checkIsActual.Properties.Caption = "Фактический адрес";
            this.checkIsActual.Size = new System.Drawing.Size(680, 22);
            this.checkIsActual.TabIndex = 1;
            // 
            // checkIsLegal
            // 
            this.checkIsLegal.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkIsLegal.Location = new System.Drawing.Point(2, 26);
            this.checkIsLegal.Name = "checkIsLegal";
            this.checkIsLegal.Properties.Caption = "Юридический адрес";
            this.checkIsLegal.Size = new System.Drawing.Size(680, 22);
            this.checkIsLegal.TabIndex = 0;
            // 
            // panelFooter
            // 
            this.panelFooter.AutoSize = true;
            this.panelFooter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelFooter.Controls.Add(this.btnCancel);
            this.panelFooter.Controls.Add(this.btnSave);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFooter.Location = new System.Drawing.Point(0, 143);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(684, 35);
            this.panelFooter.TabIndex = 64;
            // 
            // AddressFIASEdit
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(684, 185);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.gpAddress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "AddressFIASEdit";
            this.Text = "Адрес";
            this.Load += new System.EventHandler(this.AddressFIASEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpAddress)).EndInit();
            this.gpAddress.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkIsExpress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsActual.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsLegal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelFooter)).EndInit();
            this.panelFooter.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LookUpEdit txtAddress;
        private DevExpress.XtraEditors.GroupControl gpAddress;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.CheckEdit checkIsExpress;
        private DevExpress.XtraEditors.CheckEdit checkIsActual;
        private DevExpress.XtraEditors.CheckEdit checkIsLegal;
        private DevExpress.XtraEditors.PanelControl panelFooter;
    }
}