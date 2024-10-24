using Newtonsoft.Json;
using RMS.Parser.Core.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RMS.Parser.Core.Models.WebsborGksRu.Controller
{
    public static class ObjectFormsController
    {        
        public async static Task<List<ObjectForms>> GetAsync(string id)
        {
            var result = await RequestController.GetResponse(GetUrl(id), "GET");
            if (!string.IsNullOrWhiteSpace(result))
            {
                return JsonConvert.DeserializeObject<List<ObjectForms>>(result);
            }
            
            return default;
        }
        
        private static string GetUrl(string id)
        {
            return $"https://websbor.gks.ru/webstat/api/gs/organizations/{id}/forms";
        }
    }
}
