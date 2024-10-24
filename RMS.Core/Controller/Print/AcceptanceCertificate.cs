using RMS.Core.Extensions;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using System;
using System.Collections.Generic;
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
    public class AcceptanceCertificate
    {
        private Word.Application WordApplication { get; set; }
        private Word.Document WordDocument { get; set; }
        private Customer Customer { get; }
        private List<ArchiveFolderChange> ArchiveFolderChanges { get; }
        public string OutFileName { get; set; } = "Опись на передачу документов от";

        private object objMissing = Missing.Value;
        private string tempFile = Path.GetTempFileName();

        public delegate void SaveQuitEventHandler(object sender, string tempFile, byte[] wordDocument, string fileName);
        public event SaveQuitEventHandler SaveQuitEvent;

        public bool WordApplicationClose { get; set; }

        public AcceptanceCertificate(List<ArchiveFolderChange> archiveFolderChanges)
        {
            if (archiveFolderChanges is null)
            {
                throw new ArgumentNullException(nameof(archiveFolderChanges), "Пустая коллекция архивных папок");
            }

            ArchiveFolderChanges = archiveFolderChanges;
            Customer = archiveFolderChanges.Select(f => f.Customer).FirstOrDefault();

            if (Customer is null)
            {
                throw new ArgumentNullException(nameof(Customer), "В коллекции архивных папок не найден ни один клиент");
            }

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
            //var templateName = "АКТ приема-передачи документов.docx";
            var templateName = "AcceptanceCertificate.docx";
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
            FillTable();

            OutFileName = $"Опись на передачу документов от {DateTime.Now.ToShortDateString()}";
            WordApplication.Visible = isVisible;
            //WordDocument.SaveAs(tempFile);
        }

        private static void FillFirstSheet(Word.Application wordApplication, Word.Document wordDocument, Customer customer)
        {
            SearchAndReplace(wordApplication, ReportVariables.DAY, DateTime.Now.Date.Day.ToString());
            SearchAndReplace(wordApplication, ReportVariables.MONTH, GetStringGenitiveMonth(DateTime.Now.Date.Month));
            SearchAndReplace(wordApplication, ReportVariables.YEAR, DateTime.Now.Date.Year.ToString());

            SearchAndReplace(wordApplication, ReportVariables.CUSTOMER, customer.AbbreviatedName.ToString() ?? string.Empty);
            SearchAndReplace(wordApplication, ReportVariables.CUSTOMERMANAGEMENT, customer.ManagementString ?? string.Empty);
        }

        /// <summary>
        /// Проверка закрытия документа Word.
        /// </summary>
        private async System.Threading.Tasks.Task GetCloseDocument()
        {
            //await System.Threading.Tasks.Task.Run(() =>
            //{
            //    while (true)
            //    {
            //        try
            //        {
            //            if (WordApplicationClose)
            //            {
            //                SaveQuitEvent?.Invoke(this, tempFile, System.IO.File.ReadAllBytes(tempFile), OutFileName);
            //                break;
            //            }
            //        }
            //        catch (Exception) { }
            //    }
            //});
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
        /// Поиск и замена в колонтитулах.
        /// </summary>
        /// <param name="findStr"></param>
        /// <param name="repStr"></param>
        private static void SearchAndReplaceHeaderAndFooter(Word.Document wordDocument, string findStr, string repStr)
        {
            foreach (Word.Section section in wordDocument.Sections)
            {
                Word.Range footerRange = section.Footers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                footerRange.Find.ClearFormatting();
                footerRange.Find.Replacement.Text = repStr;
                footerRange.Find.Execute(findStr, Replace: Word.WdReplace.wdReplaceAll);
            }
        }

        /// <summary>
        /// Преобразование месяца в родительный падеж.
        /// </summary>
        /// <param name="month"></param>
        private static string GetStringGenitiveMonth(int month)
        {
            if (month < 1 || month > 12)
            {
                return "GetStringGenitiveMonth(int month) получил неверное значение.";
            }

            var cultureInfo = CultureInfo.GetCultureInfo("ru-RU");
            return cultureInfo.DateTimeFormat.MonthGenitiveNames[month - 1];
        }

        private void FillTable()
        {
            var archiveFolderChanges = ArchiveFolderChanges.Where(w => w.Customer == Customer).ToList();

            var status = archiveFolderChanges?.FirstOrDefault(f => f.StatusArchiveFolder == Enumerator.StatusArchiveFolder.RECEIVEDEDS);
            if (status is null)
            {
                SearchAndReplace(WordApplication, "<ORGANIZATIONACTION>", "передал");
                SearchAndReplace(WordApplication, "<F_ORGANIZATIONACTION>", "Передал");
                SearchAndReplace(WordApplication, "<CUSTOMERACTION>", "принял");
                SearchAndReplace(WordApplication, "<F_CUSTOMERACTION>", "Принял");
            }
            else
            {
                SearchAndReplace(WordApplication, "<ORGANIZATIONACTION>", "принял");
                SearchAndReplace(WordApplication, "<F_ORGANIZATIONACTION>", "Принял");
                SearchAndReplace(WordApplication, "<CUSTOMERACTION>", "передал");
                SearchAndReplace(WordApplication, "<F_CUSTOMERACTION>", "Передал");
            }

            var docTable = WordDocument.Tables[2];
            var itemCountTable = archiveFolderChanges.Count;
            var rowsTable = 2;
            var i = 1;

            foreach (var archiveFolder in archiveFolderChanges)
            {
                if (archiveFolder.ArchiveFolder is null)
                {
                    continue;
                }

                var archiveFolderName = default(string);

                if (string.IsNullOrWhiteSpace(archiveFolder.PeriodString))
                {
                    archiveFolderName = $"{archiveFolder.ArchiveFolder?.Name} за {archiveFolder.Year}г.";
                }
                else
                {
                    archiveFolderName = $"{archiveFolder.ArchiveFolder?.Name} {archiveFolder.PeriodString.ToLower().Replace("i", "I").Replace("v", "V").Replace(";", ",")}";
                }                

                docTable.Cell(rowsTable, 1).Range.Text = i.ToString();
                docTable.Cell(rowsTable, 2).Range.Text = archiveFolderName;
                docTable.Cell(rowsTable, 3).Range.Text = "1";

                if (itemCountTable > 1)
                {
                    itemCountTable--;
                    docTable.Rows.Add();
                }

                i++;
                rowsTable += 1;
            }
        }
    }
}

