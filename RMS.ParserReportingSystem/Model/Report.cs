using RMS.ParserReportingSystem.Model.Enumerator;
using System;

namespace RMS.ParserReportingSystem.Model
{
    /// <summary>
    /// Отчет.
    /// </summary>
    public class Report
    {
        /// <summary>
        /// Создание отчета.
        /// </summary>
        /// <param name="formIndex">Индекс формы.</param>
        /// <param name="name">Наименование формы.</param>
        /// <param name="periodicity">Периодичность формы.</param>
        /// <param name="deadline">Срок сдачи формы.</param>
        /// <param name="comment">Комментарий.</param>
        /// <param name="OKUD">Общероссийский классификатор управленческой документации (ОКУД).</param>
        public Report(string formIndex, string name, Periodicity periodicity, string deadline, string comment, string OKUD, string reportingPeriod)
        {
            this.OKUD = OKUD ?? throw new ArgumentNullException(nameof(OKUD), "ОКУД не может быть пустым.");
            Name = name;
            Periodicity = periodicity;
            FormIndex = formIndex;
            Deadline = deadline;
            Comment = comment;
            ReportingPeriod = reportingPeriod;
        }

        /// <summary>
        /// Индекс формы.
        /// </summary>
        public string FormIndex { get; }

        /// <summary>
        /// Наименование формы.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Периодичность формы.
        /// </summary>
        public Periodicity Periodicity { get; }

        /// <summary>
        /// Срок сдачи формы.
        /// </summary>
        public string Deadline { get; }

        /// <summary>
        /// Отчетный период.
        /// </summary>
        public string ReportingPeriod { get; set; }

        /// <summary>
        /// Комментарий.
        /// </summary>
        public string Comment { get; }

        /// <summary>
        /// Общероссийский классификатор управленческой документации (ОКУД).
        /// </summary>
        public string OKUD { get; }

        public override string ToString()
        {
            return $"{OKUD} - {Name}";
        }
    }
}
