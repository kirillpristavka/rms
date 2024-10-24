using DevExpress.Xpo;
using RMS.Core.Model;
using RMS.Core.Model.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMS.Core.Controllers.Letters
{
    /// <summary>
    /// Контроллер управления почтовыми сообщениями.
    /// </summary>
    public static class LettersController
    {
        public static async Task<List<Letter>> GetLettersAsync()
        {
            using (var uof = new UnitOfWork())
            {
                return await new XPQuery<Letter>(uof).ToListAsync();
            }
        }

        public static async Task<List<Letter>> GetLettersAsync(Session session)
        {
            return await new XPQuery<Letter>(session).ToListAsync();
        }

        public static async Task<List<Letter>> GetLettersByCatalogAsync(Session session, int oid)
        {
            return await new XPQuery<Letter>(session)
                ?.Where(w => 
                    w.LetterCatalog != null 
                    && w.LetterCatalog.Oid == oid)
                ?.ToListAsync();
        }

        public static async System.Threading.Tasks.Task<string> CreateAuthorizationLetterAsync(string guid,
            string user,
            string email,
            string mailingAddressName)
        {
            var mailbox = default(Mailbox);

            using (var uof = new UnitOfWork())
            {
                if (string.IsNullOrWhiteSpace(mailingAddressName))
                {
                    mailbox = await new XPQuery<Mailbox>(uof).FirstOrDefaultAsync(f => f != null && f.StateMailbox == Enumerator.StateMailbox.SendingLetters);
                }

                if (mailbox is null)
                {
                    mailbox = await new XPQuery<Mailbox>(uof).FirstOrDefaultAsync(f => f != null && f.MailingAddress == mailingAddressName);
                }
            }

            if (mailbox is null)
            {
                return "Не задан адрес электронной почты для отправки.";
            }

            Letter.SendMailKit(email,
                "Регистрация пользователя https://t.me/algrasbot",
                Properties.Resources.LetterAuth.Replace("&lt;GUID&gt;", guid),
                mailbox.MailingAddress,
                user,
                Mailbox.Decrypt(mailbox.Password),
                mailbox.MailboxSetup.OutgoingMailServerSMTP,
                mailbox.MailboxSetup.PortSMTP,
                null);

            return "Сообщение успешно отправлено.";
        }
    }
}
