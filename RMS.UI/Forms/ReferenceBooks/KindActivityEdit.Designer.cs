namespace RMS.UI.Forms.ReferenceBooks
{
    partial class KindActivityEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KindActivityEdit));
            this.calcBasicReturn = new DevExpress.XtraEditors.CalcEdit();
            this.checkIsRegistrationAtLocationOrganization = new DevExpress.XtraEditors.CheckEdit();
            this.memoPhysicalIndicatorDescription = new DevExpress.XtraEditors.MemoEdit();
            this.btnPhysicalIndicator = new DevExpress.XtraEditors.ButtonEdit();
            this.xtraTabKindActivity = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPageOKVED2 = new DevExpress.XtraTab.XtraTabPage();
            this.lblName = new DevExpress.XtraEditors.LabelControl();
            this.lblClassOKVED2 = new DevExpress.XtraEditors.LabelControl();
            this.btnClassOKVED2 = new DevExpress.XtraEditors.ButtonEdit();
            this.txtName = new DevExpress.XtraEditors.MemoEdit();
            this.xtraTabPageAdditionally = new DevExpress.XtraTab.XtraTabPage();
            this.lblPhysicalIndicatorDescription = new DevExpress.XtraEditors.LabelControl();
            this.lblPhysicalIndicator = new DevExpress.XtraEditors.LabelControl();
            this.lblBasicReturn = new DevExpress.XtraEditors.LabelControl();
            this.panelControlFooter = new DevExpress.XtraEditors.PanelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.calcBasicReturn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsRegistrationAtLocationOrganization.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoPhysicalIndicatorDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPhysicalIndicator.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabKindActivity)).BeginInit();
            this.xtraTabKindActivity.SuspendLayout();
            this.xtraTabPageOKVED2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnClassOKVED2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            this.xtraTabPageAdditionally.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlFooter)).BeginInit();
            this.panelControlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // calcBasicReturn
            // 
            this.calcBasicReturn.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.calcBasicReturn.Location = new System.Drawing.Point(242, 37);
            this.calcBasicReturn.Name = "calcBasicReturn";
            this.calcBasicReturn.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.calcBasicReturn.Properties.DisplayFormat.FormatString = "n";
            this.calcBasicReturn.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.calcBasicReturn.Properties.Mask.EditMask = "n";
            this.calcBasicReturn.Size = new System.Drawing.Size(165, 22);
            this.calcBasicReturn.TabIndex = 9;
            // 
            // checkIsRegistrationAtLocationOrganization
            // 
            this.checkIsRegistrationAtLocationOrganization.Location = new System.Drawing.Point(11, 11);
            this.checkIsRegistrationAtLocationOrganization.Name = "checkIsRegistrationAtLocationOrganization";
            this.checkIsRegistrationAtLocationOrganization.Properties.Caption = "Регистрация по месту нахождения организации";
            this.checkIsRegistrationAtLocationOrganization.Size = new System.Drawing.Size(598, 20);
            this.checkIsRegistrationAtLocationOrganization.TabIndex = 7;
            // 
            // memoPhysicalIndicatorDescription
            // 
            this.memoPhysicalIndicatorDescription.Location = new System.Drawing.Point(242, 95);
            this.memoPhysicalIndicatorDescription.Name = "memoPhysicalIndicatorDescription";
            this.memoPhysicalIndicatorDescription.Size = new System.Drawing.Size(329, 150);
            this.memoPhysicalIndicatorDescription.TabIndex = 13;
            // 
            // btnPhysicalIndicator
            // 
            this.btnPhysicalIndicator.Location = new System.Drawing.Point(242, 67);
            this.btnPhysicalIndicator.Name = "btnPhysicalIndicator";
            this.btnPhysicalIndicator.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.btnPhysicalIndicator.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnPhysicalIndicator.Size = new System.Drawing.Size(329, 22);
            this.btnPhysicalIndicator.TabIndex = 11;
            this.btnPhysicalIndicator.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnPhysicalIndicator_ButtonPressed);
            // 
            // xtraTabKindActivity
            // 
            this.xtraTabKindActivity.AppearancePage.HeaderActive.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.xtraTabKindActivity.AppearancePage.HeaderActive.Options.UseFont = true;
            this.xtraTabKindActivity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabKindActivity.Location = new System.Drawing.Point(0, 0);
            this.xtraTabKindActivity.Name = "xtraTabKindActivity";
            this.xtraTabKindActivity.PageImagePosition = DevExpress.XtraTab.TabPageImagePosition.None;
            this.xtraTabKindActivity.SelectedTabPage = this.xtraTabPageOKVED2;
            this.xtraTabKindActivity.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            this.xtraTabKindActivity.Size = new System.Drawing.Size(584, 145);
            this.xtraTabKindActivity.TabIndex = 91;
            this.xtraTabKindActivity.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageOKVED2,
            this.xtraTabPageAdditionally});
            // 
            // xtraTabPageOKVED2
            // 
            this.xtraTabPageOKVED2.Controls.Add(this.lblName);
            this.xtraTabPageOKVED2.Controls.Add(this.lblClassOKVED2);
            this.xtraTabPageOKVED2.Controls.Add(this.btnClassOKVED2);
            this.xtraTabPageOKVED2.Controls.Add(this.txtName);
            this.xtraTabPageOKVED2.Name = "xtraTabPageOKVED2";
            this.xtraTabPageOKVED2.Size = new System.Drawing.Size(582, 143);
            this.xtraTabPageOKVED2.Text = "ОКВЭД";
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(11, 41);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(104, 16);
            this.lblName.TabIndex = 18;
            this.lblName.Text = "Наименование:";
            // 
            // lblClassOKVED2
            // 
            this.lblClassOKVED2.Location = new System.Drawing.Point(85, 14);
            this.lblClassOKVED2.Name = "lblClassOKVED2";
            this.lblClassOKVED2.Size = new System.Drawing.Size(30, 16);
            this.lblClassOKVED2.TabIndex = 16;
            this.lblClassOKVED2.Text = "Код:";
            // 
            // btnClassOKVED2
            // 
            this.btnClassOKVED2.Location = new System.Drawing.Point(126, 11);
            this.btnClassOKVED2.Name = "btnClassOKVED2";
            this.btnClassOKVED2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.btnClassOKVED2.Size = new System.Drawing.Size(445, 22);
            this.btnClassOKVED2.TabIndex = 15;
            this.btnClassOKVED2.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnClassOKVED2_ButtonPressed);
            this.btnClassOKVED2.EditValueChanged += new System.EventHandler(this.btnClassOKVED2_EditValueChanged);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(126, 39);
            this.txtName.Name = "txtName";
            this.txtName.Properties.ReadOnly = true;
            this.txtName.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtName.Size = new System.Drawing.Size(445, 50);
            this.txtName.TabIndex = 10;
            // 
            // xtraTabPageAdditionally
            // 
            this.xtraTabPageAdditionally.Controls.Add(this.lblPhysicalIndicatorDescription);
            this.xtraTabPageAdditionally.Controls.Add(this.lblPhysicalIndicator);
            this.xtraTabPageAdditionally.Controls.Add(this.lblBasicReturn);
            this.xtraTabPageAdditionally.Controls.Add(this.btnPhysicalIndicator);
            this.xtraTabPageAdditionally.Controls.Add(this.memoPhysicalIndicatorDescription);
            this.xtraTabPageAdditionally.Controls.Add(this.calcBasicReturn);
            this.xtraTabPageAdditionally.Controls.Add(this.checkIsRegistrationAtLocationOrganization);
            this.xtraTabPageAdditionally.Name = "xtraTabPageAdditionally";
            this.xtraTabPageAdditionally.Size = new System.Drawing.Size(582, 358);
            this.xtraTabPageAdditionally.Text = "Дополнительно";
            // 
            // lblPhysicalIndicatorDescription
            // 
            this.lblPhysicalIndicatorDescription.Appearance.Options.UseTextOptions = true;
            this.lblPhysicalIndicatorDescription.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblPhysicalIndicatorDescription.Location = new System.Drawing.Point(62, 97);
            this.lblPhysicalIndicatorDescription.Name = "lblPhysicalIndicatorDescription";
            this.lblPhysicalIndicatorDescription.Size = new System.Drawing.Size(165, 32);
            this.lblPhysicalIndicatorDescription.TabIndex = 16;
            this.lblPhysicalIndicatorDescription.Text = "Подсказка физического \r\nпоказателя:";
            // 
            // lblPhysicalIndicator
            // 
            this.lblPhysicalIndicator.Location = new System.Drawing.Point(19, 70);
            this.lblPhysicalIndicator.Name = "lblPhysicalIndicator";
            this.lblPhysicalIndicator.Size = new System.Drawing.Size(208, 16);
            this.lblPhysicalIndicator.TabIndex = 15;
            this.lblPhysicalIndicator.Text = "Имя физического показателя:";
            // 
            // lblBasicReturn
            // 
            this.lblBasicReturn.Location = new System.Drawing.Point(84, 40);
            this.lblBasicReturn.Name = "lblBasicReturn";
            this.lblBasicReturn.Size = new System.Drawing.Size(143, 16);
            this.lblBasicReturn.TabIndex = 14;
            this.lblBasicReturn.Text = "Базовая доходность:";
            // 
            // panelControlFooter
            // 
            this.panelControlFooter.Controls.Add(this.btnSave);
            this.panelControlFooter.Controls.Add(this.simpleButton2);
            this.panelControlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControlFooter.Location = new System.Drawing.Point(0, 99);
            this.panelControlFooter.Name = "panelControlFooter";
            this.panelControlFooter.Size = new System.Drawing.Size(584, 46);
            this.panelControlFooter.TabIndex = 92;
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
            // simpleButton2
            // 
            this.simpleButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton2.Location = new System.Drawing.Point(492, 11);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(87, 23);
            this.simpleButton2.TabIndex = 34;
            this.simpleButton2.Text = "Отмена";
            this.simpleButton2.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // KindActivityEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 145);
            this.Controls.Add(this.panelControlFooter);
            this.Controls.Add(this.xtraTabKindActivity);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "KindActivityEdit";
            this.Text = "Вид деятельности";
            this.Load += new System.EventHandler(this.PositionEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.calcBasicReturn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsRegistrationAtLocationOrganization.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoPhysicalIndicatorDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPhysicalIndicator.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabKindActivity)).EndInit();
            this.xtraTabKindActivity.ResumeLayout(false);
            this.xtraTabPageOKVED2.ResumeLayout(false);
            this.xtraTabPageOKVED2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnClassOKVED2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            this.xtraTabPageAdditionally.ResumeLayout(false);
            this.xtraTabPageAdditionally.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlFooter)).EndInit();
            this.panelControlFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.CalcEdit calcBasicReturn;
        private DevExpress.XtraEditors.CheckEdit checkIsRegistrationAtLocationOrganization;
        private DevExpress.XtraEditors.MemoEdit memoPhysicalIndicatorDescription;
        private DevExpress.XtraEditors.ButtonEdit btnPhysicalIndicator;
        private DevExpress.XtraTab.XtraTabControl xtraTabKindActivity;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageOKVED2;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageAdditionally;
        private DevExpress.XtraEditors.PanelControl panelControlFooter;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.LabelControl lblPhysicalIndicatorDescription;
        private DevExpress.XtraEditors.LabelControl lblPhysicalIndicator;
        private DevExpress.XtraEditors.LabelControl lblBasicReturn;
        private DevExpress.XtraEditors.LabelControl lblClassOKVED2;
        private DevExpress.XtraEditors.LabelControl lblName;
        private DevExpress.XtraEditors.ButtonEdit btnClassOKVED2;
        private DevExpress.XtraEditors.MemoEdit txtName;
    }
}