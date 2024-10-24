using DevExpress.Xpo;
using RMS.Core.Model.InfoCustomer;
using RMS.Core.Model.Mail;
using RMS.Core.ModelSbis.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RMS.Core.ModelSbis
{
    public class ReportSBIS : XPObject
    {
        public ReportSBIS() { }
        public ReportSBIS(Session session) : base(session) { }

        [DisplayName("Клиент")]
        public string CustomerName => Customer?.ToString();
        [MemberDesignTimeVisibility(false)]
        public Customer Customer { get; set; }

        /// <summary>
        /// ИдКомплекта (value: 8e766971-638a-4037-9ba1-1588a9eb8dd5)
        /// </summary>
        [Size(2048)]
        [DisplayName("ИдКомплекта")]
        [MemberDesignTimeVisibility(false)]
        public string Guid { get; set; }
        [DisplayName("ID Комплекта")]
        public Guid? BaseObjGuid
        {
            get
            {
                if (System.Guid.TryParse(Guid, out Guid result))
                {
                    return result;
                }

                return default;
            }
        }

        /// <summary>
        /// ЭтоУБ (value: False)
        /// </summary>
        [Size(2048)]
        [DisplayName("ЭтоУБ")]
        [MemberDesignTimeVisibility(false)]
        public string ThisUB { get; set; }
        [DisplayName("УБ")]
        public bool? BaseObjThisUB
        {
            get
            {
                if (bool.TryParse(ThisUB, out bool result))
                {
                    return result;
                }

                return default;
            }
        }

        /// <summary>
        /// ДатаВремяСоздания (value: 2023-05-16 16:36:59.875582+03)
        /// </summary>
        [Size(2048)]
        [DisplayName("ДатаВремяСоздания")]
        [MemberDesignTimeVisibility(false)]
        public string DateOfCreation { get; set; }
        [DisplayName("Дата создания")]
        public DateTime? BaseObjDateOfCreation
        {
            get
            {
                if (DateTime.TryParse(DateOfCreation, out DateTime result))
                {
                    return result;
                }

                return default;
            }
        }

        /// <summary>
        /// Прочитан (value: True)
        /// </summary>
        [Size(2048)]
        [DisplayName("Прочитан")]
        [MemberDesignTimeVisibility(false)]
        public string IsRead { get; set; }
        [DisplayName("Прочитано")]
        public bool? BaseObjIsRead
        {
            get
            {
                if (bool.TryParse(IsRead, out bool result))
                {
                    return result;
                }

                return default;
            }
        }

        /// <summary>
        /// ТипДокумента (value: RequirementFNS)
        /// </summary>
        [Size(2048)]
        [DisplayName("ТипДокумента")]
        public string DocumentType { get; set; }

        /// <summary>
        /// ПодТипДокумента (value: 1184002)
        /// </summary>
        [Size(2048)]
        [DisplayName("ПодТипДокумента")]
        public string SubDocumentType { get; set; }

        /// <summary>
        /// Актуальность (value: True)
        /// </summary>
        [Size(2048)]
        [DisplayName("Актуальность")]
        [MemberDesignTimeVisibility(false)]
        public string Relevance { get; set; }
        [DisplayName("Актуально")]
        public bool? BaseObjRelevance
        {
            get
            {
                if (bool.TryParse(Relevance, out bool result))
                {
                    return result;
                }

                return default;
            }
        }

        /// <summary>
        /// Комплект.Редактируется (value: True)
        /// </summary>
        [Size(2048)]
        [DisplayName("Комплект.Редактируется")]
        [MemberDesignTimeVisibility(false)]
        public string KitEditAbility { get; set; }
        [DisplayName("Комплект Редактируется")]
        public bool? BaseObjKitEditAbility
        {
            get
            {
                if (bool.TryParse(KitEditAbility, out bool result))
                {
                    return result;
                }

                return default;
            }
        }

        /// <summary>
        /// Код (value: 5)
        /// </summary>
        [Size(2048)]
        [DisplayName("Код")]
        public string Code { get; set; }

        /// <summary>
        /// Год (value: 2023)
        /// </summary>
        [Size(2048)]
        [DisplayName("Год")]
        [MemberDesignTimeVisibility(false)]
        public string Year { get; set; }
        [DisplayName("Год отчета")]
        public int? BaseObjYear
        {
            get
            {
                if (int.TryParse(Year, out int result))
                {
                    return result;
                }

                return default;
            }
        }

        /// <summary>
        /// Параметры.НомерКорректировки (value: 0)
        /// </summary>
        [Size(2048)]
        [DisplayName("Параметры.НомерКорректировки")]
        public string ParametersAdjustmentNumber { get; set; }

        /// <summary>
        /// ОтмеченоЗавершенным (value: True)
        /// </summary>
        [Size(2048)]
        [DisplayName("ОтмеченоЗавершенным")]
        [MemberDesignTimeVisibility(false)]
        public string MarkedCompleted { get; set; }
        [DisplayName("Отмечено Завершенным")]
        public bool? BaseObjMarkedCompleted
        {
            get
            {
                if (bool.TryParse(MarkedCompleted, out bool result))
                {
                    return result;
                }

                return default;
            }
        }

        /// <summary>
        /// ОснованиеНомер (value: 8e766971-638a-4037-9ba1-1588a9eb8dd5)
        /// </summary>
        [Size(2048)]
        [DisplayName("ОснованиеНомер")]
        [MemberDesignTimeVisibility(false)]
        public string BaseNumber { get; set; }
        [DisplayName("Номер основания")]
        public Guid? BaseObjBaseNumber
        {
            get
            {
                if (System.Guid.TryParse(BaseNumber, out Guid result))
                {
                    return result;
                }

                return default;
            }
        }

        /// <summary>
        /// ТребованиеЗавершено (value: True)
        /// </summary>
        [Size(2048)]
        [DisplayName("ТребованиеЗавершено")]
        [MemberDesignTimeVisibility(false)]
        public string RequirementCompleted { get; set; }
        [DisplayName("Требование Завершено")]
        public bool? BaseObjRequirementCompleted
        {
            get
            {
                if (bool.TryParse(RequirementCompleted, out bool result))
                {
                    return result;
                }

                return default;
            }
        }

        /// <summary>
        /// ИНН (value: 470315065712)
        /// </summary>
        [Size(2048)]
        [DisplayName("ИНН")]
        public string Inn { get; set; }

        /// <summary>
        /// ИдентификаторСПП (value: 69056177)
        /// </summary>
        [Size(2048)]
        [DisplayName("ИдентификаторСПП")]
        public string NgnId { get; set; }

        /// <summary>
        /// ИдентификаторБиллинга (value: 133355830)
        /// </summary>
        [Size(2048)]
        [DisplayName("ИдентификаторБиллинга")]
        public string BillingId { get; set; }

        /// <summary>
        /// ОКПО (value: 2007693054)
        /// </summary>
        [Size(2048)]
        [DisplayName("ОКПО")]
        public string OKPO { get; set; }

        /// <summary>
        /// ОКТМО (value: 41612163)
        /// </summary>
        [Size(2048)]
        [DisplayName("ОКТМО")]
        public string OKTMO { get; set; }

        /// <summary>
        /// КомплектОснование (value: 8e766971-638a-4037-9ba1-1588a9eb8dd5)
        /// </summary>
        [Size(2048)]
        [DisplayName("КомплектОснование")]
        [MemberDesignTimeVisibility(false)]
        public string KitBase { get; set; }
        [DisplayName("Комплект Основание")]
        public Guid? BaseObjKitBase
        {
            get
            {
                if (System.Guid.TryParse(KitBase, out Guid result))
                {
                    return result;
                }

                return default;
            }
        }

        /// <summary>
        /// ТипСвязиСКомплектОснование (value: 0)
        /// </summary>
        [Size(2048)]
        [DisplayName("ТипСвязиСКомплектОснование")]
        public string TypeConnectionKitBase { get; set; }

        /// <summary>
        /// ТемаПисьма (value: Ответ на требование от 16.05.2023 № 18-07/4674)
        /// </summary>
        [Size(2048)]
        [DisplayName("ТемаПисьма")]
        public string LetterSubject { get; set; }

        /// <summary>
        /// Имя (value: Татьяна)
        /// </summary>
        [Size(2048)]
        [DisplayName("Имя")]
        public string Name { get; set; }

        /// <summary>
        /// Фамилия (value: Требух)
        /// </summary>
        [Size(2048)]
        [DisplayName("Фамилия")]
        public string Surname { get; set; }

        /// <summary>
        /// Отчество (value: Михайловна)
        /// </summary>
        [Size(2048)]
        [DisplayName("Отчество")]
        public string Patronymic { get; set; }

        /// <summary>
        /// НомерСтраховогоСвидетельства (value: 053-302-462 12)
        /// </summary>
        [Size(2048)]
        [DisplayName("НомерСтраховогоСвидетельства")]
        public string Snils { get; set; }

        /// <summary>
        /// ДокументСерия (value: 41 20)
        /// </summary>
        [Size(2048)]
        [DisplayName("ДокументСерия")]
        public string DocumentSeries { get; set; }

        /// <summary>
        /// ДокументНомер (value: 159061)
        /// </summary>
        [Size(2048)]
        [DisplayName("ДокументНомер")]
        public string DocumentNumber { get; set; }

        /// <summary>
        /// ДатаРождения (value: 1976-03-02)
        /// </summary>
        [Size(2048)]
        [DisplayName("ДатаРождения")]
        [MemberDesignTimeVisibility(false)]
        public string DateOfBirth { get; set; }
        [DisplayName("Дата Рождения")]
        public DateTime? BaseObjDateOfBirth
        {
            get
            {
                if (DateTime.TryParse(DateOfBirth, out DateTime result))
                {
                    return result;
                }

                return default;
            }
        }

        /// <summary>
        /// ИдентификаторФизЛица (value: ac3c4ea0-791d-421f-a8eb-f2ce269fc9c3)
        /// </summary>
        [Size(2048)]
        [DisplayName("ИдентификаторФизЛица")]
        [MemberDesignTimeVisibility(false)]
        public string IdentifierIndividuals { get; set; }
        [DisplayName("Идентификатор ФизЛица")]
        public Guid? BaseObjIdentifierIndividuals
        {
            get
            {
                if (System.Guid.TryParse(IdentifierIndividuals, out Guid result))
                {
                    return result;
                }

                return default;
            }
        }

        /// <summary>
        /// КПП (value: 780601001)
        /// </summary>
        [Size(2048)]
        [DisplayName("КПП")]
        public string KPP { get; set; }

        /// <summary>
        /// Параметры (value: "ReportGroupId"=>"0")
        /// </summary>
        [Size(2048)]
        [DisplayName("Параметры")]
        public string Options { get; set; }

        /// <summary>
        /// Отчет.ДопДанные 
        /// </summary>
        [NonPersistent]
        [DisplayName("Отчет.ДопДанные")]
        [MemberDesignTimeVisibility(false)]
        public string ReportAdditionalDataString { get; set; }
        [MemberDesignTimeVisibility(false)]
        public byte[] ReportAdditionalData { get; set; }
        [DisplayName("Отчет ДопДанные")]
        public string BaseObjReportAdditionalDataString
        {
            get
            {                
                return Letter.ByteToString(ReportAdditionalData);
            }
        }

        /// <summary>
        /// ДатаЛиквидации (value: 2023-01-25)
        /// </summary>
        [Size(2048)]
        [DisplayName("ДатаЛиквидации")]
        [MemberDesignTimeVisibility(false)]
        public string DateLiquidation { get; set; }
        [DisplayName("Дата Ликвидации")]
        public DateTime? BaseObjDateLiquidation
        {
            get
            {
                if (DateTime.TryParse(DateLiquidation, out DateTime result))
                {
                    return result;
                }

                return default;
            }
        }

        /// <summary>
        /// Нерасшифрованный (value: True)
        /// </summary>
        [Size(2048)]
        [DisplayName("Нерасшифрованный")]
        [MemberDesignTimeVisibility(false)]
        public string Undeciphered { get; set; }
        [DisplayName("Нерасшифрованно")]
        public bool? BaseObjUndeciphered
        {
            get
            {
                if (bool.TryParse(Undeciphered, out bool result))
                {
                    return result;
                }

                return default;
            }
        }

        /// <summary>
        /// КодФилиала (value: 1)
        /// </summary>
        [Size(2048)]
        [DisplayName("КодФилиала")]
        public string BranchCode { get; set; }

        /// <summary>
        /// ДатаП (value: 2023-08-02)
        /// </summary>
        [Size(2048)]
        [DisplayName("ДатаП")]
        [MemberDesignTimeVisibility(false)]
        public string DataP { get; set; }
        [DisplayName("Дата П")]
        public DateTime? BaseObjDateP
        {
            get
            {
                if (DateTime.TryParse(DataP, out DateTime result))
                {
                    return result;
                }

                return default;
            }
        }

        /// <summary>
        /// ДатаС (value: 2022-08-02)
        /// </summary>
        [Size(2048)]
        [DisplayName("ДатаС")]
        [MemberDesignTimeVisibility(false)]
        public string DataS { get; set; }
        [DisplayName("Дата С")]
        public DateTime? BaseObjDateS
        {
            get
            {
                if (DateTime.TryParse(DataS, out DateTime result))
                {
                    return result;
                }

                return default;
            }
        }

        /// <summary>
        /// Scope (value: 15)
        /// </summary>
        [Size(2048)]
        [DisplayName("Scope")]
        public string Scope { get; set; }

        /// <summary>
        /// WithoutFiles (value: False)
        /// </summary>
        [Size(2048)]
        [DisplayName("WithoutFiles")]
        [MemberDesignTimeVisibility(false)]
        public string WithoutFiles { get; set; }
        [DisplayName("Without Files")]
        public bool? BaseObjWithoutFiles
        {
            get
            {
                if (bool.TryParse(WithoutFiles, out bool result))
                {
                    return result;
                }

                return default;
            }
        }

        /// <summary>
        /// FromBilling (value: False)
        /// </summary>
        [Size(2048)]
        [DisplayName("FromBilling")]
        [MemberDesignTimeVisibility(false)]
        public string FromBilling { get; set; }
        [DisplayName("From Billing")]
        public bool? BaseObjFromBilling
        {
            get
            {
                if (bool.TryParse(FromBilling, out bool result))
                {
                    return result;
                }

                return default;
            }
        }

        /// <summary>
        /// Async (value: False)
        /// </summary>
        [Size(2048)]
        [DisplayName("Async")]
        [MemberDesignTimeVisibility(false)]
        public string Async { get; set; }
        [DisplayName("IsAsync")]
        public bool? BaseObjAsync
        {
            get
            {
                if (bool.TryParse(Async, out bool result))
                {
                    return result;
                }

                return default;
            }
        }

        /// <summary>
        /// OrigFileCount (value: 62692)
        /// </summary>
        [Size(2048)]
        [DisplayName("OrigFileCount")]
        public string OrigFileCount { get; set; }

        /// <summary>
        /// OriginalFileName (value: ВыгрузкаОтчетности 1_0 ООО «ФТ», ООО «НМК» и еще 124 - 1.zip)
        /// </summary>
        [Size(2048)]
        [DisplayName("OriginalFileName")]
        public string OriginalFileName { get; set; }

        /// <summary>
        /// ClientId (value: 6842817)
        /// </summary>
        [Size(2048)]
        [DisplayName("ClientId")]
        public string ClientId { get; set; }

        /// <summary>
        /// ArchiveVersion (value: 23.4218)
        /// </summary>
        [Size(2048)]
        [DisplayName("ArchiveVersion")]
        public string ArchiveVersion { get; set; }

        public void SetReportAdditionalDataToByte()
        {
            ReportAdditionalData = Letter.StringToByte(ReportAdditionalDataString);
        }

        public async void SetCustomer()
        {
            Customer = await new XPQuery<Customer>(Session).FirstOrDefaultAsync(f => f.INN == Inn);
            Save();
        }

        public void SetValue(ReportSbisModel reportSbisModel)
        {
            foreach (var property in GetSBISProperties()) 
            {
                var name = property.DisplayName;
                if (string.IsNullOrWhiteSpace(name))
                {
                    name = property.PropertyName;
                }

                var glossary = reportSbisModel.GetGlossary(name);
                if (glossary != null)
                {
                    var obj = GetType()
                        ?.GetProperties()
                        ?.FirstOrDefault(f => f.GetCustomAttributes(true)
                                ?.FirstOrDefault(fc => fc is DisplayNameAttribute displayNameAttribute 
                                    && !string.IsNullOrWhiteSpace(displayNameAttribute.DisplayName)
                                    && name == displayNameAttribute.DisplayName) != null );
                    if (obj != null)
                    {
                        var value = glossary.Value?.ToString();
                        obj.SetValue(this, value);
                    }
                }
            }

            SetReportAdditionalDataToByte();
            Save();
        }

        private class Property : IEquatable<Property>
        {
            public Property(string propertyName, string displayName)
            {
                if (string.IsNullOrWhiteSpace(propertyName))
                {
                    throw new ArgumentNullException(nameof(propertyName));
                }

                PropertyName = propertyName;
                DisplayName = displayName;
            }

            public string PropertyName { get; private set; }
            public string DisplayName { get; private set; }

            public override bool Equals(object obj)
            {
                return Equals(obj as Property);
            }

            public bool Equals(Property other)
            {
                return !(other is null) &&
                       PropertyName == other.PropertyName;
            }

            public override int GetHashCode()
            {
                return 487910435 + EqualityComparer<string>.Default.GetHashCode(PropertyName);
            }

            public override string ToString()
            {
                if (string.IsNullOrWhiteSpace(DisplayName))
                {
                    return PropertyName;
                }
                return DisplayName;
            }

            public static bool operator ==(Property left, Property right)
            {
                return EqualityComparer<Property>.Default.Equals(left, right);
            }

            public static bool operator !=(Property left, Property right)
            {
                return !(left == right);
            }
        }

        private static List<Property> _reportSBISProperty;
        private static List<Property> GetSBISProperties()
        {
            if (_reportSBISProperty is null)
            {
                _reportSBISProperty = new List<Property>();

                var properties = typeof(ReportSBIS)?.GetProperties();
                if (properties != null)
                {
                    foreach (var property in properties)
                    {
                        var propertyName = property.Name;
                        var displayName = default(string);

                        var attributes = property.GetCustomAttributes(true);
                        foreach (object attribute in attributes)
                        {
                            if (attribute is DisplayNameAttribute displayNameAttribute)
                            {
                                displayName = displayNameAttribute.DisplayName;
                                break;
                            }
                        }

                        var obj = new Property(propertyName, displayName);
                        if (!_reportSBISProperty.Contains(obj))
                        {
                            _reportSBISProperty.Add(obj);
                        }
                    }
                }
            }

            return _reportSBISProperty;
        }
    }

    public class ReportSbisModel
    {
        public ReportSbisModel(string path, string fileName, IEnumerable<Glossary> glossaries)
        {
            Path = path;
            FileName = fileName;
            Glossaries = glossaries;
        }

        public string Path { get; private set; }
        public string FileName { get; private set; }
        public IEnumerable<Glossary> Glossaries { get; private set; }

        public string DocumentType
        {
            get
            {
                return Glossaries?
                    .FirstOrDefault(f => !string.IsNullOrWhiteSpace(f.Name) 
                        && f.Name.Equals("ТипДокумента"))
                    ?.Value?.ToString();
            }
        }

        public string SubDocumentType
        {
            get
            {
                return Glossaries?
                    .FirstOrDefault(f => !string.IsNullOrWhiteSpace(f.Name)
                        && f.Name.Equals("ПодТипДокумента"))
                    ?.Value?.ToString();
            }
        }

        public override string ToString()
        {
            return $"{DocumentType}";
        }
    }
}