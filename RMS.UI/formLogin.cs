using AutoUpdaterDotNET;
using DevExpress.Data.Filtering;
using DevExpress.LookAndFeel;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using RMS.Core.Model;
using RMS.Core.Model.Mail;
using System;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI
{
    public partial class formLogin : XtraForm
    {
        private class UserWindowsIdentity
        {
            public string Name { get; set; }
            public string WorkName { get; set; }
        }

        public formLogin()
        {
            InitializeComponent();

            _flagProcess = false;
            _FlagOK = false;
        }

        /// <summary>
        /// Автоматическое обновление программы.
        /// </summary>
        private static async System.Threading.Tasks.Task UpdateApplication()
        {
            await System.Threading.Tasks.Task.Run(async () =>
            {
                AutoUpdater.Mandatory = true;
                var path = await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(
                    DatabaseConnection.LocalSession,
                    nameof(cls_AppParam.PathUpdateService),
                    "ftp://81.28.221.114/other/RMS/RMS.Update.xml");
                AutoUpdater.Start(path);
            });            
        }

        public bool FlagOK { get { return _FlagOK; } }
        public bool _flagProcess;

        private bool _FlagOK;

        private UserWindowsIdentity _userWindowsIdentity;
        private async void formLogin_Load(object sender, EventArgs e)
        {
            await UpdateApplication();

            try
            {
                var windowsIdentity = System.Security.Principal.WindowsIdentity.GetCurrent();
                if (windowsIdentity != null)
                {
                    _userWindowsIdentity = new UserWindowsIdentity()
                    {
                        Name = windowsIdentity.Name
                    };

                    var userWork = await new XPQuery<set_LocalSettings>(DatabaseConnection.LocalSession)
                        .FirstOrDefaultAsync(f => f.Name == "User" && f.Value3 == _userWindowsIdentity.Name);

                    if (userWork != null)
                    {
                        _userWindowsIdentity.WorkName = userWork.Value1;
                    }
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }

            try
            {
                var localSetting = DatabaseConnection.LocalSession.FindObject<set_LocalSettings>(new BinaryOperator("Name", "tek_skin"));
                if (localSetting == null)
                {
                    UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");
                }
                else
                {
                    UserLookAndFeel.Default.SetSkinStyle(localSetting.Value1);
                }

                imageComboUsers.Properties.SmallImages = cls_App.ImgCol.imageCollectionUsers;

                int i = 0;
                var index = 0;

                var session = DatabaseConnection.GetWorkSession();
                var users = await new XPQuery<User>(session).OrderBy(o => o.Name).ToListAsync();
                if (users != null)
                {
                    foreach (var user in users)
                    {
                        imageComboUsers.Properties.Items.Add(new ImageComboBoxItem(user.ToString(), user));

                        if (string.IsNullOrWhiteSpace(_userWindowsIdentity.WorkName))
                        {
                            if ((DatabaseConnection.User != null) && (user.Oid == DatabaseConnection.User.Oid))
                            {
                                index = i;
                            }
                        }
                        else if (user.Login == _userWindowsIdentity.WorkName)
                        {
                            index = i;
                        }

                        i++;
                    }
                }

                imageComboUsers.SelectedIndex = index;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }

            txtPassword?.Focus();
        }

        private async void btnOK_Click(object sender, EventArgs e)
        {
            await AuthAsync();
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            var form = new SettingLoginForm();
            form.ShowDialog();
        }

        private async void formLogin_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && isOpenMessageBox is false)
            {
                await AuthAsync();
            }
            else
            {
                isOpenMessageBox = false;
            }
        }

        private bool isOpenMessageBox = false;

        private async System.Threading.Tasks.Task AuthAsync()
        {
            try
            {
                if (imageComboUsers.EditValue is null)
                {
                    XtraMessageBox.Show("Выберите пользователя, пожалуйста !?!", " Информационное сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    isOpenMessageBox = true;
                }
                else
                {
                    if (imageComboUsers.EditValue is User user)
                    {
                        if (user.Password != Mailbox.Encrypt(txtPassword.Text))
                        {
                            XtraMessageBox.Show("Указан неверный пароль.", "Информационное сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            isOpenMessageBox = true;
                        }
                        else
                        {
                            await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "User", user.Login, true, true);
                            await BVVGlobal.oFuncXpo.GetLocalSettingsOptionsAsync(DatabaseConnection.LocalSession, "Password", user.Password, true, true);

                            await BVVGlobal.oFuncXpo.FillSettingsParameters();
                            _FlagOK = true;


                            var userWork = await new XPQuery<set_LocalSettings>(DatabaseConnection.LocalSession)
                                .FirstOrDefaultAsync(f => f.Name == "User" && f.Value3 == _userWindowsIdentity.Name);
                            if (userWork is null)
                            {
                                userWork = new set_LocalSettings(DatabaseConnection.LocalSession);
                                userWork.Name = "User";
                                userWork.g_id = Guid.NewGuid().ToString();
                            }

                            userWork.Value1 = user.Login;
                            userWork.Value3 = _userWindowsIdentity.Name;
                            userWork.Save();

                            await DatabaseConnection.RememberWorkUser(user);

                            Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message); 
                isOpenMessageBox = true;
            }
        }
    }
}