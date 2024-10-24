using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RMS.ParserReportingSystem.Extensions;
using RMS.ParserReportingSystem.Model;
using RMS.ParserReportingSystem.Model.Enumerator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace RMS.ParserReportingSystem.Controller
{
    /// <summary>
    /// Парсер https://websbor.gks.ru/
    /// </summary>
    public class ReportingSystem : IDisposable
    {
        public delegate void ErrorEventHandler(object sender, string message);
        public event ErrorEventHandler ErrorEvent;

        public OrganizationStatisticsCodes OrganizationStatisticsCodes { get; private set; }
        public List<Report> Reports { get; private set; }

        /// <summary>
        /// Адрес сервиса.
        /// </summary>
        public static string Uri { get; set; } = @"https://websbor.gks.ru/webstat/#!/gs/statistic-codes";

        /// <summary>
        /// Время задержки для получения информации.
        /// </summary>
        public static int DelayTime { get; set; } = 2000;

        /// <summary>
        /// Скрыть командную строку и браузер разработчика во время выполнения.
        /// </summary>
        public static bool IsHide { get; set; } = true;
        
        public string Inn { get; set; }
        public string Okpo { get; set; }
        public string Psrn { get; set; }

        private ChromeDriverService _chromeDriverService;
        private ChromeOptions _chromeOptions;
        private ChromeDriver _chromeDriver;
        
        public ReportingSystem(string inn = default, 
            string okpo = default, 
            string psrn = default)
        {
            if (string.IsNullOrWhiteSpace(inn) && string.IsNullOrWhiteSpace(okpo) && string.IsNullOrWhiteSpace(psrn))
            {
                return;
            }
            
            Inn = inn;
            Okpo = okpo;
            Psrn = psrn;
        }
        
        public bool StartDriver()
        {
            try
            {
                _chromeDriverService = ChromeDriverService.CreateDefaultService();
                _chromeOptions = new ChromeOptions();

                if (IsHide)
                {
                    _chromeDriverService.HideCommandPromptWindow = true;
                    _chromeOptions.AddArgument("headless");
                }

                _chromeDriver = new ChromeDriver(_chromeDriverService, _chromeOptions) { Url = Uri };               

                return true;
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());

                if (ex.Message.Contains("This version of ChromeDriver only supports"))
                {
                    ErrorEvent?.Invoke(this, $"{ex.Message}{Environment.NewLine}{Environment.NewLine}Обновите chromedriver.exe [https://chromedriver.chromium.org/]");
                }

                KillChromeDriverProcesses();
                return false;
            }
        }

        public void StartParser()
        {
            try
            {
                if (_chromeDriver is null)
                {
                    StartDriver();
                }

                FillTable(Inn, Okpo, Psrn);
                OrganizationStatisticsCodes = GetOrganizationStatisticsCodesAsync().Result;
                Reports = GetReportsAsync().Result;
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }          
        }

        public void KillChromeDriverProcesses()
        {
            _chromeDriver?.Close();
            _chromeDriver?.Quit();

            try
            {
                foreach (Process proc in Process.GetProcessesByName("chromedriver"))
                {
                    proc.Kill();
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        public void FillTable(string inn, string okpo, string psrn)
        {
            try
            {
                if (_chromeDriver is null)
                {
                    if (string.IsNullOrWhiteSpace(inn) && string.IsNullOrWhiteSpace(okpo) && string.IsNullOrWhiteSpace(psrn))
                    {
                        return;
                    }

                    Inn = inn;
                    Okpo = okpo;
                    Psrn = psrn;

                    StartDriver();
                }

                _chromeDriver.Navigate().Refresh();
                Thread.Sleep(DelayTime);

                if (!string.IsNullOrWhiteSpace(inn))
                {
                    var innTextBox = _chromeDriver.FindElement(By.Id("inn"));
                    innTextBox.Clear();
                    innTextBox.SendKeys(inn.Trim());
                }

                if (!string.IsNullOrWhiteSpace(okpo))
                {
                    var okpoTextBox = _chromeDriver.FindElement(By.Id("mat-input-2"));
                    okpoTextBox.Clear();
                    okpoTextBox.SendKeys(okpo.Trim());
                }

                if (!string.IsNullOrWhiteSpace(psrn))
                {
                    var psrnTextBox = _chromeDriver.FindElement(By.Id("ogrn"));
                    psrnTextBox.Clear();
                    psrnTextBox.SendKeys(psrn.Trim());
                }

                //var btnGetInformation = ChromeDriver.FindElement(By.XPath("//button[@type='submit'][span()=' Получить ']"));
                var btnGetInformation = _chromeDriver.FindElement(By.XPath(".//button[@class='mat-focus-indicator mat-flat-button mat-button-base mat-primary']"));
                btnGetInformation.Click();

                Thread.Sleep(DelayTime);
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());                
            }
        }

        public async Task<OrganizationStatisticsCodes> GetOrganizationStatisticsCodesAsync()
        {
            return await Task.Run(() => GetOrganizationStatisticsCodes()).ConfigureAwait(false); ;
        }

        public async Task<List<Report>> GetReportsAsync()
        {
            return await Task.Run(() => GetReports()).ConfigureAwait(false); ;
        }

        /// <summary>
        /// Получить список отчетов.
        /// </summary>
        /// <returns></returns>
        private List<Report> GetReports()
        {
            var reports = new List<Report>();

            //var formsTable = ChromeDriver.FindElement(By.Id("formsTable"));
            
            if (_chromeDriver.FindElements(By.XPath(".//table"))?.Count > 1)
            {
                var formsTable = _chromeDriver.FindElements(By.XPath(".//table"))[1];

                var rowsFormsTable = formsTable.FindElements(By.TagName("tr"));

                foreach (IWebElement row in rowsFormsTable)
                {
                    var columns = row.FindElements(By.TagName("td"));

                    if (columns.Count == 8)
                    {
                        var formindex = columns[0].Text;
                        var name = columns[1].Text;
                        var periodicity = GetPeriodicity(columns[2].Text);
                        var reportingPeriod = columns[4].Text;
                        var deadline = columns[3].Text;
                        var comment = columns[5].Text;
                        var okud = columns[6].Text;

                        var report = new Report(formindex, name, periodicity, deadline, comment, okud, reportingPeriod);
                        reports.Add(report);
                    }
                }

                return reports;
            }
            else
            {
                return reports;
            }
            
            //if (formsTable is null)
            //{
            //    return reports;
            //}

            //var rowsFormsTable = formsTable.FindElements(By.TagName("tr"));            

            //foreach (IWebElement row in rowsFormsTable)
            //{
            //    var columns = row.FindElements(By.TagName("td"));

            //    if (columns.Count == 8)
            //    {
            //        var formindex = columns[0].Text;
            //        var name = columns[1].Text;
            //        var periodicity = GetPeriodicity(columns[2].Text);
            //        var deadline = columns[3].Text;
            //        var comment = columns[5].Text;
            //        var okud = columns[6].Text;

            //        var report = new Report(formindex, name, periodicity, deadline, comment, okud);
            //        reports.Add(report);
            //    }
            //}

            //return reports;
        }

        /// <summary>
        /// Перевод текста в перечислитель.
        /// </summary>
        /// <param name="text">Полученный текст.</param>
        /// <returns>Имеющуюся периодичность.</returns>
        private Periodicity GetPeriodicity(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return default;

            foreach (Periodicity periodicity in Enum.GetValues(typeof(Periodicity)))
                if (text.Equals(periodicity.GetEnumDescription()))
                    return periodicity;

            return default;
        }

        /// <summary>
        /// Получить статистику по организации.
        /// </summary>
        /// <returns></returns>
        private OrganizationStatisticsCodes GetOrganizationStatisticsCodes()
        {
            var organizationTable = default(IWebElement);
            var rowsOrganizationTable = default(ReadOnlyCollection<IWebElement>);

            try
            {
                organizationTable = _chromeDriver.FindElement(By.XPath(".//table"));
                rowsOrganizationTable = organizationTable.FindElements(By.TagName("tr"));

                if (organizationTable is null)
                {
                    return default;
                }

                if (rowsOrganizationTable != null)
                {
                    var list = new List<string>();
                    foreach (IWebElement row in rowsOrganizationTable)
                    {
                        var columns = row.FindElements(By.TagName("td"));
                        list.Add(columns[0].Text);
                    }

                    if (list.Count > 0)
                    {
                        return new OrganizationStatisticsCodes(list[0], list[1], list[3], list[4], list[5], list[6],
                               list[7], list[8], list[9], list[10]);
                    }
                }
            }
            catch (NoSuchElementException ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                return default;
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                return default;
            }                   

            return default;
        }

        private string GetNumber(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                return text.Split()[0];
            }

            return default;
        }

        public void Dispose()
        {
            KillChromeDriverProcesses();
        }
    }
}
