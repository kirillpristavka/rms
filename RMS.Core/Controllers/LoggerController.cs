using DevExpress.Xpo;
using RMS.Core.Model.XPO;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RMS.Core.Controllers
{
    public static class LoggerController
    {
        private static DateTime _dateTime => DateTime.Now;
        private static readonly string _fileName = "_log.txt";
        private static readonly string _pathFile = $"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _fileName)}";

        public static string CurrentDateTime => _dateTime.ToString("dd.MM.yyyy (HH.mm.ss)");

        public static async Task WriteLogBaseAsync(object obj)
        {
            if (obj is string[] array)
            {
                foreach (var item in array)
                {
                    await AppendLineBaseAsync(item).ConfigureAwait(false);
                }
            }
            else if (obj is string str)
            {
                var splits = str.Split(new char[] { (char)10, (char)13 }, StringSplitOptions.RemoveEmptyEntries);
                if (splits != null && splits.Length > 0)
                {
                    foreach (var item in splits)
                    {
                        await AppendLineBaseAsync(item).ConfigureAwait(false);
                    }
                }
                else
                {
                    await AppendLineBaseAsync(str).ConfigureAwait(false);
                }
            }
        }

        private static async Task AppendLineBaseAsync(string text)
        {
            try
            {
                var obj = $"{CurrentDateTime} -> {text}";
                using (var uof = new UnitOfWork())
                {
                    uof.Save(new LoggerXPO(uof) { Message = obj });
                    await uof.CommitChangesAsync().ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
        }

        public static async void WriteLog(object obj)
        {
            await Task.Run(async () =>
            {
                if (obj is string[] array)
                {
                    foreach (var item in array)
                    {
                        await AppendLine(item);
                    }
                }
                else if (obj is string str)
                {
                    var splits = str.Split(new char[] { (char)10, (char)13 }, StringSplitOptions.RemoveEmptyEntries);
                    if (splits != null && splits.Length > 0)
                    {
                        foreach (var item in splits)
                        {
                            await AppendLine(item);
                        }
                    }
                    else
                    {
                        await AppendLine(str);
                    }
                }
            });            
        }

        private static async Task<string> AppendLine(string text, int countAttempt = 0)
        {
            var obj = $"{CurrentDateTime} -> {text}{Environment.NewLine}";
            try
            {
                File.AppendAllText(_pathFile, obj);
            }
            catch (Exception)
            {
                if (countAttempt < 10)
                {
                    await Task.Delay(1000);
                    obj = await AppendLine(text, countAttempt++);
                }
            }
            return obj;
        }
    }
}
