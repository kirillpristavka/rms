using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Security;
using MimeKit;
using RMS.Core.Controllers;
using RMS.Core.Enumerator;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Mail;
using RMS.Setting.Model.GeneralSettings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using TelegramBotRMS.Core.Methods;
using TelegramBotRMS.Core.Models;
using TelegramBotRMS.Core.Models.Core;
using static RMS.Core.Model.Mail.MailClients;

namespace MailConsoleRMS
{
    class Program
    {
        private static DateTime _dateRestart = DateTime.Now.AddHours(2);
        private static Timer _timer = null;

        private static DateTime? _emailFilteringDate;
        private static DateTime _dateTime => DateTime.Now;

        const SecureSocketOptions SslOptions = SecureSocketOptions.Auto;
        private static IEnumerable<string> EmailsSpam;

        private static void TimerCallback(Object o)
        {
            var timeRestart = _dateRestart - DateTime.Now;

            if (timeRestart.TotalSeconds < 0)
            {
                Process.Start(Assembly.GetExecutingAssembly().Location);
                Environment.Exit(0);
            }
        }

        static void Main(string[] args)
        {
            _timer = new Timer(TimerCallback, null, 0, 5000);

            using (var uof = new UnitOfWork(DatabaseConnection.GetWorkSession().DataLayer))
            {
                var mailBoxs = new XPCollection<Mailbox>(uof); 
                EmailsSpam = GetSpamEmail(uof).Result;
                foreach (var mailBox in mailBoxs)
                {
                    using (var client = new IdleClient(mailBox.MailboxSetup.IncomingMailServerIMAP,
                        mailBox.MailboxSetup.PortIMAP, SslOptions, mailBox.MailingAddress, Mailbox.Decrypt(mailBox.Password), mailBox, mailBox.AccessToken))
                    {
                        var idleTask = client.RunAsync();
                        WinConsole.ConsoleWriteLineWithColor($"[{_dateTime.ToShortDateString()} ({_dateTime:HH:mm:ss})] Запущен обработчик писем для [{mailBox}]", ConsoleColor.Yellow);

                        Task.Run(() => {
                            Console.ReadKey(true);
                        }).Wait();

                        client.Exit();

                        idleTask.GetAwaiter().GetResult();
                    }
                }
            }
            
            Console.ReadLine();
        }      

        class IdleClient : IDisposable
        {
            readonly string host, username, password, token;
            readonly SecureSocketOptions sslOptions;
            readonly int port;
            List<IMessageSummary> messages;
            CancellationTokenSource cancel;
            CancellationTokenSource done;
            bool messagesArrived;
            ImapClient client;
            Mailbox mailbox;

            public IdleClient(string host, int port, SecureSocketOptions sslOptions, string username, string password, Mailbox mailbox, string token = default)
            {
                this.client = new ImapClient();
                //this.client = new ImapClient(new ProtocolLogger(Console.OpenStandardError()));
                this.messages = new List<IMessageSummary>();
                this.cancel = new CancellationTokenSource();
                this.sslOptions = sslOptions;
                this.username = username;
                this.password = password;
                this.mailbox = mailbox;
                this.token = token;
                this.host = host;
                this.port = port;
            }

            async Task ReconnectAsync()
            {
                if (!client.IsConnected)
                {
                    await client.ConnectAsync(host, port, sslOptions, cancel.Token);
                }

                if (!client.IsAuthenticated)
                {
                    if (string.IsNullOrWhiteSpace(token))
                    {
                        await client.AuthenticateAsync(username, password, cancel.Token);
                    }
                    else
                    {
                        var oauth2 = new SaslMechanismOAuth2(username, token);
                        await client.AuthenticateAsync(oauth2, cancel.Token);
                    }

                    await client.Inbox.OpenAsync(FolderAccess.ReadWrite, cancel.Token);
                }
            }

            private async Task FetchMessageSummariesAsync(bool isSave)
            {
                IList<IMessageSummary> fetched = null;

                do
                {
                    try
                    {
                        int startIndex = messages.Count;
                        
                        WinConsole.ConsoleWriteLineWithColor($"[{_dateTime.ToShortDateString()} ({_dateTime:HH:mm:ss})] Начата обработка папки [Входящие] для {mailbox} ...", ConsoleColor.Yellow);
                        fetched = client.Inbox.Fetch(startIndex, -1, MessageSummaryItems.UniqueId | MessageSummaryItems.SaveDate | MessageSummaryItems.InternalDate, cancel.Token);
                        
                        WinConsole.ConsoleWriteLineWithColor($"[{_dateTime.ToShortDateString()} ({_dateTime:HH:mm:ss})] Найдено писем: {fetched.Count}", ConsoleColor.Yellow);
                        await SendTelegramMessageAdministrator($"[{_dateTime.ToShortDateString()} ({_dateTime:HH:mm:ss})] Для {mailbox} найдено писем: {fetched.Count}");
                        
                        await InitialCheckAsync(fetched).ConfigureAwait(false);
                        break;
                    }
                    catch (ImapProtocolException ex)
                    {
                        LoggerController.WriteLog(ex?.ToString());
                        await LoggerController.WriteLogBaseAsync(ex.ToString());
                        WinConsole.ConsoleWriteLineWithColor(ex.Message, ConsoleColor.Red);
                        await ReconnectAsync();
                    }
                    catch (IOException ex)
                    {
                        LoggerController.WriteLog(ex?.ToString());
                        await LoggerController.WriteLogBaseAsync(ex.ToString());
                        WinConsole.ConsoleWriteLineWithColor(ex.Message, ConsoleColor.Red);
                        await ReconnectAsync();
                    }
                } 
                while (true);

                foreach (var message in fetched)
                {
                    if (isSave)
                    {
                        try
                        {
                            using (var uof = new UnitOfWork(DatabaseConnection.GetWorkSession().DataLayer))
                            {
                                var messageInfoCollection = await GetMessagesInfoAsync(uof, mailbox);

                                var isUniqueIdFirstCheck = true;
                                if (isUniqueIdFirstCheck && messageInfoCollection.FirstOrDefault(f => f.UniqueId == message.UniqueId.Id) != null)
                                {
                                    continue;
                                }

                                var mes = await message.Folder.GetMessageAsync(message.UniqueId, cancel.Token);

                                if (messageInfoCollection.Contains(new MessageInfo(mes.MessageId, message.UniqueId.Id, message.Date.DateTime)))
                                {
                                    continue;
                                }

                                try
                                {
                                    if (_emailFilteringDate != null && _emailFilteringDate > mes.Date.DateTime)
                                    {
                                        WinConsole.ConsoleWriteLineWithColor($"Письмо от {mes.From?.ToString()?.Trim()?.Replace('<', '[')?.Replace('>', ']')} пропущено. " +
                                            $"[{Convert.ToDateTime(_emailFilteringDate).ToShortDateString()} > {message.Date.DateTime.ToShortDateString()}]", ConsoleColor.Green);
                                        continue;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    await LoggerController.WriteLogBaseAsync(ex.ToString());
                                    WinConsole.ConsoleWriteLineWithColor(ex.Message, ConsoleColor.Red);
                                }

                                var letter = await CreateLetter(mes, message.Folder, uof, message.UniqueId.Id, cancel.Token);                                
                                if (letter != null)
                                {                                    
                                    await uof.CommitTransactionAsync(cancel.Token);
                                    await message.Folder.AddFlagsAsync(message.UniqueId, MessageFlags.Seen, true);
                                    
                                    var msgAdmin = $"[{_dateTime.ToShortDateString()} ({_dateTime:HH:mm:ss})] [{client.Inbox}] В БД СКиД сохранено новое письмо от {letter.LetterSender.Replace('<', '[').Replace('>', ']')}";
                                    WinConsole.ConsoleWriteLineWithColor(msgAdmin, ConsoleColor.Cyan);
                                    
                                    await SendTelegramMessageAdministrator(msgAdmin);
                                    await SendTelegramMessageLetter(letter, cancel.Token);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            await LoggerController.WriteLogBaseAsync(ex.ToString());
                            WinConsole.ConsoleWriteLineWithColor(ex.Message, ConsoleColor.Red);
                        }                        
                    }    
                    messages.Add(message);
                }
            }
            
            private async Task InitialCheckAsync(IList<IMessageSummary> fetched, ConsoleColor consoleColor = ConsoleColor.DarkGray)
            {
                using (var uof = new UnitOfWork())
                {
                    var currentDateTime = _dateTime.AddDays(-7).Date;
                    WinConsole.ConsoleWriteLineWithColor($"[{_dateTime.ToShortDateString()} ({_dateTime:HH:mm:ss})] Запущена проверка соответствия полученных сообщений от {currentDateTime.ToShortDateString()}", consoleColor);

                    var uniqueIds = fetched
                        ?.Where(w => w.Date >= currentDateTime)
                        ?.Select(s => s.UniqueId)
                        ?.ToList();
                    WinConsole.ConsoleWriteLineWithColor($"[{_dateTime.ToShortDateString()} ({_dateTime:HH:mm:ss})] Уникальных идентификаторов на сервере: {uniqueIds.Count}", consoleColor);
                    
                    var letterUniqueIds = await new XPQuery<Letter>(uof)
                        ?.Where(w => w.DateReceiving >= currentDateTime && w.UniqueId != null)
                        ?.Select(s => new UniqueId((uint)s.UniqueId))
                        ?.ToListAsync();
                    WinConsole.ConsoleWriteLineWithColor($"[{_dateTime.ToShortDateString()} ({_dateTime:HH:mm:ss})] Уникальных идентификаторов в базе данных: {letterUniqueIds.Count}", consoleColor);

                    var newUids = uniqueIds.Except(letterUniqueIds);
                    WinConsole.ConsoleWriteLineWithColor($"[{_dateTime.ToShortDateString()} ({_dateTime:HH:mm:ss})] Количество сообщений не записанных в базе данных: {newUids.Count()}", consoleColor);

                    if (newUids != null && newUids.Count() > 0)
                    {
                        foreach (var uid in newUids)
                        {
                            try
                            {
                                await SaveLetterAsync(uof, fetched, uid);
                            }
                            catch (ServiceNotConnectedException ex)
                            {
                                WinConsole.ConsoleWriteLineWithColor(ex.Message, ConsoleColor.Red);
                                await ReconnectAsync();

                                try
                                {
                                    await SaveLetterAsync(uof, fetched, uid);
                                }
                                catch (Exception exS)
                                {
                                    WinConsole.ConsoleWriteLineWithColor(exS.Message, ConsoleColor.Red);
                                }

                            }
                            catch (Exception ex)
                            {
                                await LoggerController.WriteLogBaseAsync(ex.ToString());
                                WinConsole.ConsoleWriteLineWithColor(ex.Message, ConsoleColor.Red);
                            }
                        }
                    }
                    WinConsole.ConsoleWriteLineWithColor($"[{_dateTime.ToShortDateString()} ({_dateTime:HH:mm:ss})] Окончена проверка соответствия полученных сообщений от {currentDateTime.ToShortDateString()}", consoleColor);
                }
            }

            public async Task SaveLetterAsync(UnitOfWork uof, IList<IMessageSummary> fetched, UniqueId uid)
            {
                WinConsole.ConsoleWriteLineWithColor($"[{_dateTime.ToShortDateString()} ({_dateTime:HH:mm:ss})] Обработка почтового сообщения с уникальным номером: {uid}", ConsoleColor.Green);
                var message = fetched?.FirstOrDefault(f => f.UniqueId == uid);
                if (message is null)
                {
                    return;
                }

                var messageInfoCollection = await GetMessagesInfoAsync(uof, mailbox);

                var isUniqueIdFirstCheck = true;
                if (isUniqueIdFirstCheck && messageInfoCollection.FirstOrDefault(f => f.UniqueId == message.UniqueId.Id) != null)
                {
                    return;
                }

                var mes = await message.Folder?.GetMessageAsync(uid, cancel.Token);

                try
                {
                    if (_emailFilteringDate != null && _emailFilteringDate > mes.Date.DateTime)
                    {
                        WinConsole.ConsoleWriteLineWithColor($"Письмо от {mes.From?.ToString()?.Trim()?.Replace('<', '[')?.Replace('>', ']')} пропущено. " +
                            $"[{Convert.ToDateTime(_emailFilteringDate).ToShortDateString()} > {message.Date.DateTime.ToShortDateString()}]", ConsoleColor.Green);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    await LoggerController.WriteLogBaseAsync(ex.ToString());
                    WinConsole.ConsoleWriteLineWithColor(ex.Message, ConsoleColor.Red);
                }

                var letter = await CreateLetter(mes, message.Folder, uof, message.UniqueId.Id);
                if (letter != null)
                {
                    await uof.CommitTransactionAsync(cancel.Token);
                    await message.Folder.AddFlagsAsync(message.UniqueId, MessageFlags.Seen, true);

                    var msgAdmin = $"[{_dateTime.ToShortDateString()} ({_dateTime:HH:mm:ss})] [{client.Inbox}] В БД СКиД сохранено новое письмо от {letter.LetterSender.Replace('<', '[').Replace('>', ']')}";
                    WinConsole.ConsoleWriteLineWithColor(msgAdmin, ConsoleColor.Cyan);

                    await SendTelegramMessageAdministrator(msgAdmin);
                    await SendTelegramMessageLetter(letter, cancel.Token);
                }
            }

            public static async Task SendTelegramMessageAdministrator(string message, CancellationToken token = default)
            {
                try
                {
                    using (var uof = new UnitOfWork(DatabaseConnection.GetWorkSession().DataLayer))
                    {
                        var user = await uof.FindObjectAsync<RMS.Core.Model.User>(new NotOperator(new NullOperator(nameof(RMS.Core.Model.User.TelegramUserId))), token);
                        if (user != null)
                        {
                            var client = TelegramBot.GetTelegramBotClient(DatabaseConnection.GetWorkSession());
                            await client.SendTextMessageAsync(user.TelegramUserId, message, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html, cancellationToken: token);
                        }
                    }
                }
                catch (Exception ex)
                {
                    await LoggerController.WriteLogBaseAsync(ex.ToString());
                    WinConsole.ConsoleWriteLineWithColor(ex.Message, ConsoleColor.Red);
                }
            }

            public static async Task SendTelegramMessageLetter(Letter letter, CancellationToken token = default)
            {
                try
                {
                    var client = TelegramBot.GetTelegramBotClient(DatabaseConnection.GetWorkSession());
                    var staff = letter?.Customer?.PrimaryResponsible;

                    if (staff is null)
                    {
                        staff = letter?.Customer?.AccountantResponsible;
                    }
                    
                    if (staff != null && staff.TelegramUserId != null)
                    {
                        var text = $"📩 Получено новое письмо от <i>{letter?.Customer}</i>";
                        
                        text += $"{Environment.NewLine}<u>Письмо создано</u>: {letter.DateCreate.ToShortDateString()} ({letter.DateCreate.ToString("HH:mm:ss")})";                        
                        text += $"{Environment.NewLine}<u>Письмо получено</u>: {letter.DateReceiving.ToShortDateString()} ({letter.DateReceiving.ToString("HH:mm:ss")})";
                        
                        if (letter.LetterCatalog != null)
                        {
                            text += $"{Environment.NewLine}<u>Каталог</u>: {letter.LetterCatalog}";
                        }

                        if (!string.IsNullOrWhiteSpace(letter.TopicString))
                        {
                            text += $"{Environment.NewLine}<u>Тема</u>: {letter.TopicString}";
                        }

                        await client.SendTextMessageAsync(staff.TelegramUserId, text, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html, cancellationToken: token);
                        WinConsole.ConsoleWriteLineWithColor(text, ConsoleColor.DarkMagenta);
                        await SendTelegramMessageAdministrator($"[{_dateTime.ToShortDateString()} ({_dateTime:HH:mm:ss})] Пользователю {staff} отправлено следующее уведомление:{Environment.NewLine}{Environment.NewLine}{text}");
                    }
                }
                catch (Exception ex)
                {
                    await LoggerController.WriteLogBaseAsync(ex.ToString());
                    WinConsole.ConsoleWriteLineWithColor(ex.Message, ConsoleColor.Red);
                }
            }

            private async Task<Letter> CreateLetter(MimeMessage message, IMailFolder folder, UnitOfWork uof, uint? uniqueId = default, CancellationToken token = default)
            {
                var isSpam = await MailClients.IsSpamСheckAsync(uof, message.From.ToString(), token);

                var typeLetter = default(TypeLetter);
                var customer = default(Customer);

                if (isSpam)
                {
                    typeLetter = TypeLetter.Spam;
                    customer = null;
                }
                else
                {
                    typeLetter = MailClients.GetTypeLetter(folder);
                    customer = await MailClients.GetCustomerAsync(uof, message.From.Mailboxes.FirstOrDefault(f => message.From.ToString().Contains(f.Address))?.Address?.Trim(), token);
                }

                var letter = await uof.FindObjectAsync<Letter>(new BinaryOperator(nameof(Letter.MessageId), message.MessageId), token);

                if (letter != null)
                {
                    return letter;
                }
                
                letter = new Letter(uof)
                {
                    UniqueId = uniqueId,
                    DateCreate = message.Date.DateTime,
                    TextBody = Letter.StringToByte(message.TextBody),
                    HtmlBody = Letter.StringToByte(message.HtmlBody),
                    LetterSender = message.From?.ToString()?.Trim(),
                    LetterSenderAddress = message.From.Mailboxes.FirstOrDefault(f => message.From.ToString().Contains(f.Address))?.Address?.Trim(),
                    IsRead = false,
                    Topic = Letter.StringToByte(message.Subject),
                    MessageId = message.MessageId,
                    Customer = customer,
                    DateReceiving = DateTime.Now,
                    LetterSize = 0M,
                    LetterRecipient = message.To.ToString().Trim(),
                    TypeLetter = typeLetter,
                    LetterRecipientAddress = message.To.Mailboxes.FirstOrDefault(f => message.To.ToString().Contains(f.Address))?.Address?.Trim()
                };
                letter.SetMailBox(await uof.GetObjectByKeyAsync<Mailbox>(mailbox.Oid, token));

                letter.LetterCatalog = await MailClients.GetLetterCatalog(uof, letter.LetterSenderAddress, token);

                letter.Customer?.Reload();
                var staff = letter.Customer?.AccountantResponsible;

                if (letter.Customer != null && letter.Customer.CustomerIsPrimaryResponsible is false)
                {
                    staff = letter.Customer?.PrimaryResponsible;
                }
                else if (letter.LetterCatalog != null && letter.LetterCatalog.Staff != null)
                {
                    staff = letter.LetterCatalog.Staff;
                }

                if (!EmailsSpam.Contains(letter.LetterSenderAddress))
                {
                    letter.Deal = new RMS.Core.Model.Deal(uof)
                    {
                        Letter = letter,
                        Customer = letter.Customer,
                        Staff = staff,
                        DealStatus = await uof.FindObjectAsync<RMS.Core.Model.DealStatus>(new BinaryOperator(nameof(RMS.Core.Model.DealStatus.IsDefault), true), token)
                    };
                }

                if (letter.UniqueId is uint result)
                {
                    var listUniqueId = new List<UniqueId>();
                    var currentUniqueId = new UniqueId(result);
                    listUniqueId.Add(currentUniqueId);

                    var messageSummarys = await client.Inbox.FetchAsync(listUniqueId, MessageSummaryItems.UniqueId | MessageSummaryItems.BodyStructure | MessageSummaryItems.Body);
                    var obj = messageSummarys.FirstOrDefault(f => f.UniqueId.Id == letter.UniqueId);

                    if (obj != null)
                    {
                        await MailClients.GetAttachmentsFromCloudAsync(obj, uof, letter, client);

                        //foreach (var attachment in obj.Attachments)
                        //{
                        //    await MailClients.GetAttachmentsAsync(currentUniqueId, uof, letter, attachment, client);
                        //    WinConsole.ConsoleWriteLineWithColor($"[{_dateTime.ToShortDateString()} ({_dateTime:HH:mm:ss})] Получение вложений для письма: {letter}", ConsoleColor.Cyan);
                        //}
                    }
                }
                
                return letter;
            }

            async Task WaitForNewMessagesAsync()
            {
                do
                {
                    try
                    {
                        if (client.Capabilities.HasFlag(ImapCapabilities.Idle))
                        {
                            done = new CancellationTokenSource(new TimeSpan(0, 5, 0));
                            try
                            {
                                await client.IdleAsync(done.Token, cancel.Token);
                            }
                            finally
                            {
                                done.Dispose();
                                done = null;
                            }
                        }
                        else
                        {
                            await Task.Delay(new TimeSpan(0, 0, 30), cancel.Token);
                            await client.NoOpAsync(cancel.Token);
                        }
                        break;
                    }
                    catch (ImapProtocolException ex)
                    {
                        await LoggerController.WriteLogBaseAsync(ex.ToString());
                        WinConsole.ConsoleWriteLineWithColor(ex.Message, ConsoleColor.Red);
                        await ReconnectAsync();
                    }
                    catch (IOException ex)
                    {
                        await LoggerController.WriteLogBaseAsync(ex.ToString());
                        WinConsole.ConsoleWriteLineWithColor(ex.Message, ConsoleColor.Red);
                        await ReconnectAsync();
                    }
                } 
                while (true);
            }

            async Task IdleAsync()
            {
                do
                {
                    try
                    {
                        await WaitForNewMessagesAsync();

                        if (messagesArrived)
                        {
                            await FetchMessageSummariesAsync(true);
                            messagesArrived = false;
                        }
                    }
                    catch (OperationCanceledException ex)
                    {
                        await LoggerController.WriteLogBaseAsync(ex.ToString());
                        WinConsole.ConsoleWriteLineWithColor(ex.Message, ConsoleColor.Red);
                        break;
                    }
                } 
                while (!cancel.IsCancellationRequested);
            }

            public async Task RunAsync()
            {
                await GetEmailFilteringDate();

                try
                {
                    await ReconnectAsync();
                    await FetchMessageSummariesAsync(false);
                }
                catch (OperationCanceledException ex)
                {
                    await LoggerController.WriteLogBaseAsync(ex.ToString());
                    WinConsole.ConsoleWriteLineWithColor(ex.Message, ConsoleColor.Red);
                    await client.DisconnectAsync(true);
                    return;
                }

                var inbox = client.Inbox;
                inbox.CountChanged += OnCountChanged;
                inbox.MessageExpunged += OnMessageExpunged;
                inbox.MessageFlagsChanged += OnMessageFlagsChanged;

                await IdleAsync();

                inbox.MessageFlagsChanged -= OnMessageFlagsChanged;
                inbox.MessageExpunged -= OnMessageExpunged;
                inbox.CountChanged -= OnCountChanged;

                await client.DisconnectAsync(true);
            }

            private static async Task GetEmailFilteringDate()
            {
                try
                {
                    using (var uof = new UnitOfWork(DatabaseConnection.GetWorkSession().DataLayer))
                    {
                        var settings = await uof.FindObjectAsync<Settings>(null);
                        if (settings != null)
                        {
                            _emailFilteringDate = settings.EmailFilteringDate;
                        }
                    }
                }
                catch (Exception ex)
                {
                    await LoggerController.WriteLogBaseAsync(ex.ToString());
                    WinConsole.ConsoleWriteLineWithColor(ex.Message, ConsoleColor.Red);
                }
            }

            private void OnCountChanged(object sender, EventArgs e)
            {
                if (sender is ImapFolder folder && folder.Count > messages.Count)
                {
                    var arrived = folder.Count - messages.Count;
                    WinConsole.ConsoleWriteLineWithColor($"[{_dateTime.ToShortDateString()} ({_dateTime:HH:mm:ss})] {arrived} new messages have arrived.", ConsoleColor.Green);
                    messagesArrived = true;
                    done?.Cancel();
                }
            }

            void OnMessageExpunged(object sender, MessageEventArgs e)
            {
                if (sender is ImapFolder folder)
                {
                    if (e.Index < messages.Count)
                    {
                        var message = messages[e.Index];
                        WinConsole.ConsoleWriteLineWithColor($"[{_dateTime.ToShortDateString()} ({_dateTime:HH:mm:ss})] {folder}: message #{e.Index} has been expunged: {message.Envelope.Subject}", ConsoleColor.Green);
                        messages?.RemoveAt(e.Index);
                    }
                    else
                    {
                        WinConsole.ConsoleWriteLineWithColor($"[{_dateTime.ToShortDateString()} ({_dateTime:HH:mm:ss})] {folder}: message #{e.Index} has been expunged.", ConsoleColor.Green);
                    }
                }                 
            }

            void OnMessageFlagsChanged(object sender, MessageFlagsChangedEventArgs e)
            {
                if (sender is ImapFolder folder)
                {
                    WinConsole.ConsoleWriteLineWithColor($"[{_dateTime.ToShortDateString()} ({_dateTime:HH:mm:ss})] {folder}: flags have changed for message #{e.Index} ({e.Flags}).", ConsoleColor.Green);
                }
            }

            public void Exit()
            {
                cancel?.Cancel();
            }

            public void Dispose()
            {
                client?.Dispose();
                cancel?.Dispose();
            }            
        }
    }
}
