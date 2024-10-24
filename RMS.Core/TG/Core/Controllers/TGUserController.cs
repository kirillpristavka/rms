using RMS.Core.TG.Core.Models;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RMS.Core.TG.Core.Controllers
{
    public static class TGUserController
    {
        private static HttpClient httpClient;
        private static string telegramUrl = "https://telegram.me/";
        
        public static HttpClient GetHttpClient()
        {
            if (httpClient != null)
            {
                return httpClient;
            }

            httpClient = new HttpClient();
            return httpClient;
        }

        public async static Task<byte[]> GetUserAvatar(TGUser tgUser)
        {
            if (tgUser != null && !string.IsNullOrWhiteSpace(tgUser.UserName))
            {
                httpClient = GetHttpClient();
                var url = $"{telegramUrl}{tgUser.UserName}";
                var response = await httpClient.GetAsync(url);

                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var src = GetSrc(await response?.Content?.ReadAsStringAsync());
                    if (!string.IsNullOrWhiteSpace(src))
                    {
                        var image = await httpClient.GetByteArrayAsync(src);
                        if (image != null)
                        {
                            return image;
                        }
                    }                  
                }
            }
            
            return default;
        }

        private static string GetSrc(string html, string @class = "//img[@class='tgme_page_photo_image']")
        {
            if (!string.IsNullOrWhiteSpace(html))
            {
                HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                document.LoadHtml(html);

                var srcNode = document.DocumentNode.SelectSingleNode(@class);
                var src = srcNode?.Attributes?.FirstOrDefault(f => !string.IsNullOrWhiteSpace(f.Name) && f.Name.Equals("src"))?.Value;
                if (!string.IsNullOrWhiteSpace(src))
                {
                    return src;
                }
            }            

            return default;
        }
    }
}
