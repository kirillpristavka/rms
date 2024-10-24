using DevExpress.XtraPrinting;
using DevExpress.XtraRichEdit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using Word = Microsoft.Office.Interop.Word;

namespace RMS.Core.Controller.Print
{
    /// <summary>
    /// Печатная форма коммерческого предложения.
    /// </summary>
    public class CommercialOffer
    {
        private Word.Application WordApplication { get; set; }
        private Word.Document WordDocument { get; set; }
        public string OutFileName { get; set; } = "Коммерческое предложение";

        private object objMissing = Missing.Value;
        private string tempFile = Path.GetTempFileName();

        public string PathFile { get; set; }

        public delegate void SaveQuitEventHandler(object sender, string tempFile, byte[] wordDocument, string fileName);
        public event SaveQuitEventHandler SaveQuitEvent;

        public bool WordApplicationClose { get; set; }

        private CommercialOfferObject commercialOfferObject;

        public CommercialOffer(CommercialOfferObject commercialOfferObject, bool isOpen = true)
        {
            this.commercialOfferObject = commercialOfferObject;
            
            WordApplicationClose = false;
            PrintReportWord(false);
            OpenToPDF(tempFile, isOpen);

            System.Threading.Tasks.Task.Run(() => GetCloseDocument());
        }

        private void OpenToPDF(string pathFile, bool isOpen)
        {
            RichEditDocumentServer server = new RichEditDocumentServer();
            server.LoadDocument(pathFile, DevExpress.XtraRichEdit.DocumentFormat.OpenXml);

            PdfExportOptions options = new PdfExportOptions();
            options.DocumentOptions.Author = "ООО Пульс Групп";
            options.Compressed = false;
            options.ImageQuality = PdfJpegImageQuality.Highest;

            pathFile = pathFile.Replace("tmp", "pdf");
            PathFile = pathFile;
            using (FileStream pdfFileStream = new FileStream(pathFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                server.ExportToPdf(pdfFileStream, options);
            }

            if (System.IO.File.Exists(pathFile) && isOpen)
            {
                Process.Start(pathFile);
            }
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
            var templateName = "CommercialOffer.docx";
            var pathToTemplate = $"Template\\{templateName}";

            if (!File.Exists(pathToTemplate))
            {
                return;
            }

            File.Copy(pathToTemplate, tempFile, true);
            WordApplication = new Word.Application();
            ((Word.ApplicationEvents4_Event)WordApplication).Quit += Events_Quit;

            WordDocument = WordApplication.Documents.Add(tempFile, ref objMissing, ref objMissing, ref objMissing);

            FillFirstSheet(WordApplication, WordDocument);

            OutFileName = $"Коммерческое предложение от {DateTime.Now.ToString("dd.MM.yyyy_(HH.mm.ss)")}";
            WordApplication.Visible = isVisible;
            WordDocument.SaveAs(tempFile);
        }

        private void FillFirstSheet(Word.Application wordApplication, Word.Document wordDocument)
        {
            var dateTimeNow = DateTime.Now;
            var date = $"{dateTimeNow.Day} {GetStringGenitiveMonth(dateTimeNow.Month)} {dateTimeNow.Year}";
            SearchAndReplace(wordApplication, ReportVariables.DATETIME, date);
            SearchAndReplace(wordApplication, ReportVariables.INVOICESUM, commercialOfferObject.Sum);

            var docTable = wordDocument.Tables[1];
            var i = 1;

            if (commercialOfferObject.TermsObj.Count > 0)
            {
                var count = commercialOfferObject.TermsObj.Count;
                foreach (var obj in commercialOfferObject.TermsObj)
                {
                    var row = docTable.Rows[i + 1];
                    if (count > 1)
                    {
                        count--;
                        docTable.Rows.Add(row);
                    }
                    
                    docTable.Cell(i + 1, 1).Range.Text = obj.Name ?? string.Empty;
                    docTable.Cell(i + 1, 2).Range.Text = $"{obj.Price} ({obj.Count} шт.)" ?? string.Empty;
                    docTable.Cell(i + 1, 3).Range.Text = $"{obj.Sum} (руб.)" ?? string.Empty;
                    i++;
                }
            }
            else
            {
                var count = commercialOfferObject.Terms.Count;
                foreach (var info in commercialOfferObject.Terms)
                {
                    var row = docTable.Rows[i + 1];
                    if (count > 1)
                    {
                        count--;
                        docTable.Rows.Add(row);
                    }
                    
                    docTable.Cell(i + 1, 1).Range.Text = info ?? string.Empty;
                    i++;
                }
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
                            SaveQuitEvent?.Invoke(this, tempFile, File.ReadAllBytes(tempFile), OutFileName);
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
    
    public class CommercialOfferObject
    {
        public CommercialOfferObject(string sum, string document, string tax)
        {
            Sum = sum;
            Document = document;
            Tax = tax;
        }
        
        public void AddTerms(string obj)
        {
            if (!string.IsNullOrWhiteSpace(obj))
            {
                var result = obj
                    .Replace("(+10%)", "")
                    .Replace("(+15%)", "")
                    .Replace("(+20%)", "")
                    .Replace("(+25%)", "")
                    .Replace("(+30%)", "")
                    .Trim();

                Terms.Add(result);
            }
        }

        public void AddTerms(string name, string count, string price, string sum)
        {
            TermsObj.Add(new TermsObj(name, count, price, sum));
        }

        public string Sum { get; set; }
        public string Document { get; set; }
        public string Tax { get; set; }

        public List<string> Terms { get; set; } = new List<string>();

        public List<TermsObj> TermsObj { get; set; } = new List<TermsObj>();
        
    }

    public class TermsObj
    {
        public TermsObj(string name, string count, string price, string sum)
        {
            Name = name;
            Count = count;
            Price = price;
            Sum = sum;
        }

        public string Name { get; set; }
        public string Count { get; set; }
        public string Price { get; set; }
        public string Sum { get; set; }
    }
}