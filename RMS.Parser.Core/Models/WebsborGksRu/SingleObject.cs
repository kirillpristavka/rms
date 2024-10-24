using Newtonsoft.Json;
namespace RMS.Parser.Core.Models.WebsborGksRu
{
    /// <summary>
    /// Единый объект/
    /// </summary>
    public class SingleObject
    {
        [JsonProperty("id")]
        public double Id;

        [JsonProperty("code")]
        public string Code;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("parent_id")]
        public object ParentId;
    }
}
