
namespace RMS.UI.Forms
{
    partial class ProgramEventEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgramEventEdit));
            this.layoutControlEvent = new DevExpress.XtraLayout.LayoutControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.memoDescription = new DevExpress.XtraEditors.MemoEdit();
            this.cmbTypeProgramEvent = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtNameModel = new DevExpress.XtraEditors.TextEdit();
            this.btnControlSystemObject = new DevExpress.XtraEditors.ButtonEdit();
            this.btnStaff = new DevExpress.XtraEditors.ButtonEdit();
            this.dateSince = new DevExpress.XtraEditors.DateEdit();
            this.dateTo = new DevExpress.XtraEditors.DateEdit();
            this.cmbActionProgramEvent = new DevExpress.XtraEditors.ComboBoxEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemName = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemDescription = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemNameModel = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemControlSystemObject = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemStaff = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemDateSince = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemDateTo = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemActionProgramEvent = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlEvent)).BeginInit();
            this.layoutControlEvent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTypeProgramEvent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNameModel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnControlSystemObject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnStaff.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateSince.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateSince.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbActionProgramEvent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDescription)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemNameModel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemControlSystemObject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemStaff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDateSince)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDateTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemActionProgramEvent)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlEvent
            // 
            this.layoutControlEvent.Controls.Add(this.btnSave);
            this.layoutControlEvent.Controls.Add(this.btnCancel);
            this.layoutControlEvent.Controls.Add(this.txtName);
            this.layoutControlEvent.Controls.Add(this.memoDescription);
            this.layoutControlEvent.Controls.Add(this.cmbTypeProgramEvent);
            this.layoutControlEvent.Controls.Add(this.txtNameModel);
            this.layoutControlEvent.Controls.Add(this.btnControlSystemObject);
            this.layoutControlEvent.Controls.Add(this.btnStaff);
            this.layoutControlEvent.Controls.Add(this.dateSince);
            this.layoutControlEvent.Controls.Add(this.dateTo);
            this.layoutControlEvent.Controls.Add(this.cmbActionProgramEvent);
            this.layoutControlEvent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlEvent.Location = new System.Drawing.Point(0, 0);
            this.layoutControlEvent.Name = "layoutControlEvent";
            this.layoutControlEvent.Root = this.Root;
            this.layoutControlEvent.Size = new System.Drawing.Size(534, 310);
            this.layoutControlEvent.TabIndex = 0;
            this.layoutControlEvent.Text = "layoutControl1";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(269, 276);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(124, 22);
            this.btnSave.StyleController = this.layoutControlEvent;
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(397, 276);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(125, 22);
            this.btnCancel.StyleController = this.layoutControlEvent;
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(119, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(403, 22);
            this.txtName.StyleController = this.layoutControlEvent;
            this.txtName.TabIndex = 6;
            // 
            // memoDescription
            // 
            this.memoDescription.Location = new System.Drawing.Point(119, 38);
            this.memoDescription.Name = "memoDescription";
            this.memoDescription.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.memoDescription.Size = new System.Drawing.Size(403, 78);
            this.memoDescription.StyleController = this.layoutControlEvent;
            this.memoDescription.TabIndex = 7;
            // 
            // cmbTypeProgramEvent
            // 
            this.cmbTypeProgramEvent.Location = new System.Drawing.Point(119, 146);
            this.cmbTypeProgramEvent.Name = "cmbTypeProgramEvent";
            this.cmbTypeProgramEvent.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbTypeProgramEvent.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbTypeProgramEvent.Size = new System.Drawing.Size(403, 22);
            this.cmbTypeProgramEvent.StyleController = this.layoutControlEvent;
            this.cmbTypeProgramEvent.TabIndex = 8;
            // 
            // txtNameModel
            // 
            this.txtNameModel.Location = new System.Drawing.Point(119, 198);
            this.txtNameModel.Name = "txtNameModel";
            this.txtNameModel.Properties.ReadOnly = true;
            this.txtNameModel.Size = new System.Drawing.Size(403, 22);
            this.txtNameModel.StyleController = this.layoutControlEvent;
            this.txtNameModel.TabIndex = 1;
            this.txtNameModel.TabStop = false;
            // 
            // btnControlSystemObject
            // 
            this.btnControlSystemObject.Location = new System.Drawing.Point(119, 224);
            this.btnControlSystemObject.Name = "btnControlSystemObject";
            this.btnControlSystemObject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search)});
            this.btnControlSystemObject.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnControlSystemObject.Size = new System.Drawing.Size(403, 22);
            this.btnControlSystemObject.StyleController = this.layoutControlEvent;
            this.btnControlSystemObject.TabIndex = 2;
            this.btnControlSystemObject.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnControlSystemObject_ButtonPressed);
            this.btnControlSystemObject.DoubleClick += new System.EventHandler(this.btnControlSystemObject_DoubleClick);
            // 
            // btnStaff
            // 
            this.btnStaff.Location = new System.Drawing.Point(119, 250);
            this.btnStaff.Name = "btnStaff";
            this.btnStaff.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnStaff.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnStaff.Size = new System.Drawing.Size(403, 22);
            this.btnStaff.StyleController = this.layoutControlEvent;
            this.btnStaff.TabIndex = 5;
            this.btnStaff.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnStaff_ButtonPressed);
            // 
            // dateSince
            // 
            this.dateSince.EditValue = null;
            this.dateSince.Location = new System.Drawing.Point(119, 120);
            this.dateSince.Name = "dateSince";
            this.dateSince.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateSince.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateSince.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.dateSince.Size = new System.Drawing.Size(146, 22);
            this.dateSince.StyleController = this.layoutControlEvent;
            this.dateSince.TabIndex = 3;
            // 
            // dateTo
            // 
            this.dateTo.EditValue = null;
            this.dateTo.Location = new System.Drawing.Point(376, 120);
            this.dateTo.Name = "dateTo";
            this.dateTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.dateTo.Size = new System.Drawing.Size(146, 22);
            this.dateTo.StyleController = this.layoutControlEvent;
            this.dateTo.TabIndex = 4;
            // 
            // cmbActionProgramEvent
            // 
            this.cmbActionProgramEvent.Location = new System.Drawing.Point(119, 172);
            this.cmbActionProgramEvent.Name = "cmbActionProgramEvent";
            this.cmbActionProgramEvent.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbActionProgramEvent.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbActionProgramEvent.Size = new System.Drawing.Size(403, 22);
            this.cmbActionProgramEvent.StyleController = this.layoutControlEvent;
            this.cmbActionProgramEvent.TabIndex = 8;
            // 
            // Root
            // 
            this.Root.AppearanceItemCaption.Options.UseTextOptions = true;
            this.Root.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem3,
            this.emptySpaceItem2,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItemName,
            this.layoutControlItemDescription,
            this.layoutControlItem5,
            this.layoutControlItemNameModel,
            this.layoutControlItemControlSystemObject,
            this.layoutControlItemStaff,
            this.layoutControlItemDateSince,
            this.layoutControlItemDateTo,
            this.layoutControlItemActionProgramEvent});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(534, 310);
            this.Root.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 264);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(128, 26);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(128, 264);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(129, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnSave;
            this.layoutControlItem1.Location = new System.Drawing.Point(257, 264);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(128, 26);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnCancel;
            this.layoutControlItem2.Location = new System.Drawing.Point(385, 264);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(129, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItemName
            // 
            this.layoutControlItemName.Control = this.txtName;
            this.layoutControlItemName.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemName.Name = "layoutControlItemName";
            this.layoutControlItemName.Size = new System.Drawing.Size(514, 26);
            this.layoutControlItemName.Text = "Наименование:";
            this.layoutControlItemName.TextSize = new System.Drawing.Size(104, 16);
            // 
            // layoutControlItemDescription
            // 
            this.layoutControlItemDescription.Control = this.memoDescription;
            this.layoutControlItemDescription.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItemDescription.Name = "layoutControlItemDescription";
            this.layoutControlItemDescription.Size = new System.Drawing.Size(514, 82);
            this.layoutControlItemDescription.Text = "Описание:";
            this.layoutControlItemDescription.TextSize = new System.Drawing.Size(104, 16);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.cmbTypeProgramEvent;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 134);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(514, 26);
            this.layoutControlItem5.Text = "Триггер:";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(104, 16);
            // 
            // layoutControlItemNameModel
            // 
            this.layoutControlItemNameModel.Control = this.txtNameModel;
            this.layoutControlItemNameModel.CustomizationFormText = "Модуль:";
            this.layoutControlItemNameModel.Location = new System.Drawing.Point(0, 186);
            this.layoutControlItemNameModel.Name = "layoutControlItemNameModel";
            this.layoutControlItemNameModel.Size = new System.Drawing.Size(514, 26);
            this.layoutControlItemNameModel.Text = "Модуль:";
            this.layoutControlItemNameModel.TextSize = new System.Drawing.Size(104, 16);
            // 
            // layoutControlItemControlSystemObject
            // 
            this.layoutControlItemControlSystemObject.Control = this.btnControlSystemObject;
            this.layoutControlItemControlSystemObject.CustomizationFormText = "Объект:";
            this.layoutControlItemControlSystemObject.Location = new System.Drawing.Point(0, 212);
            this.layoutControlItemControlSystemObject.Name = "layoutControlItemControlSystemObject";
            this.layoutControlItemControlSystemObject.Size = new System.Drawing.Size(514, 26);
            this.layoutControlItemControlSystemObject.Text = "Объект:";
            this.layoutControlItemControlSystemObject.TextSize = new System.Drawing.Size(104, 16);
            // 
            // layoutControlItemStaff
            // 
            this.layoutControlItemStaff.Control = this.btnStaff;
            this.layoutControlItemStaff.CustomizationFormText = "Постановщик:";
            this.layoutControlItemStaff.Location = new System.Drawing.Point(0, 238);
            this.layoutControlItemStaff.Name = "layoutControlItemStaff";
            this.layoutControlItemStaff.Size = new System.Drawing.Size(514, 26);
            this.layoutControlItemStaff.Text = "Постановщик:";
            this.layoutControlItemStaff.TextSize = new System.Drawing.Size(104, 16);
            // 
            // layoutControlItemDateSince
            // 
            this.layoutControlItemDateSince.Control = this.dateSince;
            this.layoutControlItemDateSince.CustomizationFormText = "Начало контроля:";
            this.layoutControlItemDateSince.Location = new System.Drawing.Point(0, 108);
            this.layoutControlItemDateSince.Name = "layoutControlItemDateSince";
            this.layoutControlItemDateSince.Size = new System.Drawing.Size(257, 26);
            this.layoutControlItemDateSince.Text = "Начало:";
            this.layoutControlItemDateSince.TextSize = new System.Drawing.Size(104, 16);
            // 
            // layoutControlItemDateTo
            // 
            this.layoutControlItemDateTo.Control = this.dateTo;
            this.layoutControlItemDateTo.CustomizationFormText = "Окончание контроля:";
            this.layoutControlItemDateTo.Location = new System.Drawing.Point(257, 108);
            this.layoutControlItemDateTo.Name = "layoutControlItemDateTo";
            this.layoutControlItemDateTo.Size = new System.Drawing.Size(257, 26);
            this.layoutControlItemDateTo.Text = "Окончание:";
            this.layoutControlItemDateTo.TextSize = new System.Drawing.Size(104, 16);
            // 
            // layoutControlItemActionProgramEvent
            // 
            this.layoutControlItemActionProgramEvent.Control = this.cmbActionProgramEvent;
            this.layoutControlItemActionProgramEvent.CustomizationFormText = "Триггер:";
            this.layoutControlItemActionProgramEvent.Location = new System.Drawing.Point(0, 160);
            this.layoutControlItemActionProgramEvent.Name = "layoutControlItemActionProgramEvent";
            this.layoutControlItemActionProgramEvent.Size = new System.Drawing.Size(514, 26);
            this.layoutControlItemActionProgramEvent.Text = "Действие:";
            this.layoutControlItemActionProgramEvent.TextSize = new System.Drawing.Size(104, 16);
            // 
            // ProgramEventEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 310);
            this.Controls.Add(this.layoutControlEvent);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(550, 350);
            this.Name = "ProgramEventEdit";
            this.Text = "События";
            this.Load += new System.EventHandler(this.ProgramEventEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlEvent)).EndInit();
            this.layoutControlEvent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTypeProgramEvent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNameModel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnControlSystemObject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnStaff.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateSince.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateSince.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbActionProgramEvent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDescription)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemNameModel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemControlSystemObject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemStaff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDateSince)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDateTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemActionProgramEvent)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControlEvent;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.MemoEdit memoDescription;
        private DevExpress.XtraEditors.ComboBoxEdit cmbTypeProgramEvent;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemDescription;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.TextEdit txtNameModel;
        private DevExpress.XtraEditors.ButtonEdit btnControlSystemObject;
        private DevExpress.XtraEditors.ButtonEdit btnStaff;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemNameModel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemControlSystemObject;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemStaff;
        private DevExpress.XtraEditors.DateEdit dateSince;
        private DevExpress.XtraEditors.DateEdit dateTo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemDateSince;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemDateTo;
        private DevExpress.XtraEditors.ComboBoxEdit cmbActionProgramEvent;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemActionProgramEvent;
    }
}