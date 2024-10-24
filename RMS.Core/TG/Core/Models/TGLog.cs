using DevExpress.Xpo;
using System;
using System.Text;

namespace RMS.Core.TG.Core.Models
{
    public class TGLog : XPObject
    {
        public TGLog() { }
        public TGLog(Session session) : base(session) { }

        [Persistent]
        [DisplayName("Дата")]
        public DateTime Date { get; private set; }
        
        [DisplayName("Сообщение")]
        public string Text => ByteToString(Obj);

        [Persistent]
        public byte[] Obj { get; private set; }

        public void SetObj(string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                Obj = StringToByte(message);
            }
            else
            {
                Obj = null;
            }
        }

        public byte[] StringToByte(string msg)
        {
            if (string.IsNullOrWhiteSpace(msg))
            {
                return default;
            }
            else
            {
                return Encoding.Default.GetBytes(msg);
            }
        }
        
        public string ByteToString(byte[] msg)
        {
            if (msg == null)
            {
                return default;
            }
            else
            {
                return Encoding.Default.GetString(msg);
            }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Date = DateTime.Now;
        }
        
        protected override void OnSaving()
        {
            if (string.IsNullOrWhiteSpace(Text))
            {
                return;
            }

            base.OnSaving();
        }
    }
}
