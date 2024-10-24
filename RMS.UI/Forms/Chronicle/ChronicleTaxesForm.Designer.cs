namespace RMS.UI.Forms.Chronicle
{
    partial class ChronicleTaxesForm<T>
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
            this.gridControlChronicle = new DevExpress.XtraGrid.GridControl();
            this.gridViewChronicle = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlChronicle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewChronicle)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlChronicle
            // 
            this.gridControlChronicle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlChronicle.Location = new System.Drawing.Point(0, 0);
            this.gridControlChronicle.MainView = this.gridViewChronicle;
            this.gridControlChronicle.Name = "gridControlChronicle";
            this.gridControlChronicle.Size = new System.Drawing.Size(784, 360);
            this.gridControlChronicle.TabIndex = 0;
            this.gridControlChronicle.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewChronicle});
            // 
            // gridViewChronicle
            // 
            this.gridViewChronicle.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridViewChronicle.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridViewChronicle.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gridViewChronicle.GridControl = this.gridControlChronicle;
            this.gridViewChronicle.Name = "gridViewChronicle";
            this.gridViewChronicle.OptionsBehavior.Editable = false;
            this.gridViewChronicle.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gridViewChronicle.OptionsView.ShowGroupPanel = false;
            // 
            // ChronicleTaxesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 360);
            this.Controls.Add(this.gridControlChronicle);
            this.Name = "ChronicleTaxesForm";
            this.Text = "Хроника:";
            this.Load += new System.EventHandler(this.ChronicleTaxesForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlChronicle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewChronicle)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.GridControl gridControlChronicle;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewChronicle;
    }
}