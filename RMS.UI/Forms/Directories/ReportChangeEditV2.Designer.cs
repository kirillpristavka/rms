namespace RMS.UI.Forms.Directories
{
    partial class ReportChangeEditV2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportChangeEditV2));
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlReport = new DevExpress.XtraLayout.LayoutControl();
            this.dateDeliveryTime = new DevExpress.XtraEditors.DateEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnAccountantResponsible = new DevExpress.XtraEditors.ButtonEdit();
            this.btnCustomer = new DevExpress.XtraEditors.ButtonEdit();
            this.btnReport = new DevExpress.XtraEditors.ButtonEdit();
            this.txtPeriod = new DevExpress.XtraEditors.TextEdit();
            this.txtStatus = new DevExpress.XtraEditors.TextEdit();
            this.memoComment = new DevExpress.XtraEditors.MemoEdit();
            this.dateCompletion = new DevExpress.XtraEditors.DateEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItemAccountantResponsible = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemCustomer = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemReport = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemDeliveryTime = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemPeriod = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemStatus = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemComment = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemDateCompletion = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlReport)).BeginInit();
            this.layoutControlReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateDeliveryTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDeliveryTime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAccountantResponsible.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnReport.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPeriod.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateCompletion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateCompletion.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAccountantResponsible)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDeliveryTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPeriod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemComment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDateCompletion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(269, 231);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(127, 22);
            this.btnSave.StyleController = this.layoutControlReport;
            this.btnSave.TabIndex = 59;
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // layoutControlReport
            // 
            this.layoutControlReport.AllowCustomization = false;
            this.layoutControlReport.Controls.Add(this.dateDeliveryTime);
            this.layoutControlReport.Controls.Add(this.btnSave);
            this.layoutControlReport.Controls.Add(this.btnCancel);
            this.layoutControlReport.Controls.Add(this.btnAccountantResponsible);
            this.layoutControlReport.Controls.Add(this.btnCustomer);
            this.layoutControlReport.Controls.Add(this.btnReport);
            this.layoutControlReport.Controls.Add(this.txtPeriod);
            this.layoutControlReport.Controls.Add(this.txtStatus);
            this.layoutControlReport.Controls.Add(this.memoComment);
            this.layoutControlReport.Controls.Add(this.dateCompletion);
            this.layoutControlReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlReport.Location = new System.Drawing.Point(0, 0);
            this.layoutControlReport.Name = "layoutControlReport";
            this.layoutControlReport.Root = this.Root;
            this.layoutControlReport.Size = new System.Drawing.Size(534, 260);
            this.layoutControlReport.TabIndex = 62;
            this.layoutControlReport.Text = "layoutControl1";
            // 
            // dateDeliveryTime
            // 
            this.dateDeliveryTime.EditValue = null;
            this.dateDeliveryTime.Location = new System.Drawing.Point(134, 7);
            this.dateDeliveryTime.Name = "dateDeliveryTime";
            this.dateDeliveryTime.Properties.Appearance.Options.UseTextOptions = true;
            this.dateDeliveryTime.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dateDeliveryTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateDeliveryTime.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateDeliveryTime.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.DateTimeMaskManager));
            this.dateDeliveryTime.Properties.MaskSettings.Set("useAdvancingCaret", true);
            this.dateDeliveryTime.Size = new System.Drawing.Size(131, 22);
            this.dateDeliveryTime.StyleController = this.layoutControlReport;
            this.dateDeliveryTime.TabIndex = 71;
            this.dateDeliveryTime.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(400, 231);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(127, 22);
            this.btnCancel.StyleController = this.layoutControlReport;
            this.btnCancel.TabIndex = 58;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAccountantResponsible
            // 
            this.btnAccountantResponsible.Location = new System.Drawing.Point(134, 33);
            this.btnAccountantResponsible.Name = "btnAccountantResponsible";
            this.btnAccountantResponsible.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.btnAccountantResponsible.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnAccountantResponsible.Size = new System.Drawing.Size(393, 22);
            this.btnAccountantResponsible.StyleController = this.layoutControlReport;
            this.btnAccountantResponsible.TabIndex = 41;
            this.btnAccountantResponsible.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnAccountantResponsible_ButtonPressed);
            // 
            // btnCustomer
            // 
            this.btnCustomer.Location = new System.Drawing.Point(134, 59);
            this.btnCustomer.Name = "btnCustomer";
            this.btnCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnCustomer.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnCustomer.Size = new System.Drawing.Size(393, 22);
            this.btnCustomer.StyleController = this.layoutControlReport;
            this.btnCustomer.TabIndex = 34;
            this.btnCustomer.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnCustomer_ButtonPressed);
            // 
            // btnReport
            // 
            this.btnReport.Location = new System.Drawing.Point(134, 85);
            this.btnReport.Name = "btnReport";
            this.btnReport.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnReport.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnReport.Size = new System.Drawing.Size(393, 22);
            this.btnReport.StyleController = this.layoutControlReport;
            this.btnReport.TabIndex = 70;
            this.btnReport.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnReport_ButtonPressed);
            // 
            // txtPeriod
            // 
            this.txtPeriod.Location = new System.Drawing.Point(134, 111);
            this.txtPeriod.Name = "txtPeriod";
            this.txtPeriod.Size = new System.Drawing.Size(393, 22);
            this.txtPeriod.StyleController = this.layoutControlReport;
            this.txtPeriod.TabIndex = 72;
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(134, 137);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(393, 22);
            this.txtStatus.StyleController = this.layoutControlReport;
            this.txtStatus.TabIndex = 72;
            // 
            // memoComment
            // 
            this.memoComment.EditValue = "";
            this.memoComment.Location = new System.Drawing.Point(134, 163);
            this.memoComment.MaximumSize = new System.Drawing.Size(0, 100);
            this.memoComment.Name = "memoComment";
            this.memoComment.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.memoComment.Size = new System.Drawing.Size(393, 54);
            this.memoComment.StyleController = this.layoutControlReport;
            this.memoComment.TabIndex = 12;
            // 
            // dateCompletion
            // 
            this.dateCompletion.EditValue = null;
            this.dateCompletion.Location = new System.Drawing.Point(396, 7);
            this.dateCompletion.Name = "dateCompletion";
            this.dateCompletion.Properties.Appearance.Options.UseTextOptions = true;
            this.dateCompletion.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dateCompletion.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateCompletion.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateCompletion.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.DateTimeMaskManager));
            this.dateCompletion.Properties.MaskSettings.Set("useAdvancingCaret", true);
            this.dateCompletion.Size = new System.Drawing.Size(131, 22);
            this.dateCompletion.StyleController = this.layoutControlReport;
            this.dateCompletion.TabIndex = 71;
            this.dateCompletion.TabStop = false;
            // 
            // Root
            // 
            this.Root.AppearanceItemCaption.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.Root.AppearanceItemCaption.Options.UseFont = true;
            this.Root.AppearanceItemCaption.Options.UseTextOptions = true;
            this.Root.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.emptySpaceItem2,
            this.layoutControlItemAccountantResponsible,
            this.layoutControlItemCustomer,
            this.layoutControlItemReport,
            this.layoutControlItemDeliveryTime,
            this.layoutControlItemPeriod,
            this.layoutControlItemStatus,
            this.layoutControlItemComment,
            this.layoutControlItemDateCompletion,
            this.emptySpaceItem3});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.Root.Size = new System.Drawing.Size(534, 260);
            this.Root.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 224);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(131, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnCancel;
            this.layoutControlItem3.Location = new System.Drawing.Point(393, 224);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(131, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnSave;
            this.layoutControlItem4.Location = new System.Drawing.Point(262, 224);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(131, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(131, 224);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(131, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItemAccountantResponsible
            // 
            this.layoutControlItemAccountantResponsible.Control = this.btnAccountantResponsible;
            this.layoutControlItemAccountantResponsible.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItemAccountantResponsible.CustomizationFormText = "Ответственный:";
            this.layoutControlItemAccountantResponsible.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItemAccountantResponsible.Name = "layoutControlItemAccountantResponsible";
            this.layoutControlItemAccountantResponsible.Size = new System.Drawing.Size(524, 26);
            this.layoutControlItemAccountantResponsible.Text = "Ответственный:";
            this.layoutControlItemAccountantResponsible.TextSize = new System.Drawing.Size(123, 16);
            // 
            // layoutControlItemCustomer
            // 
            this.layoutControlItemCustomer.Control = this.btnCustomer;
            this.layoutControlItemCustomer.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItemCustomer.CustomizationFormText = "Клиент:";
            this.layoutControlItemCustomer.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItemCustomer.Name = "layoutControlItemCustomer";
            this.layoutControlItemCustomer.Size = new System.Drawing.Size(524, 26);
            this.layoutControlItemCustomer.Text = "Клиент:";
            this.layoutControlItemCustomer.TextSize = new System.Drawing.Size(123, 16);
            // 
            // layoutControlItemReport
            // 
            this.layoutControlItemReport.Control = this.btnReport;
            this.layoutControlItemReport.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItemReport.CustomizationFormText = "Отчет:";
            this.layoutControlItemReport.Location = new System.Drawing.Point(0, 78);
            this.layoutControlItemReport.Name = "layoutControlItemReport";
            this.layoutControlItemReport.Size = new System.Drawing.Size(524, 26);
            this.layoutControlItemReport.Text = "Отчет:";
            this.layoutControlItemReport.TextSize = new System.Drawing.Size(123, 16);
            // 
            // layoutControlItemDeliveryTime
            // 
            this.layoutControlItemDeliveryTime.Control = this.dateDeliveryTime;
            this.layoutControlItemDeliveryTime.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemDeliveryTime.Name = "layoutControlItemDeliveryTime";
            this.layoutControlItemDeliveryTime.Size = new System.Drawing.Size(262, 26);
            this.layoutControlItemDeliveryTime.Text = "Срок уплаты:";
            this.layoutControlItemDeliveryTime.TextSize = new System.Drawing.Size(123, 16);
            // 
            // layoutControlItemPeriod
            // 
            this.layoutControlItemPeriod.Control = this.txtPeriod;
            this.layoutControlItemPeriod.Location = new System.Drawing.Point(0, 104);
            this.layoutControlItemPeriod.Name = "layoutControlItemPeriod";
            this.layoutControlItemPeriod.Size = new System.Drawing.Size(524, 26);
            this.layoutControlItemPeriod.Text = "Период:";
            this.layoutControlItemPeriod.TextSize = new System.Drawing.Size(123, 16);
            // 
            // layoutControlItemStatus
            // 
            this.layoutControlItemStatus.Control = this.txtStatus;
            this.layoutControlItemStatus.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItemStatus.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItemStatus.Location = new System.Drawing.Point(0, 130);
            this.layoutControlItemStatus.Name = "layoutControlItemStatus";
            this.layoutControlItemStatus.Size = new System.Drawing.Size(524, 26);
            this.layoutControlItemStatus.Text = "Статус:";
            this.layoutControlItemStatus.TextSize = new System.Drawing.Size(123, 16);
            // 
            // layoutControlItemComment
            // 
            this.layoutControlItemComment.Control = this.memoComment;
            this.layoutControlItemComment.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItemComment.CustomizationFormText = "Комментарий:";
            this.layoutControlItemComment.Location = new System.Drawing.Point(0, 156);
            this.layoutControlItemComment.Name = "layoutControlItemComment";
            this.layoutControlItemComment.Size = new System.Drawing.Size(524, 58);
            this.layoutControlItemComment.Text = "Комментарий:";
            this.layoutControlItemComment.TextSize = new System.Drawing.Size(123, 16);
            // 
            // layoutControlItemDateCompletion
            // 
            this.layoutControlItemDateCompletion.Control = this.dateCompletion;
            this.layoutControlItemDateCompletion.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItemDateCompletion.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItemDateCompletion.Location = new System.Drawing.Point(262, 0);
            this.layoutControlItemDateCompletion.Name = "layoutControlItemDateCompletion";
            this.layoutControlItemDateCompletion.Size = new System.Drawing.Size(262, 26);
            this.layoutControlItemDateCompletion.Text = "Дата сдачи:";
            this.layoutControlItemDateCompletion.TextSize = new System.Drawing.Size(123, 16);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 214);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(524, 10);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // ReportChangeEditV2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 260);
            this.Controls.Add(this.layoutControlReport);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("ReportChangeEditV2.IconOptions.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(550, 200);
            this.Name = "ReportChangeEditV2";
            this.Text = "Отчет";
            this.Load += new System.EventHandler(this.ReportEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlReport)).EndInit();
            this.layoutControlReport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dateDeliveryTime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDeliveryTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAccountantResponsible.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnReport.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPeriod.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateCompletion.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateCompletion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAccountantResponsible)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDeliveryTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPeriod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemComment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDateCompletion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControl layoutControlReport;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.ButtonEdit btnAccountantResponsible;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemAccountantResponsible;
        private DevExpress.XtraEditors.ButtonEdit btnCustomer;
        private DevExpress.XtraEditors.ButtonEdit btnReport;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCustomer;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemReport;
        private DevExpress.XtraEditors.DateEdit dateDeliveryTime;
        private DevExpress.XtraEditors.TextEdit txtPeriod;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemDeliveryTime;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemPeriod;
        private DevExpress.XtraEditors.TextEdit txtStatus;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemStatus;
        private DevExpress.XtraEditors.MemoEdit memoComment;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemComment;
        private DevExpress.XtraEditors.DateEdit dateCompletion;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemDateCompletion;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
    }
}