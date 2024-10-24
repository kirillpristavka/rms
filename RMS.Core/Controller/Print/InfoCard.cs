using RMS.Core.Model.InfoCustomer;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using Word = Microsoft.Office.Interop.Word;

namespace RMS.Core.Controller.Print
{
    /// <summary>
    /// АКТ приема-передачи документов.
    /// </summary>
    public class InfoCard
    {
        private Word.Application WordApplication { get; set; }
        private Word.Document WordDocument { get; set; }
        private Customer Customer { get; }
        public string OutFileName { get; set; } = "Информационная карта";

        private object objMissing = Missing.Value;
        private string tempFile = Path.GetTempFileName();

        public delegate void SaveQuitEventHandler(object sender, string tempFile, byte[] wordDocument, string fileName);
        public event SaveQuitEventHandler SaveQuitEvent;

        public bool WordApplicationClose { get; set; }

        public InfoCard(Customer customer)
        {
            if (customer is null)
            {
                throw new ArgumentNullException(nameof(customer), "Пустой объект [Клиент]");
            }

            Customer = customer;

            WordApplicationClose = false;
            PrintReportWord(true);
            System.Threading.Tasks.Task.Run(() => GetCloseDocument());
        }

        private void Events_Quit()
        {
            WordApplicationClose = true;
        }

        /// <summary>
        /// Печать отчета (Word).
        /// </summary>
        private void PrintReportWord(bool isVisible)
        {
            var templateName = "Информационная карта.docx";
            var pathToTemplate = $"Template\\{templateName}";

            if (!System.IO.File.Exists(pathToTemplate))
            {
                return;
            }

            System.IO.File.Copy(pathToTemplate, tempFile, true);
            WordApplication = new Word.Application();
            ((Word.ApplicationEvents4_Event)WordApplication).Quit += Events_Quit;

            WordDocument = WordApplication.Documents.Add(tempFile, ref objMissing, ref objMissing, ref objMissing);

            FillFirstSheet(WordApplication, WordDocument, Customer);

            OutFileName = $"Информационная карта клиента {Customer} на {DateTime.Now.ToShortDateString()}";
            WordApplication.Visible = isVisible;
        }

        private static void FillFirstSheet(Word.Application wordApplication, Word.Document wordDocument, Customer customer)
        {
            SearchAndReplace(wordApplication, ReportVariables.DAY, DateTime.Now.Date.Day.ToString());
            SearchAndReplace(wordApplication, ReportVariables.MONTH, GetStringGenitiveMonth(DateTime.Now.Date.Month));
            SearchAndReplace(wordApplication, ReportVariables.YEAR, DateTime.Now.Date.Year.ToString());

            SearchAndReplace(wordApplication, ReportVariables.CUSTOMER, customer.ToString() ?? string.Empty);

            var management = $"{customer.ManagementSurname} {customer.ManagementName} {customer.ManagementPatronymic}";
            SearchAndReplace(wordApplication, ReportVariables.CUSTOMERMANAGEMENTFULL, management ?? string.Empty);

            SearchAndReplace(wordApplication, ReportVariables.TELEPHONE, customer.Telephone ?? string.Empty);
            SearchAndReplace(wordApplication, ReportVariables.EMAIL, customer.Email ?? string.Empty);

            //SearchAndReplace(wordApplication, ReportVariables.Contract, customer.Contract?.ToString() ?? string.Empty);

            SearchAndReplace(wordApplication, ReportVariables.INN, customer.INN ?? string.Empty);
            SearchAndReplace(wordApplication, ReportVariables.OKPO, customer.OKPO ?? string.Empty);
            SearchAndReplace(wordApplication, ReportVariables.KPP, customer.KPP ?? string.Empty);
            SearchAndReplace(wordApplication, ReportVariables.OKVED, customer.OKVED ?? string.Empty);
            SearchAndReplace(wordApplication, ReportVariables.OKTMO, customer.OKTMO ?? string.Empty);
            SearchAndReplace(wordApplication, ReportVariables.OKATO, customer.OKATO ?? string.Empty);
            SearchAndReplace(wordApplication, ReportVariables.ADDRESS, customer.LegalAddress ?? string.Empty);

            var psrn = default(string);
            if (!string.IsNullOrWhiteSpace(customer.PSRN))
            {
                psrn = $"{customer.PSRN} ";

                if (customer.DatePSRN != null)
                {
                    psrn += $"{Convert.ToDateTime(customer.DatePSRN).ToShortDateString()}";
                }
            }

            SearchAndReplace(wordApplication, ReportVariables.OGRN, psrn ?? string.Empty);

            SearchAndReplace(wordApplication, ReportVariables.ElectronicReporting, customer.ElectronicReportingString ?? string.Empty);

            SearchAndReplace(wordApplication, ReportVariables.FormCorporationAbbreviatedName, customer.FormCorporation?.AbbreviatedName ?? string.Empty);
            SearchAndReplace(wordApplication, ReportVariables.FormCorporationFullName, customer.FormCorporation?.FullName ?? string.Empty);
            SearchAndReplace(wordApplication, ReportVariables.FormCorporationKod, customer.FormCorporation?.Kod ?? string.Empty);

            SearchAndReplace(wordApplication, ReportVariables.KindActivity, customer.KindActivity?.ToString() ?? string.Empty);
            SearchAndReplace(wordApplication, ReportVariables.TaxSystemCustomer, customer.TaxSystemCustomer?.ToString() ?? string.Empty);
            SearchAndReplace(wordApplication, ReportVariables.Status, customer.StatusString ?? string.Empty);

            var accountantResponsible = default(string);
            var bankResponsible = default(string);
            var primaryResponsible = default(string);

            if (customer.AccountantResponsible != null)
            {
                var date = default(string);

                if (customer.AccountantResponsibleDate != null)
                {
                    date = Convert.ToDateTime(customer.AccountantResponsibleDate).ToShortDateString();
                }

                accountantResponsible = $"{customer.AccountantResponsible} c {date}";
            }

            if (customer.BankResponsible != null)
            {
                var date = default(string);

                if (customer.BankResponsibleDate != null)
                {
                    date = Convert.ToDateTime(customer.BankResponsibleDate).ToShortDateString();
                }

                bankResponsible = $"{customer.BankResponsible} c {date}";
            }

            if (customer.PrimaryResponsible != null)
            {
                var date = default(string);

                if (customer.PrimaryResponsibleDate != null)
                {
                    date = Convert.ToDateTime(customer.PrimaryResponsibleDate).ToShortDateString();
                }

                primaryResponsible = $"{customer.PrimaryResponsible} c {date}";
            }

            SearchAndReplace(wordApplication, ReportVariables.AccountantResponsible, accountantResponsible ?? string.Empty);
            SearchAndReplace(wordApplication, ReportVariables.BankResponsible, bankResponsible ?? string.Empty);
            SearchAndReplace(wordApplication, ReportVariables.PrimaryResponsible, primaryResponsible ?? string.Empty);

            SearchAndReplace(wordApplication, ReportVariables.ServiceDetails, customer.ServiceDetails ?? string.Empty);

            var statisticalReports = default(string);

            if (customer.StatisticalReports != null)
            {
                foreach (var item in customer.StatisticalReports)
                {
                    statisticalReports += $"{item.Report?.ToString()}; ";
                }
            }

            SearchAndReplace(wordApplication, ReportVariables.StatisticalReports, statisticalReports ?? string.Empty);


            var reports = default(string);

            if (customer.TaxSystemCustomer?.TaxSystem?.TaxSystemReports != null)
            {
                foreach (var item in customer.TaxSystemCustomer?.TaxSystem?.TaxSystemReports)
                {
                    reports += $"{item.Report?.ToString()}; ";
                }
            }

            SearchAndReplace(wordApplication, ReportVariables.Reports, reports ?? string.Empty);


            SearchAndReplace(wordApplication, ReportVariables.CUSTOMERMANAGEMENT, customer.ManagementString ?? string.Empty);
        }

        /// <summary>
        /// Проверка закрытия документа Word.
        /// </summary>
        private async System.Threading.Tasks.Task GetCloseDocument()
        {
            await System.Threading.Tasks.Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        if (WordApplicationClose)
                        {
                            SaveQuitEvent?.Invoke(this, tempFile, System.IO.File.ReadAllBytes(tempFile), OutFileName);
                            break;
                        }
                    }
                    catch (Exception) { }
                }
            });
        }

        /// <summary>
        /// Поиск и замена.
        /// </summary>
        private static void SearchAndReplace(Word.Application wordApplication, string findStr, string repStr)
        {
            wordApplication.Selection.Find.ClearFormatting();
            wordApplication.Selection.Find.Replacement.Text = repStr;
            wordApplication.Selection.Find.Execute(findStr, Replace: Word.WdReplace.wdReplaceAll);
        }

        /// <summary>
        /// Преобразование месяца в родительный падеж.
        /// </summary>
        /// <param name="month"></param>
        public static string GetStringGenitiveMonth(int month)
        {
            if (month < 1 || month > 12)
            {
                return "GetStringGenitiveMonth(int month) получил неверное значение.";
            }

            var cultureInfo = CultureInfo.GetCultureInfo("ru-RU");
            return cultureInfo.DateTimeFormat.MonthGenitiveNames[month - 1];
        }
    }
}

