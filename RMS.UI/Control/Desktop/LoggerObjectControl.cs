using DevExpress.XtraEditors;
using RMS.Core.Model.Chronicle;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace RMS.UI.Control.Desktop
{
    public partial class LoggerObjectControl : UserControl
    {
        private ChronicleEvents chronicleEvents;
        
        public LoggerObjectControl(ChronicleEvents chronicleEvents)
        {
            InitializeComponent();
            this.chronicleEvents = chronicleEvents;
        }

        private void TaskControl_Load(object sender, EventArgs e)
        {
            var header = default(string);
            var user = chronicleEvents.User?.Staff?.ToString() ?? chronicleEvents.User?.ToString();
            if (!string.IsNullOrWhiteSpace(user))
            {
                header += $"{user} ";
            }
            header += $"[{chronicleEvents.Date.ToShortDateString()}]";
            
            lblUser.Text = header;
            lblDescription.Text = chronicleEvents.Name;

            if (!string.IsNullOrWhiteSpace(chronicleEvents.Description))
            {
                lblDescription.ToolTip = chronicleEvents.Description;
            }
            
            var photo = chronicleEvents.User?.UserPhoto;
            if (photo != null)
            {
                pictureEdit.EditValue = photo;
            }
            else
            {
                pictureEdit.Visible = false;
            }            

            //var color = chronicleEvents.TaskStatus?.Color;
            //if (!string.IsNullOrWhiteSpace(color))
            //{
            //    lblTask.ForeColor = ControlPaint.Dark(ColorTranslator.FromHtml(color));
            //}
        }

        private void lblTask_Click(object sender, EventArgs e)
        {
            if (e is MouseEventArgs eventArgs && eventArgs.Button == MouseButtons.Left)
            {                
                //form.ShowDialog();
            }
        }

        private void lblTask_MouseEnter(object sender, EventArgs e)
        {
            var label = sender as LabelControl;
            if (label != null)
            {
                label.Font = new Font(label.Font.Name, label.Font.SizeInPoints, FontStyle.Underline);
            }
        }

        private void lblTask_MouseLeave(object sender, EventArgs e)
        {
            var label = sender as LabelControl;
            if (label != null)
            {
                label.Font = new Font(label.Font.Name, label.Font.SizeInPoints, FontStyle.Regular);
            }
        }
    }
}
