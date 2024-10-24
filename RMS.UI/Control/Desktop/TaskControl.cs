using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraLayout;
using RMS.Core.Model;
using System;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Control.Desktop
{
    public partial class TaskControl : UserControl
    {
        private Session session;
        private XPCollection<Task> tasks;
        private CriteriaOperator criteriaOperator;
        private string caption;

        private bool isUseAdditionalRule;

        public TaskControl(Session session, CriteriaOperator criteriaOperator = null, string caption = default)
        {
            InitializeComponent();
            this.session = session;
            this.criteriaOperator = criteriaOperator;
            this.caption = caption;
        }

        private void TaskControl_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(caption))
            {
                lblTask.Text = caption;
            }

            tasks = new XPCollection<Task>(session, criteriaOperator);
            tasks.Reload();
            
            ReloadControl();
        }

        public void ReloadControl()
        {
            layoutControlTaskObject.BeginUpdate();
            layoutControlTaskObject.Clear();

            var countSkip = default(int);

            if (tasks.Count >= 150)
            {
                countSkip = tasks.Count - 149;
            }

            foreach (var task in tasks
                .Skip(countSkip)
                .OrderByDescending(o => o.Oid)
                .OrderByDescending(o => o.Deadline))
            {
                var layoutControlItem = layoutControlTaskObject.Root.AddItem();
                var taskObjectControl = new TaskObjectControl(task) { Dock = DockStyle.Top };
                layoutControlItem.TextVisible = false;
                layoutControlItem.AllowHotTrack = false;
                layoutControlItem.SizeConstraintsType = SizeConstraintsType.Custom;
                layoutControlItem.MaxSize = new System.Drawing.Size(0, taskObjectControl.Size.Height);
                layoutControlItem.MinSize = new System.Drawing.Size(10, taskObjectControl.Size.Height);
                layoutControlItem.TextSize = new System.Drawing.Size(0, 0);
                layoutControlItem.Control = taskObjectControl;                
            }

            var emptySpaceItem = new EmptySpaceItem();
            layoutControlTaskObject.Root.AddItem(emptySpaceItem);

            layoutControlTaskObject.EndUpdate();

            var countTasks = tasks?.Count;

            if (countTasks > 0)
            {
                lblTaskCount.Text = countTasks.ToString();
            }
            else
            {
                lblTaskCount.Text = "0";
            }
        }

        private async void btnClear_Click(object sender, EventArgs e)
        {
            if (isUseAdditionalRule is false)
            {
                var statusDone = await session.FindObjectAsync<TaskStatus>(new BinaryOperator(nameof(TaskStatus.Name), "Выполнена"));

                if (statusDone != null)
                {
                    var groupOperator = new GroupOperator(GroupOperatorType.And);
                    var criteriaStatus = new NotOperator(new BinaryOperator(nameof(Task.TaskStatus), statusDone));
                    groupOperator.Operands.Add(criteriaStatus);

                    if (!string.IsNullOrWhiteSpace(criteriaOperator?.ToString()))
                    {
                        groupOperator.Operands.Add(criteriaOperator);
                    }

                    tasks = new XPCollection<Task>(session, groupOperator);
                    tasks.Reload();
                }

                btnClear.ToolTip = "После нажатия, выполненные задачи будут отображены в списке";
                isUseAdditionalRule = true;
            }
            else
            {
                tasks = new XPCollection<Task>(session, criteriaOperator);
                tasks.Reload();

                isUseAdditionalRule = false;
                btnClear.ToolTip = "После нажатия, выполненные задачи НЕ будут отображены в списке";
            }            

            ReloadControl();
        }
    }
}
