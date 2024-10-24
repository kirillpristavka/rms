using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using Microsoft.Office.Interop.Excel;
using RMS.Core.Enumerator;
using RMS.Core.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace RMS.Core.Controller.Print
{
    /// <summary>
    /// Информация по работе организации.
    /// </summary>
    public class OrganizationPerformanceExcel
    {
        private Application ExcelApplication { get; set; }
        private Workbook WorkBook { get; set; }
        private Worksheet WorkSheet { get; set; }
        private XPCollection<OrganizationPerformance> OrganizationPerformances { get; set; }        
        private GroupOperator GroupOperator { get; }
        private string CaptionSince { get; }
        private string CaptionTo { get; }
        
        private object _missingObj = System.Reflection.Missing.Value;
        private string _tempFile = Path.GetTempFileName();
        
        public OrganizationPerformanceExcel(GroupOperator groupOperator, string captionSince, string captionTo)
        {
            GroupOperator = groupOperator;
            CaptionSince = captionSince;
            CaptionTo = captionTo;
            OrganizationPerformances = new XPCollection<OrganizationPerformance>(groupOperator);
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

                WorkSheet.Cells[2, 1] = "Дата формирования: ";
                WorkSheet.Cells[2, 2] = DateTime.Now.ToShortDateString();

                if (!string.IsNullOrWhiteSpace(CaptionSince))
                {
                    WorkSheet.Cells[3, 1] = "Период c: ";
                    WorkSheet.Cells[3, 2] = CaptionSince;
                }

                if (!string.IsNullOrWhiteSpace(CaptionTo))
                {
                    WorkSheet.Cells[3, 3] = "по";
                    WorkSheet.Cells[3, 4] = CaptionTo;
                }

                var groups = new Dictionary<int, int>();
                var row = 5;
                var column = 1;

                WorkSheet.Cells[row, column] = "Организация";

                var groupPerformanceIndicators = OrganizationPerformances
                    .SelectMany(sM => sM.CustomerPerformanceIndicators
                    .Where(w => w.PerformanceIndicator != null
                        && w.PerformanceIndicator.GroupPerformanceIndicator != null)
                    .Select(s => s.PerformanceIndicator.GroupPerformanceIndicator))
                    .Distinct();

                foreach (var group in groupPerformanceIndicators)
                {
                    column++;
                    var groupName = group.Name ?? "Наименование группы не задано";
                    WorkSheet.Cells[row, column] = groupName;
                    MergeCell(WorkSheet, row, column, row, column + 1);
                    groups.Add(column, group.Oid);
                    column++;
                }

                column++;
                WorkSheet.Cells[row, column] = "Комментарий";
                groups.Add(-1, column);
                row++;

                MergeCell(WorkSheet, row, 1, row, column);

                foreach (var organizationPerformance in OrganizationPerformances)
                {
                    if (organizationPerformance.CustomerPerformanceIndicators.Count <= 0)
                    {
                        continue;
                    }

                    row++;

                    WorkSheet.Cells[row, 1] = organizationPerformance.Customer.ToString();

                    var maxRowList = new List<int>();

                    foreach (var group in groups)
                    {
                        var tempRow = row;

                        var listIndicators = organizationPerformance.CustomerPerformanceIndicators
                            .Where(w => !string.IsNullOrWhiteSpace(w.Value))
                            .Where(w => w.PerformanceIndicator != null && w.PerformanceIndicator.GroupPerformanceIndicator.Oid == group.Value)
                            .ToList();

                        foreach (var item in listIndicators)
                        {
                            if (item.PerformanceIndicator.TypePerformanceIndicator == TypePerformanceIndicator.Percent)
                            {
                                PrintPropertyOther(WorkSheet, ref tempRow, group.Key, item.Value, item.PerformanceIndicator.Name);
                            }
                            else
                            {
                                PrintProperty(WorkSheet, ref tempRow, group.Key, item.Value, item.PerformanceIndicator.Name);
                            }
                        }
                        maxRowList.Add(tempRow);
                    }

                    var columnComment = default(int?);
                    if (groups.TryGetValue(-1, out int result))
                    {
                        columnComment = result;
                        WorkSheet.Cells[row, columnComment] = organizationPerformance.Comment ?? string.Empty;
                    }

                    var maxRow = maxRowList.Max();

                    if (maxRow > row)
                    {
                        MergeCell(WorkSheet, row, 1, maxRow - 1, 1);

                        if (columnComment != null)
                        {
                            MergeCell(WorkSheet, row, (int)columnComment, maxRow - 1, (int)columnComment);
                        }
                    }

                    row = maxRow;

                    MergeCell(WorkSheet, row, 1, row, column);
                }

                Range rng = WorkSheet.Range[WorkSheet.Cells[5, 1], WorkSheet.Cells[row, column]];
                Borders border = rng.Borders;
                border.LineStyle = XlLineStyle.xlContinuous;

                WorkSheet.Columns.AutoFit();
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

        public void Print2()
        {
            ExcelApplication = new Application();
            try
            {
                ExcelApplication.Visible = false;
                ExcelApplication.DisplayAlerts = false;

                WorkBook = ExcelApplication.Workbooks.Add();
                WorkSheet = (Worksheet)WorkBook.Worksheets.get_Item(1);

                WorkSheet.Cells[2, 1] = "Дата формирования: ";
                WorkSheet.Cells[2, 2] = DateTime.Now.ToShortDateString();

                if (!string.IsNullOrWhiteSpace(CaptionSince))
                {
                    WorkSheet.Cells[3, 1] = "Период c: ";
                    WorkSheet.Cells[3, 2] = CaptionSince;
                }

                if (!string.IsNullOrWhiteSpace(CaptionTo))
                {
                    WorkSheet.Cells[3, 3] = "по";
                    WorkSheet.Cells[3, 4] = CaptionTo;
                }

                var periodDictionary = new Dictionary<string, int>();
                var row = 5;
                var column = 1;

                WorkSheet.Cells[row, column] = "Организация";

                column++;
                
                WorkSheet.Cells[row, column] = "Показатель";

                var periods = OrganizationPerformances.OrderBy(o => o.Month).OrderBy(o => o.Year).Select(sM => sM.Period).Distinct();
                foreach (var item in periods)
                {
                    column++;
                    var periodName = item;
                    WorkSheet.Cells[row, column] = periodName;
                    periodDictionary.Add(periodName, column);
                }

                row++;

                var groupCustomerOrganizationPerformance = OrganizationPerformances.GroupBy(g => g.Customer);

                var customerCount = default(int);

                foreach (var customer in groupCustomerOrganizationPerformance)
                {
                    var organizationPerformances = OrganizationPerformances.Where(w => w.Customer.Oid == customer.Key.Oid
                        && w.CustomerPerformanceIndicators.FirstOrDefault(f => !string.IsNullOrWhiteSpace(f.Value)) != null);

                    if (organizationPerformances.FirstOrDefault(f => f.Customer.Oid == customer.Key.Oid) is null)
                    {
                        continue;
                    }                    

                    row++;
                    customerCount++;
                    
                    WorkSheet.Cells[row, 1] = customer.Key.ToString();

                    var indicatorDictionary = new Dictionary<int, int>();

                    var maxRowList = new List<int>();

                    foreach (var perfomance in organizationPerformances)
                    {
                        periodDictionary.TryGetValue(perfomance.Period, out int columnId);

                        var rowId = row;
                        maxRowList.Add(rowId);

                        foreach (var indicator in perfomance.CustomerPerformanceIndicators)
                        {
                            if (!string.IsNullOrWhiteSpace(indicator.Value))
                            {
                                var isAddDictionary = false;

                                if (!indicatorDictionary.ContainsKey(indicator.PerformanceIndicator.Oid))
                                {
                                    indicatorDictionary.Add(indicator.PerformanceIndicator.Oid, rowId);
                                    WorkSheet.Cells[rowId, 2] = indicator.PerformanceIndicatorString;
                                    isAddDictionary = true;
                                }
                                else
                                {
                                    indicatorDictionary.TryGetValue(indicator.PerformanceIndicator.Oid, out rowId);
                                }

                                if (indicator.PerformanceIndicator.TypePerformanceIndicator == TypePerformanceIndicator.Percent)
                                {
                                    PrintPropertyOther(WorkSheet, rowId, columnId, indicator.Value);
                                }
                                else
                                {
                                    PrintProperty(WorkSheet, rowId, columnId, indicator.Value);
                                }

                                if (isAddDictionary)
                                {
                                    maxRowList.Add(rowId);
                                    rowId++;
                                }
                            }                            
                        }                        
                    }

                    var maxRow = maxRowList.Max();
                    
                    /* Окрашивание строки. */
                    Range range = WorkSheet.Range[WorkSheet.Cells[row, 1], WorkSheet.Cells[maxRow, periodDictionary.Max(v => v.Value)]];
                    if (customerCount % 2 == 0)
                    {
                        range.Interior.Color = XlRgbColor.rgbLightYellow;
                    }
                    else
                    {
                        range.Interior.Color = XlRgbColor.rgbLightGreen;
                    }
                    
                    
                    if (maxRow > row)
                    {                        
                        MergeCell(WorkSheet, row, 1, maxRow, 1);
                    }
                    else
                    {
                        MergeCell(WorkSheet, row, 1, row, 1);
                    }
                    row = maxRow;

                    row++;
                }
                
                Range rng = WorkSheet.Range[WorkSheet.Cells[5, 1], WorkSheet.Cells[row, column]];
                Borders border = rng.Borders;
                border.LineStyle = XlLineStyle.xlContinuous;

                WorkSheet.Columns.AutoFit();
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
            range.Merge(System.Type.Missing);
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
