using DevExpress.Xpo;
using RMS.Core.Enumerator;
using RMS.Core.Model.Chronicle;
using RMS.Core.Model.InfoCustomer;
using System;

namespace RMS.Core.Model.InfoContract
{
    public class Contract : XPObject
    {
        public Contract() { }
        public Contract(Session session) : base(session) { }
        
        [DisplayName("Организация")]
        public string OrganizationString => Organization?.ToString();
        /// <summary>
        /// Организация.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Organization Organization { get; set; }

        [DisplayName("Номер договора")]
        public string NumberString 
        { 
            get 
            {
                var result = default(string);

                if (!string.IsNullOrWhiteSpace(Prefix))
                {
                    result += $"{Prefix} ";
                }

                result += Number;

                if (Date is DateTime date)
                {
                    result += $" {date.ToShortDateString()}";
                }

                return result;
            } 
        }

        /// <summary>
        /// Номер договора.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        [DisplayName("Номер договора"), Size(256)]
        public string Number { get; set; }

        /// <summary>
        /// Префикс.
        /// </summary>
        [DisplayName("Префикс"), Size(32)]
        [MemberDesignTimeVisibility(false)]
        public string Prefix { get; set; }

        /// <summary>
        /// Префикс.
        /// </summary>
        [DisplayName("Год"), Size(32)]
        [MemberDesignTimeVisibility(false)]
        public string Year { get; set; }

        /// <summary>
        /// Город.
        /// </summary>
        [DisplayName("Город"), Size(256)]
        public string Town { get; set; }

        /// <summary>
        /// Дата заключения.
        /// </summary>
        [DisplayName("Дата заключения")]
        public DateTime? Date { get; set; }

        /// <summary>
        /// Дата расторжения.
        /// </summary>
        [DisplayName("Дата расторжения")]
        public DateTime? DateTermination { get; set; }

        /// <summary>
        /// Дата начала действия.
        /// </summary>
        [DisplayName("Дата с")]
        public DateTime? DateSince { get; set; }

        /// <summary>
        /// Дата окончания действия.
        /// </summary>
        [DisplayName("Дата по")]
        public DateTime? DateTo { get; set; }

        [DisplayName("Клиент")]
        public string CustomerString => Customer?.ToString();
        /// <summary>
        /// Клиент.
        /// </summary>
        [Association, MemberDesignTimeVisibility(false)]
        public Customer Customer { get; set; }

        /// <summary>
        /// Статус договора.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public StatusContract StatusContract { get; set; }

        [DisplayName("Статус")]
        public string ContractStatusString => ContractStatus?.Name;

        /// <summary>
        /// Дата окончания действия.
        /// </summary>
        [DisplayName("Комментарий")]
        [Size(4000)]
        public string Comment { get; set; }

        /// <summary>
        /// Статус договора.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public ContractStatus ContractStatus { get; set; }

        [DisplayName("Шаблон печатной формы")]
        public string PlateTemplateString => PlateTemplate?.Name;
        /// <summary>
        /// Шаблон печатной формы.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public PlateTemplate PlateTemplate { get; set; }

        /// <summary>
        /// Приложение №1.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public PlateTemplate PlateTemplate1 { get; set; }

        /// <summary>
        /// Приложение №2.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public PlateTemplate PlateTemplate2 { get; set; }

        /// <summary>
        /// Приложение №3.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public PlateTemplate PlateTemplate3 { get; set; }

        /// <summary>
        /// Файл Word.
        /// </summary>
        [MemberDesignTimeVisibility(false), Aggregated]
        public File File { get; set; }

        /// <summary>
        /// Сформированные файлы. 
        /// </summary>
        [Association, Aggregated, MemberDesignTimeVisibility(false)]
        public XPCollection<ContractFile> ContractFiles
        {
            get
            {
                return GetCollection<ContractFile>(nameof(ContractFiles));
            }
        }

        /// <summary>
        /// Дополнительные соглашения.
        /// </summary>
        [Association, Aggregated, MemberDesignTimeVisibility(false)]
        public XPCollection<SupplementaryAgreement> SupplementaryAgreements
        {
            get
            {
                return GetCollection<SupplementaryAgreement>(nameof(SupplementaryAgreements));
            }
        }

        /// <summary>
        /// Приложения к договору.
        /// </summary>
        [Association, Aggregated, MemberDesignTimeVisibility(false)]
        public XPCollection<ContractAttachment> ContractAttachments
        {
            get
            {
                return GetCollection<ContractAttachment>(nameof(ContractAttachments));
            }
        }
        
        /// <summary>
        /// Хроника договора.
        /// </summary>
        [Association, Aggregated, MemberDesignTimeVisibility(false)]
        public XPCollection<ChronicleContract> ChronicleContract
        {
            get
            {
                return GetCollection<ChronicleContract>(nameof(ChronicleContract));
            }
        }

        public override string ToString()
        {
            return NumberString;            
        }
    }
}