using Newtonsoft.Json;
using RMS.Parser.Core.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMS.Parser.Core.Models.WebsborGksRu.Controller
{
    public static class MainObjectController
    {
        private static readonly string url = "https://websbor.rosstat.gov.ru/webstat/api/gs/organizations";
        
        public async static Task<MainObject> GetAsync(string inn)
        {
            var baseObject = new BaseObject(inn);
            var json = baseObject.GetJson();

            var result = await RequestController.GetResponse(url, "POST", json);
            if (!string.IsNullOrWhiteSpace(result))
            {
                var mainObjectList = JsonConvert.DeserializeObject<List<MainObject>>(result);
                var mainObject = mainObjectList.FirstOrDefault(f => f.Inn == inn);
                if (mainObject != null)
                {
                    mainObject.AddRangeObjectForms(await ObjectFormsController.GetAsync(mainObject.Id));
                    return mainObject;
                }
            }
            
            return default;
        }
    }
}
