using Newtonsoft.Json;
using System;

namespace RMS.Parser.Core.Models.SbisRu
{
    public class BaseObject
    {
        public BaseObject(string inn, string kpp)
        {
            if (inn.Length == 10 || inn.Length == 12)
            {
                Inn = inn;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(inn));
            }

            Kpp = kpp;
        }

        [JsonProperty("inn")]
        public string Inn { get; set; }

        [JsonProperty("kpp")]
        public string Kpp { get; set; }

        public string GetJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public override string ToString()
        {
            var result = Inn;
            if (!string.IsNullOrWhiteSpace(Kpp))
            {
                result += $"/{Kpp}";
            }
            return result;
        }
    }
}
