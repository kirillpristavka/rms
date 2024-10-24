using DevExpress.Xpo;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model.InfoCustomer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RMS.Core.Model
{
    /// <summary>
    /// Сформированная архивная папка.
    /// </summary>
    public class ArchiveFolderChange : XPObject
    {
        public ArchiveFolderChange() { }
        public ArchiveFolderChange(Session session) : base(session) { }
                
        [DisplayName("Ответственный"), Size(256)]
        public string AccountantResponsibleString => AccountantResponsible?.ToString();        

        /// <summary>
        /// Ответственный.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Staff AccountantResponsible { get; set; }

        [DisplayName("Клиент"), Size(256)]
        public string CustomerString => Customer?.ToString();

        /// <summary>
        /// Клиент.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Customer Customer { get; set; }

        [DisplayName("Периоды"), Size(128)]
        public string PeriodString
        {
            get
            {
                var result = default(string);
                var list = new List<Tuple<Month?, PeriodReportChange?, int, string>>();
                
                if (Period != null)
                {
                    if (Period == PeriodArchiveFolder.YEAR)
                    {
                        list.Add(new Tuple<Month?, PeriodReportChange?, int, string>(null, null, Year, $"{Year}г."));
                    }
                    
                    if (PeriodChange !=null && (Period == PeriodArchiveFolder.HALFYEAR || Period == PeriodArchiveFolder.QUARTER))
                    {
                        list.Add(new Tuple<Month?, PeriodReportChange?, int, string>(null, PeriodChange, Year, $"{PeriodChange.GetEnumDescription()} {Year}г."));
                    }

                    if (Month != null && Period == PeriodArchiveFolder.MONTH)
                    {
                        list.Add(new Tuple<Month?, PeriodReportChange?, int, string>(Month, null, Year, $"{Month.GetEnumDescription()} {Year}г."));
                    }
                }

                if (ArchiveFolderChangePeriods != null && ArchiveFolderChangePeriods.Count > 0)
                {
                    foreach (var archiveFolderChangePeriods in ArchiveFolderChangePeriods)
                    {
                        list.Add(new Tuple<Month?, PeriodReportChange?, int, string>(
                            archiveFolderChangePeriods.Month,
                            archiveFolderChangePeriods.PeriodReportChange,
                            archiveFolderChangePeriods.Year,
                            $"{archiveFolderChangePeriods}"));
                    }
                }

                var count = list.Count;

                foreach (var item in list.OrderBy(o => o.Item1)
                        .OrderBy(o => o.Item2)
                        .OrderBy(o => o.Item3))
                {
                    count--;
                    if (count > 0)
                    {
                        result += $"{item.Item4}; ";
                    }
                    else
                    {
                        result += $"{item.Item4}";
                    }
                }

                return result;
            }
        }

        [MemberDesignTimeVisibility(false)]
        public PeriodArchiveFolder? Period { get; set; }

        [DisplayName("Год"), MemberDesignTimeVisibility(false)]
        public int Year { get; set; }

        [DisplayName("Наименование папки"), Size(256)]
        public string ArchiveFolderString => ArchiveFolder?.ToString();

        /// <summary>
        /// Архивная папка.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public ArchiveFolder ArchiveFolder { get; set; }

        [DisplayName("Состояние папки"), Size(128)]
        public string StatusArchiveFolderString => StatusArchiveFolder.GetEnumDescription();

        [MemberDesignTimeVisibility(false)]
        public StatusArchiveFolder StatusArchiveFolder { get; set; }

        [DisplayName("Месяц"), MemberDesignTimeVisibility(false)]
        public Month? Month { get; set; }

        [DisplayName("Уточнение периода"), MemberDesignTimeVisibility(false)]
        public PeriodReportChange? PeriodChange { get; set; }

        [DisplayName("Укомплектовал"), Size(256)]
        public string StaffedString => Staffed?.ToString();

        /// <summary>
        /// Укомплектовал.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Staff Staffed { get; set; }

        /// <summary>
        /// Номер коробки.
        /// </summary>
        [DisplayName("Номер коробки")]
        public int? BoxNumber { get; set; }

        /// <summary>
        /// Включено в опись.
        /// </summary>
        [DisplayName("Опись")]
        public bool IsInventory
        {
            get
            {
                if (StatusArchiveFolder == StatusArchiveFolder.ISSUED)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Дата выдачи.
        /// </summary>
        [DisplayName("Дата выдачи")]
        public DateTime? DateIssue { get; set; }

        /// <summary>
        /// Комментарий.
        /// </summary>
        [DisplayName("Комментарий"), Size(1024)]
        public string Comment { get; set; }

        [MemberDesignTimeVisibility(false)]
        public User UserCreate { get; set; }

        [MemberDesignTimeVisibility(false)]
        public DateTime DateCreate { get; set; }

        [MemberDesignTimeVisibility(false)]
        public User UserUpdate { get; set; }

        [MemberDesignTimeVisibility(false)]
        public DateTime? DateUpdate { get; set; }

        [Association, MemberDesignTimeVisibility(false)]
        public XPCollection<ArchiveFolderChangePeriod> ArchiveFolderChangePeriods
        {
            get
            {
                return GetCollection<ArchiveFolderChangePeriod>(nameof(ArchiveFolderChangePeriods));
            }
        }
        
        public override string ToString()
        {
            return $"[Год]:{Year} [Клиент]:{CustomerString} [Папка]:{ArchiveFolderString}";
        }
    }
}
