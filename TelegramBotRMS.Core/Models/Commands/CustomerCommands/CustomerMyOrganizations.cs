using DevExpress.Xpo;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.TG.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotRMS.Core.Methods;

namespace TelegramBotRMS.Core.Models.Commands.CustomerCommands
{
    public class CustomerMyOrganizations : Command
    {
        public override bool IsCallbackCommand => true;
        private Message message;
        
        public override string[] Names => new string[] { @"🏡 Мои организации" };
        
        public override string Text
        {
            get
            {
                var text = "Мои организации";
                return text;
            }
        }

        public override async Task Execute(Message message, TelegramBotClient telegramBotClient)
        {
            this.message = message;
            var chatId = message.Chat.Id;
            
            using (var uof = new UnitOfWork())
            {
                var tgUser = await new XPQuery<TGUser>(uof)?.FirstOrDefaultAsync(f => f.Id == chatId);
                if (tgUser != null)
                {
                    await GetMyOrganizationsAsync(telegramBotClient, uof, tgUser);
                }
            }                       
        }

        public static async Task GetMyOrganizationsAsync(TelegramBotClient telegramBotClient, UnitOfWork uof, TGUser tgUser)
        {
            var organizations = await new XPQuery<CustomerTelegramUser>(uof)
                                    ?.Where(w => w.TGUser != null && w.TGUser.Oid == tgUser.Oid && w.Customer != null)
                                    ?.Select(s => s.Customer)
                                    ?.ToListAsync();

            if (organizations != null)
            {
                var count = organizations.Count;
                var text = $"🏡 Найдено организаций: <b>{count}</b>";
                WinConsole.ConsoleWriteLineWithColor($"\t[ANSWER] <-> {tgUser} => {text}", ConsoleColor.Magenta);
                await telegramBotClient.SendTextMessageAsync(tgUser.Id, text, parseMode: ParseMode.Html);

                foreach (var organization in organizations)
                {
                    var answer = default(string);

                    var inn = organization.INN;
                    if (!string.IsNullOrWhiteSpace(inn))
                    {
                        answer += $"ИНН: <b>{inn}</b>{Environment.NewLine}";
                    }

                    var name = organization.AbbreviatedName ?? organization.ToString();
                    answer += $"Наименование: <b>{name}</b>";

                    var listButton = new Dictionary<string, string>();
                    listButton.Add($"CustomerInfoCommand:OID:{organization.Oid}", "ℹ Общая информация");
                    listButton.Add($"CustomerStaffs:OID:{organization.Oid}", "🤼 Сотрудники");
                    listButton.Add($"CustomerPackagesDocument:OID:{organization.Oid}", "📃 Документы");
                    var inlineKeyboardMarkup = GetInlineKeyboardMarkup(listButton);

                    WinConsole.ConsoleWriteLineWithColor($"\t[ANSWER] <-> {tgUser} => {answer}", ConsoleColor.Magenta);
                    await telegramBotClient.SendTextMessageAsync(tgUser.Id, answer, parseMode: ParseMode.Html, replyMarkup: inlineKeyboardMarkup);
                }
            }
        }
    }
}