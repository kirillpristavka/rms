using RMS.Core.Controllers.PackagesDocument;
using RMS.Core.Model.InfoCustomer;
using System;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotRMS.Core.Controllers
{
    public static class DocumentController
    {
        public static async Task PackagesDocumentInfoAsync(
            TelegramBotClient telegramBotClient,
            long chatId,
            Customer customer,
            CustomerStaff customerStaff = null,
            string caption = "Отчет по всем сотрудникам клиента")
        {
            var file = await PackagesDocumentInfoController.GetInfoFilePathAsync(customer, customerStaff);
            if (System.IO.File.Exists(file))
            {
                using (Stream stream = System.IO.File.OpenRead(file))
                {
                    await telegramBotClient.SendDocumentAsync(
                        chatId: chatId,
                        document: new InputFileStream(content: stream, 
                            fileName: $"{caption} за {DateTime.Now.Date.ToShortDateString()}.html"));
                }
            }
        }
    }
}
