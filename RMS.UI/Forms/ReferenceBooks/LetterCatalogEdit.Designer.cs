namespace RMS.UI.Forms.ReferenceBooks
{
    partial class LetterCatalogEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LetterCatalogEdit));
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.btnStaff = new DevExpress.XtraEditors.ButtonEdit();
            this.checkIsUseChild = new DevExpress.XtraEditors.CheckEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemName = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItemSave = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemCancel = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemStaff = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemIsUseChild = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
            this.layoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnStaff.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsUseChild.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemStaff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemIsUseChild)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(294, 96);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(137, 27);
            this.btnSave.StyleController = this.layoutControl;
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // layoutControl
            // 
            this.layoutControl.Controls.Add(this.btnCancel);
            this.layoutControl.Controls.Add(this.btnSave);
            this.layoutControl.Controls.Add(this.txtName);
            this.layoutControl.Controls.Add(this.btnStaff);
            this.layoutControl.Controls.Add(this.checkIsUseChild);
            this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl.Location = new System.Drawing.Point(0, 0);
            this.layoutControl.Name = "layoutControl";
            this.layoutControl.Root = this.Root;
            this.layoutControl.Size = new System.Drawing.Size(584, 153);
            this.layoutControl.TabIndex = 7;
            this.layoutControl.Text = "layoutControl1";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(435, 96);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(137, 27);
            this.btnCancel.StyleController = this.layoutControl;
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(147, 12);
            this.txtName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(425, 24);
            this.txtName.StyleController = this.layoutControl;
            this.txtName.TabIndex = 2;
            // 
            // btnStaff
            // 
            this.btnStaff.Location = new System.Drawing.Point(147, 40);
            this.btnStaff.Name = "btnStaff";
            this.btnStaff.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.btnStaff.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnStaff.Size = new System.Drawing.Size(425, 24);
            this.btnStaff.StyleController = this.layoutControl;
            this.btnStaff.TabIndex = 7;
            this.btnStaff.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnStaff_ButtonPressed);
            // 
            // checkIsUseChild
            // 
            this.checkIsUseChild.Location = new System.Drawing.Point(12, 68);
            this.checkIsUseChild.Name = "checkIsUseChild";
            this.checkIsUseChild.Properties.Caption = "Применить к дочерним объектам";
            this.checkIsUseChild.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkIsUseChild.Size = new System.Drawing.Size(560, 24);
            this.checkIsUseChild.StyleController = this.layoutControl;
            this.checkIsUseChild.TabIndex = 8;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemName,
            this.emptySpaceItem1,
            this.layoutControlItemSave,
            this.layoutControlItemCancel,
            this.layoutControlItemStaff,
            this.layoutControlItemIsUseChild});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(584, 153);
            this.Root.TextVisible = false;
            // 
            // layoutControlItemName
            // 
            this.layoutControlItemName.Control = this.txtName;
            this.layoutControlItemName.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemName.Name = "layoutControlItemName";
            this.layoutControlItemName.Size = new System.Drawing.Size(564, 28);
            this.layoutControlItemName.Text = "Наименование:";
            this.layoutControlItemName.TextSize = new System.Drawing.Size(132, 18);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 84);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(282, 49);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItemSave
            // 
            this.layoutControlItemSave.Control = this.btnSave;
            this.layoutControlItemSave.Location = new System.Drawing.Point(282, 84);
            this.layoutControlItemSave.Name = "layoutControlItemSave";
            this.layoutControlItemSave.Size = new System.Drawing.Size(141, 49);
            this.layoutControlItemSave.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemSave.TextVisible = false;
            // 
            // layoutControlItemCancel
            // 
            this.layoutControlItemCancel.Control = this.btnCancel;
            this.layoutControlItemCancel.Location = new System.Drawing.Point(423, 84);
            this.layoutControlItemCancel.Name = "layoutControlItemCancel";
            this.layoutControlItemCancel.Size = new System.Drawing.Size(141, 49);
            this.layoutControlItemCancel.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemCancel.TextVisible = false;
            // 
            // layoutControlItemStaff
            // 
            this.layoutControlItemStaff.Control = this.btnStaff;
            this.layoutControlItemStaff.Location = new System.Drawing.Point(0, 28);
            this.layoutControlItemStaff.Name = "layoutControlItemStaff";
            this.layoutControlItemStaff.Size = new System.Drawing.Size(564, 28);
            this.layoutControlItemStaff.Text = "Ответственный:";
            this.layoutControlItemStaff.TextSize = new System.Drawing.Size(132, 18);
            // 
            // layoutControlItemIsUseChild
            // 
            this.layoutControlItemIsUseChild.Control = this.checkIsUseChild;
            this.layoutControlItemIsUseChild.Location = new System.Drawing.Point(0, 56);
            this.layoutControlItemIsUseChild.Name = "layoutControlItemIsUseChild";
            this.layoutControlItemIsUseChild.Size = new System.Drawing.Size(564, 28);
            this.layoutControlItemIsUseChild.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemIsUseChild.TextVisible = false;
            // 
            // LetterCatalogEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 153);
            this.Controls.Add(this.layoutControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "LetterCatalogEdit";
            this.Text = "Должность";
            this.Load += new System.EventHandler(this.PositionEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
            this.layoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnStaff.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsUseChild.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemStaff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemIsUseChild)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraLayout.LayoutControl layoutControl;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.ButtonEdit btnStaff;
        private DevExpress.XtraEditors.CheckEdit checkIsUseChild;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemName;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemStaff;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemIsUseChild;
    }
}