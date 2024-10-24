using DevExpress.Xpo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace RMS.Core.Model
{
    /// <summary>
    /// Адрес.
    /// </summary>
    public class Address : XPObject
    {
        public Address() { }
        public Address(Session session) : base(session) { }
        
        [DisplayName("Адрес"), Size(512)]
        public string AddressString { get; set; }

        [DisplayName("Юридический")]
        public bool IsLegal { get; set; }

        [DisplayName("Фактический")]
        public bool IsActual { get; set; }

        [DisplayName("Курьерский")]
        public bool IsExpress { get; set; }

        [MemberDesignTimeVisibility(false), DisplayName("Город"), Size(128)]
        public string Town { get; set; }

        [MemberDesignTimeVisibility(false), DisplayName("Дом"), Size(128)]
        public string Home { get; set; }

        [MemberDesignTimeVisibility(false), DisplayName("Квартира"), Size(128)]
        public string Apartment { get; set; }

        [MemberDesignTimeVisibility(false), DisplayName("Населенный пункт"), Size(128)]
        public string Locality { get; set; }

        [MemberDesignTimeVisibility(false), DisplayName("Строение"), Size(32)]
        public string Structure { get; set; }

        [MemberDesignTimeVisibility(false), DisplayName("Корпус"), Size(32)]
        public string Body { get; set; }

        [MemberDesignTimeVisibility(false), DisplayName("Почтовый индекс"), Size(32)]
        public string Postcode { get; set; }

        [MemberDesignTimeVisibility(false), DisplayName("Офис"), Size(32)]
        public string Office { get; set; }

        [MemberDesignTimeVisibility(false), DisplayName("Актуален с")]
        public DateTime? Date { get; set; }

        public override string ToString()
        {
            //return $"{Postcode}, {Region}, {City}, {Street}, {House}, {Flat}";
            return AddressString;
        }

        /// <summary>
        /// Индекс.
        /// </summary>
        [DisplayName("Индекс"), JsonProperty("postal_code"), MemberDesignTimeVisibility(false)]
        public string PostalCode { get; set; }

        /// <summary>
        /// Страна.
        /// </summary>
        [DisplayName("Страна"), JsonProperty("country"), MemberDesignTimeVisibility(false)]
        public string Country { get; set; }

        /// <summary>
        /// ISO-код страны (двухсимвольный).
        /// </summary>
        [DisplayName("ISO-код страны (двухсимвольный)"), JsonProperty("country_iso_code"), MemberDesignTimeVisibility(false)]
        public string CountryIsoCode { get; set; }

        /// <summary>
        /// Федеральный округ.
        /// </summary>
        [DisplayName("Федеральный округ"), JsonProperty("federal_district"), MemberDesignTimeVisibility(false)]
        public string FederalDistrict { get; set; }

        /// <summary>
        /// Код ФИАС региона.
        /// </summary>
        [DisplayName("Код ФИАС региона"), JsonProperty("region_fias_id"), MemberDesignTimeVisibility(false)]
        public string RegionFiasId { get; set; }

        /// <summary>
        /// Код КЛАДР региона.
        /// </summary>
        [DisplayName("Код КЛАДР региона"), JsonProperty("region_kladr_id"), MemberDesignTimeVisibility(false)]
        public string RegionKladrId { get; set; }

        /// <summary>
        /// ISO-код региона.
        /// </summary>
        [DisplayName("ISO-код региона"), JsonProperty("region_iso_code"), MemberDesignTimeVisibility(false)]
        public string RegionIsoCode { get; set; }

        /// <summary>
        /// Регион с типом.
        /// </summary>
        [DisplayName("Регион с типом"), JsonProperty("region_with_type"), MemberDesignTimeVisibility(false)]
        public string RegionWithType { get; set; }

        /// <summary>
        /// Тип региона (сокращенный).
        /// </summary>
        [DisplayName("Тип региона (сокращенный)"), JsonProperty("region_type"), MemberDesignTimeVisibility(false)]
        public string RegionType { get; set; }

        /// <summary>
        /// Тип региона.
        /// </summary>
        [DisplayName("Тип региона"), JsonProperty("region_type_full"), MemberDesignTimeVisibility(false)]
        public string RegionTypeFull { get; set; }

        /// <summary>
        /// Регион.
        /// </summary>
        [DisplayName("Регион"), JsonProperty("region"), MemberDesignTimeVisibility(false)]
        public string Region { get; set; }

        /// <summary>
        /// Код ФИАС района в регионе.
        /// </summary>
        [DisplayName("Код ФИАС района в регионе"), JsonProperty("area_fias_id"), MemberDesignTimeVisibility(false)]
        public string AreaFiasId { get; set; }

        /// <summary>
        /// Код КЛАДР района в регионе.
        /// </summary>
        [DisplayName("Код КЛАДР района в регионе"), JsonProperty("area_kladr_id"), MemberDesignTimeVisibility(false)]
        public string AreaKladrId { get; set; }

        /// <summary>
        /// Район в регионе с типом.
        /// </summary>
        [DisplayName("Район в регионе с типом"), JsonProperty("area_with_type"), MemberDesignTimeVisibility(false)]
        public string AreaWithType { get; set; }

        /// <summary>
        /// Тип района в регионе (сокращенный).
        /// </summary>
        [DisplayName("Тип района в регионе (сокращенный)"), JsonProperty("area_type"), MemberDesignTimeVisibility(false)]
        public string AreaType { get; set; }

        /// <summary>
        /// Тип района в регионе.
        /// </summary>
        [DisplayName("Тип района в регионе"), JsonProperty("area_type_full"), MemberDesignTimeVisibility(false)]
        public string AreaTypeFull { get; set; }

        /// <summary>
        /// Район в регионе
        /// </summary>
        [DisplayName("Район в регионе"), JsonProperty("area"), MemberDesignTimeVisibility(false)]
        public string Area { get; set; }

        /// <summary>
        /// Код ФИАС города.
        /// </summary>
        [DisplayName("Код ФИАС города"), JsonProperty("city_fias_id"), MemberDesignTimeVisibility(false)]
        public string CityFiasId { get; set; }

        /// <summary>
        /// Код КЛАДР города.
        /// </summary>
        [DisplayName("Код КЛАДР города"), JsonProperty("city_kladr_id"), MemberDesignTimeVisibility(false)]
        public string CityKladrId { get; set; }

        /// <summary>
        /// Город с типом.
        /// </summary>
        [DisplayName("Город с типом"), JsonProperty("city_with_type"), MemberDesignTimeVisibility(false)]
        public string CityWithType { get; set; }

        /// <summary>
        /// Тип города (сокращенный)
        /// </summary>
        [DisplayName("Тип города (сокращенный)"), JsonProperty("city_type"), MemberDesignTimeVisibility(false)]
        public string CityType { get; set; }

        /// <summary>
        /// Тип города.
        /// </summary>
        [DisplayName("Тип города"), JsonProperty("city_type_full"), MemberDesignTimeVisibility(false)]
        public string CityTypeFull { get; set; }

        /// <summary>
        /// Город.
        /// </summary>
        [DisplayName("Город"), JsonProperty("city"), MemberDesignTimeVisibility(false)]
        public string City { get; set; }

        /// <summary>
        /// Административный округ (только для Москвы).
        /// </summary>
        [DisplayName("Административный округ"), JsonProperty("city_area"), MemberDesignTimeVisibility(false)]
        public string СityArea { get; set; }

        /// <summary>
        /// Код ФИАС района города (заполняется, только если район есть в ФИАС)
        /// </summary>
        [DisplayName("Код ФИАС района города"), JsonProperty("city_district_fias_id"), MemberDesignTimeVisibility(false)]
        public string CityDistrictFiasId { get; set; }

        /// <summary>
        /// Код КЛАДР района города (не заполняется).
        /// </summary>
        [DisplayName("Код КЛАДР района города (не заполняется)"), JsonProperty("city_district_kladr_id"), MemberDesignTimeVisibility(false)]
        public string CityDistrictKladrId { get; set; }

        /// <summary>
        /// Район города с типом.
        /// </summary>
        [DisplayName("Район города с типом"), JsonProperty("city_district_with_type"), MemberDesignTimeVisibility(false)]
        public string CityDistrictWithType { get; set; }

        /// <summary>
        /// Тип района города (сокращенный).
        /// </summary>
        [DisplayName("Тип района города (сокращенный)"), JsonProperty("city_district_type"), MemberDesignTimeVisibility(false)]
        public string CityDistrictType { get; set; }

        /// <summary>
        /// Тип района города.
        /// </summary>
        [DisplayName("Тип района города"), JsonProperty("city_district_type_full"), MemberDesignTimeVisibility(false)]
        public string CityDistrictTypeFull { get; set; }

        /// <summary>
        /// Район города.
        /// </summary>
        [DisplayName("Район города"), JsonProperty("city_district"), MemberDesignTimeVisibility(false)]
        public string CityDistrict { get; set; }

        /// <summary>
        /// Код ФИАС населенного пункта.
        /// </summary>
        [DisplayName("Код ФИАС населенного пункта"), JsonProperty("settlement_fias_id"), MemberDesignTimeVisibility(false)]
        public string SettlementFiasId { get; set; }

        /// <summary>
        /// Код КЛАДР населенного пункта.
        /// </summary>
        [DisplayName("Код КЛАДР населенного пункта"), JsonProperty("settlement_kladr_id"), MemberDesignTimeVisibility(false)]
        public string SettlementKladrId { get; set; }

        /// <summary>
        /// Населенный пункт с типом.
        /// </summary>
        [DisplayName("Населенный пункт с типом"), JsonProperty("settlement_with_type"), MemberDesignTimeVisibility(false)]
        public string SettlementWithType { get; set; }

        /// <summary>
        /// Тип населенного пункта (сокращенный).
        /// </summary>
        [DisplayName("Тип населенного пункта (сокращенный)"), JsonProperty("settlement_type"), MemberDesignTimeVisibility(false)]
        public object SettlementType { get; set; }

        /// <summary>
        /// Тип населенного пункта.
        /// </summary>
        [DisplayName("Тип населенного пункта"), JsonProperty("settlement_type_full"), MemberDesignTimeVisibility(false)]
        public string SettlementTypeFull { get; set; }

        /// <summary>
        /// Населенный пункт.
        /// </summary>
        [DisplayName("Населенный пункт"), JsonProperty("settlement"), MemberDesignTimeVisibility(false)]
        public string Settlement { get; set; }

        /// <summary>
        /// Код ФИАС улицы.
        /// </summary>
        [DisplayName("Код ФИАС улицы"), JsonProperty("street_fias_id"), MemberDesignTimeVisibility(false)]
        public string StreetFiasId { get; set; }

        /// <summary>
        /// Код КЛАДР улицы.
        /// </summary>
        [DisplayName("Код КЛАДР улицы"), JsonProperty("street_kladr_id"), MemberDesignTimeVisibility(false)]
        public string StreetKladrId { get; set; }

        /// <summary>
        /// Улица с типом.
        /// </summary>
        [DisplayName("Улица с типом"), JsonProperty("street_with_type"), MemberDesignTimeVisibility(false)]
        public string StreetWithType { get; set; }

        /// <summary>
        /// Тип улицы (сокращенный).
        /// </summary>
        [DisplayName("Тип улицы (сокращенный)"), JsonProperty("street_type"), MemberDesignTimeVisibility(false)]
        public string StreetType { get; set; }

        /// <summary>
        /// Тип улицы.
        /// </summary>
        [DisplayName("Тип улицы"), JsonProperty("street_type_full"), MemberDesignTimeVisibility(false)]
        public string StreetTypeFull { get; set; }

        /// <summary>
        /// Улица.
        /// </summary>
        [MemberDesignTimeVisibility(false), DisplayName("Улица"), JsonProperty("street")]
        public string Street { get; set; }

        /// <summary>
        /// Код ФИАС дома.
        /// </summary>
        [DisplayName("Код ФИАС дома"), JsonProperty("house_fias_id"), MemberDesignTimeVisibility(false)]
        public string HouseFiasId { get; set; }

        /// <summary>
        /// Код КЛАДР дома.
        /// </summary>
        [DisplayName("Код КЛАДР дома"), JsonProperty("house_kladr_id"), MemberDesignTimeVisibility(false)]
        public string HouseKladrId { get; set; }

        /// <summary>
        /// Тип дома (сокращенный).
        /// </summary>
        [DisplayName("Тип дома (сокращенный)"), JsonProperty("house_type"), MemberDesignTimeVisibility(false)]
        public string HouseType { get; set; }

        /// <summary>
        /// Тип дома.
        /// </summary>
        [DisplayName("Тип дома"), JsonProperty("house_type_full"), MemberDesignTimeVisibility(false)]
        public string HouseTypeFull { get; set; }

        /// <summary>
        /// Дом.
        /// </summary>
        [DisplayName("Дом"), JsonProperty("house"), MemberDesignTimeVisibility(false)]
        public string House { get; set; }

        /// <summary>
        /// Тип корпуса/строения (сокращенный).
        /// </summary>
        [DisplayName("Тип корпуса/строения (сокращенный)"), JsonProperty("block_type"), MemberDesignTimeVisibility(false)]
        public string BlockType { get; set; }

        /// <summary>
        /// Тип корпуса/строения.
        /// </summary>
        [DisplayName("Тип корпуса/строения"), JsonProperty("block_type_full"), MemberDesignTimeVisibility(false)]
        public string BlockTypeFull { get; set; }

        /// <summary>
        /// Корпус/строение.
        /// </summary>
        [DisplayName("Корпус/строение"), JsonProperty("block"), MemberDesignTimeVisibility(false)]
        public string Block { get; set; }

        /// <summary>
        /// Тип квартиры (сокращенный).
        /// </summary>
        [DisplayName("Тип квартиры (сокращенный)"), JsonProperty("flat_type"), MemberDesignTimeVisibility(false)]
        public string FlatType { get; set; }

        /// <summary>
        /// Тип квартиры.
        /// </summary>
        [DisplayName("Тип квартиры"), JsonProperty("flat_type_full"), MemberDesignTimeVisibility(false)]
        public string FlatTypeFull { get; set; }

        /// <summary>
        /// Квартира.
        /// </summary>
        [DisplayName("Квартира"), JsonProperty("flat"), MemberDesignTimeVisibility(false)]
        public string Flat { get; set; }

        /// <summary>
        /// Площадь квартиры.
        /// </summary>
        [DisplayName("Площадь квартиры"), JsonProperty("flat_area"), MemberDesignTimeVisibility(false)]
        public string FlatArea { get; set; }

        /// <summary>
        /// Рыночная стоимость м².
        /// </summary>
        [DisplayName("Рыночная стоимость м²"), JsonProperty("square_meter_price"), MemberDesignTimeVisibility(false)]
        public string SquareMeterPrice { get; set; }

        /// <summary>
        /// Рыночная стоимость квартиры.
        /// </summary>
        [DisplayName("Рыночная стоимость квартиры"), JsonProperty("flat_price"), MemberDesignTimeVisibility(false)]
        public string FlatPrice { get; set; }

        /// <summary>
        /// Абонентский ящик.
        /// </summary>
        [DisplayName("Абонентский ящик"), JsonProperty("postal_box"), MemberDesignTimeVisibility(false)]
        public string PostalBox { get; set; }

        /// <summary>
        /// Код ФИАС.
        /// HOUSE.HOUSEGUID, если дом найден в ФИАС по точному совпадению.
        /// ADDROBJ.AOGUID в противном случае.
        /// </summary>
        [DisplayName("Код ФИАС"), JsonProperty("fias_id"), MemberDesignTimeVisibility(false)]
        public string FiasId { get; set; }

        /// <summary>
        /// Иерархический код адреса в ФИАС (СС+РРР+ГГГ+ППП+СССС+УУУУ+ДДДД).
        /// </summary>
        [DisplayName("Иерархический код адреса в ФИАС"), JsonProperty("fias_code"), MemberDesignTimeVisibility(false)]
        public string FiasCode { get; set; }

        /// <summary>
        /// Уровень детализации, до которого адрес найден в ФИАС.
        /// </summary>
        [DisplayName("Уровень детализации"), JsonProperty("fias_level"), MemberDesignTimeVisibility(false)]
        public string FiasLevel { get; set; }

        /// <summary>
        /// Признак актуальности адреса в ФИАС.
        /// </summary>
        [DisplayName("Признак актуальности адреса в ФИАС"), JsonProperty("fias_actuality_state"), MemberDesignTimeVisibility(false)]
        public string FiasActualityState { get; set; }

        /// <summary>
        /// Код КЛАДР.
        /// </summary>
        [DisplayName("Код КЛАДР"), JsonProperty("kladr_id"), MemberDesignTimeVisibility(false)]
        public string KladrId { get; set; }

        /// <summary>
        /// Идентификатор объекта в базе GeoNames. Для российских адресов не заполняется.
        /// </summary>
        [DisplayName("Идентификатор объекта в базе GeoNames"), JsonProperty("geoname_id"), MemberDesignTimeVisibility(false)]
        public string GeonameId { get; set; }

        /// <summary>
        /// Признак центра района или региона.
        /// </summary>
        [DisplayName("Признак центра района или региона"), JsonProperty("capital_marker"), MemberDesignTimeVisibility(false)]
        public string CapitalMarker { get; set; }

        /// <summary>
        /// Код ОКАТО.
        /// </summary>
        [DisplayName("Код ОКАТО"), JsonProperty("okato"), MemberDesignTimeVisibility(false)]
        public string Okato { get; set; }

        /// <summary>
        /// Код ОКТМО.
        /// </summary>
        [DisplayName("Код ОКТМО"), JsonProperty("oktmo"), MemberDesignTimeVisibility(false)]
        public string Oktmo { get; set; }

        /// <summary>
        /// Код ИФНС для физических лиц.
        /// </summary>
        [DisplayName("Код ИФНС для физических лиц"), JsonProperty("tax_office"), MemberDesignTimeVisibility(false)]
        public string TaxOffice { get; set; }

        /// <summary>
        /// Код ИФНС для организаций.
        /// </summary>
        [DisplayName("Код ИФНС для организаций"), JsonProperty("tax_office_legal"), MemberDesignTimeVisibility(false)]
        public string TaxOfficeLegal { get; set; }

        /// <summary>
        /// Часовой пояс.
        /// </summary>
        [DisplayName("Часовой пояс"), JsonProperty("timezone"), MemberDesignTimeVisibility(false)]
        public string Timezone { get; set; }

        /// <summary>
        /// Координаты: широта.
        /// </summary>
        [DisplayName("Широта"), JsonProperty("geo_lat"), MemberDesignTimeVisibility(false)]
        public string GeoLat { get; set; }

        /// <summary>
        /// Координаты: долгота.
        /// </summary>
        [DisplayName("Долгота"), JsonProperty("geo_lon"), MemberDesignTimeVisibility(false)]
        public string GeoLon { get; set; }

        /// <summary>
        /// Внутри кольцевой?
        /// </summary>
        [DisplayName("Внутри кольцевой?"), JsonProperty("beltway_hit"), MemberDesignTimeVisibility(false)]
        public string BeltwayHit { get; set; }

        /// <summary>
        /// Расстояние от кольцевой в километрах.
        /// </summary>
        [DisplayName("Расстояние от кольцевой в километрах"), JsonProperty("beltway_distance"), MemberDesignTimeVisibility(false)]
        public string BeltwayDistance { get; set; }

        /// <summary>
        /// Код точности координат.
        /// </summary>
        [DisplayName("Код точности координат:"), JsonProperty("qc_geo"), MemberDesignTimeVisibility(false)]
        public string QcGeo { get; set; }        
        
        [DisplayName(""), JsonProperty("qc_complete"), MemberDesignTimeVisibility(false)]
        private string qc_complete { get; set; }

        [DisplayName(""), JsonProperty("qc_house"), MemberDesignTimeVisibility(false)]
        private string qc_house { get; set; }

        /// <summary>
        /// Список исторических названий объекта нижнего уровня.
        /// Если подсказка до улицы — это прошлые названия этой улицы, если до города — города.
        /// </summary>
        [MemberDesignTimeVisibility(false), DisplayName("Список исторических названий объекта нижнего уровня"), JsonProperty("history_values")]
        private IEnumerable<string> HistoryValues { get; set; }

        [MemberDesignTimeVisibility(false), DisplayName("unparsed_parts"), JsonProperty("unparsed_parts")]
        private string unparsed_parts { get; set; }

        [MemberDesignTimeVisibility(false), DisplayName("source"), JsonProperty("source")]
        private string source { get; set; }

        [MemberDesignTimeVisibility(false), DisplayName("qc"), JsonProperty("qc")]
        private string qc { get; set; }        
    }
}