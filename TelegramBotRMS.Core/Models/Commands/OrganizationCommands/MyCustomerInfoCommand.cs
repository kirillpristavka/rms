using DevExpress.Xpo;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using System;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotRMS.Core.Models.Commands.OrganizationCommands
{
    public class MyCustomerInfoCommand : Command
    {
        public override bool IsCallbackCommand => false;
        private Message message;
        
        public override string[] Names => new string[] { @"Customer:OID:" };
        
        public override string Text
        {
            get
            {
                var text = "Запущен процесс создания приказа";
                return text;
            }
        }
        
        public override async System.Threading.Tasks.Task Execute(Message message, TelegramBotClient telegramBotClient)
        {
            this.message = message;
            var chatId = message.Chat.Id;
            
            using (var uof = new UnitOfWork())
            {
                var staff = await new XPQuery<Staff>(uof)
                    ?.FirstOrDefaultAsync(f => 
                        f.TelegramUserId != null && f.TelegramUserId == chatId 
                        || (f.TGUser != null && f.TGUser.Id == chatId));
                if (staff is null)
                {
                    return;
                }

                if (int.TryParse(CallbackQuery.Data?.Replace("Customer:OID:", ""), out int customerOid))
                {
                    var customer = await new XPQuery<Customer>(uof)?.FirstOrDefaultAsync(f => f.Oid == customerOid);
                    if (customer != null)
                    {
                        var text = $"<b>Сокращенное наименование</b>: {customer.AbbreviatedName}{Environment.NewLine}";
                        text += $"<b>Полное наименование</b>: {customer.FullName}{Environment.NewLine}";

                        var address = customer.CustomerAddress?.ToList();
                        if (address != null && address.Count > 0)
                        {
                            foreach (var item in address)
                            {
                                text += $"<b>Адрес</b>: {item}{Environment.NewLine}";
                            }
                        }

                        await MyCustomerCommand.SendMessageTelegram(staff, text, replyMarkup: ReplyKeyboardMarkupStaff);

                        await SendFirstInfoAsync(staff, customer);
                        await SendEmailsAsync(staff, customer);
                        await SendTelephonesAsync(staff, customer);
                        await SendResponsibleStaffAsync(staff, customer);
                    }
                }                   
            }
        }

        private async System.Threading.Tasks.Task SendEmailsAsync(Staff staff, Customer customer)
        {
            var text = default(string);

            var emails = customer.CustomerEmails?.ToList();
            if (emails != null && emails.Count > 0)
            {
                text = $"📧 Адреса электронной почты:{Environment.NewLine}";
                foreach (var item in emails)
                {
                    text += $"{item.Email}";

                    var name = item.FullNameString ?? item.FullName;
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        text += $" - {name}";
                    }

                    text += Environment.NewLine;
                }
            }

            if (!string.IsNullOrWhiteSpace(text))
            {
                await MyCustomerCommand.SendMessageTelegram(staff, text, replyMarkup: ReplyKeyboardMarkupStaff);
            }
        }

        private async System.Threading.Tasks.Task SendTelephonesAsync(Staff staff, Customer customer)
        {
            var text = default(string);

            var telephones = customer.CustomerTelephones?.ToList();
            if (telephones != null && telephones.Count > 0)
            {
                text = $"☎ Рабочие телефоны:{Environment.NewLine}";
                foreach (var item in telephones)
                {
                    text += $"{item.Telephone}";

                    var name = item.FullNameString ?? item.FullName;
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        text += $" - {name}";
                    }

                    text += Environment.NewLine;
                }
            }

            if (!string.IsNullOrWhiteSpace(text))
            {
                await MyCustomerCommand.SendMessageTelegram(staff, text, replyMarkup: ReplyKeyboardMarkupStaff);
            }
        }

        private async System.Threading.Tasks.Task SendFirstInfoAsync(Staff staff, Customer customer)
        {
            var text = $"<b>ИНН</b>: {customer.INN}{Environment.NewLine}";
            text += $"<b>КПП</b>: {customer.KPP}{Environment.NewLine}";

            if (!string.IsNullOrWhiteSpace(customer.OKPO))
            {
                text += $"<b>ОКПО</b>: {customer.OKPO}{Environment.NewLine}";
            }
            if (!string.IsNullOrWhiteSpace(customer.OKVED))
            {
                text += $"<b>ОКВЭД</b>: {customer.OKVED}{Environment.NewLine}";
            }
            if (!string.IsNullOrWhiteSpace(customer.OKTMO))
            {
                text += $"<b>ОКТМО</b>: {customer.OKTMO}{Environment.NewLine}";
            }
            if (!string.IsNullOrWhiteSpace(customer.OKATO))
            {
                text += $"<b>ОКАТО</b>: {customer.OKATO}{Environment.NewLine}";
            }
            if (customer.DatePSRN is DateTime datePsrn)
            {
                text += $"<b>ОГРН</b>: {customer.PSRN} от {datePsrn.ToShortDateString()}{Environment.NewLine}";
            }
            if (!string.IsNullOrWhiteSpace(customer.FormCorporationString))
            {
                text += $"<b>ОПФ</b>: {customer.FormCorporationString} - {customer.FormCorporation?.FullName ?? customer.FormCorporation.ToString()}{Environment.NewLine}";
            }
            if (!string.IsNullOrWhiteSpace(customer.KindActivityString))
            {
                text += $"<b>Вид деятельности</b>: {customer.KindActivityString}{Environment.NewLine}";
            }
            text += $"<b>Руководитель</b>: {customer.ManagementFullString}{Environment.NewLine}";

            await MyCustomerCommand.SendMessageTelegram(staff, text, replyMarkup: ReplyKeyboardMarkupStaff);
        }

        private async System.Threading.Tasks.Task SendResponsibleStaffAsync(Staff staff, Customer customer)
        {
            var text = default(string);
            if (!string.IsNullOrWhiteSpace(customer.BankResponsibleString) && !customer.BankResponsibleString.Equals("Клиент"))
            {
                text += $"<b>Ответственный за банк</b>: {customer.BankResponsibleString}{Environment.NewLine}";
            }
            if (!string.IsNullOrWhiteSpace(customer.AccountantResponsibleString))
            {
                text += $"<b>Ответственный главный бухгалтер</b>: {customer.AccountantResponsibleString}{Environment.NewLine}";
            }
            if (!string.IsNullOrWhiteSpace(customer.SalaryResponsibleString) && !customer.SalaryResponsibleString.Equals("Клиент"))
            {
                text += $"<b>Ответственный за заработную плату</b>: {customer.SalaryResponsibleString}{Environment.NewLine}";
            }
            if (!string.IsNullOrWhiteSpace(customer.PrimaryResponsibleString) && !customer.PrimaryResponsibleString.Equals("Клиент"))
            {
                text += $"<b>Ответственный за первичные документы</b>: {customer.PrimaryResponsibleString}{Environment.NewLine}";
            }

            if (!string.IsNullOrWhiteSpace(text))
            {
                await MyCustomerCommand.SendMessageTelegram(staff, text, replyMarkup: ReplyKeyboardMarkupStaff);
            }
        }
    }
}