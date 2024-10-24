using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Pop3;
using MailKit.Search;
using MailKit.Security;
using MimeKit;
using RMS.Core.Enumerator;
using RMS.Core.Model.InfoCustomer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MailKit.Net.Smtp;
using System.Text.RegularExpressions;
using RMS.Core.Controllers;

namespace RMS.Core.Model.Mail
{
    /// <summary>
    /// Письма.
    /// </summary>
    public class Letter : XPObject
    {

        public Letter() { }
        public Letter(Session session) : base(session) { }
        
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            
            TypeLetter = TypeLetter.Outgoing;
        }

        protected async override void OnLoaded()
        {
            base.OnLoaded();
            
            await GetLetterLetterAttachmentsCount();
        }

        protected async override void OnSaved()
        {
            base.OnSaved();

            await GetLetterLetterAttachmentsCount();

            if (LetterCatalog != null)
            {
                await LetterCatalog?.GetQuantity();
            }            
        }

        public async System.Threading.Tasks.Task GetLetterLetterAttachmentsCount()
        {
            var count = await new XPQuery<LetterAttachment>(Session)
                ?.CountAsync(c => c.Letter != null && c.Letter.Oid == Oid);

            if (count > 0)
            {
                IsLetterAttachments = true;
            }
        }

        public void SetMailBox(Mailbox mailbox)
        {
            Mailbox = mailbox;
        }

        /// <summary>
        /// Уникальный идентификатор письма.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public uint? UniqueId { get; set; }

        /// <summary>
        /// Имеются ли вложения.
        /// </summary>
        [DisplayName("Вложения")]
        public bool IsLetterAttachments { get; private set; }
        //{
        //    get
        //    {
        //        if (LetterAttachments?.Count > 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}

        [DisplayName("Сделка")]
        public string DealStatusString => DealStatus?.ToString();

        /// <summary>
        /// Письмо, на которое был создан ответ.
        /// </summary>
        [Delayed(true)]
        [MemberDesignTimeVisibility(false)]
        public Letter AnswerLetter 
        {
            get { return GetDelayedPropertyValue<Letter>(nameof(AnswerLetter)); }
            set { SetDelayedPropertyValue(nameof(AnswerLetter), value); }
        }

        [DisplayName("Отправлен ответ")]
        public bool IsReplySent { get; set; }

        [MemberDesignTimeVisibility(false)]
        public DealStatus DealStatus => Deal?.DealStatus;
        /// <summary>
        /// Сделка.
        /// </summary>
        [Aggregated]
        [MemberDesignTimeVisibility(false)]
        public Deal Deal { get; set; }

        /// <summary>
        /// Уникальный почтовый идентификатор.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        [Size(1024)]
        public string MessageId { get; set; }

        /// <summary>
        /// Тип письма.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public TypeLetter TypeLetter { get; set; }
                
        /// <summary>
        /// Прочитано или нет.
        /// </summary>
        [DisplayName("Прочитано")]
        public bool IsRead { get; set; }

        /// <summary>
        /// Отправитель письма.
        /// </summary>
        [DisplayName("Отправитель письма")]
        [Size(1024)]
        public string LetterSender { get; set; }

        /// <summary>
        /// Отправитель письма (Email).
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        [Size(1024)]
        [DisplayName("Отправитель письма")]
        public string LetterSenderAddress { get; set; }
        
        [DisplayName("Отправил")]
        public string UserString => User?.ToString();
        /// <summary>
        /// Пользователь, отправивший письмо из программы.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public User User { get; set; }

        private string _letterRecipient;
        /// <summary>
        /// Получатель письма.
        /// </summary>
        [Size(4096)]
        [DisplayName("Получатель письма")]
        public string LetterRecipient
        {
            get => _letterRecipient;
            set => _letterRecipient = value?.Length > 4000 ? value.Substring(0, 4000) : value;
        }

        private string _letterRecipientAddress;
        /// <summary>
        /// Получатель письма (Email).
        /// </summary>
        [Size(4096)]
        [MemberDesignTimeVisibility(false)]
        [DisplayName("Получатель письма")]
        public string LetterRecipientAddress 
        { 
            get => _letterRecipientAddress; 
            set => _letterRecipientAddress = value?.Length > 4000 ? value.Substring(0, 4000) : value;
        }

        /// <summary>
        /// Тема письма.
        /// </summary>
        [DisplayName("Тема")]
        public string TopicString => ByteToString(Topic);

        /// <summary>
        /// Тема письма.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public byte[] Topic { get; set; }
        
        /// <summary>
        /// Дата получения письма.
        /// </summary>
        [DisplayName("Дата получения")]
        [System.ComponentModel.DataAnnotations.DisplayFormat(DataFormatString = "{0:dd/MM/yyyy (HH:mm:ss)}")]
        public DateTime DateReceiving { get; set; }

        /// <summary>
        /// Дата создания письма.
        /// </summary>
        [DisplayName("Дата создания")]
        [System.ComponentModel.DataAnnotations.DisplayFormat(DataFormatString = "{0:dd/MM/yyyy (HH:mm:ss)}")]
        public DateTime DateCreate { get; set; }

        /// <summary>
        /// Размер письма.
        /// </summary>
        [DisplayName("Размер письма")]
        [MemberDesignTimeVisibility(false)]
        public decimal LetterSize { get; set; }

        /// <summary>
        /// Текст письма.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public byte[] TextBody { get; set; }

        // <summary>
        /// Текст письма HTML.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public byte[] HtmlBody { get; set; }

        /// <summary>
        /// Каталог.
        /// </summary>
        [Association]
        [MemberDesignTimeVisibility(false)]
        public LetterCatalog LetterCatalog { get; set; }

        [Delayed(true)]
        [Association] 
        [Aggregated]
        [MemberDesignTimeVisibility(false)]
        public IList<LetterAttachment> LetterAttachments
        {
            get
            {
                return GetList<LetterAttachment>(nameof(LetterAttachments));
            }
        }

        public static byte[] StringToByte(string msg)
        {
            if (string.IsNullOrWhiteSpace(msg))
            {
                return default;
            }
            else
            {
                return Encoding.Default.GetBytes(msg);
            }
        }

        public static string ByteToString(byte[] msg)
        {
            if (msg == null)
            {
                return default;
            }
            else
            {
                return Encoding.Default.GetString(msg);
            }
        }

        [DisplayName("Почтовый ящик")]
        public string MailingAddress => Mailbox?.MailingAddress;
        /// <summary>
        /// Почтовый ящик.
        /// </summary>
        [Persistent]
        [MemberDesignTimeVisibility(false)]
        public Mailbox Mailbox { get; private set; }

        [DisplayName("Клиент")]
        public string CustomerString => Customer?.ToString();
        [MemberDesignTimeVisibility(false)]
        public Customer Customer { get; set; }

        [DisplayName("Комментарий")]
        public string Description => Deal?.Description;

        [Size(2048)]
        [MemberDesignTimeVisibility(false)]
        public string CopiesAddresses { get; set; }

        public override string ToString()
        {
            return $"[{DateCreate.ToShortDateString()}] от: {LetterSenderAddress} ({TopicString})";
        }

        /// <summary>
        /// Отправка e-mail с помощью NuGet MailKit.
        /// </summary>
        /// <param name="recipient">Email адрес получателя.</param>
        /// <param name="topic">Тема сообщения.</param>
        /// <param name="msg">Сообщение.</param>
        /// <param name="recipientCC">Адрес отправки копии.</param>
        public async static void SendMailKit(string recipient,
            string topic,
            string msg,
            string senderAddressEmail,
            string senderName,
            string senderPassword,
            string outgoingMailServerSMTP,
            int portSMTP,
            List<LetterAttachment> letterAttachments,
            string recipientCC = default,
            List<string> recipientsCC = null)
        {
            try
            {
                senderName = $"BK Algras & {senderName}";
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(senderName, senderAddressEmail));
                message.To.Add(new MailboxAddress(recipient, recipient));

                if (!string.IsNullOrWhiteSpace(recipientCC))
                {
                    message.Cc.Add(new MailboxAddress(recipientCC, recipientCC));
                }

                if (recipientsCC != null)
                {
                    foreach (var item in recipientsCC)
                    {
                        message.Cc.Add(new MailboxAddress(item, item));
                    }
                }

                message.Subject = topic;

                var builder = new BodyBuilder { HtmlBody = msg };

                if (letterAttachments != null)
                {
                    foreach (var letterAttachment in letterAttachments)
                    {
                        builder.Attachments.Add(letterAttachment.FullFileName, letterAttachment.FileByte);
                    }
                }

                message.Body = builder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    await client.ConnectAsync(outgoingMailServerSMTP, portSMTP);
                    await client.AuthenticateAsync(senderAddressEmail, senderPassword);
                    await client.SendAsync(message);
                    client.Disconnect(true);
                }
            }
            catch (SmtpCommandException ex)
            {
                LoggerController.WriteLog(ex?.ToString());
                throw ex;
            }
            catch (ServiceNotAuthenticatedException ex)
            {
                LoggerController.WriteLog(ex?.ToString());
                throw ex;
            }
            catch (ProtocolException ex)
            {
                LoggerController.WriteLog(ex?.ToString());
                throw ex;
            }
            catch (CommandException ex)
            {
                LoggerController.WriteLog(ex?.ToString());
                throw ex;
            }
            catch (ServiceNotConnectedException ex)
            {
                LoggerController.WriteLog(ex?.ToString());
                throw ex;
            }
        }

        public async static void GetMailPOP3(string senderAddressEmail,
            string senderPassword,
            string incomingMailServerPOP3,
            int portPOP3)
        {
            try
            {
                using (var client = new Pop3Client())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    await client.ConnectAsync(incomingMailServerPOP3, portPOP3, false);
                    await client.AuthenticateAsync(senderAddressEmail, senderPassword);
                    for (int i = 0; i < client.Count; i++)
                    {
                        _ = await client.GetMessageAsync(i);
                    }

                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Customer GetCustomer(Session session, string messageFrom)
        {
            var criteriaEmail = new BinaryOperator("Email", messageFrom);
            var customer = session.FindObject<Customer>(criteriaEmail);

            if (customer is null)
            {
                customer = session.FindObject<CustomerEmail>(criteriaEmail)?.Customer;
            }

            return customer;
        }

        public async static System.Threading.Tasks.Task<Customer> GetCustomerAsync(Session session, string messageFrom)
        {
            var criteriaEmail = new BinaryOperator("Email", messageFrom);
            var customer = await session.FindObjectAsync<Customer>(criteriaEmail);

            if (customer is null)
            {
                var customerEmail = await session.FindObjectAsync<CustomerEmail>(criteriaEmail);
                customer = customerEmail?.Customer;
            }

            return customer;
        }

        public static Customer GetCustomer(XPCollection<Customer> xpcollectionCustomer, string messageFrom)
        {
            var customer = xpcollectionCustomer.FirstOrDefault(f => !string.IsNullOrWhiteSpace(f.Email) && messageFrom.Contains(f.Email));

            if (customer is null)
            {
                customer = xpcollectionCustomer.FirstOrDefault(f => f.CustomerEmails.FirstOrDefault(fContact => !string.IsNullOrWhiteSpace(fContact.Email) && messageFrom.Contains(fContact.Email)) != null);
            }

            return customer;
        }

        private static Mailbox GetMailbox(Session session, string senderAddressEmail)
        {
            var mailbox = session.FindObject<Mailbox>(new BinaryOperator(nameof(Mail.Mailbox.MailingAddress), senderAddressEmail));
            return mailbox;
        }


        public async static System.Threading.Tasks.Task<bool> GetMailIMAPAsync(Session session,
            string senderAddressEmail,
            string senderPassword,
            string token,
            string incomingMailServerIMAP,
            int portIMAP)
        {
            return await System.Threading.Tasks.Task.Run(() => GetMailIMAP(session, senderAddressEmail, senderPassword, token, incomingMailServerIMAP, portIMAP)).ConfigureAwait(false);
        }

        private static void WriteLetter(MimeMessage message, Mailbox mailbox, Customer customer, uint? uniqueId = default)
        {
            var messageId = message.MessageId;
            var letter = mailbox.Session.FindObject<Letter>(new BinaryOperator(nameof(MessageId), messageId));

            if (letter is null)
            {
                letter = new Letter(mailbox.Session)
                {
                    UniqueId = uniqueId,
                    DateCreate = message.Date.DateTime,
                    TextBody = StringToByte(message.TextBody),
                    HtmlBody = StringToByte(message.HtmlBody),
                    LetterSender = message.From.ToString(),
                    IsRead = false,
                    Topic = Letter.StringToByte(message.Subject),
                    MessageId = message.MessageId,
                    DateReceiving = DateTime.Now,
                    Mailbox = mailbox,
                    LetterSize = 0M,
                    LetterRecipient = message.To.ToString(),
                    TypeLetter = TypeLetter.InBox,
                    Customer = mailbox.Session.FindObject<Customer>(new BinaryOperator(nameof(Oid), customer.Oid))
                };
                letter.SetMailBox(mailbox);

                foreach (var attachment in message.Attachments)
                {
                    var fileName = attachment.ContentDisposition?.FileName ?? attachment.ContentType.Name;
                    var fileExtension = attachment.ContentType.MediaSubtype;

                    using (var memory = new MemoryStream())
                    {
                        if (attachment is MimePart)
                        {
                            ((MimePart)attachment).Content.DecodeTo(memory);
                        }
                        else
                        {
                            ((MessagePart)attachment).Message.WriteTo(memory);
                        }
                        var bytes = memory.ToArray();

                        letter.LetterAttachments.Add(new LetterAttachment(mailbox.Session)
                        {
                            FileByte = bytes,
                            FullFileName = fileName,
                            FileExtension = $".{fileExtension}"
                        });
                    }
                }

                letter.Save();
            }
        }

        public static void GetLettersByAddress(string letterSenderAddress, Customer customer)
        {
            foreach (var mailClient in MailClients.ListMailClients)
            {
                if (mailClient.ImapClient.IsConnected)
                {
                    try
                    {
                        var query = SearchQuery.FromContains(letterSenderAddress);
                        var folder = mailClient.ImapClient.Inbox;

                        if (!folder.IsOpen)
                        {
                            folder.Open(FolderAccess.ReadOnly);
                        }

                        foreach (var uid in folder.Search(query))
                        {
                            var message = folder.GetMessage(uid);
                            WriteLetter(message, mailClient.Mailbox, customer, uid.Id);
                        }
                    }
                    catch (Exception ex)
                    {
                        LoggerController.WriteLog(ex?.ToString());
                    }
                }
            }
        }

        private async static System.Threading.Tasks.Task<bool> GetMailIMAP(Session session,
            string senderAddressEmail,
            string senderPassword,
            string token,
            string incomingMailServerIMAP,
            int portIMAP)
        {
            if (string.IsNullOrWhiteSpace(senderAddressEmail) || string.IsNullOrWhiteSpace(token))
            {
                return false;
            }

            try
            {
                using (var client = new ImapClient())
                {
                    var oauth2 = new SaslMechanismOAuth2(senderAddressEmail, token);
                    await client.ConnectAsync(incomingMailServerIMAP, portIMAP, true);
                    await client.AuthenticateAsync(oauth2);

                    IList<IMailFolder> folders = await client.GetFoldersAsync(client.PersonalNamespaces.First());
                    _ = folders.Select(t => t.Name).ToList();

                    var xpcollectionCustomer = new XPCollection<Customer>(session);

                    foreach (var item in folders)
                    {
                        await item.OpenAsync(FolderAccess.ReadOnly);
                        var s = SearchQuery.DeliveredAfter(DateTime.Now.Date.AddDays(-7));

                        var items = await item.SearchAsync(s);

                        for (int i = 0; i < items.Count; i++)
                        {
                            var message = await item.GetMessageAsync(items[i]);

                            if (message.Date.DateTime > DateTime.Now.Date.AddDays(-7))
                            {
                                var messageId = message.MessageId;
                                var letter = await session.FindObjectAsync<Letter>(new BinaryOperator(nameof(Letter.MessageId), messageId));

                                if (letter is null)
                                {
                                    letter = new Letter(session)
                                    {
                                        UniqueId = items[i].Id,
                                        DateCreate = message.Date.DateTime,
                                        TextBody = StringToByte(message.TextBody),
                                        HtmlBody = StringToByte(message.HtmlBody),
                                        LetterSender = message.From.ToString(),
                                        IsRead = false,
                                        Topic = Letter.StringToByte(message.Subject),
                                        MessageId = message.MessageId,
                                        Customer = GetCustomer(xpcollectionCustomer, message.From.ToString()),
                                        DateReceiving = DateTime.Now,
                                        Mailbox = GetMailbox(session, senderAddressEmail),
                                        LetterSize = 0M,
                                        LetterRecipient = message.To.ToString(),
                                        TypeLetter = TypeLetter.InBox
                                    };

                                    foreach (var attachment in message.Attachments)
                                    {
                                        var fileName = attachment.ContentDisposition?.FileName ?? attachment.ContentType.Name;
                                        var fileExtension = attachment.ContentType.MediaSubtype;

                                        using (var memory = new MemoryStream())
                                        {
                                            if (attachment is MimePart)
                                            {
                                                ((MimePart)attachment).Content.DecodeTo(memory);
                                            }
                                            else
                                            {
                                                ((MessagePart)attachment).Message.WriteTo(memory);
                                            }
                                            var bytes = memory.ToArray();

                                            letter.LetterAttachments.Add(new LetterAttachment(session)
                                            {
                                                FileByte = bytes,
                                                FullFileName = fileName,
                                                FileExtension = $".{fileExtension}"
                                            });
                                        }
                                    }

                                    letter.Save();
                                }
                            }
                        }
                    }

                    client.Disconnect(true);

                    return true;
                }
            }
            catch (ImapProtocolException ex)
            {
                LoggerController.WriteLog(ex?.ToString());
                return false;
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(ex?.ToString());
                return false;
            }
        }
        
        public static List<string> GetEmailFromText(string text)
        {
            var result = new List<string>();

            if (!string.IsNullOrWhiteSpace(text))
            {
                var matchEmailPattern =
                 @"(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                 + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                 + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                 + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})";
                
                var rx = new Regex(matchEmailPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

                foreach (Match match in rx.Matches(text))
                {
                    result.Add(match.Value);
                }
            }
            
            return result;
        }

        public static List<string> GetPhoneFromText(string text)
        {
            var result = new List<string>();

            if (!string.IsNullOrWhiteSpace(text))
            {
                try
                {
                    //TODO: Не ищет все совпадения по телефонам (https://regex101.com/).
                    var matchEmailPattern = @"\(?\d{3}\)?-? *\d{3}-? *-?\d{2}";
                    var rx = new Regex(matchEmailPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

                    foreach (Match match in rx.Matches(text))
                    {
                        result.Add(match.Value);
                    }
                }
                catch (Exception ex)
                {
                    LoggerController.WriteLog(ex?.ToString());
                }

                try
                {
                    var phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();                    
                    result.AddRange(phoneNumberUtil.FindNumbers(text, "7").Select(s => s.RawString).ToList());
                    result.AddRange(phoneNumberUtil.FindNumbers(text, "8").Select(s => s.RawString).ToList());                    
                    result = result.Distinct().ToList();
                }
                catch (Exception ex)
                {
                    LoggerController.WriteLog(ex?.ToString());
                }
            }
            
            return result;
        }

        public static string DelEmailFromText(string text, string[] args = null)
        {
            var result = default(string);

            if (!string.IsNullOrWhiteSpace(text))
            {
                var emails = Letter.GetEmailFromText(text);
                foreach (var email in emails)
                {
                    text = text.Replace(email, GetAbbreviation(email));
                }


                if (args != null)
                {
                    foreach (var item in args)
                    {
                        text = text.Replace(item, "");
                    }
                }

                result = text;
            }

            return result;
        }

        public static string DelPhoneFromText(string text, string[] args = null)
        {
            var result = default(string);

            if (!string.IsNullOrWhiteSpace(text))
            {
                var phones = Letter.GetPhoneFromText(text);
                foreach (var phone in phones)
                {
                    text = text.Replace(phone, GetAbbreviation(phone));
                }
                
                if (args != null)
                {
                    foreach (var item in args)
                    {
                        text = text.Replace(item, "");
                    }
                }

                result = text;
            }

            return result;
        }
        
        private static string GetAbbreviation(string obj, string abbreviation = "*")
        {
            if (!string.IsNullOrWhiteSpace(obj))
            {
                var result = default(string);

                var isBreak = false;
                
                for (int i = 0; i < obj.Length; i++)
                {
                    try
                    {
                        if (obj[i].Equals('@'))
                        {
                            result += "@";
                            isBreak = true;
                            break;
                        }
                        else if (obj[i].Equals('.'))
                        {
                            result += ".";
                        }
                        else if (obj[i].Equals('-'))
                        {
                            result += "-";
                        }
                        else
                        {
                            result += abbreviation;
                        }
                    }
                    catch (Exception ex)
                    {
                        LoggerController.WriteLog(ex?.ToString());
                    }
                }

                if (isBreak)
                {
                    try
                    {
                        result += obj.Substring(result.Length);
                    }
                    catch (Exception ex)
                    {
                        LoggerController.WriteLog(ex?.ToString());
                    }                    
                }

                return result;
            }
            
            return default;
        }
    }
}
