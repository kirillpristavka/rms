using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit;
using RMS.Core.Enumerator;
using RMS.Core.Model;
using RMS.Core.Model.Calculator;
using RMS.Core.Model.InfoContract;
using RMS.Core.Model.InfoContract.ContractAttachments;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.InfoCustomer.Billing;
using RMS.Core.Model.Mail;
using RMS.Core.Model.Reports;
using RMS.Setting.Model.CustomerSettings;
using RMS.Setting.Model.GeneralSettings;
using RMS.UI;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace RMS.Core.CodeFirst
{
    public static class CodeFirst
    {
        public static void RefillingDescriptionDirectories()
        {
            using (Session session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer())
            {
                DeleteData<set_SprEnumeration>(session);
                DeleteData<set_SprShablonView>(session);
                DeleteData<set_SprShablonViewDetail>(session);
                DeleteData<set_SprShablonViewEnumeration>(session);

                cls_BaseSpr.FirstFillSprShablonTables(session);
            }
        }

        public static void DeleteData<T>(Session session) where T : XPObject
        {
            if (session is null)
            {
                session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer();
            }
            
            var xpcollection = new XPCollection<T>(session);
            session.Delete(xpcollection);
        }

        /// <summary>
        /// Первоначальное заполнение БД.
        /// </summary>
        public static async System.Threading.Tasks.Task SetFirstData()
        {
            try
            {
                using (Session session = BVVGlobal.oXpo.GetSessionThreadSafeDataLayer())
                {
                    //if (await session.FindObjectAsync<Mailbox>(null) == null)
                    //{
                    //    var mailbox = new Mailbox(session);
                    //    mailbox.MailingAddress = "algrasmail@yandex.ru";
                    //    mailbox.Password = "0DY9x2MHAyobRs77G/o/Z+HvKoZ1/421F98myXMHTPM=";
                    //    mailbox.Login = "algrasmail";
                    //    mailbox.AccessToken = "AgAAAAApU1egAAZ_a4gUhm-rsk5Vgqo9pZGPFRI";
                    //    mailbox.StateMailbox = StateMailbox.Working;
                    //    mailbox.UserId = "693327776";
                    //    mailbox.MailboxSetup = new MailboxSetup(session)
                    //    {
                    //        IncomingMailServerIMAP = "imap.yandex.ru",
                    //        IncomingMailServerPOP3 = "pop.yandex.ru",
                    //        OutgoingMailServerSMTP = "smtp.yandex.ru",
                    //        PortPOP3 = 995,
                    //        PortIMAP = 993,
                    //        PortSMTP = 587,
                    //        EncryptionProtocolIncoming = EncryptionProtocol.SSL,
                    //        EncryptionProtocolOutgoing = EncryptionProtocol.SSL,
                    //    };
                    //    mailbox.Save();
                    //}

                    //var mailbox = await session.FindObjectAsync<Mailbox>(new BinaryOperator(nameof(Mailbox.MailingAddress), "algrasmail@yandex.ru"));
                    //if (mailbox is null)
                    //{
                    //    mailbox = new Mailbox(session);
                    //}

                    //mailbox.MailingAddress = "algrasmail@yandex.ru";
                    //mailbox.Password = "0DY9x2MHAyobRs77G/o/Z+HvKoZ1/421F98myXMHTPM=";
                    //mailbox.Login = "algrasmail";
                    //mailbox.AccessToken = "AgAAAAApU1egAAZ_a4gUhm-rsk5Vgqo9pZGPFRI";
                    //mailbox.StateMailbox = StateMailbox.Working;
                    //mailbox.UserId = "693327776";
                    //mailbox.MailboxSetup = new MailboxSetup(session)
                    //{
                    //    IncomingMailServerIMAP = "imap.yandex.ru",
                    //    IncomingMailServerPOP3 = "pop.yandex.ru",
                    //    OutgoingMailServerSMTP = "smtp.yandex.ru",
                    //    PortPOP3 = 995,
                    //    PortIMAP = 993,
                    //    PortSMTP = 587,
                    //    EncryptionProtocolIncoming = EncryptionProtocol.SSL,
                    //    EncryptionProtocolOutgoing = EncryptionProtocol.SSL,
                    //};
                    //mailbox.Save();

                    if (await session.FindObjectAsync<Currency>(null) == null)
                    {
                        session.Save(new Currency(session) { Name = "Рубль", ISO = "RUR", OKW = "002" });
                        session.Save(new Currency(session) { Name = "Российские рубли", ISO = "RUB", OKW = "643" });
                        session.Save(new Currency(session) { Name = "Американские доллары", ISO = "USD", OKW = "840" });
                        session.Save(new Currency(session) { Name = "Евро", ISO = "EUR", OKW = "978" });

                        using var uof = new UnitOfWork();
                        var accounts = await new XPQuery<Account>(uof)?.ToListAsync();
                        if (accounts != null)
                        {
                            var currency = await new XPQuery<Currency>(uof)?.FirstOrDefaultAsync(f => f.ISO == "RUB");
                            foreach (var account in accounts)
                            {
                                account.Currency = currency;
                                account.Save();
                            }
                        }
                        await uof.CommitTransactionAsync().ConfigureAwait(false);
                    }

                    if (await session.FindObjectAsync<CalculatorIndicator>(null) == null)
                    {
                        session.Save(new CalculatorIndicator(session)
                        {
                            Name = "Иностранные сотрудники",
                            TypeCalculatorIndicator = TypeCalculatorIndicator.Count,
                            Value = "1300"
                        });

                        session.Save(new CalculatorIndicator(session)
                        {
                            Name = "Алименты и прочие удержания",
                            TypeCalculatorIndicator = TypeCalculatorIndicator.Count,
                            Value = "500"
                        });

                        session.Save(new CalculatorIndicator(session)
                        {
                            Name = "Больничные листы",
                            TypeCalculatorIndicator = TypeCalculatorIndicator.Count,
                            Value = "1000"
                        });

                        session.Save(new CalculatorIndicator(session)
                        {
                            Name = "Путевые листы",
                            TypeCalculatorIndicator = TypeCalculatorIndicator.Count,
                            Value = "500"
                        });

                        session.Save(new CalculatorIndicator(session)
                        {
                            Name = "Корректировка деклараций или расчетов",
                            TypeCalculatorIndicator = TypeCalculatorIndicator.Count,
                            Value = "500"
                        });

                        session.Save(new CalculatorIndicator(session)
                        {
                            Name = "Иные работы",
                            TypeCalculatorIndicator = TypeCalculatorIndicator.Value,
                            Value = "0"
                        });

                        session.Save(new CalculatorIndicator(session)
                        {
                            Name = "Наличие комиссионной торговли",
                            TypeCalculatorIndicator = TypeCalculatorIndicator.Percent,
                            Value = "30"
                        });

                        session.Save(new CalculatorIndicator(session)
                        {
                            Name = "Наличие операций, облагаемых акцизами",
                            TypeCalculatorIndicator = TypeCalculatorIndicator.Percent,
                            Value = "20"
                        });

                        session.Save(new CalculatorIndicator(session)
                        {
                            Name = "Наличие филиала или обособленного подразделения",
                            TypeCalculatorIndicator = TypeCalculatorIndicator.Percent,
                            Value = "15"
                        });

                        session.Save(new CalculatorIndicator(session)
                        {
                            Name = "Наличие ВЭД",
                            TypeCalculatorIndicator = TypeCalculatorIndicator.Percent,
                            Value = "30"
                        });

                        session.Save(new CalculatorIndicator(session)
                        {
                            Name = "Строительство",
                            TypeCalculatorIndicator = TypeCalculatorIndicator.Percent,
                            Value = "25"
                        });

                        session.Save(new CalculatorIndicator(session)
                        {
                            Name = "Производство",
                            TypeCalculatorIndicator = TypeCalculatorIndicator.Percent,
                            Value = "25"
                        });

                        session.Save(new CalculatorIndicator(session)
                        {
                            Name = "Совмещение режимов налогообложения (ОСН+ЕНВД, УСН+ЕНВД и проч.)",
                            TypeCalculatorIndicator = TypeCalculatorIndicator.Percent,
                            Value = "20"
                        });

                        session.Save(new CalculatorIndicator(session)
                        {
                            Name = "Наличие раздельного учета НДС (ставки, облагаемые и не облагаемые НДС операции)",
                            TypeCalculatorIndicator = TypeCalculatorIndicator.Percent,
                            Value = "20"
                        });

                        session.Save(new CalculatorIndicator(session)
                        {
                            Name = "Применение ПБУ 18/02 «Учет расчетов по налогу на прибыль организаций»",
                            TypeCalculatorIndicator = TypeCalculatorIndicator.Percent,
                            Value = "20"
                        });

                        session.Save(new CalculatorIndicator(session)
                        {
                            Name = "Наличие операций с финансовыми вложениями, заемными средствами",
                            TypeCalculatorIndicator = TypeCalculatorIndicator.Percent,
                            Value = "15"
                        });

                        session.Save(new CalculatorIndicator(session)
                        {
                            Name = "Наличие систем оплаты/начисления заработной платы, отличной от оклада",
                            TypeCalculatorIndicator = TypeCalculatorIndicator.Percent,
                            Value = "10"
                        });
                    }

                    if (await session.FindObjectAsync<CalculatorTaxSystem>(null) == null)
                    {
                        /*1*/
                        var calculatorTaxSystem = new CalculatorTaxSystem(session)
                        {
                            Name = "Общая система налогообложения",
                            Description = "Общая система налогообложения"
                        };
                        var tariffScale = new TariffScale(session)
                        {
                            Name = "Без составления первичных документов",
                            Description = "Без составления первичных документов"
                        };
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 0, End = 7, Value = 4000 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 8, End = 15, Value = 8400 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 15, End = 30, Value = 11300 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 30, End = 50, Value = 14200 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 51, End = 70, Value = 16200 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 71, End = 100, Value = 18400 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 101, End = 130, Value = 21600 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 131, End = 160, Value = 24700 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 161, End = 200, Value = 28000 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 201, End = 250, Value = 31400 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 251, End = 300, Value = 33800 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 301, End = 400, Value = 42500 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 401, End = 500, Value = 51400 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 501, End = 600, Value = 59200 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 601, End = 750, Value = 72400 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 751, End = 900, Value = 82900 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 901, End = 1100, Value = 98800 });
                        calculatorTaxSystem.CalculatorTaxSystesmObj.Add(new CalculatorTaxSystemObj(session) { TariffScale = tariffScale });

                        tariffScale = new TariffScale(session)
                        {
                            Name = "С составлением первичных документов",
                            Description = "С составлением первичных документов"
                        };
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 0, End = 7, Value = 4800 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 8, End = 15, Value = 10080 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 15, End = 30, Value = 13560 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 30, End = 50, Value = 17040 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 51, End = 70, Value = 19440 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 71, End = 100, Value = 22080 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 101, End = 130, Value = 25920 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 131, End = 160, Value = 29640 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 161, End = 200, Value = 33600 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 201, End = 250, Value = 37680 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 251, End = 300, Value = 40560 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 301, End = 400, Value = 51000 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 401, End = 500, Value = 61680 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 501, End = 600, Value = 71040 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 601, End = 750, Value = 86880 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 751, End = 900, Value = 99480 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 901, End = 1100, Value = 118560 });
                        calculatorTaxSystem.CalculatorTaxSystesmObj.Add(new CalculatorTaxSystemObj(session) { TariffScale = tariffScale });

                        tariffScale = new TariffScale(session)
                        {
                            Name = "С обслуживанием банка клиента",
                            Description = "С обслуживанием банка клиента"
                        };
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 0, End = 7, Value = 5200 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 8, End = 15, Value = 10920 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 15, End = 30, Value = 14690 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 30, End = 50, Value = 18460 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 51, End = 70, Value = 21060 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 71, End = 100, Value = 23920 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 101, End = 130, Value = 28080 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 131, End = 160, Value = 32110 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 161, End = 200, Value = 36400 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 201, End = 250, Value = 40820 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 251, End = 300, Value = 43940 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 301, End = 400, Value = 55250 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 401, End = 500, Value = 66820 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 501, End = 600, Value = 76960 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 601, End = 750, Value = 94120 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 751, End = 900, Value = 107770 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 901, End = 1100, Value = 128440 });
                        calculatorTaxSystem.CalculatorTaxSystesmObj.Add(new CalculatorTaxSystemObj(session) { TariffScale = tariffScale });

                        calculatorTaxSystem.TariffStaffObj.Add(new TariffStaffObj(session) { Start = 1, End = 1000, Value = 1000 });

                        calculatorTaxSystem.Save();

                        /*2*/
                        calculatorTaxSystem = new CalculatorTaxSystem(session)
                        {
                            Name = "УСН",
                            Description = "УСН"
                        };
                        tariffScale = new TariffScale(session)
                        {
                            Name = "Без составления первичных документов",
                            Description = "Без составления первичных документов"
                        };
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 0, End = 7, Value = 3400 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 8, End = 15, Value = 7200 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 15, End = 30, Value = 9600 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 30, End = 50, Value = 12000 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 51, End = 70, Value = 13800 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 71, End = 100, Value = 15600 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 101, End = 130, Value = 18400 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 131, End = 160, Value = 21000 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 161, End = 200, Value = 23800 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 201, End = 250, Value = 26800 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 251, End = 300, Value = 28800 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 301, End = 400, Value = 36000 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 401, End = 500, Value = 43700 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 501, End = 600, Value = 50400 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 601, End = 750, Value = 61600 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 751, End = 900, Value = 70600 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 901, End = 1100, Value = 84000 });
                        calculatorTaxSystem.CalculatorTaxSystesmObj.Add(new CalculatorTaxSystemObj(session) { TariffScale = tariffScale });

                        tariffScale = new TariffScale(session)
                        {
                            Name = "С составлением первичных документов",
                            Description = "С составлением первичных документов"
                        };
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 0, End = 7, Value = 4080 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 8, End = 15, Value = 8640 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 15, End = 30, Value = 11520 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 30, End = 50, Value = 14400 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 51, End = 70, Value = 16560 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 71, End = 100, Value = 18720 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 101, End = 130, Value = 22080 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 131, End = 160, Value = 25200 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 161, End = 200, Value = 28560 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 201, End = 250, Value = 32160 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 251, End = 300, Value = 34560 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 301, End = 400, Value = 43200 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 401, End = 500, Value = 52440 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 501, End = 600, Value = 60480 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 601, End = 750, Value = 73920 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 751, End = 900, Value = 84720 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 901, End = 1100, Value = 100800 });
                        calculatorTaxSystem.CalculatorTaxSystesmObj.Add(new CalculatorTaxSystemObj(session) { TariffScale = tariffScale });

                        tariffScale = new TariffScale(session)
                        {
                            Name = "С обслуживанием банка клиента",
                            Description = "С обслуживанием банка клиента"
                        };
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 0, End = 7, Value = 4420 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 8, End = 15, Value = 9360 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 15, End = 30, Value = 12480 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 30, End = 50, Value = 15600 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 51, End = 70, Value = 17940 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 71, End = 100, Value = 20280 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 101, End = 130, Value = 23920 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 131, End = 160, Value = 27300 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 161, End = 200, Value = 30940 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 201, End = 250, Value = 34840 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 251, End = 300, Value = 37440 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 301, End = 400, Value = 46800 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 401, End = 500, Value = 56810 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 501, End = 600, Value = 65520 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 601, End = 750, Value = 80080 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 751, End = 900, Value = 91780 });
                        tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 901, End = 1100, Value = 109200 });
                        calculatorTaxSystem.CalculatorTaxSystesmObj.Add(new CalculatorTaxSystemObj(session) { TariffScale = tariffScale });

                        calculatorTaxSystem.TariffStaffObj.Add(new TariffStaffObj(session) { Start = 1, End = 1000, Value = 1000 });

                        calculatorTaxSystem.Save();
                        await EditTariffScalesObj(1.20M);
                    }

                    await CreateCalculatorTaxSystemAsync(session, "Нулевка ОСН", 6000);
                    await CreateCalculatorTaxSystemAsync(session, "Нулевка УСН", 4500);
                    await CreateCalculatorTaxSystemAsync(session, "Нулевка ИП", 3000);

                    await EditNameCalculatorTaxSystemAsync(session, "Общая система налогообложения", "ОСН");

                    if (await session.FindObjectAsync<Document>(null) == null)
                    {
                        session.Save(new Document(session) { Name = "Входящая информация", Description = "СНИЛС, паспорт, ИНН, должность, оклад, подразделение и т.п)" });
                        session.Save(new Document(session) { Name = "Приказ и приеме" });
                        session.Save(new Document(session) { Name = "Согласие на ОПД" });
                        session.Save(new Document(session) { Name = "Трудовой договор" });
                        session.Save(new Document(session) { Name = "Дополнительные услуги" });
                        session.Save(new Document(session) { Name = "Приказ на отпуск" });
                        session.Save(new Document(session) { Name = "Приказ на изменение" });
                        session.Save(new Document(session) { Name = "Приказ на увольнение" });
                        session.Save(new Document(session) { Name = "Ведомость на выплату ЗП" });
                    }

                    if (await session.FindObjectAsync<TaskObject>(null) == null)
                    {
                        session.Save(new TaskObject(session) { IsUse = true, Name = "Отправка документов", Description = "Отправка документов" });
                        session.Save(new TaskObject(session) { IsUse = true, Name = "Звонок клиенту", Description = "Звонок клиенту" });
                    }

                    if (await session.FindObjectAsync<CalculationScale>(null) == null)
                    {
                        var xpcollectionTaxSystem = new XPCollection<TaxSystem>(session);

                        var calculationScale = new CalculationScale(session)
                        {
                            Name = "Шкала для УСН",
                            Description = "Шкала для УСН"
                        };

                        calculationScale.CalculationScaleValues.Add(new CalculationScaleValue(session) { NumberWith = 0, NumberOf = 0, Value = 700 });
                        calculationScale.CalculationScaleValues.Add(new CalculationScaleValue(session) { NumberWith = 1, NumberOf = 7, Value = 3400 });
                        calculationScale.CalculationScaleValues.Add(new CalculationScaleValue(session) { NumberWith = 8, NumberOf = 15, Value = 7200 });
                        calculationScale.CalculationScaleValues.Add(new CalculationScaleValue(session) { NumberWith = 16, NumberOf = 30, Value = 9600 });
                        calculationScale.CalculationScaleValues.Add(new CalculationScaleValue(session) { NumberWith = 31, NumberOf = 50, Value = 12000 });
                        calculationScale.CalculationScaleValues.Add(new CalculationScaleValue(session) { NumberWith = 51, NumberOf = 70, Value = 13800 });
                        calculationScale.CalculationScaleValues.Add(new CalculationScaleValue(session) { NumberWith = 71, NumberOf = 100, Value = 15600 });
                        calculationScale.CalculationScaleValues.Add(new CalculationScaleValue(session) { NumberWith = 101, NumberOf = 130, Value = 18400 });
                        calculationScale.CalculationScaleValues.Add(new CalculationScaleValue(session) { NumberWith = 131, NumberOf = 160, Value = 21000 });
                        calculationScale.CalculationScaleValues.Add(new CalculationScaleValue(session) { NumberWith = 161, NumberOf = 200, Value = 23800 });
                        calculationScale.CalculationScaleValues.Add(new CalculationScaleValue(session) { NumberWith = 201, NumberOf = 250, Value = 26800 });
                        calculationScale.CalculationScaleValues.Add(new CalculationScaleValue(session) { NumberWith = 251, NumberOf = 300, Value = 28800 });
                        calculationScale.CalculationScaleValues.Add(new CalculationScaleValue(session) { NumberWith = 301, NumberOf = 400, Value = 36000 });
                        calculationScale.CalculationScaleValues.Add(new CalculationScaleValue(session) { NumberWith = 401, NumberOf = 500, Value = 43700 });
                        calculationScale.CalculationScaleValues.Add(new CalculationScaleValue(session) { NumberWith = 501, NumberOf = 600, Value = 50400 });
                        calculationScale.CalculationScaleValues.Add(new CalculationScaleValue(session) { NumberWith = 601, NumberOf = 750, Value = 61600 });
                        calculationScale.CalculationScaleValues.Add(new CalculationScaleValue(session) { NumberWith = 751, NumberOf = 900, Value = 70600 });
                        calculationScale.CalculationScaleValues.Add(new CalculationScaleValue(session) { NumberWith = 901, NumberOf = 1100, Value = 84000 });
                        calculationScale.Save();

                        foreach (var item in xpcollectionTaxSystem.Where(w => w.Name.Contains("УСН")))
                        {
                            item.CalculationScale = calculationScale;
                            item.Save();
                        }

                        calculationScale = new CalculationScale(session)
                        {
                            Name = "Шкала для ОСН",
                            Description = "Шкала для ОСН"
                        };

                        calculationScale.CalculationScaleValues.Add(new CalculationScaleValue(session) { NumberWith = 0, NumberOf = 0, Value = 700 });
                        calculationScale.CalculationScaleValues.Add(new CalculationScaleValue(session) { NumberWith = 1, NumberOf = 7, Value = 4000 });
                        calculationScale.CalculationScaleValues.Add(new CalculationScaleValue(session) { NumberWith = 8, NumberOf = 15, Value = 8400 });
                        calculationScale.CalculationScaleValues.Add(new CalculationScaleValue(session) { NumberWith = 16, NumberOf = 30, Value = 11300 });
                        calculationScale.CalculationScaleValues.Add(new CalculationScaleValue(session) { NumberWith = 31, NumberOf = 50, Value = 14200 });
                        calculationScale.CalculationScaleValues.Add(new CalculationScaleValue(session) { NumberWith = 51, NumberOf = 70, Value = 16200 });
                        calculationScale.CalculationScaleValues.Add(new CalculationScaleValue(session) { NumberWith = 71, NumberOf = 100, Value = 18400 });
                        calculationScale.CalculationScaleValues.Add(new CalculationScaleValue(session) { NumberWith = 101, NumberOf = 130, Value = 21600 });
                        calculationScale.CalculationScaleValues.Add(new CalculationScaleValue(session) { NumberWith = 131, NumberOf = 160, Value = 24700 });
                        calculationScale.CalculationScaleValues.Add(new CalculationScaleValue(session) { NumberWith = 161, NumberOf = 200, Value = 28000 });
                        calculationScale.CalculationScaleValues.Add(new CalculationScaleValue(session) { NumberWith = 201, NumberOf = 250, Value = 31400 });
                        calculationScale.CalculationScaleValues.Add(new CalculationScaleValue(session) { NumberWith = 251, NumberOf = 300, Value = 33800 });
                        calculationScale.CalculationScaleValues.Add(new CalculationScaleValue(session) { NumberWith = 301, NumberOf = 400, Value = 42500 });
                        calculationScale.CalculationScaleValues.Add(new CalculationScaleValue(session) { NumberWith = 401, NumberOf = 500, Value = 51400 });
                        calculationScale.CalculationScaleValues.Add(new CalculationScaleValue(session) { NumberWith = 501, NumberOf = 600, Value = 59200 });
                        calculationScale.CalculationScaleValues.Add(new CalculationScaleValue(session) { NumberWith = 601, NumberOf = 750, Value = 72400 });
                        calculationScale.CalculationScaleValues.Add(new CalculationScaleValue(session) { NumberWith = 751, NumberOf = 900, Value = 82900 });
                        calculationScale.CalculationScaleValues.Add(new CalculationScaleValue(session) { NumberWith = 901, NumberOf = 1100, Value = 98800 });
                        calculationScale.Save();

                        foreach (var item in xpcollectionTaxSystem.Where(w => w.Name.Contains("ОСН")))
                        {
                            item.CalculationScale = calculationScale;
                            item.Save();
                        }

                        xpcollectionTaxSystem?.Dispose();
                        xpcollectionTaxSystem?.Distinct();
                    }

                    if (await session.FindObjectAsync<TaskStatus>(null) == null)
                    {
                        session.Save(new TaskStatus(session)
                        {
                            Name = TaskStatus.TaskNewName,
                            Description = TaskStatus.TaskNewName,
                            IsDefault = true,
                            Color = "#DBEEE3",
                            Index = 1,
                            IsProtectionDelete = true
                        });

                        session.Save(new TaskStatus(session)
                        {
                            Name = TaskStatus.TaskTook,
                            Description = "Автоматический статус, устанавливается когда исполнитель открывает задачу",
                            Color = "#DBEEE3",
                            Index = 1,
                            IsProtectionDelete = true
                        });

                        session.Save(new TaskStatus(session)
                        {
                            Name = TaskStatus.TaskCompleted,
                            Description = TaskStatus.TaskCompleted,
                            Color = "#B7F2A1",
                            Index = 2,
                            IsProtectionDelete = true
                        });
                    }

                    if (await session.FindObjectAsync<DealStatus>(null) == null)
                    {
                        session.Save(new DealStatus(session)
                        {
                            Name = "Новая",
                            Description = "Новая",
                            IsDefault = true,
                            Color = "#DBEEE3",
                            Index = 1,
                            IsProtectionDelete = true
                        });

                        session.Save(new DealStatus(session)
                        {
                            Name = "Отложена",
                            Description = "Отложена",
                            IsDefault = false,
                            Color = "#F58096",
                            Index = 2,
                            IsProtectionDelete = true
                        });

                        session.Save(new DealStatus(session)
                        {
                            Name = "Выполнена",
                            Description = "Выполнена",
                            IsDefault = false,
                            Color = "#B7F2A1",
                            Index = 3,
                            IsProtectionDelete = true
                        });

                        session.Save(new DealStatus(session)
                        {
                            Name = "Администратор",
                            Description = "Администратор",
                            IsDefault = false,
                            Color = "#DBF156",
                            Index = 4,
                            IsProtectionDelete = true
                        });

                        session.Save(new DealStatus(session)
                        {
                            Name = "Первичка",
                            Description = "Первичка",
                            IsDefault = false,
                            Color = "#B0C5F5",
                            Index = 5,
                            IsProtectionDelete = true
                        });

                        session.Save(new DealStatus(session)
                        {
                            Name = "ГлавБух",
                            Description = "ГлавБух",
                            IsDefault = false,
                            Index = 6,
                            IsProtectionDelete = true
                        });
                    }

                    if (await session.FindObjectAsync<GroupPerformanceIndicator>(null) == null)
                    {
                        var groupPerformanceIndicator = new GroupPerformanceIndicator(session)
                        {
                            Name = "1. Операции"
                        };
                        groupPerformanceIndicator.PerformanceIndicators.Add(new PerformanceIndicator(session)
                        {
                            Name = "Количество операций по журналу",
                            Description = "Количество операций по журналу",
                            TypePerformanceIndicator = TypePerformanceIndicator.Analysis
                        });
                        groupPerformanceIndicator.PerformanceIndicators.Add(new PerformanceIndicator(session)
                        {
                            Name = "Количество авансовых отчетов",
                            Description = "Количество авансовых отчетов",
                            TypePerformanceIndicator = TypePerformanceIndicator.Base
                        });
                        groupPerformanceIndicator.PerformanceIndicators.Add(new PerformanceIndicator(session)
                        {
                            Name = "Количество операций по банку",
                            Description = "Количество операций по банку",
                            TypePerformanceIndicator = TypePerformanceIndicator.Analysis
                        });
                        groupPerformanceIndicator.PerformanceIndicators.Add(new PerformanceIndicator(session)
                        {
                            Name = "Количество покупок",
                            Description = "Количество покупок",
                            TypePerformanceIndicator = TypePerformanceIndicator.Base
                        });
                        groupPerformanceIndicator.PerformanceIndicators.Add(new PerformanceIndicator(session)
                        {
                            Name = "Количество продаж",
                            Description = "Количество продаж",
                            TypePerformanceIndicator = TypePerformanceIndicator.Base
                        });
                        groupPerformanceIndicator.PerformanceIndicators.Add(new PerformanceIndicator(session)
                        {
                            Name = "Количество банковских счетов",
                            Description = "Количество банковских счетов",
                            TypePerformanceIndicator = TypePerformanceIndicator.Bank,
                            Value = Convert.ToDecimal(2500)
                        });
                        groupPerformanceIndicator.Save();

                        groupPerformanceIndicator = new GroupPerformanceIndicator(session)
                        {
                            Name = "2. Сотрудники"
                        };
                        groupPerformanceIndicator.PerformanceIndicators.Add(new PerformanceIndicator(session)
                        {
                            Name = "Количество сотрудников",
                            Description = "Количество сотрудников",
                            TypePerformanceIndicator = TypePerformanceIndicator.Count,
                            Value = 600
                        });
                        groupPerformanceIndicator.PerformanceIndicators.Add(new PerformanceIndicator(session)
                        {
                            Name = "Количество иностранцев",
                            Description = "Количество иностранцев",
                            TypePerformanceIndicator = TypePerformanceIndicator.Count,
                            Value = 1300
                        });
                        groupPerformanceIndicator.PerformanceIndicators.Add(new PerformanceIndicator(session)
                        {
                            Name = "Количество декретных",
                            Description = "Количество декретных",
                            TypePerformanceIndicator = TypePerformanceIndicator.Count,
                            Value = 1000
                        });
                        groupPerformanceIndicator.PerformanceIndicators.Add(new PerformanceIndicator(session)
                        {
                            Name = "Количество вредников",
                            Description = "Количество вредников",
                            TypePerformanceIndicator = TypePerformanceIndicator.Count,
                            Value = 500
                        });
                        groupPerformanceIndicator.PerformanceIndicators.Add(new PerformanceIndicator(session)
                        {
                            Name = "Количество больничных",
                            Description = "Количество больничных",
                            TypePerformanceIndicator = TypePerformanceIndicator.Count,
                            Value = 1000
                        });
                        groupPerformanceIndicator.PerformanceIndicators.Add(new PerformanceIndicator(session)
                        {
                            Name = "Количество алиментов",
                            Description = "Количество алиментов",
                            TypePerformanceIndicator = TypePerformanceIndicator.Count,
                            Value = 500
                        });
                        groupPerformanceIndicator.PerformanceIndicators.Add(new PerformanceIndicator(session)
                        {
                            Name = "Количество возвращений из ФСС",
                            Description = "Количество возвращений из ФСС",
                            TypePerformanceIndicator = TypePerformanceIndicator.Count,
                            Value = 2500
                        });
                        groupPerformanceIndicator.Save();

                        groupPerformanceIndicator = new GroupPerformanceIndicator(session)
                        {
                            Name = "3. Прочее"
                        };
                        groupPerformanceIndicator.PerformanceIndicators.Add(new PerformanceIndicator(session)
                        {
                            Name = "Количество путевых листов",
                            Description = "Количество путевых листов",
                            TypePerformanceIndicator = TypePerformanceIndicator.Count,
                            Value = 500
                        });
                        groupPerformanceIndicator.PerformanceIndicators.Add(new PerformanceIndicator(session)
                        {
                            Name = "Наличие ВЭД",
                            Description = "Наличие ВЭД",
                            TypePerformanceIndicator = TypePerformanceIndicator.Percent,
                            Value = 30
                        });
                        groupPerformanceIndicator.PerformanceIndicators.Add(new PerformanceIndicator(session)
                        {
                            Name = "Наличие займов",
                            Description = "Наличие займов",
                            TypePerformanceIndicator = TypePerformanceIndicator.Percent,
                            Value = 0
                        });
                        groupPerformanceIndicator.PerformanceIndicators.Add(new PerformanceIndicator(session)
                        {
                            Name = "Наличие кассы",
                            Description = "Наличие кассы",
                            TypePerformanceIndicator = TypePerformanceIndicator.Percent,
                            Value = 0
                        });
                        groupPerformanceIndicator.PerformanceIndicators.Add(new PerformanceIndicator(session)
                        {
                            Name = "Наличие кредитов",
                            Description = "Наличие кредитов",
                            TypePerformanceIndicator = TypePerformanceIndicator.Percent,
                            Value = 0
                        });
                        groupPerformanceIndicator.PerformanceIndicators.Add(new PerformanceIndicator(session)
                        {
                            Name = "Наличие лизинга",
                            Description = "Наличие лизинга",
                            TypePerformanceIndicator = TypePerformanceIndicator.Percent,
                            Value = 0
                        });
                        groupPerformanceIndicator.PerformanceIndicators.Add(new PerformanceIndicator(session)
                        {
                            Name = "Наличие эквайринга",
                            Description = "Наличие эквайринга",
                            TypePerformanceIndicator = TypePerformanceIndicator.Percent,
                            Value = 0
                        });
                        groupPerformanceIndicator.PerformanceIndicators.Add(new PerformanceIndicator(session)
                        {
                            Name = "Строительство",
                            Description = "Строительство",
                            TypePerformanceIndicator = TypePerformanceIndicator.Percent,
                            Value = 25
                        });
                        groupPerformanceIndicator.PerformanceIndicators.Add(new PerformanceIndicator(session)
                        {
                            Name = "Производство",
                            Description = "Производство",
                            TypePerformanceIndicator = TypePerformanceIndicator.Percent,
                            Value = 25
                        });
                        groupPerformanceIndicator.PerformanceIndicators.Add(new PerformanceIndicator(session)
                        {
                            Name = "Наличие комиссионной торговли",
                            Description = "Наличие комиссионной торговли",
                            TypePerformanceIndicator = TypePerformanceIndicator.Percent,
                            Value = 30
                        });
                        groupPerformanceIndicator.PerformanceIndicators.Add(new PerformanceIndicator(session)
                        {
                            Name = "Наличие операций, облагаемых акцизами",
                            Description = "Наличие операций, облагаемых акцизами",
                            TypePerformanceIndicator = TypePerformanceIndicator.Percent,
                            Value = 20
                        });
                        groupPerformanceIndicator.PerformanceIndicators.Add(new PerformanceIndicator(session)
                        {
                            Name = "Наличие филиала/обособленного подразделения",
                            Description = "Наличие филиала/обособленного подразделения",
                            TypePerformanceIndicator = TypePerformanceIndicator.Percent,
                            Value = 15
                        });
                        groupPerformanceIndicator.PerformanceIndicators.Add(new PerformanceIndicator(session)
                        {
                            Name = "Совмещение режимов налогообложения",
                            Description = "Совмещение режимов налогообложения",
                            TypePerformanceIndicator = TypePerformanceIndicator.Percent,
                            Value = 20
                        });
                        groupPerformanceIndicator.PerformanceIndicators.Add(new PerformanceIndicator(session)
                        {
                            Name = "Наличие раздельного учета НДС",
                            Description = "Наличие раздельного учета НДС",
                            TypePerformanceIndicator = TypePerformanceIndicator.Percent,
                            Value = 20
                        });
                        groupPerformanceIndicator.PerformanceIndicators.Add(new PerformanceIndicator(session)
                        {
                            Name = "Применение ПБУ 18/02 «Учет расчетов по налогу на прибыль организаций»",
                            Description = "Применение ПБУ 18/02 «Учет расчетов по налогу на прибыль организаций»",
                            TypePerformanceIndicator = TypePerformanceIndicator.Percent,
                            Value = 20
                        });
                        groupPerformanceIndicator.PerformanceIndicators.Add(new PerformanceIndicator(session)
                        {
                            Name = "Наличие операций с финансовыми вложениями, заемными средствами",
                            Description = "Наличие операций с финансовыми вложениями, заемными средствами",
                            TypePerformanceIndicator = TypePerformanceIndicator.Percent,
                            Value = 15
                        });
                        groupPerformanceIndicator.PerformanceIndicators.Add(new PerformanceIndicator(session)
                        {
                            Name = "Наличие систем оплаты/начисления заработной платы, отличной от оклада",
                            Description = "Наличие систем оплаты/начисления заработной платы, отличной от оклада",
                            TypePerformanceIndicator = TypePerformanceIndicator.Percent,
                            Value = 10
                        });
                        groupPerformanceIndicator.Save();

                        var xpcollectionCustomer = new XPCollection<Customer>(session);
                        var xpcollectionPerformanceIndicator = new XPCollection<GroupPerformanceIndicator>(session);

                        foreach (var customer in xpcollectionCustomer)
                        {
                            if (customer.BillingInformation is null)
                            {
                                customer.BillingInformation = new BillingInformation(session)
                                {
                                    IsBillingGroupPerformanceIndicators = true,
                                };

                                foreach (var group in xpcollectionPerformanceIndicator)
                                {
                                    customer.BillingInformation.BillingGroupPerformanceIndicators.Add(new BillingGroupPerformanceIndicator(session)
                                    {
                                        GroupPerformanceIndicator = group
                                    });
                                }

                                customer.Save();
                                session.Save(customer.BillingInformation.BillingGroupPerformanceIndicators);
                            }
                        }

                        xpcollectionCustomer?.Dispose();
                        xpcollectionCustomer?.Distinct();

                        xpcollectionPerformanceIndicator?.Dispose();
                        xpcollectionPerformanceIndicator?.Distinct();
                    }

                    if (await session.FindObjectAsync<User>(null) == null)
                    {
                        var userGroup = new UserGroup(session)
                        {
                            Name = "Основная группа",
                            Description = "Основная группа"
                        };
                        userGroup.Save();

                        var user = new User(session)
                        {
                            Login = "Administrator",
                            flagAdministrator = true,
                            Name = "Администратор",
                            Password = Mailbox.Encrypt("1"),
                            FullName = "Администратор"
                        };
                        user.UserGroups.Add(new UserGroups(session)
                        {
                            UserGroup = userGroup
                        });
                        user.Save();

                    }

                    if (await session.FindObjectAsync<EntrepreneurialActivityCodesUTII>(null) == null)
                    {
                        session.Save(new EntrepreneurialActivityCodesUTII(session) { Code = "01", Name = "Оказание бытовых услуг" });
                        session.Save(new EntrepreneurialActivityCodesUTII(session) { Code = "02", Name = "Оказание ветеринарных услуг" });
                        session.Save(new EntrepreneurialActivityCodesUTII(session) { Code = "03", Name = "Оказание услуг по ремонту, техническому обслуживанию и мойке автомототранспортных средств" });
                        session.Save(new EntrepreneurialActivityCodesUTII(session) { Code = "04", Name = "Оказание услуг по предоставлению во временное владение (в пользование) мест для стоянки автомототранспортных средств, а также по хранению автомототранспортных средств на платных стоянках" });
                        session.Save(new EntrepreneurialActivityCodesUTII(session) { Code = "05", Name = "Оказание автотранспортных услуг по перевозке грузов" });
                        session.Save(new EntrepreneurialActivityCodesUTII(session) { Code = "06", Name = "Оказание автотранспортных услуг по перевозке пассажиров" });
                        session.Save(new EntrepreneurialActivityCodesUTII(session) { Code = "07", Name = "Розничная торговля, осуществляемая через объекты стационарной торговой сети, имеющие торговые залы" });
                        session.Save(new EntrepreneurialActivityCodesUTII(session) { Code = "08", Name = "Розничная торговля, осуществляемая через объекты стационарной торговой сети, не имеющие торговых залов, а также через объекты нестационарной торговой сети, площадь торгового места в которых не превышает 5 квадратных метров" });
                        session.Save(new EntrepreneurialActivityCodesUTII(session) { Code = "09", Name = "Розничная торговля, осуществляемая через объекты стационарной торговой сети, не имеющие торговых залов, а также через объекты нестационарной торговой сети, площадь торгового места в которых превышает 5 квадратных метров" });
                        session.Save(new EntrepreneurialActivityCodesUTII(session) { Code = "10", Name = "Развозная и разносная розничная торговля" });
                        session.Save(new EntrepreneurialActivityCodesUTII(session) { Code = "11", Name = "Оказание услуг общественного питания через объект организации общественного питания, имеющий зал обслуживания посетителей" });
                        session.Save(new EntrepreneurialActivityCodesUTII(session) { Code = "12", Name = "Оказание услуг общественного питания через объект организации общественного питания, не имеющий зала обслуживания посетителей" });
                        session.Save(new EntrepreneurialActivityCodesUTII(session) { Code = "13", Name = "Распространение наружной рекламы с использованием рекламных конструкций (за исключением рекламных конструкций с автоматической сменой изображения и электронных табло)" });
                        session.Save(new EntrepreneurialActivityCodesUTII(session) { Code = "14", Name = "Распространение наружной рекламы с использованием рекламных конструкций с автоматической сменой изображения" });
                        session.Save(new EntrepreneurialActivityCodesUTII(session) { Code = "15", Name = "Распространение наружной рекламы с использованием электронных табло" });
                        session.Save(new EntrepreneurialActivityCodesUTII(session) { Code = "16", Name = "Размещение рекламы с использованием внешних и внутренних поверхностей транспортных средств" });
                        session.Save(new EntrepreneurialActivityCodesUTII(session) { Code = "17", Name = "Оказание услуг по временному размещению и проживанию" });
                        session.Save(new EntrepreneurialActivityCodesUTII(session) { Code = "18", Name = "Оказание услуг по передаче во временное владение и (или) в пользование торговых мест, расположенных в объектах стационарной торговой сети, не имеющих торговых залов, объектов нестационарной торговой сети, а также объектов организации общественного питания, не имеющих залов обслуживания посетителей, если площадь каждого из них не превышает 5 квадратных метров" });
                        session.Save(new EntrepreneurialActivityCodesUTII(session) { Code = "19", Name = "Оказание услуг по передаче во временное владение и (или) в пользование торговых мест, расположенных в объектах стационарной торговой сети, не имеющих торговых залов, объектов нестационарной торговой сети, а также объектов организации общественного питания, не имеющих залов обслуживания посетителей, если площадь каждого из них превышает 5 квадратных метров" });
                        session.Save(new EntrepreneurialActivityCodesUTII(session) { Code = "20", Name = "Оказание услуг по передаче во временное владение и (или) в пользование земельных участков для размещения объектов стационарной и нестационарной торговой сети, а также объектов организации общественного питания, если площадь земельного участка не превышает 10 квадратных метров" });
                        session.Save(new EntrepreneurialActivityCodesUTII(session) { Code = "21", Name = "Оказание услуг по передаче во временное владение и (или) в пользование земельных участков для размещения объектов стационарной и нестационарной торговой сети, а также объектов организации общественного питания, если площадь земельного участка превышает 10 квадратных метров" });
                        session.Save(new EntrepreneurialActivityCodesUTII(session) { Code = "22", Name = "Реализация товаров с использованием торговых автоматов" });
                    }

                    if (await session.FindObjectAsync<CustomerSettings>(null) == null)
                    {
                        session.Save(new CustomerSettings(session)
                        {
                            Name = "Стандартный шаблон",
                            Description = "Стандартный шаблон отображения таблицы клиентов",
                            IsDefault = true,
                            IsVisibleStatus = true,
                            IsVisibleStatusStatisticalReport = true,
                            IsVisibleINN = true,
                            IsVisibleName = true,
                            IsVisibleProcessedName = true,
                            IsVisibleDefaultName = true,
                            IsVisibleDateRegistration = true,
                            IsVisibleDateLiquidation = true,
                            IsVisibleOrganizationStatus = true,
                            IsVisibleManagementString = true,
                            IsVisibleTelephone = true,
                            IsVisibleEmail = true,
                            IsVisibleLegalAddress = true,
                            IsVisibleFormCorporation = true,
                            IsVisibleContract = true
                        });
                    }

                    if (await session.FindObjectAsync<PlateTemplate>(null) == null)
                    {
                        if (System.IO.File.Exists("template\\ContractTemplate.docx"))
                        {
                            var fullPathFile = Path.GetFullPath("template\\ContractTemplate.docx");

                            var richPlateTemplate = new RichEditControl();
                            richPlateTemplate.LoadDocument(fullPathFile);

                            var tempFile = Path.GetTempFileName();
                            richPlateTemplate.SaveDocument(tempFile, DocumentFormat.Doc);
                            var byteFile = System.IO.File.ReadAllBytes(tempFile);

                            var textBody = Letter.StringToByte(richPlateTemplate.Text);
                            var htmlBody = Letter.StringToByte(richPlateTemplate.HtmlText);

                            session.Save(new PlateTemplate(session)
                            {
                                IsDefault = true,
                                Name = "Стандартный шаблон договора",
                                Description = $"Стандартный шаблон договора (автоматически создан - {DateTime.Now.ToShortDateString()}). Изменение запрещено.",
                                FileWord = byteFile,
                                TextBody = textBody,
                                HtmlBody = htmlBody
                            });

                            System.IO.File.Delete(tempFile);
                        }
                    }

                    if (await session.FindObjectAsync<FAQ>(null) == null)
                    {
                        session.Save(new FAQ(session)
                        {
                            Question = "Печать договоров в Word",
                            Answer = Letter.StringToByte("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /><title></title><style type=\"text/css\">.cs95E872D0{text-align:left;text-indent:0pt;margin:0pt 0pt 0pt 0pt}.cs9D249CCB{color:#000000;background-color:transparent;font-family:'Times New Roman';font-size:12pt;font-weight:normal;font-style:normal;}</style></head><body><p class=\"cs95E872D0\"><span class=\"cs9D249CCB\">При печати договоров в Word программа собирает информацию из БД и использует заранее подготовленный шаблон, в котором указаны переменные на замену.</span></p><p class=\"cs95E872D0\"><span class=\"cs9D249CCB\">&nbsp;</span></p><p class=\"cs95E872D0\"><span class=\"cs9D249CCB\">&lt;ORGANIZATIONNAME&gt; - Наименование организации</span></p><p class=\"cs95E872D0\"><span class=\"cs9D249CCB\">&lt;ORGANIZATIONMANAGMENT&gt; - Руководитель организации</span></p><p class=\"cs95E872D0\"><span class=\"cs9D249CCB\">&lt;ORGANIZATIOINN&gt; - ИНН организации</span></p><p class=\"cs95E872D0\"><span class=\"cs9D249CCB\">&lt;ORGANIZATIONKPP&gt; - КПП организации</span></p><p class=\"cs95E872D0\"><span class=\"cs9D249CCB\">&lt;ORGANIZATIONPSRN&gt; - ОГРН организации</span></p><p class=\"cs95E872D0\"><span class=\"cs9D249CCB\">&lt;ORGANIZATIONTELEPHONE&gt; - Телефон организации</span></p><p class=\"cs95E872D0\"><span class=\"cs9D249CCB\">&lt;DATESINCE&gt; - дата начала действия договора</span></p><p class=\"cs95E872D0\"><span class=\"cs9D249CCB\">&lt;DATETO&gt; - дата окончания действия договора</span></p><p class=\"cs95E872D0\"><span class=\"cs9D249CCB\">&nbsp;</span></p><p class=\"cs95E872D0\"><span class=\"cs9D249CCB\">и другие, первичый шаблон договора находится в папке Temlate \\Template\\ContractTemplate.docx</span></p></body></html>")
                        });
                    }

                    if (await session.FindObjectAsync<StatutoryDocument>(null) == null)
                    {
                        session.Save(new StatutoryDocument(session) { Name = "ОГРН (лист записи)" });
                        session.Save(new StatutoryDocument(session) { Name = "ИНН" });
                        session.Save(new StatutoryDocument(session) { Name = "Извещение из ПФР" });
                        session.Save(new StatutoryDocument(session) { Name = "Извещение из ФСС" });
                        session.Save(new StatutoryDocument(session) { Name = "Коды ОКВЭД" });
                        session.Save(new StatutoryDocument(session) { Name = "Уведомление о переходе на УСН" });
                        session.Save(new StatutoryDocument(session) { Name = "Устав" });
                    }

                    if (await session.FindObjectAsync<TitleDocument>(null) == null)
                    {
                        session.Save(new TitleDocument(session) { Name = "Приказы (об учетной политике, о назначении руководителя и др.)" });
                        session.Save(new TitleDocument(session) { Name = "Справка на руководителя (включая свидетельство о присвоении ИНН)" });
                        session.Save(new TitleDocument(session) { Name = "Договоры с покупателями" });
                        session.Save(new TitleDocument(session) { Name = "Договоры с поставщиками" });
                    }

                    if (await session.FindObjectAsync<TaxReportingDocument>(null) == null)
                    {
                        session.Save(new TaxReportingDocument(session) { Name = "Декларации" });
                        session.Save(new TaxReportingDocument(session) { Name = "Налоговые расчеты" });
                        session.Save(new TaxReportingDocument(session) { Name = "Бухгалтерская отчетность" });
                        session.Save(new TaxReportingDocument(session) { Name = "Книга учета доходов и расходов (только для УСН)" });
                        session.Save(new TaxReportingDocument(session) { Name = "Книга покупок" });
                        session.Save(new TaxReportingDocument(session) { Name = "Книга продаж (только для ОСН)" });
                        session.Save(new TaxReportingDocument(session) { Name = "Кассовая книга" });
                        session.Save(new TaxReportingDocument(session) { Name = "Отчет кассира" });
                    }

                    if (await session.FindObjectAsync<SourceDocument>(null) == null)
                    {
                        session.Save(new SourceDocument(session) { Name = "Платежные поручения" });
                        session.Save(new SourceDocument(session) { Name = "Банковские выписки" });
                        session.Save(new SourceDocument(session) { Name = "Приходные ордера" });
                        session.Save(new SourceDocument(session) { Name = "Расходные ордера" });
                        session.Save(new SourceDocument(session) { Name = "Счета" });
                        session.Save(new SourceDocument(session) { Name = "Счета-фактуры" });
                        session.Save(new SourceDocument(session) { Name = "Акты" });
                        session.Save(new SourceDocument(session) { Name = "Накладные" });
                        session.Save(new SourceDocument(session) { Name = "Платежные и расчетные ведомости заработной платы" });
                    }

                    if (await session.FindObjectAsync<EmployeeDetailsDocument>(null) == null)
                    {
                        session.Save(new EmployeeDetailsDocument(session) { Name = "Копия паспорта (на странице с регистрацией указать почтовый индекс)" });
                        session.Save(new EmployeeDetailsDocument(session) { Name = "Копия страхового свидетельства ПФ РФ 4. Копия ИНН физического лица" });
                        session.Save(new EmployeeDetailsDocument(session) { Name = "Заявление о приеме на работу, заявление о предоставлении налоговых вычетов" });
                    }

                    if (await session.FindObjectAsync<ArchiveFolder>(null) == null)
                    {
                        session.Save(new ArchiveFolder(session) { Name = "Счета входящие", PeriodArchiveFolder = PeriodArchiveFolder.QUARTER });
                        session.Save(new ArchiveFolder(session) { Name = "Счета исходящие", PeriodArchiveFolder = PeriodArchiveFolder.QUARTER });
                        session.Save(new ArchiveFolder(session) { Name = "Книга покупок", PeriodArchiveFolder = PeriodArchiveFolder.MONTH });
                        session.Save(new ArchiveFolder(session) { Name = "Книга продаж", PeriodArchiveFolder = PeriodArchiveFolder.QUARTER });
                        session.Save(new ArchiveFolder(session) { Name = "Эквайринг", PeriodArchiveFolder = PeriodArchiveFolder.NEEDNOT });
                        session.Save(new ArchiveFolder(session) { Name = "Путевые листы", PeriodArchiveFolder = PeriodArchiveFolder.NEEDNOT });
                        session.Save(new ArchiveFolder(session) { Name = "Входящие акты/накладные", PeriodArchiveFolder = PeriodArchiveFolder.MONTH });
                        session.Save(new ArchiveFolder(session) { Name = "Исходящие акты/накладные", PeriodArchiveFolder = PeriodArchiveFolder.NEEDNOT });
                        session.Save(new ArchiveFolder(session) { Name = "Акты сверок", PeriodArchiveFolder = PeriodArchiveFolder.YEAR });
                        session.Save(new ArchiveFolder(session) { Name = "Договоры", PeriodArchiveFolder = PeriodArchiveFolder.QUARTER });
                        session.Save(new ArchiveFolder(session) { Name = "ГТД", PeriodArchiveFolder = PeriodArchiveFolder.YEAR });
                        session.Save(new ArchiveFolder(session) { Name = "Авансовые отчеты", PeriodArchiveFolder = PeriodArchiveFolder.YEAR });
                        session.Save(new ArchiveFolder(session) { Name = "Кассовая книга", PeriodArchiveFolder = PeriodArchiveFolder.YEAR });
                        session.Save(new ArchiveFolder(session) { Name = "Отчетность. Требования", PeriodArchiveFolder = PeriodArchiveFolder.YEAR });
                        session.Save(new ArchiveFolder(session) { Name = "Кадры. Зарплата. Отчеты по сотрудникам", PeriodArchiveFolder = PeriodArchiveFolder.YEAR });
                        session.Save(new ArchiveFolder(session) { Name = "Валютный контроль", PeriodArchiveFolder = PeriodArchiveFolder.NEEDNOT });
                        session.Save(new ArchiveFolder(session) { Name = "Архив", PeriodArchiveFolder = PeriodArchiveFolder.NEEDNOT });
                        session.Save(new ArchiveFolder(session) { Name = "Отчеты агента", PeriodArchiveFolder = PeriodArchiveFolder.NEEDNOT });
                    }

                    if (await session.FindObjectAsync<Status>(null) == null)
                    {
                        session.Save(new Status(session) { Name = "Обслуживаем", Description = "Обслуживаем", IndexIcon = 0 });
                        session.Save(new Status(session) { Name = "Приостановлен", Description = "Приостановлен", IndexIcon = 1 });
                        session.Save(new Status(session) { Name = "Не работаем", Description = "Не работаем", IndexIcon = 2 });
                    }

                    if (await session.FindObjectAsync<Settings>(null) == null)
                    {
                        session.Save(new Settings(session) { IsUseYearReport = true, IsUseDeliveryYearReport = false });
                    }

                    if (await session.FindObjectAsync<ContractStatus>(null) == null)
                    {
                        session.Save(new ContractStatus(session) { Name = "Не подписан", Description = "Не подписан", IndexIcon = 1 });
                        session.Save(new ContractStatus(session) { Name = "Подписан", Description = "Подписан", IndexIcon = 0 });

                        var xpcollectionContract = new XPCollection<Contract>(session);
                        foreach (var contract in xpcollectionContract)
                        {
                            if (contract.StatusContract == StatusContract.NotSigned)
                            {
                                contract.ContractStatus = await session.FindObjectAsync<ContractStatus>(new BinaryOperator(nameof(ContractStatus.Name), "Не подписан"));
                            }
                            else if (contract.StatusContract == StatusContract.Signed)
                            {
                                contract.ContractStatus = await session.FindObjectAsync<ContractStatus>(new BinaryOperator(nameof(ContractStatus.Name), "Подписан"));
                            }
                            contract.Save();
                        }
                        xpcollectionContract.Dispose();
                    }

                    if (await session.FindObjectAsync<Report>(null) == null)
                    {
                        var report = default(Report);

                        report = new Report(session)
                        {
                            FormIndex = "2-НФДЛ",
                            Name = "2-НФДЛ",
                            Periodicity = Periodicity.YEARLY,
                            Deadline = "1 марта",
                            Comment = "Сдают все компании, которые выплачивают работникам зарплату в любой форме"
                        };

                        session.Save(new ReportSchedule(session) { Report = report, Day = 1, Month = Month.March, Period = PeriodReportChange.YEAR });
                        session.Save(report);

                        report = new Report(session)
                        {
                            FormIndex = "6-НФДЛ",
                            Name = "6-НФДЛ",
                            Periodicity = Periodicity.QUARTERLY,
                            Deadline = $"I квартал - 30 апреля{Environment.NewLine}" +
                                        $"II квартал – 31 июля{Environment.NewLine}" +
                                        $"III квартал – 31 октября{Environment.NewLine}" +
                                        $"IV квартал (Год) – 28 февраля",
                            Comment = "Сдают все компании, которые выплачивают работникам зарплату в любой форме"
                        };

                        session.Save(new ReportSchedule(session) { Report = report, Day = 30, Month = Month.April, Period = PeriodReportChange.FIRSTQUARTER });
                        session.Save(new ReportSchedule(session) { Report = report, Day = 31, Month = Month.July, Period = PeriodReportChange.SECONDQUARTER });
                        session.Save(new ReportSchedule(session) { Report = report, Day = 31, Month = Month.October, Period = PeriodReportChange.THIRDQUARTER });
                        session.Save(new ReportSchedule(session) { Report = report, Day = 28, Month = Month.February, Period = PeriodReportChange.FOURTHQUARTER });
                        session.Save(report);

                        report = new Report(session)
                        {
                            FormIndex = "РСВ",
                            Name = "РСВ",
                            Periodicity = Periodicity.QUARTERLY,
                            Deadline = $"I квартал - 30 апреля{Environment.NewLine}" +
                                        $"II квартал – 30 июля{Environment.NewLine}" +
                                        $"III квартал – 30 октября{Environment.NewLine}" +
                                        $"IV квартал (Год) – 30 января",
                            Comment = "Все работодатели"
                        };

                        session.Save(new ReportSchedule(session) { Report = report, Day = 30, Month = Month.April, Period = PeriodReportChange.FIRSTQUARTER });
                        session.Save(new ReportSchedule(session) { Report = report, Day = 30, Month = Month.July, Period = PeriodReportChange.SECONDQUARTER });
                        session.Save(new ReportSchedule(session) { Report = report, Day = 30, Month = Month.October, Period = PeriodReportChange.THIRDQUARTER });
                        session.Save(new ReportSchedule(session) { Report = report, Day = 30, Month = Month.January, Period = PeriodReportChange.FOURTHQUARTER });
                        session.Save(report);

                        report = new Report(session)
                        {
                            FormIndex = "ССЧ",
                            Name = "ССЧ",
                            Periodicity = Periodicity.YEARLY,
                            Deadline = $"20 января",
                            Comment = "Все, кто имеют работников"
                        };

                        session.Save(new ReportSchedule(session) { Report = report, Day = 20, Month = Month.January, Period = PeriodReportChange.YEAR });
                        session.Save(report);

                        report = new Report(session)
                        {
                            FormIndex = "НД Прибыль",
                            Name = "НД Прибыль",
                            Periodicity = Periodicity.QUARTERLY,
                            Deadline = $"I квартал - 28 апреля{Environment.NewLine}" +
                                        $"II квартал – 28 июля{Environment.NewLine}" +
                                        $"III квартал – 28 октября{Environment.NewLine}" +
                                        $"IV квартал – 30 марта",
                            Comment = "Организации на общей системе налогообложения"
                        };

                        session.Save(new ReportSchedule(session) { Report = report, Day = 28, Month = Month.April, Period = PeriodReportChange.FIRSTQUARTER });
                        session.Save(new ReportSchedule(session) { Report = report, Day = 28, Month = Month.July, Period = PeriodReportChange.SECONDQUARTER });
                        session.Save(new ReportSchedule(session) { Report = report, Day = 28, Month = Month.October, Period = PeriodReportChange.THIRDQUARTER });
                        session.Save(new ReportSchedule(session) { Report = report, Day = 30, Month = Month.March, Period = PeriodReportChange.FOURTHQUARTER });
                        session.Save(report);

                        report = new Report(session)
                        {
                            FormIndex = "НДС",
                            Name = "НДС",
                            Periodicity = Periodicity.QUARTERLY,
                            Deadline = $"I квартал - 25 апреля{Environment.NewLine}" +
                                        $"II квартал – 25 июля{Environment.NewLine}" +
                                        $"III квартал – 25 октября{Environment.NewLine}" +
                                        $"IV квартал – 25 января",
                            Comment = "Все, кто платит НДС"
                        };

                        session.Save(new ReportSchedule(session) { Report = report, Day = 25, Month = Month.April, Period = PeriodReportChange.FIRSTQUARTER });
                        session.Save(new ReportSchedule(session) { Report = report, Day = 25, Month = Month.July, Period = PeriodReportChange.SECONDQUARTER });
                        session.Save(new ReportSchedule(session) { Report = report, Day = 25, Month = Month.October, Period = PeriodReportChange.THIRDQUARTER });
                        session.Save(new ReportSchedule(session) { Report = report, Day = 25, Month = Month.January, Period = PeriodReportChange.FOURTHQUARTER });
                        session.Save(report);

                        report = new Report(session)
                        {
                            FormIndex = "УСН",
                            Name = "УСН",
                            Periodicity = Periodicity.YEARLY,
                            Deadline = $"31 марта",
                            Comment = "Организации и ИП на УСН"
                        };

                        session.Save(new ReportSchedule(session) { Report = report, Day = 31, Month = Month.March, Period = PeriodReportChange.YEAR });
                        session.Save(report);

                        report = new Report(session)
                        {
                            FormIndex = "ЕНВД",
                            Name = "ЕНВД",
                            Periodicity = Periodicity.QUARTERLY,
                            Deadline = $"I квартал - 20 апреля{Environment.NewLine}" +
                                        $"II квартал – 20 июля{Environment.NewLine}" +
                                        $"III квартал – 20 октября{Environment.NewLine}" +
                                        $"IV квартал – 20 января",
                            Comment = "Организации и бизнесмены на временнике"
                        };

                        session.Save(new ReportSchedule(session) { Report = report, Day = 20, Month = Month.April, Period = PeriodReportChange.FIRSTQUARTER });
                        session.Save(new ReportSchedule(session) { Report = report, Day = 20, Month = Month.July, Period = PeriodReportChange.SECONDQUARTER });
                        session.Save(new ReportSchedule(session) { Report = report, Day = 20, Month = Month.October, Period = PeriodReportChange.THIRDQUARTER });
                        session.Save(new ReportSchedule(session) { Report = report, Day = 20, Month = Month.January, Period = PeriodReportChange.FOURTHQUARTER });
                        session.Save(report);

                        report = new Report(session)
                        {
                            FormIndex = "НД Имущество",
                            Name = "НД Имущество",
                            Periodicity = Periodicity.YEARLY,
                            Deadline = $"30 марта",
                            Comment = "Организации, у которых есть на учете имущество"
                        };

                        session.Save(new ReportSchedule(session) { Report = report, Day = 30, Month = Month.March, Period = PeriodReportChange.YEAR });
                        session.Save(report);

                        report = new Report(session)
                        {
                            FormIndex = "НД Транспорт",
                            Name = "НД Транспорт",
                            Periodicity = Periodicity.YEARLY,
                            Deadline = $"2 февраля",
                            Comment = "Только организации, имеющие на балансе какой-либо транспорт"
                        };

                        session.Save(new ReportSchedule(session) { Report = report, Day = 2, Month = Month.February, Period = PeriodReportChange.YEAR });
                        session.Save(report);

                        report = new Report(session)
                        {
                            FormIndex = "НД Земля",
                            Name = "НД Земля",
                            Periodicity = Periodicity.YEARLY,
                            Deadline = $"2 февраля",
                            Comment = "Только предприятия – владельцы земельной собственности"
                        };

                        session.Save(new ReportSchedule(session) { Report = report, Day = 2, Month = Month.February, Period = PeriodReportChange.YEAR });
                        session.Save(report);

                        report = new Report(session)
                        {
                            FormIndex = "3-НДФЛ",
                            Name = "3-НДФЛ",
                            Periodicity = Periodicity.YEARLY,
                            Deadline = $"30 апреля",
                            Comment = "Индивидуальные предприниматели"
                        };

                        session.Save(new ReportSchedule(session) { Report = report, Day = 30, Month = Month.April, Period = PeriodReportChange.YEAR });
                        session.Save(report);

                        report = new Report(session)
                        {
                            FormIndex = "СЗВ-М",
                            Name = "СЗВ-М",
                            Periodicity = Periodicity.MONTHLY,
                            Deadline = $"Предоставляется ежемесячно до 15 числа",
                            Comment = string.Empty
                        };

                        session.Save(new ReportSchedule(session) { Report = report, Day = 15, Month = Month.January, Period = PeriodReportChange.MONTH });
                        session.Save(new ReportSchedule(session) { Report = report, Day = 15, Month = Month.February, Period = PeriodReportChange.MONTH });
                        session.Save(new ReportSchedule(session) { Report = report, Day = 15, Month = Month.March, Period = PeriodReportChange.MONTH });
                        session.Save(new ReportSchedule(session) { Report = report, Day = 15, Month = Month.April, Period = PeriodReportChange.MONTH });
                        session.Save(new ReportSchedule(session) { Report = report, Day = 15, Month = Month.May, Period = PeriodReportChange.MONTH });
                        session.Save(new ReportSchedule(session) { Report = report, Day = 15, Month = Month.June, Period = PeriodReportChange.MONTH });
                        session.Save(new ReportSchedule(session) { Report = report, Day = 15, Month = Month.July, Period = PeriodReportChange.MONTH });
                        session.Save(new ReportSchedule(session) { Report = report, Day = 15, Month = Month.August, Period = PeriodReportChange.MONTH });
                        session.Save(new ReportSchedule(session) { Report = report, Day = 15, Month = Month.September, Period = PeriodReportChange.MONTH });
                        session.Save(new ReportSchedule(session) { Report = report, Day = 15, Month = Month.October, Period = PeriodReportChange.MONTH });
                        session.Save(new ReportSchedule(session) { Report = report, Day = 15, Month = Month.November, Period = PeriodReportChange.MONTH });
                        session.Save(new ReportSchedule(session) { Report = report, Day = 15, Month = Month.December, Period = PeriodReportChange.MONTH });
                        session.Save(report);

                        report = new Report(session)
                        {
                            FormIndex = "СЗВ-СТАЖ",
                            Name = "СЗВ-СТАЖ",
                            Periodicity = Periodicity.YEARLY,
                            Deadline = $"2 марта",
                            Comment = "Индивидуальные предприниматели"
                        };

                        session.Save(new ReportSchedule(session) { Report = report, Day = 2, Month = Month.March, Period = PeriodReportChange.YEAR });
                        session.Save(report);

                        report = new Report(session)
                        {
                            FormIndex = "СЗВ-ТД",
                            Name = "СЗВ-ТД",
                            Periodicity = Periodicity.MONTHLY,
                            Deadline = $"Предоставляется ежемесячно до 15 числа",
                            Comment = string.Empty
                        };

                        session.Save(new ReportSchedule(session) { Report = report, Day = 15, Month = Month.January, Period = PeriodReportChange.MONTH });
                        session.Save(new ReportSchedule(session) { Report = report, Day = 15, Month = Month.February, Period = PeriodReportChange.MONTH });
                        session.Save(new ReportSchedule(session) { Report = report, Day = 15, Month = Month.March, Period = PeriodReportChange.MONTH });
                        session.Save(new ReportSchedule(session) { Report = report, Day = 15, Month = Month.April, Period = PeriodReportChange.MONTH });
                        session.Save(new ReportSchedule(session) { Report = report, Day = 15, Month = Month.May, Period = PeriodReportChange.MONTH });
                        session.Save(new ReportSchedule(session) { Report = report, Day = 15, Month = Month.June, Period = PeriodReportChange.MONTH });
                        session.Save(new ReportSchedule(session) { Report = report, Day = 15, Month = Month.July, Period = PeriodReportChange.MONTH });
                        session.Save(new ReportSchedule(session) { Report = report, Day = 15, Month = Month.August, Period = PeriodReportChange.MONTH });
                        session.Save(new ReportSchedule(session) { Report = report, Day = 15, Month = Month.September, Period = PeriodReportChange.MONTH });
                        session.Save(new ReportSchedule(session) { Report = report, Day = 15, Month = Month.October, Period = PeriodReportChange.MONTH });
                        session.Save(new ReportSchedule(session) { Report = report, Day = 15, Month = Month.November, Period = PeriodReportChange.MONTH });
                        session.Save(new ReportSchedule(session) { Report = report, Day = 15, Month = Month.December, Period = PeriodReportChange.MONTH });
                        session.Save(report);

                        report = new Report(session)
                        {
                            FormIndex = "ФСС",
                            Name = "ФСС",
                            Periodicity = Periodicity.QUARTERLY,
                            Deadline = $"I квартал - 25 апреля{Environment.NewLine}" +
                                        $"II квартал – 25 июля{Environment.NewLine}" +
                                        $"III квартал – 25 октября{Environment.NewLine}" +
                                        $"IV квартал – 25 января",
                            Comment = string.Empty
                        };

                        session.Save(new ReportSchedule(session) { Report = report, Day = 25, Month = Month.April, Period = PeriodReportChange.FIRSTQUARTER });
                        session.Save(new ReportSchedule(session) { Report = report, Day = 25, Month = Month.July, Period = PeriodReportChange.SECONDQUARTER });
                        session.Save(new ReportSchedule(session) { Report = report, Day = 25, Month = Month.October, Period = PeriodReportChange.THIRDQUARTER });
                        session.Save(new ReportSchedule(session) { Report = report, Day = 25, Month = Month.January, Period = PeriodReportChange.FOURTHQUARTER });
                        session.Save(report);

                        report = new Report(session)
                        {
                            FormIndex = "БухОтчетность",
                            Name = "БухОтчетность",
                            Periodicity = Periodicity.YEARLY,
                            Deadline = $"31 марта",
                            Comment = "Все организации"
                        };

                        session.Save(new ReportSchedule(session) { Report = report, Day = 31, Month = Month.March, Period = PeriodReportChange.YEAR });
                        session.Save(report);

                        report = new Report(session)
                        {
                            FormIndex = "ПОВД",
                            Name = "Подтверждение основного вида деятельности ",
                            Periodicity = Periodicity.YEARLY,
                            Deadline = $"15 апреля",
                            Comment = string.Empty
                        };

                        session.Save(new ReportSchedule(session) { Report = report, Day = 15, Month = Month.April, Period = PeriodReportChange.YEAR });
                        session.Save(report);
                    }

                    cls_BaseSpr.FirstFillSprShablonTables(session);
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, " Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static async System.Threading.Tasks.Task CreateCalculatorTaxSystemAsync(Session session, string name, int value)
        {
            if (await new XPQuery<CalculatorTaxSystem>(session).FirstOrDefaultAsync(f => f.Name == name) == null)
            {
                var calculatorTaxSystem = new CalculatorTaxSystem(session)
                {
                    Name = name,
                    Description = name
                };

                var tariffScale = new TariffScale(session)
                {
                    Name = "Базовая шкала",
                    Description = "Базовая шкала"
                };
                tariffScale.TariffScalesObj.Add(new TariffScaleObj(session) { Start = 0, End = 999, Value = value });
                calculatorTaxSystem.CalculatorTaxSystesmObj.Add(new CalculatorTaxSystemObj(session) { TariffScale = tariffScale });

                calculatorTaxSystem.TariffStaffObj.Add(new TariffStaffObj(session) { Start = 1, End = 1000, Value = 1000 });

                calculatorTaxSystem.Save();
            }
        }

        private static async System.Threading.Tasks.Task EditNameCalculatorTaxSystemAsync(Session session, string name, string newName)
        {
            var obj = await new XPQuery<CalculatorTaxSystem>(session).FirstOrDefaultAsync(f => f.Name == name);
            if (obj != null)
            {
                obj.Name = newName;
                obj.Save();
            }
        }


        private static async System.Threading.Tasks.Task EditTariffScalesObj(decimal value)
        {
            using (var uof = new UnitOfWork())
            {
                var tariffScales = await new XPQuery<TariffScale>(uof)?.ToListAsync();
                if (tariffScales != null)
                {
                    foreach (var obj in tariffScales)
                    {
                        if (obj.TariffScalesObj != null)
                        {
                            foreach (var tariffScalesObj in obj.TariffScalesObj)
                            {
                                tariffScalesObj.Value *= value;
                                tariffScalesObj.Save();
                            }
                        }
                    }
                }
                await uof.CommitTransactionAsync();
            }
        }
    }
}
