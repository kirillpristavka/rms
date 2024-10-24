using DevExpress.XtraEditors;
using RMS.Core.Model;
using RMS.UI.Forms.Directories;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace RMS.UI.Control.Desktop
{
    public partial class TaskNowObjectControl : UserControl
    {
        private Task task;
        public bool IsCongratulate { get; private set; }
        public bool IsVisible { get; private set; } = true;
        
        public delegate void TaskEditEventHandler(object sender, Task task);
        public event TaskEditEventHandler TaskEditEvent;

        public TaskNowObjectControl(Task task)
        {
            InitializeComponent();
            
            this.task = task;
        }

        private void TaskControl_Load(object sender, EventArgs e)
        {
            ReloadControl();
            IsCongratulate = true;
        }

        public void ReloadControl()
        {
            try
            {
                task?.Reload();
                task?.TaskStatus?.Reload();

                lblTask.Text = $"{task.GetTaskNowString()}";

                var description = task.Description?.ToString()?.Replace("\r\n", " ");
                if (string.IsNullOrWhiteSpace(description))
                {
                    lblDescription.Visible = false;
                }
                else
                {
                    lblDescription.Text = description;
                }

                var color = task.TaskStatus?.Color;
                if (!string.IsNullOrWhiteSpace(color))
                {
                    lblTask.ForeColor = ControlPaint.Dark(ColorTranslator.FromHtml(color));
                    lblDescription.ForeColor = ControlPaint.Dark(ColorTranslator.FromHtml(color));
                }

                if (task?.StatusString == "Выполнена")
                {
                    IsVisible = false;
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
            if (task != null)
            {
                var taskStatus = task?.TaskStatus;
                
                var form = new TaskEdit(task);
                form.ShowDialog();
                TaskEditEvent?.Invoke(this, form.Task);
            }
        }
    }
}
