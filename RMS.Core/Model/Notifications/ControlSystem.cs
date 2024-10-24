using DevExpress.Xpo;
using RMS.Core.Model.InfoContract;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Mail;
using RMS.Core.Model.Reports;
using RMS.Core.Model.Salary;
using RMS.Core.Model.Taxes;
using System;
using System.Threading.Tasks;

namespace RMS.Core.Model.Notifications
{
    /// <summary>
    /// Контроль.
    /// </summary>
    public class ControlSystem : XPObject
    {
        public ControlSystem() { }
        public ControlSystem(Session session) : base(session) { }

        /// <summary>
        /// Модуль.
        /// </summary>
        [Size(2048)]
        [DisplayName("Модуль")]
        public string NameModel { get; set; }

        [Size(2048)]
        [DisplayName("Наименование")]
        public string NameObj { get; set; }

        /// <summary>
        /// Уникальный идентификатор объекта.
        /// </summary>
        [DisplayName("Уникальный идентификатор объекта")]
        [MemberDesignTimeVisibility(false)]
        public int ControlSystemObjectId { get; set; }

        /// <summary>
        /// Тип объекта.
        /// </summary>
        [Size(2048)]
        [DisplayName("Тип")]
        [MemberDesignTimeVisibility(false)]
        public string TypeName { get; set; }

        /// <summary>
        /// Начало контроля.
        /// </summary>
        [DisplayName("Начало контроля")]
        public DateTime? DateSince { get; set; }

        /// <summary>
        /// Окончание контроля.
        /// </summary>
        [DisplayName("Окончание контроля")]
        public DateTime? DateTo { get; set; }

        [DisplayName("Постановщик")]
        public string StaffToString => Staff?.ToString();
        /// <summary>
        /// Постановщик.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Staff Staff { get; set; }

        [DisplayName("Комментарий")]
        public string CommentString => Letter.ByteToString(Comment);
        /// <summary>
        /// Комментарий.
        /// </summary>
        public byte[] Comment { get; set; }

        public async Task<object> GetObj(Session session)
        {
            GetTypeObj(TypeName);
            return await session.GetObjectByKeyAsync(objType, ControlSystemObjectId);
        }
        
        private Type objType;

        private void GetTypeObj(string name)
        {
            switch (name)
            {
                case nameof(Customer):
                    objType = typeof(Customer);                    
                    break;

                case nameof(BankAccess):
                    objType = typeof(BankAccess);
                    break;

                case nameof(ForeignEconomicActivity):
                    objType = typeof(ForeignEconomicActivity);
                    break;

                case nameof(OrganizationPerformance):
                    objType = typeof(OrganizationPerformance);
                    break;

                case nameof(StatisticalReport):
                    objType = typeof(StatisticalReport);
                    break;

                case nameof(CustomerEmploymentHistory):
                    objType = typeof(CustomerEmploymentHistory);
                    break;

                case nameof(Contract):
                    objType = typeof(Contract);
                    break;

                case nameof(Task):
                    objType = typeof(Task);
                    break;

                case nameof(Staff):
                    objType = typeof(Staff);
                    break;

                case nameof(ReportChange):
                    objType = typeof(ReportChange);
                    break;

                case nameof(PreTax):
                    objType = typeof(PreTax);
                    break;

                case nameof(CustomerSalaryAdvance):
                    objType = typeof(CustomerSalaryAdvance);
                    break;

                case nameof(IndividualEntrepreneursTax):
                    objType = typeof(IndividualEntrepreneursTax);
                    break;

                case nameof(Deal):
                    objType = typeof(Deal);
                    break;

                case nameof(Invoice):
                    objType = typeof(Invoice);
                    break;

                case nameof(Letter):
                    objType = typeof(Letter);
                    break;

                case nameof(ArchiveFolderChange):
                    objType = typeof(ArchiveFolderChange);
                    break;

                case nameof(RouteSheet):
                    objType = typeof(RouteSheet);
                    break;

                case nameof(TaskCourier):
                    objType = typeof(TaskCourier);
                    break;
            }
        }

        public override string ToString()
        {
            return $"[{NameModel}]";
        }
    }
}
