using DevExpress.XtraEditors;
using RMS.Core.Model.Notifications;
using RMS.UI.Forms.ReferenceBooks;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace RMS.UI.Control.Desktop
{
    public partial class ControlSystemObjectControl : UserControl
    {
        private ControlSystem controlSystem;
        public bool IsCongratulate { get; private set; }
        public bool IsVisible { get; private set; } = true;
        
        public delegate void ControlSystemEditEventHandler(object sender, ControlSystem controlSystem);
        public event ControlSystemEditEventHandler ControlSystemEditEvent;

        public ControlSystemObjectControl(ControlSystem controlSystem)
        {
            InitializeComponent();
            
            this.controlSystem = controlSystem;
        }

        private void TaskControl_Load(object sender, EventArgs e)
        {
            ReloadControl();
            IsCongratulate = true;
        }

        public async void ReloadControl()
        {
            try
            {
                controlSystem?.Reload();

                if (string.IsNullOrWhiteSpace(controlSystem.NameObj))
                {
                    lblTask.Text = $"{await controlSystem.GetObj(controlSystem.Session)}";
                }
                else
                {
                    lblTask.Text = controlSystem.NameObj;
                }                

                var description = controlSystem.CommentString?.Replace("\r\n", " ");
                if (string.IsNullOrWhiteSpace(description))
                {
                    lblDescription.Visible = false;
                }
                else
                {
                    lblDescription.Text = description;
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private void lblTask_MouseEnter(object sender, EventArgs e)
        {
            var label = sender as LabelControl;
            if (label != null)
            {
                foreach (var item in Controls)
                {
                    if (item is LabelControl labelControl)
                    {
                        labelControl.Font = new Font(labelControl.Font.Name, labelControl.Font.SizeInPoints, FontStyle.Underline);
                    }
                }
            }
        }

        private void lblTask_MouseLeave(object sender, EventArgs e)
        {
            var label = sender as LabelControl;
            if (label != null)
            {
                foreach (var item in Controls)
                {
                    if (item is LabelControl labelControl)
                    {
                        labelControl.Font = new Font(labelControl.Font.Name, labelControl.Font.SizeInPoints, FontStyle.Regular);
                    }
                }
            }
        }        

        private void lblCustomer_Click(object sender, EventArgs e)
        {
            if (controlSystem != null)
            {
                var form = new ControlSystemEdit(controlSystem);
                form.ShowDialog();
                ControlSystemEditEvent?.Invoke(this, form.ControlSystem);
            }
        }
    }
}
