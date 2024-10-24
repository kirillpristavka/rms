using DevExpress.Xpo;
using RMS.Core.Model;
using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotRMS.Core.Methods;

namespace TelegramBotRMS.Core.Models.Commands
{
    public class AuthorizationCommand : Command
    {
        public override bool IsCallbackCommand => false;
        private Message message;
        
        public override string[] Names => new string[] { @"AuthorizationCommand" };
        
        public override string Text
        {
            get
            {
                var text = "💢 Авторизация успешно пройдена.";
                return text;
            }
        }       

        public override async System.Threading.Tasks.Task Execute(Message message, TelegramBotClient telegramBotClient)
        {
            this.message = message;
            var chatId = message.Chat.Id;

            var isStaff = false;

            using (var uof = new UnitOfWork())
            {
                var staff = await new XPQuery<Staff>(uof)
                    ?.Where(w => w.TelegramUserId == chatId || (w.TGUser != null && w.TGUser.Id == chatId))
                    ?.FirstOrDefaultAsync();

                if (staff != null)
                {
                    isStaff = true;
                }
            }

            if (isStaff)
            {
                await telegramBotClient.SendTextMessageAsync(chatId, Text, parseMode: ParseMode.Html, replyMarkup: ReplyKeyboardMarkupStaff);
            }
            else
            {
                await telegramBotClient.SendTextMessageAsync(chatId, Text, parseMode: ParseMode.Html, replyMarkup: ReplyKeyboardMarkupCustomer);
            }
            WinConsole.ConsoleWriteLineWithColor(Text, ConsoleColor.Magenta);
        }
    }
}