using RMS.Core.TG.Core.Models;
using System;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot;

namespace RMS.Core.TG.Core.Controllers
{
    public static class TGFileBaseController
    {
        public async static Task DownloadFileAsync(this TGFileBase obj, TelegramBotClient client)
        {
            var tempFile = $"{Path.GetTempPath()}{(Guid.NewGuid().ToString().Replace("-", ""))}.tmp";
            using (var fileStream = File.OpenWrite(tempFile))
            {
                var file = await client.GetInfoAndDownloadFileAsync(fileId: obj.FileId, destination: fileStream);
            }
            obj.Obj = File.ReadAllBytes(tempFile);
        }
    }
}
