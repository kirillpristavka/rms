using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using RMS.Core.Enumerator;
using RMS.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Forms.ReferenceBooks
{
    public partial class TaskObjectEdit : formEdit_BaseSpr
    {
        private Session Session { get; }
        private TaskObject TaskObject { get; }

        public TaskObjectEdit()
        {
            InitializeComponent();
            checkedComboBoxEditTypeTasks.Properties.Items.AddEnum(typeof(TypeTask));

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                TaskObject = new TaskObject(Session);
            }
        }

        public TaskObjectEdit(int id) : this()
        {
            if (id > 0)
            {
                TaskObject = Session.GetObjectByKey<TaskObject>(id);
            }
        }

        public TaskObjectEdit(TaskObject taskObject) : this()
        {
            Session = taskObject.Session;
            TaskObject = taskObject;
        }
        private void PositionEdit_Load(object sender, EventArgs e)
        {
            txtName.Text = TaskObject.Name;
            memoDescription.Text = TaskObject.Description;
            checkIsUse.EditValue = TaskObject.IsUse;

            var typeTasks = TaskObject.GetTypeTasks();
            if (typeTasks != null)
            {
                foreach (var item in typeTasks)
                {
                    var checkedListBoxItem = checkedComboBoxEditTypeTasks.Properties.Items.FirstOrDefault(f => f.Value is TypeTask type && type == item.TypeTask);

                    if (checkedListBoxItem != null)
                    {
                        checkedListBoxItem.CheckState = CheckState.Checked;
                    }
                }
            }            
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            TaskObject.Name = txtName.Text;
            TaskObject.Description = memoDescription.Text;
            TaskObject.IsUse = checkIsUse.Checked;
            
            var typeTasksList = new List<TypeTasks>();
            foreach (CheckedListBoxItem item in checkedComboBoxEditTypeTasks.Properties.Items)
            {
                if (item.Value is TypeTask type)
                {
                    if (item.CheckState == CheckState.Checked)
                    {
                        typeTasksList.Add(new TypeTasks(type));
                    }
                }
            }
            TaskObject.SaveTypeTasks(typeTasksList);

            Session.Save(TaskObject);
            id = TaskObject.Oid;
            flagSave = true;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void checkedComboBoxEditTypeTasks_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var checkedComboBoxEdit = sender as CheckedComboBoxEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                foreach (CheckedListBoxItem checkedListBoxItemtem in checkedComboBoxEdit.Properties.Items)
                {
                    checkedListBoxItemtem.CheckState = CheckState.Unchecked;
                }
                return;
            }
        }
    }
}