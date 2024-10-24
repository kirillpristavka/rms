using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using RMS.Core.Model;
using RMS.Core.Model.Access;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Forms.Directories
{
    public partial class UserGroupEdit : formEdit_BaseSpr
    {
        private Session Session { get; }
        public UserGroup UserGroup { get; }
        public XPCollection<User> XPCollectionUser { get; set; }

        public UserGroupEdit()
        {
            InitializeComponent();

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                UserGroup = new UserGroup(Session);
                UserGroup.AccessRights = new AccessRights(Session);
            }
        }

        public UserGroupEdit(int id) : this()
        {
            if (id > 0)
            {
                UserGroup = Session.GetObjectByKey<UserGroup>(id);
            }
        }

        public UserGroupEdit(UserGroup userGroup) : this()
        {
            Session = userGroup.Session;
            UserGroup = userGroup;
        }

        private void ReportEdit_Load(object sender, EventArgs e)
        {
            txtName.EditValue = UserGroup.Name;
            memoDescription.EditValue = UserGroup.Description;

            if (UserGroup.AccessRights is null)
            {
                UserGroup.AccessRights = new AccessRights(Session);
                UserGroup.AccessRights.Save();
            }

            RepositoryItemCheckEdit riCheckEdit = new RepositoryItemCheckEdit();
            riCheckEdit.CheckStyle = CheckStyles.Standard;
            propertyGridAccess.DefaultEditors.Add(typeof(bool), riCheckEdit);

            propertyGridAccess.SelectedObject = UserGroup.AccessRights;
            UpdatePropertyGrid();

            var criteriaUser = new ContainsOperator(nameof(User.UserGroups), new BinaryOperator(nameof(UserGroups.UserGroup), UserGroup));
            XPCollectionUser = new XPCollection<User>(Session, criteriaUser);

            gridControlUsers.DataSource = XPCollectionUser;

            if (gridViewUsers.Columns[nameof(User.Oid)] != null)
            {
                gridViewUsers.Columns[nameof(User.Oid)].Visible = false;
                gridViewUsers.Columns[nameof(User.Oid)].Width = 18;
                gridViewUsers.Columns[nameof(User.Oid)].OptionsColumn.FixedWidth = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveUserGroup())
            {
                id = UserGroup.Oid;
                flagSave = true;
                Close();
            }
        }

        /// <summary>
        /// Сохранение отчета.
        /// </summary>
        private bool SaveUserGroup()
        {
            if (UserGroup.Oid <= 0)
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

            if (UserGroup.Name != txtName.Text)
            {
                if (Session.FindObject<UserGroup>(new BinaryOperator(nameof(UserGroup.Name), txtName.Text)) != null && !string.IsNullOrWhiteSpace(txtName.Text))
                {
                    XtraMessageBox.Show($"Группа пользователей: {txtName.Text} уже существует в системе.",
                        "Ошибка сохранения",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return false;
                }
            }

            UserGroup.Name = txtName.Text;
            UserGroup.Description = memoDescription.Text;
            UserGroup.AccessRights.Save();
            Session.Save(UserGroup);
            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnUserAdd_Click(object sender, EventArgs e)
        {
            var isSave = true;

            if (UserGroup.Oid <= 0)
            {
                if (XtraMessageBox.Show($"Перед заполнением списка пользователей необходимо сохранить группу. Продолжить?.",
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

            var id = cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.User);

            if (id > 0)
            {
                var user = Session.GetObjectByKey<User>(id);

                if (user != null)
                {
                    if (user.UserGroups.FirstOrDefault(f => f.UserGroup == UserGroup) == null)
                    {
                        if (isSave)
                        {
                            user.UserGroups.Add(new UserGroups(Session)
                            {
                                UserGroup = UserGroup
                            });
                            user.Save();
                        }
                    }
                }
            }

            XPCollectionUser.Reload();
        }

        private void btnUserDel_Click(object sender, EventArgs e)
        {
            if (gridViewUsers.IsEmpty)
            {
                return;
            }

            var user = gridViewUsers.GetRow(gridViewUsers.FocusedRowHandle) as User;

            if (XtraMessageBox.Show($"Вы действительно хотите удалить привязку пользователя {user} к группе: {UserGroup}?",
                        "Удаление пользователя из группы",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (user != null)
                {
                    var userGroups = user.UserGroups.FirstOrDefault(f => f.UserGroup == UserGroup);

                    if (userGroups != null)
                    {
                        userGroups.Delete();
                        user.Save();

                        XtraMessageBox.Show($"Пользователь: {user} успешно удален из группы: {UserGroup}",
                        "Удаление отчета",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    }
                }
            }

            XPCollectionUser.Reload();
        }

        private void SetAccessRights(bool value)
        {
            var property = UserGroup.AccessRights.GetType().GetProperties();
            foreach (var item in property)
            {
                try
                {
                    if (item.PropertyType == typeof(bool) && item.GetSetMethod() != null)
                    {
                        item.SetValue(UserGroup.AccessRights, Convert.ChangeType(value, item.PropertyType));
                    }
                }
                catch (Exception ex)
                {
                    RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                }
            }

            UpdatePropertyGrid();
        }


        private void btnAllowAll_Click(object sender, EventArgs e)
        {
            SetAccessRights(true);
        }

        private void btnBanEverything_Click(object sender, EventArgs e)
        {
            SetAccessRights(false);
        }

        private void UpdatePropertyGrid()
        {
            try
            {
                propertyGridAccess.UpdateData();

                var row = propertyGridAccess.GetRowByCaption(nameof(XPObject.Oid));
                if (row != null)
                {
                    row.Visible = false;
                }

                row = propertyGridAccess.GetRowByCaption("Прочее");
                if (row != null)
                {
                    row.Visible = false;
                }

                propertyGridAccess.RecordWidth = 25;
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }
    }
}