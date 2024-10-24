using DevExpress.Xpo;
using Microsoft.Office.Interop.Excel;
using RMS.Core.Model.Reports;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace RMS.Core.Controller.Print
{
    /// <summary>
    /// Информация для клиента по отчетам.
    /// </summary>
    public class InformationForClientReport
    {
        private Application ExcelApplication { get; set; }
        private Workbook WorkBook { get; set; }
        private Worksheet WorkSheet { get; set; }
        private XPCollection<ReportChange> ReportsChange { get; set; }

        public delegate void ProgressEventHandler(object sender, string name, int count, int number);
        public event ProgressEventHandler ProgressEvent;

        private object _missingObj = System.Reflection.Missing.Value;
        private string _tempFile = Path.GetTempFileName();
        
        public InformationForClientReport(XPCollection<ReportChange> reportsChange)
        {
            ReportsChange = reportsChange;
        }

        public async Task PrintAsync()
        {
            await Task.Run(() => Print());
        }

        public void Print()
        {
            ExcelApplication = new Application();
            try
            {
                ExcelApplication.Visible = false;
                ExcelApplication.DisplayAlerts = false;

                WorkBook = ExcelApplication.Workbooks.Add();
                WorkSheet = (Worksheet)WorkBook.Worksheets.get_Item(1);

                WorkSheet.Columns[1].ColumnWidth = 50;
                WorkSheet.Cells[1, 1] = "Клиент";
                WorkSheet.Cells[1, 2] = "Год";
                WorkSheet.Cells[1, 3] = "Отчет";
                WorkSheet.Cells[1, 4] = "Период сдачи";
                WorkSheet.Cells[1, 5] = "Статус";
                WorkSheet.Cells[1, 6] = "Сдать до";
                WorkSheet.Cells[1, 7] = "Дата сдачи";
                WorkSheet.Cells[1, 8] = "Комментарий";
                
                PositioningCell(WorkSheet, 1, 1);
                PositioningCell(WorkSheet, 1, 2);
                PositioningCell(WorkSheet, 1, 3);
                PositioningCell(WorkSheet, 1, 4);
                PositioningCell(WorkSheet, 1, 5);
                PositioningCell(WorkSheet, 1, 6);
                PositioningCell(WorkSheet, 1, 7);
                PositioningCell(WorkSheet, 1, 8);

                var rowNumber = 2;

                ProgressEvent?.Invoke(this, "Информация для клиента", ReportsChange.Count, 0);
                foreach (var customer in ReportsChange.GroupBy(g => g.Customer))
                {
                    var firstRowObjectCustomer = rowNumber;

                    var customerString = $"{customer.Key}{Environment.NewLine}" +
                        $"ИНН: {customer.Key.INN}{Environment.NewLine}" +
                        $"КПП: {customer.Key.KPP}{Environment.NewLine}" +
                        $"Руководитель: {customer.Key.ManagementString ?? "Не указан"}";
                    
                    WorkSheet.Cells[rowNumber, 1] = customerString;

                    foreach (var year in customer.GroupBy(g => g.DeliveryYear).OrderBy(o => o.Key))
                    {
                        var firstRowObjectYear = rowNumber;
                        WorkSheet.Cells[rowNumber, 2] = year.Key.ToString() ?? string.Empty;

                        foreach (var report in year.OrderBy(o => o.Report.Name).GroupBy(g => g.Report))
                        {
                            var firstRowObjectReport = rowNumber;
                            WorkSheet.Cells[rowNumber, 3] = report.Key.ToString();

                            foreach (var period in report.GroupBy(g => g.PeriodString).OrderByDescending(o => o.Key))
                            {
                                WorkSheet.Cells[rowNumber, 4] = period.Key;

                                foreach (var reportsChange in period.OrderBy(o => o.ReportString))
                                {
                                    WorkSheet.Cells[rowNumber, 5] = reportsChange.StatusString;
                                    WorkSheet.Cells[rowNumber, 6] = reportsChange.LastDayDelivery;
                                    WorkSheet.Cells[rowNumber, 7] = reportsChange.DateCompletion;
                                    WorkSheet.Cells[rowNumber, 8] = reportsChange.Comment;                                    

                                    Range range = WorkSheet.Range[WorkSheet.Cells[rowNumber, 5], WorkSheet.Cells[rowNumber, 7]];
                                    if (reportsChange.DateCompletion is null)
                                    {
                                        range.Interior.Color = XlRgbColor.rgbLightYellow;
                                    }
                                    else
                                    {
                                        range.Interior.Color = XlRgbColor.rgbLightGreen;
                                    }
                                }
                                
                                ProgressEvent?.Invoke(this, "Информация для клиента", ReportsChange.Count, rowNumber - 1);
                                rowNumber++;                                
                            }

                            if (firstRowObjectReport != rowNumber - 1)
                            {
                                MergeCell(WorkSheet, firstRowObjectReport, 3, rowNumber - 1, 3);
                            }
                            else
                            {
                                PositioningCell(WorkSheet, firstRowObjectReport, 3);
                            }
                        }

                        MergeCell(WorkSheet, firstRowObjectYear, 2, rowNumber - 1, 2);
                    }
                    
                    MergeCell(WorkSheet, firstRowObjectCustomer, 1, rowNumber - 1, 1);                    
                }
                
                PositioningCell(WorkSheet, 2, 4, rowNumber - 1, 7);
                ProgressEvent?.Invoke(this, "Информация для клиента", ReportsChange.Count, ReportsChange.Count);
                
                Range rng = WorkSheet.Range[WorkSheet.Cells[1, 1], WorkSheet.Cells[rowNumber - 1, 8]];
                Borders border = rng.Borders;
                border.LineStyle = XlLineStyle.xlContinuous;

                WorkSheet.Columns.AutoFit();
                WorkSheet.Rows.AutoFit();
                
                ExcelApplication.UserControl = true;

                _tempFile = _tempFile.Replace(".tmp", ".xls");
                WorkBook.SaveAs(_tempFile);
                WorkBook.Close(false, _missingObj, _missingObj);
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
            finally
            {
                ExcelApplication.Quit();

                Marshal.ReleaseComObject(ExcelApplication);

                ExcelApplication = null;
                WorkBook = null;
                WorkSheet = null;

                GC.Collect();
                GC.WaitForPendingFinalizers();

                Process.Start(_tempFile);
            }
        }

        private void MergeCell(Worksheet workSheet,
                               int inCellRow,
                               int inCellColumn,
                               int outCellRow,
                               int outCellColumn,
                               Constants horizontalAlignment = Constants.xlCenter,
                               Constants verticalAlignment = Constants.xlCenter)
        {
            Range range = workSheet.Range[workSheet.Cells[inCellRow, inCellColumn], workSheet.Cells[outCellRow, outCellColumn]].Cells;
            range.Merge(Type.Missing);
            range.HorizontalAlignment = horizontalAlignment;
            range.VerticalAlignment = verticalAlignment;
        }
        
        private void PositioningCell(Worksheet workSheet,
                       int inCellRow,
                       int inCellColumn,
                       int? outCellRow = null,
                       int? outCellColumn = null,
                       Constants horizontalAlignment = Constants.xlCenter,
                       Constants verticalAlignment = Constants.xlCenter)
        {
            if (outCellRow is null)
            {
                outCellRow = inCellRow;
            }
            
            if(outCellColumn is null)
            {
                outCellColumn = inCellColumn;
            }

            Range range = workSheet.Range[workSheet.Cells[inCellRow, inCellColumn], workSheet.Cells[outCellRow, outCellColumn]].Cells;
            range.HorizontalAlignment = horizontalAlignment;
            range.VerticalAlignment = verticalAlignment;
        }

        private void PrintProperty(Worksheet workSheet, int row, int column, object property)
        {
            var count = GetInt(property);
            if (count >= 0)
            {
                workSheet.Cells[row, column] = count.ToString();
            }
        }

        private void PrintProperty(Worksheet workSheet, ref int row, int column, object property, string caption)
        {
            var count = GetInt(property);
            if (count >= 0)
            {
                workSheet.Cells[row, column] = caption;
                workSheet.Cells[row, column + 1] = count.ToString();
                row++;
            }
        }

        private void PrintPropertyOther(Worksheet workSheet, int row, int column, object property)
        {
            if (property is string result)
            {
                if (bool.TryParse(result, out bool isUse))
                {
                    if (isUse is true)
                    {
                        workSheet.Cells[row, column] = "Имеется";
                    }
                }
            }
        }

        private void PrintPropertyOther(Worksheet workSheet, ref int row, int column, object property, string caption)
        {
            if (property is string result)
            {
                if (bool.TryParse(result, out bool isUse))
                {
                    if (isUse is true)
                    {
                        workSheet.Cells[row, column] = caption;
                        workSheet.Cells[row, column + 1] = "Имеется";
                        row++;
                    }
                }
            }
        }
        
        private int GetInt(object obj)
        {
            if (obj is string result)
            {
                if (int.TryParse(result, out int number))
                {
                    return number;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
    }
}
