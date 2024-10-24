using DevExpress.Xpo;
using RMS.Core.Controllers.PackagesDocument;
using RMS.Core.Model.InfoCustomer;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotRMS.Core.Controllers;
using TelegramBotRMS.Core.Methods;

namespace TelegramBotRMS.Core.Models.Commands.CustomerCommands
{
    public class CustomerMyDocumentCommand : Command
    {
        public override bool IsCallbackCommand => false;
        private Message message;
        
        public override string[] Names => new string[] { @"CustomerPackagesDocument:OID:" };
        
        public override string Text
        {
            get
            {
                var text = "Документы";
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
                    if (int.TryParse(CallbackQuery.Data?.Replace("CustomerPackagesDocument:OID:", ""), out int customerOid))
                    {
                        var customer = await new XPQuery<Customer>(uof)?.FirstOrDefaultAsync(f => f.Oid == customerOid);
                        if (customer != null)
                        {
                            if (customer?.CustomerTelegramUsers?.FirstOrDefault(f => f.TGUser != null && f.TGUser.Id == chatId) != null)
                            {
                                var text = $"🏡 Организация: {customer.AbbreviatedName ?? customer.ToString()}{Environment.NewLine}" +
                                    $"📃 Сформирован отчет по всем документам сотрудников организации";


                                WinConsole.ConsoleWriteLineWithColor($"\t[ANSWER] <-> {customerTG} => {text}", ConsoleColor.Magenta);
                                await telegramBotClient.SendTextMessageAsync(customerTG.TGUser.Id, text, parseMode: ParseMode.Html, replyMarkup: ReplyKeyboardMarkupCustomer);
                                await DocumentController.PackagesDocumentInfoAsync(telegramBotClient, chatId, customer);
                            }
                        }
                    }
                }
            }                       
        }
    }
}