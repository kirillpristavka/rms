using HtmlAgilityPack;

namespace RMS.Parser.Core.Controllers
{
    public static class HtmlWebController
    {
        private static HtmlWeb htmlWeb;

        public static HtmlWeb GetHtmlWeb()
        {
            if (htmlWeb is null)
            {
                htmlWeb = new HtmlWeb();
            }

            return htmlWeb;
        }
    }
}
