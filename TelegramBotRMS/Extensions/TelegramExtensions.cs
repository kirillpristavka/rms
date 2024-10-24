using DevExpress.Xpo;
using RMS.Core.Controllers;
using RMS.Core.TG.Core.Models;
using System;
using System.Linq;
using Telegram.Bot;
using TelegramBotRMS.Core.Methods;
using TelegramBotRMS.Core.Models;

namespace TelegramBotRMS.Extensions
{
    public static class TelegramExtensions
    {
        public async static System.Threading.Tasks.Task GetMe(this TelegramBotClient client)
        {
            try
            {
                WinConsole.ConsoleWriteLineWithColor($"[{DateTime.Now.ToString("dd.MM.yyyy (HH:mm:ss)")}] <-> Получение информации о текущем пользователе Telegram", ConsoleColor.Green);
                var user = await TelegramBot.GetTelegramBotClientUserAsync(client);

                WinConsole.ConsoleWriteLineWithColor($"[{DateTime.Now.ToString("dd.MM.yyyy (HH:mm:ss)")}] <-> Получение уникального идентификатора пользователя в базе данных", ConsoleColor.Green);
                var tgUserMeOid = await Program.GetTGUserOidAsync(user);

                if (user != null)
                {
                    try
                    {
                        using (var uof = new UnitOfWork())
                        {
                            var logs = await new XPQuery<TGLog>(uof)
                                .Where(w => w.Date >= DateTime.Now.Date.AddDays(-1))
                                .OrderByDescending(o => o.Date)
                                .Take(10)
                                .ToListAsync();

                            foreach (var log in logs.OrderBy(o => o.Date))
                            {
                                WinConsole.ConsoleWriteLineWithColor($"[{log.Date.ToString("dd.MM.yyyy (HH:mm:ss)")}] <-> {log.Text}.", ConsoleColor.Yellow, false);
                            }
                            Console.WriteLine($"{Environment.NewLine}");
                        }
                    }
                    catch (Exception ex)
                    {
                        await LoggerController.WriteLogBaseAsync(ex.ToString());
                        WinConsole.ConsoleWriteLineWithColor(ex.Message, ConsoleColor.Red);
                    }

                    WinConsole.ConsoleWriteLineWithColor($"[{DateTime.Now.ToString("dd.MM.yyyy (HH:mm:ss)")}] <-> Успешно пройдена авторизация.", ConsoleColor.DarkYellow);
                    WinConsole.ConsoleWriteLineWithColor($"[{DateTime.Now.ToString("dd.MM.yyyy (HH:mm:ss)")}] <-> Бот {user.Username} ({user.Id}) приступает к работе.", ConsoleColor.DarkYellow);
                }
                else
                {
                    WinConsole.ConsoleWriteLineWithColor($"[{DateTime.Now.ToString("dd.MM.yyyy (HH:mm:ss)")}] <-> Пользователь не найден", ConsoleColor.DarkRed);
                }
            }
            catch (Exception ex)
            {
                WinConsole.ConsoleWriteLineWithColor(ex.Message, ConsoleColor.Red);
            }
        }
    }
}
