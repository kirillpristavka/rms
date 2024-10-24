using DevExpress.Xpo;
using RMS.Core.Extensions;
using RMS.Core.Model.InfoCustomer;
using System;

namespace RMS.Core.Model.Reports
{
    /// <summary>
    ///Статистические отчеты клиента.
    /// </summary>
    public class StatisticalReport : XPObject
    {
        public StatisticalReport() { }
        public StatisticalReport(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            CreateDate = DateTime.Now;
        }

        public void SetCreateObj(User user)
        {
            if (CreateUser is null && user != null)
            {
                CreateUser = user;
                CreateDate = DateTime.Now;
            }
        }

        [DisplayName("Пользователь")]
        public string User => CreateUser?.ToString();
        [MemberDesignTimeVisibility(false)]
        public User CreateUser { get; set; }

        [Persistent]
        [DisplayName("Дата создания")]
        [System.ComponentModel.DataAnnotations.DisplayFormat(DataFormatString = "{0:dd/MM/yyyy (HH:mm:ss)}")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Отчет сдается в текущем году.
        /// </summary>
        [DisplayName(" ")]
        public bool IsCurrentYear
        {
            get
            {                
                if (Year == DateTime.Now.Year)
                {
                    return true;
                }

                return false;
            }
        }
        
        [DisplayName("Год сдачи")]
        public int Year { get; set; }

        [DisplayName("Индекс формы")]
        public string ReportFormIndex => Report?.FormIndex;

        [DisplayName("Наименование формы")]
        public string ReportName => Report?.Name;

        [DisplayName("Периодичность")]
        public string ReportPeriodicity => Report?.Periodicity.GetEnumDescription();

        [DisplayName("Сроки сдачи")]
        public string ReportDeadline => Report?.Deadline;

        [DisplayName("ОКУД")]
        public string ReportOKUD => Report?.OKUD;
        /// <summary>
        /// Отчет.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Report Report { get; set; }

        [DisplayName("Ответственный")]
        public string ResponsibleString => Responsible?.ToString();
        /// <summary>
        /// Ответственный.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Staff Responsible { get; set; }
        
        [DisplayName("Комментарий")]
        public string ReportComment => Report?.Comment;

        [Association]
        [MemberDesignTimeVisibility(false)]
        public Customer Customer { get; set; }
        
        [DisplayName("Сформированные отчеты")]
        public string StatisticalReportObjs
        {
            get
            {
                var result = default(string);
                if (StatisticalReportObj != null && StatisticalReportObj.Count > 0)
                {
                    var i = 1;
                    foreach (var item in StatisticalReportObj)
                    {
                        result += $"{i}. {item}{Environment.NewLine}";
                        i++;
                    }
                }

                return result?.Trim();
            }
        }

        [Aggregated]
        [Association]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<StatisticalReportObj> StatisticalReportObj
        {
            get
            {
                return GetCollection<StatisticalReportObj>(nameof(StatisticalReportObj));
            }
        }

        public override string ToString()
        {
            return Report?.FormIndex;
        }
    }
}
