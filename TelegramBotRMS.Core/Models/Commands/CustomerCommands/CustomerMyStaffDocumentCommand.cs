using DevExpress.Xpo;
using RMS.Core.Model.InfoCustomer;
using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotRMS.Core.Controllers;
using TelegramBotRMS.Core.Methods;

namespace TelegramBotRMS.Core.Models.Commands.CustomerCommands
{
    public class CustomerMyStaffDocumentCommand : Command
    {
        public override bool IsCallbackCommand => false;
        private Message message;
        
        public override string[] Names => new string[] { @"CustomerStaff:OID:" };
        
        public override string Text
        {
            get
            {
                var text = "Запущен процесс создания приказа";
                return text;
            }
        }
        
        public override async System.Threading.Tasks.Task Execute(Message message, TelegramBotClient telegramBotClient)
        {
            this.message = message;
            var chatId = message.Chat.Id;
            
            using (var uof = new UnitOfWork())
            {
                var customerTG = await new XPQuery<CustomerTelegramUser>(uof)?.FirstOrDefaultAsync(f => f.TGUser != null && f.TGUser.Id == chatId);
                if (customerTG != null)
                {
                    if (int.TryParse(CallbackQuery.Data?.Replace("CustomerStaff:OID:", ""), out int customerStaffOid))
                    {
                        var customerStaff = await new XPQuery<CustomerStaff>(uof)?.FirstOrDefaultAsync(f => f.Oid == customerStaffOid);
                        if (customerStaff != null && customerStaff.Customer != null)
                        {                           
                            var text = $"Сформирован отчет по сотруднику {customerStaff}";
                            WinConsole.ConsoleWriteLineWithColor($"\t[ANSWER] <-> {customerTG} => {text}", ConsoleColor.Magenta);
                            await telegramBotClient.SendTextMessageAsync(customerTG.TGUser.Id, text, parseMode: ParseMode.Html, replyMarkup: ReplyKeyboardMarkupCustomer);
                            await DocumentController.PackagesDocumentInfoAsync(telegramBotClient, chatId, customerStaff.Customer, customerStaff, $"Отчет по сотруднику {customerStaff}");
                        }
                    }
                }
            }
        }
    }
}