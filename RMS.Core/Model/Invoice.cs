using DevExpress.Xpo;
using RMS.Core.Enumerator;
using RMS.Core.Extensions;
using RMS.Core.Model.InfoCustomer;
using System;
using System.Linq;

namespace RMS.Core.Model
{
    /// <summary>
    /// Счет.
    /// </summary>
    public class Invoice : XPObject
    {
        public Invoice() { }
        public Invoice(Session session) : base(session) { }

        /// <summary>
        /// Дата отправления по Email.
        /// </summary>
        [DisplayName("Отправление по Email")]
        public bool IsSent 
        { 
            get 
            {
                if (SentByEmailDate is null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            } 
        }

        /// <summary>
        /// Дата создания счета.
        /// </summary>
        [DisplayName("Дата")]
        public DateTime Date { get; set; } = DateTime.Now.Date;

        /// <summary>
        /// Номер счета.
        /// </summary>
        [DisplayName("Номер счета")]
        public string Number { get; set; }

        [DisplayName("Клиент")]
        public string CustomerString => Customer?.ToString();
        /// <summary>
        /// Клиент.
        /// </summary>
        [Association, MemberDesignTimeVisibility(false)]
        public Customer Customer { get; set; }

        decimal value;
        /// <summary>
        /// Стоимость.
        /// </summary>
        [DisplayName("Сумма")]
        public decimal Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value.GetDecimalRound();
            }
        }

        /// <summary>
        /// Утвержден.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public bool IsApproved { get; set; }

        /// <summary>
        /// Дата утверждения.
        /// </summary>
        [DisplayName("Дата утверждения")]
        public DateTime? ApprovedDate { get; set; }

        [DisplayName("Утвердивший пользователь")]
        public string ApprovedStaffString => ApprovedStaff?.ToString();
        /// <summary>
        /// Утвердивший пользователь.
        /// </summary>
        [ MemberDesignTimeVisibility(false)]
        public Staff ApprovedStaff { get; set; }

        /// <summary>
        /// Дата отправления по Email.
        /// </summary>
        [DisplayName("Дата отправления по Email")]
        [MemberDesignTimeVisibility(false)]
        public DateTime? SentByEmailDate { get; set; }

        /// <summary>
        /// Крайняя дата оплаты.
        /// </summary>
        [DisplayName("Оплатить до")]
        public DateTime DeadlineDate { get; set; } = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, 1).AddMonths(1).AddDays(-1);

        public override string ToString()
        {
            return $"Счет № {Number} от {Date.ToShortDateString()}";
        }

        /// <summary>
        /// Платежи.
        /// </summary>
        [Association, MemberDesignTimeVisibility(false), Aggregated]
        public XPCollection<InvoicePayment> Payments
        {
            get
            {
                return GetCollection<InvoicePayment>(nameof(Payments));
            }
        }

        /// <summary>
        /// Информация по счету.
        /// </summary>
        [Association, MemberDesignTimeVisibility(false), Aggregated]
        public XPCollection<InvoiceInformation> InvoiceInformations
        {
            get
            {
                return GetCollection<InvoiceInformation>(nameof(InvoiceInformations));
            }
        }

        /// <summary>
        /// Получить счет по показателям работы организации.
        /// </summary>
        /// <param name="organizationPerformance"></param>
        /// <returns></returns>
        public static Invoice GetInvoiceForPerformanceIndicators(OrganizationPerformance organizationPerformance)
        {
            var invoice = new Invoice(organizationPerformance.Session)
            {
                Date = DateTime.Now.Date,
                Customer = organizationPerformance.Customer
            };
            
            var baseValue = default(decimal);
            var comment = default(string);
            var description = default(string);
            var countOperation = default(int);

            /* Переменная для отслеживания операции с банковским счетом. */
            var isUseBankService = false;
            
            var invoiceInformations = default(InvoiceInformation);

            /* Ищем количество операций по показателю с типом [TypePerformanceIndicator.Base]. */
            foreach (var customerPerformanceIndicators in organizationPerformance.CustomerPerformanceIndicators)
            {
                if (customerPerformanceIndicators.PerformanceIndicator.TypePerformanceIndicator == TypePerformanceIndicator.Base)
                {
                    if (int.TryParse(customerPerformanceIndicators.Value, out int result))
                    {
                        countOperation += result;
                    }
                }
            }
            
            var billingInformation = organizationPerformance.Customer.BillingInformation;

            /* Проверяем есть ли по клиенту платежная информация. */
            if (billingInformation is null)
            {
                /* Если платежная информация не заполнена, то выбираем подходящее значение стоимости операций 
                 * из шкалы указанной в системе налогообложения. */

                var calculationScale = organizationPerformance.Customer.TaxSystemCustomer?.TaxSystem?.CalculationScale;
                if (calculationScale != null)
                {
                    var calculationScaleValue = calculationScale.GetPerformanceIndicatorValue(countOperation);
                    /* Если нашли в шкале подходящее для нас значение. */
                    if (calculationScaleValue != null)
                    {
                        baseValue = calculationScaleValue.Value;
                        comment = $"[Операций: {countOperation}]. Базовая ставка рассчитана из ставки налогообложения [{organizationPerformance.Customer.TaxSystemCustomer.TaxSystem}].";
                        description = $"Бухгалтерские услуги за {organizationPerformance.Month.GetEnumDescription().ToLower()} {organizationPerformance.Year} г. " +
                            $"(Тариф до {calculationScaleValue.NumberOf} операций)";
                    }
                    /* Если поиск по значению не удался. */
                    else
                    {
                        baseValue = 0M;
                        comment = $"[Операций: {countOperation}]. По шкале, заданной в системе налогообложения " +
                            $"[{organizationPerformance.Customer.TaxSystemCustomer.TaxSystem}], не удалось найти подходящего значения.";
                        description = string.Empty;
                    }
                }
                else
                {
                    /* Если в системе налогообложения не заполнена шкала, то вернем нулевые суммы. */
                    baseValue = 0M;
                    comment = $"[Операций: {countOperation}]. Нет платежной информации по клиенту и не указаны шкалы в системе налогообложения.";
                    description = string.Empty;
                }

                invoiceInformations = new InvoiceInformation(organizationPerformance.Session)
                {
                    Name = "Базовая стоимость услуг",
                    Description = description,
                    Comment = comment,
                    Sum = baseValue,
                    Price = baseValue,
                    Count = 1,
                    Unit = "шт"
                };
                invoice.InvoiceInformations.Add(invoiceInformations);
            }
            else
            {
                /* Проверяем используется ли фиксированная ставка. */
                if (billingInformation.IsFixedBaseRate)
                {
                    /* Возможно забыли задать фиксированную ставку и ее нет или она равняется 0. */
                    if (billingInformation.FixedBaseRateValue is null || billingInformation.FixedBaseRateValue == 0)
                    {
                        baseValue = 0M;
                        comment = "Фиксированная базовая ставка не задана или равняется нулю.";
                        description = string.Empty;
                    }
                    /* Если все хорошо и фиксированная ставка задана. */
                    else
                    {
                        baseValue = Convert.ToDecimal(billingInformation.FixedBaseRateValue);
                        comment = "Расчет ведется по заданной в платежной информации фиксированный базовой ставке";
                        description = $"Бухгалтерские услуги за {organizationPerformance.Month.GetEnumDescription().ToLower()} {organizationPerformance.Year} г. ";
                    }
                }
                /* Проверяем используется ли расчет по операциям. */
                else if (billingInformation.IsSettlementOfTransactions)
                {
                    /* Проверяем используется ли своя шкала для поиска значений. */
                    if (billingInformation.CalculationScale is null)
                    {
                        /* Так как шкала не указана, берем значения из шкалы системы налогообложения. */
                        var calculationScale = organizationPerformance.Customer.TaxSystemCustomer?.TaxSystem?.CalculationScale;
                        if (calculationScale != null)
                        {
                            var calculationScaleValue = calculationScale.GetPerformanceIndicatorValue(countOperation);
                            /* Если нашли в шкале подходящее для нас значение. */
                            if (calculationScaleValue != null)
                            {
                                baseValue = calculationScaleValue.Value;
                                comment = $"[Операций: {countOperation}]. Базовая ставка рассчитана из ставки налогообложения [{organizationPerformance.Customer.TaxSystemCustomer.TaxSystem}].";
                                description = $"Бухгалтерские услуги за {organizationPerformance.Month.GetEnumDescription().ToLower()} {organizationPerformance.Year} г. " +
                                    $"(Тариф от {calculationScaleValue.NumberWith} до {calculationScaleValue.NumberOf} операций)";
                            }
                            /* Если поиск по значению не удался. */
                            else
                            {
                                baseValue = 0M;
                                comment = $"[Операций: {countOperation}]. По шкале, заданной в системе налогообложения " +
                                    $"[{organizationPerformance.Customer.TaxSystemCustomer.TaxSystem}], не удалось найти подходящего значения.";
                                description = string.Empty;
                            }
                        }
                        else
                        {
                            /* Если в системе налогообложения не заполнена шкала, то вернем нулевые суммы. */
                            baseValue = 0M;
                            comment = $"[Операций: {countOperation}]. Не указаны шкалы в системе налогообложения.";
                            description = string.Empty;
                        }
                    }
                    /* Возьмем значение из указанной шкалы. */
                    else
                    {
                        var calculationScaleValue = billingInformation.CalculationScale.GetPerformanceIndicatorValue(countOperation);
                        /* Если нашли в шкале подходящее для нас значение. */
                        if (calculationScaleValue != null)
                        {
                            baseValue = calculationScaleValue.Value;
                            comment = $"[Операций: {countOperation}]. Базовая ставка рассчитана из указанной ставки [{billingInformation.CalculationScale}].";
                            description = $"Бухгалтерские услуги за {organizationPerformance.Month.GetEnumDescription().ToLower()} {organizationPerformance.Year} г. " +
                                $"(Тариф от {calculationScaleValue.NumberWith} до {calculationScaleValue.NumberOf} операций)";
                        }
                        /* Если поиск по значению не удался. */
                        else
                        {
                            baseValue = 0M;
                            comment = $"[Операций: {countOperation}]. По шкале [{billingInformation.CalculationScale}] " +
                                $"не удалось найти подходящего значения.";
                            description = string.Empty;
                        }
                    }
                }
                /* Если есть платежная информация и нет одного из двух методов расчета, то что-то пошло не так. */
                else
                {
                    throw new Exception("Ошибка формирования счета. Не найдем метод расчета.");
                }
                
                invoiceInformations = new InvoiceInformation(organizationPerformance.Session)
                {
                    Name = "Базовая стоимость услуг",
                    Description = description,
                    Comment = comment,
                    Sum = baseValue.GetDecimalRound(),
                    Price = baseValue.GetDecimalRound(),
                    Count = countOperation == 0 ? 1 : countOperation,
                    Unit = "шт"
                };
                invoice.InvoiceInformations.Add(invoiceInformations);

                var preparationPrimaryDocument = default(decimal);
                /* Проверяем учитываются ли операции по составлению первичных документов. */
                if (billingInformation.IsPreparationPrimaryDocuments)
                {
                    comment = string.Empty;
                    description = string.Empty;
                    
                    var value = billingInformation.PreparationPrimaryDocumentsValue;
                    if (value is null)
                    {
                        value = Convert.ToDecimal(20);
                        comment = "[Ставка составления первичных документов не задана или равна нулю в платежной информации по клиенту, значение заменено на 20%]. ";
                    }

                    preparationPrimaryDocument = baseValue * Convert.ToDecimal(value) / 100;
                    comment = $"{comment}В базовую ставку включена операция по составлению первичных документов: [{value}%] -> [{preparationPrimaryDocument.GetDecimalRound()}].";
                    description = "Бухгалтерские услуги (ввод первичных документов)";

                    invoiceInformations = new InvoiceInformation(organizationPerformance.Session)
                    {
                        Name = "Базовая стоимость услуг (ввод первичных документов)",
                        Description = description,
                        Comment = comment,
                        Sum = preparationPrimaryDocument.GetDecimalRound(),
                        Price = Convert.ToDecimal(value).GetDecimalRound(),
                        Count = 1,
                        Unit = "%"
                    };
                    invoice.InvoiceInformations.Add(invoiceInformations);
                }

                var customerBankServiceValue = default(decimal);
                /* Проверяем учитываются ли операции по обслуживанию банка. */
                if (billingInformation.IsCustomerBankService)
                {
                    comment = string.Empty;
                    description = string.Empty;

                    var value = billingInformation.CustomerBankServiceValue;
                    if (value is null)
                    {
                        value = Convert.ToDecimal(30);
                        comment = "[Ставка составления операций по банку не задана или равна нулю в платежной информации по клиенту, значение заменено на 30%]. ";
                    }

                    customerBankServiceValue = baseValue * Convert.ToDecimal(value) / 100;
                    comment = $"{comment}В базовую ставку включена операция по обслуживанию банка: [{value}%] -> [{customerBankServiceValue.GetDecimalRound()}].";
                    description = "Бухгалтерские услуги (обслуживание клиентского банка)";

                    isUseBankService = true;
                    invoiceInformations = new InvoiceInformation(organizationPerformance.Session)
                    {
                        Name = "Базовая стоимость услуг (обслуживание клиентского банка)",
                        Description = description,
                        Comment = comment,
                        Sum = customerBankServiceValue.GetDecimalRound(),
                        Price = Convert.ToDecimal(value).GetDecimalRound(),
                        Count = 1,
                        Unit = "%",
                        CustomerPerformanceIndicator = null,
                        CustomerServiceProvided = null
                    };
                    invoice.InvoiceInformations.Add(invoiceInformations);

                    baseValue = baseValue + preparationPrimaryDocument + customerBankServiceValue;
                }
            }

            /* Проверяем есть ли у клиента платежная информация и используются ли группы для расчета. */
            if (billingInformation != null && billingInformation.IsBillingGroupPerformanceIndicators)
            {
                foreach (var customerPerformanceIndicators in organizationPerformance.CustomerPerformanceIndicators
                .Where(w => w.PerformanceIndicator.TypePerformanceIndicator != TypePerformanceIndicator.Analysis
                    && w.PerformanceIndicator.TypePerformanceIndicator != TypePerformanceIndicator.Base
                    && w.PerformanceIndicator.IsUseWhenFormingAnInvoice == true))
                {
                    comment = string.Empty;
                    description = string.Empty;

                    var value = default(decimal);
                    var result = default(decimal);
                    var count = default(int);                    

                    /* Проверяем относится ли текущей показатель, к группе с которой разрешена работа. */
                    if (billingInformation.BillingGroupPerformanceIndicators
                        .FirstOrDefault(f => f.GroupPerformanceIndicator.Oid == customerPerformanceIndicators.PerformanceIndicator.GroupPerformanceIndicator?.Oid) != null)
                    {
                        /* Проверяем есть ли в платежной информации значение для показателя. */
                        var indicatorBilling = billingInformation.BillingPerformanceIndicators
                            .FirstOrDefault(f => f.PerformanceIndicator.Oid == customerPerformanceIndicators.PerformanceIndicator.Oid);

                        if (indicatorBilling != null)
                        {
                            if (indicatorBilling.Value is null 
                                || indicatorBilling.Value == 0M)
                            {
                                value = 0M;
                                comment = $"Показатель [{customerPerformanceIndicators.PerformanceIndicator}] присутствует в платежной информации клиента, " +
                                    $"но его значение не задано или равно нулю.";
                            }
                            else
                            {
                                value = Convert.ToDecimal(indicatorBilling.Value).GetDecimalRound();
                                comment = $"Значение показателя [{customerPerformanceIndicators.PerformanceIndicator}] берется из платежной информации клиента " +
                                    $"-> [{Convert.ToDecimal(indicatorBilling.Value).GetDecimalRound()}]";
                            }            
                        }
                        /* Если значение не указано, то берем стандартное. */
                        else
                        {
                            if (customerPerformanceIndicators.PerformanceIndicator.TypePerformanceIndicator == TypePerformanceIndicator.Sample)
                            {
                                if (int.TryParse(customerPerformanceIndicators.Value, out int intValue))
                                {
                                    var performanceIndicatorValue = customerPerformanceIndicators.PerformanceIndicator.GetPerformanceIndicatorValue(intValue);
                                    if (performanceIndicatorValue != null)
                                    {
                                        value = performanceIndicatorValue.Value.GetDecimalRound();
                                        comment = $"Показатель [{customerPerformanceIndicators.PerformanceIndicator}] " +
                                            $"с типом [{customerPerformanceIndicators.PerformanceIndicator.TypePerformanceIndicator.GetEnumDescription()}]. " +
                                            $"Значение выбрано из шкалы с {performanceIndicatorValue.NumberWith} по {performanceIndicatorValue.NumberOf}. => [{performanceIndicatorValue.Value.GetDecimalRound()}]";
                                    }
                                    else
                                    {
                                        value = 0M;
                                        value = performanceIndicatorValue.Value.GetDecimalRound();
                                        comment = $"Показатель [{customerPerformanceIndicators.PerformanceIndicator}] " +
                                            $"с типом [{customerPerformanceIndicators.PerformanceIndicator.TypePerformanceIndicator.GetEnumDescription()}]. " +
                                            $"Не найдена шкала по условию  c => {intValue}, по <= {intValue}. Значение установлено в 0. Проверьте шкалы показателя.";
                                    }                                   
                                }
                            }
                            else
                            {
                                if (customerPerformanceIndicators.PerformanceIndicator.Value is null 
                                    || customerPerformanceIndicators.PerformanceIndicator.Value == 0M)
                                {
                                    value = 0;
                                    comment = $"Показатель [{customerPerformanceIndicators.PerformanceIndicator}] " +
                                            $"с типом [{customerPerformanceIndicators.PerformanceIndicator.TypePerformanceIndicator.GetEnumDescription()}]. " +
                                            $"Значение показателя не задано или равно 0.";
                                }
                                else
                                {
                                    value = Convert.ToDecimal(customerPerformanceIndicators.PerformanceIndicator.Value).GetDecimalRound();
                                    comment = $"Показатель [{customerPerformanceIndicators.PerformanceIndicator}] " +
                                            $"с типом [{customerPerformanceIndicators.PerformanceIndicator.TypePerformanceIndicator.GetEnumDescription()}]. " +
                                            $"Взято стандартно заданное значение => [{Convert.ToDecimal(customerPerformanceIndicators.PerformanceIndicator.Value).GetDecimalRound()}]";
                                }
                            }
                        }

                        if (customerPerformanceIndicators.PerformanceIndicator.TypePerformanceIndicator == TypePerformanceIndicator.Percent)
                        {
                            if (bool.TryParse(customerPerformanceIndicators.Value, out bool isUse))
                            {
                                if (isUse)
                                {
                                    result = (baseValue * value / 100).GetDecimalRound();
                                    count = 1;
                                    comment = $"{comment}{Environment.NewLine}[База = {baseValue.GetDecimalRound()}] * [Значение = {(value / 100).GetDecimalRound()}] => {result}";
                                }
                            }
                        }
                        /* Если это банковская операция, то надо проверить не включена ли ее стоимость в сам договор. */
                        else if (customerPerformanceIndicators.PerformanceIndicator.TypePerformanceIndicator == TypePerformanceIndicator.Bank)
                        {
                            if (int.TryParse(customerPerformanceIndicators.Value, out int intValue))
                            {
                                if (isUseBankService)
                                {
                                    intValue = intValue - 1;                                    
                                    isUseBankService = false;
                                    comment = $"{comment}{Environment.NewLine}Имеется операция по обслуживанию банка включенная в базовую стоимость. [Количество = {intValue + 1}] - 1 = {intValue}";
                                }

                                result = (intValue * value).GetDecimalRound();
                                count = intValue;
                                comment = $"{comment}{Environment.NewLine}[Количество = {intValue}] * [Значение = {(value).GetDecimalRound()}] => {result}";
                            }                                
                        }
                        else
                        {                            
                            if (int.TryParse(customerPerformanceIndicators.Value, out int intValue))
                            {                                
                                result = (intValue * value).GetDecimalRound();
                                count = intValue;
                                comment = $"{comment}{Environment.NewLine}[Количество = {intValue}] * [Значение = {(value).GetDecimalRound()}] => {result}";
                            }
                        }

                        description = $"Дополнительные бухгалтерские услуги ({customerPerformanceIndicators.PerformanceIndicator.Description.ToLower()})";
                        description = $"{customerPerformanceIndicators.PerformanceIndicator.GroupPerformanceIndicator.Description} ({customerPerformanceIndicators.PerformanceIndicator.Description.ToLower()})";
                    }

                    if (result > 0)
                    {
                        var unit = default(string);
                        if (string.IsNullOrWhiteSpace(customerPerformanceIndicators.PerformanceIndicator.Unit))
                        {
                            if (customerPerformanceIndicators.PerformanceIndicator.TypePerformanceIndicator == TypePerformanceIndicator.Percent)
                            {
                                unit = "%";
                            }
                            else if (customerPerformanceIndicators.PerformanceIndicator.GroupPerformanceIndicator != null
                                && customerPerformanceIndicators.PerformanceIndicator.GroupPerformanceIndicatorString.ToLower().Contains("сотру"))
                            {
                                unit = "чел";
                            }
                            else
                            {
                                unit = "шт";
                            }
                        }
                        else
                        {
                            unit = customerPerformanceIndicators.PerformanceIndicator.Unit;
                        }
                        
                        invoiceInformations = new InvoiceInformation(organizationPerformance.Session)
                        {
                            Name = "Дополнительные услуги",
                            Description = description,
                            Comment = comment,
                            Sum = result.GetDecimalRound(),
                            Count = count,
                            Price = value.GetDecimalRound(),
                            Unit = unit,
                            CustomerPerformanceIndicator = customerPerformanceIndicators,
                            CustomerServiceProvided = null
                        };
                        invoice.InvoiceInformations.Add(invoiceInformations);
                    }
                }
            }
            
            invoice.Value = invoice.InvoiceInformations.Sum(s => s.Sum);

            return invoice;
        }
    }
}