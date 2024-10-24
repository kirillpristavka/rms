using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.TG.Core.Models;
using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotRMS.Core.Controllers;
using TelegramBotRMS.Core.Methods;
using TelegramBotRMS.Core.Models.Commands.CustomerCommands;

namespace TelegramBotRMS.Core.Models.Commands
{
    public class StartCommand : Command
    {
        public override bool IsCallbackCommand => false;
        private Message message;
        
        public override string[] Names => new string[] { @"/start" };
                
        public override string Text
        {
            get
            {
                var userName = message?.From?.Username;
                if (!string.IsNullOrWhiteSpace(userName))
                {
                    userName = $", @{userName}";
                }
                
                var text = $"Добро пожаловать{userName}!{Environment.NewLine}Для авторизации пожалуйста обратитесь к сотрудникам ООО \"БК \"АЛЬГРАС\".";               
                return text;
            }
        }       

        public override async System.Threading.Tasks.Task Execute(Message message, TelegramBotClient telegramBotClient)
        {
            this.message = message;
            var chatId = message.Chat.Id;

            var splits = message.Text?.Split();
            if (splits.Length == 2)
            {
                if (Guid.TryParse(splits[1], out Guid guid))
                {
                    await UserController.UserAuthorizationAsync(message, guid);
                }
            }

            using (var uof = new UnitOfWork())
            {
                var userAdministrator = await new XPQuery<RMS.Core.Model.User>(uof)?.FirstOrDefaultAsync(f => f.TelegramUserId == chatId);
                if (userAdministrator != null)
                {
                    var text = $"⚜ Добро пожаловать великий и могучий!";
                    await telegramBotClient.SendTextMessageAsync(chatId, text, parseMode: ParseMode.Html, replyMarkup: ReplyKeyboardMarkupStaff);
                    WinConsole.ConsoleWriteLineWithColor(text, ConsoleColor.Magenta);
                    return;
                }

                var tgUser = await new XPQuery<TGUser>(uof)?.FirstOrDefaultAsync(f => f.Id == chatId);
                if (tgUser != null)
                {
                    var text = $"Добро пожаловать, {tgUser}!{Environment.NewLine}{Environment.NewLine}" +
                        $"✅ Вы успешно авторизованы в системе.";
                    await telegramBotClient.SendTextMessageAsync(chatId, text, parseMode: ParseMode.Html, replyMarkup: ReplyKeyboardMarkupCustomer);
                    WinConsole.ConsoleWriteLineWithColor(text, ConsoleColor.Magenta);

                    await CustomerMyOrganizations.GetMyOrganizationsAsync(telegramBotClient, uof, tgUser);
                    return;
                }


                var staff = await uof.FindObjectAsync<Staff>(new BinaryOperator(nameof(Staff.TelegramUserId), chatId));
                if (staff != null)
                {
                    var text = $"Добро пожаловать, {staff}";
                    await telegramBotClient.SendTextMessageAsync(chatId, text, parseMode: ParseMode.Html, replyMarkup: ReplyKeyboardMarkupStaff); 
                    WinConsole.ConsoleWriteLineWithColor(text, ConsoleColor.Magenta);
                    return;
                }

                await telegramBotClient.SendTextMessageAsync(chatId, Text, parseMode: ParseMode.Html);
                WinConsole.ConsoleWriteLineWithColor(Text, ConsoleColor.Magenta);
            }
        }
    }
}