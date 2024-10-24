using DevExpress.Xpo;
using RMS.Core.Controllers;
using RMS.Core.TG.Core.Controllers;
using RMS.Core.TG.Core.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using TelegramBotRMS.Core.Controllers;
using TelegramBotRMS.Core.Methods;
using TelegramBotRMS.Core.Models;
using TelegramBotRMS.Core.Models.Core;
using TelegramBotRMS.Extensions;

namespace TelegramBotRMS
{
    public class Program
    {
        private static Session _workSession;
        private static DateTime dateRestart = DateTime.Now.AddHours(3);
        private static Timer timer = null;

        private static void TimerCallback(Object o)
        {
            var timeRestart = dateRestart - DateTime.Now;

            if (timeRestart.TotalSeconds < 0)
            {
                Process.Start(Assembly.GetExecutingAssembly().Location);
                Environment.Exit(0);
            }
        }

        static void Main(string[] args)
        {
            WinConsole.ConsoleWriteLineWithColor($"[{DateTime.Now.ToString("dd.MM.yyyy (HH:mm:ss)")}] <-> Запуск TelegramBotRMS", ConsoleColor.Green, false);
            WinConsole.ConsoleWriteLineWithColor($"[{DateTime.Now.ToString("dd.MM.yyyy (HH:mm:ss)")}] <-> Соединение с базой данных", ConsoleColor.Green, false);
            _workSession = DatabaseConnection.GetWorkSession();

            WinConsole.ConsoleWriteLineWithColor($"[{DateTime.Now.ToString("dd.MM.yyyy (HH:mm:ss)")}] <-> Установка таймера для перезагрузки", ConsoleColor.Green, false);
            timer = new Timer(TimerCallback, null, 0, 5000);

            var cts = new CancellationTokenSource();

            WinConsole.ConsoleWriteLineWithColor($"[{DateTime.Now.ToString("dd.MM.yyyy (HH:mm:ss)")}] <-> Соединение с сервисом Telegram", ConsoleColor.Green, false);
            var client = TelegramBot.GetTelegramBotClient();
            client.GetMe().Wait();

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }
            };

            WinConsole.ConsoleWriteLineWithColor($"[{DateTime.Now.ToString("dd.MM.yyyy (HH:mm:ss)")}] <-> Запуск службы приема сообщений Telegram", ConsoleColor.Green, false);
            client.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken: cts.Token);

            System.Threading.Tasks.Task.Run(() =>
            {
                while (true)
                {
                    var command = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(command)
                        && command.Equals("exit", StringComparison.OrdinalIgnoreCase))
                    {
                        Environment.Exit(0);
                    }
                }
            }).Wait();

            cts.Cancel();
        }

        public static async System.Threading.Tasks.Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {            
            var tgUserSenderOid = await GetTGUserOidAsync(update?.Message?.From);
            var tgUserRecipientOid = await GetTGUserOidAsync(await TelegramBot.GetTelegramBotClientUserAsync());
            await GetTGMessageAsync(update?.Message, tgUserSenderOid, tgUserRecipientOid).ConfigureAwait(false);

            try
            {
                switch (update.Type)
                {
                    case Telegram.Bot.Types.Enums.UpdateType.Unknown:
                        break;
                    case Telegram.Bot.Types.Enums.UpdateType.Message:
                        OnMessage(botClient, update.Message);
                        break;
                    case Telegram.Bot.Types.Enums.UpdateType.InlineQuery:
                        break;
                    case Telegram.Bot.Types.Enums.UpdateType.ChosenInlineResult:
                        break;
                    case Telegram.Bot.Types.Enums.UpdateType.CallbackQuery:
                        OnCallbackQuery(botClient, update.CallbackQuery);
                        break;
                    case Telegram.Bot.Types.Enums.UpdateType.EditedMessage:
                        OnMessage(botClient, update.EditedMessage);
                        break;
                    case Telegram.Bot.Types.Enums.UpdateType.ChannelPost:
                        break;
                    case Telegram.Bot.Types.Enums.UpdateType.EditedChannelPost:
                        break;
                    case Telegram.Bot.Types.Enums.UpdateType.ShippingQuery:
                        break;
                    case Telegram.Bot.Types.Enums.UpdateType.PreCheckoutQuery:
                        break;
                    case Telegram.Bot.Types.Enums.UpdateType.Poll:
                        break;
                    case Telegram.Bot.Types.Enums.UpdateType.PollAnswer:
                        break;
                    case Telegram.Bot.Types.Enums.UpdateType.MyChatMember:
                        break;
                    case Telegram.Bot.Types.Enums.UpdateType.ChatMember:
                        break;
                    case Telegram.Bot.Types.Enums.UpdateType.ChatJoinRequest:
                        break;
                    default:
                        break;
                }
            }
            catch (Exception exception)
            {
                await HandleErrorAsync(botClient, exception, cancellationToken);
            }
        }

        public static async System.Threading.Tasks.Task GetTGMessageAsync(Message message, int? tgUserSenderOid, int? tgUserUserRecipientOid)
        {
            if (tgUserSenderOid != null)
            {
                if (message != null)
                {
                    using (var uof = new UnitOfWork())
                    {
                        var tgUserSender = await new XPQuery<TGUser>(uof).FirstOrDefaultAsync(f => f.Oid == tgUserSenderOid);
                        if (tgUserSender != null)
                        {
                            var tgMessage = await new XPQuery<TGMessage>(uof).FirstOrDefaultAsync(f => f.Id == message.MessageId);
                            if (tgMessage is null)
                            {
                                tgMessage = new TGMessage(uof);
                            }

                            var currentMessage = new TGMessage()
                            {
                                Id = message.MessageId,
                                Date = message.Date,
                                TGUserSender = tgUserSender,
                                TGUserRecipient = await new XPQuery<TGUser>(uof).FirstOrDefaultAsync(f => f.Oid == tgUserUserRecipientOid)
                            };

                            if (!string.IsNullOrWhiteSpace(message.Text))
                            {
                                currentMessage.SetObj(message.Text);
                            }
                            else if (!string.IsNullOrWhiteSpace(message.Caption))
                            {
                                currentMessage.SetObj(message.Caption);
                            }

                            if (message.Document != null)
                            {
                                var document = TGDocumentController.CreateDocument(uof, message.Document);
                                if (document != null)
                                {
                                    await document.DownloadFileAsync(TelegramBot.GetTelegramBotClient());
                                    tgMessage.TGDocument = document;
                                    document.Save();
                                }
                            }

                            if (message.Photo != null)
                            {
                                foreach (var photo in message.Photo)
                                {
                                    var obj = TGPhotoSizeController.CreatePhoto(uof, message.Photo[0]);
                                    if (obj != null)
                                    {
                                        await obj.DownloadFileAsync(TelegramBot.GetTelegramBotClient());
                                        var tgMessagePhoto = new TGMessagePhoto(uof)
                                        {
                                            TGPhoto = obj,
                                            TGMessage = tgMessage
                                        };
                                        tgMessagePhoto.Save();

                                        tgMessage.TGMessagePhotos.Add(tgMessagePhoto);
                                        obj.Save();
                                    }
                                }                                
                            }

                            if (await tgMessage.Edit(currentMessage))
                            {
                                await uof.CommitTransactionAsync().ConfigureAwait(false);

                                WinConsole.ConsoleWriteLineWithColor($"[{DateTime.Now.ToString("dd.MM.yyyy (HH:mm:ss)")}] <-> Получение сообщение от пользователя {tgUserSender} => {currentMessage.Text}", ConsoleColor.Cyan);
                            }
                        }
                    }
                }

            }
        }
        
        public static async System.Threading.Tasks.Task<int?> GetTGUserOidAsync(User user)
        {
            if (user != null)
            {
                using (var uof = new UnitOfWork())
                {
                    var isNewUser = false;
                    
                    WinConsole.ConsoleWriteLineWithColor($"[{DateTime.Now.ToString("dd.MM.yyyy (HH:mm:ss)")}] <-> Поиск пользователя в базе данных по ID [{user.Id}].", ConsoleColor.Green);
                    var tgUser = await new XPQuery<TGUser>(uof).FirstOrDefaultAsync(f => f.Id == user.Id);
                    if (tgUser is null)
                    {
                        tgUser = new TGUser(uof); 
                        WinConsole.ConsoleWriteLineWithColor($"[{DateTime.Now.ToString("dd.MM.yyyy (HH:mm:ss)")}] <-> Создание нового пользователя с ID [{user.Id}].", ConsoleColor.Green);
                        isNewUser = true;
                    }

                    var currentUser = new TGUser()
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        IsBot = user.IsBot,
                        UserName = user.Username
                    };
                    WinConsole.ConsoleWriteLineWithColor($"[{DateTime.Now.ToString("dd.MM.yyyy (HH:mm:ss)")}] <-> Обновление информации о пользователе.", ConsoleColor.Green);

                    if (tgUser.Avatar is null)
                    {
                        try
                        {
                            WinConsole.ConsoleWriteLineWithColor($"[{DateTime.Now.ToString("dd.MM.yyyy (HH:mm:ss)")}] <-> Загрузка аватара пользователя.", ConsoleColor.Green);
                            currentUser.Avatar = await TGUserController.GetUserAvatar(tgUser);

                            if (currentUser.Avatar != null)
                            {
                                WinConsole.ConsoleWriteLineWithColor($"[{DateTime.Now.ToString("dd.MM.yyyy (HH:mm:ss)")}] <-> Скачан новый аватар пользователя {tgUser}.", ConsoleColor.Cyan);
                            }                            
                        }
                        catch (Exception ex)
                        {
                            await LoggerController.WriteLogBaseAsync(ex.ToString());
                            WinConsole.ConsoleWriteLineWithColor(ex.Message, ConsoleColor.Red);
                        }
                    }

                    if (tgUser.Edit(currentUser))
                    {
                        try
                        {
                            WinConsole.ConsoleWriteLineWithColor($"[{DateTime.Now.ToString("dd.MM.yyyy (HH:mm:ss)")}] <-> Сохранение информации о пользователе.", ConsoleColor.Green);
                            await uof.CommitTransactionAsync();
                        }
                        catch (Exception ex)
                        {
                            await LoggerController.WriteLogBaseAsync(ex.ToString());
                            WinConsole.ConsoleWriteLineWithColor(ex.Message, ConsoleColor.Red);
                        }
                        
                        if (isNewUser)
                        {
                            WinConsole.ConsoleWriteLineWithColor($"[{DateTime.Now.ToString("dd.MM.yyyy (HH:mm:ss)")}] <-> Добавлен новый пользователь {tgUser}.", ConsoleColor.Cyan);
                        }
                        else
                        {
                            WinConsole.ConsoleWriteLineWithColor($"[{DateTime.Now.ToString("dd.MM.yyyy (HH:mm:ss)")}] <-> Изменилось описание пользователя {tgUser}.", ConsoleColor.Cyan);
                        }
                    }

                    return tgUser.Oid;
                }

            }
            else
            {
                WinConsole.ConsoleWriteLineWithColor($"[{DateTime.Now.ToString("dd.MM.yyyy (HH:mm:ss)")}] <-> Не задан пользователь для метода {nameof(GetTGUserOidAsync)}.", ConsoleColor.Red);
            }

            return default;
        }

        public static System.Threading.Tasks.Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var error = exception?.Message ?? exception?.ToString();
            WinConsole.ConsoleWriteLineWithColor($"[{DateTime.Now.ToString("dd.MM.yyyy (HH:mm:ss)")}] <-> {error}", ConsoleColor.Red);
            return System.Threading.Tasks.Task.CompletedTask;
        }

        private static async void OnCallbackQuery(object sender, CallbackQuery callbackQuery)
        {
            var commands = TelegramBot.Commands;
            var client = TelegramBot.GetTelegramBotClient();

            foreach (var command in commands)
            {
                if (command.Contains(callbackQuery))
                {
                    try
                    {
                        command.GetCallbackQuery(callbackQuery);
                        await command.Execute(callbackQuery.Message, client);
                    }
                    catch (Exception ex)
                    {
                        await LoggerController.WriteLogBaseAsync(ex.ToString());
                        WinConsole.ConsoleWriteLineWithColor(ex.Message, ConsoleColor.Red);
                    }

                    break;
                }
            }
        }

        private static async void OnMessage(object sender, Message message)
        {
            var commands = TelegramBot.Commands;
            var client = TelegramBot.GetTelegramBotClient();
            
            try
            {
                var dateTime = DateTime.Now;                
                var text = $"[{dateTime.ToShortDateString()} ({dateTime:HH:mm:ss})] <-> {message.Chat.Username} ({message.Chat.Id}) => {message.Text}";
                WinConsole.ConsoleWriteLineWithColor(text, ConsoleColor.Green);

                if (Guid.TryParse(message.Text, out Guid guid))
                {
                    await UserController.UserAuthorizationAsync(message, guid);                    
                    return;
                }
            }
            catch (Exception ex)
            {
                await LoggerController.WriteLogBaseAsync(ex.ToString());
                WinConsole.ConsoleWriteLineWithColor(ex.Message, ConsoleColor.Red);
            }

            foreach (var command in commands)
            {
                if (command.Contains(message))
                {
                    try
                    {
                        await command.Execute(message, client);
                    }
                    catch (Exception ex)
                    {
                        await LoggerController.WriteLogBaseAsync(ex.ToString());
                        WinConsole.ConsoleWriteLineWithColor(ex.Message, ConsoleColor.Red);
                    }

                    break;
                }
            }
        }        
    }
}
