using DevExpress.Xpo;
using DevExpress.XtraLayout;
using RMS.Core.Model;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Control.Desktop
{
    public partial class VacationControl : UserControl
    {
        private Session _session;
        public int Count { get; private set; }
        
        public VacationControl(Session session)
        {
            InitializeComponent();
            _session = session;
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            var dateTimeNow = DateTime.Now.Date;
            var userOid = DatabaseConnection.User?.Oid ?? -1;

            var user = await new XPQuery<User>(_session).FirstOrDefaultAsync(f => f.Oid == userOid);
            if (user.flagAdministrator)
            {
                var vacations = await new XPQuery<Vacation>(_session)
                    ?.Where(w => w.IsConfirm == false)
                    ?.ToListAsync();

                if (vacations != null)
                {
                    layoutControlLogger.BeginUpdate();
                    foreach (var vacation in vacations)
                    {
                        var objectControl = new VacationObjectControl(vacation) { Dock = DockStyle.Top };
                        objectControl.CreateControl();
                        if (objectControl.IsCongratulate)
                        {
                            var layoutControlItem = layoutControlLogger.Root.AddItem();
                            layoutControlItem.TextVisible = false;
                            layoutControlItem.AllowHotTrack = false;
                            layoutControlItem.MaxSize = new System.Drawing.Size(0, objectControl.Size.Height);
                            layoutControlItem.MinSize = new System.Drawing.Size(10, objectControl.Size.Height);
                            layoutControlItem.SizeConstraintsType = SizeConstraintsType.Custom;
                            layoutControlItem.TextSize = new System.Drawing.Size(0, 0);
                            layoutControlItem.Control = objectControl;

                            Count++;

                            objectControl.SaveEvent += ObjectControl_SaveEvent;
                        }
                    }
                    var emptySpaceItem = new EmptySpaceItem();
                    layoutControlLogger.Root.AddItem(emptySpaceItem);

                    layoutControlLogger.EndUpdate();
                }
            }
        }

        private void ObjectControl_SaveEvent(object sender)
        {
            try
            {
                var objectControl = sender as VacationObjectControl;
                if (objectControl != null)
                {
                    try
                    {
                        objectControl.ReloadControl();
                        objectControl.Refresh();

                        if (objectControl.IsCongratulate is false)
                        {
                            objectControl?.Dispose();
                            layoutControlLogger?.Refresh();
                        }
                    }
                    catch (Exception ex)
                    {
                        RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }
    }
}
