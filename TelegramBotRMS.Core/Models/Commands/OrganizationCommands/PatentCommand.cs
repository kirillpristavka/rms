using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using RMS.Core.Controllers;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using System;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotRMS.Core.Methods;

namespace TelegramBotRMS.Core.Models.Commands.OrganizationCommands
{
    public class PatentCommand : Command
    {
        private DateTime dateSince;
        private DateTime dateTo;

        public override bool IsCallbackCommand => false;
        private Message message;
        
        public override string[] Names => new string[] { @"📃 Патенты" };
        
        public override string Text
        {
            get
            {
                var text = "📨 В списке ниже будут отображены клиенты и информация окончании действия патентов по ним 🔽";
                return text;
            }
        }       

        public override async System.Threading.Tasks.Task Execute(Message message, TelegramBotClient telegramBotClient)
        {            
            dateSince = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dateTo = dateSince.AddMonths(2).AddDays(-1);            
            
            this.message = message;
            var chatId = message.Chat.Id;

            using (var uof = new UnitOfWork())
            {
                var staff = await uof.FindObjectAsync<Staff>(new BinaryOperator(nameof(Staff.TelegramUserId), chatId));
                if (staff != null)
                {
                    WinConsole.ConsoleWriteLineWithColor($"\t[ANSWER] <-> {staff} => Патенты с {dateSince.ToShortDateString()} по {dateTo.ToShortDateString()}", ConsoleColor.Magenta);
                    await SendMessageTelegram(staff, Text);

                    var groupOperator = new GroupOperator(GroupOperatorType.And);

                    var groupOperatorStaff = new GroupOperator(GroupOperatorType.Or);
                    var accountantResponsibleCriteria = new BinaryOperator(nameof(Customer.AccountantResponsible), staff);
                    groupOperatorStaff.Operands.Add(accountantResponsibleCriteria);
                    var primaryResponsibleCriteria = new BinaryOperator(nameof(Customer.PrimaryResponsible), staff);
                    groupOperatorStaff.Operands.Add(primaryResponsibleCriteria);
                    var bankResponsibleCriteria = new BinaryOperator(nameof(Customer.BankResponsible), staff);
                    groupOperatorStaff.Operands.Add(bankResponsibleCriteria);
                    groupOperator.Operands.Add(groupOperatorStaff);

                    var statusCustomer = await uof.FindObjectAsync<Status>(new BinaryOperator(nameof(Status.Name), "Обслуживаем"));
                    if (statusCustomer != null)
                    {
                        var criteriaStatus = new BinaryOperator($"{nameof(Customer.CustomerStatus)}.{nameof(CustomerStatus.Status)}", statusCustomer);
                        groupOperator.Operands.Add(criteriaStatus);
                    }
                    else
                    {
                        var text = "💥 В базе данных не найдена запись о статусе клиента -> [Обслуживаем]";
                        await SendMessageTelegram(staff, text);
                        return;
                    }

                    using (var customers = new XPCollection<Customer>(uof, groupOperator))
                    {
                        var text = default(string);
                        foreach (var customer in customers)
                        {
                            try
                            {
                                var patentObject = customer.Tax?.Patent?.PatentObjects;
                                patentObject?.Reload();

                                if (patentObject != null)
                                {
                                    foreach (var obj in patentObject.Where(w => w.DateTo >= dateSince && w.DateTo <= dateTo))
                                    {
                                        try
                                        {
                                            obj?.Reload();
                                            if (obj.DateTo is DateTime dateTimeTo)
                                            {
                                                var lastObj = patentObject.Where(w => w.ClassOKVED2 == obj.ClassOKVED2).FirstOrDefault(f => f.DateTo is null || f.DateTo > dateTo);
                                                if (lastObj != null)
                                                {
                                                    continue;
                                                }

                                                text += $"💢 [OID]: {customer.Oid}{Environment.NewLine}" +
                                                    $"<u>Клиент</u>: {customer}{Environment.NewLine}" +
                                                    $"<u>Патент</u>: {obj}{Environment.NewLine}" +
                                                    $"<u>Дата окончания действия</u>: {dateTimeTo.ToShortDateString()}{Environment.NewLine}{Environment.NewLine}";
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
                            catch (Exception ex)
                            {
                                await LoggerController.WriteLogBaseAsync(ex.ToString());
                                WinConsole.ConsoleWriteLineWithColor(ex.Message, ConsoleColor.Red);
                            }
                        }

                        if (string.IsNullOrWhiteSpace(text))
                        {
                            text = $"Информация по заканчивающимся патентам за период " +
                                $"с <u>{dateSince.ToShortDateString()}</u> по <u>{dateTo.ToShortDateString()}</u> не найдена";
                        }
                        
                        await SendMessageTelegram(staff, text);
                    }
                }
            }
            
        }

        private async System.Threading.Tasks.Task SendMessageTelegram(Staff staff, string message)
        {
            try
            {
                var client = TelegramBot.GetTelegramBotClient();

                if (staff != null && staff.TelegramUserId != null)
                {                    
                    await client.SendTextMessageAsync(staff.TelegramUserId, message, parseMode: ParseMode.Html, replyMarkup: ReplyKeyboardMarkupStaff);
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