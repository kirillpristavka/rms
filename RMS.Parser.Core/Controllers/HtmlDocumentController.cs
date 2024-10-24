using HtmlAgilityPack;

namespace RMS.Parser.Core.Controllers
{
    public static class HtmlDocumentController
    {
        private static HtmlDocument htmlDocument;

        public static HtmlDocument GetHtmlDocument()
        {
            if (htmlDocument is null)
            {
                htmlDocument = new HtmlDocument();
            }

            return htmlDocument;
        }
    }
}
