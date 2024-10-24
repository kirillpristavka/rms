using RMS.UI.Forms;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace RMS.UI
{
    static class Program
    {
        public static MainForm MainForm;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.UserSkins.BonusSkins.Register();

            KillChromeDriverProcesses();

            MainForm = new MainForm();
            Application.Run(MainForm);
        }
        
        public static void KillChromeDriverProcesses()
        {
            try
            {
                foreach (Process proc in Process.GetProcessesByName("chromedriver"))
                {
                    proc.Kill();
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }
    }

    class BVVGlobal
    {
        public static cls_App oApp = new cls_App();
        public static DatabaseConnection oXpo = new DatabaseConnection();
        public static cls_GlobalFunctionsXpo oFuncXpo = new cls_GlobalFunctionsXpo();
    }
}