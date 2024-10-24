using DevExpress.Xpo;
using RMS.Core.Model.InfoCustomer;
using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotRMS.Core.Methods;

namespace TelegramBotRMS.Core.Models.Commands.CustomerCommands
{
    public class CustomerMyStaffCommand : Command
    {
        public override bool IsCallbackCommand => false;
        private Message message;
        
        public override string[] Names => new string[] { @"CustomerStaffs:OID:" };
        
        public override string Text
        {
            get
            {
                var text = "Мои сотрудники";
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
                    if (int.TryParse(CallbackQuery.Data?.Replace("CustomerStaffs:OID:", ""), out int customerOid))
                    {
                        var customer = await new XPQuery<Customer>(uof)?.FirstOrDefaultAsync(f => f.Oid == customerOid);
                        if (customer != null)
                        {
                            if (customer?.CustomerTelegramUsers?.FirstOrDefault(f => f.TGUser != null && f.TGUser.Id == chatId) != null)
                            {
                                var customerStaffs = await new XPQuery<CustomerStaff>(uof)
                                    ?.Where(w => w.Customer != null && w.Customer.Oid == customerOid)
                                    ?.ToListAsync();

                                var listButton = new Dictionary<string, string>();
                                foreach (var customerStaff in customerStaffs.OrderBy(o => o.Surname).ThenBy(t => t.Name))
                                {
                                    listButton.Add($"CustomerStaff:OID:{customerStaff.Oid}", $"{customerStaff}");
                                }
                                var inlineKeyboardMarkup = GetInlineKeyboardMarkup(listButton);

                                var count = customerStaffs.Count;
                                var text = $"🏡 Организация: {customer.AbbreviatedName ?? customer.ToString()}{Environment.NewLine}" +
                                    $"🤼 Найдено сотрудников: {count}";
                                WinConsole.ConsoleWriteLineWithColor($"\t[ANSWER] <-> {customerTG} => {text}", ConsoleColor.Magenta);
                                await telegramBotClient.SendTextMessageAsync(customerTG.TGUser.Id, text, parseMode: ParseMode.Html, replyMarkup: inlineKeyboardMarkup);
                            }
                        }
                    }
                }
            }                       
        }
    }
}