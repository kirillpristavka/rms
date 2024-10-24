using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace RMS.Parser.Core.Controllers
{
    public static class RequestController
    {
        private static HttpClient _httpClient;
        public static HttpClient GetHttpClient()
        {
            if (_httpClient is null)
            {
                _httpClient = new HttpClient();
            }
            
            return _httpClient;
        }        

        public async static Task<string> GetResponse(string url, string method, string json = default)         
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = method;

            if (!string.IsNullOrWhiteSpace(json))
            {
                using (var streamWriter = new StreamWriter(await httpWebRequest.GetRequestStreamAsync()))
                {
                    await streamWriter.WriteAsync(json);
                }
            }
            
            var httpResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync();            
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                return await streamReader.ReadToEndAsync();
            }
        }
    }
}
