using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using RMS.Core.Enumerator;
using RMS.Core.Model.Chronicle.Taxes;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RMS.Core.Model.Taxes
{
    /// <summary>
    /// Патент.
    /// </summary>
    public class Patent : XPObject
    {
        public Patent() { }
        public Patent(Session session) : base(session) { }

        /// <summary>
        /// Коллекция всех возможных патентов.
        /// </summary>
        [Association]
        [Aggregated]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<PatentObject> PatentObjects
        {
            get
            {
                return GetCollection<PatentObject>(nameof(PatentObjects));
            }
        }

        /// <summary>
        /// Хроника изменений патентов.
        /// </summary>
        [Association]
        [Aggregated]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<ChroniclePatent> ChroniclePatents
        {
            get
            {
                var sortCollection = new SortingCollection();
                sortCollection.Add(new SortProperty(nameof(ChroniclePatent.DateCreate), SortingDirection.Descending));

                var collection = GetCollection<ChroniclePatent>(nameof(ChroniclePatents));
                collection.Sorting = sortCollection;

                return collection;
            }
        }

        /// <summary>
        /// Комментарий.
        /// </summary>
        [DisplayName("Комментарий")]
        [Size(1024)]
        [MemberDesignTimeVisibility(false)]
        public string Comment { get; set; }
        
        [MemberDesignTimeVisibility(false)]
        public Tax Tax { get; set; }

        public List<PatentObject> GetPatentObjects(int year)
        {
            var dateSince = new DateTime(year, 1, 1);
            var dateTo = new DateTime(year, 12, 31);

            return GetPatentObjects(dateSince, dateTo);
        }

        public List<PatentObject> GetPatentObjects(int year, PeriodReportChange periodReportChange)
        {
            var dateSince = default(DateTime?);
            var dateTo = default(DateTime?);

            if (periodReportChange == PeriodReportChange.FIRSTQUARTER)
            {
                dateSince = new DateTime(year, 1, 1);
                dateTo = new DateTime(year, 3, 31);
            }
            else if (periodReportChange == PeriodReportChange.SECONDQUARTER)
            {
                dateSince = new DateTime(year, 4, 1);
                dateTo = new DateTime(year, 6, 30);
            }
            else if (periodReportChange == PeriodReportChange.THIRDQUARTER)
            {
                dateSince = new DateTime(year, 7, 1);
                dateTo = new DateTime(year, 9, 30);
            }
            else if (periodReportChange == PeriodReportChange.FOURTHQUARTER)
            {
                dateSince = new DateTime(year, 10, 1);
                dateTo = new DateTime(year, 12, 31);
            }
            else
            {
                return default;
            }

            return GetPatentObjects(dateSince, dateTo);
        }

        public List<PatentObject> GetPatentObjects(DateTime? dateSince, DateTime? dateTo)
        {
            try
            {
                var listObj = new List<PatentObject>();

                foreach (var obj in PatentObjects)
                {                    
                    if (obj.DateSince < obj.DateTo || obj.DateSince is null || obj.DateTo is null)
                    {
                        if (obj.DateSince is null && obj.DateTo is null)
                        {
                            listObj.Add(obj);
                        }
                        else if (obj.DateSince is null &&
                            ((obj.DateTo >= dateSince && obj.DateTo <= dateTo) || (obj.DateTo >= dateSince && obj.DateTo > dateTo)))
                        {
                            listObj.Add(obj);
                        }
                        else if (obj.DateTo is null &&
                            ((obj.DateSince >= dateSince && obj.DateSince <= dateTo) || (dateSince > obj.DateSince && dateTo > obj.DateSince)))
                        {
                            listObj.Add(obj);
                        }
                        else if (obj.DateSince >= dateSince && obj.DateSince <= dateTo && obj.DateTo >= dateSince && obj.DateTo <= dateTo)
                        {
                            listObj.Add(obj);
                        }
                        else if (obj.DateSince <= dateSince && obj.DateSince <= dateTo && obj.DateTo >= dateSince)
                        {
                            listObj.Add(obj);
                        }
                        else if (obj.DateTo >= dateTo)
                        {
                            if (obj.DateSince >= dateSince && obj.DateSince <= dateTo && obj.DateTo >= dateTo)
                            {
                                listObj.Add(obj);
                            }
                        }
                    }
                }

                return listObj;
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                return default;
            }
        }

        public override string ToString()
        {
            if (PatentObjects != null && PatentObjects.Count == 0)
            {
                return $"Нет";
            }
            else if (PatentObjects != null && PatentObjects.Count > 0)
            {
                return $"Есть {PatentObjects.Count}";
            }
            else
            {
                return default;
            }
        }
    }
}