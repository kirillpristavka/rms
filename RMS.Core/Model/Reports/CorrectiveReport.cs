using DevExpress.Xpo;
using RMS.Core.Extensions;

namespace RMS.Core.Model.Reports
{
    public class CorrectiveReport : XPObject
    {
        public CorrectiveReport() { }
        public CorrectiveReport(Session session) : base(session) { }

        [DisplayName(" ")]
        public string Fault
        {
            get
            {
                if (ReportChange != null)
                {
                    if (ReportChange.IsOurFault)
                    {
                        return "Наша вина";
                    }

                    if (ReportChange.IsCustomerFault)
                    {
                        return "Вина клиента";
                    }
                }

                return default;
            }
        }

        [DisplayName("Период")]
        public string PeriodString
        {
            get
            {
                if (ReportChange != null)
                {
                    return $"{ReportChange?.PeriodString} {ReportChange?.DeliveryYear} г.";
                }

                return default;
            }
        }

        [DisplayName("Отчет")]
        public string ReportString => ReportChange?.ReportString;

        [DisplayName("Статус")]
        public string StatusString => ReportChange?.StatusString;

        [DisplayName("Клиент")]
        public string ReportCustomer => ReportChange?.CustomerString;

        /// <summary>
        /// Номер корректировки.
        /// </summary>
        [DisplayName("Номер корректировки")]
        public string CorrectiveNumber { get; set; }
        
        [Size(2048)]
        [DisplayName("Причина")]
        public string Cause { get; set; }

        [DisplayName("Сдал")]
        public string PassedStaffString => PassedStaff?.ToString();
        /// <summary>
        /// Пользователь, сдавший отчет.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Staff PassedStaff { get; set; }

        [DisplayName("Комментарий")]
        public string Comment => ReportChange?.Comment;

        /// <summary>
        /// Корректирующий отчет.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public ReportChange ReportChange { get; set; }
        
        /// <summary>
        /// Первоначальный отчет.
        /// </summary>
        [Association]
        [MemberDesignTimeVisibility(false)]
        public ReportChange CurrentReportChange { get; set; }

        public override string ToString()
        {
            var result = default(string);

            if (ReportChange != null)
            {
                if (ReportChange.DateCompletion != null)
                {
                    result += $"{ReportChange.DateCompletion.Value.ToShortDateString()} - ";
                }

                result += $" {ReportChange.StatusReport.GetEnumDescription()}".Trim();
            }

            var number = CorrectiveNumber;

            var isUseText = false;
            if (string.IsNullOrWhiteSpace(number) || !string.IsNullOrWhiteSpace(Cause))
            {
                result += " (";
                isUseText = true;
            }
            
            if (!string.IsNullOrWhiteSpace(number))
            {
                result += $"№ {number}";
            }

            if (!string.IsNullOrWhiteSpace(Cause))
            {
                result += $" - {Cause}";
            }

            if (isUseText)
            {
                result += ")";
            }

            if (!string.IsNullOrWhiteSpace(result))
            {
                return result.Trim();
            }

            return base.ToString();            
        }
    }
}