using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using RMS.Core.Enumerator;
using RMS.Core.Interface;
using RMS.Core.Model.InfoContract;
using RMS.Core.Model.OKVED;
using RMS.Core.Model.Taxes;
using System;
using System.Diagnostics;

namespace RMS.Core.Model.InfoCustomer
{
    /// <summary>
    /// Фильтр для раздела клиенты.
    /// </summary>
    public class CustomerFilter : XPObject
    {
        public CustomerFilter() { }
        public CustomerFilter(Session session) : base(session) { }

        /// <summary>
        /// Глобальная фильтрация.
        /// Фильтрация происходит по всем разделам и проверяются все параметры.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public bool IsGlobalFiltering { get; set; } = false;

        /// <summary>
        /// Номер фильтра (используется для сортировки)
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public int? Number { get; set; }
        
        /// <summary>
        /// Отображение в модуле клиенты.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public bool IsVisibleCustomer { get; set; }

        /// <summary>
        /// Отображение в модуле сделки.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public bool IsVisibleDeal { get; set; }
        
        /// <summary>
        /// Отображение в модуле задач.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public bool IsVisibleTask { get; set; }

        /// <summary>
        /// Отображение в модуле договора.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public bool IsVisibleContract { get; set; }

        [MemberDesignTimeVisibility(false)]
        public CriterionVariant CriterionVariantFilter { get; set; }
        
        [MemberDesignTimeVisibility(false)]
        public CriterionVariant CriterionVariantFilterCustomer { get; set; }
        
        [MemberDesignTimeVisibility(false)]
        public CriterionVariant CriterionVariantFilterContract { get; set; }

        /// <summary>
        /// Наименование.
        /// </summary>
        [DisplayName("Наименование"), Size(256)]
        public string Name { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        [DisplayName("Описание"), Size(1024)]
        public string Description { get; set; }
        
        [MemberDesignTimeVisibility(false)]
        public CriterionVariant CriterionVariantAccountantResponsible { get; set; }

        /// <summary>
        /// Статусы.
        /// </summary>
        [Association, Aggregated, MemberDesignTimeVisibility(false)]
        public XPCollection<CustomerFilterAccountantResponsible> AccountantResponsible
        {
            get
            {
                return GetCollection<CustomerFilterAccountantResponsible>(nameof(AccountantResponsible));
            }
        }

        [MemberDesignTimeVisibility(false)]
        public CriterionVariant CriterionVariantPrimaryResponsible { get; set; }

        /// <summary>
        /// Статусы.
        /// </summary>
        [Association, Aggregated, MemberDesignTimeVisibility(false)]
        public XPCollection<CustomerFilterPrimaryResponsible> PrimaryResponsible
        {
            get
            {
                return GetCollection<CustomerFilterPrimaryResponsible>(nameof(PrimaryResponsible));
            }
        }

        [MemberDesignTimeVisibility(false)]
        public CriterionVariant CriterionVariantBankResponsible { get; set; }

        /// <summary>
        /// Ответственные за ЗП.
        /// </summary>
        [Association, Aggregated, MemberDesignTimeVisibility(false)]
        public XPCollection<CustomerFilterSalaryResponsible> SalaryResponsible
        {
            get
            {
                return GetCollection<CustomerFilterSalaryResponsible>(nameof(SalaryResponsible));
            }
        }

        [MemberDesignTimeVisibility(false)]
        public CriterionVariant CriterionVariantSalaryResponsible { get; set; }

        /// <summary>
        /// Статусы.
        /// </summary>
        [Association, Aggregated, MemberDesignTimeVisibility(false)]
        public XPCollection<CustomerFilterBankResponsible> BankResponsible
        {
            get
            {
                return GetCollection<CustomerFilterBankResponsible>(nameof(BankResponsible));
            }
        }

        [MemberDesignTimeVisibility(false)]
        public CriterionVariant CriterionVariantStatus { get; set; }

        /// <summary>
        /// Статусы.
        /// </summary>
        [Association, Aggregated, MemberDesignTimeVisibility(false)]
        public XPCollection<CustomerFilterStatus> Status
        {
            get
            {
                return GetCollection<CustomerFilterStatus>(nameof(Status));
            }
        }

        [MemberDesignTimeVisibility(false)]
        public CriterionVariant CriterionVariantTaxSystems { get; set; }

        /// <summary>
        /// Системы налогообложения.
        /// </summary>
        [Association, Aggregated, MemberDesignTimeVisibility(false)]
        public XPCollection<CustomerFilterTaxSystem> TaxSystems
        {
            get
            {
                return GetCollection<CustomerFilterTaxSystem>(nameof(TaxSystems));
            }
        }

        [MemberDesignTimeVisibility(false)]
        public CriterionVariant CriterionVariantFormCorporations { get; set; }

        /// <summary>
        /// ОПФ.
        /// </summary>
        [Association, Aggregated, MemberDesignTimeVisibility(false)]
        public XPCollection<CustomerFilterFormCorporation> FormCorporations
        {
            get
            {
                return GetCollection<CustomerFilterFormCorporation>(nameof(FormCorporations));
            }
        }

        [MemberDesignTimeVisibility(false)]
        public CriterionVariant CriterionVariantClassOKVED2 { get; set; }

        /// <summary>
        /// Вид деятельности.
        /// </summary>
        [Association, Aggregated, MemberDesignTimeVisibility(false)]
        public XPCollection<CustomerFilterClassOKVED2> ClassOKVED2
        {
            get
            {
                return GetCollection<CustomerFilterClassOKVED2>(nameof(ClassOKVED2));
            }
        }

        [MemberDesignTimeVisibility(false)]
        public CriterionVariant CriterionVariantElectronicReporting { get; set; }

        /// <summary>
        /// Вид деятельности.
        /// </summary>
        [Association, Aggregated, MemberDesignTimeVisibility(false)]
        public XPCollection<CustomerFilterElectronicReporting> ElectronicReporting
        {
            get
            {
                return GetCollection<CustomerFilterElectronicReporting>(nameof(ElectronicReporting));
            }
        }

        [MemberDesignTimeVisibility(false)]
        public CriterionVariant CriterionVariantTaxes { get; set; }

        /// <summary>
        /// Налог на алкоголь.
        /// </summary>
        public bool IsTaxAlcohol { get; set; }

        /// <summary>
        /// Налог на прибыль.
        /// </summary>
        public bool IsTaxIncome { get; set; }

        /// <summary>
        /// Косвенные налоги.
        /// </summary>
        public bool IsTaxIndirect { get; set; }

        /// <summary>
        /// Земельный налог.
        /// </summary>
        public bool IsTaxLand { get; set; }

        /// <summary>
        /// Налог на имущество.
        /// </summary>
        public bool IsTaxProperty { get; set; }

        /// <summary>
        /// Транспортный налог.
        /// </summary>
        public bool IsTaxTransport { get; set; }

        /// <summary>
        /// Единый налог на временный доход (ЕНВД).
        /// </summary>
        public bool IsTaxSingleTemporaryIncome { get; set; }

        /// <summary>
        /// Патент.
        /// </summary>
        public bool IsPatent { get; set; }

        /// <summary>
        /// Используется день заработной платы.
        /// </summary>
        public bool IsSalary { get; set; }

        /// <summary>
        /// День заработной платы.
        /// </summary>
        public int? SalaryDay { get; set; }

        /// <summary>
        /// Используется день аванса.
        /// </summary>
        public bool IsImprest { get; set; }
        /// <summary>
        /// День аванса.
        /// </summary>
        public int? ImprestDay { get; set; }

        /// <summary>
        /// Пользователи.
        /// </summary>
        [Association, Aggregated, MemberDesignTimeVisibility(false)]
        public XPCollection<CustomerFilterUser> Users
        {
            get
            {
                return GetCollection<CustomerFilterUser>(nameof(Users));
            }
        }

        /// <summary>
        /// Группы пользователей.
        /// </summary>
        [Association, Aggregated, MemberDesignTimeVisibility(false)]
        public XPCollection<CustomerFilterUserGroup> UserGroups
        {
            get
            {
                return GetCollection<CustomerFilterUserGroup>(nameof(UserGroups));
            }
        }

        [MemberDesignTimeVisibility(false)]
        public CriterionVariant CriterionVariantContractStatus { get; set; }
        public DateTime? ContractDateConclusionSince { get; set; }
        public DateTime? ContractDateConclusionTo { get; set; }
        public DateTime? ContractTerminationSince { get; set; }
        public DateTime? ContractTerminationTo { get; set; }

        /// <summary>
        /// Статус договора клиента.
        /// </summary>
        [Association, Aggregated, MemberDesignTimeVisibility(false)]
        public XPCollection<CustomerFilterContractStatus> ContractStatus
        {
            get
            {
                return GetCollection<CustomerFilterContractStatus>(nameof(ContractStatus));
            }
        }

        #region Свойства для фильтра по сделкам.
        
        [MemberDesignTimeVisibility(false)]
        public CriterionVariant CriterionVariantFilterDeal { get; set; }

        /// <summary>
        /// Статус сделки.
        /// </summary>
        [Association, Aggregated, MemberDesignTimeVisibility(false)]
        public XPCollection<CustomerFilterStatusDeal> StatusDeal
        {
            get
            {
                return GetCollection<CustomerFilterStatusDeal>(nameof(StatusDeal));
            }
        }

        [MemberDesignTimeVisibility(false)]
        public CriterionVariant CriterionVariantStatusDeal { get; set; }

        /// <summary>
        /// Клиент по сделки.
        /// </summary>
        [Association, Aggregated, MemberDesignTimeVisibility(false)]
        public XPCollection<CustomerFilterCustomerDeal> CustomerDeal
        {
            get
            {
                return GetCollection<CustomerFilterCustomerDeal>(nameof(CustomerDeal));
            }
        }

        [MemberDesignTimeVisibility(false)]
        public CriterionVariant CriterionVariantCustomerDeal { get; set; }

        /// <summary>
        /// Ответственный по сделки.
        /// </summary>
        [Association, Aggregated, MemberDesignTimeVisibility(false)]
        public XPCollection<CustomerFilterStaffDeal> StaffDeal
        {
            get
            {
                return GetCollection<CustomerFilterStaffDeal>(nameof(StaffDeal));
            }
        }

        [MemberDesignTimeVisibility(false)]
        public CriterionVariant CriterionVariantStaffDeal { get; set; }

        #endregion

        #region Свойства для фильтра по задачам.

        [MemberDesignTimeVisibility(false)]
        public CriterionVariant CriterionVariantFilterTask { get; set; }

        /// <summary>
        /// Статус задачи.
        /// </summary>
        [Association]
        [Aggregated]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<CustomerFilterStatusTask> StatusTask
        {
            get
            {
                return GetCollection<CustomerFilterStatusTask>(nameof(StatusTask));
            }
        }
        
        [MemberDesignTimeVisibility(false)]
        public CriterionVariant CriterionVariantStatusTask { get; set; }

        /// <summary>
        /// Тип задачи.
        /// </summary>
        [Association]
        [Aggregated]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<CustomerFilterTypeTask> TypeTask
        {
            get
            {
                return GetCollection<CustomerFilterTypeTask>(nameof(TypeTask));
            }
        }

        [MemberDesignTimeVisibility(false)]
        public CriterionVariant CriterionVariantTypeTask { get; set; }

        /// <summary>
        /// Клиент задачи.
        /// </summary>
        [Association]
        [Aggregated]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<CustomerFilterCustomerTask> CustomerTask
        {
            get
            {
                return GetCollection<CustomerFilterCustomerTask>(nameof(CustomerTask));
            }
        }

        [MemberDesignTimeVisibility(false)]
        public CriterionVariant CriterionVariantCustomerTask { get; set; }

        /// <summary>
        /// Постановщик задачи.
        /// </summary>
        [Association]
        [Aggregated]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<CustomerFilterGivenStaffTask> GivenStaffTask
        {
            get
            {
                return GetCollection<CustomerFilterGivenStaffTask>(nameof(GivenStaffTask));
            }
        }
        
        [MemberDesignTimeVisibility(false)]
        public CriterionVariant CriterionVariantGivenStaffTask { get; set; }

        /// <summary>
        /// Ответственный за задачи.
        /// </summary>
        [Association]
        [Aggregated]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<CustomerFilterStaffTask> StaffTask
        {
            get
            {
                return GetCollection<CustomerFilterStaffTask>(nameof(StaffTask));
            }
        }        

        [MemberDesignTimeVisibility(false)]
        public CriterionVariant CriterionVariantStaffTask { get; set; }

        /// <summary>
        /// Соисполнитель задачи.
        /// </summary>
        [Association]
        [Aggregated]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<CustomerFilterCoExecutorTask> CoExecutorTask
        {
            get
            {
                return GetCollection<CustomerFilterCoExecutorTask>(nameof(CoExecutorTask));
            }
        }        

        [MemberDesignTimeVisibility(false)]
        public CriterionVariant CriterionVariantCoExecutorTask { get; set; }

        #endregion

        /// <summary>
        /// Возвращает готовый GroupOperator для отбора в модуле клиенты.
        /// </summary>
        public GroupOperator GetGroupOperatorCustomer()
        {
            var groupOperator = new GroupOperator(GetGroupOperatorType(CriterionVariantFilterCustomer));
            
            GetGroupOperatorXpCollection<CustomerFilterAccountantResponsible, Staff>(ref groupOperator, AccountantResponsible, GetGroupOperatorType(CriterionVariantAccountantResponsible), nameof(AccountantResponsible));
            GetGroupOperatorXpCollection<CustomerFilterPrimaryResponsible, Staff>(ref groupOperator, PrimaryResponsible, GetGroupOperatorType(CriterionVariantPrimaryResponsible), nameof(PrimaryResponsible));
            GetGroupOperatorXpCollection<CustomerFilterBankResponsible, Staff>(ref groupOperator, BankResponsible, GetGroupOperatorType(CriterionVariantBankResponsible), nameof(BankResponsible));
            GetGroupOperatorXpCollection<CustomerFilterSalaryResponsible, Staff>(ref groupOperator, SalaryResponsible, GetGroupOperatorType(CriterionVariantSalaryResponsible), nameof(SalaryResponsible));

            GetGroupOperatorXpCollection<CustomerFilterStatus, Status, CustomerStatus>(ref groupOperator, Status, GetGroupOperatorType(CriterionVariantStatus));
            GetGroupOperatorXpCollection<CustomerFilterTaxSystem, TaxSystem, TaxSystemCustomer>(ref groupOperator, TaxSystems, GetGroupOperatorType(CriterionVariantTaxSystems));
            GetGroupOperatorXpCollection<CustomerFilterFormCorporation, FormCorporation>(ref groupOperator, FormCorporations, GetGroupOperatorType(CriterionVariantFormCorporations));
            GetGroupOperatorXpCollection<CustomerFilterClassOKVED2, ClassOKVED2, KindActivity>(ref groupOperator, ClassOKVED2, GetGroupOperatorType(CriterionVariantClassOKVED2));
            GetGroupOperatorXpCollection<CustomerFilterElectronicReporting, ElectronicReporting>(ref groupOperator, ElectronicReporting, GetGroupOperatorType(CriterionVariantElectronicReporting));
            GetGroupOperatorTaxes(ref groupOperator);

            CriteriaOperatorNotNull(ref groupOperator, IsImprest, nameof(ImprestDay));
            CriteriaOperatorNotNull(ref groupOperator, IsSalary, nameof(SalaryDay));
            
            GetGroupOperator(ref groupOperator, SalaryDay, nameof(SalaryDay));            
            GetGroupOperator(ref groupOperator, ImprestDay, nameof(ImprestDay));

            GetGroupOperatorDate(ref groupOperator, ContractDateConclusionSince, true, nameof(Contract.Date), nameof(Customer.Contracts));
            GetGroupOperatorDate(ref groupOperator, ContractDateConclusionTo, false, nameof(Contract.Date), nameof(Customer.Contracts));
            GetGroupOperatorDate(ref groupOperator, ContractTerminationSince, true, nameof(Contract.DateTermination), nameof(Customer.Contracts));
            GetGroupOperatorDate(ref groupOperator, ContractTerminationTo, false, nameof(Contract.DateTermination), nameof(Customer.Contracts));
            GetGroupOperatorXpCollection<CustomerFilterContractStatus, ContractStatus, Contract>(ref groupOperator, ContractStatus, GetGroupOperatorType(CriterionVariantContractStatus), true, nameof(Customer.Contracts));

            if (groupOperator.Operands.Count > 0)
            {
                var criteria = GroupOperator();
                criteria.Operands.Add(groupOperator);
                return criteria;
            }
            else
            {
                return default;
            }
        }
        
        /// <summary>
        /// Возвращает готовый GroupOperator для отбора в модуле договора.
        /// </summary>
        public GroupOperator GetGroupOperatorContract()
        {
            var groupOperator = new GroupOperator(GetGroupOperatorType(CriterionVariantFilterContract));

            GetGroupOperatorXpCollection<CustomerFilterAccountantResponsible, Staff>(ref groupOperator, AccountantResponsible, GetGroupOperatorType(CriterionVariantAccountantResponsible), nameof(AccountantResponsible), nameof(Customer));
            GetGroupOperatorXpCollection<CustomerFilterPrimaryResponsible, Staff>(ref groupOperator, PrimaryResponsible, GetGroupOperatorType(CriterionVariantPrimaryResponsible), nameof(PrimaryResponsible), nameof(Customer));
            GetGroupOperatorXpCollection<CustomerFilterBankResponsible, Staff>(ref groupOperator, BankResponsible, GetGroupOperatorType(CriterionVariantBankResponsible), nameof(BankResponsible), nameof(Customer));

            GetGroupOperatorXpCollection<CustomerFilterStatus, Status, CustomerStatus>(ref groupOperator, Status, GetGroupOperatorType(CriterionVariantStatus), superiorClassName: nameof(Customer));
            GetGroupOperatorXpCollection<CustomerFilterTaxSystem, TaxSystem, TaxSystemCustomer>(ref groupOperator, TaxSystems, GetGroupOperatorType(CriterionVariantTaxSystems), superiorClassName: nameof(Customer));
            GetGroupOperatorXpCollection<CustomerFilterFormCorporation, FormCorporation>(ref groupOperator, FormCorporations, GetGroupOperatorType(CriterionVariantFormCorporations), superiorClassName: nameof(Customer));
            GetGroupOperatorXpCollection<CustomerFilterClassOKVED2, ClassOKVED2, KindActivity>(ref groupOperator, ClassOKVED2, GetGroupOperatorType(CriterionVariantClassOKVED2), superiorClassName: nameof(Customer));
            GetGroupOperatorXpCollection<CustomerFilterElectronicReporting, ElectronicReporting>(ref groupOperator, ElectronicReporting, GetGroupOperatorType(CriterionVariantElectronicReporting), superiorClassName: nameof(Customer));
            GetGroupOperatorTaxes(ref groupOperator, nameof(Customer));

            CriteriaOperatorNotNull(ref groupOperator, IsSalary, nameof(SalaryDay), nameof(Customer));
            CriteriaOperatorNotNull(ref groupOperator, IsImprest, nameof(ImprestDay), nameof(Customer));
            
            GetGroupOperator(ref groupOperator, SalaryDay, nameof(SalaryDay), nameof(Customer));
            GetGroupOperator(ref groupOperator, ImprestDay, nameof(ImprestDay), nameof(Customer));

            GetGroupOperatorDate(ref groupOperator, ContractDateConclusionSince, true, nameof(Contract.Date));
            GetGroupOperatorDate(ref groupOperator, ContractDateConclusionTo, false, nameof(Contract.Date));
            GetGroupOperatorDate(ref groupOperator, ContractTerminationSince, true, nameof(Contract.DateTermination));
            GetGroupOperatorDate(ref groupOperator, ContractTerminationTo, false, nameof(Contract.DateTermination));
            GetGroupOperatorXpCollection<CustomerFilterContractStatus, ContractStatus>(ref groupOperator, ContractStatus, GetGroupOperatorType(CriterionVariantContractStatus));

            if (groupOperator.Operands.Count > 0)
            {
                var criteria = GroupOperator();
                criteria.Operands.Add(groupOperator);
                return criteria;
            }
            else
            {
                return default;
            }
        }

        /// <summary>
        /// Возвращает готовый GroupOperator для отбора в модуле сделки.
        /// </summary>
        public GroupOperator GetGroupOperatorDeal()
        {
            var groupOperator = new GroupOperator(GetGroupOperatorType(CriterionVariantFilterDeal));
            
            GetGroupOperatorXpCollection<CustomerFilterStatusDeal, DealStatus>(ref groupOperator, StatusDeal, GetGroupOperatorType(CriterionVariantStatusDeal), nameof(DealStatus));
            GetGroupOperatorXpCollection<CustomerFilterCustomerDeal, Customer>(ref groupOperator, CustomerDeal, GetGroupOperatorType(CriterionVariantCustomerDeal));
            GetGroupOperatorXpCollection<CustomerFilterStaffDeal, Staff>(ref groupOperator, StaffDeal, GetGroupOperatorType(CriterionVariantStaffDeal));

            //if (IsGlobalFiltering)
            //{
            //    GetGroupOperatorXpCollection<CustomerFilterAccountantResponsible, Staff>(ref groupOperator, AccountantResponsible, GetGroupOperatorType(CriterionVariantAccountantResponsible), nameof(AccountantResponsible), nameof(Customer));
            //    GetGroupOperatorXpCollection<CustomerFilterPrimaryResponsible, Staff>(ref groupOperator, PrimaryResponsible, GetGroupOperatorType(CriterionVariantPrimaryResponsible), nameof(PrimaryResponsible), nameof(Customer));
            //    GetGroupOperatorXpCollection<CustomerFilterBankResponsible, Staff>(ref groupOperator, BankResponsible, GetGroupOperatorType(CriterionVariantBankResponsible), nameof(BankResponsible), nameof(Customer));

            //    GetGroupOperatorXpCollection<CustomerFilterStatus, Status, CustomerStatus>(ref groupOperator, Status, GetGroupOperatorType(CriterionVariantStatus), superiorClassName: nameof(Customer));
            //    GetGroupOperatorXpCollection<CustomerFilterTaxSystem, TaxSystem, TaxSystemCustomer>(ref groupOperator, TaxSystems, GetGroupOperatorType(CriterionVariantTaxSystems), superiorClassName: nameof(Customer));
            //    GetGroupOperatorXpCollection<CustomerFilterFormCorporation, FormCorporation>(ref groupOperator, FormCorporations, GetGroupOperatorType(CriterionVariantFormCorporations), superiorClassName: nameof(Customer));
            //    GetGroupOperatorXpCollection<CustomerFilterClassOKVED2, ClassOKVED2, KindActivity>(ref groupOperator, ClassOKVED2, GetGroupOperatorType(CriterionVariantClassOKVED2), superiorClassName: nameof(Customer));
            //    GetGroupOperatorXpCollection<CustomerFilterElectronicReporting, ElectronicReporting>(ref groupOperator, ElectronicReporting, GetGroupOperatorType(CriterionVariantElectronicReporting), superiorClassName: nameof(Customer));
            //    GetGroupOperatorTaxes(ref groupOperator, nameof(Customer));

            //    CriteriaOperatorNotNull(ref groupOperator, IsSalary, nameof(SalaryDay), nameof(Customer));
            //    CriteriaOperatorNotNull(ref groupOperator, IsImprest, nameof(ImprestDay), nameof(Customer));

            //    GetGroupOperator(ref groupOperator, SalaryDay, nameof(SalaryDay), nameof(Customer));
            //    GetGroupOperator(ref groupOperator, ImprestDay, nameof(ImprestDay), nameof(Customer));

            //    GetGroupOperatorDate(ref groupOperator, ContractDateConclusionSince, true, nameof(Contract.Date), $"{nameof(Deal.Customer)}.{nameof(Customer.Contracts)}");
            //    GetGroupOperatorDate(ref groupOperator, ContractDateConclusionTo, false, nameof(Contract.Date), $"{nameof(Deal.Customer)}.{nameof(Customer.Contracts)}");
            //    GetGroupOperatorDate(ref groupOperator, ContractTerminationSince, true, nameof(Contract.DateTermination), $"{nameof(Deal.Customer)}.{nameof(Customer.Contracts)}");
            //    GetGroupOperatorDate(ref groupOperator, ContractTerminationTo, false, nameof(Contract.DateTermination), $"{nameof(Deal.Customer)}.{nameof(Customer.Contracts)}");
            //    GetGroupOperatorXpCollection<CustomerFilterContractStatus, ContractStatus, Contract>(ref groupOperator, ContractStatus, GetGroupOperatorType(CriterionVariantContractStatus), true, $"{nameof(Deal.Customer)}.{nameof(Customer.Contracts)}");
            //}

            if (groupOperator.Operands.Count > 0)
            {
                var criteria = GroupOperator();
                criteria.Operands.Add(groupOperator);
                return criteria;
            }
            else
            {
                return default;
            }
        }

        /// <summary>
        /// Возвращает готовый GroupOperator для отбора в модуле сделки.
        /// </summary>
        public GroupOperator GetGroupOperatorTask()
        {
            var groupOperator = new GroupOperator(GetGroupOperatorType(CriterionVariantFilterTask));            
            GetGroupOperatorXpCollection<CustomerFilterStatusTask, TaskStatus>(ref groupOperator, StatusTask, GetGroupOperatorType(CriterionVariantStatusTask));
            GetGroupOperatorXpCollection<CustomerFilterTypeTask, TypeTask>(ref groupOperator, TypeTask, GetGroupOperatorType(CriterionVariantTypeTask));
            GetGroupOperatorXpCollection<CustomerFilterCustomerTask, Customer>(ref groupOperator, CustomerTask, GetGroupOperatorType(CriterionVariantCustomerTask));
            GetGroupOperatorXpCollection<CustomerFilterGivenStaffTask, Staff>(ref groupOperator, GivenStaffTask, GetGroupOperatorType(CriterionVariantGivenStaffTask));

            var groupOperatorStaff = new GroupOperator(GroupOperatorType.Or);            
            GetGroupOperatorXpCollection<CustomerFilterStaffTask, Staff>(ref groupOperatorStaff, StaffTask, GetGroupOperatorType(CriterionVariantStaffTask));
            GetGroupOperatorXpCollection<CustomerFilterCoExecutorTask, Staff>(ref groupOperatorStaff, CoExecutorTask, GetGroupOperatorType(CriterionVariantCoExecutorTask), nameof(Task.CoExecutor));
            if (groupOperatorStaff.Operands.Count > 0)
            {
                groupOperator.Operands.Add(groupOperatorStaff);
            }
            
            //if (IsGlobalFiltering)
            //{
            //    GetGroupOperatorXpCollection<CustomerFilterAccountantResponsible, Staff>(ref groupOperator, AccountantResponsible, GetGroupOperatorType(CriterionVariantAccountantResponsible), nameof(AccountantResponsible), nameof(Customer));
            //    GetGroupOperatorXpCollection<CustomerFilterPrimaryResponsible, Staff>(ref groupOperator, PrimaryResponsible, GetGroupOperatorType(CriterionVariantPrimaryResponsible), nameof(PrimaryResponsible), nameof(Customer));
            //    GetGroupOperatorXpCollection<CustomerFilterBankResponsible, Staff>(ref groupOperator, BankResponsible, GetGroupOperatorType(CriterionVariantBankResponsible), nameof(BankResponsible), nameof(Customer));

            //    GetGroupOperatorXpCollection<CustomerFilterStatus, Status, CustomerStatus>(ref groupOperator, Status, GetGroupOperatorType(CriterionVariantStatus), superiorClassName: nameof(Customer));
            //    GetGroupOperatorXpCollection<CustomerFilterTaxSystem, TaxSystem, TaxSystemCustomer>(ref groupOperator, TaxSystems, GetGroupOperatorType(CriterionVariantTaxSystems), superiorClassName: nameof(Customer));
            //    GetGroupOperatorXpCollection<CustomerFilterFormCorporation, FormCorporation>(ref groupOperator, FormCorporations, GetGroupOperatorType(CriterionVariantFormCorporations), superiorClassName: nameof(Customer));
            //    GetGroupOperatorXpCollection<CustomerFilterClassOKVED2, ClassOKVED2, KindActivity>(ref groupOperator, ClassOKVED2, GetGroupOperatorType(CriterionVariantClassOKVED2), superiorClassName: nameof(Customer));
            //    GetGroupOperatorXpCollection<CustomerFilterElectronicReporting, ElectronicReporting>(ref groupOperator, ElectronicReporting, GetGroupOperatorType(CriterionVariantElectronicReporting), superiorClassName: nameof(Customer));
            //    GetGroupOperatorTaxes(ref groupOperator, nameof(Customer));

            //    CriteriaOperatorNotNull(ref groupOperator, IsSalary, nameof(SalaryDay), nameof(Customer));
            //    CriteriaOperatorNotNull(ref groupOperator, IsImprest, nameof(ImprestDay), nameof(Customer));

            //    GetGroupOperator(ref groupOperator, SalaryDay, nameof(SalaryDay), nameof(Customer));
            //    GetGroupOperator(ref groupOperator, ImprestDay, nameof(ImprestDay), nameof(Customer));

            //    GetGroupOperatorDate(ref groupOperator, ContractDateConclusionSince, true, nameof(Contract.Date), $"{nameof(Task.Customer)}.{nameof(Customer.Contracts)}");
            //    GetGroupOperatorDate(ref groupOperator, ContractDateConclusionTo, false, nameof(Contract.Date), $"{nameof(Task.Customer)}.{nameof(Customer.Contracts)}");
            //    GetGroupOperatorDate(ref groupOperator, ContractTerminationSince, true, nameof(Contract.DateTermination), $"{nameof(Task.Customer)}.{nameof(Customer.Contracts)}");
            //    GetGroupOperatorDate(ref groupOperator, ContractTerminationTo, false, nameof(Contract.DateTermination), $"{nameof(Task.Customer)}.{nameof(Customer.Contracts)}");
            //    GetGroupOperatorXpCollection<CustomerFilterContractStatus, ContractStatus, Contract>(ref groupOperator, ContractStatus, GetGroupOperatorType(CriterionVariantContractStatus), true, $"{nameof(Task.Customer)}.{nameof(Customer.Contracts)}");
            //}

            if (groupOperator.Operands.Count > 0)
            {
                var criteria = GroupOperator();
                criteria.Operands.Add(groupOperator);
                return criteria;
            }
            else
            {
                return default;
            }
        }

        public GroupOperator GroupOperator()
        {
            return new GroupOperator(GetGroupOperatorType(CriterionVariantFilter));
        }

        private void GetGroupOperatorDate(ref GroupOperator groupOperator,
                                          DateTime? obj,
                                          bool isMore,
                                          string nameProperty,
                                          string superiorClassName = default)
        {
            if (obj != null)
            {
                var date = Convert.ToDateTime(obj);
                var criteriaOperator = default(CriteriaOperator);

                if (!string.IsNullOrWhiteSpace(superiorClassName))
                {
                    if (isMore)
                    {
                        criteriaOperator = new ContainsOperator(superiorClassName, CriteriaOperator.Parse($"[{nameProperty}] >= ?", date));
                    }
                    else
                    {
                        criteriaOperator = new ContainsOperator(superiorClassName, CriteriaOperator.Parse($"[{nameProperty}] <= ?", date));
                    }
                }
                else
                {
                    if (isMore)
                    {
                        criteriaOperator = CriteriaOperator.Parse($"[{nameProperty}] >= ?", date);
                    }
                    else
                    {
                        criteriaOperator = CriteriaOperator.Parse($"[{nameProperty}] <= ?", date);
                    }
                }                

                groupOperator.Operands.Add(criteriaOperator);
            }
        }

        private void GetGroupOperatorTaxes(ref GroupOperator groupOperator, string superiorClassName = default)
        {
            var groupOperatorTaxes = new GroupOperator(GetGroupOperatorType(CriterionVariantTaxes));

            CriteriaOperatorTaxes(ref groupOperatorTaxes, IsTaxAlcohol, nameof(Tax.TaxAlcohol), superiorClassName);
            CriteriaOperatorTaxes(ref groupOperatorTaxes, IsTaxIncome, nameof(Tax.TaxIncome), superiorClassName);
            CriteriaOperatorTaxes(ref groupOperatorTaxes, IsTaxIndirect, nameof(Tax.TaxIndirect), superiorClassName);
            CriteriaOperatorTaxes(ref groupOperatorTaxes, IsTaxLand, nameof(Tax.TaxLand), superiorClassName);
            CriteriaOperatorTaxes(ref groupOperatorTaxes, IsTaxProperty, nameof(Tax.TaxProperty), superiorClassName);
            CriteriaOperatorTaxes(ref groupOperatorTaxes, IsTaxTransport, nameof(Tax.TaxTransport), superiorClassName);
            CriteriaOperatorTaxes(ref groupOperatorTaxes, IsTaxSingleTemporaryIncome, nameof(Tax.TaxSingleTemporaryIncome), superiorClassName);
            CriteriaOperatorTaxes(ref groupOperatorTaxes, IsPatent, nameof(Tax.Patent), superiorClassName);

            if (groupOperatorTaxes.Operands.Count > 0)
            {
                groupOperator.Operands.Add(groupOperatorTaxes);
            }
        }

        /// <summary>
        /// Оператор который проверяет значения на null.
        /// </summary>
        /// <param name="groupOperator"></param>
        /// <param name="isUse"></param>
        /// <param name="nameProperty"></param>
        /// <param name="superiorClassName"></param>
        private void CriteriaOperatorNotNull(ref GroupOperator groupOperator, bool isUse, string nameProperty, string superiorClassName = default)
        {
            try
            {
                if (isUse)
                {
                    var criteria = default(CriteriaOperator);

                    if (!string.IsNullOrWhiteSpace(superiorClassName))
                    {
                        criteria = new UnaryOperator(UnaryOperatorType.Not, new NullOperator($"{superiorClassName}.{nameProperty}"));
                    }
                    else
                    {
                        criteria = new UnaryOperator(UnaryOperatorType.Not, new NullOperator(nameProperty));
                    }

                    groupOperator.Operands.Add(criteria);
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private void CriteriaOperatorTaxes(ref GroupOperator groupOperator, bool isUse, string nameProperty, string superiorClassName = default)
        {
            try
            {
                if (isUse)
                {
                    var criteria = default(CriteriaOperator);
                    if (!string.IsNullOrWhiteSpace(superiorClassName))
                    {
                        criteria = new BinaryOperator($"{superiorClassName}.{nameof(Customer.Tax)}.{nameProperty}.{nameof(ITax.IsUse)}", true);
                    }
                    else
                    {
                        criteria = new BinaryOperator($"{nameof(Customer.Tax)}.{nameProperty}.{nameof(ITax.IsUse)}", true);
                    }
                     
                    groupOperator.Operands.Add(criteria);
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }            
        }

        private GroupOperatorType GetGroupOperatorType(CriterionVariant criterionVariant)
        {
            if (criterionVariant == CriterionVariant.And)
            {
                return GroupOperatorType.And;
            }
            else
            {
                return GroupOperatorType.Or;
            }
        }

        private void GetGroupOperator<T>(ref GroupOperator groupOperator, T data, string nameProperty, string superiorClassName = default)
        {
            if (data != null)
            {
                var criteria = default(CriteriaOperator);
                if (!string.IsNullOrWhiteSpace(superiorClassName))
                {
                    criteria = new BinaryOperator($"{superiorClassName}.{nameProperty}", data);
                }
                else
                {
                    criteria = new BinaryOperator(nameProperty, data);
                }
                
                groupOperator.Operands.Add(criteria);
            }
        }

        private void GetGroupOperatorXpCollection<T1, T2>(ref GroupOperator groupOperator,
                                                          XPCollection<T1> xpcollection,
                                                          GroupOperatorType groupOperatorType = GroupOperatorType.Or,
                                                          string nameProperty = default,
                                                          string superiorClassName = default)
            where T1 : XPObject
        {
            try
            {
                if (xpcollection != null && xpcollection.Count > 0)
                {
                    var nameXpObjectT2 = typeof(T2).Name;

                    if (!string.IsNullOrWhiteSpace(nameProperty))
                    {
                        nameXpObjectT2 = nameProperty;
                    }

                    var xpcollectionGroupOperator = new GroupOperator(groupOperatorType);

                    foreach (var item in xpcollection)
                    {
                        var obj = item.GetMemberValue(nameXpObjectT2);
                        if (obj is XPObject xPObject)
                        {
                            CriteriaOperator criteria;
                            if (!string.IsNullOrWhiteSpace(superiorClassName))
                            {
                                criteria = new BinaryOperator($"{superiorClassName}.{nameXpObjectT2}.{nameof(XPObject.Oid)}", xPObject.Oid);
                            }
                            else
                            {
                                criteria = new BinaryOperator($"{nameXpObjectT2}.{nameof(XPObject.Oid)}", xPObject.Oid);
                            }

                            xpcollectionGroupOperator.Operands.Add(criteria);
                        }
                        else if (obj is T2 enumObj)
                        {
                            CriteriaOperator criteria;
                            if (!string.IsNullOrWhiteSpace(superiorClassName))
                            {
                                criteria = new BinaryOperator($"{superiorClassName}.{nameXpObjectT2}", enumObj);
                            }
                            else
                            {
                                criteria = new BinaryOperator($"{nameXpObjectT2}", enumObj);
                            }

                            xpcollectionGroupOperator.Operands.Add(criteria);
                        }
                    }
                    groupOperator.Operands.Add(xpcollectionGroupOperator);
                }
            }
            catch (Exception ex)
            {
                RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
            }
        }

        private void GetGroupOperatorXpCollection<T1, T2, T3>(ref GroupOperator groupOperator,
                                                              XPCollection<T1> xpcollection,
                                                              GroupOperatorType groupOperatorType = GroupOperatorType.Or,
                                                              bool isContainsOperator = false,
                                                              string namePropertyContains = default,
                                                              string superiorClassName = default)
            where T1 : XPObject
        {
            if (xpcollection != null && xpcollection.Count > 0)
            {
                var nameXpObjectT2 = typeof(T2).Name;
                var nameXpObjectT3 = typeof(T3).Name;

                if (!string.IsNullOrWhiteSpace(namePropertyContains))
                {
                    nameXpObjectT3 = namePropertyContains;
                }
                
                var xpcollectionGroupOperator = new GroupOperator(groupOperatorType);
                
                foreach (var item in xpcollection)
                {
                    var obj = item.GetMemberValue(nameXpObjectT2);
                    if (obj is XPObject xPObject)
                    {
                        CriteriaOperator criteriaOperator;
                        if (isContainsOperator)
                        {
                            criteriaOperator = new ContainsOperator(nameXpObjectT3, new BinaryOperator($"{nameXpObjectT2}.{nameof(XPObject.Oid)}", xPObject.Oid));
                        }
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(superiorClassName))
                            {
                                criteriaOperator = new BinaryOperator($"{superiorClassName}.{nameXpObjectT3}.{nameXpObjectT2}.{nameof(XPObject.Oid)}", xPObject.Oid);
                            }
                            else
                            {
                                criteriaOperator = new BinaryOperator($"{nameXpObjectT3}.{nameXpObjectT2}.{nameof(XPObject.Oid)}", xPObject.Oid);
                            }
                        }
                        xpcollectionGroupOperator.Operands.Add(criteriaOperator);
                    }
                }
                groupOperator.Operands.Add(xpcollectionGroupOperator);
            }            
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
