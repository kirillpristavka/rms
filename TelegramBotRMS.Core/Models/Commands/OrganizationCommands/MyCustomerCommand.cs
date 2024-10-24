using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using RMS.Core.Controllers;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotRMS.Core.Methods;

namespace TelegramBotRMS.Core.Models.Commands.OrganizationCommands
{
    public class MyCustomerCommand : Command
    {
        public override bool IsCallbackCommand => false;
        private Message message;
        
        public override string[] Names => new string[] { @"🤼 Мои клиенты" };
        
        public override string Text
        {
            get
            {
                var text = "🤼 Мои клиенты";
                return text;
            }
        }

        public override async System.Threading.Tasks.Task Execute(Message message, TelegramBotClient telegramBotClient)
        {
            this.message = message;
            var chatId = message.Chat.Id;
            
            using (var uof = new UnitOfWork())
            {
                var staff = await new XPQuery<Staff>(uof).FirstOrDefaultAsync(f => f.TelegramUserId != null && f.TelegramUserId == chatId);
                if (staff != null)
                {
                    var customers = new List<Customer>();

                    var status = await new XPQuery<Status>(uof).FirstOrDefaultAsync(f => f.Name != null && f.Name.Equals("Обслуживаем"));
                    if (status is null)
                    {
                        var text = "💥 В базе данных не найдена запись о статусе клиента -> [Обслуживаем]";
                        await SendMessageTelegram(staff, text, ReplyKeyboardMarkupStaff);
                        return;
                    }

                    var user = await new XPQuery<RMS.Core.Model.User>(uof).FirstOrDefaultAsync(f => f.Staff != null && f.Staff.Oid == staff.Oid);
                    if (user != null && user.flagAdministrator)
                    {
                        customers = await new XPQuery<Customer>(uof)
                            .Where(w => w.CustomerStatus != null
                                && w.CustomerStatus.Status != null
                                && w.CustomerStatus.Status.Oid == status.Oid)
                            .ToListAsync();
                    }
                    else
                    {
                        var groupOperator = new GroupOperator(GroupOperatorType.And);

                        var groupOperatorStaff = new GroupOperator(GroupOperatorType.Or);
                        var accountantResponsibleCriteria = new BinaryOperator(nameof(Customer.AccountantResponsible), staff);
                        groupOperatorStaff.Operands.Add(accountantResponsibleCriteria);
                        var primaryResponsibleCriteria = new BinaryOperator(nameof(Customer.PrimaryResponsible), staff);
                        groupOperatorStaff.Operands.Add(primaryResponsibleCriteria);
                        var bankResponsibleCriteria = new BinaryOperator(nameof(Customer.BankResponsible), staff);
                        groupOperatorStaff.Operands.Add(bankResponsibleCriteria);
                        groupOperator.Operands.Add(groupOperatorStaff);
                        var criteriaStatus = new BinaryOperator($"{nameof(Customer.CustomerStatus)}.{nameof(CustomerStatus.Status)}.{nameof(Status.Oid)}", status.Oid);
                        groupOperator.Operands.Add(criteriaStatus);

                        using (var collection = new XPCollection<Customer>(uof, groupOperator))
                        {
                            customers = collection?.ToList();
                        }
                    }

                    var count = customers.Count;
                    if (count > 0)
                    {
                        var listButton = new Dictionary<string, string>();
                        foreach (var customer in customers.OrderBy(o => o.Name).ThenBy(t => t.INN))
                        {
                            var name = customer.AbbreviatedName;
                            if (!string.IsNullOrWhiteSpace(customer.INN))
                            {
                                name += $" {customer.INN}";
                            }
                            listButton.Add($"Customer:OID:{customer.Oid}", name);
                        }
                        var inlineKeyboardMarkup = GetInlineKeyboardMarkup(listButton, 2);

                        var text = $"🤼 Найдено клиентов: {count}";
                        await SendMessageTelegram(staff, text, inlineKeyboardMarkup);
                    }
                }
            }                       
        }

        public static async System.Threading.Tasks.Task SendMessageTelegram(Staff staff, string message, IReplyMarkup replyMarkup = null)
        {
            try
            {
                var client = TelegramBot.GetTelegramBotClient();

                if (staff != null && staff.TelegramUserId != null)
                {
                    await client.SendTextMessageAsync(staff.TelegramUserId, message, parseMode: ParseMode.Html, replyMarkup: replyMarkup);
                    WinConsole.ConsoleWriteLineWithColor(message, ConsoleColor.Magenta);
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