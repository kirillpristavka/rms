using DevExpress.Xpo;
using Microsoft.Office.Interop.Excel;
using RMS.Core.Model.InfoCustomer;
using System;

namespace RMS.Core.Model.Exchange
{
    public class ImportEEU : XPObject
    {
        public ImportEEU() { }
        public ImportEEU(Session session) : base(session) { }

        [DisplayName("Дата ввоза")]
        public DateTime? ImportDate { get; set; }

        [DisplayName("Страна")]
        public string CountryName => Country?.Name;
        [MemberDesignTimeVisibility(false)]
        public Country Country { get; set; }

        [DisplayName("Оплата НДС")]
        public decimal? Payment { get; set; }

        [MemberDesignTimeVisibility(false)]
        public Customer Customer { get; set; }

        public void Edit(ImportEEU obj)
        {
            if (obj is null || obj.Customer is null)
            {
                return;
            }

            Customer = obj.Customer;
            ImportDate = obj.ImportDate;
            Country = obj.Country;
            Payment = obj.Payment;

            Save();
        }

        //[Association]
        //public XPCollection<ImportEEUReport> ImportEEUReports
        //{
        //    get
        //    {
        //        return GetCollection<ImportEEUReport>(nameof(ImportEEUReports));
        //    }
        //}
    }

    //public class ImportEEUReport : XPObject
    //{
    //    public ImportEEUReport() { }
    //    public ImportEEUReport(Session session) : base(session) { }

    //    [Association]
    //    public ImportEEU ImportEEU { get; set; }
    //}
}
