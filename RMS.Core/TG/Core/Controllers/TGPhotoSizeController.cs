using DevExpress.Xpo;
using RMS.Core.TG.Core.Models;
using Telegram.Bot.Types;

namespace RMS.Core.TG.Core.Controllers
{
    public static class TGPhotoSizeController
    {
        public static TGPhoto CreatePhoto(UnitOfWork uof, PhotoSize photoSize)
        {
            var obj = new TGPhoto(uof)
            {
                FileId = photoSize.FileId,
                FileSize = photoSize.FileSize,
                FileUniqueId = photoSize.FileUniqueId,
                Width = photoSize.Width,
                Height = photoSize.Height
            };

            return obj;
        }
    }
}
