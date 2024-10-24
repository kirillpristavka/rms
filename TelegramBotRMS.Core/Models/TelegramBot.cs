using DevExpress.Xpo;
using Newtonsoft.Json;
using RMS.Core.Controllers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBotRMS.Core.Methods;
using TelegramBotRMS.Core.Models.Commands;
using TelegramBotRMS.Core.Models.Commands.CustomerCommands;
using TelegramBotRMS.Core.Models.Commands.OrganizationCommands;
using TelegramBotRMS.Core.Models.Core;

namespace TelegramBotRMS.Core.Models
{
    public static class TelegramBot
    {
        private static Session _session;
        
        private static TelegramBotClient client;
        private static User user;
        private static List<Command> commandsList;

        public static IReadOnlyList<Command> Commands { get => commandsList.AsReadOnly(); }

        public async static Task<User> GetTelegramBotClientUserAsync(Session session = null) 
        {
            if (user != null)
            {
                return user;
            }

            var client = GetTelegramBotClient(session);
            user = await client.GetMeAsync();
            return user;
        }

        public async static Task<User> GetTelegramBotClientUserAsync(TelegramBotClient client)
        {
            if (user != null)
            {
                return user;
            }

            user = await client.GetMeAsync();
            return user;
        }

        public static TelegramBotClient GetTelegramBotClient(Session session = null)
        {
            if (client != null)
            {
                return client;
            }

            GetAppSettings(session);
            commandsList = GetCommandList();
            client = new TelegramBotClient(AppSettings.Token);
            
            return client;
        }
        
        private static async void GetAppSettings(Session inSession = null)
        {
            var fileNameSettings = "TelegramBotSettings.json";

            WinConsole.ConsoleWriteLineWithColor($"[{DateTime.Now.ToString("dd.MM.yyyy (HH:mm:ss)")}] <-> Получение рабочей сессии", ConsoleColor.Green, false);
            var session = DatabaseConnection.GetWorkSession(inSession);
            if (session != null && session.IsConnected)
            {
                WinConsole.ConsoleWriteLineWithColor($"[{DateTime.Now.ToString("dd.MM.yyyy (HH:mm:ss)")}] <-> Поиск настроек в базе данных", ConsoleColor.Green, false);
                var settings = await session.FindObjectAsync<RMS.Setting.Model.GeneralSettings.Settings>(null);
                if (settings != null)
                {
                    if (!string.IsNullOrWhiteSpace(settings.TelegramBotToken))
                    {
                        WinConsole.ConsoleWriteLineWithColor($"[{DateTime.Now.ToString("dd.MM.yyyy (HH:mm:ss)")}] <-> Установка токена телеграмм бота, для дальнейшей работы", ConsoleColor.Green, false);
                        AppSettings.Token = settings.TelegramBotToken;
                    }
                }
                else
                {
                    WinConsole.ConsoleWriteLineWithColor($"[{DateTime.Now.ToString("dd.MM.yyyy (HH:mm:ss)")}] <-> Настройки в базе данных не найдены", ConsoleColor.Red, false);
                }
            }
            
            if (string.IsNullOrWhiteSpace(AppSettings.Token) && System.IO.File.Exists(fileNameSettings))
            {
                WinConsole.ConsoleWriteLineWithColor($"[{DateTime.Now.ToString("dd.MM.yyyy (HH:mm:ss)")}] <-> Чтение настроек телеграмм бота из файла", ConsoleColor.Green, false);
                try
                {
                    WinConsole.ConsoleWriteLineWithColor($"[{DateTime.Now.ToString("dd.MM.yyyy (HH:mm:ss)")}] <-> Обработка файла {fileNameSettings}", ConsoleColor.Green, false);
                    var json = System.IO.File.ReadAllText(fileNameSettings);
                    var telegramBotSettings = JsonConvert.DeserializeObject<TelegramBotSettings>(json);

                    if (telegramBotSettings != null)
                    {
                        WinConsole.ConsoleWriteLineWithColor($"[{DateTime.Now.ToString("dd.MM.yyyy (HH:mm:ss)")}] <-> Установка токена телеграмм бота из файла, для дальнейшей работы", ConsoleColor.Green, false);
                        AppSettings.Token = telegramBotSettings.Token;

                        if (!string.IsNullOrWhiteSpace(AppSettings.Token))
                        {
                            if (session != null && session.IsConnected)
                            {
                                var settings = await session.FindObjectAsync<RMS.Setting.Model.GeneralSettings.Settings>(null);
                                if (settings != null)
                                {
                                    if (!string.IsNullOrWhiteSpace(AppSettings.Token))
                                    {
                                        settings.TelegramBotToken = AppSettings.Token;
                                        settings.Save();

                                        try
                                        {
                                            System.IO.File.Delete(fileNameSettings);
                                        }
                                        catch (Exception ex)
                                        {
                                            await LoggerController.WriteLogBaseAsync(ex.ToString());
                                            WinConsole.ConsoleWriteLineWithColor(ex.Message, ConsoleColor.Red);
                                        } 
                                    }
                                }
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    await LoggerController.WriteLogBaseAsync(ex.ToString());
                    WinConsole.ConsoleWriteLineWithColor(ex.Message, ConsoleColor.Red);
                }
            }  
        }
        
        private static List<Command> GetCommandList()
        {
            var commandsList = new List<Command>();
            
            commandsList.Add(new StartCommand());
            commandsList.Add(new AuthorizationCommand());

            commandsList.Add(new TaskCommand());
            commandsList.Add(new TaskNowCommand());
            commandsList.Add(new DealCommand());
            commandsList.Add(new ControlSystemCommand());
            commandsList.Add(new ElectronicReportingCommand());
            commandsList.Add(new PatentCommand());
            commandsList.Add(new MyCustomerCommand());
            commandsList.Add(new MyCustomerInfoCommand());

            commandsList.Add(new CustomerInfoCommand());
            commandsList.Add(new CustomerMyOrganizations());
            commandsList.Add(new CustomerMyStaffCommand());
            commandsList.Add(new CustomerMyDocumentCommand());
            commandsList.Add(new CustomerMyStaffDocumentCommand());

            return commandsList;
        }
    }
}
