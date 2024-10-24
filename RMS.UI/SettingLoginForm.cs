using DevExpress.Xpo;
using DevExpress.XtraEditors;
using System;
using System.Linq;
using System.Windows.Forms;

namespace RMS.UI
{
    public partial class SettingLoginForm : XtraForm
    {
        public SettingLoginForm()
        {
            InitializeComponent();
        }

        private async void btnClearLocalSettings_Click(object sender, EventArgs e)
        {
            try
            {
                if (XtraMessageBox.Show("Операция очистит параметры пользователей по автоматическому открытию вкладок.Продолжить?",
                        "Информационное сообщение",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Information) == DialogResult.OK)
                {
                    var localSession = DatabaseConnection.LocalSession;
                    if (localSession != null && localSession.IsConnected)
                    {
                        var parametrs = await new XPQuery<set_LocalSettings>(localSession)
                            .Where(w => w.Name != null && w.Name.Contains("OpenForms"))
                            .ToListAsync();

                        foreach (var param in parametrs)
                        {
                            param.Value1 = "False";
                            param.Save();
                        }

                        XtraMessageBox.Show("Операция успешно выполнена.",
                            "Информационное сообщение",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }                
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private async void btnClearUserSettingsView_Click(object sender, EventArgs e)
        {
            try
            {
                if (XtraMessageBox.Show("Операция очистит пользовательские настройки форм во всех разделах.Продолжить?",
                        "Информационное сообщение",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Information) == DialogResult.OK)
                {
                    var localSession = DatabaseConnection.LocalSession;
                    if (localSession != null && localSession.IsConnected)
                    {
                        var parametrs = await new XPQuery<set_LocalSettings>(localSession)
                            .Where(w => w.Name != null && (w.Name.Contains("_gridView") || w.Name.Contains("_view") || w.Name.Contains("_layout")))
                            .ToListAsync();
                        await localSession.DeleteAsync(parametrs);

                        XtraMessageBox.Show("Операция успешно выполнена.",
                            "Информационное сообщение",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }
    }
}