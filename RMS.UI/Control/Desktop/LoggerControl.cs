using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraLayout;
using RMS.Core.Model;
using RMS.Core.Model.Chronicle;
using System;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Control.Desktop
{
    public partial class LoggerControl : UserControl
    {
        public Session Session { get; set; }

        private XPCollection<ChronicleEvents> chronicleEvents;
        
        public LoggerControl(Session session)
        {
            InitializeComponent();
            Session = session;
        }

        private void LoggerControl_Load(object sender, EventArgs e)
        {            
            var user = Session.GetObjectByKey<User>(DatabaseConnection.User.Oid);

            if (user != null)
            {
                chronicleEvents = new XPCollection<ChronicleEvents>(Session);

                layoutControlLogger.BeginUpdate();
                
                var countSkip = default(int);
                if (chronicleEvents.Count >= 150)
                {
                    countSkip = chronicleEvents.Count - 149;
                }

                foreach (var chronicleEvent in chronicleEvents.Skip(countSkip).OrderByDescending(o => o.Oid))
                {
                    var layoutControlItem = layoutControlLogger.Root.AddItem();
                    var loggerObjectControl = new LoggerObjectControl(chronicleEvent) { Dock = DockStyle.Top };                    
                    layoutControlItem.TextVisible = false;
                    layoutControlItem.AllowHotTrack = false;
                    layoutControlItem.MaxSize = new System.Drawing.Size(0, loggerObjectControl.Size.Height);
                    layoutControlItem.MinSize = new System.Drawing.Size(10, loggerObjectControl.Size.Height);
                    layoutControlItem.SizeConstraintsType = SizeConstraintsType.Custom;
                    layoutControlItem.TextSize = new System.Drawing.Size(0, 0);
                    layoutControlItem.Control = loggerObjectControl;                
                }               
                
                var emptySpaceItem = new EmptySpaceItem();
                layoutControlLogger.Root.AddItem(emptySpaceItem);

                layoutControlLogger.EndUpdate();
            }           
        }
    }
}
