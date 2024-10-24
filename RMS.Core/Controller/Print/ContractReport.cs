using DevExpress.Xpo;
using Microsoft.Office.Interop.Word;
using RMS.Core.Enumerator;
using RMS.Core.Model;
using RMS.Core.Model.InfoContract;
using System;
using System.IO;
using System.Reflection;
using Word = Microsoft.Office.Interop.Word;

namespace RMS.Core.Controller.Print
{
    public class ContractReport
    {
        private Word.Application WordApplication { get; set; }
        private Word.Document WordDocument { get; set; }
        private Contract Contract { get; }
        public string OutFileName { get; set; } = "Договор №";

        private object objMissing = Missing.Value;
        private string tempFile = Path.GetTempFileName();

        public delegate void SaveQuitEventHandler(object sender, string tempFile, byte[] wordDocument, string fileName);
        public event SaveQuitEventHandler SaveQuitEvent;

        public bool WordApplicationClose { get; set; }

        public ContractReport(Contract contract)
        {
            WordApplicationClose = false;
            Contract = contract;
            PrintReportWord(true);
            System.Threading.Tasks.Task.Run(() => GetCloseDocument());
        }

        public ContractReport(byte[] wordDocument, bool isPDF)
        {
            if (wordDocument != null)
            {
                WordApplicationClose = false;
                System.IO.File.WriteAllBytes(tempFile, wordDocument);

                WordApplication = new Word.Application() { Visible = true };
                WordDocument = WordApplication.Documents.Open(tempFile);

                ((Word.ApplicationEvents4_Event)WordApplication).Quit += Events_Quit;

                System.Threading.Tasks.Task.Run(() => GetCloseDocument());
            }
        }

        private void Events_Quit()
        {
            WordApplicationClose = true;
        }

        private void Сleaning(Word.Application wordApplication)
        {
            SearchAndReplace(wordApplication, "^p^p^p^p^p^p^p", string.Empty);
            SearchAndReplace(wordApplication, "^p^p^p^p^p^p", string.Empty);
            SearchAndReplace(wordApplication, "^p^p^p^p^p", string.Empty);
            SearchAndReplace(wordApplication, "^p^p^p^p", string.Empty);
            SearchAndReplace(wordApplication, "^p^p^p", string.Empty);
            SearchAndReplace(wordApplication, "^p^p", string.Empty);
        }

        /// <summary>
        /// Печать отчета (Word).
        /// </summary>
        private void PrintReportWord(bool isVisible)
        {
            var plateTemplate = default(PlateTemplate);

            if (Contract.PlateTemplate != null)
            {
                plateTemplate = Contract.PlateTemplate;
            }
            else
            {
                //plateTemplate = Contract.Session.FindObject<PlateTemplate>(new BinaryOperator(nameof(PlateTemplate.IsDefault), true));
                plateTemplate = Contract.Session.FindObject<PlateTemplate>(null);
            }

            if (plateTemplate == null)
            {
                return;
            }

            System.IO.File.WriteAllBytes(tempFile, plateTemplate.FileWord);

            WordApplication = new Word.Application();
            ((Word.ApplicationEvents4_Event)WordApplication).Quit += Events_Quit;

            WordDocument = WordApplication.Documents.Add(tempFile, ref objMissing, ref objMissing, ref objMissing);

            FillingMainContract(WordApplication, WordDocument, Contract);
            FillingApplication(WordApplication, WordDocument, Contract);

            Сleaning(WordApplication);
            WordApplication.Visible = isVisible;
            WordDocument.SaveAs(tempFile);

            if (Contract.File is null)
            {
                var newTempFile = tempFile.Replace(".tmp", ".docx");
                System.IO.File.Copy(tempFile, newTempFile);
                var byteFile = System.IO.File.ReadAllBytes(newTempFile);

                Contract.File = new Model.File(Contract.Session, newTempFile)
                {
                    FileName = $"{OutFileName} {Contract.Number} от {DateTime.Now.ToShortDateString()}"
                };
                
                Contract.Save();
                System.IO.File.Delete(newTempFile);
            }
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
        /// Заполнение первого листа (одинаково для всех вариантов отчета).
        /// </summary>
        private static void FillingMainContract(Word.Application wordApplication, Word.Document wordDocument, Contract contract)
        {
            var customer = contract.Customer;
            var date = DateTime.Now;

            if (contract.Date != null)
            {
                date = Convert.ToDateTime(contract.Date);
            }

            SearchAndReplace(wordApplication, ReportVariables.TOWN, contract.Town);
            SearchAndReplace(wordApplication, ReportVariables.DAY, date.Day.ToString());
            SearchAndReplace(wordApplication, ReportVariables.MONTH, InfoCard.GetStringGenitiveMonth(date.Month));
            SearchAndReplace(wordApplication, ReportVariables.YEAR, date.Year.ToString());
            SearchAndReplace(wordApplication, ReportVariables.CUSTOMER, customer.ToString() ?? string.Empty);
            SearchAndReplace(wordApplication, ReportVariables.CUSTOMERFULLNAME, customer.FullName.ToString() ?? string.Empty);
            SearchAndReplace(wordApplication, ReportVariables.CUSTOMERABBREVIATEDNAME, customer.AbbreviatedName.ToString() ?? string.Empty);

            var management = $"{customer.ManagementSurname} {customer.ManagementName} {customer.ManagementPatronymic}";
            SearchAndReplace(wordApplication, ReportVariables.CUSTOMERMANAGEMENTFULL, management ?? string.Empty);
            
            SearchAndReplace(wordApplication, ReportVariables.CUSTOMERMANAGEMENT, customer.ManagementString ?? string.Empty);
            SearchAndReplace(wordApplication, ReportVariables.CUSTOMERMANAGEMENTPOSITION, customer.ManagementPositionString ?? string.Empty);
            SearchAndReplace(wordApplication, ReportVariables.TELEPHONE, customer.Telephone ?? string.Empty);
            SearchAndReplace(wordApplication, ReportVariables.Contract, contract?.ToString() ?? string.Empty);
            SearchAndReplace(wordApplication, ReportVariables.INN, customer.INN ?? string.Empty);
            SearchAndReplace(wordApplication, ReportVariables.KPP, customer.KPP ?? string.Empty);
            SearchAndReplace(wordApplication, ReportVariables.OGRN, customer.PSRN ?? string.Empty);
            SearchAndReplace(wordApplication, ReportVariables.ADDRESS, customer.LegalAddress ?? string.Empty);
            
            SearchAndReplace(wordApplication, ReportVariables.ORGANIZATIONNAME, contract.Organization?.Name ?? string.Empty);
            SearchAndReplace(wordApplication, ReportVariables.ORGANIZATIONFULLNAME, contract.Organization?.FullName ?? string.Empty);
            SearchAndReplace(wordApplication, ReportVariables.ORGANIZATIONABBREVIATEDNAME, contract.Organization?.AbbreviatedName ?? string.Empty);
            
            SearchAndReplace(wordApplication, ReportVariables.ORGANIZATIONMANAGMENT, contract.Organization?.ManagementString ?? string.Empty);
            SearchAndReplace(wordApplication, ReportVariables.ORGANIZATIONMANAGMENTPOSITION, contract.Organization?.ManagementPositionString ?? string.Empty);
            SearchAndReplace(wordApplication, ReportVariables.ORGANIZATIOINN, contract.Organization?.INN ?? string.Empty);
            SearchAndReplace(wordApplication, ReportVariables.ORGANIZATIONKPP, contract.Organization?.KPP ?? string.Empty);
            SearchAndReplace(wordApplication, ReportVariables.ORGANIZATIONPSRN, contract.Organization?.PSRN ?? string.Empty);
            SearchAndReplace(wordApplication, ReportVariables.ORGANIZATIONTELEPHONE, contract.Organization?.Telephone ?? string.Empty);

            SearchAndReplace(wordApplication, ReportVariables.DATESINCE, contract.DateSince?.ToShortDateString() ?? string.Empty);
            SearchAndReplace(wordApplication, ReportVariables.DATETO, contract.DateTo?.ToShortDateString() ?? string.Empty);
            
            //SearchAndReplaceHeaderAndFooter(wordDocument, ReportVariables.YEAR, finalProtocol.Date.Year.ToString() ?? string.Empty);
            //SearchAndReplace(wordApplication, ReportVariables.NAMEENTITY, finalProtocol.NameOrg ?? string.Empty);
        }

        private void FillingApplication(Word.Application wordApplication, Word.Document wordDocument, Contract contract)
        {
            if (contract.ContractAttachments != null && contract.ContractAttachments.Count > 0)
            {
                var number = default(int);
                foreach (var attachament in contract.ContractAttachments)
                {
                    number++;
                    wordDocument.Words.Last.InsertBreak(Word.WdBreakType.wdPageBreak);

                    if (attachament.OptionContractAttachment == OptionContractAttachment.ListTransmittedDocumentsAndInformation)
                    {
                        ListDocumentsTransmitted(WordApplication, WordDocument, Contract, attachament, number);
                    }
                    else if (attachament.OptionContractAttachment == OptionContractAttachment.ListRenderedServicesAndAccountingServices)
                    {
                        ListRenderedServices(WordApplication, WordDocument, Contract, attachament, number);
                    }
                }
            }
        }

        private void ListRenderedServices(Application wordApplication, Word.Document wordDocument, Contract contract, ContractAttachment attachament, int number)
        {
            Word.Range tableLocation2 = wordDocument.Words.Last;
            var tableColumns = 3;
            var tableRows = 4;

            var table = wordDocument.Tables.Add(tableLocation2, tableRows, tableColumns);

            var date = DateTime.Now;
            if (contract.Date != null)
            {
                date = Convert.ToDateTime(contract.Date);
            }

            table.Cell(1, tableColumns).Range.Text = $"Приложение №{number}";
            table.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;

            table.Cell(2, tableColumns).Range.Text = $"к договору №{contract.Number} от «{date.Day}» {InfoCard.GetStringGenitiveMonth(date.Month)} {date.Year}г.";
            table.Rows[2].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;

            table.Cell(3, tableColumns).Range.Text = $"Перечень оказываемых услуг и расчет стоимости бухгалтерского обслуживания";
            table.Rows[3].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            table.Rows[3].Range.Font.Bold = 1;

            table.Cell(4, 1).Range.Text = "Сведения/Услуга/работа";
            table.Cell(4, 2).Range.Text = "Показатель";
            table.Cell(4, 3).Range.Text = "Сумма";

            table.Rows[4].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            table.Rows[4].Range.Font.Bold = 1;

            table.Cell(4, 1).Range.Borders[WdBorderType.wdBorderLeft].LineStyle = WdLineStyle.wdLineStyleSingle;
            table.Cell(4, 1).Range.Borders[WdBorderType.wdBorderRight].LineStyle = WdLineStyle.wdLineStyleSingle;
            table.Cell(4, 1).Range.Borders[WdBorderType.wdBorderTop].LineStyle = WdLineStyle.wdLineStyleSingle;
            table.Cell(4, 1).Range.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleSingle;

            table.Cell(4, 2).Range.Borders[WdBorderType.wdBorderLeft].LineStyle = WdLineStyle.wdLineStyleSingle;
            table.Cell(4, 2).Range.Borders[WdBorderType.wdBorderRight].LineStyle = WdLineStyle.wdLineStyleSingle;
            table.Cell(4, 2).Range.Borders[WdBorderType.wdBorderTop].LineStyle = WdLineStyle.wdLineStyleSingle;
            table.Cell(4, 2).Range.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleSingle;

            table.Cell(4, 3).Range.Borders[WdBorderType.wdBorderLeft].LineStyle = WdLineStyle.wdLineStyleSingle;
            table.Cell(4, 3).Range.Borders[WdBorderType.wdBorderRight].LineStyle = WdLineStyle.wdLineStyleSingle;
            table.Cell(4, 3).Range.Borders[WdBorderType.wdBorderTop].LineStyle = WdLineStyle.wdLineStyleSingle;
            table.Cell(4, 3).Range.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleSingle;

            if (attachament.ServiceListContract.Count > 0)
            {
                foreach (var serviceList in attachament.ServiceListContract)
                {
                    table.Rows.Add();
                    tableRows++;

                    table.Cell(tableRows, 1).Range.Text = serviceList.TypeServiceList;
                    table.Cell(tableRows, 2).Range.Text = serviceList.Mark;
                    table.Cell(tableRows, 3).Range.Text = serviceList.Value;
                    table.Rows[tableRows].Range.Font.Bold = 0;

                    table.Cell(tableRows, 1).Range.Borders[WdBorderType.wdBorderLeft].LineStyle = WdLineStyle.wdLineStyleSingle;
                    table.Cell(tableRows, 2).Range.Borders[WdBorderType.wdBorderLeft].LineStyle = WdLineStyle.wdLineStyleSingle;
                    table.Cell(tableRows, 3).Range.Borders[WdBorderType.wdBorderLeft].LineStyle = WdLineStyle.wdLineStyleSingle;

                    table.Cell(tableRows, 1).Range.Borders[WdBorderType.wdBorderRight].LineStyle = WdLineStyle.wdLineStyleSingle;
                    table.Cell(tableRows, 2).Range.Borders[WdBorderType.wdBorderRight].LineStyle = WdLineStyle.wdLineStyleSingle;
                    table.Cell(tableRows, 3).Range.Borders[WdBorderType.wdBorderRight].LineStyle = WdLineStyle.wdLineStyleSingle;

                    table.Cell(tableRows, 1).Range.Borders[WdBorderType.wdBorderTop].LineStyle = WdLineStyle.wdLineStyleSingle;
                    table.Cell(tableRows, 2).Range.Borders[WdBorderType.wdBorderTop].LineStyle = WdLineStyle.wdLineStyleSingle;
                    table.Cell(tableRows, 3).Range.Borders[WdBorderType.wdBorderTop].LineStyle = WdLineStyle.wdLineStyleSingle;

                    table.Cell(tableRows, 1).Range.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleSingle;
                    table.Cell(tableRows, 2).Range.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleSingle;
                    table.Cell(tableRows, 3).Range.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleSingle;
                }
            }

            table.Cell(1, 1).Merge(table.Cell(1, tableColumns));
            table.Cell(2, 1).Merge(table.Cell(2, tableColumns));
            table.Cell(3, 1).Merge(table.Cell(3, tableColumns));

            //tableServiceList.Cell(tableRows, 1).Range.Text = $"Сведения/Услуга/работа";
            //tableServiceList.Rows[tableRows].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            //tableServiceList.Cell(tableRows, 2).Range.Text = $"Показатель";
            //tableServiceList.Rows[tableRows].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            //tableServiceList.Cell(tableRows, 3).Range.Text = $"Сумма";
            //tableServiceList.Rows[tableRows].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            //tableServiceList.Rows[tableRows].Range.Font.Bold = 1;

            //if (attachament.ServiceListContract.Count > 0)
            //{
            //    foreach (var serviceList in attachament.ServiceListContract)
            //    {
            //        tableServiceList.Rows.Add();
            //        tableRows++;

            //        tableServiceList.Cell(tableRows, 1).Range.Text = serviceList.TypeServiceList;
            //        tableServiceList.Cell(tableRows, 2).Range.Text = serviceList.Mark;
            //        tableServiceList.Cell(tableRows, 3).Range.Text = serviceList.Value;
            //        tableServiceList.Rows[tableRows].Range.Font.Bold = 0;
            //    }               
            //}

            //tableServiceList.Cell(tableRows, tableColumns).Range.Borders[WdBorderType.wdBorderLeft].LineStyle = WdLineStyle.wdLineStyleSingle;
            //tableServiceList.Cell(tableRows, tableColumns).Range.Borders[WdBorderType.wdBorderRight].LineStyle = WdLineStyle.wdLineStyleSingle;
            //tableServiceList.Cell(tableRows, tableColumns).Range.Borders[WdBorderType.wdBorderTop].LineStyle = WdLineStyle.wdLineStyleSingle;
            //tableServiceList.Cell(tableRows, tableColumns).Range.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleSingle;
        }

        /// <summary>
        /// Приложение (Перечень передаваемых документов).
        /// </summary>
        /// <param name="wordApplication"></param>
        /// <param name="wordDocument"></param>
        /// <param name="contract"></param>
        /// <param name="number"></param>
        private void ListDocumentsTransmitted(Word.Application wordApplication, Word.Document wordDocument, Contract contract, ContractAttachment contractAttachment, int number)
        {
            Word.Range tableLocation2 = wordDocument.Words.Last;
            var tableColumns = 1;
            var tableRows = 3;

            var table = wordDocument.Tables.Add(tableLocation2, tableRows, tableColumns);

            var date = DateTime.Now;
            if (contract.Date != null)
            {
                date = Convert.ToDateTime(contract.Date);
            }

            table.Cell(1, tableColumns).Range.Text = $"Приложение №{number}";
            table.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;

            table.Cell(2, tableColumns).Range.Text = $"к договору №{contract.Number} от «{date.Day}» {InfoCard.GetStringGenitiveMonth(date.Month)} {date.Year}г.";
            table.Rows[2].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;

            table.Cell(3, tableColumns).Range.Text = $"Перечень передаваемых документов и сведений";
            table.Rows[3].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            table.Rows[3].Range.Font.Bold = 1;

            if (contractAttachment.StatutoryDocumentsContract.Count > 0)
            {
                table.Rows.Add();
                tableRows++;

                table.Cell(tableRows, tableColumns).Range.Text = $"Уставные документы (передаются ксерокопии): ";
                table.Rows[tableRows].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                table.Rows[tableRows].Range.Font.Bold = 0;

                table.Rows[tableRows].Range.Paragraphs[1].Range.Font.Bold = 1;
                table.Rows[tableRows].Range.Paragraphs[1].Range.Font.Underline = Word.WdUnderline.wdUnderlineSingle;

                FillingTable(contractAttachment.StatutoryDocumentsContract, tableColumns, tableRows, table);

                table.Rows[tableRows].Range.Paragraphs[2].Range.Font.Bold = 0;
                table.Rows[tableRows].Range.Paragraphs[2].Range.Font.Underline = Word.WdUnderline.wdUnderlineNone;
            }
            if (contractAttachment.TitleDocumentsContract.Count > 0)
            {
                table.Rows.Add();
                tableRows++;

                table.Cell(tableRows, tableColumns).Range.Text = $"Правоустанавливающие документы (передаются ксерокопии): ";
                table.Rows[tableRows].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                table.Rows[tableRows].Range.Font.Bold = 0;

                table.Rows[tableRows].Range.Paragraphs[1].Range.Font.Bold = 1;
                table.Rows[tableRows].Range.Paragraphs[1].Range.Font.Underline = Word.WdUnderline.wdUnderlineSingle;

                FillingTable(contractAttachment.TitleDocumentsContract, tableColumns, tableRows, table);

                table.Rows[tableRows].Range.Paragraphs[2].Range.Font.Bold = 0;
                table.Rows[tableRows].Range.Paragraphs[2].Range.Font.Underline = Word.WdUnderline.wdUnderlineNone;
            }
            if (contractAttachment.TaxReportingDocumentsContract.Count > 0)
            {
                table.Rows.Add();
                tableRows++;

                table.Cell(tableRows, tableColumns).Range.Text = $"Налоговая отчетность (подлинники за последний год): ";
                table.Rows[tableRows].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                table.Rows[tableRows].Range.Font.Bold = 0;

                table.Rows[tableRows].Range.Paragraphs[1].Range.Font.Bold = 1;
                table.Rows[tableRows].Range.Paragraphs[1].Range.Font.Underline = Word.WdUnderline.wdUnderlineSingle;

                FillingTable(contractAttachment.TaxReportingDocumentsContract, tableColumns, tableRows, table);

                table.Rows[tableRows].Range.Paragraphs[2].Range.Font.Bold = 0;
                table.Rows[tableRows].Range.Paragraphs[2].Range.Font.Underline = Word.WdUnderline.wdUnderlineNone;
            }
            if (contractAttachment.SourceDocumentsContract.Count > 0)
            {
                table.Rows.Add();
                tableRows++;

                table.Cell(tableRows, tableColumns).Range.Text = $"Первичные документы (подлинники, если будут храниться в БК Альграс): ";
                table.Rows[tableRows].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                table.Rows[tableRows].Range.Font.Bold = 0;

                table.Rows[tableRows].Range.Paragraphs[1].Range.Font.Bold = 1;
                table.Rows[tableRows].Range.Paragraphs[1].Range.Font.Underline = Word.WdUnderline.wdUnderlineSingle;

                FillingTable(contractAttachment.SourceDocumentsContract, tableColumns, tableRows, table);

                table.Rows[tableRows].Range.Paragraphs[2].Range.Font.Bold = 0;
                table.Rows[tableRows].Range.Paragraphs[2].Range.Font.Underline = Word.WdUnderline.wdUnderlineNone;
            }
            if (contractAttachment.EmployeeDetailsDocumentsContract.Count > 0)
            {
                table.Rows.Add();
                tableRows++;

                table.Cell(tableRows, tableColumns).Range.Text = $"Анкетные данные сотрудников: ";
                table.Rows[tableRows].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                table.Rows[tableRows].Range.Font.Bold = 0;

                table.Rows[tableRows].Range.Paragraphs[1].Range.Font.Bold = 1;
                table.Rows[tableRows].Range.Paragraphs[1].Range.Font.Underline = Word.WdUnderline.wdUnderlineSingle;

                FillingTable(contractAttachment.EmployeeDetailsDocumentsContract, tableColumns, tableRows, table);

                table.Rows[tableRows].Range.Paragraphs[2].Range.Font.Bold = 0;
                table.Rows[tableRows].Range.Paragraphs[2].Range.Font.Underline = Word.WdUnderline.wdUnderlineNone;
            }
        }

        private static void FillingTable<T>(XPCollection<T> xPcollection, int tableColumns, int tableRows, Table table)
        {
            var result = default(string);
            var count = xPcollection.Count;

            foreach (var document in xPcollection)
            {
                count--;

                if (count == 0)
                {
                    result += $"{document}";
                    break;
                }

                result += $"{document}, ";
            }

            table.Cell(tableRows, tableColumns).Range.Text += result;

            table.Cell(tableRows, tableColumns).Range.Borders[WdBorderType.wdBorderLeft].LineStyle = WdLineStyle.wdLineStyleSingle;
            table.Cell(tableRows, tableColumns).Range.Borders[WdBorderType.wdBorderRight].LineStyle = WdLineStyle.wdLineStyleSingle;
            table.Cell(tableRows, tableColumns).Range.Borders[WdBorderType.wdBorderTop].LineStyle = WdLineStyle.wdLineStyleSingle;
            table.Cell(tableRows, tableColumns).Range.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleSingle;
        }
    }
}
