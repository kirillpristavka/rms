using DevExpress.Xpo;
using RMS.Core.Controllers;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.TG.Core.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Telegram.Bot.Types;
using TelegramBotRMS.Core.Methods;
using TelegramBotRMS.Core.Models;
using TelegramBotRMS.Core.Models.Commands;

namespace TelegramBotRMS.Core.Controllers
{
    public static class UserController
    {
        public async static System.Threading.Tasks.Task UserAuthorizationAsync(Message message, Guid guid)
        {
            var commands = TelegramBot.Commands;
            var client = TelegramBot.GetTelegramBotClient();

            using (var uof = new UnitOfWork())
            {
                await AuthorizationByCustomer(message, guid, uof, commands, client);
                await AuthorizationByStaff(message, guid, uof, commands, client);
            }
        }

        private static async System.Threading.Tasks.Task AuthorizationByCustomer(Message message,
            Guid guid,
            UnitOfWork uof,
            IReadOnlyList<Command> commands,
            Telegram.Bot.TelegramBotClient client)
        {
            var customerEmail = await new XPQuery<CustomerEmail>(uof)?.FirstOrDefaultAsync(f => f != null && f.Guid == guid.ToString());
            if (customerEmail != null)
            {
                var customer = customerEmail.Customer;
                var userIdByMessage = message?.From?.Id;

                if (customer != null && userIdByMessage != null)
                {
                    var customerOid = customer.Oid;
                    var customerTelegramUser = await new XPQuery<CustomerTelegramUser>(uof)
                        ?.Where(w => w.TGUser != null && w.Customer != null && w.Customer.Oid == customerOid)
                        ?.FirstOrDefaultAsync(f => f.TGUser.Id == userIdByMessage);

                    if (customerTelegramUser is null)
                    {
                        customerTelegramUser = new CustomerTelegramUser(uof)
                        {
                            Guid = guid.ToString(),
                            TGUser = await new XPQuery<TGUser>(uof)?.FirstOrDefaultAsync(f => f != null && f.Id == message.From.Id),
                            Customer = customer
                        };
                        customerTelegramUser.Save();

                        await uof.CommitTransactionAsync();
                    }
                }

                await UseAuthorizationCommandAsync(message, commands, client);
            }
        }

        private static async System.Threading.Tasks.Task AuthorizationByStaff(Message message,
            Guid guid,
            UnitOfWork uof,
            IReadOnlyList<Command> commands,
            Telegram.Bot.TelegramBotClient client)
        {
            var staff = await new XPQuery<Staff>(uof)?.FirstOrDefaultAsync(f => f != null && f.Guid == guid.ToString());
            if (staff != null)
            {
                if (staff.TelegramUserId is null || staff.TGUser is null)
                {
                    staff.TelegramUserId = message.From.Id;
                    staff.TGUser = await new XPQuery<TGUser>(uof)?.FirstOrDefaultAsync(f => f != null && f.Id == message.From.Id);
                    staff.Save();
                    await uof.CommitTransactionAsync();
                }

                await UseAuthorizationCommandAsync(message, commands, client);
            }
        }

        private static async System.Threading.Tasks.Task UseAuthorizationCommandAsync(Message message,
            IReadOnlyList<Command> commands,
            Telegram.Bot.TelegramBotClient client)
        {
            try
            {
                var command = commands?.FirstOrDefault(f => f.Contains(nameof(AuthorizationCommand)));
                await command?.Execute(message, client);
            }
            catch (Exception ex)
            {
                await LoggerController.WriteLogBaseAsync(ex.ToString());
                WinConsole.ConsoleWriteLineWithColor(ex.Message, ConsoleColor.Red);
            }
        }
    }
}
