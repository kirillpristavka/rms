using DevExpress.Xpo;
using RMS.Core.Model.OKVED;
using RMS.Core.Model.Taxes;
using System;

namespace RMS.Core.Model.Chronicle.Taxes
{
    /// <summary>
    /// Хроника изменений патента.
    /// </summary>
    public class ChroniclePatent : XPObject
    {
        public ChroniclePatent() { }
        public ChroniclePatent(Session session) : base(session) { }
        
        /// <summary>
        /// Дата ввода.
        /// </summary>
        [DisplayName("Дата изменения")]
        [System.ComponentModel.DataAnnotations.DisplayFormat(DataFormatString = "{0:dd/MM/yyyy (HH:mm:ss)}")]
        public DateTime DateCreate { get; set; } = DateTime.Now; 

        [DisplayName("Пользователь")]
        public string UserString => User?.ToString();
        /// <summary>
        /// Пользователь внесший изменения.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public User User { get; set; }

        [DisplayName("Наименование")]
        [Size(1024)]
        public string Name { get; set; }

        /// <summary>
        /// Дата с.
        /// </summary>
        [DisplayName("Дата с")]
        public DateTime? DateSince { get; set; }

        /// <summary>
        /// Дата по.
        /// </summary>
        [DisplayName("Дата по")]
        public DateTime? DateTo { get; set; }

        [DisplayName("Налоговый орган")]
        [Size(1024)]
        [MemberDesignTimeVisibility(false)]
        public string TaxAuthority { get; set; }

        /// <summary>
        /// Вид деятельности.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public KindActivity KindActivity { get; set; }

        /// <summary>
        /// Вид деятельности.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public ClassOKVED2 ClassOKVED2 { get; set; }

        /// <summary>
        /// Дата первой оплаты.
        /// </summary>
        [DisplayName("Дата первой оплаты")]
        public DateTime? AdvancePaymentDate { get; set; }

        /// <summary>
        /// Сумма первой оплаты.
        /// </summary>
        [DisplayName("Сумма первой оплаты")]
        public decimal? AdvancePaymentValue { get; set; }

        /// <summary>
        /// Оплата #1.
        /// </summary>
        [DisplayName("Оплата #1")]
        public bool IsPaymentAdvancePaymentValue { get; set; }

        /// <summary>
        /// Дата второй оплаты.
        /// </summary>
        [DisplayName("Дата второй оплаты")]
        public DateTime? ActualPaymentDate { get; set; }

        /// <summary>
        /// Сумма второй оплаты.
        /// </summary>
        [DisplayName("Сумма второй оплаты")]
        public decimal? ActualPaymentValue { get; set; }

        /// <summary>
        /// Оплата #2.
        /// </summary>
        [DisplayName("Оплата #2")]
        public bool IsPaymentActualPaymentValue { get; set; }

        [DisplayName("Статус")]
        public string PatentString => PatentStatus?.ToString();
        /// <summary>
        /// Статус патента.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public PatentStatus PatentStatus { get; set; }

        [DisplayName("Заявление на зачет страховых 1")]
        public string AccountingInsuranceStatusString => AccountingInsuranceStatus?.ToString();
        /// <summary>
        /// Заявление на зачет страховых.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public AccountingInsuranceStatus AccountingInsuranceStatus { get; set; }
        /// <summary>
        /// Дата отправки заявления.
        /// </summary>
        [DisplayName("Дата отправки 1")]
        public DateTime? DateAccountingInsuranceStatus { get; set; }

        [DisplayName("Заявление на зачет страховых 2")]
        public string AccountingInsuranceStatusString2 => AccountingInsuranceStatus2?.ToString();
        /// <summary>
        /// Заявление на зачет страховых.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public AccountingInsuranceStatus AccountingInsuranceStatus2 { get; set; }
        /// <summary>
        /// Дата отправки заявления.
        /// </summary>
        [DisplayName("Дата отправки 2")]
        public DateTime? DateAccountingInsuranceStatus2 { get; set; }

        /// <summary>
        /// Комментарий.
        /// </summary>
        [DisplayName("Комментарий")]
        [Size(1024)]
        [MemberDesignTimeVisibility(false)]
        public string Comment { get; set; }

        [Association, MemberDesignTimeVisibility(false)]
        public Patent Patent { get; set; }
    }
}