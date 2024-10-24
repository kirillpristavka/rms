using DevExpress.Xpo;
using OfficeOpenXml;
using RMS.Core.Controllers.Staffs;
using System.IO;
using System.Linq;

namespace RMS.Core.Controller.Print
{
    public class CustomerVacationStatistics
    {
        public CustomerVacationStatistics()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public async System.Threading.Tasks.Task<string> GetReportAsync()
        {
            var tempFile = Path.GetTempFileName();
            tempFile = tempFile.Replace(".tmp", ".xlsx");

            using (var package = new ExcelPackage(tempFile))
            {
                var sheet = package.Workbook.Worksheets.Add("Лист 1");

                var row = 1;
                sheet.Cells[row, 1].Value = "Сотрудник";
                sheet.Cells[row, 2].Value = "Период";
                sheet.Cells[row, 3].Value = "Вид отпуска";
                sheet.Cells[row, 4].Value = "Использовано";
                sheet.Cells[row, 5].Value = "Остаток";

                sheet.Cells[row, 1, row, 5].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                sheet.Cells[row, 1, row, 5].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                sheet.Cells[row, 1, row, 5].Style.Font.Bold = true;

                row++;

                using (var uof = new UnitOfWork())
                {
                    var staffs = await StaffController.GetStaffsAsync(uof, isOnlyWorking: true);
                    staffs = staffs.OrderBy(o => o.Surname).ThenBy(t => t.Name).ThenBy(t => t.Patronymic).ToList();

                    foreach (var staff in staffs)
                    {
                        var countStaffUseRow = 0;

                        var vacantions = staff.Vacations;
                        var confirmVacantions = vacantions.Where(w => w.IsConfirm);
                        if (confirmVacantions.Count() > 0)
                        {
                            sheet.Cells[row, 1].Value = staff.ToString();
                            foreach (var periodGroup in confirmVacantions.GroupBy(g => g.Period))
                            {
                                foreach (var typeGroup in periodGroup.GroupBy(f => f.VacationTypeName))
                                {
                                    sheet.Cells[row, 2].Value = periodGroup.Key;

                                    var typeName = typeGroup.Key;
                                    sheet.Cells[row, 3].Value = typeName;

                                    var useDay = typeGroup.Sum(s => s.Duration);
                                    sheet.Cells[row, 4].Value = useDay;

                                    if (!string.IsNullOrWhiteSpace(typeName) 
                                        && typeName.Trim().Equals("ежегодный", System.StringComparison.OrdinalIgnoreCase))
                                    {
                                        var restDay = 28 - useDay;
                                        sheet.Cells[row, 5].Value = restDay;
                                    }                                    

                                    countStaffUseRow++;
                                    row++;
                                }
                            }

                            if (countStaffUseRow > 1)
                            {
                                sheet.Cells[row - countStaffUseRow, 1, row, 1].Merge = true;
                            }
                        }
                    }
                }

                row--;
                sheet.Cells[2, 1, row, 3].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                sheet.Cells[2, 1, row, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                sheet.Cells[1, 1, row, 5].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                sheet.Cells[1, 1, row, 5].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                sheet.Cells[1, 1, row, 5].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                sheet.Cells[1, 1, row, 5].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                sheet.Column(1).Width = 20;
                sheet.Column(2).Width = 25;
                sheet.Column(3).Width = 15;
                sheet.Column(4).Width = 15;
                sheet.Column(5).Width = 15;
                package.Save();
            }

            return tempFile;
        }
    }
}
