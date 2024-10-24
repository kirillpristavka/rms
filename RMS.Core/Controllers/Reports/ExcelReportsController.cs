using DevExpress.Xpo;
using ExcelDataReader;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Reports;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RMS.Core.Controllers.Reports
{
    public class ExcelReportsController
    {
        public IEnumerable<CustomerReport> CustomerReports { get; private set; }
        public IEnumerable<Report> Reports { get; private set; }

        private readonly string _path;
        public ExcelReportsController(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException($"\"{nameof(path)}\" не может быть пустым или содержать только пробел.", nameof(path));
            }

            _path = path;

            Read();
        }

        public class Report : IEquatable<Report>
        {
            private string _obj;
            public Report(int position, string obj)
            {
                Position = position;

                if (string.IsNullOrWhiteSpace(obj))
                {
                    throw new ArgumentException(nameof(obj));
                }

                _obj = obj;

                GetName();
                GetPeriod();
            }

            private void GetPeriod()
            {
                var splits = _obj.Split(' ').Reverse();
                var period = new List<string>();

                foreach (var line in splits)
                {
                    if (line == "за")
                    {
                        break;
                    }
                    else
                    {
                        period.Add(line);
                    }
                }

                period.Reverse();

                Period =  string.Join(" ", period);
            }

            private void GetName()
            {
                var splits = _obj.Split(' ');
                var name = default(string);

                foreach (var line in splits)
                {
                    if (line == "за")
                    {
                        Name = name?.Trim();
                        break;
                    }
                    else
                    {
                        name += $"{line} ";
                    }
                }
            }

            public int Position { get; private set; }
            public string Name { get; private set; }
            public DateTime? Date {get; private set; }
            public string Status { get; set; }
            public string Period { get; set; }

            public void SetStatus(object obj)
            {
                Status = obj?.ToString();
            }

            public void SetDate(object obj)
            {
                if (obj is DateTime date)
                {
                    Date = date;
                }
                else
                {
                    if (DateTime.TryParse(obj?.ToString(), out DateTime parseDate))
                    {
                        Date = parseDate;
                    }
                }
            }

            public override string ToString()
            {
                var result = Name;

                if (Date is DateTime date)
                {
                    result = $"({date.ToShortDateString()}) {Name}";
                }

                return result;
            }

            public override bool Equals(object obj)
            {
                return Equals(obj as Report);
            }

            public bool Equals(Report other)
            {
                return !(other is null) &&
                       Name == other.Name;
            }

            public override int GetHashCode()
            {
                return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
            }

            public static bool operator ==(Report left, Report right)
            {
                return EqualityComparer<Report>.Default.Equals(left, right);
            }

            public static bool operator !=(Report left, Report right)
            {
                return !(left == right);
            }
        }

        public class CustomerReport
        {
            private string _obj;
            public CustomerReport(string obj, List<Report> reports = null)
            {
                if (string.IsNullOrWhiteSpace(obj))
                {
                    throw new ArgumentException(nameof(obj));
                }

                Name = GetName(obj);
                Reports.AddRange(reports);
            }

            public string Name { get; private set; }

            public List<Report> Reports { get; private set; } = new List<Report> { };
            
            private string GetName(string obj)
            {
                var result = obj
                    ?.ToUpper()
                    ?.Replace("ОБЩЕСТВО С ОГРАНИЧЕННОЙ ОТВЕТСТВЕННОСТЬЮ", "")
                    ?.Replace(", ООО", "")
                    ?.Replace(", ИП", "")
                    ?.Replace(", ОО", "")
                    ?.Replace("ООО", "")
                    ?.Replace("\"", "")
                    ?.ToString();

                if (obj.Contains("филиал"))
                {
                    var splits = result.Split(new char[] { ',' });
                    result = splits.First();
                }

                return result?.Trim();
            }

            public override string ToString()
            {
                return Name;
            }
        }

        public async System.Threading.Tasks.Task CreateReportAsync()
        {
            var reports = Reports.OrderBy(o => o.Name).Distinct();

            using (var uof = new UnitOfWork())
            {
                foreach (var report in reports)
                {
                    var obj = await new XPQuery<ReportV2>(uof).FirstOrDefaultAsync(f => f.Name == report.Name);
                    if (obj is null)
                    {
                        obj = new ReportV2(uof) 
                        {
                            Name = report.Name,
                            Description = report.Name
                        };
                        obj.Save();
                    }
                }

                await uof.CommitTransactionAsync();
            }
        }

        public async System.Threading.Tasks.Task CreateReportChangeAsync()
        {
            using (var uof = new UnitOfWork())
            {
                foreach (var customerReports in CustomerReports)
                {
                    var customer = await new XPQuery<Customer>(uof)
                        .FirstOrDefaultAsync(f => f.Name
                            .Replace("ООО", "")
                            .Replace("ОО", "")
                            .Replace("НП", "")
                            .Replace("ИП", "")
                            .Replace("БФ", "")
                            .Replace("АНО", "")
                            .Replace("\"", "")
                            .Trim() == customerReports.Name);

                    if (customer != null)
                    {
                        foreach (var report in customerReports.Reports.Where(w => !string.IsNullOrWhiteSpace(w.Status)))
                        {
                            var obj = await new XPQuery<ReportChangeCustomerV2>(uof)
                                .FirstOrDefaultAsync(f =>
                                    f.Customer != null
                                    && f.Customer.Oid == customer.Oid
                                    && f.ReportV2 != null 
                                    && f.ReportV2.Name== report.Name
                                    && f.DeliveryTime == report.Date
                                );

                            if (obj is null)
                            {
                                obj = new ReportChangeCustomerV2(uof)
                                {
                                    Customer = customer,
                                    ReportV2 = await new XPQuery<ReportV2>(uof).FirstOrDefaultAsync(f => f.Name == report.Name),
                                    AccountantResponsible = customer.AccountantResponsible,
                                    Status = report.Status,
                                    Period = report.Period
                                };

                                if (report.Date is DateTime date)
                                {
                                    obj.DeliveryTime = date;
                                }

                                obj.Save();
                            }
                        }
                    }
                }

                await uof.CommitTransactionAsync();
            }
        }

        public void Read()
        {
            var reports = new List<Report>();
            var customerReports = new List<CustomerReport>();

            using (var stream = File.Open(_path, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    do
                    {
                        if (reader.Name == "Лист1")
                        {
                            var i = 0;
                            while (reader.Read())
                            {
                                i++;

                                if (i < 11)
                                {
                                    continue;
                                }

                                if (i == 11)
                                {
                                    GetReports(reports, reader);
                                }

                                if (i == 12)
                                {
                                    GetReportsDate(reports, reader);
                                }

                                if (i > 12)
                                {
                                    GetCustomerReports(reports, customerReports, reader);
                                }                                
                            }
                        }                        
                    } 
                    while (reader.NextResult());
                }
            }

            Reports = reports;
            CustomerReports = customerReports;;
        }

        private static void GetCustomerReports(List<Report> reports, List<CustomerReport> customerReports, IExcelDataReader reader)
        {
            var name = reader.GetString(0);
            if (!string.IsNullOrWhiteSpace(name))
            {
                var customerReport = new CustomerReport(name, reports);
                foreach (var item in customerReport.Reports)
                {
                    var result = reader.GetString(item.Position);
                    item.SetStatus(result);
                }
                customerReport.Reports.RemoveAll(r => string.IsNullOrWhiteSpace(r.Status));
                customerReports.Add(customerReport);
            }
        }

        private static void GetReports(List<Report> report, IExcelDataReader reader)
        {
            var j = 1;
            var result = default(string);
            do
            {
                result = reader.GetString(j);

                if (!string.IsNullOrWhiteSpace(result))
                {
                    var obj = new Report(j, result);
                    report.Add(obj);
                }
                j++;
            }
            while (reader.FieldCount > j);
        }

        private static void GetReportsDate(List<Report> report, IExcelDataReader reader)
        {
            var j = 1;
            var result = default(string);
            do
            {
                result = reader.GetString(j);

                if (!string.IsNullOrWhiteSpace(result))
                {
                    var obj = report.FirstOrDefault(f => f.Position == j);
                    if (obj != null)
                    {
                        obj.SetDate(result);
                    }
                }
                j++;
            }
            while (reader.FieldCount > j);
        }
    }
}
