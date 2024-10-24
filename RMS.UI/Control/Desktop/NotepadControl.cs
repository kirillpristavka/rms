using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraLayout;
using RMS.Core.Model;
using RMS.Core.Model.Reports;
using System;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Control.Desktop
{
    public partial class NotepadControl : UserControl
    {
        private Session session;
        private string caption;        

        public NotepadControl(Session session)
        {
            InitializeComponent();
            this.session = session;
        }

        private void TaskControl_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(caption))
            {
                lblName.Text = caption;
            }            
            
            FillingLayout();
        }

        private void FillingLayout()
        {
            layoutControlNotepadObject.BeginUpdate();
            layoutControlNotepadObject.Clear();
            
            var layoutControlItem = layoutControlNotepadObject.Root.AddItem();
            var reportControl = new NotepadObjectControl<ReportChange>(session, "Отчеты") { Dock = DockStyle.Top };
            layoutControlItem.TextVisible = false;
            layoutControlItem.AllowHotTrack = false;
            layoutControlItem.SizeConstraintsType = SizeConstraintsType.Custom;
            layoutControlItem.MaxSize = new System.Drawing.Size(0, reportControl.Size.Height);
            layoutControlItem.MinSize = new System.Drawing.Size(10, reportControl.Size.Height);
            layoutControlItem.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem.Control = reportControl;

            var emptySpaceItem = new EmptySpaceItem();
            layoutControlNotepadObject.Root.AddItem(emptySpaceItem);

            layoutControlNotepadObject.EndUpdate();            
        }        
    }
}
