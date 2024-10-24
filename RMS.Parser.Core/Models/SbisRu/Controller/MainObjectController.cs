using RMS.Parser.Core.Controllers;
using System.Threading.Tasks;

namespace RMS.Parser.Core.Models.SbisRu.Controller
{
    public static class MainObjectController
    {
        private static readonly string url = "https://sbis.ru/contragents";
        
        public async static Task<string> GetStatusByResponseAsync(string inn, string kpp = default)
        {
            var baseObject = new BaseObject(inn, kpp);
            return await RequestController.GetResponse($"{url}/{baseObject}", "GET");
        }

        public async static Task<string> GetStatusByWebHtmlAsync(string inn, string kpp = default)
        {
            var baseObject = new BaseObject(inn, kpp);
            var htmlDocument = await HtmlWebController.GetHtmlWeb().LoadFromWebAsync($"{url}/{baseObject}");
            var status = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='c-sbisru-CardStatus__closed']")?.InnerText;
            status = status?.Replace(",", "")?.Trim();
            return status;
        }
    }
}
