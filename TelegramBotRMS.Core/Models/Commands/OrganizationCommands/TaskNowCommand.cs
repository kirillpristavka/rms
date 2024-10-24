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
    public class TaskNowCommand : Command
    {
        private DateTime dateSince;
        private DateTime dateTo;
        
        public override bool IsCallbackCommand => false;
        private Message message;
                
        public override string[] Names => new string[] { @"⚡ Мои задачи (сегодня)" };
        
        public override string Text
        {
            get
            {
                var text = "Актуальные задачи";
                return text;
            }
        }       

        public override async System.Threading.Tasks.Task Execute(Message message, TelegramBotClient telegramBotClient)
        {
            var currentDateTime = DateTime.Now.Date;
            dateSince = currentDateTime.AddDays(-1);
            dateTo = currentDateTime.AddDays(1);

            this.message = message;
            var chatId = message.Chat.Id;

            using (var uof = new UnitOfWork())
            {
                var staff = await uof.FindObjectAsync<Staff>(new BinaryOperator(nameof(Staff.TelegramUserId), chatId));
                if (staff != null)
                {                    
                    var groupOperatorTask = new GroupOperator(GroupOperatorType.And);

                    var groupOperator = new GroupOperator(GroupOperatorType.Or);
                    var staffCriteria = new BinaryOperator(nameof(Task.Staff), staff);
                    groupOperator.Operands.Add(staffCriteria);
                    var coExecutorCriteria = new BinaryOperator(nameof(Task.CoExecutor), staff);
                    groupOperator.Operands.Add(coExecutorCriteria);

                    groupOperatorTask.Operands.Add(groupOperator);

                    var taskStatusDone = await uof.FindObjectAsync<TaskStatus>(new BinaryOperator(nameof(TaskStatus.Name), "Выполнена"));
                    if (taskStatusDone != null)
                    {
                        var groupOperatorStatus = new GroupOperator(GroupOperatorType.Or);

                        var taskCriteriaStatusNull = new NullOperator(nameof(Task.TaskStatus));
                        groupOperatorStatus.Operands.Add(taskCriteriaStatusNull);

                        var taskCriteriaStatus = new NotOperator(new BinaryOperator(nameof(Task.TaskStatus), taskStatusDone));
                        groupOperatorStatus.Operands.Add(taskCriteriaStatus);

                        groupOperatorTask.Operands.Add(groupOperatorStatus);
                    }

                    var groupDateCriteria = new GroupOperator(GroupOperatorType.Or);

                    var groupCriteriaDeadline = new GroupOperator(GroupOperatorType.And);
                    var criteriaDateSinceDeadline = new BinaryOperator(nameof(Task.Deadline), dateSince, BinaryOperatorType.Greater);
                    groupCriteriaDeadline.Operands.Add(criteriaDateSinceDeadline);
                    var criteriaDateToDeadline = new BinaryOperator(nameof(Task.Deadline), dateTo, BinaryOperatorType.Less);
                    groupCriteriaDeadline.Operands.Add(criteriaDateToDeadline);
                    groupDateCriteria.Operands.Add(groupCriteriaDeadline);

                    var groupCriteriaConfirmationDate = new GroupOperator(GroupOperatorType.And);
                    var criteriaDateSinceConfirmationDate = new BinaryOperator(nameof(Task.ConfirmationDate), dateSince, BinaryOperatorType.Greater);
                    groupCriteriaConfirmationDate.Operands.Add(criteriaDateSinceConfirmationDate);
                    var criteriaDateToConfirmationDate = new BinaryOperator(nameof(Task.ConfirmationDate), dateTo, BinaryOperatorType.Less);
                    groupCriteriaConfirmationDate.Operands.Add(criteriaDateToConfirmationDate);
                    groupDateCriteria.Operands.Add(groupCriteriaConfirmationDate);

                    var groupCriteriaReplyDate = new GroupOperator(GroupOperatorType.And);
                    var criteriaDateSinceReplyDate = new BinaryOperator(nameof(Task.ReplyDate), dateSince, BinaryOperatorType.Greater);
                    groupCriteriaReplyDate.Operands.Add(criteriaDateSinceReplyDate);
                    var criteriaDateToReplyDate = new BinaryOperator(nameof(Task.ReplyDate), dateTo, BinaryOperatorType.Less);
                    groupCriteriaReplyDate.Operands.Add(criteriaDateToReplyDate);
                    groupDateCriteria.Operands.Add(groupCriteriaReplyDate);

                    groupOperatorTask.Operands.Add(groupDateCriteria);

                    using (var tasks = new XPCollection<Task>(uof, groupOperatorTask))
                    {
                        tasks?.Reload();

                        var count = tasks.Count;
                        var text = $"📑 Всего найдено задач на {currentDateTime.ToShortDateString()}: {count}";
                        WinConsole.ConsoleWriteLineWithColor($"\t[ANSWER] <-> {staff} => {text}", ConsoleColor.Magenta);
                        await telegramBotClient.SendTextMessageAsync(staff.TelegramUserId, text, parseMode: ParseMode.Html, replyMarkup: ReplyKeyboardMarkupStaff);

                        foreach (var task in tasks.OrderByDescending(o => o.Oid).Skip(count - 10))
                        {
                            try
                            {
                                await SendMessageTelegram(task, staff);
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

        private async System.Threading.Tasks.Task SendMessageTelegram(Task task, Staff staff)
        {
            try
            {
                if (task is null)
                {
                    return;
                }

                var givenStaff = task.GivenStaff;
                var customer = task.Customer;

                var client = TelegramBot.GetTelegramBotClient();

                if (staff != null && staff.TelegramUserId != null)
                {
                    staff.Reload();
                    var text = $"⚡ [OID]: {task.Oid}";

                    if (givenStaff != null)
                    {
                        text += $"{Environment.NewLine}<u>Постановщик</u>: {givenStaff}";
                    }

                    if (task.TypeTask != null)
                    {
                        text += $"{Environment.NewLine}<u>Тип задачи</u>: {task.TypeTaskString}";
                    }

                    if (task.TypeTask == RMS.Core.Enumerator.TypeTask.Demand)
                    {
                        if (task.ConfirmationDate is DateTime confirmationDate)
                        {
                            text += $"{Environment.NewLine}<u>Подтвердить до</u>: {confirmationDate.ToShortDateString()}";
                        }

                        if (task.ReplyDate is DateTime replyDate)
                        {
                            text += $"{Environment.NewLine}<u>Ответить до</u>: {replyDate.ToShortDateString()}";
                        }
                    }
                    else
                    {
                        if (task.Date is DateTime date)
                        {
                            text += $"{Environment.NewLine}<u>Дата постановки</u>: {date.ToShortDateString()}";
                        }

                        if (task.Deadline is DateTime deadline)
                        {
                            text += $"{Environment.NewLine}<u>Дата окончания</u>: {deadline.ToShortDateString()}";
                        }
                    }

                    if (customer != null)
                    {
                        text += $"{Environment.NewLine}<u>Клиент</u>: {customer}";
                    }

                    if (!string.IsNullOrWhiteSpace(task.Description))
                    {
                        text += $"{Environment.NewLine}<u>Описание</u>: {task.Description}";
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