using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;
using MimeKit;
using RMS.Core.Controllers;
using RMS.Core.Enumerator;
using RMS.Core.Model.InfoCustomer;
using RMS.Parser.Core.Models.YandexDisk.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RMS.Core.Model.Mail
{
    public class MailClients
    {
        public delegate void UpdateEventHandler(bool isUpdate);
        public event UpdateEventHandler Update;

        public delegate void GetLetterEventHandler(object sender, string folderName, int count, int number);
        public event GetLetterEventHandler GetLetter;

        public delegate void SaveLetterEventHandler(object sender, Letter letter);
        public event SaveLetterEventHandler SaveLetter;

        public static bool IsFinishGetLetter
        {
            get
            {
                var useClient = ListMailClients?.FirstOrDefault(f => f.IsReceivingLetters is true);
                if (useClient is null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static List<MailClients> ListMailClients { get; set; } = new List<MailClients>();

        /// <summary>
        /// Получение всех писем с почтового ящика.
        /// </summary>
        public bool IsReceivingLetters { get; set; }

        public static void FillingListMailClients(XPCollection<Mailbox> collectionMailbox)
        {
            ListMailClients.RemoveAll(r => !r.ImapClient.IsConnected);

            foreach (var mailbox in collectionMailbox)
            {
                if (ListMailClients.FirstOrDefault(f => f.Mailbox.Oid == mailbox.Oid) is null)
                {
                    var mailClients = new MailClients(mailbox);
                    if (mailClients != null && mailClients.ImapClient != null && mailClients.ImapClient.IsConnected && mailClients.ImapClient.IsAuthenticated)
                    {
                        //var idleTask = mailClients.IdleMailClient.RunAsync();
                        //mailClients.IdleMailClient.Exit();
                        //idleTask.GetAwaiter().GetResult();

                        ListMailClients.Add(mailClients);
                    }
                }
            }
        }

        public static void FillingListMailClients(IEnumerable<Mailbox> objCollection)
        {
            ListMailClients.RemoveAll(r => !r.ImapClient.IsConnected);

            foreach (var mailbox in objCollection)
            {
                if (ListMailClients.FirstOrDefault(f => f.Mailbox.Oid == mailbox.Oid) is null)
                {
                    var mailClients = new MailClients(mailbox);
                    if (mailClients != null && mailClients.ImapClient != null && mailClients.ImapClient.IsConnected && mailClients.ImapClient.IsAuthenticated)
                    {
                        //var idleTask = mailClients.IdleMailClient.RunAsync();
                        //mailClients.IdleMailClient.Exit();
                        //idleTask.GetAwaiter().GetResult();

                        ListMailClients.Add(mailClients);
                    }
                }
            }
        }

        public MailClients(Mailbox mailbox)
        {
            Mailbox = mailbox ?? throw new ArgumentNullException(nameof(mailbox));
            ImapClient = GetImapClientAsync(Mailbox);
            Folders = GetFolders(ImapClient);
            //IdleMailClient = new IdleMailClient(mailbox.MailboxSetup.IncomingMailServerIMAP,
            //                                    mailbox.MailboxSetup.PortIMAP, 
            //                                    SecureSocketOptions.Auto,
            //                                    mailbox.MailingAddress, 
            //                                    mailbox.Password);
        }

        public Mailbox Mailbox { get; }
        public ImapClient ImapClient { get; private set; }
        //public IdleMailClient IdleMailClient { get; set; }
        public IList<IMailFolder> Folders { get; }

        public static bool Authenticate(ImapClient imapClient, Mailbox mailbox)
        {
            imapClient = GetImapClientAsync(mailbox);
            return imapClient?.IsAuthenticated ?? default;
        }

        /// <summary>
        /// Соединение с почтовым сервером.
        /// </summary>
        /// <param name="mailbox"></param>
        /// <returns></returns>
        private static ImapClient GetImapClientAsync(Mailbox mailbox)
        {
            try
            {
                var imapClient = new ImapClient();
                imapClient.Connect(mailbox.MailboxSetup.IncomingMailServerIMAP, mailbox.MailboxSetup.PortIMAP, true);

                if (mailbox.AccessToken is null)
                {
                    imapClient.Authenticate(mailbox.MailingAddress, Mailbox.Decrypt(mailbox.Password));
                }
                else
                {
                    var oauth2 = new SaslMechanismOAuth2(mailbox.MailingAddress, mailbox.AccessToken);
                    imapClient.Authenticate(oauth2);
                }

                return imapClient;
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                return null;
            }
        }

        /// <summary>
        /// Получение списка папок у почтового клиента.
        /// </summary>
        /// <param name="imapClient"></param>
        /// <returns></returns>
        private IList<IMailFolder> GetFolders(ImapClient imapClient)
        {
            if (imapClient != null)
            {
                return imapClient.GetFolders(imapClient.PersonalNamespaces.First());
            }

            return null;
        }

        public async Task<bool> GetAttachments(Letter letter)
        {
            if (letter is null)
            {
                return false;
            }

            if (Mailbox != null && ImapClient != null)
            {
                if (!ImapClient.IsAuthenticated && !ImapClient.IsConnected)
                {
                    if (Authenticate(ImapClient, Mailbox) is false)
                    {
                        return false;
                    }
                }

                var listUniqueId = new List<UniqueId>();
                var uniqueId = default(UniqueId);

                if (letter.UniqueId is uint result)
                {
                    uniqueId = new UniqueId(result);
                    listUniqueId.Add(uniqueId);
                }

                await ImapClient.Inbox.OpenAsync(FolderAccess.ReadOnly);
                var messageSummarys = await ImapClient.Inbox.FetchAsync(listUniqueId, MessageSummaryItems.UniqueId | MessageSummaryItems.BodyStructure | MessageSummaryItems.Body);
                var obj = messageSummarys.FirstOrDefault(f => f.UniqueId.Id == letter.UniqueId);

                if (obj != null)
                {
                    using (var uof = new UnitOfWork())
                    {
                        var currentLetter = await new XPQuery<Letter>(uof).FirstOrDefaultAsync(f => f.Oid == letter.Oid);
                        if (currentLetter != null)
                        {
                            await GetAttachmentsFromCloudAsync(obj, uof, currentLetter, ImapClient);

                            foreach (var attachment in obj.Attachments)
                            {
                                await GetAttachmentsAsync(uniqueId, uof, currentLetter, attachment, ImapClient);
                            }
                        }
                    }
                    return true;
                }
            }

            return default;
        }

        public static async System.Threading.Tasks.Task GetAttachmentsAsync(UniqueId uniqueId, UnitOfWork uof, Letter currentLetter, BodyPartBasic attachment, ImapClient client)
        {
            var entity = client.Inbox.GetBodyPart(uniqueId, attachment);

            if (entity is MimePart mimePart)
            {
                var fileName = mimePart.FileName;
                using (var memory = new MemoryStream())
                {
                    await mimePart.Content.DecodeToAsync(memory);
                    var bytes = memory.ToArray();

                    if (currentLetter?.LetterAttachments?.FirstOrDefault(f => f.FullFileName == fileName) is null)
                    {
                        var letterAttachment = new LetterAttachment(uof)
                        {
                            FileByte = bytes,
                            FullFileName = fileName,
                            Letter = currentLetter
                        };
                        letterAttachment.Save();

                        await uof.CommitTransactionAsync().ConfigureAwait(false);
                    }
                }
            }
        }

        public static async System.Threading.Tasks.Task GetAttachmentsFromCloudAsync(IMessageSummary obj, UnitOfWork uof, Letter currentLetter, ImapClient client)
        {
            foreach (var objBodyPart in obj.BodyParts)
            {
                if (objBodyPart.ContentType?.Name?.Equals("narod_attachment_links.html") is true)
                {
                    if (client?.Inbox?.GetBodyPart(obj.UniqueId, objBodyPart) is TextPart body)
                    {
                        var text = body.Text;

                        var links = YandexDiskController.GetLinks(text);
                        var spentLinks = currentLetter?.LetterAttachments?.Select(s => s.Link);

                        var needLinks = links?.Except(spentLinks);

                        if (needLinks != null && needLinks.Count() > 0)
                        {
                            var i = 0;
                            foreach (var link in links)
                            {
                                try
                                {
                                    i++;
                                    var @base = await YandexDiskController.GetYandexFileAsync(link);
                                    if (@base != null)
                                    {
                                        if (currentLetter?.LetterAttachments?.FirstOrDefault(f => f.FullFileName == @base.FileName) is null)
                                        {
                                            var letterAttachment = new LetterAttachment(uof)
                                            {
                                                FileByte = @base.Obj,
                                                FullFileName = @base.FileName,
                                                Letter = currentLetter,
                                                Link = link
                                            };
                                            letterAttachment.Save();

                                            await uof.CommitTransactionAsync().ConfigureAwait(false);
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                                }
                            }
                        }
                    }
                }
                else if (client?.Inbox?.GetBodyPart(obj.UniqueId, objBodyPart) is MimePart mimePart)
                {
                    try
                    {
                        var fileName = mimePart.FileName;

                        if (!string.IsNullOrWhiteSpace(fileName))
                        {
                            using (var memory = new MemoryStream())
                            {
                                await mimePart.Content.DecodeToAsync(memory);
                                var bytes = memory.ToArray();

                                if (currentLetter?.LetterAttachments?.FirstOrDefault(f => f.FullFileName == fileName) is null)
                                {
                                    var letterAttachment = new LetterAttachment(uof)
                                    {
                                        FileByte = bytes,
                                        FullFileName = fileName,
                                        Letter = currentLetter
                                    };
                                    letterAttachment.Save();

                                    await uof.CommitTransactionAsync().ConfigureAwait(false);
                                }
                            }
                        }                        
                    }
                    catch (Exception ex)
                    {
                        
                    }
                }
            }
        }

        /// <summary>
        /// Получение всех писем за дату.
        /// </summary>
        public async Task<bool> GetAllEmailsByDate(DateTime? dateTime = default, int countToSave = 0, DateTime? emailFilteringDate = null, bool isDeliveredOn = false, bool isUniqueIdFirstCheck = true)
        {
            try
            {
                using (var uof = new UnitOfWork())
                {
                    try
                    {
                        EmailsSpam.AddRange(await GetSpamEmail(uof));
                    }
                    catch (Exception ex)
                    {
                        RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                    }

                    if (Mailbox != null && ImapClient != null && ImapClient.IsAuthenticated && ImapClient.IsConnected)
                    {
                        IsReceivingLetters = true;

                        if (Folders.Count > 0)
                        {
                            var messageInfoCollection = await GetMessagesInfoAsync(uof, Mailbox);

                            foreach (var folder in Folders.Where(w => w.FullName.Contains("INBOX") || w.FullName.Contains("Входящие")))
                            {
                                if (!folder.IsOpen)
                                {
                                    await folder.OpenAsync(FolderAccess.ReadWrite);
                                }

                                var folderName = folder.FullName;

                                if (folderName.Equals("INBOX", StringComparison.OrdinalIgnoreCase))
                                {
                                    folderName = "Входящие";
                                }

                                var listLetter = new List<Letter>();

                                if (dateTime is DateTime date)
                                {
                                    var searchQuery = default(DateSearchQuery);

                                    if (isDeliveredOn)
                                    {
                                        searchQuery = SearchQuery.DeliveredOn(date);
                                    }
                                    else
                                    {
                                        searchQuery = SearchQuery.DeliveredAfter(date);
                                    }
                                    var listUniqueId = await folder.SearchAsync(searchQuery);
                                    GetLetter?.Invoke(this, folderName, listUniqueId.Count(), 0);

                                    var count = default(int);
                                    foreach (var uniqueId in listUniqueId.OrderByDescending(x => x.Id))
                                    {
                                        try
                                        {
                                            count++;
                                            GetLetter?.Invoke(this, folderName, listUniqueId.Count(), count);

                                            if (isUniqueIdFirstCheck && messageInfoCollection.FirstOrDefault(f => f.UniqueId == uniqueId.Id) != null)
                                            {
                                                continue;
                                            }

                                            var message = await folder.GetMessageAsync(uniqueId);

                                            if (messageInfoCollection.Contains(new MessageInfo(message.MessageId, uniqueId.Id, message.Date.DateTime)))
                                            {
                                                continue;
                                            }

                                            try
                                            {
                                                if (emailFilteringDate != null && emailFilteringDate > message.Date.DateTime)
                                                {
                                                    LoggerController.WriteLog($"Письмо от {message.From?.ToString()?.Trim()?.Replace('<', '[')?.Replace('>', ']')} пропущено. " +
                                                        $"[{Convert.ToDateTime(emailFilteringDate).ToShortDateString()} > {message.Date.DateTime.ToShortDateString()}]");
                                                    continue;
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                                            }

                                            var letter = await CreateLetter(message, folder, uof, uniqueId.Id);

                                            if (countToSave > 0)
                                            {
                                                listLetter.Add(letter);
                                                if (listLetter.Count >= countToSave)
                                                {
                                                    listLetter = await SaveListLetter(listLetter, uof).ConfigureAwait(false);
                                                }
                                            }
                                            else
                                            {
                                                await SaveListLetter(uof, letter).ConfigureAwait(false);
                                            }

                                            await folder.AddFlagsAsync(uniqueId, MessageFlags.Seen, true);
                                        }
                                        catch (Exception ex)
                                        {
                                            RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                                        }
                                    }
                                }
                                else
                                {
                                    var summaries = await folder.FetchAsync(0, -1, MessageSummaryItems.Envelope | MessageSummaryItems.UniqueId);

                                    GetLetter?.Invoke(this, folderName, summaries.Count(), 0);

                                    var count = default(int);
                                    foreach (var messageSummary in summaries)
                                    {
                                        count++;
                                        GetLetter?.Invoke(this, folderName, summaries.Count(), count);
                                        
                                        if (messageInfoCollection.Contains(new MessageInfo(messageSummary.Envelope.MessageId, messageSummary.UniqueId.Id, messageSummary.Date.DateTime)))
                                        {
                                            continue;
                                        }

                                        var message = await folder.GetMessageAsync(messageSummary.UniqueId);
                                        var letter = await CreateLetter(message, folder, uof, messageSummary.UniqueId.Id);

                                        if (countToSave > 0)
                                        {
                                            listLetter.Add(letter);
                                            if (listLetter.Count >= countToSave)
                                            {
                                                listLetter = await SaveListLetter(listLetter, uof);
                                            }
                                        }
                                        else
                                        {
                                            await SaveListLetter(uof, letter);
                                        }
                                    }
                                }

                                if (listLetter.Count > 0)
                                {
                                    listLetter = await SaveListLetter(listLetter, uof, true);
                                }
                            }
                        }

                        IsReceivingLetters = false;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                IsReceivingLetters = false;
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }

            IsReceivingLetters = false;
            return false;
        }

        public class MessageInfo : IEquatable<MessageInfo>
        {
            public MessageInfo(string messageId, uint? uniqueId, DateTime date)
            {
                MessageId = messageId;
                UniqueId = uniqueId ?? 0;
                Date = date;
            }

            public string MessageId { get; private set; }
            public uint UniqueId { get; private set; }
            public DateTime Date { get; private set; }

            public override bool Equals(object obj)
            {
                return Equals(obj as MessageInfo);
            }

            public bool Equals(MessageInfo other)
            {
                return other != null &&
                       MessageId == other.MessageId &&
                       UniqueId == other.UniqueId &&
                       Date == other.Date;
            }

            public override int GetHashCode()
            {
                int hashCode = -1747185435;
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(MessageId);
                hashCode = hashCode * -1521134295 + UniqueId.GetHashCode();
                hashCode = hashCode * -1521134295 + Date.GetHashCode();
                return hashCode;
            }

            public static bool operator ==(MessageInfo left, MessageInfo right)
            {
                return EqualityComparer<MessageInfo>.Default.Equals(left, right);
            }

            public static bool operator !=(MessageInfo left, MessageInfo right)
            {
                return !(left == right);
            }

            public override string ToString()
            {
                return $"[{Date}] [{UniqueId}] {MessageId}";
            }
        }

        /// <summary>
        /// Получение всех писем по указанному email..
        /// </summary>
        public async Task<bool> GetAllEmailsByEmail(string email = default, int countToSave = 0, DateTime? emailFilteringDate = null, bool isUniqueIdFirstCheck = true)
        {
            try
            {
                using (var uof = new UnitOfWork())
                {
                    try
                    {
                        EmailsSpam.AddRange(await GetSpamEmail(uof));
                    }
                    catch (Exception ex)
                    {
                        RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                    }                    
                    
                    if (Mailbox != null && ImapClient != null && ImapClient.IsAuthenticated && ImapClient.IsConnected)
                    {
                        IsReceivingLetters = true;

                        if (Folders.Count > 0)
                        {
                            var messageInfoCollection = await GetMessagesInfoAsync(uof, Mailbox);

                            foreach (var folder in Folders.Where(w => w.FullName.Contains("INBOX") || w.FullName.Contains("Входящие")))
                            {
                                if (!folder.IsOpen)
                                {
                                    await folder.OpenAsync(FolderAccess.ReadWrite);
                                }

                                var folderName = folder.FullName;

                                if (folderName.Equals("INBOX", StringComparison.OrdinalIgnoreCase))
                                {
                                    folderName = "Входящие";
                                }

                                var listLetter = new List<Letter>();

                                if (!string.IsNullOrWhiteSpace(email))
                                {
                                    var searchQuery = SearchQuery.FromContains(email);
                                    var listUniqueId = await folder.SearchAsync(searchQuery);

                                    GetLetter?.Invoke(this, folderName, listUniqueId.Count(), 0);

                                    var count = default(int);
                                    foreach (var uniqueId in listUniqueId.OrderByDescending(x => x.Id))
                                    {
                                        try
                                        {
                                            count++;
                                            GetLetter?.Invoke(this, folderName, listUniqueId.Count(), count);

                                            if (isUniqueIdFirstCheck && messageInfoCollection.FirstOrDefault(f => f.UniqueId == uniqueId.Id) != null)
                                            {
                                                continue;
                                            }

                                            var message = await folder.GetMessageAsync(uniqueId);

                                            if (messageInfoCollection.Contains(new MessageInfo(message.MessageId, uniqueId.Id, message.Date.DateTime)))
                                            {
                                                continue;
                                            }

                                            try
                                            {
                                                if (emailFilteringDate != null && emailFilteringDate > message.Date.DateTime)
                                                {
                                                    LoggerController.WriteLog($"Письмо от {message.From?.ToString()?.Trim()?.Replace('<', '[')?.Replace('>', ']')} пропущено. " +
                                                        $"[{Convert.ToDateTime(emailFilteringDate).ToShortDateString()} > {message.Date.DateTime.ToShortDateString()}]");
                                                    continue;
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                                            }

                                            var letter = await CreateLetter(message, folder, uof, uniqueId.Id);

                                            if (countToSave > 0)
                                            {
                                                listLetter.Add(letter);
                                                if (listLetter.Count >= countToSave)
                                                {
                                                    listLetter = await SaveListLetter(listLetter, uof).ConfigureAwait(false);
                                                }
                                            }
                                            else
                                            {
                                                await SaveListLetter(uof, letter).ConfigureAwait(false);
                                            }

                                            await folder.AddFlagsAsync(uniqueId, MessageFlags.Seen, true);
                                        }
                                        catch (Exception ex)
                                        {
                                            RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                                        }
                                    }
                                }

                                if (listLetter.Count > 0)
                                {
                                    listLetter = await SaveListLetter(listLetter, uof, true);
                                }
                            }
                        }

                        IsReceivingLetters = false;
                        return true;
                    }
                }                
            }
            catch (Exception ex)
            {
                IsReceivingLetters = false;
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }            
            
            IsReceivingLetters = false;
            return false;
        }

        public static async Task<MessageInfo[]> GetMessagesInfoAsync(UnitOfWork uof, Mailbox mailbox)
        {
            return await new XPQuery<Letter>(uof)
                .Where(w => w.Mailbox != null && w.Mailbox.Oid == mailbox.Oid)
                .OrderByDescending(o => o.DateCreate)
                .Select(s => new MessageInfo(s.MessageId, s.UniqueId, s.DateCreate))
                .ToArrayAsync();
        }

        public static async Task<IEnumerable<string>> GetSpamEmail(UnitOfWork uof)
        {
            var emailsSpam = await new XPQuery<Letter>(uof)
                .Where(w => w.TypeLetter == TypeLetter.Spam)
                .Select(s => s.LetterSenderAddress)
                .Distinct()
                .ToListAsync();

            return emailsSpam.OrderBy(o => o);
        }

        private List<string> EmailsSpam = new List<string>();
        
        /// <summary>
        /// Создание письма.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="folder"></param>
        /// <returns></returns>
        public async Task<Letter> CreateLetter(MimeMessage message, IMailFolder folder, UnitOfWork uof, uint? uniqueId = default, CancellationToken token = default)
        {
            var isSpam = await IsSpamСheckAsync(uof, message.From.ToString(), token);

            var typeLetter = default(TypeLetter);
            var customer = default(Customer);
            
            if (isSpam)
            {
                typeLetter = TypeLetter.Spam;
                customer = null;
            }
            else
            {
                typeLetter = GetTypeLetter(folder);
                customer = await GetCustomerAsync(uof, message.From.Mailboxes.FirstOrDefault(f => message.From.ToString().Contains(f.Address))?.Address?.Trim(), token);
            }

            //var letter = await uof.FindObjectAsync<Letter>(new BinaryOperator(nameof(Letter.MessageId), message.MessageId), token);
            var letter = await new XPQuery<Letter>(uof).FirstOrDefaultAsync(f => f.MessageId == message.MessageId && f.DateCreate == message.Date.DateTime);

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
                LetterSender = message.From.ToString().Trim(),
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
            letter.SetMailBox(await uof.GetObjectByKeyAsync<Mailbox>(Mailbox.Oid, token));

            letter.LetterCatalog = await GetLetterCatalog(uof, letter.LetterSenderAddress, token);

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
                letter.Deal = new Deal(uof)
                {
                    Letter = letter,
                    Customer = letter.Customer,
                    Staff = staff,
                    DealStatus = await uof.FindObjectAsync<DealStatus>(new BinaryOperator(nameof(DealStatus.IsDefault), true), token)
                };
            }
            
            if (letter.UniqueId is uint result)
            {
                var listUniqueId = new List<UniqueId>();
                var currentUniqueId = new UniqueId(result);
                listUniqueId.Add(currentUniqueId);

                var messageSummarys = await ImapClient.Inbox.FetchAsync(listUniqueId, MessageSummaryItems.UniqueId | MessageSummaryItems.BodyStructure | MessageSummaryItems.Body);
                var obj = messageSummarys.FirstOrDefault(f => f.UniqueId.Id == letter.UniqueId);

                if (obj != null)
                {
                    await GetAttachmentsFromCloudAsync(obj, uof, letter, ImapClient);
                    
                    //foreach (var attachment in obj.Attachments)
                    //{
                    //    await GetAttachmentsAsync(currentUniqueId, uof, letter, attachment, ImapClient);
                    //}
                }
            }
            
            return letter;
        }

        /// <summary>
        /// Получение необходимой папки из фильтра.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="LetterSenderAddress"></param>
        /// <returns></returns>
        public static async Task<LetterCatalog> GetLetterCatalog(UnitOfWork unitOfWork, string LetterSenderAddress, CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(LetterSenderAddress))
            {
                return default;
            }
            
            var letterFilter = await unitOfWork.FindObjectAsync<LetterFilter>(new BinaryOperator(nameof(LetterFilter.Email), LetterSenderAddress), token);
            if (letterFilter != null)
            {
                return letterFilter.LetterCatalog;
            }
            else
            {
                return default;
            }
        }

        /// <summary>
        /// Сохранение писем в коллекцию.
        /// </summary>
        /// <param name="xpCollectionLetter"></param>
        /// <param name="listLetter"></param>
        private async System.Threading.Tasks.Task SaveListLetter(UnitOfWork unitOfWork, Letter letter, bool isUpdate = false)
        {
            if (letter != null && letter.Oid > 0)
            {
                return;
            }
            
            try
            {
                letter.Save();
                SaveLetter?.Invoke(this, letter);
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
            
            await unitOfWork.CommitChangesAsync().ConfigureAwait(false);
            
            if (isUpdate)
            {
                Update?.Invoke(true);
            }
        }

        /// <summary>
        /// Сохранение писем в коллекцию.
        /// </summary>
        /// <param name="xpCollectionLetter"></param>
        /// <param name="listLetter"></param>
        private async Task<List<Letter>> SaveListLetter(List<Letter> listLetter, UnitOfWork unitOfWork, bool isUpdate = false)
        {
            //xpCollectionLetter.AddRange(listLetter);

            foreach (var letter in listLetter)
            {
                try
                {
                    if (letter != null && letter.Oid > 0)
                    {
                        continue;
                    }

                    letter.Save();
                    SaveLetter?.Invoke(this, letter);
                }
                catch (Exception ex)
                {
                    RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                }
            }
            
            await unitOfWork.CommitChangesAsync().ConfigureAwait(false);
            //Mailbox.Session.Save(xpCollectionLetter);
            if (isUpdate)
            {
                Update?.Invoke(true);                
            }
            return new List<Letter>();
        }

        /// <summary>
        /// Получение клиента по Email.
        /// </summary>
        /// <param name="unitOfWork">Активная сессия.</param>
        /// <param name="messageFrom">Адрес отправителя.</param>
        /// <returns></returns>
        public static async Task<CustomerEmail> GetCustomerEmailAsync(UnitOfWork unitOfWork, string messageFrom)
        {
            var groupOperator = new GroupOperator(GroupOperatorType.Or);

            var criteriaCustomerEmail = new BinaryOperator(nameof(CustomerEmail.Email), messageFrom.Trim());
            groupOperator.Operands.Add(criteriaCustomerEmail);

            var criteriaCustomerEmail2 = new BinaryOperator(nameof(CustomerEmail.Email2), messageFrom.Trim());
            groupOperator.Operands.Add(criteriaCustomerEmail2);

            var customerEmail = await unitOfWork.FindObjectAsync<CustomerEmail>(groupOperator);

            if (customerEmail is null)
            {
                return null;
            }
            else
            {
                return customerEmail;
            }
        }

        /// <summary>
        /// Получение клиента по Email.
        /// </summary>
        /// <param name="unitOfWork">Активная сессия.</param>
        /// <param name="messageFrom">Адрес отправителя.</param>
        /// <returns></returns>
        public static async Task<Customer> GetCustomerAsync(UnitOfWork unitOfWork, string messageFrom, CancellationToken token = default)
        {
            var groupOperator = new GroupOperator(GroupOperatorType.Or);
            
            var criteriaCustomerEmail = new BinaryOperator(nameof(CustomerEmail.Email), messageFrom.Trim());
            groupOperator.Operands.Add(criteriaCustomerEmail);

            var criteriaCustomerEmail2 = new BinaryOperator(nameof(CustomerEmail.Email2), messageFrom.Trim());
            groupOperator.Operands.Add(criteriaCustomerEmail2);

            var customerEmail = await unitOfWork.FindObjectAsync<CustomerEmail>(groupOperator, token);

            if (customerEmail is null)
            {
                return null;
            }
            else
            {
                return customerEmail.Customer ?? null;
            }            
        }

        /// <summary>
        /// Проверяем есть ли письма от этого отправления в спаме.
        /// </summary>
        /// <param name="unitOfWork">Активная сессия.</param>
        /// <param name="messageFrom">Отправитель.</param>
        /// <returns></returns>
        public static async Task<bool> IsSpamСheckAsync(UnitOfWork unitOfWork, string messageFrom, CancellationToken token = default)
        {
            var groupOperator = new GroupOperator(GroupOperatorType.And);

            var criteriaEmail = new BinaryOperator(nameof(Letter.LetterSenderAddress), messageFrom);
            groupOperator.Operands.Add(criteriaEmail);

            var criteriaTypeLetter = new BinaryOperator(nameof(Letter.TypeLetter), TypeLetter.Spam);
            groupOperator.Operands.Add(criteriaTypeLetter);
            
            var letter = await unitOfWork.FindObjectAsync<Letter>(groupOperator, token);

            if (letter is null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static TypeLetter GetTypeLetter(IMailFolder mailFolder)
        {
            switch (mailFolder.Attributes)
            {
                case FolderAttributes.Remote:
                    return TypeLetter.Basket;
                    
                case FolderAttributes.Drafts:
                    return TypeLetter.Draft;

                case FolderAttributes.Junk:
                    return TypeLetter.Basket;
                    
                case FolderAttributes.Marked | FolderAttributes.HasNoChildren | FolderAttributes.Junk:
                    return TypeLetter.Spam;
                    
                case FolderAttributes.Sent | FolderAttributes.Marked | FolderAttributes.HasNoChildren:
                    return TypeLetter.Outgoing;

                case FolderAttributes.Marked | FolderAttributes.HasNoChildren | FolderAttributes.Trash:
                    return TypeLetter.Basket;

                default:
                    return TypeLetter.InBox;
            }
        }
    }
}
