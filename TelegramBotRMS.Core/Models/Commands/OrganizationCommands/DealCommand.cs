using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using RMS.Core.Controllers;
using RMS.Core.Model;
using System;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotRMS.Core.Methods;

namespace TelegramBotRMS.Core.Models.Commands.OrganizationCommands
{
    public class DealCommand : Command
    {
        public override bool IsCallbackCommand => false;
        private Message message;
        
        public override string[] Names => new string[] { @"🤝 Мои сделки" };
        
        public override string Text
        {
            get
            {
                var text = "Актуальные сделки";
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
                    var dealCriteria = new GroupOperator(GroupOperatorType.And);
                    var dealCriteriaStaff = new BinaryOperator(nameof(Deal.Staff), staff);
                    dealCriteria.Operands.Add(dealCriteriaStaff);

                    var dealStatusDone = await uof.FindObjectAsync<DealStatus>(new BinaryOperator(nameof(DealStatus.Name), "Выполнена"));
                    if (dealStatusDone != null)
                    {
                        var groupOperatorStatus = new GroupOperator(GroupOperatorType.Or);

                        var dealCriteriaStatusNull = new NullOperator(nameof(Deal.DealStatus));
                        groupOperatorStatus.Operands.Add(dealCriteriaStatusNull);

                        var dealCriteriaDealStatus = new NotOperator(new BinaryOperator(nameof(Deal.DealStatus), dealStatusDone));
                        groupOperatorStatus.Operands.Add(dealCriteriaDealStatus);

                        dealCriteria.Operands.Add(groupOperatorStatus);
                    }

                    using (var deals = new XPCollection<Deal>(uof, dealCriteria))
                    {
                        deals?.Reload();

                        var count = deals.Count;
                        var text = $"🤝 Всего найдено активных сделок: {count}";
                        WinConsole.ConsoleWriteLineWithColor($"\t[ANSWER] <-> {staff} => {text}", ConsoleColor.Magenta);
                        await telegramBotClient.SendTextMessageAsync(staff.TelegramUserId, text, parseMode: ParseMode.Html, replyMarkup: ReplyKeyboardMarkupStaff);

                        foreach (var deal in deals.OrderBy(o => o.Oid).Skip(count - 10).OrderByDescending(o => o.Oid))
                        {
                            try
                            {
                                await SendMessageTelegram(deal);
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
        }

        private async System.Threading.Tasks.Task SendMessageTelegram(Deal deal)
        {
            try
            {
                if (deal is null)
                {
                    return;
                }

                var staff = deal.Staff;
                var customer = deal.Customer;

                var client = TelegramBot.GetTelegramBotClient(staff.Session);

                if (staff != null && staff.TelegramUserId != null)
                {
                    staff.Reload();
                    var text = $"🆕 [OID]: {deal.Oid}";

                    if (deal.LetterDate is DateTime date)
                    {
                        text += $"{Environment.NewLine}<u>Дата получения</u>: {date.ToShortDateString()}";
                    }
                    
                    if (deal.DealStatus != null)
                    {
                        text += $"{Environment.NewLine}<u>Статус сделки</u>: {deal.StatusString}";
                    }

                    if (!string.IsNullOrWhiteSpace(deal.LetterTopic))
                    {
                        text += $"{Environment.NewLine}<u>Тема</u>: {deal.LetterTopic}";
                    }
                    
                    if (customer != null)
                    {
                        text += $"{Environment.NewLine}<u>Клиент</u>: {customer}";
                    }

                    if (!string.IsNullOrWhiteSpace(deal.Description))
                    {
                        text += $"{Environment.NewLine}<u>Описание</u>: {deal.Description}";
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