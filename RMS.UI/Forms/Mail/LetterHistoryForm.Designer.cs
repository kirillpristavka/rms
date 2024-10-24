namespace RMS.UI.Forms.Mail
{
    partial class LetterHistoryForm
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
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LetterHistoryForm));
            this.gridControlLetters = new DevExpress.XtraGrid.GridControl();
            this.gridViewLetters = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.richLetter = new DevExpress.XtraRichEdit.RichEditControl();
            this.txtTopic = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlLetter = new DevExpress.XtraLayout.LayoutControl();
            this.checkedListCustomerEmail = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.btnLetterSender = new DevExpress.XtraEditors.ButtonEdit();
            this.txtLetterRecipient = new DevExpress.XtraEditors.TextEdit();
            this.btnCustomer = new DevExpress.XtraEditors.ButtonEdit();
            this.txtEmail = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemCustomer = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemEmail = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem2 = new DevExpress.XtraLayout.SplitterItem();
            this.imageCollectionTypeLetter = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlLetters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLetters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTopic.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlLetter)).BeginInit();
            this.layoutControlLetter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListCustomerEmail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnLetterSender.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLetterRecipient.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemEmail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionTypeLetter)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlLetters
            // 
            this.gridControlLetters.Location = new System.Drawing.Point(294, 12);
            this.gridControlLetters.MainView = this.gridViewLetters;
            this.gridControlLetters.Name = "gridControlLetters";
            this.gridControlLetters.Size = new System.Drawing.Size(702, 191);
            this.gridControlLetters.TabIndex = 38;
            this.gridControlLetters.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewLetters});
            // 
            // gridViewLetters
            // 
            this.gridViewLetters.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridViewLetters.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridViewLetters.GridControl = this.gridControlLetters;
            this.gridViewLetters.Name = "gridViewLetters";
            this.gridViewLetters.OptionsBehavior.Editable = false;
            this.gridViewLetters.OptionsSelection.MultiSelect = true;
            this.gridViewLetters.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewLetters.OptionsView.ShowGroupPanel = false;
            this.gridViewLetters.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewLetters_FocusedRowChanged);
            this.gridViewLetters.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridViewLetters_MouseDown);
            // 
            // richLetter
            // 
            this.richLetter.Location = new System.Drawing.Point(294, 297);
            this.richLetter.Name = "richLetter";
            this.richLetter.Options.HorizontalRuler.Visibility = DevExpress.XtraRichEdit.RichEditRulerVisibility.Hidden;
            this.richLetter.Options.VerticalRuler.Visibility = DevExpress.XtraRichEdit.RichEditRulerVisibility.Hidden;
            this.richLetter.Size = new System.Drawing.Size(702, 419);
            this.richLetter.TabIndex = 105;
            this.richLetter.DocumentLoaded += new System.EventHandler(this.richLetter_DocumentLoaded);
            // 
            // txtTopic
            // 
            this.txtTopic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTopic.Location = new System.Drawing.Point(350, 271);
            this.txtTopic.Name = "txtTopic";
            this.txtTopic.Properties.ReadOnly = true;
            this.txtTopic.Size = new System.Drawing.Size(646, 22);
            this.txtTopic.StyleController = this.layoutControlLetter;
            this.txtTopic.TabIndex = 18;
            // 
            // layoutControlLetter
            // 
            this.layoutControlLetter.Controls.Add(this.checkedListCustomerEmail);
            this.layoutControlLetter.Controls.Add(this.btnLetterSender);
            this.layoutControlLetter.Controls.Add(this.txtTopic);
            this.layoutControlLetter.Controls.Add(this.richLetter);
            this.layoutControlLetter.Controls.Add(this.txtLetterRecipient);
            this.layoutControlLetter.Controls.Add(this.gridControlLetters);
            this.layoutControlLetter.Controls.Add(this.btnCustomer);
            this.layoutControlLetter.Controls.Add(this.txtEmail);
            this.layoutControlLetter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlLetter.Location = new System.Drawing.Point(0, 0);
            this.layoutControlLetter.Name = "layoutControlLetter";
            this.layoutControlLetter.Root = this.Root;
            this.layoutControlLetter.Size = new System.Drawing.Size(1008, 728);
            this.layoutControlLetter.TabIndex = 47;
            this.layoutControlLetter.Text = "layoutControl1";
            // 
            // checkedListCustomerEmail
            // 
            this.checkedListCustomerEmail.CheckOnClick = true;
            this.checkedListCustomerEmail.Location = new System.Drawing.Point(12, 64);
            this.checkedListCustomerEmail.Name = "checkedListCustomerEmail";
            this.checkedListCustomerEmail.Size = new System.Drawing.Size(266, 652);
            this.checkedListCustomerEmail.StyleController = this.layoutControlLetter;
            this.checkedListCustomerEmail.TabIndex = 109;
            // 
            // btnLetterSender
            // 
            this.btnLetterSender.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLetterSender.Location = new System.Drawing.Point(350, 219);
            this.btnLetterSender.Name = "btnLetterSender";
            this.btnLetterSender.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, false, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.btnLetterSender.Properties.ReadOnly = true;
            this.btnLetterSender.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnLetterSender.Size = new System.Drawing.Size(646, 22);
            this.btnLetterSender.StyleController = this.layoutControlLetter;
            this.btnLetterSender.TabIndex = 16;
            // 
            // txtLetterRecipient
            // 
            this.txtLetterRecipient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLetterRecipient.Location = new System.Drawing.Point(350, 245);
            this.txtLetterRecipient.Name = "txtLetterRecipient";
            this.txtLetterRecipient.Properties.ReadOnly = true;
            this.txtLetterRecipient.Size = new System.Drawing.Size(646, 22);
            this.txtLetterRecipient.StyleController = this.layoutControlLetter;
            this.txtLetterRecipient.TabIndex = 17;
            // 
            // btnCustomer
            // 
            this.btnCustomer.Location = new System.Drawing.Point(68, 38);
            this.btnCustomer.Name = "btnCustomer";
            this.btnCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.btnCustomer.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnCustomer.Size = new System.Drawing.Size(210, 22);
            this.btnCustomer.StyleController = this.layoutControlLetter;
            this.btnCustomer.TabIndex = 106;
            this.btnCustomer.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnCustomer_ButtonPressed);
            this.btnCustomer.EditValueChanged += new System.EventHandler(this.btnCustomer_EditValueChanged);
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(68, 12);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(210, 22);
            this.txtEmail.StyleController = this.layoutControlLetter;
            this.txtEmail.TabIndex = 107;
            this.txtEmail.EditValueChanged += new System.EventHandler(this.txtEmail_EditValueChanged);
            // 
            // Root
            // 
            this.Root.AppearanceItemCaption.Options.UseTextOptions = true;
            this.Root.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.splitterItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItemCustomer,
            this.layoutControlItemEmail,
            this.layoutControlItem9,
            this.splitterItem2});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1008, 728);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControlLetters;
            this.layoutControlItem1.Location = new System.Drawing.Point(282, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(706, 195);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new System.Drawing.Point(282, 195);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(706, 12);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.richLetter;
            this.layoutControlItem2.Location = new System.Drawing.Point(282, 285);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(706, 423);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtTopic;
            this.layoutControlItem3.Location = new System.Drawing.Point(282, 259);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(706, 26);
            this.layoutControlItem3.Text = "Тема:";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(53, 16);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnLetterSender;
            this.layoutControlItem4.Location = new System.Drawing.Point(282, 207);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(706, 26);
            this.layoutControlItem4.Text = "От:";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(53, 16);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.txtLetterRecipient;
            this.layoutControlItem5.Location = new System.Drawing.Point(282, 233);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(706, 26);
            this.layoutControlItem5.Text = "Кому:";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(53, 16);
            // 
            // layoutControlItemCustomer
            // 
            this.layoutControlItemCustomer.Control = this.btnCustomer;
            this.layoutControlItemCustomer.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItemCustomer.Name = "layoutControlItemCustomer";
            this.layoutControlItemCustomer.Size = new System.Drawing.Size(270, 26);
            this.layoutControlItemCustomer.Text = "Клиент:";
            this.layoutControlItemCustomer.TextSize = new System.Drawing.Size(53, 16);
            // 
            // layoutControlItemEmail
            // 
            this.layoutControlItemEmail.Control = this.txtEmail;
            this.layoutControlItemEmail.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemEmail.Name = "layoutControlItemEmail";
            this.layoutControlItemEmail.Size = new System.Drawing.Size(270, 26);
            this.layoutControlItemEmail.Text = "Email:";
            this.layoutControlItemEmail.TextSize = new System.Drawing.Size(53, 16);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.checkedListCustomerEmail;
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(270, 656);
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextVisible = false;
            // 
            // splitterItem2
            // 
            this.splitterItem2.AllowHotTrack = true;
            this.splitterItem2.Location = new System.Drawing.Point(270, 0);
            this.splitterItem2.Name = "splitterItem2";
            this.splitterItem2.Size = new System.Drawing.Size(12, 708);
            // 
            // imageCollectionTypeLetter
            // 
            this.imageCollectionTypeLetter.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollectionTypeLetter.ImageStream")));
            this.imageCollectionTypeLetter.Images.SetKeyName(0, "inbox_16x16.png");
            this.imageCollectionTypeLetter.Images.SetKeyName(1, "outbox_16x16.png");
            this.imageCollectionTypeLetter.Images.SetKeyName(2, "warning_16x16.png");
            this.imageCollectionTypeLetter.Images.SetKeyName(3, "trash_16x16.png");
            // 
            // LetterHistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 728);
            this.Controls.Add(this.layoutControlLetter);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LetterHistoryForm";
            this.Text = "История переписки";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.LetterForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlLetters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLetters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTopic.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlLetter)).EndInit();
            this.layoutControlLetter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkedListCustomerEmail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnLetterSender.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLetterRecipient.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemEmail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionTypeLetter)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.GridControl gridControlLetters;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewLetters;
        private DevExpress.XtraRichEdit.RichEditControl richLetter;
        private DevExpress.XtraEditors.TextEdit txtTopic;
        private DevExpress.XtraEditors.ButtonEdit btnLetterSender;
        private DevExpress.XtraEditors.TextEdit txtLetterRecipient;
        private DevExpress.XtraLayout.LayoutControl layoutControlLetter;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.CheckedListBoxControl checkedListCustomerEmail;
        private DevExpress.XtraEditors.ButtonEdit btnCustomer;
        private DevExpress.XtraEditors.TextEdit txtEmail;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCustomer;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemEmail;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.SplitterItem splitterItem2;
        private DevExpress.Utils.ImageCollection imageCollectionTypeLetter;
    }
}