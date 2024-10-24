using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using RMS.Core.Model;
using System;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Forms.Directories
{
    public partial class GroupPerformanceIndicatorEdit : formEdit_BaseSpr
    {
        private Session Session { get; }
        public GroupPerformanceIndicator GroupPerformanceIndicator { get; }

        public GroupPerformanceIndicatorEdit()
        {
            InitializeComponent();

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                GroupPerformanceIndicator = new GroupPerformanceIndicator(Session);
            }
        }

        public GroupPerformanceIndicatorEdit(int id) : this()
        {
            if (id > 0)
            {
                GroupPerformanceIndicator = Session.GetObjectByKey<GroupPerformanceIndicator>(id);
            }
        }

        public GroupPerformanceIndicatorEdit(GroupPerformanceIndicator groupPerformanceIndicator) : this()
        {
            Session = groupPerformanceIndicator.Session;
            GroupPerformanceIndicator = groupPerformanceIndicator;
        }

        private void FormLoad(object sender, EventArgs e)
        {
            txtName.EditValue = GroupPerformanceIndicator.Name;
            memoDescription.EditValue = GroupPerformanceIndicator.Description;
            gridControl.DataSource = GroupPerformanceIndicator.PerformanceIndicators;

            if (gridView.Columns[nameof(PerformanceIndicator.Oid)] != null)
            {
                gridView.Columns[nameof(PerformanceIndicator.Oid)].Visible = false;
                gridView.Columns[nameof(PerformanceIndicator.Oid)].Width = 18;
                gridView.Columns[nameof(PerformanceIndicator.Oid)].OptionsColumn.FixedWidth = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveUserGroup())
            {
                id = GroupPerformanceIndicator.Oid;
                flagSave = true;
                Close();
            }
        }

        /// <summary>
        /// Сохранение отчета.
        /// </summary>
        private bool SaveUserGroup()
        {
            if (GroupPerformanceIndicator.Oid <= 0)
            {
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    XtraMessageBox.Show($"Сохранение без наименования не возможно.",
                        "Ошибка сохранения",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    txtName.Focus();
                    return false;
                }
            }

            if (GroupPerformanceIndicator.Name != txtName.Text)
            {
                if (Session.FindObject<GroupPerformanceIndicator>(new BinaryOperator(nameof(GroupPerformanceIndicator.Name), txtName.Text)) != null && !string.IsNullOrWhiteSpace(txtName.Text))
                {
                    XtraMessageBox.Show($"Группа: {txtName.Text} уже существует в системе.",
                        "Ошибка сохранения",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return false;
                }
            }

            GroupPerformanceIndicator.Name = txtName.Text;
            GroupPerformanceIndicator.Description = memoDescription.Text;
            Session.Save(GroupPerformanceIndicator);
            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnUserAdd_Click(object sender, EventArgs e)
        {
            var isSave = true;

            if (GroupPerformanceIndicator.Oid <= 0)
            {
                if (XtraMessageBox.Show($"Перед заполнением списка необходимо сохранить группу. Продолжить?.",
                        "Сохранение группы",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Information) == DialogResult.OK)
                {
                    isSave = SaveUserGroup();
                }
                else
                {
                    isSave = false;
                }

                if (isSave == false)
                {
                    return;
                }
            }

            var id = cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.PerformanceIndicator);

            if (id > 0)
            {
                var performanceIndicator = Session.GetObjectByKey<PerformanceIndicator>(id);

                if (performanceIndicator != null)
                {
                    if (GroupPerformanceIndicator.PerformanceIndicators.FirstOrDefault(f => f.Oid == performanceIndicator.Oid) == null)
                    {
                        performanceIndicator.GroupPerformanceIndicator = GroupPerformanceIndicator;
                        performanceIndicator.Save();
                    }
                }
            }

            GroupPerformanceIndicator.PerformanceIndicators.Reload();
        }

        private void btnUserDel_Click(object sender, EventArgs e)
        {
            if (gridView.IsEmpty)
            {
                return;
            }

            var performanceIndicator = gridView.GetRow(gridView.FocusedRowHandle) as PerformanceIndicator;

            if (XtraMessageBox.Show($"Вы действительно хотите удалить привязку {performanceIndicator} к группе: {GroupPerformanceIndicator}?",
                        "Удаление из группы",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (performanceIndicator != null)
                {
                    performanceIndicator.GroupPerformanceIndicator = null;
                    performanceIndicator.Save();
                    XtraMessageBox.Show($"Показатель: {performanceIndicator} успешно удален из группы: {GroupPerformanceIndicator}",
                        "Удаление из группы",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }

            GroupPerformanceIndicator.PerformanceIndicators.Reload();
        }
    }
}