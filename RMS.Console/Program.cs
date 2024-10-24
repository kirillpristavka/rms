using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Console
{
    class Program
    {        
        static void Main(string[] args)
        {
            using (var webClient = new WebClient())
            {
                webClient.Headers["Content-Type"] = "application/json";
                webClient.Encoding = Encoding.UTF8;
                var json = "{\r\n    \"jsonrpc\": \"2.0\",\r\n    \"method\": \"СБИС.Аутентифицировать\",\r\n    \"params\": {\r\n        \"Параметр\": {\r\n            \"Логин\": \"3091349@algras.ru\",\r\n            \"Пароль\": \"3091349@algras.ru1!\"\r\n        }\r\n    },\r\n    \"id\": 0\r\n}";
                var response = webClient.UploadString($"https://online.sbis.ru/auth/service", "POST", json);
            }


            Task.Run(async () =>
            {
                try
                {
                    var mainObject1 = await Parser.Core.Models.SbisRu.Controller.MainObjectController.GetStatusByWebHtmlAsync("7706764674", "770601001");
                    var mainObject2 = await Parser.Core.Models.SbisRu.Controller.MainObjectController.GetStatusByWebHtmlAsync("890415962910");
                    var mainObject3 = await Parser.Core.Models.SbisRu.Controller.MainObjectController.GetStatusByWebHtmlAsync("7838093768");

                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine($"---> {System.DateTime.Now} {ex.Message}");
                }
            }).Wait();
        }

        private static string GetStringGivenSize(string str, int size = 10)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return default;
            }

            if (str.Length < size)
            {
                while (str.Length != size)
                {
                    str = $"0{str}";
                }
            }

            return str;
        }
    }
}
