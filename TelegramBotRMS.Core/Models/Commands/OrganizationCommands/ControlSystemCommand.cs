using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using RMS.Core.Controllers;
using RMS.Core.Model;
using RMS.Core.Model.Notifications;
using System;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotRMS.Core.Methods;

namespace TelegramBotRMS.Core.Models.Commands.OrganizationCommands
{
    public class ControlSystemCommand : Command
    {
        public override bool IsCallbackCommand => false;
        private Message message;
        
        public override string[] Names => new string[] { @"🌟 Список контроля" };
        
        public override string Text
        {
            get
            {
                var text = "Актуальный список контроля";
                return text;
            }
        }
        
        public override async System.Threading.Tasks.Task Execute(Message message, TelegramBotClient telegramBotClient)
        {
            this.message = message;
            var chatId = message.Chat.Id;
            
            using (var uof = new UnitOfWork())
            {
                var staff = await uof.FindObjectAsync<Staff>(new BinaryOperator(nameof(Staff.TelegramUserId), chatId));
                if (staff != null)
                {
                    var collection = await new XPQuery<ControlSystem>(uof)
                        .Where(w => w.Staff != null && w.Staff.Oid == staff.Oid)
                        .ToListAsync();

                    var count = collection.Count;
                    var text = $"🤝 Всего найдено объектов на контроле: {count}";                    
                    WinConsole.ConsoleWriteLineWithColor($"\t[ANSWER] <-> {staff} => {text}", ConsoleColor.Magenta);
                    await telegramBotClient.SendTextMessageAsync(staff.TelegramUserId, text, parseMode: ParseMode.Html, replyMarkup: ReplyKeyboardMarkupStaff);
                                        
                    foreach (var obj in collection.OrderByDescending(o => o.Oid).Skip(count - 10))
                    {
                        try
                        {
                            await SendMessageTelegram(obj);
                        }
                        catch (Exception ex)
                        {
                            await LoggerController.WriteLogBaseAsync(ex.ToString());
                            WinConsole.ConsoleWriteLineWithColor(ex.Message, ConsoleColor.Red);
                        }
                    }
                }
            }                       
        }

        private async System.Threading.Tasks.Task SendMessageTelegram(ControlSystem obj)
        {
            try
            {
                if (obj is null)
                {
                    return;
                }

                var staff = obj.Staff;
                var client = TelegramBot.GetTelegramBotClient();

                if (staff != null && staff.TelegramUserId != null)
                {
                    staff.Reload();
                    var text = $"🆕 [OID]: {obj.Oid}";

                    text += $"{Environment.NewLine}<u>Модуль</u>: {obj.NameModel}";
                    
                    if (!string.IsNullOrWhiteSpace(obj.NameObj))
                    {
                        text += $"{Environment.NewLine}<u>Наименование</u>: {obj.NameObj}";
                    }
                    
                    if (obj.DateSince is DateTime dateSince)
                    {
                        text += $"{Environment.NewLine}<u>Начало контроля</u>: {dateSince.ToShortDateString()}";
                    }
                    
                    if (obj.DateTo is DateTime dateTo)
                    {
                        text += $"{Environment.NewLine}<u>Окончание контроля</u>: {dateTo.ToShortDateString()}";
                    }

                    if (!string.IsNullOrWhiteSpace(obj.CommentString))
                    {
                        text += $"{Environment.NewLine}<u>Комментарий</u>: {obj.CommentString}";
                    }

                    await client.SendTextMessageAsync(staff.TelegramUserId, text, parseMode: ParseMode.Html, replyMarkup: ReplyKeyboardMarkupStaff);
                    WinConsole.ConsoleWriteLineWithColor(text, ConsoleColor.Magenta);
                }
            }
            catch (Exception ex)
            {
                await LoggerController.WriteLogBaseAsync(ex.ToString());
                WinConsole.ConsoleWriteLineWithColor(ex.Message, ConsoleColor.Red);
            }
        }
    }
}