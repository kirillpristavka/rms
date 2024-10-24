using Newtonsoft.Json;
using System.Collections.Generic;

namespace RMS.Parser.Core.Models.WebsborGksRu
{
    /// <summary>
    /// Главный объект.
    /// </summary>
    public class MainObject
    {
        [JsonProperty("id")]
        public string Id;

        [JsonProperty("type")]
        public double Type;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("short_name")]
        public string ShortName;

        [JsonProperty("okpo_ul")]
        public string OkpoUl;

        [JsonProperty("okpo")]
        public string Okpo;

        [JsonProperty("date_reg")]
        public string DateReg;

        [JsonProperty("reg_resolution_authority")]
        public object RegResolutionAuthority;

        [JsonProperty("reg_resolution_date")]
        public object RegResolutionDate;

        [JsonProperty("reg_resolution_num")]
        public object RegResolutionNum;

        [JsonProperty("tosp_id")]
        public object TospId;

        [JsonProperty("update_type")]
        public double UpdateType;

        [JsonProperty("record_comment")]
        public object RecordComment;

        [JsonProperty("inn")]
        public string Inn;

        [JsonProperty("ogrn")]
        public string Ogrn;

        [JsonProperty("address_fact")]
        public string AddressFact;

        [JsonProperty("okato_reg")]
        public SingleObject OkatoReg;

        [JsonProperty("oktmo_reg")]
        public SingleObject OktmoReg;

        [JsonProperty("okato_fact")]
        public SingleObject OkatoFact;

        [JsonProperty("oktmo_fact")]
        public SingleObject OktmoFact;

        [JsonProperty("okved2_fact")]
        public SingleObject Okved2Fact;

        [JsonProperty("okopf")]
        public SingleObject Okopf;

        [JsonProperty("okfs")]
        public SingleObject Okfs;

        [JsonProperty("okogu")]
        public SingleObject Okogu;

        [JsonProperty("forms")]
        public List<ObjectForms> ObjectForms { get; private set; } = new List<ObjectForms>();

        public void AddRangeObjectForms(List<ObjectForms> obj)
        {
            if (obj != null && obj.Count > 0)
            {
                ObjectForms.AddRange(obj);
            }
        }

        public string GetJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
