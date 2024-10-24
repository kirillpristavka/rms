using System;
using System.Web;

namespace RMS.Parser.Core.Models.YandexDisk
{
    public class Base
    {
        public Base(string requestMessage)
        {
            SetFileName(requestMessage);
        }

        public string FileName { get; private set; }
        public byte[] Obj { get; private set; }

        private void SetFileName(string requestMessage)
        {
            var uri = new Uri(requestMessage);
            FileName = HttpUtility.ParseQueryString(uri.Query).Get("fileName");
        }

        public void SetObj(byte[] obj)
        {
            Obj = obj;
        }

        public override string ToString()
        {
            return FileName;
        }
    }
}
