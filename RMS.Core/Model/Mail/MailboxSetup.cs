using DevExpress.Xpo;
using RMS.Core.Enumerator;

namespace RMS.Core.Model.Mail
{
    /// <summary>
    /// Настройки почтового ящика.
    /// </summary>
    public class MailboxSetup : XPObject
    {
        public MailboxSetup() { }
        public MailboxSetup(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

            IncomingMailServerIMAP = "imap.mail.ru";
            IncomingMailServerPOP3 = "pop.mail.ru";
            OutgoingMailServerSMTP = "smtp.mail.ru";
            PortSMTP = 587;
            PortPOP3 = 995;
            PortIMAP = 993;
            EncryptionProtocolIncoming = EncryptionProtocol.SSL;
            EncryptionProtocolOutgoing = EncryptionProtocol.SSL;
        }

        /// <summary>
        /// Сервер входящей почты IMAP.
        /// </summary>
        public string IncomingMailServerIMAP { get; set; }

        /// <summary>
        /// Сервер входящей почты POP3.
        /// </summary>
        public string IncomingMailServerPOP3 { get; set; }

        /// <summary>
        /// Сервер исходящей почты SMTP.
        /// </summary>
        public string OutgoingMailServerSMTP { get; set; }

        /// <summary>
        /// Порт SMTP.
        /// </summary>
        public int PortSMTP { get; set; }

        /// <summary>
        /// Порт POP3.
        /// </summary>
        public int PortPOP3 { get; set; }

        /// <summary>
        /// Порт IMAP.
        /// </summary>
        public int PortIMAP { get; set; }

        /// <summary>
        /// Протокол шифрования входящей почты.
        /// </summary>
        public EncryptionProtocol EncryptionProtocolIncoming { get; set; }

        /// <summary>
        /// Протокол шифрования исходящей почты.
        /// </summary>
        public EncryptionProtocol EncryptionProtocolOutgoing { get; set; }
    }
}
