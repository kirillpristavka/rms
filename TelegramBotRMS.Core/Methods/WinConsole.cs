using DevExpress.Xpo;
using RMS.Core.TG.Core.Models;
using System;

namespace TelegramBotRMS.Core.Methods
{
    public static class WinConsole
    {
        public async static void ConsoleWriteLineWithColor(string message, ConsoleColor consoleColor, bool isUseLog = true)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                Console.ForegroundColor = consoleColor;
                Console.WriteLine(message);
                Console.ResetColor();

                if (isUseLog)
                {
                    try
                    {
                        await GetTgLogAsync(message).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        ConsoleWriteLineWithColor(ex.Message, ConsoleColor.Red, false);
                    }
                }
            }
        }

        private static async System.Threading.Tasks.Task GetTgLogAsync(string message)
        {
            using (var uof = new UnitOfWork())
            {
                var tgLog = new TGLog(uof);
                tgLog.SetObj(message);

                await uof.CommitTransactionAsync().ConfigureAwait(false);
            }
        }
    }
}
