using DevExpress.XtraEditors;
using RMS.Core.Model;
using RMS.UI.Forms.Directories;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace RMS.UI.Control.Desktop
{
    public partial class TaskObjectControl : UserControl
    {
        private Task Task;
        
        public TaskObjectControl(Task task)
        {
            InitializeComponent();
            Task = task;
        }

        private void TaskControl_Load(object sender, EventArgs e)
        {
            lblTaskStatus.Text = Task?.StatusString;
            lblTask.Text = Task?.ToString() ?? "Задача не содержит информации";

            var color = Task.TaskStatus?.Color;
            if (!string.IsNullOrWhiteSpace(color))
            {
                lblTaskStatus.ForeColor = ControlPaint.Dark(ColorTranslator.FromHtml(color));
                lblTask.ForeColor = ControlPaint.Dark(ColorTranslator.FromHtml(color));
            }
        }

        private void lblTask_Click(object sender, EventArgs e)
        {
            if (e is MouseEventArgs eventArgs && eventArgs.Button == MouseButtons.Left)
            {
                var form = new TaskEdit(Task);
                form.ShowDialog();
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
                        labelControl.Font = new Font(label.Font.Name, label.Font.SizeInPoints, FontStyle.Underline);
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
                        labelControl.Font = new Font(label.Font.Name, label.Font.SizeInPoints, FontStyle.Regular);
                    }
                }
            }
        }
    }
}
