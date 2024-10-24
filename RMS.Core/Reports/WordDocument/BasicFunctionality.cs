using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;

namespace RMS.Core.Reports.WordDocument
{
    public static class BasicFunctionality
    {
        public static string RenameOrCopyTmpToDocx(string tmpFilePath)
        {
            if (string.IsNullOrWhiteSpace(tmpFilePath))
            {
                throw new ArgumentException("The path to the .tmp file cannot be null or empty.", nameof(tmpFilePath));
            }

            if (!File.Exists(tmpFilePath))
            {
                throw new FileNotFoundException("The specified .tmp file does not exist.", tmpFilePath);
            }

            string docxFilePath = Path.ChangeExtension(tmpFilePath, ".docx");

            try
            {
                File.Move(tmpFilePath, docxFilePath);
                return docxFilePath;
            }
            catch (IOException)
            {
                try
                {
                    File.Copy(tmpFilePath, docxFilePath, overwrite: true);
                    return docxFilePath;
                }
                catch (Exception) { }
            }
            catch (Exception) { }

            return tmpFilePath;
        }

        /// <summary>
        /// Поиск текста и его замена на текст.
        /// </summary>
        public static void FindingTextAndReplacingItWithText(WordprocessingDocument wordprocessingDocument, string findStr, string replaceStr)
        {
            var body = wordprocessingDocument.MainDocumentPart.Document.Body;

            bool isReplace = false;
            foreach (var text in body.Descendants<Text>())
            {
                if (text.Text.Contains(findStr))
                {
                    if (replaceStr.Equals("-", StringComparison.OrdinalIgnoreCase))
                    {
                        if (text.Parent is Run run)
                        {
                            run.RunProperties?.Underline?.Remove();
                        }
                    }

                    text.Text = text.Text.Replace(findStr, replaceStr);
                }
            }

            if (isReplace is false)
            {
                var paragraphs = body.Elements<Paragraph>();
                foreach (var p in paragraphs)
                {
                    ReplaceText(p, findStr, replaceStr);
                }
            }
        }



        public static SdtRun CreateListYesNo(string sdtAlias, string tag)
        {
            var dictionary = new Dictionary<string, string>
            {
                { "1", "ДА" },
                { "2", "НЕТ" }
            };

            return CreateSdtContentDropDownList(sdtAlias, tag, dictionary);
        }

        public static SdtRun CreateSdtContentDropDownList(
            string sdtAlias, string tag, Dictionary<string, string> dictionaries,
            string defaultText = "Выберите значение")
        {
            SdtRun sdtRun = new SdtRun();

            SdtProperties sdtProperties = new SdtProperties(
                new SdtAlias { Val = sdtAlias },
                new Tag { Val = tag },
                new SdtContentDropDownList()
            );

            SdtContentDropDownList dropDownList = new SdtContentDropDownList();
            foreach (var dictionary in dictionaries)
            {
                dropDownList.AppendChild(new ListItem { DisplayText = dictionary.Value, Value = dictionary.Key });
            }
            sdtProperties.Append(dropDownList);

            SdtContentRun sdtContentRun = new SdtContentRun();
            sdtContentRun.AppendChild(new Text(defaultText));

            sdtRun.Append(sdtProperties);
            sdtRun.Append(sdtContentRun);

            return sdtRun;
        }

        /// <summary>
        /// Поиск текста и его замена на список.
        /// </summary>
        public static void FindingTextAndReplacingItWithListYesNo(WordprocessingDocument wordprocessingDocument,
            string findStr, string sdtAlias)
        {
            var mainPart = wordprocessingDocument.MainDocumentPart;
            var body = mainPart.Document.Body;

            var paragraph = body.Elements<Paragraph>()
                                .FirstOrDefault(p => p.InnerText.Contains(findStr));

            if (paragraph != null)
            {
                var run = paragraph.Elements<Run>()
                                   .FirstOrDefault(r => r.InnerText.Contains(findStr));

                if (run != null)
                {
                    run.Append(CreateListYesNo(sdtAlias, findStr));
                    FindingTextAndReplacingItWithText(wordprocessingDocument, findStr, " ");
                }
            }
        }

        /// <summary>
        /// Поиск текста и его замена на список.
        /// </summary>
        public static void FindingTextAndReplacingItWithList(WordprocessingDocument wordprocessingDocument, string findStr, SdtRun sdtRun)
        {
            var mainPart = wordprocessingDocument.MainDocumentPart;
            var body = mainPart.Document.Body;

            var paragraph = body.Elements<Paragraph>()
                                .FirstOrDefault(p => p.InnerText.Contains(findStr));

            if (paragraph != null)
            {
                var run = paragraph.Elements<Run>()
                                   .FirstOrDefault(r => r.InnerText.Contains(findStr));

                if (run != null)
                {
                    run.Append(sdtRun);
                    FindingTextAndReplacingItWithText(wordprocessingDocument, findStr, " ");
                }
            }
        }

        /// <summary>
        /// Поиск и замена в колонтитулах.
        /// </summary>
        /// <param name="wordApplication">Объект работы с докуентом Word.</param>
        /// <param name="findStr">Искомая строка.</param>
        /// <param name="replaceStr">Строка для замены.</param>
        public static void SearchAndReplaceHeaderAndFooter(WordprocessingDocument wordApplication, string findStr, string replaceStr)
        {
            foreach (var headerPart in wordApplication.MainDocumentPart.HeaderParts)
            {
                ReplaceTextInHeaderFooter(headerPart, findStr, replaceStr);
            }

            foreach (var footerPart in wordApplication.MainDocumentPart.FooterParts)
            {
                ReplaceTextInHeaderFooter(footerPart, findStr, replaceStr);
            }

            // Обрабатываем верхние и нижние колонтитулы в секциях
            foreach (var section in wordApplication.MainDocumentPart.Document.Body.Elements<SectionProperties>())
            {
                ReplaceTextInSectionHeadersAndFooters(wordApplication, section, findStr, replaceStr);
            }
        }

        /// <summary>
        /// Замена текста в колонтитуле (верхнем или нижнем).
        /// </summary>
        /// <param name="headerFooterPart">Часть колонтитула (верхнего или нижнего).</param>
        /// <param name="findStr">Искомая строка.</param>
        /// <param name="replaceStr">Строка для замены.</param>
        public static void ReplaceTextInHeaderFooter(OpenXmlPart headerFooterPart, string findStr, string replaceStr)
        {
            bool isReplace = false;
            foreach (var text in headerFooterPart.RootElement.Descendants<Text>())
            {
                if (text.Text.Contains(findStr))
                {
                    text.Text = text.Text.Replace(findStr, replaceStr);
                }
            }

            if (isReplace is false)
            {
                var paragraphs = headerFooterPart.RootElement.Elements<Paragraph>();
                foreach (var p in paragraphs)
                {
                    ReplaceText(p, findStr, replaceStr);
                }
            }
        }

        /// <summary>
        /// Замена текста в верхних и нижних колонтитулах секции.
        /// </summary>
        /// <param name="wordDoc">Открытый документ WordprocessingDocument.</param>
        /// <param name="section">Свойства раздела SectionProperties.</param>
        /// <param name="findStr">Искомая строка.</param>
        /// <param name="repStr">Строка для замены.</param>
        public static void ReplaceTextInSectionHeadersAndFooters(WordprocessingDocument wordDoc, SectionProperties section, string findStr, string repStr)
        {
            // Обработка нижних колонтитулов
            var footerReferences = section.Elements<FooterReference>();
            foreach (var footerReference in footerReferences)
            {
                var footerPart = (FooterPart)wordDoc.MainDocumentPart.GetPartById(footerReference.Id);
                ReplaceTextInHeaderFooter(footerPart, findStr, repStr);
            }

            // Обработка верхних колонтитулов
            var headerReferences = section.Elements<HeaderReference>();
            foreach (var headerReference in headerReferences)
            {
                var headerPart = (HeaderPart)wordDoc.MainDocumentPart.GetPartById(headerReference.Id);
                ReplaceTextInHeaderFooter(headerPart, findStr, repStr);
            }
        }

        public static void SetCellFormattedText(TableCell cell, string text, string fontName = "Times New Roman",
                                                double fontSize = 9.5, bool isBold = false,
                                                JustificationValues justification = default,
                                                TableVerticalAlignmentValues verticalAlignment = default)
        {
            cell.RemoveAllChildren();

            var paragraph = new Paragraph();

            var justificationElement = new Justification { Val = justification };
            var paragraphProperties = new ParagraphProperties();
            paragraphProperties.Justification = justificationElement;
            paragraph.ParagraphProperties = paragraphProperties;

            var tableCellProperties = new TableCellProperties();
            tableCellProperties.Append(new TableCellVerticalAlignment { Val = verticalAlignment });

            var run = new Run();

            string[] newLineArray = { Environment.NewLine };
            var textArray = text.Split(newLineArray, StringSplitOptions.None);
            bool first = true;
            if (textArray.Length > 1)
            {
                foreach (string line in textArray)
                {
                    if (!first)
                    {
                        run.Append(new Break());
                    }

                    first = false;

                    var textElement = new Text(line);
                    run.Append(textElement);
                }
            }
            else
            {
                var textElement = new Text(text);
                run.Append(textElement);
            }

            var runProperties = new RunProperties();

            if (!string.IsNullOrEmpty(fontName))
            {
                runProperties.Append(new RunFonts { Ascii = fontName });
            }

            if (fontSize > 0)
            {
                runProperties.Append(new FontSize { Val = (fontSize * 2).ToString() });
            }

            if (isBold)
            {
                runProperties.Append(new Bold());
            }

            run.RunProperties = runProperties;
            paragraph.Append(run);
            cell.Append(tableCellProperties);
            cell.Append(paragraph);
        }

        public static TableCell CreateTableCell(string text)
        {
            return new TableCell(new Paragraph(new Run(new Text(text))));
        }

        /// <summary>
        /// Find/replace within the specified paragraph.
        /// </summary>
        /// <param name="paragraph"></param>
        /// <param name="findStr"></param>
        /// <param name="replaceStr"></param>
        public static void ReplaceText(Paragraph paragraph, string findStr, string replaceStr)
        {
            var texts = paragraph.Descendants<Text>();
            for (int t = 0; t < texts.Count(); t++)
            {
                var text = texts.ElementAt(t);
                for (int c = 0; c < text.Text.Length; c++)
                {
                    var match = IsMatch(texts, t, c, findStr);
                    if (match != null)
                    {
                        string[] lines = replaceStr.Replace(Environment.NewLine, "\r").Split('\n', '\r');
                        int skip = lines[lines.Length - 1].Length - 1;

                        if (c > 0)
                        {
                            lines[0] = text.Text.Substring(0, c) + lines[0];
                        }

                        if (match.EndCharIndex + 1 < texts.ElementAt(match.EndElementIndex).Text.Length)
                        {
                            lines[lines.Length - 1] = lines[lines.Length - 1] + texts.ElementAt(match.EndElementIndex).Text.Substring(match.EndCharIndex + 1);
                        }

                        text.Space = new EnumValue<SpaceProcessingModeValues>(SpaceProcessingModeValues.Preserve);
                        text.Text = lines[0];

                        if (replaceStr.Equals("-", StringComparison.OrdinalIgnoreCase))
                        {
                            if (text.Parent is Run run)
                            {
                                run.RunProperties?.Underline?.Remove();
                            }
                        }

                        for (int i = t + 1; i <= match.EndElementIndex; i++)
                        {
                            texts.ElementAt(i).Text = string.Empty;
                        }

                        if (lines.Count() > 1)
                        {
                            OpenXmlElement currEl = text;
                            Break br;

                            var run = text.Parent as Run;
                            for (int i = 1; i < lines.Count(); i++)
                            {
                                br = new Break();
                                run.InsertAfter<Break>(br, currEl);
                                currEl = br;
                                text = new Text(lines[i]);
                                run.InsertAfter<Text>(text, currEl);
                                t++;
                                currEl = text;
                            }
                            c = skip;
                        }
                        else
                        {
                            c += skip;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Determine if the texts (starting at element t, char c) exactly contain the find text
        /// </summary>
        /// <param name="texts"></param>
        /// <param name="t"></param>
        /// <param name="c"></param>
        /// <param name="find"></param>
        /// <returns>null or the result info</returns>
        static Match IsMatch(IEnumerable<Text> texts, int t, int c, string find)
        {
            int ix = 0;
            for (int i = t; i < texts.Count(); i++)
            {
                for (int j = c; j < texts.ElementAt(i).Text.Length; j++)
                {
                    if (find[ix] != texts.ElementAt(i).Text[j])
                    {
                        return null;
                    }
                    ix++;
                    if (ix == find.Length)
                    {
                        return new Match()
                        {
                            EndElementIndex = i,
                            EndCharIndex = j
                        };
                    }

                }
                c = 0;
            }
            return null;
        }

        private class Match
        {
            /// <summary>
            /// Last matching element index containing part of the search text
            /// </summary>
            public int EndElementIndex { get; set; }

            /// <summary>
            /// Last matching char index of the search text in last matching element
            /// </summary>
            public int EndCharIndex { get; set; }
        }

        /// <summary>
        /// Преобразование месяца в родительный падеж.
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public static string GetStringGenitiveMonth(int month)
        {
            if (month < 1 || month > 12)
            {
                return "GetStringGenitiveMonth(int month) получил неверное значение.";
            }

            var cultureInfo = CultureInfo.GetCultureInfo("ru-RU");
            return cultureInfo.DateTimeFormat.MonthGenitiveNames[month - 1];
        }

        private static string GetFullPath(string nameTemplate, string pathTemplate)
        {
            return Path.Combine(pathTemplate, nameTemplate);
        }

        public static bool CheckingExistenceTemplate(string nameTemplate, string pathTemplate)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nameTemplate)
                    || string.IsNullOrWhiteSpace(pathTemplate))
                {
                    return false;
                }

                if (Directory.Exists(pathTemplate) is false)
                {
                    return false;
                }

                string path = GetFullPath(nameTemplate, pathTemplate);

                if (File.Exists(path) is false)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return false;
        }

        public static string GetPathCreatedTemporaryFile(string nameTemplate, string pathTemplate,
                                                         string pathTempFile = default, string extension = default)
        {
            if (CheckingExistenceTemplate(nameTemplate, pathTemplate) is true)
            {
                if (string.IsNullOrWhiteSpace(pathTempFile))
                {
                    pathTempFile = Path.GetTempFileName();

                    if (!string.IsNullOrWhiteSpace(extension))
                    {
                        if (extension[0] != '.')
                        {
                            extension = $".{extension}";
                        }

                        pathTempFile = pathTempFile.Replace(".tmp", $"{extension}");
                    }
                }

                string path = GetFullPath(nameTemplate, pathTemplate);

                try
                {
                    File.Copy(path, pathTempFile, true);
                    return pathTempFile;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
            return default;
        }

        //public static string GetTemplatesFolder()
        //{
        //    var pathTemplate = BVVGlobal.oApp?.AppParam?.TemplatesDir;
        //    if (Directory.Exists(pathTemplate))
        //    {
        //        return pathTemplate;
        //    }
        //    else
        //    {
        //        pathTemplate = $"templates";
        //        if (Directory.Exists(pathTemplate))
        //        {
        //            return pathTemplate;
        //        }
        //    }

        //    return default;
        //}

        public static void MergeCellsHorizontally(TableRow row)
        {
            try
            {
                var cells = row.Elements<TableCell>().ToList();

                if (cells.Count == 0)
                    return;

                // Инициализация первой ячейки
                var firstCell = cells.First();
                if (firstCell.TableCellProperties == null)
                {
                    firstCell.TableCellProperties = new TableCellProperties();
                }
                else
                {
                    firstCell.TableCellProperties.RemoveAllChildren<HorizontalMerge>();
                }
                firstCell.TableCellProperties.Append(new HorizontalMerge() { Val = MergedCellValues.Restart });
                SetCellBorders(firstCell);

                // Инициализация всех остальных ячеек
                foreach (var cell in cells.Skip(1))
                {
                    if (cell.TableCellProperties == null)
                    {
                        cell.TableCellProperties = new TableCellProperties();
                    }
                    else
                    {
                        cell.TableCellProperties.RemoveAllChildren<HorizontalMerge>();
                    }
                    cell.TableCellProperties.Append(new HorizontalMerge() { Val = MergedCellValues.Continue });
                    SetCellBorders(cell);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public static void MergeCellsVertically(Table table, int startRowIndex, int endRowIndex, int cellIndex)
        {
            try
            {
                var rows = table.Elements<TableRow>().ToList();

                TableCell firstCell = rows[startRowIndex].Elements<TableCell>().ElementAt(cellIndex);
                firstCell.TableCellProperties = new TableCellProperties();
                firstCell.TableCellProperties.Append(new VerticalMerge() { Val = MergedCellValues.Restart });

                SetCellBorders(firstCell);

                for (int i = startRowIndex + 1; i <= endRowIndex; i++)
                {
                    TableCell cell = rows[i].Elements<TableCell>().ElementAt(cellIndex);
                    cell.TableCellProperties = new TableCellProperties();
                    cell.TableCellProperties.Append(new VerticalMerge() { Val = MergedCellValues.Continue });

                    SetCellBorders(cell);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public static void SetCellBorders(TableCell cell)
        {
            try
            {
                var borders = new TableCellBorders(
                    new TopBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                    new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                    new LeftBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 },
                    new RightBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 4 }
                );

                var cellProperties = cell.GetFirstChild<TableCellProperties>();
                if (cellProperties == null)
                {
                    cellProperties = new TableCellProperties();
                    cell.AppendChild(cellProperties);
                }

                cellProperties.TableCellBorders = borders;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public static void DeleteRowFromTable(Table indicatorsAndMeasurementsTable, int elementPosition, int elementCount)
        {
            for (int i = 0; i < elementCount; i++)
            {
                var deleterow = indicatorsAndMeasurementsTable.Elements<TableRow>().ElementAt(elementPosition);
                deleterow.Remove();
            }
        }
    }
}
