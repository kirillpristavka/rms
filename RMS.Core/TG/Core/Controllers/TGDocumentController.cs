using DevExpress.Xpo;
using RMS.Core.TG.Core.Models;
using Telegram.Bot.Types;

namespace RMS.Core.TG.Core.Controllers
{
    public static class TGDocumentController
    {
        public static TGDocument CreateDocument(UnitOfWork uof, Document document)
        {
            var obj = new TGDocument(uof)
            {
                FileId = document.FileId,
                FileName = document.FileName,
                FileSize = document.FileSize,
                FileUniqueId = document.FileUniqueId,
                MimeType = document.MimeType
            };

            return obj;
        }
    }
}
