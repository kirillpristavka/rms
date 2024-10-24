namespace RMS.UI
{
    partial class cls_App
    {
        /// <summary>
        /// Варианты справочников.
        /// </summary>
        public enum ReferenceBooks
        {
            /// <summary>
            /// Пользователи.
            /// </summary>
            User = 1,

            /// <summary>
            /// Группы пользователей.
            /// </summary>
            UserGroup = 2,

            /// <summary>
            /// Банки.
            /// </summary>
            Bank = 3,

            /// <summary>
            /// Адреса.
            /// </summary>
            Address = 4,

            /// <summary>
            /// Должности.
            /// </summary>
            Position = 5,

            /// <summary>
            /// Сотрудники.
            /// </summary>
            Staff = 6,

            /// <summary>
            /// Клиенты.
            /// </summary>
            Customer = 7,

            /// <summary>
            /// Статусы.
            /// </summary>
            Status = 8,

            /// <summary>
            /// Системы налогообложения.
            /// </summary>
            TaxSystem = 9,

            /// <summary>
            /// Состояние.
            /// </summary>
            State = 10,

            /// <summary>
            /// Организационно-правовые формы.
            /// </summary>
            FormCorporation = 11,

            /// <summary>
            /// Виды платежей.
            /// </summary>
            TypePayment = 13,

            /// <summary>
            /// Прайс.
            /// </summary>
            PriceList = 16,

            /// <summary>
            /// Отчеты.
            /// </summary>
            Report = 17,

            /// <summary>
            /// Электронная отчетность.
            /// </summary>
            ElectronicReporting = 18,

            /// <summary>
            /// Льготы.
            /// </summary>
            Privilege = 19,

            /// <summary>
            /// Физические показатели.
            /// </summary>
            PhysicalIndicator = 20,

            /// <summary>
            /// Архивные папки.
            /// </summary>
            ArchiveFolder = 21,

            /// <summary>
            /// Физическое лицо.
            /// </summary>
            Individual = 22,

            /// <summary>
            /// Маршрутные листы.
            /// </summary>
            RouteSheet = 23,

            /// <summary>
            /// Почтовый ящик.
            /// </summary>
            Mailbox = 24,

            /// <summary>
            /// Задачи для курьера.
            /// </summary>
            TaskCourier = 25,

            /// <summary>
            /// Шаблоны почтовых сообщений
            /// </summary>
            LetterTemplate = 26,

            /// <summary>
            /// Цветовые схемы.
            /// </summary>
            ColorStatus = 27,

            /// <summary>
            /// Настройка отображения таблицы клиентов.
            /// </summary>
            CustomerSettings = 28,

            /// <summary>
            /// Шаблон печатной формы.
            /// </summary>
            PlateTemplate = 29,

            /// <summary>
            /// Уставные документы.
            /// </summary>
            StatutoryDocument = 30,

            /// <summary>
            /// Право устанавливающий документ.
            /// </summary>
            TitleDocument = 31,

            /// <summary>
            /// Документы по налоговой отчетности.
            /// </summary>
            TaxReportingDocument = 32,

            /// <summary>
            /// Первичные документы.
            /// </summary>
            SourceDocument = 33,

            /// <summary>
            /// Анкетные данные сотрудников.
            /// </summary>
            EmployeeDetailsDocument = 34,

            /// <summary>
            /// Фильтры для модуля клиенты.
            /// </summary>
            CustomerFilter = 35,

            /// <summary>
            /// Раздел ОКВЭД2.
            /// </summary>
            SectionOKVED2 = 36,

            /// <summary>
            /// Класс ОКВЭД2.
            /// </summary>
            ClassOKVED2 = 37,

            /// <summary>
            /// Статусы.
            /// </summary>
            ContractStatus = 38,

            /// <summary>
            /// Коды предпринимательской деятельности ЕНВД
            /// </summary>
            EntrepreneurialActivityCodesUTII = 39,

            /// <summary>
            /// Показатель работы.
            /// </summary>
            PerformanceIndicator = 40,

            /// <summary>
            /// Группа показателей работы.
            /// </summary>
            GroupPerformanceIndicator = 41,

            /// <summary>
            /// Шкалы.
            /// </summary>
            CalculationScale = 42,

            /// <summary>
            /// Статья расходов.
            /// </summary>
            CostItem = 43,

            /// <summary>
            /// Фильтры для писем.
            /// </summary>
            LetterFilter = 44,

            /// <summary>
            /// Организация.
            /// </summary>
            Organization = 45,

            /// <summary>
            /// Каталоги для FAQ.
            /// </summary>
            FAQCatalog = 46,

            /// <summary>
            /// Выплаты или удержания.
            /// </summary>
            PayoutDictionary = 47,

            /// <summary>
            /// Задача для списка задач.
            /// </summary>
            TaskObject = 48,

            /// <summary>
            /// Статус платежей.
            /// </summary>
            StatusAccrual = 49,

            /// <summary>
            /// Статус патента.
            /// </summary>
            PatentStatus = 50,

            /// <summary>
            ///  Статус учета страховых.
            /// </summary>
            AccountingInsuranceStatus = 51,

            /// <summary>
            ///  Статус предварительных налогов.
            /// </summary>
            StatusPreTax = 52,
            
            /// <summary>
            /// Статус ИП отчетов.
            /// </summary>
            IndividualEntrepreneursTaxStatus = 53,

            /// <summary>
            /// Статус задачи.
            /// </summary>
            TaskStatus = 54,

            /// <summary>
            /// Статус сделки.
            /// </summary>
            DealStatus = 55,

            /// <summary>
            /// Общий словарь.
            /// </summary>
            GeneralVocabulary = 56,

            /// <summary>
            /// Система налогообложения для калькулятора.
            /// </summary>
            CalculatorTaxSystem = 57,

            /// <summary>
            /// Показатели для калькулятора.
            /// </summary>
            CalculatorIndicator = 58,

            /// <summary>
            /// Вид отпуска.
            /// </summary>
            VacationType = 59,
            
            /// <summary>
            /// Маршрутные листы.
            /// </summary>
            RouteSheetv2 = 60,

            /// <summary>
            /// Задачи для маршрутных листов.
            /// </summary>
            TaskRouteSheetv2 = 61,

            /// <summary>
            /// Провайдер ЭДО.
            /// </summary>
            ElectronicDocumentManagement = 63,

            /// <summary>
            /// Документы.
            /// </summary>
            Document = 64,

            /// <summary>
            /// Статусы пакетов документов. 
            /// </summary>
            PackageDocumentStatus = 65,

            /// <summary>
            /// Статусы счетов.
            /// </summary>
            AccountStatementStatus = 66,

            /// <summary>
            /// Сотрудники клиента.
            /// </summary>
            CustomerStaff = 67,

            /// <summary>
            /// Отчеты v2.
            /// </summary>
            ReportV2 = 68,
        };
    }
}

