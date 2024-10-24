using System;
using System.IO;
using System.Threading.Tasks;

namespace RMS.Parser.Core.Controllers
{
    public static class LoggerController
    {
        private static DateTime _dateTime => DateTime.Now;
        private static readonly string _fileName = "_log.txt";

        public static string CurrentDateTime => _dateTime.ToString("dd.MM.yyyy (HH.mm.ss)");

        public static async void WriteLog(object obj)
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
        }

        private static async Task<string> AppendLine(string text)
        {
            var obj = $"{CurrentDateTime} -> {text}{Environment.NewLine}";
            try
            {
                File.AppendAllText(_fileName, obj);
            }
            catch (Exception)
            {
                await Task.Delay(1000);
                obj = await AppendLine(text);
            }
            return obj;
        }
    }
}
