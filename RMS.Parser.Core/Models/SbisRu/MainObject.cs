using Newtonsoft.Json;

namespace RMS.Parser.Core.Models.SbisRu
{
    /// <summary>
    /// Главный объект.
    /// </summary>
    public class MainObject
    {        
        [JsonProperty("name")]
        public string Name;

        [JsonProperty("short_name")]
        public string ShortName;

        [JsonProperty("status")]
        public string Status { get; set; }

        public string GetJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
