using Newtonsoft.Json;
using System;

namespace RMS.Parser.Core.Models.WebsborGksRu
{
    public class BaseObject
    {
        public BaseObject(string inn)
        {
            if (inn.Length == 10 || inn.Length == 12)
            {
                Inn = inn;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(inn));
            }
        }

        [JsonProperty("okpo")]
        public string Okpo { get; set; }

        [JsonProperty("inn")]
        public string Inn { get; set; }

        [JsonProperty("ogrn")]
        public string Ogrn { get; set; }

        public string GetJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
