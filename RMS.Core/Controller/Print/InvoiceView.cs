using DevExpress.XtraPrinting;
using DevExpress.XtraRichEdit;
using RMS.Core.Model;
using RMS.Core.Model.InfoCustomer;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Word = Microsoft.Office.Interop.Word;

namespace RMS.Core.Controller.Print
{
    /// <summary>
    /// Печатная форма счета.
    /// </summary>
    public class InvoiceView
    {
        private Word.Application WordApplication { get; set; }
        private Word.Document WordDocument { get; set; }
        private Customer Customer { get; }
        private Invoice Invoice { get; }
        public string OutFileName { get; set; } = "Счет";

        private object objMissing = Missing.Value;
        private string tempFile = Path.GetTempFileName();

        public string PathFile { get; set; }

        public delegate void SaveQuitEventHandler(object sender, string tempFile, byte[] wordDocument, string fileName);
        public event SaveQuitEventHandler SaveQuitEvent;

        public bool WordApplicationClose { get; set; }

        public InvoiceView(Invoice invoice, bool isOpen = true)
        {
            if (invoice is null)
            {
                throw new ArgumentNullException(nameof(invoice), "Пустой объект [Клиент]");
            }
            
            Invoice = invoice;
            Customer = invoice.Customer;

            WordApplicationClose = false;
            //PrintReportWord(true);

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
            var templateName = "Score.docx";
            var pathToTemplate = $"Template\\{templateName}";

            if (!System.IO.File.Exists(pathToTemplate))
            {
                return;
            }

            System.IO.File.Copy(pathToTemplate, tempFile, true);
            WordApplication = new Word.Application();
            ((Word.ApplicationEvents4_Event)WordApplication).Quit += Events_Quit;

            WordDocument = WordApplication.Documents.Add(tempFile, ref objMissing, ref objMissing, ref objMissing);

            FillFirstSheet(WordApplication, WordDocument, Invoice);

            OutFileName = $"Счет № {Invoice.Number} от {Invoice.Date.ToShortDateString()}";
            WordApplication.Visible = isVisible;
            WordDocument.SaveAs(tempFile);
        }

        private static void FillFirstSheet(Word.Application wordApplication, Word.Document wordDocument, Invoice invoice)
        {
            SearchAndReplace(wordApplication, ReportVariables.INVOICENUMBER, invoice.Number);

            var date = $"{invoice.Date.Day} {GetStringGenitiveMonth(invoice.Date.Month)} {invoice.Date.Year}";
            SearchAndReplace(wordApplication, ReportVariables.DATETIME, date);
            SearchAndReplace(wordApplication, ReportVariables.CUSTOMER, invoice.Customer.FullName ?? string.Empty);
            SearchAndReplace(wordApplication, ReportVariables.CUSTOMERADDRESS, invoice.Customer.LegalAddress ?? string.Empty);
            SearchAndReplace(wordApplication, ReportVariables.Contract, invoice.Customer.Contracts.FirstOrDefault()?.ToString() ?? string.Empty);
            SearchAndReplace(wordApplication, ReportVariables.INVOICESUM, invoice.Value.ToString());
            
            var invoiceInformationCount = invoice.InvoiceInformations.Count;
            SearchAndReplace(wordApplication, "<COUNT>", invoiceInformationCount.ToString());

            var docTable = wordDocument.Tables[3];
            var i = 1;
            
            foreach (var info in invoice.InvoiceInformations)
            {
                var row = docTable.Rows[i + 1];
                if (invoiceInformationCount > 1)
                {
                    invoiceInformationCount--;
                    docTable.Rows.Add(row);
                }
                
                docTable.Cell(i + 1, 1).Range.Text = i.ToString();
                docTable.Cell(i + 1, 2).Range.Text = info.Description ?? string.Empty;                

                if (info.Unit.Equals("%"))
                {
                    docTable.Cell(i + 1, 3).Range.Text = string.Empty; 
                    docTable.Cell(i + 1, 4).Range.Text = string.Empty;
                    docTable.Cell(i + 1, 5).Range.Text = $"{info.Price}%";
                }
                else
                {
                    docTable.Cell(i + 1, 3).Range.Text = info.Count.ToString() ?? string.Empty;
                    docTable.Cell(i + 1, 4).Range.Text = info.Unit ?? string.Empty;
                    docTable.Cell(i + 1, 5).Range.Text = info.Price.ToString() ?? string.Empty;
                }

                docTable.Cell(i + 1, 6).Range.Text = info.Sum.ToString() ?? string.Empty;

                i++;
            }

            var invoiceString = $"{RusNumber.Str(Convert.ToInt32(invoice.Value)).Trim()} рубля 00 копеек";
            SearchAndReplace(wordApplication, ReportVariables.INVOICESUMSTRING, invoiceString);

            SearchAndReplace(wordApplication, ReportVariables.INVOICEDATETO, invoice.DeadlineDate.ToShortDateString());
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

    public class RusNumber
    {
        //Наименования сотен
        private static string[] hunds =
        {
            "", "сто ", "двести ", "триста ", "четыреста ",
            "пятьсот ", "шестьсот ", "семьсот ", "восемьсот ", "девятьсот "
        };
        //Наименования десятков
        private static string[] tens =
        {
            "", "десять ", "двадцать ", "тридцать ", "сорок ", "пятьдесят ",
            "шестьдесят ", "семьдесят ", "восемьдесят ", "девяносто "
        };
        /// <summary>
        /// Перевод в строку числа с учётом падежного окончания относящегося к числу существительного
        /// </summary>
        /// <param name="val">Число</param>
        /// <param name="male">Род существительного, которое относится к числу</param>
        /// <param name="one">Форма существительного в единственном числе</param>
        /// <param name="two">Форма существительного от двух до четырёх</param>
        /// <param name="five">Форма существительного от пяти и больше</param>
        /// <returns></returns>
        public static string Str(int val, bool male, string one, string two, string five)
        {
            string[] frac20 =
            {
                "", "один ", "два ", "три ", "четыре ", "пять ", "шесть ",
                "семь ", "восемь ", "девять ", "десять ", "одиннадцать ",
                "двенадцать ", "тринадцать ", "четырнадцать ", "пятнадцать ",
                "шестнадцать ", "семнадцать ", "восемнадцать ", "девятнадцать "
            };

            int num = val % 1000;
            if (0 == num) return "";
            if (num < 0) throw new ArgumentOutOfRangeException("val", "Параметр не может быть отрицательным");
            if (!male)
            {
                frac20[1] = "одна ";
                frac20[2] = "две ";
            }

            StringBuilder r = new StringBuilder(hunds[num / 100]);

            if (num % 100 < 20)
            {
                r.Append(frac20[num % 100]);
            }
            else
            {
                r.Append(tens[num % 100 / 10]);
                r.Append(frac20[num % 10]);
            }

            r.Append(Case(num, one, two, five));

            if (r.Length != 0) r.Append(" ");
            return r.ToString();
        }
        /// <summary>
        /// Выбор правильного падежного окончания сущесвительного
        /// </summary>
        /// <param name="val">Число</param>
        /// <param name="one">Форма существительного в единственном числе</param>
        /// <param name="two">Форма существительного от двух до четырёх</param>
        /// <param name="five">Форма существительного от пяти и больше</param>
        /// <returns>Возвращает существительное с падежным окончанием, которое соответсвует числу</returns>
        public static string Case(int val, string one, string two, string five)
        {
            int t = (val % 100 > 20) ? val % 10 : val % 20;

            switch (t)
            {
                case 1: return one;
                case 2: case 3: case 4: return two;
                default: return five;
            }
        }
        /// <summary>
        /// Перевод целого числа в строку
        /// </summary>
        /// <param name="val">Число</param>
        /// <returns>Возвращает строковую запись числа</returns>
        public static string Str(int val)
        {
            bool minus = false;
            if (val < 0) { val = -val; minus = true; }

            int n = (int)val;

            StringBuilder r = new StringBuilder();

            if (0 == n) r.Append("0 ");
            if (n % 1000 != 0)
                r.Append(RusNumber.Str(n, true, "", "", ""));

            n /= 1000;

            r.Insert(0, RusNumber.Str(n, false, "тысяча", "тысячи", "тысяч"));
            n /= 1000;

            r.Insert(0, RusNumber.Str(n, true, "миллион", "миллиона", "миллионов"));
            n /= 1000;

            r.Insert(0, RusNumber.Str(n, true, "миллиард", "миллиарда", "миллиардов"));
            n /= 1000;

            r.Insert(0, RusNumber.Str(n, true, "триллион", "триллиона", "триллионов"));
            n /= 1000;

            r.Insert(0, RusNumber.Str(n, true, "триллиард", "триллиарда", "триллиардов"));
            if (minus) r.Insert(0, "минус ");

            //Делаем первую букву заглавной
            r[0] = char.ToUpper(r[0]);

            return r.ToString();
        }
    }
}

