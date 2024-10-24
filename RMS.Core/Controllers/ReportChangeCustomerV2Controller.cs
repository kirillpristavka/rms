using DevExpress.Xpo;
using RMS.Core.Extensions;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Reports;
using RMS.Core.ObjectDTO.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RMS.Core.Controllers
{
    public static class ReportChangeCustomerV2Controller
    {
        public async static Task CreateNewReportsAsync(int? year = null)
        {
            if (year is null)
            {
                year = DateTime.Now.Year;
            }

            using (var uof = new UnitOfWork())
            {
                var customers = await new XPQuery<Customer>(uof)
                    .Select(s => new CustomerDTOPartial(s.Oid, $"{s.Name} ({s.TaxSystemCustomerString})", s.DefaultName))
                    .ToListAsync();

                foreach (var customer in customers)
                {
                    var reports = await new XPQuery<ReportChangeCustomerV2>(uof)
                        .Where(w => w.Customer != null && w.Customer.Oid == customer.Oid && w.Year == year)
                        .ToListAsync();

                    if (reports is null || reports.Count == 0)
                    {
                        var baseReporst = await new XPQuery<CustomerReport>(uof)
                            .Where(w => w.Customer != null && w.Customer.Oid == customer.Oid)
                            .ToListAsync();

                        if (baseReporst.Count > 0)
                        {
                            var currentCustomer = await new XPQuery<Customer>(uof).FirstOrDefaultAsync(f => f.Oid == customer.Oid); 
                            foreach (var baseReport in baseReporst)
                            {
                                foreach (var reportSchedule in baseReport.Report?.ReportSchedules)
                                {
                                    if (reportSchedule.Day >= 1 && reportSchedule.Day <= 31)
                                    {
                                        //var newReport = new ReportChangeCustomerV2(uof)
                                        //{
                                        //    Year = year.Value,
                                        //    Customer = currentCustomer,
                                        //    Report = baseReport.Report,
                                        //    AccountantResponsible = currentCustomer.AccountantResponsible,
                                        //    DeliveryTime = new DateTime(year.Value, (int)reportSchedule.Month, reportSchedule.Day),
                                        //    Period = reportSchedule.Period.GetEnumDescription()
                                        //};
                                        //newReport.Save();
                                    }
                                }
                            }

                            try
                            {
                                await uof.CommitTransactionAsync().ConfigureAwait(false);
                            }
                            catch (Exception) { }
                        }
                    }
                }
            }
        }
    }
}
