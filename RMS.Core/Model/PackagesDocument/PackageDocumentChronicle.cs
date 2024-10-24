using DevExpress.Xpo;
using RMS.Core.Model.Mail;
using System;

namespace RMS.Core.Model.PackagesDocument
{
    public class PackageDocumentChronicle : XPObject
    {
        public PackageDocumentChronicle() { }
        public PackageDocumentChronicle(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Date = DateTime.Now;
        }

        [Persistent]
        public DateTime Date { get; private set; }

        public string DocumentName => Document?.ToString();
        public Document Document { get; set; }

        public string UserName
        {
            get
            {
                if (Staff != null)
                {
                    return Staff?.ToString();
                }

                if (User != null)
                {
                    return User?.ToString();
                }

                return "Система СКиД";
            }
        }

        public User User { get; set; }
        public Staff Staff { get; set; }

        [Size(1024)]
        public string Name { get; set; }

        public string EventString => Letter.ByteToString(Event);
        [Persistent]
        public byte[] Event { get; private set; }

        public void SetEvent(string message)
        {
            Event = Letter.StringToByte(message?.Trim());
            Save();
        }
        
        public PackageDocumentStatus PackageDocumentStatusOld { get; set; }
        public PackageDocumentStatus PackageDocumentStatusNew { get; set; }

        [Association]
        [MemberDesignTimeVisibility(false)]
        public PackageDocumentType PackageDocumentType { get; set; }

        public override string ToString()
        {
            return $"[{DateTime.Now:dd.MM.yyyy (HH:mm:ss)}] ({UserName ?? "Пользователь"}) <-> {EventString}";
        }
    }
}