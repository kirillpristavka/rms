using RMS.Parser.Core.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RMS.Parser.Core.Models.YandexDisk.Controllers
{
    public static class YandexDiskController
    {
        private async static Task<HttpResponseMessage> GetLinkYandexResponseAsync(string url)
        {
            return await RequestController.GetHttpClient()?.GetAsync($"https://getfile.dokpub.com/yandex/get/{url}");
        }

        public static IEnumerable<string> GetLinks(string html)
        {
            var htmlDocument = HtmlDocumentController.GetHtmlDocument();
            htmlDocument.LoadHtml(html);

            var nodes = htmlDocument.DocumentNode.SelectNodes("//a[@class='narod-attachment']");

            var links = new List<string>();
            foreach (var node in nodes)
            {
                if (node.Attributes != null && node.Attributes.Contains("href"))
                {
                    var attribute = node.Attributes.FirstOrDefault(f => f.Name == "href");
                    if (attribute != null)
                    {
                        links.Add(attribute.Value);
                    }
                }
            }

            return links;
        }

        public async static Task<Base> GetYandexFileAsync(string url)
        {
            try
            {
                var response = await GetLinkYandexResponseAsync(url);
                var requestMessage = response?.RequestMessage?.ToString();

                if (string.IsNullOrWhiteSpace(requestMessage))
                {
                    return default;
                }

                var @base = new Base(requestMessage);
                var byteArray = await response?.Content?.ReadAsByteArrayAsync();
                @base.SetObj(byteArray);

                return @base;
            }
            catch (Exception ex)
            {
                RMS.Parser.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                return default;
            }
        }
    }
}
