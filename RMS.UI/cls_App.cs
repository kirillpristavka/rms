using System;
using System.IO;
using System.Threading;

namespace RMS.UI
{
    public class cls_AppParam
    {
        public string TmpDir { get; set; }
        public string OutDir { get; set; }
        public string TemplatesDir { get; set; }

        /// <summary>
        /// Стандартная почта для отправки сообщений.
        /// </summary>
        public string MailboxForSending { get; set; }

        /// <summary>
        /// Количество дней для приема писем.
        /// </summary>
        public string CountOfDaysToAcceptLetter { get; set; }

        /// <summary>
        /// Количество писем для сохранения коллекции.
        /// Если 0 то письмо сохраняется без заполнения коллекции.
        /// </summary>
        public string CountOfLetterToSave { get; set; }

        /// <summary>
        /// Путь к сервису обновлений.
        /// </summary>
        public string PathUpdateService { get; set; }

        /// <summary>
        /// Включение/отключение предварительного просмотра писем.
        /// </summary>
        public string EnableOrDisableEmailPreview { get; set; }

        /// <summary>
        /// Рабочий каталог с файлами.
        /// </summary>
        public string MyFolderPath { get; set; }

        /// <summary>
        /// Включение/отключение приема писем при старте программы.
        /// </summary>
        public string EnableOrDisableGetEmails { get; set; }
    }

    partial class cls_App
    {
        public cls_App()
        {
            WorkDate = DateTime.Now;
            HostName = System.Net.Dns.GetHostName();
            IP = System.Net.Dns.GetHostEntry(HostName).AddressList[0].ToString();
            UserNameWin = Environment.UserName;
            BaseDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            _flagProcess = false;
            AppWorkThread = null;
            ImgCol = new imgCollections();
            AppParam = new cls_AppParam();
        }

        public Thread AppWorkThread;

        public static imgCollections ImgCol;

        public enum EditFormVariants
        {
            None,                        // Форма редактирования - не определена
            Edit_Enumeration,            // пока нету
            Edit_TablePrintSettings      // formEdit_TablePrintSettings
        };

        public string BaseDirectory { get; }
        //public string TempDirectory { get; set; }


        /// <summary>
        /// рабочая дата
        /// </summary>
        public DateTime WorkDate { get; set; }

        /// <summary>
        /// версия программы
        /// </summary>
        public string ProgramVersion { get; set; }

        public string User { get; set; }
        public string Password { get; set; }

        /// <summary>
        /// Имя Компьютера, Имя пользователя, IP-адрес
        /// </summary>
        public string HostName { get; set; }
        public string UserNameWin { get; set; }
        public string IP { get; set; }

        public cls_AppParam AppParam;

        private bool _flagProcess;

        public bool Get_flagProcess() { return _flagProcess; }

        public void Progress_Start(int iBegin, int iEnd)
        {
            Program.MainForm.Progress_Start(iBegin, iEnd);
            _flagProcess = true;
        }

        public void Progress_Position(int iValue, bool flCaption = false)
        {
            Program.MainForm.Progress_Position(iValue, flCaption);
        }

        public void Progress_Stop()
        {
            Program.MainForm.Progress_Stop();
            _flagProcess = false;
        }

        public static System.Drawing.Point GetStartPositionPoint(int x, int y, int width = 0, int height = 0)
        {
            if (x < 0) x = 0;
            if (y < 0) y = 0;

            if ((x + width) > System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width) x = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - width;
            if ((y + height) > System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height) y = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - height;

            return new System.Drawing.Point(x, y);
        }

        public static int GetFormWidth(int width)
        {
            if (width > System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width) width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
            return width;
        }

        public static int GetFormHeight(int height)
        {
            if (height > System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height) height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            return height;
        }

        /// <summary>
        /// Скачивает файл по http
        /// </summary>
        /// <param name="lPath">откуда</param>
        /// <param name="lPathToPut">l_path2put - куда(полное имя директории)</param>
        /// <returns>полное имя файла</returns>
        public static string DownloadFile(System.Windows.Forms.Form any_form, string lPath, string lPathToPut)
        {
            if (lPath.Length == 0 || lPathToPut.Length == 0) return string.Empty;

            //Int64 size = 0;
            //System.Diagnostics.Stopwatch speedTimer = new System.Diagnostics.Stopwatch();
            string filename = lPathToPut + "\\" + lPath.Substring(lPath.LastIndexOf("/") + 1, lPath.Length - lPath.LastIndexOf("/") - 1);
            // string filename1 = String.Format("{0}\\{1}", l_path2put, l_Path.Substring(l_Path.LastIndexOf("/") + 1, l_Path.Length - l_Path.LastIndexOf("/") - 1));

            FileStream writer = null;
            System.Net.HttpWebRequest webReq = null;
            System.Net.HttpWebResponse webResp = null;

            try
            {
                writer = new FileStream(filename, System.IO.FileMode.Create);
                webReq = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(lPath);
                webResp = (System.Net.HttpWebResponse)webReq.GetResponse();
                //size = webResp.ContentLength;
                int numBytesToRead = (int)webResp.ContentLength;
                byte[] bytes = new byte[numBytesToRead];

                int currentPackageSize;
                int currentFileProgress = 0;
                any_form.BeginInvoke(new System.Windows.Forms.MethodInvoker(delegate { BVVGlobal.oApp.Progress_Start(0, numBytesToRead - 1); }));

                while (currentFileProgress < numBytesToRead)
                {
                    currentPackageSize = webResp.GetResponseStream().Read(bytes, 0, numBytesToRead);
                    currentFileProgress += currentPackageSize;
                    writer.Write(bytes, 0, currentPackageSize);
                    //Thread.Sleep(2000);
                    any_form.BeginInvoke(new System.Windows.Forms.MethodInvoker(delegate { BVVGlobal.oApp.Progress_Position(currentFileProgress, true); }));
                }

                any_form.BeginInvoke(new System.Windows.Forms.MethodInvoker(delegate { BVVGlobal.oApp.Progress_Stop(); }));
            }
            catch (Exception ex)
            {
                filename = string.Empty;

                throw new Exception("DownloadFile: " + ex.Message);
            }
            finally
            {
                if (writer != null) writer.Close();
                if (webResp != null) webResp.Close();
            }

            return filename;
        }


    }
}

