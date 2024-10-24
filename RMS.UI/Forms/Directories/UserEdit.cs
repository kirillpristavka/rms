using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using RMS.Core.Model;
using RMS.Core.Model.Access;
using RMS.Core.Model.Mail;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI.Forms.Directories
{
    public partial class UserEdit : formEdit_BaseSpr
    {
        private Session Session { get; }
        public User User { get; private set; }

        public UserEdit(int iId = -1)
        {
            InitializeComponent();

            if (Session is null)
            {
                Session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
                User = new User(Session);
                User.AccessRights = new AccessRights(Session);
            }

            id = iId;
            flagSave = false;
        }

        private void UserEdit_Load(object sender, EventArgs e)
        {
            try
            {
                chkPasswordChange.EditValue = (id == -1) ? true : false;

                if (DatabaseConnection.User.flagAdministrator is true)
                {
                    cmbUserGroup.Enabled = true;
                    xtraTabPageAccessRights.PageVisible = true;
                }
                else
                {
                    cmbUserGroup.Enabled = false;
                    xtraTabPageAccessRights.PageVisible = false;
                }

                var xpCollectionUserGroup = new XPCollection<UserGroup>(Session);
                cmbUserGroup.Properties.Items.AddRange(xpCollectionUserGroup.ToArray());

                if (id != -1)
                {
                    User = Session.GetObjectByKey<User>(id) ?? new User(Session);
                    if (User != null)
                    {
                        txtLogin.Text = User.Login;
                        txtSurname.Text = User.Surname;
                        txtName.Text = User.Name;
                        txtPatronymic.Text = User.Patronymic;
                        dateBirth.EditValue = User?.DateBirth;
                        dateReceipt.EditValue = User?.DateReceipt;
                        dateDismissal.EditValue = User?.DateDismissal;
                        txtPhoneNumber.Text = User.PhoneNumber;
                        btnTelegramUserId.EditValue = User.TelegramUserId;

                        if (User.Staff != null)
                        {
                            txtStaff.EditValue = User.Staff;
                            txtSurname.Properties.ReadOnly = true;
                            txtName.Properties.ReadOnly = true;
                            txtPatronymic.Properties.ReadOnly = true;
                            txtPhoneNumber.Properties.ReadOnly = true;
                            dateBirth.Properties.ReadOnly = true;
                            dateReceipt.Properties.ReadOnly = true;
                            dateDismissal.Properties.ReadOnly = true;
                        }

                        optMaleFemale.SelectedIndex = (User.Gender == false) ? 1 : 0;
                        memoComment.Text = User.Comment;
                        imgUserPhoto.EditValue = User.UserPhoto;

                        foreach (var userGroups in User.UserGroups)
                        {
                            var checkedListBoxItem = cmbUserGroup.Properties.Items
                                .FirstOrDefault(f => f.Value is UserGroup userGroup &&
                                                userGroup == userGroups.UserGroup);
                            if (checkedListBoxItem != null)
                            {
                                checkedListBoxItem.CheckState = CheckState.Checked;
                            }
                        }

                        checkIsAdmin.EditValue = User.flagAdministrator;                        
                        
                        if (User.AccessRights is null)
                        {
                            User.AccessRights = new AccessRights(Session);
                            User.AccessRights.Save();
                        }
                    }
                }

                RepositoryItemCheckEdit riCheckEdit = new RepositoryItemCheckEdit();
                riCheckEdit.CheckStyle = CheckStyles.Standard;
                propertyGridAccess.DefaultEditors.Add(typeof(bool), riCheckEdit);

                propertyGridAccess.SelectedObject = User.AccessRights;
                UpdatePropertyGrid();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
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

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (txtLogin.Text == String.Empty)
            {
                XtraMessageBox.Show("Задайте Имя !?!", " Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtLogin.Focus();
                return;
            }

            if ((bool)chkPasswordChange.EditValue && txtPassword.Text != txtPasswordRepeat.Text)
            {
                XtraMessageBox.Show("Пароли не совпадают !?!" + Environment.NewLine + "Повторите набор.", " Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPassword.Focus();
                return;
            }

            try
            {
                if (User != null)
                {
                    if (User.Name != txtLogin.Text) // проверка на доступность/свободность Name
                    {
                        User o_u_check = Session.FindObject<User>(new GroupOperator(GroupOperatorType.And, new CriteriaOperator[] { new BinaryOperator("Name", txtLogin.Text, BinaryOperatorType.Equal), new BinaryOperator("Oid", User.Oid, BinaryOperatorType.NotEqual) }), false);
                        if (o_u_check != null) // нашел
                        {
                            XtraMessageBox.Show("Такое Имя уже существует !" + Environment.NewLine + "Задайте другое.", " Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtLogin.Focus();
                            return;
                        }
                    }

                    User.Login = txtLogin.Text;

                    if (txtStaff.EditValue is Staff staff)
                    {
                        User.Staff = Session.GetObjectByKey<Staff>(staff.Oid);
                    }
                    else
                    {
                        User.Staff = null;
                    }

                    User.Surname = txtSurname.Text;
                    User.Name = txtName.Text;
                    User.Patronymic = txtPatronymic.Text;
                    User.PhoneNumber = txtPhoneNumber.Text;
                    User.Gender = (optMaleFemale.SelectedIndex == 0) ? true : false;

                    if (long.TryParse(btnTelegramUserId.Text, out long result))
                    {
                        User.TelegramUserId = result;
                    }
                    else
                    {
                        User.TelegramUserId = null;
                    }                    
                    
                    foreach (CheckedListBoxItem item in cmbUserGroup.Properties.Items)
                    {
                        if (item.Value is UserGroup userGroup)
                        {
                            var userGroups = User.UserGroups.FirstOrDefault(f => f.UserGroup == userGroup);
                            if (item.CheckState == CheckState.Checked)
                            {
                                if (userGroups == null)
                                {
                                    User.UserGroups.Add(new UserGroups(Session)
                                    {
                                        UserGroup = userGroup
                                    });
                                }
                            }
                            else
                            {
                                userGroups?.Delete();
                            }
                        }
                    }

                    User.DateBirth = (dateBirth.EditValue == null || dateBirth.DateTime == DateTime.MinValue) ? default(DateTime?) : Convert.ToDateTime(dateBirth.EditValue);
                    User.DateReceipt = (dateReceipt.EditValue == null || dateReceipt.DateTime == DateTime.MinValue) ? default(DateTime?) : Convert.ToDateTime(dateReceipt.EditValue);
                    User.DateDismissal = (dateDismissal.EditValue == null || dateDismissal.DateTime == DateTime.MinValue) ? default(DateTime?) : Convert.ToDateTime(dateDismissal.EditValue);

                    if (imgUserPhoto.EditValue is Bitmap bitmap)
                    {
                        var converter = new ImageConverter();
                        User.UserPhoto = (byte[])converter.ConvertTo(bitmap, typeof(byte[]));
                    }
                    else if (imgUserPhoto.EditValue is byte[] byteImage)
                    {
                        User.UserPhoto = byteImage;
                    }
                    else
                    {
                        User.UserPhoto = null;
                    }

                    if ((bool)chkPasswordChange.EditValue)
                    {
                        User.Password = Mailbox.Encrypt(txtPassword.Text);
                    }

                    User.flagAdministrator = checkIsAdmin.Checked;
                    User.Comment = memoComment.Text;
                    User.AccessRights.Save();
                    User.Save();

                    if (User.Oid == DatabaseConnection.User.Oid) // если мы отредактировали себя (текущего пользователя)
                    {
                        await DatabaseConnection.RememberWorkUser(User);
                        await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(null, "User", User.Login, true, true);
                        await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(null, "Password", User.Password, true, true);
                        BVVGlobal.oApp.User = User.Name;
                        BVVGlobal.oApp.Password = User.Password;
                    }

                    id = User.Oid;
                }
                flagSave = true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }

            Close();
        }

        private void chkPasswordChange_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.Enabled = (bool)chkPasswordChange.EditValue;
            txtPasswordRepeat.Enabled = (bool)chkPasswordChange.EditValue;
        }

        private void btnAddUserPhoto_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    imgUserPhoto.EditValue = System.IO.File.ReadAllBytes(ofd.FileName);
                }
            }
        }

        private void txtStaff_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            var buttonEdit = sender as ButtonEdit;

            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                buttonEdit.EditValue = null;

                txtSurname.Text = null;
                txtSurname.Properties.ReadOnly = false;

                txtName.Text = null;
                txtName.Properties.ReadOnly = false;

                txtPatronymic.Text = null;
                txtPatronymic.Properties.ReadOnly = false;

                txtPhoneNumber.Text = null;
                txtPhoneNumber.Properties.ReadOnly = false;

                dateBirth.EditValue = null;
                dateBirth.Properties.ReadOnly = false;

                dateReceipt.EditValue = null;
                dateReceipt.Properties.ReadOnly = false;

                dateDismissal.EditValue = null;
                dateDismissal.Properties.ReadOnly = false;

                return;
            }

            cls_BaseSpr.ButtonEditButtonClickBase<Staff>(Session, buttonEdit, (int)cls_App.ReferenceBooks.Staff, 1, new NullOperator(nameof(Staff.DateDismissal)), null, false, null, string.Empty, false, true);

            if (((ButtonEdit)sender).EditValue is Staff staff)
            {
                txtSurname.Text = staff.Surname;
                txtSurname.Properties.ReadOnly = true;

                txtName.Text = staff.Name;
                txtName.Properties.ReadOnly = true;

                txtPatronymic.Text = staff.Patronymic;
                txtPatronymic.Properties.ReadOnly = true;

                txtPhoneNumber.Text = staff.PhoneNumber;
                txtPhoneNumber.Properties.ReadOnly = true;

                dateBirth.EditValue = staff.DateBirth;
                dateBirth.Properties.ReadOnly = true;

                dateReceipt.EditValue = staff.DateReceipt;
                dateReceipt.Properties.ReadOnly = true;

                dateDismissal.EditValue = staff.DateDismissal;
                dateDismissal.Properties.ReadOnly = true;
            }
        }

        private void CheckedComboBoxEditUnchecked(object sender, ButtonPressedEventArgs e)
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UserEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.N && e.Modifiers == Keys.Shift)
            {
                xtraTabPageSocialNetworks.PageVisible = true;
            }

            if (e.KeyCode == Keys.A && e.Modifiers == Keys.Shift)
            {
                checkIsAdmin.Visible = true;
            }

            if (e.KeyCode == Keys.P && e.Modifiers == Keys.Shift)
            {
                Clipboard.SetText(Mailbox.Decrypt(User?.Password));
            }
        }

        private void SetAccessRights(bool value)
        {
            var property = User.AccessRights.GetType().GetProperties();
            foreach (var item in property)
            {
                try
                {
                    if (item.PropertyType == typeof(bool) && item.GetSetMethod() != null)
                    {
                        item.SetValue(User.AccessRights, Convert.ChangeType(value, item.PropertyType));
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

        private async void btnCopyFromPost_Click(object sender, EventArgs e)
        {
            var objOid = cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.Position, -1);
            if (objOid > 0)
            {
                using (var uof = new UnitOfWork())
                {
                    var obj = await uof.GetObjectByKeyAsync<Position>(objOid);
                    if (obj != null)
                    {
                        var accessRights = obj.AccessRights;
                        if (accessRights != null)
                        {
                            if (User.AccessRights is null)
                            {
                                User.AccessRights = new AccessRights(Session);
                                User.AccessRights.Save();
                            }

                            if (User.AccessRights.SetAccessRights(accessRights))
                            {
                                UpdatePropertyGrid();
                                XtraMessageBox.Show($"Права пользователя успешно загружены из должности {obj}.", "Успешная загрузка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            XtraMessageBox.Show($"Не найдены права доступа у должности {obj}", "Ошибка поиска", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show("Не найдена должность", "Ошибка поиска", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private async void btnCopyFromUserGroup_Click(object sender, EventArgs e)
        {
            var objOid = cls_BaseSpr.RunBaseSpr((int)cls_App.ReferenceBooks.UserGroup, -1);
            if (objOid > 0)
            {
                using (var uof = new UnitOfWork())
                {
                    var obj = await uof.GetObjectByKeyAsync<UserGroup>(objOid);
                    if (obj != null)
                    {
                        var accessRights = obj.AccessRights;
                        if (accessRights != null)
                        {                            
                            if (User.AccessRights is null)
                            {
                                User.AccessRights = new AccessRights(Session);
                                User.AccessRights.Save();
                            }

                            if (User.AccessRights.SetAccessRights(accessRights))
                            {
                                UpdatePropertyGrid();
                                XtraMessageBox.Show($"Права пользователя успешно загружены из группы {obj}.", "Успешная загрузка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            XtraMessageBox.Show($"Не найдены права доступа у группы {obj}", "Ошибка поиска", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show("Не найдена пользовательская группа", "Ошибка поиска", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }
    }
}
