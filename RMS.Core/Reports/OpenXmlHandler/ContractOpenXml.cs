using DevExpress.Xpo;
using DevExpress.XtraRichEdit;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using RMS.Core.Controller.Print;
using RMS.Core.Model.InfoContract;
using RMS.Core.Reports.WordDocument;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace RMS.Core.Reports.OpenXmlHandler
{
    public class ContractOpenXml
    {
        public Contract Contract { get; private set; }

        private WordprocessingDocument WordApplication { get; set; }

        /// <summary>
        /// Путь к шаблону.
        /// </summary>
        protected string PathTemplate { get; private set; }

        /// <summary>
        /// Путь к временному файлу.
        /// </summary>
        private string _tempFile = Path.GetTempFileName();

        /// <summary>
        /// Выходное наименование файла.
        /// </summary>
        public string OutFileName { get; set; } = "Клиентский договор";

        /// <summary>
        /// Открытие .tmp файла в PDF.
        /// </summary>
        /// <param name="pathFile"></param>
        protected void OpenToPDF(string pathFile)
        {
            RichEditDocumentServer server = new RichEditDocumentServer();
            server.LoadDocument(pathFile, DevExpress.XtraRichEdit.DocumentFormat.OpenXml);

            var options = new DevExpress.XtraPrinting.PdfExportOptions();
            options.DocumentOptions.Author = "ilel@list.ru";
            options.Compressed = false;
            options.ImageQuality = DevExpress.XtraPrinting.PdfJpegImageQuality.Highest;

            pathFile = pathFile.Replace("tmp", "pdf");

            using (FileStream pdfFileStream = new FileStream(pathFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                server.ExportToPdf(pdfFileStream, options);
            }

            if (File.Exists(_tempFile))
            {
                File.Delete(_tempFile);
            }

            if (File.Exists(pathFile))
            {
                Process.Start(pathFile);
            }
        }

        public ContractOpenXml(Contract contract, bool isPDF = false)
        {
            Contract = contract;

            try
            {
                Contract?.Reload();
                //FinalProtocol?.Protokols?.Reload();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            if (isPDF)
            {
                PrintReportWord(false);
                OpenToPDF(_tempFile);
                return;
            }
            else
            {
                PrintReportWord(true);
            }
            Process.Start(BasicFunctionality.RenameOrCopyTmpToDocx(_tempFile));
        }


        public ContractOpenXml(byte[] wordDocument, bool isPDF)
        {
            if (wordDocument != null)
            {
                File.WriteAllBytes(_tempFile, wordDocument);

                if (isPDF)
                {
                    OpenToPDF(_tempFile);
                    return;
                }

                Process.Start(BasicFunctionality.RenameOrCopyTmpToDocx(_tempFile));
            }
        }

        /// <summary>
        /// Печать отчета.
        /// </summary>
        /// <param name="isVisible"></param>
        /// <param name="path"></param>
        public void PrintReportWord(bool isVisible, string path = null)
        {
            var templateList = new List<Model.PlateTemplate>();
            AddPlateTemplateToList(templateList, Contract?.PlateTemplate1);
            AddPlateTemplateToList(templateList, Contract?.PlateTemplate2);
            AddPlateTemplateToList(templateList, Contract?.PlateTemplate3);

            var template = Contract?.PlateTemplate;
            if (template is null)
            {
                var session = Contract.Session;
                if (session != null)
                {
                    template = new XPQuery<Model.PlateTemplate>(session)
                        .Where(w => w.IsDefault && w.FileWord != null)
                        .FirstOrDefault();
                }
            }

            if (template != null)
            {
                var wordDocument = template.FileWord;
                if (wordDocument != null)
                {
                    File.WriteAllBytes(_tempFile, wordDocument);

                    using (WordApplication = WordprocessingDocument.Open(_tempFile, true))
                    {
                        var mergedBody = WordApplication.MainDocumentPart.Document.Body;

                        foreach (var item in templateList)
                        {
                            var templatePath = Path.GetTempFileName();
                            File.WriteAllBytes(templatePath, item.FileWord);
                            using (var wordApplication = WordprocessingDocument.Open(templatePath, true))
                            {
                                var body = wordApplication.MainDocumentPart.Document.Body;
                                mergedBody.AppendChild(new Paragraph(new Run(new Break() { Type = BreakValues.Page })));
                                
                                foreach (var element in body.Elements())
                                {
                                    var clonedElement = element.CloneNode(true);
                                    mergedBody.AppendChild(clonedElement);
                                }
                            }
                        }    

                        FillFirstSheet();
                        //FillSecondSheet(WordApplication, Organization, FinalProtocol);

                        OutFileName = $"Клиентский договор {Contract.Number}";

                        WordApplication.Save();
                    }
                    SaveFileInDataBase();
                }
            }
        }

        private void AddPlateTemplateToList(List<Model.PlateTemplate> templateList, Model.PlateTemplate plateTemplate)
        {
            if (plateTemplate != null 
                && templateList.FirstOrDefault(f => f.Oid == plateTemplate.Oid) is null)
            {
                templateList.Add(plateTemplate);
            }
        }

        protected virtual void FillFirstSheet()
        {
            var contract = Contract;
            if (contract is null)
            {
                return;
            }

            var customer = contract.Customer;
            var date = DateTime.Now;

            if (contract.Date != null)
            {
                date = Convert.ToDateTime(contract.Date);
            }

            BasicFunctionality.FindingTextAndReplacingItWithText(WordApplication, ReportVariables.TOWN, contract.Town);
            BasicFunctionality.FindingTextAndReplacingItWithText(WordApplication, ReportVariables.DAY, date.Day.ToString());
            BasicFunctionality.FindingTextAndReplacingItWithText(WordApplication, ReportVariables.MONTH, InfoCard.GetStringGenitiveMonth(date.Month));
            BasicFunctionality.FindingTextAndReplacingItWithText(WordApplication, ReportVariables.CONTRACTNUMBER, contract?.Number ?? string.Empty);
            BasicFunctionality.FindingTextAndReplacingItWithText(WordApplication, ReportVariables.YEAR, date.Year.ToString());
            BasicFunctionality.FindingTextAndReplacingItWithText(WordApplication, ReportVariables.CUSTOMER, customer.ToString() ?? string.Empty);
            BasicFunctionality.FindingTextAndReplacingItWithText(WordApplication, ReportVariables.CUSTOMERFULLNAME, customer.FullName.ToString() ?? string.Empty);
            BasicFunctionality.FindingTextAndReplacingItWithText(WordApplication, ReportVariables.CUSTOMERABBREVIATEDNAME, customer.AbbreviatedName.ToString() ?? string.Empty);

            var management = $"{customer.ManagementSurname} {customer.ManagementName} {customer.ManagementPatronymic}";
            BasicFunctionality.FindingTextAndReplacingItWithText(WordApplication, ReportVariables.CUSTOMERMANAGEMENTFULL, management ?? string.Empty);

            BasicFunctionality.FindingTextAndReplacingItWithText(WordApplication, ReportVariables.CUSTOMERMANAGEMENT, customer.ManagementString ?? string.Empty);
            BasicFunctionality.FindingTextAndReplacingItWithText(WordApplication, ReportVariables.CUSTOMERMANAGEMENTPOSITION, customer.ManagementPositionString ?? string.Empty);
            BasicFunctionality.FindingTextAndReplacingItWithText(WordApplication, ReportVariables.TELEPHONE, customer.Telephone ?? string.Empty);
            BasicFunctionality.FindingTextAndReplacingItWithText(WordApplication, ReportVariables.Contract, contract?.ToString() ?? string.Empty);
            BasicFunctionality.FindingTextAndReplacingItWithText(WordApplication, ReportVariables.INN, customer.INN ?? string.Empty);
            BasicFunctionality.FindingTextAndReplacingItWithText(WordApplication, ReportVariables.KPP, customer.KPP ?? string.Empty);
            BasicFunctionality.FindingTextAndReplacingItWithText(WordApplication, ReportVariables.OGRN, customer.PSRN ?? string.Empty);
            BasicFunctionality.FindingTextAndReplacingItWithText(WordApplication, ReportVariables.ADDRESS, customer.LegalAddress ?? string.Empty);

            BasicFunctionality.FindingTextAndReplacingItWithText(WordApplication, ReportVariables.ORGANIZATIONNAME, contract.Organization?.Name ?? string.Empty);
            BasicFunctionality.FindingTextAndReplacingItWithText(WordApplication, ReportVariables.ORGANIZATIONFULLNAME, contract.Organization?.FullName ?? string.Empty);
            BasicFunctionality.FindingTextAndReplacingItWithText(WordApplication, ReportVariables.ORGANIZATIONABBREVIATEDNAME, contract.Organization?.AbbreviatedName ?? string.Empty);

            BasicFunctionality.FindingTextAndReplacingItWithText(WordApplication, ReportVariables.ORGANIZATIONMANAGMENT, contract.Organization?.ManagementString ?? string.Empty);
            BasicFunctionality.FindingTextAndReplacingItWithText(WordApplication, ReportVariables.ORGANIZATIONMANAGMENTPOSITION, contract.Organization?.ManagementPositionString ?? string.Empty);
            BasicFunctionality.FindingTextAndReplacingItWithText(WordApplication, ReportVariables.ORGANIZATIOINN, contract.Organization?.INN ?? string.Empty);
            BasicFunctionality.FindingTextAndReplacingItWithText(WordApplication, ReportVariables.ORGANIZATIONKPP, contract.Organization?.KPP ?? string.Empty);
            BasicFunctionality.FindingTextAndReplacingItWithText(WordApplication, ReportVariables.ORGANIZATIONPSRN, contract.Organization?.PSRN ?? string.Empty);
            BasicFunctionality.FindingTextAndReplacingItWithText(WordApplication, ReportVariables.ORGANIZATIONTELEPHONE, contract.Organization?.Telephone ?? string.Empty);

            BasicFunctionality.FindingTextAndReplacingItWithText(WordApplication, ReportVariables.DATESINCE, contract.DateSince?.ToShortDateString() ?? string.Empty);
            BasicFunctionality.FindingTextAndReplacingItWithText(WordApplication, ReportVariables.DATETO, contract.DateTo?.ToShortDateString() ?? string.Empty);

            BasicFunctionality.FindingTextAndReplacingItWithText(WordApplication, ReportVariables.CUSTOMERADDRESS, contract.Customer?.LegalAddress ?? "-");
            BasicFunctionality.FindingTextAndReplacingItWithText(WordApplication, ReportVariables.CUSTOMERINN, contract.Customer?.INN ?? "-");
            BasicFunctionality.FindingTextAndReplacingItWithText(WordApplication, ReportVariables.CUSTOMERPSRN, contract.Customer?.PSRN ?? "-");
            BasicFunctionality.FindingTextAndReplacingItWithText(WordApplication, ReportVariables.CUSTOMERBANKINFO, "-");
            var telephones = default(string);
            var customerTelephones = contract.Customer?.CustomerTelephones;
            if (customerTelephones != null)
            {
                foreach (var item in customerTelephones)
                {
                    telephones += $"{item.Telephone}; ";
                }
            }
            BasicFunctionality.FindingTextAndReplacingItWithText(WordApplication, ReportVariables.CUSTOMERPHONES, telephones?.Trim() ?? "-");

            BasicFunctionality.FindingTextAndReplacingItWithText(WordApplication, ReportVariables.ORGANIZATIONADDRESS, "-");
            BasicFunctionality.FindingTextAndReplacingItWithText(WordApplication, ReportVariables.ORGANIZATIONINN, contract.Organization?.INN ?? "-");
            BasicFunctionality.FindingTextAndReplacingItWithText(WordApplication, ReportVariables.ORGANIZATIONPSRN, contract.Organization?.PSRN ?? "-");
            BasicFunctionality.FindingTextAndReplacingItWithText(WordApplication, ReportVariables.ORGANIZATIONBANKINFO,  "-");
            BasicFunctionality.FindingTextAndReplacingItWithText(WordApplication, ReportVariables.ORGANIZATIONPHONES, contract.Organization?.Telephone ?? "-");
            BasicFunctionality.FindingTextAndReplacingItWithText(WordApplication, ReportVariables.ORGANIZATIONMANAGEMENTPOSITION, contract.Organization?.ManagementPosition?.ToString() ?? "-");

            var listObj = new List<string>()
            {
                "<TAXATION_SYSTEM_IN_PERCENTAGE>",
                "<TAXATION_SYSTEM>",
                "<PATENTS>",
                "<AVAILABILITY_OF_BRANCHES_SEPARATE_DIVISIONS>",
                "<AVAILABILITY_OF_BRANCHES_SEPARATE_DIVISIONS_LIST>",
                "<AVAILABILITY_OF_FOREIGN_TRADE_ACTIVITIES>",
                "<AVAILABILITY_OF_FOREIGN_TRADE_ACTIVITIES_COUNTRY>",
                "<CONDUCTING_BANKING_OPERATIONS_CUSTOMER>",
                "<CONDUCTING_BANKING_OPERATIONS_EXECUTOR>",
                "<ACCESS_TO_THE_BANK_FROM_THE_CONTRACTOR>",
                "<MAINTAINING_PERSONNEL_RECORDS_CUSTOMER>",
                "<MAINTAINING_PERSONNEL_RECORDS_EXECUTOR>",
                "<MAINTAINING_PERSONNEL_RECORDS_SALARY_PAYMENT_DATE_1>",
                "<MAINTAINING_PERSONNEL_RECORDS_SALARY_PAYMENT_DATE_2>",
                "<PREPARATION_AND_SUBMISSION_OF_REPORTS_SALARY_CUSTOMER>",
                "<PREPARATION_AND_SUBMISSION_OF_REPORTS_SALARY_EXECUTOR>",
                "<PREPARATION_AND_SUBMISSION_OF_REPORTS_ACCORDING_MAIN_SYSTEM_CUSTOMER>",
                "<PREPARATION_AND_SUBMISSION_OF_REPORTS_ACCORDING_MAIN_SYSTEM_EXECUTOR>",
                "<PREPARATION_AND_SUBMISSION_OF_REPORTS_STATISTICS_CUSTOMER>",
                "<PREPARATION_AND_SUBMISSION_OF_REPORTS_STATISTICS_EXECUTOR>"
            };

            foreach (var item in listObj)
            {
                BasicFunctionality.FindingTextAndReplacingItWithListYesNo(WordApplication, $"{item}", $"{item.Replace("<","").Replace(">","")}");
            }

            //BasicFunctionality.FindingTextAndReplacingItWithListYesNo(WordApplication, "<TAXATION_SYSTEM>", "Taxation System");
            //BasicFunctionality.FindingTextAndReplacingItWithListYesNo(WordApplication, "<TAXATION_SYSTEM_IN_PERCENTAGE>", "Taxation System Percentage");
        }


        /// <summary>
        /// Сохранение файла отчета в базу данных.
        /// </summary>
        protected virtual void SaveFileInDataBase()
        {
            var session = Contract.Session;
            if (session.IsConnected)           
            {

                var contractNumber = default(string);

                if (int.TryParse(Contract.Number, out int result))
                {
                    contractNumber = result.ToString();
                }

                try
                {
                    var fileContract = new ContractFile(session)
                    {
                        DateCreate = DateTime.Now,
                        Contract = Contract,
                        File = new Model.File(session, _tempFile)
                        {
                            FileName = OutFileName
                        }
                    };
                    fileContract.Save();
                    Contract.ContractFiles?.Reload();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }
    }
}
