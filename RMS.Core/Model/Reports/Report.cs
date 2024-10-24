using DevExpress.Xpo;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using System;

namespace RMS.Core.Model.Reports
{
    /// <summary>
    /// Отчеты.
    /// </summary>
    public class Report : XPObject
    {
        public Report() { }
        public Report(Session session) : base(session) { }

        /// <summary>
        /// Индекс формы.
        /// </summary>
        [DisplayName("Индекс формы"), Size(128)]
        public string FormIndex { get; set; }

        /// <summary>
        /// Наименование формы.
        /// </summary>
        [DisplayName("Наименование"), Size(256)]
        public string Name { get; set; }

        /// <summary>
        /// Периодичность формы.
        /// </summary>
        [DisplayName("Периодичность")]
        public Periodicity Periodicity { get; set; }

        /// <summary>
        /// Срок сдачи формы.
        /// </summary>
        [DisplayName("Срок сдачи"), Size(512)]
        public string Deadline { get; set; }

        /// <summary>
        /// Комментарий.
        /// </summary>
        [DisplayName("Комментарий"), Size(4000)]
        public string Comment { get; set; }

        /// <summary>
        /// Общероссийский классификатор управленческой документации (ОКУД).
        /// </summary>
        [DisplayName("ОКУД"), Size(128)]
        public string OKUD { get; set; }

        /// <summary>
        /// Действует до.
        /// </summary>
        [DisplayName("Действует до")]
        public DateTime? DateTo { get; set; }

        /// <summary>
        /// Предварительные налоги.
        /// </summary>
        [DisplayName("Предварительные налоги")]
        public bool IsInputTax { get; set; }

        /// <summary>
        /// Годовой отчет.
        /// </summary>
        [DisplayName("Годовой отчет")]
        public bool IsReportAnnual { get; set; }

        /// <summary>
        /// Полу-годовой отчет.
        /// </summary>
        [DisplayName("Полу-годовой отчет")]
        public bool IsReportSemiAnnual { get; set; }

        /// <summary>
        /// Квартальный отчет.
        /// </summary>
        [DisplayName("Квартальный отчет")]
        public bool IsReportQuarterly { get; set; }

        /// <summary>
        /// Месячный отчет.
        /// </summary>
        [DisplayName("Месячный отчет")]
        public bool IsReportMonthly { get; set; }

        /// <summary>
        /// Отчет сдаётся в тот же месяц, в котором он создаётся.
        /// </summary>
        [DisplayName("Отчет сдаётся в тот же месяц, в котором он создаётся")]
        [MemberDesignTimeVisibility(false)]
        public bool? IsReportSubmissionMonthInSameMonthAsCreation { get; set; }

        /// <summary>
        /// График сдачи отчета.
        /// </summary>
        [Aggregated]
        [Association]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<ReportSchedule> ReportSchedules
        {
            get
            {
                return GetCollection<ReportSchedule>(nameof(ReportSchedules));
            }
        }

        /// <summary>
        /// Показатели.
        /// </summary>
        [Aggregated]
        [Association]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<ReportPerformanceIndicator> ReportPerformanceIndicators
        {
            get
            {
                return GetCollection<ReportPerformanceIndicator>(nameof(ReportPerformanceIndicators));
            }
        }

        /// <summary>
        /// Перевод текста в перечислитель.
        /// </summary>
        /// <param name="text">Полученный текст.</param>
        /// <returns>Имеющуюся периодичность.</returns>
        public static Periodicity GetPeriodicity(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return default;

            foreach (Periodicity periodicity in Enum.GetValues(typeof(Periodicity)))
                if (text.Equals(periodicity.GetEnumDescription()))
                    return periodicity;

            return default;
        }

        public override string ToString()
        {
            if (!string.IsNullOrWhiteSpace(FormIndex))
            {
                return FormIndex;
            }
            else if (!string.IsNullOrWhiteSpace(Name))
            {
                return Name;
            }
            else
            {
                return "Не указаны индекс формы и наименование";
            }
        }
    }
}
